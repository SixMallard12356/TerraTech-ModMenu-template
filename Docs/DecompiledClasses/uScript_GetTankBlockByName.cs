#define UNITY_EDITOR
using System;

[FriendlyName("Tank/Get Tank Block By Name")]
public class uScript_GetTankBlockByName : uScriptLogic
{
	private TankBlock m_Block;

	public bool Out => true;

	public bool Returned => m_Block != null;

	public bool NotFound => m_Block == null;

	public TankBlock In(Tank tank, string blockTypeName)
	{
		if ((bool)tank)
		{
			if (Enum.TryParse<BlockTypes>(blockTypeName, out var blockType))
			{
				m_Block = tank.blockman.IterateBlocks().FirstOrDefault((TankBlock x) => x.visible.ItemType == (int)blockType);
			}
			else
			{
				d.LogError("uScript_GetTankBlockByName is being an invalid blockTypeName so cannot obtain block type " + blockTypeName);
				m_Block = null;
			}
		}
		else
		{
			d.LogError("uScript_GetTankBlockByName is being passed a null tank so cannot obtain block type " + blockTypeName);
			m_Block = null;
		}
		return m_Block;
	}

	public void OnDisable()
	{
		m_Block = null;
	}
}
