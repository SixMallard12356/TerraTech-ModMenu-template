#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public abstract class CheckpointTriggerGate : Checkpoint
{
	private enum CheckpointPassType
	{
		TouchTrigger,
		PassCentre,
		PassThrough
	}

	private struct EntryData
	{
		public Tank tank;

		public Vector3 triggerEnterDirection;
	}

	[Tooltip("What counts as passing a gate. Note that the Pass<> types may fail to behave as expected if the tech is modified (damage or other) while in contact with the trigger.")]
	[SerializeField]
	private CheckpointPassType m_PassType;

	[SerializeField]
	private bool m_ForwardsOnly = true;

	[SerializeField]
	private GameObject m_TriggerGameObject;

	private Transform m_Transform;

	private List<EntryData> m_EntryData = new List<EntryData>();

	private Dictionary<Tank, int> m_TriggerHitCount = new Dictionary<Tank, int>();

	public Transform Trans => m_Transform;

	public override bool IsTechInContact(Tank tank)
	{
		bool result = false;
		if (tank != null)
		{
			for (int i = 0; i < m_EntryData.Count; i++)
			{
				if (m_EntryData[i].tank == tank)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	protected virtual bool TriggerEntryValid(Tank tank)
	{
		return true;
	}

	private void HandleTriggerEnter(Tank tank)
	{
		if (!TriggerEntryValid(tank))
		{
			return;
		}
		bool flag = Vector3.Dot(m_Transform.forward, tank.rbody.velocity.normalized) > 0f;
		if (!m_ForwardsOnly || flag)
		{
			if (m_PassType == CheckpointPassType.TouchTrigger)
			{
				OnCheckpointPassed.Send(this, tank);
			}
			else if (!IsTechInContact(tank))
			{
				m_EntryData.Add(new EntryData
				{
					tank = tank,
					triggerEnterDirection = (flag ? m_Transform.forward : (-m_Transform.forward))
				});
			}
			else
			{
				d.LogError("HandleTriggerEnter - Trying to add tank " + tank.name + " but tank is already in the list of entries!");
			}
		}
	}

	private void HandleTriggerExit(Tank tank)
	{
		int num = -1;
		if (tank != null)
		{
			for (int i = 0; i < m_EntryData.Count; i++)
			{
				if (m_EntryData[i].tank == tank)
				{
					num = i;
					break;
				}
			}
		}
		if (num >= 0)
		{
			if (m_PassType == CheckpointPassType.PassThrough && Vector3.Dot(m_EntryData[num].triggerEnterDirection, tank.rbody.velocity.normalized) > 0f)
			{
				OnCheckpointPassed.Send(this, tank);
			}
			m_EntryData.RemoveAt(num);
		}
	}

	private Tank GetTechFromCollider(Collider collider)
	{
		Visible visible = Visible.FindVisibleUpwards(collider);
		Tank result = null;
		if (visible != null)
		{
			if (visible.type == ObjectTypes.Vehicle)
			{
				result = visible.tank;
			}
			else if (visible.type == ObjectTypes.Block)
			{
				result = visible.block.tank;
			}
		}
		return result;
	}

	private void OnTriggerEvent(TriggerCatcher.Interaction triggerType, Collider collider)
	{
		if (collider.isTrigger)
		{
			return;
		}
		switch (triggerType)
		{
		case TriggerCatcher.Interaction.Enter:
		{
			Tank techFromCollider2 = GetTechFromCollider(collider);
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
			Tank techFromCollider = GetTechFromCollider(collider);
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
		m_Transform = base.transform;
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
		base.Update();
		if (m_PassType != CheckpointPassType.PassCentre || m_EntryData.Count <= 0)
		{
			return;
		}
		for (int num = m_EntryData.Count - 1; num >= 0; num--)
		{
			Tank tank = m_EntryData[num].tank;
			if (tank == null || !tank.visible.isActive)
			{
				m_EntryData.RemoveAt(num);
			}
			else
			{
				Vector3 vector = m_Transform.position - tank.boundsCentreWorld;
				if (Vector3.Dot(m_EntryData[num].triggerEnterDirection, vector.normalized) < 0f)
				{
					m_EntryData.RemoveAt(num);
					OnCheckpointPassed.Send(this, tank);
					break;
				}
			}
		}
	}
}
