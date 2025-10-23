#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;

public class ModeAttract : Mode<ModeAttract>
{
	[Serializable]
	public class Spawn
	{
		public BiomeMap biomeMap;

		public PositionWithFacing vehicleSpawnCentre = PositionWithFacing.identity;

		public PositionWithFacing cameraSpawn = PositionWithFacing.identity;
	}

	private enum State
	{
		Start,
		SettingUp,
		Running,
		FadingOut,
		Clearing
	}

	public ManGameMode.GameType m_MyType;

	public float resetTime = 120f;

	public TankPreset[] attractTanks;

	[Tooltip("Used on platforms where we don't have a store implementation, so don't want to upsell paid DLC")]
	public TankPreset[] attractTanksNoStore;

	public int numberToSpawn = 3;

	public Spawn[] spawns;

	public float randomSpread = 20f;

	public int m_StartingSpawn = -1;

	public bool m_FirstFrame;

	private const float WITHOUT_SCREEN_COUNTDOWN_TIME = 1f;

	private float resetAtTime;

	private float countdownTimerWithoutScreen = 1f;

	private int spawnIndex;

	private State m_State;

	private bool m_FirstLoadAttempt;

	private bool m_CanStartNewAttractModeSequence = true;

	private Queue<Action> m_EnterScreenSpawnQueue = new Queue<Action>();

	public void SetCanStartNewAttractModeSequence(bool enabled)
	{
		m_CanStartNewAttractModeSequence = enabled;
	}

	public void ShowNextEnterScreenOrMainMenu()
	{
		if (m_EnterScreenSpawnQueue.Count > 0)
		{
			m_EnterScreenSpawnQueue.Dequeue()();
		}
		else
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MainMenu);
		}
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 28);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				Application.Quit();
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

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		SetupTerrain();
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		EnterDefaultCameraMode();
		Singleton.Manager<FTUE>.inst.Stop();
		ManSaveGame.ShouldStore = false;
		m_FirstFrame = true;
		m_State = State.SettingUp;
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		if (m_FirstFrame)
		{
			m_FirstFrame = false;
			return FunctionStatus.Running;
		}
		return FunctionStatus.Done;
	}

	protected override void EnterModeUpdateImpl()
	{
		m_EnterScreenSpawnQueue.Clear();
		if (Singleton.Manager<ManUI>.inst.IsStackEmpty() || Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.ControllerDisconnected))
		{
			if (Singleton.Manager<ManInput>.inst.UseUserEngagementScreen())
			{
				m_EnterScreenSpawnQueue.Enqueue(delegate
				{
					Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.UserEngagement);
				});
			}
			bool flag = false;
			if (m_FirstLoadAttempt)
			{
				if (SKU.IsSteam)
				{
					string argument = CommandLineReader.GetArgument("+connect_lobby");
					if (argument != null)
					{
						Singleton.Manager<ManNetworkLobby>.inst.JoinLobby(new TTNetworkID(argument), fromInvite: true);
						flag = true;
					}
				}
				if (SKU.IsEpicGS && Singleton.Manager<ManEOS>.inst.IsOfflineMode)
				{
					string offlineModeWarningTitle = Singleton.Manager<Localisation>.inst.GetLocalisedString(Singleton.Manager<ManEOS>.inst.FailedToLoadAnyOfflineDLCEntitlements ? LocalisationEnums.NewMenuMain.OfflineModeWarning_Title_NoDLCorMP : LocalisationEnums.NewMenuMain.OfflineModeWarning_Title_NoMP);
					m_EnterScreenSpawnQueue.Enqueue(delegate
					{
						UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
						uIScreenNotifications.Set(offlineModeWarningTitle, ShowNextEnterScreenOrMainMenu, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.NewMenuMain.OfflineModeWarning_ContinueButton));
						Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
					});
				}
			}
			if (!flag)
			{
				ShowNextEnterScreenOrMainMenu();
			}
		}
		m_FirstLoadAttempt = false;
		countdownTimerWithoutScreen = 1f;
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		switch (m_State)
		{
		case State.Start:
			if (m_CanStartNewAttractModeSequence)
			{
				SetupTerrain();
				m_State = State.SettingUp;
				countdownTimerWithoutScreen = 1f;
			}
			break;
		case State.SettingUp:
			if ((bool)Singleton.Manager<ManWorld>.inst.CurrentBiomeMap && !Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating && Singleton.Manager<ManSplashScreen>.inst.HasExited)
			{
				SetupTechs();
				if (!Singleton.Manager<ManSplashScreen>.inst.IsBusy)
				{
					Singleton.Manager<ManUI>.inst.ClearFade(3f);
				}
				m_State = State.Running;
				countdownTimerWithoutScreen = 1f;
			}
			break;
		case State.Running:
			if (Singleton.Manager<ManTechs>.inst.Count < 2 || Time.time > resetAtTime)
			{
				bool num = Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen);
				bool flag = Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyList);
				if (!(num || flag))
				{
					UILoadingScreenHints.SuppressNextHint = true;
					Singleton.Manager<ManUI>.inst.FadeToBlack();
					m_State = State.FadingOut;
				}
				countdownTimerWithoutScreen = 1f;
			}
			else if (Singleton.Manager<ManUI>.inst.IsStackEmpty())
			{
				countdownTimerWithoutScreen -= Time.deltaTime;
				if (countdownTimerWithoutScreen <= 0f)
				{
					countdownTimerWithoutScreen = 1f;
					Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MainMenu);
				}
			}
			else
			{
				countdownTimerWithoutScreen = 1f;
			}
			break;
		case State.FadingOut:
		{
			if (!Singleton.Manager<ManUI>.inst.FadeFinished())
			{
				break;
			}
			List<int> list = new List<int>();
			foreach (TrackedVisible allTrackedVisible in Singleton.Manager<ManVisible>.inst.AllTrackedVisibles)
			{
				list.Add(allTrackedVisible.ID);
			}
			foreach (int item in list)
			{
				Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(item);
			}
			Singleton.Manager<ManWorld>.inst.Reset(null);
			m_State = State.Clearing;
			break;
		}
		case State.Clearing:
			if (Singleton.Manager<ManWorld>.inst.TileManager.IsCleared)
			{
				m_State = State.Start;
				Singleton.Manager<ManSaveGame>.inst.Clear();
			}
			break;
		default:
			d.AssertFormat(false, "ModeAttract is in unsupported state {0}", m_State);
			m_State = State.Start;
			break;
		}
		Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.Generic);
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		m_CanStartNewAttractModeSequence = true;
	}

	public override ManGameMode.GameType GetGameType()
	{
		return m_MyType;
	}

	public override string GetGameMode()
	{
		return "";
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return spawns[spawnIndex].biomeMap.WorldGenVersionData;
	}

	private void SetupTerrain()
	{
		d.Assert(spawns.Length != 0, "need at least one spawn point");
		Singleton.Manager<ManWorld>.inst.SeedString = null;
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(spawns[spawnIndex].biomeMap, spawns[spawnIndex].cameraSpawn.position, spawns[spawnIndex].cameraSpawn.orientation);
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
	}

	private void SetupTechs()
	{
		if (!Singleton.Manager<ManDLC>.inst.SupportsStore() && attractTanksNoStore != null && attractTanksNoStore.Length != 0)
		{
			attractTanks = attractTanksNoStore;
		}
		attractTanks.Shuffle();
		int num = 0;
		Quaternion quaternion = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f);
		float num2 = 360f / (float)numberToSpawn;
		for (int i = 0; i < attractTanks.Length; i++)
		{
			if (num >= numberToSpawn)
			{
				break;
			}
			if (!(attractTanks[i] != null))
			{
				continue;
			}
			Vector3 vector = quaternion * Quaternion.Euler(0f, (float)num * num2, 0f) * Vector3.forward * randomSpread;
			Vector3 position = spawns[spawnIndex].vehicleSpawnCentre.position + vector;
			Quaternion rotation = Quaternion.AngleAxis(UnityEngine.Random.value * 360f, Vector3.up);
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = attractTanks[i].GetTechDataFormatted(),
				blockIDs = null,
				teamID = -1,
				position = position,
				rotation = rotation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			if (tank != null)
			{
				num++;
				BlockManager.BlockIterator<ModuleVision>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleVision>().GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.SetRange(100f);
				}
			}
		}
		resetAtTime = Time.time + resetTime;
		spawnIndex = (spawnIndex + 1) % spawns.Length;
	}

	private void Awake()
	{
		m_FirstLoadAttempt = true;
		spawnIndex = 0;
	}
}
