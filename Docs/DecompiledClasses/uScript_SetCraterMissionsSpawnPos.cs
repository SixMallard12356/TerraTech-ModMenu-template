#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class uScript_SetCraterMissionsSpawnPos : uScriptLogic
{
	[Serializable]
	public struct CraterSpawnData
	{
		public FactionSubTypes m_Corp;

		public int m_Grade;

		public string m_Category;

		public string m_EncounterName;

		public List<Biome> m_PreferedBiomes;

		public SceneryTypes[] m_NearbyScenery;

		public float m_MinDistFromStart;

		public float m_MaxDistFromStart;
	}

	private class PossibleLocations
	{
		public WorldPosition m_Position;

		public float m_DistFromCenter;

		public ManWorld.CachedBiomeBlendWeights m_BiomeWeights;

		public PossibleLocations(WorldPosition position, float distance, ManWorld.CachedBiomeBlendWeights weights)
		{
			m_Position = position;
			m_DistFromCenter = distance;
			m_BiomeWeights = weights;
		}
	}

	private class FoundLocations
	{
		public List<PossibleLocations> m_MatchingAll = new List<PossibleLocations>();

		public List<PossibleLocations> m_MatchingLocation = new List<PossibleLocations>();

		public List<PossibleLocations> m_MatchingBiome = new List<PossibleLocations>();

		public List<PossibleLocations> m_MatchingScenery = new List<PossibleLocations>();

		public void AddLocation(PossibleLocations location, bool correctBiome, bool correctScenery)
		{
			m_MatchingLocation.Add(location);
			if (correctBiome)
			{
				m_MatchingBiome.Add(location);
				if (correctScenery)
				{
					m_MatchingAll.Add(location);
				}
			}
			if (correctScenery)
			{
				m_MatchingScenery.Add(location);
			}
		}

		public List<PossibleLocations> GetBestList()
		{
			if (m_MatchingAll.Count > 0)
			{
				return m_MatchingAll;
			}
			if (m_MatchingBiome.Count > 0)
			{
				return m_MatchingBiome;
			}
			if (m_MatchingScenery.Count > 0)
			{
				return m_MatchingScenery;
			}
			if (m_MatchingLocation.Count > 0)
			{
				return m_MatchingLocation;
			}
			return null;
		}
	}

	private class SearchPair
	{
		public float m_Dist;

		public int m_Index;

		public SearchPair(float distance, int index)
		{
			m_Dist = distance;
			m_Index = index;
		}
	}

	private int m_SpawnIndex;

	private int m_RetryIndex;

	private bool m_FoundPositions;

	private bool m_LookingForSpace;

	private FreeSpaceFinder m_FreeSpaceFinder;

	private EncounterIdentifier m_CurrentIdent;

	private List<WorldPosition> m_SpawnPositions = new List<WorldPosition>();

	private bool m_InitialPositionsFound;

	private FoundLocations[] m_ValidLocations;

	private readonly float m_MinBiomeWeight = 0.8f;

	private readonly Bitfield<ObjectTypes> kSceneryBitfield = new Bitfield<ObjectTypes>(3);

	public bool Finished => m_FoundPositions;

	public void In(CraterSpawnData[] spawnData)
	{
		if (!m_InitialPositionsFound)
		{
			int num = int.MaxValue;
			int num2 = int.MinValue;
			for (int i = 0; i < spawnData.Length; i++)
			{
				num = (int)Mathf.Min(num, spawnData[i].m_MinDistFromStart);
				num2 = (int)Mathf.Max(num2, spawnData[i].m_MaxDistFromStart);
			}
			List<PossibleLocations> possibleSpawnLocations = GetPossibleSpawnLocations(Mode<ModeMain>.inst.StartPositionScene, num, num2);
			m_ValidLocations = new FoundLocations[spawnData.Length];
			for (int j = 0; j < spawnData.Length; j++)
			{
				CraterSpawnData craterSpawnData = spawnData[j];
				m_ValidLocations[j] = new FoundLocations();
				for (int k = 0; k < possibleSpawnLocations.Count; k++)
				{
					PossibleLocations possibleLocations = possibleSpawnLocations[k];
					if (!(possibleLocations.m_DistFromCenter <= craterSpawnData.m_MaxDistFromStart) || !(possibleLocations.m_DistFromCenter >= craterSpawnData.m_MinDistFromStart))
					{
						continue;
					}
					bool correctScenery = true;
					if (craterSpawnData.m_NearbyScenery.Length != 0)
					{
						correctScenery = false;
						foreach (Visible item2 in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(possibleLocations.m_Position.ScenePosition, 10f, kSceneryBitfield))
						{
							SceneryTypes[] nearbyScenery = craterSpawnData.m_NearbyScenery;
							for (int l = 0; l < nearbyScenery.Length; l++)
							{
								if (nearbyScenery[l] == (SceneryTypes)item2.ItemType)
								{
									correctScenery = true;
									break;
								}
							}
						}
					}
					bool correctBiome = CheckWeights(craterSpawnData.m_PreferedBiomes, possibleLocations.m_BiomeWeights);
					m_ValidLocations[j].AddLocation(possibleLocations, correctBiome, correctScenery);
				}
			}
			m_InitialPositionsFound = true;
		}
		else
		{
			if (m_FoundPositions || m_LookingForSpace)
			{
				return;
			}
			if (m_SpawnIndex < spawnData.Length)
			{
				List<PossibleLocations> bestList = m_ValidLocations[m_SpawnIndex].GetBestList();
				if (bestList != null && bestList.Count > 0)
				{
					int num3 = -1;
					bool flag = false;
					if (m_SpawnIndex == 0)
					{
						num3 = UnityEngine.Random.Range(0, bestList.Count);
					}
					else
					{
						List<SearchPair> list = new List<SearchPair>();
						for (int m = 0; m < bestList.Count; m++)
						{
							float num4 = float.MaxValue;
							for (int n = 0; n < m_SpawnPositions.Count; n++)
							{
								float sqrMagnitude = (m_SpawnPositions[n].ScenePosition - bestList[m].m_Position.ScenePosition).sqrMagnitude;
								num4 = Mathf.Min(num4, sqrMagnitude);
							}
							SearchPair item = new SearchPair(num4, m);
							list.Add(item);
						}
						list.Sort(SearchPairComparer);
						while (m_RetryIndex < list.Count)
						{
							int index = list[m_RetryIndex].m_Index;
							Vector3 scenePosition = bestList[index].m_Position.ScenePosition;
							if (Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(scenePosition))
							{
								num3 = index;
								break;
							}
							m_RetryIndex++;
						}
						if (num3 == -1)
						{
							num3 = list[0].m_Index;
							flag = true;
						}
					}
					m_CurrentIdent = new EncounterIdentifier(spawnData[m_SpawnIndex].m_Corp, spawnData[m_SpawnIndex].m_Grade, spawnData[m_SpawnIndex].m_Category, spawnData[m_SpawnIndex].m_EncounterName);
					if (num3 != -1 && !flag)
					{
						EncounterData encounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(m_CurrentIdent);
						if (encounterData != null)
						{
							ManFreeSpace.FreeSpaceParams freeParams = new ManFreeSpace.FreeSpaceParams
							{
								m_DebugName = encounterData.m_Name,
								m_ObjectsToAvoid = new Bitfield<ObjectTypes>(new ObjectTypes[3]
								{
									ObjectTypes.Scenery,
									ObjectTypes.Vehicle,
									ObjectTypes.Block
								}),
								m_CircleRadius = encounterData.m_EncounterPrefab.SpawnRadius,
								m_CenterPosWorld = bestList[num3].m_Position,
								m_CircleIndex = 0,
								m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
								m_CheckSafeArea = false,
								m_RejectFunc = null,
								m_SilentFailIfNoSpaceFound = true
							};
							m_LookingForSpace = true;
							if (m_FreeSpaceFinder == null)
							{
								m_FreeSpaceFinder = new FreeSpaceFinder();
								m_FreeSpaceFinder.FreeSpaceFoundEvent.Subscribe(OnSpaceFound);
							}
							m_FreeSpaceFinder.Setup(freeParams, "SetCraterMissionsSpawnPos script", autoRetry: false);
						}
						else
						{
							d.Log("uScript_SetCraterMissionsSpawnPos - Encounter Data for mission " + m_CurrentIdent.m_Name + "is null");
							m_SpawnIndex++;
						}
					}
					else
					{
						if (flag)
						{
							Singleton.Manager<ManProgression>.inst.SetCoreEncounterSpawnPos(m_CurrentIdent, bestList[num3].m_Position);
							m_SpawnPositions.Add(bestList[num3].m_Position);
							d.LogError("uScript_SetCraterMissionsSpawnPos - Setting mission " + m_CurrentIdent.m_Name + " at fallback spawn pos: " + bestList[num3].m_Position.ScenePosition);
						}
						else
						{
							d.LogError("uScript_SetCraterMissionsSpawnPos - No position found for mission " + m_CurrentIdent.m_Name);
						}
						m_SpawnIndex++;
					}
				}
				else
				{
					d.LogError("uScript_SetCraterMissionsSpawnPos - No Locations Found to search for free space for " + m_CurrentIdent.m_Name);
				}
			}
			else
			{
				m_FoundPositions = true;
			}
		}
	}

	private static int SearchPairComparer(SearchPair a, SearchPair b)
	{
		if (a.m_Dist < b.m_Dist)
		{
			return 1;
		}
		if (a.m_Dist > b.m_Dist)
		{
			return -1;
		}
		return 0;
	}

	public void OnEnable()
	{
		m_SpawnPositions.Clear();
		m_InitialPositionsFound = false;
		m_FoundPositions = false;
		m_LookingForSpace = false;
		m_SpawnIndex = 0;
		m_RetryIndex = 0;
	}

	private List<PossibleLocations> GetPossibleSpawnLocations(Vector3 scenePos, int minRadius, int maxRadius)
	{
		int num = -maxRadius;
		int num2 = 10;
		float y = UnityEngine.Random.Range(0f, 360f);
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		List<PossibleLocations> list = new List<PossibleLocations>();
		for (int i = num; i < maxRadius; i += num2)
		{
			for (int j = num; j < maxRadius; j += num2)
			{
				Vector3 vector = new Vector3(i, 0f, j);
				float magnitude = vector.magnitude;
				if (magnitude >= (float)minRadius && magnitude <= (float)maxRadius)
				{
					Vector3 scenePos2 = scenePos + quaternion * vector;
					scenePos2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos2);
					ManWorld.CachedBiomeBlendWeights biomeWeightsAtScenePosition = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos2);
					PossibleLocations item = new PossibleLocations(WorldPosition.FromScenePosition(in scenePos2), magnitude, biomeWeightsAtScenePosition);
					list.Add(item);
				}
			}
		}
		return list;
	}

	private bool CheckWeights(List<Biome> preferredBiomes, ManWorld.CachedBiomeBlendWeights weights)
	{
		bool result = false;
		for (int i = 0; i < weights.NumWeights; i++)
		{
			if (preferredBiomes.Contains(weights.Biome(i)) && weights.Weight(i) > m_MinBiomeWeight)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void OnSpaceFound(WorldPosition? position)
	{
		if (position.HasValue)
		{
			Singleton.Manager<ManProgression>.inst.SetCoreEncounterSpawnPos(m_CurrentIdent, position.Value);
			m_SpawnPositions.Add(position.Value);
			m_SpawnIndex++;
			m_RetryIndex = 0;
		}
		else
		{
			d.Log("uScript_SetCraterMissionsSpawnPos - No space found for crater " + m_CurrentIdent.m_Name + ". Try Number: " + m_RetryIndex + " try again!");
			m_RetryIndex++;
		}
		m_LookingForSpace = false;
	}
}
