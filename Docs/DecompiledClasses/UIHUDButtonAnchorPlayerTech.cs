using UnityEngine;

public class UIHUDButtonAnchorPlayerTech : UIHUDElement
{
	[SerializeField]
	private UIAnchorPlayerTechButton m_Button;

	public override void Show(object context)
	{
		base.Show(context);
		if (m_Button != null)
		{
			m_Button.UpdateButton();
		}
	}
}
