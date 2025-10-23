[FriendlyName("Block/Is block anchored")]
public class uScript_IsBlockAnchored : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(TankBlock block)
	{
		if ((bool)block)
		{
			m_True = (bool)block.tank && block.tank.IsAnchored;
		}
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
