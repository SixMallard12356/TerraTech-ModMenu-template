using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleHUDContextControl_ColorPickerField))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_SFX_Oneshot : Module, ICircuitDispensor
{
	[SerializeField]
	private TechAudio.SFXType m_SFXType;

	[SerializeField]
	private string m_SubOptionParamName;

	[Tooltip("Array index matches the index of the choice - only show the one that is currently active")]
	[SerializeField]
	private GameObject[] m_CurrentChoiceVisuals;

	[SerializeField]
	private UnityEvent m_SFXPlayedEvent;

	private int m_LastFrameCharge;

	private ModuleHUDContextControl_ColorPickerField m_SfxChoiceControl;

	private int m_CurrentSFXParamIndex;

	private const float kSFXRepeatCooldown = 0f;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return m_LastFrameCharge;
	}

	private void OnChargeChanged(Circuits.BlockChargeData newCharge)
	{
		int chargeStrength = newCharge.ChargeStrength;
		bool num = base.block.IsAttached && chargeStrength > 0;
		bool flag = m_LastFrameCharge > 0;
		if (num && !flag)
		{
			TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, m_SFXType, 0f);
			FMODEvent.FMODParams additionalParam = new FMODEvent.FMODParams(m_SubOptionParamName, m_CurrentSFXParamIndex);
			base.block.tank.TechAudio.PlayOneshot(data, additionalParam);
			m_SFXPlayedEvent?.Invoke();
		}
		m_LastFrameCharge = chargeStrength;
	}

	private void OnSFXIndexChosen()
	{
		m_CurrentSFXParamIndex = m_SfxChoiceControl.CurrentOption.m_Params.m_Return_Int;
		for (int i = 0; i < m_CurrentChoiceVisuals.Length; i++)
		{
			if (m_CurrentChoiceVisuals[i] != null)
			{
				bool active = i == m_CurrentSFXParamIndex;
				m_CurrentChoiceVisuals[i].SetActive(active);
			}
		}
	}

	private void OnPool()
	{
		m_SfxChoiceControl = GetComponent<ModuleHUDContextControl_ColorPickerField>();
		m_SfxChoiceControl.OptionSetEvent.Subscribe(OnSFXIndexChosen);
	}

	private void OnSpawn()
	{
		m_LastFrameCharge = 0;
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
	}
}
