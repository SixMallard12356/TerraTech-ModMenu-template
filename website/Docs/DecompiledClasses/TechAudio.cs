#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechAudio : TechComponent
{
	private struct WheelWrapper
	{
		public ModuleWheels wheelModule;

		public float groundedTimeStamp;
	}

	private enum States
	{
		Idle,
		Active
	}

	[Serializable]
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class TechAudioEvent
	{
		public FMODEvent m_Event;

		public bool m_AlwaysRunning = true;

		[NonSerialized]
		public FMODEventInstance m_EventInstance;

		[NonSerialized]
		public bool UninitialisedEventParams = true;

		[NonSerialized]
		public Tank m_Tech;

		[NonSerialized]
		private int[] m_InstanceIndLookup;

		private int m_Priority = -1;

		public List<string> m_ParamNames { get; internal set; }

		public bool GetActive()
		{
			return m_EventInstance.IsInited;
		}

		public void SetActive(bool isActive)
		{
			if (isActive && !m_EventInstance.IsInited)
			{
				m_EventInstance = m_Event.PlayEventTrackedObject(m_Tech.trans, m_Tech.rbody);
				if (m_EventInstance.IsInited)
				{
					m_EventInstance.m_EventInstance.setProperty(EVENT_PROPERTY.CHANNELPRIORITY, m_Priority);
					UninitialisedEventParams = true;
				}
			}
			if (!isActive && m_EventInstance.IsInited)
			{
				m_EventInstance.StopAndRelease();
			}
		}

		public void SetPriority(int priority)
		{
			if (priority != m_Priority)
			{
				m_Priority = priority;
				if (m_EventInstance.IsInited)
				{
					m_EventInstance.m_EventInstance.setProperty(EVENT_PROPERTY.CHANNELPRIORITY, m_Priority);
				}
			}
		}

		public void SetTimelinePosition(int milliSeconds)
		{
			if (m_EventInstance.IsInited)
			{
				m_EventInstance.m_EventInstance.setTimelinePosition(milliSeconds);
			}
		}

		public bool ParamToInstanceInd(int paramInd, out int instanceInd)
		{
			if (m_InstanceIndLookup == null)
			{
				m_InstanceIndLookup = new int[m_ParamNames.Count];
				for (int i = 0; i < m_ParamNames.Count; i++)
				{
					m_InstanceIndLookup[i] = int.MinValue;
				}
				EventDescription eventDescription = RuntimeManager.GetEventDescription(m_Event.EventPath);
				eventDescription.getParameterCount(out var count);
				for (int j = 0; j < count; j++)
				{
					eventDescription.getParameterByIndex(j, out var parameter);
					for (int k = 0; k < m_ParamNames.Count; k++)
					{
						if (m_ParamNames[k] == parameter.name)
						{
							d.Assert(parameter.index != int.MinValue, "fmod parameter returned index that clashes with our own sentinel");
							m_InstanceIndLookup[k] = parameter.index;
							break;
						}
					}
				}
			}
			instanceInd = int.MinValue;
			if (paramInd >= 0 && paramInd < m_InstanceIndLookup.Length)
			{
				instanceInd = m_InstanceIndLookup[paramInd];
			}
			return instanceInd != int.MinValue;
		}
	}

	[Serializable]
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public class TechAudioEventSimple
	{
		public SFXPlaybackType m_PlaybackType;

		public FMODEvent m_Event;
	}

	public struct AudioTickData
	{
		public IModuleAudioProvider provider;

		public TankBlock block;

		public SFXType sfxType;

		public int numTriggered;

		public float triggerCooldown;

		public bool isNoteOn;

		public float adsrTime01;

		public int SFXTypeIndex => (int)sfxType;

		public static AudioTickData ConfigureOneshot(Module module, SFXType sfxType)
		{
			return ConfigureOneshot(module.block, sfxType);
		}

		public static AudioTickData ConfigureOneshot(TankBlock block, SFXType sfxType)
		{
			return ConfigureOneshot(block, sfxType, Globals.inst.m_TechAudioOneshotCooldown);
		}

		public static AudioTickData ConfigureOneshot(TankBlock block, SFXType sfxType, float cooldown, int numTriggered = 1)
		{
			return new AudioTickData
			{
				block = block,
				sfxType = sfxType,
				triggerCooldown = cooldown,
				numTriggered = numTriggered,
				isNoteOn = false,
				adsrTime01 = 0f
			};
		}

		public static AudioTickData ConfigureLoopedADSR<T>(T module, SFXType sfxType, bool isNoteOn, float adsrTime01, float cooldown = 1f, int numTriggered = 1) where T : Module, IModuleAudioProvider
		{
			return ConfigureLoopedADSR(module, module.block, sfxType, isNoteOn, adsrTime01, cooldown, numTriggered);
		}

		public static AudioTickData ConfigureLoopedADSR(IModuleAudioProvider provider, TankBlock block, SFXType sfxType, bool isNoteOn, float adsrTime01, float cooldown = 1f, int numTriggered = 1)
		{
			return new AudioTickData
			{
				provider = provider,
				block = block,
				sfxType = sfxType,
				isNoteOn = isNoteOn,
				adsrTime01 = adsrTime01,
				numTriggered = (isNoteOn ? numTriggered : 0),
				triggerCooldown = cooldown
			};
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class UpdateAudioCache
	{
		public int wheelsTotalNum;

		public int wheelsGroundedNum;

		public List<int> wheelsGroundedPerType = new List<int>();

		public int boosterTotalNum;

		public int boosterActiveNum;

		public List<int> boosterActiveNumPerType = new List<int>();

		public List<float> boosterTypeFireRate = new List<float>();

		public int wingsTotalNum;

		public int hoversTotalNum;

		public int hoversActiveNum;

		public float hoversMaxPushForce;

		public int hoversSmallNum;

		public int hoversMediumNum;

		public int hoversLargeNum;

		public float gravityScale;

		public int numLinearMotion;

		public float linearMotionPower;

		public FactionSubTypes corpMain;

		public int corpBlockCount = -1;

		public Dictionary<int, float> corpPercentages = new Dictionary<int, float>();

		public List<float> biomePercentages = new List<float>();

		public static List<T> Init<T>(List<T> list, int size)
		{
			if (list == null)
			{
				list = new List<T>(size);
			}
			int num = list.Count;
			if (num > size)
			{
				list.RemoveRange(size, num - size);
				num = size;
			}
			for (int i = 0; i < size; i++)
			{
				if (i >= num)
				{
					list.Add(default(T));
				}
				else
				{
					list[i] = default(T);
				}
			}
			return list;
		}
	}

	public interface IModuleAudioProvider
	{
		SFXType SFXType { get; }

		event Action<AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class LoopingADSRState
	{
		public FMODEventInstance eventInstance;

		public bool wasNoteOn;

		public bool wasInMainSequence;
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class EventParameters
	{
		private struct EventValue
		{
			public string name;

			public float value;

			public bool dirty;
		}

		private int setIndex;

		private EventValue[] values = new EventValue[32];

		public EventParameters()
		{
			Reset();
		}

		public void CompleteFrame()
		{
			setIndex = 0;
			for (int i = 0; i < values.Length; i++)
			{
				values[i].dirty = false;
			}
		}

		public void SetValueAtFixedIndex(string name, float value)
		{
			ref EventValue reference = ref values[setIndex];
			if (reference.value != value)
			{
				reference.name = name;
				reference.dirty = true;
				reference.value = value;
			}
			setIndex++;
			if (setIndex >= values.Length)
			{
				Array.Resize(ref values, values.Length * 2);
			}
		}

		public void SetValueAtVariableIndex(string name, float value)
		{
			ref EventValue reference = ref values[setIndex];
			if (reference.value != value || reference.name != name)
			{
				reference.name = name;
				reference.dirty = true;
				reference.value = value;
			}
			setIndex++;
			if (setIndex >= values.Length)
			{
				Array.Resize(ref values, values.Length * 2);
			}
		}

		public void SetValuesAtFixedIndex(string[] names, List<float> values)
		{
			for (int i = 0; i < names.Length; i++)
			{
				SetValueAtFixedIndex(names[i], values[i]);
			}
		}

		public void ApplyParameters(TechAudioEvent evtData, bool forceUpdateAll = false)
		{
			for (int i = 0; i < setIndex; i++)
			{
				ref EventValue reference = ref values[i];
				if (!reference.dirty && !evtData.UninitialisedEventParams)
				{
					continue;
				}
				List<string> paramNames = evtData.m_ParamNames;
				int count = paramNames.Count;
				for (int j = 0; j < count; j++)
				{
					if (paramNames[j] == reference.name)
					{
						if (evtData.ParamToInstanceInd(j, out var instanceInd))
						{
							evtData.m_EventInstance.m_EventInstance.setParameterValueByIndex(instanceInd, reference.value);
						}
						break;
					}
				}
			}
			evtData.UninitialisedEventParams = false;
		}

		public void Reset()
		{
			for (int i = 0; i < values.Length; i++)
			{
				values[i].value = float.MinValue;
			}
			setIndex = 0;
		}

		public string DebugLogValues()
		{
			string text = "Params: ";
			for (int i = 0; i < setIndex; i++)
			{
				text += $"\n{values[i].name}: Value = {values[i].value} Changed = {values[i].dirty}";
			}
			return text;
		}
	}

	public enum AudioEventTypes
	{
		Engine,
		EngineOn,
		EngineOff,
		Wheels,
		WheelSurface,
		WingSurface,
		Fans,
		Hovers,
		AntiGrav
	}

	public enum WheelTypes
	{
		RubberWheel,
		LargeRubberWheel,
		SmallWheel,
		MetalWheel,
		TankTrack,
		VentureWheel,
		HawkeyeWheel,
		SJCageWheel,
		SJStudWheel,
		SJScrewTrack,
		SJRollerWheel
	}

	private enum SizeUsage
	{
		BlockCount,
		TechRadius,
		FilledCells,
		Mass
	}

	public enum BoosterEngineType
	{
		Propeller,
		LargeRotor,
		MediumRotor,
		RotorFan,
		Fan,
		SteeringHover,
		IonDrive,
		JetEngine,
		Ornithopter,
		BatWings,
		Broomstick
	}

	public enum SFXPlaybackType
	{
		Oneshot,
		LoopedADSR
	}

	public enum SFXType
	{
		Default,
		CoilLaserSmall,
		LightMachineGun,
		StudLaser,
		PoundCannon,
		MiniMortar,
		MegatonCannon,
		VPipMachineGun,
		VHailFireRifle,
		VOozeeSMG,
		HEShotgun,
		HEMachineGun,
		HEBurstGun,
		HERailGun,
		HEChainGun,
		HEMiniGun,
		HECannonTurret,
		HEHomingMissile,
		HECruiseMissile,
		GSOCab,
		VENCab,
		VENAIGuardModule,
		HECab,
		GSOAIAnchor,
		GSOGuardController,
		HEAITurret,
		VENFlameThrower,
		GSODrillSmall,
		GSODrillLarge,
		GCRamSpike,
		GCTripleBore,
		GCJackHammer,
		GCPlasmaCutter,
		GCDiggerScoopLift,
		GCDiggerScoopRelease,
		GCBuzzSaw,
		DeliveryCannon,
		Refinery,
		ComponentFactory,
		Fabricator,
		Scrapper,
		Generator,
		AutoMiner,
		GeothermalGenerator,
		ItemResourceConsumed,
		ItemResourceProduced,
		ItemBlockConsumed,
		ItemBlockProduced,
		ItemCannonDelivered,
		ItemFiltered,
		ResourceCalling,
		ItemPickup,
		BlockMagnetLoop,
		BlockMagnetPickup,
		SCULoop,
		SCUItemConsumed,
		SCUPickupLoop,
		ScrapperPickupLoop,
		VENRPGLauncher,
		Anchored,
		UnAnchored,
		GSORepuslor,
		HEMortarEnd,
		GCBuzzSawEnd,
		MegatonLongBarrel,
		GCPlasmaCutterAuto,
		VENMachineGunFixedForward,
		VENShotgunRotating,
		VENLaserNano,
		VENCannonRapid,
		VENLaserMG,
		FlameThrowerPlasma,
		VENMortarRoundabout,
		VENShotgunCombat,
		GSOShotgunCombat,
		HECannonBattleShip,
		HELaserZeus,
		BigBertha,
		FireworksLauncher,
		VENMicroMissile,
		BFLaser,
		BFLaserCannon,
		BFLaserGatling,
		BFLaserGatlingDeploy,
		BFLaserScatter,
		BFLaserScatterDeploy,
		BFLaserCyberDisk,
		BFLaserFlyingSaucer,
		AnchoredSky,
		UnAnchoredSky,
		BFCab,
		RRHFLaser,
		RRSonicBoomBlaster,
		RRVibroKnife,
		EXP_Circuits_Actuator_Gate_Loop,
		EXP_Circuits_Actuator_Ramp_Loop,
		EXP_Circuits_Sensor_Tech_Loop,
		EXP_Circuits_Actuator_Repulsor_Loop,
		SJFusionDeploy,
		SJFusion,
		SJCannon,
		SJChainsaw,
		SJFlameThrower,
		SJHarpoon,
		SJMachineGun,
		SJDrill,
		SJBubbleGrenade,
		SJFusionPickUpLoop,
		SJCatapult,
		SJCogsJamming,
		SJRadar,
		SJEngine,
		SJBooster,
		SJScrapGun,
		RRFastenerLock,
		RRFastenerUnlock,
		EXP_Circuits_Note_Xylophone,
		EXP_Circuits_Note_Percussion,
		SPE_Halloween_Teeth,
		SJBalloon,
		SJBalloonInflate,
		SJBalloonDeflate
	}

	public class SFXTypeComparer : IEqualityComparer<SFXType>
	{
		public bool Equals(SFXType x, SFXType y)
		{
			return x == y;
		}

		public int GetHashCode(SFXType obj)
		{
			return (int)obj;
		}
	}

	[EnumArray(typeof(AudioEventTypes))]
	[SerializeField]
	private TechAudioEvent[] m_Events;

	[SerializeField]
	private bool m_AverageRadius;

	[SerializeField]
	private SizeUsage m_SizeUsage;

	[SerializeField]
	private float m_GroundedTime = 1f;

	[SerializeField]
	private float m_TurningThrottlePercentage = 0.5f;

	[Header("Idle Throttle")]
	[SerializeField]
	private bool m_IdleThrottle;

	[SerializeField]
	private float m_StationarySpeedThreshold = 5f;

	[SerializeField]
	private float m_StationaryEngineThrottle = 0.15f;

	[SerializeField]
	private float m_MaxSpeedBasedEngineThrottle = 0.6f;

	[SerializeField]
	[Range(0f, 1f)]
	[Space]
	private float m_ThrottleUpInterpSpeed = 0.95f;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_ThrottleDownInterpSpeed = 0.5f;

	[Header("Weapons and Bases SFX")]
	[SerializeField]
	[EnumArray(typeof(SFXType))]
	[FormerlySerializedAs("m_WeaponFireEvents")]
	private TechAudioEventSimple[] m_SimpleEvents = new TechAudioEventSimple[1];

	[SerializeField]
	[Tooltip("How many instances of the same sound can play at one time")]
	private int m_MaxLoopedADSRVoicesPerSFX = 3;

	[Tooltip("Allow one shots to play more rapidly when it is the player tech")]
	[SerializeField]
	private float m_PlayerCooldownModifier = 0.1f;

	[SerializeField]
	[Tooltip("Repeat one shots less often when non player tech")]
	private float m_OtherTechCooldownModifier = 1f;

	[SerializeField]
	[Space]
	private bool m_DebugToConsole;

	[SerializeField]
	[EnumArray(typeof(AudioEventTypes))]
	private bool[] m_DebugSoundEnabled;

	[SerializeField]
	private float m_RoadForceDamper = 0.5f;

	[SerializeField]
	private float m_RoadForceScale = 0.1f;

	[SerializeField]
	private float m_RoadForceCompensate;

	[SerializeField]
	private bool m_TestSpeedRoadForce;

	private States m_State;

	private EventParameters m_TechParameters = new EventParameters();

	private const int kFMODPriorityDefault = -1;

	private const int kFMODPriorityPlayerTank = 32;

	private Dictionary<int, List<WheelWrapper>> m_WheelsByType = new Dictionary<int, List<WheelWrapper>>();

	private int m_WheelCount;

	private bool m_PrevDriving;

	private Dictionary<int, HashSet<ModuleBooster>> m_BoosterTypes = new Dictionary<int, HashSet<ModuleBooster>>();

	private HashSet<ModuleWing> m_Wings = new HashSet<ModuleWing>();

	private HashSet<ModuleHover> m_Hovers = new HashSet<ModuleHover>();

	private HashSet<ModuleLinearMotionEngine> m_LinearMotionEngines = new HashSet<ModuleLinearMotionEngine>();

	private HashSet<IModuleAudioProvider> m_Modules = new HashSet<IModuleAudioProvider>();

	private Dictionary<SFXType, float> m_OneshotHistory = new Dictionary<SFXType, float>(new SFXTypeComparer());

	private float m_CurrentOneshotCooldownModifier = 1f;

	private Dictionary<IModuleAudioProvider, LoopingADSRState> m_LoopingADSRHistory = new Dictionary<IModuleAudioProvider, LoopingADSRState>();

	private Dictionary<SFXType, int> m_LoopingADSRCountPerSFX = new Dictionary<SFXType, int>(new SFXTypeComparer());

	private UpdateAudioCache m_Cache = new UpdateAudioCache();

	private float m_Throttle;

	private float m_DampedRoadForce;

	private float m_Drive;

	private float m_Turn;

	private static readonly List<string> k_WheelParams = new List<string> { "Throttle", "Speed", "Turn", "Size", "ActiveWheelCount" };

	private static readonly List<string> k_EngineParams = new List<string> { "Throttle", "Speed", "GroundSpeedForce", "Turn", "Size", "ActiveWheelCount", "MainCorp" };

	private static readonly List<string> k_WingParams = new List<string> { "Speed", "Turn", "Size", "InAir", "NumWings" };

	private static readonly List<string> k_FanParams = new List<string> { "Turn", "LMEngine", "LMEngineRate" };

	private static readonly List<string> k_HoverParams = new List<string> { "Speed", "Turn", "Size", "NumHovers", "ActiveHovers", "HoverPush", "SmallHover", "MediumHover", "LargeHover" };

	private static int s_EventCount;

	private static List<string>[] s_ParameterLists;

	private static string[] s_BiomeTypeNames;

	private static string[] s_WheelTypeNames;

	private static string[] s_CorpNames;

	private static int s_NumBoosterTypes;

	private static string[] s_NumActiveFansOfTypeNames;

	private static string[] s_FanTypeFireRateNames;

	private const string kFanTypeCountNameFormat = "{0}";

	private const string kFanTypeFireRateNameFormat = "{0}Rate";

	public int PriorityRank { get; set; }

	public void AddWheel(ModuleWheels wheel)
	{
		if (!m_WheelsByType.TryGetValue((int)wheel.AudioType, out var value))
		{
			value = new List<WheelWrapper>();
			m_WheelsByType.Add((int)wheel.AudioType, value);
		}
		value.Add(new WheelWrapper
		{
			wheelModule = wheel,
			groundedTimeStamp = 0f
		});
		m_WheelCount++;
	}

	public void RemoveWheel(ModuleWheels wheel)
	{
		if (m_WheelsByType.TryGetValue((int)wheel.AudioType, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				if (value[i].wheelModule == wheel)
				{
					value[i] = value[value.Count - 1];
					value.RemoveAt(value.Count - 1);
					break;
				}
			}
		}
		else
		{
			d.LogError("TechAudio.RemoveWheel - Wheel " + wheel.name + " is not tracked (Dictionary). How?");
		}
		m_WheelCount--;
	}

	public void AddBooster(ModuleBooster booster)
	{
		if (!booster.IsRocketBooster)
		{
			if (!m_BoosterTypes.TryGetValue((int)booster.BoosterEngineType, out var value))
			{
				value = new HashSet<ModuleBooster>();
				m_BoosterTypes.Add((int)booster.BoosterEngineType, value);
			}
			value.Add(booster);
		}
	}

	public void RemoveBooster(ModuleBooster booster)
	{
		if (!booster.IsRocketBooster)
		{
			bool condition = false;
			if (m_BoosterTypes.TryGetValue((int)booster.BoosterEngineType, out var value))
			{
				condition = value.Remove(booster);
			}
			d.AssertFormat(condition, "TechAudio.RemoveBooster - Booster {0} is not tracked (Dictionary). How?", booster.name);
		}
	}

	public void AddWing(ModuleWing wing)
	{
		m_Wings.Add(wing);
	}

	public void RemoveWing(ModuleWing wing)
	{
		d.AssertFormat(m_Wings.Remove(wing), "TechAudio.RemoveWing - Wing {0} is not tracked (List). How?", wing.name);
	}

	public void AddHover(ModuleHover hover)
	{
		m_Hovers.Add(hover);
	}

	public void RemoveHover(ModuleHover hover)
	{
		d.AssertFormat(m_Hovers.Remove(hover), "TechAudio.RemoveHover - Hover {0} is not tracked (List). How?", hover.name);
	}

	public void AddLinearMotionEngine(ModuleLinearMotionEngine lme)
	{
		m_LinearMotionEngines.Add(lme);
	}

	public void RemoveLinearMotionEngine(ModuleLinearMotionEngine lme)
	{
		d.AssertFormat(m_LinearMotionEngines.Remove(lme), "TechAudio.RemoveLinearMotionEngine - LME {0} is not tracked (List). How?", lme.name);
	}

	public void AddModule<T>(T module) where T : IModuleAudioProvider
	{
		m_Modules.Add(module);
		module.OnAudioTickUpdate += OnModuleTickData;
	}

	public void RemoveModule<T>(T module) where T : IModuleAudioProvider
	{
		d.AssertFormat(m_Modules.Remove(module), "TechAudio.RemoveModule - Module {0} is not tracked (List). How?", module.SFXType);
		Action<AudioTickData, FMODEvent.FMODParams> value = OnModuleTickData;
		module.OnAudioTickUpdate -= value;
		LoopingADSRState value2 = null;
		if (m_LoopingADSRHistory.TryGetValue(module, out value2) && value2.eventInstance.IsInited)
		{
			value2.eventInstance.StopAndRelease();
			m_LoopingADSRHistory.Remove(module);
			Dictionary<SFXType, int> loopingADSRCountPerSFX = m_LoopingADSRCountPerSFX;
			SFXType sFXType = module.SFXType;
			int value3 = loopingADSRCountPerSFX[sFXType] - 1;
			loopingADSRCountPerSFX[sFXType] = value3;
		}
	}

	public void PlayOneshot(SFXType sfxType)
	{
		PlayOneshot(base.Tech.blockman.GetRootBlock(), sfxType);
	}

	public void PlayOneshot(TankBlock block, SFXType sfxType)
	{
		PlayOneshot(block, sfxType, Globals.inst.m_TechAudioOneshotCooldown);
	}

	public void PlayOneshot(TankBlock block, SFXType sfxType, float cooldown)
	{
		PlayOneshot(AudioTickData.ConfigureOneshot(block, sfxType, cooldown));
	}

	public void PlayOneshot(AudioTickData data)
	{
		PlayOneshot(data, FMODEvent.FMODParams.empty);
	}

	public void PlayOneshot(AudioTickData data, FMODEvent.FMODParams additionalParam)
	{
		int sFXTypeIndex = data.SFXTypeIndex;
		TechAudioEventSimple techAudioEventSimple = m_SimpleEvents[sFXTypeIndex];
		if (techAudioEventSimple.m_PlaybackType != SFXPlaybackType.Oneshot)
		{
			d.LogError(string.Concat("TechAudio.PlayOneshot - Attempting to play looped audio (", data.sfxType, ") as one shot, this would cause awful hanging sounds. Abandoned request"));
		}
		else
		{
			PlayOneShotClamped(data.sfxType, techAudioEventSimple.m_Event, data.triggerCooldown, data.block.trans.position, additionalParam);
		}
	}

	public int NumAudioProvidingModules(UpdateAudioCache cache)
	{
		return cache.boosterTotalNum + cache.hoversTotalNum + cache.wheelsTotalNum + cache.wingsTotalNum + m_Modules.Count;
	}

	public float CalculateRawPriority()
	{
		float num = float.MaxValue;
		if (base.Tech.blockman.blockCount == 0)
		{
			return -1f;
		}
		if (base.Tech == Singleton.playerTank)
		{
			return 0f;
		}
		if (base.Tech.IsAnchored && NumAudioProvidingModules(m_Cache) <= 0)
		{
			return -1f;
		}
		float sqrMagnitude = (base.Tech.boundsCentreWorld - Singleton.cameraTrans.position).sqrMagnitude;
		if (sqrMagnitude < Globals.inst.AudioMaxDistance * Globals.inst.AudioMaxDistance)
		{
			return sqrMagnitude;
		}
		return -1f;
	}

	private void PlayOneShotClamped(SFXType type, FMODEvent audioEvent, float cooldown, Vector3 position)
	{
		PlayOneShotClamped(type, audioEvent, cooldown, position, FMODEvent.FMODParams.empty);
	}

	private void PlayOneShotClamped(SFXType type, FMODEvent audioEvent, float cooldown, Vector3 position, FMODEvent.FMODParams additionalParam)
	{
		if (audioEvent.IsValid())
		{
			m_OneshotHistory.TryGetValue(type, out var value);
			if (Time.time - value >= cooldown * m_CurrentOneshotCooldownModifier)
			{
				audioEvent.PlayOneShot(position, additionalParam);
				m_OneshotHistory[type] = Time.time;
			}
		}
	}

	private FMODEventInstance PlayTrackedEvent(FMODEvent eventToPlay)
	{
		if (eventToPlay.IsValid())
		{
			return eventToPlay.PlayEventTrackedObject(base.Tech.trans, base.Tech.rbody);
		}
		d.LogErrorFormat("TechAudio.PlayTrackedEvent - Trying to play invalid event, EventPath: \"{0}\"", eventToPlay.EventPath);
		return default(FMODEventInstance);
	}

	private float GetSizeParam()
	{
		float num = 0f;
		switch (m_SizeUsage)
		{
		case SizeUsage.BlockCount:
			num = base.Tech.blockman.blockCount;
			break;
		case SizeUsage.TechRadius:
			num = ((!m_AverageRadius) ? base.Tech.visible.Radius : ((base.Tech.blockBounds.extents.x + base.Tech.blockBounds.extents.z + base.Tech.blockBounds.extents.y) / 3f));
			break;
		case SizeUsage.FilledCells:
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = base.Tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				num += (float)current.filledCells.Length;
			}
			break;
		}
		case SizeUsage.Mass:
			num = base.Tech.rbody.mass;
			break;
		}
		return num;
	}

	private void GetCorpParams(UpdateAudioCache cache)
	{
		if (cache.corpBlockCount == base.Tech.blockman.blockCount)
		{
			return;
		}
		cache.corpBlockCount = base.Tech.blockman.blockCount;
		List<int> list = new List<int>(cache.corpPercentages.Keys);
		foreach (int item in list)
		{
			cache.corpPercentages[item] = 0f;
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = base.Tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			TankBlock current2 = enumerator2.Current;
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(current2.BlockType);
			if (!cache.corpPercentages.ContainsKey((int)corporation))
			{
				int num = (int)corporation;
				cache.corpPercentages.Add(num, 0f);
				list.Add(num);
			}
			cache.corpPercentages[(int)corporation]++;
		}
		cache.corpMain = FactionSubTypes.NULL;
		float num2 = 0f;
		foreach (int item2 in list)
		{
			float num3 = cache.corpPercentages[item2];
			if (num3 > num2)
			{
				num2 = num3;
				cache.corpMain = (FactionSubTypes)item2;
			}
			cache.corpPercentages[item2] = num3 / (float)cache.corpBlockCount;
		}
	}

	private void GetWheelParams(UpdateAudioCache cache)
	{
		cache.wheelsTotalNum = 0;
		cache.wheelsGroundedNum = 0;
		UpdateAudioCache.Init(cache.wheelsGroundedPerType, s_WheelTypeNames.Length);
		float num = 0f;
		for (int i = 0; i < s_WheelTypeNames.Length; i++)
		{
			int num2 = 0;
			WheelTypes key = (WheelTypes)i;
			if (m_WheelsByType.TryGetValue((int)key, out var value))
			{
				cache.wheelsTotalNum += value.Count;
				for (int j = 0; j < value.Count; j++)
				{
					bool flag = false;
					if (value[j].wheelModule.Grounded)
					{
						value[j] = new WheelWrapper
						{
							wheelModule = value[j].wheelModule,
							groundedTimeStamp = Time.time
						};
						flag = true;
					}
					else
					{
						flag = Time.time < value[j].groundedTimeStamp + m_GroundedTime;
					}
					if (flag)
					{
						num2++;
						num += value[j].wheelModule.AverageRoadForceSqr();
					}
				}
			}
			cache.wheelsGroundedNum += num2;
			cache.wheelsGroundedPerType[i] = num2;
		}
		float num3 = ((cache.wheelsGroundedNum == 0) ? 0f : Mathf.Sqrt(num / (float)cache.wheelsGroundedNum));
		m_DampedRoadForce = (1f - m_RoadForceDamper) * m_DampedRoadForce + m_RoadForceDamper * num3;
	}

	private void GetBoosterParams(UpdateAudioCache cache)
	{
		cache.boosterTotalNum = 0;
		cache.boosterActiveNum = 0;
		UpdateAudioCache.Init(cache.boosterActiveNumPerType, s_NumBoosterTypes);
		UpdateAudioCache.Init(cache.boosterTypeFireRate, s_NumBoosterTypes);
		for (int i = 0; i < s_NumBoosterTypes; i++)
		{
			BoosterEngineType key = (BoosterEngineType)i;
			cache.boosterActiveNumPerType[i] = 0;
			float num = 0f;
			if (m_BoosterTypes.TryGetValue((int)key, out var value))
			{
				foreach (ModuleBooster item in value)
				{
					cache.boosterTotalNum++;
					if (item.IsFiring || item.FireRate > 0.01f)
					{
						cache.boosterActiveNum++;
						List<int> boosterActiveNumPerType = cache.boosterActiveNumPerType;
						int index = i;
						int value2 = boosterActiveNumPerType[index] + 1;
						boosterActiveNumPerType[index] = value2;
					}
					num = Mathf.Max(num, item.FireRate);
				}
			}
			cache.boosterTypeFireRate[i] = num;
		}
	}

	private void GetWingParams(UpdateAudioCache cache)
	{
		cache.wingsTotalNum = m_Wings.Count;
	}

	private void GetHoverParams(UpdateAudioCache cache)
	{
		cache.hoversTotalNum = m_Hovers.Count;
		cache.hoversActiveNum = 0;
		cache.hoversMaxPushForce = 0f;
		cache.hoversSmallNum = 0;
		cache.hoversMediumNum = 0;
		cache.hoversLargeNum = 0;
		foreach (ModuleHover hover in m_Hovers)
		{
			float averageNormalizedPushForce = hover.GetAverageNormalizedPushForce();
			if (hover.UseBoosterAudio)
			{
				int boosterAudioType = (int)hover.BoosterAudioType;
				cache.boosterTotalNum++;
				if (averageNormalizedPushForce > 0.01f)
				{
					cache.boosterActiveNum++;
					List<int> boosterActiveNumPerType = cache.boosterActiveNumPerType;
					int index = boosterAudioType;
					int value = boosterActiveNumPerType[index] + 1;
					boosterActiveNumPerType[index] = value;
					cache.boosterTypeFireRate[boosterAudioType] = Mathf.Max(cache.boosterTypeFireRate[boosterAudioType], averageNormalizedPushForce);
				}
				continue;
			}
			cache.hoversMaxPushForce = Mathf.Max(cache.hoversMaxPushForce, averageNormalizedPushForce);
			if (averageNormalizedPushForce > 0.01f)
			{
				cache.hoversActiveNum++;
			}
			switch (hover.HoverSize)
			{
			case ModuleHover.Size.Small:
				cache.hoversSmallNum++;
				break;
			case ModuleHover.Size.Medium:
				cache.hoversMediumNum++;
				break;
			case ModuleHover.Size.Large:
				cache.hoversLargeNum++;
				break;
			}
		}
	}

	private void GetAntiGravParams(UpdateAudioCache cache)
	{
		cache.gravityScale = (base.Tech.EnableGravity ? base.Tech.GetGravityScale() : 1f);
	}

	private void GetLinearMotionEngineParams(UpdateAudioCache cache)
	{
		cache.numLinearMotion = 0;
		cache.linearMotionPower = 0f;
		foreach (ModuleLinearMotionEngine linearMotionEngine in m_LinearMotionEngines)
		{
			cache.numLinearMotion++;
			cache.linearMotionPower = Mathf.Max(Mathf.Abs(linearMotionEngine.CurrentPower), cache.linearMotionPower);
		}
	}

	private void GetBiomeParams(UpdateAudioCache cache)
	{
		UpdateAudioCache.Init(cache.biomePercentages, s_BiomeTypeNames.Length);
		ManWorld.CachedBiomeBlendWeights currentBiomeWeights = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights;
		int numWeights = currentBiomeWeights.NumWeights;
		for (int i = 0; i < numWeights; i++)
		{
			Biome biome = currentBiomeWeights.Biome(i);
			if ((bool)biome)
			{
				BiomeTypes biomeType = biome.BiomeType;
				cache.biomePercentages[(int)biomeType] += currentBiomeWeights.Weight(i);
			}
		}
	}

	private void IdleUpdate()
	{
		EngineOnOffUpdate(driving: false);
	}

	private void EngineOnOffUpdate(bool driving)
	{
		TechAudioEvent techAudioEvent = m_Events[1];
		TechAudioEvent techAudioEvent2 = m_Events[2];
		if (driving && !m_PrevDriving)
		{
			m_PrevDriving = true;
			techAudioEvent2.m_EventInstance.StopAndRelease();
			if (techAudioEvent != null)
			{
				techAudioEvent.m_EventInstance = PlayTrackedEvent(techAudioEvent.m_Event);
			}
			return;
		}
		if (!driving && m_PrevDriving)
		{
			m_PrevDriving = false;
			techAudioEvent.m_EventInstance.StopAndRelease();
			if (techAudioEvent2 != null)
			{
				techAudioEvent2.m_EventInstance = PlayTrackedEvent(techAudioEvent2.m_Event);
			}
			return;
		}
		if (techAudioEvent2.m_EventInstance.IsInited && techAudioEvent.m_EventInstance.CheckPlaybackState(PLAYBACK_STATE.STOPPED))
		{
			techAudioEvent2.m_EventInstance.StopAndRelease();
		}
		if (techAudioEvent.m_EventInstance.IsInited && techAudioEvent.m_EventInstance.CheckPlaybackState(PLAYBACK_STATE.STOPPED))
		{
			techAudioEvent.m_EventInstance.StopAndRelease();
		}
	}

	private void ActiveUpdate()
	{
		GetWheelParams(m_Cache);
		int wheelsTotalNum = m_Cache.wheelsTotalNum;
		int wheelsGroundedNum = m_Cache.wheelsGroundedNum;
		List<int> wheelsGroundedPerType = m_Cache.wheelsGroundedPerType;
		GetBoosterParams(m_Cache);
		int boosterTotalNum = m_Cache.boosterTotalNum;
		int boosterActiveNum = m_Cache.boosterActiveNum;
		List<int> boosterActiveNumPerType = m_Cache.boosterActiveNumPerType;
		List<float> boosterTypeFireRate = m_Cache.boosterTypeFireRate;
		GetWingParams(m_Cache);
		int wingsTotalNum = m_Cache.wingsTotalNum;
		GetHoverParams(m_Cache);
		int hoversTotalNum = m_Cache.hoversTotalNum;
		int hoversActiveNum = m_Cache.hoversActiveNum;
		float hoversMaxPushForce = m_Cache.hoversMaxPushForce;
		float value = 0f;
		float value2 = 0f;
		float value3 = 0f;
		if (hoversTotalNum > 0)
		{
			float num = 1f / (float)hoversTotalNum;
			value = (float)m_Cache.hoversSmallNum * num;
			value2 = (float)m_Cache.hoversMediumNum * num;
			value3 = (float)m_Cache.hoversLargeNum * num;
		}
		GetAntiGravParams(m_Cache);
		float gravityScale = m_Cache.gravityScale;
		GetLinearMotionEngineParams(m_Cache);
		int numLinearMotion = m_Cache.numLinearMotion;
		float linearMotionPower = m_Cache.linearMotionPower;
		float num2 = ((!((float)wheelsGroundedNum > 0f)) ? 1 : 0);
		bool driving = (float)wheelsGroundedNum > 0f && m_Drive != 0f;
		EngineOnOffUpdate(driving);
		float sizeParam = GetSizeParam();
		GetCorpParams(m_Cache);
		FactionSubTypes corpMain = m_Cache.corpMain;
		Dictionary<int, float> corpPercentages = m_Cache.corpPercentages;
		Vector3 vector = base.Tech.trans.InverseTransformDirection(base.Tech.rbody.velocity);
		float num3 = vector.z;
		float num4 = Mathf.MoveTowards(num3, 0f, m_RoadForceScale * m_DampedRoadForce + m_RoadForceCompensate * num3);
		if (m_TestSpeedRoadForce)
		{
			num3 = num4;
		}
		GetBiomeParams(m_Cache);
		List<float> biomePercentages = m_Cache.biomePercentages;
		float num5 = m_Drive;
		if (wheelsGroundedNum == 0)
		{
			num5 = 0f;
		}
		else if (m_Drive == 0f && m_Turn != 0f)
		{
			num5 = m_TurningThrottlePercentage;
		}
		if (m_IdleThrottle)
		{
			float b = m_MaxSpeedBasedEngineThrottle;
			float num6 = Mathf.Abs(num3);
			if (num6 < m_StationarySpeedThreshold)
			{
				float num7 = Mathf.InverseLerp(0f, m_StationarySpeedThreshold, num6);
				num7 *= num7;
				b = Mathf.Lerp(m_StationaryEngineThrottle, m_MaxSpeedBasedEngineThrottle, num7);
			}
			num5 = ((num5 >= 0f) ? Mathf.Max(num5, b) : Mathf.Min(num5, b));
		}
		float t = ((num5 - m_Throttle >= 0f == num5 > 0f) ? m_ThrottleUpInterpSpeed : m_ThrottleDownInterpSpeed);
		m_Throttle = Mathf.Lerp(m_Throttle, num5, t);
		bool flag = vector.sqrMagnitude > 0.1f;
		TechAudioEvent obj = m_Events[0];
		TechAudioEvent techAudioEvent = m_Events[3];
		TechAudioEvent techAudioEvent2 = m_Events[4];
		TechAudioEvent techAudioEvent3 = m_Events[5];
		TechAudioEvent techAudioEvent4 = m_Events[6];
		TechAudioEvent techAudioEvent5 = m_Events[7];
		TechAudioEvent techAudioEvent6 = m_Events[8];
		int priority = ((PriorityRank == 0) ? 32 : (-1));
		obj.SetActive(wheelsTotalNum > 0);
		obj.SetPriority(priority);
		techAudioEvent.SetActive(wheelsTotalNum > 0);
		techAudioEvent.SetPriority(priority);
		techAudioEvent2.SetActive(num2 == 0f && flag);
		techAudioEvent2.SetPriority(priority);
		techAudioEvent3.SetActive(wingsTotalNum > 0);
		techAudioEvent3.SetPriority(priority);
		techAudioEvent4.SetActive(boosterTotalNum > 0 || numLinearMotion > 0);
		techAudioEvent4.SetPriority(priority);
		techAudioEvent5.SetActive(hoversTotalNum > 0);
		techAudioEvent5.SetPriority(priority);
		bool active = techAudioEvent6.GetActive();
		bool flag2 = gravityScale < 0.99f;
		techAudioEvent6.SetActive(flag2 && (active || Singleton.Manager<ManTechMaterialSwap>.inst.AntiGravAnimLooped));
		techAudioEvent6.SetPriority(priority);
		m_TechParameters.SetValueAtFixedIndex("Throttle", m_Throttle);
		m_TechParameters.SetValueAtFixedIndex("Speed", num3);
		m_TechParameters.SetValueAtFixedIndex("GroundSpeedForce", num4);
		m_TechParameters.SetValueAtFixedIndex("Turn", m_Turn);
		m_TechParameters.SetValueAtFixedIndex("Size", sizeParam);
		m_TechParameters.SetValueAtFixedIndex("MainCorp", (float)corpMain);
		m_TechParameters.SetValueAtFixedIndex("InAir", num2);
		m_TechParameters.SetValueAtFixedIndex("TotalWheelCount", wheelsTotalNum);
		m_TechParameters.SetValueAtFixedIndex("ActiveWheelCount", wheelsGroundedNum);
		m_TechParameters.SetValueAtFixedIndex("TotalFanCount", boosterTotalNum);
		m_TechParameters.SetValueAtFixedIndex("ActiveFanCount", boosterActiveNum);
		m_TechParameters.SetValueAtFixedIndex("NumWings", wingsTotalNum);
		m_TechParameters.SetValueAtFixedIndex("NumHovers", hoversTotalNum);
		m_TechParameters.SetValueAtFixedIndex("ActiveHovers", hoversActiveNum);
		m_TechParameters.SetValueAtFixedIndex("HoverPush", hoversMaxPushForce);
		m_TechParameters.SetValueAtFixedIndex("SmallHover", value);
		m_TechParameters.SetValueAtFixedIndex("MediumHover", value2);
		m_TechParameters.SetValueAtFixedIndex("LargeHover", value3);
		m_TechParameters.SetValuesAtFixedIndex(s_BiomeTypeNames, biomePercentages);
		for (int i = 0; i < s_NumBoosterTypes; i++)
		{
			m_TechParameters.SetValueAtFixedIndex(s_NumActiveFansOfTypeNames[i], boosterActiveNumPerType[i]);
			m_TechParameters.SetValueAtFixedIndex(s_FanTypeFireRateNames[i], boosterTypeFireRate[i]);
		}
		m_TechParameters.SetValueAtFixedIndex("LMEngine", numLinearMotion);
		m_TechParameters.SetValueAtFixedIndex("LMEngineRate", linearMotionPower * 100f);
		for (int j = 0; j < s_WheelTypeNames.Length; j++)
		{
			m_TechParameters.SetValueAtFixedIndex(s_WheelTypeNames[j], wheelsGroundedPerType[j]);
		}
		foreach (KeyValuePair<int, float> item in corpPercentages)
		{
			string text = ((!Singleton.Manager<ManMods>.inst.IsModdedCorp((FactionSubTypes)item.Key)) ? s_CorpNames[item.Key] : Singleton.Manager<ManMods>.inst.FindCorpShortName((FactionSubTypes)item.Key));
			m_TechParameters.SetValueAtVariableIndex(text, item.Value);
		}
		if (m_DebugToConsole)
		{
			d.Log("FMOD TechAudio. " + m_TechParameters.DebugLogValues());
		}
		for (int k = 0; k < m_Events.Length; k++)
		{
			TechAudioEvent techAudioEvent7 = m_Events[k];
			if (techAudioEvent7.m_EventInstance.IsInited)
			{
				m_TechParameters.ApplyParameters(techAudioEvent7);
			}
		}
		m_TechParameters.CompleteFrame();
	}

	private void ActiveExit()
	{
		TechAudioEvent techAudioEvent = m_Events[0];
		TechAudioEvent techAudioEvent2 = m_Events[3];
		TechAudioEvent techAudioEvent3 = m_Events[4];
		TechAudioEvent techAudioEvent4 = m_Events[5];
		TechAudioEvent techAudioEvent5 = m_Events[6];
		TechAudioEvent obj = m_Events[7];
		techAudioEvent.SetActive(isActive: false);
		techAudioEvent2.SetActive(isActive: false);
		techAudioEvent3.SetActive(isActive: false);
		techAudioEvent4.SetActive(isActive: false);
		techAudioEvent5.SetActive(isActive: false);
		obj.SetActive(isActive: false);
		m_TechParameters.Reset();
	}

	private void UpdateLoopedADSR(FMODEvent audioEvent, AudioTickData data, FMODEvent.FMODParams additionalParam)
	{
		LoopingADSRState value = null;
		m_LoopingADSRHistory.TryGetValue(data.provider, out value);
		FMODEventInstance fMODEventInstance = value?.eventInstance ?? default(FMODEventInstance);
		bool flag = value?.wasNoteOn ?? false;
		bool flag2 = value?.wasInMainSequence ?? false;
		if (data.isNoteOn)
		{
			if (!flag)
			{
				bool flag3 = false;
				if (fMODEventInstance.IsInited)
				{
					flag3 = true;
					fMODEventInstance.StopAndRelease();
				}
				else
				{
					int value2 = 0;
					if (!m_LoopingADSRCountPerSFX.TryGetValue(data.sfxType, out value2) || value2 < m_MaxLoopedADSRVoicesPerSFX)
					{
						flag3 = true;
						value = new LoopingADSRState();
						m_LoopingADSRHistory.Remove(data.provider);
						m_LoopingADSRHistory.Add(data.provider, value);
						m_LoopingADSRCountPerSFX[data.sfxType] = value2 + 1;
					}
				}
				if (flag3)
				{
					fMODEventInstance = (value.eventInstance = audioEvent.PlayEventTrackedObject(data.block.tank.trans, data.block.tank.rbody));
				}
			}
			if (fMODEventInstance.IsInited && !flag2 && data.adsrTime01.Approximately(1f))
			{
				fMODEventInstance.triggerCue();
				flag2 = true;
			}
		}
		else if (fMODEventInstance.IsInited)
		{
			if (flag2 && data.adsrTime01 < 1f)
			{
				fMODEventInstance.triggerCue();
				flag2 = false;
			}
			if (data.adsrTime01.Approximately(0f))
			{
				fMODEventInstance.StopAndRelease(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				m_LoopingADSRHistory.Remove(data.provider);
				Dictionary<SFXType, int> loopingADSRCountPerSFX = m_LoopingADSRCountPerSFX;
				SFXType sfxType = data.sfxType;
				int value3 = loopingADSRCountPerSFX[sfxType] - 1;
				loopingADSRCountPerSFX[sfxType] = value3;
			}
		}
		if (fMODEventInstance.IsInited)
		{
			fMODEventInstance.setParameterValue("numShotsFired", data.numTriggered);
			fMODEventInstance.setParameterValue("powered", data.isNoteOn ? 1f : 0f);
			fMODEventInstance.setParameterValue("rate", data.adsrTime01);
			if (additionalParam != FMODEvent.FMODParams.empty)
			{
				fMODEventInstance.ApplyParam(additionalParam);
			}
			value.wasNoteOn = data.isNoteOn;
			value.wasInMainSequence = flag2;
		}
	}

	private void OnControlInput(TankControl.ControlState data)
	{
		m_Drive = data.InputMovement.magnitude;
		m_Turn = data.InputRotation.magnitude;
	}

	private void OnTankCollision(Tank.CollisionInfo collision, Tank.CollisionInfo.Event e)
	{
		if (e == Tank.CollisionInfo.Event.Enter)
		{
			Singleton.Manager<ManSFX>.inst.PlayTechImpactSFX(base.Tech, collision);
		}
	}

	private void OnTankAnchor(bool anchored, bool skyAnchor)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
		{
			Vector3 position = base.Tech.trans.position;
			if (!((position - Singleton.cameraTrans.position).sqrMagnitude > Globals.inst.AudioMaxDistance * Globals.inst.AudioMaxDistance))
			{
				SFXType sFXType = ((!skyAnchor) ? (anchored ? SFXType.Anchored : SFXType.UnAnchored) : (anchored ? SFXType.AnchoredSky : SFXType.UnAnchoredSky));
				TechAudioEventSimple techAudioEventSimple = m_SimpleEvents[(int)sFXType];
				PlayOneShotClamped(sFXType, techAudioEventSimple.m_Event, Globals.inst.m_TechAudioOneshotCooldown, position);
			}
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool isPlayerControlledTech)
	{
		if (tank == base.Tech)
		{
			m_CurrentOneshotCooldownModifier = (isPlayerControlledTech ? m_PlayerCooldownModifier : m_OtherTechCooldownModifier);
		}
	}

	private void OnModuleTickData(AudioTickData tickData, FMODEvent.FMODParams additionalParam)
	{
		int sFXTypeIndex = tickData.SFXTypeIndex;
		TechAudioEventSimple techAudioEventSimple = m_SimpleEvents[sFXTypeIndex];
		if (!techAudioEventSimple.m_Event.IsValid())
		{
			return;
		}
		switch (techAudioEventSimple.m_PlaybackType)
		{
		case SFXPlaybackType.Oneshot:
			if (tickData.numTriggered > 0)
			{
				PlayOneshot(tickData, additionalParam);
			}
			break;
		case SFXPlaybackType.LoopedADSR:
			UpdateLoopedADSR(techAudioEventSimple.m_Event, tickData, additionalParam);
			break;
		default:
			d.LogError("TechAudio.UpdateWeaponSFX - No case for SFXPlaybackType " + techAudioEventSimple.m_PlaybackType);
			break;
		}
	}

	private void ChangeStates(States newState)
	{
		if (m_State != newState)
		{
			States state = m_State;
			if (state != States.Idle && state == States.Active)
			{
				ActiveExit();
			}
			m_State = newState;
			if (m_State != States.Idle)
			{
				_ = 1;
			}
		}
	}

	private void PrePool()
	{
		s_BiomeTypeNames = EnumNamesIterator<BiomeTypes>.Names;
		s_WheelTypeNames = EnumNamesIterator<WheelTypes>.Names;
		s_CorpNames = EnumNamesIterator<FactionSubTypes>.Names;
		string[] names = EnumNamesIterator<BoosterEngineType>.Names;
		s_NumBoosterTypes = names.Length;
		s_NumActiveFansOfTypeNames = new string[s_NumBoosterTypes];
		s_FanTypeFireRateNames = new string[s_NumBoosterTypes];
		for (int i = 0; i < names.Length; i++)
		{
			s_NumActiveFansOfTypeNames[i] = $"{names[i]}";
			s_FanTypeFireRateNames[i] = $"{names[i]}Rate";
		}
		s_EventCount = EnumValuesIterator<AudioEventTypes>.Count;
		s_ParameterLists = new List<string>[s_EventCount];
		for (int j = 0; j < s_EventCount; j++)
		{
			s_ParameterLists[j] = new List<string>();
		}
		List<string> list = new List<string>(k_EngineParams);
		list.AddRange(s_CorpNames);
		List<string> list2 = new List<string>(k_WheelParams);
		list2.AddRange(s_WheelTypeNames);
		List<string> list3 = new List<string>(list2);
		list3.AddRange(s_BiomeTypeNames);
		List<string> list4 = new List<string>(k_WingParams);
		List<string> list5 = new List<string>(k_FanParams);
		list5.AddRange(s_NumActiveFansOfTypeNames);
		list5.AddRange(s_FanTypeFireRateNames);
		List<string> list6 = new List<string>(k_HoverParams);
		s_ParameterLists[0] = list;
		s_ParameterLists[1] = list;
		s_ParameterLists[2] = list;
		s_ParameterLists[3] = list2;
		s_ParameterLists[4] = list3;
		s_ParameterLists[5] = list4;
		s_ParameterLists[6] = list5;
		s_ParameterLists[7] = list6;
	}

	private void OnPool()
	{
		base.Tech.control.driveControlEvent.Subscribe(OnControlInput);
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
		if (m_DebugSoundEnabled.Length != s_EventCount)
		{
			m_DebugSoundEnabled = new bool[s_EventCount];
		}
		for (int i = 0; i < s_EventCount; i++)
		{
			m_DebugSoundEnabled[i] = true;
		}
		for (int j = 0; j < m_Events.Length; j++)
		{
			TechAudioEvent obj = m_Events[j];
			obj.m_Tech = base.Tech;
			obj.m_ParamNames = s_ParameterLists[j];
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		base.Tech.CollisionEvent.Subscribe(OnTankCollision);
		base.Tech.Anchors.AnchorEvent.Subscribe(OnTankAnchor);
		ChangeStates(States.Idle);
	}

	private void OnRecycle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < m_Events.Length; i++)
			{
				m_Events[i].SetActive(isActive: false);
			}
		}
		foreach (KeyValuePair<IModuleAudioProvider, LoopingADSRState> item in m_LoopingADSRHistory)
		{
			if (item.Value.eventInstance.IsInited)
			{
				item.Value.eventInstance.StopAndRelease();
			}
		}
		m_TechParameters.Reset();
		m_LoopingADSRHistory.Clear();
		m_LoopingADSRCountPerSFX.Clear();
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		base.Tech.CollisionEvent.Unsubscribe(OnTankCollision);
		base.Tech.Anchors.AnchorEvent.Unsubscribe(OnTankAnchor);
	}

	private void OnUpdate()
	{
		if (PriorityRank >= 0 && PriorityRank < Globals.inst.AudioMaxTechInstances)
		{
			ChangeStates(States.Active);
		}
		else
		{
			ChangeStates(States.Idle);
		}
		switch (m_State)
		{
		case States.Idle:
			IdleUpdate();
			break;
		case States.Active:
			ActiveUpdate();
			break;
		}
	}
}
