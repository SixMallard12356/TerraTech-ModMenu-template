[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires the appropriate output link(s) depending on the comparison of the attached integer variables.")]
[NodeHelp("http://docs.uscript.net/#3-Working_With_uScript/3.4-Nodes.htm")]
[FriendlyName("Check Int Equals", "Fires the appropriate output link(s) depending on the comparison of the attached integer variables.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptCon_CheckIntEquals : uScriptLogic
{
	private bool m_Equal;

	public bool True => m_Equal;

	public bool False => !m_Equal;

	public void In([FriendlyName("A", "First value to compare.")] int A, [FriendlyName("B", "Second value to compare.")] int B)
	{
		m_Equal = A == B;
	}
}
