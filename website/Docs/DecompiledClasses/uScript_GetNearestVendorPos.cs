using UnityEngine;

[FriendlyName("uScript_GetNearestVendorPos", "Find position of nearest vendor")]
public class uScript_GetNearestVendorPos : uScriptLogic
{
	private bool m_FoundVendor;

	private Vector3 m_CachedNearestVendorPositionWorld = Vector3.zero;

	public bool Out => true;

	public bool Found => m_FoundVendor;

	public bool Missing => !m_FoundVendor;

	public Vector3 In()
	{
		Vector3 worldPos = Singleton.playerPos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
		m_FoundVendor = Singleton.Manager<ManWorld>.inst.TryFindNearestVendorPos(worldPos, out var nearestVendorPosWorld);
		if (!nearestVendorPosWorld.Approximately(in m_CachedNearestVendorPositionWorld, 1f))
		{
			m_CachedNearestVendorPositionWorld = nearestVendorPosWorld;
			Vector3 scenePos = nearestVendorPosWorld + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			Singleton.Manager<ManWorld>.inst.GetTerrainHeight(scenePos, out var outHeight);
			m_CachedNearestVendorPositionWorld.y = outHeight;
		}
		return m_CachedNearestVendorPositionWorld + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
	}

	public void OnEnable()
	{
		m_FoundVendor = false;
		m_CachedNearestVendorPositionWorld = Vector3.zero;
	}
}
