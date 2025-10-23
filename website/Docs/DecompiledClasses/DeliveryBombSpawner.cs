#define UNITY_EDITOR
using UnityEngine;

public class DeliveryBombSpawner : MonoBehaviour
{
	private enum DeliveryState
	{
		Waiting,
		Falling,
		Impact,
		Delivered
	}

	public class Data
	{
		public V3Serial m_Pos;

		public WorldPosition m_WorldPosition;

		public float m_EnableBombTime;
	}

	public enum ImpactMarkerType
	{
		Tech,
		Crate,
		FriendlyTech
	}

	[SerializeField]
	private GameObject m_DeliveryBomb;

	[SerializeField]
	private GameObject m_ReplacementPrefab;

	[SerializeField]
	private ParticleSystem m_TrailSystem;

	[SerializeField]
	private ParticleSystem m_ImpactSystemPrefab;

	[SerializeField]
	private Projector m_ImpactMarkerPrefab;

	[SerializeField]
	[EnumArray(typeof(ImpactMarkerType))]
	private Material[] m_ImpactTypeMaterials;

	[SerializeField]
	[Tooltip("Marker will scale down to zero once the bomb hits this height off the ground")]
	private float m_ProjectorScaleToZeroHeight = 10f;

	[SerializeField]
	[Tooltip("Marker will pulse up and down by this much extra / less")]
	private float m_MarkerPulseVariance = 1f;

	[SerializeField]
	[Tooltip("A full pulse cycle will take this much time in seconds")]
	private float m_MarkerPulseTime = 1f;

	[SerializeField]
	private FMODEvent m_SFXImpact;

	public Event<Vector3> BombDeliveredEvent;

	private Rigidbody m_RigidBody;

	private Transform m_Trans;

	private DeliveryState m_CurrentState;

	private Projector m_ImpactMarker;

	private float m_ProjectorOrthoScale;

	private float m_FallbackImpactHeight;

	private ImpactMarkerType m_ImpactMarkerType;

	private Data m_SaveData;

	public float ImpactMarkerSize
	{
		get
		{
			if (!(m_ImpactMarkerPrefab != null))
			{
				return 0f;
			}
			return m_ImpactMarkerPrefab.orthographicSize;
		}
	}

	public bool SelfManagedRecycle
	{
		get
		{
			if (m_CurrentState == DeliveryState.Delivered && m_TrailSystem != null)
			{
				return m_TrailSystem.particleCount > 0;
			}
			return false;
		}
	}

	public void SetSpawnParams(Vector3 targetImpactPosition, ImpactMarkerType impactMarkerType, float delayBeforeEnableBomb = 0f)
	{
		m_SaveData = new Data();
		m_ImpactMarkerType = impactMarkerType;
		m_SaveData.m_EnableBombTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() + delayBeforeEnableBomb;
		m_SaveData.m_Pos = Vector3.zero;
		m_SaveData.m_WorldPosition = WorldPosition.FromScenePosition(in targetImpactPosition);
	}

	public Data GetSaveData()
	{
		return m_SaveData;
	}

	private void EnableBomb(bool enable)
	{
		if ((bool)m_DeliveryBomb)
		{
			m_DeliveryBomb.SetActive(enable);
		}
		m_RigidBody.useGravity = enable;
		if ((bool)m_TrailSystem)
		{
			m_TrailSystem.SetEmissionEnabled(enable);
		}
		if (enable)
		{
			Singleton.Manager<ManSFX>.inst.TryStartTransformLoopingSFX(ManSFX.TransformLoopingSFXTypes.DeliveryBomb, m_Trans);
		}
		else
		{
			Singleton.Manager<ManSFX>.inst.TryStopTransformLoopingSFX(ManSFX.TransformLoopingSFXTypes.DeliveryBomb, m_Trans);
		}
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
	}

	private void EnableImpactMarker(bool enable)
	{
		if (!m_ImpactMarkerPrefab)
		{
			return;
		}
		if (enable)
		{
			Vector3 position = Singleton.Manager<ManWorld>.inst.ProjectToGround(m_Trans.position) + Vector3.up * 5f;
			Quaternion rotation = Quaternion.Euler(90f, 0f, Random.value * 360f);
			m_ImpactMarker = m_ImpactMarkerPrefab.Spawn(position, rotation);
			int impactMarkerType = (int)m_ImpactMarkerType;
			Material material = ((impactMarkerType < m_ImpactTypeMaterials.Length) ? m_ImpactTypeMaterials[impactMarkerType] : null);
			if (material != null)
			{
				m_ImpactMarker.material = material;
			}
			else
			{
				d.LogError("ERROR: unable to find a material for impact marker type " + m_ImpactMarkerType);
			}
			m_ImpactMarker.orthographicSize = m_ImpactMarkerPrefab.orthographicSize;
			m_ProjectorOrthoScale = m_ImpactMarkerPrefab.orthographicSize;
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(ManSFX.MiscSfxType.PayloadIncoming, position);
		}
		else
		{
			m_ImpactMarker.Recycle();
			m_ImpactMarker = null;
		}
	}

	private bool CanStartFalling()
	{
		bool num = Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(m_Trans.position);
		bool flag = Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame;
		bool flag2 = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() >= m_SaveData.m_EnableBombTime;
		return num && flag && flag2;
	}

	private void PrePool()
	{
		base.gameObject.AddComponent<WorldSpaceObject>();
	}

	private void OnPool()
	{
		m_Trans = base.transform;
		m_RigidBody = GetComponent<Rigidbody>();
		m_RigidBody.useGravity = false;
	}

	private void OnSpawn()
	{
		m_CurrentState = DeliveryState.Waiting;
	}

	private void OnRecycle()
	{
		if ((bool)m_ImpactMarker)
		{
			m_ImpactMarker.Recycle();
			m_ImpactMarker = null;
		}
		m_RigidBody.useGravity = false;
		BombDeliveredEvent.Clear();
		Singleton.Manager<ManSFX>.inst.TryStopTransformLoopingSFX(ManSFX.TransformLoopingSFXTypes.DeliveryBomb, m_Trans);
	}

	private void OnCollisionEnter(Collision col)
	{
		if (m_CurrentState == DeliveryState.Falling)
		{
			m_CurrentState = DeliveryState.Impact;
		}
	}

	private void Update()
	{
		switch (m_CurrentState)
		{
		case DeliveryState.Waiting:
			if (CanStartFalling())
			{
				Vector3 scenePos2 = m_Trans.position;
				if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos2))
				{
					m_FallbackImpactHeight = scenePos2.y;
					EnableBomb(enable: true);
					EnableImpactMarker(enable: true);
					m_CurrentState = DeliveryState.Falling;
				}
			}
			break;
		case DeliveryState.Falling:
		{
			Vector3 scenePos = m_Trans.position;
			float num = (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos) ? scenePos.y : m_FallbackImpactHeight);
			if (m_Trans.position.y < num - 50f)
			{
				m_CurrentState = DeliveryState.Impact;
			}
			if ((bool)m_ImpactMarker)
			{
				float num2 = m_Trans.position.y - num;
				if (num2 <= m_ProjectorScaleToZeroHeight)
				{
					m_ImpactMarker.orthographicSize = m_ProjectorOrthoScale * (num2 / m_ProjectorScaleToZeroHeight);
					break;
				}
				float num3 = 2f / m_MarkerPulseTime;
				m_ImpactMarker.orthographicSize = m_ImpactMarkerPrefab.orthographicSize + Mathf.Sin(Time.time * num3) * m_MarkerPulseVariance;
				m_ProjectorOrthoScale = m_ImpactMarker.orthographicSize;
			}
			break;
		}
		case DeliveryState.Impact:
			EnableBomb(enable: false);
			EnableImpactMarker(enable: false);
			m_CurrentState = DeliveryState.Delivered;
			if ((bool)m_ImpactSystemPrefab)
			{
				m_ImpactSystemPrefab.transform.Spawn(base.transform.position);
			}
			m_SFXImpact.PlayOneShot(m_Trans.position);
			BombDeliveredEvent.Send(m_Trans.position);
			break;
		case DeliveryState.Delivered:
			if (m_TrailSystem == null || m_TrailSystem.particleCount == 0)
			{
				this.Recycle();
			}
			break;
		}
	}
}
