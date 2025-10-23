#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

internal class TrackEdgeSide
{
	public struct Params
	{
		public Vector3[] rawPositionsLeft;

		public Vector3[] rawPositionsRight;

		public float trackEdgeHeightOffGround;

		public WorldPosition trackPosition;

		public Quaternion trackRotation;

		public float lineSimplificationAllowedError;

		public float trackEdgeWidth;

		public Transform prefab;
	}

	public struct ClosestPoint
	{
		public Vector3 closestPos;

		public Vector3 tangent;

		public float distanceOutsideTrack;

		public float distanceFromStart;
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

	private TubeMeshGenerator.MeshDefinition m_MeshDef;

	private Transform m_SpawnedWorldObject;

	private Dictionary<IntVector2, List<int>> m_TileToIndMap = new Dictionary<IntVector2, List<int>>();

	private bool m_RightNotLeft;

	public TrackEdgeSide(Params aParams, bool rightNotLeft = false)
	{
		m_Params = aParams;
		m_RightNotLeft = rightNotLeft;
		Vector3[] array = (rightNotLeft ? m_Params.rawPositionsRight : m_Params.rawPositionsLeft);
		Vector3 vector = m_Params.trackPosition.ScenePosition.SetY(0f);
		m_RawPointsLocal = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector2 = m_Params.trackRotation * array[i];
			Vector3 posScene = vector + vector2;
			vector2.y = Singleton.Manager<ManWorld>.inst.ProjectToGround(posScene).y + m_Params.trackEdgeHeightOffGround;
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
			m_RawPointsLocal[i] = vector2;
		}
	}

	public void UpdateTile(IntVector2 tileVec)
	{
		if (m_TileToIndMap.TryGetValue(tileVec, out var value))
		{
			Vector3 scenePosition = m_Params.trackPosition.ScenePosition;
			for (int i = 0; i < value.Count; i++)
			{
				int num = value[i];
				Vector3 vector = m_RawPointsLocal[num];
				Vector3 scenePos = scenePosition + vector;
				vector.y = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos).y + m_Params.trackEdgeHeightOffGround;
				m_RawPointsLocal[num] = vector;
			}
		}
	}

	public void GenerateMeshDef()
	{
		m_MeshDef = null;
		Vector3[] points = SimplifyLine(m_RawPointsLocal, m_Params.lineSimplificationAllowedError);
		m_MeshDef = TubeMeshGenerator.GenerateMeshDef(points, m_Params.trackEdgeWidth, 6);
		m_ColData = CreateCollisionData(points);
	}

	public void SpawnGameObject()
	{
		if (m_SpawnedWorldObject == null)
		{
			if (m_Params.prefab != null)
			{
				m_SpawnedWorldObject = m_Params.prefab.Spawn(m_Params.trackPosition.ScenePosition.SetY(0f));
			}
			else
			{
				d.LogWarning("SpawnGameObject: Trying to create track edge line, but prefab is null");
			}
		}
		if ((bool)m_SpawnedWorldObject)
		{
			MeshFilter component = m_SpawnedWorldObject.gameObject.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.mesh = m_MeshDef.GenerateMesh();
			}
			else
			{
				d.LogError("Track edge prefab \"" + m_Params.prefab.name + "\" needs to have a MeshFilter added to it, or it won't be able to render a track edge!");
			}
		}
	}

	public void RecycleGameObject()
	{
		if ((bool)m_SpawnedWorldObject)
		{
			MeshFilter component = m_SpawnedWorldObject.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.mesh = null;
			}
			m_SpawnedWorldObject.Recycle();
			m_SpawnedWorldObject = null;
		}
	}

	public void SetVisible(bool visible)
	{
		if ((bool)m_SpawnedWorldObject)
		{
			m_SpawnedWorldObject.gameObject.SetActive(visible);
		}
	}

	public static Vector3[] SimplifyLine(Vector3[] points, float maxAllowedError)
	{
		float num = maxAllowedError * maxAllowedError;
		List<int> list = new List<int>(points.Length);
		for (int i = 0; i < points.Length; i++)
		{
			list.Add(i);
		}
		bool flag = true;
		while (flag)
		{
			flag = false;
			int num2 = 0;
			while (num2 < list.Count - 2)
			{
				int num3 = list[num2];
				int num4 = list[num2 + 1];
				int num5 = list[num2 + 2];
				Vector3 vector = points[num3];
				Vector3 outlyingPoint = points[num4];
				Vector3 vector2 = points[num5];
				Vector3 vector3 = vector2 - vector;
				float magnitude = vector3.magnitude;
				Vector3 vector4 = ((magnitude > 0f) ? (vector3 / magnitude) : Vector3.forward);
				Vector3 lineNormal = Maths.CalcNormalFromTangent(vector4, Vector3.up, Vector3.forward);
				float num6 = SimplifyLineCalcErrorSq(vector, vector2, vector4, lineNormal, magnitude, outlyingPoint);
				bool flag2 = false;
				if (num6 <= num)
				{
					bool flag3 = true;
					for (int j = num3 + 1; j < num5; j++)
					{
						if (j != num4 && SimplifyLineCalcErrorSq(vector, vector2, vector4, lineNormal, magnitude, points[j]) > maxAllowedError)
						{
							flag3 = false;
							break;
						}
					}
					flag2 = flag3;
				}
				if (flag2)
				{
					list.RemoveAt(num2 + 1);
					flag = true;
				}
				else
				{
					num2++;
				}
			}
		}
		Vector3[] array = new Vector3[list.Count];
		for (int k = 0; k < array.Length; k++)
		{
			array[k] = points[list[k]];
		}
		return array;
	}

	public static float SimplifyLineCalcErrorSq(Vector3 lineStart, Vector3 lineEnd, Vector3 lineTangent, Vector3 lineNormal, float lineLen, Vector3 outlyingPoint)
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
				Vector3 vector = points[i].SetY(0f);
				Vector3 vector2 = points[i + 1].SetY(0f);
				Vector3 vector3 = vector2 - vector;
				float magnitude = vector3.magnitude;
				Vector3 vector4 = ((magnitude > Mathf.Epsilon) ? (vector3 / magnitude) : Vector3.forward);
				array[i] = new ColData
				{
					startPos = vector,
					endPos = vector2,
					len = magnitude,
					tang = vector4,
					norm = Vector3.Cross(m_RightNotLeft ? Vector3.up : Vector3.down, vector4),
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

	public ClosestPoint FindClosestPointTo(Vector3 scenePos)
	{
		ClosestPoint result = default(ClosestPoint);
		Vector3 vector = m_Params.trackPosition.ScenePosition.SetY(0f);
		Vector3 vector2 = scenePos.SetY(0f) - vector;
		ColData[] colData = m_ColData;
		if (colData != null && colData.Length != 0)
		{
			float num = float.MaxValue;
			int num2 = 0;
			Vector3 zero = Vector3.zero;
			float num3 = 0f;
			Vector3 vector3 = vector2 - colData[0].startPos;
			float sqrMagnitude = vector3.sqrMagnitude;
			for (int i = 0; i < colData.Length; i++)
			{
				zero = vector3;
				num3 = sqrMagnitude;
				vector3 = vector2 - colData[i].endPos;
				sqrMagnitude = vector3.sqrMagnitude;
				float num4 = Vector3.Dot(zero, colData[i].tang);
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
					float num6 = Vector3.Dot(zero, colData[i].norm);
					num5 = num6 * num6;
				}
				if (num5 < num)
				{
					num = num5;
					num2 = i;
				}
			}
			zero = vector2 - colData[num2].startPos;
			float num7 = Mathf.Clamp(Vector3.Dot(zero, colData[num2].tang), 0f, colData[num2].len);
			result.closestPos = vector + colData[num2].startPos + colData[num2].tang * num7;
			result.tangent = colData[num2].tang;
			result.distanceFromStart = colData[num2].distFromStart + num7;
			bool flag = Vector3.Dot(zero, colData[num2].norm) >= 0f;
			float distanceOutsideTrack = Mathf.Sqrt(num) * (flag ? 1f : (-1f));
			result.distanceOutsideTrack = distanceOutsideTrack;
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
