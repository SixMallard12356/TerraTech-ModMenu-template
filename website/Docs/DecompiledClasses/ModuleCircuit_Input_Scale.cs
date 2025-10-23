using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_Input_Scale : Module, ICircuitDispensor, INetworkedModule
{
	[SerializeField]
	private Collider m_Collider;

	private int m_AccumulationFixedFrame;

	private Vector3 m_AccumulatedImpulse;

	private const int kNumFixedFrameJumps = 2;

	private NetworkedProperty<BoolParamBlockMessage> net_PowerControlSetting;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		if (m_AccumulationFixedFrame + 2 < Singleton.instance.FixedFrameCount)
		{
			return 0;
		}
		int result = 0;
		if (m_AccumulatedImpulse.sqrMagnitude > 0.01f)
		{
			result = Mathf.Max(Mathf.RoundToInt((base.block.trans.InverseTransformVector(m_AccumulatedImpulse) / Time.fixedDeltaTime).z), 0);
		}
		return result;
	}

	private void OnMPPowerControlSynced(BoolParamBlockMessage msg)
	{
	}

	private void OnAttached()
	{
		base.block.tank.CollisionEvent.Subscribe(OnTankCollision);
	}

	private void OnDetaching()
	{
		base.block.tank.CollisionEvent.Unsubscribe(OnTankCollision);
	}

	private void OnTankCollision(Tank.CollisionInfo collision, Tank.CollisionInfo.Event e)
	{
		if ((e == Tank.CollisionInfo.Event.Stay || e == Tank.CollisionInfo.Event.Enter) && (object)collision.a.collider == m_Collider)
		{
			int fixedFrameCount = Singleton.instance.FixedFrameCount;
			if (fixedFrameCount != m_AccumulationFixedFrame)
			{
				m_AccumulatedImpulse = Vector3.zero;
				m_AccumulationFixedFrame = fixedFrameCount;
			}
			Vector3 vector = collision.impulse;
			if (Vector3.Dot(collision.normal, vector) < 0f)
			{
				vector = -vector;
			}
			m_AccumulatedImpulse += vector;
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		net_PowerControlSetting = new NetworkedProperty<BoolParamBlockMessage>(this, TTMsgType.SyncCircuitInputButton, OnMPPowerControlSynced);
	}

	private void OnSpawn()
	{
		m_AccumulationFixedFrame = -1;
		m_AccumulatedImpulse = Vector3.zero;
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Input_KeyBind;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_PowerControlSetting.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		net_PowerControlSetting.Deserialise(reader);
	}
}
