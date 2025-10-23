using System;
using UnityEngine;

public class ModuleLandingGear : ModuleAnimator
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public bool isDeployed;
	}

	[SerializeField]
	public float m_DeployBelowAltitude = 20f;

	[SerializeField]
	public bool m_IsRetractedByDefault;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	[HideInInspector]
	[SerializeField]
	private ModuleWheels m_Wheels;

	private AnimatorBool m_DeployBool = new AnimatorBool("Deploy");

	public float DeployBelowAltitude => m_DeployBelowAltitude;

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private void SetDeployed(bool deploy)
	{
		Set(m_DeployBool, deploy);
		if ((bool)m_Wheels)
		{
			m_Wheels.SetAnimated(!deploy);
		}
	}

	private void OnAttached()
	{
		SetDeployed(!m_IsRetractedByDefault);
		base.block.tank.AddLandingGearImpl(this);
	}

	private void OnDetaching()
	{
		SetDeployed(!m_IsRetractedByDefault);
		base.block.tank.RemoveLandingGearImpl(this);
	}

	public void OnLandingGearEvent(bool deploy)
	{
		if (!CircuitControlled)
		{
			SetDeployed(deploy);
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.isDeployed = Get(m_DeployBool);
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			SetDeployed(serialData2.isDeployed);
		}
	}

	private void OnCircuitStateChanged()
	{
		if (CircuitControlled)
		{
			bool deployed = base.block.CircuitNode.Receiver.CurrentChargeData > 0;
			SetDeployed(deployed);
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		OnCircuitStateChanged();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		OnCircuitStateChanged();
	}

	private void PrePool()
	{
		m_Wheels = GetComponent<ModuleWheels>();
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		SetDeployed(!m_IsRetractedByDefault);
	}
}
