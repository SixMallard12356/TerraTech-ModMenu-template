using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModeFlightChallenge : Mode<ModeFlightChallenge>
{
	[Serializable]
	public class ScoreRecord
	{
		public string name;

		public float distance;
	}

	public ManGameMode.GameType m_MyType;

	public BiomeMap biomeMap;

	public TankPreset presetToSpawnFinally;

	public PositionWithFacing playerSpawn = PositionWithFacing.identity;

	public PositionWithFacing cameraSpawn = PositionWithFacing.identity;

	public ScoreRecord[] scoreRecords = new ScoreRecord[3]
	{
		new ScoreRecord
		{
			name = "Reece",
			distance = 100f
		},
		new ScoreRecord
		{
			name = "Russ",
			distance = 300f
		},
		new ScoreRecord
		{
			name = "Royce",
			distance = 500f
		}
	};

	[SerializeField]
	private bool m_ShowInfo;

	public LocalisedString m_Info;

	public LocalisedString m_Continue;

	[Tooltip("Change this from -1 if overriding")]
	[SerializeField]
	private float m_OverrideGrabDistance = -1f;

	[HideInInspector]
	public SpawnList spawnList = new SpawnList();

	[HideInInspector]
	public SpawnList kamikazeTargets = new SpawnList();

	private Vector3 distChallengeStart;

	private float distChallengeTravelled;

	private float reportDistance;

	private float bestDistance;

	private int distanceSwitch;

	private int[] distancesToShowMessage = new int[12]
	{
		5000, 6000, 10000, 15000, 20000, 30000, 50000, 55000, 65000, 80000,
		100000, 101000
	};

	private bool setNewRecord;

	private bool loadedTank;

	private ManOnScreenMessages.OnScreenMessage msgBuild = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Build a flying machine!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgRotate = new ManOnScreenMessages.OnScreenMessage(new string[1] { "While placing a Block, scroll the Mouse Wheel or click the Right Button to rotate it." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgFuel = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Fuel tanks allow you to boost for longer, but they also increase the weight of your craft." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgSteer = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Steering thrusters allow you to steer in-flight, using WASD. Be careful, they burn fuel too!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgLaunch = new ManOnScreenMessages.OnScreenMessage(new string[1] { "When you're ready, use Left Shift to fire your boosters and launch into the air!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgTravel = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Try to fly as far as you can, away from the starting point!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgRefill = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Fuel tanks will slowly refill over time, whilst not in use." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgRecover = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Remember, tap B to toggle beam mode on and off, to right your craft." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgRecord = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Nice, you beat the record!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgLoad = new ManOnScreenMessages.OnScreenMessage(new string[1] { "You have loaded a pre-made vehicle: you can practice, but your records will not count." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgTooHeavy = new ManOnScreenMessages.OnScreenMessage(new string[1] { "This craft might be a bit too heavy to fly as it is, try removing some blocks to make it lighter." }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage msgLightEnough = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Cool, the craft might be light enough to take off now, make sure you have enough thrusters!" }, ManOnScreenMessages.MessagePriority.Medium);

	private ManOnScreenMessages.OnScreenMessage[] flightLegthMessages = new ManOnScreenMessages.OnScreenMessage[12]
	{
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Congratulations! You've built something that will probably fly forever." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Awesome! You really can fly! Your machine can probably fly forever." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Just to let you know, there is no prize for endurance here... just saying." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Yes, very well done. I get the point. Your flying machine will go as far as you like." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Honestly, it is impressive, but wouldn't you rather play another challenge mode?" }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Well I can see you're committed to this now. Good for you, but it will go on forever." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Honestly, just stop. What are you doing?" }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "STAHP ALREADY! Nothing more is going to happen!" }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "...I don't know what to say. I'm worried for you." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Ok, ok! You've proved your point! You're the best at this. You Win." }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Finish!" }, ManOnScreenMessages.MessagePriority.Medium),
		new ManOnScreenMessages.OnScreenMessage(new string[1] { "Please finish... please stop flying!" }, ManOnScreenMessages.MessagePriority.Medium)
	};

	public bool KamikazeMode { get; private set; }

	private IEnumerator TutorialMain()
	{
		if (!loadedTank)
		{
			Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgBuild, boolVal: true);
		}
		while (!Singleton.playerTank || !Singleton.playerTank.grounded)
		{
			yield return null;
		}
		Singleton.Manager<FTUE>.inst.RunParallel(TutorialLoopExplainBlocks());
		Singleton.Manager<FTUE>.inst.RunParallel(TutorialLoopResults());
		yield return Singleton.Manager<FTUE>.inst.RunInSequence(TutorialWaitForTakeoff());
		Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
		Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgTravel, boolVal: true);
		yield return Singleton.Manager<FTUE>.inst.RunInSequence(TutorialWaitForLanding());
		Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgRecover, boolVal: true);
	}

	private IEnumerator TutorialWaitForTakeoff()
	{
		float placementTime = Time.time;
		bool hasPromptedToLaunch = false;
		while (!PlayerTankIsAirborne() || reportDistance < 1f)
		{
			if (Singleton.Manager<ManTechBuilder>.inst.IsPlacingBlock())
			{
				placementTime = Time.time;
			}
			if (!hasPromptedToLaunch && Time.time - placementTime > 10f && (bool)Singleton.playerTank && Singleton.playerTank.blockman.IterateBlockComponents<ModuleBooster>().FirstOrDefault() != null)
			{
				Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgLaunch, boolVal: true);
				hasPromptedToLaunch = true;
			}
			yield return null;
		}
	}

	private IEnumerator TutorialWaitForLanding()
	{
		bool fuelConsumed = false;
		bool hasWarnedfuelConsumed = false;
		Action<Tank> OnFuelLow = delegate
		{
			fuelConsumed = true;
		};
		Singleton.Manager<ManTechs>.inst.TankFuelEmptyEvent.Subscribe(OnFuelLow);
		while (PlayerTankIsAirborne())
		{
			if (!hasWarnedfuelConsumed && fuelConsumed)
			{
				Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgRefill, boolVal: true);
				hasWarnedfuelConsumed = true;
			}
			yield return null;
		}
		Singleton.Manager<ManTechs>.inst.TankFuelEmptyEvent.Unsubscribe(OnFuelLow);
	}

	private IEnumerator TutorialLoopExplainBlocks()
	{
		List<ManOnScreenMessages.BlockExplainer> explainers = new List<ManOnScreenMessages.BlockExplainer>
		{
			new ManOnScreenMessages.BlockExplainer(msgFuel, (TankBlock block) => block.GetComponent<ModuleFuelTank>()),
			new ManOnScreenMessages.BlockExplainer(msgSteer, delegate(TankBlock block)
			{
				ModuleBooster component = block.GetComponent<ModuleBooster>();
				return (bool)component && component.UsesDriveControls && (bool)block.GetComponentInChildren<BoosterJet>();
			}),
			new ManOnScreenMessages.BlockExplainer(msgRotate, (TankBlock block) => (bool)block.GetComponent<ModuleBooster>() && Singleton.Manager<ManTechBuilder>.inst.IsPlacingBlock(block) && !Singleton.Manager<ManTechBuilder>.inst.IsPlacingBlockRotated())
		};
		bool atLeastOneUnexplainedBlock = true;
		while (atLeastOneUnexplainedBlock)
		{
			if ((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block)
			{
				atLeastOneUnexplainedBlock = false;
				for (int num = 0; num < explainers.Count; num++)
				{
					ManOnScreenMessages.BlockExplainer blockExplainer = explainers.ElementAt(num);
					if (!blockExplainer.m_HasExplained)
					{
						atLeastOneUnexplainedBlock = true;
						if (blockExplainer.RecogniseBlock(Singleton.Manager<ManPointer>.inst.DraggingItem.block))
						{
							Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
							Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(blockExplainer.m_Message, boolVal: true);
							blockExplainer.m_HasExplained = true;
						}
					}
				}
			}
			yield return null;
		}
	}

	private IEnumerator TutorialLoopResults()
	{
		while (true)
		{
			if (!PlayerTankIsAirborne())
			{
				yield return null;
				continue;
			}
			bool newRecordWhilstInFlight = false;
			while (PlayerTankIsAirborne())
			{
				newRecordWhilstInFlight = setNewRecord;
				yield return null;
			}
			if (newRecordWhilstInFlight && bestDistance > 1f)
			{
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgRecord, boolVal: true);
			}
		}
	}

	private TechData GetPlayerData(InitSettings initSettings)
	{
		loadedTank = false;
		TechData result = ((presetToSpawnFinally != null) ? presetToSpawnFinally.GetTechDataFormatted() : null);
		object value = null;
		if (initSettings.TryGetValue("LoadPlayerPreset", out value))
		{
			result = value as TechData;
			loadedTank = true;
		}
		return result;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, cameraSpawn.position, cameraSpawn.orientation);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.FlyingChallenge, scoreRecords);
		EnterDefaultCameraMode();
		KamikazeMode = initSettings.ContainsKey("Kamikaze");
		if (KamikazeMode)
		{
			kamikazeTargets.SpawnAll();
		}
		else
		{
			spawnList.SpawnAll();
		}
		TechData playerData = GetPlayerData(initSettings);
		if (playerData != null)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = playerData,
				blockIDs = null,
				teamID = 0,
				position = playerSpawn.position,
				rotation = playerSpawn.orientation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
		}
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		FunctionStatus result = FunctionStatus.Running;
		if (Singleton.playerTank.grounded)
		{
			if (!loadedTank)
			{
				Singleton.Manager<ManSFX>.inst.SuppressUISFX();
				Singleton.playerTank.beam.EnableBeam(enable: true);
			}
			Singleton.Manager<FTUE>.inst.Execute(TutorialMain());
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.FlyingChallenge);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
			reportDistance = 0f;
			bestDistance = 0f;
			distChallengeTravelled = -1f;
			setNewRecord = false;
			distanceSwitch = 0;
			if (m_ShowInfo)
			{
				(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(m_Info.Value, delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
				}, m_Continue.Value);
				Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen, ManUI.PauseType.Pause);
			}
			if (m_OverrideGrabDistance != -1f)
			{
				Singleton.Manager<ManPointer>.inst.SetPickupRange(m_OverrideGrabDistance);
			}
			result = FunctionStatus.Done;
		}
		return result;
	}

	protected override void EnterModeUpdateImpl()
	{
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		if ((bool)Singleton.playerTank)
		{
			if (Singleton.playerTank.grounded)
			{
				distanceSwitch = 0;
				reportDistance = 0f;
				setNewRecord = false;
			}
			UIRocketChallengeHUD uIRocketChallengeHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FlyingChallenge) as UIRocketChallengeHUD;
			if (distChallengeTravelled == -1f || Singleton.playerTank.grounded)
			{
				distChallengeStart = Singleton.playerTank.trans.position;
			}
			else
			{
				reportDistance = distChallengeTravelled;
				if (reportDistance > bestDistance)
				{
					setNewRecord = true;
					bestDistance = reportDistance;
					uIRocketChallengeHUD.SetBest((int)bestDistance);
				}
				if (reportDistance > 1f)
				{
					Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.SetPiece, FactionSubTypes.VEN);
				}
			}
			distChallengeTravelled = (Singleton.playerTank.trans.position - distChallengeStart).SetY(0f).magnitude;
			uIRocketChallengeHUD.SetCurrent((int)distChallengeTravelled);
			if (distanceSwitch < distancesToShowMessage.Length - 1 && distChallengeTravelled > (float)distancesToShowMessage[distanceSwitch])
			{
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(flightLegthMessages[distanceSwitch], boolVal: true);
				distanceSwitch++;
			}
		}
		else
		{
			distanceSwitch = 0;
			reportDistance = 0f;
			setNewRecord = false;
		}
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		KamikazeMode = false;
	}

	public override ManGameMode.GameType GetGameType()
	{
		return m_MyType;
	}

	public override string GetGameMode()
	{
		return "ModeFlying";
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override void ResetPlayerPosition()
	{
		if ((bool)Singleton.playerTank)
		{
			Singleton.playerTank.visible.Teleport(playerSpawn.position, playerSpawn.orientation);
			distChallengeStart = Singleton.playerTank.trans.position;
		}
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return biomeMap.WorldGenVersionData;
	}

	private bool PlayerTankIsAirborne()
	{
		if ((bool)Singleton.playerTank && !Singleton.playerTank.beam.IsActive)
		{
			return !Singleton.playerTank.grounded;
		}
		return false;
	}
}
