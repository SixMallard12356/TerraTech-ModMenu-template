using UnityEngine;

[FriendlyName("Access Block Option Picker Config")]
[NodePath("TerraTech/Actions/Blocks")]
[NodeToolTip("Allows setting/getting of a provided block's option picker config")]
[NodeDescription("Get/Set the currentOption (Index) from a block that has a valid configuration")]
public class uScript_AccessModuleHUDContextControl_ColorPickerField : uScriptLogic
{
	protected bool m_HasBlock;

	protected uint m_CurTankBlockID;

	protected bool m_CurTankBlockValid;

	protected ModuleHUDContextControl_ColorPickerField m_HUDOptionPickerModule;

	protected float m_ConfigChangedTime;

	private const float m_ConfigChangedTimeout = 1f;

	public bool Out => true;

	public bool HasValidBlock => m_CurTankBlockValid;

	public bool ConfigChanged => m_ConfigChangedTime + 1f > Time.time;

	protected int CurrentOptionIndex
	{
		get
		{
			if (!m_CurTankBlockValid)
			{
				return 0;
			}
			return m_HUDOptionPickerModule.FirstCurrentOptionIndex;
		}
	}

	public int In(TankBlock block)
	{
		if (block == null && m_HasBlock)
		{
			Reset();
			return CurrentOptionIndex;
		}
		if (m_HasBlock && block.blockPoolID == m_CurTankBlockID)
		{
			return CurrentOptionIndex;
		}
		m_CurTankBlockID = block.blockPoolID;
		m_HasBlock = true;
		if (m_CurTankBlockValid)
		{
			m_HUDOptionPickerModule.OptionSetEvent.Unsubscribe(OnOptionSet);
		}
		m_HUDOptionPickerModule = block.GetComponent<ModuleHUDContextControl_ColorPickerField>();
		m_CurTankBlockValid = m_HUDOptionPickerModule != null;
		if (m_CurTankBlockValid)
		{
			m_HUDOptionPickerModule.OptionSetEvent.Subscribe(OnOptionSet);
		}
		return CurrentOptionIndex;
	}

	public int In_SetValue(TankBlock block, int value)
	{
		In(block);
		byte b = (byte)Mathf.Min(value, 255);
		if (!HasValidBlock || m_HUDOptionPickerModule.CurrentOptionsBitfield == 1 << (int)b)
		{
			return CurrentOptionIndex;
		}
		m_HUDOptionPickerModule.SetOptionMultiplayerSafe(1 << (int)b);
		return CurrentOptionIndex;
	}

	private void Reset()
	{
		if (m_CurTankBlockValid)
		{
			m_HUDOptionPickerModule.OptionSetEvent.Unsubscribe(OnOptionSet);
		}
		m_HasBlock = false;
		m_CurTankBlockID = 0u;
		m_CurTankBlockValid = false;
		m_HUDOptionPickerModule = null;
		m_ConfigChangedTime = 0f;
	}

	private void OnOptionSet()
	{
		m_ConfigChangedTime = Time.time;
	}

	public void OnDisable()
	{
		Reset();
	}
}
