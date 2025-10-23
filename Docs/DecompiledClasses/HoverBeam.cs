#define UNITY_EDITOR
using System.Linq;
using UnityEngine;

public class HoverBeam : MonoBehaviour
{
	public Color beamColour = Color.white;

	public Color beamEmissive = Color.white;

	public Color smokeColour = Color.white;

	public Color smokeEmissive = Color.white;

	public float m_ExtendTopBy = 5f;

	private Transform trans;

	private MeshRenderer beamRenderer;

	private Transform beamRendererXform;

	private Visible visible;

	private float groundHeight;

	public float BaseY { get; private set; }

	public float Height { get; private set; }

	public void SetBeamAndSmokeColour(Color beamCol, Color smokeCol)
	{
		if (beamColour != beamCol)
		{
			SetBeamColour(beamCol);
		}
		if (smokeColour != smokeCol)
		{
			SetSmokeColour(smokeCol);
		}
	}

	public void SetBeamColour(Color newCol)
	{
		SetShaderColour("_BeamTint", newCol);
		beamColour = newCol;
	}

	public void SetBeamEmissive(Color newCol)
	{
		SetShaderColour("_BeamEmissive", newCol);
		beamEmissive = newCol;
	}

	public void SetSmokeColour(Color newCol)
	{
		SetShaderColour("_TintColor", newCol);
		smokeColour = newCol;
	}

	public void SetSmokeEmissive(Color newCol)
	{
		SetShaderColour("_EmissiveColor", newCol);
		smokeEmissive = newCol;
	}

	private void SetShaderColour(string varName, Color newCol)
	{
		if (beamRenderer != null)
		{
			Vector4 value = new Vector4(newCol.r, newCol.g, newCol.b, newCol.a);
			beamRenderer.material.SetVector(varName, value);
		}
	}

	private void UpdateYHeight()
	{
		if (beamRenderer != null)
		{
			beamRenderer.material.SetFloat("_YHeight", groundHeight);
		}
	}

	private bool UpdateTransformAndTiling()
	{
		d.Assert(visible.isActive);
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (beamRenderer != null)
		{
			beamRenderer.material.SetTextureScale("_MainTex", new Vector2(beamRendererXform.localScale.z / 2f, 1f));
		}
		Vector3 input = Singleton.cameraTrans.position - trans.position;
		input.y = 0f;
		trans.SetRotationIfChanged(Quaternion.LookRotation(-input.SetY(0f)));
		Plane[] array = Util.CalculateFrustumPlanes(Singleton.camera);
		Ray ray = new Ray(trans.position, -Vector3.up);
		array[2].Raycast(ray, out var enter);
		Vector3 point = ray.GetPoint(enter);
		array[3].Raycast(ray, out enter);
		Vector3 point2 = ray.GetPoint(enter);
		if (point.y > point2.y)
		{
			float y = point2.y;
			point2.y = point.y;
			point.y = y;
		}
		if ((bool)beamRenderer)
		{
			BaseY = groundHeight;
			Height = Mathf.Clamp(point2.y - BaseY, 1f, 1000f);
			Vector3 newWorldPos = trans.position.SetY(BaseY + Height / 2f + m_ExtendTopBy / 2f);
			beamRendererXform.SetPositionIfChanged(newWorldPos);
			beamRendererXform.SetLocalScaleIfChanged(beamRendererXform.localScale.SetZ(Height / 2f + m_ExtendTopBy));
		}
		UpdateYHeight();
		return false;
	}

	private void OnPool()
	{
		trans = base.transform;
		beamRenderer = GetComponentsInChildren<MeshRenderer>(includeInactive: true).FirstOrDefault();
		beamRendererXform = beamRenderer.transform;
		SetBeamColour(beamColour);
		SetSmokeColour(smokeColour);
		SetBeamEmissive(beamEmissive);
		SetSmokeEmissive(smokeEmissive);
		if ((bool)GetComponent<Collider>())
		{
			GetComponent<Collider>().enabled = false;
		}
		visible = Visible.FindVisibleUpwards(this);
	}

	private void OnSpawn()
	{
		trans.rotation = Quaternion.identity;
		Singleton.Manager<ManTimedEvents>.inst.AddPreRenderEvent(UpdateTransformAndTiling);
		if ((bool)GetComponent<Collider>())
		{
			GetComponent<Collider>().enabled = true;
		}
	}

	private void OnRecycle()
	{
		trans.rotation = Quaternion.identity;
		Singleton.Manager<ManTimedEvents>.inst.RemovePreRenderEvent(UpdateTransformAndTiling);
		if ((bool)GetComponent<Collider>())
		{
			GetComponent<Collider>().enabled = false;
		}
	}

	private void Update()
	{
		bool flag = !Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		if (beamRendererXform != null && beamRendererXform.gameObject.activeSelf != flag)
		{
			beamRendererXform.gameObject.SetActive(flag);
		}
		Vector3 scenePos = trans.TransformPoint(beamRendererXform.localPosition + Vector3.up * 0.5f);
		groundHeight = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos, hitScenery: true).y;
	}
}
