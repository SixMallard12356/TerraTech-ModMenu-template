using System;

public class uScript_BoundsWarningEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler OnBoundsWarningCaution;

	public event uScriptEventHandler OnBoundsWarningIllegal;

	public void OnEnable()
	{
		CheckpointChallenge.OnBoundsWarning.Subscribe(OnBoundsWarningEvent);
	}

	public void OnDisable()
	{
		CheckpointChallenge.OnBoundsWarning.Unsubscribe(OnBoundsWarningEvent);
	}

	private void OnBoundsWarningEvent(CheckpointChallenge.BoundsArea area)
	{
		if (area == CheckpointChallenge.BoundsArea.Caution && this.OnBoundsWarningCaution != null)
		{
			this.OnBoundsWarningCaution(this, new EventArgs());
		}
		else if (area == CheckpointChallenge.BoundsArea.Illegal && this.OnBoundsWarningIllegal != null)
		{
			this.OnBoundsWarningIllegal(this, new EventArgs());
		}
	}
}
