#define UNITY_EDITOR
[NodeToolTip("Locks all the blocks on a tech. This will prevent them from being picked up (but can still be interacted with!)")]
[FriendlyName("Lock Tech Functionality")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_LockTech : uScriptLogic
{
	public enum TechLockType
	{
		LockDetach,
		LockInteraction,
		LockAttach,
		LockDetachAndInteraction,
		LockAll
	}

	public bool Out => true;

	public void In(Tank tech, [DefaultValue(TechLockType.LockDetachAndInteraction)] TechLockType lockType = TechLockType.LockDetachAndInteraction)
	{
		if (!(tech != null))
		{
			return;
		}
		bool lockInteraction = false;
		bool lockDetach = false;
		bool lockAttach = false;
		GetLockSettings(lockType, out lockInteraction, out lockDetach, out lockAttach);
		if (lockInteraction)
		{
			tech.visible.SetLockTimout(Visible.LockTimerTypes.Interactible);
		}
		if (lockAttach)
		{
			tech.visible.SetLockTimout(Visible.LockTimerTypes.BlocksAttachable);
			if (tech.netTech != null && ManNetwork.IsNetworked && ManNetwork.IsHost)
			{
				tech.netTech.OnServerSetLockBlockAttach();
			}
		}
		if (lockDetach)
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.visible.SetLockTimout(Visible.LockTimerTypes.Grabbable);
			}
			if (tech.netTech != null && ManNetwork.IsNetworked && ManNetwork.IsHost)
			{
				tech.netTech.OnServerSetLockBlockDetach();
			}
		}
	}

	private void GetLockSettings(TechLockType lockType, out bool lockInteraction, out bool lockDetach, out bool lockAttach)
	{
		lockInteraction = false;
		lockDetach = false;
		lockAttach = false;
		switch (lockType)
		{
		case TechLockType.LockDetach:
			lockDetach = true;
			break;
		case TechLockType.LockInteraction:
			lockInteraction = true;
			break;
		case TechLockType.LockAttach:
			lockAttach = true;
			break;
		case TechLockType.LockDetachAndInteraction:
			lockInteraction = true;
			lockDetach = true;
			break;
		case TechLockType.LockAll:
			lockInteraction = true;
			lockDetach = true;
			lockAttach = true;
			break;
		default:
			d.LogError("uScript_LockTech - Unhandled LockType! " + lockType);
			break;
		}
	}
}
