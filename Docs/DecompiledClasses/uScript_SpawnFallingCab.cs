#define UNITY_EDITOR
using UnityEngine;

public class uScript_SpawnFallingCab : uScriptLogic
{
	private bool m_Fired;

	private bool m_Finished;

	private Transform m_Particles;

	private Tank m_SpawnedTank;

	private float m_Timer;

	private bool m_Switched;

	private float m_HitTime;

	public bool Finished => m_Finished;

	public bool Out => true;

	public Transform In(Transform particleEffect, TankPreset preset, GameObject owner, string uniqueName, bool damage = true)
	{
		if (!m_Fired && Singleton.Manager<ManUI>.inst.FadeFinished())
		{
			Encounter component = owner.GetComponent<Encounter>();
			m_Particles = component.GetStoredObject(particleEffect.name, uniqueName);
			if (m_Particles == null)
			{
				Vector3 vector = Mode<ModeMain>.inst.StartPositionScene + Mode<ModeMain>.inst.m_GameStartPosOffset;
				m_Particles = particleEffect.Spawn(vector);
				m_Particles.name = particleEffect.name;
				m_HitTime = Random.Range(3f, 5f);
				Vector3 zero = Vector3.zero;
				m_HitTime += Time.deltaTime;
				m_Particles.GetComponent<Rigidbody>().AddForce(zero, ForceMode.VelocityChange);
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
				{
					NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
					TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnNetworkedTechRef(preset.GetTechDataFormatted(), vector, Quaternion.Euler(Random.onUnitSphere), myPlayer.TechTeamID, myPlayer);
					m_SpawnedTank = (trackedVisible.visible.IsNotNull() ? trackedVisible.visible.tank : null);
					d.Assert(m_SpawnedTank.IsNotNull(), "Player cab spawned from mission ended up off tile?");
				}
				else
				{
					ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
					{
						techData = preset.GetTechDataFormatted(),
						blockIDs = null,
						teamID = 0,
						position = vector,
						rotation = Quaternion.Euler(Random.onUnitSphere),
						grounded = false
					};
					m_SpawnedTank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
				}
				m_SpawnedTank.name = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 75);
				m_Particles.gameObject.SendMessage("SetEncounter", component, SendMessageOptions.DontRequireReceiver);
				m_Particles.GetComponent<ActivateEncounterObjectOnCollision>().OnCollision.Subscribe(OnGroundCollision);
				if (damage)
				{
					BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_SpawnedTank.blockman.IterateBlocks().GetEnumerator();
					while (enumerator.MoveNext())
					{
						TankBlock current = enumerator.Current;
						Singleton.Manager<ManDamage>.inst.DealImpactDamage(current.visible.damageable, current.visible.damageable.MaxHealth * 0.8f, null);
					}
				}
				component.AddStoredObject(m_Particles, uniqueName);
				m_Fired = true;
				m_Switched = false;
			}
		}
		if (m_SpawnedTank != null)
		{
			m_SpawnedTank.visible.SetLockTimout(Visible.LockTimerTypes.Interactible);
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_SpawnedTank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.visible.SetLockTimout(Visible.LockTimerTypes.Grabbable);
			}
		}
		if (m_Fired)
		{
			if (m_HitTime > m_Timer && !m_Finished)
			{
				if (m_Timer > 1f && !m_Switched)
				{
					Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(m_SpawnedTank);
					m_Switched = true;
				}
				m_SpawnedTank.trans.position = m_Particles.transform.position;
				m_Timer += Time.deltaTime;
			}
			else
			{
				m_SpawnedTank.grounded = true;
				m_Finished = true;
			}
		}
		return m_Particles;
	}

	public void OnDisable()
	{
		m_Fired = false;
		if (m_Particles != null)
		{
			m_Particles.GetComponent<ActivateEncounterObjectOnCollision>().OnCollision.Unsubscribe(OnGroundCollision);
			m_Particles = null;
		}
		m_Finished = false;
		m_Timer = 0f;
	}

	private void OnGroundCollision(int dummy)
	{
		m_SpawnedTank.grounded = true;
		m_Finished = true;
	}

	private Vector3 CalculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		Vector3 vector2;
		Vector3 vector = (vector2 = target - origin);
		vector2.y = 0f;
		float y = vector.y;
		float magnitude = vector2.magnitude;
		float y2 = y / timeToTarget + 0.5f * Physics.gravity.magnitude * timeToTarget;
		float num = magnitude / timeToTarget;
		Vector3 normalized = vector2.normalized;
		normalized *= num;
		normalized.y = y2;
		return normalized;
	}
}
