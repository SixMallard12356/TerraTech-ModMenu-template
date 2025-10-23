#define UNITY_EDITOR
using System;
using Snapshots;
using UnityEngine;

public class ManBtnPrompt : Singleton.Manager<ManBtnPrompt>
{
	public enum PromptType
	{
		none,
		FreeDriving,
		PauseMenu,
		SaveGame,
		Shop,
		FabricatorMenu,
		ComponentMenu,
		RadialMenus,
		MissionLog,
		MissionBoard,
		InteractionMode,
		BuildBeam,
		Inventory,
		TechLoader,
		TechLoaderViewOptions,
		SnapshotPanel,
		TechRespawnScreen,
		ItemDragging,
		ContextPickup,
		ContextDismissHint,
		ContextInfo,
		ContextTechOptions,
		ContextUndo,
		ContextAttach,
		ContextShowReticule,
		ContextDetach,
		MainMenu,
		NewGameSelectingModes,
		NewGameCampaign,
		NewGameCreative,
		GauntletTrackSelect,
		LoadGame,
		MultiplayerEntry,
		MultiplayerFindGame,
		Options,
		Credits,
		ContextAnchor,
		ContextToggle,
		ContextPlay,
		ContextRandomise,
		ContextSetManually,
		ContextSelect,
		ContextClose,
		ContextScroll,
		ContextInteractMenu,
		ContextOpenRadialMenu,
		ContextOpenInteractMenu,
		ContextRotateBlock,
		SaveGameRename,
		ContextGrab,
		ContextRelease,
		ContextBuildBeam,
		ContextTogglePalette,
		ContextBuildBeamNudge,
		ContextCraftingCraft,
		ContextCraftingCancel,
		ContextChangeCorp,
		ContextHideReticule,
		MultiplayerMenu,
		MultiplayerLobbyList,
		MultiplayerLobbyView,
		ContextKick,
		MultiplayerScoreboard,
		ContextMute,
		ContextUnmute,
		ContextKickScoreboard,
		TechLoaderBlocksPanel,
		TechLoaderPlacingTech,
		ContextTechLoaderDeploy,
		ContextTechLoaderReplace,
		ContextShowXboxProfileCard,
		ControlScheme,
		ContextSchemeRestore,
		ContextSchemeDelete,
		SkinsPalette,
		ContextSchemeClear,
		GamepadQuickMenu,
		TechManager,
		PlayerList,
		SaveGameScreenRenameOption,
		WorldMap,
		WorldMapAddPinContext,
		WorldMapRemovePinContext,
		BlockContextMenu,
		ContextBlockContextMenuSliderSelected
	}

	[Serializable]
	public class PromptData
	{
		public LocalisedString[] prompts;
	}

	[SerializeField]
	[EnumArray(typeof(PromptType))]
	private PromptData[] m_PromptData;

	[SerializeField]
	private CoreEncounterCompletedCondition m_FreeDrivePromptsEncounterCompletedCondition;

	public UIBtnPromptElement m_BtnPromptPrefab;

	private PromptData m_Context;

	private PromptType m_CurrentPromptType;

	private UIHUDElement m_CurrentHUD;

	private UIBtnPrompts m_BtnPrompts;

	private const float kUpdateInterval = 0.1f;

	private float m_TimeSinceLastUpdate;

	public void ShowHUDPrompt(PromptType type)
	{
		if (m_CurrentPromptType != type)
		{
			if (m_BtnPrompts == null)
			{
				m_BtnPrompts = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.ButtonPrompt) as UIBtnPrompts;
			}
			m_CurrentPromptType = type;
			PromptData prompts = m_PromptData[(int)type];
			DisplayPrompt(prompts);
		}
	}

	public void HideHUDPrompt()
	{
		m_CurrentPromptType = PromptType.none;
		if (m_BtnPrompts != null && m_BtnPrompts.IsVisible)
		{
			m_BtnPrompts.Hide(m_Context);
		}
	}

	public void UpdateCurrentHUDPrompt(PromptType promptType)
	{
		if (m_BtnPrompts != null)
		{
			PromptData promptData = m_PromptData[(int)promptType];
			m_BtnPrompts.UpdateCurrentPrompt(promptData.prompts[0]);
		}
	}

	public void HidePromptType(PromptType promptType)
	{
		if (m_BtnPrompts != null)
		{
			PromptData promptData = m_PromptData[(int)promptType];
			m_BtnPrompts.ToggleBtnPrompt(active: false, promptData.prompts[0].m_InlineGlyphs);
		}
	}

	public void HidePromptForced()
	{
		HideHUDPrompt();
	}

	public void ToggleBtnPrompt(bool active, Localisation.GlyphInfo[] rewiredActionIds)
	{
		if (m_BtnPrompts != null)
		{
			m_BtnPrompts.ToggleBtnPrompt(active, rewiredActionIds);
		}
	}

	public PromptData GetPromptDataByType(PromptType promptType)
	{
		return m_PromptData[(int)promptType];
	}

	public PromptData GetScreenPromptData(ManUI.ScreenType screenType)
	{
		PromptData result = null;
		switch (screenType)
		{
		case ManUI.ScreenType.MainMenu:
			result = m_PromptData[26];
			break;
		case ManUI.ScreenType.Options:
			result = m_PromptData[34];
			break;
		case ManUI.ScreenType.Pause:
			result = m_PromptData[2];
			break;
		case ManUI.ScreenType.NameVehicle:
			result = m_PromptData[15];
			break;
		case ManUI.ScreenType.LoadSave:
			result = m_PromptData[31];
			break;
		case ManUI.ScreenType.About:
			result = m_PromptData[35];
			break;
		case ManUI.ScreenType.NewGame:
			result = m_PromptData[27];
			break;
		case ManUI.ScreenType.RespawnTechChoice:
			result = m_PromptData[16];
			break;
		case ManUI.ScreenType.SaveGame:
			result = m_PromptData[3];
			break;
		case ManUI.ScreenType.SaveGameRename:
			result = m_PromptData[48];
			break;
		case ManUI.ScreenType.MultiplayerSetupTEMP:
			result = m_PromptData[58];
			break;
		case ManUI.ScreenType.MatchmakingLobbyList:
			result = m_PromptData[59];
			break;
		case ManUI.ScreenType.MatchmakingLobbyScreen:
			result = m_PromptData[60];
			break;
		case ManUI.ScreenType.MultiplayerScoreboard:
			result = m_PromptData[62];
			break;
		case ManUI.ScreenType.ControlSchema:
			result = m_PromptData[71];
			break;
		default:
			d.LogWarning($"The requested screen {screenType} does not support button prompts.");
			break;
		case ManUI.ScreenType.Fabricator:
		case ManUI.ScreenType.DeliveryCannon:
		case ManUI.ScreenType.SelectChallengeMenu:
		case ManUI.ScreenType.LoadVehiclesMenu:
		case ManUI.ScreenType.Refinery:
		case ManUI.ScreenType.RacingTrackSelect:
		case ManUI.ScreenType.FlyingTrackSelect:
		case ManUI.ScreenType.BaseHelper:
		case ManUI.ScreenType.SelectLoadOption:
		case ManUI.ScreenType.OptionsSound:
		case ManUI.ScreenType.OptionsControls:
		case ManUI.ScreenType.OptionsGraphics:
		case ManUI.ScreenType.OptionsCamera:
		case ManUI.ScreenType.ExitConfirmMenu:
		case ManUI.ScreenType.DefenseOver:
		case ManUI.ScreenType.SendInvader:
		case ManUI.ScreenType.UserSelect:
		case ManUI.ScreenType.HumbleUpdateNotification:
		case ManUI.ScreenType.NotificationScreen:
		case ManUI.ScreenType.Shop:
		case ManUI.ScreenType.Multiplayer:
		case ManUI.ScreenType.BugReport:
		case ManUI.ScreenType.CanaryLogin:
		case ManUI.ScreenType.StartNew:
		case ManUI.ScreenType.PauseChallenge:
		case ManUI.ScreenType.EnterName:
		case ManUI.ScreenType.LeaderBoard:
		case ManUI.ScreenType.GauntletAttract:
		case ManUI.ScreenType.GauntletReplay:
		case ManUI.ScreenType.NewUser:
		case ManUI.ScreenType.InvaderSentTechChoice:
		case ManUI.ScreenType.RenameTech:
		case ManUI.ScreenType.TechLoaderScreen:
		case ManUI.ScreenType.RenameSchema:
		case ManUI.ScreenType.RenameTech_MarkerBlock:
			break;
		}
		return result;
	}

	private void DisplayPrompt(PromptData prompts)
	{
		m_Context = prompts;
		if (m_BtnPrompts != null)
		{
			m_BtnPrompts.Show(m_Context);
		}
	}

	private void UpdateFreeDrivingPrompts()
	{
		if (Singleton.playerTank != null && (!Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() || m_FreeDrivePromptsEncounterCompletedCondition.Passes()))
		{
			ShowHUDPrompt(PromptType.FreeDriving);
			if (Singleton.Manager<ManPointer>.inst.targetVisible != null && Singleton.Manager<ManPointer>.inst.ItemIsGrabbable(Singleton.Manager<ManPointer>.inst.targetVisible))
			{
				UpdateCurrentHUDPrompt(PromptType.ContextPickup);
			}
			else
			{
				HidePromptType(PromptType.ContextPickup);
			}
			UpdatePaletteTogglePrompt();
			UpdateShowReticulePrompt();
			UpdateBuildBeamContextualPrompt();
		}
		else
		{
			HideHUDPrompt();
		}
	}

	private void UpdateInteractionModePrompts()
	{
		ShowHUDPrompt(PromptType.InteractionMode);
		Visible targetVisible = Singleton.Manager<ManPointer>.inst.targetVisible;
		TankBlock targetBlock = Singleton.Manager<ManPointer>.inst.targetBlock;
		bool num = Singleton.Manager<ManPointer>.inst.ItemIsGrabbable(targetVisible);
		bool flag = targetBlock.IsNotNull() && targetBlock.HasAccessibleContextMenu(includeRadial: false);
		bool flag2 = targetBlock.IsNotNull() && targetBlock.HasAccessibleContextMenu(includeRadial: true, includeInteract: false);
		if (num)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextGrab);
		}
		else if (flag)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextInteractMenu);
		}
		else
		{
			HidePromptType(PromptType.ContextGrab);
		}
		if (flag)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextOpenInteractMenu);
		}
		else
		{
			UpdatePaletteTogglePrompt();
		}
		UpdateBuildBeamContextualPrompt();
		UpdateCurrentHUDPrompt(PromptType.ContextHideReticule);
		if (targetVisible != null && (targetVisible.block == null || targetBlock.tank == null || targetBlock.tank.IsFriendly(0)))
		{
			UpdateCurrentHUDPrompt(PromptType.ContextInfo);
		}
		else
		{
			HidePromptType(PromptType.ContextInfo);
		}
		if (flag2)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextOpenRadialMenu);
		}
		else if (Singleton.Manager<ManPointer>.inst.targetTank != null && Singleton.Manager<ManPointer>.inst.targetTank.IsFriendly(0))
		{
			UpdateCurrentHUDPrompt(PromptType.ContextTechOptions);
		}
		else
		{
			HidePromptType(PromptType.ContextTechOptions);
		}
		UpdateUndoContextualPrompt();
	}

	private void UpdateItemDraggingPrompts()
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem != null)
		{
			ShowHUDPrompt(PromptType.ItemDragging);
			UpdateItemDraggingContextualPrompts();
			UpdateUndoContextualPrompt();
		}
		else
		{
			HideHUDPrompt();
		}
	}

	private void UpdateInventoryBuildingPrompts()
	{
		ShowHUDPrompt(PromptType.Inventory);
		UpdateItemDraggingContextualPrompts();
		UpdateUndoContextualPrompt();
	}

	private void UpdateBlockBuildingPrompts_deprecated()
	{
		if (!Singleton.Manager<ManHUD>.inst.CurrentHUD.IsHudElementExpanded(ManHUD.HUDElementType.BlockPalette) && !Singleton.playerTank.beam.IsLocked)
		{
			ShowHUDPrompt(PromptType.BuildBeam);
			if (Singleton.Manager<ManUndo>.inst.UndoAvailable)
			{
				UpdateCurrentHUDPrompt(PromptType.ContextUndo);
			}
			else
			{
				HidePromptType(PromptType.ContextUndo);
			}
			if (Singleton.Manager<ManControllerTechBuilder>.inst.m_InBuildMode)
			{
				bool flag = Singleton.Manager<ManControllerTechBuilder>.inst.SelectedBlock != null && Singleton.Manager<ManPointer>.inst.DraggingItem == null;
				bool flag2 = Singleton.Manager<ManControllerTechBuilder>.inst.SelectedBlock != null && Singleton.Manager<ManPointer>.inst.DraggingItem != null;
				if (flag)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextDetach);
				}
				else if (flag2)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextRotateBlock);
				}
				else
				{
					HidePromptType(PromptType.ContextDetach);
				}
				if (!flag2)
				{
					HidePromptType(PromptType.ContextRotateBlock);
				}
				if (!Singleton.Manager<ManPlayer>.inst.PaletteUnlocked)
				{
					HidePromptType(PromptType.ContextTogglePalette);
				}
				if (Singleton.Manager<ManControllerTechBuilder>.inst.CurrentPlacement != null && flag2)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextAttach);
				}
				else if (Singleton.Manager<ManControllerTechBuilder>.inst.NearbyPickupBlock != null && flag)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextGrab);
				}
				else
				{
					HidePromptType(PromptType.ContextGrab);
				}
			}
		}
		else
		{
			HideHUDPrompt();
		}
	}

	private void UpdateItemDraggingContextualPrompts()
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextRotateBlock);
		}
		else
		{
			HidePromptType(PromptType.ContextRotateBlock);
		}
		if (Singleton.Manager<ManPointer>.inst.DraggingItem != null)
		{
			if (Singleton.Manager<ManTechBuilder>.inst.PlacementReadyToAttach != null)
			{
				UpdateCurrentHUDPrompt(PromptType.ContextAttach);
			}
			else if (Singleton.Manager<ManTechBuilder>.inst.DraggingAnchor != null && Singleton.Manager<ManTechBuilder>.inst.DraggingAnchor.WouldAnchorToGround())
			{
				UpdateCurrentHUDPrompt(PromptType.ContextAnchor);
			}
			else
			{
				UpdateCurrentHUDPrompt(PromptType.ContextRelease);
			}
		}
		else
		{
			HidePromptType(PromptType.ContextAttach);
		}
		UpdatePaletteTogglePrompt();
		UpdateShowReticulePrompt();
		UpdateBuildBeamContextualPrompt();
	}

	private void UpdatePaletteTogglePrompt()
	{
		if (Singleton.playerTank != null && Singleton.Manager<ManPurchases>.inst.IsPaletteAvailable())
		{
			UpdateCurrentHUDPrompt(PromptType.ContextTogglePalette);
		}
		else
		{
			HidePromptType(PromptType.ContextTogglePalette);
		}
	}

	private void UpdateShowReticulePrompt()
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == null || Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextShowReticule);
		}
		else
		{
			HidePromptType(PromptType.ContextShowReticule);
		}
	}

	private void UpdateUndoContextualPrompt()
	{
		if (Singleton.Manager<ManUndo>.inst.UndoAvailable)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextUndo);
		}
		else
		{
			HidePromptType(PromptType.ContextUndo);
		}
	}

	private void UpdateBuildBeamContextualPrompt()
	{
		if (Singleton.playerTank != null && !Singleton.playerTank.beam.IsLocked)
		{
			UpdateCurrentHUDPrompt(PromptType.ContextBuildBeam);
			if (Singleton.playerTank.beam.IsActive && !Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled && !Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded())
			{
				UpdateCurrentHUDPrompt(PromptType.ContextBuildBeamNudge);
			}
			else
			{
				HidePromptType(PromptType.ContextBuildBeamNudge);
			}
		}
		else
		{
			HidePromptType(PromptType.ContextBuildBeam);
		}
	}

	private void UpdateRadialMenuPrompts()
	{
		ShowHUDPrompt(PromptType.RadialMenus);
	}

	private void UpdateMissionLogContextPrompts()
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeCampaign())
		{
			if ((Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionLog) as UIMissionLogFullHUD).CanCancelMission())
			{
				ToggleBtnPrompt(active: true, new Localisation.GlyphInfo[1]
				{
					new Localisation.GlyphInfo(58)
				});
			}
			else
			{
				ToggleBtnPrompt(active: false, new Localisation.GlyphInfo[1]
				{
					new Localisation.GlyphInfo(58)
				});
			}
		}
	}

	private void UpdateItemMenuContextPrompts()
	{
		UIItemRecipeSelect uIItemRecipeSelect = null;
		if (m_CurrentHUD != null && m_CurrentHUD.HudElementType == ManHUD.HUDElementType.BlockRecipeSelect)
		{
			uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockRecipeSelect) as UIItemRecipeSelect;
		}
		else if (m_CurrentHUD != null && m_CurrentHUD.HudElementType == ManHUD.HUDElementType.ComponentRecipeSelect)
		{
			uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.ComponentRecipeSelect) as UIItemRecipeSelect;
		}
		if (uIItemRecipeSelect != null)
		{
			if (uIItemRecipeSelect.BuiltItemWasSelected)
			{
				UpdateCurrentHUDPrompt(PromptType.ContextCraftingCancel);
			}
			else
			{
				UpdateCurrentHUDPrompt(PromptType.ContextCraftingCraft);
			}
			return;
		}
		UIShopBlockSelect uIShopBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockShop) as UIShopBlockSelect;
		if (uIShopBlockSelect != null && uIShopBlockSelect.IsVisible)
		{
			if (uIShopBlockSelect.ShowAllCorps)
			{
				UpdateCurrentHUDPrompt(PromptType.ContextChangeCorp);
			}
			else
			{
				HidePromptType(PromptType.ContextChangeCorp);
			}
		}
	}

	private void UpdateSnapshotPanelContextPrompts()
	{
		UISnapshotsPanelHUD uISnapshotsPanelHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as UISnapshotsPanelHUD;
		if (!(uISnapshotsPanelHUD != null))
		{
			return;
		}
		if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.Placing)
		{
			ShowHUDPrompt(PromptType.TechLoaderPlacingTech);
		}
		else
		{
			if (!uISnapshotsPanelHUD.IsVisible)
			{
				return;
			}
			switch (uISnapshotsPanelHUD.GetFocusTarget())
			{
			case VMSnapshotPanel.FocusTarget.Snapshots:
				ShowHUDPrompt(PromptType.TechLoader);
				if (!uISnapshotsPanelHUD.GetIsASnapshotCurrentlySelected())
				{
					HidePromptType(PromptType.ContextTechLoaderReplace);
					HidePromptType(PromptType.ContextTechLoaderDeploy);
					break;
				}
				if (Singleton.Manager<ManSnapshots>.inst.m_SwapAvailableInMode.Value)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextTechLoaderReplace);
				}
				else
				{
					HidePromptType(PromptType.ContextTechLoaderReplace);
				}
				if (Singleton.Manager<ManSnapshots>.inst.m_PlacementAvailable.Value)
				{
					UpdateCurrentHUDPrompt(PromptType.ContextTechLoaderDeploy);
				}
				else
				{
					HidePromptType(PromptType.ContextTechLoaderDeploy);
				}
				break;
			case VMSnapshotPanel.FocusTarget.Settings:
			case VMSnapshotPanel.FocusTarget.Actions:
			case VMSnapshotPanel.FocusTarget.Search:
				ShowHUDPrompt(PromptType.TechLoaderViewOptions);
				break;
			case VMSnapshotPanel.FocusTarget.Blocks:
				ShowHUDPrompt(PromptType.TechLoaderBlocksPanel);
				break;
			default:
				d.LogErrorFormat("TechLoader FocusTarget type {0} was not supported by Button Prompts!", uISnapshotsPanelHUD.GetFocusTarget());
				break;
			}
		}
	}

	private void UpdateControlSchemePrompt()
	{
		ShowHUDPrompt(PromptType.ControlScheme);
	}

	private void UpdateWorldMapContextPrompts()
	{
		UIHUDWorldMap uIHUDWorldMap = null;
		if (m_CurrentHUD != null && m_CurrentHUD.HudElementType == ManHUD.HUDElementType.WorldMap)
		{
			uIHUDWorldMap = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.WorldMap) as UIHUDWorldMap;
		}
		bool flag = false;
		if (uIHUDWorldMap != null)
		{
			GameObject cursorGO = uIHUDWorldMap.TryGetCursorTarget();
			flag = uIHUDWorldMap.TryGetWaypoint(cursorGO) != null;
		}
		if (flag)
		{
			UpdateCurrentHUDPrompt(PromptType.WorldMapRemovePinContext);
		}
		else
		{
			UpdateCurrentHUDPrompt(PromptType.WorldMapAddPinContext);
		}
	}

	private void OnShowHUD(UIHUDElement element)
	{
		bool flag = true;
		switch (element.HudElementType)
		{
		case ManHUD.HUDElementType.TechLoader:
			ShowHUDPrompt(PromptType.TechLoader);
			break;
		case ManHUD.HUDElementType.BlockShop:
			ShowHUDPrompt(PromptType.Shop);
			break;
		case ManHUD.HUDElementType.MissionLog:
			ShowHUDPrompt(PromptType.MissionLog);
			break;
		case ManHUD.HUDElementType.BlockRecipeSelect:
			ShowHUDPrompt(PromptType.FabricatorMenu);
			break;
		case ManHUD.HUDElementType.MissionBoard:
			ShowHUDPrompt(PromptType.MissionBoard);
			break;
		case ManHUD.HUDElementType.ComponentRecipeSelect:
			ShowHUDPrompt(PromptType.ComponentMenu);
			break;
		case ManHUD.HUDElementType.SkinsPalette:
			ShowHUDPrompt(PromptType.SkinsPalette);
			break;
		case ManHUD.HUDElementType.GamepadQuickMenu:
			ShowHUDPrompt(PromptType.GamepadQuickMenu);
			break;
		case ManHUD.HUDElementType.TechManager:
			ShowHUDPrompt(PromptType.TechManager);
			break;
		case ManHUD.HUDElementType.PlayerInfo:
			ShowHUDPrompt(PromptType.PlayerList);
			break;
		case ManHUD.HUDElementType.ScoreBoard:
			ShowHUDPrompt(PromptType.MultiplayerScoreboard);
			break;
		case ManHUD.HUDElementType.TeleportMenu:
			ShowHUDPrompt(PromptType.PauseMenu);
			break;
		case ManHUD.HUDElementType.WorldMap:
			ShowHUDPrompt(PromptType.WorldMap);
			break;
		case ManHUD.HUDElementType.BlockOptionsContextMenu:
			ShowHUDPrompt(PromptType.BlockContextMenu);
			break;
		case ManHUD.HUDElementType.Radar:
		case ManHUD.HUDElementType.ModeScore:
		case ManHUD.HUDElementType.ModeTimer:
		case ManHUD.HUDElementType.Announcement:
		case ManHUD.HUDElementType.MoneyCounter:
		case ManHUD.HUDElementType.CheckpointChallenge:
		case ManHUD.HUDElementType.FlyingChallenge:
		case ManHUD.HUDElementType.RaDTestChamber:
		case ManHUD.HUDElementType.Snapshot:
		case ManHUD.HUDElementType.ResetPosition:
		case ManHUD.HUDElementType.TweetThis:
		case ManHUD.HUDElementType.GetBeta:
		case ManHUD.HUDElementType.TwitchButtonTT:
		case ManHUD.HUDElementType.TwitchButtonOther:
		case ManHUD.HUDElementType.TwitchStreamList:
		case ManHUD.HUDElementType.InfoBox:
		case ManHUD.HUDElementType.RestartTutorial:
		case ManHUD.HUDElementType.UndoButton:
		case ManHUD.HUDElementType.NetworkPlayerList:
		case ManHUD.HUDElementType.Corporation:
		case ManHUD.HUDElementType.Speedo:
		case ManHUD.HUDElementType.Altimeter:
		case ManHUD.HUDElementType.BlockMenuSelection:
		case ManHUD.HUDElementType.StartChallenge:
		case ManHUD.HUDElementType.TechSavedMessage:
		case ManHUD.HUDElementType.BouncingArrow:
		case ManHUD.HUDElementType.PrintEnabledIcon:
		case ManHUD.HUDElementType.LaunchTutorialButton:
		case ManHUD.HUDElementType.TechLoaderButton:
		case ManHUD.HUDElementType.TechControlChoice:
		case ManHUD.HUDElementType.TechControlChoiceSetAITarget:
		case ManHUD.HUDElementType.GrabItIcon:
		case ManHUD.HUDElementType.LicenceLevelUp:
		case ManHUD.HUDElementType.LicenceMaxedNotification:
		case ManHUD.HUDElementType.HUDMissionTracker:
		case ManHUD.HUDElementType.FactionLicences:
		case ManHUD.HUDElementType.BlockPalette:
		case ManHUD.HUDElementType.TechShop:
		case ManHUD.HUDElementType.HUDMask:
		case ManHUD.HUDElementType.FilterMenu:
		case ManHUD.HUDElementType.GrabItNotification:
		case ManHUD.HUDElementType.Hint:
		case ManHUD.HUDElementType.HintFloating:
		case ManHUD.HUDElementType.Sumo:
		case ManHUD.HUDElementType.Pacemaker:
		case ManHUD.HUDElementType.AnchorTech:
		case ManHUD.HUDElementType.FuelGauge:
		case ManHUD.HUDElementType.PowerGauge:
		case ManHUD.HUDElementType.Multiplayer:
		case ManHUD.HUDElementType.OutOfBounds:
		case ManHUD.HUDElementType.Score:
		case ManHUD.HUDElementType.MPTimeRemaining:
		case ManHUD.HUDElementType.MPKillStreakClaimReward:
		case ManHUD.HUDElementType.MPKillStreakClaimRewardBeingClaimed:
		case ManHUD.HUDElementType.SelfDestruct:
		case ManHUD.HUDElementType.InteractionMode:
		case ManHUD.HUDElementType.TechAndBlockActions:
		case ManHUD.HUDElementType.MPChat:
		case ManHUD.HUDElementType.ButtonPrompt:
		case ManHUD.HUDElementType.BlockLimit:
		case ManHUD.HUDElementType.MPTechActions:
		case ManHUD.HUDElementType.ConveyorMenu:
		case ManHUD.HUDElementType._deprecated_HoverControl:
		case ManHUD.HUDElementType.BlockControl:
		case ManHUD.HUDElementType.GyroControl:
		case ManHUD.HUDElementType.TrimControl:
		case ManHUD.HUDElementType.ControlSchema:
		case ManHUD.HUDElementType.SkinsPaletteButton:
		case ManHUD.HUDElementType.CoopPlayerInfo:
		case ManHUD.HUDElementType.TechManagerButton:
		case ManHUD.HUDElementType.BlockLimiterWarning:
		case ManHUD.HUDElementType.VoiceIndicator:
		case ManHUD.HUDElementType.MPMissionUpdates:
		case ManHUD.HUDElementType.BlockControlOnOff:
		case ManHUD.HUDElementType.TechControlChoice_RadarMarker:
		case ManHUD.HUDElementType.WorldMapButton:
		case ManHUD.HUDElementType.SliderControlRadialMenu:
		case ManHUD.HUDElementType.PowerToggleBlockMenu:
		case ManHUD.HUDElementType.SimpleOnOffRadial:
			flag = false;
			break;
		default:
			d.LogErrorFormat("The requested HUD '{0}' does not support button prompts.", element.HudElementType);
			flag = false;
			break;
		}
		if (flag)
		{
			m_CurrentHUD = element;
		}
	}

	private void OnHideHUD(UIHUDElement element)
	{
		if (m_CurrentHUD != null && m_CurrentHUD.HudElementType == element.HudElementType)
		{
			HideHUDPrompt();
			m_CurrentHUD = null;
		}
	}

	private void OnModeCleanup(Mode newMode)
	{
		HideHUDPrompt();
		m_CurrentHUD = null;
		m_BtnPrompts = null;
	}

	private void Start()
	{
		if (SKU.ConsoleUI)
		{
			Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Subscribe(OnShowHUD);
			Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Subscribe(OnHideHUD);
			Singleton.Manager<ManHUD>.inst.OnExpandHUDElementEvent.Subscribe(OnShowHUD);
			Singleton.Manager<ManHUD>.inst.OnCollapseHUDElementEvent.Subscribe(OnHideHUD);
			Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		}
	}

	private void OnDestroy()
	{
		if (SKU.ConsoleUI)
		{
			Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Unsubscribe(OnShowHUD);
			Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Unsubscribe(OnHideHUD);
			Singleton.Manager<ManHUD>.inst.OnExpandHUDElementEvent.Unsubscribe(OnShowHUD);
			Singleton.Manager<ManHUD>.inst.OnCollapseHUDElementEvent.Unsubscribe(OnHideHUD);
			Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Unsubscribe(OnModeCleanup);
		}
	}

	private void LateUpdate()
	{
		m_TimeSinceLastUpdate += Time.deltaTime;
		if (m_TimeSinceLastUpdate < 0.1f)
		{
			return;
		}
		m_TimeSinceLastUpdate = 0f;
		bool flag = !Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() || m_FreeDrivePromptsEncounterCompletedCondition.Passes();
		if (m_BtnPrompts.IsNotNull() && m_BtnPrompts.IsShowing)
		{
			m_BtnPrompts.SetVisible(Singleton.Manager<ManUI>.inst.IsStackEmpty());
		}
		if (SKU.ConsoleUI && Singleton.Manager<ManUI>.inst.IsStackEmpty() && Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame && flag)
		{
			UIInputMode currentUIInputMode = Singleton.Manager<ManInput>.inst.GetCurrentUIInputMode();
			switch (currentUIInputMode)
			{
			case UIInputMode.Default:
				UpdateFreeDrivingPrompts();
				break;
			case UIInputMode.Interaction:
				UpdateInteractionModePrompts();
				break;
			case UIInputMode.ItemDragging:
				UpdateItemDraggingPrompts();
				break;
			case UIInputMode.UIInventoryPanel:
				UpdateInventoryBuildingPrompts();
				break;
			case UIInputMode.Radial:
				UpdateRadialMenuPrompts();
				break;
			case UIInputMode.UIMissionLog:
				UpdateMissionLogContextPrompts();
				break;
			case UIInputMode.UIItemMenu:
				UpdateItemMenuContextPrompts();
				break;
			case UIInputMode.BlockBuilding:
				UpdateBlockBuildingPrompts_deprecated();
				break;
			case UIInputMode.ControlScheme:
				UpdateControlSchemePrompt();
				break;
			case UIInputMode.UIDeployTechPanel:
			case UIInputMode.UITechLoaderPanel:
				UpdateSnapshotPanelContextPrompts();
				break;
			case UIInputMode.WorldMap:
				UpdateWorldMapContextPrompts();
				break;
			default:
				d.LogErrorFormat("Unhandled UIInputMode '{0}' in ManBtnPrompt.", currentUIInputMode);
				break;
			case UIInputMode.FullscreenUI:
			case UIInputMode.UISkinsPalettePanel:
				break;
			}
		}
	}
}
