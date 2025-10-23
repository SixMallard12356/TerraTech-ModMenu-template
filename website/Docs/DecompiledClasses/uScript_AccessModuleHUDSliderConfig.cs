using UnityEngine;

[NodeToolTip("Allows setting/getting of a provided block's slider value")]
[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("Access Block Slider Config")]
[NodeDescription("Get/Set the slider value (in the associated player-facing unit type) from a block that has a valid configuration")]
public class uScript_AccessModuleHUDSliderConfig : uScriptLogic
{
	protected bool m_HasBlock;

	protected float m_ConfigChangedTime;

	protected uint m_CurTankBlockID;

	protected bool m_CurTankBlockValid;

	protected ModuleHUDSliderControl m_HUDSliderModule;

	private const float m_ConfigChangedTimeout = 1f;

	public bool Out => true;

	public bool HasValidBlock => m_CurTankBlockValid;

	public bool ConfigChanged => m_ConfigChangedTime + 1f > Time.time;

	protected float SliderValue
	{
		get
		{
			if (!m_CurTankBlockValid)
			{
				return 0f;
			}
			return m_HUDSliderModule.Value;
		}
	}

	public float In(TankBlock block, [DefaultValue(-1f)][SocketState(false, true)] float setValue = -1f)
	{
		if (block == null && m_HasBlock)
		{
			Reset();
			return SliderValue;
		}
		if (m_HasBlock && block.blockPoolID == m_CurTankBlockID)
		{
			return SliderValue;
		}
		m_CurTankBlockID = block.blockPoolID;
		m_HasBlock = true;
		if (m_CurTankBlockValid)
		{
			m_HUDSliderModule.OptionSetEvent.Unsubscribe(OnOptionSet);
		}
		m_HUDSliderModule = block.GetComponent<ModuleHUDSliderControl>();
		m_CurTankBlockValid = m_HUDSliderModule != null;
		if (m_CurTankBlockValid)
		{
			m_HUDSliderModule.OptionSetEvent.Subscribe(OnOptionSet);
		}
		return SliderValue;
	}

	public float In_SetValue(TankBlock block, [DefaultValue(0f)][SocketState(true, true)] float setValue)
	{
		In(block);
		if (!HasValidBlock || m_HUDSliderModule.Value.Approximately(setValue))
		{
			return SliderValue;
		}
		m_HUDSliderModule.SetValueMultiplayerSafe(setValue);
		return SliderValue;
	}

	private void Reset()
	{
		if (m_CurTankBlockValid)
		{
			m_HUDSliderModule.OptionSetEvent.Unsubscribe(OnOptionSet);
		}
		m_HasBlock = false;
		m_CurTankBlockID = 0u;
		m_CurTankBlockValid = false;
		m_HUDSliderModule = null;
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
