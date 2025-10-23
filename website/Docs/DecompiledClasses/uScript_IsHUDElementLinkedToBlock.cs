#define UNITY_EDITOR
[FriendlyName("uScript_IsHUDElementLinkedToBlock", "Checks whether or not a UI element is on-screen")]
[NodePath("TerraTech/UI")]
public class uScript_IsHUDElementLinkedToBlock : uScriptLogic
{
	private bool m_LinkMatches;

	public bool True => m_LinkMatches;

	public bool False => !m_LinkMatches;

	public void In(ManHUD.HUDElementType hudElement, TankBlock targetBlock)
	{
		m_LinkMatches = false;
		if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hudElement))
		{
			return;
		}
		UIHUDElement hudElement2 = Singleton.Manager<ManHUD>.inst.GetHudElement(hudElement);
		if (hudElement2 != null)
		{
			TankBlock tankBlock = null;
			UIItemRecipeSelect uIItemRecipeSelect = hudElement2 as UIItemRecipeSelect;
			if (uIItemRecipeSelect != null)
			{
				tankBlock = ((uIItemRecipeSelect.Consume != null) ? uIItemRecipeSelect.Consume.block : null);
			}
			else
			{
				d.LogError("uScript_IsHUDElementLinkedToBlock - Hud element type not yet supported!");
			}
			m_LinkMatches = tankBlock != null && tankBlock == targetBlock;
		}
	}
}
