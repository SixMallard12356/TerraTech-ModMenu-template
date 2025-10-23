using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleSpeedo : Module, ICircuitDispensor
{
	[SerializeField]
	private float m_SpinnerRotationModifier = 0.0666f;

	[SerializeField]
	private float m_SpinnerMaxRotationSpeedChange = 1f;

	[SerializeField]
	private float m_StationaryEpsilon = 0.05f;

	[SerializeField]
	private bool m_ShowOnHUD = true;

	private Spinner[] m_Spinners;

	private float m_CurrentSpinSpeed;

	private float m_DisplaySpeed;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return Mathf.RoundToInt(m_DisplaySpeed);
	}

	private void OnAttached()
	{
		base.block.tank.HUDControl.AddHudElement(this, ManHUD.HUDElementType.Speedo);
	}

	private void OnDetaching()
	{
		base.block.tank.HUDControl.RemoveHudElement(this);
	}

	private void OnPool()
	{
		m_Spinners = GetComponentsInChildren<Spinner>(includeInactive: true);
		if (m_ShowOnHUD)
		{
			base.block.AttachedEvent.Subscribe(OnAttached);
			base.block.DetachingEvent.Subscribe(OnDetaching);
		}
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_DisplaySpeed = 0f;
		Spinner[] spinners = m_Spinners;
		for (int i = 0; i < spinners.Length; i++)
		{
			spinners[i].SetAngle(0f);
		}
	}

	private void OnUpdate()
	{
		float num = (base.block.IsAttached ? base.block.tank.GetForwardSpeed() : 0f);
		m_DisplaySpeed = GameUnits.GetSpeed(num);
		if (m_Spinners != null)
		{
			if (num < m_StationaryEpsilon)
			{
				num = 0f;
			}
			float deltaTime = Time.deltaTime;
			m_CurrentSpinSpeed = Mathf.MoveTowards(m_CurrentSpinSpeed, num, deltaTime * m_SpinnerMaxRotationSpeedChange);
			float num2 = m_CurrentSpinSpeed * m_SpinnerRotationModifier;
			Spinner[] spinners = m_Spinners;
			foreach (Spinner obj in spinners)
			{
				obj.UpdateSpin(obj.Speed * num2 * deltaTime);
			}
		}
	}
}
