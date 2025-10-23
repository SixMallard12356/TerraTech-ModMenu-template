using System.Collections.Generic;
using UnityEngine;

public class CraterGroundAlign : MonoBehaviour
{
	[SerializeField]
	private List<Transform> m_InnerCircle;

	[SerializeField]
	private List<Transform> m_OuterCircle;

	[SerializeField]
	private bool m_RotateOuterCircleToGround = true;

	private bool m_UpdateOnLateUpdate;

	private List<Vector3> m_InnerInitialPos = new List<Vector3>();

	private List<Vector3> m_OuterInitialPos = new List<Vector3>();

	private List<Quaternion> m_InnerInitialRot = new List<Quaternion>();

	private List<Quaternion> m_OuterInitialRot = new List<Quaternion>();

	private void ChangeAllColliderLayers(string layerName)
	{
		MeshCollider[] componentsInChildren = GetComponentsInChildren<MeshCollider>();
		int layer = LayerMask.NameToLayer(layerName);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = layer;
		}
	}

	private void AllignToGround(Transform trans, bool rotateToGround = true)
	{
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(trans.position);
		Vector3 terrainNormal = Singleton.Manager<ManWorld>.inst.GetTerrainNormal(vector);
		trans.position = vector;
		if (rotateToGround)
		{
			float newY = Vector3.Angle(terrainNormal, Vector3.up);
			trans.localEulerAngles = trans.localEulerAngles.SetY(newY);
		}
	}

	private void Awake()
	{
		foreach (Transform item in m_InnerCircle)
		{
			m_InnerInitialPos.Add(item.localPosition);
			m_InnerInitialRot.Add(item.localRotation);
		}
		foreach (Transform item2 in m_OuterCircle)
		{
			m_OuterInitialPos.Add(item2.localPosition);
			m_OuterInitialRot.Add(item2.localRotation);
		}
	}

	private void OnEnable()
	{
		m_UpdateOnLateUpdate = true;
	}

	private void OnDisable()
	{
		if (m_InnerInitialPos.Count == 0 && m_OuterInitialPos.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (Transform item in m_InnerCircle)
		{
			if ((bool)item)
			{
				item.localRotation = m_InnerInitialRot[num];
				item.localPosition = m_InnerInitialPos[num];
				num++;
			}
		}
		num = 0;
		foreach (Transform item2 in m_OuterCircle)
		{
			if ((bool)item2)
			{
				item2.localRotation = m_OuterInitialRot[num];
				item2.localPosition = m_OuterInitialPos[num];
				num++;
			}
		}
	}

	private void LateUpdate()
	{
		if (!m_UpdateOnLateUpdate)
		{
			return;
		}
		ChangeAllColliderLayers("Default");
		Vector3 scenePos = base.transform.position;
		if (!Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos))
		{
			return;
		}
		foreach (Transform item in m_InnerCircle)
		{
			AllignToGround(item);
		}
		foreach (Transform item2 in m_OuterCircle)
		{
			AllignToGround(item2, m_RotateOuterCircleToGround);
		}
		m_UpdateOnLateUpdate = false;
		ChangeAllColliderLayers("Terrain");
	}
}
