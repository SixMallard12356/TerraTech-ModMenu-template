#define UNITY_EDITOR
using System;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using Rewired;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIScreenSnapshot : UIScreen
{
	[SerializeField]
	private InputField m_NameText;

	[SerializeField]
	private Image m_SnapshotImage;

	[SerializeField]
	private IntVector2 m_SnapshotSize = new IntVector2(548, 322);

	[SerializeField]
	private GameObject m_TwitterPanel;

	[SerializeField]
	private InputField m_TweetTextField;

	[SerializeField]
	private Text m_TagsAndLinks;

	[SerializeField]
	private Button m_InvaderButton;

	[SerializeField]
	private GameObject m_GrabItPanel;

	[SerializeField]
	private GameObject m_SteamWorkshopPanel;

	[SerializeField]
	private Button m_SteamUploadBtn;

	[SerializeField]
	private Text m_SteamUploadBtnLabel;

	[SerializeField]
	[FormerlySerializedAs("m_HiddenUIOnConsole")]
	private GameObject[] m_KeyboardUI;

	private string m_DefaultTweetTag = "#MyTerraTech";

	private string m_SumoTweetTag = "#MyTerraTechSumo";

	private string m_Link = "bit.ly/TerraTechUsingTwitter";

	private bool m_SkipNameCheck;

	private Texture2D m_Snapshot;

	private TechData m_SnapshotTechData;

	private bool m_MessageUnchanged;

	private bool m_GoingToInfoScreen;

	private string m_PreInputName;

	private CommandOperation<SteamUploadData> m_SteamUploadOp;

	public TrackedVisible TargetOverride { get; set; }

	public IntVector2 SnapshotSize => m_SnapshotSize;

	public void InitialiseSnapshotData(Texture2D snapshot, TechData techData, TrackedVisible targetOverride = null)
	{
		DestroySnapshotTexture();
		m_Snapshot = snapshot;
		m_SnapshotTechData = techData;
		TargetOverride = targetOverride;
		if (m_Snapshot.IsNull() || techData == null)
		{
			d.LogError("UIScreenSnapshot::InsertSnapshotData: Null snapshot or tech data passed in!");
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Snapshot);
			if (TargetOverride == null)
			{
				m_NameText.text = (Singleton.playerTank ? Singleton.playerTank.name : "MyTech");
			}
			else
			{
				m_NameText.text = (TargetOverride.visible.IsNotNull() ? TargetOverride.visible.name : m_SnapshotTechData.Name);
			}
			m_MessageUnchanged = true;
			if (SKU.AllowTextInput)
			{
				m_PreInputName = m_NameText.text;
				if (VirtualKeyboard.IsRequired())
				{
					OpenVirtualKeyboard();
				}
				else
				{
					m_NameText.Select();
					m_NameText.ActivateInputField();
				}
			}
			else
			{
				m_NameText.interactable = false;
				if ((bool)Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop)
				{
					m_NameText.text = Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop.GetNextAvailableSnapshotName(m_NameText.text);
				}
			}
			Singleton.Manager<ManScreenshot>.inst.PlayFlash();
			if (SKU.IsSteam)
			{
				bool flag = Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled;
				m_SteamWorkshopPanel.SetActive(flag);
				if (flag && m_SteamUploadOp != null)
				{
					UpdateSteamUploadButton(m_SteamUploadOp.IsRunning);
				}
			}
			else
			{
				m_SteamWorkshopPanel.SetActive(value: false);
			}
			m_TwitterPanel.gameObject.SetActive(value: false);
			if (SKU.GrabItEnabled)
			{
				Singleton.Manager<ManGrabIt>.inst.PrepareTechGrab(Singleton.playerTank);
				Singleton.Manager<ManGrabIt>.inst.SetInitCallback(OnGrabItInitialised);
				m_GrabItPanel.SetActive(value: false);
			}
			else
			{
				m_GrabItPanel.gameObject.SetActive(value: false);
			}
			if (!SKU.ConsoleUI)
			{
				int num = Singleton.Manager<ManTechs>.inst.IteratePlayerTechs().Count();
				bool active = Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() && num > 1;
				m_InvaderButton.gameObject.SetActive(active);
			}
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				ToggleJoypadUI(on: true);
			}
			else
			{
				ToggleJoypadUI(on: false);
			}
			if (SKU.ConsoleUI)
			{
				m_ExitButton.gameObject.SetActive(value: false);
			}
		}
		if ((bool)m_Snapshot)
		{
			m_SnapshotImage.sprite = Sprite.Create(m_Snapshot, new Rect(0f, 0f, m_Snapshot.width, m_Snapshot.height), new Vector2(0.5f, 0.5f));
		}
		m_GoingToInfoScreen = false;
	}

	public override void Hide()
	{
		if (!m_GoingToInfoScreen)
		{
			Singleton.Manager<ManScreenshot>.inst.TakingSnapshot = false;
			TargetOverride = null;
		}
		base.Hide();
		UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
		if (uITechManagerHUD != null)
		{
			uITechManagerHUD.ShowIfTemporarilyHidden();
		}
	}

	private void ToggleJoypadUI(bool on)
	{
		GameObject[] keyboardUI = m_KeyboardUI;
		for (int i = 0; i < keyboardUI.Length; i++)
		{
			keyboardUI[i].gameObject.SetActive(!on);
		}
	}

	private void TrySaveSnapshot(Action<bool> onSaveCallback)
	{
		if (Singleton.Manager<ManSnapshots>.inst.ServiceDisk.CheckSnapshotExists(m_NameText.text))
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 59);
			localisedString = string.Format(localisedString, m_NameText.text);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 60);
			Action accept = delegate
			{
				SaveSnapshot(saveToDisk: true, onSaveCallback);
				Singleton.Manager<ManUI>.inst.PopScreen();
				UITechManagerHUD uITechManagerHUD2 = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
				if (uITechManagerHUD2 != null)
				{
					uITechManagerHUD2.ShowIfTemporarilyHidden();
				}
			};
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5);
			Action decline = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			};
			m_GoingToInfoScreen = true;
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString, accept, decline, localisedString2, localisedString3);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen, ManUI.PauseType.Pause);
			UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
			if ((bool)uITechManagerHUD)
			{
				uITechManagerHUD.TemporarilyHide();
			}
		}
		else
		{
			SaveSnapshot(saveToDisk: true, onSaveCallback);
		}
	}

	private void SaveSnapshot(bool saveToDisk, Action<bool> onSaveCallback = null)
	{
		if (saveToDisk)
		{
			Singleton.Manager<ManScreenshot>.inst.RenderTechImage(TargetOverride, Singleton.Manager<ManSnapshots>.inst.GetDiskSnapshotImageSize(), encodeTechData: true, delegate(TechData techData, Texture2D techImage)
			{
				DestroySnapshotTexture();
				m_Snapshot = techImage;
				if (SKU.ConsoleUI)
				{
					SaveDataConsoles.ShowSavePopupNotification();
				}
				bool isPlayerTech = TargetOverride == null;
				Singleton.Manager<ManSnapshots>.inst.SaveSnapshotRender(techData, m_Snapshot, m_NameText.text, isPlayerTech, onSaveCallback);
			});
		}
		else
		{
			Singleton.Manager<ManScreenshot>.inst.RenderTechImage(TargetOverride, m_SnapshotSize, encodeTechData: false, delegate(TechData techData, Texture2D techImage)
			{
				DestroySnapshotTexture();
				m_Snapshot = techImage;
			});
		}
	}

	private void OnGrabItInitialised(bool initialised)
	{
		m_GrabItPanel.SetActive(initialised);
	}

	private void DoGrabItUpload()
	{
		SaveSnapshot(saveToDisk: false);
		Singleton.Manager<ManGrabIt>.inst.DoGrab(m_Snapshot);
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private string GetTweetMessage(string name)
	{
		LocalisationEnums.Social stringID = (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeSumo>() ? LocalisationEnums.Social.socTweetSumoDefault : LocalisationEnums.Social.socTweetDefault);
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, (int)stringID), name);
	}

	private void UpdateSteamUploadButton(bool isUploading)
	{
		if (m_SteamUploadBtn != null)
		{
			m_SteamUploadBtn.interactable = !isUploading;
		}
		string text = (isUploading ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 47) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 2));
		if (m_SteamUploadBtnLabel != null)
		{
			m_SteamUploadBtnLabel.text = text.ToUpper();
		}
	}

	public void OnSave()
	{
		if (m_NameText.text.Length == 0)
		{
			d.LogWarning("Trying to save snapshot with empty name! Don't allow this (but maybe we should provide user feedback..)");
			return;
		}
		if (SKU.IsNetEase)
		{
			if (!m_SkipNameCheck)
			{
				Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_NameText.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
				{
					if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
					{
						m_SkipNameCheck = true;
						m_NameText.text = response.Content;
						OnSave();
					}
				});
				return;
			}
			m_SkipNameCheck = false;
		}
		if (TargetOverride == null)
		{
			Singleton.playerTank.name = m_NameText.text;
			Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(Singleton.playerTank, null);
		}
		else
		{
			UIScreenRenameTech.RenameTechMultiplayerSafe(TargetOverride, m_NameText.text);
		}
		TrySaveSnapshot(OnSaveCallback);
	}

	private void OnSaveCallback(bool success)
	{
		if (SKU.ConsoleUI)
		{
			SaveDataConsoles.RemoveSavePopupNotification();
		}
		m_GoingToInfoScreen = false;
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
	}

	public void OnTweet()
	{
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage(TargetOverride, m_SnapshotSize, encodeTechData: true, delegate(TechData techData, Texture2D techImage)
		{
			DestroySnapshotTexture();
			m_Snapshot = techImage;
		});
		UIScreenTwitterAuth uIScreenTwitterAuth = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitterAuth) as UIScreenTwitterAuth;
		uIScreenTwitterAuth.SetFailAction(delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		});
		uIScreenTwitterAuth.SetSuccessAction(delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = true;
			string text = m_TweetTextField.text + " " + m_TagsAndLinks.text;
			Singleton.Manager<TwitterAPI>.inst.PostTweetAsync(text, m_Snapshot.EncodeToPNG());
		});
		uIScreenTwitterAuth.SetUseLegacyMode();
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenTwitterAuth);
	}

	public void OnTweetInvader()
	{
		TrySaveSnapshot(OnTweetInvaderSaveComplete);
	}

	private void OnTweetInvaderSaveComplete(bool success)
	{
		UIScreenSendInvader screen = (UIScreenSendInvader)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SendInvader);
		Singleton.Manager<ManUI>.inst.GoToScreen(screen, ManUI.PauseType.Pause);
	}

	public void OnSteamUpload()
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("UIScreenSnapshot.OnSteamUpload - Steam is not initialised");
		}
		else if (!m_SteamUploadOp.IsRunning)
		{
			string text = m_NameText.text;
			SteamUploadData data = SteamUploadData.Create(SteamItemCategory.Techs, text);
			if (m_Snapshot.IsNotNull() && m_SnapshotTechData != null)
			{
				data.m_SnapshotInput = m_Snapshot;
				data.m_TechDataInput = m_SnapshotTechData;
			}
			m_SteamUploadOp.Execute(data);
			UpdateSteamUploadButton(isUploading: true);
		}
	}

	private void OnUploadCompleted(SteamUploadData data)
	{
		UpdateSteamUploadButton(isUploading: false);
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
	}

	private void OnUploadCancelled(SteamUploadData data)
	{
		UpdateSteamUploadButton(isUploading: false);
		if (!data.m_CancelledByUser)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 42);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(localisedString, delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}, localisedString2);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	public void OnGrabItUpload()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 57);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		Action accept = delegate
		{
			m_GoingToInfoScreen = false;
			Singleton.Manager<ManUI>.inst.PopScreen();
			DoGrabItUpload();
		};
		string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5);
		Action decline = delegate
		{
			m_GoingToInfoScreen = false;
			Singleton.Manager<ManUI>.inst.PopScreen();
		};
		m_GoingToInfoScreen = true;
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString, accept, decline, localisedString2, localisedString3);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
	}

	public void OnNameChanged(string name)
	{
		if (m_MessageUnchanged)
		{
			m_TweetTextField.text = GetTweetMessage(m_NameText.text);
		}
	}

	public void OnTweetMsgChanged(string msg)
	{
		if (m_MessageUnchanged && GetTweetMessage(m_NameText.text) != m_TweetTextField.text)
		{
			m_MessageUnchanged = false;
		}
	}

	public void OnSnapshotInfo()
	{
		m_GoingToInfoScreen = true;
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.WhatIsSnapshot);
	}

	public void OnTwitterInfo()
	{
		m_GoingToInfoScreen = true;
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.WhatIsTwitter);
	}

	public void OnSteamInfo()
	{
		m_GoingToInfoScreen = true;
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.WhatIsSteam);
	}

	public void OnGrabItInfo()
	{
		m_GoingToInfoScreen = true;
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.WhatIsGrabIt);
	}

	public void OnEndEdit(string name)
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Keyboard))
		{
			OnSave();
		}
	}

	private void OnSpawn()
	{
		if (Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled)
		{
			if (m_SteamUploadOp == null)
			{
				m_SteamUploadOp = new CommandOperation<SteamUploadData>();
				SteamOptionsCommand command = new SteamOptionsCommand();
				SteamCreateRenderCommand command2 = new SteamCreateRenderCommand();
				PackageCommand command3 = new PackageCommand();
				SteamCreateItemCommand command4 = new SteamCreateItemCommand();
				SteamSubmitItemCommand command5 = new SteamSubmitItemCommand();
				SteamUploadFeedbackCommand command6 = new SteamUploadFeedbackCommand();
				SteamGoToItemCommand steamGoToItemCommand = new SteamGoToItemCommand();
				m_SteamUploadOp.Add(command);
				m_SteamUploadOp.Add(command2);
				m_SteamUploadOp.Add(command3);
				m_SteamUploadOp.Add(command4);
				m_SteamUploadOp.Add(command5);
				m_SteamUploadOp.Add(command6);
				m_SteamUploadOp.AddConditional(SteamConditions.CheckGoToItem, steamGoToItemCommand);
				m_SteamUploadOp.Completed.Subscribe(OnUploadCompleted);
				m_SteamUploadOp.Cancelled.Subscribe(OnUploadCancelled);
			}
			UpdateSteamUploadButton(isUploading: false);
		}
	}

	private void OnRecycle()
	{
		DestroySnapshotTexture();
	}

	private void Update()
	{
		if (SaveDataConsoles.IsShowingSavePopupNotification())
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				m_NameText.DeactivateInputField();
			}
			else
			{
				OnSave();
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				m_NameText.text = m_PreInputName;
				m_NameText.DeactivateInputField();
			}
			else
			{
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(57) && SKU.AllowTextInput)
		{
			if (VirtualKeyboard.IsRequired())
			{
				OpenVirtualKeyboard();
			}
			else
			{
				m_NameText.Select();
				m_NameText.ActivateInputField();
			}
		}
		if (!SKU.ConsoleUI && Singleton.Manager<ManInput>.inst.GetButtonDown(58))
		{
			OnSnapshotInfo();
		}
	}

	private void OpenVirtualKeyboard()
	{
		VirtualKeyboard.PromptInput(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 3), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 4), onCompleteHandler: delegate(bool accepted, string result)
		{
			d.Log("UIScreenSnapshot - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted && !string.IsNullOrEmpty(result))
			{
				m_NameText.text = result;
				OnSave();
			}
			else
			{
				m_NameText.text = m_PreInputName;
			}
		}, defaultText: m_NameText.text);
	}

	private void DestroySnapshotTexture()
	{
		if (m_Snapshot != null)
		{
			UnityEngine.Object.Destroy(m_Snapshot);
			m_Snapshot = null;
		}
	}
}
