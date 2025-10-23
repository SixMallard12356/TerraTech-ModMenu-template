using UnityEngine;

public class UISetModeLoadRestrictions : MonoBehaviour
{
	public bool m_SetVehicleModeRestriction;

	public string m_VehicleModeRestriction;

	public bool m_SetVehicleSubmodeRestriction;

	public string m_VehicleSubmodeRestriction;

	public bool m_SetUserDataRestriction;

	public string m_UserDataRestriction;

	public void OnButtonClicked()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleModeRestriction = (m_SetVehicleModeRestriction ? m_VehicleModeRestriction : null);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleSubModeRestriction = (m_SetVehicleSubmodeRestriction ? m_VehicleSubmodeRestriction : null);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleUserDataRestriction = (m_SetUserDataRestriction ? m_UserDataRestriction : null);
	}
}
