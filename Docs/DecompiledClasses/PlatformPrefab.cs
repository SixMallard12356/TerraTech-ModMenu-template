#define UNITY_EDITOR
using UnityEngine;

public class PlatformPrefab : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Transform m_BasePrefab;

	[SerializeField]
	[HideInInspector]
	private Transform m_SwitchPrefab;

	[SerializeField]
	private GameObject m_BasePrefabGameObject;

	[SerializeField]
	private GameObject m_SwitchPrefabGameObject;

	private Transform m_InstantiatedPrefab;

	private void PrePool()
	{
		d.Assert(m_BasePrefabGameObject != null && m_SwitchPrefabGameObject != null, "PlatformPrefab on " + base.gameObject.name + " must have both a Base and Switch prefab assigned!", this);
	}

	private void OnSpawn()
	{
		GameObject gameObject = (SKU.SwitchUI ? m_SwitchPrefabGameObject : m_BasePrefabGameObject);
		if (gameObject != null)
		{
			if (PrefabSpawner.SpawnUnpooled)
			{
				m_InstantiatedPrefab = gameObject.transform.UnpooledSpawnWithLocalTransform(base.transform, Vector3.zero, Quaternion.identity);
			}
			else
			{
				m_InstantiatedPrefab = gameObject.transform.SpawnWithLocalTransform(base.transform);
			}
		}
	}

	private void OnRecycle()
	{
		if (m_InstantiatedPrefab != null)
		{
			m_InstantiatedPrefab.Recycle();
		}
	}
}
