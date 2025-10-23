using System.Collections.Generic;

[FriendlyName("Tank/Has any block been attached")]
public class uScript_HasAnyBlockBeenAttached : uScriptLogic
{
	private bool m_Attached;

	private bool m_Registered;

	private List<TankBlock> m_Blocks = new List<TankBlock>();

	public bool Out => true;

	public bool True => m_Attached;

	public bool False => !m_Attached;

	public void In(List<TankBlock> blocks)
	{
		if (m_Registered || m_Attached || blocks == null)
		{
			return;
		}
		foreach (TankBlock block in blocks)
		{
			block.AttachedEvent.Subscribe(OnBlockAttached);
			m_Blocks.Add(block);
		}
		m_Registered = true;
	}

	private void OnBlockAttached()
	{
		m_Attached = true;
		UnregisterEvents();
	}

	private void UnregisterEvents()
	{
		if (m_Blocks == null)
		{
			return;
		}
		foreach (TankBlock block in m_Blocks)
		{
			block.AttachedEvent.Unsubscribe(OnBlockAttached);
		}
		m_Registered = false;
	}

	public void OnDisable()
	{
		if (m_Registered)
		{
			UnregisterEvents();
		}
		m_Registered = false;
		m_Attached = false;
	}
}
