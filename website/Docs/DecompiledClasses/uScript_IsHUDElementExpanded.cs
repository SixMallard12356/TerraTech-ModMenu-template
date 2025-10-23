[FriendlyName("uScript_IsHUDElementExpanded", "Checks whether or not a UI element is on-screen")]
[NodePath("TerraTech/UI")]
public class uScript_IsHUDElementExpanded : uScriptLogic
{
	private bool m_Expanded;

	public bool True => m_Expanded;

	public bool False => !m_Expanded;

	public void In(ManHUD.HUDElementType hudElement)
	{
		m_Expanded = Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(hudElement);
	}
}
