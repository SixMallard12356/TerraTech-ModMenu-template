#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UICorpToggles
{
	private struct CachedCorpSkinInfo
	{
		public bool showCorp;

		public int currentCorpSkin;
	}

	[SerializeField]
	private UICorpToggle m_CorpTogglePrefab;

	[SerializeField]
	private Toggle m_AllCorpsToggle;

	[SerializeField]
	private HorizontalOrVerticalLayoutGroup m_CorpLayoutGroup;

	[SerializeField]
	private Transform m_CorpSkinImagePrefab;

	[SerializeField]
	private Transform m_CorpSkinImageParent;

	[SerializeField]
	private GameObject m_ModdedCorpsSection;

	[SerializeField]
	private GameObject m_ModdedCorpsFoldout;

	[SerializeField]
	private UICorpToggle m_ModdedCorpButtonPrefab;

	[SerializeField]
	private GameObject m_ModdedCorpFilterHolder;

	public EventNoParams OnChanged;

	private int m_NumModdedCorps;

	private UITogglesController m_Controller = new UITogglesController();

	private Dictionary<FactionSubTypes, UICorpToggle> m_SpawnedToggles;

	private List<Transform> m_SpawnedCorpSkinImages = new List<Transform>();

	private Dictionary<int, CachedCorpSkinInfo> m_CachedCorpSkinInfo = new Dictionary<int, CachedCorpSkinInfo>();

	public List<int> Selection => m_Controller.Selection;

	public void Setup(CorporationOrder optionalOrder)
	{
		m_Controller.OnChanged.Unsubscribe(OnControllerChanged);
		m_Controller.OnChanged.Subscribe(OnControllerChanged);
		List<FactionSubTypes> list = Singleton.Manager<ManPurchases>.inst.AvailableCorporations;
		if (optionalOrder != null)
		{
			list = new List<FactionSubTypes>(Singleton.Manager<ManPurchases>.inst.AvailableCorporations);
			list.Sort(delegate(FactionSubTypes itemA, FactionSubTypes itemB)
			{
				optionalOrder.Lookup(itemA, out var order);
				optionalOrder.Lookup(itemB, out var order2);
				return order.CompareTo(order2);
			});
		}
		int count = list.Count;
		if (m_SpawnedToggles != null)
		{
			foreach (UICorpToggle value in m_SpawnedToggles.Values)
			{
				UnityEngine.Object.Destroy(value.gameObject);
			}
		}
		m_SpawnedToggles = new Dictionary<FactionSubTypes, UICorpToggle>(default(FactionSubTypesComparer));
		m_Controller.Clear();
		m_NumModdedCorps = 0;
		for (int num = 0; num < count; num++)
		{
			FactionSubTypes factionSubTypes = list[num];
			if (Singleton.Manager<ManMods>.inst.IsModdedCorp(factionSubTypes))
			{
				if (m_ModdedCorpButtonPrefab != null)
				{
					m_NumModdedCorps++;
					UICorpToggle uICorpToggle = m_ModdedCorpButtonPrefab.Spawn(m_ModdedCorpFilterHolder.transform);
					Transform transform = uICorpToggle.transform;
					transform.localPosition = transform.localPosition.SetZ(0f);
					transform.localScale = Vector3.one;
					uICorpToggle.SetCorp(factionSubTypes);
					m_SpawnedToggles[factionSubTypes] = uICorpToggle;
					if ((bool)m_CorpSkinImagePrefab && (bool)m_CorpSkinImageParent)
					{
						Transform transform2 = m_CorpSkinImagePrefab.Spawn(m_CorpSkinImageParent);
						transform2.localPosition = transform.localPosition.SetZ(0f);
						transform2.localScale = Vector3.one;
						m_SpawnedCorpSkinImages.Add(transform2);
						m_CachedCorpSkinInfo[(int)factionSubTypes] = new CachedCorpSkinInfo
						{
							showCorp = false,
							currentCorpSkin = -1
						};
					}
					m_Controller.AddToggle(uICorpToggle.Toggle, (int)factionSubTypes);
					uICorpToggle.Toggle.interactable = true;
				}
			}
			else
			{
				UICorpToggle uICorpToggle2 = m_CorpTogglePrefab.Spawn(m_CorpLayoutGroup.transform);
				Transform transform3 = uICorpToggle2.transform;
				transform3.localPosition = transform3.localPosition.SetZ(0f);
				transform3.localScale = Vector3.one;
				uICorpToggle2.SetCorp(factionSubTypes);
				m_SpawnedToggles[factionSubTypes] = uICorpToggle2;
				if ((bool)m_CorpSkinImagePrefab && (bool)m_CorpSkinImageParent)
				{
					Transform transform4 = m_CorpSkinImagePrefab.Spawn(m_CorpSkinImageParent);
					transform4.localPosition = transform3.localPosition.SetZ(0f);
					transform4.localScale = Vector3.one;
					m_SpawnedCorpSkinImages.Add(transform4);
					m_CachedCorpSkinInfo[(int)factionSubTypes] = new CachedCorpSkinInfo
					{
						showCorp = false,
						currentCorpSkin = -1
					};
				}
				m_Controller.AddToggle(uICorpToggle2.Toggle, (int)factionSubTypes);
				uICorpToggle2.Toggle.interactable = true;
			}
		}
		LayoutElement component = m_CorpTogglePrefab.GetComponent<LayoutElement>();
		HorizontalLayoutGroup component2 = m_CorpLayoutGroup.GetComponent<HorizontalLayoutGroup>();
		if ((bool)component && (bool)component2)
		{
			float num2 = component2.preferredWidth - component.preferredWidth * (float)count;
			m_CorpLayoutGroup.spacing = num2 / (float)(count + 1);
		}
		if (m_ModdedCorpsSection != null)
		{
			m_ModdedCorpsSection.SetActive(m_NumModdedCorps > 0);
		}
		else
		{
			d.LogWarning("Modded corps buttons missing");
		}
		if (m_ModdedCorpsFoldout != null)
		{
			m_ModdedCorpsFoldout.SetActive(value: false);
		}
		if ((bool)m_AllCorpsToggle)
		{
			m_Controller.SetAllToggle(m_AllCorpsToggle);
		}
	}

	public void ToggleModdedCorpsPanel()
	{
		if (m_NumModdedCorps > 0)
		{
			m_ModdedCorpsFoldout.SetActive(!m_ModdedCorpsFoldout.activeSelf);
		}
	}

	public void UpdateMiniPalette()
	{
		if (!m_CorpSkinImagePrefab.IsNotNull() || !m_CorpSkinImageParent.IsNotNull())
		{
			return;
		}
		List<FactionSubTypes> availableCorporations = Singleton.Manager<ManPurchases>.inst.AvailableCorporations;
		for (int i = 0; i < availableCorporations.Count; i++)
		{
			FactionSubTypes factionSubTypes = availableCorporations[i];
			bool flag = Singleton.Manager<ManCustomSkins>.inst.ShowCorpInUI(factionSubTypes);
			int currentSelectedSkinInCorp = Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedSkinInCorp(factionSubTypes);
			if ((m_CachedCorpSkinInfo.TryGetValue((int)factionSubTypes, out var value) && currentSelectedSkinInCorp != value.currentCorpSkin) || flag != value.showCorp)
			{
				m_SpawnedCorpSkinImages[i].gameObject.SetActive(flag);
				Image component = m_SpawnedCorpSkinImages[i].GetComponent<Image>();
				if (component.IsNotNull())
				{
					component.sprite = Singleton.Manager<ManCustomSkins>.inst.GetCorpSkinUIInfos(factionSubTypes)[currentSelectedSkinInCorp].m_SkinMiniPaletteImage;
				}
				value.showCorp = flag;
				value.currentCorpSkin = currentSelectedSkinInCorp;
				m_CachedCorpSkinInfo[(int)factionSubTypes] = value;
			}
		}
	}

	public void TakeDown()
	{
		m_Controller.Clear();
		m_Controller.OnChanged.Unsubscribe(OnControllerChanged);
		if (m_SpawnedToggles != null)
		{
			foreach (UICorpToggle value in m_SpawnedToggles.Values)
			{
				value.transform.SetParent(null, worldPositionStays: false);
				value.Recycle();
			}
			m_SpawnedToggles.Clear();
		}
		if (m_SpawnedCorpSkinImages != null)
		{
			for (int i = 0; i < m_SpawnedCorpSkinImages.Count; i++)
			{
				m_SpawnedCorpSkinImages[i].SetParent(null, worldPositionStays: false);
				m_SpawnedCorpSkinImages[i].Recycle();
			}
			m_SpawnedCorpSkinImages.Clear();
		}
	}

	public void ToggleAllOn()
	{
		m_Controller.SetAllToggleSelected(selected: true);
	}

	public void CycleSingleToggle(bool forward = true)
	{
		m_Controller.CycleSingleToggle(forward);
	}

	public void SetToggleSelected(int selectionIndex, bool selected)
	{
		m_Controller.SetToggleSelected(selectionIndex, selected);
	}

	private void OnControllerChanged()
	{
		OnChanged.Send();
	}
}
