#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SceneryFader))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(TerrainObject))]
[RequireComponent(typeof(Visible))]
public class ResourceDispenser : MonoBehaviour
{
	[Serializable]
	public struct DamageStage
	{
		public Transform m_Geometry;

		public float m_Health;

		public bool m_Invulnerable;

		public bool m_ForceUpright;

		public bool m_IgnoredForPlacementCheck;
	}

	public struct PersistentState
	{
		public int currentStage;

		public int chunksSpawned;

		public float regrowDelay;

		public float health;

		public bool removedFromWorld;

		public ResourceReservoir.SerialData resourceReservoirSaveData;
	}

	[SerializeField]
	private ResourceSpawnChance[] m_ResourceSpawnChances;

	[SerializeField]
	private DamageStage[] m_DamageStages;

	[SerializeField]
	private int m_TotalChunks;

	[SerializeField]
	private Transform m_SpawnPointOverride;

	[SerializeField]
	private float m_SpawnRange;

	[SerializeField]
	private float m_SpawnSpeed = 10f;

	[SerializeField]
	private Vector3 m_SpawnVelocityRandom = Vector3.one;

	[SerializeField]
	private float m_SpawnRotationRandom = 1f;

	[SerializeField]
	private Transform m_AnimatedTransform;

	[SerializeField]
	private ManSceneryAnimation.AnimTypes m_DamageAnim;

	[SerializeField]
	private ManSceneryAnimation.AnimTypes m_DeathAnim;

	[SerializeField]
	private ManSceneryAnimation.AnimTypes m_RegrowAnim;

	[SerializeField]
	private bool m_DontRegrow;

	[SerializeField]
	private FMODEvent m_RegrowSfxEvent;

	[SerializeField]
	private Transform m_HitPrefab;

	[SerializeField]
	private Transform m_DebrisPrefab;

	[SerializeField]
	private Transform m_BigDebrisPrefab;

	[SerializeField]
	private bool m_IgnoreSceneryFade;

	public Event<ResourceDispenser> ResourceGiverDamageStageChangingEvent;

	public Event<ResourceDispenser> ResourceGiverDamageStageChangedEvent;

	public Event<ResourceDispenser> ResourceGiverDestroyedEvent;

	public Event<ResourceDispenser> ResourceGiverRegrowEvent;

	private Transform m_AnimatedTransformParent;

	private Transform m_GeometryParent;

	private TerrainObject m_TerrainObject;

	private SceneryFader m_SceneryFader;

	private int m_CurrentDamageStage;

	private Transform m_CurrentDamageStageGeometry;

	private int m_NumChunksSpawned;

	private int m_LastNumChunksSpawned;

	private bool m_AtLeastOneThresholdPassed;

	private float m_HitEffectTimeout;

	private float m_RegrowTimeOverride;

	private ManTimedEvents.ManagedEvent m_RegrowEvent;

	private ManDamage.DamageInfo m_DeathDamageInfo;

	[SerializeField]
	[HideInInspector]
	private float m_GroundRadius;

	[SerializeField]
	[HideInInspector]
	private float m_MinY;

	[HideInInspector]
	[SerializeField]
	private float m_MaxY;

	private bool m_Awake;

	private float m_SleepingRegrowDelay;

	private bool m_RemovedFromWorld;

	private bool m_NeedsAnimReset;

	private ResourceReservoir.SerialData m_ResourceReservoirSaveData;

	private static List<Collider> s_ExistingColliderLookup = new List<Collider>();

	public float regrowTimeout { get; private set; }

	public Visible visible { get; private set; }

	public IntVector2 cellCoord => m_TerrainObject.TileCellCoord;

	public bool WasPlacedManually => m_TerrainObject.WasPlacedManually;

	public float GroundRadius => m_GroundRadius;

	public ThermalPowerSource ThermalSource { get; private set; }

	public ResourceReservoir ResourceReservoir { get; private set; }

	public bool IsResourceReservoir => ResourceReservoir != null;

	public bool IgnoreSceneryFade => m_IgnoreSceneryFade;

	public bool IsIgnoredForPlacementCheck
	{
		get
		{
			if (m_CurrentDamageStage < m_DamageStages.Length)
			{
				return m_DamageStages[m_CurrentDamageStage].m_IgnoredForPlacementCheck;
			}
			return false;
		}
	}

	public bool IsDeactivated
	{
		get
		{
			if (!visible.isActive)
			{
				if (m_RegrowEvent == null || !m_RegrowEvent.IsSet)
				{
					return m_RemovedFromWorld;
				}
				return true;
			}
			return false;
		}
	}

	public int CurrentDamageState => m_CurrentDamageStage;

	public void SetRegrowOverrideTime(float overrideTime)
	{
		m_RegrowTimeOverride = overrideTime;
	}

	public PersistentState Store()
	{
		d.AssertFormat(visible.isActive || IsDeactivated, "ResourceDispenser.Store - ResourceDispenser '{0}' at {1} has Disabled GameObject but ResourceDispenser itself was not Disabled! Was it recycled but still being stored?", base.name, base.transform.position);
		float num = Mathf.Max(visible.damageable.Health, 0f);
		float regrowDelay = -1f;
		if (!visible.isActive || num <= Mathf.Epsilon)
		{
			regrowDelay = ((m_RegrowEvent != null && m_RegrowEvent.IsSet) ? m_RegrowEvent.TimeRemaining : GetRegrowDelay());
		}
		if (ResourceReservoir != null)
		{
			ResourceReservoir.Store(ref m_ResourceReservoirSaveData);
		}
		return new PersistentState
		{
			currentStage = m_CurrentDamageStage,
			chunksSpawned = m_NumChunksSpawned,
			health = num,
			regrowDelay = regrowDelay,
			removedFromWorld = m_RemovedFromWorld,
			resourceReservoirSaveData = m_ResourceReservoirSaveData
		};
	}

	public void Restore(PersistentState state)
	{
		d.Assert(state.health != 0f || state.regrowDelay > 0f || m_RemovedFromWorld, "Restoring dead scenery with no regrow timer: " + base.name);
		m_RemovedFromWorld = state.removedFromWorld;
		m_CurrentDamageStage = state.currentStage;
		m_NumChunksSpawned = state.chunksSpawned;
		m_LastNumChunksSpawned = m_NumChunksSpawned;
		m_AtLeastOneThresholdPassed = false;
		visible.damageable.InitHealth(state.health);
		m_ResourceReservoirSaveData = state.resourceReservoirSaveData;
		if (ResourceReservoir != null)
		{
			ResourceReservoir.Restore(m_ResourceReservoirSaveData);
		}
		if (state.health == 0f || m_RemovedFromWorld)
		{
			Deactivate(state.regrowDelay);
		}
		else
		{
			SetupDamageState(m_CurrentDamageStage);
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsServer || WasPlacedManually)
		{
			return;
		}
		WorldTile tile = visible.tileCache.tile;
		d.Assert(tile != null, "ResourceDispenser.Regrow - Could not find tile for damaged resource dispenser");
		d.Assert(tile.NetTile.IsNotNull(), "ResourceDispenser.Regrow - Tile did not have an attached NetTile");
		if (tile != null && tile.NetTile.IsNotNull())
		{
			if (state.health == 0f || m_RemovedFromWorld)
			{
				tile.NetTile.OnServerSetResourceDispenserDead(cellCoord);
			}
			else
			{
				tile.NetTile.OnServerSetResourceDamageState(cellCoord, m_CurrentDamageStage);
			}
		}
	}

	private bool TryRestoreSavedState()
	{
		bool result = false;
		if (!WasPlacedManually)
		{
			if (m_TerrainObject.Tile == null)
			{
				d.LogWarning("ResourceDispenser spawned without valid Tile reference (" + base.name + "): is this an Encounter-spawned Scenery object?");
				return result;
			}
			if (m_TerrainObject.Tile.SaveData == null)
			{
				d.LogWarning("ResourceDispenser spawned without valid Tile SaveData (" + base.name + "): what's going on here, then?");
				return result;
			}
			if (m_TerrainObject.Tile.SaveData.m_Scenery == null)
			{
				d.LogWarning("ResourceDispenser spawned without valid Tile SaveData Scenery reference (" + base.name + "): Interesting...");
				return result;
			}
			if (visible == null)
			{
				d.LogWarning("ResourceDispenser spawned without valid visible reference (" + base.name + ")");
				return result;
			}
			ManSaveGame.StoredTile.StoredSceneryState value = null;
			if (m_TerrainObject.Tile.SaveData.m_Scenery.TryGetValue(m_TerrainObject.TileCellCoord, out value))
			{
				if (value == null)
				{
					d.LogError("TryRestoreSavedState - Stored scenery save data was NULL! Name=" + base.name + " Visible=" + visible.m_ItemType.name);
				}
				else if (value.sceneryType == (SceneryTypes)visible.ItemType)
				{
					Restore(value.state);
					result = true;
				}
			}
		}
		return result;
	}

	public void SetEmpty()
	{
		m_NumChunksSpawned = m_TotalChunks;
		m_LastNumChunksSpawned = m_NumChunksSpawned;
		RemoveResourceTrackedOnTile();
	}

	public void SetAwake(bool awake)
	{
		if (awake != m_Awake)
		{
			m_Awake = awake;
			if (awake)
			{
				if (m_CurrentDamageStage < m_DamageStages.Length && m_DamageStages[m_CurrentDamageStage].m_Invulnerable)
				{
					visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
				}
				else
				{
					visible.damageable.SetInvulnerable(invulnerable: false, unlimitedInvulnerability: false);
				}
				if (ManNetwork.IsHost && m_SleepingRegrowDelay >= 0f && !m_RemovedFromWorld)
				{
					SetRegrowEvent(m_SleepingRegrowDelay);
					m_SleepingRegrowDelay = -1f;
				}
				TryAddResourceTrackedOnTile();
			}
			else
			{
				visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
				m_SleepingRegrowDelay = -1f;
				if (m_RegrowEvent != null && m_RegrowEvent.IsSet)
				{
					m_SleepingRegrowDelay = m_RegrowEvent.TimeRemaining;
					m_RegrowEvent.Clear();
				}
				if (m_NumChunksSpawned < m_TotalChunks)
				{
					RemoveResourceTrackedOnTile();
				}
			}
			visible.SetCollidersEnabled(awake);
		}
		ThermalSource?.SetAwake(awake);
		ResourceReservoir?.SetAwake(awake);
	}

	private void TryAddResourceTrackedOnTile()
	{
		if (m_NumChunksSpawned < m_TotalChunks)
		{
			for (int i = 0; i < m_ResourceSpawnChances.Length; i++)
			{
				visible.tileCache.tile.AddResourceSource(visible, m_ResourceSpawnChances[i].chunkType);
			}
		}
	}

	private void RemoveResourceTrackedOnTile()
	{
		for (int i = 0; i < m_ResourceSpawnChances.Length; i++)
		{
			visible.tileCache.tile.RemoveResourceSource(visible, m_ResourceSpawnChances[i].chunkType);
		}
	}

	public bool HasDispenseType(ChunkTypes type)
	{
		for (int i = 0; i < m_ResourceSpawnChances.Length; i++)
		{
			if (m_ResourceSpawnChances[i].chunkType == type)
			{
				return true;
			}
		}
		return false;
	}

	public IEnumerable<ChunkTypes> AllDispensableItems()
	{
		return from r in m_ResourceSpawnChances
			where r.chunkType != ChunkTypes.Null && Singleton.Manager<ResourceManager>.inst.GetResourceDef(r.chunkType) != null
			select r.chunkType;
	}

	public SceneryTypes GetSceneryType()
	{
		SceneryTypes result = (SceneryTypes)visible.ItemType;
		if (ThermalSource != null)
		{
			result = ThermalSource.GetSceneryType();
		}
		if (ResourceReservoir != null)
		{
			result = ResourceReservoir.GetSceneryType();
		}
		return result;
	}

	public void RegisterThermalSource(ThermalPowerSource source, bool register)
	{
		if (register)
		{
			d.Assert(ThermalSource == null, "ResourceDispenser.RegisterThermalSource - Thermal source was already registered. On " + visible.name);
			d.Assert(ResourceReservoir == null, "ResourceDispenser.RegisterThermalSource - Both ThermalSource _and_ ResourceReservoir were present on ResourceDispenser " + visible.name + "! Only one allowed active at a time");
			ThermalSource = source;
		}
		else
		{
			d.Assert(ThermalSource == source, "ResourceDispenser.RegisterThermalSource - Thermal source was not registered. On " + visible.name);
			ThermalSource = null;
		}
	}

	public void RegisterResourceReservoir(ResourceReservoir reservoir, bool register)
	{
		if (register)
		{
			d.Assert(ResourceReservoir == null, "ResourceDispenser.RegisterResourceReservoir - Registering resource reservoir while there was already one registered! On " + visible.name);
			d.Assert(ThermalSource == null, "ResourceDispenser.RegisterThermalSource - Both ThermalSource _and_ ResourceReservoir were present on ResourceDispenser " + visible.name + "! Only one allowed active at a time");
			ResourceReservoir = reservoir;
			if (m_ResourceReservoirSaveData != null)
			{
				ResourceReservoir.Restore(m_ResourceReservoirSaveData);
			}
		}
		else
		{
			d.Assert(ResourceReservoir == reservoir, "ResourceDispenser.RegisterResourceReservoir - Unregistering resource reservoir when the passed in one was not registered! On " + visible.name);
			ResourceReservoir = null;
		}
	}

	public void RemoveFromWorld(bool spawnChunks, bool neverRegrow = false, bool removeInstant = false, bool removePersistentDamageStage = false)
	{
		RemoveFromWorld(visible.centrePosition, Vector3.zero, spawnChunks, neverRegrow, removeInstant, removePersistentDamageStage);
	}

	public void RemoveFromWorld(Vector3 chunkSpawnPos, Vector3 damageDir, bool spawnChunks, bool neverRegrow = false, bool removeInstant = false, bool removePersistentDamageStage = false)
	{
		if (visible.tileCache.tile == null || !visible.tileCache.tile.IsLoaded)
		{
			d.LogErrorFormat("Deleting object " + visible.name + " on tile that is not loaded (will not be stored back)");
			return;
		}
		int num = 0;
		if (spawnChunks)
		{
			num = m_TotalChunks - m_NumChunksSpawned;
			ManDamage.DamageInfo hitInfo = new ManDamage.DamageInfo(0f, ManDamage.DamageType.Standard, null, null, chunkSpawnPos, damageDir);
			SpawnChunks(num, hitInfo);
		}
		SetEmpty();
		float delay = 0f;
		if (neverRegrow)
		{
			m_RemovedFromWorld = true;
		}
		else
		{
			delay = GetRegrowDelay();
		}
		int num2 = -1;
		for (int i = m_CurrentDamageStage; i < m_DamageStages.Length; i++)
		{
			if (m_DamageStages[i].m_Invulnerable)
			{
				num2 = i;
				break;
			}
		}
		int num3 = ((num2 < 0 || removePersistentDamageStage) ? m_DamageStages.Length : num2);
		if (!removeInstant && m_CurrentDamageStage < num3)
		{
			Singleton.Manager<ManSFX>.inst.PlaySceneryDebrisSFX(this, num);
		}
		float num4 = 0f;
		if (num3 < m_DamageStages.Length)
		{
			for (int j = num3; j < m_DamageStages.Length; j++)
			{
				num4 += m_DamageStages[j].m_Health;
			}
		}
		visible.damageable.InitHealth(num4);
		if (num2 >= 0)
		{
			SetupDamageState(num3);
		}
		else if (removeInstant)
		{
			Deactivate(delay);
		}
		else
		{
			PlayDeathAnimation(damageDir);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(visible.centrePosition);
			d.Assert(worldTile != null, "ResourceDispenser.RemoveFromWorld - Could not find tile for damaged resource dispenser");
			d.Assert(worldTile?.NetTile.IsNotNull() ?? true, "ResourceDispenser.RemoveFromWorld - Tile did not have an attached NetTile");
			if (worldTile != null && worldTile.NetTile.IsNotNull())
			{
				worldTile.NetTile.OnServerSetResourceDispenserDead(cellCoord);
			}
		}
	}

	private ChunkTypes ResourceToSpawnRandomised()
	{
		return ResourceSpawnChance.GetChunkFromSpawnGroup(m_ResourceSpawnChances);
	}

	private int SpawnChunks(int num, ManDamage.DamageInfo hitInfo)
	{
		if (!ManNetwork.IsHost || (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCampaign>() && !Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCreative>()))
		{
			return 0;
		}
		int num2 = 0;
		d.AssertFormat(m_Awake, "ResourceDispenser {0} is spawning chunks whilst asleep", visible.name);
		Vector3 vector = hitInfo.HitPosition;
		Vector3 vector2 = -hitInfo.DamageDirection;
		Vector3 vector3 = Vector3.zero;
		if ((bool)m_SpawnPointOverride)
		{
			vector = m_SpawnPointOverride.position;
			vector2 = m_SpawnPointOverride.transform.forward;
		}
		if (m_SpawnRange != 0f)
		{
			Vector3 vector4 = (m_SpawnPointOverride ? m_SpawnPointOverride.forward : Vector3.up);
			vector3 = (vector + vector4 * Mathf.Max(m_SpawnRange, 1f) - vector) / num;
		}
		for (int i = 0; i < num; i++)
		{
			ChunkTypes chunkTypes = ResourceToSpawnRandomised();
			if (chunkTypes == ChunkTypes.Null)
			{
				continue;
			}
			Vector3 pos = vector + i * vector3;
			Vector3 vel = vector2 * m_SpawnSpeed + new Vector3(m_SpawnVelocityRandom.x * (UnityEngine.Random.value * 2f - 1f), m_SpawnVelocityRandom.y * (UnityEngine.Random.value * 2f - 1f), m_SpawnVelocityRandom.z * (UnityEngine.Random.value * 2f - 1f));
			Vector3 rotVel = new Vector3(m_SpawnRotationRandom * (UnityEngine.Random.value * 2f - 1f), m_SpawnRotationRandom * (UnityEngine.Random.value * 2f - 1f), m_SpawnRotationRandom * (UnityEngine.Random.value * 2f - 1f));
			if ((bool)Singleton.Manager<ManLooseBlocks>.inst.HostSpawnChunk(chunkTypes, pos, Quaternion.identity, initNew: true, vel, rotVel))
			{
				num2++;
				if (hitInfo.SourceTank != null && hitInfo.SourceTank.IsFriendly(0))
				{
					Singleton.Manager<ManStats>.inst.ResourceHarvested(visible.block, chunkTypes);
				}
			}
			else
			{
				d.LogError("ResourceDispenser.SpawnChunk - '" + base.name + "' Failed to spawn resource: " + chunkTypes.ToString() + " ...check ResourceTable");
			}
		}
		return num2;
	}

	private void Regrow()
	{
		if (!m_Awake)
		{
			return;
		}
		d.Assert(!m_RemovedFromWorld, "ResourceDispenser.Regrow - Function called on resource dispenser " + base.name + " that's set to 'Removed From World', eg never regrow. !");
		if (!m_DontRegrow)
		{
			float num = 0f;
			if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCheckpointChallenge>() && Mode<ModeCheckpointChallenge>.inst.InProgress)
			{
				num = Singleton.Manager<ManGameMode>.inst.GetSceneryRegrowTime();
			}
			if (Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Regrow, visible.trans.position, m_GroundRadius))
			{
				num = Globals.inst.m_BlockedSceneryRegrowInterval;
			}
			if (num != 0f)
			{
				m_RegrowEvent?.Clear();
				SetRegrowEvent(num);
				return;
			}
		}
		base.gameObject.SetActive(value: true);
		InitState();
		RefillDefault();
		TryAddResourceTrackedOnTile();
		ResourceGiverRegrowEvent.Send(this);
		if (m_RegrowAnim != ManSceneryAnimation.AnimTypes.None)
		{
			Singleton.Manager<ManSceneryAnimation>.inst.PlayAnimation(m_RegrowAnim, m_AnimatedTransform.gameObject);
		}
		if (m_RegrowSfxEvent.IsValid())
		{
			m_RegrowSfxEvent.PlayOneShot(visible.trans.position);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && !WasPlacedManually)
		{
			WorldTile tile = visible.tileCache.tile;
			d.Assert(tile != null, "ResourceDispenser.Regrow - Could not find tile for damaged resource dispenser");
			d.Assert(tile.NetTile.IsNotNull(), "ResourceDispenser.Regrow - Tile did not have an attached NetTile");
			if (tile != null && tile.NetTile.IsNotNull())
			{
				tile.NetTile.OnServerSetResourceDamageState(cellCoord, m_CurrentDamageStage);
			}
		}
	}

	private void SwivelAnimBase(Quaternion rot)
	{
		d.Assert(m_AnimatedTransform, "ResourceDispenser - Attempting to swivel base on object '" + base.name + "' when no animated transform present!");
		Quaternion rotation = m_GeometryParent.rotation;
		m_AnimatedTransformParent.rotation = rot;
		m_GeometryParent.rotation = rotation;
	}

	private void DeathAnimFinished()
	{
		float regrowDelay = GetRegrowDelay();
		Deactivate(regrowDelay);
		if (RemoveCurrentDamageGeometry(onRecycle: false))
		{
			ResourceGiverDamageStageChangedEvent.Send(this);
		}
		if (m_DeathDamageInfo.SourceTank != null)
		{
			if (m_DeathDamageInfo.SourceTank == Singleton.playerTank)
			{
				Singleton.Manager<ManStats>.inst.ResourceGiverDestroyed(this, m_DeathDamageInfo);
			}
			else if ((bool)m_DeathDamageInfo.SourceTank.netTech && (bool)m_DeathDamageInfo.SourceTank.netTech.NetPlayer)
			{
				Singleton.Manager<ManStats>.inst.ResourceGiverDestroyed(this, m_DeathDamageInfo);
			}
		}
		ResourceGiverDestroyedEvent.Send(this);
	}

	private float GetRegrowDelay()
	{
		float num = m_RegrowTimeOverride;
		if (num == 0f)
		{
			num = Singleton.Manager<ManGameMode>.inst.GetSceneryRegrowTime();
		}
		return num;
	}

	private void Deactivate(float delay)
	{
		base.gameObject.SetActive(value: false);
		m_RegrowEvent?.Clear();
		if (ManNetwork.IsHost && !m_DontRegrow && !m_RemovedFromWorld)
		{
			if (m_Awake)
			{
				SetRegrowEvent(delay);
			}
			else
			{
				m_SleepingRegrowDelay = delay;
			}
		}
	}

	private void InitState()
	{
		m_RemovedFromWorld = false;
		float num = 0f;
		DamageStage[] damageStages = m_DamageStages;
		for (int i = 0; i < damageStages.Length; i++)
		{
			DamageStage damageStage = damageStages[i];
			num += damageStage.m_Health;
		}
		visible.damageable.SetMaxHealth(num);
		visible.damageable.destroyOnDeath = false;
		m_NumChunksSpawned = 0;
		m_LastNumChunksSpawned = 0;
		m_AtLeastOneThresholdPassed = false;
		m_RegrowTimeOverride = 0f;
	}

	private void RefillDefault()
	{
		visible.damageable.InitHealth(visible.damageable.MaxHealth);
		m_CurrentDamageStage = 0;
		SetupDamageState(m_CurrentDamageStage);
		visible.SetCollidersEnabled(m_Awake);
	}

	private void SetupDamageState(int idx, bool updateGeometry = true)
	{
		m_CurrentDamageStage = idx;
		if (updateGeometry)
		{
			UpdateDamageStateGeometry();
		}
		if (idx < m_DamageStages.Length)
		{
			float num = 0f;
			for (int i = m_CurrentDamageStage + 1; i < m_DamageStages.Length; i++)
			{
				num += m_DamageStages[i].m_Health;
			}
			visible.damageable.SetDamageThreshold(num);
			if (m_DamageStages[m_CurrentDamageStage].m_Invulnerable)
			{
				visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
			}
		}
	}

	private void UpdateDamageStateGeometry()
	{
		bool flag = false;
		if (m_CurrentDamageStageGeometry.IsNotNull() && (m_CurrentDamageStage >= m_DamageStages.Length || Singleton.Manager<ComponentPool>.inst.GetOriginalPrefab(m_CurrentDamageStageGeometry) != m_DamageStages[m_CurrentDamageStage].m_Geometry))
		{
			flag = RemoveCurrentDamageGeometry(onRecycle: false);
		}
		if (m_CurrentDamageStageGeometry.IsNull() && m_CurrentDamageStage < m_DamageStages.Length && m_DamageStages[m_CurrentDamageStage].m_Geometry != null)
		{
			if (!TryGetCustomDamageStageGeomRotation(out var rotation))
			{
				rotation = m_GeometryParent.rotation;
			}
			m_CurrentDamageStageGeometry = m_DamageStages[m_CurrentDamageStage].m_Geometry.Spawn(m_GeometryParent, m_GeometryParent.position, rotation);
			flag = true;
		}
		if (flag)
		{
			ResourceGiverDamageStageChangedEvent.Send(this);
			visible.SetCollidersEnabled(m_Awake);
		}
	}

	private bool TryGetCustomDamageStageGeomRotation(out Quaternion rotation)
	{
		if (m_CurrentDamageStage < m_DamageStages.Length && m_DamageStages[m_CurrentDamageStage].m_ForceUpright)
		{
			if (!Singleton.Manager<ManWorld>.inst.GetTerrainNormal(m_GeometryParent.position, out var outNormal))
			{
				d.LogWarningFormat("UpdateDamageStateGeometry - Trying to get terrain normal at {0} tile {3} for RespDisp at {1} tile {2}, but tile under position isn't loaded!", m_GeometryParent.position, Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(m_GeometryParent.position), visible.trans.position, (visible.tileCache.tile != null) ? visible.tileCache.tile.Coord.ToString() : "null");
				outNormal = Vector3.up;
			}
			rotation = Quaternion.FromToRotation(Vector3.up, outNormal);
			rotation *= Quaternion.Euler(0f, m_GeometryParent.rotation.eulerAngles.y, 0f);
			return true;
		}
		rotation = Quaternion.identity;
		return false;
	}

	private bool RemoveCurrentDamageGeometry(bool onRecycle)
	{
		bool result = false;
		if ((bool)m_CurrentDamageStageGeometry)
		{
			if (m_SceneryFader != null)
			{
				Singleton.Manager<ManSceneryFade>.inst.RevertFadedScenery(m_SceneryFader, onRecycle);
			}
			ResourceGiverDamageStageChangingEvent.Send(this);
			m_CurrentDamageStageGeometry.Recycle();
			m_CurrentDamageStageGeometry = null;
			result = true;
		}
		return result;
	}

	private void CleanupDamageGeometryOnRecycle(bool forceRemove = false)
	{
		Transform currentDamageStageGeometry = m_CurrentDamageStageGeometry;
		bool flag = forceRemove || m_CurrentDamageStage != 0;
		if (m_CurrentDamageStageGeometry.IsNotNull() && flag && RemoveCurrentDamageGeometry(onRecycle: true))
		{
			currentDamageStageGeometry.SetParent(null, worldPositionStays: false);
			ResourceGiverDamageStageChangedEvent.Send(this);
		}
	}

	private bool OnDamageThreshold(ManDamage.DamageInfo info)
	{
		if (m_CurrentDamageStage >= m_DamageStages.Length)
		{
			d.LogError("Damage threshold hit while resource dispenser was in an invalid state");
			m_CurrentDamageStage = m_DamageStages.Length - 1;
		}
		float num = (float)(m_TotalChunks - m_NumChunksSpawned) / (float)(m_DamageStages.Length - m_CurrentDamageStage);
		int num2 = (int)num;
		float num3 = num - (float)num2;
		int num4 = num2 + ((UnityEngine.Random.value < num3) ? 1 : 0);
		if (num4 != 0)
		{
			int num5 = SpawnChunks(num4, info);
			m_NumChunksSpawned += num5;
			if (m_NumChunksSpawned == m_TotalChunks)
			{
				RemoveResourceTrackedOnTile();
			}
		}
		SetupDamageState(m_CurrentDamageStage + 1, updateGeometry: false);
		m_AtLeastOneThresholdPassed = true;
		if (m_CurrentDamageStage < m_DamageStages.Length)
		{
			return !m_DamageStages[m_CurrentDamageStage].m_Invulnerable;
		}
		return false;
	}

	public void OnNetDamage(ManDamage.DamageInfo info, bool thresholdPassed, int damageStage, int numChunksSpawned)
	{
		if (thresholdPassed)
		{
			m_NumChunksSpawned = numChunksSpawned;
			m_CurrentDamageStage = damageStage;
			UpdateDamageStateGeometry();
			if (m_NumChunksSpawned == m_TotalChunks)
			{
				RemoveResourceTrackedOnTile();
			}
		}
		visible.damageable.ApplyDamageOnly(info);
		PlayDamageAnim();
	}

	public void OnClientUpdateDamageState(int damageState)
	{
		if (m_CurrentDamageStage != damageState)
		{
			SetupDamageState(damageState);
			m_AtLeastOneThresholdPassed = m_CurrentDamageStage > 0;
		}
	}

	public void OnClientDeath()
	{
		RemoveFromWorld(spawnChunks: false, neverRegrow: true);
	}

	public void OnClientRegrow()
	{
		m_RemovedFromWorld = false;
		Regrow();
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		if (info.Damage == 0f)
		{
			return;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.IsServer && !WasPlacedManually)
		{
			WorldTile tile = visible.tileCache.tile;
			d.Assert(tile != null, "ResourceDispenser.OnDamaged - Could not find tile for damaged resource dispenser");
			d.Assert(tile?.NetTile.IsNotNull() ?? true, "ResourceDispenser.OnDamaged - Tile did not have an attached NetTile");
			if (tile != null && tile.NetTile.IsNotNull())
			{
				tile.NetTile.OnServerSetResourceDamageState(cellCoord, m_CurrentDamageStage);
			}
		}
		if (m_AtLeastOneThresholdPassed)
		{
			m_AtLeastOneThresholdPassed = false;
			if ((bool)m_DebrisPrefab)
			{
				m_DebrisPrefab.Spawn(info.HitPosition, Quaternion.LookRotation(-info.DamageDirection));
			}
			int numChunksSpawned = m_LastNumChunksSpawned - m_NumChunksSpawned;
			m_LastNumChunksSpawned = m_NumChunksSpawned;
			Singleton.Manager<ManSFX>.inst.PlaySceneryDebrisSFX(this, numChunksSpawned);
			UpdateDamageStateGeometry();
			if (m_CurrentDamageStageGeometry.IsNotNull() && m_CurrentDamageStage == m_DamageStages.Length - 1 && m_DamageStages[m_CurrentDamageStage].m_Invulnerable && info.SourceTank != null)
			{
				if (info.SourceTank == Singleton.playerTank)
				{
					Singleton.Manager<ManStats>.inst.ResourceGiverDestroyed(this, info);
				}
				else if (info.SourceTank.netTech != null && info.SourceTank.netTech.NetPlayer != null)
				{
					Singleton.Manager<ManStats>.inst.ResourceGiverDestroyed(this, info);
				}
			}
		}
		else if ((bool)m_HitPrefab && Time.time > m_HitEffectTimeout)
		{
			m_HitPrefab.Spawn(info.HitPosition, Quaternion.LookRotation(-info.DamageDirection));
			m_HitEffectTimeout = Time.time + Globals.inst.m_SceneryHitEffectMinInterval;
			Singleton.Manager<ManSFX>.inst.PlaySceneryDebrisSFX(this, 0);
		}
		PlayDamageAnim();
	}

	private void OnDeath(Damageable deadThing, ManDamage.DamageInfo info)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "Can't set server damage on the client");
			WorldTile tile = visible.tileCache.tile;
			d.Assert(tile != null, "ResourceDispenser.OnDamaged - Could not find tile for damaged resource dispenser");
			d.Assert(tile?.NetTile.IsNotNull() ?? true, "ResourceDispenser.OnDamaged - Tile did not have an attached NetTile");
			if (tile != null && tile.NetTile.IsNotNull())
			{
				tile.NetTile.OnServerSetResourceDispenserDead(cellCoord);
			}
		}
		Die(info);
	}

	private void Die(ManDamage.DamageInfo info)
	{
		m_DeathDamageInfo = info;
		PlayDeathAnimation(info.DamageDirection);
	}

	private void PlayDeathAnimation(Vector3 damageDirection)
	{
		if ((bool)m_BigDebrisPrefab)
		{
			m_BigDebrisPrefab.Spawn(base.transform.position, Quaternion.Euler(90f, 0f, 0f));
		}
		if (m_DeathAnim != ManSceneryAnimation.AnimTypes.None)
		{
			visible.SetCollidersEnabled(enabled: false);
			if (damageDirection != Vector3.zero)
			{
				SwivelAnimBase(Quaternion.LookRotation(damageDirection.SetY(0f)));
			}
			Singleton.Manager<ManSceneryAnimation>.inst.StopAllAnimation(m_AnimatedTransform.gameObject);
			Singleton.Manager<ManSceneryAnimation>.inst.PlayAnimation(m_DeathAnim, m_AnimatedTransform.gameObject, DeathAnimFinished);
		}
		else
		{
			DeathAnimFinished();
		}
	}

	private void PlayDamageAnim()
	{
		if (m_DamageAnim != ManSceneryAnimation.AnimTypes.None && !Singleton.Manager<ManSceneryAnimation>.inst.IsPlayingAny(m_AnimatedTransform.gameObject))
		{
			SwivelAnimBase(Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f));
			Singleton.Manager<ManSceneryAnimation>.inst.PlayAnimation(m_DamageAnim, m_AnimatedTransform.gameObject);
		}
		Singleton.Manager<ManSFX>.inst.PlaySceneryDestroyedSFX(this);
	}

	private void OnMultiplayerVisualOnlyDamageEvent(Damageable damageable, ManDamage.DamageInfo info)
	{
		OnDamaged(info);
	}

	private void SetRegrowEvent(float delay)
	{
		if (m_RegrowEvent == null)
		{
			m_RegrowEvent = new ManTimedEvents.ManagedEvent(Regrow);
		}
		m_RegrowEvent.Set(delay);
	}

	private void PrePool()
	{
		if (base.transform.GetComponentInChildren<Collider>(includeInactive: true) == null)
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger = true;
			sphereCollider.radius = 0.5f;
		}
		if (m_MinY < m_MaxY)
		{
			int num = Globals.inst.layerSceneryCoarse;
			bool flag = false;
			bool includeInactive = true;
			base.transform.GetComponentsInChildren(includeInactive, s_ExistingColliderLookup);
			for (int i = 0; i < s_ExistingColliderLookup.Count; i++)
			{
				if (s_ExistingColliderLookup[i].gameObject.layer == num)
				{
					flag = true;
					break;
				}
			}
			s_ExistingColliderLookup.Clear();
			if (!flag)
			{
				GameObject obj = new GameObject("CoarseCollider_AutoGenerated")
				{
					layer = num
				};
				CapsuleCollider capsuleCollider = obj.AddComponent<CapsuleCollider>();
				capsuleCollider.radius = m_GroundRadius;
				capsuleCollider.height = m_MaxY - m_MinY + m_GroundRadius * 2f;
				capsuleCollider.center = capsuleCollider.center.SetY((m_MaxY + m_MinY) / 2f);
				bool worldPositionStays = false;
				obj.transform.SetParent(base.transform, worldPositionStays);
			}
		}
		if (m_AnimatedTransform != null)
		{
			LODGroup componentInChildren = m_AnimatedTransform.GetComponentInChildren<LODGroup>();
			if (!Globals.inst.m_DisableMeshMergers && componentInChildren != null && GetComponentInChildren<MeshMerger>() == null)
			{
				componentInChildren.gameObject.AddComponent<MeshMerger>().CombineMeshes();
			}
			AnimEvent component = m_AnimatedTransform.GetComponent<AnimEvent>();
			if ((bool)component)
			{
				d.LogWarning("ResourceDispenser " + base.name + " contained obsolete AnimEvent component! This should be removed from the prefab");
				UnityEngine.Object.DestroyImmediate(component);
			}
			Animation component2 = m_AnimatedTransform.GetComponent<Animation>();
			if ((bool)component2)
			{
				d.LogWarning("ResourceDispenser " + base.name + " contained obsolete Animation component! This should be removed from the prefab");
				UnityEngine.Object.DestroyImmediate(component2);
			}
		}
		if (!QualitySettingsExtended.AllowLightsOnSceneryObjects)
		{
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<TwilightEffectsComponent>());
			Light[] componentsInChildren = base.transform.GetComponentsInChildren<Light>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				UnityEngine.Object.Destroy(componentsInChildren[j].gameObject);
			}
		}
	}

	private void OnPool()
	{
		visible = GetComponent<Visible>();
		visible.damageable.damageThresholdEvent += OnDamageThreshold;
		visible.damageable.damageEvent.Subscribe(OnDamaged);
		visible.damageable.deathEvent.Subscribe(OnDeath);
		visible.damageable.multiplayerDamageVisualOnlyEvent.Subscribe(OnMultiplayerVisualOnlyDamageEvent);
		if ((bool)m_AnimatedTransform)
		{
			m_AnimatedTransformParent = m_AnimatedTransform.parent;
			m_GeometryParent = m_AnimatedTransform.GetChild(0);
		}
		m_TerrainObject = GetComponent<TerrainObject>();
		m_TerrainObject.SetGroundRadius(m_GroundRadius);
		m_SceneryFader = GetComponent<SceneryFader>();
		ResourceGiverDamageStageChangedEvent.Subscribe(m_SceneryFader.OnResourceDispDamaged);
	}

	private void OnSpawn()
	{
		m_SleepingRegrowDelay = -1f;
		int currentDamageStage = m_CurrentDamageStage;
		InitState();
		if (!TryRestoreSavedState())
		{
			RefillDefault();
		}
		if (m_CurrentDamageStage != currentDamageStage && m_CurrentDamageStageGeometry.IsNotNull() && TryGetCustomDamageStageGeomRotation(out var rotation))
		{
			m_CurrentDamageStageGeometry.rotation = rotation;
		}
		if (m_NeedsAnimReset)
		{
			Singleton.Manager<ManSceneryAnimation>.inst.SampleAnimation(m_DeathAnim, m_AnimatedTransform.gameObject);
		}
	}

	private void OnRecycle()
	{
		d.AssertFormat(!m_Awake, "Scenery {0} was recycled but it was still Awake!", base.name);
		m_NeedsAnimReset = m_RemovedFromWorld || visible.damageable.Health <= 0f || Singleton.Manager<ManSceneryAnimation>.inst.IsPlayingAny(m_AnimatedTransform.gameObject);
		CleanupDamageGeometryOnRecycle();
		m_RegrowEvent = null;
		m_ResourceReservoirSaveData = null;
	}

	private void OnDepool()
	{
		CleanupDamageGeometryOnRecycle(forceRemove: true);
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle() && m_SpawnRange != 0f)
		{
			Gizmos.color = Color.cyan;
			Vector3 obj = (m_SpawnPointOverride ? m_SpawnPointOverride.transform.position : base.transform.position);
			Vector3 vector = (m_SpawnPointOverride ? m_SpawnPointOverride.transform.forward : Vector3.up);
			Gizmos.DrawLine(obj, obj + vector * m_SpawnRange);
		}
		if (Singleton.Manager<ManPath>.inst != null && Singleton.Manager<ManPath>.inst.gameObject.EditorSelectedSingle())
		{
			Gizmos.DrawWireCube(base.transform.position, new Vector3(m_GroundRadius * 2f, m_GroundRadius * 2f, m_GroundRadius * 2f));
		}
	}
}
