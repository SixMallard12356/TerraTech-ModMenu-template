#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockLimit : UIHUDElement
{
	private struct TechSort
	{
		public int key;

		public UIBlockLimitChunk chunk;
	}

	public enum ShowReason
	{
		BuildBeam,
		InventoryOpen,
		Warning,
		Temporary,
		Tutorial,
		TechManagerOpen
	}

	public enum SortingMode
	{
		NoSort,
		SortToLeft,
		SortToRight
	}

	[SerializeField]
	private RectTransform m_PerTechChunkPrefab;

	[SerializeField]
	private RectTransform m_TechBar;

	[SerializeField]
	private Text m_TotalCountLabel;

	[SerializeField]
	private Text m_TotalCountText;

	[SerializeField]
	private Text m_TotalCountTextOverlay;

	[SerializeField]
	private Color[] m_SectionColours;

	[SerializeField]
	private Color m_PlayerTechColourTint;

	[SerializeField]
	private Color m_AlternateTint;

	[SerializeField]
	private SortingMode m_SortActiveTechToEnd;

	[SerializeField]
	private float m_AutoHideDelay = 2f;

	private Dictionary<int, UIBlockLimitChunk> m_TechSections = new Dictionary<int, UIBlockLimitChunk>();

	private UIBlockLimitChunk m_ActivePlayerSection;

	private int m_MaxWidth;

	private float m_ScaleWidth = 1f;

	private float m_AutoHideTime;

	private Bitfield<ShowReason> m_ShowReasonBitfield = new Bitfield<ShowReason>();

	private bool m_HiddenByOverlap;

	private float m_FlashAnimTimer;

	private bool m_Flashing;

	private int m_TotalCountShown;

	private List<TechSort> techSortList = new List<TechSort>();

	public override void Show(object context)
	{
		base.Show(context);
	}

	public override void Hide(object context)
	{
		m_ShowReasonBitfield.Clear();
		m_AutoHideTime = 0f;
		base.Hide(context);
	}

	private void Clear()
	{
		for (int num = m_TechBar.childCount - 1; num >= 0; num--)
		{
			((RectTransform)m_TechBar.GetChild(num)).Recycle(worldPosStays: false);
		}
		m_TechSections.Clear();
	}

	private void UpdateColour(UIBlockLimitChunk section)
	{
		int siblingIndex = section.transform.GetSiblingIndex();
		Color color = m_SectionColours[(uint)section.TeamColour % m_SectionColours.Length];
		if (section == m_ActivePlayerSection)
		{
			color = Color.Lerp(color, m_PlayerTechColourTint, m_PlayerTechColourTint.a);
		}
		else if (siblingIndex % 2 == 1)
		{
			color = Color.Lerp(color, m_AlternateTint, m_AlternateTint.a);
		}
		color.a = 1f;
		section.SetColour(color);
	}

	private void RemoveTech(int techID, UIBlockLimitChunk section)
	{
		section.transform.Recycle(worldPosStays: false);
		m_TechSections.Remove(techID);
		if (section == m_ActivePlayerSection)
		{
			m_ActivePlayerSection = null;
		}
		foreach (KeyValuePair<int, UIBlockLimitChunk> techSection in m_TechSections)
		{
			UpdateColour(techSection.Value);
		}
	}

	private UIBlockLimitChunk CreateTechSection(int techID)
	{
		UIBlockLimitChunk component = m_PerTechChunkPrefab.SpawnWithLocalTransform(m_TechBar).GetComponent<UIBlockLimitChunk>();
		if (Singleton.playerTank != null && Singleton.playerTank.visible.ID == techID)
		{
			m_ActivePlayerSection = component;
		}
		m_TechSections.Add(techID, component);
		UpdateColour(component);
		SortTechs();
		return component;
	}

	private void ScaleWidthForCost(int newTotalCost)
	{
		float num = (float)m_MaxWidth / (float)Mathf.Max(newTotalCost, Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage);
		if (num == m_ScaleWidth)
		{
			return;
		}
		m_ScaleWidth = num;
		foreach (UIBlockLimitChunk value in m_TechSections.Values)
		{
			value.SetScale(m_ScaleWidth);
		}
	}

	private void OnIDSwappedEvent(int oldID, int newID)
	{
		if (m_TechSections.TryGetValue(oldID, out var value))
		{
			m_TechSections.Remove(oldID);
			m_TechSections[newID] = value;
		}
	}

	private void OnBlockLimitChanged(ManBlockLimiter.CostChangeInfo info)
	{
		if (info.m_TechCategory != ManBlockLimiter.TechCategory.Player)
		{
			return;
		}
		UIBlockLimitChunk value;
		if (info.m_TechCost <= 0)
		{
			if (m_TechSections.TryGetValue(info.m_VisibleID, out value))
			{
				RemoveTech(info.m_VisibleID, value);
				value = null;
			}
		}
		else
		{
			if (!m_TechSections.TryGetValue(info.m_VisibleID, out value))
			{
				value = CreateTechSection(info.m_VisibleID);
			}
			value.SetCost(info.m_TechCost);
			value.SetScale(m_ScaleWidth);
			value.TeamColour = info.m_TeamColour;
		}
		m_Flashing = info.m_CategoryCost > Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage;
		m_TotalCountShown = info.m_CategoryCost;
		UpdateCountText();
		if (value.IsNotNull())
		{
			UpdateColour(value);
			SortTechs();
		}
		ScaleWidthForCost(info.m_CategoryCost);
	}

	private void UpdateCountText()
	{
		if ((bool)m_TotalCountLabel && (bool)m_TotalCountText && (bool)m_TotalCountTextOverlay)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.BlockLimiter, 7);
			string text = m_TotalCountShown.ToString();
			string arg = "<color=#ffffff00>" + text + "</color>";
			m_TotalCountTextOverlay.enabled = m_Flashing;
			m_TotalCountText.enabled = m_Flashing;
			if (m_Flashing)
			{
				int num = localisedString.IndexOf("{0}");
				d.Assert(num >= 0);
				m_TotalCountLabel.text = string.Format(localisedString.Substring(0, num), text, Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage);
				m_TotalCountText.text = string.Format(localisedString.Substring(num), arg, Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage);
				m_TotalCountTextOverlay.text = text;
			}
			else
			{
				m_TotalCountLabel.text = string.Format(localisedString, text, Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage);
			}
		}
	}

	private void SortTechs()
	{
		foreach (KeyValuePair<int, UIBlockLimitChunk> techSection in m_TechSections)
		{
			int key = techSection.Value.TeamColour;
			if (m_SortActiveTechToEnd != SortingMode.NoSort && m_ActivePlayerSection != null)
			{
				if (techSection.Value == m_ActivePlayerSection)
				{
					key = -2;
				}
				else if (techSection.Value.TeamColour == m_ActivePlayerSection.TeamColour)
				{
					key = -1;
				}
			}
			techSortList.Add(new TechSort
			{
				key = key,
				chunk = techSection.Value
			});
		}
		if (m_SortActiveTechToEnd != SortingMode.SortToRight)
		{
			techSortList.Sort((TechSort a, TechSort b) => a.key - b.key);
		}
		else
		{
			techSortList.Sort((TechSort a, TechSort b) => b.key - a.key);
		}
		bool flag = false;
		for (int num = 0; num < techSortList.Count; num++)
		{
			if (num != techSortList[num].chunk.transform.GetSiblingIndex())
			{
				techSortList[num].chunk.transform.SetSiblingIndex(num);
				flag = true;
			}
		}
		if (flag)
		{
			foreach (KeyValuePair<int, UIBlockLimitChunk> techSection2 in m_TechSections)
			{
				UpdateColour(techSection2.Value);
			}
		}
		techSortList.Clear();
	}

	private void OnPlayerTechChanged(Tank tank, bool select)
	{
		if (m_TechSections.TryGetValue(tank.visible.ID, out var value))
		{
			if (select)
			{
				m_ActivePlayerSection = value;
			}
			else if (m_ActivePlayerSection == value)
			{
				m_ActivePlayerSection = null;
			}
			UpdateColour(value);
			SortTechs();
		}
	}

	private void OnLanguageChanged()
	{
		UpdateCountText();
	}

	public static void ShowUI(ShowReason reason)
	{
		UIBlockLimit uIBlockLimit = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockLimit) as UIBlockLimit;
		if (uIBlockLimit != null && Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			uIBlockLimit.m_ShowReasonBitfield.Set((int)reason, enabled: true);
			if (!uIBlockLimit.IsVisible && !uIBlockLimit.m_HiddenByOverlap)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockLimit);
			}
			if (reason == ShowReason.Temporary)
			{
				uIBlockLimit.m_AutoHideTime = Time.time + uIBlockLimit.m_AutoHideDelay;
				return;
			}
			uIBlockLimit.m_ShowReasonBitfield.Set(3, enabled: false);
			uIBlockLimit.m_AutoHideTime = 0f;
		}
	}

	public static void HideUI(ShowReason reason)
	{
		UIBlockLimit uIBlockLimit = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockLimit) as UIBlockLimit;
		if (!(uIBlockLimit != null) || !uIBlockLimit.m_ShowReasonBitfield.Contains((int)reason))
		{
			return;
		}
		bool flag = reason == ShowReason.Warning;
		uIBlockLimit.m_ShowReasonBitfield.Set((int)reason, enabled: false);
		if (!uIBlockLimit.m_ShowReasonBitfield.AnySet && !uIBlockLimit.m_HiddenByOverlap)
		{
			if (flag && uIBlockLimit.m_AutoHideDelay > 0f)
			{
				uIBlockLimit.m_ShowReasonBitfield.Set(3, enabled: true);
				uIBlockLimit.m_AutoHideTime = Time.time + uIBlockLimit.m_AutoHideDelay;
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockLimit);
			}
		}
	}

	public static void SetHiddenByOverlappingUI(bool hide)
	{
		UIBlockLimit uIBlockLimit = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockLimit) as UIBlockLimit;
		if (uIBlockLimit != null && uIBlockLimit.m_HiddenByOverlap != hide)
		{
			uIBlockLimit.m_HiddenByOverlap = hide;
			if (hide)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockLimit);
			}
			else if (uIBlockLimit.m_ShowReasonBitfield.AnySet)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockLimit);
			}
		}
	}

	private void OnPool()
	{
		m_PerTechChunkPrefab.CreatePool(5);
		m_MaxWidth = (int)m_TechBar.rect.width;
	}

	private void OnSpawn()
	{
		Clear();
		Singleton.Manager<ManBlockLimiter>.inst.CostChangedEvent.Subscribe(OnBlockLimitChanged);
		Singleton.Manager<ManBlockLimiter>.inst.IDSwappedEvent.Subscribe(OnIDSwappedEvent);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTechChanged);
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManBlockLimiter>.inst.CostChangedEvent.Unsubscribe(OnBlockLimitChanged);
		Singleton.Manager<ManBlockLimiter>.inst.IDSwappedEvent.Unsubscribe(OnIDSwappedEvent);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTechChanged);
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChanged);
		Clear();
	}

	private void Update()
	{
		if (m_AutoHideTime > 0f && Time.time > m_AutoHideTime)
		{
			m_AutoHideTime = 0f;
			HideUI(ShowReason.Temporary);
		}
		if (m_Flashing && m_TotalCountTextOverlay != null)
		{
			m_FlashAnimTimer = (m_FlashAnimTimer + Time.deltaTime) % 1f;
			Color color = m_TotalCountTextOverlay.color;
			color.a = Mathf.Sin(m_FlashAnimTimer * (float)Math.PI * 2f) * 0.5f + 0.5f;
			m_TotalCountTextOverlay.color = color;
		}
	}
}
