[FriendlyName("Techs/Toggle Remote Charger Station")]
public class uScript_ToggleRemoteChargerStation : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tank, bool newChargerStatus)
	{
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleRemoteCharger component = enumerator.Current.GetComponent<ModuleRemoteCharger>();
			if ((bool)component)
			{
				component.SetChargerStatus(newChargerStatus);
			}
		}
	}
}
