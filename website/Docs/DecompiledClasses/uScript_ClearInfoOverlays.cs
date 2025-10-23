[FriendlyName("Overlays/Clear all info")]
public class uScript_ClearInfoOverlays : uScriptLogic
{
	private bool m_Done;

	public bool Out => true;

	public void In()
	{
		if (!m_Done)
		{
			Singleton.Manager<ManOverlay>.inst.DismissAllInfos();
			m_Done = true;
		}
	}

	public void OnDisable()
	{
		m_Done = false;
	}
}
