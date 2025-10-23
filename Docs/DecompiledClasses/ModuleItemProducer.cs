#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Networking;

public class ModuleItemProducer : Module, INetworkedModule
{
	[Flags]
	public enum OperateConditionFlags
	{
		Anchored = 1,
		ResourceReservoir = 2
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public bool isProducing;

		public float produceTime;

		public float lastDispenseTime;

		public float timeLastLive;

		public int numQueuedUpItems;
	}

	[SerializeField]
	private ChunkTypes[] m_CompatibleChunkTypes = new ChunkTypes[1];

	[SerializeField]
	[EnumFlag]
	private OperateConditionFlags m_OperationMode;

	[Tooltip("Number of seconds it takes to produce a single output item, modified by ResourceReservoir mine speed")]
	[SerializeField]
	private float m_SecPerItemProduced = 1f;

	[SerializeField]
	private float m_MinDispenseInterval;

	[SerializeField]
	private Transform m_ItemSpawnTransform;

	[SerializeField]
	private float m_ItemSpawnVelocity;

	[SerializeField]
	private Vector3 m_ItemSpawnAngularVelocity;

	[SerializeField]
	private ParticleSystem m_ItemSpawnEffect;

	[SerializeField]
	private string m_DialAnimationName;

	[SerializeField]
	private int m_DialAnimatorLayer = 1;

	[SerializeField]
	private bool m_ProduceWhileUnloaded = true;

	[SerializeField]
	private Vector3 m_ResourceSiphonPoint;

	[SerializeField]
	private float m_ResourceGroundRadius;

	[SerializeField]
	private TechAudio.SFXType m_OperatingSFXType;

	[SerializeField]
	private TechAudio.SFXType m_ProduceSFXType;

	[SerializeField]
	private float m_ProduceSFXDelay;

	private ModuleItemHolder m_Holder;

	private ModuleAnimator m_AnimatorController;

	private ModuleAudioProvider m_AudioProvider;

	private ResourceReservoir m_TargetResourceReservoir;

	private bool m_IsProducing;

	private float m_ProduceTime;

	private float m_LastDispenseTime;

	private int m_NumQueuedUpItems;

	private NetworkedProperty<IntParamBlockMessage> m_SyncedState;

	private float m_NetClientFullness01;

	private AnimatorBool k_IsProducingAnimatorBool = new AnimatorBool("IsProducing");

	private static readonly Bitfield<ObjectTypes> k_MaskScenery = new Bitfield<ObjectTypes>(new ObjectTypes[1] { ObjectTypes.Scenery });

	public bool IsProducing => m_IsProducing;

	private bool CanProduceOutput()
	{
		if ((m_OperationMode & OperateConditionFlags.Anchored) != 0 && (base.block.tank == null || !base.block.tank.IsAnchored))
		{
			return false;
		}
		if ((m_OperationMode & OperateConditionFlags.ResourceReservoir) != 0 && (m_TargetResourceReservoir.IsNull() || m_TargetResourceReservoir.IsEmpty))
		{
			return false;
		}
		return true;
	}

	private void DispenseItem(ItemTypeInfo dispensableItem)
	{
		Vector3 position = m_ItemSpawnTransform.position;
		Visible visible = null;
		if (ManNetwork.IsHost)
		{
			visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(dispensableItem, m_ItemSpawnTransform.position, m_ItemSpawnTransform.rotation);
			if ((bool)visible)
			{
				visible.centrePosition = position;
				if (m_Holder != null)
				{
					m_Holder.SingleStack.Take(visible, force: true);
				}
				else
				{
					visible.rbody.velocity = m_ItemSpawnTransform.forward * m_ItemSpawnVelocity;
					visible.rbody.angularVelocity = m_ItemSpawnAngularVelocity;
				}
				if (base.block.tank != null && base.block.tank.IsFriendly(0) && dispensableItem.ObjectType == ObjectTypes.Chunk)
				{
					Singleton.Manager<ManStats>.inst.ResourceHarvested(base.block, (ChunkTypes)dispensableItem.ItemType);
				}
			}
			else
			{
				d.LogErrorFormat(this, "ModuleItemProducer.DispenseItem Failed to spawn: {0}", dispensableItem.name);
			}
		}
		if (visible != null || !ManNetwork.IsHost)
		{
			if ((bool)m_ItemSpawnEffect)
			{
				m_ItemSpawnEffect.transform.Spawn(position);
			}
			TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, m_ProduceSFXType);
			base.block.tank.TechAudio.PlayOneshot(data);
		}
	}

	private void SetIsProducing(bool isProducing)
	{
		if (isProducing && !m_IsProducing)
		{
			m_ProduceTime = 0f;
		}
		m_IsProducing = isProducing;
		if (m_AnimatorController != null)
		{
			m_AnimatorController.Set(k_IsProducingAnimatorBool, isProducing);
			UpdateReservoirDial();
		}
	}

	private ResourceReservoir GetClosestResourceReservoirInRange()
	{
		ResourceReservoir result = null;
		float num = m_ResourceGroundRadius * m_ResourceGroundRadius;
		Vector3 scenePos = base.transform.TransformPoint(m_ResourceSiphonPoint);
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(scenePos, m_ResourceGroundRadius, k_MaskScenery, includeTriggers: true))
		{
			float sqrMagnitude = (item.centrePosition - base.block.visible.centrePosition).SetY(0f).sqrMagnitude;
			if (!(sqrMagnitude < num) || !(item.resdisp.ResourceReservoir != null))
			{
				continue;
			}
			bool flag = false;
			for (int i = 0; i < m_CompatibleChunkTypes.Length; i++)
			{
				if (m_CompatibleChunkTypes[i] == item.resdisp.ResourceReservoir.ResourceType)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				result = item.resdisp.ResourceReservoir;
				num = sqrMagnitude;
			}
		}
		return result;
	}

	private float GetTimePerItem()
	{
		float num = m_SecPerItemProduced;
		if (m_TargetResourceReservoir.IsNotNull())
		{
			num /= m_TargetResourceReservoir.ExtractionSpeedMultiplier;
		}
		return num;
	}

	private float GetReservoirFullness()
	{
		if (ManNetwork.IsHost)
		{
			return m_TargetResourceReservoir.IsNotNull() ? m_TargetResourceReservoir.GetRemainingFraction() : 0f;
		}
		return m_NetClientFullness01;
	}

	private void UpdateReservoirDial()
	{
		if (m_AnimatorController != null && !m_DialAnimationName.NullOrEmpty())
		{
			float reservoirFullness = GetReservoirFullness();
			m_AnimatorController.Animator.Play(m_DialAnimationName, m_DialAnimatorLayer, reservoirFullness);
		}
	}

	private void OnAttached()
	{
		if ((m_OperationMode & (OperateConditionFlags.Anchored | OperateConditionFlags.ResourceReservoir)) == 0)
		{
			SetIsProducing(isProducing: true);
		}
	}

	private void OnDetaching()
	{
		SetIsProducing(isProducing: false);
	}

	private void OnAnchorStatusChanged(ModuleAnchor anchor)
	{
		if ((m_OperationMode & OperateConditionFlags.ResourceReservoir) != 0)
		{
			m_TargetResourceReservoir = (anchor.IsAnchored ? GetClosestResourceReservoirInRange() : null);
			m_NetClientFullness01 = 0f;
			SetIsProducing(m_TargetResourceReservoir.IsNotNull());
		}
		else if ((m_OperationMode & OperateConditionFlags.Anchored) != 0)
		{
			SetIsProducing(anchor.IsAnchored);
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.isProducing = m_IsProducing;
			serialData.produceTime = m_ProduceTime;
			serialData.lastDispenseTime = m_LastDispenseTime;
			serialData.timeLastLive = ((m_IsProducing && m_ProduceWhileUnloaded) ? Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() : (-1f));
			serialData.numQueuedUpItems = m_NumQueuedUpItems;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_IsProducing = serialData2.isProducing;
			m_ProduceTime = serialData2.produceTime;
			m_LastDispenseTime = serialData2.lastDispenseTime;
			m_NumQueuedUpItems = serialData2.numQueuedUpItems;
			if (m_IsProducing && m_ProduceWhileUnloaded && serialData2.timeLastLive > 0f)
			{
				float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() - serialData2.timeLastLive;
				m_ProduceTime += num;
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleItemProducer;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		m_SyncedState.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_SyncedState.Deserialise(reader);
	}

	private int PackNetState(bool isProducing, float reservoirFullness01)
	{
		int num = (isProducing ? 1 : 0);
		int num2 = (int)(reservoirFullness01 * 100f);
		return num | (num2 << 1);
	}

	private void UnpackNetState(int packedState, out bool isProducing, out float reservoirFullness01)
	{
		isProducing = (packedState & 1) != 0;
		int num = packedState >> 1;
		d.Assert(num >= 0 && num <= 100);
		reservoirFullness01 = (float)num * 0.01f;
	}

	private void SyncToClients()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			int num = PackNetState(m_IsProducing, GetReservoirFullness());
			if (num != m_SyncedState.Data.value)
			{
				m_SyncedState.Data.value = num;
				m_SyncedState.Sync();
			}
		}
	}

	private void OnMPStateSet(IntParamBlockMessage msg)
	{
		UnpackNetState(msg.value, out var isProducing, out m_NetClientFullness01);
		isProducing = isProducing && base.block.IsAttached;
		SetIsProducing(isProducing);
	}

	private void OnPool()
	{
		m_Holder = GetComponent<ModuleItemHolder>();
		m_AnimatorController = GetComponent<ModuleAnimator>();
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
		ModuleAnchor component = GetComponent<ModuleAnchor>();
		if (component != null)
		{
			component.AnchorEvent.Subscribe(OnAnchorStatusChanged);
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_SyncedState = new NetworkedProperty<IntParamBlockMessage>(this, TTMsgType.SetItemProducerState, OnMPStateSet);
	}

	private void OnRecycle()
	{
		SetIsProducing(isProducing: false);
		m_TargetResourceReservoir = null;
		m_NetClientFullness01 = 0f;
		m_ProduceTime = 0f;
		m_LastDispenseTime = 0f;
		m_NumQueuedUpItems = 0;
	}

	private void OnUpdate()
	{
		if (!base.block.IsAttached)
		{
			return;
		}
		if (ManNetwork.IsHost)
		{
			if (m_IsProducing && CanProduceOutput())
			{
				float timePerItem = GetTimePerItem();
				if (timePerItem > 0f)
				{
					while (m_ProduceTime >= timePerItem)
					{
						m_NumQueuedUpItems++;
						m_ProduceTime -= timePerItem;
					}
				}
				m_ProduceTime += Time.deltaTime;
				float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
				if (m_NumQueuedUpItems > 0 && currentModeRunningTime >= m_LastDispenseTime + m_MinDispenseInterval && (m_Holder == null || m_Holder.IsEmpty))
				{
					ChunkTypes itemType;
					if (m_TargetResourceReservoir.IsNotNull())
					{
						itemType = m_TargetResourceReservoir.TakeResource();
						UpdateReservoirDial();
					}
					else
					{
						int num = UnityEngine.Random.Range(0, m_CompatibleChunkTypes.Length);
						itemType = m_CompatibleChunkTypes[num];
					}
					ItemTypeInfo dispensableItem = new ItemTypeInfo(ObjectTypes.Chunk, (int)itemType);
					DispenseItem(dispensableItem);
					m_NumQueuedUpItems--;
					m_LastDispenseTime = currentModeRunningTime;
				}
			}
			else
			{
				SetIsProducing(isProducing: false);
				m_NumQueuedUpItems = 0;
			}
			SyncToClients();
		}
		if (m_AudioProvider != null)
		{
			m_AudioProvider.SetNoteOn(m_OperatingSFXType, m_IsProducing);
		}
	}
}
