using UnityEngine;

public class uScript_FindNearestVendor : uScriptLogic
{
	private Tank m_Tank;

	public bool Out => true;

	public bool Returned => m_Tank != null;

	public bool NotReturned => m_Tank == null;

	private static bool IsVendor(Tank tech)
	{
		bool result = false;
		if (tech.Team == -2)
		{
			int iD = (tech.netTech.IsNotNull() ? tech.netTech.HostID : tech.visible.ID);
			result = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(iD).RadarType == RadarTypes.Vendor;
		}
		return result;
	}

	public static Tank FindNearestVendorTo(Vector3 scenePos)
	{
		Tank tank = null;
		Vector3 worldPos = scenePos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
		Vector3 nearestVendorPosWorld;
		bool flag = Singleton.Manager<ManWorld>.inst.TryFindNearestVendorPos(worldPos, out nearestVendorPosWorld);
		Vector3 vector = nearestVendorPosWorld + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
		float num = 0f;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			if (!IsVendor(item))
			{
				continue;
			}
			bool flag2;
			if (flag)
			{
				float num2 = 50f;
				flag2 = (vector - item.trans.position).SetY(0f).sqrMagnitude <= num2 * num2;
			}
			else
			{
				flag2 = true;
			}
			if (flag2)
			{
				float sqrMagnitude = (scenePos - item.trans.position).sqrMagnitude;
				if (tank == null || sqrMagnitude < num)
				{
					tank = item;
					num = sqrMagnitude;
				}
			}
		}
		return tank;
	}

	public Tank In()
	{
		m_Tank = FindNearestVendorTo(Singleton.playerPos);
		return m_Tank;
	}
}
