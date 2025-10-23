#define UNITY_EDITOR
using Rewired;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Input_KeyBind : Module, ICircuitDispensor, INetworkedModule
{
	[SerializeField]
	[Tooltip("The number of circuit ticks to output for once triggered")]
	private int m_NumOutputTicks = 10;

	[SerializeField]
	private Animation m_Anim;

	private int m_OutputTicksRemaining;

	private bool m_IsOn;

	private int m_KeyDownFrameNr;

	private int m_CurrentAssignedRewiredAction = -1;

	private NetworkedProperty<BoolParamBlockMessage> net_PowerControlSetting;

	protected const int kActionIDInvalid = int.MinValue;

	private bool IsOutputting
	{
		get
		{
			if (m_OutputTicksRemaining <= 0)
			{
				return m_KeyDownFrameNr == Time.frameCount;
			}
			return true;
		}
	}

	protected int CurrentAssignedRewiredAction => m_CurrentAssignedRewiredAction;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		if (m_OutputTicksRemaining > 0)
		{
			m_OutputTicksRemaining--;
		}
		if (!m_IsOn && !IsOutputting)
		{
			return 0;
		}
		return base.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	protected void SetOn(bool isOn)
	{
		if (isOn != m_IsOn)
		{
			if (!m_IsOn)
			{
				m_OutputTicksRemaining = m_NumOutputTicks;
				m_KeyDownFrameNr = Time.frameCount;
			}
			else
			{
				m_OutputTicksRemaining = 0;
				m_KeyDownFrameNr = -1;
			}
		}
		else if (m_IsOn && isOn)
		{
			m_OutputTicksRemaining = Mathf.Max(m_OutputTicksRemaining, 1);
		}
		if (isOn != m_IsOn && m_Anim != null && m_Anim.clip != null)
		{
			bool flag = !m_Anim.isPlaying;
			foreach (AnimationState item in m_Anim)
			{
				flag = flag || item.normalizedTime < 0.1f || item.normalizedTime > 0.9f;
				item.speed = (isOn ? 1f : (-1f));
				if (flag)
				{
					item.normalizedTime = ((!isOn) ? 1 : 0);
					m_Anim.Play();
				}
			}
		}
		m_IsOn = isOn;
		net_PowerControlSetting.Data.value = m_IsOn;
	}

	protected void SetRewiredActionID(int rewiredActionID)
	{
		int currentAssignedRewiredAction = m_CurrentAssignedRewiredAction;
		m_CurrentAssignedRewiredAction = rewiredActionID;
		if (currentAssignedRewiredAction != m_CurrentAssignedRewiredAction && base.block.IsAttached && base.block.tank.IsPlayer)
		{
			EnableKeyHandler(enable: false, currentAssignedRewiredAction);
			EnableKeyHandler(enable: true, m_CurrentAssignedRewiredAction);
		}
	}

	protected void EnableKeyHandler(bool enable, int actionID)
	{
		if (m_CurrentAssignedRewiredAction == int.MinValue)
		{
			d.LogErrorFormat(this, "Input block {0} was not assigned a rewired action and will not function!", base.name);
			return;
		}
		if (enable)
		{
			EnableKeyHandler_Internal(enable: true, actionID);
			return;
		}
		EnableKeyHandler_Internal(enable: false, actionID);
		if (m_IsOn)
		{
			SetOn(isOn: false);
			net_PowerControlSetting.Sync();
		}
	}

	protected void HandleKeyPressed()
	{
		if (IsBlockInputEnabled())
		{
			SetOn(isOn: true);
			net_PowerControlSetting.Sync();
		}
	}

	protected void HandleKeyReleased()
	{
		if (m_IsOn && IsBlockInputEnabled())
		{
			SetOn(isOn: false);
			net_PowerControlSetting.Sync();
		}
	}

	private bool IsBlockInputEnabled()
	{
		if (base.block.IsInteractible && !base.block.damage.AboutToDie && !Singleton.Manager<ManGameMode>.inst.LockPlayerControls)
		{
			if (!Singleton.Manager<CameraManager>.inst.IsCurrent<TankCamera>())
			{
				if (Singleton.Manager<CameraManager>.inst.IsCurrent<DebugCamera>())
				{
					return Singleton.Manager<CameraManager>.inst.GetCamera<DebugCamera>().IsLocked;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	protected virtual void EnableKeyHandler_Internal(bool enable, int actionID)
	{
		if (enable)
		{
			Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(actionID, InputActionEventType.ButtonJustPressed, OnKeyPressed);
			Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(actionID, InputActionEventType.ButtonJustReleased, OnKeyReleased);
		}
		else
		{
			Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(actionID, InputActionEventType.ButtonJustPressed, OnKeyPressed);
			Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(actionID, InputActionEventType.ButtonJustReleased, OnKeyReleased);
		}
	}

	private void OnMPPowerControlSynced(BoolParamBlockMessage msg)
	{
		SetOn(msg.value);
	}

	private void OnAttached()
	{
		SetOn(isOn: false);
		if (base.block.tank.IsPlayer)
		{
			EnableKeyHandler(enable: true, m_CurrentAssignedRewiredAction);
		}
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
	}

	private void OnDetaching()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		if (base.block.tank.IsPlayer)
		{
			EnableKeyHandler(enable: false, m_CurrentAssignedRewiredAction);
		}
		SetOn(isOn: false);
	}

	private void OnKeyPressed(InputActionEventData eventData)
	{
		HandleKeyPressed();
	}

	private void OnKeyReleased(InputActionEventData eventData)
	{
		HandleKeyReleased();
	}

	private void OnPlayerTankChanged(Tank tank, bool willBePlayerControlled)
	{
		if ((object)base.block.tank == tank)
		{
			EnableKeyHandler(willBePlayerControlled, m_CurrentAssignedRewiredAction);
		}
	}

	private void OnLockTimeoutSet(Visible.LockTimerTypes lockType)
	{
		if (m_IsOn && lockType == Visible.LockTimerTypes.Interactible)
		{
			SetOn(isOn: false);
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.visible.LockTimeoutSetEvent.Subscribe(OnLockTimeoutSet);
		net_PowerControlSetting = new NetworkedProperty<BoolParamBlockMessage>(this, TTMsgType.SyncCircuitInputButton, OnMPPowerControlSynced);
	}

	private void OnSpawn()
	{
		foreach (AnimationState item in m_Anim)
		{
			item.normalizedTime = 0f;
		}
		m_Anim.Sample();
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public virtual NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Input_KeyBind;
	}

	public virtual void OnSerialize(NetworkWriter writer)
	{
		net_PowerControlSetting.Serialise(writer);
	}

	public virtual void OnDeserialize(NetworkReader reader)
	{
		net_PowerControlSetting.Deserialise(reader);
	}
}
