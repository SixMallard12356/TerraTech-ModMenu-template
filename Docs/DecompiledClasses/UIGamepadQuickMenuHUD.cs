#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamepadQuickMenuHUD : UIHUDElement
{
	[Serializable]
	private class LocString
	{
		public LocalisedString m_LocString;
	}

	[SerializeField]
	private GameObject m_ButtonParent;

	[SerializeField]
	private GameObject m_ButtonPrefab;

	[EnumArray(typeof(ManHUD.HUDElementType))]
	[SerializeField]
	private LocString[] m_HUDElementLocStrings;

	[EnumArray(typeof(ManHUD.HUDElementType))]
	[SerializeField]
	private Sprite[] m_HUDElementButtonSprites;

	[SerializeField]
	private TextMeshProUGUI m_HoverText;

	private List<UIHUDOpenButton> m_Buttons = new List<UIHUDOpenButton>();

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
		if (m_ButtonParent.IsNull())
		{
			d.LogError("No button parent assigned in UIGamepadQuickMenuHUD in GameObject: " + base.gameObject.name);
		}
		if (m_HoverText.IsNull())
		{
			d.LogError("No hover text assigned in UIGamepadQuickMenuHUD in GameObject: " + base.gameObject.name);
		}
	}

	public override void Show(object context)
	{
		base.Show(context);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Open);
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
		}
		List<ManHUD.HUDElementType> list = new List<ManHUD.HUDElementType>();
		switch (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType())
		{
		case ManGameMode.GameType.MainGame:
			if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.WorldMap, UIHUD.ShowAction.Show) && Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.MissionLog, UIHUD.ShowAction.Show) && Singleton.Manager<ManQuestLog>.inst.QuestLogAvailable)
			{
				list.Add(ManHUD.HUDElementType.MissionLog);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			break;
		case ManGameMode.GameType.RaD:
			if (Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			break;
		case ManGameMode.GameType.Deathmatch:
			if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.WorldMap, UIHUD.ShowAction.Show) && Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			list.Add(ManHUD.HUDElementType.ScoreBoard);
			break;
		case ManGameMode.GameType.Creative:
			if (Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			break;
		case ManGameMode.GameType.CoOpCreative:
			if (Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			list.Add(ManHUD.HUDElementType.PlayerInfo);
			break;
		case ManGameMode.GameType.Misc:
			if (Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			break;
		case ManGameMode.GameType.CoOpCampaign:
			if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.WorldMap, UIHUD.ShowAction.Show) && Singleton.Manager<ManMap>.inst.MapAvailable)
			{
				list.Add(ManHUD.HUDElementType.WorldMap);
			}
			if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.MissionLog, UIHUD.ShowAction.Show) && Singleton.Manager<ManQuestLog>.inst.QuestLogAvailable)
			{
				list.Add(ManHUD.HUDElementType.MissionLog);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
			{
				list.Add(ManHUD.HUDElementType.TechLoader);
			}
			list.Add(ManHUD.HUDElementType.TechManager);
			break;
		default:
			d.LogError("Mode not implemented in UIGamepadQuickMenuHUD!");
			break;
		case ManGameMode.GameType.Attract:
		case ManGameMode.GameType.RacingChallenge:
		case ManGameMode.GameType.FlyingChallenge:
		case ManGameMode.GameType.SumoShowdown:
		case ManGameMode.GameType.Defense:
		case ManGameMode.GameType.Gauntlet:
			break;
		}
		list.Add(ManHUD.HUDElementType.SkinsPalette);
		for (int num = m_Buttons.Count - 1; num >= 0; num--)
		{
			m_Buttons[num].OnHUDButtonHighlighted.Unsubscribe(OnHUDElementButtonHighlighted);
			m_Buttons[num].transform.Recycle();
		}
		m_Buttons.Clear();
		foreach (ManHUD.HUDElementType item in list)
		{
			GameObject gameObject = m_ButtonPrefab.transform.Spawn().gameObject;
			gameObject.transform.SetParent(m_ButtonParent.transform, worldPositionStays: false);
			UIHUDOpenButton component = gameObject.GetComponent<UIHUDOpenButton>();
			if ((bool)component)
			{
				component.SetHUDElementType(item);
				component.OnHUDButtonHighlighted.Subscribe(OnHUDElementButtonHighlighted);
				Image component2 = gameObject.GetComponent<Image>();
				if ((bool)component2)
				{
					component2.sprite = m_HUDElementButtonSprites[(int)item];
				}
				else
				{
					d.LogError("No image component on button prefab in UIGamepadQuickMenuHUD");
				}
				UIShowHUDElementButton component3 = gameObject.GetComponent<UIShowHUDElementButton>();
				if ((bool)component3)
				{
					component3.m_HUDElementToShow = item;
				}
				else
				{
					d.LogError("No UIShowHUDElementButton component on button prefab in UIGamepadQuickMenuHUD");
				}
			}
			else
			{
				d.LogError("No button component on button prefab in UIGamepadQuickMenuHUD");
			}
			m_Buttons.Add(component);
		}
		Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_Buttons[0].gameObject);
	}

	private void OnHUDElementButtonHighlighted(ManHUD.HUDElementType element)
	{
		m_HoverText.text = m_HUDElementLocStrings[(int)element].m_LocString.Value;
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_Buttons[0].gameObject);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Close);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManUI>.inst.HideScreenPrompt();
		base.Hide(context);
	}
}
