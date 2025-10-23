#define UNITY_EDITOR
using System;

public class uScript_SaveLoad : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_JustAwoke = true;

	private bool m_MustLoad;

	[FriendlyName("On Save Event")]
	public event uScriptEventHandler SaveEvent;

	[FriendlyName("On Load Event")]
	public event uScriptEventHandler LoadEvent;

	[FriendlyName("On Restart Event")]
	public event uScriptEventHandler RestartEvent;

	private void Start()
	{
		m_JustAwoke = true;
	}

	private void Update()
	{
		if (m_JustAwoke)
		{
			if (m_MustLoad)
			{
				Load(null);
			}
			m_JustAwoke = false;
		}
	}

	private void OnPool()
	{
		Singleton.Manager<ManEncounter>.inst.SaveEvent.Subscribe(Save);
		Singleton.Manager<ManEncounter>.inst.LoadEvent.Subscribe(Load);
	}

	private void OnSpawn()
	{
		m_MustLoad = true;
		Restart();
	}

	private void OnRecycle()
	{
		Restart();
	}

	private void OnDepool()
	{
		Singleton.Manager<ManEncounter>.inst.SaveEvent.Unsubscribe(Save);
		Singleton.Manager<ManEncounter>.inst.LoadEvent.Unsubscribe(Load);
	}

	public void Save(ManEncounter.SaveData data)
	{
		if (base.gameObject.activeInHierarchy && this.SaveEvent != null)
		{
			this.SaveEvent(this, new EventArgs());
		}
	}

	private void Load(ManEncounter.SaveData data)
	{
		if (base.gameObject.activeInHierarchy)
		{
			d.Assert(m_MustLoad, "uScript_SaveLoad.Load - Called while the script has already been loaded this lifetime!?");
			if (this.LoadEvent != null)
			{
				this.LoadEvent(this, new EventArgs());
			}
			m_MustLoad = false;
		}
	}

	private void Restart()
	{
		if (this.RestartEvent != null)
		{
			this.RestartEvent(this, new EventArgs());
		}
	}
}
