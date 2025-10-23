using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleScoop : Module, TechWeapon.IMeleeWeapon
{
	[SerializeField]
	private Animation actuator;

	[SerializeField]
	private Collider containmentCollider;

	[SerializeField]
	private float scoopVelocityDamping = 0.2f;

	[SerializeField]
	private TechAudio.SFXType m_SFXTypeScoopLift;

	[SerializeField]
	private TechAudio.SFXType m_SFXTypeScoopRelease;

	[SerializeField]
	protected bool m_IsUsedOnCircuit = true;

	public Event<ModuleScoop, TriggerCatcher.Interaction, Collider> ContainmentTriggerEvent;

	private AnimationState m_LiftAnim;

	private AnimationState m_DropAnim;

	private bool m_LiftActive;

	private bool m_WantsLift;

	private bool m_UpAndDownMode;

	private Vector3 m_ScoopVelocityDamped;

	private WorldPosition m_ScoopPosLastUpdate;

	private bool m_ControllerInputting;

	public bool IsLifting => m_LiftActive;

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

	public bool IsActive => m_WantsLift;

	public void SetContinuousMode(bool continuous)
	{
		m_UpAndDownMode = continuous;
	}

	private void PlayScoopSfx(TechAudio.SFXType sfxType, float cooldown)
	{
		TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, sfxType, cooldown);
		base.block.tank.TechAudio.PlayOneshot(data);
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		RefreshIsLifting();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		if (base.block.IsAttached)
		{
			RefreshIsLifting();
		}
	}

	private void OnTankBeamEnabled(Tank tech, bool enabled)
	{
		if (base.block.IsAttached && base.block.tank == tech)
		{
			RefreshIsLifting();
		}
	}

	private void OnAttached()
	{
		base.block.tank.Weapons.RegisterMelee(this);
		base.block.tank.control.manualAimFireEvent.Subscribe(ControlInput);
		TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		TriggerCatcher.Subscribe(containmentCollider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnContainmentTrigger);
	}

	private void OnDetaching()
	{
		actuator.Play(m_LiftAnim.name);
		actuator.Stop();
		m_WantsLift = false;
		m_LiftActive = false;
		m_UpAndDownMode = false;
		base.block.tank.Weapons.UnregisterMelee(this);
		base.block.tank.control.manualAimFireEvent.Unsubscribe(ControlInput);
		TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		TriggerCatcher.Unsubscribe(containmentCollider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnContainmentTrigger);
	}

	private void ControlInput(int aim, bool doLift)
	{
		m_ControllerInputting = doLift;
		RefreshIsLifting();
	}

	private void RefreshIsLifting()
	{
		m_WantsLift = (!m_LiftActive || !m_UpAndDownMode) && ((!CircuitControlled) ? m_ControllerInputting : (base.block.CircuitNode.Receiver.CurrentChargeData > 0 && base.block.CircuitReceiver.ShouldProcessInput));
		if (!actuator.isPlaying && m_LiftActive != m_WantsLift)
		{
			m_LiftActive = m_WantsLift;
			if (m_LiftActive)
			{
				m_ScoopPosLastUpdate = WorldPosition.FromScenePosition(containmentCollider.transform.position);
			}
			actuator.Play((m_LiftActive ? m_LiftAnim : m_DropAnim).name);
			if (base.block.IsAttached)
			{
				TechAudio.SFXType sfxType = (m_LiftActive ? m_SFXTypeScoopLift : m_SFXTypeScoopRelease);
				AnimationState animationState = (m_LiftActive ? m_LiftAnim : m_DropAnim);
				PlayScoopSfx(sfxType, animationState.length);
			}
		}
	}

	private void OnContainmentTrigger(TriggerCatcher.Interaction t, Collider other)
	{
		if (t == TriggerCatcher.Interaction.Stay && actuator.isPlaying && m_LiftActive && (bool)other.attachedRigidbody && !other.attachedRigidbody.isKinematic)
		{
			other.attachedRigidbody.velocity = m_ScoopVelocityDamped;
		}
		ContainmentTriggerEvent.Send(this, t, other);
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
		foreach (AnimationState item in actuator)
		{
			if (item.name.ToLower().Contains("lift"))
			{
				m_LiftAnim = item;
			}
			else if (item.name.ToLower().Contains("drop"))
			{
				m_DropAnim = item;
			}
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		actuator.Play(m_LiftAnim.name);
		actuator.Stop();
	}

	private void LateUpdate()
	{
		if (actuator.isPlaying && m_LiftActive)
		{
			Vector3 vector = (containmentCollider.transform.position - m_ScoopPosLastUpdate.ScenePosition) / Time.deltaTime;
			if (vector != Vector3.zero)
			{
				m_ScoopVelocityDamped = m_ScoopVelocityDamped * (1f - scoopVelocityDamping) + vector * scoopVelocityDamping;
			}
			m_ScoopPosLastUpdate = WorldPosition.FromScenePosition(containmentCollider.transform.position);
		}
		else
		{
			m_ScoopVelocityDamped = Vector3.zero;
			m_ScoopPosLastUpdate = default(WorldPosition);
		}
	}
}
