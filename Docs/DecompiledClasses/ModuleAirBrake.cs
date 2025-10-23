#define UNITY_EDITOR
using UnityEngine;

public class ModuleAirBrake : ModuleAnimator
{
	[SerializeField]
	private Transform m_Effector;

	[SerializeField]
	private Vector3 m_Strength = new Vector3(0f, 0f, 0.1f);

	[SerializeField]
	private float m_MinForceThreshold = 1f;

	private AnimatorBool m_DeployBool = new AnimatorBool("Deploy");

	private bool m_Deployed;

	private bool m_NoInput;

	private bool m_HasVelocity;

	private void OnAttached()
	{
		base.block.tank.control.driveControlEvent.Subscribe(OnControlInput);
	}

	private void OnDetaching()
	{
		base.block.tank.control.driveControlEvent.Unsubscribe(OnControlInput);
		m_NoInput = true;
		m_HasVelocity = false;
		SetDeployed(value: false);
	}

	private void OnControlInput(TankControl.ControlState controlState)
	{
		m_NoInput = !controlState.AnyMovementOrBoostControl;
	}

	private void SetDeployed(bool value)
	{
		if (value != m_Deployed)
		{
			m_Deployed = value;
			Set(m_DeployBool, value);
		}
	}

	private void OnPool()
	{
		d.Assert(m_Effector != null, base.block.name + " - no effector specified");
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		m_NoInput = true;
		m_HasVelocity = false;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnRecycle()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
	}

	private void OnFixedUpdate()
	{
		if ((bool)base.block.tank && (bool)m_Effector)
		{
			Rigidbody rbody = base.block.tank.rbody;
			Vector3 vector = rbody.position + (m_Effector.position - rbody.transform.position);
			Vector3 pointVelocity = rbody.GetPointVelocity(vector);
			pointVelocity = base.block.tank.control.ZeroWorldVelocityInThrottledAxes(base.block.tank, pointVelocity);
			Vector3 vector2 = base.transform.InverseTransformVector(pointVelocity);
			vector2.x = vector2.x * vector2.x * Mathf.Sign(vector2.x) * m_Strength.x;
			vector2.y = vector2.y * vector2.y * Mathf.Sign(vector2.y) * m_Strength.y;
			vector2.z = vector2.z * vector2.z * Mathf.Sign(vector2.z) * m_Strength.z;
			m_HasVelocity = vector2.magnitude > m_MinForceThreshold;
			bool passiveBrakesEnabled = base.block.tank.PassiveBrakesEnabled;
			SetDeployed(passiveBrakesEnabled && (m_HasVelocity & m_NoInput));
			if (m_Deployed)
			{
				pointVelocity = base.transform.TransformVector(vector2);
				rbody.AddForceAtPosition(-pointVelocity, vector);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (m_Deployed)
		{
			Gizmos.DrawSphere(m_Effector.position, 1.2f);
		}
	}
}
