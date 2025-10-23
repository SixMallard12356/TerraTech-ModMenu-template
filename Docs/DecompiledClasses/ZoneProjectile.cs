using UnityEngine;

public class ZoneProjectile : Projectile
{
	public enum DeployType
	{
		DeployOnStick,
		DeployOnCreate,
		DeployOnCollide
	}

	[SerializeField]
	private GameObject m_Zone;

	[SerializeField]
	private float m_DeployDelay = 3f;

	[SerializeField]
	private float m_ZoneTargetRadius = 10f;

	[SerializeField]
	protected LayerMask m_LayersToAffect;

	[SerializeField]
	private bool m_HideProjectileOnDeploy;

	[SerializeField]
	private DeployType m_DeployType;

	[SerializeField]
	private AnimationCurve m_DeployCurve;

	[SerializeField]
	private AnimationCurve m_UndeployCurve;

	[SerializeField]
	private float m_DeployAnimationTimeSeconds = 1f;

	[SerializeField]
	private float m_UndeployAnimationTimeSeconds = 1f;

	private bool m_Deployed;

	private float m_ZoneCurrentRadius;

	private ManTimedEvents.ManagedEvent m_DeployZoneEvent = new ManTimedEvents.ManagedEvent();

	private ManTimedEvents.ManagedEvent m_DeployZoneAnimationEvent = new ManTimedEvents.ManagedEvent();

	private ManTimedEvents.ManagedEvent m_UndeployZoneAnimationEvent = new ManTimedEvents.ManagedEvent();

	public bool Deployed => m_Deployed;

	public float ZoneRadius => m_ZoneCurrentRadius;

	public Vector3 GetZonePosition()
	{
		return m_Zone.transform.position;
	}

	public override void HandleCollision(Damageable damageable, Vector3 hitPoint, Collider otherCollider, bool ForceDestroy)
	{
		base.HandleCollision(damageable, hitPoint, otherCollider, ForceDestroy);
		if (m_DeployType == DeployType.DeployOnCollide)
		{
			TryDeploy();
		}
	}

	protected override void OnLifetimeEnd()
	{
		BeginUndeploy();
	}

	private void BeginUndeploy()
	{
		m_UndeployZoneAnimationEvent.Set(m_UndeployAnimationTimeSeconds, FinishUndeploy);
	}

	private void FinishUndeploy()
	{
		base.OnLifetimeEnd();
	}

	private void FinishDeployAnimation()
	{
	}

	protected override bool IsProjectileArmed()
	{
		return false;
	}

	private void TryDeploy()
	{
		if (!m_Deployed && !m_DeployZoneEvent.IsSet)
		{
			BeginDeploy();
		}
	}

	private void BeginDeploy()
	{
		m_DeployZoneEvent.Set(m_DeployDelay, delegate
		{
			m_DeployZoneAnimationEvent.Set(m_DeployAnimationTimeSeconds, FinishDeployAnimation);
			m_Deployed = true;
		});
		if (m_HideProjectileOnDeploy)
		{
			ModifyProjectileVisibility(show: false);
		}
	}

	private void LerpZoneScale(float orig, float target, float percent)
	{
		Vector3 a = Vector3.one * orig * 2f;
		Vector3 b = Vector3.one * target * 2f;
		m_Zone.transform.SetLocalScaleIfChanged(Vector3.LerpUnclamped(a, b, percent));
		m_ZoneCurrentRadius = Mathf.LerpUnclamped(orig, target, percent);
	}

	private void OnStuck(bool isStuck, Projectile _, Visible __)
	{
		if (isStuck)
		{
			TryDeploy();
		}
	}

	private void OnPool()
	{
		if (m_DeployType == DeployType.DeployOnStick)
		{
			m_StuckToVisibleEvent.Subscribe(OnStuck);
		}
	}

	private void OnSpawn()
	{
		if (m_DeployType == DeployType.DeployOnCreate)
		{
			BeginDeploy();
		}
		LerpZoneScale(0f, m_ZoneTargetRadius, 0f);
	}

	private void OnRecycle()
	{
		m_Deployed = false;
		m_DeployZoneEvent.Clear();
	}

	private void Update()
	{
		if (m_DeployZoneAnimationEvent.IsSet)
		{
			LerpZoneScale(0f, m_ZoneTargetRadius, m_DeployCurve.Evaluate(m_DeployAnimationTimeSeconds - m_DeployZoneAnimationEvent.TimeRemaining / m_DeployAnimationTimeSeconds));
		}
		else if (m_UndeployZoneAnimationEvent.IsSet)
		{
			LerpZoneScale(m_ZoneTargetRadius, 0f, m_UndeployCurve.Evaluate(m_UndeployAnimationTimeSeconds - m_UndeployZoneAnimationEvent.TimeRemaining / m_UndeployAnimationTimeSeconds));
		}
	}
}
