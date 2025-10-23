using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class ActivateEncounterObjectOnCollision : MonoBehaviour
{
	[FormerlySerializedAs("m_SpawnOnCollision")]
	[SerializeField]
	private List<Transform> m_SpawnPermanent = new List<Transform>();

	[SerializeField]
	private List<Transform> m_SpawnTemporary = new List<Transform>();

	[SerializeField]
	private List<ParticleSystem> m_StopOnCollision = new List<ParticleSystem>();

	[SerializeField]
	private LayerMask m_HitLayers;

	[SerializeField]
	private float m_HeightAboveHitPoint = 0.2f;

	[SerializeField]
	private bool m_HideOnTerrainCollision;

	[SerializeField]
	private bool m_KillOnTerrainCollision;

	[SerializeField]
	private float m_AfterTime = 3f;

	private bool m_Collided;

	private float m_Timer;

	public Event<int> OnCollision;

	private Encounter m_Encounter;

	private void OnCollisionEnter(Collision col)
	{
		HandleCollision(col.gameObject);
	}

	private void OnTriggerEnter(Collider col)
	{
		HandleCollision(col.gameObject);
	}

	private void SetEncounter(Encounter encounter)
	{
		m_Encounter = encounter;
	}

	private void HandleCollision(GameObject colObject)
	{
		if (m_Collided || (m_HitLayers.value & (1 << colObject.layer)) != 1 << colObject.layer)
		{
			return;
		}
		for (int i = 0; i < m_SpawnPermanent.Count; i++)
		{
			Transform transform = m_SpawnPermanent[i];
			bool addToEncounter = true;
			string nameWithinEncounter = ((transform != null) ? (transform.name + i) : "null");
			SpawnObject(transform, addToEncounter, nameWithinEncounter);
		}
		for (int j = 0; j < m_SpawnTemporary.Count; j++)
		{
			Transform prefab = m_SpawnTemporary[j];
			bool addToEncounter2 = false;
			string nameWithinEncounter2 = "noname";
			SpawnObject(prefab, addToEncounter2, nameWithinEncounter2);
		}
		foreach (ParticleSystem item in m_StopOnCollision)
		{
			item.Stop();
		}
		m_Collided = true;
		OnCollision.Send(0);
		OnCollision.Clear();
		m_Timer = 0f;
	}

	private void SpawnObject(Transform prefab, bool addToEncounter, string nameWithinEncounter)
	{
		if (!addToEncounter || !m_Encounter || !m_Encounter.GetStoredObject(prefab.name, nameWithinEncounter))
		{
			Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(base.transform.position);
			Vector3 terrainNormal = Singleton.Manager<ManWorld>.inst.GetTerrainNormal(vector);
			Transform transform = prefab.Spawn(vector + terrainNormal * m_HeightAboveHitPoint);
			transform.name = prefab.name;
			if (addToEncounter && (bool)m_Encounter)
			{
				m_Encounter.AddStoredObject(transform, nameWithinEncounter);
			}
			transform.LookAt(vector + terrainNormal);
		}
	}

	private void OnEnable()
	{
		m_Collided = false;
		m_Encounter = null;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDrag);
	}

	private void OnDisable()
	{
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(OnDrag);
	}

	private void Update()
	{
		if (!m_Collided || (!m_KillOnTerrainCollision && !m_HideOnTerrainCollision))
		{
			return;
		}
		m_Timer += Time.deltaTime;
		if (m_Timer > m_AfterTime)
		{
			if (m_HideOnTerrainCollision)
			{
				base.gameObject.SetActive(value: false);
			}
			else
			{
				base.transform.Recycle();
			}
		}
	}

	private void OnDrag(Visible held, ManPointer.DragAction da, Vector3 pos)
	{
		if (held.trans == base.transform.parent)
		{
			base.transform.Recycle();
		}
	}
}
