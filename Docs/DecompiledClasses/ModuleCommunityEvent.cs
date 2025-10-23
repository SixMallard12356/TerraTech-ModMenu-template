using UnityEngine;

[DisallowMultipleComponent]
public class ModuleCommunityEvent : Module
{
	[SerializeField]
	private int m_Duration = 20;

	private int m_CurrentSignal = -1;

	private int m_SignalCount = -1;

	private bool CircuitControlled => base.block.CircuitReceiver.IsConnectedToOtherNodes;

	private void UpdateFromCircuitInput()
	{
		if (!ManNetwork.IsHost || !CircuitControlled)
		{
			return;
		}
		int chargeStrength = base.block.CircuitReceiver.CurrentChargeData.ChargeStrength;
		if (chargeStrength != m_CurrentSignal)
		{
			if (chargeStrength > 0)
			{
				TrySendEvent(m_CurrentSignal);
				m_SignalCount = 0;
			}
			else
			{
				m_SignalCount = -1;
			}
			m_CurrentSignal = chargeStrength;
		}
	}

	private void TrySendEvent(int message)
	{
		if (m_SignalCount >= m_Duration && m_CurrentSignal > 0)
		{
			CommunityEngagementEvent.inst.TrySendMessage(message);
			m_SignalCount = -1;
		}
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		UpdateFromCircuitInput();
	}

	private void OnConnectedToCircuitNetwork(bool connected)
	{
		if (ManNetwork.IsHost)
		{
			if (connected)
			{
				Circuits.EndChargeUpdate.Subscribe(OnEndCSTick);
				UpdateFromCircuitInput();
			}
			else
			{
				Circuits.EndChargeUpdate.Unsubscribe(OnEndCSTick);
			}
		}
	}

	private void OnEndCSTick()
	{
		if (m_SignalCount >= 0)
		{
			m_SignalCount++;
			TrySendEvent(m_CurrentSignal);
		}
	}

	private void PrePool()
	{
		this.SetupForCircuitInput();
	}

	private void OnPool()
	{
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
	}
}
