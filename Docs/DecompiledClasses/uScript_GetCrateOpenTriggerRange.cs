#define UNITY_EDITOR
public class uScript_GetCrateOpenTriggerRange : uScriptLogic
{
	private bool m_ErrorThrown;

	public bool Out => true;

	public float In(Crate crate)
	{
		float result = 0f;
		if (crate != null)
		{
			result = crate.OpenTriggerRange;
		}
		else if (!m_ErrorThrown)
		{
			d.LogError("uScript_GetCrateOpenTriggerRange - crate is null");
			m_ErrorThrown = true;
		}
		return result;
	}
}
