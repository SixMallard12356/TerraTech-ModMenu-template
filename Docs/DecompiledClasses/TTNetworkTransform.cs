#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Networking;

[DisallowMultipleComponent]
public class TTNetworkTransform : NetworkBehaviour
{
	public enum TransformSyncMode
	{
		SyncNone,
		SyncTransform,
		SyncRigidbody3D,
		SyncNetBlockChunk
	}

	public enum AxisSyncMode
	{
		None,
		AxisX,
		AxisY,
		AxisZ,
		AxisXY,
		AxisXZ,
		AxisYZ,
		AxisXYZ
	}

	public enum CompressionSyncMode
	{
		None,
		Low,
		High
	}

	[SerializeField]
	private TransformSyncMode m_TransformSyncMode;

	[SerializeField]
	private float m_SendInterval = 0.1f;

	[SerializeField]
	private AxisSyncMode m_SyncRotationAxis = AxisSyncMode.AxisXYZ;

	[SerializeField]
	private CompressionSyncMode m_RotationSyncCompression;

	[SerializeField]
	private bool m_SyncSpin;

	[SerializeField]
	private float m_MovementTheshold = 0.001f;

	[SerializeField]
	private float m_VelocityThreshold = 0.0001f;

	[SerializeField]
	private float m_SnapThreshold = 5f;

	[SerializeField]
	private float m_InterpolateRotation = 1f;

	[SerializeField]
	private float m_InterpolateMovement = 1f;

	private NetBlockChunk m_NetBlock;

	private Rigidbody m_RigidBody3D;

	private bool m_Grounded = true;

	private bool m_needUpdatePosition;

	private bool m_needUpdateVelocity;

	private bool m_needUpdateRotation;

	private bool m_needUpdateAngularVelocity;

	private bool m_firstTimeInterpolationUpdate = true;

	private int m_forceResetFrameNumber = -1;

	private WorldPosition m_TargetSyncPosition;

	private Vector3 m_TargetSyncVelocity;

	private Vector3 m_FixedPosDiff;

	private Quaternion m_TargetSyncRotation3D;

	private Vector3 m_TargetSyncAngularVelocity3D;

	private float m_LastClientSyncTime;

	private float m_LastClientSendTime;

	private float m_ServerSendTimeout;

	private Vector3 m_PrevPosition;

	private Quaternion m_PrevRotation;

	private float m_PrevSqrVelocity;

	private const float k_LocalMovementSqrThreshold = 1.0000001E-06f;

	private const float k_LocalRotationAngleThreshold = 0.01f;

	private const float k_LocalVelocitySqrThreshold = 0.0001f;

	private const float k_MoveAheadRatio = 1f;

	private NetworkWriter m_LocalTransformWriter;

	private GameObject m_GameObject;

	private bool m_IsRecycled;

	private uint m_Sequence;

	private const uint SENTINEL_VALUE = 199u;

	private const float MAX_TIME_BETWEEN_FORCED_SEND_UPDATES = 0.2f;

	public NetworkIdentity NetIdentity { get; private set; }

	private bool hasNetBlockRigidbody
	{
		get
		{
			if (m_NetBlock.IsNotNull())
			{
				return m_NetBlock.rbody != null;
			}
			return false;
		}
	}

	private Rigidbody netBlockRigidbody
	{
		get
		{
			if (!hasNetBlockRigidbody)
			{
				d.LogError("Missing rigidbody for blockPoolID " + m_NetBlock.BlockPoolID + " and type " + m_NetBlock.GetDebugTypeName());
			}
			return m_NetBlock.rbody;
		}
	}

	public TransformSyncMode transformSyncMode
	{
		get
		{
			return m_TransformSyncMode;
		}
		set
		{
			m_TransformSyncMode = value;
		}
	}

	public float sendInterval
	{
		get
		{
			return m_SendInterval;
		}
		set
		{
			m_SendInterval = value;
		}
	}

	public AxisSyncMode syncRotationAxis
	{
		get
		{
			return m_SyncRotationAxis;
		}
		set
		{
			m_SyncRotationAxis = value;
		}
	}

	public CompressionSyncMode rotationSyncCompression
	{
		get
		{
			return m_RotationSyncCompression;
		}
		set
		{
			m_RotationSyncCompression = value;
		}
	}

	public bool syncSpin
	{
		get
		{
			return m_SyncSpin;
		}
		set
		{
			m_SyncSpin = value;
		}
	}

	public float movementTheshold
	{
		get
		{
			return m_MovementTheshold;
		}
		set
		{
			m_MovementTheshold = value;
		}
	}

	public float velocityThreshold
	{
		get
		{
			return m_VelocityThreshold;
		}
		set
		{
			m_VelocityThreshold = value;
		}
	}

	public float snapThreshold
	{
		get
		{
			return m_SnapThreshold;
		}
		set
		{
			m_SnapThreshold = value;
		}
	}

	public float interpolateRotation
	{
		get
		{
			return m_InterpolateRotation;
		}
		set
		{
			m_InterpolateRotation = value;
		}
	}

	public float interpolateMovement
	{
		get
		{
			return m_InterpolateMovement;
		}
		set
		{
			m_InterpolateMovement = value;
		}
	}

	public Rigidbody rigidbody3D => m_RigidBody3D;

	public float lastSyncTime => m_LastClientSyncTime;

	public Vector3 targetSyncVelocity => m_TargetSyncVelocity;

	public Quaternion targetSyncRotation3D => m_TargetSyncRotation3D;

	public bool grounded
	{
		get
		{
			return m_Grounded;
		}
		set
		{
			m_Grounded = value;
		}
	}

	public int forceResetFrameNumber()
	{
		return m_forceResetFrameNumber;
	}

	private void OnValidate()
	{
		if (m_TransformSyncMode < TransformSyncMode.SyncNone || m_TransformSyncMode > TransformSyncMode.SyncNetBlockChunk)
		{
			m_TransformSyncMode = TransformSyncMode.SyncTransform;
		}
		if (m_SendInterval < 0f)
		{
			m_SendInterval = 0f;
		}
		if (m_SyncRotationAxis < AxisSyncMode.None || m_SyncRotationAxis > AxisSyncMode.AxisXYZ)
		{
			m_SyncRotationAxis = AxisSyncMode.None;
		}
		if (m_MovementTheshold < 0f)
		{
			m_MovementTheshold = 0.001f;
		}
		if (m_VelocityThreshold < 0f)
		{
			m_VelocityThreshold = 0.0001f;
		}
		if (m_SnapThreshold < 0f)
		{
			m_SnapThreshold = 0.01f;
		}
		if (m_InterpolateRotation < 0f)
		{
			m_InterpolateRotation = 0.01f;
		}
		if (m_InterpolateMovement < 0f)
		{
			m_InterpolateMovement = 0.01f;
		}
	}

	private void Awake()
	{
		m_NetBlock = GetComponent<NetBlockChunk>();
		m_RigidBody3D = GetComponent<Rigidbody>();
		m_PrevPosition = base.transform.position;
		m_PrevRotation = base.transform.rotation;
		m_PrevSqrVelocity = 0f;
		if (base.localPlayerAuthority)
		{
			m_LocalTransformWriter = new NetworkWriter();
		}
		m_GameObject = base.gameObject;
	}

	public override void OnStartServer()
	{
		m_LastClientSyncTime = 0f;
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		if (!base.hasAuthority && transformSyncMode == TransformSyncMode.SyncRigidbody3D)
		{
			base.transform.rotation = rigidbody3D.rotation;
			base.transform.position = rigidbody3D.position;
		}
		if (!base.hasAuthority && transformSyncMode == TransformSyncMode.SyncNetBlockChunk && hasNetBlockRigidbody)
		{
			base.transform.rotation = netBlockRigidbody.rotation;
			base.transform.position = netBlockRigidbody.position;
		}
		m_LastClientSyncTime = 0f;
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		bool flag = base.syncVarDirtyBits != 0;
		if (flag && m_NetBlock.IsNotNull() && m_NetBlock.IsInHolder && m_NetBlock.visible.IsNotNull() && m_NetBlock.visible.holderStack != null && m_NetBlock.visible.holderStack.NetClientsUpdateItemPos)
		{
			flag = false;
		}
		uint value = (flag ? 1u : 0u);
		writer.WritePackedUInt32(value);
		if (!(initialState || flag))
		{
			return false;
		}
		if (NetIdentity.HasEffectiveAuthority())
		{
			m_Sequence++;
		}
		writer.WritePackedUInt32(m_Sequence);
		writer.WritePackedUInt32(199u);
		bool flag2 = true;
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncNone:
			return false;
		case TransformSyncMode.SyncTransform:
			flag2 = SerializeModeTransform(writer);
			break;
		case TransformSyncMode.SyncRigidbody3D:
			flag2 = SerializeMode3D(writer);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			flag2 = SerializeModeNetBlock(writer, initialState);
			break;
		}
		if (!flag2)
		{
			return false;
		}
		Singleton.Manager<ManNetwork>.inst.RecordSendMetrics(TTMsgType.TTLocalPlayerTransform, writer);
		return true;
	}

	private bool SerializeModeTransform(NetworkWriter writer)
	{
		writer.Write(WorldPosition.FromScenePosition(base.transform.position));
		if (m_SyncRotationAxis != AxisSyncMode.None)
		{
			SerializeRotation3D(writer, base.transform.rotation, syncRotationAxis, rotationSyncCompression);
		}
		m_PrevPosition = base.transform.position;
		m_PrevRotation = base.transform.rotation;
		m_PrevSqrVelocity = 0f;
		return true;
	}

	private bool VerifySerializeComponentExists()
	{
		bool flag = false;
		Type type = null;
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncRigidbody3D:
			if (!m_RigidBody3D)
			{
				flag = true;
				type = typeof(Rigidbody);
			}
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			if (!m_NetBlock)
			{
				flag = true;
				type = typeof(NetBlockChunk);
			}
			break;
		}
		if (flag && type != null)
		{
			int num = -1;
			NetBlockChunk component = base.gameObject.GetComponent<NetBlockChunk>();
			if (component != null && component.visible != null)
			{
				num = component.visible.ID;
			}
			d.LogError($"Error: transformSyncMode Name={base.gameObject.name} NetId={base.netId} VisibleID={num} set to {transformSyncMode} but {type.Name} component is missing/null/destroyed.  IsRecycled={m_IsRecycled}");
			return false;
		}
		return true;
	}

	private bool SerializeMode3D(NetworkWriter writer)
	{
		return SerializeRigidbody(writer, m_RigidBody3D);
	}

	private bool SerializeModeNetBlock(NetworkWriter writer, bool initialState)
	{
		if (!hasNetBlockRigidbody)
		{
			if (m_NetBlock.visible.IsNotNull())
			{
				return SerializeFakeRigidbodyData(writer, m_NetBlock.visible);
			}
			return false;
		}
		return SerializeRigidbody(writer, netBlockRigidbody);
	}

	private bool SerializeFakeRigidbodyData(NetworkWriter writer, Visible visible)
	{
		writer.Write(WorldPosition.FromScenePosition(visible.trans.position));
		SerializeVelocity3D(writer, Vector3.zero, CompressionSyncMode.None);
		if (syncRotationAxis != AxisSyncMode.None)
		{
			SerializeRotation3D(writer, visible.trans.rotation, syncRotationAxis, rotationSyncCompression);
		}
		if (m_SyncSpin)
		{
			SerializeSpin3D(writer, Vector3.zero, syncRotationAxis, rotationSyncCompression);
		}
		return true;
	}

	private bool SerializeRigidbody(NetworkWriter writer, Rigidbody rb)
	{
		if (!VerifySerializeComponentExists())
		{
			return false;
		}
		m_firstTimeInterpolationUpdate = true;
		if (base.isServer && m_LastClientSyncTime != 0f)
		{
			writer.Write(m_TargetSyncPosition);
			SerializeVelocity3D(writer, m_TargetSyncVelocity, CompressionSyncMode.None);
			if (syncRotationAxis != AxisSyncMode.None)
			{
				SerializeRotation3D(writer, m_TargetSyncRotation3D, syncRotationAxis, rotationSyncCompression);
			}
		}
		else
		{
			WorldPosition worldPos = WorldPosition.FromScenePosition(rb.position);
			writer.Write(worldPos);
			SerializeVelocity3D(writer, rb.velocity, CompressionSyncMode.None);
			if (syncRotationAxis != AxisSyncMode.None)
			{
				SerializeRotation3D(writer, rb.rotation, syncRotationAxis, rotationSyncCompression);
			}
			m_TargetSyncPosition = WorldPosition.FromScenePosition(rb.position);
			m_TargetSyncRotation3D = rb.rotation;
			m_TargetSyncVelocity = rb.velocity;
		}
		if (m_SyncSpin)
		{
			SerializeSpin3D(writer, rb.angularVelocity, syncRotationAxis, rotationSyncCompression);
		}
		m_PrevPosition = rb.position;
		m_PrevRotation = base.transform.rotation;
		m_PrevSqrVelocity = rb.velocity.sqrMagnitude;
		return true;
	}

	private bool _validateSequence(NetworkReader reader)
	{
		uint num = reader.ReadPackedUInt32();
		bool result;
		if (!NetIdentity.HasEffectiveAuthority())
		{
			if (num > m_Sequence)
			{
				result = true;
				m_Sequence = num;
			}
			else
			{
				result = false;
			}
		}
		else
		{
			result = false;
		}
		if (reader.ReadPackedUInt32() != 199)
		{
			result = false;
		}
		return result;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (base.isServer && NetworkServer.localClientActive)
		{
			return;
		}
		uint num = reader.ReadPackedUInt32();
		if (!initialState && num == 0)
		{
			return;
		}
		bool flag = _validateSequence(reader);
		if (m_IsRecycled || !flag)
		{
			switch (transformSyncMode)
			{
			case TransformSyncMode.SyncTransform:
				SkipTransformData(reader);
				break;
			case TransformSyncMode.SyncRigidbody3D:
			case TransformSyncMode.SyncNetBlockChunk:
				SkipRigidbodyData(reader);
				break;
			}
			return;
		}
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncNone:
			return;
		case TransformSyncMode.SyncTransform:
			UnserializeModeTransform(reader, initialState);
			break;
		case TransformSyncMode.SyncRigidbody3D:
			UnserializeMode3D(reader, initialState);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			UnserializeModeNetBlock(reader, initialState);
			break;
		}
		m_LastClientSyncTime = Time.time;
	}

	private void UnserializeModeTransform(NetworkReader reader, bool initialState)
	{
		if (base.hasAuthority)
		{
			SkipTransformData(reader);
			return;
		}
		base.transform.position = reader.ReadWorldPosition().ScenePosition;
		if (syncRotationAxis != AxisSyncMode.None)
		{
			base.transform.rotation = UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
		}
	}

	private void SkipWorldPosition(NetworkReader reader)
	{
		reader.ReadPackedInt32();
		reader.ReadPackedInt32();
		reader.ReadVector3();
	}

	private void SkipTransformData(NetworkReader reader)
	{
		SkipWorldPosition(reader);
		if (syncRotationAxis != AxisSyncMode.None)
		{
			UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
		}
	}

	private void SkipRigidbodyData(NetworkReader reader)
	{
		SkipWorldPosition(reader);
		reader.ReadVector3();
		if (syncRotationAxis != AxisSyncMode.None)
		{
			UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
		}
		if (syncSpin)
		{
			UnserializeSpin3D(reader, syncRotationAxis, rotationSyncCompression);
		}
	}

	private void UnserializeModeNetBlock(NetworkReader reader, bool initialState)
	{
		if (m_NetBlock.IsAcceptingUpdates && hasNetBlockRigidbody)
		{
			UnserializeRigidbody3D(reader, initialState, netBlockRigidbody);
		}
		else
		{
			UnserializeRigidbody3D(reader, initialState, null);
		}
		if (initialState && m_NetBlock.IsAcceptingUpdates && m_NetBlock.visible.IsNotNull())
		{
			m_NetBlock.visible.trans.position = m_TargetSyncPosition.ScenePosition;
			m_NetBlock.visible.trans.rotation = m_TargetSyncRotation3D;
		}
	}

	private void UnserializeMode3D(NetworkReader reader, bool initialState)
	{
		UnserializeRigidbody3D(reader, initialState, m_RigidBody3D);
	}

	private void UnserializeRigidbody3D(NetworkReader reader, bool initialState, Rigidbody rb)
	{
		if (base.hasAuthority || rb == null)
		{
			SkipRigidbodyData(reader);
			return;
		}
		m_TargetSyncPosition = reader.ReadWorldPosition();
		m_TargetSyncVelocity = reader.ReadVector3();
		if (syncRotationAxis != AxisSyncMode.None)
		{
			m_TargetSyncRotation3D = UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
		}
		if (syncSpin)
		{
			m_TargetSyncAngularVelocity3D = UnserializeSpin3D(reader, syncRotationAxis, rotationSyncCompression);
		}
		if (rb == null)
		{
			return;
		}
		float num = 0f;
		if (m_LastClientSyncTime > 0f && !m_firstTimeInterpolationUpdate && !base.isServer)
		{
			num = Time.time - m_LastClientSyncTime;
			if (num > 2f)
			{
				num = 2f;
			}
			Vector3 delta = m_TargetSyncVelocity * num * 1f;
			m_TargetSyncPosition = WorldPosition.OffsetPosition(in m_TargetSyncPosition, in delta);
		}
		if (base.isServer && !base.isClient)
		{
			m_needUpdatePosition = true;
			m_needUpdateRotation = true;
			m_needUpdateVelocity = true;
			if (syncSpin)
			{
				m_needUpdateAngularVelocity = true;
			}
			m_firstTimeInterpolationUpdate = false;
			return;
		}
		if (GetNetworkSendInterval() == 0f)
		{
			m_needUpdatePosition = true;
			m_needUpdateVelocity = true;
			if (syncRotationAxis != AxisSyncMode.None)
			{
				m_needUpdateRotation = true;
			}
			if (syncSpin)
			{
				m_needUpdateAngularVelocity = true;
			}
			m_firstTimeInterpolationUpdate = false;
			return;
		}
		if ((rb.position - m_TargetSyncPosition.ScenePosition).magnitude > snapThreshold || m_firstTimeInterpolationUpdate)
		{
			m_needUpdatePosition = true;
			m_needUpdateVelocity = true;
			if (syncRotationAxis != AxisSyncMode.None)
			{
				m_needUpdateRotation = true;
			}
			if (syncSpin)
			{
				m_needUpdateAngularVelocity = true;
			}
			_ = m_firstTimeInterpolationUpdate;
			_updateTargetSyncValues();
		}
		else
		{
			if (interpolateRotation == 0f && syncRotationAxis != AxisSyncMode.None)
			{
				m_needUpdateRotation = true;
				if (syncSpin)
				{
					m_needUpdateAngularVelocity = true;
				}
			}
			if (m_InterpolateMovement == 0f)
			{
				m_needUpdatePosition = true;
				m_needUpdateVelocity = true;
			}
			if (initialState && syncRotationAxis != AxisSyncMode.None)
			{
				m_needUpdateRotation = true;
			}
		}
		m_firstTimeInterpolationUpdate = false;
	}

	private void FixedUpdate()
	{
		_updateTargetSyncValues();
		if (base.isServer)
		{
			FixedUpdateServer();
		}
		if (base.isClient)
		{
			FixedUpdateClient();
		}
	}

	private void _updateTargetSyncValues()
	{
		if (m_needUpdatePosition || m_needUpdateRotation || m_needUpdateAngularVelocity || m_needUpdateVelocity)
		{
			Rigidbody rigidbody = null;
			rigidbody = ((transformSyncMode != TransformSyncMode.SyncNetBlockChunk) ? m_RigidBody3D : ((m_NetBlock.IsNotNull() && m_NetBlock.IsAcceptingUpdates) ? netBlockRigidbody : null));
			if (rigidbody.IsNotNull())
			{
				m_forceResetFrameNumber = Time.frameCount;
				if (m_needUpdatePosition)
				{
					rigidbody.MovePosition(m_TargetSyncPosition.ScenePosition);
					m_needUpdatePosition = false;
				}
				if (m_needUpdateRotation)
				{
					rigidbody.MoveRotation(m_TargetSyncRotation3D);
					m_needUpdateRotation = false;
					if (!m_needUpdateAngularVelocity)
					{
						rigidbody.angularVelocity = Vector3.zero;
					}
				}
				if (m_needUpdateVelocity)
				{
					if (m_InterpolateMovement == 0f)
					{
						rigidbody.velocity = m_TargetSyncVelocity;
					}
					m_needUpdateVelocity = false;
				}
				if (m_needUpdateAngularVelocity)
				{
					rigidbody.angularVelocity = m_TargetSyncAngularVelocity3D;
					m_needUpdateAngularVelocity = false;
				}
			}
		}
		d.Assert(!m_needUpdatePosition);
		d.Assert(!m_needUpdateRotation);
		d.Assert(!m_needUpdateVelocity);
		d.Assert(!m_needUpdateAngularVelocity);
	}

	private void FixedUpdateServer()
	{
		if (base.syncVarDirtyBits != 0 || !NetworkServer.active || !base.isServer || GetNetworkSendInterval() == 0f || (m_NetBlock.IsNotNull() && m_NetBlock.IsInHolder && m_NetBlock.visible.IsNotNull() && m_NetBlock.visible.holderStack != null && m_NetBlock.visible.holderStack.NetClientsUpdateItemPos))
		{
			return;
		}
		if (!CheckVelocityChanged() && (base.transform.position - m_PrevPosition).sqrMagnitude < m_MovementTheshold * m_MovementTheshold && Quaternion.Angle(m_PrevRotation, base.transform.rotation) < 0.01f)
		{
			m_ServerSendTimeout -= Time.deltaTime;
			if (m_ServerSendTimeout > 0f)
			{
				return;
			}
		}
		SetDirtyBit(1u);
		m_ServerSendTimeout = GetNetworkSendInterval();
	}

	private bool CheckVelocityChanged()
	{
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncRigidbody3D:
			if ((bool)m_RigidBody3D && m_VelocityThreshold > 0f)
			{
				return Mathf.Abs(m_RigidBody3D.velocity.sqrMagnitude - m_PrevSqrVelocity) >= m_VelocityThreshold;
			}
			return false;
		case TransformSyncMode.SyncNetBlockChunk:
			if (hasNetBlockRigidbody && m_VelocityThreshold > 0f)
			{
				return Mathf.Abs(netBlockRigidbody.velocity.sqrMagnitude - m_PrevSqrVelocity) >= m_VelocityThreshold;
			}
			return false;
		default:
			return false;
		}
	}

	private void FixedUpdateClient()
	{
		if (m_LastClientSyncTime == 0f || (!NetworkServer.active && !NetworkClient.active) || (!base.isServer && !base.isClient) || GetNetworkSendInterval() == 0f || base.hasAuthority)
		{
			return;
		}
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncNone:
			break;
		case TransformSyncMode.SyncTransform:
			break;
		case TransformSyncMode.SyncRigidbody3D:
			InterpolateTransformMode3D(m_RigidBody3D);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			if (hasNetBlockRigidbody)
			{
				if (m_NetBlock.visible.holderStack == null)
				{
					InterpolateTransformMode3D(netBlockRigidbody);
					break;
				}
				m_TargetSyncVelocity = netBlockRigidbody.velocity;
				m_TargetSyncPosition = WorldPosition.FromScenePosition(netBlockRigidbody.position);
				m_TargetSyncRotation3D = netBlockRigidbody.rotation;
				m_TargetSyncAngularVelocity3D = netBlockRigidbody.angularVelocity;
			}
			break;
		}
	}

	private void InterpolateTransformMode3D(Rigidbody rb)
	{
		if (m_InterpolateMovement != 0f)
		{
			float num = ((GetNetworkSendInterval() == 0f) ? 1f : GetNetworkSendInterval());
			float num2 = Mathf.Clamp01(Time.fixedDeltaTime / num) * 0.8f;
			Vector3 vector = (m_TargetSyncPosition.ScenePosition - rb.position) * num2;
			Vector3 position = rb.position + vector;
			rb.MovePosition(position);
			rb.velocity = m_TargetSyncVelocity;
			m_forceResetFrameNumber = Time.frameCount;
		}
		if (interpolateRotation != 0f)
		{
			float num3 = ((GetNetworkSendInterval() == 0f) ? 1f : GetNetworkSendInterval());
			float t = Mathf.Clamp01(Time.fixedDeltaTime / num3);
			Quaternion rot = Quaternion.Slerp(rb.rotation, m_TargetSyncRotation3D, t);
			rb.MoveRotation(rot);
			m_forceResetFrameNumber = Time.frameCount;
		}
		m_TargetSyncPosition = WorldPosition.OffsetPosition(in m_TargetSyncPosition, m_TargetSyncVelocity * Time.fixedDeltaTime * 1f);
	}

	private void Update()
	{
		if (base.hasAuthority && base.localPlayerAuthority && !NetworkServer.active && (!m_NetBlock.IsNotNull() || (m_NetBlock.GetAuthorityReason != ManNetwork.AuthorityReason.Collision && m_NetBlock.GetAuthorityReason != ManNetwork.AuthorityReason.NoAuthority)))
		{
			SendClientUpdate();
		}
	}

	[Client]
	private void SendClientUpdate()
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void TTNetworkTransform::SendClientUpdate()' called on server");
			return;
		}
		bool forceSend = Time.time - m_LastClientSendTime > 0.2f;
		if (SendTransform(forceSend))
		{
			m_LastClientSendTime = Time.time;
		}
	}

	private bool HasMoved()
	{
		float num = 0f;
		TransformSyncMode transformSyncMode;
		if (m_RigidBody3D != null)
		{
			d.Assert(this.transformSyncMode == TransformSyncMode.SyncRigidbody3D, "TTNetworkTransform - Syncing Rigidbody3D component in wrong mode");
			transformSyncMode = TransformSyncMode.SyncRigidbody3D;
		}
		else if (hasNetBlockRigidbody && m_NetBlock != null)
		{
			d.Assert(this.transformSyncMode == TransformSyncMode.SyncNetBlockChunk, "TTNetworkTransform - Syncing NetBlockChunk component in wrong mode");
			transformSyncMode = TransformSyncMode.SyncNetBlockChunk;
		}
		else
		{
			transformSyncMode = TransformSyncMode.SyncTransform;
		}
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncRigidbody3D:
			num = (m_RigidBody3D.position - m_PrevPosition).sqrMagnitude;
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			num = (netBlockRigidbody.position - m_PrevPosition).sqrMagnitude;
			break;
		case TransformSyncMode.SyncTransform:
			num = (base.transform.position - m_PrevPosition).sqrMagnitude;
			break;
		default:
			d.LogError("Unexpected sourceMode from where to gather positional data!");
			break;
		}
		if (num > 1.0000001E-06f)
		{
			return true;
		}
		float num2 = 0f;
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncRigidbody3D:
			num2 = Quaternion.Angle(m_RigidBody3D.rotation, m_PrevRotation);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			num2 = Quaternion.Angle(netBlockRigidbody.rotation, m_PrevRotation);
			break;
		case TransformSyncMode.SyncTransform:
			num2 = Quaternion.Angle(base.transform.rotation, m_PrevRotation);
			break;
		default:
			d.LogError("Unexpected sourceMode from where to gather rotation data!");
			break;
		}
		if (num2 > 0.01f)
		{
			return true;
		}
		float num3 = 0f;
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncRigidbody3D:
			num3 = Mathf.Abs(m_RigidBody3D.velocity.sqrMagnitude - m_PrevSqrVelocity);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			num3 = Mathf.Abs(netBlockRigidbody.velocity.sqrMagnitude - m_PrevSqrVelocity);
			break;
		default:
			d.LogError("Unexpected sourceMode from where to gather velocity data!");
			break;
		case TransformSyncMode.SyncTransform:
			break;
		}
		if (num3 > 0.0001f)
		{
			return true;
		}
		return false;
	}

	[Client]
	private bool SendTransform(bool forceSend)
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Boolean TTNetworkTransform::SendTransform(System.Boolean)' called on server");
			return false;
		}
		if ((!forceSend && !HasMoved()) || ClientScene.readyConnection == null || !m_GameObject.activeInHierarchy)
		{
			return false;
		}
		m_LocalTransformWriter.StartMessage(112);
		m_LocalTransformWriter.Write(base.netId);
		d.Assert(base.hasAuthority, "Sending Transform without authority");
		m_Sequence++;
		m_LocalTransformWriter.WritePackedUInt32(m_Sequence);
		m_LocalTransformWriter.WritePackedUInt32(199u);
		bool flag = false;
		switch (transformSyncMode)
		{
		case TransformSyncMode.SyncNone:
			return false;
		case TransformSyncMode.SyncTransform:
			flag = SerializeModeTransform(m_LocalTransformWriter);
			break;
		case TransformSyncMode.SyncRigidbody3D:
			flag = SerializeMode3D(m_LocalTransformWriter);
			break;
		case TransformSyncMode.SyncNetBlockChunk:
			flag = SerializeModeNetBlock(m_LocalTransformWriter, initialState: false);
			break;
		}
		if (m_RigidBody3D != null)
		{
			d.Assert(transformSyncMode == TransformSyncMode.SyncRigidbody3D, "TTNetworkTransform - Syncing Rigidbody3D component in wrong mode");
			m_PrevPosition = m_RigidBody3D.position;
			m_PrevRotation = m_RigidBody3D.rotation;
			m_PrevSqrVelocity = m_RigidBody3D.velocity.sqrMagnitude;
		}
		else if (hasNetBlockRigidbody && m_NetBlock.IsNotNull())
		{
			d.Assert(transformSyncMode == TransformSyncMode.SyncNetBlockChunk, "TTNetworkTransform - Syncing NetBlockChunk component in wrong mode");
			m_PrevPosition = netBlockRigidbody.position;
			m_PrevRotation = netBlockRigidbody.rotation;
			m_PrevSqrVelocity = netBlockRigidbody.velocity.sqrMagnitude;
		}
		else
		{
			m_PrevPosition = base.transform.position;
			m_PrevRotation = base.transform.rotation;
			m_PrevSqrVelocity = 0f;
		}
		if (!flag)
		{
			return false;
		}
		m_LocalTransformWriter.FinishMessage();
		Singleton.Manager<ManNetwork>.inst.RecordSendMetrics(TTMsgType.TTLocalPlayerTransform, m_LocalTransformWriter);
		ClientScene.readyConnection.SendWriter(m_LocalTransformWriter, GetNetworkChannel());
		return true;
	}

	public static void HandleTransform(NetworkMessage netMsg)
	{
		NetworkInstanceId item = netMsg.reader.ReadNetworkId();
		GameObject gameObject = NetworkServer.FindLocalObject(item);
		if (gameObject == null)
		{
			return;
		}
		TTNetworkTransform component = gameObject.GetComponent<TTNetworkTransform>();
		if (component == null)
		{
			if (LogFilter.logError)
			{
				d.LogError("HandleTransform null target");
			}
		}
		else if (!component.localPlayerAuthority)
		{
			if (LogFilter.logError)
			{
				d.LogError("HandleTransform no localPlayerAuthority");
			}
		}
		else if (netMsg.conn.clientOwnedObjects == null)
		{
			if (LogFilter.logError)
			{
				d.LogError("HandleTransform object not owned by connection");
			}
		}
		else if (netMsg.conn.clientOwnedObjects.Contains(item) && component._validateSequence(netMsg.reader))
		{
			switch (component.transformSyncMode)
			{
			case TransformSyncMode.SyncNone:
				return;
			case TransformSyncMode.SyncTransform:
				component.UnserializeModeTransform(netMsg.reader, initialState: false);
				break;
			case TransformSyncMode.SyncRigidbody3D:
				component.UnserializeMode3D(netMsg.reader, initialState: false);
				break;
			case TransformSyncMode.SyncNetBlockChunk:
				component.UnserializeModeNetBlock(netMsg.reader, initialState: false);
				break;
			}
			component.m_LastClientSyncTime = Time.time;
		}
	}

	private static void WriteAngle(NetworkWriter writer, float angle, CompressionSyncMode compression)
	{
		switch (compression)
		{
		case CompressionSyncMode.None:
			writer.Write(angle);
			break;
		case CompressionSyncMode.Low:
			writer.Write((short)angle);
			break;
		case CompressionSyncMode.High:
			writer.Write((short)angle);
			break;
		}
	}

	private static float ReadAngle(NetworkReader reader, CompressionSyncMode compression)
	{
		return compression switch
		{
			CompressionSyncMode.None => reader.ReadSingle(), 
			CompressionSyncMode.Low => reader.ReadInt16(), 
			CompressionSyncMode.High => reader.ReadInt16(), 
			_ => 0f, 
		};
	}

	public static void SerializeVelocity3D(NetworkWriter writer, Vector3 velocity, CompressionSyncMode compression)
	{
		writer.Write(velocity);
	}

	public static void SerializeRotation3D(NetworkWriter writer, Quaternion rot, AxisSyncMode mode, CompressionSyncMode compression)
	{
		switch (mode)
		{
		case AxisSyncMode.AxisX:
			WriteAngle(writer, rot.eulerAngles.x, compression);
			break;
		case AxisSyncMode.AxisY:
			WriteAngle(writer, rot.eulerAngles.y, compression);
			break;
		case AxisSyncMode.AxisZ:
			WriteAngle(writer, rot.eulerAngles.z, compression);
			break;
		case AxisSyncMode.AxisXY:
			WriteAngle(writer, rot.eulerAngles.x, compression);
			WriteAngle(writer, rot.eulerAngles.y, compression);
			break;
		case AxisSyncMode.AxisXZ:
			WriteAngle(writer, rot.eulerAngles.x, compression);
			WriteAngle(writer, rot.eulerAngles.z, compression);
			break;
		case AxisSyncMode.AxisYZ:
			WriteAngle(writer, rot.eulerAngles.y, compression);
			WriteAngle(writer, rot.eulerAngles.z, compression);
			break;
		case AxisSyncMode.AxisXYZ:
			WriteAngle(writer, rot.eulerAngles.x, compression);
			WriteAngle(writer, rot.eulerAngles.y, compression);
			WriteAngle(writer, rot.eulerAngles.z, compression);
			break;
		case AxisSyncMode.None:
			break;
		}
	}

	public static void SerializeSpin3D(NetworkWriter writer, Vector3 angularVelocity, AxisSyncMode mode, CompressionSyncMode compression)
	{
		switch (mode)
		{
		case AxisSyncMode.AxisX:
			WriteAngle(writer, angularVelocity.x, compression);
			break;
		case AxisSyncMode.AxisY:
			WriteAngle(writer, angularVelocity.y, compression);
			break;
		case AxisSyncMode.AxisZ:
			WriteAngle(writer, angularVelocity.z, compression);
			break;
		case AxisSyncMode.AxisXY:
			WriteAngle(writer, angularVelocity.x, compression);
			WriteAngle(writer, angularVelocity.y, compression);
			break;
		case AxisSyncMode.AxisXZ:
			WriteAngle(writer, angularVelocity.x, compression);
			WriteAngle(writer, angularVelocity.z, compression);
			break;
		case AxisSyncMode.AxisYZ:
			WriteAngle(writer, angularVelocity.y, compression);
			WriteAngle(writer, angularVelocity.z, compression);
			break;
		case AxisSyncMode.AxisXYZ:
			WriteAngle(writer, angularVelocity.x, compression);
			WriteAngle(writer, angularVelocity.y, compression);
			WriteAngle(writer, angularVelocity.z, compression);
			break;
		case AxisSyncMode.None:
			break;
		}
	}

	public static Vector3 UnserializeVelocity3D(NetworkReader reader, CompressionSyncMode compression)
	{
		return reader.ReadVector3();
	}

	public static Quaternion UnserializeRotation3D(NetworkReader reader, AxisSyncMode mode, CompressionSyncMode compression)
	{
		Quaternion identity = Quaternion.identity;
		Vector3 zero = Vector3.zero;
		switch (mode)
		{
		case AxisSyncMode.AxisX:
			zero.Set(ReadAngle(reader, compression), 0f, 0f);
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisY:
			zero.Set(0f, ReadAngle(reader, compression), 0f);
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisZ:
			zero.Set(0f, 0f, ReadAngle(reader, compression));
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisXY:
			zero.Set(ReadAngle(reader, compression), ReadAngle(reader, compression), 0f);
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisXZ:
			zero.Set(ReadAngle(reader, compression), 0f, ReadAngle(reader, compression));
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisYZ:
			zero.Set(0f, ReadAngle(reader, compression), ReadAngle(reader, compression));
			identity.eulerAngles = zero;
			break;
		case AxisSyncMode.AxisXYZ:
			zero.Set(ReadAngle(reader, compression), ReadAngle(reader, compression), ReadAngle(reader, compression));
			identity.eulerAngles = zero;
			break;
		}
		return identity;
	}

	public static Vector3 UnserializeSpin3D(NetworkReader reader, AxisSyncMode mode, CompressionSyncMode compression)
	{
		Vector3 zero = Vector3.zero;
		switch (mode)
		{
		case AxisSyncMode.AxisX:
			zero.Set(ReadAngle(reader, compression), 0f, 0f);
			break;
		case AxisSyncMode.AxisY:
			zero.Set(0f, ReadAngle(reader, compression), 0f);
			break;
		case AxisSyncMode.AxisZ:
			zero.Set(0f, 0f, ReadAngle(reader, compression));
			break;
		case AxisSyncMode.AxisXY:
			zero.Set(ReadAngle(reader, compression), ReadAngle(reader, compression), 0f);
			break;
		case AxisSyncMode.AxisXZ:
			zero.Set(ReadAngle(reader, compression), 0f, ReadAngle(reader, compression));
			break;
		case AxisSyncMode.AxisYZ:
			zero.Set(0f, ReadAngle(reader, compression), ReadAngle(reader, compression));
			break;
		case AxisSyncMode.AxisXYZ:
			zero.Set(ReadAngle(reader, compression), ReadAngle(reader, compression), ReadAngle(reader, compression));
			break;
		}
		return zero;
	}

	public override int GetNetworkChannel()
	{
		return 1;
	}

	public override float GetNetworkSendInterval()
	{
		return m_SendInterval;
	}

	public override void OnStartAuthority()
	{
		m_LastClientSyncTime = 0f;
	}

	private void OnPool()
	{
		NetIdentity = GetComponent<NetworkIdentity>();
	}

	private void OnSpawn()
	{
		m_Sequence = 0u;
		_resetMovementVars();
		m_IsRecycled = false;
	}

	private void OnRecycle()
	{
		_resetMovementVars();
		m_IsRecycled = true;
	}

	private void _resetMovementVars()
	{
		m_needUpdatePosition = false;
		m_needUpdateVelocity = false;
		m_needUpdateRotation = false;
		m_needUpdateAngularVelocity = false;
		m_firstTimeInterpolationUpdate = true;
		m_LastClientSyncTime = 0f;
		m_LastClientSendTime = 0f;
		m_ServerSendTimeout = GetNetworkSendInterval();
		m_forceResetFrameNumber = -1;
		m_PrevPosition = Vector3.zero;
		m_PrevRotation = Quaternion.identity;
		m_PrevSqrVelocity = 0f;
	}

	private void UNetVersion()
	{
	}
}
