using UnityEngine;

public class ModulePacemaker : Module, ManPointer.OpenMenuEventConsumer
{
	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private bool m_IsPartOfTech;

	private static readonly TechHolders.HeartbeatSpeed[] kCircuitChargeValueToSpeed = new TechHolders.HeartbeatSpeed[4]
	{
		TechHolders.HeartbeatSpeed.Paused,
		TechHolders.HeartbeatSpeed.Slow,
		TechHolders.HeartbeatSpeed.Normal,
		TechHolders.HeartbeatSpeed.Fast
	};

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitReceiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	public bool CanOpenMenu(bool isRadial)
	{
		if (CircuitControlled)
		{
			return false;
		}
		if (isRadial && base.block.IsAttached)
		{
			return !base.block.tank.IsEnemy();
		}
		return false;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (CircuitControlled)
		{
			return false;
		}
		if (openMenu.m_AllowRadialMenu)
		{
			Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.Pacemaker, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), openMenu);
			return true;
		}
		return false;
	}

	private void UpdateSettingFromCircuitInput()
	{
		if (m_IsPartOfTech && ManNetwork.IsHost && CircuitControlled)
		{
			int num = Mathf.Clamp(base.block.CircuitReceiver.CurrentChargeData.ChargeStrength, 0, kCircuitChargeValueToSpeed.Length - 1);
			TechHolders.HeartbeatSpeed speed = kCircuitChargeValueToSpeed[num];
			base.block.tank.Holders.RequestSetHeartbeatSpeed(speed);
		}
	}

	private void OnAttached()
	{
		base.block.tank.Holders.AddPacemaker(this);
		m_IsPartOfTech = true;
	}

	private void OnDetaching()
	{
		m_IsPartOfTech = false;
		base.block.tank.Holders.RemovePacemaker(this);
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		UpdateSettingFromCircuitInput();
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		UpdateSettingFromCircuitInput();
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}
}
