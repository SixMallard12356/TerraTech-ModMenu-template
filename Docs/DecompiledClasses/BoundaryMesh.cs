#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryMesh : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Scaling from world scale to UV on boundary material")]
	private float m_BoundaryUVScale;

	[SerializeField]
	private bool m_Regenerate;

	private Material m_BoundaryMaterial;

	private int m_BoundaryMaterialTechPosID;

	private Vector3 m_RemainingMoveDelta;

	private float m_RemainingMoveTime;

	private void GenerateMesh()
	{
		Renderer component = GetComponent<Renderer>();
		if (component != null)
		{
			m_BoundaryMaterial = new Material(component.sharedMaterial);
			component.sharedMaterial = m_BoundaryMaterial;
			m_BoundaryMaterialTechPosID = Shader.PropertyToID("_TechPos");
		}
		MeshFilter component2 = GetComponent<MeshFilter>();
		if (component2 != null)
		{
			float num = ((m_BoundaryMaterial == null) ? 100f : (m_BoundaryMaterial.GetFloat("_DrawDistance") + m_BoundaryUVScale));
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < 360; i++)
			{
				Vector3 item = new Vector3(Mathf.Sin((float)Math.PI / 180f * (float)i), 0f, Mathf.Cos((float)Math.PI / 180f * (float)i)) * Singleton.Manager<ManNetwork>.inst.DangerDistance;
				item.y = 0f - num;
				list.Add(item);
			}
			component2.mesh = PrismMeshGenerator.GenerateMesh(list.ToArray(), Vector3.up * (num * 2f), m_BoundaryUVScale);
		}
		else
		{
			d.LogWarning("Track edge prefab needs to have a MeshFilter added to it");
		}
		m_Regenerate = false;
	}

	public void Move(Vector3 oldPos, Vector3 newPos, float time)
	{
		base.transform.position = oldPos;
		m_RemainingMoveDelta = (newPos - oldPos).SetY(0f);
		m_RemainingMoveTime = time;
	}

	private void OnSpawn()
	{
		GenerateMesh();
	}

	private void OnRecycle()
	{
		MeshFilter component = GetComponent<MeshFilter>();
		if (component != null && component.mesh != null)
		{
			UnityEngine.Object.Destroy(component.mesh);
			component.mesh = null;
		}
	}

	private void LateUpdate()
	{
		if (m_Regenerate)
		{
			GenerateMesh();
		}
		if (m_RemainingMoveTime > 0f && Time.deltaTime > 0f)
		{
			Vector3 vector = Mathf.Clamp01(Time.deltaTime / m_RemainingMoveTime) * m_RemainingMoveDelta;
			base.transform.position += vector;
			m_RemainingMoveTime -= Time.deltaTime;
			m_RemainingMoveDelta -= vector;
		}
		if (!Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull() || !(Singleton.Manager<ManNetwork>.inst.NetController != null))
		{
			return;
		}
		Vector3 position = base.transform.position;
		NetTech curTech = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech;
		if (curTech.IsNotNull())
		{
			Vector3 boundsCentreWorldNoCheck = curTech.tech.boundsCentreWorldNoCheck;
			_ = (boundsCentreWorldNoCheck - position).SetY(0f).magnitude;
			if (m_BoundaryUVScale != 0f)
			{
				position.y = boundsCentreWorldNoCheck.y - boundsCentreWorldNoCheck.y % (1f / m_BoundaryUVScale);
				base.transform.SetPositionIfChanged(position);
			}
			if (m_BoundaryMaterial != null)
			{
				m_BoundaryMaterial.SetVector(m_BoundaryMaterialTechPosID, boundsCentreWorldNoCheck);
			}
		}
	}
}
