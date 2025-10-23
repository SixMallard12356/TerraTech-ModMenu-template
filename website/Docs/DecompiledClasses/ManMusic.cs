#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Networking;

public class ManMusic : Singleton.Manager<ManMusic>
{
	public enum MusicTypes
	{
		Attract,
		Main,
		Gauntlet,
		Sumo
	}

	public enum MiscDangerMusicType
	{
		None,
		Halloween
	}

	public enum Bookmark
	{
		Quiet,
		Danger
	}

	[Serializable]
	public class BookmarkData
	{
		public float m_DangerTargetValue;
	}

	private class ParamFader
	{
		public float m_CurrentVolume;

		public float m_TargetVolume;

		public ParamFader()
		{
			m_CurrentVolume = 0f;
			m_TargetVolume = 0f;
		}

		public void SetVolume(float volume, bool blendIn)
		{
			m_TargetVolume = Mathf.Clamp01(volume);
			if (!blendIn)
			{
				m_CurrentVolume = m_TargetVolume;
			}
		}
	}

	public struct DangerContext
	{
		public enum Circumstance
		{
			Generic,
			SetPiece,
			Enemy,
			Multiplayer
		}

		public Circumstance m_Circumstance;

		public FactionSubTypes m_Corporation;

		public float m_Timeout;

		public int m_BlockCount;

		public int m_VisibleID;

		public void Serialize(NetworkWriter writer)
		{
			writer.WritePackedInt32((int)m_Circumstance);
			writer.WritePackedInt32((int)m_Corporation);
			writer.Write(m_Timeout);
			writer.Write(m_BlockCount);
			writer.Write(m_VisibleID);
		}

		public void Deserialize(NetworkReader reader)
		{
			m_Circumstance = (Circumstance)reader.ReadPackedInt32();
			m_Corporation = (FactionSubTypes)reader.ReadPackedInt32();
			m_Timeout = reader.ReadSingle();
			m_BlockCount = reader.ReadInt32();
			m_VisibleID = reader.ReadInt32();
		}

		public void FillMessageDangerContextData(DangerMusicMessage.DangerContextData dangerContextData)
		{
			dangerContextData.m_BlockCount = m_BlockCount;
			dangerContextData.m_Circumstance = m_Circumstance;
			dangerContextData.m_Corporation = m_Corporation;
			dangerContextData.m_Timeout = m_Timeout;
			dangerContextData.m_VisibleID = m_VisibleID;
		}

		public void UpdateFromMessageDangerContextData(DangerMusicMessage.DangerContextData dangerContextData)
		{
			m_BlockCount = dangerContextData.m_BlockCount;
			m_Circumstance = dangerContextData.m_Circumstance;
			m_Corporation = dangerContextData.m_Corporation;
			m_Timeout = dangerContextData.m_Timeout;
			m_VisibleID = dangerContextData.m_VisibleID;
		}
	}

	public class DangerContextHistory
	{
		private List<DangerContext> m_List = new List<DangerContext>();

		public int Count => m_List.Count;

		public DangerContext this[int index]
		{
			get
			{
				if (index < 0 || index >= m_List.Count)
				{
					throw new ArgumentOutOfRangeException($"Index {index} is out of range of list {m_List.Count}");
				}
				return m_List[index];
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int IndexOfTech(int visibleID)
		{
			for (int i = 0; i < m_List.Count; i++)
			{
				if (m_List[i].m_VisibleID == visibleID)
				{
					return i;
				}
			}
			return -1;
		}

		public int IndexOfCircumstance(DangerContext.Circumstance circumstance)
		{
			for (int i = 0; i < m_List.Count; i++)
			{
				if (m_List[i].m_Circumstance == circumstance)
				{
					return i;
				}
			}
			return -1;
		}

		public void Record(DangerContext context)
		{
			if (context.m_VisibleID > 0)
			{
				int num = IndexOfTech(context.m_VisibleID);
				if (num >= 0)
				{
					d.AssertFormat(num >= 0 && num < m_List.Count, "Index {0} is out of range of list count {1} visilbeID {2}", num, m_List.Count, context.m_VisibleID);
					m_List[num] = context;
				}
				else
				{
					m_List.Add(context);
				}
			}
			else
			{
				int num2 = IndexOfCircumstance(context.m_Circumstance);
				if (num2 >= 0)
				{
					d.AssertFormat(num2 >= 0 && num2 < m_List.Count, "Index {0} is out of range of list count {1} visilbeID {2}", num2, m_List.Count, context.m_VisibleID);
					m_List[num2] = context;
				}
				else
				{
					m_List.Add(context);
				}
			}
		}

		public void RemoveAt(int index)
		{
			m_List.RemoveAt(index);
		}

		public void Clear()
		{
			m_List.Clear();
		}
	}

	[SerializeField]
	private float m_BlendTime = 2.5f;

	[SerializeField]
	private float m_BlendTimeToAmbience = 7f;

	[SerializeField]
	private float m_DangerMusicHoldTime = 1f;

	[SerializeField]
	private float m_DangerMusicMinTime;

	[SerializeField]
	private float m_MPDangerMusicCooldownTime;

	[SerializeField]
	[EnumArray(typeof(MiscDangerMusicType))]
	private string[] m_MiscDangerMusicParamNames;

	[SerializeField]
	private FMODEvent m_AmbientEvent;

	[SerializeField]
	private FMODEvent m_LoadingFMODSnapshot;

	[SerializeField]
	private FMODEvent m_PauseFMODSnapshot;

	[SerializeField]
	[EnumArray(typeof(MusicTypes))]
	private FMODEvent[] m_MusicEvents = new FMODEvent[1];

	[SerializeField]
	private string m_FMODTimeParamName = "Time";

	[SerializeField]
	private string m_FMODDangerParamName = "Danger";

	[SerializeField]
	private string m_FMODHeightParamName = "Height";

	[SerializeField]
	private string m_FMODExploreParamName = "Explore";

	[SerializeField]
	[EnumArray(typeof(Bookmark))]
	private List<BookmarkData> m_Bookmarks = new List<BookmarkData>(1);

	[SerializeField]
	private bool m_DbgGraphValues;

	private Bookmark m_CurrentMusic;

	private bool m_DangerMusicIsPlaying;

	private DangerContext.Circumstance m_LastCircumstance;

	private float m_ChangeMusicTimeout;

	private float m_DangerMusicCooldownTimeout;

	private float m_DangerMusicMinTimeout;

	private MiscDangerMusicType m_DangerMusicOverrideMisc;

	private Biome m_CurrentMainBiome;

	private int m_NumBiomeAudioTypes;

	private MusicTypes m_CurrentMusicType;

	private FMODEventInstance m_MusicInstance;

	private FMODEventInstance m_AmbienceInstance;

	private FMODEventInstance m_LoadingSnapshotInstance;

	private FMODEventInstance m_PauseSnapshotInstance;

	private ParamFader m_DangerFader;

	private List<ParamFader> m_ParamFaders;

	private VCA m_MasterVCA;

	private VCA m_MusicVCA;

	private VCA m_SfxVCA;

	private static string k_MasterPath = "vca:/Master Volume";

	private static string k_MusicPath = "vca:/Music";

	private static string k_SfxPath = "vca:/SFX";

	private float[] m_BiomeAudioWeights;

	private int[] m_BiomeCount;

	private string[] m_BiomeTypeNames;

	private string m_RaDBiomeName;

	private float m_RaDAudioWeight;

	private int m_NumCorpTypes;

	private int[] m_CorpBlockCount;

	private float[] m_CorpWeights;

	private string[] m_CorpTypeNames;

	private Color[] m_DgbCorpColours = new Color[8]
	{
		Color.magenta,
		Color.grey,
		Color.yellow,
		Color.white,
		Color.blue,
		Color.black,
		Color.green,
		Color.red
	};

	private DangerContextHistory m_DangerHistory = new DangerContextHistory();

	public bool EnableSequencing { get; set; }

	public void SetMasterMixerVolume(float value)
	{
		SetVCAFaderLevel(m_MasterVCA, value);
	}

	public float GetMasterMixerVolume()
	{
		return GetVCAFaderLevel(m_MasterVCA);
	}

	public void SetMusicMixerVolume(float value)
	{
		SetVCAFaderLevel(m_MusicVCA, value);
	}

	public float GetMusicMixerVolume()
	{
		return GetVCAFaderLevel(m_MusicVCA);
	}

	public void SetSFXMixerVolume(float value)
	{
		SetVCAFaderLevel(m_SfxVCA, value);
	}

	public float GetSFXMixerVolume()
	{
		return GetVCAFaderLevel(m_SfxVCA);
	}

	public void SetPaused(bool pause)
	{
		if (pause)
		{
			if (!m_PauseSnapshotInstance.IsInited)
			{
				m_PauseSnapshotInstance = m_PauseFMODSnapshot.PlayEvent();
			}
			else
			{
				m_PauseSnapshotInstance.start();
			}
			if (m_MusicInstance.IsInited)
			{
				m_MusicInstance.setPaused(paused: true);
			}
		}
		else
		{
			m_PauseSnapshotInstance.stop();
			if (m_MusicInstance.IsInited)
			{
				m_MusicInstance.setPaused(paused: false);
			}
		}
	}

	public void SetDangerMusicOverride(MiscDangerMusicType overrideType)
	{
		m_DangerMusicOverrideMisc = overrideType;
	}

	public MiscDangerMusicType GetDangerMusicOverride()
	{
		return m_DangerMusicOverrideMisc;
	}

	private void UpdateSequencing()
	{
		if (!EnableSequencing)
		{
			return;
		}
		float value = (float)Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay / 24f;
		float value2 = 0f;
		if ((bool)Singleton.playerTank)
		{
			value2 = Singleton.cameraTrans.position.y - Singleton.playerPos.y;
		}
		for (int i = 0; i < m_NumBiomeAudioTypes; i++)
		{
			m_BiomeAudioWeights[i] = 0f;
			m_BiomeCount[i] = 0;
		}
		m_RaDAudioWeight = 0f;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD)
		{
			m_RaDAudioWeight = 1f;
		}
		else if (Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Valid)
		{
			ManWorld.CachedBiomeBlendWeights currentBiomeWeights = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights;
			for (int j = 0; j < currentBiomeWeights.NumWeights; j++)
			{
				Biome biome = currentBiomeWeights.Biome(j);
				if ((bool)biome)
				{
					int biomeType = (int)biome.BiomeType;
					m_BiomeAudioWeights[biomeType] += currentBiomeWeights.Weight(j);
					m_BiomeCount[biomeType]++;
				}
			}
		}
		for (int k = 0; k < m_NumBiomeAudioTypes; k++)
		{
			if (m_BiomeCount[k] > 1)
			{
				m_BiomeAudioWeights[k] /= m_BiomeCount[k];
			}
			m_AmbienceInstance.setParameterValue(m_BiomeTypeNames[k], m_BiomeAudioWeights[k]);
			m_MusicInstance.setParameterValue(m_BiomeTypeNames[k], m_BiomeAudioWeights[k]);
		}
		m_AmbienceInstance.setParameterValue(m_RaDBiomeName, m_RaDAudioWeight);
		m_MusicInstance.setParameterValue(m_RaDBiomeName, m_RaDAudioWeight);
		int num = 0;
		for (int l = 0; l < m_NumCorpTypes; l++)
		{
			m_CorpBlockCount[l] = 0;
		}
		bool flag = false;
		FactionSubTypes factionSubTypes = FactionSubTypes.NULL;
		for (int num2 = m_DangerHistory.Count - 1; num2 >= 0; num2--)
		{
			DangerContext dangerContext = m_DangerHistory[num2];
			if (dangerContext.m_Timeout > Time.time)
			{
				if (dangerContext.m_Circumstance == DangerContext.Circumstance.SetPiece)
				{
					flag = true;
					factionSubTypes = dangerContext.m_Corporation;
				}
				else
				{
					num += dangerContext.m_BlockCount;
					m_CorpBlockCount[(int)dangerContext.m_Corporation] += dangerContext.m_BlockCount;
				}
			}
			else
			{
				m_DangerHistory.RemoveAt(num2);
			}
		}
		EnumValuesIterator<MiscDangerMusicType> enumerator = EnumIterator<MiscDangerMusicType>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			MiscDangerMusicType current = enumerator.Current;
			if (current != MiscDangerMusicType.None)
			{
				if (current >= MiscDangerMusicType.Halloween && (int)current < m_MiscDangerMusicParamNames.Length && !m_MiscDangerMusicParamNames[(int)current].NullOrEmpty())
				{
					m_MusicInstance.setParameterValue(m_MiscDangerMusicParamNames[(int)current], (current == m_DangerMusicOverrideMisc) ? 1 : 0);
				}
				else
				{
					d.LogError("Trying to set danger music override without a FMOD parameter name being set in ManMusic");
				}
			}
		}
		for (int m = 0; m < m_NumCorpTypes; m++)
		{
			if (flag)
			{
				m_CorpWeights[m] = ((factionSubTypes == (FactionSubTypes)m) ? 1 : 0);
			}
			else if (num != 0)
			{
				m_CorpWeights[m] = (float)m_CorpBlockCount[m] / (float)num;
			}
			else
			{
				m_CorpWeights[m] = 0f;
			}
			m_MusicInstance.setParameterValue(m_CorpTypeNames[m], m_CorpWeights[m]);
		}
		float value3 = (IsDangerous() ? 1f : 0f);
		bool flag2 = Singleton.Manager<ManGameMode>.inst.IsSwitchingMode || Singleton.Manager<ManUI>.inst.IsFadingDown();
		if (m_DangerMusicIsPlaying && (!IsDangerous() || flag2) && m_DangerMusicMinTimeout < Time.time)
		{
			if (!flag2)
			{
				ChangeCurrentMusic(Bookmark.Quiet);
			}
			else
			{
				FadeDownAll();
			}
			m_DangerMusicIsPlaying = false;
			m_DangerMusicCooldownTimeout = Time.time + GetDangerMusicCooldownTime();
		}
		if (!m_DangerMusicIsPlaying && IsDangerous() && !flag2 && m_DangerMusicCooldownTimeout < Time.time)
		{
			ChangeCurrentMusic(Bookmark.Danger);
			m_DangerMusicIsPlaying = true;
			m_DangerMusicMinTimeout = Time.time + GetDangerMusicMinTime();
		}
		m_AmbienceInstance.setParameterValue(m_FMODTimeParamName, value);
		m_AmbienceInstance.setParameterValue(m_FMODDangerParamName, value3);
		m_AmbienceInstance.setParameterValue(m_FMODHeightParamName, value2);
		m_MusicInstance.setParameterValue(m_FMODDangerParamName, m_DangerFader.m_CurrentVolume);
	}

	private bool IsDangerous()
	{
		for (int i = 0; i < m_DangerHistory.Count; i++)
		{
			if (m_DangerHistory[i].m_Timeout > Time.time)
			{
				return true;
			}
		}
		return false;
	}

	private void ChangeCurrentMusic(Bookmark bookmark, bool crossFade = true)
	{
		m_CurrentMusic = bookmark;
		BookmarkData bookmarkData = m_Bookmarks[(int)m_CurrentMusic];
		m_DangerFader.SetVolume(bookmarkData.m_DangerTargetValue, blendIn: false);
	}

	private void UpdateFading()
	{
		float num = Time.deltaTime / m_BlendTime;
		if (m_CurrentMusic == Bookmark.Quiet && !Singleton.Manager<ManGameMode>.inst.IsSwitchingMode && !Singleton.Manager<ManUI>.inst.IsFadingDown())
		{
			num = Time.deltaTime / m_BlendTimeToAmbience;
		}
		for (int i = 0; i < m_ParamFaders.Count; i++)
		{
			float num2 = Mathf.Clamp(m_ParamFaders[i].m_TargetVolume - m_ParamFaders[i].m_CurrentVolume, 0f - num, num);
			m_ParamFaders[i].m_CurrentVolume = Mathf.Clamp01(m_ParamFaders[i].m_CurrentVolume + num2);
		}
	}

	public void SetDanger(DangerContext.Circumstance circumstance)
	{
		d.AssertFormat(m_CurrentMusicType != MusicTypes.Main, "ManMusic.SetDanger - The main event needs to specify corp types. Overloaded SetDanger methods instead");
		DangerContext context = new DangerContext
		{
			m_Circumstance = circumstance,
			m_Timeout = Time.time + GetDangerMusicHoldtime()
		};
		SetDanger(context, null);
	}

	public void SetDanger(DangerContext.Circumstance circumstance, FactionSubTypes corp)
	{
		d.Assert(circumstance != DangerContext.Circumstance.Enemy, "ManMusic.SetDanger - when circumstance is 'Enemy' you must specify a tech");
		DangerContext context = new DangerContext
		{
			m_Circumstance = circumstance,
			m_Timeout = Time.time + GetDangerMusicHoldtime(),
			m_Corporation = corp
		};
		SetDanger(context, null);
	}

	public void SetDanger(DangerContext.Circumstance circumstance, Tank enemyTech, Tank friendlyTech)
	{
		d.Assert(enemyTech != null, "ManMusic.SetDanger - tech must have a valid reference");
		DangerContext context = new DangerContext
		{
			m_Circumstance = circumstance,
			m_Timeout = Time.time + GetDangerMusicHoldtime(),
			m_Corporation = enemyTech.GetMainCorp(),
			m_BlockCount = enemyTech.blockman.blockCount,
			m_VisibleID = enemyTech.visible.ID
		};
		SetDanger(context, friendlyTech);
	}

	public void SetDangerClient(DangerContext.Circumstance circumstance, Tank enemyTech, Tank friendlyTech)
	{
		if (friendlyTech != null && Singleton.Manager<ManNetwork>.inst.IsServer && friendlyTech != Singleton.playerTank && friendlyTech.netTech != null && friendlyTech.netTech.NetPlayer != null)
		{
			int connectionId = friendlyTech.netTech.NetPlayer.connectionToClient.connectionId;
			DangerMusicMessage dangerMusicMessage = new DangerMusicMessage();
			dangerMusicMessage.context = new DangerMusicMessage.DangerContextData
			{
				m_Circumstance = circumstance,
				m_Timeout = GetDangerMusicHoldtime(),
				m_Corporation = enemyTech.GetMainCorp(),
				m_BlockCount = enemyTech.blockman.blockCount,
				m_VisibleID = enemyTech.visible.ID
			};
			Singleton.Manager<ManNetwork>.inst.SendToClient(connectionId, TTMsgType.DangerMusicMessage, dangerMusicMessage);
		}
	}

	private void SetDanger(DangerContext context, Tank friendlyTech)
	{
		m_DangerHistory.Record(context);
	}

	public void ResetDangerMusic()
	{
		m_DangerHistory.Clear();
	}

	public void FadeDownAll()
	{
		ChangeCurrentMusic(Bookmark.Quiet);
		m_DangerMusicIsPlaying = false;
	}

	public IEnumerator StartLoadingScreenMusic()
	{
		PlayMusicEvent(MusicTypes.Attract);
		yield break;
	}

	private void Init()
	{
		m_DangerFader = new ParamFader();
		m_ParamFaders = new List<ParamFader>();
		m_ParamFaders.Add(m_DangerFader);
		if (m_AmbientEvent.IsValid())
		{
			m_AmbienceInstance = m_AmbientEvent.PlayEventTrackedObject(Singleton.cameraTrans, null);
		}
		m_BiomeTypeNames = EnumNamesIterator<BiomeTypes>.Names;
		m_NumBiomeAudioTypes = m_BiomeTypeNames.Length;
		m_BiomeAudioWeights = new float[m_NumBiomeAudioTypes];
		m_BiomeCount = new int[m_NumBiomeAudioTypes];
		m_RaDBiomeName = ManGameMode.GameType.RaD.ToString();
		m_CorpTypeNames = EnumNamesIterator<FactionSubTypes>.Names;
		m_NumCorpTypes = m_CorpTypeNames.Length;
		m_CorpBlockCount = new int[m_NumCorpTypes];
		m_CorpWeights = new float[m_NumCorpTypes];
		m_MasterVCA = RuntimeManager.GetVCA(k_MasterPath);
		m_MusicVCA = RuntimeManager.GetVCA(k_MusicPath);
		m_SfxVCA = RuntimeManager.GetVCA(k_SfxPath);
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeFinished);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.OverrideDangerMusicRequest, OnOverrideDangerMusicRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.DangerMusicMessage, OnDangerMusicMessage);
	}

	private void OnModeStart(Mode mode)
	{
		switch (mode.GetGameType())
		{
		case ManGameMode.GameType.Attract:
			PlayMusicEvent(MusicTypes.Attract);
			break;
		case ManGameMode.GameType.MainGame:
		case ManGameMode.GameType.RaD:
		case ManGameMode.GameType.Misc:
		case ManGameMode.GameType.Defense:
		case ManGameMode.GameType.Deathmatch:
		case ManGameMode.GameType.Creative:
		case ManGameMode.GameType.CoOpCreative:
		case ManGameMode.GameType.CoOpCampaign:
			PlayMusicEvent(MusicTypes.Main);
			break;
		case ManGameMode.GameType.RacingChallenge:
		case ManGameMode.GameType.FlyingChallenge:
		case ManGameMode.GameType.Gauntlet:
			PlayMusicEvent(MusicTypes.Gauntlet);
			break;
		case ManGameMode.GameType.SumoShowdown:
			PlayMusicEvent(MusicTypes.Sumo);
			break;
		default:
			d.LogErrorFormat("ManMusic.OnModeStart - No handler for ManGameMode.GameType {0} found", mode.GetGameType());
			break;
		}
		if (m_LoadingSnapshotInstance.IsInited)
		{
			m_LoadingSnapshotInstance.stop();
		}
	}

	private void OnModeFinished(Mode mode)
	{
		if (m_MusicInstance.IsInited)
		{
			m_MusicInstance.StopAndRelease(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
		m_DangerMusicOverrideMisc = MiscDangerMusicType.None;
		if (!m_LoadingSnapshotInstance.IsInited)
		{
			m_LoadingSnapshotInstance = m_LoadingFMODSnapshot.PlayEvent();
		}
		else
		{
			m_LoadingSnapshotInstance.start();
		}
	}

	private void OnOverrideDangerMusicRequest(NetworkMessage netMsg)
	{
		OverrideDangerMusicRequest overrideDangerMusicRequest = netMsg.ReadMessage<OverrideDangerMusicRequest>();
		SetDangerMusicOverride(overrideDangerMusicRequest.m_DangerMusicType);
	}

	private void OnDangerMusicMessage(NetworkMessage netMsg)
	{
		DangerMusicMessage dangerMusicMessage = netMsg.ReadMessage<DangerMusicMessage>();
		DangerContext context = default(DangerContext);
		context.UpdateFromMessageDangerContextData(dangerMusicMessage.context);
		context.m_Timeout += Time.time;
		m_DangerHistory.Record(context);
	}

	private void PlayMusicEvent(MusicTypes musicType)
	{
		if (m_MusicInstance.IsInited)
		{
			if (m_CurrentMusicType == musicType)
			{
				return;
			}
			m_MusicInstance.StopAndRelease();
		}
		FMODEvent fMODEvent = m_MusicEvents[(int)musicType];
		m_CurrentMusicType = musicType;
		if (fMODEvent.IsValid())
		{
			m_MusicInstance = fMODEvent.PlayEventTrackedObject(Singleton.cameraTrans, null);
		}
		else
		{
			d.LogErrorFormat("ManMusic.PlayMusicEvent - invalid event for music type {0}", musicType);
		}
		ResetDangerMusic();
		m_DangerMusicIsPlaying = false;
		m_DangerMusicMinTimeout = 0f;
		m_DangerMusicCooldownTimeout = 0f;
		ChangeCurrentMusic(Bookmark.Quiet);
	}

	private void SetVCAFaderLevel(VCA vca, float volume)
	{
		if (vca.isValid())
		{
			vca.setVolume(volume);
		}
		else
		{
			d.LogError("ManMusic.SetVCAFaderLevel - VCA is null");
		}
	}

	private float GetVCAFaderLevel(VCA vca)
	{
		float volume = 0f;
		float finalvolume = 0f;
		if (vca.isValid())
		{
			vca.getVolume(out volume, out finalvolume);
		}
		else
		{
			d.LogError("ManMusic.GetVCAFaderLevel - VCA is null");
		}
		return volume;
	}

	private float GetDangerMusicHoldtime()
	{
		return m_DangerMusicHoldTime;
	}

	private float GetDangerMusicMinTime()
	{
		return m_DangerMusicMinTime;
	}

	private float GetDangerMusicCooldownTime()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
		{
			return m_MPDangerMusicCooldownTime;
		}
		return 0f;
	}

	private void Start()
	{
		EnableSequencing = true;
		Singleton.DoOnceAfterStart(Init);
	}

	private void Update()
	{
		UpdateSequencing();
		UpdateFading();
	}
}
