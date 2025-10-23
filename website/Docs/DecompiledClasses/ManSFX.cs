#define UNITY_EDITOR
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

public class ManSFX : Singleton.Manager<ManSFX>, IWorldTreadmill
{
	public enum WeaponImpactSfxType
	{
		None,
		Bullet,
		Laser,
		Missile,
		Mortar,
		Hammer,
		Cannon,
		Firework,
		MissileMicro,
		Grenade,
		GrenadeBubble,
		SJHarpoonProjectile
	}

	public enum ExplosionType
	{
		None,
		Blocks,
		Weapon,
		Fuel,
		Chemical,
		Firework,
		Pop
	}

	public enum ExplosionSize
	{
		Small,
		Medium,
		Large
	}

	private enum ScenerySFXTypes
	{
		None,
		Rock,
		Tree,
		Gem,
		Metal
	}

	private enum ObjectImpactType
	{
		Tech,
		Block,
		Chunk
	}

	private enum BlockImpactSfxType
	{
		Block,
		RubberWheel
	}

	private enum ChunkSFXTypes
	{
		Crystal,
		Ingot,
		Jelly,
		Ore,
		Rock,
		Rubber,
		Wood,
		Component
	}

	public enum ProjectileFlightType
	{
		None,
		Missile,
		_unused_DeliveryBomb,
		Firework,
		MissileMicro
	}

	public enum TransformLoopingSFXTypes
	{
		None,
		DeliveryBomb,
		SJHarpoon_ChainReeling
	}

	public enum TransformOneshotSFXTypes
	{
		None,
		SPE_Halloween_JackInTheBox
	}

	public enum UISfxType
	{
		AnchorFailed,
		TargetAcquired,
		TargetLost,
		AcceptMission,
		Back,
		Button,
		Buy,
		CheckBox,
		Close,
		Craft,
		DropDown,
		Enter,
		Hover,
		InfoClose,
		InfoOpen,
		Open,
		SaveGame,
		Scroll,
		Select,
		Slider,
		Snapshot,
		EarnMoney,
		EarnXP,
		Hint,
		LevelUp,
		LicenseUpgrade,
		LockOn,
		MissionAccept,
		MissionComplete,
		MissionFailed,
		MissionLog,
		MissionNewObjective,
		MissionStateChange,
		PayloadIncoming,
		PopUpClose,
		PopUpOpen,
		RadarOn,
		RewardCrate,
		SaveGameOverwrite,
		Undo,
		Warning,
		AIFollow,
		AIGuard,
		AIIdle,
		RadialClose,
		RadialHover,
		RadialOpen,
		RadialMove,
		Rename,
		SendToInventoryReady,
		SendToInventoryWheel,
		SwitchTech,
		LoadTech,
		SendToInventory,
		PaintBlockSkin,
		PaintTechSkin
	}

	public enum MiscSfxType
	{
		None,
		IntroWindAndAlarm,
		IntroWhoosh,
		IntroExplosion,
		IntroExplosionHuge,
		IntroCabImpact,
		IntroThudSmall,
		CabDetachKlaxon,
		Artefact,
		StuntRingStart,
		StuntRing,
		StuntComplete,
		AnimGSOCab,
		AnimGCCab,
		AnimVENCab,
		AnimHECab,
		AnimGSOAICab,
		AnimGCAICab,
		AnimVENAICab,
		AnimHEAICab,
		AnimSolarGen,
		AnimPlasmaFurn,
		AnimGSODeliCannonMob,
		AnimGSODeliCannon,
		AnimGCDeliCannon,
		AnimVENDeliCannon,
		AnimGSOPayTerminal,
		AnimGCPayTerminal,
		AnimVENPayTerminal,
		AnimHEPayTerminal,
		AnimGSOSCU,
		AnimGCSCU,
		AnimVENSCU,
		AnimHESCU,
		AnimGSOFabricator,
		AnimGCFabricator,
		AnimGSOScrapper,
		AnimGCScrapper,
		AnimHEScrapper,
		AnimHERefinery,
		AnimHEFabricator,
		AnimHEMortar,
		AnimHEDeliCannon,
		AnimCrateUnlock,
		AnimCrateOpen,
		CheatCode,
		StuntFailed,
		BuildBeam,
		PayloadIncoming,
		AnimBFCab,
		AnimBFLaserTrapdoor,
		AnimBFSmallSkidOpen,
		AnimBFSmallSkidClose,
		AnimBFLargeSkidOpen,
		AnimBFLargeSkidClose,
		AnimBFSolarOpen,
		AnimBFSolarClose,
		AnimBFAntiGravPip,
		AnimBFAntiGravApple,
		AnimBFAntiGravEco,
		AnimBFFabricator,
		AnimBFScrapper,
		AnimBFAirReceiver,
		SJBubbleCannonRetract
	}

	private struct PositionedSFXInstance
	{
		public WorldPosition position;

		public FMODEventInstance eventInstance;
	}

	private struct AttachedSFXInstance
	{
		public Transform transform;

		public Rigidbody rigidbody;

		public FMODEventInstance eventInstance;
	}

	public struct LoopingSFXKey
	{
		public string eventPath;

		public Transform transform;

		public LoopingSFXKey(string eventPath, Transform transform)
		{
			this.eventPath = eventPath;
			this.transform = transform;
		}
	}

	public class LoopinSFXKeyComparer : IEqualityComparer<LoopingSFXKey>
	{
		public bool Equals(LoopingSFXKey x, LoopingSFXKey y)
		{
			if (x.transform == y.transform && x.eventPath == y.eventPath)
			{
				return true;
			}
			return false;
		}

		public int GetHashCode(LoopingSFXKey key)
		{
			return (17 * 31 + key.eventPath.GetHashCode()) * 31 + key.transform.GetHashCode();
		}
	}

	[FormerlySerializedAs("m_ProjectileImpactEvents")]
	[EnumArray(typeof(WeaponImpactSfxType))]
	[SerializeField]
	[Header("Projectiles")]
	private FMODEvent[] m_WeaponImpactEvents = new FMODEvent[1];

	[EnumArray(typeof(ProjectileFlightType))]
	[SerializeField]
	private FMODEvent[] m_ProjectileFlightEvents = new FMODEvent[1];

	[SerializeField]
	[EnumArray(typeof(TransformLoopingSFXTypes))]
	[Header("Misc Transform Following SFX")]
	private FMODEvent[] m_TransformLoopingEvents = new FMODEvent[1];

	[SerializeField]
	[EnumArray(typeof(TransformOneshotSFXTypes))]
	private FMODEvent[] m_TransformOneshotEvents = new FMODEvent[1];

	[Header("Explosions")]
	[SerializeField]
	[EnumArray(typeof(ExplosionType))]
	private FMODEvent[] m_ExplosionEvents = new FMODEvent[1];

	[SerializeField]
	[EnumArray(typeof(ExplosionSize))]
	private int[] m_ExplosionSize;

	[EnumArray(typeof(ScenerySFXTypes))]
	[SerializeField]
	private FMODEvent[] m_SceneryDebrisEvents = new FMODEvent[1];

	[EnumArray(typeof(ScenerySFXTypes))]
	[SerializeField]
	private FMODEvent[] m_SceneryDestroyedEvents = new FMODEvent[1];

	[EnumArray(typeof(BlockImpactSfxType))]
	[SerializeField]
	private FMODEvent[] m_BlockImpactEvents = new FMODEvent[1];

	[SerializeField]
	[EnumArray(typeof(ChunkSFXTypes))]
	private FMODEvent[] m_ChunkImpactEvents = new FMODEvent[1];

	[SerializeField]
	private FMODEvent m_TechImpactEvent;

	[SerializeField]
	[EnumArray(typeof(ObjectImpactType))]
	private float[] m_ImpactRepeatTimeouts = new float[1];

	[EnumArray(typeof(WeaponImpactSfxType))]
	[SerializeField]
	private float[] m_ImpactWeaponRepeatTimeouts = new float[1];

	[SerializeField]
	[EnumArray(typeof(UISfxType))]
	private FMODEvent[] m_UIEvents = new FMODEvent[1];

	[SerializeField]
	[EnumArray(typeof(MiscSfxType))]
	private FMODEvent[] m_MiscEvents = new FMODEvent[1];

	[SerializeField]
	private int m_MaxExplosionSfxPerFrameConsole = 10;

	private Dictionary<LoopingSFXKey, FMODEventInstance> m_LoopingSFX = new Dictionary<LoopingSFXKey, FMODEventInstance>(new LoopinSFXKeyComparer());

	private float m_sfxVolume = 1f;

	private List<PositionedSFXInstance> m_PositionedSFX = new List<PositionedSFXInstance>(128);

	private List<AttachedSFXInstance> m_AttachedSFX = new List<AttachedSFXInstance>(128);

	private float[] m_LastObjectImpactTimes;

	private float[] m_LastWeaponImpactTimes;

	private int m_LastPlayUISoundFrame;

	private int m_NumExplosionSfxThisFrame;

	private int m_NumExplosionSfxRejectedThisFrame;

	private static readonly FMODEvent.FMODParams[] s_ExplosionParams = new FMODEvent.FMODParams[2]
	{
		new FMODEvent.FMODParams("mass", 0f),
		new FMODEvent.FMODParams("corp", 0f)
	};

	private static readonly FMODEvent.FMODParams[] s_HitParams = new FMODEvent.FMODParams[5]
	{
		new FMODEvent.FMODParams("techCorp", 0f),
		new FMODEvent.FMODParams("blockCorp", 0f),
		new FMODEvent.FMODParams("scenery", 0f),
		new FMODEvent.FMODParams("chunk", 0f),
		new FMODEvent.FMODParams("terrain", 0f)
	};

	private static readonly FMODEvent.FMODParams[] s_TechImpactParams = new FMODEvent.FMODParams[2]
	{
		new FMODEvent.FMODParams("Size", 0f),
		new FMODEvent.FMODParams("Velocity", 0f)
	};

	private static readonly FMODEvent.FMODParams[] s_BlockImpactParams = new FMODEvent.FMODParams[2]
	{
		new FMODEvent.FMODParams("Mass", 0f),
		new FMODEvent.FMODParams("Velocity", 0f)
	};

	public float SFXVolume
	{
		get
		{
			return m_sfxVolume;
		}
		set
		{
			m_sfxVolume = value;
			AudioListener.volume = value;
		}
	}

	public void PlayExplosionSFX(Vector3 position, ExplosionType type, ExplosionSize explosionSize, FactionSubTypes corpType)
	{
		if (m_ExplosionEvents[(int)type].IsValid())
		{
			m_NumExplosionSfxThisFrame++;
			s_ExplosionParams.SetParamValue(0, m_ExplosionSize[(int)explosionSize]);
			s_ExplosionParams.SetParamValue(1, (float)corpType);
			m_ExplosionEvents[(int)type].PlayOneShot(position, s_ExplosionParams);
		}
	}

	public void PlayImpactSFX(Tank sourceTech, WeaponImpactSfxType impactType, Damageable damageable, Vector3 hitPoint, Collider otherCollider)
	{
		if (impactType == WeaponImpactSfxType.None)
		{
			return;
		}
		if (CanPlayImpact(impactType, Time.time) && m_WeaponImpactEvents[(int)impactType].IsValid() && (sourceTech == null || otherCollider.attachedRigidbody != sourceTech.rbody))
		{
			SetLastImpactTime(impactType, Time.time);
			if (GetImpactParams(otherCollider, damageable, out var techCorp, out var blockCorp, out var sceneryType, out var chunkType, out var terrainType))
			{
				s_HitParams.SetParamValue(0, techCorp);
				s_HitParams.SetParamValue(1, blockCorp);
				s_HitParams.SetParamValue(2, sceneryType);
				s_HitParams.SetParamValue(3, chunkType);
				s_HitParams.SetParamValue(4, terrainType);
				m_WeaponImpactEvents[(int)impactType].PlayOneShot(hitPoint, s_HitParams);
			}
		}
	}

	public bool TryStartProjectileFlightSFX(Projectile projectile)
	{
		LoopingSFXKey sfxKey;
		return TryStartProjectileSFX(projectile.FlightSFXType, projectile.trans, out sfxKey);
	}

	public bool TryStartProjectileSFX(ProjectileFlightType sfxType, Transform transform, out LoopingSFXKey sfxKey)
	{
		return TryPlayLoopingSFX(m_ProjectileFlightEvents[(int)sfxType], transform, out sfxKey);
	}

	public bool TryStopProjectileSFX(Projectile projectile)
	{
		return TryStopProjectileSFX(projectile.FlightSFXType, projectile.transform);
	}

	public bool TryStopProjectileSFX(ProjectileFlightType sfxType, Transform transform)
	{
		return TryStopLoopingSFX(m_ProjectileFlightEvents[(int)sfxType], transform);
	}

	public bool TryStartTransformLoopingSFX(TransformLoopingSFXTypes sfxType, Transform objectTransform)
	{
		LoopingSFXKey sfxKey;
		return TryStartTransformLoopingSFX(sfxType, objectTransform, out sfxKey);
	}

	public bool TryStartTransformLoopingSFX(TransformLoopingSFXTypes sfxType, Transform objectTransform, out LoopingSFXKey sfxKey)
	{
		return TryPlayLoopingSFX(m_TransformLoopingEvents[(int)sfxType], objectTransform, out sfxKey);
	}

	public void SetTransformLoopingSFXParam(TransformLoopingSFXTypes sfxType, Transform transform, string paramName, float paramValue)
	{
		TrySetLoopingSFXParam(m_TransformLoopingEvents[(int)sfxType], transform, paramName, paramValue);
	}

	public void SetTransformLoopingSFXParam(LoopingSFXKey eventKey, string paramName, float paramValue)
	{
		TrySetLoopingSFXParam(eventKey, paramName, paramValue);
	}

	public bool TryStopTransformLoopingSFX(TransformLoopingSFXTypes sfxType, Transform transform)
	{
		return TryStopLoopingSFX(m_TransformLoopingEvents[(int)sfxType], transform);
	}

	public void PlayTransformOneshotSFX(TransformOneshotSFXTypes sfxType, Transform transform)
	{
		m_TransformOneshotEvents[(int)sfxType].PlayOneShot(transform);
	}

	public void PlaySceneryDebrisSFX(ResourceDispenser scenery, int numChunksSpawned)
	{
		SceneryTypes sceneryType = scenery.GetSceneryType();
		int scenerySFXType = (int)GetScenerySFXType(sceneryType);
		if (m_SceneryDebrisEvents[scenerySFXType].IsValid())
		{
			m_SceneryDebrisEvents[scenerySFXType].PlayOneShot(scenery.visible.trans.position, new FMODEvent.FMODParams("amount", numChunksSpawned));
		}
	}

	public void PlaySceneryDestroyedSFX(ResourceDispenser scenery)
	{
		SceneryTypes sceneryType = scenery.GetSceneryType();
		int scenerySFXType = (int)GetScenerySFXType(sceneryType);
		if (m_SceneryDestroyedEvents[scenerySFXType].IsValid())
		{
			m_SceneryDestroyedEvents[scenerySFXType].PlayOneShot(scenery.visible.trans.position);
		}
	}

	public void PlayTechImpactSFX(Tank tech, Tank.CollisionInfo collision)
	{
		if (CanPlayImpact(ObjectImpactType.Tech, Time.time) && m_TechImpactEvent.IsValid())
		{
			SetLastImpactTime(ObjectImpactType.Tech, Time.time);
			Vector3 position = Vector3.zero;
			float paramValue = 1f;
			float paramValue2 = 1f;
			if (tech.rbody != null)
			{
				position = tech.rbody.position;
				paramValue = tech.rbody.mass;
			}
			if (collision != null)
			{
				position = collision.point;
				paramValue2 = collision.relVelocity.magnitude;
			}
			s_TechImpactParams.SetParamValue(0, paramValue);
			s_TechImpactParams.SetParamValue(1, paramValue2);
			m_TechImpactEvent.PlayOneShot(position, s_TechImpactParams);
		}
	}

	public void PlayBlockImpactSFX(TankBlock block, Collision collision)
	{
		int blockImpactSFXTypes = (int)GetBlockImpactSFXTypes(block);
		if (!CanPlayImpact(ObjectImpactType.Block, Time.time) || !m_BlockImpactEvents[blockImpactSFXTypes].IsValid())
		{
			return;
		}
		SetLastImpactTime(ObjectImpactType.Block, Time.time);
		float currentMass = block.CurrentMass;
		float paramValue = 0f;
		Vector3 position = block.trans.position;
		if (collision != null)
		{
			paramValue = collision.relativeVelocity.magnitude;
			if (collision.contacts.Length != 0)
			{
				position = collision.contacts[0].point;
			}
		}
		s_BlockImpactParams.SetParamValue(0, currentMass);
		s_BlockImpactParams.SetParamValue(1, paramValue);
		m_BlockImpactEvents[blockImpactSFXTypes].PlayOneShot(position, s_BlockImpactParams);
	}

	public void PlayChunkImpactSFX(ResourcePickup pickup, float velocity)
	{
		ChunkTypes chunkType = pickup.ChunkType;
		int chunkSFXTypes = (int)GetChunkSFXTypes(chunkType);
		if (CanPlayImpact(ObjectImpactType.Chunk, Time.time) && m_ChunkImpactEvents[chunkSFXTypes].IsValid())
		{
			SetLastImpactTime(ObjectImpactType.Chunk, Time.time);
			m_ChunkImpactEvents[chunkSFXTypes].PlayOneShot(pickup.rbody.position, new FMODEvent.FMODParams("Velocity", velocity));
		}
	}

	public void PlayUISFX(UISfxType sfxType)
	{
		if (m_LastPlayUISoundFrame != Time.frameCount)
		{
			FMODEvent fMODEvent = m_UIEvents[(int)sfxType];
			if (fMODEvent.IsValid())
			{
				fMODEvent.PlayOneShot();
				m_LastPlayUISoundFrame = Time.frameCount;
			}
		}
	}

	public void SuppressUISFX()
	{
		m_LastPlayUISoundFrame = Time.frameCount;
	}

	public void PlayMiscSFX(MiscSfxType sfxType)
	{
		PlayMiscSFX(sfxType, Singleton.cameraTrans.position);
	}

	public void PlayMiscSFX(MiscSfxType sfxType, Vector3 position)
	{
		if (sfxType != MiscSfxType.None)
		{
			d.Assert(m_MiscEvents[(int)sfxType].IsValid(), "Invalid sound found for " + sfxType);
			m_MiscEvents[(int)sfxType].PlayOneShot(position);
		}
	}

	public void PlayMiscLoopingSFX(MiscSfxType sfxType)
	{
		PlayMiscLoopingSFX(sfxType, Singleton.cameraTrans);
	}

	public void PlayMiscLoopingSFX(MiscSfxType sfxType, Transform transform)
	{
		TryPlayLoopingSFX(m_MiscEvents[(int)sfxType], transform, out var _);
	}

	public void StopMiscLoopingSFX(MiscSfxType sfxType)
	{
		StopMiscLoopingSFX(sfxType, Singleton.cameraTrans);
	}

	public void StopMiscLoopingSFX(MiscSfxType sfxType, Transform transform)
	{
		if (sfxType != MiscSfxType.None)
		{
			FMODEvent evt = m_MiscEvents[(int)sfxType];
			TryStopLoopingSFX(evt, transform);
		}
	}

	public void AttachInstanceToPosition(FMODEventInstance instance, Vector3 scenePos)
	{
		m_PositionedSFX.Add(new PositionedSFXInstance
		{
			position = WorldPosition.FromScenePosition(in scenePos),
			eventInstance = instance
		});
		instance.set3DAttributes(scenePos);
	}

	public void AttachInstanceToTransform(FMODEventInstance instance, Transform trans, Rigidbody rbody)
	{
		m_AttachedSFX.Add(new AttachedSFXInstance
		{
			transform = trans,
			rigidbody = rbody,
			eventInstance = instance
		});
		instance.set3DAttributes(trans, rbody);
	}

	private bool GetImpactParams(Collider colliderHit, Damageable damageableHit, out int techCorp, out int blockCorp, out int sceneryType, out int chunkType, out int terrainType)
	{
		bool result = false;
		techCorp = 0;
		blockCorp = 0;
		sceneryType = 0;
		chunkType = 0;
		terrainType = 0;
		if ((bool)damageableHit)
		{
			Visible visible = Visible.FindVisibleUpwards(damageableHit);
			if ((bool)visible)
			{
				switch (visible.type)
				{
				case ObjectTypes.Vehicle:
				{
					FactionSubTypes mainCorp = visible.tank.GetMainCorp();
					techCorp = (int)mainCorp;
					result = true;
					break;
				}
				case ObjectTypes.Block:
				case ObjectTypes.Crate:
				{
					FactionSubTypes factionSubTypes = ((visible.type == ObjectTypes.Crate) ? FactionSubTypes.GSO : Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)visible.ItemType));
					blockCorp = (int)factionSubTypes;
					result = true;
					break;
				}
				case ObjectTypes.Scenery:
				{
					ScenerySFXTypes scenerySFXType = GetScenerySFXType((SceneryTypes)visible.ItemType);
					sceneryType = (int)scenerySFXType;
					result = true;
					break;
				}
				case ObjectTypes.Chunk:
					chunkType = 1;
					result = true;
					break;
				}
			}
		}
		else if (colliderHit.IsTerrain() || (bool)colliderHit.GetComponentInParents<TerrainObject>(thisObjectFirst: true))
		{
			terrainType = 1;
			result = true;
		}
		return result;
	}

	private ScenerySFXTypes GetScenerySFXType(SceneryTypes sceneryType)
	{
		switch (sceneryType)
		{
		case SceneryTypes.ConeTree:
		case SceneryTypes.ShroomTree:
		case SceneryTypes.ChristmasTree:
		case SceneryTypes.MountainTree:
		case SceneryTypes.DesertTree:
		case SceneryTypes.DeadTree:
			return ScenerySFXTypes.Tree;
		case SceneryTypes.SmokerRock:
		case SceneryTypes.DesertRock:
		case SceneryTypes.CarbiteSeam:
		case SceneryTypes.OleiteSeam:
		case SceneryTypes.PlumbiteSeam:
		case SceneryTypes.MountainRock:
		case SceneryTypes.GrasslandRock:
		case SceneryTypes.RoditeSeam:
		case SceneryTypes.TitaniteSeam:
		case SceneryTypes.CrystalSpire:
		case SceneryTypes.Pillar:
		case SceneryTypes.WastelandRock:
			return ScenerySFXTypes.Rock;
		case SceneryTypes.CelestiteOutcrop:
		case SceneryTypes.EruditeOutcrop:
		case SceneryTypes.IgniteOutcrop:
		case SceneryTypes.LuxiteOutcrop:
			return ScenerySFXTypes.Gem;
		case SceneryTypes.ScrapPile:
			return ScenerySFXTypes.Metal;
		default:
			d.LogErrorFormat("ManSFX.GetScenerySFXType - Failed to find mapping from SceneryType ({0}) to ScenerySFXType! Did a new type get added?", sceneryType);
			return ScenerySFXTypes.None;
		}
	}

	private ChunkSFXTypes GetChunkSFXTypes(ChunkTypes chunkType)
	{
		ChunkSFXTypes result;
		switch (chunkType)
		{
		case ChunkTypes.Wood:
			result = ChunkSFXTypes.Wood;
			break;
		case ChunkTypes.RubberJelly:
		case ChunkTypes.OleiteJelly:
			result = ChunkSFXTypes.Jelly;
			break;
		case ChunkTypes.FibronChunk:
		case ChunkTypes.RubberBrick:
		case ChunkTypes.OlasticBrick:
		case ChunkTypes.RodiusCapsule:
			result = ChunkSFXTypes.Rubber;
			break;
		case ChunkTypes.PlumbiteOre:
		case ChunkTypes.TitaniteOre:
		case ChunkTypes.CarbiteOre:
		case ChunkTypes.RoditeOre:
		case ChunkTypes.IgniteShard:
		case ChunkTypes.CelestiteShard:
		case ChunkTypes.EruditeShard:
		case ChunkTypes.LuxiteShard:
			result = ChunkSFXTypes.Ore;
			break;
		case ChunkTypes.PlumbiaIngot:
		case ChunkTypes.TitaniaIngot:
		case ChunkTypes.CarbiusBrick:
			result = ChunkSFXTypes.Ingot;
			break;
		case ChunkTypes.IgnianCrystal:
		case ChunkTypes.CelestianCrystal:
		case ChunkTypes.ErudianCrystal:
		case ChunkTypes.LuxianCrystal:
			result = ChunkSFXTypes.Crystal;
			break;
		case ChunkTypes.FibrePlating:
		case ChunkTypes.PlubonicGreebles:
		case ChunkTypes.HardenedTitanic:
		case ChunkTypes.BlastCaps:
		case ChunkTypes.FuelInjector:
		case ChunkTypes.AcidElectrode:
		case ChunkTypes.HeatCoil:
		case ChunkTypes.HardLightDrive:
		case ChunkTypes.SensoryTransmitter:
		case ChunkTypes.RuggedFiblar:
		case ChunkTypes.PlubonicAlloy:
		case ChunkTypes.TitanicAlloy:
		case ChunkTypes.Z4Explosives:
		case ChunkTypes.IonPulseCell:
		case ChunkTypes.CycloneJet:
		case ChunkTypes.PlasmaEmitter:
		case ChunkTypes.ZeroPointSplitter:
		case ChunkTypes.MotherBrain:
		case ChunkTypes.DervishGel:
		case ChunkTypes.CoffmanCell:
		case ChunkTypes.ThermoJet:
		case ChunkTypes.GluonBeam:
		case ChunkTypes.SeedAI:
		case ChunkTypes.ProximaDark:
			result = ChunkSFXTypes.Component;
			break;
		case ChunkTypes._deprecated_TerreriaIngot:
		case ChunkTypes._deprecated_ThermiaIngot:
		case ChunkTypes._deprecated_FulmeniaIngot:
		case ChunkTypes._deprecated_FunderiaIngot:
		case ChunkTypes._deprecated_PenniaIngot:
		case ChunkTypes._deprecated_BosoniaIngot:
		case ChunkTypes._deprecated_ChristmasPresent1:
		case ChunkTypes._deprecated_ChristmasPresent2:
		case ChunkTypes._deprecated_ChristmasPresent3:
		case ChunkTypes._deprecated_ChristmasPresent4:
		case ChunkTypes._deprecated_Stone:
		case ChunkTypes._deprecated_HeartOre:
		case ChunkTypes._deprecated_HeartCrystal:
		case ChunkTypes._deprecated_CommOre:
		case ChunkTypes._deprecated_CommCrystal:
		case ChunkTypes._deprecated_SenseOre:
		case ChunkTypes._deprecated_SenseCrystal:
		case ChunkTypes._deprecated_SmallMetalOre:
		case ChunkTypes._deprecated_SmallMetalIngot:
		case ChunkTypes._deprecated_AlloyExpRes:
			result = ChunkSFXTypes.Rock;
			break;
		default:
			result = ChunkSFXTypes.Rock;
			d.LogErrorFormat("ManSFX.GetChunkSFXType - Failed to find mapping from ChunkType {0} to ChunkSFXType! Did a new type get added?", chunkType);
			break;
		}
		return result;
	}

	private BlockImpactSfxType GetBlockImpactSFXTypes(TankBlock block)
	{
		if (block.BlockCategory == BlockCategories.Wheels)
		{
			return BlockImpactSfxType.RubberWheel;
		}
		return BlockImpactSfxType.Block;
	}

	private bool CanPlayImpact(ObjectImpactType type, float time)
	{
		if (m_LastObjectImpactTimes[(int)type] + m_ImpactRepeatTimeouts[(int)type] < Time.time)
		{
			return true;
		}
		return false;
	}

	private void SetLastImpactTime(ObjectImpactType type, float time)
	{
		m_LastObjectImpactTimes[(int)type] = time;
	}

	private bool CanPlayImpact(WeaponImpactSfxType type, float time)
	{
		if (m_LastWeaponImpactTimes[(int)type] + m_ImpactWeaponRepeatTimeouts[(int)type] < Time.time)
		{
			return true;
		}
		return false;
	}

	private void SetLastImpactTime(WeaponImpactSfxType type, float time)
	{
		m_LastWeaponImpactTimes[(int)type] = time;
	}

	private bool TryPlayLoopingSFX(FMODEvent evt, Transform transform, out LoopingSFXKey key)
	{
		d.Assert(evt.IsValid(), "Invalid sound found");
		key = new LoopingSFXKey(evt.EventPath, transform);
		if (!evt.IsValid())
		{
			d.LogError("Attempted to fire invalid audio event! Did you not assign a valid type?");
			return false;
		}
		if (m_LoopingSFX.ContainsKey(key))
		{
			d.LogErrorFormat("ManSFX.PlayLoopingSFX - Already playing evt {0} on transform {1}. Only one sound per transform is currently supported", evt.EventPath, (transform != null) ? transform.name : "null");
			return false;
		}
		FMODEventInstance value = evt.PlayEventTrackedObject(transform, null);
		m_LoopingSFX.Add(key, value);
		return true;
	}

	private bool TrySetLoopingSFXParam(FMODEvent evt, Transform transform, string paramName, float paramValue)
	{
		if (!evt.IsValid())
		{
			d.LogError("Attempted to set param on invalid audio event! Did you not assign a valid type?");
			return false;
		}
		return TrySetLoopingSFXParam(new LoopingSFXKey(evt.EventPath, transform), paramName, paramValue);
	}

	private bool TrySetLoopingSFXParam(LoopingSFXKey eventKey, string paramName, float paramValue)
	{
		if (!m_LoopingSFX.TryGetValue(eventKey, out var value))
		{
			return false;
		}
		value.setParameterValue(paramName, paramValue);
		return true;
	}

	private bool TryStopLoopingSFX(FMODEvent evt, Transform transform)
	{
		if (!evt.IsValid())
		{
			d.LogError("Attempted to stop invalid audio event! Did you not assign a valid type?");
			return false;
		}
		LoopingSFXKey key = new LoopingSFXKey(evt.EventPath, transform);
		if (m_LoopingSFX.TryGetValue(key, out var value))
		{
			value.StopAndRelease();
			m_LoopingSFX.Remove(key);
			return true;
		}
		return false;
	}

	private void ClearAllLoopingSFX()
	{
		foreach (KeyValuePair<LoopingSFXKey, FMODEventInstance> item in m_LoopingSFX)
		{
			item.Value.StopAndRelease();
		}
		m_LoopingSFX.Clear();
	}

	private void OnManagersCreated()
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeFinish);
		Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Subscribe(OnTechAnchorFailed);
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
		Singleton.Manager<ManWorldTreadmill>.inst.OnAfterWorldOriginMoved.Subscribe(OnAfterWorldOriginMoved);
	}

	private void OnModeFinish(Mode mode)
	{
		ClearAllLoopingSFX();
		Singleton.Manager<ManWorldTreadmill>.inst.OnAfterWorldOriginMoved.Unsubscribe(OnAfterWorldOriginMoved);
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		int count = m_PositionedSFX.Count;
		for (int i = 0; i < count; i++)
		{
			PositionedSFXInstance positionedSFXInstance = m_PositionedSFX[i];
			Vector3 position = positionedSFXInstance.position.ScenePosition + amountToMove;
			positionedSFXInstance.eventInstance.set3DAttributes(position);
		}
	}

	public void OnAfterWorldOriginMoved(IntVector3 amountMoved)
	{
		int count = m_AttachedSFX.Count;
		for (int i = 0; i < count; i++)
		{
			AttachedSFXInstance attachedSFXInstance = m_AttachedSFX[i];
			attachedSFXInstance.eventInstance.set3DAttributes(attachedSFXInstance.transform, attachedSFXInstance.rigidbody);
		}
	}

	private void OnTechAnchorFailed()
	{
		PlayUISFX(UISfxType.AnchorFailed);
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		Singleton.DoOnceAfterStart(OnManagersCreated);
		m_LastObjectImpactTimes = new float[m_ImpactRepeatTimeouts.Length];
		m_LastWeaponImpactTimes = new float[m_ImpactWeaponRepeatTimeouts.Length];
	}

	private void Update()
	{
		if (m_NumExplosionSfxRejectedThisFrame > 0)
		{
			d.Log($"NumExplosionSfxRejectedThisFrame={m_NumExplosionSfxRejectedThisFrame}");
		}
		m_NumExplosionSfxThisFrame = 0;
		m_NumExplosionSfxRejectedThisFrame = 0;
		for (int num = m_PositionedSFX.Count - 1; num >= 0; num--)
		{
			FMODEventInstance eventInstance = m_PositionedSFX[num].eventInstance;
			if (!eventInstance.m_EventInstance.isValid() || eventInstance.CheckPlaybackState(PLAYBACK_STATE.STOPPED))
			{
				m_PositionedSFX.RemoveAt(num);
			}
		}
		for (int num2 = m_AttachedSFX.Count - 1; num2 >= 0; num2--)
		{
			FMODEventInstance eventInstance2 = m_AttachedSFX[num2].eventInstance;
			if (m_AttachedSFX[num2].transform == null || !m_AttachedSFX[num2].transform.gameObject.activeSelf || !eventInstance2.m_EventInstance.isValid() || eventInstance2.CheckPlaybackState(PLAYBACK_STATE.STOPPED))
			{
				m_AttachedSFX.RemoveAt(num2);
			}
			else
			{
				eventInstance2.set3DAttributes(m_AttachedSFX[num2].transform, m_AttachedSFX[num2].rigidbody);
			}
		}
	}
}
