#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace cakeslice;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class Outline : MonoBehaviour
{
	public enum OutlineEnableReason
	{
		Pointer,
		ScriptHighlight,
		CustomSkinHighlight
	}

	public struct MeshDataPair
	{
		public MeshFilter mesh;

		public Renderer renderer;

		public SkinnedMeshRenderer skinnedMeshRenderer;

		public Material m_CachedOutlineMaterial;
	}

	public bool eraseRenderer;

	private List<MeshDataPair> m_MeshData;

	private Visible m_Vis;

	private BitfieldNonAlloc<OutlineEnableReason> m_EnabledReason;

	private bool m_MeshesDirty;

	private int m_UsageID;

	public int color => m_UsageID;

	public List<MeshDataPair> MeshData => m_MeshData;

	public void EnableOutline(bool enable, OutlineEnableReason reason)
	{
		bool anySet = m_EnabledReason.AnySet;
		m_EnabledReason = m_EnabledReason.Set((int)reason, enable);
		UpdateUsageID();
		if (m_EnabledReason.AnySet != anySet)
		{
			UpdateOutlineEnabledState();
		}
	}

	public void OnMeshesUpdated()
	{
		m_MeshesDirty = true;
		if (m_EnabledReason.AnySet)
		{
			UpdateFromChildren();
		}
	}

	private void UpdateUsageID()
	{
		int usageID = m_UsageID;
		if (m_EnabledReason.Contains(0))
		{
			m_UsageID = 1;
		}
		else if (m_EnabledReason.Contains(1))
		{
			m_UsageID = 0;
		}
		else if (m_EnabledReason.Contains(2))
		{
			m_UsageID = 2;
		}
		else
		{
			d.AssertFormat(m_EnabledReason.IsNull, "Unhandled mapping from outline usage to Usage(colour)ID");
			m_UsageID = 0;
		}
		if (usageID != m_UsageID)
		{
			for (int i = 0; i < m_MeshData.Count; i++)
			{
				MeshDataPair value = m_MeshData[i];
				value.m_CachedOutlineMaterial = null;
				m_MeshData[i] = value;
			}
		}
	}

	private void UpdateOutlineEnabledState()
	{
		if (m_EnabledReason.AnySet)
		{
			if (m_MeshesDirty)
			{
				UpdateFromChildren();
			}
			Singleton.Manager<CameraManager>.inst.OutlineEffect.AddOutline(this);
		}
		else if (Singleton.Manager<CameraManager>.inst != null && Singleton.Manager<CameraManager>.inst.OutlineEffect != null)
		{
			Singleton.Manager<CameraManager>.inst.OutlineEffect.RemoveOutline(this);
		}
	}

	private void UpdateFromChildren()
	{
		if (m_MeshData == null)
		{
			m_MeshData = new List<MeshDataPair>();
		}
		else
		{
			m_MeshData.Clear();
		}
		UpdateRenderableMeshesOnChildren(base.transform);
		m_MeshesDirty = false;
	}

	private void UpdateRenderableMeshesOnChildren(Transform trans)
	{
		if (!(trans.GetComponent<NoOutline>() == null))
		{
			return;
		}
		MeshFilter meshFilter = trans.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = null;
		}
		Renderer renderer = null;
		SkinnedMeshRenderer skinnedMeshRenderer = null;
		if (meshFilter.IsNotNull())
		{
			renderer = trans.GetComponent<Renderer>();
			if (renderer == null)
			{
				renderer = null;
			}
		}
		else
		{
			skinnedMeshRenderer = trans.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				skinnedMeshRenderer = null;
			}
			renderer = skinnedMeshRenderer;
		}
		if (renderer != null)
		{
			m_MeshData.Add(new MeshDataPair
			{
				mesh = meshFilter,
				skinnedMeshRenderer = skinnedMeshRenderer,
				renderer = renderer
			});
		}
		int childCount = trans.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = trans.GetChild(i);
			UpdateRenderableMeshesOnChildren(child);
		}
	}

	private void OnPool()
	{
		UpdateFromChildren();
		m_Vis = GetComponent<Visible>();
		if (m_Vis != null)
		{
			m_Vis.MesheRenderersUpdatedEvent.Subscribe(OnMeshesUpdated);
		}
	}

	private void OnDepool()
	{
		if (m_Vis != null)
		{
			m_Vis.MesheRenderersUpdatedEvent.Unsubscribe(OnMeshesUpdated);
		}
	}

	private void OnRecycle()
	{
		bool anySet = m_EnabledReason.AnySet;
		m_EnabledReason = default(BitfieldNonAlloc<OutlineEnableReason>);
		if (anySet)
		{
			UpdateOutlineEnabledState();
		}
	}
}
