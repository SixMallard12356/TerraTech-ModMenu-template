using System.Collections.Generic;

public class ManFTUE : Singleton.Manager<ManFTUE>, Mode.IManagerModeEvents
{
	public enum SaveStates
	{
		Tutorial,
		Normal,
		Disable
	}

	private class SaveData
	{
		public SaveStates m_SaveState;

		public bool m_PopulationTutorialStarted;

		public bool m_FirstPopulationEnemyTutDone;

		public bool m_SecondPopulationTutDone;

		public bool m_HasPlayerAttachedBlockNoBeam;
	}

	public uScriptCode m_ShowBuildGraph;

	public float m_SecondEnemyKilledRestartDist = 30f;

	private bool m_PopulationTutorialStarted;

	private bool m_HasPlayerAttachedBlockNoBeam;

	private bool m_FirstPopulaionTutorialDone;

	private bool m_SecondPopulationTutorialDone;

	private Dictionary<string, int> m_Delivered = new Dictionary<string, int>();

	public bool CanSave
	{
		get
		{
			if (SaveState != SaveStates.Tutorial)
			{
				return SaveState == SaveStates.Normal;
			}
			return true;
		}
	}

	public SaveStates SaveState { get; set; }

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		StartDefault();
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManFTUE, out var saveData) && saveData != null)
		{
			SaveState = saveData.m_SaveState;
			StartPopulationTutorial(saveData.m_FirstPopulationEnemyTutDone, saveData.m_SecondPopulationTutDone);
			m_HasPlayerAttachedBlockNoBeam = saveData.m_HasPlayerAttachedBlockNoBeam;
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		saveData.m_SaveState = SaveState;
		saveData.m_PopulationTutorialStarted = m_PopulationTutorialStarted;
		saveData.m_FirstPopulationEnemyTutDone = m_FirstPopulaionTutorialDone;
		saveData.m_SecondPopulationTutDone = m_SecondPopulationTutorialDone;
		saveData.m_HasPlayerAttachedBlockNoBeam = m_HasPlayerAttachedBlockNoBeam;
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManFTUE, saveData);
	}

	public void ModeExit()
	{
	}

	public void DisableAll()
	{
		UnsubscribeAllEvents();
		if ((bool)m_ShowBuildGraph)
		{
			m_ShowBuildGraph.gameObject.SetActive(value: false);
		}
		m_Delivered.Clear();
	}

	public void StartShowSequence()
	{
		if ((bool)m_ShowBuildGraph)
		{
			m_ShowBuildGraph.gameObject.SetActive(value: true);
		}
	}

	private void StartDefault()
	{
		SaveState = SaveStates.Tutorial;
		m_PopulationTutorialStarted = false;
		m_HasPlayerAttachedBlockNoBeam = false;
		m_FirstPopulaionTutorialDone = false;
		m_SecondPopulationTutorialDone = false;
	}

	public void StartPopulationTutorial(bool firts, bool second)
	{
		if (!m_PopulationTutorialStarted && (firts || second))
		{
			Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankKilled);
			Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnModuleAttached);
			m_PopulationTutorialStarted = true;
		}
		m_FirstPopulaionTutorialDone = !firts;
		m_SecondPopulationTutorialDone = !second;
	}

	private void OnTankKilled(Tank tank, ManDamage.DamageInfo info)
	{
		if (!Singleton.Manager<ManPop>.inst.IsTankPartOfWave(tank) || !info.SourceTank || !(info.SourceTank == Singleton.playerTank))
		{
			return;
		}
		if (!m_FirstPopulaionTutorialDone)
		{
			if (m_HasPlayerAttachedBlockNoBeam)
			{
				string[] messages = new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HintSection, 34) };
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(new ManOnScreenMessages.OnScreenMessage(messages, ManOnScreenMessages.MessagePriority.Medium), boolVal: true);
			}
			else
			{
				string[] messages2 = new string[2]
				{
					Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HintSection, 34),
					Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HintSection, 35)
				};
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(new ManOnScreenMessages.OnScreenMessage(messages2, ManOnScreenMessages.MessagePriority.Medium), boolVal: true);
			}
			m_FirstPopulaionTutorialDone = true;
			StartPopulationTutorial(firts: false, second: true);
		}
		else if (!m_SecondPopulationTutorialDone)
		{
			string[] messages3 = new string[2]
			{
				Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HintSection, 36),
				Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HintSection, 37)
			};
			Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(new ManOnScreenMessages.OnScreenMessage(messages3, ManOnScreenMessages.MessagePriority.Medium), boolVal: true);
			Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankKilled);
			m_SecondPopulationTutorialDone = true;
			UnsubscribeAllEvents();
		}
	}

	private void OnModuleAttached(Tank tank, TankBlock block)
	{
		if ((bool)Singleton.playerTank && tank == Singleton.playerTank && !Singleton.playerTank.beam.IsActive)
		{
			m_HasPlayerAttachedBlockNoBeam = true;
		}
	}

	public void UnsubscribeAllEvents()
	{
		if (m_PopulationTutorialStarted)
		{
			if (!m_FirstPopulaionTutorialDone || !m_SecondPopulationTutorialDone)
			{
				Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnModuleAttached);
			}
			if (!m_SecondPopulationTutorialDone)
			{
				Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankKilled);
			}
		}
	}
}
