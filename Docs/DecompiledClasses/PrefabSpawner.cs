using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
	[Serializable]
	public class PrefabData : ISerializationCallbackReceiver
	{
		[SerializeField]
		private string prefabName;

		public bool enabled = true;

		public Transform prefab;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale = Vector3.zero;

		public Vector3 pivot;

		public Vector2 sizeDelta;

		public Vector2 anchorMin;

		public Vector2 anchorMax;

		public void ApplyPositionData(Transform trans, bool applyScale)
		{
			RectTransform rectTransform = trans as RectTransform;
			if (rectTransform != null)
			{
				rectTransform.anchoredPosition3D = position;
				rectTransform.pivot = pivot;
				rectTransform.sizeDelta = sizeDelta;
				rectTransform.anchorMin = anchorMin;
				rectTransform.anchorMax = anchorMax;
				rectTransform.rotation = rotation;
				rectTransform.localScale = Vector3.one;
			}
			else
			{
				trans.localPosition = position;
				trans.localRotation = rotation;
				if (applyScale && scale.x != 0f)
				{
					trans.localScale = scale;
				}
			}
		}

		public void OnBeforeSerialize()
		{
			prefabName = ((prefab != null) ? prefab.name : "null");
		}

		public void OnAfterDeserialize()
		{
		}
	}

	public interface EditorPreInit
	{
		bool OnPreInitEditor();
	}

	[SerializeField]
	private PrefabData[] m_PrefabData;

	[SerializeField]
	private bool m_PreserveChildScale;

	[SerializeField]
	private bool m_SpawnChildrenAcrossMultipleFrames;

	public static bool SpawnUnpooled;

	private bool m_Instantiated;

	private List<Transform> m_Children = new List<Transform>();

	private bool m_SpawningChildren;

	private int m_SpawnChildrenIndex;

	private const int kTimeToProcessPerUpdateMS = 150;

	private Stopwatch m_ProcessCostTimer = new Stopwatch();

	public bool IsSettingUp
	{
		get
		{
			if (m_Instantiated)
			{
				return m_SpawningChildren;
			}
			return true;
		}
	}

	public void SpawnChildren(bool fromEditor = false, bool fromEditorRecursive = false)
	{
		if (!m_Instantiated)
		{
			m_SpawningChildren = true;
			base.enabled = true;
			m_SpawnChildrenIndex = 0;
			SpawnChildrenInternal(fromEditor);
			m_Instantiated = true;
		}
	}

	private void SpawnChildrenInternal(bool fromEditor)
	{
		bool flag = m_SpawnChildrenAcrossMultipleFrames && !fromEditor;
		Transform parentTrans = base.transform;
		m_ProcessCostTimer.Restart();
		for (int i = m_SpawnChildrenIndex; i < m_PrefabData.Length; i++)
		{
			if (flag && m_ProcessCostTimer.ElapsedMilliseconds > 150)
			{
				break;
			}
			if (!fromEditor && !m_PrefabData[i].enabled)
			{
				m_SpawnChildrenIndex++;
				continue;
			}
			Transform prefab = m_PrefabData[i].prefab;
			Transform item = SpawnPrefab(fromEditor, parentTrans, prefab, m_PrefabData[i]);
			m_Children.Add(item);
			m_SpawnChildrenIndex++;
		}
		m_ProcessCostTimer.Stop();
		if (m_SpawnChildrenIndex >= m_PrefabData.Length)
		{
			m_SpawningChildren = false;
			m_SpawnChildrenIndex = 0;
		}
	}

	private void RecycleChildren()
	{
		for (int i = 0; i < m_Children.Count; i++)
		{
			RectTransform rectTransform = m_Children[i] as RectTransform;
			if (rectTransform != null)
			{
				rectTransform.transform.SetParent(null, worldPositionStays: false);
			}
			m_Children[i].Recycle();
		}
		m_Children.Clear();
		m_Instantiated = false;
	}

	private Transform SpawnPrefab(bool fromEditor, Transform parentTrans, Transform prefabObj, PrefabData prefabData)
	{
		Transform transform;
		if (prefabObj as RectTransform != null)
		{
			transform = (SpawnUnpooled ? prefabObj.UnpooledSpawn(parentTrans) : prefabObj.Spawn(parentTrans));
			prefabData.ApplyPositionData(transform, m_PreserveChildScale);
		}
		else
		{
			transform = (SpawnUnpooled ? prefabObj.UnpooledSpawnWithLocalTransform(base.transform, prefabData.position, prefabData.rotation) : prefabObj.SpawnWithLocalTransform(base.transform, prefabData.position, prefabData.rotation));
			if (m_PreserveChildScale)
			{
				transform.localScale = prefabData.scale;
			}
		}
		transform.name = prefabObj.name;
		return transform;
	}

	private void Update()
	{
		if (m_SpawningChildren)
		{
			SpawnChildrenInternal(fromEditor: false);
		}
		else
		{
			base.enabled = false;
		}
	}

	private void OnSpawn()
	{
		SpawnChildren();
	}

	private void OnRecycle()
	{
		RecycleChildren();
	}
}
