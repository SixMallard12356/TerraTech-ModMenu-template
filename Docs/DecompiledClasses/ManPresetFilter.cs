#define UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class ManPresetFilter : Singleton.Manager<ManPresetFilter>, Mode.IManagerModeEvents
{
	private PopulationTable m_PopulationTable;

	private bool m_HasCompletedPresetSetup;

	private bool m_InitialisingPresets;

	private int m_ProcessPresetFilterFolderIndex;

	private int m_ProcessPresetFilterTechIndex;

	private bool m_ResettingPresetsForMode;

	private int m_InitPresetsForModeIndex;

	private const int kTimeToProcessPerUpdateMS = 80;

	private Stopwatch m_ProcessCostTimer = new Stopwatch();

	private List<PresetInfo> m_AllTechs = new List<PresetInfo>();

	private Dictionary<string, List<PresetInfo>> m_TechsInFolderLookUp = new Dictionary<string, List<PresetInfo>>();

	private Dictionary<int, List<PresetInfo>> m_CachedStaticFilters = new Dictionary<int, List<PresetInfo>>();

	public bool IsSettingUp
	{
		get
		{
			if (!m_InitialisingPresets)
			{
				return m_ResettingPresetsForMode;
			}
			return true;
		}
	}

	public PopulationTable PopulationTable => m_PopulationTable;

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		SetupPresetInfo();
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
		if (!m_HasCompletedPresetSetup)
		{
			m_ProcessPresetFilterFolderIndex = 0;
			m_ProcessPresetFilterTechIndex = 0;
			m_InitialisingPresets = false;
		}
	}

	public List<PresetInfo> FilterPresets(TechSpawnFilter[] filters, bool allowFallbacks, TechSpawnFilter.CheckType checkType, bool logResults = false)
	{
		int filterListHash = GetFilterListHash(filters);
		if (checkType == TechSpawnFilter.CheckType.StaticOnly && m_CachedStaticFilters.TryGetValue(filterListHash, out var value))
		{
			return value;
		}
		d.Assert(filters.Length != 0, "FilterPresets needs at least one filter provided");
		int[] debugRejectCounts = null;
		int[] debugFailReasons = null;
		List<PresetInfo> list = null;
		TechSpawnFilter.FallbackStage fallbackStage = TechSpawnFilter.FallbackStage.Normal;
		TechSpawnFilter techSpawnFilter = filters[0];
		int num = EnumNamesIterator<TechSpawnFilter.FallbackStage>.Names.Length;
		for (int i = 0; i < num; i++)
		{
			if (techSpawnFilter.UseFolderFilters)
			{
				list = new List<PresetInfo>();
				for (int j = 0; j < techSpawnFilter.FoldersToUse.Length; j++)
				{
					string text = techSpawnFilter.FoldersToUse[j];
					if (m_TechsInFolderLookUp.TryGetValue(text, out var value2))
					{
						List<PresetInfo> list2 = FilterPresetList(filters, debugRejectCounts, debugFailReasons, value2, fallbackStage, checkType);
						if (list2.Count > 0)
						{
							list.AddRange(list2);
						}
					}
					else
					{
						d.LogError("ManPresetFilter.FilterPresets: No folder set in PopulationTable with name " + text);
					}
				}
			}
			else
			{
				list = FilterPresetList(filters, debugRejectCounts, debugFailReasons, m_AllTechs, fallbackStage, checkType);
			}
			if (list.Count > 0 || !allowFallbacks)
			{
				break;
			}
			fallbackStage++;
		}
		if (checkType == TechSpawnFilter.CheckType.StaticOnly)
		{
			m_CachedStaticFilters.Add(filterListHash, list);
		}
		return list;
	}

	private int GetFilterListHash(TechSpawnFilter[] filters)
	{
		int num = 17;
		for (int i = 0; i < filters.Length; i++)
		{
			num = num * 31 + filters[i].GetInstanceID();
		}
		return num;
	}

	public TechData GetRandomTechDataFromFilterList(TechSpawnFilter[] filters)
	{
		bool allowFallbacks = true;
		List<PresetInfo> presetList = FilterPresets(filters, allowFallbacks, TechSpawnFilter.CheckType.StaticAndDynamic);
		return GetRandomTechDatatFromList(presetList);
	}

	public List<PresetInfo> FilterPresetList(TechSpawnFilter[] filters, List<PresetInfo> presetInfoList, TechSpawnFilter.FallbackStage fallbackStage, TechSpawnFilter.CheckType checkType)
	{
		return FilterPresetList(filters, null, null, presetInfoList, fallbackStage, checkType);
	}

	private List<PresetInfo> FilterPresetList(TechSpawnFilter[] filters, int[] debugRejectCounts, int[] debugFailReasons, List<PresetInfo> presetInfoList, TechSpawnFilter.FallbackStage fallbackStage, TechSpawnFilter.CheckType checkType)
	{
		List<PresetInfo> list = new List<PresetInfo>();
		for (int i = 0; i < presetInfoList.Count; i++)
		{
			PresetInfo presetInfo = presetInfoList[i];
			bool flag = true;
			int num = 0;
			while (flag && num < filters.Length)
			{
				if (!filters[num].PresetPasses(presetInfo, fallbackStage, checkType))
				{
					flag = false;
				}
				else
				{
					num++;
				}
			}
			if (flag)
			{
				list.Add(presetInfo);
			}
		}
		return list;
	}

	private void SetupPresetInfo()
	{
		if (!m_HasCompletedPresetSetup)
		{
			m_AllTechs.Clear();
			m_TechsInFolderLookUp.Clear();
			m_InitialisingPresets = true;
			m_ProcessPresetFilterFolderIndex = 0;
			m_ProcessPresetFilterTechIndex = 0;
		}
		else
		{
			m_ResettingPresetsForMode = true;
			m_InitPresetsForModeIndex = 0;
		}
	}

	private TechData GetRandomTechDatatFromList(List<PresetInfo> presetList)
	{
		TechData result = null;
		if (presetList != null && presetList.Count > 0)
		{
			int index = Random.Range(0, presetList.Count);
			result = presetList[index].TechData;
		}
		return result;
	}

	private void OnBlockDiscovered(BlockTypes blockDiscovered)
	{
		for (int i = 0; i < m_AllTechs.Count; i++)
		{
			m_AllTechs[i].BlockDiscovered(blockDiscovered);
		}
	}

	private void OnBlockAvailable(BlockTypes blockAvailable)
	{
		for (int i = 0; i < m_AllTechs.Count; i++)
		{
			m_AllTechs[i].BlockAvailable(blockAvailable);
		}
	}

	private void OnBundleLoaded(AssetBundleCreateRequest request)
	{
		if ((bool)request.assetBundle)
		{
			d.Log($"[ManPresetFilter.OnBundleLoaded] loaded assetBundle {request.assetBundle?.name} @{Time.realtimeSinceStartup}");
			request.assetBundle.LoadAssetAsync<PopulationTable>("PopulationTable").completed += delegate(AsyncOperation op)
			{
				OnBundleAssetsLoaded((AssetBundleRequest)op);
			};
		}
		else
		{
			d.LogError("[ManPresetFilter.OnBundleLoaded] Failed to load assetBundle");
		}
	}

	private void OnBundleAssetsLoaded(AssetBundleRequest request)
	{
		m_PopulationTable = (PopulationTable)request.asset;
		d.Log($"[ManPresetFilter.OnBundleAssetsLoaded] Loaded population table with {m_PopulationTable.GetPresetCount()} presets @{Time.realtimeSinceStartup}");
	}

	private void Start()
	{
		bool switchUI = SKU.SwitchUI;
		if (m_PopulationTable == null)
		{
			string path = (switchUI ? "population.switch" : "population.default");
			string text = Path.Combine(Application.streamingAssetsPath, path);
			d.Log($"[ManPresetFilter.Start] Loading asset bundle {text} @{Time.realtimeSinceStartup}");
			AssetBundle.LoadFromFileAsync(text).completed += delegate(AsyncOperation op)
			{
				OnBundleLoaded((AssetBundleCreateRequest)op);
			};
		}
		m_HasCompletedPresetSetup = false;
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Subscribe(OnBlockDiscovered);
		Singleton.Manager<ManLicenses>.inst.BlockAvailableEvent.Subscribe(OnBlockAvailable);
	}

	private bool ProcessPresetFilters()
	{
		if (m_PopulationTable == null)
		{
			return true;
		}
		m_ProcessCostTimer.Restart();
		bool flag = false;
		while (m_ProcessPresetFilterFolderIndex < m_PopulationTable.m_FolderTechs.Length)
		{
			PopulationTable.FolderTechList folderTechList = m_PopulationTable.m_FolderTechs[m_ProcessPresetFilterFolderIndex];
			string folderName = folderTechList.m_FolderName;
			List<TankPreset> presets = folderTechList.m_Presets;
			int count = presets.Count;
			if (!m_TechsInFolderLookUp.TryGetValue(folderName, out var value))
			{
				value = new List<PresetInfo>(count);
			}
			while (m_ProcessPresetFilterTechIndex < count && m_ProcessCostTimer.ElapsedMilliseconds < 80)
			{
				TankPreset tankPreset = presets[m_ProcessPresetFilterTechIndex];
				if ((bool)tankPreset)
				{
					PresetInfo item = new PresetInfo(tankPreset);
					value.Add(item);
				}
				else
				{
					d.LogError("ManPresetFilter.SetupPresetInfo - Preset is null in Population Table - folder: " + folderName);
				}
				m_ProcessPresetFilterTechIndex++;
			}
			m_TechsInFolderLookUp[folderName] = value;
			if (m_ProcessPresetFilterTechIndex < count)
			{
				flag = true;
				break;
			}
			m_ProcessPresetFilterTechIndex = 0;
			m_AllTechs.AddRange(value);
			m_ProcessPresetFilterFolderIndex++;
		}
		if (!flag)
		{
			m_HasCompletedPresetSetup = true;
			m_ResettingPresetsForMode = true;
			m_InitPresetsForModeIndex = 0;
		}
		m_ProcessCostTimer.Stop();
		return flag;
	}

	private bool ResetPresetInfos()
	{
		int count = m_AllTechs.Count;
		m_ProcessCostTimer.Restart();
		while (m_InitPresetsForModeIndex < count && m_ProcessCostTimer.ElapsedMilliseconds < 80)
		{
			m_AllTechs[m_InitPresetsForModeIndex].ReInitForMode();
			m_InitPresetsForModeIndex++;
		}
		m_ProcessCostTimer.Stop();
		return m_InitPresetsForModeIndex < count;
	}

	private void Update()
	{
		if (m_InitialisingPresets)
		{
			m_InitialisingPresets = ProcessPresetFilters();
		}
		else if (m_ResettingPresetsForMode)
		{
			m_ResettingPresetsForMode = ResetPresetInfos();
		}
	}
}
