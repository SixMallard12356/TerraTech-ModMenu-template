namespace Payload.UI.Saving;

public class ResumeGameCommand : SaveCommand
{
	public override void Execute(SaveOperationData data)
	{
		if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack())
		{
			Singleton.Manager<ManPauseGame>.inst.TogglePauseGame();
		}
		SetComplete(data);
	}
}
