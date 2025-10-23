using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManSceneryFade : Singleton.Manager<ManSceneryFade>
{
	[Serializable]
	public struct MatReplacePairs
	{
		public Material NormalMat;

		public Material ReplacementMat;
	}

	private struct MaterialAndFrame
	{
		public Material mat;

		public Material PrevMat;

		public int frame;

		public float Time;

		public bool FadedOut;

		public MaterialAndFrame(Material material, Material prevMat, int currentFrame, float time)
		{
			mat = material;
			PrevMat = prevMat;
			frame = currentFrame;
			Time = time;
			FadedOut = true;
		}
	}

	[SerializeField]
	private float m_FadeTime = 0.25f;

	[SerializeField]
	private float m_MinAlpha = 0.45f;

	[SerializeField]
	private List<MatReplacePairs> m_MaterialPairs;

	private Dictionary<SceneryFader, MaterialAndFrame> m_Materials = new Dictionary<SceneryFader, MaterialAndFrame>();

	private Dictionary<Material, int> m_SwapMaterialLookup = new Dictionary<Material, int>();

	private Dictionary<Material, int> m_OriginalMaterialLookup = new Dictionary<Material, int>();

	private List<Material>[] m_UnusedMaterials;

	private const float kMinSphereCastSize = 1f;

	private Ray m_Ray;

	private float m_Size;

	private float m_Distance;

	private RaycastHit[] m_RaycastResultBuffer = new RaycastHit[16];

	private int m_RaycastResultBufferSize = 16;

	private const int kRaycastResultBufferInitialSize = 16;

	public void UpdateFadedScenery(SceneryFader sceneryFader)
	{
		if (m_Materials.TryGetValue(sceneryFader, out var value))
		{
			for (int i = 0; i < sceneryFader.Renderers.Count; i++)
			{
				sceneryFader.Renderers[i].sharedMaterial = value.mat;
			}
		}
	}

	public void RevertFadedScenery(SceneryFader sceneryFader, bool removeFromList)
	{
		if (m_Materials.TryGetValue(sceneryFader, out var value))
		{
			for (int i = 0; i < sceneryFader.Renderers.Count; i++)
			{
				sceneryFader.Renderers[i].sharedMaterial = value.PrevMat;
			}
			if (removeFromList)
			{
				m_Materials.Remove(sceneryFader);
			}
		}
	}

	private Material GetMaterial(int index)
	{
		Material material = null;
		if (m_UnusedMaterials[index].Count > 0)
		{
			material = m_UnusedMaterials[index][0];
			m_UnusedMaterials[index].RemoveAt(0);
		}
		else
		{
			material = new Material(m_MaterialPairs[index].ReplacementMat);
		}
		return material;
	}

	private float GetFadeValue(MaterialAndFrame materialData, bool fadeDown)
	{
		float num = Mathf.Clamp(Time.time - materialData.Time, 0f, m_FadeTime);
		num /= m_FadeTime;
		if (fadeDown)
		{
			num = 1f - num;
		}
		return m_MinAlpha + (1f - m_MinAlpha) * num;
	}

	private void Awake()
	{
		m_UnusedMaterials = new List<Material>[m_MaterialPairs.Count];
		for (int i = 0; i < m_MaterialPairs.Count; i++)
		{
			m_SwapMaterialLookup.Add(m_MaterialPairs[i].NormalMat, i);
			m_OriginalMaterialLookup.Add(m_MaterialPairs[i].ReplacementMat, i);
			m_UnusedMaterials[i] = new List<Material>();
		}
	}

	private void Update()
	{
		if ((bool)Singleton.playerTank)
		{
			Vector3 vector = Singleton.playerTank.rbody.position + Singleton.playerTank.rbody.rotation * Singleton.playerTank.blockBounds.center - Singleton.cameraTrans.position;
			m_Distance = vector.magnitude;
			m_Size = 1f;
			float num = m_Size * m_Size;
			Vector3 normalized = vector.normalized;
			m_Ray = new Ray(Singleton.cameraTrans.position, normalized);
			int num2 = Physics.SphereCastNonAlloc(m_Ray, m_Size, m_RaycastResultBuffer, m_Distance, Globals.inst.layerScenery.mask | Globals.inst.layerLandmark.mask | Globals.inst.layerIgnoreScenery.mask | Globals.inst.layerSceneryFader.mask);
			if (num2 == m_RaycastResultBufferSize)
			{
				m_RaycastResultBufferSize *= 2;
				Array.Resize(ref m_RaycastResultBuffer, m_RaycastResultBufferSize);
			}
			for (int i = 0; i < num2; i++)
			{
				RaycastHit raycastHit = m_RaycastResultBuffer[i];
				if (raycastHit.collider.gameObject.layer == (int)Globals.inst.layerLandmark && (Singleton.playerTank.boundsCentreWorld - raycastHit.point).sqrMagnitude < num)
				{
					continue;
				}
				SceneryFader componentInParent = raycastHit.collider.GetComponentInParent<SceneryFader>();
				if (!(componentInParent != null) || componentInParent.IgnoreSceneryFade)
				{
					continue;
				}
				if (m_Materials.TryGetValue(componentInParent, out var value))
				{
					value.frame = Time.frameCount;
					m_Materials[componentInParent] = value;
					if (!value.FadedOut)
					{
						value.Time = Time.time;
					}
					value.FadedOut = true;
					continue;
				}
				Material material = null;
				int value2 = 0;
				for (int j = 0; j < componentInParent.Renderers.Count; j++)
				{
					Renderer renderer = componentInParent.Renderers[j];
					if (material == null && m_SwapMaterialLookup.TryGetValue(renderer.sharedMaterial, out value2))
					{
						material = GetMaterial(value2);
					}
					if (material != null)
					{
						renderer.sharedMaterial = material;
					}
				}
				if (material != null)
				{
					MaterialAndFrame value3 = new MaterialAndFrame(material, m_MaterialPairs[value2].NormalMat, Time.frameCount, Time.time);
					m_Materials.Add(componentInParent, value3);
				}
			}
		}
		for (int num3 = m_Materials.Count - 1; num3 >= 0; num3--)
		{
			KeyValuePair<SceneryFader, MaterialAndFrame> keyValuePair = m_Materials.ElementAt(num3);
			MaterialAndFrame value4 = keyValuePair.Value;
			if (value4.frame == Time.frameCount)
			{
				float fadeValue = GetFadeValue(value4, fadeDown: true);
				value4.mat.SetColor("_Color", new Color(1f, 1f, 1f, fadeValue));
			}
			else
			{
				if (value4.FadedOut)
				{
					value4.Time = Time.time;
					value4.FadedOut = false;
				}
				float fadeValue2 = GetFadeValue(value4, fadeDown: false);
				value4.mat.SetColor("_Color", new Color(1f, 1f, 1f, fadeValue2));
				SceneryFader key = keyValuePair.Key;
				if (fadeValue2 >= 1f || !key.gameObject.activeInHierarchy)
				{
					for (int k = 0; k < key.Renderers.Count; k++)
					{
						Renderer renderer2 = key.Renderers[k];
						renderer2.sharedMaterial = value4.PrevMat;
						if (k == 0 && m_SwapMaterialLookup.TryGetValue(renderer2.sharedMaterial, out var value5))
						{
							m_UnusedMaterials[value5].Add(value4.mat);
						}
					}
					m_Materials.Remove(keyValuePair.Key);
				}
				else
				{
					m_Materials.Remove(keyValuePair.Key);
					m_Materials.Add(keyValuePair.Key, value4);
				}
			}
		}
	}
}
