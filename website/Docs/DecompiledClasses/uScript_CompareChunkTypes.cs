[FriendlyName("Compare ChunkTypes", "Fires the appropriate output link depending on the comparison of the attached ChunkType (resource) variables.")]
[NodePath("Conditions/Comparison")]
public class uScript_CompareChunkTypes : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("A", "First value to compare.")] ChunkTypes A, [FriendlyName("B", "Second value to compare.")] ChunkTypes B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
