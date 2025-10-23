#define UNITY_EDITOR
using System;
using UnityEngine;

public class ResourceReservoir : MonoBehaviour
{
	[Serializable]
	public class SerialData
	{
		public int totalChunks;

		public int numChunksRemaining;
	}

	[SerializeField]
	private ChunkTypes m_ChunkType;

	[SerializeField]
	[Tooltip("Multiplier applied to the extraction speed of the AutoMiner. 1 is normal speed, 2 is twice as fast (half time), and 0.5 is twice as slow.")]
	private float m_ExtractionSpeedMultiplier = 1f;

	[SerializeField]
	private int m_MinTotalChunks = 50;

	[SerializeField]
	private int m_MaxTotalChunks = 100;

	[SerializeField]
	private SceneryTypes m_SceneryType;

	[SerializeField]
	private Transform[] m_ObjectsToHideWhenEmpty;

	[SerializeField]
	private float m_MinScaleOfObjectsToHide = 1f;

	private int m_TotalChunks;

	private int m_NumChunksRemaining;

	private Visible m_ParentVisible;

	public bool IsEmpty => m_NumChunksRemaining == 0;

	public float ExtractionSpeedMultiplier => m_ExtractionSpeedMultiplier;

	public ChunkTypes ResourceType => m_ChunkType;

	public ChunkTypes TakeResource()
	{
		bool num = m_NumChunksRemaining > 0;
		d.Assert(num, "ResourceReservoir.TakeResource called while there was nothing left!");
		if (num)
		{
			m_NumChunksRemaining--;
			UpdateVisualState();
			if (m_NumChunksRemaining <= 0)
			{
				m_ParentVisible.tileCache.tile.RemoveResourceSource(m_ParentVisible, m_ChunkType);
			}
		}
		return m_ChunkType;
	}

	public void Store(ref SerialData serialData)
	{
		if (serialData == null)
		{
			serialData = new SerialData();
		}
		serialData.totalChunks = m_TotalChunks;
		serialData.numChunksRemaining = m_NumChunksRemaining;
	}

	public void Restore(SerialData serialData)
	{
		if (serialData != null)
		{
			m_TotalChunks = serialData.totalChunks;
			m_NumChunksRemaining = serialData.numChunksRemaining;
			UpdateVisualState();
		}
	}

	public float GetRemainingFraction()
	{
		return (float)m_NumChunksRemaining / (float)m_TotalChunks;
	}

	public SceneryTypes GetSceneryType()
	{
		return m_SceneryType;
	}

	public void SetAwake(bool awake)
	{
		if (awake)
		{
			if (m_NumChunksRemaining > 0)
			{
				m_ParentVisible.tileCache.tile.AddResourceSource(m_ParentVisible, m_ChunkType);
			}
		}
		else
		{
			m_ParentVisible.tileCache.tile.RemoveResourceSource(m_ParentVisible, m_ChunkType);
		}
	}

	private void UpdateVisualState()
	{
		if (m_ObjectsToHideWhenEmpty == null)
		{
			return;
		}
		bool active = !IsEmpty;
		float num = Mathf.Lerp(m_MinScaleOfObjectsToHide, 1f, GetRemainingFraction());
		Vector3 localScale = Vector3.one * num;
		for (int i = 0; i < m_ObjectsToHideWhenEmpty.Length; i++)
		{
			if (m_ObjectsToHideWhenEmpty[i] != null)
			{
				m_ObjectsToHideWhenEmpty[i].gameObject.SetActive(active);
				if (m_MinScaleOfObjectsToHide < 1f)
				{
					m_ObjectsToHideWhenEmpty[i].localScale = localScale;
				}
			}
		}
	}

	private void OnSpawn()
	{
		m_ParentVisible = Visible.FindVisibleUpwards(this);
		d.Assert((bool)m_ParentVisible && (bool)m_ParentVisible.resdisp, "Resource reservoir spawned not as child of ResourceDispenser");
		m_TotalChunks = UnityEngine.Random.Range(m_MinTotalChunks, m_MaxTotalChunks);
		m_NumChunksRemaining = m_TotalChunks;
		UpdateVisualState();
		m_ParentVisible.resdisp.RegisterResourceReservoir(this, register: true);
	}

	private void OnRecycle()
	{
		m_ParentVisible.resdisp.RegisterResourceReservoir(this, register: false);
		m_ParentVisible = null;
	}
}
