#define UNITY_EDITOR
[FriendlyName("Unlock Dispenser")]
public class uScript_UnlockDispenser : uScriptLogic
{
	private bool m_Unlocked;

	public bool Out => true;

	public bool Unlocked => m_Unlocked;

	public void In(Tank dispenser, [SocketState(false, false)] Tank[] dispensers = null)
	{
		if (dispenser != null && !m_Unlocked)
		{
			Unlock(dispenser);
			m_Unlocked = true;
		}
	}

	public void UnlockMultiple(Tank dispenser, [SocketState(false, false)] Tank[] dispensers = null)
	{
		if (dispensers == null || m_Unlocked)
		{
			return;
		}
		foreach (Tank tank in dispensers)
		{
			if ((bool)tank)
			{
				Unlock(tank);
				m_Unlocked = true;
			}
		}
	}

	private void Unlock(Tank dispenser)
	{
		ModuleItemDispenser moduleItemDispenser = dispenser.blockman.IterateBlockComponents<ModuleItemDispenser>().FirstOrDefault();
		ModuleItemHolder moduleItemHolder = dispenser.blockman.IterateBlockComponents<ModuleItemHolder>().FirstOrDefault();
		d.Assert((bool)moduleItemDispenser && (bool)moduleItemHolder, "dispenser missing ModuleDispenser or ModuleHolder");
		if (moduleItemHolder.SingleStack.IsEmpty)
		{
			moduleItemDispenser.amountRemaining = 1;
		}
		else
		{
			moduleItemDispenser.amountRemaining = 0;
		}
	}

	public void OnDisable()
	{
		m_Unlocked = false;
	}
}
