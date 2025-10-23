[FriendlyName("uScript_SetLabShutterSwitch", "Cause a Pressure Pad to trigger their door open or closed")]
[NodePath("TerraTech/Actions")]
public class uScript_SetLabShutterSwitch : uScriptLogic
{
	public bool Out => true;

	public void In(string switchToOpen, string switchToClose, string[] batchSwitchesToOpen, string[] batchSwitchesToClose)
	{
		if (!string.IsNullOrEmpty(switchToOpen))
		{
			LaserLabShutterSwitch.TriggerPressurePadOn(switchToOpen);
		}
		if (!string.IsNullOrEmpty(switchToClose))
		{
			LaserLabShutterSwitch.TriggerPressurePadOff(switchToClose);
		}
		string[] array = batchSwitchesToOpen;
		for (int i = 0; i < array.Length; i++)
		{
			LaserLabShutterSwitch.TriggerPressurePadOn(array[i]);
		}
		array = batchSwitchesToClose;
		for (int i = 0; i < array.Length; i++)
		{
			LaserLabShutterSwitch.TriggerPressurePadOff(array[i]);
		}
	}
}
