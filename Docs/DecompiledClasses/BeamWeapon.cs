#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BeamWeapon : MonoBehaviour
{
	[SerializeField]
	private float m_Range = 5f;

	[SerializeField]
	private int m_DamagePerSecond = 20;

	[SerializeField]
	private ManDamage.DamageType m_DamageType = ManDamage.DamageType.Energy;

	[SerializeField]
	private float m_FadeOutTime = 0.2f;

	[SerializeField]
	private ParticleSystem m_BeamParticlesPrefab;

	[SerializeField]
	private ParticleSystem m_HitParticlesPrefab;

	[SerializeField]
	private float m_BeamStartDistance;

	private float m_FadeTimer;

	private ParticleSystem m_BeamParticles;

	private ParticleSystem m_HitParticles;

	private Transform trans;

	private MuzzleFlash m_FlashEffect;

	[SerializeField]
	[HideInInspector]
	private TankBlock m_Block;

	[SerializeField]
	[HideInInspector]
	private LineRenderer m_BeamLine;

	private static int k_RaycastLayerMask;

	private static RaycastHit[] s_Hits = new RaycastHit[32];

	public float Range => m_Range;

	public ManDamage.DamageType DamageType => m_DamageType;

	public float DamagePerSecond => m_DamagePerSecond;

	public void SetActive(MuzzleFlash flashEffect)
	{
		if ((bool)m_BeamParticles && !m_BeamParticles.isPlaying)
		{
			m_BeamParticles.Play();
		}
		if ((bool)m_BeamLine)
		{
			m_BeamLine.enabled = true;
		}
		m_FlashEffect = flashEffect;
		if ((bool)m_FlashEffect)
		{
			m_FlashEffect.Hold(on: true);
		}
		m_FadeTimer = m_FadeOutTime;
	}

	private void PrePool()
	{
		k_RaycastLayerMask = Globals.inst.layerScenery.mask | Globals.inst.layerWater.mask | Globals.inst.layerLandmark.mask | Globals.inst.layerTerrain.mask | Globals.inst.layerPickup.mask | Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask | Globals.inst.layerContainer.mask | Globals.inst.layerShieldBulletsFilter.mask | Globals.inst.layerShieldPiercingBullet.mask | (int)Globals.inst.layerIgnoreScenery;
		m_Block = this.GetComponentInParents<TankBlock>(thisObjectFirst: true);
		d.AssertFormat(m_Block.IsNotNull(), "Expected {0} '{1}' to be a child in the hierarchy of a {2}, but it was not!", "BeamWeapon", base.name, "TankBlock");
		m_BeamLine = GetComponent<LineRenderer>();
		if ((bool)m_BeamLine)
		{
			m_BeamLine.positionCount = 2;
			m_BeamLine.SetPosition(0, Vector3.zero);
			m_BeamLine.SetPosition(1, new Vector3(0f, 0f, m_Range));
		}
	}

	private void OnPool()
	{
		trans = base.transform;
		if ((bool)m_BeamParticlesPrefab)
		{
			m_BeamParticles = Object.Instantiate(m_BeamParticlesPrefab, trans.position, trans.rotation);
			m_BeamParticles.transform.parent = trans;
		}
		m_Block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		if ((bool)m_BeamParticles)
		{
			m_BeamParticles.Stop();
		}
		if ((bool)m_BeamLine)
		{
			m_BeamLine.enabled = false;
		}
	}

	private void OnRecycle()
	{
		if ((bool)m_BeamParticles)
		{
			m_BeamParticles.Stop();
		}
		if ((bool)m_BeamLine)
		{
			m_BeamLine.enabled = false;
		}
		if ((bool)m_FlashEffect)
		{
			m_FlashEffect.Hold(on: false);
			m_FlashEffect = null;
		}
		if ((bool)m_HitParticles)
		{
			m_HitParticles.Stop();
			m_HitParticles.Recycle();
			m_HitParticles = null;
		}
	}

	private void OnUpdate()
	{
		float num = m_Range * m_FadeTimer / m_FadeOutTime;
		int num2 = -1;
		if (m_Block.IsAttached || num > 0f)
		{
			int num3 = Physics.RaycastNonAlloc(trans.position + trans.forward * m_BeamStartDistance, trans.forward, s_Hits, num, k_RaycastLayerMask, QueryTriggerInteraction.Collide);
			for (int i = 0; i < num3; i++)
			{
				ref RaycastHit reference = ref s_Hits[i];
				if (reference.distance == 0f || reference.distance > num)
				{
					continue;
				}
				if (m_Block.IsAttached && reference.collider.gameObject.layer == (int)Globals.inst.layerShieldBulletsFilter)
				{
					Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(reference.collider);
					if (visible.IsNotNull() && visible.block.tank.IsNotNull() && !visible.block.tank.IsEnemy(m_Block.tank.Team))
					{
						continue;
					}
				}
				num2 = i;
				num = reference.distance;
			}
		}
		if (num2 >= 0)
		{
			ref RaycastHit reference2 = ref s_Hits[num2];
			float num4 = (float)m_DamagePerSecond * Time.deltaTime;
			if (num4 != 0f)
			{
				Damageable componentInParents = reference2.collider.GetComponentInParents<Damageable>(thisObjectFirst: true);
				if ((bool)componentInParents)
				{
					Singleton.Manager<ManDamage>.inst.DealDamage(componentInParents, num4, m_DamageType, this, m_Block.tank, reference2.point, trans.forward);
				}
			}
		}
		if ((bool)m_BeamLine)
		{
			m_BeamLine.SetPosition(1, new Vector3(0f, 0f, num));
		}
		m_FadeTimer -= Time.deltaTime;
		if (m_FadeTimer < 0f)
		{
			if ((bool)m_BeamParticles)
			{
				m_BeamParticles.Stop();
			}
			if ((bool)m_BeamLine)
			{
				m_BeamLine.enabled = false;
			}
			num2 = -1;
			if ((bool)m_FlashEffect)
			{
				m_FlashEffect.Hold(on: false);
				m_FlashEffect = null;
			}
		}
		if (!m_HitParticlesPrefab)
		{
			return;
		}
		if (num2 >= 0)
		{
			RaycastHit raycastHit = s_Hits[num2];
			Quaternion quaternion = Quaternion.LookRotation(raycastHit.normal);
			if (m_HitParticles.IsNull())
			{
				m_HitParticles = m_HitParticlesPrefab.Spawn(trans, raycastHit.point, quaternion);
				m_HitParticles.Play();
			}
			m_HitParticles.transform.SetPositionIfChanged(raycastHit.point);
			m_HitParticles.transform.SetRotationIfChanged(quaternion);
		}
		else if (m_HitParticles.IsNotNull())
		{
			m_HitParticles.Stop();
			m_HitParticles.Recycle();
			m_HitParticles = null;
		}
	}

	private void OnDrawGizmosSelected()
	{
		DebugUtil.GizmosDrawArrow(base.transform.position + base.transform.forward * m_BeamStartDistance, base.transform.position + base.transform.forward * m_Range);
	}
}
