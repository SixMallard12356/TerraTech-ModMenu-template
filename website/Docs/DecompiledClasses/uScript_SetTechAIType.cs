#define UNITY_EDITOR
public class uScript_SetTechAIType : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, AITreeType.AITypes aiType)
	{
		if (tech != null)
		{
			tech.AI.SetBehaviorType(aiType);
		}
		else
		{
			d.Log("uScript_SetTechAIType - tech is null");
		}
	}
}
