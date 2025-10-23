#define UNITY_EDITOR
using BehaviorDesigner.Runtime;
using UnityEngine;

public class FlyTechAway : MonoBehaviour
{
	private float m_LifeTime;

	private float m_TargetHeight;

	private Transform m_OnRemovalPrefab;

	private Tank m_Tech;

	private float m_UnachorDelay;

	private bool m_HasTriggeredDespawn;

	public static void InitiateTakeOff(Tank targetTech, float maxLifetime, float targetHeight, ExternalBehaviorTree aiTree, Transform removalParticles = null)
	{
		if (!targetTech.gameObject.activeInHierarchy)
		{
			d.LogError("ERROR - FlyTechAway.InitiateTakeOff Target tech " + targetTech.name + " is not active in the heirarchy and appears to be a recycled tech!");
			return;
		}
		if (targetTech.gameObject.GetComponent<FlyTechAway>() != null)
		{
			d.LogError("ERROR - FlyTechAway.InitiateTakeOff Target tech " + targetTech.name + " already has takeof initialised (FlyTechAway component already attached). Cannot call this twice!");
			return;
		}
		FlyTechAway flyTechAway = targetTech.gameObject.AddComponent<FlyTechAway>();
		flyTechAway.m_Tech = targetTech;
		flyTechAway.m_LifeTime = maxLifetime;
		flyTechAway.m_TargetHeight = targetHeight;
		flyTechAway.m_OnRemovalPrefab = removalParticles;
		flyTechAway.m_UnachorDelay = 0.08f;
		flyTechAway.InitTick();
		targetTech.AI.SetCustomBehaviourTree(aiTree);
		targetTech.SetInvulnerable(invulnerable: true, forever: true);
		targetTech.visible.RecycledEvent.Subscribe(flyTechAway.OnTargetTechRecycled);
		targetTech.visible.SetManagedByTile(managed: false);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(flyTechAway.CleanupOnModeExit);
	}

	private void InitTick()
	{
		if (m_LifeTime.Approximately(0f))
		{
			Debug.Log("[FlyTechAway.InitTick] Immediately triggering Despawn Effects");
			TriggerDespawnEffect();
		}
	}

	private void TriggerDespawnEffect()
	{
		if (!m_HasTriggeredDespawn && m_OnRemovalPrefab != null)
		{
			m_OnRemovalPrefab.Spawn(Singleton.dynamicContainer, m_Tech.boundsCentreWorld, m_Tech.trans.rotation);
			m_HasTriggeredDespawn = true;
		}
	}

	private void OnTargetTechRecycled(Visible recycledVisible)
	{
		recycledVisible.RecycledEvent.Unsubscribe(OnTargetTechRecycled);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Unsubscribe(CleanupOnModeExit);
		Object.Destroy(this);
	}

	private void CleanupOnModeExit(Mode exitingMode)
	{
		m_Tech.visible.RemoveFromGame();
		m_Tech = null;
	}

	private void Update()
	{
		if (!(m_Tech != null))
		{
			return;
		}
		if (m_LifeTime <= 0f || m_Tech.Boosters.FuelLevel <= 0f || m_Tech.boundsCentreWorld.y >= m_TargetHeight)
		{
			TriggerDespawnEffect();
			if (Singleton.Manager<ManNetwork>.inst.IsServer || !ManNetwork.IsNetworked)
			{
				Debug.Log("[FlyTechAway.Update] Removing " + m_Tech.name + " from game");
				m_Tech.visible.RemoveFromGame();
				m_Tech = null;
			}
		}
		else
		{
			if (m_UnachorDelay <= 0f && m_Tech.IsAnchored)
			{
				m_Tech.Anchors.UnanchorAll(playAnim: false);
			}
			m_LifeTime -= Time.deltaTime;
			m_UnachorDelay -= Time.deltaTime;
		}
	}
}
