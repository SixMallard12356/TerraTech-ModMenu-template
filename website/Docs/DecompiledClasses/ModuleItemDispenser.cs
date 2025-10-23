#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleItemHolder))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleItemDispenser : Module
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public ItemTypeInfo itemType;

		public int remaining;
	}

	[SerializeField]
	private ItemTypeInfo m_ItemToDispense;

	[SerializeField]
	private int defaultAmount;

	[SerializeField]
	private float dispenseInterval = 1f;

	[SerializeField]
	private ParticleSystem m_SpawnEffect;

	public Event<Visible> VisibleSpawnedEvent;

	private ModuleItemHolder m_Holder;

	private float m_DispenseTimer;

	public int amountRemaining { get; set; }

	public void ReturnItem(Visible visible)
	{
		float num = 3f;
		visible.trans.position = base.block.trans.position + Vector3.up * num;
		m_Holder.SingleStack.Take(visible);
	}

	private void OnAttached()
	{
		base.block.tank.SetInvulnerable(invulnerable: true, forever: true);
		base.block.tank.ShouldShowOverlay = false;
	}

	private void Dispense()
	{
		Vector3 vector = m_Holder.SingleStack.BasePosWorld() + Vector3.up * 2f;
		Visible visible = null;
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock((BlockTypes)m_ItemToDispense.ItemType, vector, Quaternion.identity);
		if (tankBlock != null)
		{
			visible = tankBlock.visible;
		}
		if ((bool)visible)
		{
			visible.centrePosition = vector;
			m_Holder.SingleStack.Take(visible, force: true);
			if ((bool)m_SpawnEffect)
			{
				m_SpawnEffect.transform.Spawn(visible.centrePosition);
			}
			VisibleSpawnedEvent.Send(visible);
		}
		else
		{
			d.LogError("failed to dispense: " + m_ItemToDispense.name);
		}
		m_DispenseTimer = dispenseInterval;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.itemType = m_ItemToDispense;
			serialData.remaining = amountRemaining;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_ItemToDispense = serialData2.itemType;
			amountRemaining = serialData2.remaining;
		}
	}

	public static void AddSerialData(TankPreset.BlockSpec blockSpec, ItemTypeInfo type, int amount)
	{
		SerialData serialData = new SerialData();
		serialData.itemType = type;
		serialData.remaining = amount;
		serialData.Store(blockSpec.saveState);
	}

	private void OnPool()
	{
		m_Holder = GetComponent<ModuleItemHolder>();
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		amountRemaining = defaultAmount;
		m_DispenseTimer = dispenseInterval;
	}

	private void OnRecycle()
	{
		VisibleSpawnedEvent.Clear();
	}

	private void OnUpdate()
	{
		if (!base.block.tank || !m_Holder.IsEmpty || amountRemaining == 0 || !ManNetwork.IsHost)
		{
			return;
		}
		if (m_DispenseTimer > 0f)
		{
			if (m_Holder.IsEmpty)
			{
				m_DispenseTimer -= Time.deltaTime;
			}
		}
		else if (m_ItemToDispense.ObjectType == ObjectTypes.Null)
		{
			d.LogError("Item to dispense is null, emptying");
			amountRemaining = 0;
		}
		else
		{
			Dispense();
			amountRemaining--;
		}
	}
}
