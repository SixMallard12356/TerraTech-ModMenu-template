#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ManPop : Singleton.Manager<ManPop>, Mode.IManagerModeEvents
{
	public enum CreativeModePopulationDifficulty
	{
		Disabled,
		Easy,
		Medium,
		Hard
	}

	[Serializable]
	public struct CreativeModePopDifficultySetting
	{
		public string popTypeName;

		public LocalisedString displayName;
	}

	[Serializable]
	private struct PopTypeRuntime
	{
		public PopType m_Type;

		public List<PresetInfo> m_CachedPresets;

		public bool m_Enabled;
	}

	[Serializable]
	public class FavouredCorpPerBiome
	{
		public Biome m_Biome;

		public TechSpawnFilter.CorpPercentage m_FavouredCorp;

		public TechSpawnFilter.CorpPercentage m_FavouredCorpFallback;
	}

	[Serializable]
	public struct PopType
	{
		[SerializeField]
		public string m_Name;

		[SerializeField]
		public TechSpawnFilter m_Filter;

		[Range(0f, 1f)]
		[SerializeField]
		public float m_Likelyhood;

		[SerializeField]
		public bool m_UseAI;

		[SerializeField]
		public AITreeType m_AITreeToUse;
	}

	[Serializable]
	public struct WaveData
	{
		public TankPreset[] m_TankChoices;

		public float m_SpawnRadiusMin;

		public float m_SpawnRadiusMax;

		public float m_PatrolRangeMin;

		public float m_PatrolRangeMax;

		public float m_PlayerHealPercentage;

		public float m_BlockDetachHealPercentage;
	}

	private struct SpawnRec
	{
		public TechData preset;

		public int popTypeIndex;

		public float weight;
	}

	public enum WaveState
	{
		NotDoingWaves,
		WaveStart,
		WaveInProgress
	}

	private class SaveData
	{
		public bool m_Active;

		public float m_PauseTimer;

		public WaveState m_WaveState = WaveState.WaveStart;

		public int m_WaveInd;

		public List<int> m_SpawnedIds = new List<int>();

		public float m_TimeBeforeSpawn;

		public float m_TimeOutProgress;

		public bool m_TimeOutInZone;

		public V3Serial m_TimeOutCentre = Vector3.zero;

		public WorldPosition m_TimeOutCentreWorld;

		public int m_EnemiesKilled;

		public List<string> m_HistoryList = new List<string>();

		public List<string> m_EnabledPopTypes = new List<string>();

		public CreativeModePopulationDifficulty m_CurrentCreativePopulationDifficulty;
	}

	[Header("Types")]
	[SerializeField]
	private WaveData[] m_WaveData;

	[SerializeField]
	private PopType[] m_PopulationTypes;

	[SerializeField]
	[EnumArray(typeof(CreativeModePopulationDifficulty))]
	private CreativeModePopDifficultySetting[] m_CreativePopulationDifficultyTypeNames;

	[Header("Filters")]
	[SerializeField]
	private TechSpawnFilter m_DayFilter;

	[SerializeField]
	private TechSpawnFilter m_NightFilter;

	[SerializeField]
	private TechSpawnFilter m_NoVendorFilter;

	[SerializeField]
	private TechSpawnFilter m_NoExpFilter;

	[Header("Weighting")]
	[SerializeField]
	private FavouredCorpPerBiome[] m_CorpPerBiome;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_BiomeWeighting;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_LikelyhoodWeighting;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_HistoryWeighting;

	[Header("Spawning")]
	[SerializeField]
	private float m_SpawnRadiusMin;

	[SerializeField]
	private float m_SpawnRadiusMax;

	[SerializeField]
	[Tooltip("This maps angle to likelyhood")]
	private AnimationCurve m_SpawnAngularDistribution;

	[Header("Timeout Area")]
	[SerializeField]
	[Tooltip("Initial size of area which player has to leave for action to continue after an encounter")]
	private float m_TimeOutAreaRadiusDefaultMax = 3f;

	[SerializeField]
	[Tooltip("Eventual size of area which player has to leave for action to continue after an encounter")]
	public float m_TimeOutAreaRadiusDefaultMin = 10f;

	[SerializeField]
	[Tooltip("Duration of reduction of area which player has to leave for action to continue after an encounter")]
	public float m_TimeOutAreaShrinkTime = 60f;

	[Header("Population control")]
	[SerializeField]
	private float m_DestroyRadiusThreshold;

	[SerializeField]
	private int m_PopulationLimit;

	[SerializeField]
	private float m_MaxPausedTime = 30f;

	[SerializeField]
	private float m_PauseCooloffTime = 5f;

	[SerializeField]
	public float m_MinPeriodBetweenSpawns;

	[Header("Related to block detach")]
	[SerializeField]
	private int m_SaveBlocksFromEnemiesDestroyed = 5;

	public static bool s_DebugShowInfo;

	[SerializeField]
	[HideInInspector]
	private PopTypeRuntime[] m_PopTypeRuntimes;

	private const int kAngleNumQuadrants = 32;

	private const float kAngleQuadrantSize = 11.25f;

	private float[] m_AngleWeights;

	private float m_TotalAngleWeight;

	private SaveData m_SaveData;

	private ObjectSpawner m_Spawner = new ObjectSpawner();

	private List<SpawnRec> m_PotentialSpawnAggregator = new List<SpawnRec>(1024);

	private const int kHistoryLength = 20;

	private Dictionary<string, int> m_HistoryLookup;

	private List<TrackedVisible> m_SpawnedTechs;

	private int m_SetupPopTypeFiltersIndex;

	private const int k_SetupPopTypePassesPerUpdate = 1;

	private const int kTimeToProcessPerUpdateMS = 80;

	private Stopwatch m_ProcessCostTimer = new Stopwatch();

	private OnGUICallback m_GuiCallback;

	private TechSpawnFilter[] s_FilterArray = new TechSpawnFilter[1];

	private static TechSpawnFilter.CorpPercentage[] s_CorpPercentageArray = new TechSpawnFilter.CorpPercentage[1];

	public bool SaveEnemyDisconnectingBlocks
	{
		get
		{
			if (m_SaveData != null)
			{
				return m_SaveData.m_EnemiesKilled <= m_SaveBlocksFromEnemiesDestroyed;
			}
			return true;
		}
	}

	public bool IsSpawningEnabled
	{
		get
		{
			if (m_SaveData != null)
			{
				return m_SaveData.m_Active;
			}
			return false;
		}
	}

	public CreativeModePopulationDifficulty CreativePopulationDifficulty
	{
		get
		{
			if (m_SaveData == null)
			{
				return CreativeModePopulationDifficulty.Disabled;
			}
			return m_SaveData.m_CurrentCreativePopulationDifficulty;
		}
	}

	public bool IsSettingUp { get; private set; }

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Clear();
		optionalLoadState?.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManPop, out m_SaveData);
		if (m_SaveData == null)
		{
			m_SaveData = new SaveData();
		}
		m_SetupPopTypeFiltersIndex = 0;
		IsSettingUp = true;
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTechChanged);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Subscribe(OnTechTeamChange);
		if (m_SaveData.m_CurrentCreativePopulationDifficulty != CreativeModePopulationDifficulty.Disabled)
		{
			SetCreativePopulationDifficulty(m_SaveData.m_CurrentCreativePopulationDifficulty);
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		m_SaveData.m_SpawnedIds.Clear();
		for (int i = 0; i < m_SpawnedTechs.Count; i++)
		{
			if (!m_SpawnedTechs[i].wasDestroyed)
			{
				m_SaveData.m_SpawnedIds.Add(m_SpawnedTechs[i].ID);
			}
		}
		m_SaveData.m_EnabledPopTypes.Clear();
		for (int j = 0; j < m_PopTypeRuntimes.Length; j++)
		{
			if (m_PopTypeRuntimes[j].m_Enabled)
			{
				m_SaveData.m_EnabledPopTypes.Add(m_PopTypeRuntimes[j].m_Type.m_Name);
			}
		}
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManPop, m_SaveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTechChanged);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Unsubscribe(OnTechTeamChange);
		m_SetupPopTypeFiltersIndex = 0;
		IsSettingUp = false;
		Clear();
	}

	public void SetSpawningEnabled(bool enableSpawning)
	{
		if (enableSpawning != m_SaveData.m_Active)
		{
			m_SaveData.m_Active = enableSpawning;
			if (enableSpawning && m_SaveData.m_WaveState != WaveState.NotDoingWaves)
			{
				SetTimeoutAreaToCurrentPosition();
			}
		}
	}

	public void RemoveSpawnedPopulation()
	{
		for (int num = m_SpawnedTechs.Count - 1; num >= 0; num--)
		{
			TrackedVisible trackedVis = m_SpawnedTechs[num];
			Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(trackedVis);
			m_SpawnedTechs.RemoveAt(num);
		}
		m_Spawner.Cancel();
	}

	public void SkipWaveSpawns()
	{
		m_SaveData.m_WaveInd = -1;
		m_SaveData.m_WaveState = WaveState.NotDoingWaves;
	}

	public void SetPaused(bool paused)
	{
		if (m_SaveData != null)
		{
			m_SaveData.m_PauseTimer = (paused ? m_MaxPausedTime : Mathf.Min(m_SaveData.m_PauseTimer, m_PauseCooloffTime));
		}
	}

	public bool IsPaused()
	{
		if (m_SaveData == null)
		{
			return false;
		}
		return m_SaveData.m_PauseTimer > 0f;
	}

	public void SetPopTypeEnabled(string popTypeName, bool enabled)
	{
		bool flag = false;
		for (int i = 0; i < m_PopTypeRuntimes.Length; i++)
		{
			if (m_PopTypeRuntimes[i].m_Type.m_Name == popTypeName)
			{
				m_PopTypeRuntimes[i].m_Enabled = enabled;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			d.LogError("ERROR: ManPop.SetPopTypeEnabled unable to find group called " + popTypeName);
		}
	}

	public void SetCreativePopulationDifficulty(CreativeModePopulationDifficulty difficultySetting)
	{
		if (m_SaveData == null)
		{
			d.LogError($"Trying to set creative population difficulty to {difficultySetting} before mode start - will be ignored");
		}
		else if (difficultySetting != m_SaveData.m_CurrentCreativePopulationDifficulty)
		{
			string popTypeName = m_CreativePopulationDifficultyTypeNames[(int)m_SaveData.m_CurrentCreativePopulationDifficulty].popTypeName;
			if (!popTypeName.NullOrEmpty())
			{
				SetPopTypeEnabled(popTypeName, enabled: false);
			}
			m_SaveData.m_CurrentCreativePopulationDifficulty = difficultySetting;
			string popTypeName2 = m_CreativePopulationDifficultyTypeNames[(int)m_SaveData.m_CurrentCreativePopulationDifficulty].popTypeName;
			if (!popTypeName2.NullOrEmpty())
			{
				SetPopTypeEnabled(popTypeName2, enabled: true);
			}
			RemoveSpawnedPopulation();
			bool flag = m_SaveData.m_CurrentCreativePopulationDifficulty != CreativeModePopulationDifficulty.Disabled;
			SetSpawningEnabled(flag);
			if (flag)
			{
				SkipWaveSpawns();
			}
		}
	}

	public void GetCreativePopDifficultyNames(ref List<string> outNamesList)
	{
		if (outNamesList == null)
		{
			outNamesList = new List<string>(m_CreativePopulationDifficultyTypeNames.Length);
		}
		d.Assert(outNamesList.Count == 0, "GetCreativePopDifficultyNames - Was passed non-empty list!");
		outNamesList.Clear();
		for (int i = 0; i < m_CreativePopulationDifficultyTypeNames.Length; i++)
		{
			outNamesList.Add(m_CreativePopulationDifficultyTypeNames[i].displayName.Value);
		}
	}

	public bool CheckTechDataContainsPrototypeParts(TechData techData)
	{
		for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec blockSpec = techData.m_BlockSpecs[i];
			if (Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockSpec.GetBlockType()))
			{
				return true;
			}
		}
		return false;
	}

	public void DebugForceSpawn()
	{
		bool debugForceFromPopulation = true;
		TryToSpawn(debugForceFromPopulation);
	}

	public bool IsTankPartOfWave(Tank tank)
	{
		bool result = false;
		if ((bool)tank && m_SaveData != null && m_SaveData.m_WaveState != WaveState.NotDoingWaves)
		{
			for (int i = 0; i < m_SpawnedTechs.Count; i++)
			{
				if (m_SpawnedTechs[i].ID == tank.visible.ID)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public TechData ChoosePopTechToSpawn(TechSpawnFilter spawnFilter, out AITreeType AITreeToUse)
	{
		TechSpawnFilter[] filterChain = new TechSpawnFilter[2] { null, spawnFilter };
		DeterminePotentialSpawnsImpl(filterChain, hasFavouriteCorp: false, default(TechSpawnFilter.CorpPercentage), enableAll: true, m_PotentialSpawnAggregator, out var totalWeight);
		TechData result = ChoosePopTechToSpawnImpl(m_PotentialSpawnAggregator, totalWeight, out AITreeToUse);
		m_PotentialSpawnAggregator.Clear();
		return result;
	}

	private void Clear()
	{
		m_SaveData = null;
		m_HistoryLookup.Clear();
		m_SpawnedTechs.Clear();
		m_Spawner.Cancel();
		for (int i = 0; i < m_PopTypeRuntimes.Length; i++)
		{
			m_PopTypeRuntimes[i].m_Enabled = false;
		}
	}

	private bool SetupPopTypeFilters()
	{
		m_ProcessCostTimer.Restart();
		while (m_SetupPopTypeFiltersIndex < m_PopTypeRuntimes.Length && m_ProcessCostTimer.ElapsedMilliseconds < 80)
		{
			TechSpawnFilter filter = m_PopTypeRuntimes[m_SetupPopTypeFiltersIndex].m_Type.m_Filter;
			if (filter != null)
			{
				bool allowFallbacks = false;
				s_FilterArray[0] = filter;
				m_PopTypeRuntimes[m_SetupPopTypeFiltersIndex].m_CachedPresets = Singleton.Manager<ManPresetFilter>.inst.FilterPresets(s_FilterArray, allowFallbacks, TechSpawnFilter.CheckType.StaticOnly);
				if (m_PopTypeRuntimes[m_SetupPopTypeFiltersIndex].m_CachedPresets == null || m_PopTypeRuntimes[m_SetupPopTypeFiltersIndex].m_CachedPresets.Count == 0)
				{
					d.LogWarning("WARNING: ManPop determined that filter on pop type " + m_PopTypeRuntimes[m_SetupPopTypeFiltersIndex].m_Type.m_Name + " called " + filter.name + " will never return any valid presets!");
				}
			}
			m_SetupPopTypeFiltersIndex++;
		}
		m_ProcessCostTimer.Stop();
		return m_SetupPopTypeFiltersIndex < m_PopTypeRuntimes.Length;
	}

	private void TryToSpawn(bool debugForceFromPopulation = false)
	{
		if ((bool)Singleton.playerTank && !m_Spawner.IsBusy)
		{
			AITreeType AITreeToUse = null;
			TechData techData;
			float spawnRadiusMin;
			float spawnRadiusMax;
			if (m_SaveData.m_WaveState == WaveState.NotDoingWaves || debugForceFromPopulation)
			{
				techData = ChoosePopTechToSpawn(out AITreeToUse);
				spawnRadiusMin = m_SpawnRadiusMin;
				spawnRadiusMax = m_SpawnRadiusMax;
			}
			else
			{
				techData = ChooseWaveTechToSpawn(out spawnRadiusMin, out spawnRadiusMax);
			}
			if (techData != null)
			{
				float num = UnityEngine.Random.Range(spawnRadiusMin, spawnRadiusMax);
				float y = GenerateRandomSpawnAngleDegrees();
				Transform trans = Singleton.playerTank.trans;
				Vector3 vector = Quaternion.Euler(0f, y, 0f) * Maths.VecToXZUnitVec(trans.forward);
				Vector3 scenePos = trans.position + vector * num;
				ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
				{
					m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
					m_CircleRadius = techData.Radius,
					m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
					m_CircleIndex = 0,
					m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
					m_CheckSafeArea = !debugForceFromPopulation,
					m_RejectFunc = null
				};
				ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
				{
					m_TechToSpawn = techData,
					m_AIType = AITreeToUse,
					m_Team = -1,
					m_Rotation = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f),
					m_Grounded = true,
					m_SpawnVisualType = ManSpawn.SpawnVisualType.Bomb,
					m_IsPopulation = true
				};
				bool autoRetry = false;
				m_Spawner.TrySpawn(objectSpawnParams, freeSpaceParams, null, "PopSpawn", autoRetry);
			}
			else
			{
				m_SaveData.m_TimeBeforeSpawn = 2f;
			}
		}
	}

	private TechData ChoosePopTechToSpawn(out AITreeType AITreeToUse)
	{
		DeterminePotentialSpawns(m_PotentialSpawnAggregator, out var totalWeight);
		TechData result = ChoosePopTechToSpawnImpl(m_PotentialSpawnAggregator, totalWeight, out AITreeToUse);
		m_PotentialSpawnAggregator.Clear();
		return result;
	}

	private TechData ChoosePopTechToSpawnImpl(List<SpawnRec> potentialSpawns, float totalWeight, out AITreeType AITreeToUse)
	{
		TechData result;
		if (potentialSpawns != null && potentialSpawns.Count > 0 && totalWeight > 0f)
		{
			float num = UnityEngine.Random.Range(0f, totalWeight);
			float num2 = 0f;
			int num3 = 0;
			while (true)
			{
				num2 += potentialSpawns[num3].weight;
				if (num3 + 1 >= potentialSpawns.Count || !(num2 < num))
				{
					break;
				}
				num3++;
			}
			result = potentialSpawns[num3].preset;
			int popTypeIndex = potentialSpawns[num3].popTypeIndex;
			AITreeToUse = (m_PopulationTypes[popTypeIndex].m_UseAI ? m_PopulationTypes[popTypeIndex].m_AITreeToUse : null);
		}
		else
		{
			result = null;
			AITreeToUse = null;
		}
		return result;
	}

	private TechData ChooseWaveTechToSpawn(out float spawnRadiusMin, out float spawnRadiusMax)
	{
		TechData techData;
		if (m_SaveData.m_WaveInd >= 0 && m_SaveData.m_WaveInd < m_WaveData.Length && m_WaveData[m_SaveData.m_WaveInd].m_TankChoices.Length != 0)
		{
			int num = UnityEngine.Random.Range(0, m_WaveData[m_SaveData.m_WaveInd].m_TankChoices.Length);
			techData = m_WaveData[m_SaveData.m_WaveInd].m_TankChoices[num].GetTechDataFormatted();
			spawnRadiusMin = m_WaveData[m_SaveData.m_WaveInd].m_SpawnRadiusMin;
			spawnRadiusMax = m_WaveData[m_SaveData.m_WaveInd].m_SpawnRadiusMax;
			if (techData == null)
			{
				d.LogWarning("WARNING ManPop.ChooseWavePresetToSpawn has a null TankPreset in wave " + m_SaveData.m_WaveInd + ", preset " + num);
				m_SaveData.m_WaveState = WaveState.NotDoingWaves;
			}
		}
		else
		{
			d.LogWarning("WARNING ManPop.ChooseWavePresetToSpawn has invalid wave data for wave " + m_SaveData.m_WaveInd);
			techData = null;
			spawnRadiusMin = 0f;
			spawnRadiusMax = 0f;
			m_SaveData.m_WaveState = WaveState.NotDoingWaves;
		}
		return techData;
	}

	private void DeterminePotentialSpawns(List<SpawnRec> outPotentialSpawns, out float totalWeight)
	{
		bool flag = Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD);
		TechSpawnFilter[] array = new TechSpawnFilter[flag ? 3 : 4];
		array[1] = (Singleton.Manager<ManTimeOfDay>.inst.NightTime ? m_NightFilter : m_DayFilter);
		array[2] = m_NoVendorFilter;
		if (!flag)
		{
			array[3] = m_NoExpFilter;
		}
		TechSpawnFilter.CorpPercentage outCorpPercent;
		bool hasFavouriteCorp = DetermineBiomeFavouredCorp(flag, out outCorpPercent);
		DeterminePotentialSpawnsImpl(array, hasFavouriteCorp, outCorpPercent, enableAll: false, outPotentialSpawns, out totalWeight);
	}

	private void DeterminePotentialSpawnsImpl(TechSpawnFilter[] filterChain, bool hasFavouriteCorp, TechSpawnFilter.CorpPercentage favouriteCorpPercentage, bool enableAll, List<SpawnRec> outPotentialSpawns, out float totalWeight)
	{
		outPotentialSpawns.Clear();
		int num = 0;
		for (int i = 0; i < m_PopTypeRuntimes.Length; i++)
		{
			if (m_PopTypeRuntimes[i].m_Type.m_Filter != null && (enableAll || m_PopTypeRuntimes[i].m_Enabled))
			{
				filterChain[0] = m_PopTypeRuntimes[i].m_Type.m_Filter;
				List<PresetInfo> list = Singleton.Manager<ManPresetFilter>.inst.FilterPresetList(filterChain, m_PopTypeRuntimes[i].m_CachedPresets, TechSpawnFilter.FallbackStage.Normal, TechSpawnFilter.CheckType.DynamicOnly);
				num = Math.Max(list.Count, num);
				for (int j = 0; j < list.Count; j++)
				{
					float weight = CalculateWeight(list[j], hasFavouriteCorp, favouriteCorpPercentage, m_PopTypeRuntimes[i].m_Type.m_Likelyhood) / (float)list.Count;
					outPotentialSpawns.Add(new SpawnRec
					{
						preset = list[j].TechData,
						popTypeIndex = i,
						weight = weight
					});
				}
			}
		}
		totalWeight = 0f;
		float num2 = num;
		for (int k = 0; k < outPotentialSpawns.Count; k++)
		{
			SpawnRec value = outPotentialSpawns[k];
			value.weight *= num2;
			outPotentialSpawns[k] = value;
			totalWeight += value.weight;
		}
	}

	private float CalculateWeight(PresetInfo presetInfo, bool hasFavouriteCorp, TechSpawnFilter.CorpPercentage favouriteCorpPercentage, float rawLikelyhoodWeight)
	{
		float num = 0f;
		if (hasFavouriteCorp)
		{
			s_CorpPercentageArray[0] = favouriteCorpPercentage;
			if (presetInfo.ContainsCorpPercentage(s_CorpPercentageArray))
			{
				num = 1f;
			}
		}
		float num2 = num * m_BiomeWeighting;
		float num3 = rawLikelyhoodWeight * m_LikelyhoodWeighting;
		float num4 = GetHistoryScore(presetInfo.TechData.Name) * m_HistoryWeighting;
		return num2 + num3 + num4;
	}

	private bool DetermineBiomeFavouredCorp(bool hasRAndD, out TechSpawnFilter.CorpPercentage outCorpPercent)
	{
		bool result = false;
		outCorpPercent.m_Corp = FactionSubTypes.GSO;
		outCorpPercent.m_Percentage = 0f;
		Biome biome = null;
		float num = 0f;
		for (int i = 0; i < 4; i++)
		{
			float num2 = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Weight(i);
			if (biome == null || num2 > num)
			{
				biome = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Biome(i);
				num = num2;
			}
		}
		if (biome != null)
		{
			for (int j = 0; j < m_CorpPerBiome.Length; j++)
			{
				if (biome == m_CorpPerBiome[j].m_Biome)
				{
					outCorpPercent = m_CorpPerBiome[j].m_FavouredCorp;
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private void StoreInHistory(string presetName)
	{
		if (m_SaveData.m_HistoryList.Count >= 20)
		{
			string key = m_SaveData.m_HistoryList[0];
			m_SaveData.m_HistoryList.RemoveAt(0);
			if (m_HistoryLookup.TryGetValue(key, out var value))
			{
				value--;
				if (value == 0)
				{
					m_HistoryLookup.Remove(key);
				}
				else
				{
					m_HistoryLookup[key] = value;
				}
			}
		}
		m_SaveData.m_HistoryList.Add(presetName);
		AddToHistoryLookup(presetName);
	}

	private void AddToHistoryLookup(string presetName)
	{
		if (!m_HistoryLookup.TryGetValue(presetName, out var value))
		{
			m_HistoryLookup.Add(presetName, 1);
			return;
		}
		value++;
		m_HistoryLookup[presetName] = value;
	}

	private float GetHistoryScore(string presetName)
	{
		if (m_HistoryLookup.ContainsKey(presetName))
		{
			int num = m_SaveData.m_HistoryList.IndexOf(presetName);
			int num2 = Mathf.Max(m_SaveData.m_HistoryList.Count, 1);
			return 1f - (float)(num + 1) / (float)num2;
		}
		return 1f;
	}

	private float GenerateRandomSpawnAngleDegrees()
	{
		float num = UnityEngine.Random.Range(0f, m_TotalAngleWeight);
		float num2 = 0f;
		int num3 = 0;
		while (true)
		{
			float num4 = m_AngleWeights[num3];
			num2 += num4;
			if (num3 + 1 >= m_AngleWeights.Length || num2 > num)
			{
				break;
			}
			num3++;
		}
		return (float)num3 * 11.25f + 5.625f - 180f;
	}

	private void CheckForDestroyedTechs()
	{
		for (int num = m_SpawnedTechs.Count - 1; num >= 0; num--)
		{
			TrackedVisible trackedVisible = m_SpawnedTechs[num];
			if (trackedVisible.wasDestroyed)
			{
				m_SpawnedTechs.RemoveAt(num);
				m_SaveData.m_EnemiesKilled++;
				SetTimeoutAreaToCurrentPosition();
			}
			else
			{
				bool flag = false;
				foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
				{
					if ((trackedVisible.Position - allPlayerTech.boundsCentreWorld).ToVector2XZ().sqrMagnitude <= m_DestroyRadiusThreshold * m_DestroyRadiusThreshold)
					{
						flag = true;
						break;
					}
				}
				if ((trackedVisible.Position - Singleton.cameraTrans.position).ToVector2XZ().sqrMagnitude <= m_DestroyRadiusThreshold * m_DestroyRadiusThreshold)
				{
					flag = true;
				}
				WorldPosition worldPosition = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? WorldPosition.FromGameWorldPosition(Singleton.playerPos) : Singleton.Manager<ManNetwork>.inst.MapCenter);
				if (!flag && (trackedVisible.Position - worldPosition.ScenePosition).ToVector2XZ().sqrMagnitude > m_DestroyRadiusThreshold * m_DestroyRadiusThreshold)
				{
					Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(trackedVisible);
					m_SpawnedTechs.RemoveAt(num);
				}
			}
		}
	}

	private int CountInRangeTechs()
	{
		int num = 0;
		for (int num2 = m_SpawnedTechs.Count - 1; num2 >= 0; num2--)
		{
			TrackedVisible trackedVisible = m_SpawnedTechs[num2];
			foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
			{
				if ((trackedVisible.Position - allPlayerTech.boundsCentreWorld).ToVector2XZ().sqrMagnitude <= m_DestroyRadiusThreshold * m_DestroyRadiusThreshold)
				{
					num++;
					break;
				}
			}
		}
		return num;
	}

	private void ConfigureWaveTech(Tank tech)
	{
		if ((bool)tech && m_SaveData.m_WaveState != WaveState.NotDoingWaves && m_SaveData.m_WaveInd < m_WaveData.Length)
		{
			WaveData waveData = m_WaveData[m_SaveData.m_WaveInd];
			BlockManager.BlockIterator<ModuleDamage>.Enumerator enumerator = tech.blockman.IterateBlockComponents<ModuleDamage>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.SetHealOnDetatch(waveData.m_BlockDetachHealPercentage);
			}
			BlockManager.BlockIterator<ModuleDriveBot>.Enumerator enumerator2 = tech.blockman.IterateBlockComponents<ModuleDriveBot>().GetEnumerator();
			while (enumerator2.MoveNext())
			{
				enumerator2.Current.SetPatrolDist(waveData.m_PatrolRangeMin, waveData.m_PatrolRangeMax);
			}
		}
	}

	private void UpdateTimeOutZone()
	{
		if (m_SaveData.m_TimeOutInZone)
		{
			float num2;
			if (m_SaveData.m_TimeOutProgress < m_TimeOutAreaShrinkTime && m_TimeOutAreaShrinkTime > 0f)
			{
				float num = m_TimeOutAreaRadiusDefaultMax - m_TimeOutAreaRadiusDefaultMin;
				num2 = m_TimeOutAreaRadiusDefaultMax - Mathf.Clamp01(m_SaveData.m_TimeOutProgress / m_TimeOutAreaShrinkTime) * num;
				m_SaveData.m_TimeOutProgress += Time.deltaTime;
			}
			else
			{
				num2 = m_TimeOutAreaRadiusDefaultMin;
			}
			if ((bool)Singleton.playerTank)
			{
				float num3 = num2 * Singleton.playerTank.visible.Radius;
				Vector3 vector = Singleton.playerPos.SetY(0f) - m_SaveData.m_TimeOutCentreWorld.ScenePosition;
				m_SaveData.m_TimeOutInZone = vector.sqrMagnitude <= num3 * num3;
			}
		}
	}

	private void SetTimeoutAreaToCurrentPosition()
	{
		if ((bool)Singleton.playerTank)
		{
			m_SaveData.m_TimeOutCentre = Vector3.zero;
			m_SaveData.m_TimeOutCentreWorld = WorldPosition.FromScenePosition(Singleton.playerPos.SetY(0f));
			m_SaveData.m_TimeOutProgress = 0f;
			m_SaveData.m_TimeOutInZone = true;
		}
	}

	private void OnModePreExit(Mode mode)
	{
		if (m_SaveData != null)
		{
			m_SaveData.m_Active = false;
		}
		m_Spawner.Cancel();
	}

	private void OnSpawned(TrackedVisible tv, string identifier, PerVisibleParams encounterParams, string debugLog)
	{
		if (tv != null)
		{
			if (tv.visible != null)
			{
				Tank tank = tv.visible.tank;
				d.AssertFormat(tank != null, "ManPop.OnSpawned has got a visible that isnt a tank (name={0})", base.name);
				m_SaveData.m_TimeBeforeSpawn = m_MinPeriodBetweenSpawns;
				StoreInHistory(tank.IsNotNull() ? tank.name : "null tech!");
				m_SpawnedTechs.Add(tv);
				if (m_SaveData.m_WaveState != WaveState.NotDoingWaves && tank != null)
				{
					ConfigureWaveTech(tank);
				}
				if (m_SaveData.m_WaveState == WaveState.WaveStart)
				{
					m_SaveData.m_WaveState = WaveState.WaveInProgress;
				}
			}
			else
			{
				m_SpawnedTechs.Add(tv);
			}
		}
		else
		{
			d.LogWarning("WARNING: ManPop.OnSpawned: Tracked Visible not returned by FreeSpaceFinder, ID = " + identifier);
			d.LogWarning("FreeSpaceFinder Debug Output: " + debugLog);
		}
	}

	private void OnPlayerTechChanged(Tank tech, bool nowActive)
	{
		if ((bool)tech)
		{
			if (nowActive)
			{
				tech.DamageEvent.Subscribe(OnPlayerDamaged);
				tech.TankRecycledEvent.Unsubscribe(OnPlayerTankRecycled);
				tech.TankRecycledEvent.Subscribe(OnPlayerTankRecycled);
			}
			else
			{
				tech.DamageEvent.Unsubscribe(OnPlayerDamaged);
				tech.TankRecycledEvent.Unsubscribe(OnPlayerTankRecycled);
			}
		}
	}

	private void OnTechTeamChange(Tank tech, ManTechs.TeamChangeInfo changeInfo)
	{
		if (changeInfo.m_OldIsPopulation && !changeInfo.m_NewIsPopulation)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tech.visible.ID);
			m_SpawnedTechs.Remove(trackedVisible);
		}
	}

	private void OnPlayerTankRecycled(Tank tech)
	{
		if ((bool)tech)
		{
			tech.DamageEvent.Unsubscribe(OnPlayerDamaged);
		}
	}

	private void OnPlayerDamaged(ManDamage.DamageInfo info)
	{
		if (m_SaveData == null || m_SaveData.m_WaveState == WaveState.NotDoingWaves || m_SaveData.m_WaveInd >= m_WaveData.Length || !Singleton.playerTank || !info.SourceTank)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < m_SpawnedTechs.Count; i++)
		{
			if ((bool)m_SpawnedTechs[i].visible && m_SpawnedTechs[i].visible.tank == info.SourceTank)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			float playerHealPercentage = m_WaveData[m_SaveData.m_WaveInd].m_PlayerHealPercentage;
			BlockManager.BlockIterator<Damageable>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlockComponents<Damageable>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Damageable current = enumerator.Current;
				float amount = playerHealPercentage * info.Damage;
				current.Repair(amount, sendEvent: false);
			}
		}
	}

	private void DrawLegacyGUI()
	{
		string text = ((m_SaveData != null && m_SaveData.m_PauseTimer > 0f) ? $"Population paused: {m_SaveData.m_PauseTimer}" : "Population running");
		GUI.Box(new Rect(10f, 10f, 300f, 30f), text);
	}

	private void Start()
	{
		IsSettingUp = false;
		m_HistoryLookup = new Dictionary<string, int>(20);
		m_SpawnedTechs = new List<TrackedVisible>();
		m_PopTypeRuntimes = new PopTypeRuntime[m_PopulationTypes.Length];
		for (int i = 0; i < m_PopTypeRuntimes.Length; i++)
		{
			m_PopTypeRuntimes[i].m_Type = m_PopulationTypes[i];
		}
		m_AngleWeights = new float[32];
		m_TotalAngleWeight = 0f;
		for (int j = 0; j < 32; j++)
		{
			float time = (float)j * 11.25f + 5.625f;
			float num = m_SpawnAngularDistribution.Evaluate(time);
			m_AngleWeights[j] = num;
			m_TotalAngleWeight += num;
		}
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModePreExit);
		m_Spawner.OnObjectSpawned.Subscribe(OnSpawned);
	}

	private void FinishSetup()
	{
		if (m_SaveData == null)
		{
			return;
		}
		m_SpawnedTechs.Clear();
		for (int i = 0; i < m_SaveData.m_SpawnedIds.Count; i++)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_SaveData.m_SpawnedIds[i]);
			if (trackedVisible != null)
			{
				m_SpawnedTechs.Add(trackedVisible);
			}
			else
			{
				d.LogWarning("WARNING ManPop.Load count not find spawned tracked visible with ID " + m_SaveData.m_SpawnedIds[i]);
			}
		}
		m_SaveData.m_SpawnedIds.Clear();
		if (m_SaveData.m_WaveState != WaveState.NotDoingWaves)
		{
			for (int j = 0; j < m_SpawnedTechs.Count; j++)
			{
				if ((bool)m_SpawnedTechs[j].visible && (bool)m_SpawnedTechs[j].visible.tank)
				{
					ConfigureWaveTech(m_SpawnedTechs[j].visible.tank);
				}
			}
		}
		m_HistoryLookup.Clear();
		for (int k = 0; k < m_SaveData.m_HistoryList.Count; k++)
		{
			AddToHistoryLookup(m_SaveData.m_HistoryList[k]);
		}
		for (int l = 0; l < m_PopTypeRuntimes.Length; l++)
		{
			if (m_SaveData.m_EnabledPopTypes.Contains(m_PopTypeRuntimes[l].m_Type.m_Name))
			{
				m_PopTypeRuntimes[l].m_Enabled = true;
			}
		}
		m_SaveData.m_EnabledPopTypes.Clear();
		if (m_SaveData.m_TimeOutCentreWorld == default(WorldPosition) && m_SaveData.m_TimeOutCentre != Vector3.zero)
		{
			m_SaveData.m_TimeOutCentreWorld = WorldPosition.FromGameWorldPosition((Vector3)m_SaveData.m_TimeOutCentre);
			m_SaveData.m_TimeOutCentre = Vector3.zero;
		}
	}

	private void Update()
	{
		if (m_SaveData == null)
		{
			return;
		}
		if (IsSettingUp && !Singleton.Manager<ManPresetFilter>.inst.IsSettingUp)
		{
			IsSettingUp = SetupPopTypeFilters();
			if (!IsSettingUp)
			{
				FinishSetup();
			}
		}
		if ((bool)Singleton.playerTank)
		{
			m_SaveData.m_PauseTimer = Math.Max(m_SaveData.m_PauseTimer - Time.deltaTime, 0f);
			m_SaveData.m_TimeBeforeSpawn = Math.Max(m_SaveData.m_TimeBeforeSpawn - Time.deltaTime, 0f);
		}
		if (ManNetwork.IsHost)
		{
			CheckForDestroyedTechs();
		}
		int num = CountInRangeTechs();
		if (m_SaveData.m_WaveState == WaveState.WaveInProgress && num == 0)
		{
			if (m_SaveData.m_WaveInd + 1 < m_WaveData.Length)
			{
				m_SaveData.m_WaveInd++;
				m_SaveData.m_WaveState = WaveState.WaveStart;
			}
			else
			{
				m_SaveData.m_WaveInd = -1;
				m_SaveData.m_WaveState = WaveState.NotDoingWaves;
			}
		}
		UpdateTimeOutZone();
		if (m_SaveData.m_Active && m_SaveData.m_PauseTimer == 0f && !m_Spawner.IsBusy && m_SaveData.m_TimeBeforeSpawn == 0f && !m_SaveData.m_TimeOutInZone && num < m_PopulationLimit && m_SaveData.m_WaveState != WaveState.WaveInProgress && ManNetwork.IsHost && !Singleton.Manager<ManBlockLimiter>.inst.IsOverNonPlayerLimit)
		{
			TryToSpawn();
		}
		if (s_DebugShowInfo != m_GuiCallback.IsNotNull())
		{
			if (s_DebugShowInfo)
			{
				m_GuiCallback = OnGUICallback.AddGUICallback(base.gameObject);
				m_GuiCallback.OnGUIEvent.Subscribe(DrawLegacyGUI);
			}
			else
			{
				m_GuiCallback.OnGUIEvent.Unsubscribe(DrawLegacyGUI);
				m_GuiCallback = null;
			}
		}
	}
}
