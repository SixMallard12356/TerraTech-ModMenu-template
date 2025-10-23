public class uScript_GetPromptResult : uScriptLogic
{
	private uScript_ShowPrompt.Context m_Context;

	public bool Out => true;

	public bool Accepted
	{
		get
		{
			if (m_Context != null)
			{
				return m_Context.m_State == uScript_ShowPrompt.State.Accepted;
			}
			return false;
		}
	}

	public bool Declined
	{
		get
		{
			if (m_Context != null)
			{
				return m_Context.m_State == uScript_ShowPrompt.State.Declined;
			}
			return false;
		}
	}

	public bool Showing
	{
		get
		{
			if (m_Context != null)
			{
				return m_Context.m_State == uScript_ShowPrompt.State.Showing;
			}
			return false;
		}
	}

	public void In(uScript_ShowPrompt.Context context)
	{
		m_Context = context;
	}

	public void OnDisable()
	{
		m_Context = null;
	}
}
