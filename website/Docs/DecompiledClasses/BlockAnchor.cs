#define UNITY_EDITOR
using UnityEngine;

public class BlockAnchor : MonoBehaviour, IWorldTreadmill
{
	[SerializeField]
	public Transform m_GroundPoint;

	[SerializeField]
	public Transform m_BeamPoint;

	public GameObject m_AnchorGeometry;

	[HideInInspector]
	public Collider[] m_AnchorGeometryColliders;

	public float m_SnapToleranceUp = 0.25f;

	public float m_SnapToleranceDown = 0.5f;

	public float m_MaxExtraReach;

	[SerializeField]
	public float m_SkyAnchorStrength = 1f;

	[SerializeField]
	public float m_SkyAnchorDamping = 0.5f;

	[SerializeField]
	public bool m_IsSkyAnchor;

	[SerializeField]
	public float m_SkyAnchorSpeed = 100f;

	[SerializeField]
	public float m_SkyAnchorRecoilForce = 2f;

	[SerializeField]
	public Transform m_DeathExplosion;

	[SerializeField]
	public FactionSubTypes m_Corporation = FactionSubTypes.BF;

	[SerializeField]
	[HideInInspector]
	private Collider[] m_Colliders;

	private ModuleAnchor m_AnchorModule;

	private TankBlock m_ParentBlock;

	private LineRenderer m_Beam;

	private Vector3 m_GroundPosWorld;

	private Quaternion m_GroundRotWorld;

	private Vector3 m_GroundPointParentRel;

	private ParticleSystem m_BeamParticlesTop;

	private Transform m_SkyAnchorProxy;

	private float m_AnimTime;

	private float m_AnimSpeed;

	private Damageable m_Damageable;

	[SerializeField]
	[HideInInspector]
	private Quaternion m_GeometryDefaultLocalRotation = Quaternion.identity;

	private static int s_LayerConnectedStatic;

	private static int s_LayerDisconnected;

	public const string kSkyBlockAnchorTag = "_B";

	private const float kMinSkyAnchorAnimHeight = 3f;

	[SerializeField]
	[HideInInspector]
	private MaterialSwapper m_MaterialSwapper;

	public Vector3 GroundPoint => m_GroundPoint.transform.position;

	public Vector3 GroundPointInitial => base.gameObject.transform.parent.TransformPoint(m_GroundPointParentRel);

	public void Activate(bool active, bool playAnim)
	{
		if ((bool)m_AnchorGeometry)
		{
			if (!active)
			{
				bool flag = false;
				HideSkyAnchorProxy();
				if (playAnim && m_IsSkyAnchor)
				{
					float num = m_AnchorModule.SkyAnchorFirePoint.position.y - m_GroundPosWorld.y;
					if (num > 3f)
					{
						m_AnimSpeed = (0f - m_SkyAnchorSpeed) / num;
						flag = true;
					}
				}
				if (!flag)
				{
					DeactivateImmediately();
				}
			}
			else
			{
				base.gameObject.SetActive(value: true);
				if (m_AnchorGeometry != null)
				{
					m_AnchorGeometry.transform.localRotation = m_GeometryDefaultLocalRotation;
				}
				if (m_IsSkyAnchor)
				{
					m_Damageable.InitHealth(-1337f);
					Vector3 scenePos = GroundPointInitial;
					if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos, out var _))
					{
						m_GroundPosWorld = scenePos - m_GroundPointParentRel;
						m_GroundRotWorld = Quaternion.identity;
					}
					float num2 = m_AnchorModule.SkyAnchorFirePoint.position.y - m_GroundPosWorld.y;
					if (!playAnim || num2 < 3f || m_SkyAnchorSpeed <= 0f)
					{
						m_AnimSpeed = 0f;
						m_AnimTime = 1f;
					}
					else
					{
						m_AnimSpeed = m_SkyAnchorSpeed / num2;
						m_AnimTime = 0f;
					}
					if ((bool)m_ParentBlock.tank && playAnim)
					{
						Rigidbody rbody = m_ParentBlock.tank.rbody;
						if (rbody != null)
						{
							Vector3 position = m_AnchorModule.SkyAnchorBeamAttachPoint.position;
							Vector3 force = new Vector3(0f, m_SkyAnchorRecoilForce, 0f);
							rbody.AddForceAtPosition(force, position, ForceMode.VelocityChange);
						}
					}
				}
				UpdateTransforms();
			}
		}
		if ((bool)m_BeamParticlesTop)
		{
			if (!active)
			{
				m_BeamParticlesTop.Stop();
				m_BeamParticlesTop.Clear();
			}
			else
			{
				m_BeamParticlesTop.Play();
			}
		}
	}

	private void ShowSkyAnchorProxy(Vector3 position, Quaternion rotation)
	{
		if (!(m_SkyAnchorProxy == null))
		{
			return;
		}
		MeshRenderer componentInChildren = m_AnchorModule.SkyAnchorBeamAttachPoint.GetComponentInChildren<MeshRenderer>();
		if ((bool)componentInChildren)
		{
			Transform prefab = componentInChildren.gameObject.transform;
			m_SkyAnchorProxy = prefab.UnpooledSpawn(null, worldPosStays: false);
			m_SkyAnchorProxy.SetPositionAndRotation(position, rotation);
			Collider componentInChildren2 = m_SkyAnchorProxy.GetComponentInChildren<Collider>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.enabled = false;
			}
			componentInChildren.enabled = false;
			m_MaterialSwapper.SetProxyRenderer(m_SkyAnchorProxy.GetComponent<MeshRenderer>());
		}
	}

	private void HideSkyAnchorProxy()
	{
		if (!(m_SkyAnchorProxy != null))
		{
			return;
		}
		m_MaterialSwapper.SetProxyRenderer(null);
		Object.Destroy(m_SkyAnchorProxy.gameObject);
		m_SkyAnchorProxy = null;
		if (m_AnchorModule != null)
		{
			MeshRenderer componentInChildren = m_AnchorModule.SkyAnchorBeamAttachPoint.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				componentInChildren.enabled = true;
			}
		}
	}

	public void FindChildColliders()
	{
		m_Colliders = GetComponentsInChildren<Collider>(includeInactive: true);
		d.Assert(m_Colliders.Length != 0, $"BlockAnchor {base.name} expected at least one 1 collider: found {m_Colliders.Length}");
	}

	public void EnableCollision(bool enable)
	{
		bool flag = enable && !m_AnchorModule.AllowsRotation;
		for (int i = 0; i < m_Colliders.Length; i++)
		{
			m_Colliders[i].isTrigger = !enable;
			m_Colliders[i].gameObject.layer = (flag ? s_LayerConnectedStatic : s_LayerDisconnected);
		}
	}

	private void PrePool()
	{
		if (m_AnchorGeometry != null)
		{
			m_GeometryDefaultLocalRotation = m_AnchorGeometry.transform.localRotation;
		}
		FindChildColliders();
		if (m_IsSkyAnchor)
		{
			Collider[] colliders = m_Colliders;
			for (int i = 0; i < colliders.Length; i++)
			{
				colliders[i].tag = "_B";
			}
			m_MaterialSwapper = base.gameObject.AddComponent<MaterialSwapper>();
		}
		s_LayerConnectedStatic = Globals.inst.layerTank;
		s_LayerDisconnected = Globals.inst.layerTankIgnoreTerrain;
	}

	private void OnPool()
	{
		m_AnchorModule = this.GetComponentInParents<ModuleAnchor>();
		m_ParentBlock = this.GetComponentInParents<TankBlock>();
		if ((bool)m_BeamPoint)
		{
			m_Beam = m_BeamPoint.GetComponent<LineRenderer>();
		}
		m_AnchorGeometryColliders = ((m_AnchorGeometry != null) ? m_AnchorGeometry.GetComponentsInChildren<Collider>() : new Collider[0]);
		if (m_IsSkyAnchor)
		{
			m_Damageable = GetComponent<Damageable>();
			m_Damageable.damageEvent.Subscribe(OnDamaged);
			m_Damageable.SetRejectDamageHandler(OnRejectDamage);
			m_Damageable.deathEvent.Subscribe(OnDeath);
			m_MaterialSwapper.SetupMaterial(m_Damageable, m_Corporation);
			m_BeamParticlesTop = m_AnchorModule.SkyAnchorBeamAttachPoint.GetComponentInChildren<ParticleSystem>(includeInactive: true);
			m_ParentBlock.visible.HasMultipleDamageables = true;
			m_ParentBlock.BlockFixedUpdate.Subscribe(OnFixedUpdate);
			m_ParentBlock.BlockLateUpdate.Subscribe(OnLateUpdate);
		}
		m_GroundPointParentRel = m_GroundPoint.localPosition;
	}

	private void OnSpawn()
	{
		if (m_IsSkyAnchor)
		{
			m_BeamParticlesTop.Stop();
			Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
		}
	}

	private void OnRecycle()
	{
		if (m_IsSkyAnchor)
		{
			Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
		}
	}

	private void DeactivateImmediately()
	{
		if (m_IsSkyAnchor)
		{
			HideSkyAnchorProxy();
			base.gameObject.transform.SetLocalPositionIfChanged(Vector3.zero);
		}
		base.gameObject.SetActive(value: false);
		if ((bool)m_MaterialSwapper)
		{
			m_MaterialSwapper.RestoreDefaultMat();
		}
		if ((bool)m_Beam)
		{
			m_Beam.positionCount = 0;
		}
	}

	private bool OnRejectDamage(ManDamage.DamageInfo info, bool actuallyDealDamage)
	{
		if ((bool)m_ParentBlock.tank && !Singleton.Manager<ManGameMode>.inst.IsFriendlyFireEnabled() && !Tank.IsEnemy(info.SourceTeamID, m_ParentBlock.tank.Team))
		{
			return true;
		}
		return false;
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		m_MaterialSwapper.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
	}

	private void Explode(Tank sourceTank)
	{
		if ((bool)m_DeathExplosion)
		{
			Explosion component = m_DeathExplosion.Spawn(Singleton.dynamicContainer, base.gameObject.transform.position, base.gameObject.transform.rotation).GetComponent<Explosion>();
			if ((bool)component)
			{
				component.SetDamageSource(sourceTank);
				component.DoDamage = false;
				component.SetCorpType(Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)m_ParentBlock.visible.ItemType));
			}
		}
	}

	private void OnDeath(Damageable damageable, ManDamage.DamageInfo damage)
	{
		Explode(damage.SourceTank);
		m_AnchorModule.OnAnchorBlockDeath();
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		Vector3 vector = new Vector3(amountToMove.x, amountToMove.y, amountToMove.z);
		m_GroundPosWorld += vector;
		if (m_SkyAnchorProxy != null)
		{
			m_SkyAnchorProxy.position += vector;
		}
	}

	private void UpdateTransforms()
	{
		if (m_IsSkyAnchor)
		{
			Vector3 position = Vector3.Lerp(m_AnchorModule.SkyAnchorFirePoint.position, m_GroundPosWorld, m_AnimTime);
			Quaternion rotation = Quaternion.Lerp(m_AnchorModule.SkyAnchorFirePoint.rotation, m_GroundRotWorld, m_AnimTime);
			base.gameObject.transform.SetPositionAndRotation(position, rotation);
			Vector3 position2 = m_BeamPoint.position;
			Vector3 position3 = m_AnchorModule.SkyAnchorBeamAttachPoint.position;
			m_Beam.positionCount = 2;
			m_Beam.SetPosition(0, position2);
			m_Beam.SetPosition(1, position3);
			Vector3 vector = Vector3.Normalize(position3 - position2);
			m_BeamPoint.SetRotationIfChanged(Quaternion.LookRotation(vector));
			m_AnchorModule.SkyAnchorBeamAttachPoint.SetRotationIfChanged(Quaternion.LookRotation(-vector));
			if (m_AnimSpeed >= 0f && m_AnimTime == 1f)
			{
				ShowSkyAnchorProxy(position, rotation);
			}
		}
	}

	private void OnFixedUpdate()
	{
		if (!m_IsSkyAnchor || !base.gameObject.activeSelf)
		{
			return;
		}
		if ((bool)m_ParentBlock.tank)
		{
			Rigidbody rbody = m_ParentBlock.tank.rbody;
			if (rbody != null)
			{
				Vector3 position = m_AnchorModule.SkyAnchorBeamAttachPoint.position;
				Vector3 force = Vector3.Scale(GroundPoint - position - m_SkyAnchorDamping * rbody.GetPointVelocity(position), new Vector3(m_SkyAnchorStrength, 0f, m_SkyAnchorStrength));
				rbody.AddForceAtPosition(force, position, ForceMode.Acceleration);
				float num = position.y - GroundPoint.y - m_SnapToleranceDown;
				if (num > 0f)
				{
					force = new Vector3(0f, m_SkyAnchorStrength * ((0f - m_SkyAnchorDamping) * rbody.GetPointVelocity(position).y - num), 0f);
					rbody.AddForceAtPosition(force, position, ForceMode.Acceleration);
				}
			}
		}
		m_AnimTime = Mathf.Clamp(m_AnimTime + m_AnimSpeed * Time.fixedDeltaTime, 0f, 1f);
		if (m_AnimSpeed < 0f && m_AnimTime == 0f)
		{
			DeactivateImmediately();
		}
	}

	private void OnLateUpdate()
	{
		if (m_IsSkyAnchor && base.gameObject.activeSelf)
		{
			UpdateTransforms();
		}
	}
}
