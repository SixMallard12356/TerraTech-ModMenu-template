using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
	private struct EdgeMarker
	{
		public Transform m_Transform;

		public Renderer m_Renderer;
	}

	[SerializeField]
	private GameObject m_CornerMarkerPrefab;

	[SerializeField]
	private GameObject m_EdgeMarkerPrefab;

	[SerializeField]
	private GameObject m_CubeMesh;

	[SerializeField]
	private Material[] m_Materials;

	private Visible m_HighlightedObject;

	private Renderer m_Renderer;

	private Transform m_Transform;

	private Transform m_CubeMeshTrans;

	private Transform[] m_CornerMarkers = new Transform[8];

	private List<EdgeMarker> m_UsedEdgeMarkers = new List<EdgeMarker>();

	private List<EdgeMarker> m_FreeEdgeMarkers = new List<EdgeMarker>();

	private Material[] m_MaterialInstances;

	private static Quaternion[] m_ZEdgeRotations = new Quaternion[4]
	{
		Quaternion.Euler(0f, 270f, 0f),
		Quaternion.Euler(270f, 270f, 0f),
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(270f, 90f, 0f)
	};

	private static Quaternion[] m_XEdgeRotations = new Quaternion[4]
	{
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 180f, 180f),
		Quaternion.Euler(0f, 0f, 0f),
		Quaternion.Euler(270f, 0f, 0f)
	};

	private static Quaternion[] m_YEdgeRotations = new Quaternion[4]
	{
		Quaternion.Euler(0f, 180f, 90f),
		Quaternion.Euler(0f, 180f, 270f),
		Quaternion.Euler(0f, 0f, 270f),
		Quaternion.Euler(0f, 90f, 270f)
	};

	public Vector3 HighlightScale => m_CubeMeshTrans.lossyScale;

	public void Highlight(Visible visible, ManPointer.HighlightVariation type)
	{
		SetHighlightType(type);
		Highlight(visible);
	}

	public void Highlight(Visible visible)
	{
		if ((bool)m_HighlightedObject)
		{
			if (m_HighlightedObject == visible)
			{
				return;
			}
			HideHighlight();
		}
		m_HighlightedObject = visible;
		m_HighlightedObject.RecycledEvent.Subscribe(OnHighlightedObjectRecycled);
		m_Transform.SetParent(visible.trans, worldPositionStays: false);
		m_Transform.SetPositionAndRotationIfChanged(visible.centrePosition, visible.trans.rotation);
		Vector3 vector = Vector3.one;
		Vector3 vector2 = Vector3.zero;
		if ((bool)visible.block)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			IntVector3[] filledCells = visible.block.filledCells;
			for (int i = 0; i < filledCells.Length; i++)
			{
				IntVector3 intVector = filledCells[i];
				zero.x = Mathf.Max(zero.x, intVector.x);
				zero.y = Mathf.Max(zero.y, intVector.y);
				zero.z = Mathf.Max(zero.z, intVector.z);
				zero2.x = Mathf.Min(zero2.x, intVector.x);
				zero2.y = Mathf.Min(zero2.y, intVector.y);
				zero2.z = Mathf.Min(zero2.z, intVector.z);
			}
			vector = zero - zero2;
			vector2 = vector + Vector3.one;
			vector += new Vector3(1.1f, 1.1f, 1.1f);
			m_Transform.SetLocalPositionIfChanged(visible.block.BlockCellBounds.center);
		}
		m_CubeMeshTrans.SetLocalScaleIfChanged(vector);
		float num = 0f;
		Vector3 vector3 = vector * 0.5f;
		for (int j = 0; j < 8; j++)
		{
			float z = ((j > 3) ? 0f : 270f);
			m_CornerMarkers[j].SetLocalEulersIfChanged(new Vector3(0f, num, z));
			num += 90f;
			float x = ((j == 0 || j == 3 || j == 4 || j == 7) ? vector3.x : (0f - vector3.x));
			float y = ((j > 3) ? vector3.y : (0f - vector3.y));
			float z2 = ((j == 2 || j == 3 || j == 6 || j == 7) ? vector3.z : (0f - vector3.z));
			m_CornerMarkers[j].SetLocalPositionIfChanged(new Vector3(x, y, z2));
		}
		int num2 = (int)vector2.z - 1;
		bool num3 = num2 % 2 != 0;
		float num4 = vector.z / vector2.z;
		float num5 = (num3 ? num4 : (num4 * 0.5f));
		if (num3)
		{
			AddEdgeMarker(new Vector3(vector3.x, vector3.y, 0f), m_ZEdgeRotations[0]);
			AddEdgeMarker(new Vector3(vector3.x, 0f - vector3.y, 0f), m_ZEdgeRotations[1]);
			AddEdgeMarker(new Vector3(0f - vector3.x, vector3.y, 0f), m_ZEdgeRotations[2]);
			AddEdgeMarker(new Vector3(0f - vector3.x, 0f - vector3.y, 0f), m_ZEdgeRotations[3]);
			num2--;
		}
		for (int k = 0; k < num2; k++)
		{
			bool num6 = k % 2 != 0;
			float z2 = (num6 ? (0f - num5) : num5);
			AddEdgeMarker(new Vector3(vector3.x, vector3.y, z2), m_ZEdgeRotations[0]);
			AddEdgeMarker(new Vector3(vector3.x, 0f - vector3.y, z2), m_ZEdgeRotations[1]);
			AddEdgeMarker(new Vector3(0f - vector3.x, vector3.y, z2), m_ZEdgeRotations[2]);
			AddEdgeMarker(new Vector3(0f - vector3.x, 0f - vector3.y, z2), m_ZEdgeRotations[3]);
			if (num6)
			{
				num5 += num4;
			}
		}
		int num7 = (int)vector2.x - 1;
		bool num8 = num7 % 2 != 0;
		float num9 = vector.x / vector2.x;
		num5 = (num8 ? num9 : (num9 * 0.5f));
		if (num8)
		{
			AddEdgeMarker(new Vector3(0f, vector3.y, vector3.z), m_XEdgeRotations[0]);
			AddEdgeMarker(new Vector3(0f, 0f - vector3.y, vector3.z), m_XEdgeRotations[1]);
			AddEdgeMarker(new Vector3(0f, vector3.y, 0f - vector3.z), m_XEdgeRotations[2]);
			AddEdgeMarker(new Vector3(0f, 0f - vector3.y, 0f - vector3.z), m_XEdgeRotations[3]);
			num7--;
		}
		for (int l = 0; l < num7; l++)
		{
			bool num10 = l % 2 != 0;
			float x = (num10 ? (0f - num5) : num5);
			AddEdgeMarker(new Vector3(x, vector3.y, vector3.z), m_XEdgeRotations[0]);
			AddEdgeMarker(new Vector3(x, 0f - vector3.y, vector3.z), m_XEdgeRotations[1]);
			AddEdgeMarker(new Vector3(x, vector3.y, 0f - vector3.z), m_XEdgeRotations[2]);
			AddEdgeMarker(new Vector3(x, 0f - vector3.y, 0f - vector3.z), m_XEdgeRotations[3]);
			if (num10)
			{
				num5 += num9;
			}
		}
		int num11 = (int)vector2.y - 1;
		bool num12 = num11 % 2 != 0;
		float num13 = vector.y / vector2.y;
		num5 = (num12 ? num13 : (num13 * 0.5f));
		if (num12)
		{
			AddEdgeMarker(new Vector3(vector3.x, 0f, vector3.z), m_YEdgeRotations[0]);
			AddEdgeMarker(new Vector3(0f - vector3.x, 0f, vector3.z), m_YEdgeRotations[1]);
			AddEdgeMarker(new Vector3(vector3.x, 0f, 0f - vector3.z), m_YEdgeRotations[2]);
			AddEdgeMarker(new Vector3(0f - vector3.x, 0f, 0f - vector3.z), m_YEdgeRotations[3]);
			num11--;
		}
		for (int m = 0; m < num11; m++)
		{
			bool num14 = m % 2 != 0;
			float y = (num14 ? (0f - num5) : num5);
			AddEdgeMarker(new Vector3(vector3.x, y, vector3.z), m_YEdgeRotations[0]);
			AddEdgeMarker(new Vector3(0f - vector3.x, y, vector3.z), m_YEdgeRotations[1]);
			AddEdgeMarker(new Vector3(vector3.x, y, 0f - vector3.z), m_YEdgeRotations[2]);
			AddEdgeMarker(new Vector3(0f - vector3.x, y, 0f - vector3.z), m_YEdgeRotations[3]);
			if (num14)
			{
				num5 += num13;
			}
		}
		base.gameObject.SetActive(value: true);
	}

	public void HideHighlight()
	{
		base.gameObject.SetActive(value: false);
		m_Transform.SetParent(Singleton.Manager<ManPointer>.inst.transform, worldPositionStays: false);
		m_CubeMeshTrans.localScale = Vector3.one;
		if (m_HighlightedObject != null)
		{
			m_HighlightedObject.RecycledEvent.Unsubscribe(OnHighlightedObjectRecycled);
		}
		m_HighlightedObject = null;
		for (int num = m_UsedEdgeMarkers.Count - 1; num >= 0; num--)
		{
			m_UsedEdgeMarkers[num].m_Renderer.enabled = false;
			m_FreeEdgeMarkers.Add(m_UsedEdgeMarkers[num]);
		}
		m_UsedEdgeMarkers.Clear();
	}

	public void SetHighlightType(ManPointer.HighlightVariation highlightType)
	{
		m_Renderer.sharedMaterial = m_MaterialInstances[(int)highlightType];
	}

	private void AddEdgeMarker(Vector3 localPos, Quaternion localRot)
	{
		EdgeMarker item;
		if (m_FreeEdgeMarkers.Count > 0)
		{
			item = m_FreeEdgeMarkers[0];
			m_FreeEdgeMarkers.RemoveAt(0);
		}
		else
		{
			item = default(EdgeMarker);
			item.m_Transform = m_EdgeMarkerPrefab.transform.Spawn();
			item.m_Transform.parent = m_Transform;
			item.m_Renderer = item.m_Transform.GetComponent<Renderer>();
		}
		item.m_Transform.SetLocalPositionIfChanged(localPos);
		item.m_Transform.SetLocalRotationIfChanged(localRot);
		item.m_Renderer.enabled = true;
		m_UsedEdgeMarkers.Add(item);
	}

	private void OnHighlightedObjectRecycled(Visible vis)
	{
		if (vis == m_HighlightedObject)
		{
			HideHighlight();
		}
	}

	private void Awake()
	{
		m_Transform = base.transform;
		m_CubeMeshTrans = m_CubeMesh.transform;
		m_Renderer = m_CubeMeshTrans.GetComponent<Renderer>();
		m_MaterialInstances = new Material[m_Materials.Length];
		for (int i = 0; i < m_Materials.Length; i++)
		{
			m_MaterialInstances[i] = new Material(m_Materials[i]);
			m_MaterialInstances[i].renderQueue = 3001;
		}
		for (int j = 0; j < 8; j++)
		{
			m_CornerMarkers[j] = Object.Instantiate(m_CornerMarkerPrefab.transform);
			m_CornerMarkers[j].parent = m_Transform;
		}
	}
}
