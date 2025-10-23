public class uScript_SetBatteryChargeAmount : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, float chargeAmount)
	{
		if (tech != null)
		{
			tech.EnergyRegulator.SetAllStoresAmount(chargeAmount);
		}
	}
}
