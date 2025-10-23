#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleAnchor : Module
{
	public enum SpecialAnchorLocation
	{
		None,
		ThermalVents,
		ResourceReservoirs
	}

	public enum OnAttachBehaviour
	{
		Never,
		FirstOrSameAs,
		AlwaysTry
	}

	public enum APBlockSource
	{
		MeshRendererBounds = 1,
		CollisionBounds
	}

	[Serializable]
	public new class SerialData : SerialData<SerialData>
	{
		public bool anchored;
	}

	[SerializeField]
	public float m_MaxAngularVelocity;

	[SerializeField]
	public float m_MaxTorque;

	[SerializeField]
	public float m_BrakeTorque = 1000f;

	[SerializeField]
	public bool m_ForceHorizontal = true;

	[SerializeField]
	public bool m_IsSkyAnchor;

	[SerializeField]
	public Transform m_SkyAnchorBeamAttachPoint;

	[SerializeField]
	public Transform m_SkyAnchorFirePoint;

	[SerializeField]
	public SpecialAnchorLocation m_VisibleAnchorExceptions;

	[SerializeField]
	[HideInInspector]
	public ModuleAnimator m_AnimatorController;

	[SerializeField]
	[EnumFlag]
	private APBlockSource m_AnchorAPBlockingBehaviour = APBlockSource.MeshRendererBounds;

	public Event<ModuleAnchor> AnchorEvent;

	private AnimatorBool m_DeployBool = new AnimatorBool("Deploy");

	private Quaternion m_OriginalAnchorGeomRotation;

	[SerializeField]
	[HideInInspector]
	private BlockAnchor m_Anchor;

	[HideInInspector]
	[SerializeField]
	private int[] m_APsBlockedByAnchor;

	[HideInInspector]
	[SerializeField]
	private Vector3[] m_AnchorAttachPositionsLocal;

	private static RaycastHit[] s_AnchorTestRaycastResults;

	private static RaycastHitSortComparer s_RaycastResultsSortComparer;

	private static List<Collider> s_CollidersList;

	private static List<Vector3> s_AnchorAttachPositions;

	private static List<int> s_OverlappingAPIndices;

	private bool m_HasBeenRestored;

	private Quaternion m_PreRotateRot;

	private bool m_AnchorBlockedStateDirty = true;

	private bool m_AnchorBlockedByOtherBlocksOnTech;

	private float m_CloggedTorque;

	private static List<Renderer> s_Renderers;

	private static Collider[] _s_AnchorGeometryTestColliders;

	public bool IsAnchored { get; private set; }

	public bool AnchorGeometryActive => m_Anchor.m_AnchorGeometry.activeInHierarchy;

	public static OnAttachBehaviour AnchorOnAttach { get; set; }

	public static bool AnchorOnAttachSnap { get; set; }

	public bool AllowsRotation => m_MaxAngularVelocity != 0f;

	public float MaxTorque => m_MaxTorque;

	public float BrakeTorque => m_BrakeTorque;

	public float CloggedTorque => Mathf.Min(m_CloggedTorque, m_MaxTorque);

	public float MaxAngVel => m_MaxAngularVelocity;

	public Vector3 GroundPoint => m_Anchor.GroundPoint;

	public bool IsSkyAnchor => m_IsSkyAnchor;

	public Transform SkyAnchorBeamAttachPoint => m_SkyAnchorBeamAttachPoint;

	public Transform SkyAnchorFirePoint => m_SkyAnchorFirePoint;

	public BlockAnchor Anchor => m_Anchor;

	public bool SnapGroundPositionUpwards()
	{
		if (MultiplayerReject())
		{
			return false;
		}
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Anchor.GroundPoint) - m_Anchor.GroundPoint;
		if (vector.y > 0f)
		{
			base.block.trans.position += vector;
			return true;
		}
		return false;
	}

	public void UnAnchorFromGround(bool playAnim)
	{
		ActivateAnchorGeometry(active: false, enableCollision: false, playAnim);
		IsAnchored = false;
		if (base.block.tank.Anchors.IsAnchored(this))
		{
			base.block.tank.Anchors.RemoveAnchor(this);
			base.block.tank.NotifyAnchor(this, anchored: false);
			AnchorEvent.Send(this);
		}
	}

	public float HeightOffGroundForMaxAnchor()
	{
		return (Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Anchor.GroundPointInitial) - m_Anchor.GroundPointInitial).y;
	}

	public float HeightOffGroundForMaxExtension()
	{
		if (m_Anchor.m_MaxExtraReach <= 0f)
		{
			return 0f;
		}
		return Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Anchor.GroundPointInitial).y - m_Anchor.GroundPointInitial.y + m_Anchor.m_MaxExtraReach;
	}

	public bool WouldAnchorToGround(bool populatingTech = false)
	{
		if (MultiplayerReject())
		{
			return false;
		}
		if (m_IsSkyAnchor && !base.block.IsAttached)
		{
			return false;
		}
		if (Vector3.Dot(base.block.trans.up, Vector3.up) < 0.9f)
		{
			return false;
		}
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Anchor.GroundPointInitial);
		Vector3 vector2 = vector - m_Anchor.GroundPointInitial;
		if ((!populatingTech || !m_IsSkyAnchor) && (vector2.y > m_Anchor.m_SnapToleranceUp || vector2.y < 0f - m_Anchor.m_SnapToleranceDown))
		{
			return false;
		}
		if (base.block.IsAttached && !TechVolumeTest())
		{
			return false;
		}
		if (!RaycastGroundTest(vector))
		{
			return false;
		}
		if (!AnchorGeometryTest())
		{
			return false;
		}
		return true;
	}

	public void AnchorToGround(bool snapTechToGround = true, bool fromAfterTechPopulate = false)
	{
		d.Assert(base.block.tank, "Can't ground-attach a block that has no tank");
		if (!base.block.tank.IsAnchored && snapTechToGround && !m_IsSkyAnchor)
		{
			Quaternion quaternion = base.block.tank.beam.CalcHoverOrientation();
			if (m_ForceHorizontal)
			{
				base.block.tank.trans.rotation = quaternion;
			}
			else
			{
				Vector3 terrainNormal = Singleton.Manager<ManWorld>.inst.GetTerrainNormal(base.block.trans.position);
				Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.right, terrainNormal), terrainNormal) * quaternion;
				base.block.tank.trans.rotation = rotation;
			}
			Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Anchor.GroundPointInitial) - m_Anchor.GroundPointInitial;
			base.block.tank.trans.position = base.block.tank.trans.position + vector;
		}
		base.block.tank.Anchors.AddAnchor(this);
		ActivateAnchorGeometry(active: true, enableCollision: true, !fromAfterTechPopulate);
		IsAnchored = true;
		base.block.tank.NotifyAnchor(this, anchored: true, fromAfterTechPopulate);
		AnchorEvent.Send(this);
	}

	public void ActivateAnchorGeometry(bool active, bool enableCollision, bool playAnim = true)
	{
		if ((bool)m_AnimatorController)
		{
			m_AnimatorController.Set(m_DeployBool, active);
		}
		m_Anchor.Activate(active, playAnim);
		m_Anchor.EnableCollision(enableCollision);
	}

	public void InitRotation()
	{
		m_PreRotateRot = m_Anchor.m_AnchorGeometry.transform.rotation;
	}

	public void ResetStaticGeometry()
	{
		m_Anchor.m_AnchorGeometry.transform.rotation = m_PreRotateRot;
	}

	public void OnAnchorBlockDeath()
	{
		base.block.tank.Anchors.UnanchorSingle(this, playAnim: false);
	}

	public void SetCloggedTorque(float value)
	{
		m_CloggedTorque = value;
	}

	static ModuleAnchor()
	{
		s_AnchorTestRaycastResults = new RaycastHit[8];
		s_RaycastResultsSortComparer = new RaycastHitSortComparer();
		s_CollidersList = new List<Collider>();
		s_AnchorAttachPositions = new List<Vector3>();
		s_OverlappingAPIndices = new List<int>();
		s_Renderers = new List<Renderer>();
		_s_AnchorGeometryTestColliders = new Collider[8];
		AnchorOnAttachSnap = true;
	}

	private bool IsColliderBlockingAnchor(Collider col, bool ignoreOwnTech, out bool isTerrainCollider)
	{
		isTerrainCollider = false;
		if (col.gameObject.IsTerrain())
		{
			isTerrainCollider = true;
			return false;
		}
		Visible visible = Visible.FindVisibleUpwards(col);
		if (visible.IsNotNull())
		{
			if (visible == base.block.visible)
			{
				return false;
			}
			if (ignoreOwnTech && visible.type == ObjectTypes.Block && (object)visible.block.tank == base.block.tank)
			{
				return false;
			}
			if (visible.type == ObjectTypes.Block && (!visible.block.IsAttached || !visible.block.tank.Anchors.Fixed))
			{
				return false;
			}
			if (visible.type == ObjectTypes.Chunk)
			{
				return false;
			}
			return !IsAllowedToAnchorOnVisible(visible);
		}
		return true;
	}

	private bool RaycastGroundTest(Vector3 groundPosWorld)
	{
		float maxDistance = (base.block.centreOfMassWorld.y - groundPosWorld.y) * 2f;
		bool flag = false;
		int num = 0;
		while (!flag && num < m_AnchorAttachPositionsLocal.Length)
		{
			Vector3 origin = base.block.trans.TransformPoint(m_AnchorAttachPositionsLocal[num]);
			num++;
			int num2 = 0;
			do
			{
				if (num2 == s_AnchorTestRaycastResults.Length)
				{
					Array.Resize(ref s_AnchorTestRaycastResults, s_AnchorTestRaycastResults.Length * 2);
				}
				num2 = Physics.RaycastNonAlloc(origin, -base.block.trans.up, s_AnchorTestRaycastResults, maxDistance, -5, QueryTriggerInteraction.Ignore);
				d.Assert(num2 != 0, string.Format("ModuleAnchor.WouldAnchorToGround - Could not anchor Block {0}{1}: No hits with raycast.", base.block.name, (base.block.tank != null) ? (" on tech " + base.block.tank.name) : string.Empty));
			}
			while (num2 == s_AnchorTestRaycastResults.Length);
			if (num2 > 1)
			{
				Array.Sort(s_AnchorTestRaycastResults, 0, num2, s_RaycastResultsSortComparer);
			}
			for (int i = 0; i < num2; i++)
			{
				flag = IsColliderBlockingAnchor(s_AnchorTestRaycastResults[i].collider, ignoreOwnTech: false, out var isTerrainCollider);
				if (isTerrainCollider || flag)
				{
					break;
				}
			}
		}
		return !flag;
	}

	private bool AnchorGeometryTest()
	{
		int num = 0;
		int num2 = 0;
		bool activeSelf = Anchor.gameObject.activeSelf;
		Anchor.gameObject.SetActive(value: true);
		Collider[] anchorGeometryColliders = Anchor.m_AnchorGeometryColliders;
		foreach (Collider collider in anchorGeometryColliders)
		{
			int layerCollisionMask = PhysicsUtils.GetLayerCollisionMask(collider.gameObject.layer);
			Bounds bounds = collider.bounds;
			int num3 = 0;
			do
			{
				if (num3 == _s_AnchorGeometryTestColliders.Length)
				{
					Array.Resize(ref _s_AnchorGeometryTestColliders, _s_AnchorGeometryTestColliders.Length * 2);
				}
				num3 = Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents / 2f, _s_AnchorGeometryTestColliders, collider.transform.rotation, layerCollisionMask);
			}
			while (num3 == _s_AnchorGeometryTestColliders.Length);
			num2 += num3;
			for (int j = 0; j < num3; j++)
			{
				num++;
				Collider collider2 = _s_AnchorGeometryTestColliders[j];
				if (!Anchor.m_AnchorGeometryColliders.Contains(collider2) && IsColliderBlockingAnchor(collider2, ignoreOwnTech: true, out var _))
				{
					Anchor.gameObject.SetActive(activeSelf);
					return false;
				}
			}
		}
		Anchor.gameObject.SetActive(activeSelf);
		return true;
	}

	private bool TechVolumeTest()
	{
		if (m_AnchorBlockedStateDirty)
		{
			m_AnchorBlockedStateDirty = false;
			m_AnchorBlockedByOtherBlocksOnTech = false;
			Bounds blockBounds = base.block.tank.blockBounds;
			Vector3[] anchorAttachPositionsLocal = m_AnchorAttachPositionsLocal;
			foreach (Vector3 vector in anchorAttachPositionsLocal)
			{
				Vector3Int vector3Int = Vector3Int.RoundToInt(base.block.cachedLocalPosition + base.block.cachedLocalRotation * vector);
				int num = vector3Int.y - (int)blockBounds.min.y + 1;
				for (int j = 1; j < num; j++)
				{
					if (base.block.tank.blockman.GetBlockAtPosition(vector3Int + Vector3Int.down * j).IsNotNull())
					{
						m_AnchorBlockedByOtherBlocksOnTech = true;
						return false;
					}
				}
			}
		}
		if (m_AnchorBlockedByOtherBlocksOnTech)
		{
			return false;
		}
		return true;
	}

	private void FindAPsBlockedByAnchorGeom()
	{
		Bounds bounds = default(Bounds);
		IEnumerable<Bounds> enumerable = null;
		if ((m_AnchorAPBlockingBehaviour & APBlockSource.MeshRendererBounds) != 0)
		{
			m_Anchor.GetComponentsInChildren(includeInactive: true, s_Renderers);
			if (s_Renderers.Count > 0)
			{
				enumerable = from r in s_Renderers
					where !(r is ParticleSystemRenderer) && !(r is LineRenderer) && !(r is TrailRenderer)
					select r.bounds;
			}
		}
		if ((m_AnchorAPBlockingBehaviour & APBlockSource.CollisionBounds) != 0)
		{
			m_Anchor.GetComponentsInChildren(includeInactive: true, s_CollidersList);
			if (s_CollidersList.Count > 0)
			{
				IEnumerable<Bounds> enumerable2 = s_CollidersList.Select((Collider c) => c.bounds);
				enumerable = ((enumerable == null) ? enumerable2 : enumerable.Concat(enumerable2));
			}
		}
		if (enumerable != null)
		{
			IEnumerator<Bounds> enumerator = enumerable.GetEnumerator();
			enumerator.MoveNext();
			bounds = enumerator.Current;
			while (enumerator.MoveNext())
			{
				bounds.Encapsulate(enumerator.Current);
			}
			for (int num = 0; num < base.block.attachPoints.Length; num++)
			{
				IntVector3 intVector = base.block.attachPoints[num].LocalToAP().PadHalf();
				if (bounds.Contains(base.transform.TransformPoint(intVector)))
				{
					s_OverlappingAPIndices.Add(num);
				}
			}
			m_APsBlockedByAnchor = s_OverlappingAPIndices.ToArray();
			s_OverlappingAPIndices.Clear();
		}
		s_Renderers.Clear();
		s_CollidersList.Clear();
	}

	private void FindCellsConnectedToAnchorGeom()
	{
		m_Anchor.GetComponentsInChildren(includeInactive: true, s_CollidersList);
		Physics.SyncTransforms();
		for (int i = 0; i < base.block.filledCells.Length; i++)
		{
			Vector3Int vector3Int = base.block.filledCells[i];
			Vector3Int vector3Int2 = vector3Int + Vector3Int.down;
			if (base.block.filledCells.Contains<IntVector3>(vector3Int2))
			{
				continue;
			}
			Vector3 vector = base.transform.TransformPoint(vector3Int2);
			foreach (Collider s_Colliders in s_CollidersList)
			{
				if (s_Colliders.bounds.Contains(vector) && s_Colliders.ClosestPoint(vector) == vector)
				{
					s_AnchorAttachPositions.Add(vector3Int);
				}
			}
		}
		s_CollidersList.Clear();
		if (s_AnchorAttachPositions.Count == 0)
		{
			d.LogWarning("Anchor geometry for " + base.name + " failed to match to any filled cells to originate from! Adding anchor centre as ray origin");
			s_AnchorAttachPositions.Add(base.transform.InverseTransformPoint(m_Anchor.transform.position));
		}
		m_AnchorAttachPositionsLocal = s_AnchorAttachPositions.ToArray();
		s_AnchorAttachPositions.Clear();
	}

	private bool IsAllowedToAnchorOnVisible(Visible visible)
	{
		switch (m_VisibleAnchorExceptions)
		{
		case SpecialAnchorLocation.None:
			return false;
		case SpecialAnchorLocation.ThermalVents:
			return visible.resdisp != null && visible.resdisp.ThermalSource != null;
		case SpecialAnchorLocation.ResourceReservoirs:
			return visible.resdisp != null && visible.resdisp.ResourceReservoir != null;
		default:
			d.LogError("ModuleAnchor.IsAllowedToAnchorOnVisible Unsupported visible type exception " + m_VisibleAnchorExceptions);
			return false;
		}
	}

	private bool MultiplayerReject()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Globals.inst.Multiplayer_AnchorsOff)
		{
			if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.AllowCollaboration)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	private void OnAttached()
	{
		base.block.tank.Anchors.AddPossibleAnchor(this);
		if ((AnchorOnAttach == OnAttachBehaviour.AlwaysTry || (AnchorOnAttach == OnAttachBehaviour.FirstOrSameAs && (base.block.tank.blockman.blockCount == 1 || base.block.tank.IsAnchored))) && WouldAnchorToGround(base.block.tank.FirstUpdateAfterSpawn))
		{
			AnchorToGround(AnchorOnAttachSnap, base.block.tank.FirstUpdateAfterSpawn);
		}
		base.block.tank.FixupAnchorEvent.Subscribe(OnFixupAnchorsAfterTechPopulated);
		base.block.tank.DetachEvent.Subscribe(OnTankBlockAddedOrRemoved);
		base.block.tank.AttachEvent.Subscribe(OnTankBlockAddedOrRemoved);
		m_AnchorBlockedStateDirty = true;
	}

	private void OnDetaching()
	{
		base.block.tank.DetachEvent.Unsubscribe(OnTankBlockAddedOrRemoved);
		base.block.tank.AttachEvent.Unsubscribe(OnTankBlockAddedOrRemoved);
		base.block.tank.FixupAnchorEvent.Unsubscribe(OnFixupAnchorsAfterTechPopulated);
		if (IsAnchored)
		{
			UnAnchorFromGround(playAnim: false);
		}
		m_Anchor.m_AnchorGeometry.transform.SetLocalRotationIfChanged(m_OriginalAnchorGeomRotation);
		base.block.tank.Anchors.RemovePossibleAnchor(this);
	}

	private void OnTankBlockAddedOrRemoved(TankBlock block, Tank tech)
	{
		m_AnchorBlockedStateDirty = true;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.anchored = IsAnchored;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			IsAnchored = serialData2.anchored;
			m_HasBeenRestored = true;
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(GetType(), "anchored", IsAnchored ? "true" : "false");
			return;
		}
		string text = context.Retrieve(GetType(), "anchored");
		if (onTech && text == "true")
		{
			IsAnchored = true;
		}
	}

	private void OnFixupAnchorsAfterTechPopulated(bool forceAnchor, bool tryAnchorAnyValid)
	{
		d.Assert(base.block.tank);
		if (IsAnchored || tryAnchorAnyValid)
		{
			IsAnchored = false;
			if (WouldAnchorToGround(populatingTech: true) || (forceAnchor && !base.block.tank.IsAnchored))
			{
				bool snapTechToGround = !m_HasBeenRestored && !tryAnchorAnyValid;
				AnchorToGround(snapTechToGround, fromAfterTechPopulate: true);
			}
		}
	}

	private bool IsAPBlockedByAnchor(int apIndex)
	{
		if (IsAnchored)
		{
			for (int i = 0; i < m_APsBlockedByAnchor.Length; i++)
			{
				if (m_APsBlockedByAnchor[i] == apIndex)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void PrePool()
	{
		m_Anchor = GetComponentsInChildren<BlockAnchor>(includeInactive: true).FirstOrDefault();
		if (m_Anchor != null)
		{
			FindAPsBlockedByAnchorGeom();
			FindCellsConnectedToAnchorGeom();
			m_Anchor.FindChildColliders();
		}
		else
		{
			d.LogError("ModuleAnchor on" + base.name + "failed to find a BlockAnchor component in hierarchy");
			UnityEngine.Object.DestroyImmediate(this);
		}
	}

	private void OnPool()
	{
		m_ControlCategoryType = ModuleControlCategory.Anchor;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		if (m_APsBlockedByAnchor != null && m_APsBlockedByAnchor.Length != 0)
		{
			base.block.SetAPIgnoreFilter(IsAPBlockedByAnchor);
		}
		m_OriginalAnchorGeomRotation = m_Anchor.m_AnchorGeometry.transform.localRotation;
		ActivateAnchorGeometry(active: false, enableCollision: false, playAnim: false);
	}

	private void OnSpawn()
	{
		SetCloggedTorque(0f);
		IsAnchored = false;
		m_AnimatorController = GetComponent<ModuleAnimator>();
	}

	private void OnRecycle()
	{
		m_AnimatorController = null;
		ActivateAnchorGeometry(active: false, enableCollision: false, playAnim: false);
		m_HasBeenRestored = false;
	}
}
