[NodePath("TerraTech/Actions/Tutorial")]
[FriendlyName("uScript_GetJoypadControlMode", "Check if we're using joypad control")]
public class uScript_GetJoypadControlMode : uScriptLogic
{
	private bool m_UsingJoypad;

	public bool Joypad => m_UsingJoypad;

	public bool MouseAndKeyboard => !m_UsingJoypad;

	public void In()
	{
		m_UsingJoypad = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
	}

	public void OnDisable()
	{
		m_UsingJoypad = false;
	}
}
