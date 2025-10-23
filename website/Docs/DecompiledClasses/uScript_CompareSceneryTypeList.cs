[NodePath("Conditions/Comparison")]
[FriendlyName("Compare SceneryType List", "Fires the appropriate output link depending on the comparison of the attached SceneryType variables. Matching any value in the list counts as being equal.")]
public class uScript_CompareSceneryTypeList : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)  ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("A", "First value to compare.")] SceneryTypes A, [FriendlyName("B (List)", "Second value to compare.")] SceneryTypes[] B)
	{
		m_EqualTo = false;
		for (int i = 0; i < B.Length; i++)
		{
			if (A == B[i])
			{
				m_EqualTo = true;
				break;
			}
		}
	}
}
