[FriendlyName("For Each In List (Block)", "Iterates through a list, one item at a time, and returns the current item.\n\nNote: uScript events must drive each iteration.")]
[NodeToolTip("Iterate through each Block in a Block List (uScript events must drive each iteration). Note that the list will be stored until all items have been iterated through or Reset is hit with a new list (which can only be done using named list variables).")]
[NodePath("TerraTech/Actions/Blocks")]
[NodeHelp("http://docs.uscript.net/#3-Working_With_uScript/3.4-Nodes.htm")]
public class uScript_ForEachListBlock : uScriptLogic
{
	private TankBlock[] m_List;

	private int m_CurrentIndex;

	private bool m_Done;

	private bool m_ImmediateDone;

	public bool Immediate
	{
		get
		{
			if (!m_ImmediateDone)
			{
				m_ImmediateDone = true;
				return true;
			}
			return false;
		}
	}

	[FriendlyName("Done Iterating")]
	public bool Done => m_Done;

	[FriendlyName("Iteration")]
	public bool Iteration
	{
		get
		{
			if (m_List != null && m_CurrentIndex <= m_List.Length)
			{
				return m_CurrentIndex != 0;
			}
			return false;
		}
	}

	[FriendlyName("Reset")]
	public void Reset(TankBlock[] List, out TankBlock Value, out int currentIndex)
	{
		Value = null;
		m_List = List;
		m_CurrentIndex = 0;
		currentIndex = m_CurrentIndex;
		m_Done = false;
		m_ImmediateDone = false;
	}

	public void In([FriendlyName("List", "The list to iterate over.")] TankBlock[] List, [FriendlyName("Current", "The item for the current loop iteration.")] out TankBlock Value, [SocketState(false, false)][FriendlyName("Current Index", "The index value for the current loop iteration.")] out int currentIndex)
	{
		if (m_List == null)
		{
			m_List = List;
			m_CurrentIndex = 0;
			m_Done = false;
		}
		m_ImmediateDone = m_List == null || m_CurrentIndex != 0;
		Value = null;
		currentIndex = m_CurrentIndex;
		if (m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				Value = m_List[m_CurrentIndex];
				currentIndex = m_CurrentIndex;
			}
			m_CurrentIndex++;
			if (m_CurrentIndex > m_List.Length)
			{
				m_List = null;
				m_Done = true;
			}
		}
	}
}
