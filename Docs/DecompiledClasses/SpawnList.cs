using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SpawnList
{
	[Serializable]
	public class SpawnItem
	{
		public Transform prefab;

		public TankPreset preset;

		public int m_PresetTeam = -2;

		public ItemTypeInfo m_DispenseItem;

		public string dispenseName;

		public int maxDispense;

		[FormerlySerializedAs("position")]
		public Vector3 worldPosition = Vector3.zero;

		public Vector3 eulerAngles = Vector3.zero;

		public bool m_HideMarker;

		public bool m_DisableOnSwitch;

		public Quaternion rotation => Quaternion.Euler(eulerAngles);

		public string EditorGuiName
		{
			get
			{
				string text = "";
				if ((bool)prefab)
				{
					return prefab.name;
				}
				if ((bool)preset)
				{
					Type itemType = ItemTypeInfo.GetItemType(m_DispenseItem.ObjectType);
					if (itemType != null)
					{
						return Enum.GetName(itemType, m_DispenseItem.ItemType) + " x " + ((maxDispense == 0) ? "∞" : maxDispense.ToString());
					}
					return preset.name;
				}
				return "Type Not Set";
			}
		}

		public SpawnItem()
		{
		}

		public SpawnItem(SpawnItem copy)
		{
			prefab = copy.prefab;
			preset = copy.preset;
			m_DispenseItem = copy.m_DispenseItem;
			dispenseName = copy.dispenseName;
			maxDispense = copy.maxDispense;
			worldPosition = copy.worldPosition;
			eulerAngles = copy.eulerAngles;
			m_HideMarker = copy.m_HideMarker;
		}

		public Transform Spawn(List<Transform> prefabSpawns)
		{
			if (m_DisableOnSwitch && SKU.SwitchUI)
			{
				return null;
			}
			Transform transform = null;
			Vector3 vector = worldPosition + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			if ((bool)prefab)
			{
				TerrainObject component = prefab.GetComponent<TerrainObject>();
				if (component != null)
				{
					component.SpawnFromPrefabAndAddToSaveData(vector, rotation);
				}
				else
				{
					transform = prefab.Spawn(vector, rotation);
					prefabSpawns.Add(transform);
				}
			}
			else if ((bool)preset)
			{
				TechData techDataFormatted = preset.GetTechDataFormatted();
				int num = techDataFormatted.m_BlockSpecs.FindIndex((TankPreset.BlockSpec bs) => bs.GetBlockType() == BlockTypes.GSODispenserMini_111);
				Dictionary<int, Module.SerialData> saveState = new Dictionary<int, Module.SerialData>();
				if (num != -1)
				{
					techDataFormatted.m_BlockSpecs[num] = techDataFormatted.m_BlockSpecs[num].SetSaveState(saveState);
					ModuleItemDispenser.AddSerialData(techDataFormatted.m_BlockSpecs[num], m_DispenseItem, (maxDispense == 0) ? (-1) : maxDispense);
				}
				ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
				{
					techData = techDataFormatted,
					blockIDs = null,
					teamID = m_PresetTeam,
					position = vector,
					rotation = rotation,
					grounded = true,
					hideMarker = m_HideMarker
				};
				TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
				transform = (trackedVisible.visible ? trackedVisible.visible.trans : null);
			}
			return transform;
		}
	}

	public SpawnItem[] items = new SpawnItem[0];

	private List<Transform> m_PrefabSpawns = new List<Transform>();

	public int SpawnListCount => items.Length;

	public int SpawnedUntrackedPrefabCount => m_PrefabSpawns.Count;

	public IEnumerable<Transform> SpawnedUntrackedPrefabs => m_PrefabSpawns;

	public void SpawnAll(List<Transform> outputList = null)
	{
		SpawnItem[] array = items;
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform = array[i].Spawn(m_PrefabSpawns);
			if (outputList != null && transform != null)
			{
				outputList.Add(transform);
			}
		}
	}

	public void RecycleAllPrefabs()
	{
		for (int i = 0; i < m_PrefabSpawns.Count; i++)
		{
			if ((bool)m_PrefabSpawns[i])
			{
				m_PrefabSpawns[i].Recycle();
			}
		}
		m_PrefabSpawns.Clear();
	}
}
