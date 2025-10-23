[FriendlyName("Building Options")]
public class uScript_ChangeBuildingOptions : uScriptLogic
{
	public enum BuildingOptions
	{
		Rotate,
		Detach,
		ToggleBeam,
		ReduceDragSpeed,
		MoveInBeam
	}

	public bool Out => true;

	public void In(BuildingOptions change, bool allow)
	{
		switch (change)
		{
		case BuildingOptions.Rotate:
			if (allow)
			{
				Mode<ModeMain>.inst.TutorialDisableBlockRotation = false;
			}
			else
			{
				Mode<ModeMain>.inst.TutorialDisableBlockRotation = true;
			}
			break;
		case BuildingOptions.Detach:
			if (allow)
			{
				Mode<ModeMain>.inst.TutorialDisableBlockRemoval = false;
			}
			else
			{
				Mode<ModeMain>.inst.TutorialDisableBlockRemoval = true;
			}
			break;
		case BuildingOptions.ToggleBeam:
			if (allow)
			{
				Mode<ModeMain>.inst.TutorialLockBeam = false;
			}
			else
			{
				Mode<ModeMain>.inst.TutorialLockBeam = true;
			}
			break;
		case BuildingOptions.ReduceDragSpeed:
			if (allow)
			{
				Mode<ModeMain>.inst.ReduceBlockDragReleaseSpeed = true;
			}
			else
			{
				Mode<ModeMain>.inst.ReduceBlockDragReleaseSpeed = false;
			}
			break;
		case BuildingOptions.MoveInBeam:
			if (allow)
			{
				Mode<ModeMain>.inst.TutorialLockBeamMove = false;
			}
			else
			{
				Mode<ModeMain>.inst.TutorialLockBeamMove = true;
			}
			break;
		}
	}
}
