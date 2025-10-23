public class uScript_SetVendorsEnabled : uScriptLogic
{
	public bool Out => true;

	public void In(bool enableShop, bool enableMissionBoard, bool enableSelling, bool enableSCU, bool enableCharging)
	{
		Singleton.Manager<ManWorld>.inst.Vendors.SetShopActive(enableShop);
		Singleton.Manager<ManWorld>.inst.Vendors.SetMissionBoardActive(enableMissionBoard);
		Singleton.Manager<ManWorld>.inst.Vendors.SetSellingActive(enableSelling);
		Singleton.Manager<ManWorld>.inst.Vendors.SetSCUActive(enableSCU);
		Singleton.Manager<ManWorld>.inst.Vendors.SetRemoteChargingActive(enableCharging);
	}
}
