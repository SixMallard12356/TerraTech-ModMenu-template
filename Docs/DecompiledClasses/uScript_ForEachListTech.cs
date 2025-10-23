[NodePath("TerraTech/Actions/Techs")]
[NodeHelp("http://docs.uscript.net/#3-Working_With_uScript/3.4-Nodes.htm")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("For Each In List (Tech)", "Iterates through a list, one item at a time, and returns the current item.\n\nNote: uScript events must drive each iteration.")]
[NodeToolTip("Iterate through each Tech in a Tech List (uScript events must drive each iteration). Note that the list will be stored until all items have been iterated through or Reset is hit with a new list (which can only be done using named list variables).")]
public class uScript_ForEachListTech : uScriptLogic
{
	private Tank[] m_List;

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
	public void Reset(Tank[] List, out Tank Value, out int currentIndex)
	{
		Value = null;
		m_List = List;
		m_CurrentIndex = 0;
		currentIndex = m_CurrentIndex;
		m_Done = false;
		m_ImmediateDone = false;
	}

	public void In([FriendlyName("List", "The list to iterate over.")] Tank[] List, [FriendlyName("Current", "The item for the current loop iteration.")] out Tank Value, [SocketState(false, false)][FriendlyName("Current Index", "The index value for the current loop iteration.")] out int currentIndex)
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
