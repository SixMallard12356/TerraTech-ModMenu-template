#define UNITY_EDITOR
[FriendlyName("Tank/Get Tank Block")]
public class uScript_GetTankBlock : uScriptLogic
{
	private TankBlock m_Block;

	public bool Out => true;

	public bool Returned => m_Block != null;

	public bool NotFound => m_Block == null;

	public TankBlock In(Tank tank, BlockTypes blockType)
	{
		if ((bool)tank)
		{
			m_Block = tank.blockman.IterateBlocks().FirstOrDefault((TankBlock x) => x.visible.ItemType == (int)blockType);
		}
		else
		{
			d.LogError($"uScript_GetTankBlock is being passed a null tank so cannot obtain block type {blockType}");
			m_Block = null;
		}
		return m_Block;
	}

	public void OnDisable()
	{
		m_Block = null;
	}
}
