#define UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenRenameTech : UIScreen
{
	[SerializeField]
	private InputField m_NameText;

	[SerializeField]
	private Button m_NameButton;

	[SerializeField]
	private GameObject m_NameContainer;

	[SerializeField]
	private RadarMarkerEditButton[] m_IconButtons = new RadarMarkerEditButton[0];

	[SerializeField]
	private RadarMarkerEditButton[] m_ColorButtons = new RadarMarkerEditButton[0];

	[SerializeField]
	private GameObject m_ConsoleCommandText;

	[SerializeField]
	private GameObject[] m_HiddenUIOnConsole;

	private TrackedVisible m_SelectedTech;

	private string m_PreInputName;

	private ManRadar.RadarMarkerColorType m_SelectedColor;

	private ManRadar.IconType m_SelectedIcon;

	private int m_RadarMarkerButtonCount = -1;

	private Dictionary<RadarMarkerEditButton.EditingType, RadarMarkerEditButton[]> m_RadarMarkerEditButtons = new Dictionary<RadarMarkerEditButton.EditingType, RadarMarkerEditButton[]>();

	public bool IsRadarMarkerEditor
	{
		get
		{
			if (m_RadarMarkerButtonCount == -1)
			{
				m_RadarMarkerButtonCount = m_ColorButtons.Length + m_IconButtons.Length;
			}
			return m_RadarMarkerButtonCount > 0;
		}
	}

	private bool AllowEditName
	{
		get
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return !SKU.OverrideMpTechNames;
			}
			return true;
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		TogglePlatformUI();
		m_PreInputName = "MyTech";
		ManRadar.IconType iconType = ManRadar.IconType.FriendlyVehicle;
		ManRadar.RadarMarkerColorType radarMarkerColorType = ManRadar.RadarMarkerColorType.White;
		if (m_SelectedTech != null)
		{
			if (m_SelectedTech.visible.IsNotNull())
			{
				m_PreInputName = m_SelectedTech.visible.name;
				if (IsRadarMarkerEditor)
				{
					RadarMarker radarMarkerConfig = m_SelectedTech.visible.tank.RadarMarker.RadarMarkerConfig;
					iconType = radarMarkerConfig.Icon;
					radarMarkerColorType = radarMarkerConfig.Color;
				}
			}
			else
			{
				TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(m_SelectedTech);
				if (storedTechData != null)
				{
					m_PreInputName = storedTechData.Name;
					if (IsRadarMarkerEditor)
					{
						iconType = storedTechData.RadarMarkerConfig.Icon;
						radarMarkerColorType = storedTechData.RadarMarkerConfig.Color;
					}
				}
				else
				{
					d.LogError("No TechData for tracked visible with ID " + m_SelectedTech.ID);
				}
			}
		}
		if (IsRadarMarkerEditor)
		{
			m_SelectedIcon = iconType;
			m_SelectedColor = radarMarkerColorType;
			RadarMarkerEditButton[] iconButtons = m_IconButtons;
			foreach (RadarMarkerEditButton obj in iconButtons)
			{
				obj.SetHilighted(state: false);
				obj.SetOptionSelected(obj.EnumerationValue == (int)iconType);
			}
			iconButtons = m_ColorButtons;
			foreach (RadarMarkerEditButton obj2 in iconButtons)
			{
				obj2.SetHilighted(state: false);
				obj2.SetOptionSelected(obj2.EnumerationValue == (int)radarMarkerColorType);
			}
		}
		m_NameText.text = m_PreInputName;
		m_NameText.readOnly = true;
		SelectDefaultUIOption();
		if (AllowEditName && !IsRadarMarkerEditor)
		{
			ActivateRenaming();
		}
	}

	public void ActivateRenaming()
	{
		if (VirtualKeyboard.IsRequired())
		{
			OpenVirtualKeyboard();
			return;
		}
		m_NameText.readOnly = false;
		m_NameText.interactable = true;
		if (IsRadarMarkerEditor)
		{
			m_NameText.Select();
		}
		m_NameText.ActivateInputField();
	}

	public void DeactivateRenaming()
	{
		m_NameText.readOnly = true;
		m_NameText.interactable = false;
		m_NameText.DeactivateInputField();
		if (IsRadarMarkerEditor)
		{
			SelectDefaultUIOption();
		}
	}

	public void OpenVirtualKeyboard()
	{
		VirtualKeyboard.PromptInput(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 1), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 2), onCompleteHandler: delegate(bool accepted, string result)
		{
			if (accepted && !string.IsNullOrEmpty(result))
			{
				m_NameText.text = result;
				if (!IsRadarMarkerEditor)
				{
					OnSave();
				}
			}
			else if (!IsRadarMarkerEditor)
			{
				OnCancel();
			}
		}, defaultText: m_NameText.text);
	}

	public override void Hide()
	{
		m_NameText.text = m_PreInputName;
		base.Hide();
		UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
		if (uITechManagerHUD != null)
		{
			uITechManagerHUD.ShowIfTemporarilyHidden();
		}
	}

	private void TogglePlatformUI()
	{
		bool flag = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		GameObject[] hiddenUIOnConsole = m_HiddenUIOnConsole;
		for (int i = 0; i < hiddenUIOnConsole.Length; i++)
		{
			hiddenUIOnConsole[i].gameObject.SetActive(!flag);
		}
		if (m_ConsoleCommandText != null)
		{
			m_ConsoleCommandText.SetActive(flag);
		}
		if (m_NameContainer != null)
		{
			m_NameContainer.SetActive(AllowEditName);
		}
	}

	public void SetSelectedTech(TrackedVisible trackedVis)
	{
		m_SelectedTech = trackedVis;
	}

	public static void RenameTechMultiplayerSafe(TrackedVisible tv, string newName)
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			DoRenameTech(tv, newName);
			return;
		}
		SetTechNameMessage message = new SetTechNameMessage
		{
			m_NewName = newName,
			m_HostId = tv.HostID
		};
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RenameTech, message);
	}

	public static void DoRenameTech(TrackedVisible tv, string newName)
	{
		if (tv.visible.IsNotNull())
		{
			if (tv.visible.tank.IsNotNull())
			{
				tv.visible.tank.SetName(newName);
				Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(tv.visible.tank, tv);
			}
			else
			{
				d.LogErrorFormat("DoRename tech being asked to name a visible that isn't a tech with ID {0}", tv.ID);
			}
			return;
		}
		ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(tv.Position, createNewDataIfNotFound: false);
		if (storedTileIfNotSpawned != null)
		{
			if (storedTileIfNotSpawned.TryChangeSavedTechInfo(tv.ID, delegate(ManSaveGame.StoredTech s)
			{
				s.m_TechData.Name = newName;
			}, "RenameSavedTech"))
			{
				Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(null, tv);
			}
		}
		else
		{
			d.LogErrorFormat("Trying to rename tech, but tech did not resolve to a valid Tile, Loaded or not, at position {0}", tv.Position);
		}
	}

	public static void ChangeRadarMarkerMultiplayerSafe(TrackedVisible tv, ManRadar.RadarMarkerColorType color, ManRadar.IconType icon)
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			DoChangeTechRadarMarker(tv, color, icon);
			return;
		}
		SetTechRadarMarkerConfigMessage message = new SetTechRadarMarkerConfigMessage
		{
			m_NewColor = (int)color,
			m_NewIcon = (int)icon,
			m_HostId = tv.HostID
		};
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ChangeTechRadarMarker, message);
	}

	public static void DoChangeTechRadarMarker(TrackedVisible tv, ManRadar.RadarMarkerColorType color, ManRadar.IconType icon)
	{
		if (tv.visible.IsNotNull())
		{
			if (tv.visible.tank.IsNotNull())
			{
				tv.visible.tank.RadarMarker.RadarMarkerConfig = new RadarMarker(icon, color, tv.visible.tank.RadarMarker.RadarMarkerConfig.IsUsed);
				return;
			}
			d.LogErrorFormat("DoChangeTechRadarMarker tech being asked to change a visible's radarmarker that isn't a tech with ID {0}", tv.ID);
			return;
		}
		ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(tv.Position, createNewDataIfNotFound: false);
		if (storedTileIfNotSpawned != null)
		{
			storedTileIfNotSpawned.TryChangeSavedTechInfo(tv.ID, delegate(ManSaveGame.StoredTech r)
			{
				r.m_TechData.RadarMarkerConfig = new RadarMarker(icon, color);
			}, "ChangeRadarMarkerForSavedTech");
		}
		else
		{
			d.LogErrorFormat("Trying to change tech radar marker info, but tech did not resolve to a valid Tile, Loaded or not, at position {0}", tv.Position);
		}
	}

	private void SelectDefaultUIOption()
	{
		if (m_NameButton != null && m_NameButton.gameObject.activeInHierarchy)
		{
			m_NameButton.Select();
		}
		else if (IsRadarMarkerEditor)
		{
			m_IconButtons[0].Button.Select();
		}
	}

	public void OnSave()
	{
		if (m_SelectedTech != null)
		{
			string newName = ((!string.IsNullOrEmpty(m_NameText.text)) ? m_NameText.text : "MyTech");
			if (SKU.IsNetEase)
			{
				Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_NameText.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
				{
					if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
					{
						m_NameText.text = response.Content;
						RenameTechMultiplayerSafe(m_SelectedTech, m_NameText.text);
						Singleton.Manager<ManUI>.inst.PopScreen();
					}
				});
			}
			else
			{
				RenameTechMultiplayerSafe(m_SelectedTech, newName);
			}
			if (m_SelectedTech.RadarMarkerConfig.IsUsed)
			{
				ChangeRadarMarkerMultiplayerSafe(m_SelectedTech, m_SelectedColor, m_SelectedIcon);
			}
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	public void OnEndEdit(string name)
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Keyboard) && !IsRadarMarkerEditor)
		{
			OnSave();
		}
		StartCoroutine(DeactivateRenamingCo());
	}

	private IEnumerator DeactivateRenamingCo()
	{
		yield return new WaitForEndOfFrame();
		DeactivateRenaming();
	}

	public void OnCancel()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void OnRadarMarkerIconSelected(int newSelection)
	{
		m_SelectedIcon = (ManRadar.IconType)newSelection;
		RadarMarkerEditButton[] iconButtons = m_IconButtons;
		foreach (RadarMarkerEditButton obj in iconButtons)
		{
			obj.SetOptionSelected(obj.EnumerationValue == newSelection);
		}
	}

	private void OnRadarMarkerColourSelected(int newSelection)
	{
		m_SelectedColor = (ManRadar.RadarMarkerColorType)newSelection;
		RadarMarkerEditButton[] colorButtons = m_ColorButtons;
		foreach (RadarMarkerEditButton obj in colorButtons)
		{
			obj.SetOptionSelected(obj.EnumerationValue == newSelection);
		}
	}

	private void OnPool()
	{
		RadarMarkerEditButton[] iconButtons = m_IconButtons;
		for (int i = 0; i < iconButtons.Length; i++)
		{
			iconButtons[i].OnSelectedOptionUpdated.Subscribe(OnRadarMarkerIconSelected);
		}
		iconButtons = m_ColorButtons;
		for (int i = 0; i < iconButtons.Length; i++)
		{
			iconButtons[i].OnSelectedOptionUpdated.Subscribe(OnRadarMarkerColourSelected);
		}
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				DeactivateRenaming();
			}
			else if (!IsRadarMarkerEditor)
			{
				OnSave();
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				m_NameText.text = m_PreInputName;
				DeactivateRenaming();
			}
			else if (!IsRadarMarkerEditor)
			{
				OnCancel();
			}
		}
	}
}
