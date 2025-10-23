using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class UIMiniMapLayerPlayer : UIMiniMapLayer
{
	public override void UpdateLayer()
	{
		Transform transform = ((Singleton.playerTank != null) ? Singleton.playerTank.rootBlockTrans : Singleton.cameraTrans);
		if (m_MapDisplay.AllowsPanning)
		{
			Vector2 vector = m_MapDisplay.SceneToMap(transform.position);
			m_RectTrans.anchoredPosition = vector * m_MapDisplay.CurrentZoomLevel;
		}
		float z = Vector3.SignedAngle(transform.forward.SetY(0f), Vector3.forward, Vector3.up);
		m_RectTrans.rotation = m_MapDisplay.MapRotation * Quaternion.Euler(0f, 0f, z);
	}
}
