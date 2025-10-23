#define UNITY_EDITOR
public class uScript_AI_SetBehaviourType : uScriptLogic
{
	private bool m_IsSet;

	public bool Out => true;

	public void In(Tank tank, TechAI.AITypes aiType)
	{
		if (!m_IsSet && tank != null)
		{
			d.LogError("uScript_AI_SetBehaviourType is not implemented!");
			m_IsSet = true;
		}
	}

	public void OnEnable()
	{
		m_IsSet = false;
	}
}
