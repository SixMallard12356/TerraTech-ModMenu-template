#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ManFreeSpace : Singleton.Manager<ManFreeSpace>
{
	public abstract class SafeArea
	{
		public float m_Radius;

		public abstract Vector3 Position { get; }

		public virtual Transform Transform => null;

		public SafeArea(float radius)
		{
			m_Radius = radius;
		}

		public bool ContainsPoint(Vector3 scenePos)
		{
			return (Position - scenePos).ToVector2XZ().sqrMagnitude < m_Radius * m_Radius;
		}

		public bool AreaOverlaps(Vector3 scenePos, float radius)
		{
			float num = m_Radius + radius;
			return (Position - scenePos).sqrMagnitude < num * num;
		}
	}

	public class TechSafeArea : SafeArea
	{
		public Transform m_Transform;

		public override Vector3 Position => m_Transform.position;

		public override Transform Transform => m_Transform;

		public TechSafeArea(Transform transform, float radius)
			: base(radius)
		{
			d.Assert(transform != null, "Null Transform passed in to TechSafeArea constructor!");
			m_Transform = transform;
		}
	}

	public class EncounterSafeArea : SafeArea
	{
		public Encounter m_LinkedEncounter;

		public WorldPosition m_Position;

		public override Vector3 Position => m_Position.ScenePosition;

		public override Transform Transform
		{
			get
			{
				if (!(m_LinkedEncounter != null))
				{
					return null;
				}
				return m_LinkedEncounter.transform;
			}
		}

		public EncounterSafeArea(WorldPosition position, float radius, Encounter linkedEncounter)
			: base(radius)
		{
			d.Assert(linkedEncounter != null, "Null encounter passed in to EncounterSafeArea constructor!");
			m_LinkedEncounter = linkedEncounter;
			m_Position = position;
		}
	}

	public class OrphanedSafeArea : SafeArea
	{
		public WorldPosition m_Position;

		public override Vector3 Position => m_Position.ScenePosition;

		public OrphanedSafeArea(WorldPosition position, float radius)
			: base(radius)
		{
			m_Position = position;
		}
	}

	[Serializable]
	public struct FreeSpaceParams
	{
		public delegate bool RejectFunction(Vector3 position, float radius, object context);

		public delegate bool CustomValidatorFunc(in FreeSpaceParams freeSpaceParams, Vector3 scenePos);

		public string m_DebugName;

		public Bitfield<ObjectTypes> m_ObjectsToAvoid;

		public bool m_AvoidLandmarks;

		public float m_CircleRadius;

		public float m_SearchRadiusMultiplier;

		public V3Serial m_CenterPos;

		public WorldPosition m_CenterPosWorld;

		public int m_CircleIndex;

		public ManSpawn.CameraSpawnConditions m_CameraSpawnConditions;

		public bool m_CheckSafeArea;

		public bool m_AllowSpawnInSceneryBlocker;

		public bool m_AllowUnloadedTiles;

		[JsonIgnore]
		public RejectFunction m_RejectFunc;

		[JsonIgnore]
		public object m_RejectFuncContext;

		[JsonIgnore]
		public CustomValidatorFunc CustomValidator;

		public bool m_SilentFailIfNoSpaceFound;

		public bool IsValid => m_ObjectsToAvoid != null;
	}

	[Serializable]
	public class FreeSpaceEnumeratorParams
	{
		public bool m_FoundPos;

		public WorldPosition m_Pos;

		public int m_IteratorCount;

		public FreeSpaceEnumeratorParams(bool found, WorldPosition pos, int iteratorCount)
		{
			m_FoundPos = found;
			m_Pos = pos;
			m_IteratorCount = iteratorCount;
		}
	}

	[Serializable]
	private struct ReservedSpace
	{
		public WorldPosition reservedPosition;

		public float reservedRadius;

		public ObjectTypes reservationType;
	}

	public class FreeSpaceDebug
	{
		public float m_Radius;

		public WorldPosition m_Location;

		public bool m_Valid;

		public float m_TimeStarted;
	}

	[SerializeField]
	private int m_MaxSpawnFreeSpaceFrames = 30;

	[SerializeField]
	[Tooltip("Minimum Diameter of the distance between search point centres - in case search area is very small")]
	private float m_MinSpawnFreeSpaceCircleDiameter = 5f;

	[Tooltip("No-spawn radius around friendly techs and anchored base units")]
	[SerializeField]
	private float m_SafeAreaRadius;

	[SerializeField]
	private Transform m_TestSpawnLocator;

	private List<FreeSpaceFinder> m_FreeSpaceFinderList = new List<FreeSpaceFinder>();

	private FreeSpaceFinder m_CurrentFreeSpaceFinder;

	private IEnumerator<FreeSpaceEnumeratorParams> m_FreeSpaceFinderEnumerator;

	private List<SafeArea> m_SafeAreas = new List<SafeArea>();

	private Dictionary<string, ReservedSpace> m_ReservedSpaces = new Dictionary<string, ReservedSpace>();

	private static RaycastHit[] s_SphereCastResult = new RaycastHit[1];

	protected static bool s_ReportInvalidSpawnPoints = true;

	private FreeSpaceFinder m_DebugSpaceFinder = new FreeSpaceFinder();

	private List<FreeSpaceDebug> m_DEBUG_FreeSpace = new List<FreeSpaceDebug>();

	public float DefaultSafeAreaRadius => m_SafeAreaRadius;

	public bool IsOverlappingSafeArea(Vector3 scenePos, float radius, Transform transToIgnore = null)
	{
		foreach (SafeArea safeArea in m_SafeAreas)
		{
			if ((transToIgnore == null || safeArea.Transform != transToIgnore) && safeArea.AreaOverlaps(scenePos, radius))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsSafeArea(Vector3 scenePos)
	{
		foreach (SafeArea safeArea in m_SafeAreas)
		{
			if (safeArea.ContainsPoint(scenePos))
			{
				return true;
			}
		}
		return false;
	}

	public void AddSafeArea(SafeArea area)
	{
		m_SafeAreas.Add(area);
	}

	public void RemoveSafeArea(SafeArea area)
	{
		bool flag = false;
		for (int num = m_SafeAreas.Count - 1; num >= 0; num--)
		{
			if (m_SafeAreas[num] == area)
			{
				if (flag)
				{
					d.LogError("Found multiple added safe area entries for the same safe area! " + area);
				}
				m_SafeAreas.RemoveAt(num);
				flag = true;
			}
		}
	}

	public void RemoveSafeAreas(Func<SafeArea, bool> removeSafeAreaPredicate)
	{
		for (int num = m_SafeAreas.Count - 1; num >= 0; num--)
		{
			if (removeSafeAreaPredicate(m_SafeAreas[num]))
			{
				m_SafeAreas.RemoveAt(num);
			}
		}
	}

	public void SearchForFreeSpace(FreeSpaceFinder freeSpaceFinder)
	{
		ref readonly FreeSpaceParams freeSpaceParams = ref freeSpaceFinder.FreeSpaceParams;
		Vector3 scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(freeSpaceParams.m_CenterPosWorld.ScenePosition);
		if (IsSpawnPosValid(in freeSpaceParams, scenePos))
		{
			WorldPosition worldPosition = WorldPosition.FromScenePosition(in scenePos);
			FreeSpaceEnumeratorParams searchParams = new FreeSpaceEnumeratorParams(found: true, worldPosition, 0);
			freeSpaceFinder.UpdateSearchParameters(searchParams);
			freeSpaceFinder.FreeSpaceFound(worldPosition);
		}
		else
		{
			m_FreeSpaceFinderList.Add(freeSpaceFinder);
		}
	}

	public void ResumeFreeSpaceSearch(FreeSpaceFinder freeSpaceFinder)
	{
		m_FreeSpaceFinderList.Add(freeSpaceFinder);
	}

	public void CancelFreeSpaceSearch(FreeSpaceFinder freeSpaceFinder)
	{
		m_FreeSpaceFinderList.Remove(freeSpaceFinder);
	}

	public bool IsSpawnPosValid(in FreeSpaceParams freeSpaceParams, Vector3 scenePos)
	{
		if (freeSpaceParams.CustomValidator == null)
		{
			return DefaultPositionValidator(in freeSpaceParams, scenePos);
		}
		return freeSpaceParams.CustomValidator(in freeSpaceParams, scenePos);
	}

	public bool DefaultPositionValidator(in FreeSpaceParams freeSpaceParams, Vector3 scenePos)
	{
		bool flag = false;
		float circleRadius = freeSpaceParams.m_CircleRadius;
		bool flag2 = Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(scenePos);
		if (!flag && !freeSpaceParams.m_AllowUnloadedTiles)
		{
			flag = !flag2;
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to missing tile at position: " + scenePos.ToString("F4"));
			}
		}
		if (!flag)
		{
			flag = freeSpaceParams.m_CheckSafeArea && IsSafeArea(scenePos);
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to invalid SAFE area at position: " + scenePos.ToString("F4"));
			}
		}
		if (!flag && freeSpaceParams.m_CameraSpawnConditions != ManSpawn.CameraSpawnConditions.Anywhere)
		{
			bool flag3 = Singleton.Manager<CameraManager>.inst.IsPosInsideCamFrustrum(scenePos);
			flag = (freeSpaceParams.m_CameraSpawnConditions == ManSpawn.CameraSpawnConditions.OffCamera && flag3) || (freeSpaceParams.m_CameraSpawnConditions == ManSpawn.CameraSpawnConditions.OnCamera && !flag3);
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to Frustum check!");
			}
		}
		if (!flag)
		{
			flag = !freeSpaceParams.m_AllowSpawnInSceneryBlocker && Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, scenePos, circleRadius);
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to SceneryBlocker at position: " + scenePos.ToString("F4") + " Radius:" + circleRadius.ToString("F2"));
			}
		}
		bool includeTriggers = false;
		int pickerMask = Singleton.Manager<ManVisible>.inst.VisiblePickerMask | Globals.inst.layerSceneryCoarse.mask;
		if (!flag)
		{
			flag = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(scenePos, circleRadius, freeSpaceParams.m_ObjectsToAvoid, includeTriggers, pickerMask).Any();
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to Visibles Touching Radius at position: " + scenePos.ToString("F4") + " Radius:" + circleRadius.ToString("F2"));
			}
		}
		flag = flag || OverlapsReservedSpace(scenePos, circleRadius, freeSpaceParams.m_ObjectsToAvoid);
		if (!flag && freeSpaceParams.m_AvoidLandmarks)
		{
			flag = Physics.SphereCastNonAlloc(scenePos + Vector3.up * 100f, circleRadius, Vector3.down, s_SphereCastResult, 100f, Globals.inst.layerLandmark.mask, QueryTriggerInteraction.Ignore) > 0;
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to intersection with Landmark at position: " + scenePos.ToString("F4") + " Radius:" + circleRadius.ToString("F2"));
			}
		}
		if (!flag && freeSpaceParams.m_RejectFunc != null)
		{
			flag = freeSpaceParams.m_RejectFunc(scenePos, circleRadius, freeSpaceParams.m_RejectFuncContext);
			if (flag && s_ReportInvalidSpawnPoints)
			{
				d.Log("ManSpawn.IsSpawnPosValid - Position FAILED due to RejectionFunction at position:" + scenePos.ToString("F4") + " Radius:" + circleRadius.ToString("F2") + " RejectFunctName=" + freeSpaceParams.m_RejectFunc.Method.Name);
			}
		}
		return !flag;
	}

	public void AddReservedSpace(Vector3 position, float radius, ObjectTypes type, string uniqueName)
	{
		if (!m_ReservedSpaces.ContainsKey(uniqueName))
		{
			ReservedSpace value = new ReservedSpace
			{
				reservedPosition = WorldPosition.FromScenePosition(in position),
				reservedRadius = radius,
				reservationType = type
			};
			m_ReservedSpaces.Add(uniqueName, value);
		}
		else
		{
			d.LogErrorFormat("ManFreeSpace.AddReservedSpace - Already have free space reserved with ID {0}", uniqueName);
		}
	}

	public void RemoveReservedSpace(string uniqueName)
	{
		m_ReservedSpaces.Remove(uniqueName);
	}

	private bool OverlapsReservedSpace(Vector3 scenePos, float radius, Bitfield<ObjectTypes> types)
	{
		bool result = false;
		foreach (ReservedSpace value in m_ReservedSpaces.Values)
		{
			if (types.Contains((int)value.reservationType))
			{
				float num = value.reservedRadius + radius;
				if ((value.reservedPosition.ScenePosition - scenePos).sqrMagnitude < num * num)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private IEnumerator<FreeSpaceEnumeratorParams> FindFreeSpaceIterator(FreeSpaceFinder finder)
	{
		FreeSpaceParams freeSpaceParams = finder.FreeSpaceParams;
		float circleRadius = freeSpaceParams.m_CircleRadius;
		Vector3 originScene = ((!freeSpaceParams.m_CheckSafeArea) ? freeSpaceParams.m_CenterPosWorld.ScenePosition : FindPositionOutsideOfSafeAreas(freeSpaceParams.m_CenterPosWorld.ScenePosition));
		float num = Mathf.Max(circleRadius * 2f, m_MinSpawnFreeSpaceCircleDiameter);
		if (freeSpaceParams.m_SearchRadiusMultiplier > 0f)
		{
			num *= freeSpaceParams.m_SearchRadiusMultiplier;
		}
		if (finder.LocationEnum == null)
		{
			finder.LocationEnum = GenerateSpawnPositions(originScene, num, freeSpaceParams.m_CircleIndex);
		}
		bool generatedSpawnPoint = false;
		int tryCount = 0;
		while (!generatedSpawnPoint && tryCount < m_MaxSpawnFreeSpaceFrames && finder.LocationEnum.MoveNext())
		{
			Vector3 scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(finder.LocationEnum.Current.ScenePosition);
			bool num2 = IsSpawnPosValid(in freeSpaceParams, scenePos);
			WorldPosition pos = WorldPosition.FromScenePosition(in scenePos);
			FreeSpaceEnumeratorParams freeSpaceEnumeratorParams = new FreeSpaceEnumeratorParams(num2, pos, tryCount);
			if (num2)
			{
				yield return freeSpaceEnumeratorParams;
				generatedSpawnPoint = true;
			}
			else
			{
				yield return freeSpaceEnumeratorParams;
			}
			int num3 = tryCount + 1;
			tryCount = num3;
		}
	}

	private IEnumerator<WorldPosition> GenerateSpawnPositions(Vector3 originScene, float circleDiameter, int startingCircleIndex)
	{
		int curCircleIndex = startingCircleIndex;
		if (curCircleIndex == 0)
		{
			yield return WorldPosition.FromScenePosition(in originScene);
			int num = curCircleIndex + 1;
			curCircleIndex = num;
		}
		while (true)
		{
			int numPointsOnThisCircle = curCircleIndex * 6;
			Vector3 offsetPos = Vector3.forward * circleDiameter * curCircleIndex;
			Quaternion rot = Quaternion.AngleAxis(360f / (float)numPointsOnThisCircle, Vector3.up);
			int num;
			for (int pointInd = 0; pointInd < numPointsOnThisCircle; pointInd = num)
			{
				yield return WorldPosition.FromScenePosition(originScene + offsetPos);
				offsetPos = rot * offsetPos;
				num = pointInd + 1;
			}
			num = curCircleIndex + 1;
			curCircleIndex = num;
		}
	}

	private void FindFreeSpaceUpdate()
	{
		if (m_CurrentFreeSpaceFinder != null)
		{
			if (m_CurrentFreeSpaceFinder.IsValid && m_FreeSpaceFinderEnumerator != null)
			{
				FreeSpaceEnumeratorParams freeSpaceEnumeratorParams = null;
				bool flag = false;
				if (m_FreeSpaceFinderEnumerator.MoveNext())
				{
					freeSpaceEnumeratorParams = m_FreeSpaceFinderEnumerator.Current;
					m_CurrentFreeSpaceFinder.UpdateSearchParameters(freeSpaceEnumeratorParams);
					if (freeSpaceEnumeratorParams.m_FoundPos)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					WorldPosition? position = null;
					if (freeSpaceEnumeratorParams != null && freeSpaceEnumeratorParams.m_FoundPos)
					{
						position = freeSpaceEnumeratorParams.m_Pos;
					}
					m_CurrentFreeSpaceFinder.FreeSpaceFound(position);
					m_CurrentFreeSpaceFinder = null;
					m_FreeSpaceFinderEnumerator = null;
				}
			}
			else
			{
				m_CurrentFreeSpaceFinder = null;
				m_FreeSpaceFinderEnumerator = null;
			}
		}
		else if (m_FreeSpaceFinderList.Count > 0)
		{
			bool flag2 = false;
			do
			{
				m_CurrentFreeSpaceFinder = m_FreeSpaceFinderList[0];
				m_FreeSpaceFinderList.RemoveAt(0);
				flag2 = m_CurrentFreeSpaceFinder.FreeSpaceParams.IsValid;
			}
			while (m_FreeSpaceFinderList.Count > 0 && !flag2);
			if (flag2)
			{
				m_FreeSpaceFinderEnumerator = FindFreeSpaceIterator(m_CurrentFreeSpaceFinder);
			}
		}
	}

	private static Bounds BuildBoundsFromSphere(Vector3 position, float radius)
	{
		Bounds result = default(Bounds);
		Vector3 vector = new Vector3(radius, radius, radius);
		result.min = position - vector;
		result.max = position + vector;
		return result;
	}

	private Bounds FindBoundsEncompassingSafeAreas(Vector3 scenePos)
	{
		Bounds bounds = new Bounds(scenePos, Vector3.zero);
		Bounds bounds2;
		do
		{
			bounds2 = bounds;
			foreach (SafeArea safeArea in m_SafeAreas)
			{
				if (bounds.IntersectSphere(safeArea.Position, safeArea.m_Radius))
				{
					bounds.Encapsulate(BuildBoundsFromSphere(safeArea.Position, safeArea.m_Radius));
				}
			}
		}
		while (!(bounds2 == bounds));
		return bounds;
	}

	private static Vector3 MovePointOutsideOfBounds(Vector3 position, Bounds bounds)
	{
		if (bounds.Contains(position))
		{
			Vector3 vector = position - bounds.center;
			float num = bounds.size.x / 2f - Math.Abs(vector.x);
			float num2 = bounds.size.z / 2f - Math.Abs(vector.z);
			if (num <= num2)
			{
				float x = ((vector.x >= 0f) ? num : (0f - num));
				return position + new Vector3(x, 0f, 0f);
			}
			float z = ((vector.z >= 0f) ? num2 : (0f - num2));
			return position + new Vector3(0f, 0f, z);
		}
		return position;
	}

	private Vector3 FindNearestIntersectionWithSafeAreas(Vector3 raySceneStart, Vector3 raySceneEnd)
	{
		Vector3 vector = raySceneEnd - raySceneStart;
		if (Math.Abs(vector.x) > float.Epsilon || Math.Abs(vector.y) > float.Epsilon || Math.Abs(vector.z) > float.Epsilon)
		{
			float magnitude = vector.magnitude;
			Vector3 vector2 = vector / magnitude;
			float num = float.MaxValue;
			foreach (SafeArea safeArea in m_SafeAreas)
			{
				if (Maths.IntersectRayWithSphere(raySceneStart, vector2, magnitude, safeArea.Position, safeArea.m_Radius, out var intersectDist) && intersectDist < num)
				{
					num = intersectDist;
				}
			}
			return raySceneStart + num * vector2;
		}
		return raySceneStart;
	}

	private Vector3 FindPositionOutsideOfSafeAreas(Vector3 scenePos)
	{
		bool flag = false;
		foreach (SafeArea safeArea in m_SafeAreas)
		{
			if (safeArea.ContainsPoint(scenePos))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Bounds bounds = FindBoundsEncompassingSafeAreas(scenePos);
			Vector3 raySceneStart = MovePointOutsideOfBounds(scenePos, bounds);
			return FindNearestIntersectionWithSafeAreas(raySceneStart, scenePos);
		}
		return scenePos;
	}

	private void OnModeCleanUp(Mode currentMode)
	{
		m_SafeAreas.Clear();
		m_CurrentFreeSpaceFinder = null;
		m_FreeSpaceFinderEnumerator = null;
		m_FreeSpaceFinderList.Clear();
		m_ReservedSpaces.Clear();
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanUp);
	}

	private void OnDestroy()
	{
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Unsubscribe(OnModeCleanUp);
	}

	private void Update()
	{
		FindFreeSpaceUpdate();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.2f, 0.9f, 0.2f);
		foreach (SafeArea safeArea in m_SafeAreas)
		{
			Gizmos.DrawWireSphere(safeArea.Position, safeArea.m_Radius);
		}
		foreach (ReservedSpace value in m_ReservedSpaces.Values)
		{
			Gizmos.color = new Color(0.8f, 0.3f, 0.6f);
			Gizmos.DrawWireSphere(value.reservedPosition.ScenePosition, value.reservedRadius);
		}
		for (int num = m_DEBUG_FreeSpace.Count - 1; num >= 0; num--)
		{
			FreeSpaceDebug freeSpaceDebug = m_DEBUG_FreeSpace[num];
			Gizmos.color = (freeSpaceDebug.m_Valid ? new Color(0.1f, 0.8f, 0.1f) : new Color(0.8f, 0.1f, 0.1f));
			Gizmos.DrawWireSphere(freeSpaceDebug.m_Location.ScenePosition, freeSpaceDebug.m_Radius);
			if (freeSpaceDebug.m_TimeStarted < Time.time - 5f)
			{
				m_DEBUG_FreeSpace.RemoveAt(num);
			}
		}
	}
}
