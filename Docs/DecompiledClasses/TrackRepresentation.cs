using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TrackRepresentation
{
	private enum TrackEdgeRegenState
	{
		Idle,
		InProgress,
		Complete
	}

	public struct RelativeTrackInfo
	{
		public Vector3 closestPointOnTrack;

		public Vector3 trackTangent;

		public float distanceFromStart;

		public float distanceOutsideWarningArea;

		public float distanceOutsidePenaltyArea;
	}

	private CheckpointChallenge.Track m_Track;

	private TrackEdge m_TrackEdge;

	private TrackEdge m_PenaltyEdge;

	private TrackCentre3D m_ReferenceSpline;

	private ThreadWorker m_ThreadWorker;

	private volatile TrackEdgeRegenState m_TrackEdgeRegenState;

	private List<IntVector2> m_DirtyTrackEdgeTiles;

	private float[] m_NodeDistancesFromStart;

	public void Setup(CheckpointChallenge.Track track, Challenge.PlacementInfo placementInfo)
	{
		m_Track = track;
		if (m_Track.spline.IsShowingBoundary)
		{
			TrackEdgeSide.Params aParams = new TrackEdgeSide.Params
			{
				trackPosition = placementInfo.spawnPosition,
				trackRotation = placementInfo.spawnRotation,
				trackEdgeWidth = m_Track.trackEdgeWidth,
				trackEdgeHeightOffGround = m_Track.trackEdgeHeight,
				lineSimplificationAllowedError = m_Track.edgeMaxRenderError
			};
			if (m_Track.trackEdgePrefab != null)
			{
				aParams.rawPositionsLeft = m_Track.spline.GenerateCurve(TrackSpline.CurveType.TrackLeft, m_Track.splineResolution);
				aParams.rawPositionsRight = m_Track.spline.GenerateCurve(TrackSpline.CurveType.TrackRight, m_Track.splineResolution);
				aParams.prefab = m_Track.trackEdgePrefab;
				m_TrackEdge = new TrackEdge(aParams);
			}
			if (m_Track.boundsEdgePrefab != null)
			{
				aParams.prefab = m_Track.boundsEdgePrefab;
				aParams.rawPositionsLeft = m_Track.spline.GenerateCurve(TrackSpline.CurveType.PenaltyLeft, m_Track.splineResolution);
				aParams.rawPositionsRight = m_Track.spline.GenerateCurve(TrackSpline.CurveType.PenaltyRight, m_Track.splineResolution);
				m_PenaltyEdge = new TrackEdge(aParams);
			}
			if (m_TrackEdge != null)
			{
				m_TrackEdge.GenerateMeshDefs();
				m_TrackEdge.SpawnGameObjects();
			}
			if (m_PenaltyEdge != null)
			{
				m_PenaltyEdge.GenerateMeshDefs();
				m_PenaltyEdge.SpawnGameObjects();
			}
			if (m_TrackEdge != null)
			{
				m_TrackEdge.SetVisible(visible: true);
			}
			if (m_PenaltyEdge != null)
			{
				m_PenaltyEdge.SetVisible(visible: false);
			}
			m_TrackEdgeRegenState = TrackEdgeRegenState.Idle;
			m_DirtyTrackEdgeTiles = new List<IntVector2>();
			m_ThreadWorker = new ThreadWorker();
			Thread thread = new Thread(m_ThreadWorker.DoWork);
			thread.Name = "TrackRepresentation";
			thread.Start();
		}
		if (m_Track.spline.IsFreeform)
		{
			m_ReferenceSpline = new TrackCentre3D(new TrackCentre3D.Params
			{
				trackPosition = placementInfo.spawnPosition,
				trackRotation = placementInfo.spawnRotation,
				rawPositions = m_Track.spline.GenerateCurve(TrackSpline.CurveType.TrackCentre, m_Track.splineResolution),
				lineSimplificationAllowedError = m_Track.edgeMaxRenderError,
				projectToGround = false
			});
			int numNodes = m_Track.spline.NumNodes;
			m_NodeDistancesFromStart = new float[numNodes];
			float num = 0f;
			m_NodeDistancesFromStart[0] = 0f;
			for (int i = 1; i < numNodes; i++)
			{
				float magnitude = (m_Track.spline.GetNodePos(i) - m_Track.spline.GetNodePos(i - 1)).magnitude;
				num += magnitude;
				m_NodeDistancesFromStart[i] = num;
			}
		}
		Singleton.Manager<ManWorld>.inst.TileManager.TilePopulatedEvent.Subscribe(OnTilePopulated);
	}

	public void TearDown()
	{
		if (m_ThreadWorker != null)
		{
			m_ThreadWorker.RequestStop();
			m_ThreadWorker.CancelAllActions();
			m_ThreadWorker = null;
		}
		Singleton.Manager<ManWorld>.inst.TileManager.TilePopulatedEvent.Unsubscribe(OnTilePopulated);
		if (m_TrackEdge != null)
		{
			m_TrackEdge.RecycleGameObjects();
			m_TrackEdge = null;
		}
		if (m_PenaltyEdge != null)
		{
			m_PenaltyEdge.RecycleGameObjects();
			m_PenaltyEdge = null;
		}
		if (m_ReferenceSpline != null)
		{
			m_ReferenceSpline = null;
		}
	}

	public void Update()
	{
		if (!m_Track.spline.IsShowingBoundary)
		{
			return;
		}
		if (m_TrackEdgeRegenState == TrackEdgeRegenState.Idle && m_DirtyTrackEdgeTiles.Count > 0 && !Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
		{
			for (int i = 0; i < m_DirtyTrackEdgeTiles.Count; i++)
			{
				m_TrackEdge.UpdateTile(m_DirtyTrackEdgeTiles[i]);
				m_PenaltyEdge.UpdateTile(m_DirtyTrackEdgeTiles[i]);
			}
			m_DirtyTrackEdgeTiles.Clear();
			m_ThreadWorker.AddAction(GenerateTrackEdgeDefsAction(), null);
			m_TrackEdgeRegenState = TrackEdgeRegenState.InProgress;
		}
		if (m_TrackEdgeRegenState == TrackEdgeRegenState.Complete)
		{
			m_TrackEdge.SpawnGameObjects();
			m_PenaltyEdge.SpawnGameObjects();
			m_TrackEdgeRegenState = TrackEdgeRegenState.Idle;
		}
	}

	public void ShowTrackBoundary()
	{
		if (m_PenaltyEdge != null)
		{
			m_PenaltyEdge.SetVisible(visible: true);
		}
	}

	public float GetDistanceFromStart(Vector3 position)
	{
		TrackEdgeSide.ClosestPoint obj = ((m_ReferenceSpline != null) ? m_ReferenceSpline.FindClosestPointTo(position) : m_TrackEdge.FindClosestPointTo(position));
		return obj.distanceFromStart;
	}

	public RelativeTrackInfo GetRelativeTrackInformation(Vector3 position)
	{
		TrackEdgeSide.ClosestPoint closestPoint = ((m_ReferenceSpline != null) ? m_ReferenceSpline.FindClosestPointTo(position) : m_TrackEdge.FindClosestPointTo(position));
		float distanceOutsideWarningArea;
		float distanceOutsidePenaltyArea;
		if (m_Track.spline.IsFreeform && !m_Track.spline.IsIgnoringYAxisOutOfBounds)
		{
			int num = -1;
			int num2 = m_NodeDistancesFromStart.Length;
			float num3 = closestPoint.distanceFromStart;
			for (int i = 0; i < num2; i++)
			{
				num3 -= m_NodeDistancesFromStart[i];
				if (num3 < 0f)
				{
					num = i;
					break;
				}
			}
			int num4 = Mathf.Clamp(num - 1, 0, num2 - 1);
			num = Mathf.Clamp(num, 0, num2 - 1);
			float num5 = closestPoint.distanceFromStart - m_NodeDistancesFromStart[num4];
			float num6 = m_NodeDistancesFromStart[num] - m_NodeDistancesFromStart[num4];
			float t = num5 / num6;
			float a = m_Track.spline.GetTrackWidthAtNodeIndex(num4) * 0.5f;
			float b = m_Track.spline.GetTrackWidthAtNodeIndex(num) * 0.5f;
			float num7 = Mathf.Lerp(a, b, t);
			float a2 = m_Track.spline.GetTrackPenaltyWidthAtNodeIndex(num4) * 0.5f;
			float b2 = m_Track.spline.GetTrackPenaltyWidthAtNodeIndex(num) * 0.5f;
			float num8 = Mathf.Lerp(a2, b2, t);
			float distanceOutsideTrack = closestPoint.distanceOutsideTrack;
			distanceOutsideWarningArea = distanceOutsideTrack - num7;
			distanceOutsidePenaltyArea = distanceOutsideTrack - num8;
		}
		else
		{
			distanceOutsideWarningArea = closestPoint.distanceOutsideTrack;
			distanceOutsidePenaltyArea = ((closestPoint.distanceOutsideTrack > 0f && m_PenaltyEdge != null) ? m_PenaltyEdge.FindClosestPointTo(position).distanceOutsideTrack : 0f);
		}
		return new RelativeTrackInfo
		{
			closestPointOnTrack = closestPoint.closestPos,
			trackTangent = closestPoint.tangent,
			distanceFromStart = closestPoint.distanceFromStart,
			distanceOutsideWarningArea = distanceOutsideWarningArea,
			distanceOutsidePenaltyArea = distanceOutsidePenaltyArea
		};
	}

	private IEnumerator GenerateTrackEdgeDefsAction()
	{
		m_TrackEdge.GenerateMeshDefs();
		m_PenaltyEdge.GenerateMeshDefs();
		m_TrackEdgeRegenState = TrackEdgeRegenState.Complete;
		yield break;
	}

	private void OnTilePopulated(WorldTile tile)
	{
		if (m_DirtyTrackEdgeTiles != null && !m_DirtyTrackEdgeTiles.Contains(tile.Coord))
		{
			m_DirtyTrackEdgeTiles.Add(tile.Coord);
		}
	}
}
