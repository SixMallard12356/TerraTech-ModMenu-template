[FriendlyName("Compare FactionSubTypes", "Fires the appropriate output link depending on the comparison of the attached FactionSubType variables.")]
[NodePath("Conditions/Comparison")]
public class uScript_CompareFactionSubTypes : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("A", "First value to compare.")] FactionSubTypes A, [FriendlyName("B", "Second value to compare.")] FactionSubTypes B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
