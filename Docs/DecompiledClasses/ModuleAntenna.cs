using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleAnimator))]
public class ModuleAntenna : Module, INetworkedModule
{
	private enum States
	{
		Deploy,
		Glow
	}

	[SerializeField]
	private GameObject m_GlowEffectObj;

	[SerializeField]
	private float m_TransitionDelay;

	[SerializeField]
	private TechAudio.SFXType m_SFXType;

	private Bitfield<States> _RequestStates = new Bitfield<States>();

	private ParticleSystem[] m_ParticleSystems;

	private ModuleAnimator m_Animator;

	private bool m_Deployed;

	private bool m_Glowing;

	private bool m_Transitioning;

	private float m_TransitionTimer;

	private float m_SFXLoopSyncDelay;

	private float m_AudioTimer;

	private AnimatorTrigger m_TrigUnfold = new AnimatorTrigger("UnfoldAntenna");

	private AnimatorTrigger m_TrigFold = new AnimatorTrigger("FoldAntenna");

	private NetworkedProperty<IntParamBlockMessage> m_SyncedState;

	public bool RequestGlow => _RequestStates.Contains(1);

	public bool RequestDeploy => _RequestStates.Contains(0);

	public void SetActive(bool active, bool netSend = true)
	{
		_RequestStates.Set(0, active);
		_RequestStates.Set(1, active);
		if (m_SyncedState.Data.value != _RequestStates.Field)
		{
			m_SyncedState.Data.value = _RequestStates.Field;
			if (netSend)
			{
				m_SyncedState.Sync();
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleAntenna;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		m_SyncedState.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_SyncedState.Deserialise(reader);
	}

	private void OnMPStateSet(IntParamBlockMessage msg)
	{
		_RequestStates.SetFlags(msg.value);
	}

	private void OnDetaching()
	{
		SetActive(active: false, netSend: false);
	}

	private void OnPool()
	{
		m_Animator = GetComponent<ModuleAnimator>();
		if (m_GlowEffectObj != null)
		{
			bool includeInactive = true;
			m_ParticleSystems = m_GlowEffectObj.GetComponentsInChildren<ParticleSystem>(includeInactive);
			if (m_ParticleSystems.Length != 0)
			{
				m_SFXLoopSyncDelay = m_ParticleSystems[0].main.duration;
			}
		}
		else
		{
			m_ParticleSystems = new ParticleSystem[0];
		}
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_SyncedState = new NetworkedProperty<IntParamBlockMessage>(this, TTMsgType.SetAntennaState, OnMPStateSet);
	}

	private void OnSpawn()
	{
		if (m_GlowEffectObj != null)
		{
			m_GlowEffectObj.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		m_Deployed = false;
		m_Glowing = false;
		m_Transitioning = false;
		m_TransitionTimer = 0f;
	}

	private void OnUpdate()
	{
		if (m_Transitioning)
		{
			m_TransitionTimer -= Time.deltaTime;
			if (m_TransitionTimer <= 0f)
			{
				m_Deployed = !m_Deployed;
				m_Transitioning = false;
			}
		}
		if (m_Deployed != RequestDeploy && !m_Transitioning)
		{
			if (RequestDeploy)
			{
				m_Animator.Set(m_TrigUnfold);
				m_Animator.Reset(m_TrigFold);
			}
			else
			{
				m_Animator.Set(m_TrigFold);
				m_Animator.Reset(m_TrigUnfold);
			}
			m_Transitioning = true;
			m_TransitionTimer = m_TransitionDelay;
		}
		if (!(m_GlowEffectObj != null))
		{
			return;
		}
		bool flag = RequestGlow && m_Deployed && !m_Transitioning;
		if (flag != m_Glowing)
		{
			for (int i = 0; i < m_ParticleSystems.Length; i++)
			{
				ParticleSystem.MainModule main = m_ParticleSystems[i].main;
				main.loop = flag;
			}
			m_GlowEffectObj.SetActive(flag);
			m_Glowing = flag;
			if (flag)
			{
				m_AudioTimer = 0f;
			}
		}
		if (flag)
		{
			m_AudioTimer -= Time.deltaTime;
			if (m_AudioTimer <= 0f)
			{
				m_AudioTimer += m_SFXLoopSyncDelay;
				TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, m_SFXType);
				base.block.tank.TechAudio.PlayOneshot(data);
			}
		}
	}
}
