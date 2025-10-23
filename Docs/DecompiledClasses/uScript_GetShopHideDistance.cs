public class uScript_GetShopHideDistance : uScriptLogic
{
	public bool Out => true;

	public float In()
	{
		return Singleton.Manager<ManPurchases>.inst.ShopHideDistance;
	}
}
