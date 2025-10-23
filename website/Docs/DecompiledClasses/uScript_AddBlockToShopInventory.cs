[NodeDescription("Forces a block into the shop inventory. Shop panel must be open!")]
public class uScript_AddBlockToShopInventory : uScriptLogic
{
	private bool m_FirstFrame;

	public bool Out => true;

	public void In(BlockTypes blockType)
	{
		if (m_FirstFrame)
		{
			UIShopBlockSelect uIShopBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockShop) as UIShopBlockSelect;
			if (uIShopBlockSelect != null)
			{
				uIShopBlockSelect.EnsureBlockInInvetory(blockType);
			}
			m_FirstFrame = false;
		}
	}

	public void OnEnable()
	{
		m_FirstFrame = true;
	}
}
