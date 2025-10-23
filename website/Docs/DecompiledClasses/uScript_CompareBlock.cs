[FriendlyName("Compare Block", "Fires the appropriate output link depending on the comparison of the attached Block variables.")]
[NodePath("Conditions/Comparison")]
public class uScript_CompareBlock : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([DefaultValue(null)][FriendlyName("A", "First value to compare.")] TankBlock A, [DefaultValue(null)][FriendlyName("B", "Second value to compare.")] TankBlock B)
	{
		m_EqualTo = A == B;
	}
}
