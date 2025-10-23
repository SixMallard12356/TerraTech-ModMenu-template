#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TrackSpline : MonoBehaviour
{
	public enum TrackType
	{
		FreeformTrack,
		RaceTrack
	}

	[Serializable]
	public abstract class TrackNode
	{
		[Tooltip("Whether there is a checkpoint at this node")]
		[SerializeField]
		public bool m_IsCheckpointGate;

		public abstract Vector3 Position { get; set; }

		public abstract Vector3 GetOffset(CurveType curveType, Vector3 forward, Vector3 right);

		public abstract void SetOffset(CurveType curveType, Vector3 forward, Vector3 up, Vector3 right, Vector3 offset);

		public abstract float GetWidth();

		public abstract float GetHeight();

		public abstract float GetPenaltyWidth();
	}

	[Serializable]
	public class FreeformTrackNode : TrackNode
	{
		[Tooltip("Position of this node")]
		[SerializeField]
		public Vector3 m_Position;

		[Tooltip("Radius of the track at this node position")]
		[SerializeField]
		public float m_TrackRadius;

		[Tooltip("Radius of the penalty bounds at this node position")]
		[SerializeField]
		public float m_PenaltyRadius;

		public override Vector3 Position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public override Vector3 GetOffset(CurveType curveType, Vector3 forward, Vector3 right)
		{
			Vector2 vector;
			switch (curveType)
			{
			case CurveType.Reference:
			case CurveType.TrackCentre:
			case CurveType.PenaltyCentre:
				vector = Vector2.zero;
				break;
			case CurveType.TrackLeft:
				vector = Vector3.left * m_TrackRadius;
				break;
			case CurveType.TrackRight:
				vector = Vector3.right * m_TrackRadius;
				break;
			case CurveType.PenaltyLeft:
				vector = Vector3.left * m_PenaltyRadius;
				break;
			case CurveType.PenaltyRight:
				vector = Vector3.right * m_PenaltyRadius;
				break;
			default:
				d.Assert(condition: false, "Invalid curve type " + curveType);
				vector = Vector2.zero;
				break;
			}
			return vector.x * right + vector.y * forward;
		}

		public override void SetOffset(CurveType curveType, Vector3 forward, Vector3 up, Vector3 right, Vector3 offset)
		{
			float magnitude = ((Vector3)new Vector2(Vector3.Dot(up, offset), Vector3.Dot(right, offset))).magnitude;
			switch (curveType)
			{
			case CurveType.Reference:
				d.LogError("FreeformTrackNode.SetOffset - Curve type " + curveType.ToString() + " is not supported for this NodeType. To set the node position - set it directly.");
				break;
			case CurveType.TrackLeft:
			case CurveType.TrackRight:
				m_TrackRadius = magnitude;
				break;
			case CurveType.PenaltyLeft:
			case CurveType.PenaltyRight:
				m_PenaltyRadius = magnitude;
				break;
			case CurveType.TrackCentre:
			case CurveType.PenaltyCentre:
				d.LogError("FreeformTrackNode.SetOffset - Curve type " + curveType.ToString() + " is not supported for this NodeType.");
				break;
			default:
				d.Assert(condition: false, "Invalid curve type " + curveType);
				break;
			}
		}

		public override float GetWidth()
		{
			return m_TrackRadius * 2f;
		}

		public override float GetHeight()
		{
			return m_TrackRadius * 2f;
		}

		public override float GetPenaltyWidth()
		{
			return m_PenaltyRadius * 2f;
		}
	}

	[Serializable]
	public class RaceTrackNode : TrackNode
	{
		[Tooltip("Position of this node")]
		[SerializeField]
		public Vector2 m_Position;

		[SerializeField]
		[Tooltip("Offset of the left hand side of the track from the node position")]
		public Vector2 m_TrackLeftOffset;

		[SerializeField]
		[Tooltip("Offset of the right hand side of the track from the node position")]
		public Vector2 m_TrackRightOffset;

		[SerializeField]
		[Tooltip("Offset of the left penalty bounds from the node position")]
		public Vector2 m_PenaltyLeftOffset;

		[Tooltip("Offset of the right penalty bounds from the node position")]
		[SerializeField]
		public Vector2 m_PenaltyRightOffset;

		[SerializeField]
		[Tooltip("Height of gate (if any) at this node (0 for no override)")]
		public float m_HeightOverride;

		public override Vector3 Position
		{
			get
			{
				return m_Position.ToVector3XZ();
			}
			set
			{
				m_Position = value.ToVector2XZ();
			}
		}

		public override Vector3 GetOffset(CurveType curveType, Vector3 forward, Vector3 right)
		{
			Vector2 vector;
			switch (curveType)
			{
			case CurveType.Reference:
				vector = Vector2.zero;
				break;
			case CurveType.TrackLeft:
				vector = m_TrackLeftOffset;
				break;
			case CurveType.TrackRight:
				vector = m_TrackRightOffset;
				break;
			case CurveType.PenaltyLeft:
				vector = m_PenaltyLeftOffset;
				break;
			case CurveType.PenaltyRight:
				vector = m_PenaltyRightOffset;
				break;
			case CurveType.TrackCentre:
				vector = m_TrackLeftOffset + m_TrackRightOffset;
				break;
			case CurveType.PenaltyCentre:
				vector = m_PenaltyLeftOffset + m_PenaltyRightOffset;
				break;
			default:
				d.Assert(condition: false, "Invalid curve type " + curveType);
				vector = Vector2.zero;
				break;
			}
			return vector.x * right + vector.y * forward;
		}

		public override void SetOffset(CurveType curveType, Vector3 forward, Vector3 up, Vector3 right, Vector3 offset)
		{
			Vector2 vector = new Vector2(Vector3.Dot(right, offset), Vector3.Dot(forward, offset));
			switch (curveType)
			{
			case CurveType.Reference:
				d.LogError("RaceTrackNode.SetOffset - Curve type " + curveType.ToString() + " is not supported for this NodeType. To set the node position - set it directly.");
				break;
			case CurveType.TrackLeft:
				m_TrackLeftOffset = vector;
				break;
			case CurveType.TrackRight:
				m_TrackRightOffset = vector;
				break;
			case CurveType.PenaltyLeft:
				m_PenaltyLeftOffset = vector;
				break;
			case CurveType.PenaltyRight:
				m_PenaltyRightOffset = vector;
				break;
			case CurveType.TrackCentre:
			case CurveType.PenaltyCentre:
				d.LogError("RaceTrackNode.SetOffset - Curve type " + curveType.ToString() + " is not supported for this NodeType.");
				break;
			default:
				d.Assert(condition: false, "Invalid curve type " + curveType);
				break;
			}
		}

		public override float GetWidth()
		{
			return (m_TrackLeftOffset - m_TrackRightOffset).magnitude;
		}

		public override float GetHeight()
		{
			return m_HeightOverride;
		}

		public override float GetPenaltyWidth()
		{
			return (m_PenaltyLeftOffset - m_PenaltyRightOffset).magnitude;
		}
	}

	public enum CurveType
	{
		Reference,
		TrackLeft,
		TrackRight,
		PenaltyLeft,
		PenaltyRight,
		TrackCentre,
		PenaltyCentre
	}

	public struct Iterator
	{
		public struct PositionInfo
		{
			public Vector3 position;

			public int associatedNodeIdx;

			public float progressToNextNodeFrac;
		}

		private struct SplinePoint
		{
			public Vector3 position;

			public int associatedNodeIndex;

			public float progressToNext;

			public float distFromStart;
		}

		private SplinePoint[] m_SplinePoints;

		private float m_StepDistance;

		private float m_ProgressMadeFromStart;

		private int m_FurthestPassedPointIndex;

		private PositionInfo m_Current;

		public PositionInfo Current => m_Current;

		public Iterator GetEnumerator()
		{
			return this;
		}

		public Iterator(TrackSpline spline, float stepDistance = 0f, int resolutionBetweenPoints = 2)
		{
			Vector3[] array = spline.GenerateCurve(CurveType.TrackCentre, resolutionBetweenPoints);
			int num = array.Length;
			m_SplinePoints = new SplinePoint[num];
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = array[i];
				float num3 = 0f;
				if (i + 1 < num)
				{
					num3 = (array[i + 1] - vector).magnitude;
				}
				float num4 = Mathf.Min((float)i / (float)resolutionBetweenPoints, spline.NumNodes - 1);
				int num5 = Mathf.FloorToInt(num4);
				float progressToNext = num4 - (float)num5;
				m_SplinePoints[i] = new SplinePoint
				{
					position = vector,
					associatedNodeIndex = num5,
					progressToNext = progressToNext,
					distFromStart = num2
				};
				num2 += num3;
			}
			m_StepDistance = stepDistance;
			m_ProgressMadeFromStart = 0f;
			m_FurthestPassedPointIndex = -1;
			m_Current = default(PositionInfo);
		}

		public void Reset()
		{
			m_ProgressMadeFromStart = 0f;
			m_FurthestPassedPointIndex = -1;
			m_Current = default(PositionInfo);
		}

		public bool MoveNext()
		{
			d.Assert(m_StepDistance > 0f, "TrackPosIterator.MoveNext called with stepsize == 0. Cannot iterate without progressing.");
			return ManualMoveNext(m_StepDistance);
		}

		public bool ManualMoveNext(float stepSize)
		{
			if (m_FurthestPassedPointIndex + 1 >= m_SplinePoints.Length)
			{
				return false;
			}
			if (m_FurthestPassedPointIndex < 0)
			{
				m_FurthestPassedPointIndex = 0;
				m_Current = GetPointInfo(0);
				return true;
			}
			float num = stepSize;
			while (true)
			{
				SplinePoint splinePoint = m_SplinePoints[m_FurthestPassedPointIndex + 1];
				float distFromStart = splinePoint.distFromStart;
				float progressMadeFromStart = m_ProgressMadeFromStart;
				m_ProgressMadeFromStart = Mathf.MoveTowards(progressMadeFromStart, distFromStart, num);
				num -= m_ProgressMadeFromStart - progressMadeFromStart;
				if (num > 0.001f)
				{
					m_FurthestPassedPointIndex++;
					if (m_FurthestPassedPointIndex + 1 >= m_SplinePoints.Length)
					{
						m_Current = GetPointInfo(m_FurthestPassedPointIndex);
						break;
					}
					continue;
				}
				SplinePoint splinePoint2 = m_SplinePoints[m_FurthestPassedPointIndex];
				float num2 = m_ProgressMadeFromStart - splinePoint2.distFromStart;
				float num3 = splinePoint.distFromStart - splinePoint2.distFromStart;
				float t = num2 / num3;
				Vector3 position = Vector3.Lerp(splinePoint2.position, splinePoint.position, t);
				float progressToNextNodeFrac = Mathf.Lerp(splinePoint2.progressToNext, splinePoint.progressToNext, t);
				m_Current = new PositionInfo
				{
					position = position,
					associatedNodeIdx = splinePoint2.associatedNodeIndex,
					progressToNextNodeFrac = progressToNextNodeFrac
				};
				break;
			}
			return true;
		}

		private PositionInfo GetPointInfo(int splinePointIdx)
		{
			d.Assert(splinePointIdx >= 0 && splinePointIdx < m_SplinePoints.Length, "SplineIterator.GetPointInfo - Invalid point index " + splinePointIdx);
			SplinePoint splinePoint = m_SplinePoints[splinePointIdx];
			return new PositionInfo
			{
				position = splinePoint.position,
				associatedNodeIdx = splinePoint.associatedNodeIndex,
				progressToNextNodeFrac = 0f
			};
		}
	}

	public struct AreaIterator
	{
		public struct PositionInfo
		{
			public Vector3 position;

			public float width;

			public float penaltyWidth;

			public int associatedNodeIdx;

			public float progressToNextNodeFrac;
		}

		private TrackSpline m_Spline;

		private float m_PercentageOfLastWidthStepSize;

		private Iterator m_SplineIterator;

		private float m_LastWidth;

		private PositionInfo m_Current;

		public PositionInfo Current => m_Current;

		public AreaIterator GetEnumerator()
		{
			return this;
		}

		public AreaIterator(TrackSpline spline, float percentageOfLastWidthStepSize = 0.8f, int resolutionBetweenPoints = 10)
		{
			m_Spline = spline;
			m_PercentageOfLastWidthStepSize = percentageOfLastWidthStepSize;
			m_SplineIterator = new Iterator(m_Spline, 0f, resolutionBetweenPoints);
			m_LastWidth = 0f;
			m_Current = default(PositionInfo);
		}

		public void Reset()
		{
			m_SplineIterator.Reset();
			m_LastWidth = 0f;
			m_Current = default(PositionInfo);
		}

		public bool MoveNext()
		{
			float stepSize = m_LastWidth * m_PercentageOfLastWidthStepSize;
			bool num = m_SplineIterator.ManualMoveNext(stepSize);
			if (num)
			{
				Iterator.PositionInfo current = m_SplineIterator.Current;
				float trackWidthAtNodeIndex = m_Spline.GetTrackWidthAtNodeIndex(current.associatedNodeIdx);
				float trackWidthAtNodeIndex2 = m_Spline.GetTrackWidthAtNodeIndex(Mathf.Min(current.associatedNodeIdx + 1, m_Spline.NumNodes - 1));
				float width = (m_LastWidth = Mathf.Lerp(trackWidthAtNodeIndex, trackWidthAtNodeIndex2, current.progressToNextNodeFrac));
				float trackPenaltyWidthAtNodeIndex = m_Spline.GetTrackPenaltyWidthAtNodeIndex(current.associatedNodeIdx);
				float trackPenaltyWidthAtNodeIndex2 = m_Spline.GetTrackPenaltyWidthAtNodeIndex(Mathf.Min(current.associatedNodeIdx + 1, m_Spline.NumNodes - 1));
				float penaltyWidth = Mathf.Lerp(trackPenaltyWidthAtNodeIndex, trackPenaltyWidthAtNodeIndex2, current.progressToNextNodeFrac);
				m_Current = new PositionInfo
				{
					position = current.position,
					width = width,
					penaltyWidth = penaltyWidth,
					associatedNodeIdx = current.associatedNodeIdx,
					progressToNextNodeFrac = current.progressToNextNodeFrac
				};
			}
			return num;
		}
	}

	[SerializeField]
	private TrackType m_TrackNodeType = TrackType.RaceTrack;

	[SerializeField]
	private RaceTrackNode[] m_RaceTrackNodes;

	[SerializeField]
	private FreeformTrackNode[] m_FreeformTrackNodes;

	[SerializeField]
	private bool m_Loops;

	[SerializeField]
	[HideInInspector]
	private bool m_ShowBoundary;

	[SerializeField]
	private bool m_IgnoreYAxisOutOfBounds;

	private Transform _trans;

	public int NumNodes => Nodes.Length;

	public bool IsFreeform => m_TrackNodeType == TrackType.FreeformTrack;

	public bool IsShowingBoundary
	{
		get
		{
			if (m_TrackNodeType != TrackType.RaceTrack)
			{
				return m_ShowBoundary;
			}
			return true;
		}
	}

	public bool IsIgnoringYAxisOutOfBounds => m_IgnoreYAxisOutOfBounds;

	private TrackNode[] Nodes
	{
		get
		{
			if (!IsFreeform)
			{
				return m_RaceTrackNodes;
			}
			return m_FreeformTrackNodes;
		}
	}

	private Transform trans
	{
		get
		{
			if (_trans == null)
			{
				_trans = base.transform;
			}
			return _trans;
		}
	}

	public Vector3[] GenerateCurve(CurveType type, int resolutionBetweenPoints)
	{
		Vector3[] array = GenerateRawControlPoints();
		Vector3[] array2;
		if (type != CurveType.Reference)
		{
			array2 = new Vector3[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				Vector3 vector = CatmullRom.CalcCurveTangentSlow(array, m_Loops, i, 0f);
				Vector3 right = Vector3.Cross(Vector3.up, vector);
				int num = Mathf.Min(i, Nodes.Length - 1);
				Vector3 offset = Nodes[num].GetOffset(type, vector, right);
				array2[i] = array[i] + offset;
			}
		}
		else
		{
			array2 = array;
		}
		return CatmullRom.GenerateCurve(array2, m_Loops, resolutionBetweenPoints);
	}

	public void CalcNodeValuesSlow(CurveType type, int index, out Vector3 position, out Vector3 forward, out Vector3 up)
	{
		if (index < Nodes.Length)
		{
			position = trans.TransformPoint(Nodes[index].Position);
			Vector3[] controlPoints = GenerateRawControlPoints();
			forward = CatmullRom.CalcCurveTangentSlow(controlPoints, m_Loops, index, 0f);
			if (type != CurveType.Reference)
			{
				Vector3 right = Vector3.Cross(Vector3.up, forward);
				Vector3 offset = Nodes[index].GetOffset(type, forward, right);
				position += offset;
			}
		}
		else
		{
			d.Assert(condition: false, "Invalid node index " + index);
			position = Vector3.zero;
			forward = Vector3.zero;
		}
		up = Vector3.up;
	}

	public Vector3 CalcNodePosSlow(CurveType type, int index)
	{
		Vector3 result;
		if (index < Nodes.Length)
		{
			result = trans.TransformPoint(Nodes[index].Position);
			if (type != CurveType.Reference)
			{
				Vector3 vector = CatmullRom.CalcCurveTangentSlow(GenerateRawControlPoints(), m_Loops, index, 0f);
				Vector3 right = Vector3.Cross(Vector3.up, vector);
				Vector3 offset = Nodes[index].GetOffset(type, vector, right);
				result += offset;
			}
		}
		else
		{
			d.Assert(condition: false, "Invalid node index " + index);
			result = Vector3.zero;
		}
		return result;
	}

	public void SetNodePosSlow(CurveType type, int index, Vector3 pos)
	{
		if (index < Nodes.Length)
		{
			if (type == CurveType.Reference)
			{
				Nodes[index].Position = trans.InverseTransformPoint(pos);
				return;
			}
			Vector3 vector = CatmullRom.CalcCurveTangentSlow(GenerateRawControlPoints(), m_Loops, index, 0f);
			Vector3 up = Vector3.up;
			Vector3 right = Vector3.Cross(up, vector);
			Vector3 offset = pos - trans.TransformPoint(Nodes[index].Position);
			Nodes[index].SetOffset(type, vector, up, right, offset);
		}
		else
		{
			d.Assert(condition: false, "Invalid node index " + index);
		}
	}

	public Vector3 CalcNodeTangentSlow(int index)
	{
		return CatmullRom.CalcCurveTangentSlow(GenerateRawControlPoints(), m_Loops, index, 0f);
	}

	public bool IsGateAtNodeIndex(int index)
	{
		if (index < Nodes.Length)
		{
			return Nodes[index].m_IsCheckpointGate;
		}
		d.Assert(condition: false, "Invalid node index " + index);
		return false;
	}

	public float GetTrackWidthAtNodeIndex(int index)
	{
		if (index < Nodes.Length)
		{
			return Nodes[index].GetWidth();
		}
		d.Assert(condition: false, "Invalid node index " + index);
		return 0f;
	}

	public float GetHeightAtNodeIndex(int index)
	{
		if (index < Nodes.Length)
		{
			return Nodes[index].GetHeight();
		}
		d.Assert(condition: false, "Invalid node index " + index);
		return 0f;
	}

	public float GetTrackPenaltyWidthAtNodeIndex(int index)
	{
		if (index < Nodes.Length)
		{
			return Nodes[index].GetPenaltyWidth();
		}
		d.Assert(condition: false, "Invalid node index " + index);
		return 0f;
	}

	public Vector3 GetNodePos(int nodeIndex)
	{
		if (nodeIndex < Nodes.Length)
		{
			return Nodes[nodeIndex].Position;
		}
		d.Assert(condition: false, "Invalid node index " + nodeIndex);
		return Vector3.zero;
	}

	private Vector3[] GenerateRawControlPoints()
	{
		Vector3[] array;
		if (Nodes != null)
		{
			int num = Nodes.Length;
			int num2 = Mathf.Max(num, 4);
			array = new Vector3[num2];
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			for (int i = 0; i < num2; i++)
			{
				Vector3 vector3 = ((i >= num) ? (vector + vector2.normalized * 0.1f) : Nodes[i].Position);
				vector2 = vector3 - vector;
				vector = vector3;
				vector3 = trans.TransformPoint(vector3);
				array[i] = vector3;
			}
		}
		else
		{
			array = new Vector3[0];
		}
		return array;
	}
}
