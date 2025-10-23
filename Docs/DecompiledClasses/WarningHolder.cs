using System;
using Newtonsoft.Json;
using UnityEngine;

public class WarningHolder
{
	public enum WarningType
	{
		GunLineOfSight,
		ShieldPowered,
		Anchored,
		WheelSideways,
		LowPower,
		WheelOverloaded,
		NoControlsMapped
	}

	public bool m_Registered;

	public string m_Title;

	public string m_Description;

	public int m_Priority = -1;

	public float m_RegisteredTime;

	public float m_CurrentTime;

	public WarningType m_WarnType;

	public int m_RegisteredAmount;

	private Visible m_Visible;

	private InfoOverlay m_InfoOverlay;

	private Action<Tank, bool> playerTankChangedAction;

	public WarningHolder(Visible visible, WarningType type)
	{
		m_Visible = visible;
		m_WarnType = type;
		playerTankChangedAction = OnPlayerTankChanged;
	}

	[JsonConstructor]
	public WarningHolder()
	{
	}

	public void TryRegisterWarning(LocalisationEnums.Warnings title, LocalisationEnums.Warnings warning, int priority)
	{
		if (CanRegister())
		{
			Register(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, (int)title), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, (int)warning), priority);
		}
	}

	public void TryRegisterWarning(string title, string warning, int priority)
	{
		if (CanRegister())
		{
			Register(title, warning, priority);
		}
	}

	private bool CanRegister()
	{
		return false;
	}

	private void Register(string title, string warning, int priority)
	{
		if (!m_Registered)
		{
			Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(playerTankChangedAction);
		}
		m_Registered = true;
		m_RegisteredTime = Time.time;
		m_Title = title;
		m_Description = warning;
		m_Priority = priority;
		m_RegisteredAmount++;
		if (m_InfoOverlay == null)
		{
			m_InfoOverlay = Singleton.Manager<ManOverlay>.inst.AddWarningOverlay(m_Visible, Singleton.Manager<ManOverlay>.inst.ShowWarningWhileBuilding(m_WarnType));
		}
		m_InfoOverlay.OverrideDataHook(InfoPanelDataTransform);
	}

	private InfoOverlayDataValues InfoPanelDataTransform(InfoOverlayDataValues data)
	{
		data.m_Subtitle = m_Title;
		data.m_Description = m_Description;
		data.IconSprite = null;
		return data;
	}

	public void Remove()
	{
		if (m_Registered)
		{
			if (m_InfoOverlay != null)
			{
				Singleton.Manager<ManOverlay>.inst.RemoveWarningOverlay(m_InfoOverlay);
				m_InfoOverlay = null;
			}
			Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(playerTankChangedAction);
			m_Registered = false;
		}
	}

	public void Clear()
	{
		Remove();
		m_Title = null;
		m_Description = null;
		m_Priority = -1;
	}

	public void Reset()
	{
		Clear();
		m_RegisteredTime = 0f;
		m_RegisteredAmount = 0;
	}

	public void Restore(WarningHolder from)
	{
		if (from != null)
		{
			m_Registered = from.m_Registered;
			m_RegisteredAmount = from.m_RegisteredAmount;
		}
		if (from.m_Priority != -1)
		{
			m_RegisteredTime = Time.time - from.m_CurrentTime;
			TryRegisterWarning(from.m_Title, from.m_Description, from.m_Priority);
		}
	}

	private void OnPlayerTankChanged(Tank t, bool enabled)
	{
		Remove();
	}
}
