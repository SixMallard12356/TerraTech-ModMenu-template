[FriendlyName("uScript_IsHUDElementVisible", "Checks whether or not a UI element is on-screen")]
[NodePath("TerraTech/UI")]
public class uScript_IsHUDElementVisible : uScriptLogic
{
	private bool m_Visible;

	public bool True => m_Visible;

	public bool False => !m_Visible;

	public void In(ManHUD.HUDElementType hudElement)
	{
		m_Visible = Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hudElement);
	}
}
