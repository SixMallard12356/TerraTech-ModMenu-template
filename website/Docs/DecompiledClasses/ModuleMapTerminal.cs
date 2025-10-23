using System.Collections;
using UnityEngine;

public class ModuleMapTerminal : Module, ManPointer.OpenMenuEventConsumer
{
	[SerializeField]
	private bool m_CanLaunchByDefault = true;

	[SerializeField]
	private float m_ExplorationRange = 500f;

	[SerializeField]
	private float m_RevealDelaySeconds = 0.5f;

	[SerializeField]
	private float m_RevealDurationSeconds = 2f;

	[SerializeField]
	private FMODEvent m_RevealSfx;

	[SerializeField]
	private float m_MaxInteractDistance = 50f;

	private bool m_CanLaunch;

	private ModuleAnimator m_Animator;

	private AnimatorBool m_ReadyBool = new AnimatorBool("Ready");

	private Coroutine m_DelayedDataUpload;

	public bool CanLaunch
	{
		set
		{
			m_CanLaunch = value;
		}
	}

	public bool CanOpenMenu(bool isRadial)
	{
		bool result = false;
		if (ManNetwork.IsHost && !isRadial)
		{
			result = m_CanLaunch && base.block.tank.IsNotNull() && base.block.tank.IsAnchored && !base.block.tank.IsEnemy();
		}
		return result;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowNonRadialMenu && base.block.tank.IsNotNull() && !base.block.tank.IsEnemy() && m_CanLaunch && base.block.tank.IsAnchored && (Singleton.playerTank == null || (Singleton.playerPos - base.block.tank.boundsCentreWorld).SetY(0f).sqrMagnitude < m_MaxInteractDistance * m_MaxInteractDistance))
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.WorldMap);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManMap>.inst.ClientMapDataReceivedEvent.Subscribe(OnMapUpdated);
				Singleton.Manager<ManMap>.inst.RequestMapData();
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(base.block.tank.visible.ID);
			if (trackedVisible.IsVendor && !Singleton.Manager<ManMap>.inst.HasTradingStationPerformedScan(trackedVisible))
			{
				Singleton.Manager<ManMap>.inst.ExploreArea(base.block.tank.boundsCentreWorld, m_ExplorationRange, m_RevealDurationSeconds, m_RevealDelaySeconds);
				Singleton.Manager<ManMap>.inst.MarkTradingStationAsScanPerformed(trackedVisible);
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
				{
					UploadDataDelayed(m_RevealDurationSeconds + m_RevealDelaySeconds + 0.1f);
				}
				if (m_RevealSfx.IsValid())
				{
					StartCoroutine(PlayDelayedSFX(m_RevealSfx, m_RevealDelaySeconds));
				}
			}
			return true;
		}
		return false;
		static IEnumerator PlayDelayedSFX(FMODEvent fmodEvent, float sfxDelay)
		{
			yield return new WaitForSeconds(sfxDelay);
			fmodEvent.PlayOneShot();
		}
	}

	private void UploadDataDelayed(float delay)
	{
		StopDelayedUpload();
		m_DelayedDataUpload = StartCoroutine(UploadDelayed(delay));
		static IEnumerator UploadDelayed(float uploadDelay)
		{
			yield return new WaitForSeconds(uploadDelay);
			Singleton.Manager<ManMap>.inst.UploadMapData();
		}
	}

	private void StopDelayedUpload()
	{
		if (m_DelayedDataUpload != null)
		{
			StopCoroutine(m_DelayedDataUpload);
			m_DelayedDataUpload = null;
		}
	}

	private void OnMapUpdated()
	{
		Singleton.Manager<ManMap>.inst.ClientMapDataReceivedEvent.Unsubscribe(OnMapUpdated);
		if (m_DelayedDataUpload == null)
		{
			Singleton.Manager<ManMap>.inst.UploadMapData();
		}
	}

	private void OnPool()
	{
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Animator = GetComponent<ModuleAnimator>();
	}

	private void OnSpawn()
	{
		m_CanLaunch = m_CanLaunchByDefault;
	}

	private void OnRecycle()
	{
		StopDelayedUpload();
	}

	private void OnUpdate()
	{
		if ((bool)m_Animator)
		{
			bool value = m_CanLaunch && base.block.tank != null && base.block.tank.IsAnchored;
			m_Animator.Set(m_ReadyBool, value);
		}
	}
}
