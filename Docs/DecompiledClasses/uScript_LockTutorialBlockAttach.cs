#define UNITY_EDITOR
[NodeToolTip("Locks the block's to prevent it being attached incorrectly or anchored on its own as a new tech")]
public class uScript_LockTutorialBlockAttach : uScriptLogic
{
	private ModuleAnchor m_Anchor;

	private bool m_AnchorChecked;

	public bool Out => true;

	public void In(TankBlock block)
	{
		if (block != null)
		{
			if (m_Anchor == null && !m_AnchorChecked)
			{
				m_Anchor = block.GetComponent<ModuleAnchor>();
			}
			m_AnchorChecked = true;
			if ((bool)m_Anchor)
			{
				block.LockBlockAnchor();
			}
			block.LockTutorialBlockAttach();
		}
		else
		{
			d.LogError("uScript_LockTutorialBlockAttach - NULL parameter passed in for block");
		}
	}

	public void OnEnable()
	{
		m_AnchorChecked = false;
	}
}
