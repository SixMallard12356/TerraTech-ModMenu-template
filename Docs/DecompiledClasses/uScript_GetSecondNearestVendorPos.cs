using UnityEngine;

public class uScript_GetSecondNearestVendorPos : uScriptLogic
{
	private bool m_FoundVendor;

	public bool Out => true;

	public bool Found => m_FoundVendor;

	public bool Missing => !m_FoundVendor;

	public Vector3 In()
	{
		WorldPosition worldPosition = WorldPosition.FromScenePosition(Singleton.playerPos);
		m_FoundVendor = Singleton.Manager<ManWorld>.inst.TryFindNearestNeighbouringVendorPos(worldPosition.GameWorldPosition, out var secondNearestVendorPosWorld);
		return secondNearestVendorPosWorld + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
	}

	public void OnEnable()
	{
		m_FoundVendor = false;
	}
}
