#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_ALU : Module, ICircuitDispensor, INetworkedModule
{
	public enum ProcessType
	{
		Arithmetic_Add,
		Arithmetic_Sub,
		Logic_AND,
		Logic_OR,
		Logic_NOT,
		Compare_Equals,
		Compare_GreaterThan,
		Compare_LessThan,
		Arithmetic_Multiply,
		Arithmetic_Divide,
		Logic_XOR,
		Logic_Toggle,
		Logic_Latch,
		Logic_Multiplexer,
		Arithmetic_Modulo
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public int m_LastCalculationResult;
	}

	[SerializeField]
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	private ProcessType m_ProcessType;

	[Tooltip("The primary AP should be in position 0")]
	[SerializeField]
	private Vector3[] m_ChargeInAPs = new Vector3[0];

	[SerializeField]
	private Visualiser_Anim m_Visualiser;

	public const int kMinChargeValue = 0;

	public const int kMaxChargeValue = int.MaxValue;

	protected static Dictionary<ProcessType, Func<ModuleCircuit_ALU, int>> m_Processes = new Dictionary<ProcessType, Func<ModuleCircuit_ALU, int>>
	{
		{
			ProcessType.Logic_AND,
			Process_AND
		},
		{
			ProcessType.Logic_OR,
			Process_OR
		},
		{
			ProcessType.Logic_NOT,
			Process_NOT
		},
		{
			ProcessType.Logic_XOR,
			Process_XOR
		},
		{
			ProcessType.Arithmetic_Add,
			Process_ADDITION
		},
		{
			ProcessType.Arithmetic_Sub,
			Process_SUBTRACTION
		},
		{
			ProcessType.Arithmetic_Multiply,
			Process_MULTIPLY
		},
		{
			ProcessType.Arithmetic_Divide,
			Process_DIVIDE
		},
		{
			ProcessType.Arithmetic_Modulo,
			Process_MODULO
		},
		{
			ProcessType.Compare_Equals,
			Process_Equals
		},
		{
			ProcessType.Compare_GreaterThan,
			Process_GreaterThan
		},
		{
			ProcessType.Compare_LessThan,
			Process_LessThan
		},
		{
			ProcessType.Logic_Toggle,
			Process_Toggle
		},
		{
			ProcessType.Logic_Latch,
			Process_Latch
		},
		{
			ProcessType.Logic_Multiplexer,
			Process_Multiplexer
		}
	};

	private int m_LastCalculationResult;

	private NetworkedProperty<BoolParamBlockMessage> net_IsVisualiserActive;

	public int LastDispensedCharge => base.block.CircuitNode.Dispensor.LastDispensedChargeData.Value.ChargeStrength;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		if (!base.block.CircuitNode.Receiver.InitialisedOnNetwork)
		{
			return 0;
		}
		return m_LastCalculationResult;
	}

	private static int Process_ADDITION(ModuleCircuit_ALU module)
	{
		int num = 0;
		checked
		{
			try
			{
				for (int i = 0; i < module.m_ChargeInAPs.Length; i++)
				{
					Vector3 localAP = module.m_ChargeInAPs[i];
					if (module.TryGetInputChargeValue(localAP, out var chargeValue))
					{
						num += chargeValue;
					}
				}
			}
			catch (OverflowException)
			{
				num = int.MaxValue;
			}
			return num;
		}
	}

	private static int Process_SUBTRACTION(ModuleCircuit_ALU module)
	{
		Vector3 vector = module.m_ChargeInAPs[0];
		if (!module.TryGetInputChargeValue(vector, out var chargeValue) || chargeValue == 0)
		{
			return 0;
		}
		int num = chargeValue;
		checked
		{
			try
			{
				for (int i = 0; i < module.m_ChargeInAPs.Length; i++)
				{
					Vector3 vector2 = module.m_ChargeInAPs[i];
					if (!(vector2 == vector) && module.TryGetInputChargeValue(vector2, out var chargeValue2))
					{
						num -= chargeValue2;
					}
				}
			}
			catch (OverflowException)
			{
				num = 0;
			}
			return Mathf.Max(0, num);
		}
	}

	private static int Process_MULTIPLY(ModuleCircuit_ALU module)
	{
		if (module.block.CircuitNode.ConnexionMetadata.ConnectedNodeCount < 2)
		{
			return 0;
		}
		Vector3 vector = module.m_ChargeInAPs[0];
		if (!module.TryGetInputChargeValue(vector, out var chargeValue) || chargeValue == 0)
		{
			return 0;
		}
		int num = chargeValue;
		checked
		{
			try
			{
				for (int i = 0; i < module.m_ChargeInAPs.Length; i++)
				{
					Vector3 vector2 = module.m_ChargeInAPs[i];
					if (!(vector2 == vector) && module.TryGetInputChargeValue(vector2, out var chargeValue2))
					{
						num *= chargeValue2;
					}
				}
			}
			catch (OverflowException)
			{
				num = int.MaxValue;
			}
			return num;
		}
	}

	private static int Process_DIVIDE(ModuleCircuit_ALU module)
	{
		if (module.block.CircuitNode.ConnexionMetadata.ConnectedNodeCount < 2)
		{
			return 0;
		}
		Vector3 vector = module.m_ChargeInAPs[0];
		if (!module.TryGetInputChargeValue(vector, out var chargeValue) || chargeValue == 0)
		{
			return 0;
		}
		int num = chargeValue;
		try
		{
			for (int i = 0; i < module.m_ChargeInAPs.Length; i = checked(i + 1))
			{
				Vector3 vector2 = module.m_ChargeInAPs[i];
				if (!(vector2 == vector) && module.TryGetInputChargeValue(vector2, out var chargeValue2))
				{
					if (chargeValue2 == 0)
					{
						throw new DivideByZeroException();
					}
					num /= chargeValue2;
				}
			}
		}
		catch (DivideByZeroException)
		{
			num = 0;
		}
		catch (OverflowException)
		{
			num = 0;
		}
		return Mathf.Max(0, num);
	}

	private static int Process_MODULO(ModuleCircuit_ALU module)
	{
		if (module.block.CircuitNode.ConnexionMetadata.ConnectedNodeCount < 2)
		{
			return 0;
		}
		Vector3 vector = module.m_ChargeInAPs[0];
		if (!module.TryGetInputChargeValue(vector, out var chargeValue) || chargeValue == 0)
		{
			return 0;
		}
		int num = chargeValue;
		try
		{
			for (int i = 0; i < module.m_ChargeInAPs.Length; i = checked(i + 1))
			{
				Vector3 vector2 = module.m_ChargeInAPs[i];
				if (!(vector2 == vector) && module.TryGetInputChargeValue(vector2, out var chargeValue2))
				{
					if (chargeValue2 == 0)
					{
						throw new DivideByZeroException();
					}
					num %= chargeValue2;
				}
			}
		}
		catch (DivideByZeroException)
		{
			num = 0;
		}
		catch (OverflowException)
		{
			num = 0;
		}
		return Mathf.Max(0, num);
	}

	private static int Process_AND(ModuleCircuit_ALU module)
	{
		int connectedInputs;
		int activeInputs;
		int numActiveInputs = GetNumActiveInputs(module, out connectedInputs, out activeInputs);
		if (connectedInputs < 2 || connectedInputs != activeInputs)
		{
			return 0;
		}
		return numActiveInputs;
	}

	private static int Process_OR(ModuleCircuit_ALU module)
	{
		int connectedInputs;
		int activeInputs;
		int numActiveInputs = GetNumActiveInputs(module, out connectedInputs, out activeInputs);
		if (activeInputs <= 0)
		{
			return 0;
		}
		return numActiveInputs;
	}

	private static int Process_NOT(ModuleCircuit_ALU module)
	{
		if (!module.TryGetInputChargeValue(module.m_ChargeInAPs[0], out var chargeValue) || chargeValue == 0)
		{
			return module.block.CircuitNode.Dispensor.DefaultChargeStrength;
		}
		return 0;
	}

	private static int Process_XOR(ModuleCircuit_ALU module)
	{
		int connectedInputs;
		int activeInputs;
		int numActiveInputs = GetNumActiveInputs(module, out connectedInputs, out activeInputs);
		if (activeInputs % 2 != 1)
		{
			return 0;
		}
		return numActiveInputs;
	}

	private static int Process_Equals(ModuleCircuit_ALU module)
	{
		if (!ValueCompareConnectedInputs(module, out var comparisonResult, requiresPrimaryInput: false) || comparisonResult != 0)
		{
			return 0;
		}
		return module.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private static int Process_GreaterThan(ModuleCircuit_ALU module)
	{
		if (!ValueCompareConnectedInputs(module, out var comparisonResult, requiresPrimaryInput: true) || comparisonResult != 1)
		{
			return 0;
		}
		return module.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private static int Process_LessThan(ModuleCircuit_ALU module)
	{
		if (!ValueCompareConnectedInputs(module, out var comparisonResult, requiresPrimaryInput: true) || comparisonResult != -1)
		{
			return 0;
		}
		return module.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private static int Process_Toggle(ModuleCircuit_ALU module)
	{
		int chargeStrength = module.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength;
		int chargeStrength2 = module.block.CircuitNode.Receiver.PreviousChargeData.ChargeStrength;
		bool num = chargeStrength > 0 && chargeStrength2 == 0;
		int num2 = module.block.CircuitNode.Dispensor.LastDispensedCharge.ChargeStrength;
		if (num && module.LastDispensedCharge != 0)
		{
			num2 = 0;
		}
		else if (chargeStrength > 0)
		{
			num2 = chargeStrength;
		}
		module.m_Visualiser?.SetOn(num2 != 0);
		return num2;
	}

	private static int Process_Latch(ModuleCircuit_ALU module)
	{
		int chargeValue;
		bool flag = module.TryGetInputChargeValue(module.m_ChargeInAPs[0], out chargeValue) && chargeValue > 0;
		int chargeValue2;
		bool num = module.TryGetInputChargeValue(module.m_ChargeInAPs[1], out chargeValue2) && chargeValue2 > 0;
		int num2 = module.block.CircuitNode.Dispensor.LastDispensedCharge.ChargeStrength;
		if (num)
		{
			num2 = 0;
		}
		else if (flag)
		{
			num2 = chargeValue;
		}
		module.m_Visualiser?.SetOn(num2 != 0);
		return num2;
	}

	private static int Process_Multiplexer(ModuleCircuit_ALU module)
	{
		module.TryGetInputChargeValue(module.m_ChargeInAPs[0], out var chargeValue);
		int num = Mathf.Clamp(chargeValue + 1, 1, module.m_ChargeInAPs.Length - 1);
		module.TryGetInputChargeValue(module.m_ChargeInAPs[num], out var chargeValue2);
		module.m_Visualiser?.SetOn(chargeValue != 0);
		return chargeValue2;
	}

	private static int GetNumActiveInputs(ModuleCircuit_ALU module, out int connectedInputs, out int activeInputs)
	{
		connectedInputs = 0;
		activeInputs = 0;
		for (int i = 0; i < module.m_ChargeInAPs.Length; i++)
		{
			if (module.TryGetInputChargeValue(module.m_ChargeInAPs[i], out var chargeValue))
			{
				connectedInputs++;
				if (chargeValue > 0)
				{
					activeInputs++;
				}
			}
		}
		return module.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength;
	}

	private static bool ValueCompareConnectedInputs(ModuleCircuit_ALU module, out int comparisonResult, bool requiresPrimaryInput)
	{
		comparisonResult = int.MinValue;
		int num = int.MinValue;
		for (int i = 0; i < module.m_ChargeInAPs.Length; i++)
		{
			Vector3 localAP = module.m_ChargeInAPs[i];
			module.TryGetInputChargeValue(localAP, out var chargeValue);
			int num2 = comparisonResult;
			if (num == int.MinValue)
			{
				num = chargeValue;
			}
			else
			{
				comparisonResult = num.CompareTo(chargeValue);
			}
			if (num2 != int.MinValue && num2 != comparisonResult)
			{
				d.LogErrorFormat("Different comparison results ({0} and {1}) detected on a >2 input comparer. This is not supported!", num2, comparisonResult);
				return false;
			}
		}
		return true;
	}

	private bool TryGetInputChargeValue(Vector3 localAP, out int chargeValue)
	{
		if (base.block.CircuitNode.HasConnectionOnAP(localAP, ModuleCircuitNode.ConnexionTypes.Input))
		{
			if (!base.block.CircuitNode.Receiver.InitialisedOnNetwork || !base.block.CircuitNode.Receiver.CurrentChargeData.AllChargeAPsAndCharges.TryGetValue(localAP, out chargeValue))
			{
				chargeValue = 0;
			}
			return true;
		}
		chargeValue = 0;
		return false;
	}

	private void EvaluateLogic()
	{
		if (ManNetwork.IsHost)
		{
			m_LastCalculationResult = m_Processes[m_ProcessType](this);
		}
	}

	private void OnAttaching()
	{
		m_LastCalculationResult = 0;
	}

	protected void OnChargeChanged(Circuits.BlockChargeData chargeInfo)
	{
		EvaluateLogic();
	}

	private void OnPostBlockSerialization(bool saving)
	{
		if (!saving)
		{
			EvaluateLogic();
		}
	}

	private void OnConnexionsChanged()
	{
		EvaluateLogic();
	}

	private void OnMPSync_Visualiser(BoolParamBlockMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			m_Visualiser?.SetOn(msg.value);
		}
	}

	private void OnUpdateCircuitVisuals()
	{
		if (!(m_Visualiser == null) && net_IsVisualiserActive.Data.value != m_Visualiser.IsOn)
		{
			net_IsVisualiserActive.Data.value = m_Visualiser.IsOn;
			net_IsVisualiserActive.Sync();
		}
	}

	private void OnPool()
	{
		net_IsVisualiserActive = new NetworkedProperty<BoolParamBlockMessage>(this, TTMsgType.SyncCircuitALU_VisualiserActive, OnMPSync_Visualiser);
		base.block.PostSerializeEvent.Subscribe(OnPostBlockSerialization);
		base.block.AttachingEvent.Subscribe(OnAttaching);
		base.block.CircuitNode.ConnexionsUpdatedEvent.Subscribe(OnConnexionsChanged);
	}

	private void OnSpawn()
	{
		Circuits.PostSlowUpdate.Subscribe(OnUpdateCircuitVisuals);
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: true);
		m_LastCalculationResult = 0;
		m_Visualiser?.Reset();
	}

	private void OnRecycle()
	{
		Circuits.PostSlowUpdate.Unsubscribe(OnUpdateCircuitVisuals);
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
	}

	TankBlock INetworkedModule.GetBlock()
	{
		return base.block;
	}

	NetworkedModuleID INetworkedModule.GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_ALU;
	}

	void INetworkedModule.OnSerialize(NetworkWriter writer)
	{
		net_IsVisualiserActive.Serialise(writer);
	}

	void INetworkedModule.OnDeserialize(NetworkReader reader)
	{
		net_IsVisualiserActive.Deserialise(reader);
	}
}
