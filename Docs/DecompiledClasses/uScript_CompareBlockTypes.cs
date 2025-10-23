[NodePath("Conditions/Comparison")]
[FriendlyName("Compare BlockTypes", "Fires the appropriate output link depending on the comparison of the attached BlockType variables.")]
public class uScript_CompareBlockTypes : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("A", "First value to compare.")] BlockTypes A, [FriendlyName("B", "Second value to compare.")] BlockTypes B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
