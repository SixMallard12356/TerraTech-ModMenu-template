using System.Collections.Generic;

[FriendlyName("Tank/Get tank blocks")]
public class uScript_GetTankBlocks : uScriptLogic
{
	private List<TankBlock> m_Blocks = new List<TankBlock>();

	private bool m_Done;

	private Tank m_LastTank;

	public bool Out => true;

	public List<TankBlock> In(Tank tank)
	{
		if ((!m_Done || m_LastTank != tank) && (bool)tank)
		{
			m_LastTank = tank;
			m_Blocks = tank.blockman.IterateBlocks().ToList();
			m_Done = true;
		}
		return m_Blocks;
	}

	public void OnDisable()
	{
		m_Done = false;
		m_Blocks.Clear();
	}
}
