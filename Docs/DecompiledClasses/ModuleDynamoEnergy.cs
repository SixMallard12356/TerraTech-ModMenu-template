using UnityEngine;

public class ModuleDynamoEnergy : Module
{
	[SerializeField]
	private float m_SpinnerRotationModifier = 0.0666f;

	[SerializeField]
	private float m_SpinnerMaxRotationSpeedChange = 1f;

	[SerializeField]
	private float m_MaxEnergySupplyRate;

	[SerializeField]
	private TechEnergy.EnergyType m_EnergySupplyType;

	[SerializeField]
	private float m_MinForwardSpeed;

	[SerializeField]
	private float m_MaxForwardSpeed;

	[SerializeField]
	private AnimationCurve m_EnergySpeedGraph;

	[SerializeField]
	private bool m_MustBeOnGround;

	private ModuleEnergy m_ModuleEnergy;

	private Spinner[] m_Spinners;

	private float m_CurrentSpeed;

	private float m_ForwardSpeed;

	private bool m_CanGenerate;

	private void UpdateSpinners(float speed)
	{
		if (m_ForwardSpeed >= m_MinForwardSpeed && m_CanGenerate && m_Spinners != null)
		{
			float deltaTime = Time.deltaTime;
			m_CurrentSpeed = Mathf.MoveTowards(m_CurrentSpeed, speed, deltaTime * m_SpinnerMaxRotationSpeedChange);
			float num = m_CurrentSpeed * m_SpinnerRotationModifier * deltaTime;
			for (int i = 0; i < m_Spinners.Length; i++)
			{
				m_Spinners[i].UpdateSpin(m_Spinners[i].Speed * num);
			}
		}
	}

	private void OnUpdateSupplyEnergy()
	{
		if (m_ForwardSpeed >= m_MinForwardSpeed && m_CanGenerate)
		{
			if (m_ForwardSpeed >= m_MaxForwardSpeed)
			{
				m_ForwardSpeed = m_MaxForwardSpeed;
			}
			float time = Mathf.InverseLerp(m_MinForwardSpeed, m_MaxForwardSpeed, m_ForwardSpeed);
			float num = m_EnergySpeedGraph.Evaluate(time) * m_MaxEnergySupplyRate;
			m_ModuleEnergy.Supply(m_EnergySupplyType, num);
			if (base.block.tank != null && base.block.tank.IsFriendly(0))
			{
				Singleton.Manager<ManStats>.inst.EnergyGenerated(base.block, num);
			}
		}
	}

	private void OnPool()
	{
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		if ((bool)m_ModuleEnergy)
		{
			m_ModuleEnergy.UpdateSupplyEvent.Subscribe(OnUpdateSupplyEnergy);
		}
		m_Spinners = GetComponentsInChildren<Spinner>(includeInactive: true);
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_CurrentSpeed = 0f;
	}

	private void OnUpdate()
	{
		m_ForwardSpeed = ((base.block.tank != null) ? base.block.tank.GetForwardSpeed() : 0f);
		if (base.block.tank.IsNotNull())
		{
			if (m_MustBeOnGround)
			{
				m_CanGenerate = base.block.tank.grounded && base.block.tank.wheelGrounded;
			}
			else
			{
				m_CanGenerate = true;
			}
		}
		else
		{
			m_CanGenerate = false;
		}
		UpdateSpinners(m_ForwardSpeed);
	}
}
