using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StaticTechSnapshotCamera : MonoBehaviour
{
	[SerializeField]
	private Vector3 m_OffsetVector;

	[Tooltip("Static padding applied to the frame - same for all tech sizes")]
	[SerializeField]
	private float m_FramePadding;

	[Tooltip("Dynamic padding based on target tech size. Helps offset big techs appearing relatively smaller in frame than small techs.")]
	[SerializeField]
	private float m_SizeBasedPadding = -6.5f;

	private Camera m_Camera;

	public Camera Camera => m_Camera;

	public void FrameTech(Tank tech)
	{
		Vector3 offsetVector = m_OffsetVector;
		float f = m_Camera.fieldOfView * 0.5f * ((float)Math.PI / 180f);
		Vector3 vector = tech.trans.TransformDirection(offsetVector);
		float magnitude = ((tech.blockBounds.size + Vector3.one) * 0.5f).magnitude;
		float num = magnitude * 2f / Mathf.Tan(f);
		SetCameraView(tech.boundsCentreWorld, vector * num, 0.1f, 1000f);
		Bounds blockBounds = tech.blockBounds;
		Vector3 center = blockBounds.center;
		Vector3 extents = blockBounds.extents;
		Vector3[] obj = new Vector3[8]
		{
			new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
			new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
			new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
			new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z),
			new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
			new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
			new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
			new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z)
		};
		Vector3 vector2 = new Vector3(Screen.width, Screen.height, float.MaxValue);
		Vector3 vector3 = new Vector3(0f, 0f, 0f);
		Vector3[] array = obj;
		foreach (Vector3 position in array)
		{
			Vector3 position2 = tech.trans.TransformPoint(position);
			Vector3 rhs = m_Camera.WorldToScreenPoint(position2);
			vector2 = Vector3.Min(vector2, rhs);
			vector3 = Vector3.Max(vector3, rhs);
		}
		Vector3 position3 = (vector2 + vector3) * 0.5f;
		Vector3 targetPos = m_Camera.ScreenToWorldPoint(position3);
		num = magnitude / Mathf.Tan(f);
		SetCameraView(targetPos, vector * num, num - magnitude, num + magnitude);
		if (m_FramePadding != 0f || m_SizeBasedPadding != 0f)
		{
			float num2 = m_FramePadding + (magnitude - 1f) * m_SizeBasedPadding;
			Vector3 vector4 = m_Camera.WorldToScreenPoint(tech.boundsCentreWorld);
			float num3 = m_Camera.ScreenToWorldPoint(vector4 + Vector3.up * num2).y - tech.boundsCentreWorld.y;
			magnitude += num3;
			num = magnitude / Mathf.Tan(f);
			SetCameraView(targetPos, vector * num, num - magnitude, num + magnitude);
		}
	}

	private void SetCameraView(Vector3 targetPos, Vector3 offsetVector, float nearPlane, float farPlane)
	{
		m_Camera.transform.position = targetPos + offsetVector;
		m_Camera.transform.LookAt(targetPos);
		m_Camera.nearClipPlane = nearPlane;
		m_Camera.farClipPlane = farPlane;
	}

	private void Awake()
	{
		m_OffsetVector.Normalize();
		m_Camera = GetComponent<Camera>();
	}
}
