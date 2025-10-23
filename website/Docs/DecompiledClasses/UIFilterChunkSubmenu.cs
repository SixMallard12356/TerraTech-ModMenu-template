#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFilterChunkSubmenu : RadialMenuSubmenu
{
	private struct ChunkChoice
	{
		public Transform transform;

		public QueryableSelectable selectable;

		public ItemTypeInfo itemType;
	}

	[Serializable]
	private struct ChunkDisplayCategories
	{
		public RectTransform raw;

		public RectTransform refined;

		public RectTransform componentT1;

		public RectTransform componentT2;

		public RectTransform componentT3;

		public RectTransform componentT4;
	}

	[SerializeField]
	private ChunkDisplayCategories m_SpecificChunkParentContainers;

	public Event<ItemTypeInfo> OnItemSelected;

	private List<ChunkChoice> m_SpawnedChunkObjects;

	private int m_LastSelectedIndex;

	public void AddItem(Transform itemPrefab, ItemTypeInfo itemType)
	{
		if (m_SpawnedChunkObjects == null)
		{
			m_SpawnedChunkObjects = new List<ChunkChoice>();
		}
		Transform transform = itemPrefab.Spawn();
		RectTransform chunkDisplayParent = GetChunkDisplayParent(itemType);
		transform.SetParent(chunkDisplayParent, worldPositionStays: false);
		transform.GetComponent<UIItemDisplay>().Setup(itemType);
		QueryableSelectable component = transform.GetComponent<QueryableSelectable>();
		if ((bool)component)
		{
			Navigation navigation = component.navigation;
			navigation.mode = Navigation.Mode.Automatic;
			component.navigation = navigation;
		}
		ChunkChoice item = new ChunkChoice
		{
			transform = transform,
			selectable = transform.GetComponent<QueryableSelectable>(),
			itemType = itemType
		};
		m_SpawnedChunkObjects.Add(item);
	}

	private RectTransform GetChunkDisplayParent(ItemTypeInfo chunkTypeInfo)
	{
		RectTransform result = m_SpecificChunkParentContainers.raw;
		int hashCode = chunkTypeInfo.GetHashCode();
		ChunkCategory descriptorFlags = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode);
		if ((descriptorFlags & ChunkCategory.Raw) != ChunkCategory.Null)
		{
			result = m_SpecificChunkParentContainers.raw;
		}
		else if ((descriptorFlags & ChunkCategory.Refined) != ChunkCategory.Null)
		{
			result = m_SpecificChunkParentContainers.refined;
		}
		else if ((descriptorFlags & ChunkCategory.Component) != ChunkCategory.Null)
		{
			switch ((ComponentTier)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(hashCode))
			{
			case ComponentTier.Simple:
				result = m_SpecificChunkParentContainers.componentT1;
				break;
			case ComponentTier.Advanced:
				result = m_SpecificChunkParentContainers.componentT2;
				break;
			case ComponentTier.Complex:
				result = m_SpecificChunkParentContainers.componentT3;
				break;
			case ComponentTier.Exotic:
				result = m_SpecificChunkParentContainers.componentT4;
				break;
			default:
				d.LogError("UIFilterMenu.GetChunkDisplayParent - Unhandled Component Tier for chunk " + chunkTypeInfo);
				result = m_SpecificChunkParentContainers.componentT1;
				break;
			}
		}
		else
		{
			d.LogError("UIFilterMenu.GetChunkDisplayParent - Unhandled ChunkCategory for chunk " + chunkTypeInfo);
		}
		return result;
	}

	public void OnRecycle()
	{
		foreach (ChunkChoice spawnedChunkObject in m_SpawnedChunkObjects)
		{
			spawnedChunkObject.transform.Recycle();
		}
		m_SpawnedChunkObjects.Clear();
		m_SpawnedChunkObjects = null;
	}

	private void CheckForSelectedItem()
	{
		ItemTypeInfo itemTypeInfo = null;
		for (int i = 0; i < m_SpawnedChunkObjects.Count; i++)
		{
			if (m_SpawnedChunkObjects[i].selectable.IsHighlighted)
			{
				itemTypeInfo = m_SpawnedChunkObjects[i].itemType;
				break;
			}
		}
		if (itemTypeInfo != null)
		{
			OnItemSelected.Send(itemTypeInfo);
		}
	}

	protected override void OnOpen()
	{
		m_LastSelectedIndex = -1;
		if (base.m_Controller.IsGamePad())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_SpawnedChunkObjects[0].selectable.gameObject);
		}
	}

	protected override void OnClose()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_SpawnedChunkObjects[0].selectable.gameObject);
	}

	protected override void OnUpdate()
	{
		if (!base.m_Controller.IsGamePad())
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < m_SpawnedChunkObjects.Count; i++)
		{
			if (m_SpawnedChunkObjects[i].selectable.IsHighlighted)
			{
				num = i;
				break;
			}
		}
		if (num != m_LastSelectedIndex)
		{
			if (m_LastSelectedIndex != -1)
			{
				m_SpawnedChunkObjects[m_LastSelectedIndex].selectable.GetComponent<TooltipComponent>().OnPointerExit(null);
			}
			if (num != -1)
			{
				m_SpawnedChunkObjects[num].selectable.GetComponent<TooltipComponent>().OnPointerEnter(null);
			}
			m_LastSelectedIndex = num;
		}
	}

	protected override void OnOptionSelected(QueryableSelectable option)
	{
		CheckForSelectedItem();
		base.OnOptionSelected(option);
	}
}
