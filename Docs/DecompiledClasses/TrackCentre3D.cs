#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

internal class TrackCentre3D
{
	public struct Params
	{
		public Vector3[] rawPositions;

		public WorldPosition trackPosition;

		public Quaternion trackRotation;

		public float lineSimplificationAllowedError;

		public bool projectToGround;
	}

	private struct ColData
	{
		public Vector3 startPos;

		public Vector3 endPos;

		public Vector3 tang;

		public Vector3 norm;

		public float len;

		public float distFromStart;
	}

	private Params m_Params;

	private Vector3[] m_RawPointsLocal;

	private volatile ColData[] m_ColData;

	private Dictionary<IntVector2, List<int>> m_TileToIndMap;

	public TrackCentre3D(Params aParams)
	{
		m_Params = aParams;
		Vector3[] rawPositions = m_Params.rawPositions;
		Vector3 scenePosition = m_Params.trackPosition.ScenePosition;
		m_RawPointsLocal = new Vector3[rawPositions.Length];
		for (int i = 0; i < rawPositions.Length; i++)
		{
			Vector3 vector = m_Params.trackRotation * rawPositions[i];
			if (m_Params.projectToGround)
			{
				Vector3 posScene = scenePosition + vector;
				vector.y = Singleton.Manager<ManWorld>.inst.ProjectToGround(posScene).y;
				if (m_TileToIndMap == null)
				{
					m_TileToIndMap = new Dictionary<IntVector2, List<int>>();
				}
				IntVector2 key = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in posScene);
				if (m_TileToIndMap.TryGetValue(key, out var value))
				{
					value.Add(i);
				}
				else
				{
					value = new List<int>(1) { i };
					m_TileToIndMap.Add(key, value);
				}
			}
			m_RawPointsLocal[i] = vector;
		}
		GenerateMeshDef();
	}

	public void UpdateTile(IntVector2 tileVec)
	{
		if (m_Params.projectToGround && m_TileToIndMap.TryGetValue(tileVec, out var value))
		{
			Vector3 scenePosition = m_Params.trackPosition.ScenePosition;
			for (int i = 0; i < value.Count; i++)
			{
				int num = value[i];
				Vector3 vector = m_RawPointsLocal[num];
				Vector3 scenePos = scenePosition + vector;
				vector.y = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos).y;
				m_RawPointsLocal[num] = vector;
			}
		}
	}

	public void GenerateMeshDef()
	{
		Vector3[] points = TrackEdgeSide.SimplifyLine(m_RawPointsLocal, m_Params.lineSimplificationAllowedError);
		m_ColData = CreateCollisionData(points);
	}

	private static float SimplifyLineCalcErrorSq(Vector3 lineStart, Vector3 lineEnd, Vector3 lineTangent, Vector3 lineNormal, float lineLen, Vector3 outlyingPoint)
	{
		Vector3 rhs = outlyingPoint - lineStart;
		float num = Vector3.Dot(lineTangent, rhs);
		if (num < 0f)
		{
			return rhs.sqrMagnitude;
		}
		if (num > lineLen)
		{
			return (outlyingPoint - lineEnd).sqrMagnitude;
		}
		Vector3 vector = lineStart + num * lineTangent;
		return (outlyingPoint - vector).sqrMagnitude;
	}

	private ColData[] CreateCollisionData(Vector3[] points)
	{
		ColData[] array;
		if (points != null && points.Length > 1)
		{
			float num = 0f;
			array = new ColData[points.Length - 1];
			for (int i = 0; i < array.Length; i++)
			{
				Vector3 vector = points[i];
				Vector3 vector2 = points[i + 1];
				Vector3 vector3 = vector2 - vector;
				float magnitude = vector3.magnitude;
				Vector3 vector4 = ((magnitude > Mathf.Epsilon) ? (vector3 / magnitude) : Vector3.forward);
				array[i] = new ColData
				{
					startPos = vector,
					endPos = vector2,
					len = magnitude,
					tang = vector4,
					norm = Vector3.Cross(Vector3.up, vector4),
					distFromStart = num
				};
				num += magnitude;
			}
		}
		else
		{
			array = null;
		}
		return array;
	}

	public TrackEdgeSide.ClosestPoint FindClosestPointTo(Vector3 scenePos)
	{
		TrackEdgeSide.ClosestPoint result = default(TrackEdgeSide.ClosestPoint);
		Vector3 scenePosition = m_Params.trackPosition.ScenePosition;
		Vector3 vector = scenePos - scenePosition;
		ColData[] colData = m_ColData;
		if (colData != null && colData.Length != 0)
		{
			float num = float.MaxValue;
			int num2 = 0;
			_ = Vector3.zero;
			float num3 = 0f;
			Vector3 vector2 = vector - colData[0].startPos;
			float sqrMagnitude = vector2.sqrMagnitude;
			for (int i = 0; i < colData.Length; i++)
			{
				Vector3 lhs = vector2;
				num3 = sqrMagnitude;
				vector2 = vector - colData[i].endPos;
				sqrMagnitude = vector2.sqrMagnitude;
				float num4 = Vector3.Dot(lhs, colData[i].tang);
				float num5;
				if (num4 < 0f)
				{
					num5 = num3;
				}
				else if (num4 > colData[i].len)
				{
					num5 = sqrMagnitude;
				}
				else
				{
					Vector3 vector3 = colData[i].startPos + colData[i].tang * num4;
					num5 = (vector - vector3).sqrMagnitude;
				}
				if (num5 < num)
				{
					num = num5;
					num2 = i;
				}
			}
			float num6 = Mathf.Clamp(Vector3.Dot(vector - colData[num2].startPos, colData[num2].tang), 0f, colData[num2].len);
			result.closestPos = scenePosition + colData[num2].startPos + colData[num2].tang * num6;
			result.tangent = colData[num2].tang;
			result.distanceFromStart = colData[num2].distFromStart + num6;
			result.distanceOutsideTrack = Mathf.Sqrt(num);
		}
		else
		{
			d.Assert(condition: false, "No collision data");
			result.closestPos = Vector3.zero;
			result.distanceFromStart = 0f;
			result.distanceOutsideTrack = 0f;
			result.tangent = Vector3.forward;
		}
		return result;
	}
}
