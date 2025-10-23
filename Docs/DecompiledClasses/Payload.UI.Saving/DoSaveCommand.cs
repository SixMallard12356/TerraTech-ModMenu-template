namespace Payload.UI.Saving;

public class DoSaveCommand : SaveCommand
{
	private bool m_ShowingSaveNotification;

	public override void Execute(SaveOperationData data)
	{
		if (Singleton.Manager<ManGameMode>.inst.SaveCurrentMode(data.m_Name))
		{
			SetComplete(data);
			if (Singleton.playerTank != null && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>())
			{
				_ = (Singleton.playerPos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld).SetY(0f).magnitude;
			}
		}
		else
		{
			data.m_Error = $"Could not save {data.m_Name}";
			SetCancelled(data);
		}
	}
}
