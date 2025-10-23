[NodePath("Conditions/Comparison")]
[FriendlyName("Compare SceneryTypes", "Fires the appropriate output link depending on the comparison of the attached SceneryType variables.")]
public class uScript_CompareSceneryTypes : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("A", "First value to compare.")] SceneryTypes A, [FriendlyName("B", "Second value to compare.")] SceneryTypes B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
