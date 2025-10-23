#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTriggerArea : Checkpoint
{
	private struct EntryData
	{
		public float triggerEntryTime;
	}

	[SerializeField]
	private float m_InitialSize = 1f;

	[SerializeField]
	private bool m_IgnoreTrackHeight = true;

	[SerializeField]
	private float m_TimeBeforePass = 0.5f;

	[SerializeField]
	private GameObject m_TriggerGameObject;

	private Dictionary<Tank, EntryData> m_EntryData = new Dictionary<Tank, EntryData>();

	private Dictionary<Tank, int> m_TriggerHitCount = new Dictionary<Tank, int>();

	protected override void SetupCheckpoint(Vector3 position, Vector3 fwdDir, Vector3 upDir, float width, float height)
	{
		base.transform.position = position;
		base.transform.rotation = Quaternion.LookRotation(fwdDir, upDir);
		float num = width / m_InitialSize;
		if (m_IgnoreTrackHeight)
		{
			height = 1f;
		}
		base.transform.localScale = new Vector3(num, height, num);
	}

	public override bool IsTechInContact(Tank tank)
	{
		return m_EntryData.ContainsKey(tank);
	}

	private void HandleTriggerEnter(Tank tank)
	{
		if (m_TimeBeforePass <= 0f)
		{
			OnCheckpointPassed.Send(this, tank);
			return;
		}
		m_EntryData.Add(tank, new EntryData
		{
			triggerEntryTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime()
		});
	}

	private void HandleTriggerExit(Tank tank)
	{
		m_EntryData.Remove(tank);
	}

	private void OnTriggerEvent(TriggerCatcher.Interaction triggerType, Collider collider)
	{
		switch (triggerType)
		{
		case TriggerCatcher.Interaction.Enter:
		{
			Tank techFromCollider2 = Tank.GetTechFromCollider(collider);
			if (techFromCollider2 != null)
			{
				int value2 = 0;
				if (!m_TriggerHitCount.TryGetValue(techFromCollider2, out value2) || value2 == 0)
				{
					HandleTriggerEnter(techFromCollider2);
				}
				m_TriggerHitCount[techFromCollider2] = value2 + 1;
			}
			break;
		}
		case TriggerCatcher.Interaction.Exit:
		{
			Tank techFromCollider = Tank.GetTechFromCollider(collider);
			if (!(techFromCollider != null))
			{
				break;
			}
			int value = 0;
			if (m_TriggerHitCount.TryGetValue(techFromCollider, out value))
			{
				value--;
				if (value == 0)
				{
					HandleTriggerExit(techFromCollider);
					m_TriggerHitCount.Remove(techFromCollider);
				}
				else
				{
					m_TriggerHitCount[techFromCollider] = value;
				}
			}
			else
			{
				d.LogError("CheckpointTriggerGate.OnTankTrigger (Exit) - Trigger exit called without matching trigger enter! Colliding objects: " + base.name + " and " + techFromCollider.name);
			}
			break;
		}
		}
	}

	private void OnSpawn()
	{
		TriggerCatcher.Subscribe((m_TriggerGameObject != null) ? m_TriggerGameObject : base.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit, OnTriggerEvent);
	}

	private void OnRecycle()
	{
		TriggerCatcher.Unsubscribe((m_TriggerGameObject != null) ? m_TriggerGameObject : base.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit, OnTriggerEvent);
		m_EntryData.Clear();
		m_TriggerHitCount.Clear();
	}

	protected override void Update()
	{
		if (!(m_TimeBeforePass > 0f) || m_EntryData.Count <= 0)
		{
			return;
		}
		float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		foreach (KeyValuePair<Tank, EntryData> entryDatum in m_EntryData)
		{
			float triggerEntryTime = entryDatum.Value.triggerEntryTime;
			if (currentModeRunningTime > triggerEntryTime + m_TimeBeforePass)
			{
				Tank key = entryDatum.Key;
				OnCheckpointPassed.Send(this, key);
				m_EntryData.Remove(key);
				break;
			}
		}
	}
}
