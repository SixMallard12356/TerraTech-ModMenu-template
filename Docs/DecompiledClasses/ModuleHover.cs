#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleHover : Module
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float hoverPower;
	}

	public enum Size : byte
	{
		Small,
		Medium,
		Large
	}

	[SerializeField]
	private float m_HoverPower = 0.5f;

	[SerializeField]
	private Size m_HoverSize;

	[SerializeField]
	private bool m_AutoEnableSpinnersOnAttach;

	[SerializeField]
	private bool m_UseBoosterAudio;

	[SerializeField]
	private TechAudio.BoosterEngineType m_BoosterAudioType;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	public const float kDefaultPower = 0.5f;

	private List<HoverJet> jets = new List<HoverJet>();

	private bool m_Enabled;

	public float HoverPower
	{
		get
		{
			return m_HoverPower;
		}
		set
		{
			m_HoverPower = value;
		}
	}

	public Size HoverSize => m_HoverSize;

	public bool UseBoosterAudio => m_UseBoosterAudio;

	public TechAudio.BoosterEngineType BoosterAudioType => m_BoosterAudioType;

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

	public float GetAverageNormalizedPushForce()
	{
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < jets.Count; i++)
		{
			float normalizedPushForce = jets[i].NormalizedPushForce;
			num += normalizedPushForce;
			if (normalizedPushForce > 0.01f)
			{
				num2++;
			}
		}
		if (num2 <= 0)
		{
			return 0f;
		}
		return num / (float)num2;
	}

	public void CopyAudioType(ModuleHover source)
	{
		m_UseBoosterAudio = source.m_UseBoosterAudio;
		m_BoosterAudioType = source.m_BoosterAudioType;
	}

	public void SetEnabled(bool enable)
	{
		m_Enabled = enable;
		UpdateHoverEnabledState();
	}

	private void DriveControlInput(TankControl.ControlState controlState)
	{
		foreach (HoverJet jet in jets)
		{
			jet.OnControlInput(controlState, m_HoverPower);
		}
	}

	private void UpdateHoverEnabledState(bool resetHoverPower = false)
	{
		bool flag = m_Enabled && ((!CircuitControlled) ? (base.block.IsAttached && IsCategoryEnabled() && !base.block.tank.beam.IsActive) : (base.block.CircuitReceiver.ShouldProcessInput && base.block.CircuitReceiver.CurrentChargeData > 0));
		foreach (HoverJet jet in jets)
		{
			jet.SetEnabled(flag);
			if (resetHoverPower && flag)
			{
				jet.InitHoverPower(m_HoverPower);
			}
		}
	}

	private void OnAttached()
	{
		base.block.tank.control.driveControlEvent.Subscribe(DriveControlInput);
		base.block.tank.ResetPhysicsEvent.Subscribe(OnResetTechPhysics);
		base.block.tank.BlockStateController.AddHover(this);
		base.block.tank.TechAudio.AddHover(this);
		TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		base.block.tank.BlockStateController.CategoryActiveChangedEvent.Subscribe(OnCategoryStateChanged);
		UpdateHoverEnabledState();
	}

	private void OnDetaching()
	{
		base.block.tank.control.driveControlEvent.Unsubscribe(DriveControlInput);
		base.block.tank.ResetPhysicsEvent.Unsubscribe(OnResetTechPhysics);
		base.block.tank.TechAudio.RemoveHover(this);
		base.block.tank.BlockStateController.RemoveHover(this);
		TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		base.block.tank.BlockStateController.CategoryActiveChangedEvent.Unsubscribe(OnCategoryStateChanged);
	}

	private void OnResetTechPhysics()
	{
		foreach (HoverJet jet in jets)
		{
			jet.OnResetTechPhysics();
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.hoverPower = m_HoverPower;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_HoverPower = Mathf.Clamp01(serialData2.hoverPower);
		}
		UpdateHoverEnabledState(resetHoverPower: true);
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(GetType(), "hoverPower", m_HoverPower.ToString(CultureInfo.InvariantCulture));
			return;
		}
		string text = context.Retrieve(GetType(), "hoverPower");
		if (!text.NullOrEmpty())
		{
			if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
			{
				m_HoverPower = Mathf.Clamp01(result);
			}
			else
			{
				d.LogError("ModuleHover.OnSerializeText - Failed to parse hoverPower setting from save data on block '" + base.block.name + "'. Expected float value 0-1 but got '" + text + "'. Setting to default value of 0.5!");
				m_HoverPower = 0.5f;
			}
		}
		else
		{
			m_HoverPower = 0.5f;
		}
		if (onTech)
		{
			UpdateHoverEnabledState(resetHoverPower: true);
		}
	}

	private void OnTankBeamEnabled(Tank tech, bool enabled)
	{
		if (base.block.tank != null && base.block.tank == tech)
		{
			UpdateHoverEnabledState();
		}
	}

	private void OnCategoryStateChanged(ModuleControlCategory controllerModuleType, bool enabled)
	{
		if (controllerModuleType == ModuleControlCategory.Hover)
		{
			UpdateHoverEnabledState();
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		UpdateHoverEnabledState();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		UpdateHoverEnabledState();
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
		m_ControlCategoryType = ModuleControlCategory.Hover;
		GetComponentsInChildren(includeInactive: true, jets);
		d.Assert(jets.Count != 0, "ModuleHover needs at least one HoverJet in hiearcarchy");
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		m_Enabled = true;
	}
}
