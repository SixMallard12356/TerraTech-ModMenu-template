#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;

public class TankDescriptionOverlay : Overlay
{
	private enum MarkerStates
	{
		Hidden,
		Visible
	}

	private enum NameStates
	{
		Hidden,
		Visible,
		Cooldown,
		Rename
	}

	private TankDescriptionData m_Data;

	private LocatorPanel m_PanelInst;

	private Tank m_Tank;

	private TrackedVisible m_Tracked;

	private bool m_IsQuestObject;

	private float m_NamePanelDisplayTime;

	private bool m_IsMarkerOnScreen;

	private bool m_IsFriendly;

	private bool m_IsPointerOverTank;

	private bool m_ForceShowName;

	private MarkerStates m_MarkerState;

	private NameStates m_NameState;

	public TankDescriptionOverlay(Tank tank, TankDescriptionData data)
	{
		m_Tank = tank;
		m_Data = data;
	}

	public override void Update()
	{
		if (m_Tank != null && m_Tracked == null)
		{
			m_Tracked = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_Tank.visible.ID);
		}
		d.Assert(m_Tank != null, "Tank Overlay has a null tank reference. Grab a coder.");
		m_IsFriendly = m_Tank != null && m_Tank.IsFriendly(Singleton.Manager<ManPlayer>.inst.PlayerTeam);
		m_IsPointerOverTank = m_Tank != null && m_Tank == Singleton.Manager<ManPointer>.inst.targetTank;
		m_ForceShowName = m_Tank.netTech.IsNotNull() && m_Tank.netTech.NetPlayer.IsNotNull() && Singleton.Manager<ManGameMode>.inst.CurrentModeOverlayShowsAllNetPlayerTechs();
		switch (m_MarkerState)
		{
		case MarkerStates.Hidden:
			MarkerHiddenUpdate();
			break;
		case MarkerStates.Visible:
			MarkerVisibleUpdate();
			break;
		}
		switch (m_NameState)
		{
		case NameStates.Hidden:
			NamePanelHiddenUpdate();
			break;
		case NameStates.Visible:
			NamePanelVisibleUpdate();
			break;
		case NameStates.Cooldown:
			NamePanelCooldownUpdate();
			break;
		case NameStates.Rename:
			NamePanelRenameUpdate();
			break;
		}
	}

	public override bool HasExpired()
	{
		return false;
	}

	public override void PerformCleanup()
	{
		if (m_PanelInst != null)
		{
			Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Unsubscribe(OnTankNameChanged);
			m_PanelInst.Recycle();
			m_PanelInst = null;
		}
	}

	private void MarkerHiddenUpdate()
	{
		if (m_Tank == null || m_Tank.IsPlayer || !m_Tank.ShouldShowOverlay || !m_Data.VisibleInCurrentMode || (m_Tank.RadarMarker.RadarMarkerConfig.IsUsed && !m_IsPointerOverTank) || (!m_ForceShowName && !CheckRange(m_Data.m_PanelMaxDisplayDistance) && !CheckIsQuestObject()))
		{
			return;
		}
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch && m_Tank.netTech != null && m_Tank.netTech.InitialSpawnShieldID != 0)
		{
			NetSpawnPoint pNSP = null;
			if (Singleton.Manager<ManNetwork>.inst.IsSpawnShieldActive(m_Tank.netTech.InitialSpawnShieldID, ref pNSP))
			{
				flag = false;
			}
		}
		if (flag)
		{
			ChangeMarkerState(MarkerStates.Visible);
		}
	}

	private void MarkerVisibleEnter()
	{
		if (m_PanelInst == null)
		{
			m_PanelInst = m_Data.m_PanelPrefab.Spawn();
		}
		m_IsMarkerOnScreen = m_PanelInst.PointToWorldPosition(m_Tank.WorldTop);
		RefreshMarker();
	}

	private void MarkerVisibleUpdate()
	{
		if (m_Tank == null)
		{
			ChangeMarkerState(MarkerStates.Hidden);
			return;
		}
		m_IsMarkerOnScreen = m_PanelInst.PointToWorldPosition(m_Tank.WorldTop);
		m_IsQuestObject = CheckIsQuestObject();
		RefreshMarker();
		if (m_IsQuestObject)
		{
			return;
		}
		if (m_Tank.IsPlayer || !m_Tank.ShouldShowOverlay || (!m_ForceShowName && !CheckRange(m_Data.m_PanelMaxDisplayDistance)) || (m_Tank.RadarMarker.RadarMarkerConfig.IsUsed && !m_IsPointerOverTank))
		{
			ChangeMarkerState(MarkerStates.Hidden);
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch && m_Tank.netTech != null && m_Tank.netTech.InitialSpawnShieldID != 0)
		{
			NetSpawnPoint pNSP = null;
			if (Singleton.Manager<ManNetwork>.inst.IsSpawnShieldActive(m_Tank.netTech.InitialSpawnShieldID, ref pNSP))
			{
				ChangeMarkerState(MarkerStates.Hidden);
			}
		}
	}

	private void MarkerVisibleExit()
	{
		m_IsMarkerOnScreen = false;
		PerformCleanup();
	}

	private void NamePanelHiddenUpdate()
	{
		if (m_MarkerState != MarkerStates.Hidden && m_IsMarkerOnScreen && CheckRange(m_Data.m_NamesDisplayDistance) && (!m_IsFriendly || m_IsPointerOverTank) && (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || m_IsPointerOverTank || !m_Tank.netTech.IsNotNull() || !m_Tank.netTech.NetPlayer.IsNotNull()))
		{
			ChangeNameState(NameStates.Visible);
		}
	}

	private void NamePanelVisibleEnter()
	{
		if (m_MarkerState == MarkerStates.Hidden || !m_IsMarkerOnScreen)
		{
			d.LogError("NamePanelVisibleEnter() called while parent object is hidden. This shouldn't happen, but object should recover.");
			ChangeNameState(NameStates.Hidden);
			return;
		}
		ResetPanelTimeout();
		RefreshNamePanel();
		m_PanelInst.SetNamesPanelVisible(visible: true);
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Subscribe(OnTankNameChanged);
	}

	private void NamePanelVisibleUpdate()
	{
		if (m_MarkerState == MarkerStates.Hidden || !m_IsMarkerOnScreen)
		{
			ChangeNameState(NameStates.Hidden);
			return;
		}
		if (m_IsFriendly)
		{
			if (CanRenameTech() && Singleton.Manager<ManInput>.inst.GetButtonDown(28) && !Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				ChangeNameState(NameStates.Rename);
				return;
			}
			if (m_IsPointerOverTank)
			{
				ResetPanelTimeout();
			}
			else if (Singleton.Manager<ManPointer>.inst.targetTank != null && Singleton.Manager<ManPointer>.inst.targetTank.IsFriendly(Singleton.Manager<ManPlayer>.inst.PlayerTeam))
			{
				ChangeNameState(NameStates.Hidden);
				return;
			}
		}
		if (m_PanelInst.PointerInside)
		{
			ResetPanelTimeout();
		}
		if (!m_ForceShowName && !UpdatePanelTimeout())
		{
			if (m_IsFriendly)
			{
				ChangeNameState(NameStates.Hidden);
			}
			else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_Tank.netTech.IsNotNull() && m_Tank.netTech.NetPlayer.IsNotNull())
			{
				ChangeNameState(NameStates.Hidden);
			}
			else
			{
				ChangeNameState(NameStates.Cooldown);
			}
		}
	}

	private void NamePanelVisibleExit()
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.SetNamesPanelVisible(visible: false);
		}
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Unsubscribe(OnTankNameChanged);
	}

	private void NamePanelCooldownUpdate()
	{
		if (m_MarkerState == MarkerStates.Hidden || !CheckRange(m_Data.m_NamesDisplayDistance) || !m_IsMarkerOnScreen)
		{
			ChangeNameState(NameStates.Hidden);
		}
	}

	private void NamePanelRenameEnter()
	{
		UIScreenRenameTech uIScreenRenameTech = (UIScreenRenameTech)Singleton.Manager<ManUI>.inst.GetScreen((m_Tank.IsNotNull() && m_Tank.RadarMarker.RadarMarkerConfig.IsUsed) ? ManUI.ScreenType.RenameTech_MarkerBlock : ManUI.ScreenType.RenameTech);
		if ((bool)uIScreenRenameTech && m_Tank.IsNotNull())
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_Tank.visible.ID);
			if (trackedVisible != null)
			{
				uIScreenRenameTech.SetSelectedTech(trackedVisible);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenRenameTech, ManUI.PauseType.Pause);
			}
		}
	}

	private void NamePanelRenameUpdate()
	{
		if (!Singleton.Manager<ManPauseGame>.inst.IsPaused)
		{
			ChangeNameState(NameStates.Visible);
		}
	}

	private bool CanRenameTech()
	{
		if (m_Tank.visible.IsInteractible && SKU.AllowTextInput)
		{
			return !Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		}
		return false;
	}

	private void OnTankNameChanged(Tank tech, TrackedVisible techTv)
	{
		if ((tech.IsNotNull() ? tech : ((!(techTv?.visible != null)) ? null : techTv?.visible.tank)) == m_Tank)
		{
			ResetPanelTimeout();
			RefreshNamePanel();
		}
	}

	private void RefreshMarker()
	{
		if (!(m_PanelInst != null))
		{
			return;
		}
		int teamID = ((m_Tracked != null) ? m_Tracked.RadarTeamID : m_Tank.Team);
		bool flag = false;
		TankDescriptionData.IconSetup iconSetup = (Tank.IsFriendly(teamID, 0) ? m_Data.m_FriendlyIconSetup : ((!Tank.IsEnemy(teamID, 0)) ? m_Data.m_NeutralIconSetup : m_Data.m_EnemyIconSetup));
		if (m_Tank.netTech != null && ManSpawn.IsPlayerTeam(m_Tank.netTech.Team))
		{
			iconSetup.m_Color = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerTechColour(m_Tank.netTech, m_Tank.netTech.Team, iconSetup.m_Color);
		}
		AICategories aICategories = m_Tank.AI.GetAICategory();
		if (!m_Tank.IsFriendly() && aICategories != AICategories.AIFlee)
		{
			aICategories = AICategories.Null;
		}
		iconSetup.m_Sprite = Singleton.Manager<ManUI>.inst.GetAICategoryIcon(aICategories);
		Sprite pinSprite = ((m_Tank.IsAnchored && m_IsMarkerOnScreen) ? m_Data.m_PinSpriteAnchored : m_Data.m_PinSprite);
		Color pinColour = iconSetup.m_Color;
		if (m_IsQuestObject)
		{
			if (m_Data.m_QuestMarkerIconSprite != null)
			{
				iconSetup.m_Sprite = m_Data.m_QuestMarkerIconSprite;
			}
			if (m_Data.m_QuestMarkerUseIconColour)
			{
				iconSetup.m_Color = m_Data.m_QuestMarkerIconColour;
			}
			if (m_Data.m_QuestMarkerUsePinColour)
			{
				pinColour = m_Data.m_QuestMarkerPinColour;
			}
		}
		TechWeapon.ManualTargetingReticuleState manualTargetingReticuleState = TechWeapon.ManualTargetingReticuleState.NotTargeted;
		if ((bool)Singleton.playerTank && (bool)Singleton.playerTank.Weapons)
		{
			manualTargetingReticuleState = Singleton.playerTank.Weapons.GetManualTargetingReticuleState(m_Tank.visible);
		}
		if (m_Tank.netTech.IsNotNull() && m_Tank.netTech.NetPlayer.IsNotNull())
		{
			iconSetup.m_Sprite = m_Tank.netTech.NetPlayer.Sprite;
			iconSetup.m_Color = Color.white;
			flag = m_ForceShowName && m_Tank != Singleton.playerTank && !m_IsMarkerOnScreen;
		}
		if (flag)
		{
			int distance = Mathf.FloorToInt((Singleton.playerPos - m_Tank.boundsCentreWorldNoCheck).magnitude);
			m_PanelInst.SetDistance(distance, show: true);
		}
		else
		{
			m_PanelInst.SetDistance(0, show: false);
		}
		m_PanelInst.Format(iconSetup.m_Sprite, iconSetup.m_Color, pinSprite, pinColour, manualTargetingReticuleState);
	}

	private void RefreshNamePanel()
	{
		if (!(m_PanelInst != null))
		{
			return;
		}
		if (m_Tank.netTech != null && m_Tank.netTech.NetPlayer != null)
		{
			m_PanelInst.TopName = m_Tank.netTech.NetPlayer.name;
		}
		else
		{
			m_PanelInst.TopName = m_Tank.name;
		}
		m_PanelInst.BottomColor = m_Data.m_TextDefaultColour;
		if (m_IsFriendly)
		{
			if (CanRenameTech() && !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				string keyBoundPrimaryName = Singleton.Manager<ManInput>.inst.GetKeyBoundPrimaryName(28);
				if (keyBoundPrimaryName.NullOrEmpty())
				{
					m_PanelInst.BottomName = string.Empty;
					return;
				}
				m_PanelInst.BottomColor = m_Data.m_TextHintColour;
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 4);
				m_PanelInst.BottomName = string.Format(localisedString, keyBoundPrimaryName);
			}
			else
			{
				m_PanelInst.BottomName = string.Empty;
			}
		}
		else if (!m_Tank.m_Creator.NullOrEmpty())
		{
			m_PanelInst.BottomName = m_Tank.m_Creator;
		}
		else
		{
			m_PanelInst.BottomName = string.Empty;
		}
	}

	private bool CheckRange(float maxDist)
	{
		return (Singleton.cameraTrans.position - m_Tank.trans.position).sqrMagnitude <= maxDist * maxDist;
	}

	private bool CheckIsQuestObject()
	{
		bool result = false;
		if (Singleton.Manager<ManQuestLog>.inst.HasTrackedEncounter && m_Tracked != null)
		{
			QuestLogData questLogData = Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData()?.ActiveQuestLog;
			if (questLogData != null)
			{
				result = m_Tracked == questLogData.GetEncounterWaypoint();
			}
		}
		return result;
	}

	private void ResetPanelTimeout()
	{
		m_NamePanelDisplayTime = (m_IsFriendly ? m_Data.m_NameDisplayTimePlayer : m_Data.m_NameDisplayTimeEnemy);
	}

	private bool UpdatePanelTimeout()
	{
		if (m_NamePanelDisplayTime < 0f)
		{
			return true;
		}
		m_NamePanelDisplayTime = Mathf.Max(m_NamePanelDisplayTime - Time.deltaTime, 0f);
		return m_NamePanelDisplayTime > 0f;
	}

	private void ChangeMarkerState(MarkerStates nextState)
	{
		if (nextState != m_MarkerState)
		{
			MarkerStates markerState = m_MarkerState;
			if (markerState != MarkerStates.Hidden && markerState == MarkerStates.Visible)
			{
				MarkerVisibleExit();
			}
			m_MarkerState = nextState;
			markerState = m_MarkerState;
			if (markerState != MarkerStates.Hidden && markerState == MarkerStates.Visible)
			{
				MarkerVisibleEnter();
			}
		}
	}

	private void ChangeNameState(NameStates nextState)
	{
		if (nextState != m_NameState)
		{
			switch (m_NameState)
			{
			case NameStates.Visible:
				NamePanelVisibleExit();
				break;
			}
			m_NameState = nextState;
			switch (m_NameState)
			{
			case NameStates.Visible:
				NamePanelVisibleEnter();
				break;
			case NameStates.Rename:
				NamePanelRenameEnter();
				break;
			case NameStates.Hidden:
			case NameStates.Cooldown:
				break;
			}
		}
	}
}
