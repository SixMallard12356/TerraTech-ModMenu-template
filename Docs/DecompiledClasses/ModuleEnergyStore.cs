#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleEnergy))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleEnergyStore : Module, ManVisible.StateVisualiser.Provider, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float energy;

		public WarningHolder warning;
	}

	public TechEnergy.EnergyType m_EnergyType;

	public float m_Capacity = 100f;

	[SerializeField]
	private bool m_ProvideUnlimitedCharge;

	public bool m_AcceptRemoteCharge;

	[SerializeField]
	private float m_ShowWarningWhenLeft = 0.1f;

	[SerializeField]
	private bool m_ShowWarning = true;

	public static bool s_DebugShowPowerLevels;

	private ModuleEnergy m_ModuleEnergy;

	private EnergyGauge[] m_EnergyGauges;

	private WarningHolder m_Warning;

	private OnGUICallback m_DrawPowerCallback;

	public float CurrentAmount { get; private set; }

	public float SpareCapacity => m_Capacity - CurrentAmount;

	public static bool CheatFillMaxPower { get; set; }

	public bool AddEnergy(float amount, bool fillIfOverflow = true)
	{
		if (amount > SpareCapacity && !fillIfOverflow)
		{
			return false;
		}
		OnServerSetCurrentAmount(CurrentAmount + Mathf.Min(amount, SpareCapacity));
		return true;
	}

	public void DrainEnergy()
	{
		OnServerSetCurrentAmount(0f);
	}

	public void UpdateGauges()
	{
		float num = CurrentAmount / m_Capacity;
		Tank tank = base.block.tank;
		EnergyGauge[] energyGauges = m_EnergyGauges;
		for (int i = 0; i < energyGauges.Length; i++)
		{
			energyGauges[i].UpdateGaugeLevel(tank, num);
		}
		if (m_ShowWarning)
		{
			if (num < m_ShowWarningWhenLeft)
			{
				m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleBatteryLow, LocalisationEnums.Warnings.warningMsgBattteryLow, 8);
			}
			else
			{
				m_Warning.Remove();
			}
		}
	}

	private void OnUpdateSupplyEnergy()
	{
		if (ManNetwork.IsHost)
		{
			d.Assert(base.block.tank.IsNotNull(), "ModuleEnergyStore.OnUpdateSupplyEnergy - block has no tech");
			base.block.tank.EnergyRegulator.IncrementStorageAllocation(m_EnergyType, m_Capacity);
			if (CurrentAmount != 0f)
			{
				m_ModuleEnergy.Supply(m_EnergyType, CurrentAmount);
				OnServerSetCurrentAmount(0f);
			}
		}
	}

	private void OnUpdateConsumeEnergy()
	{
		if (ManNetwork.IsHost)
		{
			d.Assert(base.block.tank.IsNotNull(), "ModuleEnergyStore.OnUpdateConsumeEnergy - block has no tech");
			float b = base.block.tank.EnergyRegulator.AllocateForStorage(m_EnergyType, m_Capacity);
			OnServerSetCurrentAmount(m_ModuleEnergy.ConsumeUpToMax(m_EnergyType, Mathf.Min(m_Capacity, b)));
			if (m_ProvideUnlimitedCharge)
			{
				OnServerSetCurrentAmount(m_Capacity);
			}
			else if (CheatFillMaxPower)
			{
				OnServerSetCurrentAmount(m_Capacity);
			}
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.energy = CurrentAmount;
			serialData.warning = m_Warning;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			CurrentAmount = serialData2.energy;
			if (serialData2.warning != null)
			{
				m_Warning.Restore(serialData2.warning);
			}
		}
	}

	private void OnAttached()
	{
		if (Singleton.Manager<ManSpawn>.inst.IsTechSpawning && Tank.IsEnemy(0, base.block.tank.Team))
		{
			CurrentAmount = m_Capacity;
		}
		if (m_EnergyGauges.Length != 0)
		{
			base.block.tank.EnergyRegulator.Register(this);
		}
		if (s_DebugShowPowerLevels)
		{
			m_DrawPowerCallback = OnGUICallback.AddGUICallback(base.gameObject);
			m_DrawPowerCallback.OnGUIEvent.Subscribe(OnGuiCallback);
		}
	}

	private void OnDetaching()
	{
		if (m_DrawPowerCallback != null)
		{
			m_DrawPowerCallback.OnGUIEvent.Unsubscribe(OnGuiCallback);
			OnGUICallback.RemoveGUICallback(m_DrawPowerCallback);
			m_DrawPowerCallback = null;
		}
		m_Warning.Reset();
		if (m_EnergyGauges.Length != 0)
		{
			EnergyGauge[] energyGauges = m_EnergyGauges;
			for (int i = 0; i < energyGauges.Length; i++)
			{
				energyGauges[i].OnDetach(base.block.tank);
			}
			base.block.tank.EnergyRegulator.Unregister(this);
		}
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (flags.Contains(0))
		{
			DrawPowerLevel(screenPos);
		}
	}

	private void OnGuiCallback()
	{
		Vector2 screenPos = Singleton.camera.WorldToScreenPoint(base.transform.position);
		DrawPowerLevel(screenPos);
	}

	private void DrawPowerLevel(Vector2 screenPos)
	{
		float num = CurrentAmount / m_Capacity;
		Color textColor = ((num > 0.5f) ? Color.green : ((num > 0.25f) ? Color.yellow : Color.red));
		DebugGui.LabelScreen(((int)(num * 100f)).ToString("00") + "%", textColor, screenPos);
	}

	private void OnServerSetCurrentAmount(float amount)
	{
		CurrentAmount = amount;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			base.block.tank.netTech.SetModuleDirty(this);
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleEnergyStore;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(CurrentAmount);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		CurrentAmount = reader.ReadSingle();
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateSupplyEvent.Subscribe(OnUpdateSupplyEnergy);
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		m_EnergyGauges = GetComponentsInChildren<EnergyGauge>(includeInactive: true);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.LowPower);
		m_ShowWarning = false;
	}

	private void OnSpawn()
	{
		CurrentAmount = 0f;
	}
}
