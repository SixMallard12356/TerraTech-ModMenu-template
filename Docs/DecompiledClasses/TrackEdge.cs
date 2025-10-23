using UnityEngine;

internal class TrackEdge
{
	private TrackEdgeSide m_Left;

	private TrackEdgeSide m_Right;

	public TrackEdge(TrackEdgeSide.Params aParams)
	{
		m_Left = new TrackEdgeSide(aParams);
		m_Right = new TrackEdgeSide(aParams, rightNotLeft: true);
	}

	public void UpdateTile(IntVector2 tileVec)
	{
		m_Left.UpdateTile(tileVec);
		m_Right.UpdateTile(tileVec);
	}

	public void GenerateMeshDefs()
	{
		m_Left.GenerateMeshDef();
		m_Right.GenerateMeshDef();
	}

	public void SpawnGameObjects()
	{
		m_Left.SpawnGameObject();
		m_Right.SpawnGameObject();
	}

	public void RecycleGameObjects()
	{
		m_Left.RecycleGameObject();
		m_Right.RecycleGameObject();
	}

	public void SetVisible(bool visible)
	{
		m_Left.SetVisible(visible);
		m_Right.SetVisible(visible);
	}

	public TrackEdgeSide.ClosestPoint FindClosestPointTo(Vector3 scenePos)
	{
		TrackEdgeSide.ClosestPoint closestPoint = m_Left.FindClosestPointTo(scenePos);
		TrackEdgeSide.ClosestPoint closestPoint2 = m_Right.FindClosestPointTo(scenePos);
		bool flag = closestPoint.distanceOutsideTrack > closestPoint2.distanceOutsideTrack;
		return new TrackEdgeSide.ClosestPoint
		{
			closestPos = (flag ? closestPoint.closestPos : closestPoint2.closestPos),
			distanceOutsideTrack = (flag ? closestPoint.distanceOutsideTrack : closestPoint2.distanceOutsideTrack),
			tangent = (closestPoint.tangent + closestPoint2.tangent).normalized,
			distanceFromStart = (closestPoint.distanceFromStart + closestPoint2.distanceFromStart) / 2f
		};
	}
}
