using System.Collections.Generic;

public class ManNewFTUE : Singleton.Manager<ManNewFTUE>, Mode.IManagerModeEvents
{
	private class SaveData
	{
		public Dictionary<string, int> m_GameActions = new Dictionary<string, int>();
	}

	private SaveData m_SaveData = new SaveData();

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManFTUENew, out var saveData) && saveData != null)
		{
			m_SaveData = saveData;
		}
		else
		{
			m_SaveData = new SaveData();
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManFTUENew, m_SaveData);
	}

	public void ModeExit()
	{
	}

	public bool CheckEvent(FTUEActions ftueAction)
	{
		return CheckEvent(new FTUEEnumType(ftueAction));
	}

	public bool CheckEvent(FTUEEnumType eventType)
	{
		if (eventType.IsValid(out var _))
		{
			return CheckEvent(eventType.m_TypeName);
		}
		return false;
	}

	public void SetEvent(FTUEActions ftueAction)
	{
		SetEvent(new FTUEEnumType(ftueAction));
	}

	public void SetEvent(FTUEEnumType eventType)
	{
		if (eventType.IsValid(out var _))
		{
			SetEvent(eventType.m_TypeName);
		}
	}

	private void SetEvent(string eventType)
	{
		if (!m_SaveData.m_GameActions.TryGetValue(eventType, out var _))
		{
			m_SaveData.m_GameActions.Add(eventType, 1);
		}
	}

	private bool CheckEvent(string eventType)
	{
		if (m_SaveData.m_GameActions.TryGetValue(eventType, out var _))
		{
			return true;
		}
		return false;
	}
}
