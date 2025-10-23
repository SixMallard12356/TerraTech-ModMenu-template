using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitDispensor), typeof(ModuleHUDContextControl_ColorPickerField))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Input_KeyBind_FromUI : ModuleCircuit_Input_KeyBind
{
	[Header("Display")]
	[SerializeField]
	private DigitalDisplay m_BoundKeyDisplay;

	[SerializeField]
	private TextMeshPro m_BoundKeyDisplayText;

	[SerializeField]
	private ModuleHUDContextControl_ColorPickerField m_KeybindControl;

	[SerializeField]
	[RewiredAction]
	private int[] m_ActionChoices;

	public void SetInputActionIndex(int actionIndex)
	{
		int num = m_ActionChoices[actionIndex];
		SetRewiredActionID(num);
		m_BoundKeyDisplay?.SetValue(actionIndex);
		if (m_BoundKeyDisplayText != null)
		{
			string keyBoundPrimaryName = Singleton.Manager<ManInput>.inst.GetKeyBoundPrimaryName(num);
			m_BoundKeyDisplayText.text = keyBoundPrimaryName;
		}
	}

	private void OnKeybindUIOptionSet()
	{
		int return_Int = m_KeybindControl.CurrentOption.m_Params.m_Return_Int;
		SetInputActionIndex(return_Int);
	}

	private void OnPool()
	{
		m_KeybindControl.OptionSetEvent.Subscribe(OnKeybindUIOptionSet);
	}

	private void OnSpawn()
	{
		SetInputActionIndex(1);
	}
}
