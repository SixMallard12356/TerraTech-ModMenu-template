using UnityEngine;

public class SpawnOnFirstUpdate : MonoBehaviour
{
	public Transform m_Prefab;

	private void OnNextUpdate()
	{
		m_Prefab.Spawn(base.transform.position);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(0f, OnNextUpdate);
	}
}
