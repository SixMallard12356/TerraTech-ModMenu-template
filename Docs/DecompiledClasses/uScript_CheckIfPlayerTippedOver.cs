using UnityEngine;

[FriendlyName("Player tipped over state")]
public class uScript_CheckIfPlayerTippedOver : uScriptLogic
{
	private enum State
	{
		Up,
		TippedOver,
		Recovered
	}

	private State m_TankState;

	public bool TippedOver => m_TankState == State.TippedOver;

	public bool NotTippedOver => m_TankState == State.Up;

	public bool Recovered => m_TankState == State.Recovered;

	public void In()
	{
		if (m_TankState == State.Recovered)
		{
			m_TankState = State.Up;
		}
		if (PlayerTankIsTippedOver())
		{
			m_TankState = State.TippedOver;
		}
		else if (PlayerTankIsRecovered() && m_TankState != State.Up)
		{
			m_TankState = State.Recovered;
		}
	}

	private bool PlayerTankIsTippedOver()
	{
		Tank playerTank = Singleton.playerTank;
		if ((bool)playerTank && (bool)playerTank.blockman.GetRootBlock() && playerTank.grounded)
		{
			return Vector3.Dot(playerTank.rootBlockTrans.up, Vector3.up) < Mathf.Cos(1.3962634f);
		}
		return false;
	}

	private bool PlayerTankIsRecovered()
	{
		Tank playerTank = Singleton.playerTank;
		if ((bool)playerTank && (bool)playerTank.blockman.GetRootBlock())
		{
			return Vector3.Dot(playerTank.rootBlockTrans.up, Vector3.up) > Mathf.Cos(0.17453292f);
		}
		return false;
	}
}
