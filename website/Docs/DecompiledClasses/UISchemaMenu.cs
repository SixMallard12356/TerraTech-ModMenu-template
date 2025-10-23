using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISchemaMenu : UIScreen
{
	private struct SchemaMapping
	{
		public InputAxisMapping m_Mapping;

		public bool m_Invert;

		public SchemaMapping(InputAxisMapping mapping, bool invert)
		{
			m_Mapping = mapping;
			m_Invert = invert;
		}
	}

	[SerializeField]
	private UISchemaMenuView m_SchemaView;

	[SerializeField]
	private UISchemaMenu3DModel m_3DModel;

	[SerializeField]
	private InputAxisMapping[] m_DisabledControllerMappings;

	[SerializeField]
	private Text m_DeleteButtonText;

	public static Event<Tank> ControlSchemesEditedEvent;

	private Tank m_Tech;

	private ControlSchemeLibrary m_SchemeLibraryClone;

	private ControlScheme m_CurrentScheme;

	private List<ControlScheme> m_TechSchemes;

	private List<ControlScheme> m_InitialSchemes;

	private UISchemaMenu3DModel m_3DModelInstance;

	private string m_YesText;

	private string m_NoText;

	private Player m_RewiredPlayer;

	private bool m_PollingInput;

	private int m_PollingBindingIndex;

	private ControlSchemeLibrary m_SavedLibrary;

	private bool m_ShowingPopup;

	private List<SchemaMapping> m_LastFrameMappings = new List<SchemaMapping>();

	private List<SchemaMapping> m_CurrentFrameMappings = new List<SchemaMapping>();

	public bool AllowMultiBinding => !SKU.ConsoleUI;

	public override void Show(bool fromStackPop)
	{
		m_Tech = Singleton.playerTank;
		m_RewiredPlayer = ReInput.players.GetPlayer(0);
		bool flag = false;
		if (m_SavedLibrary != null)
		{
			SetupSavedLibrary();
			flag = true;
		}
		else
		{
			SetupNewLibrary();
		}
		ControlScheme schema = m_SchemeLibraryClone.Schemes[0];
		if ((bool)m_Tech)
		{
			schema = m_SchemeLibraryClone.LookupScheme(m_Tech.control.ActiveScheme);
		}
		m_3DModelInstance = m_3DModel.Spawn();
		m_3DModelInstance.transform.position = new Vector3(0f, 100f, 0f);
		InputAxisMapping[] disabledMappings = null;
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			disabledMappings = m_DisabledControllerMappings;
		}
		m_SchemaView.Init(m_3DModelInstance.GetRenderTex(), m_SchemeLibraryClone, m_TechSchemes, disabledMappings, OnSchemeSelected, OnSchemeToggled);
		OnSchemeSelected(schema);
		EventSystem.current.SetSelectedGameObject(m_SchemaView.Library.GetCurrentlySelectedElement().GetButton().gameObject);
		if (flag)
		{
			UpdateApplyButton();
		}
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TechManager))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechManager);
		}
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.PlayerInfo))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.PlayerInfo);
		}
		base.Show(fromStackPop);
	}

	public override void Hide()
	{
		base.Hide();
		if (m_3DModelInstance != null)
		{
			m_3DModelInstance.Recycle();
			m_3DModelInstance = null;
		}
		m_ShowingPopup = false;
	}

	public override void HideBehindPopup()
	{
		m_ShowingPopup = true;
		base.HideBehindPopup();
	}

	public override void ReturnFromPopup()
	{
		m_ShowingPopup = false;
		base.ReturnFromPopup();
	}

	public override bool GoBack()
	{
		if (m_PollingInput)
		{
			CancelKeyRebind();
		}
		else if (!m_ShowingPopup)
		{
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				ApplyAndExit();
			}
			else
			{
				CloseSchemaMenu();
			}
		}
		return false;
	}

	public void CloseSchemaMenu()
	{
		if (HasSchemeChanged())
		{
			OpenConfirmationScreen(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 69), CloseMenu, CloseConfirmationScreen);
		}
		else
		{
			CloseMenu();
		}
	}

	public void ApplyAndExit()
	{
		if ((bool)m_Tech && m_TechSchemes.Count == 0)
		{
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
			};
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 105);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(localisedString, accept, localisedString2);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			return;
		}
		ControlSchemeLibrary controlSchemeLibrary = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary;
		controlSchemeLibrary.ApplyLibraryChangesFrom(m_SchemeLibraryClone);
		Singleton.Manager<ManProfile>.inst.Save();
		if ((bool)m_Tech)
		{
			m_Tech.control.Schemes = controlSchemeLibrary.LookupSchemes(m_TechSchemes);
			m_Tech.control.CheckSchemeIsSet();
			ApplyActiveControlScheme();
			m_Tech.control.UpdateSchemeHUD();
			ControlSchemesEditedEvent.Send(m_Tech);
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	public void DuplicateScheme()
	{
		ControlScheme controlScheme = m_SchemeLibraryClone.CreateNewCustomScheme(m_CurrentScheme, m_CurrentScheme.GetName());
		if (controlScheme != null)
		{
			m_SchemaView.AddNewScheme(controlScheme, OnSchemeSelected, OnSchemeToggled);
			Canvas.ForceUpdateCanvases();
			OnSchemeSelected(controlScheme);
			EventSystem.current.SetSelectedGameObject(m_SchemaView.Library.GetCurrentlySelectedElement().GetButton().gameObject);
		}
		else
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 108);
			ShowWarningPopup(localisedString);
		}
	}

	public void RenameControlScheme()
	{
		UIScreenRenameScheme uIScreenRenameScheme = (UIScreenRenameScheme)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RenameSchema);
		uIScreenRenameScheme.Set(m_CurrentScheme, OnSchemeRenamed, CloseConfirmationScreen);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenRenameScheme);
	}

	public void RestoreDefaultsOrDeleteScheme()
	{
		if (m_CurrentScheme.IsCustom)
		{
			OpenConfirmationScreen(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 70), DeleteCurrentScheme, CloseConfirmationScreen);
			return;
		}
		ControlScheme defaultSettingsFor = m_SchemeLibraryClone.GetDefaultSettingsFor(m_CurrentScheme);
		if (!m_CurrentScheme.Equals(defaultSettingsFor))
		{
			OpenConfirmationScreen(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 71), RestoreDefaultsToCurrentScheme, CloseConfirmationScreen);
		}
	}

	public void OpenRebindKeysMenu()
	{
		m_SavedLibrary = m_SchemeLibraryClone;
		UIScreen screen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.Options);
		Singleton.Manager<ManUI>.inst.PushScreen(screen);
		((UIScreenOptions)screen).ShowOptions(UIScreenOptions.OptionsType.Controls);
	}

	public void CancelKeyRebind()
	{
		m_SchemaView.RefreshAvailableAxis(m_CurrentScheme);
	}

	private void SetupSavedLibrary()
	{
		m_SchemeLibraryClone = m_SavedLibrary;
		m_SavedLibrary = null;
	}

	private void SetupNewLibrary()
	{
		m_SchemeLibraryClone = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.CloneLibrary();
		if ((bool)m_Tech)
		{
			m_TechSchemes = m_SchemeLibraryClone.LookupSchemes(m_Tech.control.Schemes);
		}
		else
		{
			m_TechSchemes = new List<ControlScheme>();
		}
		m_InitialSchemes = new List<ControlScheme>(m_TechSchemes);
	}

	private void OnSchemeSelected(ControlScheme schema)
	{
		m_CurrentScheme = schema;
		if ((bool)m_DeleteButtonText)
		{
			m_DeleteButtonText.text = (m_CurrentScheme.IsCustom ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 59) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 57));
		}
		m_SchemaView.DisplayScheme(m_CurrentScheme, EnableInputPolling, OnMappingChanged, OnInvertReverseChanged, OnAxisHovered);
		Singleton.Manager<ManNavUI>.inst.AddEntryTarget(m_SchemaView.Library.GetCurrentlySelectedElement().GetButton().gameObject);
	}

	private void OnSchemeToggled(ControlScheme schema, bool selected)
	{
		if (m_TechSchemes != null)
		{
			if (selected && !m_TechSchemes.Contains(schema))
			{
				int maxSchemesPerTech = m_SchemeLibraryClone.GetMaxSchemesPerTech();
				if (m_TechSchemes.Count >= maxSchemesPerTech)
				{
					Action accept = delegate
					{
						Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
						m_SchemaView.UpdateTechSchemeToggles(m_TechSchemes);
					};
					UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
					string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 104);
					localisedString = string.Format(localisedString, maxSchemesPerTech);
					string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
					uIScreenNotifications.Set(localisedString, accept, localisedString2);
					Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
				}
				else
				{
					m_TechSchemes.Add(schema);
					OnSchemeSelected(schema);
				}
			}
			else if (!selected)
			{
				m_TechSchemes.Remove(schema);
			}
		}
		UpdateApplyButton();
	}

	private void OnAxisHovered(MovementAxis axis, bool hovered)
	{
		if (!m_PollingInput)
		{
			if (hovered)
			{
				m_3DModelInstance.SetAnimation(axis);
			}
			else
			{
				m_3DModelInstance.StopAnimation();
			}
			m_SchemaView.SetAxisImage(axis, hovered, m_CurrentScheme.GetAxisMapping(axis));
		}
	}

	private void ApplyActiveControlScheme()
	{
		if ((bool)m_Tech)
		{
			ControlScheme controlScheme = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.LookupScheme(m_CurrentScheme);
			if (controlScheme != null && m_Tech.control.Schemes.Contains(controlScheme))
			{
				m_Tech.control.SetActiveScheme(controlScheme);
			}
		}
	}

	private void CloseConfirmationScreen()
	{
		if (Singleton.Manager<ManUI>.inst.IsPopupShowing())
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
		m_SchemaView.Library.GetCurrentlySelectedElement().AddDynamicButtonPrompts();
	}

	private void RestoreDefaultsToCurrentScheme()
	{
		m_SchemeLibraryClone.ResetToDefault(m_CurrentScheme);
		m_SchemaView.DisplayScheme(m_CurrentScheme, EnableInputPolling, OnMappingChanged, OnInvertReverseChanged, OnAxisHovered);
		m_SchemaView.UpdateTextForScheme(m_CurrentScheme);
		CloseConfirmationScreen();
		UpdateApplyButton();
	}

	private void OnMappingChanged(MovementAxis movementAxis, int bindingIndex, InputAxisMapping inputAxis, bool invertAxis)
	{
		if (AllowMultiBinding)
		{
			m_CurrentScheme.SetAxisMapping(movementAxis, bindingIndex, inputAxis, invertAxis);
		}
		else
		{
			m_CurrentScheme.ClearAxisMapping(movementAxis);
			m_CurrentScheme.SetAxisMapping(movementAxis, 0, inputAxis, invertAxis);
		}
		m_SchemaView.RefreshAvailableAxis(m_CurrentScheme);
		UpdateApplyButton();
	}

	private void OnInvertReverseChanged(bool invert)
	{
		m_CurrentScheme.ReverseSteering = invert;
		UpdateApplyButton();
	}

	private void OnSchemeRenamed(string newName)
	{
		Singleton.Manager<ManUI>.inst.PopAllPopups();
		newName = newName.Trim();
		if (newName.Length != 0)
		{
			if (newName.Length > ControlSchemeLibrary.GetMaxNameLength())
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 106);
				localisedString = string.Format(localisedString, 1, ControlSchemeLibrary.GetMaxNameLength());
				ShowWarningPopup(localisedString);
			}
			else if (newName != m_CurrentScheme.GetName() && m_SchemeLibraryClone.GetControlSchemeByName(newName) != null)
			{
				ShowWarningPopup(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 107));
			}
			else
			{
				m_CurrentScheme.SetName(newName);
				m_SchemaView.UpdateTextForScheme(m_CurrentScheme);
			}
		}
		UpdateApplyButton();
		m_SchemaView.Library.GetCurrentlySelectedElement().AddDynamicButtonPrompts();
	}

	private void DeleteCurrentScheme()
	{
		int num = m_SchemeLibraryClone.Schemes.IndexOf(m_CurrentScheme);
		m_SchemeLibraryClone.RemoveControlScheme(m_CurrentScheme);
		m_SchemaView.RemoveScheme(m_CurrentScheme);
		ControlScheme schema = ((num >= m_SchemeLibraryClone.Schemes.Count) ? m_SchemeLibraryClone.Schemes[num - 1] : m_SchemeLibraryClone.Schemes[num]);
		OnSchemeSelected(schema);
		EventSystem.current.SetSelectedGameObject(m_SchemaView.Library.GetCurrentlySelectedElement().GetButton().gameObject);
		CloseConfirmationScreen();
		UpdateApplyButton();
	}

	private void OpenConfirmationScreen(string text, Action confirmAction, Action cancelAction)
	{
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		uIScreenNotifications.Set(text, confirmAction, cancelAction, m_YesText, m_NoText);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void ShowWarningPopup(string text)
	{
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		uIScreenNotifications.Set(text, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, localisedString);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void EnableInputPolling(bool enable, MovementAxis axis, int bindingIndex)
	{
		m_PollingInput = enable;
		m_PollingBindingIndex = bindingIndex;
		if (enable)
		{
			m_3DModelInstance.SetAnimation(axis);
		}
		else
		{
			m_3DModelInstance.StopAnimation();
		}
		m_SchemaView.SetAxisImage(axis, enable, m_CurrentScheme.GetAxisMapping(axis));
		EventSystem.current.sendNavigationEvents = !enable;
	}

	private void PollForKeyInput()
	{
		m_LastFrameMappings.Clear();
		m_LastFrameMappings.AddRange(m_CurrentFrameMappings);
		m_CurrentFrameMappings.Clear();
		foreach (Controller controller in m_RewiredPlayer.controllers.Controllers)
		{
			foreach (ControllerPollingInfo item in controller.PollForAllElements())
			{
				foreach (ControllerMap map in m_RewiredPlayer.controllers.maps.GetMaps(controller))
				{
					foreach (ActionElementMap allMap in map.AllMaps)
					{
						if (item.elementIdentifierId != allMap.elementIdentifierId)
						{
							continue;
						}
						InputAxisMapping axisMappingForRewiredAction = AxisMapping.GetAxisMappingForRewiredAction(allMap.actionId);
						if (axisMappingForRewiredAction != InputAxisMapping.Unmapped && IsMappingEnabled(axisMappingForRewiredAction))
						{
							bool flag = allMap.axisContribution == Pole.Negative;
							if (m_SchemaView.Bindings.RequestingNegativeDirection)
							{
								flag = !flag;
							}
							m_CurrentFrameMappings.Add(new SchemaMapping(axisMappingForRewiredAction, flag));
							break;
						}
					}
				}
			}
		}
		foreach (SchemaMapping lastFrameMapping in m_LastFrameMappings)
		{
			bool flag2 = false;
			foreach (SchemaMapping currentFrameMapping in m_CurrentFrameMappings)
			{
				if (currentFrameMapping.m_Mapping == lastFrameMapping.m_Mapping)
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				OnMappingChanged(m_SchemaView.Bindings.CurrentAxis, m_PollingBindingIndex, lastFrameMapping.m_Mapping, lastFrameMapping.m_Invert);
			}
		}
	}

	private bool HasSchemeChanged()
	{
		if (m_TechSchemes.Count != m_InitialSchemes.Count)
		{
			return true;
		}
		for (int i = 0; i < m_TechSchemes.Count; i++)
		{
			if (m_TechSchemes[i] != m_InitialSchemes[i])
			{
				return true;
			}
		}
		return !Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.Equals(m_SchemeLibraryClone);
	}

	private void CloseMenu()
	{
		m_SchemaView.Close();
		CloseConfirmationScreen();
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private bool IsMappingEnabled(InputAxisMapping mapping)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			InputAxisMapping[] disabledControllerMappings = m_DisabledControllerMappings;
			foreach (InputAxisMapping inputAxisMapping in disabledControllerMappings)
			{
				if (mapping == inputAxisMapping)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void OnPool()
	{
		m_YesText = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 1);
		m_NoText = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 2);
	}

	private void OnSpawn()
	{
	}

	private void UpdateApplyButton()
	{
		m_SchemaView.SetApplyButtonInteractable(HasSchemeChanged());
	}

	private void Update()
	{
		if (m_PollingInput)
		{
			PollForKeyInput();
		}
		else if (!m_ShowingPopup && Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
			{
				RestoreDefaultsOrDeleteScheme();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(58))
			{
				RenameControlScheme();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(42))
			{
				DuplicateScheme();
			}
			if (!SKU.ConsoleUI && Singleton.Manager<ManInput>.inst.GetButtonDown(41))
			{
				OpenRebindKeysMenu();
			}
		}
	}
}
