#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine.UI;

public class ManChallenge : Singleton.Manager<ManChallenge>, Mode.IManagerModeEvents
{
	public class InitParams
	{
		public ChallengeData data;

		public Challenge.PlacementInfo placementInfo;

		public BuildModeType buildModeType;

		public StartModeType startModeType;

		public bool endChallengeWhenPlayerDies = true;

		public bool exitOnOutOfBounds = true;

		public bool displaysPauseMenu = true;

		public bool restoreSavedHighscore = true;
	}

	private enum State
	{
		Idle,
		Building,
		Playing
	}

	public enum BuildModeType
	{
		None,
		Simultaneous,
		Sequential
	}

	public enum StartModeType
	{
		FirstGate,
		Immediate
	}

	public enum ChallengeEndReason
	{
		Unspecified,
		OutOfBounds,
		PlayerDestroyed,
		PlayerExit
	}

	private class SaveData
	{
		public Dictionary<string, object> m_SavedChallengeData = new Dictionary<string, object>();
	}

	public Event<Challenge.ChallengeEndData> OnChallengeEnded;

	private InitParams m_Params;

	private State m_State;

	private Challenge m_Challenge;

	private Tank.WeakReference m_Protagonist = new Tank.WeakReference();

	private SaveData m_SaveData;

	public bool IsChallengeRunning => m_State != State.Idle;

	public bool LastChallengeResult { get; private set; }

	public Tank Protagonist
	{
		get
		{
			Tank tank = m_Protagonist.Get();
			if (!tank)
			{
				return Singleton.playerTank;
			}
			return tank;
		}
		set
		{
			m_Protagonist.Set(value);
			if (m_Challenge != null)
			{
				m_Challenge.Protagonist = value;
			}
		}
	}

	public bool IsShowingResults()
	{
		if (m_Challenge == null)
		{
			return false;
		}
		return m_Challenge.IsShowingResults();
	}

	public void SetupChallenge(InitParams myParams)
	{
		KillChallenge();
		m_Params = myParams;
		m_State = ((m_Params.buildModeType == BuildModeType.Sequential) ? State.Building : State.Playing);
		SetupState(m_State);
		if (m_Params.buildModeType == BuildModeType.Simultaneous)
		{
			ShowBuildModeInterface(show: true);
		}
		if (m_Params.startModeType == StartModeType.Immediate)
		{
			BeginChallenge();
		}
		LastChallengeResult = false;
		Mode<ModeMain>.inst.SetCanAutoSave(canAutoSave: false);
		if (m_Params.displaysPauseMenu)
		{
			bool pauseGameplay = true;
			Singleton.Manager<ManPauseGame>.inst.PushPauseMenu(ManUI.ScreenType.PauseChallenge, pauseGameplay, ConfigureExitConfirmMenu);
		}
	}

	public void BeginChallenge()
	{
		if (m_Challenge != null)
		{
			m_Challenge.Begin();
		}
	}

	public void KillChallenge(ChallengeEndReason endReason = ChallengeEndReason.Unspecified)
	{
		if (m_Challenge != null)
		{
			m_Challenge.End(endReason);
		}
	}

	public void Update()
	{
		if (m_Challenge != null)
		{
			m_Challenge.Update();
		}
	}

	public bool IsChallengeInProgress(string uniqueChallengeID)
	{
		bool result = false;
		if (IsChallengeRunning)
		{
			string runningChallengeSaveDataID = GetRunningChallengeSaveDataID();
			result = uniqueChallengeID.Equals(runningChallengeSaveDataID);
		}
		return result;
	}

	public static string GetUniqueChallengeID(InitParams challengeParams)
	{
		IntVector3 intVector = challengeParams.placementInfo.spawnPosition.TileRelativePos;
		return (challengeParams.placementInfo.spawnPosition.TileCoord.GetHashCode() ^ intVector.GetHashCode()).ToString();
	}

	private void TearDownChallenge()
	{
		if (m_State != State.Idle)
		{
			TearDownState(m_State);
			m_State = State.Idle;
			Mode<ModeMain>.inst.SetCanAutoSave(canAutoSave: true);
			d.Assert(m_Challenge == null, "m_Challenge still exists after KillChallenge");
			Protagonist = null;
			if (m_Params.displaysPauseMenu)
			{
				Singleton.Manager<ManPauseGame>.inst.PopPauseMenu();
			}
		}
	}

	private void SetupState(State state)
	{
		switch (m_State)
		{
		case State.Building:
			ShowBuildModeInterface(show: true);
			SetupStartChallengeButton();
			break;
		case State.Playing:
		{
			bool num = m_Params.data != null;
			d.Assert(num, "ManChallenge: does not have the necessary data to setup a challenge");
			if (!num)
			{
				break;
			}
			m_Challenge = m_Params.data.CreateChallenge(m_Params.placementInfo);
			m_Challenge.Protagonist = m_Protagonist.Get();
			m_Challenge.OnChallengeEnded.Subscribe(OnChallengeEnd);
			CheckpointChallenge.OnBoundsWarning.Subscribe(OnBoundsWarning);
			object savedContextData = null;
			if (m_Params.restoreSavedHighscore)
			{
				string runningChallengeSaveDataID = GetRunningChallengeSaveDataID();
				if (m_SaveData.m_SavedChallengeData.TryGetValue(runningChallengeSaveDataID, out var value))
				{
					savedContextData = value;
				}
			}
			m_Challenge.Setup(savedContextData);
			break;
		}
		default:
			d.Assert(condition: false, "SetupState: invalid state " + m_State);
			break;
		case State.Idle:
			break;
		}
	}

	private string GetRunningChallengeSaveDataID()
	{
		return GetUniqueChallengeID(m_Params);
	}

	private void TearDownState(State state)
	{
		switch (m_State)
		{
		case State.Building:
			ShowBuildModeInterface(show: false);
			TearDownStartChallengeButton();
			break;
		case State.Playing:
			if (m_Challenge != null)
			{
				CheckpointChallenge.OnBoundsWarning.Unsubscribe(OnBoundsWarning);
				m_Challenge.OnChallengeEnded.Unsubscribe(OnChallengeEnd);
				m_Challenge.TearDown();
				m_Challenge = null;
			}
			break;
		default:
			d.Assert(condition: false, "KillState: invalid state " + m_State);
			break;
		case State.Idle:
			break;
		}
	}

	private void ShowBuildModeInterface(bool show)
	{
		IInventory<BlockTypes> inventory = null;
		if (show)
		{
			inventory = new SingleplayerInventory();
			m_Params.data.BuildInventory(inventory);
		}
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show);
		Singleton.Manager<ManPurchases>.inst.SetInventory(inventory);
		if ((bool)Protagonist)
		{
			Protagonist.beam.EnableBeam(show);
		}
	}

	private void SetupStartChallengeButton()
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.StartChallenge);
		UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.StartChallenge);
		if (hudElement != null)
		{
			Button component = hudElement.GetComponent<Button>();
			if (component != null)
			{
				component.onClick.AddListener(StartChallengeClicked);
			}
			else
			{
				d.LogWarning("ManChallenge has no Button component in its start challenge button prefab");
			}
		}
		else
		{
			d.LogWarning("ManChallenge has no start challenge button prefab");
		}
	}

	private void TearDownStartChallengeButton()
	{
		UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.StartChallenge);
		if (hudElement != null)
		{
			Button component = hudElement.GetComponent<Button>();
			if (component != null)
			{
				component.onClick.RemoveListener(StartChallengeClicked);
			}
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.StartChallenge);
		}
	}

	private void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuPause, 0);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				KillChallenge(ChallengeEndReason.PlayerExit);
				exitScreen.BlockScreenExit(exitBlocked: false);
				Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		exitScreen.Set(localisedString, accept, decline);
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_State = State.Idle;
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
		optionalLoadState?.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManChallenge, out m_SaveData);
		if (m_SaveData == null)
		{
			m_SaveData = new SaveData();
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManChallenge, m_SaveData);
	}

	public void ModeExit()
	{
		KillChallenge(ChallengeEndReason.PlayerExit);
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankDestroyed);
		if (m_SaveData != null)
		{
			m_SaveData.m_SavedChallengeData.Clear();
			m_SaveData = null;
		}
	}

	private void OnChallengeEnd(Challenge.ChallengeEndData challengeEndData)
	{
		LastChallengeResult = challengeEndData.completedWithSuccess;
		if (challengeEndData.storeSaveData)
		{
			string runningChallengeSaveDataID = GetRunningChallengeSaveDataID();
			m_SaveData.m_SavedChallengeData[runningChallengeSaveDataID] = challengeEndData.saveData;
		}
		OnChallengeEnded.Send(challengeEndData);
		TearDownChallenge();
	}

	private void OnBoundsWarning(CheckpointChallenge.BoundsArea area)
	{
		if (area == CheckpointChallenge.BoundsArea.Illegal && m_Params.exitOnOutOfBounds)
		{
			KillChallenge(ChallengeEndReason.OutOfBounds);
		}
	}

	private void StartChallengeClicked()
	{
		d.Assert(m_State == State.Building, "ManChallenge: StartChallengeClicked event received when not building!");
		if (m_State == State.Building)
		{
			TearDownState(m_State);
			m_State = State.Playing;
			SetupState(m_State);
		}
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		if (IsChallengeRunning && Protagonist == null && m_Params.endChallengeWhenPlayerDies)
		{
			KillChallenge(ChallengeEndReason.PlayerDestroyed);
		}
	}
}
