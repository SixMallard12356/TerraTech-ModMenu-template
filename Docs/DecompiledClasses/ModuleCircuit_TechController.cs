#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleTechController), typeof(ModuleCircuitReceiver))]
public class ModuleCircuit_TechController : Module, ICustomTechController
{
	public enum ControlInput
	{
		MoveRight,
		MoveLeft,
		MoveUp,
		MoveDown,
		MoveForward,
		MoveBack,
		PitchUp,
		PitchDown,
		YawLeft,
		YawRight,
		RollRight,
		RollLeft,
		ThrottleRight,
		ThrottleLeft,
		ThrottleUp,
		ThrottleDown,
		ThrottleForward,
		ThrottleBack,
		BoostProps,
		BoostJets
	}

	private enum ControlGroup
	{
		Movement = 0,
		Rotation = 6,
		Throttle = 12,
		Boost = 18
	}

	private struct ThrottleEffector
	{
		public Transform owner;

		public Vector3 throttleDir;
	}

	[SerializeField]
	[EnumArray(typeof(ControlInput))]
	private Vector3[] m_ControlInAPs = new Vector3[EnumIterator<ControlInput>.Count];

	[SerializeField]
	[Tooltip("By default, axis effects are unaffected by the orientation of the block. Enable this to change which axis is controlled based on the relative orientation of the block.")]
	private bool m_UseRelativeOrientation;

	[HideInInspector]
	[SerializeField]
	private bool m_HasThrottleControl;

	private Dictionary<Vector3, ControlInput> m_APToControlInput;

	private Dictionary<Vector3, ThrottleEffector> m_ActiveAPThrottleContributors;

	private static List<Vector3> s_APsInCommon = new List<Vector3>();

	private static readonly Quaternion kRotationAxisOffset = Quaternion.Euler(0f, -90f, 0f);

	private const int kInputsPerGroup = 6;

	int ICustomTechController.DefaultPriority => 3;

	private bool ApplyControlFromCircuitInput()
	{
		Vector3 inputMovement = ConvertControlInputToVector(ControlGroup.Movement);
		Vector3 inputRotation = ConvertControlInputToVector(ControlGroup.Rotation);
		Vector3 inputThrottle = ConvertControlInputToVector(ControlGroup.Throttle);
		bool boostProps = GetInput(ControlInput.BoostProps) > 0f;
		bool boostJets = GetInput(ControlInput.BoostJets) > 0f;
		base.block.tank.control.CollectMovementInput(inputMovement, inputRotation, inputThrottle, boostProps, boostJets);
		return true;
	}

	bool ICustomTechController.ExecuteControl(bool additive)
	{
		return ApplyControlFromCircuitInput();
	}

	private Vector3 ConvertControlInputToVector(ControlGroup controlGroup)
	{
		d.Assert((int)controlGroup % 6 == 0, "ConvertControlInputToVector Expects a very specific ordering of the enum, and starting value passed in. You have not complied..!");
		Vector3 vector = new Vector3
		{
			x = GetInput((ControlInput)controlGroup) - GetInput((ControlInput)(controlGroup + 1)),
			y = GetInput((ControlInput)(controlGroup + 2)) - GetInput((ControlInput)(controlGroup + 3)),
			z = GetInput((ControlInput)(controlGroup + 4)) - GetInput((ControlInput)(controlGroup + 5))
		};
		if (m_UseRelativeOrientation)
		{
			vector = ((controlGroup != ControlGroup.Rotation) ? ((Quaternion)base.block.cachedLocalRotation * vector) : (base.block.cachedLocalRotation * kRotationAxisOffset * vector));
		}
		return vector;
	}

	private float GetInput(ControlInput input)
	{
		Vector3 vector = m_ControlInAPs[(int)input];
		int value = 0;
		if (vector != Vector3.zero)
		{
			base.block.CircuitNode.Receiver.CurrentChargeData.AllChargeAPsAndCharges.TryGetValue(vector, out value);
		}
		return value;
	}

	private static ControlGroup GetGroup(ControlInput input)
	{
		return (ControlGroup)((int)input / 6 * 6);
	}

	private void SetupTrottleInputs(Vector3 apPos, ControlInput controlInput)
	{
		d.AssertFormat(!m_ActiveAPThrottleContributors.ContainsKey(apPos), "Throttle input for AP {0} on block {1} is already set?!", apPos, base.block.name);
		if (GetGroup(controlInput) == ControlGroup.Throttle && base.block.CircuitNode.HasConnectionOnAP(apPos, ModuleCircuitNode.ConnexionTypes.Input))
		{
			Vector3 zero = Vector3.zero;
			ControlInput num = controlInput - 12;
			int index = (int)num / 2;
			bool flag = (int)num % 2 == 1;
			zero[index] = ((!flag) ? 1 : (-1));
			Vector3 vecToAlign = (Quaternion)base.block.cachedLocalRotation * zero;
			vecToAlign = vecToAlign.AlignToAxis();
			base.block.tank.control.AddThrottleControlEnabler(base.block.trans, vecToAlign, respondsToDriveInput: false);
			m_ActiveAPThrottleContributors.Add(apPos, new ThrottleEffector
			{
				owner = base.block.trans,
				throttleDir = vecToAlign
			});
		}
	}

	private void OnAttached()
	{
		for (int i = 0; i < m_ControlInAPs.Length; i++)
		{
			SetupTrottleInputs(m_ControlInAPs[i], (ControlInput)i);
		}
	}

	private void OnDetaching()
	{
		foreach (KeyValuePair<Vector3, ThrottleEffector> activeAPThrottleContributor in m_ActiveAPThrottleContributors)
		{
			base.block.tank.control.RemoveThrottleControlEnabler(activeAPThrottleContributor.Value.owner, activeAPThrottleContributor.Value.throttleDir, respondsToDriveInput: false);
		}
		m_ActiveAPThrottleContributors.Clear();
	}

	private void OnNeighbourAttached(TankBlock neighbourBlock)
	{
		base.block.GetLocalAPsForAttachedBlock(neighbourBlock, s_APsInCommon);
		foreach (Vector3 item in s_APsInCommon)
		{
			if (m_APToControlInput.TryGetValue(item, out var value))
			{
				SetupTrottleInputs(item, value);
			}
		}
	}

	private void OnNeighbourDetaching(TankBlock neighbourBlock)
	{
		base.block.GetLocalAPsForAttachedBlock(neighbourBlock, s_APsInCommon);
		foreach (Vector3 item in s_APsInCommon)
		{
			if (m_ActiveAPThrottleContributors.TryGetValue(item, out var value))
			{
				base.block.tank.control.RemoveThrottleControlEnabler(value.owner, value.throttleDir, respondsToDriveInput: false);
				m_ActiveAPThrottleContributors.Remove(item);
			}
		}
	}

	private void PrePool()
	{
		int num = 12;
		for (int i = num; i < num + 6; i++)
		{
			if (m_ControlInAPs[i] != Vector3.zero)
			{
				m_HasThrottleControl = true;
				break;
			}
		}
	}

	private void OnPool()
	{
		base.block.CircuitNode.Receiver.SetRequireExtensiveChargeData();
		if (!m_HasThrottleControl)
		{
			return;
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.NeighbourAttachedEvent.Subscribe(OnNeighbourAttached);
		base.block.NeighbourDetachingEvent.Subscribe(OnNeighbourDetaching);
		m_APToControlInput = new Dictionary<Vector3, ControlInput>();
		m_ActiveAPThrottleContributors = new Dictionary<Vector3, ThrottleEffector>();
		for (int i = 0; i < m_ControlInAPs.Length; i++)
		{
			Vector3 vector = m_ControlInAPs[i];
			if (vector != Vector3.zero)
			{
				ControlInput value = (ControlInput)i;
				m_APToControlInput.Add(vector, value);
			}
		}
	}
}
