#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public class GameHints
{
	public enum HintID
	{
		Invalid,
		RotateBlock,
		PickUpAnchoredBlock,
		Boost,
		Snapshot,
		TradingStationRestock,
		TradingStationHasRestocked,
		HoldDownFireButton,
		RecallStoredTech,
		NewBlockDiscovered,
		DefeatedByTraderTroll1,
		DefeatedByTraderTroll2,
		ObjectiveMarker,
		MissionLog,
		Radar,
		Minimap,
		BatteryPower,
		BatteryCharge,
		ConveyorDirection,
		CraftingMissionInDesert,
		AnchorTechFailed,
		PowerStorage,
		RaceFailedOutOfTime,
		ManualTarget,
		ExplosiveBolt,
		BlockLimit,
		BlockLimit2,
		NoFlightControl
	}

	private bool m_Enabled;

	[SerializeField]
	[Header("Auto Hide Hints When Condition Fulfilled")]
	[Tooltip("Auto-hide hints after their hide criteria has been fulfilled")]
	private bool m_AutoHideHints;

	[Tooltip("Auto-hide hints this many seconds after the hide criteria has been fulfilled")]
	[SerializeField]
	private float m_AutoHideHintDelay = 1f;

	[Header("Pick Up Anchored Block")]
	[Tooltip("Show hint when driving this far away from the anchored block")]
	[SerializeField]
	private float m_DistanceFromAnchoredBlock = 50f;

	[SerializeField]
	[Tooltip("The block type we expect to be anchored")]
	private BlockTypes m_AnchoredBlockType = BlockTypes.GSOGeneratorSolar_141;

	[Tooltip("Show hint when player tech is worth at least this amount of money")]
	[Header("Snapshot")]
	[SerializeField]
	private float m_SnapshotTechValue = 10000f;

	[Header("Trading Station Restock")]
	[SerializeField]
	[Tooltip("Hour of the day that Trading Stations restock, e.g. 5 = 5AM")]
	[Range(0f, 24f)]
	private int m_TradingStationRestockTime = 5;

	[Tooltip("Show hint when player presses fire button this many times in a row")]
	[SerializeField]
	[Header("Hold Down Fire Button")]
	private int m_FireButtonPressCountThreshold = 10;

	[SerializeField]
	[Tooltip("Maximum time between two button presses for them to count as being pressed in a row")]
	private float m_FireButtonPressTimeThreshold = 0.5f;

	[SerializeField]
	[Tooltip("Hide hint when player holds down fire button for this many seconds")]
	private float m_FireButtonHoldDownTime = 2f;

	[SerializeField]
	[Tooltip("How many seconds between checks for manual target hint")]
	private float m_ManualTargetHintTime = 10f;

	private TankBlock m_LastBlockPickedUp;

	private bool m_BlockRotated;

	private float m_BlockRotatedTime;

	private bool m_AnchoredBlockPickedUp;

	private float m_AnchoredBlockPickedUpTime;

	private BlockTypes m_LastBoostCheckedBlockType;

	private bool m_BoostersFired;

	private float m_BoostersFiredTime;

	private bool m_SnapshotTaken;

	private bool m_HasShownTradingStationHintToday;

	private int m_FireButtonPressCount;

	private float m_LastFireButtonPressTime;

	private float m_FireButtonHoldDownTimer;

	private int m_ManualTargetHintTimeout;

	public void EnableHints(bool enable)
	{
		if (enable != m_Enabled)
		{
			m_Enabled = enable;
			if (enable)
			{
				SetupHints();
			}
			else
			{
				CleanupHints();
			}
		}
	}

	private void ShowHint(HintID hintID)
	{
		Singleton.Manager<ManHints>.inst.ShowHint(hintID);
	}

	private void HideHint(HintID hintID)
	{
		Singleton.Manager<ManHints>.inst.HideHint(hintID);
	}

	private bool IsShowingHint(HintID hintID)
	{
		return Singleton.Manager<ManHints>.inst.IsShowingHint(hintID);
	}

	private void SetHintShownBefore(HintID hintID)
	{
		Singleton.Manager<ManProfile>.inst.GetCurrentUser().SetHintSeen(hintID);
	}

	private bool HasShownHintBefore(HintID hintID)
	{
		return Singleton.Manager<ManProfile>.inst.GetCurrentUser().HasSeenHint(hintID);
	}

	private void OnDragEvent(Visible draggedItem, ManPointer.DragAction dragAction, Vector3 position)
	{
		switch (dragAction)
		{
		case ManPointer.DragAction.Grab:
			if (draggedItem.type == ObjectTypes.Block)
			{
				OnBlockPickup(draggedItem.block);
			}
			break;
		case ManPointer.DragAction.ReleaseLoose:
		case ManPointer.DragAction.ReleaseAllowPlace:
			if (draggedItem.type == ObjectTypes.Block)
			{
				OnBlockRelease(draggedItem.block);
			}
			break;
		default:
			d.LogError("OnDragEvent.OnDragEvent - Unhandled DragAction passed in!");
			break;
		case ManPointer.DragAction.Update:
		case ManPointer.DragAction.PostRelease:
			break;
		}
	}

	private void SetupHints()
	{
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDragEvent);
		Singleton.Manager<ManTechBuilder>.inst.OnBlockRotatedEvent.Subscribe(OnBlockRotated);
		Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Subscribe(OnSnapshotSaved);
		Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Subscribe(OnAnchorTechFailed);
		m_LastBlockPickedUp = null;
		m_BlockRotated = false;
		m_BlockRotatedTime = -1f;
		m_AnchoredBlockPickedUp = false;
		m_AnchoredBlockPickedUpTime = -1f;
		m_LastBoostCheckedBlockType = BlockTypes.GSOBlock_111;
		m_BoostersFired = false;
		m_BoostersFiredTime = -1f;
		m_SnapshotTaken = false;
		m_FireButtonPressCount = 0;
		m_LastFireButtonPressTime = 0f;
		m_FireButtonHoldDownTimer = 0f;
	}

	private void CleanupHints()
	{
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(OnDragEvent);
		Singleton.Manager<ManTechBuilder>.inst.OnBlockRotatedEvent.Unsubscribe(OnBlockRotated);
		Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Unsubscribe(OnSnapshotSaved);
		Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Unsubscribe(OnAnchorTechFailed);
	}

	public void UpdateHints()
	{
		UpdateHintBlockRotation();
		UpdateHintBoost();
		UpdateHintSnapshot();
		UpdateHintTradingStationHasRestocked();
		UpdateHintHoldDownFireButton();
		UpdateHintCraftingMissionInDesert();
		UpdateHintManualTarget();
		UpdateHintExplosiveBolt();
		UpdateHintFlightControls();
	}

	public void UpdateHintBlockRotation()
	{
		if (m_BlockRotated && m_AutoHideHints && m_BlockRotatedTime > 0f && Time.time > m_BlockRotatedTime + m_AutoHideHintDelay)
		{
			HideHint(HintID.RotateBlock);
			m_BlockRotated = false;
			m_BlockRotatedTime = -1f;
		}
	}

	public void UpdateHintPickUpAnchoredBlock()
	{
		if (!(Singleton.playerTank != null))
		{
			return;
		}
		if (!HasShownHintBefore(HintID.PickUpAnchoredBlock) && Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(FactionSubTypes.GSO, 1, "1-4 Solar Gen"))
		{
			bool flag = true;
			foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
			{
				if (!(currentTech != Singleton.playerTank) || !currentTech.IsAnchored || !currentTech.IsFriendly())
				{
					continue;
				}
				if ((Singleton.playerTank.boundsCentreWorld - currentTech.boundsCentreWorld).SetY(0f).magnitude > m_DistanceFromAnchoredBlock)
				{
					ShowHint(HintID.PickUpAnchoredBlock);
				}
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = currentTech.blockman.IterateBlocks().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.BlockType == m_AnchoredBlockType)
					{
						flag = false;
						m_AnchoredBlockPickedUp = false;
						m_AnchoredBlockPickedUpTime = -1f;
						break;
					}
				}
			}
			if (flag)
			{
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().SetHintSeen(HintID.PickUpAnchoredBlock);
			}
		}
		else if (IsShowingHint(HintID.PickUpAnchoredBlock) && m_AnchoredBlockPickedUp && m_AutoHideHints && m_AnchoredBlockPickedUpTime > 0f && Time.time > m_AnchoredBlockPickedUpTime + m_AutoHideHintDelay)
		{
			HideHint(HintID.PickUpAnchoredBlock);
		}
	}

	public void UpdateHintBoost()
	{
		if (!HasShownHintBefore(HintID.Boost) && m_LastBlockPickedUp != null)
		{
			BlockTypes blockType = m_LastBlockPickedUp.BlockType;
			if (blockType != m_LastBoostCheckedBlockType)
			{
				if (Singleton.Manager<ManSpawn>.inst.HasBlockDescriptorEnum(blockType, typeof(BlockControlAttributes), 0))
				{
					ShowHint(HintID.Boost);
					m_BoostersFired = false;
					m_BoostersFiredTime = -1f;
				}
				m_LastBoostCheckedBlockType = blockType;
			}
		}
		else if (m_AutoHideHints && IsShowingHint(HintID.Boost))
		{
			if (!m_BoostersFired && Singleton.playerTank != null && Singleton.playerTank.Boosters.BoostersFiring)
			{
				m_BoostersFired = true;
				m_BoostersFiredTime = Time.time;
			}
			else if (m_BoostersFired && m_BoostersFiredTime > 0f && Time.time > m_BoostersFiredTime + m_AutoHideHintDelay)
			{
				HideHint(HintID.Boost);
			}
		}
	}

	public void UpdateHintSnapshot()
	{
		if (!HasShownHintBefore(HintID.Snapshot) && Singleton.playerTank != null && (float)Singleton.playerTank.GetValue() >= m_SnapshotTechValue)
		{
			ShowHint(HintID.Snapshot);
		}
		else if (IsShowingHint(HintID.Snapshot) && m_SnapshotTaken && m_AutoHideHints)
		{
			HideHint(HintID.Snapshot);
			m_SnapshotTaken = false;
		}
	}

	public void UpdateHintTradingStationHasRestocked()
	{
		int num = m_TradingStationRestockTime + 1;
		if (!IsShowingHint(HintID.TradingStationHasRestocked))
		{
			if (!m_HasShownTradingStationHintToday && Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay >= m_TradingStationRestockTime && Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay < num && Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(FactionSubTypes.GSO, 2, "2-1 Use Mission Board"))
			{
				ShowHint(HintID.TradingStationHasRestocked);
				m_HasShownTradingStationHintToday = true;
			}
		}
		else if (m_HasShownTradingStationHintToday)
		{
			if (Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay >= num)
			{
				HideHint(HintID.TradingStationHasRestocked);
			}
			else if (Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay < m_TradingStationRestockTime)
			{
				m_HasShownTradingStationHintToday = false;
			}
		}
	}

	public void UpdateHintHoldDownFireButton()
	{
		int rewiredAction = 2;
		bool flag = Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndInvulnerable();
		if (!HasShownHintBefore(HintID.HoldDownFireButton) && !flag && Singleton.Manager<ManInput>.inst.GetButtonDown(rewiredAction))
		{
			if (Time.time - m_LastFireButtonPressTime < m_FireButtonPressTimeThreshold)
			{
				m_LastFireButtonPressTime = Time.time;
				m_FireButtonPressCount++;
				if (m_FireButtonPressCount >= m_FireButtonPressCountThreshold)
				{
					ShowHint(HintID.HoldDownFireButton);
					m_FireButtonHoldDownTimer = 0f;
				}
			}
			else
			{
				m_LastFireButtonPressTime = Time.time;
				m_FireButtonPressCount = 1;
			}
		}
		else
		{
			if (!IsShowingHint(HintID.HoldDownFireButton))
			{
				return;
			}
			if (Singleton.Manager<ManInput>.inst.GetButton(rewiredAction))
			{
				m_FireButtonHoldDownTimer += Time.deltaTime;
				if (m_FireButtonHoldDownTimer >= m_FireButtonHoldDownTime)
				{
					HideHint(HintID.HoldDownFireButton);
				}
			}
			else
			{
				m_FireButtonHoldDownTimer = 0f;
			}
		}
	}

	public void UpdateHintCraftingMissionInDesert()
	{
		if (!HasShownHintBefore(HintID.CraftingMissionInDesert) && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.MissionBoard) && Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(FactionSubTypes.GSO, 2, "Crafting Tutorial 03"))
		{
			ShowHint(HintID.CraftingMissionInDesert);
		}
	}

	private void UpdateHintManualTarget()
	{
		m_ManualTargetHintTime -= Time.deltaTime;
		if (!(m_ManualTargetHintTime < 0f))
		{
			return;
		}
		m_ManualTargetHintTime = m_ManualTargetHintTimeout;
		Tank playerTank = Singleton.playerTank;
		if ((bool)playerTank)
		{
			_ = playerTank.Vision;
			TechWeapon weapons = playerTank.Weapons;
			if (!HasShownHintBefore(HintID.ManualTarget) && weapons.ShouldShowHint())
			{
				ShowHint(HintID.ManualTarget);
			}
			if (IsShowingHint(HintID.ManualTarget) && (bool)weapons.GetManualTarget())
			{
				HideHint(HintID.ManualTarget);
			}
		}
	}

	private void UpdateHintExplosiveBolt()
	{
		if (!HasShownHintBefore(HintID.ExplosiveBolt) && m_LastBlockPickedUp != null)
		{
			BlockTypes blockType = m_LastBlockPickedUp.BlockType;
			if (blockType == BlockTypes.GSO_Exploder_A1_111 || blockType == BlockTypes.GSO_Exploder_A2_111 || blockType == BlockTypes.GSO_Exploder_A3_111 || blockType == BlockTypes.GSO_Exploder_A4_111 || blockType == BlockTypes.GSO_Exploder_B_111)
			{
				ShowHint(HintID.ExplosiveBolt);
			}
		}
	}

	private void UpdateHintFlightControls()
	{
		if (!HasShownHintBefore(HintID.NoFlightControl) && Singleton.playerTank != null && !Singleton.playerTank.grounded)
		{
			ControlScheme activeScheme = Singleton.playerTank.control.ActiveScheme;
			if (activeScheme != null && !activeScheme.HasAxisMapping(MovementAxis.RotateZ_RollRight) && !activeScheme.HasAxisMapping(MovementAxis.RotateX_PitchUp) && AutoDefaultSchemeSelector.HasUprightWing(Singleton.playerTank))
			{
				ShowHint(HintID.NoFlightControl);
			}
		}
	}

	private void OnBlockPickup(TankBlock block)
	{
		m_LastBlockPickedUp = block;
		if (block.BlockType == m_AnchoredBlockType)
		{
			OnAnchoredBlockPickup();
		}
	}

	private void OnAnchoredBlockPickup()
	{
		if (!m_AnchoredBlockPickedUp)
		{
			m_AnchoredBlockPickedUp = true;
			m_AnchoredBlockPickedUpTime = Time.time;
		}
	}

	private void OnBlockRelease(TankBlock block)
	{
	}

	private void OnBlockRotated()
	{
		if (!m_BlockRotated)
		{
			m_BlockRotated = true;
			m_BlockRotatedTime = Time.time;
		}
	}

	private void OnSnapshotSaved(TechData tech, Texture2D texture, bool isPlayerTech)
	{
		if (!m_SnapshotTaken)
		{
			m_SnapshotTaken = true;
			SetHintShownBefore(HintID.Snapshot);
		}
	}

	private void OnAnchorTechFailed()
	{
		if (!HasShownHintBefore(HintID.AnchorTechFailed))
		{
			ShowHint(HintID.AnchorTechFailed);
		}
	}
}
