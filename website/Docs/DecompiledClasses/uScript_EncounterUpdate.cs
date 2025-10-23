using System;

[FriendlyName("Encounter Update")]
[NodePath("Events")]
public class uScript_EncounterUpdate : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private Encounter m_EncounterObj;

	private bool m_IsUpdating;

	public event uScriptEventHandler OnUpdate;

	public event uScriptEventHandler OnSuspend;

	public event uScriptEventHandler OnResume;

	public void OnEnable()
	{
		if (m_EncounterObj.IsNull())
		{
			m_EncounterObj = GetComponent<Encounter>();
		}
		m_IsUpdating = true;
	}

	public void Update()
	{
		bool flag = Singleton.Manager<ManEncounter>.inst.UpdateEnabled && m_EncounterObj.IsScriptUpdateEnabled;
		if (flag)
		{
			if (!m_IsUpdating && this.OnResume != null)
			{
				this.OnResume(this, EventArgs.Empty);
			}
			if (this.OnUpdate != null)
			{
				this.OnUpdate(this, EventArgs.Empty);
			}
		}
		else if (m_IsUpdating && this.OnSuspend != null)
		{
			this.OnSuspend(this, EventArgs.Empty);
		}
		m_IsUpdating = flag;
	}
}
