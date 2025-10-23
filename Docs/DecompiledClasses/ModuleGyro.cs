#define UNITY_EDITOR
using System;
using System.Globalization;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleGyro : Module
{
	private enum UseFilter
	{
		Never,
		Always,
		NoInput,
		NoDrive,
		NoTurn,
		AnyInput,
		HasDrive,
		HasTurn
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float trim;
	}

	[SerializeField]
	private float m_TrimPitchMax;

	[SerializeField]
	private float m_TrimAdjustSpeed;

	[SerializeField]
	private UseFilter m_UsePassive;

	[SerializeField]
	private Vector3 m_PassiveStrength = new Vector3(100f, 100f, 100f);

	[SerializeField]
	private UseFilter m_UseActive = UseFilter.Always;

	[FormerlySerializedAs("speed")]
	[SerializeField]
	private float m_ActiveSpeed = 50f;

	[FormerlySerializedAs("stability")]
	[SerializeField]
	private float m_ActiveStability = 0.01f;

	[SerializeField]
	private Vector3 m_ActiveAxisScale = new Vector3(1f, 1f, 1f);

	[SerializeField]
	private Transform m_RotationAnimTransform;

	[SerializeField]
	private float m_RotationAnimSpeed = 4f;

	[SerializeField]
	private float m_RotationMinSpeed = 15f;

	[SerializeField]
	private float m_RotationMaxSpeed = 350f;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleAnimator m_ModuleAnimator;

	private AnimatorBool m_AnimatorEnableSpin = new AnimatorBool("SpinActive");

	public const float kDefaultTrim = 0f;

	private Vector3 m_PrevAngularVel;

	private float m_Trim;

	private bool m_HasTurnInput;

	private bool m_HasDriveInput;

	private float m_ControlTrim;

	private float m_ControlTrimTarget;

	private bool m_IsEnabled;

	public float Trim
	{
		get
		{
			return m_Trim;
		}
		set
		{
			m_Trim = value;
		}
	}

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

	private bool IsEnabled
	{
		get
		{
			if (m_IsEnabled)
			{
				if (CircuitControlled)
				{
					return base.block.CircuitReceiver.CurrentChargeData > 0;
				}
				return true;
			}
			return false;
		}
	}

	private bool IsTrimControlEnabled => IsCategoryEnabled();

	public void SetEnabled(bool enable)
	{
		m_IsEnabled = enable;
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
		m_ControlCategoryType = ModuleControlCategory.GyroTrim;
		m_ModuleAnimator = GetComponent<ModuleAnimator>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		m_Trim = 0f;
		m_IsEnabled = true;
	}

	private void OnAttached()
	{
		if (base.block.tank.BlockStateController.GyroTrimControllerCount > 0)
		{
			m_Trim = base.block.tank.BlockStateController.GyroTrim;
		}
		base.block.tank.BlockStateController.AddGyro(this);
		base.block.tank.control.driveControlEvent.Subscribe(OnControlInput);
	}

	private void OnDetaching()
	{
		base.block.tank.BlockStateController.RemoveGyro(this);
		base.block.tank.control.driveControlEvent.Unsubscribe(OnControlInput);
		m_HasTurnInput = false;
		m_HasDriveInput = false;
		m_ControlTrim = 0f;
		m_ControlTrimTarget = 0f;
	}

	private void OnControlInput(TankControl.ControlState controlState)
	{
		m_HasDriveInput = controlState.InputMovement != Vector3.zero;
		m_HasTurnInput = controlState.InputRotation != Vector3.zero;
		m_ControlTrimTarget = Mathf.Clamp(0f - controlState.InputRotation.x, -1f - m_Trim, 1f - m_Trim);
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.trim = m_Trim;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_Trim = Mathf.Clamp(serialData2.trim, -1f, 1f);
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(GetType(), "trim", m_Trim.ToString(CultureInfo.InvariantCulture));
			return;
		}
		string text = context.Retrieve(GetType(), "trim");
		if (text != null && text.Length > 0)
		{
			if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out m_Trim))
			{
				m_Trim = Mathf.Clamp(m_Trim, -1f, 1f);
				return;
			}
			d.LogError("ModuleGyro.OnSerializeText - Failed to parse trim setting from save data on block '" + base.block.name + "'. Expected float value 0-1 but got '" + text + "'. Setting to default value of 0!");
			m_Trim = 0f;
		}
		else
		{
			m_Trim = 0f;
		}
	}

	private bool TestUseFilter(UseFilter filter)
	{
		bool result = false;
		switch (filter)
		{
		case UseFilter.Never:
			result = false;
			break;
		case UseFilter.Always:
			result = true;
			break;
		case UseFilter.NoInput:
			result = !(m_HasDriveInput | m_HasTurnInput);
			break;
		case UseFilter.NoDrive:
			result = !m_HasDriveInput;
			break;
		case UseFilter.NoTurn:
			result = !m_HasTurnInput;
			break;
		case UseFilter.AnyInput:
			result = m_HasDriveInput | m_HasTurnInput;
			break;
		case UseFilter.HasDrive:
			result = m_HasDriveInput;
			break;
		case UseFilter.HasTurn:
			result = m_HasTurnInput;
			break;
		}
		return result;
	}

	public void EnableResistive(bool enable)
	{
		if (m_UseActive != UseFilter.Never)
		{
			m_UseActive = (enable ? UseFilter.Always : UseFilter.NoInput);
		}
		if (m_UsePassive != UseFilter.Never)
		{
			m_UsePassive = (enable ? UseFilter.Always : UseFilter.NoInput);
		}
	}

	private void OnFixedUpdate()
	{
		bool value = false;
		if (base.block.IsAttached && IsEnabled && !base.block.tank.beam.IsActive)
		{
			Rigidbody rbody = base.block.tank.rbody;
			Vector3 zero = Vector3.zero;
			if (TestUseFilter(m_UseActive))
			{
				value = true;
				Vector3 vector = Quaternion.AngleAxis(rbody.angularVelocity.magnitude * 57.29578f * (m_ActiveStability / m_ActiveSpeed), rbody.angularVelocity) * base.block.tank.rootBlockTrans.up;
				Vector3 vector2 = Vector3.up;
				if (IsTrimControlEnabled)
				{
					Vector3 vector3 = ((m_ActiveAxisScale.sqrMagnitude > 1f) ? base.block.tank.rootBlockTrans.right : base.transform.TransformVector(m_ActiveAxisScale));
					if (Mathf.Abs(Vector3.Dot(vector2, vector3)) < 0.95f)
					{
						m_ControlTrim = Mathf.MoveTowards(m_ControlTrim, m_ControlTrimTarget, Time.deltaTime * m_TrimAdjustSpeed);
						float num = Mathf.Clamp(m_Trim + m_ControlTrim, -1f, 1f);
						vector2 = Quaternion.AngleAxis(m_TrimPitchMax * num, vector3) * vector2;
					}
				}
				Vector3 vector4 = Vector3.Cross(vector, vector2);
				Vector3 vector5 = base.transform.InverseTransformVector(vector4);
				vector5.x *= m_ActiveAxisScale.x;
				vector5.y *= m_ActiveAxisScale.y;
				vector5.z *= m_ActiveAxisScale.z;
				vector4 = base.transform.TransformVector(vector5);
				zero += vector4 * m_ActiveSpeed * m_ActiveSpeed;
				Debug.DrawLine(rbody.worldCenterOfMass, rbody.worldCenterOfMass + rbody.transform.up * 10f, Color.green);
				Debug.DrawLine(rbody.worldCenterOfMass, rbody.worldCenterOfMass + vector * 10f, Color.cyan);
				Debug.DrawLine(rbody.worldCenterOfMass, rbody.worldCenterOfMass + vector4 * 10f, Color.red);
				Debug.DrawLine(base.block.centreOfMassWorld, base.block.centreOfMassWorld + vector4 * 10f, Color.red);
			}
			if (TestUseFilter(m_UsePassive))
			{
				value = true;
				Vector3 vector6 = rbody.position + (base.block.transform.position - rbody.transform.position);
				Vector3 vector7 = rbody.GetPointVelocity(vector6) - rbody.velocity;
				Vector3 vector8 = base.transform.InverseTransformVector(vector7);
				vector8.x *= m_PassiveStrength.x;
				vector8.y *= m_PassiveStrength.y;
				vector8.z *= m_PassiveStrength.z;
				vector7 = base.transform.TransformVector(vector8);
				zero += Vector3.Cross(vector7, vector6 - rbody.worldCenterOfMass);
				if (Vector3.Dot(rbody.angularVelocity, m_PrevAngularVel) < -1f)
				{
					zero *= 0.5f;
				}
			}
			m_PrevAngularVel = rbody.angularVelocity;
			rbody.AddTorque(zero);
			if ((bool)m_RotationAnimTransform)
			{
				float num2 = Mathf.Min(zero.magnitude, m_RotationMaxSpeed);
				if (num2 > m_RotationMinSpeed)
				{
					m_RotationAnimTransform.localRotation *= Quaternion.AngleAxis(num2 * m_RotationAnimSpeed * Time.fixedDeltaTime, Vector3.forward);
				}
			}
		}
		if (m_ModuleAnimator != null)
		{
			m_ModuleAnimator.Set(m_AnimatorEnableSpin, value);
		}
	}
}
