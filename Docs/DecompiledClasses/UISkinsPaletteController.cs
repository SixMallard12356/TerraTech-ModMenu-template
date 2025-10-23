#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UISkinsPaletteHUD))]
public class UISkinsPaletteController : MonoBehaviour, IMoveHandler, IEventSystemHandler, ISubmitHandler
{
	public enum SkinEditType
	{
		Off,
		Paint,
		Fill
	}

	private UISkinsPaletteHUD m_Palette;

	private ManPointer.BuildingMode m_ModeBeforeAlt;

	private float m_LastAltPressTime;

	private bool m_BuySelected;

	public FactionSubTypes GetCurrentSelectedCorp()
	{
		return Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp();
	}

	public void SetCurrentSelectedCorp(FactionSubTypes corp)
	{
		Singleton.Manager<ManCustomSkins>.inst.SetCurrentSelectedCorp(corp);
		List<CorporationSkinUIInfo> corpSkinUIInfos = Singleton.Manager<ManCustomSkins>.inst.GetCorpSkinUIInfos(corp);
		m_Palette.SetupSkinButtons(corpSkinUIInfos);
		m_Palette.SetSelectedSkin(corpSkinUIInfos[GetSelectedSkinInCurrentCorp()], corp);
		m_Palette.SelectSkinButton(GetSelectedSkinInCurrentCorp(), m_BuySelected);
	}

	public int GetCurrentSelectedSkinInCorp(FactionSubTypes corp)
	{
		return Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedSkinInCorp(corp);
	}

	public int GetSelectedSkinInCurrentCorp()
	{
		return GetCurrentSelectedSkinInCorp(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
	}

	public void SetSelectedSkinForCurrentCorp(int skin)
	{
		SetSelectedSkinForCorp(skin, Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
	}

	public void SetSelectedSkinForCorp(int skin, FactionSubTypes corp)
	{
		Singleton.Manager<ManCustomSkins>.inst.SetSelectedSkinForCorp(skin, corp);
		if (m_Palette.IsNotNull())
		{
			m_Palette.SetSelectedSkin(Singleton.Manager<ManCustomSkins>.inst.GetSkinUIInfo(corp, GetCurrentSelectedSkinInCorp(corp)), corp);
		}
	}

	public void ChangeSelectedCorporation(bool next)
	{
		m_BuySelected = false;
		Singleton.Manager<ManCustomSkins>.inst.ChangeSelectedCorporation(next);
		List<CorporationSkinUIInfo> corpSkinUIInfos = Singleton.Manager<ManCustomSkins>.inst.GetCorpSkinUIInfos(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
		m_Palette.SetupSkinButtons(corpSkinUIInfos);
		m_Palette.SetSelectedSkin(corpSkinUIInfos[GetSelectedSkinInCurrentCorp()], Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
		m_Palette.SelectCorpButton(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
		m_Palette.SelectSkinButton(GetSelectedSkinInCurrentCorp(), m_BuySelected);
	}

	public void ChangeSkinInSelectedCorp(bool next)
	{
		if (m_BuySelected && !next)
		{
			m_BuySelected = false;
		}
		else
		{
			bool flag = Singleton.Manager<ManCustomSkins>.inst.ChangeSkinInSelectedCorp(next);
			m_BuySelected = next && !flag && Singleton.Manager<ManDLC>.inst.SupportsStore();
		}
		if (m_Palette.IsNotNull())
		{
			int selectedSkinInCurrentCorp = GetSelectedSkinInCurrentCorp();
			m_Palette.SetSelectedSkin(Singleton.Manager<ManCustomSkins>.inst.GetSkinUIInfo(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp(), selectedSkinInCurrentCorp), Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
			bool buySelected = m_BuySelected;
			m_Palette.SelectSkinButton(selectedSkinInCurrentCorp, buySelected);
			m_Palette.TryShowSkinButton(selectedSkinInCurrentCorp, buySelected);
		}
	}

	public void SetSkinEditType(SkinEditType editType)
	{
		switch (editType)
		{
		case SkinEditType.Off:
			Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.Grab);
			break;
		case SkinEditType.Paint:
			Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.PaintSkin);
			break;
		case SkinEditType.Fill:
			Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.PaintSkinTech);
			break;
		default:
			d.LogError($"Unknown skin edit type {editType}!");
			break;
		}
	}

	public bool ShowCorpInUI(FactionSubTypes corp)
	{
		return Singleton.Manager<ManCustomSkins>.inst.ShowCorpInUI(corp);
	}

	public void OnMove(AxisEventData eventData)
	{
		if (!Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButton(66))
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Up:
				ChangeSelectedCorporation(next: false);
				break;
			case MoveDirection.Down:
				ChangeSelectedCorporation(next: true);
				break;
			default:
				d.LogErrorFormat("Unhandled Move Dir in UIPaletteBlockSelect {0}", eventData.moveDir);
				break;
			case MoveDirection.Left:
			case MoveDirection.Right:
			case MoveDirection.None:
				break;
			}
		}
		else
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Up:
				ChangeSkinInSelectedCorp(next: false);
				break;
			case MoveDirection.Down:
				ChangeSkinInSelectedCorp(next: true);
				break;
			default:
				d.LogErrorFormat("Unhandled Move Dir in UIPaletteBlockSelect {0}", eventData.moveDir);
				break;
			case MoveDirection.Left:
			case MoveDirection.Right:
			case MoveDirection.None:
				break;
			}
		}
		eventData.Use();
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (m_BuySelected)
		{
			m_Palette.OnButtonBuySkinClicked();
		}
	}

	private void OnPool()
	{
		m_Palette = GetComponent<UISkinsPaletteHUD>();
		if (m_Palette.IsNull())
		{
			d.LogAssertion("Must have a UISkinsPaletteHUD component to match UISkinsController!");
		}
	}

	private void OnSpawn()
	{
		EnumValuesIterator<FactionSubTypes> enumerator = EnumIterator<FactionSubTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			FactionSubTypes current = enumerator.Current;
			if (current != FactionSubTypes.NULL)
			{
				SetSelectedSkinForCorp(0, current);
			}
		}
		SetCurrentSelectedCorp(FactionSubTypes.GSO);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			m_ModeBeforeAlt = Singleton.Manager<ManPointer>.inst.BuildMode;
			if (m_ModeBeforeAlt == ManPointer.BuildingMode.PaintSkin || m_ModeBeforeAlt == ManPointer.BuildingMode.PaintSkinTech)
			{
				Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.Grab);
			}
		}
		if (Input.GetKeyUp(KeyCode.LeftAlt) && (m_ModeBeforeAlt == ManPointer.BuildingMode.PaintSkin || m_ModeBeforeAlt == ManPointer.BuildingMode.PaintSkinTech))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (realtimeSinceStartup - m_LastAltPressTime <= 0.6f)
			{
				Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.Grab);
				return;
			}
			Singleton.Manager<ManPointer>.inst.ChangeBuildMode((m_ModeBeforeAlt == ManPointer.BuildingMode.PaintSkin) ? ManPointer.BuildingMode.PaintSkin : ManPointer.BuildingMode.PaintSkinTech);
			m_LastAltPressTime = realtimeSinceStartup;
		}
	}
}
