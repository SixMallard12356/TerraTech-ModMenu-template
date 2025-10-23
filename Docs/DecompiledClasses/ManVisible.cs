#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ManVisible : Singleton.Manager<ManVisible>, Mode.IManagerModeEvents
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class CacheSphere
	{
		public WorldPosition centre;

		public float radius;

		public Bitfield<ObjectTypes> types;

		public int pickerMask;

		public List<Visible> visibles = new List<Visible>();
	}

	[Serializable]
	public struct SavedTrackedVisible
	{
		public int m_VisibleID;

		public V3Serial m_Pos;

		public WorldPosition m_WorldPos;

		public ObjectTypes m_Type;

		public RadarTypes m_RadarType;

		public int m_RadarTeamID;

		public int m_TeamID;

		public RadarMarker m_RadarMarkerConfig;

		public void Init(TrackedVisible trackedVisible)
		{
			m_VisibleID = trackedVisible.ID;
			m_Pos = Vector3.zero;
			m_WorldPos = trackedVisible.GetWorldPosition();
			m_Type = trackedVisible.ObjectType;
			m_RadarType = trackedVisible.RadarType;
			m_RadarTeamID = trackedVisible.RawRadarTeamID;
			m_TeamID = trackedVisible.TeamID;
			m_RadarMarkerConfig = trackedVisible.RadarMarkerConfig;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (m_WorldPos == default(WorldPosition) && m_Pos != Vector3.zero)
			{
				m_WorldPos = WorldPosition.FromGameWorldPosition(m_Pos + Singleton.Manager<ManWorld>.inst.GameWorldToScene);
				m_Pos = Vector3.zero;
			}
		}
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public class SearchIterator
	{
		private Collider[] colliders = new Collider[2048];

		private Bitfield<ObjectTypes> types;

		private int index;

		private int numResults;

		private int resultsBufferSize = 2048;

		private bool typedSearch;

		private HashSet<int> seenVisibles = new HashSet<int>();

		private const int k_InitialResultsBufferSize = 2048;

		public Visible Current { get; private set; }

		public bool Finished => index == numResults;

		public int Count => numResults;

		public SearchIterator InitSearch(Vector3 scenePos, float radius, int layerMask, Bitfield<ObjectTypes> types, bool includeTriggers)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;
			if (includeTriggers || types.IsNull || types.Contains(1))
			{
				queryTriggerInteraction = QueryTriggerInteraction.Collide;
			}
			while (true)
			{
				numResults = Physics.OverlapSphereNonAlloc(scenePos, radius, colliders, layerMask, queryTriggerInteraction);
				if (numResults < resultsBufferSize)
				{
					break;
				}
				resultsBufferSize *= 2;
				Array.Resize(ref colliders, resultsBufferSize);
			}
			this.types = types;
			typedSearch = !types.IsNull;
			index = -1;
			Current = null;
			seenVisibles.Clear();
			return this;
		}

		public bool MoveNext()
		{
			Visible value = null;
			bool flag = false;
			if (typedSearch)
			{
				do
				{
					index++;
					if (index == numResults)
					{
						return false;
					}
					if (Singleton.Manager<ManVisible>.inst.m_ColliderToVisibleLookup.TryGetValue(colliders[index], out value) && value != null && value.isActive && !seenVisibles.Contains(value.ID))
					{
						flag = types.Contains((int)value.type);
					}
				}
				while (!flag);
			}
			else
			{
				do
				{
					index++;
					if (index == numResults)
					{
						return false;
					}
				}
				while (!Singleton.Manager<ManVisible>.inst.m_ColliderToVisibleLookup.TryGetValue(colliders[index], out value) || !(value != null) || !value.isActive || seenVisibles.Contains(value.ID));
			}
			seenVisibles.Add(value.ID);
			Current = value;
			return true;
		}

		public SearchIterator GetEnumerator()
		{
			return this;
		}

		public void Rewind(bool assertFinished = true)
		{
			d.Assert(!assertFinished || Finished, "rewinding iterator while in progress");
			index = -1;
		}

		public Visible FirstOrDefault()
		{
			Rewind(assertFinished: false);
			if (MoveNext())
			{
				Visible current = Current;
				Rewind(assertFinished: false);
				return current;
			}
			return null;
		}

		public bool Any()
		{
			return FirstOrDefault() != null;
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class CachedSearchIterator
	{
		private List<Visible> visibles;

		private Bitfield<ObjectTypes> types;

		private int index;

		private bool typedSearch;

		public Visible Current => visibles[index];

		public bool Finished => index == visibles.Count;

		public int Count => visibles.Count;

		public CachedSearchIterator Init(List<Visible> visibles)
		{
			this.visibles = visibles;
			typedSearch = false;
			index = -1;
			d.Assert(this.visibles != null);
			return this;
		}

		public CachedSearchIterator Init(List<Visible> visibles, Bitfield<ObjectTypes> types)
		{
			this.visibles = visibles;
			this.types = types;
			typedSearch = !types.IsNull;
			index = -1;
			d.Assert(this.visibles != null);
			return this;
		}

		public bool MoveNext()
		{
			d.Assert(!Finished);
			if (typedSearch)
			{
				do
				{
					index++;
					if (index == visibles.Count)
					{
						return false;
					}
				}
				while (!types.Contains((int)visibles[index].type));
			}
			else if (++index == visibles.Count)
			{
				return false;
			}
			return true;
		}

		public CachedSearchIterator GetEnumerator()
		{
			return this;
		}

		public void Rewind()
		{
			d.Assert(Finished);
			index = -1;
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class StateVisualiser
	{
		public interface Provider
		{
			void Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags);
		}

		private bool ScreenPosIfDrawable(Vector3 worldPos, out Vector2 pos)
		{
			Vector3 rhs = worldPos - Singleton.cameraTrans.position;
			if (Vector3.Dot(Singleton.cameraTrans.forward, rhs) < 0.1f || rhs.sqrMagnitude > 10000f)
			{
				pos = Vector2.zero;
				return false;
			}
			pos = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(worldPos);
			return true;
		}
	}

	public struct ClientTechData
	{
		public TechData techData;

		public Quaternion rotation;
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class SaveData
	{
		public SavedTrackedVisible[] m_SavedThings;

		public SavedTrackedObject[] m_SavedTrackedObjects;
	}

	public int DBGRO_NumCollidersRegistered;

	public Event<TrackedVisible> OnStartedTrackingVisible;

	public Event<TrackedVisible> OnStoppedTrackingVisible;

	private Dictionary<int, TrackedVisible> m_AllTrackedVisibles = new Dictionary<int, TrackedVisible>();

	private Dictionary<int, TrackedVisible> m_TrackedVisibleByHostID = new Dictionary<int, TrackedVisible>();

	private HashSet<int> m_UnsavedTrackedVisibles = new HashSet<int>();

	private Dictionary<uint, TrackedObjectReference> m_TrackedObjects = new Dictionary<uint, TrackedObjectReference>();

	private Dictionary<Collider, Visible> m_ColliderToVisibleLookup = new Dictionary<Collider, Visible>(20000);

	private Dictionary<int, ParticleSystem> m_AttachedParticles = new Dictionary<int, ParticleSystem>();

	private List<CacheSphere> m_SearchCaches = new List<CacheSphere>();

	private int m_CacheFrame;

	private SearchIterator m_SearchIterator = new SearchIterator();

	private CachedSearchIterator m_CachedSearchIterator = new CachedSearchIterator();

	private StateVisualiser m_VisibleStateVisualiser = new StateVisualiser();

	private Dictionary<int, ClientTechData> m_ClientTechData = new Dictionary<int, ClientTechData>();

	private Dictionary<int, Visible> m_ClientHostIDToVisibleLookup = new Dictionary<int, Visible>();

	public int VisiblePickerMask { get; private set; }

	public int VisiblePickerMaskNoTechs { get; private set; }

	public IEnumerable<TrackedVisible> AllTrackedVisibles => m_AllTrackedVisibles.Values;

	public IEnumerable<int> UnsavedTrackedVisibleIDs => m_UnsavedTrackedVisibles;

	public Dictionary<int, TrackedVisible>.Enumerator AllTrackedVisiblesEnumerator => m_AllTrackedVisibles.GetEnumerator();

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_AllTrackedVisibles.Clear();
		m_TrackedVisibleByHostID.Clear();
		m_UnsavedTrackedVisibles.Clear();
		m_TrackedObjects.Clear();
		m_ClientTechData.Clear();
		if (optionalLoadState == null || !optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManVisible, out var saveData) || saveData == null)
		{
			return;
		}
		if (saveData.m_SavedThings != null)
		{
			for (int i = 0; i < saveData.m_SavedThings.Length; i++)
			{
				SavedTrackedVisible savedTrackedVisible = saveData.m_SavedThings[i];
				if (savedTrackedVisible.m_Type != ObjectTypes.Scenery)
				{
					RadarTypes radarType = savedTrackedVisible.m_RadarType;
					int teamID = savedTrackedVisible.m_TeamID;
					int radarTeamID = ((!ManSaveGame.SavedInVersionPriorTo(10750)) ? savedTrackedVisible.m_RadarTeamID : teamID);
					TrackedVisible trackedVisible = new TrackedVisible(savedTrackedVisible.m_VisibleID, null, savedTrackedVisible.m_Type, radarType);
					trackedVisible.RadarTeamID = radarTeamID;
					trackedVisible.TeamID = teamID;
					trackedVisible.SetPos(savedTrackedVisible.m_WorldPos);
					trackedVisible.RadarMarkerConfig = savedTrackedVisible.m_RadarMarkerConfig;
					TrackVisible(trackedVisible);
				}
			}
		}
		if (saveData.m_SavedTrackedObjects != null)
		{
			for (int j = 0; j < saveData.m_SavedTrackedObjects.Length; j++)
			{
				TrackedObjectReference refToTrack = new TrackedObjectReference(saveData.m_SavedTrackedObjects[j]);
				TrackObject(refToTrack);
			}
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		int num = m_AllTrackedVisibles.Count - m_UnsavedTrackedVisibles.Count;
		if (num > 0)
		{
			saveData.m_SavedThings = new SavedTrackedVisible[num];
			int num2 = 0;
			foreach (TrackedVisible value in m_AllTrackedVisibles.Values)
			{
				if (!m_UnsavedTrackedVisibles.Contains(value.ID) && (ManSaveGame.ShouldStoreTechSeparatelyHandler == null || !ManSaveGame.ShouldStoreTechSeparatelyHandler(value.ID)))
				{
					saveData.m_SavedThings[num2].Init(value);
					num2++;
				}
			}
		}
		if (m_TrackedObjects.Count > 0)
		{
			saveData.m_SavedTrackedObjects = new SavedTrackedObject[m_TrackedObjects.Count];
			int num3 = 0;
			foreach (TrackedObjectReference value2 in m_TrackedObjects.Values)
			{
				saveData.m_SavedTrackedObjects[num3].Init(value2);
				num3++;
			}
		}
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManVisible, saveData);
	}

	public void ModeExit()
	{
	}

	public void VisualiserNeedsUpdate()
	{
	}

	public void RegisterColliderToVisibleLookup(Visible v, Collider c)
	{
		if (m_ColliderToVisibleLookup.ContainsKey(c))
		{
			d.LogErrorFormat("ColliderToVisibleClash: trying to add {0}->{1} when already exists {0}->{2}", c, v, m_ColliderToVisibleLookup[c]);
		}
		m_ColliderToVisibleLookup[c] = v;
	}

	public void UnregisterColliderToVisibleLookup(Collider c)
	{
		d.AssertFormat(m_ColliderToVisibleLookup.Remove(c), "UnregisterColliderToVisibleLookup - Failed to remove collider '{0}' from lookup table!?", c.name);
	}

	public Visible FindVisible(Collider c)
	{
		m_ColliderToVisibleLookup.TryGetValue(c, out var value);
		return value;
	}

	public SearchIterator VisiblesTouchingRadius(Vector3 scenePos, float radius, Bitfield<ObjectTypes> types, bool includeTriggers = false, int pickerMask = 0)
	{
		if (pickerMask == 0)
		{
			pickerMask = VisiblePickerMask;
		}
		return m_SearchIterator.InitSearch(scenePos, radius, pickerMask, types, includeTriggers);
	}

	public void VisiblesTouchingRadius(SearchIterator useIterator, Vector3 scenePos, float radius, Bitfield<ObjectTypes> types, bool includeTriggers = false, int pickerMask = 0)
	{
		if (pickerMask == 0)
		{
			pickerMask = VisiblePickerMask;
		}
		d.Assert(useIterator != null);
		useIterator.InitSearch(scenePos, radius, pickerMask, types, includeTriggers);
	}

	public CachedSearchIterator VisiblesTouchingRadiusCached(Vector3 scenePos, float radius, Bitfield<ObjectTypes> types, float cacheRadiusMul = 2f, int pickerMask = 0)
	{
		if (pickerMask == 0)
		{
			pickerMask = VisiblePickerMask;
		}
		if (m_CacheFrame != Singleton.instance.FixedFrameCount)
		{
			for (int i = 0; i < m_SearchCaches.Count; i++)
			{
				m_SearchCaches[i].radius = 0f;
				m_SearchCaches[i].visibles.Clear();
			}
			m_CacheFrame = Singleton.instance.FixedFrameCount;
		}
		CacheSphere cacheSphere = null;
		for (int j = 0; j < m_SearchCaches.Count; j++)
		{
			CacheSphere cacheSphere2 = m_SearchCaches[j];
			if (m_SearchCaches[j].visibles.Count == 0)
			{
				if (cacheSphere == null)
				{
					cacheSphere = cacheSphere2;
				}
			}
			else if ((cacheSphere2.types.IsNull || (!types.IsNull && cacheSphere2.types.Contains(types))) && (cacheSphere2.pickerMask & pickerMask) == pickerMask && (scenePos - cacheSphere2.centre.ScenePosition).magnitude + radius < cacheSphere2.radius)
			{
				if (cacheSphere2.types != types)
				{
					return m_CachedSearchIterator.Init(cacheSphere2.visibles, types);
				}
				return m_CachedSearchIterator.Init(cacheSphere2.visibles);
			}
		}
		bool includeTriggers = false;
		SearchIterator searchIterator = VisiblesTouchingRadius(scenePos, radius * cacheRadiusMul, types, includeTriggers, pickerMask);
		if (cacheSphere == null)
		{
			cacheSphere = new CacheSphere();
			m_SearchCaches.Add(cacheSphere);
		}
		cacheSphere.centre = WorldPosition.FromScenePosition(in scenePos);
		cacheSphere.radius = radius * cacheRadiusMul;
		cacheSphere.types = types;
		cacheSphere.pickerMask = pickerMask;
		foreach (Visible item in searchIterator)
		{
			cacheSphere.visibles.Add(item);
		}
		return m_CachedSearchIterator.Init(cacheSphere.visibles);
	}

	public Visible GetVisibleFromObject(object visibleObject)
	{
		Visible result = null;
		if (visibleObject != null)
		{
			Type type = visibleObject.GetType();
			if (type == typeof(Visible))
			{
				result = visibleObject as Visible;
			}
			else if (type == typeof(Tank))
			{
				result = (visibleObject as Tank).visible;
			}
			else if (type == typeof(TankBlock))
			{
				result = (visibleObject as TankBlock).visible;
			}
			else if (type == typeof(ResourcePickup))
			{
				result = (visibleObject as ResourcePickup).visible;
			}
			else if (type == typeof(ResourceDispenser))
			{
				result = (visibleObject as ResourceDispenser).visible;
			}
			else if (type == typeof(Waypoint))
			{
				result = (visibleObject as Waypoint).visible;
			}
			else if (type == typeof(Crate))
			{
				result = (visibleObject as Crate).visible;
			}
			else
			{
				d.LogError("GetVisibleFromObject - System.Object " + visibleObject.ToString() + " is not a supported type: " + type.ToString());
			}
		}
		return result;
	}

	public void StopTrackingVisible(int ID)
	{
		RemoveTrackedVisible(ID);
	}

	public void TrackWithoutSaving(TrackedVisible refToTrack)
	{
		TrackVisible(refToTrack, addToUnsavedList: true);
	}

	public void TrackVisible(TrackedVisible refToTrack, bool addToUnsavedList = false)
	{
		if (ManNetwork.IsHost)
		{
			TrackVisibleInternal(refToTrack, addToUnsavedList);
		}
	}

	public void TrackVisibleInternal(TrackedVisible refToTrack, bool addToUnsavedList)
	{
		refToTrack.OnRespawnEvent.Subscribe(OnTrackedRespawn);
		refToTrack.OnDespawnEvent.Subscribe(OnTrackedDespawn);
		if (!m_AllTrackedVisibles.ContainsKey(refToTrack.ID))
		{
			m_AllTrackedVisibles.Add(refToTrack.ID, refToTrack);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				AddTrackedVisibleMessage message = new AddTrackedVisibleMessage(refToTrack, addToUnsavedList);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.AddTrackedVisible, message);
			}
		}
		if (refToTrack.HostID != 0)
		{
			if (m_TrackedVisibleByHostID.ContainsKey(refToTrack.HostID))
			{
				d.LogWarning($"TrackVisibleInternal - Already tracking m_TrackedVisibleByHostID with hostID {refToTrack.HostID}");
			}
			m_TrackedVisibleByHostID[refToTrack.HostID] = refToTrack;
		}
		if ((bool)refToTrack.visible)
		{
			OnTrackedRespawn(refToTrack.visible);
		}
		if (addToUnsavedList)
		{
			m_UnsavedTrackedVisibles.Add(refToTrack.ID);
		}
		OnStartedTrackingVisible.Send(refToTrack);
	}

	public TrackedVisible GetTrackedVisible(int ID)
	{
		TrackedVisible value = null;
		m_AllTrackedVisibles.TryGetValue(ID, out value);
		return value;
	}

	public TrackedVisible GetTrackedVisibleByHostID(int hostID)
	{
		m_TrackedVisibleByHostID.TryGetValue(hostID, out var value);
		return value;
	}

	public TechData GetStoredTechData(TrackedVisible tv)
	{
		TryGetStoredTechData(tv, out var techData, out var _);
		return techData;
	}

	public bool TryGetStoredTechData(TrackedVisible tv, out TechData techData, out Quaternion rotation)
	{
		ClientTechData value;
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			ManSaveGame.StoredTech storedTech = Singleton.Manager<ManSaveGame>.inst.GetStoredTech(tv);
			if (storedTech != null)
			{
				techData = storedTech.m_TechData;
				rotation = storedTech.m_Rotation;
				return true;
			}
		}
		else if (m_ClientTechData.TryGetValue(tv.HostID, out value))
		{
			techData = value.techData;
			rotation = value.rotation;
			return true;
		}
		techData = null;
		rotation = default(Quaternion);
		return false;
	}

	public void TryLinkVisibleToTrackedVisible(Visible visible, int trackedVisHostID)
	{
		if (trackedVisHostID != 0)
		{
			if (!LinkVisibleToTrackedVisible(visible, trackedVisHostID, errorOnFail: false) && !m_ClientHostIDToVisibleLookup.ContainsKey(trackedVisHostID))
			{
				m_ClientHostIDToVisibleLookup.Add(trackedVisHostID, visible);
				visible.RecycledEvent.Subscribe(OnNotYetClientTrackedVisibleRecycled);
			}
		}
		else
		{
			d.LogError("TryLinkVisibleToTrackedVisible - Host ID was 0 when trying to link visible " + visible.name + " to TrackedVisible");
		}
	}

	private void OnNotYetClientTrackedVisibleRecycled(Visible visible)
	{
		int hostIDFromVisible = GetHostIDFromVisible(visible);
		m_ClientHostIDToVisibleLookup.Remove(hostIDFromVisible);
		visible.RecycledEvent.Unsubscribe(OnNotYetClientTrackedVisibleRecycled);
	}

	private bool LinkVisibleToTrackedVisible(Visible visible, int trackedVisHostID, bool errorOnFail = true)
	{
		bool result = false;
		TrackedVisible trackedVisibleByHostID = GetTrackedVisibleByHostID(trackedVisHostID);
		if (trackedVisibleByHostID != null)
		{
			UpdateTrackedVisibleID(trackedVisibleByHostID, visible.ID);
			trackedVisibleByHostID.OnRespawn(visible);
			result = true;
		}
		else if (errorOnFail)
		{
			d.LogError($"LinkVisibleToTrackedVisible - Could not link visible {visible.name} to its TrackedVisible - TrackedVisible with hostID {trackedVisHostID} does not exist!");
		}
		return result;
	}

	private void UpdateTrackedVisibleID(TrackedVisible trackedVisible, int newID)
	{
		int iD = trackedVisible.ID;
		if (iD != newID)
		{
			trackedVisible.ClientUpdateID(newID);
			if (m_AllTrackedVisibles.Remove(iD))
			{
				m_AllTrackedVisibles.Add(newID, trackedVisible);
			}
			if (m_UnsavedTrackedVisibles.Remove(iD))
			{
				m_UnsavedTrackedVisibles.Add(newID);
			}
			if (trackedVisible.ObjectType == ObjectTypes.Vehicle)
			{
				Singleton.Manager<ManBlockLimiter>.inst.ReassociateID(iD, newID);
			}
		}
	}

	private void SetHostIDOnVisible(Visible visible, int newHostID)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "SetHostIDOnVisible was called on client!");
		if (!Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return;
		}
		switch (visible.type)
		{
		case ObjectTypes.Vehicle:
			d.Assert(visible.tank.netTech.IsNotNull(), "SetHostIDOnVisible called on null netTech");
			if (visible.tank.netTech.IsNotNull())
			{
				visible.tank.netTech.HostID = newHostID;
			}
			break;
		case ObjectTypes.Waypoint:
			d.Assert(visible.Waypoint.netWaypoint.IsNotNull(), "SetHostIDOnVisible called on null netWaypoint");
			if (visible.Waypoint.netWaypoint.IsNotNull())
			{
				visible.Waypoint.netWaypoint.HostID = newHostID;
			}
			break;
		case ObjectTypes.Crate:
			d.Assert(visible.crate.netCrate.IsNotNull(), "SetHostIDOnVisible called on null netCrate");
			if (visible.crate.netCrate.IsNotNull())
			{
				visible.crate.netCrate.HostID = newHostID;
			}
			break;
		default:
			d.LogError($"SetHostIDOnVisible called on visible of type {visible.type} - Update of host ID on netObject not implemented");
			break;
		}
	}

	public int GetHostIDFromVisible(Visible visible)
	{
		int result = 0;
		switch (visible.type)
		{
		case ObjectTypes.Vehicle:
			d.Assert(visible?.tank.netTech.IsNotNull() ?? false, "GetHostIDFromVisible called on null netTech");
			if ((object)visible != null && visible.tank.netTech.IsNotNull())
			{
				result = visible.tank.netTech.HostID;
			}
			break;
		case ObjectTypes.Waypoint:
			d.Assert(visible?.Waypoint.netWaypoint.IsNotNull() ?? false, "GetHostIDFromVisible called on null netWaypoint");
			if ((object)visible != null && visible.Waypoint.netWaypoint.IsNotNull())
			{
				result = visible.Waypoint.netWaypoint.HostID;
			}
			break;
		case ObjectTypes.Crate:
			d.Assert(visible?.crate.netCrate.IsNotNull() ?? false, "GetHostIDFromVisible called on null netCrate");
			if ((object)visible != null && visible.crate.netCrate.IsNotNull())
			{
				result = visible.crate.netCrate.HostID;
			}
			break;
		default:
			d.LogError($"GetHostIDFromVisible called on visible of type {visible.type} - GetHostID on netObject not implemented");
			break;
		}
		return result;
	}

	public void TrackObject(TrackedObjectReference refToTrack)
	{
		if (!m_TrackedObjects.ContainsKey(refToTrack.TrackedId))
		{
			m_TrackedObjects.Add(refToTrack.TrackedId, refToTrack);
		}
		else
		{
			d.LogError("ManVisible.TrackObject - Trying to track object, but object unique ID was already in the table!");
		}
	}

	public void StopTrackingObject(uint trackedID)
	{
		TrackedObjectReference trackedObject = GetTrackedObject(trackedID);
		if (trackedObject != null)
		{
			if (trackedObject.TrackedObject != null)
			{
				trackedObject.TrackedObject.ClearTrackingID();
			}
			m_TrackedObjects.Remove(trackedID);
		}
		else
		{
			d.LogError("ManVisible.StopTrackingObject - Trying to stop tracking object, but tracked ID was not found!");
		}
	}

	public void SendTrackedVisiblesToClient(NetPlayer targetPlayer)
	{
		HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
		foreach (KeyValuePair<int, TrackedVisible> allTrackedVisible in m_AllTrackedVisibles)
		{
			AddTrackedVisibleMessage message = new AddTrackedVisibleMessage(allTrackedVisible.Value, m_UnsavedTrackedVisibles.Contains(allTrackedVisible.Key));
			Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.AddTrackedVisible, message);
			if (allTrackedVisible.Value.ObjectType == ObjectTypes.Vehicle)
			{
				IntVector2 tileCoord = allTrackedVisible.Value.GetWorldPosition().TileCoord;
				hashSet.Add(tileCoord);
			}
		}
		foreach (IntVector2 item in hashSet)
		{
			ManSaveGame.StoredTile storedTile = Singleton.Manager<ManSaveGame>.inst.GetStoredTile(item, createNewIfNotFound: false);
			if (storedTile == null)
			{
				d.LogWarningFormat("Occupied tile at {0} isn't in the stored tile data on the host.", item);
			}
			else
			{
				if (!storedTile.m_StoredVisibles.TryGetValue(1, out var value))
				{
					continue;
				}
				foreach (ManSaveGame.StoredVisible item2 in value)
				{
					ManSaveGame.StoredTech storedTech = item2 as ManSaveGame.StoredTech;
					TechDataMessage message2 = new TechDataMessage
					{
						m_HostID = storedTech.m_ID,
						m_TechInfo = new ClientTechData
						{
							rotation = storedTech.m_Rotation,
							techData = storedTech.m_TechData
						}
					};
					Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.TechData, message2);
				}
			}
		}
	}

	public void ObliterateTrackedVisibleFromWorld(int trackedVisID)
	{
		ObliterateTrackedVisibleFromWorld(m_AllTrackedVisibles[trackedVisID]);
	}

	public void ObliterateTrackedVisibleFromWorld(TrackedVisible trackedVis)
	{
		if (trackedVis.visible != null)
		{
			trackedVis.visible.RemoveFromGame();
			return;
		}
		ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(trackedVis.Position, createNewDataIfNotFound: false);
		if (storedTileIfNotSpawned != null)
		{
			storedTileIfNotSpawned.RemoveSavedVisible(trackedVis.ObjectType, trackedVis.ID);
		}
		else
		{
			WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(trackedVis.Position);
			if (worldTile != null)
			{
				worldTile.RemoveStoredVisibleWaitingToLoad(trackedVis.ID);
			}
			else
			{
				d.LogErrorFormat("Trying to remove visible, but visible did not resolve to a valid Tile, Loaded or not, at position {0}", trackedVis.Position);
			}
		}
		Singleton.Manager<ManVisible>.inst.StopTrackingVisible(trackedVis.ID);
		if (trackedVis.ObjectType == ObjectTypes.Vehicle)
		{
			Singleton.Manager<ManTechs>.inst.RemoveOverlappingTechData(trackedVis.ID);
			Singleton.Manager<ManBlockLimiter>.inst.RemoveTechByID(trackedVis.ID);
		}
	}

	public void ObliterateTrackedObjectFromWorld(TrackedObjectReference trackedObj)
	{
		if (trackedObj.TrackedObject != null)
		{
			trackedObj.TrackedObject.transform.Recycle();
			return;
		}
		Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(trackedObj.ScenePosition, createNewDataIfNotFound: false)?.RemoveSavedTrackedObject(trackedObj.TrackedId);
		Singleton.Manager<ManVisible>.inst.StopTrackingObject(trackedObj.TrackedId);
	}

	public TrackedObjectReference GetTrackedObject(uint trackedID)
	{
		m_TrackedObjects.TryGetValue(trackedID, out var value);
		return value;
	}

	public ParticleSystem SpawnAttachParticles(Visible item, ParticleSystem particlesPrefab)
	{
		ClearParticles(item);
		ParticleSystem particleSystem = particlesPrefab.Spawn(item.trans, item.centrePosition);
		particleSystem.Play();
		m_AttachedParticles[item.ID] = particleSystem;
		return particleSystem;
	}

	public void ClearParticles(Visible item)
	{
		if (m_AttachedParticles.TryGetValue(item.ID, out var value))
		{
			value.Stop();
			value.transform.parent = null;
			value.Recycle();
			m_AttachedParticles.Remove(item.ID);
		}
	}

	public ParticleSystem GetParticles(Visible item)
	{
		m_AttachedParticles.TryGetValue(item.ID, out var value);
		return value;
	}

	private void RemoveTrackedVisible(int ID)
	{
		if (ManNetwork.IsHost)
		{
			RemoveTrackedVisibleInternal(ID);
		}
	}

	private void RemoveTrackedVisibleInternal(int ID)
	{
		TrackedVisible value = null;
		if (m_AllTrackedVisibles.TryGetValue(ID, out value))
		{
			OnStoppedTrackingVisible.Send(value);
			value.OnRespawnEvent.Unsubscribe(OnTrackedRespawn);
			value.OnDespawnEvent.Unsubscribe(OnTrackedDespawn);
			m_AllTrackedVisibles.Remove(ID);
			m_TrackedVisibleByHostID.Remove(value.HostID);
			m_ClientTechData.Remove(value.HostID);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.RemoveTrackedVisible, new RemoveTrackedVisibleMessage
				{
					m_HostID = ID
				});
			}
		}
		if (m_UnsavedTrackedVisibles.Contains(ID))
		{
			m_UnsavedTrackedVisibles.Remove(ID);
		}
	}

	private void OnTrackedRespawn(Visible visible)
	{
		switch (visible.type)
		{
		case ObjectTypes.Vehicle:
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				SetHostIDOnVisible(visible, visible.ID);
			}
			break;
		case ObjectTypes.Waypoint:
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				SetHostIDOnVisible(visible, visible.ID);
			}
			visible.RecycledEvent.Subscribe(OnTrackedVisibleRecycled);
			break;
		case ObjectTypes.Block:
			visible.RecycledEvent.Subscribe(OnTrackedVisibleRecycled);
			break;
		case ObjectTypes.Crate:
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				SetHostIDOnVisible(visible, visible.ID);
			}
			break;
		default:
			d.LogError(string.Concat("ManVisible.OnRespawn - We don't handle items of this type! Visible ", visible.name, " of type ", visible.type, ": ", visible.ItemType, " shouldn't be here"));
			break;
		case ObjectTypes.Scenery:
			break;
		}
	}

	private void OnTrackedDespawn(Visible visible)
	{
		switch (visible.type)
		{
		case ObjectTypes.Block:
		case ObjectTypes.Waypoint:
			visible.RecycledEvent.Unsubscribe(OnTrackedVisibleRecycled);
			return;
		case ObjectTypes.Vehicle:
		case ObjectTypes.Scenery:
		case ObjectTypes.Crate:
			return;
		}
		d.LogError(string.Concat("ManVisible.OnDespawn - We don't handle items of this type! Visible ", visible.name, " of type ", visible.type, ": ", visible.ItemType, " shouldn't be here"));
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		RemoveTrackedVisible(tank.visible.ID);
	}

	private void OnTrackedVisibleRecycled(Visible visible)
	{
		if (visible.Killed)
		{
			RemoveTrackedVisible(visible.ID);
		}
		visible.RecycledEvent.Unsubscribe(OnTrackedVisibleRecycled);
	}

	private void OnClientAddTrackedVisible(NetworkMessage netMsg)
	{
		AddTrackedVisibleMessage addTrackedVisibleMessage = netMsg.ReadMessage<AddTrackedVisibleMessage>();
		TrackedVisible trackedVisible = GetTrackedVisibleByHostID(addTrackedVisibleMessage.m_HostID);
		if (trackedVisible == null)
		{
			if (m_ClientHostIDToVisibleLookup.TryGetValue(addTrackedVisibleMessage.m_HostID, out var value))
			{
				m_ClientHostIDToVisibleLookup.Remove(addTrackedVisibleMessage.m_HostID);
				value.RecycledEvent.Unsubscribe(OnNotYetClientTrackedVisibleRecycled);
			}
			trackedVisible = new TrackedVisible(value ? value.ID : Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(addTrackedVisibleMessage.m_ObjectType), value, addTrackedVisibleMessage.m_ObjectType, addTrackedVisibleMessage.m_RadarType);
			trackedVisible.HostID = addTrackedVisibleMessage.m_HostID;
			TrackVisibleInternal(trackedVisible, addTrackedVisibleMessage.m_IsTrackedUnsaved);
		}
		else
		{
			d.LogError("OnClientAddTrackedVisible called for visible with HostID that already exists!");
			d.Assert(addTrackedVisibleMessage.m_ObjectType == trackedVisible.ObjectType, "OnClientAddTrackedVisible - Tracked visible type missmatch between available data and incoming message data!");
		}
		trackedVisible.TeamID = addTrackedVisibleMessage.m_TeamID;
		trackedVisible.SetPos(addTrackedVisibleMessage.m_WorldPosition);
		trackedVisible.RadarMarkerConfig = addTrackedVisibleMessage.m_RadarMarkerConfig;
		trackedVisible.RadarType = addTrackedVisibleMessage.m_RadarType;
		trackedVisible.RadarTeamID = addTrackedVisibleMessage.m_RadarTeamID;
		trackedVisible.IsQuestObject = addTrackedVisibleMessage.m_IsQuestObject;
		if (trackedVisible.ObjectType == ObjectTypes.Vehicle && trackedVisible.visible.IsNull())
		{
			int cost = Mathf.Max(addTrackedVisibleMessage.m_BlockLimitCost, Singleton.Manager<ManBlockLimiter>.inst.PerTechCost);
			Singleton.Manager<ManBlockLimiter>.inst.AddTrackedVisibleTech(trackedVisible.ID, cost, trackedVisible.TeamID, isPopulation: false);
		}
	}

	private void OnClientUpdateTrackedVisible(NetworkMessage netMsg)
	{
		UpdateTrackedVisibleMessage updateTrackedVisibleMessage = netMsg.ReadMessage<UpdateTrackedVisibleMessage>();
		TrackedVisible trackedVisibleByHostID = GetTrackedVisibleByHostID(updateTrackedVisibleMessage.m_HostID);
		d.Assert(trackedVisibleByHostID != null, $"Expecting to update tracked visible on Client - but client does not have tracked visible with ID {updateTrackedVisibleMessage.m_HostID}");
		if (trackedVisibleByHostID != null)
		{
			trackedVisibleByHostID.RadarTeamID = updateTrackedVisibleMessage.m_RadarTeamID;
			trackedVisibleByHostID.IsQuestObject = updateTrackedVisibleMessage.m_IsQuestObject;
			trackedVisibleByHostID.TeamID = updateTrackedVisibleMessage.m_TeamID;
			trackedVisibleByHostID.SetPos(updateTrackedVisibleMessage.m_WorldPosition);
			trackedVisibleByHostID.RadarType = updateTrackedVisibleMessage.m_RadarType;
			trackedVisibleByHostID.WaypointOverlayEnabled = updateTrackedVisibleMessage.m_IsOverlayEnabled;
			trackedVisibleByHostID.RadarMarkerConfig = updateTrackedVisibleMessage.m_RadarMarkerConfig;
		}
	}

	private void OnClientAddTechData(NetworkMessage netMsg)
	{
		TechDataMessage techDataMessage = netMsg.ReadMessage<TechDataMessage>();
		m_ClientTechData[techDataMessage.m_HostID] = techDataMessage.m_TechInfo;
	}

	private void OnClientRemoveTrackedVisible(NetworkMessage netMsg)
	{
		RemoveTrackedVisibleMessage removeTrackedVisibleMessage = netMsg.ReadMessage<RemoveTrackedVisibleMessage>();
		TrackedVisible trackedVisibleByHostID = GetTrackedVisibleByHostID(removeTrackedVisibleMessage.m_HostID);
		d.Assert(trackedVisibleByHostID != null, $"Expecting to remove tracked visible on Client - but client does not have tracked visible with ID {removeTrackedVisibleMessage.m_HostID}");
		if (trackedVisibleByHostID != null)
		{
			RemoveTrackedVisibleInternal(trackedVisibleByHostID.ID);
			if (trackedVisibleByHostID.ObjectType == ObjectTypes.Vehicle)
			{
				Singleton.Manager<ManBlockLimiter>.inst.RemoveTechByID(trackedVisibleByHostID.ID);
			}
			Singleton.Manager<ManEncounter>.inst.RemoveNavigationOverlay(trackedVisibleByHostID);
		}
	}

	private void Start()
	{
		VisiblePickerMask = Globals.inst.layerTank.mask | Globals.inst.layerCosmetic.mask | Globals.inst.layerScenery.mask | Globals.inst.layerPickup.mask;
		VisiblePickerMaskNoTechs = Globals.inst.layerTank.mask | Globals.inst.layerScenery.mask | Globals.inst.layerPickup.mask;
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.AddTrackedVisible, OnClientAddTrackedVisible);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.UpdateTrackedVisible, OnClientUpdateTrackedVisible);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.RemoveTrackedVisible, OnClientRemoveTrackedVisible);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.TechData, OnClientAddTechData);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
