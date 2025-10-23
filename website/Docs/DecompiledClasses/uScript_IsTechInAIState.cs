#define UNITY_EDITOR
public class uScript_IsTechInAIState : uScriptLogic
{
	private bool m_IsInAIState;

	public bool True => m_IsInAIState;

	public bool False => !m_IsInAIState;

	public void In(Tank tech, AITreeType.AITypes targetAIType)
	{
		m_IsInAIState = false;
		if (tech != null)
		{
			if (tech.AI.TryGetCurrentAIType(out var aiType))
			{
				m_IsInAIState = aiType == targetAIType;
			}
		}
		else
		{
			d.Log("uScript_IsTechInAIState - tech is null");
		}
	}
}
