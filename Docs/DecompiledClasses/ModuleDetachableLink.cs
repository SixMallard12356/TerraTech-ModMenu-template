using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleDetachableLink : Module
{
	[SerializeField]
	private Transform m_ExplosionFX;

	[SerializeField]
	[Tooltip("The order in which the explosion occurs, Lower is sooner")]
	[Range(1f, 4f)]
	private int m_ExplosionOrder = 1;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private bool m_DetachQueued;

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

	private void DetachBlock()
	{
		if (base.block.tank != null)
		{
			if (base.block.tank.beam.IsActive)
			{
				base.block.tank.beam.EnableBeam(enable: false);
			}
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ShowExplosiveBoltsFXMessage message = new ShowExplosiveBoltsFXMessage
				{
					m_BlockId = base.block.blockPoolID
				};
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ShowExplosiveBoltsFX, message);
			}
			Singleton.Manager<ManLooseBlocks>.inst.HostDetachBlock(base.block, allowHeadlessTech: false, propagate: true);
		}
		PlayExplosionFX();
		Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(base.block);
	}

	public void PlayExplosionFX()
	{
		if (m_ExplosionFX != null)
		{
			Explosion component = m_ExplosionFX.Spawn(Singleton.dynamicContainer, base.block.trans.position).GetComponent<Explosion>();
			if (component != null)
			{
				component.SetDamageSource(base.block.tank);
			}
		}
	}

	private void DetachExplosively()
	{
		m_DetachQueued = true;
	}

	private void OnAttached()
	{
		base.block.tank.control.explosiveBoltDetonateEvents[m_ExplosionOrder - 1].Subscribe(OnDetachTriggeredByTechController);
	}

	private void OnDetaching()
	{
		base.block.tank.control.explosiveBoltDetonateEvents[m_ExplosionOrder - 1].Unsubscribe(OnDetachTriggeredByTechController);
	}

	private void OnDetachTriggeredByTechController()
	{
		if (!CircuitControlled)
		{
			DetachExplosively();
		}
	}

	private void OnShowExplosiveBoltsFXMessage(NetworkMessage msg)
	{
		ShowExplosiveBoltsFXMessage showExplosiveBoltsFXMessage = msg.ReadMessage<ShowExplosiveBoltsFXMessage>();
		if (base.block.blockPoolID == showExplosiveBoltsFXMessage.m_BlockId)
		{
			PlayExplosionFX();
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		if (CircuitControlled && base.block.CircuitReceiver.ShouldProcessInput && charge > 0)
		{
			DetachExplosively();
		}
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
		base.block.BlockUpdate.Subscribe(OnUpdate);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		m_DetachQueued = false;
	}

	private void OnUpdate()
	{
		if (m_DetachQueued)
		{
			DetachBlock();
			m_DetachQueued = false;
		}
	}
}
