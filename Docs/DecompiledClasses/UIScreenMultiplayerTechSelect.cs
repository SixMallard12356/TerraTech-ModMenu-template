#define UNITY_EDITOR
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenMultiplayerTechSelect : UIScreen
{
	private enum SelectStates
	{
		Init,
		Select,
		Confirm,
		Ready,
		Done
	}

	[SerializeField]
	private Text m_TitleTextField;

	[SerializeField]
	private Button m_ReadyButton;

	[SerializeField]
	private UIMultiplayerTechSelectReady m_Ready;

	[SerializeField]
	private UIMultiplayerTechSelectItem[] m_CorpItems;

	[SerializeField]
	private Text m_StartingTechSelectionTimer;

	[SerializeField]
	private Text m_GameStartedText;

	[SerializeField]
	private List<FactionSubTypes> m_CorpItemsMapping;

	private int m_CorporationSelected;

	private int m_BlockPaletteSelected;

	private int m_SkinSelected;

	private List<MultiplayerTechSelectPresetAsset> m_CorporationPresets;

	private const int kMaxCorporations = 4;

	private const int kMaxBlockPalettes = 2;

	private const int kMaxBlocksPerPalette = 3;

	private bool m_EnteredViaTechSelectionPhase;

	private float m_ConfirmEnterTime;

	private StateMachine<SelectStates> m_Fsm;

	private int CurrentDeathStreakLevel { get; set; }

	private void Update()
	{
		UpdateTimer();
		if (!(Singleton.Manager<ManNetwork>.inst.NetController != null) || Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.TechSelection || !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			if (!Singleton.Manager<ManNetwork>.inst.GetPlayer(i).IsFirstTechSelected)
			{
				flag = false;
			}
		}
		if (flag)
		{
			Singleton.Manager<ManNetwork>.inst.NetController.ServerChangePhase(Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase + 1);
		}
	}

	private void UpdateTimer()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.TechSelection)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 103);
			bool showMinutes = false;
			bool showMilliseconds = false;
			m_StartingTechSelectionTimer.text = string.Format(localisedString, Util.GetTimeString(_getTechSelectionTimeRemainingInSecs(), showMinutes, showMilliseconds));
			m_GameStartedText.gameObject.SetActive(value: false);
		}
		else
		{
			m_StartingTechSelectionTimer.gameObject.SetActive(value: false);
			if (m_EnteredViaTechSelectionPhase)
			{
				m_GameStartedText.gameObject.SetActive(value: true);
			}
		}
	}

	private void Init_Enter()
	{
		m_EnteredViaTechSelectionPhase = Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.TechSelection;
		m_CorporationPresets = Mode<ModeDeathmatch>.inst.GetAvailableLoadouts().GetTechPresets();
		ApplySelection();
		m_Fsm.ChangeState(SelectStates.Select);
	}

	private void Select_Enter()
	{
		EnableCorporationButtons(enable: true);
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].OnBlockPaletteTriggered.Subscribe(Select_OnPaletteTriggered);
			m_CorpItems[i].OnBlockPaletteHighlighted.Subscribe(Select_OnPaletteHighlighted);
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_Ready.Hide();
		}
		else
		{
			m_Ready.Show();
			m_Ready.Triggered.Subscribe(Select_OnReadyTriggered);
		}
		UpdateBlockPaletteButtonSelection();
	}

	private void Select_OnPaletteHighlighted(int corp, int palette)
	{
		SetSelected(corp, palette);
	}

	private void Select_OnPaletteTriggered(int corp, int palette)
	{
		SetSelected(corp, palette);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_Fsm.ChangeState(SelectStates.Confirm);
		}
	}

	private void Select_OnSkinSwapped(int corp, int skinIndex)
	{
	}

	private void Select_OnReadyTriggered()
	{
		ChangeStateToReadyOrDone();
	}

	private void Select_Exit()
	{
		m_Ready.Triggered.Unsubscribe(Select_OnReadyTriggered);
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].OnBlockPaletteTriggered.Unsubscribe(Select_OnPaletteTriggered);
			m_CorpItems[i].OnBlockPaletteHighlighted.Unsubscribe(Select_OnPaletteHighlighted);
		}
	}

	private void Confirm_Enter()
	{
		m_Ready.Triggered.Subscribe(Confirm_OnReadyTriggered);
		m_Ready.Show();
		m_ConfirmEnterTime = Time.time;
		FocusSelectedItem(showFocus: true);
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].OnBlockPaletteTriggered.Subscribe(Confirm_OnPaletteTriggered);
		}
	}

	private void Confirm_Update()
	{
		if (m_ConfirmEnterTime != Time.time)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(21))
			{
				ChangeStateToReadyOrDone();
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(22))
			{
				m_Fsm.ChangeState(SelectStates.Select);
			}
		}
	}

	private void Confirm_OnPaletteTriggered(int corp, int palette)
	{
		SetSelected(corp, palette);
		m_Fsm.ChangeState(SelectStates.Select);
	}

	private void Confirm_OnReadyTriggered()
	{
		ChangeStateToReadyOrDone();
	}

	private void Confirm_Exit()
	{
		m_Ready.Triggered.Unsubscribe(Confirm_OnReadyTriggered);
		m_Ready.Hide();
		FocusSelectedItem(showFocus: false);
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].OnBlockPaletteTriggered.Unsubscribe(Confirm_OnPaletteTriggered);
		}
	}

	private void Ready_Enter()
	{
		Singleton.Manager<ManNetwork>.inst.MyPlayer.SelectFirstTech();
		FocusSelectedItem(showFocus: true);
		m_Ready.Hide();
		EnableCorporationButtons(enable: false);
	}

	private void Ready_Update()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.TechSelection)
		{
			m_Fsm.ChangeState(SelectStates.Done);
		}
	}

	private void Ready_Exit()
	{
		FocusSelectedItem(showFocus: false);
	}

	private void Done_Enter()
	{
		Singleton.Manager<ManNetwork>.inst.KillStreakRewards = m_CorporationPresets[m_CorporationSelected].m_KillStreakRewards;
		Singleton.Manager<ManNetwork>.inst.MyPlayer.InitDeathStreakRewards(m_CorporationPresets[m_CorporationSelected]);
		FactionSubTypes corp = m_CorpItemsMapping[m_CorporationSelected];
		Singleton.Manager<ManNetwork>.inst.MyPlayer.OnTechToSpawnSelected(m_CorporationSelected, 0, m_BlockPaletteSelected, Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID((byte)Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedSkinInCorp(corp), corp));
		Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
	}

	private void ChangeStateToReadyOrDone()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController.IsNotNull())
		{
			if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.TechSelection)
			{
				m_Fsm.ChangeState(SelectStates.Ready);
			}
			else
			{
				m_Fsm.ChangeState(SelectStates.Done);
			}
		}
		else
		{
			d.LogError("Cannot change tech select screen to ready because NetController is null");
		}
	}

	public override void ScreenInitialize(ManUI.ScreenType type)
	{
		base.ScreenInitialize(type);
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].SetCorpIndex(i, m_CorpItemsMapping[i]);
		}
		m_CorporationPresets = Mode<ModeDeathmatch>.inst.GetAvailableLoadouts().GetTechPresets();
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded())
		{
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: false, UIShopBlockSelect.ExpandReason.Button, forceClose: true);
		}
		m_Fsm.ChangeState(SelectStates.Init);
	}

	public override void Hide()
	{
		base.Hide();
	}

	public override bool GoBack()
	{
		return true;
	}

	public void OnBackClicked()
	{
	}

	public void SetCorporationSelected(int corpSelected)
	{
		m_CorporationSelected = corpSelected;
	}

	public void SetBlockPaletteSelected(int blocksSelected)
	{
		m_BlockPaletteSelected = blocksSelected;
	}

	public void ApplySelection()
	{
		for (int i = 0; i < 4; i++)
		{
			MultiplayerTechSelectPresetAsset presetData = m_CorporationPresets[i];
			m_CorpItems[i].SetPresetData(presetData);
		}
	}

	private void SetScreenTitle(string titleText)
	{
		m_TitleTextField.text = titleText.ToUpper();
	}

	private void SetSelected(int corpSelected, int blocksSelected)
	{
		SetCorporationSelected(corpSelected);
		SetBlockPaletteSelected(blocksSelected);
		UpdateBlockPaletteButtonSelection();
	}

	private void UpdateBlockPaletteButtonSelection()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				m_CorpItems[i].SelectBlockPalette(j, isSelected: false);
			}
		}
		m_CorpItems[m_CorporationSelected].SelectBlockPalette(m_BlockPaletteSelected, isSelected: true);
	}

	private void EnableCorporationButtons(bool enable)
	{
		for (int i = 0; i < 4; i++)
		{
			m_CorpItems[i].SetEnabled(enable);
		}
	}

	private void FocusSelectedItem(bool showFocus)
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				m_CorpItems[i].SetFocused(m_CorporationSelected, m_BlockPaletteSelected, showFocus);
			}
		}
	}

	private float _getTechSelectionTimeRemainingInSecs()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.TechSelection);
		if (!(Singleton.Manager<ManNetwork>.inst.NetController != null))
		{
			return 0f;
		}
		return Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhaseTimer;
	}

	private void PrePool()
	{
		m_Fsm = StateMachine<SelectStates>.Initialize(this);
	}
}
