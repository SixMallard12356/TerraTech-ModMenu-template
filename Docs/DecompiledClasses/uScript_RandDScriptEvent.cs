using System;

public class uScript_RandDScriptEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_IsUpdating;

	public event uScriptEventHandler OnUpdate;

	public event uScriptEventHandler OnActivate;

	public event uScriptEventHandler OnDeactivate;

	public void OnEnable()
	{
		m_IsUpdating = false;
	}

	public void Activate()
	{
		m_IsUpdating = true;
		if (this.OnActivate != null)
		{
			this.OnActivate(this, new EventArgs());
		}
	}

	public void Deactivate()
	{
		m_IsUpdating = false;
		if (this.OnDeactivate != null)
		{
			this.OnDeactivate(this, new EventArgs());
		}
	}

	public void Update()
	{
		if (m_IsUpdating && this.OnUpdate != null)
		{
			this.OnUpdate(this, new EventArgs());
		}
	}
}
