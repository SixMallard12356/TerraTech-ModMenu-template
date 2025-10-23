#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechEnergy : TechComponent
{
	[Serializable]
	public struct MeterRenderingProfile
	{
		public int GradationWeightZero;

		public int GradationWeightBlink;

		public int GradationWeightSolid;

		public float BlinkSpeed;

		public static readonly MeterRenderingProfile standard = new MeterRenderingProfile
		{
			GradationWeightZero = 1,
			GradationWeightBlink = 3,
			GradationWeightSolid = 3,
			BlinkSpeed = 10f
		};
	}

	public enum EnergyType
	{
		Electric,
		Hydrocarbon
	}

	public class EnergyState
	{
		public EnergyType type;

		public float currentAmount;

		public float spareCapacity;

		public float storageTotal;

		public float toStoreThisFrame;

		public float previousAmount;

		public float previousSpareCapacity;
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float initStoresFullnessProportion;
	}

	private List<ModuleEnergy>[] m_EnergyModules;

	private List<ModuleEnergyStore> m_EnergyStores = new List<ModuleEnergyStore>(10);

	private EnergyState[] m_EnergyStates;

	private bool m_ConsumingSuppliedEnergy;

	public void Register(ModuleEnergy module)
	{
		List<ModuleEnergy> list = m_EnergyModules[(int)module.OutputEnergyType];
		int count = list.Count;
		int i;
		for (i = 0; i < count; i++)
		{
			if (module.Priority > list[i].Priority)
			{
				list.Insert(i, module);
				break;
			}
		}
		if (i == count)
		{
			list.Add(module);
		}
	}

	public void Unregister(ModuleEnergy module)
	{
		m_EnergyModules[(int)module.OutputEnergyType].Remove(module);
	}

	public void Register(ModuleEnergyStore module)
	{
		m_EnergyStores.Add(module);
	}

	public void Unregister(ModuleEnergyStore module)
	{
		m_EnergyStores.Remove(module);
	}

	public void Supply(EnergyType type, ModuleEnergy module, float amount)
	{
		if (ManNetwork.IsHost)
		{
			Energy(type).currentAmount += amount;
		}
	}

	public bool ConsumeIfEnough(EnergyType type, float amount)
	{
		if (Energy(type).currentAmount >= amount)
		{
			Energy(type).currentAmount -= amount;
			return true;
		}
		return false;
	}

	public float ConsumeUpToMax(EnergyType type, float amount)
	{
		float num = Mathf.Min(amount, Energy(type).currentAmount);
		if (num != 0f)
		{
			Energy(type).currentAmount -= num;
			return num;
		}
		return 0f;
	}

	public void IncrementStorageAllocation(EnergyType type, float amount)
	{
		d.Assert(!m_ConsumingSuppliedEnergy);
		Energy(type).storageTotal += amount;
	}

	public float AllocateForStorage(EnergyType type, float amount)
	{
		if (Energy(type).toStoreThisFrame == -1f)
		{
			Energy(type).toStoreThisFrame = Energy(type).currentAmount;
		}
		d.Assert(m_ConsumingSuppliedEnergy);
		return amount / Energy(type).storageTotal * Energy(type).toStoreThisFrame;
	}

	public float GetCurrentAmount(EnergyType type)
	{
		d.Assert(m_ConsumingSuppliedEnergy);
		return Energy(type).currentAmount;
	}

	public float GetSpareStorageCapacity(EnergyType type)
	{
		d.Assert(!m_ConsumingSuppliedEnergy);
		return Energy(type).spareCapacity;
	}

	public void SetAllStoresAmount(float fullnessProportion)
	{
		EnergyState[] energyStates = m_EnergyStates;
		foreach (EnergyState energyState in energyStates)
		{
			foreach (ModuleEnergy item in m_EnergyModules[(int)energyState.type])
			{
				if (item.Store.IsNotNull())
				{
					item.Store.DrainEnergy();
					item.Store.AddEnergy(item.Store.m_Capacity * fullnessProportion);
				}
			}
		}
	}

	public EnergyState Energy(EnergyType type)
	{
		return m_EnergyStates[(int)type];
	}

	private void UpdateEnergy()
	{
		EnergyState[] energyStates = m_EnergyStates;
		foreach (EnergyState energyState in energyStates)
		{
			EnergyType type = energyState.type;
			energyState.currentAmount = 0f;
			energyState.storageTotal = 0f;
			energyState.toStoreThisFrame = -1f;
			foreach (ModuleEnergy item in m_EnergyModules[(int)type])
			{
				item.UpdateSupplyEvent.Send();
			}
			m_ConsumingSuppliedEnergy = true;
			energyState.previousAmount = energyState.currentAmount;
			energyState.spareCapacity = 0f;
			foreach (ModuleEnergy item2 in m_EnergyModules[(int)type])
			{
				item2.UpdateConsumeEvent.Send();
				energyState.spareCapacity += item2.GetSpareStorageCapacity(type);
				item2.UpdateSfx();
			}
			m_ConsumingSuppliedEnergy = false;
			energyState.previousSpareCapacity = energyState.spareCapacity;
		}
	}

	private void UpdateEnergyStores()
	{
		foreach (ModuleEnergyStore energyStore in m_EnergyStores)
		{
			energyStore.UpdateGauges();
		}
	}

	public static void AddSerialData(Dictionary<int, TechComponent.SerialData> saveState, float fullness)
	{
		SerialData serialData = new SerialData();
		serialData.initStoresFullnessProportion = fullness;
		serialData.Store(saveState);
	}

	public static void CalculateGauge(float powerLevel, int numLeds, out int numLit, out float blinkSpeed)
	{
		CalculateGauge(powerLevel, numLeds, MeterRenderingProfile.standard, out numLit, out blinkSpeed);
	}

	public static void CalculateGauge(float powerLevel, int numLeds, MeterRenderingProfile renderProfile, out int numLit, out float blinkSpeed)
	{
		int num = renderProfile.GradationWeightSolid + renderProfile.GradationWeightBlink;
		int num2 = numLeds * num + renderProfile.GradationWeightZero;
		int num3 = Mathf.Min(Mathf.FloorToInt(powerLevel * (float)num2), num2 - 1);
		numLit = ((num3 >= renderProfile.GradationWeightZero) ? (1 + (num3 - renderProfile.GradationWeightZero) / num) : 0);
		bool flag = (num3 - renderProfile.GradationWeightZero) % num < renderProfile.GradationWeightBlink;
		blinkSpeed = (flag ? renderProfile.BlinkSpeed : 0f);
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (!saving)
		{
			SerialData serialData = SerialData<SerialData>.Retrieve(saveState);
			if (serialData != null)
			{
				SetAllStoresAmount(serialData.initStoresFullnessProportion);
			}
		}
	}

	private void OnPool()
	{
		int num = EnumValuesIterator<EnergyType>.Values.Length;
		m_EnergyStates = new EnergyState[num];
		m_EnergyModules = new List<ModuleEnergy>[2];
		for (int i = 0; i < num; i++)
		{
			m_EnergyStates[i] = new EnergyState
			{
				type = (EnergyType)i
			};
			m_EnergyModules[i] = new List<ModuleEnergy>(10);
		}
	}

	private void OnSpawn()
	{
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
		m_ConsumingSuppliedEnergy = false;
		EnergyState[] energyStates = m_EnergyStates;
		for (int i = 0; i < energyStates.Length; i++)
		{
			energyStates[i].currentAmount = 0f;
		}
	}

	private void OnRecycle()
	{
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
	}

	private void Update()
	{
		if (!Singleton.Manager<ManPauseGame>.inst.IsPaused)
		{
			UpdateEnergy();
			UpdateEnergyStores();
		}
	}
}
