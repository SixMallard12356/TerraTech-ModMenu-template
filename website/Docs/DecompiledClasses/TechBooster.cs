using System;
using System.Collections.Generic;
using FMOD.Studio;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TechBooster : TechComponent
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float fuelAmount;
	}

	public enum BoosterType
	{
		GSOMegaBooster,
		GSOBoosterJet,
		VENBooster,
		HERocketJet,
		BFBooster
	}

	[SerializeField]
	[EnumArray(typeof(BoosterType))]
	private FMODEvent[] m_BoosterAudioEvents = new FMODEvent[1];

	public EventNoParams FuelEmptyEvent;

	public EventNoParams FuelFullEvent;

	public EventNoParams FiringBoostersEvent;

	private FMODEventInstance[] m_EventInstances;

	private List<ModuleBooster> m_BoosterModules;

	private HashSet<ModuleFuelTank> m_FuelTankModules = new HashSet<ModuleFuelTank>();

	private float m_FuelCapacity;

	private float m_FuelRefill;

	private float m_FuelAmountCurrent;

	private int[] m_NumRocketBoostersOfType;

	private bool[] m_BoosterFiring;

	private float m_FuelBurnRate;

	private const string kNumBoostersParamName = "NumBoosters";

	private const string kFuelParamName = "Fuel";

	public float FuelLevel => m_FuelAmountCurrent / m_FuelCapacity;

	public bool FuelBurnedOut { get; private set; }

	public bool BoostersFiring { get; private set; }

	public void AddBooster(ModuleBooster booster)
	{
		m_BoosterModules.Add(booster);
		m_FuelBurnRate += booster.FuelBurnPerSecond();
		if (booster.IsRocketBooster)
		{
			int rocketBoosterType = (int)booster.RocketBoosterType;
			m_NumRocketBoostersOfType[rocketBoosterType]++;
		}
	}

	public void RemoveBooster(ModuleBooster booster)
	{
		m_BoosterModules.Remove(booster);
		m_FuelBurnRate -= booster.FuelBurnPerSecond();
		if (booster.IsRocketBooster)
		{
			int rocketBoosterType = (int)booster.RocketBoosterType;
			m_NumRocketBoostersOfType[rocketBoosterType] = Mathf.Max(0, m_NumRocketBoostersOfType[rocketBoosterType] - 1);
			if (m_NumRocketBoostersOfType[rocketBoosterType] == 0)
			{
				m_EventInstances[rocketBoosterType].StopAndRelease();
				m_BoosterFiring[rocketBoosterType] = false;
			}
		}
	}

	public void AddFuelTank(ModuleFuelTank fuelTank, float fuelAmountHeld)
	{
		m_FuelTankModules.Add(fuelTank);
		m_FuelAmountCurrent += fuelAmountHeld;
		m_FuelCapacity += fuelTank.Capacity;
		m_FuelRefill += fuelTank.RefillRate;
	}

	public void RemoveFuelTank(ModuleFuelTank fuelTank, float fuelAmountTaken)
	{
		m_FuelTankModules.Remove(fuelTank);
		m_FuelAmountCurrent -= fuelAmountTaken;
		m_FuelCapacity -= fuelTank.Capacity;
		m_FuelRefill -= fuelTank.RefillRate;
	}

	public void Burn(float fuelAmount)
	{
		BoostersFiring = true;
		if (m_FuelAmountCurrent != 0f)
		{
			m_FuelAmountCurrent = Mathf.Max(m_FuelAmountCurrent - fuelAmount, 0f);
		}
		if (m_FuelAmountCurrent == 0f && !FuelBurnedOut)
		{
			FuelBurnedOut = true;
			FuelEmptyEvent.Send();
		}
	}

	public void SetAllFuelAmount(float fullnessProportion)
	{
		m_FuelAmountCurrent = m_FuelCapacity * fullnessProportion;
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.fuelAmount = m_FuelAmountCurrent;
			serialData.Store(saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(saveState);
		if (serialData2 != null)
		{
			m_FuelAmountCurrent = serialData2.fuelAmount;
		}
	}

	private void OnPool()
	{
		int count = EnumIterator<BoosterType>.Count;
		m_EventInstances = new FMODEventInstance[count];
		m_BoosterModules = new List<ModuleBooster>();
		m_NumRocketBoostersOfType = new int[count];
		m_BoosterFiring = new bool[count];
		for (int i = 0; i < count; i++)
		{
			m_NumRocketBoostersOfType[i] = 0;
			m_BoosterFiring[i] = false;
		}
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
		base.Tech.FixedUpdateEvent.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		m_FuelCapacity = 0f;
		m_FuelRefill = 0f;
		m_FuelAmountCurrent = 0f;
		m_FuelBurnRate = 0f;
		m_FuelTankModules.Clear();
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
		FuelBurnedOut = true;
	}

	private void OnRecycle()
	{
		for (int i = 0; i < EnumIterator<BoosterType>.Count; i++)
		{
			m_NumRocketBoostersOfType[i] = 0;
			m_BoosterFiring[i] = false;
			m_EventInstances[i].StopAndRelease();
		}
		m_BoosterModules.Clear();
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
	}

	private void OnDepool()
	{
		FuelEmptyEvent.EnsureNoSubscribers();
		FuelFullEvent.EnsureNoSubscribers();
		FiringBoostersEvent.EnsureNoSubscribers();
	}

	private void OnUpdate()
	{
		bool flag = false;
		foreach (ModuleBooster boosterModule in m_BoosterModules)
		{
			if (boosterModule.OnTechUpdate())
			{
				flag = true;
			}
		}
		int count = m_BoosterModules.Count;
		EnumValuesIterator<BoosterType> enumerator2 = EnumIterator<BoosterType>.Values().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			BoosterType current = enumerator2.Current;
			int num = (int)current;
			if (!m_BoosterAudioEvents[num].IsValid() || m_NumRocketBoostersOfType[num] <= 0)
			{
				continue;
			}
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < count; i++)
			{
				ModuleBooster moduleBooster = m_BoosterModules[i];
				if (moduleBooster.IsRocketBooster && moduleBooster.RocketBoosterType == current)
				{
					num2++;
					if (moduleBooster.IsFiring)
					{
						num3++;
					}
				}
			}
			if (num3 > 0)
			{
				if (!m_BoosterFiring[num])
				{
					m_EventInstances[num] = m_BoosterAudioEvents[num].PlayEventTrackedObject(base.Tech.trans, base.Tech.rbody);
				}
				m_BoosterFiring[num] = true;
			}
			else if (m_EventInstances[num].IsInited && m_BoosterFiring[num])
			{
				m_EventInstances[num].triggerCue();
				m_BoosterFiring[num] = false;
			}
			if (m_EventInstances[num].IsInited)
			{
				if (m_EventInstances[num].CheckPlaybackState(PLAYBACK_STATE.STOPPED))
				{
					m_EventInstances[num].StopAndRelease();
				}
				else
				{
					m_EventInstances[num].setParameterValue("NumBoosters", num3);
					m_EventInstances[num].setParameterValue("Fuel", FuelLevel);
				}
			}
			if (BoostersFiring)
			{
				_ = m_FuelAmountCurrent;
				_ = m_FuelBurnRate * Globals.inst.m_BoosterMinBurnTime * 0.5f;
			}
		}
		if (flag)
		{
			FiringBoostersEvent.Send();
		}
	}

	private void OnFixedUpdate()
	{
		m_FuelAmountCurrent = Mathf.Min(m_FuelAmountCurrent, m_FuelCapacity);
		if (!BoostersFiring && m_FuelAmountCurrent != m_FuelCapacity)
		{
			m_FuelAmountCurrent = Mathf.MoveTowards(m_FuelAmountCurrent, m_FuelCapacity, m_FuelRefill * Time.deltaTime);
			if (m_FuelAmountCurrent == m_FuelCapacity)
			{
				FuelFullEvent.Send();
			}
		}
		if (FuelBurnedOut && m_FuelCapacity > 0f)
		{
			FuelBurnedOut = m_FuelAmountCurrent != m_FuelCapacity && m_FuelAmountCurrent < m_FuelBurnRate * Globals.inst.m_BoosterMinBurnTime;
		}
		BoostersFiring = false;
	}
}
