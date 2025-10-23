#define UNITY_EDITOR
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleTechController))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleTechCoupling : Module, ICustomTechController, IHUDPowerToggleControlledModule
{
	[SerializeField]
	private Transform m_JointPosition;

	[SerializeField]
	private Transform m_JointPairPosition;

	[SerializeField]
	private float m_MaxActivationRange = 1f;

	[SerializeField]
	private float m_JointSpring = 100f;

	[SerializeField]
	private float m_JointDamper = 30f;

	[SerializeField]
	private float m_JointLimit;

	[SerializeField]
	private float m_JointBounciness;

	[SerializeField]
	private float m_JointContactDistance;

	[SerializeField]
	private bool m_ProcessInput = true;

	[SerializeField]
	private float m_MaxDriveChange = 5f;

	[SerializeField]
	private float m_MaxTurnChange = 5f;

	[SerializeField]
	private float m_AlignmentBasedTurnContribution = 0.6f;

	[SerializeField]
	private float m_ForceBasedTurnContribution = 0.4f;

	[SerializeField]
	private Gradient m_LinkGradient;

	[SerializeField]
	private Gradient m_UnlinkGradient;

	private ConfigurableJoint m_Joint;

	private ModuleTechCoupling m_CouplePair;

	private Tank m_Leader;

	private float m_DriveInput;

	private float m_TurnInput;

	private ModuleTechCoupling m_ClosestTarget;

	private ArcEffect[] m_ArcEffects;

	private static readonly Bitfield<ObjectTypes> kBlockOnlyHitmask = new Bitfield<ObjectTypes>(2);

	public Vector3 CouplingPosition => m_JointPosition.position;

	public bool IsCoupled => m_CouplePair.IsNotNull();

	public bool IsLeader
	{
		get
		{
			if (IsCoupled)
			{
				return m_Leader.IsNull();
			}
			return false;
		}
	}

	public bool IsFollower
	{
		get
		{
			if (IsCoupled)
			{
				return m_Leader.IsNotNull();
			}
			return false;
		}
	}

	int ICustomTechController.DefaultPriority
	{
		get
		{
			if (!IsFollower)
			{
				return 0;
			}
			return 2;
		}
	}

	bool IHUDPowerToggleControlledModule.PowerControlSetting
	{
		get
		{
			return !IsCoupled;
		}
		set
		{
			if (!IsCoupled)
			{
				TryPrimeCouplingTarget();
				if (m_ClosestTarget != null)
				{
					TryAchieveCoupling(m_ClosestTarget);
				}
			}
			else
			{
				Decouple();
			}
		}
	}

	Gradient IHUDPowerToggleControlledModule.ToggleGradientOverride
	{
		get
		{
			if (!IsCoupled)
			{
				return m_LinkGradient;
			}
			return m_UnlinkGradient;
		}
	}

	bool IHUDPowerToggleControlledModule.AutoCloseMenuOnComplete => false;

	bool IHUDPowerToggleControlledModule.CanOpenMenuOnBlock => true;

	bool ICustomTechController.ExecuteControl(bool additive)
	{
		if (!IsCoupled)
		{
			return false;
		}
		if (m_ProcessInput && IsFollower)
		{
			ConfigurableJoint joint = m_CouplePair.m_Joint;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			joint.currentForce.SetY(0f);
			Vector3 normalized = joint.currentForce.normalized;
			float value = 0f - Vector3.Dot(normalized, m_JointPosition.forward);
			float num = Mathf.Abs(m_Leader.control.CurState.Drive);
			value = Mathf.Clamp(value, 0f - num, num);
			m_DriveInput = Mathf.MoveTowards(m_DriveInput, value, m_MaxDriveChange * Time.deltaTime);
			zero.z = m_DriveInput;
			float num2 = 0f;
			if (Mathf.Sign(Vector3.Dot(m_Leader.trans.forward, m_JointPosition.forward)) * m_Leader.control.CurState.Drive < 0f)
			{
				num2 = Vector3.Dot(normalized, m_JointPosition.right);
			}
			else
			{
				float num3 = Vector3.Dot(normalized, m_JointPosition.right);
				num2 = Mathf.Clamp(Vector3.Dot(base.block.tank.trans.forward, m_Leader.trans.right) * m_AlignmentBasedTurnContribution + num3 * m_ForceBasedTurnContribution, -1f, 1f);
			}
			num2 *= Mathf.Abs(m_DriveInput);
			m_TurnInput = Mathf.MoveTowards(m_TurnInput, num2, m_MaxTurnChange * Time.deltaTime);
			zero2.y = m_TurnInput;
			base.block.tank.control.CollectMovementInput(zero, zero2, Vector3.zero, m_Leader.control.CurState.m_BoostProps, m_Leader.control.CurState.m_BoostJets);
			return true;
		}
		return false;
	}

	public void CoupleToNearestTarget()
	{
	}

	private void TryPrimeCouplingTarget()
	{
		if (!base.block.tank.IsNotNull() || IsCoupled)
		{
			return;
		}
		ModuleTechCoupling moduleTechCoupling = null;
		float num = float.MaxValue;
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(m_JointPosition.position, m_MaxActivationRange, kBlockOnlyHitmask))
		{
			ModuleTechCoupling component;
			if (item != base.block.visible && item.block.IsNotNull() && (item.block.tank == null || item.block.tank != base.block.tank) && (object)(component = item.block.GetComponent<ModuleTechCoupling>()) != null)
			{
				float sqrMagnitude = (CouplingPosition - component.CouplingPosition).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					moduleTechCoupling = component;
				}
			}
		}
		if (moduleTechCoupling != null)
		{
			if (moduleTechCoupling != m_ClosestTarget)
			{
				m_ArcEffects[0].Fire(-1);
				m_ClosestTarget = moduleTechCoupling;
			}
		}
		else if (m_ClosestTarget != null)
		{
			m_ArcEffects[0].Hide();
			m_ClosestTarget = null;
		}
	}

	private bool TryAchieveCoupling(ModuleTechCoupling target)
	{
		d.Assert(base.block.tank.IsNotNull(), "Trying to engage coupling while the block is not attached to a tech!");
		d.Assert(target != this, "Trying to couple to itself..");
		d.Assert(target.block.tank == null || target.block.tank != base.block.tank, "Can't create coupling between blocks on the same tech!");
		m_Joint = base.block.tank.gameObject.AddComponent<ConfigurableJoint>();
		m_Joint.anchor = base.block.tank.trans.InverseTransformPoint(CouplingPosition);
		m_Joint.xMotion = ConfigurableJointMotion.Limited;
		m_Joint.yMotion = ConfigurableJointMotion.Limited;
		m_Joint.zMotion = ConfigurableJointMotion.Limited;
		m_Joint.linearLimitSpring = new SoftJointLimitSpring
		{
			spring = m_JointSpring,
			damper = m_JointDamper
		};
		m_Joint.linearLimit = new SoftJointLimit
		{
			limit = m_JointLimit,
			bounciness = m_JointBounciness,
			contactDistance = m_JointContactDistance
		};
		m_Joint.enableCollision = true;
		Visible visible = (target.block.tank.IsNotNull() ? target.block.tank.visible : target.block.visible);
		m_Joint.connectedBody = visible.rbody;
		Vector3 position = ((m_JointPairPosition != null) ? m_JointPairPosition.position : CouplingPosition);
		m_Joint.connectedAnchor = visible.trans.InverseTransformPoint(position);
		ConfigureCoupleRelationship(target, this);
		int num = 0;
		ArcEffect[] arcEffects = m_ArcEffects;
		for (int i = 0; i < arcEffects.Length; i++)
		{
			arcEffects[i].Fire(num++);
		}
		return true;
	}

	private void Decouple()
	{
		if (IsFollower)
		{
			m_CouplePair.Decouple();
			return;
		}
		if (IsCoupled)
		{
			ConfigureCoupleRelationship(null, null);
		}
		if (m_Joint != null)
		{
			Object.Destroy(m_Joint);
			m_Joint = null;
		}
		ArcEffect[] arcEffects = m_ArcEffects;
		for (int i = 0; i < arcEffects.Length; i++)
		{
			arcEffects[i].Hide();
		}
		m_ClosestTarget = null;
	}

	private void ConfigureCoupleRelationship(ModuleTechCoupling partner, ModuleTechCoupling leader, bool propagate = true)
	{
		d.Assert(partner.IsNull() == leader.IsNull(), "Parameters must either both be null, or both be non-null!");
		if (propagate && m_CouplePair != null)
		{
			m_CouplePair.ConfigureCoupleRelationship(null, null, propagate: false);
		}
		m_CouplePair = partner;
		m_Leader = ((leader == null || leader == this) ? null : leader.block.tank);
		if (propagate && m_CouplePair != null)
		{
			m_CouplePair.ConfigureCoupleRelationship(this, leader, propagate: false);
		}
	}

	private void OnAttached()
	{
	}

	private void OnDetaching()
	{
		Decouple();
	}

	private void UpdateVFX()
	{
		if (IsCoupled && IsLeader)
		{
			Vector3 couplingPosition = CouplingPosition;
			Vector3 couplingPosition2 = m_CouplePair.CouplingPosition;
			ArcEffect[] arcEffects = m_ArcEffects;
			for (int i = 0; i < arcEffects.Length; i++)
			{
				arcEffects[i].UpdatePositionIfActive(couplingPosition, couplingPosition2);
			}
		}
		else if (!IsCoupled && m_ClosestTarget != null)
		{
			m_ArcEffects[0].UpdatePositionIfActive(CouplingPosition, m_ClosestTarget.CouplingPosition);
		}
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		LineRenderer[] componentsInChildren = GetComponentsInChildren<LineRenderer>(includeInactive: true);
		m_ArcEffects = componentsInChildren.Select((LineRenderer l) => new ArcEffect(l)).ToArray();
	}

	private void OnSpawn()
	{
		ArcEffect[] arcEffects = m_ArcEffects;
		for (int i = 0; i < arcEffects.Length; i++)
		{
			arcEffects[i].Hide();
		}
	}

	private void OnRecycle()
	{
	}

	private void Update()
	{
		UpdateVFX();
	}
}
