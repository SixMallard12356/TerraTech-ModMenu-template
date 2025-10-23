#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManProgression : Singleton.Manager<ManProgression>
{
	public enum ManuallyManagedEncounter
	{
		FindMissionGiver,
		FindMissionGiverCoOp
	}

	public enum EncounterListType
	{
		Singleplayer,
		CoOp,
		Demo
	}

	[Serializable]
	public class ManuallyManagedEncounterData
	{
		public FactionSubTypes corp;

		public int grade;

		public EncounterIdentifier encounterIdentifier;

		public EncounterData encounterData;

		public void SetupIdentifier(ManuallyManagedEncounter encounterType)
		{
			encounterIdentifier = new EncounterIdentifier(corp, grade, "manual", encounterType.ToString());
		}

		public bool IsValid()
		{
			if (encounterData != null)
			{
				return encounterData.m_EncounterPrefab != null;
			}
			return false;
		}
	}

	[SerializeField]
	private EncounterList m_SingleplayerEncounterList;

	[SerializeField]
	private EncounterList m_CoOpEncounterList;

	[SerializeField]
	private EncounterList m_DemoEncounterList;

	[Tooltip("Time - in seconds - to wait and leave the mission log empty before spawning the 'find nearby trading station to give you missions' encounter")]
	[SerializeField]
	private float m_TimeMissionLogEmptyBeforeFindMissionGiverEncounter = 60f;

	[EnumArray(typeof(ManuallyManagedEncounter))]
	[SerializeField]
	private ManuallyManagedEncounterData[] m_ManuallyManagedEncounterData = new ManuallyManagedEncounterData[1];

	private EncounterList m_CurrentEncounterList;

	private List<EncounterIdentifier> m_CompletedEncounters = new List<EncounterIdentifier>();

	private List<EncounterToSpawn> m_AvailableCoreEncounters = new List<EncounterToSpawn>();

	private bool m_UseRandomEncounterSpawn;

	private float m_EncounterSpawnTimer;

	private float m_LastTimeEncounterInLog;

	private ManuallyManagedEncounterData m_FindMissionGiver;

	public EventNoParams OnEncountersLoaded;

	private List<CorpEncounters> Encounters => m_CurrentEncounterList.Encounters;

	public void SetEncounterList(EncounterListType type)
	{
		Singleton.Manager<ManEncounterPlacement>.inst.ClearReservedEncounters();
		for (int i = 0; i < m_ManuallyManagedEncounterData.Length; i++)
		{
			m_ManuallyManagedEncounterData[i].SetupIdentifier((ManuallyManagedEncounter)i);
		}
		switch (type)
		{
		case EncounterListType.Singleplayer:
			m_CurrentEncounterList = m_SingleplayerEncounterList;
			m_FindMissionGiver = m_ManuallyManagedEncounterData[0];
			break;
		case EncounterListType.CoOp:
			m_CurrentEncounterList = m_CoOpEncounterList;
			m_FindMissionGiver = m_ManuallyManagedEncounterData[1];
			break;
		case EncounterListType.Demo:
			m_CurrentEncounterList = m_DemoEncounterList;
			m_FindMissionGiver = null;
			break;
		default:
			d.LogError("Invalid encounter list type");
			break;
		}
		d.Assert(GetAllEncounterLists().Contains(m_CurrentEncounterList), "ManProgression.SetEncounterList is using an encounter list that's not returned by GetAllEncounterLists");
		Singleton.Manager<ManEncounter>.inst.RegisterEncounters(m_CurrentEncounterList);
		for (int j = 0; j < m_ManuallyManagedEncounterData.Length; j++)
		{
			Singleton.Manager<ManEncounter>.inst.RegisterEncounter(m_ManuallyManagedEncounterData[j].encounterIdentifier, m_ManuallyManagedEncounterData[j].encounterData);
		}
		m_LastTimeEncounterInLog = 0f;
	}

	public List<EncounterList> GetAllEncounterLists()
	{
		return new List<EncounterList> { m_SingleplayerEncounterList, m_CoOpEncounterList, m_DemoEncounterList };
	}

	public EncounterIdentifier CreateEncounterIdentifier(int hashID)
	{
		foreach (CorpEncounters encounter in Encounters)
		{
			for (int i = 0; i < encounter.m_Grades.Count; i++)
			{
				foreach (EncounterCategory category in encounter.m_Grades[i].m_Categories)
				{
					foreach (EncounterData encounter2 in category.m_Encounters)
					{
						EncounterIdentifier result = new EncounterIdentifier(encounter.m_Corp, i + 1, category.m_Name, encounter2.m_Name);
						if (result.GetHashCode() == hashID)
						{
							return result;
						}
					}
				}
			}
		}
		return EncounterIdentifier.Invalid;
	}

	public bool SetCoreEncounterSpawnPos(EncounterIdentifier encounterDesc, WorldPosition spawnPos)
	{
		bool result = false;
		for (int i = 0; i < m_AvailableCoreEncounters.Count; i++)
		{
			if (m_AvailableCoreEncounters[i].m_EncounterDef == encounterDesc)
			{
				m_AvailableCoreEncounters[i].m_Position = spawnPos;
				m_AvailableCoreEncounters[i].m_UsePosForPlacement = true;
				result = true;
				break;
			}
		}
		return result;
	}

	public bool IsCoreEncounterCompleted(FactionSubTypes corp, int grade, string encounterName)
	{
		EncounterIdentifier encounterDef = new EncounterIdentifier(corp, grade, "core", encounterName);
		return IsCoreEncounterCompleted(encounterDef);
	}

	public bool IsCoreEncounterCompleted(EncounterIdentifier encounterDef)
	{
		d.Assert(encounterDef.IsCoreEncounter, "IsCoreEncounterCompleted was passed a non-core encounter!");
		return m_CompletedEncounters.Contains(encounterDef);
	}

	public void EnableOtherEncounters()
	{
		m_UseRandomEncounterSpawn = true;
	}

	public List<EncounterToSpawn> GetMissionList(EncounterCollectionParams collectParams)
	{
		return Singleton.Manager<ManEncounterPlacement>.inst.GetReservedEncounters(collectParams);
	}

	public void StartSearchForEncounters(EncounterCollectionParams collectParams, int requesterID, Action<EncounterToSpawn> encounterCollectedEvent, Action finishedCollectingEvent, List<EncounterToSpawn> initialList = null)
	{
		WeightedGroup<FactionSubTypes> corpWeights = default(WeightedGroup<FactionSubTypes>);
		PopulateCorpWeights(ref corpWeights);
		Dictionary<FactionSubTypes, List<EncounterToSpawn>> randomEncounters = new Dictionary<FactionSubTypes, List<EncounterToSpawn>>(4, default(FactionSubTypesComparer));
		if (Singleton.Manager<DebugUtil>.inst.SpawnAllMissions)
		{
			DebugGetAllEncounters(randomEncounters);
		}
		else
		{
			PopulateListOfRandomEncountersAvailable(ref randomEncounters, collectParams.centrePosition, ref corpWeights, autoSpawningEncounter: false);
		}
		Singleton.Manager<ManEncounterPlacement>.inst.CollectLocalAvailableEncounters(collectParams, requesterID, m_AvailableCoreEncounters, randomEncounters, corpWeights, encounterCollectedEvent, finishedCollectingEvent, initialList);
	}

	public void StopSearchForEncounters(int requesterID)
	{
		Singleton.Manager<ManEncounterPlacement>.inst.StopCollectingLocalEncounters(requesterID);
	}

	private void PopulateCorpWeights(ref WeightedGroup<FactionSubTypes> corpWeights)
	{
		corpWeights.Clear();
		foreach (CorpEncounters encounter in m_CurrentEncounterList.Encounters)
		{
			FactionSubTypes corp = encounter.m_Corp;
			if (Singleton.Manager<ManLicenses>.inst.IsLicenseSupported(corp) && Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(corp))
			{
				float newWeight = ((Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corp) >= Singleton.Manager<ManLicenses>.inst.MaxSupportedTier(corp)) ? 50f : 100f);
				corpWeights.SetWeight(corp, newWeight);
			}
		}
	}

	private void PopulateListOfRandomEncountersAvailable(ref Dictionary<FactionSubTypes, List<EncounterToSpawn>> randomEncounters, Vector3 worldPosition, ref WeightedGroup<FactionSubTypes> corpWeights, bool autoSpawningEncounter)
	{
		int count = Encounters.Count;
		for (int i = 0; i < count; i++)
		{
			FactionSubTypes corp = Encounters[i].m_Corp;
			List<EncounterToSpawn> list = new List<EncounterToSpawn>(32);
			int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corp);
			int val = Singleton.Manager<ManLicenses>.inst.MaxSupportedTier(corp);
			int num = Math.Min(currentLevel, val);
			if (num >= 0 && num < Encounters[i].m_Grades.Count)
			{
				EncounterGrade encounterGrade = Encounters[i].m_Grades[num];
				int count2 = encounterGrade.m_Categories.Count;
				for (int j = 1; j < count2; j++)
				{
					EncounterCategory encounterCategory = encounterGrade.m_Categories[j];
					int count3 = encounterCategory.m_Encounters.Count;
					for (int k = 0; k < count3; k++)
					{
						EncounterData encounterData = encounterCategory.m_Encounters[k];
						if (autoSpawningEncounter == encounterData.m_SpawnWithoutUserAccept && encounterData.CheckSpawnConditions())
						{
							EncounterIdentifier encounterDef = new EncounterIdentifier(corp, num + 1, encounterCategory.m_Name, encounterData.m_Name);
							EncounterToSpawn item = new EncounterToSpawn(encounterData, encounterDef);
							list.Add(item);
						}
					}
				}
			}
			if (list.Count == 0)
			{
				corpWeights.SetWeight(corp, 0f);
			}
			randomEncounters.Add(corp, list);
		}
	}

	private void UpdateFindNearbyMissionGiverEncounter()
	{
		if (!Singleton.playerTank || m_FindMissionGiver == null || !m_FindMissionGiver.IsValid() || !ManNetwork.IsHost)
		{
			return;
		}
		Encounter activeEncounter = Singleton.Manager<ManEncounter>.inst.GetActiveEncounter(m_FindMissionGiver.encounterIdentifier);
		if (activeEncounter == null && (!Singleton.Manager<ManLicenses>.inst.HasMaxedAllLicences() || m_AvailableCoreEncounters.Count > 0) && !Singleton.Manager<ManQuestLog>.inst.HasTrackedEncounter && Singleton.Manager<ManQuestLog>.inst.NumEncountersInLog == 0)
		{
			if (Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() > m_LastTimeEncounterInLog + m_TimeMissionLogEmptyBeforeFindMissionGiverEncounter && m_FindMissionGiver.encounterData.CheckSpawnConditions())
			{
				EncounterToSpawn encounterToSpawn = new EncounterToSpawn(m_FindMissionGiver.encounterData, m_FindMissionGiver.encounterIdentifier);
				encounterToSpawn.m_Position = WorldPosition.FromScenePosition(Singleton.playerPos);
				Singleton.Manager<ManEncounter>.inst.StartEncounter(encounterToSpawn);
			}
			return;
		}
		bool flag = false;
		foreach (Encounter activeEncounter2 in Singleton.Manager<ManEncounter>.inst.ActiveEncounters)
		{
			if (activeEncounter2.VisibleInLog && (activeEncounter == null || activeEncounter2 != activeEncounter))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			m_LastTimeEncounterInLog = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
			if (activeEncounter != null)
			{
				Singleton.Manager<ManEncounter>.inst.FinishEncounter(activeEncounter, ManEncounter.FinishState.Completed);
			}
		}
	}

	public void SkipEncountersForTutorialSkip()
	{
		foreach (CorpEncounters encounter in Encounters)
		{
			for (int i = 0; i < encounter.m_Grades.Count; i++)
			{
				foreach (EncounterCategory category in encounter.m_Grades[i].m_Categories)
				{
					foreach (EncounterData encounter2 in category.m_Encounters)
					{
						if (!encounter2.m_SkippedByTutorialSkip)
						{
							continue;
						}
						EncounterIdentifier id = new EncounterIdentifier(encounter.m_Corp, i + 1, category.m_Name, encounter2.m_Name);
						if (m_CompletedEncounters.Contains(id))
						{
							continue;
						}
						if (!id.IsCoreEncounter)
						{
							d.LogWarning($"SkipEncountersForTutorial: unable to skip non-core encounter: {id}");
							continue;
						}
						if (Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(id))
						{
							d.LogWarning($"SkipEncountersForTutorial: unable to skip encounter which is currently active: {id}");
							continue;
						}
						m_CompletedEncounters.Add(id);
						m_AvailableCoreEncounters.RemoveAll((EncounterToSpawn enc) => enc.m_EncounterDef == id);
						Singleton.Manager<ManEncounterPlacement>.inst.RemoveSkippedCoreEncounter(id);
						d.Log($"SkipEncountersForTutorial: Skipped: {id}");
					}
				}
			}
		}
	}

	private void LoadNewRequiredCoreEncounters()
	{
		for (int num = Encounters.Count - 1; num >= 0; num--)
		{
			FactionSubTypes corp = Encounters[num].m_Corp;
			if (Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(corp))
			{
				for (int num2 = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corp); num2 >= 0; num2--)
				{
					EncounterCategory encounterCategory = Encounters[num].m_Grades[num2].m_Categories[0];
					List<EncounterData> encounters = encounterCategory.m_Encounters;
					for (int num3 = encounters.Count - 1; num3 >= 0; num3--)
					{
						EncounterData encounterData = encounters[num3];
						if (encounterData.m_ForceSpawnIfNew)
						{
							EncounterIdentifier encounterIdentifier = new EncounterIdentifier(corp, num2 + 1, encounterCategory.m_Name, encounterData.m_Name);
							bool flag = true;
							if (m_CompletedEncounters.Contains(encounterIdentifier))
							{
								flag = false;
							}
							if (flag)
							{
								for (int i = 0; i < m_AvailableCoreEncounters.Count; i++)
								{
									if (m_AvailableCoreEncounters[i].m_EncounterDef == encounterIdentifier)
									{
										flag = false;
										break;
									}
								}
								if (flag && Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(encounterIdentifier))
								{
									flag = false;
								}
							}
							if (flag)
							{
								EncounterToSpawn item = new EncounterToSpawn(encounterData, encounterIdentifier);
								m_AvailableCoreEncounters.Insert(0, item);
							}
						}
					}
				}
			}
		}
	}

	private void MarkCoreEncountersAvailable(FactionSubTypes corpType, int grade)
	{
		for (int i = 0; i < Encounters.Count; i++)
		{
			if (corpType != Encounters[i].m_Corp)
			{
				continue;
			}
			if (grade >= Encounters[i].m_Grades.Count)
			{
				break;
			}
			EncounterGrade encounterGrade = Encounters[i].m_Grades[grade];
			EncounterCategory encounterCategory = encounterGrade.m_Categories[0];
			for (int j = 0; j < encounterCategory.m_Encounters.Count; j++)
			{
				EncounterData encounterData = encounterCategory.m_Encounters[j];
				string text = encounterCategory.m_Encounters[j].m_Name;
				if (encounterData != null)
				{
					EncounterIdentifier encounterDef = new EncounterIdentifier(corpType, grade + 1, "core", text);
					EncounterToSpawn item = new EncounterToSpawn(encounterData, encounterDef);
					m_AvailableCoreEncounters.Add(item);
					continue;
				}
				d.LogError(string.Concat("ManEncounter.StartMission Failed for: ", Encounters[i].m_Corp, ", ", encounterGrade.m_Name, ", ", text));
			}
			break;
		}
	}

	private void OnLicenseLevelUp(FactionSubTypes corpType, int grade)
	{
		MarkCoreEncountersAvailable(corpType, grade);
	}

	private void ResetEncounters()
	{
		m_AvailableCoreEncounters.Clear();
		m_CompletedEncounters.Clear();
		m_UseRandomEncounterSpawn = false;
		m_LastTimeEncounterInLog = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
	}

	private void EncounterCompleted(Encounter encounter, bool success)
	{
		EncounterIdentifier encounterDef = encounter.EncounterDef;
		if (success && encounterDef.IsCoreEncounter && !m_CompletedEncounters.Contains(encounterDef))
		{
			m_CompletedEncounters.Add(encounterDef);
		}
	}

	private void EncounterAccepted(EncounterToSpawn encounterToSpawn)
	{
		_ = encounterToSpawn.m_EncounterData;
		for (int i = 0; i < m_AvailableCoreEncounters.Count; i++)
		{
			if (m_AvailableCoreEncounters[i].m_EncounterDef == encounterToSpawn.m_EncounterDef)
			{
				m_AvailableCoreEncounters.RemoveAt(i);
				break;
			}
		}
		m_EncounterSpawnTimer = 0f;
	}

	private void EncounterCancelled(Encounter encounter)
	{
		if (encounter.EncounterDef.IsCoreEncounter)
		{
			EncounterToSpawn item = new EncounterToSpawn(Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounter.EncounterDef), encounter.EncounterDef);
			m_AvailableCoreEncounters.Insert(0, item);
		}
	}

	private void Load(ManEncounter.SaveData data)
	{
		if (data != null)
		{
			for (int i = 0; i < data.m_CoreEncountersToSpawn.Count; i++)
			{
				EncounterToSpawn encounterToSpawn = data.m_CoreEncountersToSpawn[i];
				EncounterData encounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounterToSpawn.m_EncounterDef);
				if (encounterData != null)
				{
					encounterToSpawn.m_EncounterData = encounterData;
					m_AvailableCoreEncounters.Add(encounterToSpawn);
				}
			}
			data.m_CoreEncountersToSpawn.Clear();
			for (int j = 0; j < data.m_AvailableCoreEncounters.Count; j++)
			{
				EncounterToSpawn encounterToSpawn2 = data.m_AvailableCoreEncounters[j];
				EncounterData encounterData2 = Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounterToSpawn2.m_EncounterDef);
				if (encounterData2 != null)
				{
					encounterToSpawn2.m_EncounterData = encounterData2;
					m_AvailableCoreEncounters.Add(encounterToSpawn2);
				}
			}
			data.m_AvailableCoreEncounters.Clear();
			m_EncounterSpawnTimer = data.m_SpawnTimer;
			m_LastTimeEncounterInLog = ((data.m_LastTimeEncounterInLog > 0f) ? data.m_LastTimeEncounterInLog : Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime());
			m_CompletedEncounters = data.m_CompletedEncounters;
			m_UseRandomEncounterSpawn = data.m_UseRandomEncounterSpawn;
			LoadNewRequiredCoreEncounters();
		}
		OnEncountersLoaded.Send();
	}

	private void Save(ManEncounter.SaveData data)
	{
		data.m_UseRandomEncounterSpawn = m_UseRandomEncounterSpawn;
		data.m_AvailableCoreEncounters = m_AvailableCoreEncounters;
		data.m_CompletedEncounters = m_CompletedEncounters;
		data.m_SpawnTimer = m_EncounterSpawnTimer;
		data.m_LastTimeEncounterInLog = m_LastTimeEncounterInLog;
	}

	private void UpdateAutoAcceptEncounterSpawn()
	{
		if (!Singleton.Manager<ManEncounterPlacement>.inst.CanSpawnAutoAcceptEncounter || !ManNetwork.IsHost)
		{
			return;
		}
		for (int i = 0; i < m_AvailableCoreEncounters.Count; i++)
		{
			EncounterToSpawn encounterToSpawn = m_AvailableCoreEncounters[i];
			if (encounterToSpawn.m_EncounterData.m_SpawnWithoutUserAccept && Singleton.Manager<ManEncounterPlacement>.inst.SpawnAutoAcceptEncounter(encounterToSpawn, OnAutoAcceptEncounterSearchComplete))
			{
				break;
			}
		}
	}

	private void OnAutoAcceptEncounterSearchComplete(EncounterToSpawn encounterToSpawn, bool validPos, WorldPosition foundPosition, Quaternion foundRotation)
	{
		if (!validPos)
		{
			return;
		}
		bool flag = true;
		int num = -1;
		for (int i = 0; i < m_AvailableCoreEncounters.Count; i++)
		{
			if (m_AvailableCoreEncounters[i].m_EncounterDef == encounterToSpawn.m_EncounterDef)
			{
				num = i;
				break;
			}
		}
		if (num == -1 && !encounterToSpawn.m_EncounterData.m_HasNoPosition)
		{
			Vector3 gameWorldPosition = foundPosition.GameWorldPosition;
			if (Singleton.Manager<ManWorld>.inst.TryFindNearestVendorPos(gameWorldPosition, out var nearestVendorPosWorld) && (nearestVendorPosWorld - gameWorldPosition).sqrMagnitude < 360000f)
			{
				flag = Singleton.Manager<ManEncounter>.inst.GetNumNearbyEncounters(nearestVendorPosWorld + Singleton.Manager<ManWorld>.inst.GameWorldToScene, 600f, IsHiddenWorldEncounter) < 1;
			}
		}
		if (flag)
		{
			encounterToSpawn.m_Position = foundPosition;
			encounterToSpawn.m_Rotation = foundRotation;
			if (Singleton.Manager<ManEncounter>.inst.SpawnEncounter(encounterToSpawn))
			{
				m_EncounterSpawnTimer = 0f;
				if (num != -1)
				{
					m_AvailableCoreEncounters.RemoveAt(num);
				}
			}
		}
		else
		{
			m_EncounterSpawnTimer *= 0.5f;
		}
	}

	private bool IsHiddenWorldEncounter(Encounter encounter)
	{
		return !encounter.VisibleInLog;
	}

	private void DebugGetAllEncounters(Dictionary<FactionSubTypes, List<EncounterToSpawn>> encountersByCorp)
	{
		encountersByCorp.Clear();
		foreach (CorpEncounters encounter in Encounters)
		{
			List<EncounterToSpawn> list = new List<EncounterToSpawn>(32);
			for (int i = 0; i < encounter.m_Grades.Count; i++)
			{
				EncounterGrade encounterGrade = encounter.m_Grades[i];
				for (int j = 1; j < encounterGrade.m_Categories.Count; j++)
				{
					EncounterCategory encounterCategory = encounterGrade.m_Categories[j];
					foreach (EncounterData encounter2 in encounterCategory.m_Encounters)
					{
						if (!encounter2.m_Name.NullOrEmpty())
						{
							EncounterIdentifier encounterDef = new EncounterIdentifier(encounter.m_Corp, i + 1, encounterCategory.m_Name, encounter2.m_Name);
							EncounterToSpawn item = new EncounterToSpawn(encounter2, encounterDef);
							list.Add(item);
						}
					}
				}
			}
			encountersByCorp.Add(encounter.m_Corp, list);
		}
	}

	private void Awake()
	{
		SetEncounterList(EncounterListType.Singleplayer);
		d.Assert(m_CurrentEncounterList.IsNotNull(), "Default encounter list is null");
	}

	private void Start()
	{
		Singleton.Manager<ManEncounter>.inst.ResetEvent.Subscribe(ResetEncounters);
		Singleton.Manager<ManEncounter>.inst.SaveEvent.Subscribe(Save);
		Singleton.Manager<ManEncounter>.inst.LoadEvent.Subscribe(Load);
		Singleton.Manager<ManEncounter>.inst.EncounterAcceptedEvent.Subscribe(EncounterAccepted);
		Singleton.Manager<ManEncounter>.inst.EncounterCancelledEvent.Subscribe(EncounterCancelled);
		Singleton.Manager<ManEncounter>.inst.EncounterCompletedEvent.Subscribe(EncounterCompleted);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Subscribe(OnLicenseLevelUp);
	}

	private void Update()
	{
		if (Singleton.Manager<ManEncounter>.inst.UpdateEnabled && ManNetwork.IsHost)
		{
			UpdateAutoAcceptEncounterSpawn();
			UpdateFindNearbyMissionGiverEncounter();
			m_EncounterSpawnTimer += Time.deltaTime;
		}
	}
}
