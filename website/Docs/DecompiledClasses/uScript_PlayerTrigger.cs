using System;
using UnityEngine;

public class uScript_PlayerTrigger : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private int m_HitCount;

	[FriendlyName("On Trigger Enter")]
	public event uScriptEventHandler OnEnterTrigger;

	[FriendlyName("On Trigger Exit")]
	public event uScriptEventHandler OnExitTrigger;

	private void OnTriggerEnter(Collider other)
	{
		if (IsColliderPlayer(other))
		{
			if (m_HitCount == 0 && this.OnEnterTrigger != null)
			{
				this.OnEnterTrigger(this, new EventArgs());
			}
			m_HitCount++;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (IsColliderPlayer(other))
		{
			m_HitCount--;
			if (m_HitCount == 0 && this.OnExitTrigger != null)
			{
				this.OnExitTrigger(this, new EventArgs());
			}
		}
	}

	public void OnEnable()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(PlayerTankChanged);
	}

	public void OnDisable()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(PlayerTankChanged);
		m_HitCount = 0;
	}

	private void PlayerTankChanged(Tank tank, bool enabled)
	{
		m_HitCount = 0;
	}

	private static bool IsColliderPlayer(Collider col)
	{
		bool result = false;
		if (Singleton.playerTank != null && (col.gameObject.layer & (int)Globals.inst.layerTank) != 0)
		{
			result = col.GetComponentInParents<Tank>(thisObjectFirst: true) == Singleton.playerTank;
		}
		return result;
	}
}
