using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TankBeam : MonoBehaviour
{
	private enum ReAnchorState
	{
		Ready,
		ReadySky,
		Trying,
		Done
	}

	[SerializeField]
	protected Transform beamEffect;

	[SerializeField]
	protected Transform beamBaseEffect;

	[SerializeField]
	protected float beamBaseYOffset = 0.5f;

	[SerializeField]
	protected AnimationCurve beamEffectScaleProfile;

	public float hoverClearance = 2f;

	public float beamInterpSpeed = 1f;

	public float spinSpeedManual = 4f;

	public float nudgeSpeedForward = 20f;

	public float nudgeSpeedRotate = 30f;

	public float nudgeDeadzone = 0.01f;

	public float interpDamageReleaseToleranceAngle = 10f;

	public float damageReleaseGraceTime = 0.7f;

	[SerializeField]
	private float m_AllowedActivationHeight = 100f;

	private bool m_ForceGizmosActive;

	public float floatFallSpeedCushion = 3f;

	public float floatAntigravMinimum = 0.7f;

	public float objectRepulsionMaximum = 100f;

	public float objectRepulsionDamping = 0.1f;

	public float beamParticleSpeed = 3f;

	public float beamParticleSize = 2.5f;

	public float beamLightRange = 5f;

	public static Event<Tank, bool> OnBeamEnabled;

	private Tank tank;

	private ParticleSystem[] m_BeamParticleSystems;

	private Light[] m_BeamLights;

	private Quaternion m_HoverOrient;

	private Vector3 m_HoverBase;

	private float m_NudgeRotate;

	private Vector3 m_NudgeStrafe;

	private float minBoundsCentreHeight;

	private float m_DamageCancelsBeamTime;

	protected ForceGizmo m_CoMForceGizmo;

	protected ForceGizmo m_CoLForceGizmo;

	protected ForceGizmo m_CoBTForceGizmo;

	protected ForceGizmo m_CoPTForceGizmo;

	private float m_CombinedBodyMass;

	private Vector3 m_CombinedBodyCOMLocal;

	private List<Rigidbody> m_ChildBodiesWorkingList = new List<Rigidbody>();

	private bool m_TutorialToggleBeamLocked;

	private bool m_SumoPrematchLock;

	private ReAnchorState m_ReAnchorState;

	private static readonly Bitfield<ObjectTypes> k_ObjectRepulsionMask = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Block,
		ObjectTypes.Chunk
	});

	public bool rotationLimit = true;

	public float rotationLimitSizeStart = 20f;

	public float rotationLimitTechSizeModifier = 1f;

	public bool IsActive { get; private set; }

	public float HeightAboveIdeal { get; private set; }

	public Vector3 m_LocalCenterOfLiftApproximation { get; private set; }

	public Vector3 m_LiftVectorApproximation { get; private set; }

	public Vector3 m_LocalCenterOfBoosterThrustApproximation { get; private set; }

	public Vector3 m_BoosterThrustVectorApproximation { get; private set; }

	public Vector3 m_LocalCenterOfPropellerThrustApproximation { get; private set; }

	public Vector3 m_PropellerThrustVectorApproximation { get; private set; }

	public float HoverHeight => tank.rbody.worldCenterOfMass.y - m_HoverBase.y;

	public bool IsLocked => m_TutorialToggleBeamLocked;

	public void SetHoverBase()
	{
		Vector3 vector = tank.rbody.position + tank.rbody.rotation * tank.blockBounds.center;
		beamEffect.SetPositionIfChanged(vector);
		m_HoverBase = Singleton.Manager<ManWorld>.inst.ProjectToGround(vector, hitScenery: true);
	}

	public void SetMinBoundsCentreHeight(float height)
	{
		minBoundsCentreHeight = height;
	}

	public void DisableAndAnchor(bool force)
	{
		m_ReAnchorState = ReAnchorState.Ready;
		EnableBeam(enable: false, force);
	}

	private void NetworkEnableBeam(bool enable, bool wasDamaged)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			tank.control.SetBeamControlState(enable);
			if ((bool)tank.netTech && (bool)tank.netTech.NetPlayer)
			{
				tank.netTech.NetPlayer.EnterBuildBeam(enable, wasDamaged);
			}
		}
	}

	public bool EnableBeam(bool enable, bool force = false, bool suppressInventory = false)
	{
		if (!force)
		{
			if (m_TutorialToggleBeamLocked || m_SumoPrematchLock || Time.timeScale == 0f)
			{
				return false;
			}
			if (IsActive == enable)
			{
				return false;
			}
			if (enable && !tank.grounded && HoverHeight > m_AllowedActivationHeight)
			{
				return false;
			}
			if (enable && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && (bool)tank && (bool)tank.netTech && (bool)tank.netTech.NetPlayer && tank.netTech.NetPlayer.BuildBeamCooldownActive())
			{
				return false;
			}
		}
		bool flag = enable != IsActive;
		if (flag)
		{
			tank.ControlsActive = !enable;
			IsActive = enable;
			HeightAboveIdeal = -1f;
			OnBeamEnabled.Send(tank, enable);
			if (IsActive)
			{
				bool flag2 = tank.IsAnchored || tank.Anchors.RetryAnchorOnBeam;
				m_ReAnchorState = ((!flag2) ? ReAnchorState.Done : (tank.IsSkyAnchored ? ReAnchorState.ReadySky : ReAnchorState.Ready));
				RecalculateCombinedMassCOM();
				CalculateLiftVectorApproximation();
				CalculateBoosterPropellerThrustVectorApproximation();
			}
			RefreshForceGizmosActive();
		}
		if (IsActive)
		{
			tank.Anchors.UnanchorAll(playAnim: false);
			tank.rbody.velocity = Vector3.zero;
			tank.rbody.angularVelocity = Vector3.zero;
			SetHoverBase();
			ResetHoverOrientation();
			if (flag && Quaternion.Angle(tank.rbody.rotation, m_HoverOrient) > interpDamageReleaseToleranceAngle)
			{
				m_DamageCancelsBeamTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() + damageReleaseGraceTime;
			}
			beamEffect.gameObject.SetActive(value: true);
			beamBaseEffect.gameObject.SetActive(value: true);
		}
		if (Singleton.playerTank == tank && (IsActive || flag) && Singleton.Manager<ManPurchases>.inst.IsPaletteAvailable())
		{
			if (IsActive)
			{
				suppressInventory = suppressInventory || Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() || (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManPointer>.inst.IsPaintingBlocked);
				if (!suppressInventory)
				{
					Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: true, UIShopBlockSelect.ExpandReason.Beam);
				}
			}
			else
			{
				Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: false, UIShopBlockSelect.ExpandReason.Beam);
			}
		}
		NetworkEnableBeam(enable, wasDamaged: false);
		return true;
	}

	public Quaternion CalcHoverOrientation()
	{
		if (!tank.blockman.GetRootBlock())
		{
			return Quaternion.LookRotation(tank.trans.forward.SetY(0f));
		}
		if (Mathf.Abs(tank.rootBlockTrans.forward.Dot(Vector3.up)) > 0.9f)
		{
			return tank.FacingOrientation(tank.rootBlockTrans.right.Cross(Vector3.up));
		}
		return tank.FacingOrientation(tank.rootBlockTrans.forward.SetY(0f));
	}

	private void ResetHoverOrientation()
	{
		m_HoverOrient = CalcHoverOrientation();
	}

	private void TryReAnchorBuilding(bool moveTech)
	{
		tank.trans.rotation = Quaternion.Slerp(tank.trans.rotation, m_HoverOrient, beamInterpSpeed * Time.deltaTime);
		tank.Anchors.TryAnchorAll(moveTech, fromAfterTechPopulate: true);
	}

	private void UpdateBeamEffect()
	{
		Vector3 newWorldPos = m_HoverBase + new Vector3(0f, beamBaseYOffset, 0f);
		beamBaseEffect.SetPositionAndRotationIfChanged(newWorldPos, Quaternion.identity);
		float time = 0f;
		if (tank.blockman.blockCount > 0)
		{
			time = tank.blockBounds.extents.magnitude;
		}
		float num = beamEffectScaleProfile.Evaluate(time);
		Vector3 vector = Vector3.one * num;
		beamEffect.SetLocalScaleIfChanged(vector.SetY(beamEffect.localScale.y));
		beamBaseEffect.SetLocalScaleIfChanged(vector);
		ParticleSystem[] beamParticleSystems = m_BeamParticleSystems;
		for (int i = 0; i < beamParticleSystems.Length; i++)
		{
			ParticleSystem.MainModule main = beamParticleSystems[i].main;
			main.startSpeedMultiplier = beamParticleSpeed * num;
			main.startSizeMultiplier = beamParticleSize * num;
		}
		Light[] beamLights = m_BeamLights;
		for (int i = 0; i < beamLights.Length; i++)
		{
			beamLights[i].range = beamLightRange * num;
		}
		Vector3 input = Singleton.cameraTrans.position - beamEffect.position;
		beamEffect.SetRotationIfChanged(Quaternion.LookRotation(-input.SetY(0f), Vector3.up));
	}

	public void RefreshForceGizmosActive()
	{
		SetForceGizmosActive(IsActive && tank == Singleton.playerTank && Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_UseForceGizmosInBuildBeam);
	}

	private void SetForceGizmosActive(bool state)
	{
		if (m_ForceGizmosActive == state)
		{
			return;
		}
		m_ForceGizmosActive = state;
		if (state)
		{
			m_CoMForceGizmo = ForceGizmo.SpawnForceGizmo(tank.trans, Globals.inst.m_ForceGizmo_Mass_Color, disableIfZero: false, 1f);
			m_CoLForceGizmo = ForceGizmo.SpawnForceGizmo(tank.trans, Globals.inst.m_ForceGizmo_Lift_Color, disableIfZero: true, 0.0007f);
			m_CoBTForceGizmo = ForceGizmo.SpawnForceGizmo(tank.trans, Globals.inst.m_ForceGizmo_Boosters_Color, disableIfZero: true, 0.029f);
			m_CoPTForceGizmo = ForceGizmo.SpawnForceGizmo(tank.trans, Globals.inst.m_ForceGizmo_Propellers_Color, disableIfZero: true, 0.029f);
			UpdateForceGizmos();
			return;
		}
		if (m_CoMForceGizmo != null)
		{
			m_CoMForceGizmo.Recycle();
			m_CoMForceGizmo = null;
		}
		if (m_CoLForceGizmo != null)
		{
			m_CoLForceGizmo.Recycle();
			m_CoLForceGizmo = null;
		}
		if (m_CoBTForceGizmo != null)
		{
			m_CoBTForceGizmo.Recycle();
			m_CoBTForceGizmo = null;
		}
		if (m_CoPTForceGizmo != null)
		{
			m_CoPTForceGizmo.Recycle();
			m_CoPTForceGizmo = null;
		}
	}

	private void UpdateForceGizmos()
	{
		if (m_ForceGizmosActive)
		{
			if (m_CoMForceGizmo != null)
			{
				m_CoMForceGizmo.SetForceVector(tank.rbody.centerOfMass, Vector3.down * tank.rbody.mass);
			}
			if (m_CoLForceGizmo != null)
			{
				m_CoLForceGizmo.SetForceVector(m_LocalCenterOfLiftApproximation, tank.rootBlockTrans.rotation * m_LiftVectorApproximation);
			}
			if (m_CoBTForceGizmo != null)
			{
				m_CoBTForceGizmo.SetForceVector(m_LocalCenterOfBoosterThrustApproximation, -(tank.rootBlockTrans.rotation * m_BoosterThrustVectorApproximation));
			}
			if (m_CoPTForceGizmo != null)
			{
				m_CoPTForceGizmo.SetForceVector(m_LocalCenterOfPropellerThrustApproximation, tank.rootBlockTrans.rotation * m_PropellerThrustVectorApproximation);
			}
			ForceGizmo.RescaleLinesToFitMaxSize();
		}
	}

	private void UpdateTankFloat()
	{
		float t = beamInterpSpeed * Time.deltaTime;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && tank.netTech != null && tank.netTech.NetPlayer != null && !tank.netTech.NetPlayer.IsActuallyLocalPlayer)
		{
			ResetHoverOrientation();
		}
		else if (m_NudgeRotate != 0f)
		{
			float num = nudgeSpeedRotate;
			if (rotationLimit)
			{
				float num2 = Mathf.Max(tank.blockBounds.size.x, tank.blockBounds.size.z);
				float num3 = Mathf.Clamp01(rotationLimitSizeStart / (num2 * rotationLimitTechSizeModifier));
				num *= num3;
			}
			Quaternion quaternion = Quaternion.AngleAxis(m_NudgeRotate * (num * Time.deltaTime), Vector3.up);
			m_HoverOrient = quaternion * m_HoverOrient;
		}
		Quaternion rotation = tank.rbody.rotation;
		Vector3 vector = rotation * tank.blockBounds.center;
		Quaternion quaternion2 = Quaternion.Slerp(rotation, m_HoverOrient, t);
		tank.rbody.MoveRotation(quaternion2);
		Vector3 vector2 = quaternion2 * tank.blockBounds.center;
		if (vector2 != vector)
		{
			tank.rbody.MovePosition(tank.rbody.position + vector - vector2);
		}
		float floatHeight = Mathf.Max(tank.rbody.centerOfMass.y - tank.blockBounds.min.y, minBoundsCentreHeight) + hoverClearance;
		m_HoverBase = Singleton.Manager<ManWorld>.inst.ProjectToGround(beamEffect.position, hitScenery: true);
		if (m_NudgeStrafe != Vector3.zero)
		{
			Vector3 vector3 = tank.rootBlockTrans.localToWorldMatrix.MultiplyVector(m_NudgeStrafe).SetY(0f) * (nudgeSpeedForward * Time.deltaTime);
			Vector3 b = tank.rbody.position + vector3;
			Vector3 position = Vector3.Lerp(tank.rbody.position, b, t);
			tank.visible.rbody.MovePosition(position);
		}
		ApplyFloatForce(tank.rbody, m_HoverBase, floatHeight);
		if (!tank.rbody.isKinematic)
		{
			tank.rbody.angularVelocity = Vector3.zero;
		}
		Vector3 velocity = Vector3.zero.SetY(tank.rbody.velocity.y);
		tank.rbody.velocity = velocity;
	}

	private void UpdateObjectRepulsion()
	{
		if (!Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() || !Mode<ModeMain>.inst.TutorialDisableBlockRemoval)
		{
			float radius = tank.dragSphere.radius;
			Vector3 position = tank.rbody.position + tank.rbody.rotation * tank.blockBounds.center;
			Singleton.Manager<ManSpawn>.inst.RepelVisiblesInRadius(position, radius, k_ObjectRepulsionMask, objectRepulsionMaximum, objectRepulsionDamping);
		}
	}

	private void RecalculateCombinedMassCOM()
	{
		GetComponentsInChildren(includeInactive: false, m_ChildBodiesWorkingList);
		m_CombinedBodyMass = 0f;
		m_CombinedBodyCOMLocal = Vector3.zero;
		foreach (Rigidbody childBodiesWorking in m_ChildBodiesWorkingList)
		{
			if (!(childBodiesWorking != tank.rbody) || !childBodiesWorking.isKinematic)
			{
				m_CombinedBodyMass += childBodiesWorking.mass;
				m_CombinedBodyCOMLocal += tank.trans.InverseTransformPoint(childBodiesWorking.worldCenterOfMass) * childBodiesWorking.mass;
			}
		}
		m_CombinedBodyCOMLocal /= m_CombinedBodyMass;
	}

	private void CalculateLiftVectorApproximation()
	{
		Vector3 airVelocity = tank.rootBlockTrans.forward * 100f;
		m_LiftVectorApproximation = Vector3.zero;
		m_LocalCenterOfLiftApproximation = Vector3.zero;
		float num = 0f;
		Vector3 zero = Vector3.zero;
		BlockManager.BlockIterator<ModuleWing>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleWing>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleWing current = enumerator.Current;
			Vector3 localCenterOfLift;
			Vector3 vector = current.QueryLiftInConditions(airVelocity, out localCenterOfLift);
			if (!vector.magnitude.Approximately(0f))
			{
				zero += vector;
				num += vector.magnitude;
				m_LocalCenterOfLiftApproximation += (current.block.trans.localPosition + current.block.trans.localRotation * localCenterOfLift) * vector.magnitude;
			}
		}
		Vector3 zero2 = Vector3.zero;
		BlockManager.BlockIterator<ModuleBalloon>.Enumerator enumerator2 = tank.blockman.IterateBlockComponents<ModuleBalloon>().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			ModuleBalloon current2 = enumerator2.Current;
			Vector3 centerOfLiftWorld;
			Vector3 vector2 = current2.CalculateLiftForce(out centerOfLiftWorld);
			if (!vector2.magnitude.Approximately(0f))
			{
				zero2 += vector2;
				num += vector2.magnitude;
				m_LocalCenterOfLiftApproximation += (current2.block.trans.localPosition + current2.block.trans.localRotation * current2.block.trans.InverseTransformPoint(centerOfLiftWorld)) * vector2.magnitude;
			}
		}
		m_LocalCenterOfLiftApproximation /= ((num == 0f) ? 1f : num);
		m_LiftVectorApproximation = Quaternion.Inverse(tank.rootBlockTrans.rotation) * zero + zero2;
	}

	private void CalculateBoosterPropellerThrustVectorApproximation()
	{
		m_LocalCenterOfBoosterThrustApproximation = Vector3.zero;
		m_LocalCenterOfPropellerThrustApproximation = Vector3.zero;
		m_PropellerThrustVectorApproximation = Vector3.zero;
		m_BoosterThrustVectorApproximation = Vector3.zero;
		float num = 0f;
		float num2 = 0f;
		BlockManager.BlockIterator<ModuleBooster>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleBooster>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleBooster current = enumerator.Current;
			Vector3 centerOfThrustLocal;
			Vector3 vector = current.QueryBoosterThrustVectorApproximation(out centerOfThrustLocal);
			if (vector.magnitude != 0f)
			{
				m_BoosterThrustVectorApproximation += vector;
				num += vector.magnitude;
				m_LocalCenterOfBoosterThrustApproximation += centerOfThrustLocal * vector.magnitude;
			}
			Vector3 centerOfThrustLocal2;
			Vector3 vector2 = current.QueryPropellerThrustVectorApproximation(out centerOfThrustLocal2);
			if (vector2.magnitude != 0f)
			{
				m_PropellerThrustVectorApproximation += vector2;
				num2 += vector2.magnitude;
				m_LocalCenterOfPropellerThrustApproximation += centerOfThrustLocal2 * vector2.magnitude;
			}
		}
		Quaternion quaternion = Quaternion.Inverse(tank.rootBlockTrans.rotation);
		m_LocalCenterOfBoosterThrustApproximation /= ((num == 0f) ? 1f : num);
		m_BoosterThrustVectorApproximation = quaternion * m_BoosterThrustVectorApproximation;
		m_LocalCenterOfPropellerThrustApproximation /= ((num2 == 0f) ? 1f : num2);
		m_PropellerThrustVectorApproximation = quaternion * m_PropellerThrustVectorApproximation;
	}

	private void ApplyFloatForce(Rigidbody rbody, Vector3 floatBase, float floatHeight)
	{
		if (tank.EnableGravity)
		{
			HeightAboveIdeal = rbody.worldCenterOfMass.y - floatBase.y - floatHeight;
			float num = 1f / Mathf.Max(HeightAboveIdeal + 1f, floatAntigravMinimum);
			float num2 = Mathf.Max(0f - rbody.velocity.y, 0f);
			float num3 = (0f - Physics.gravity.y) * num + num2 * num2 * floatFallSpeedCushion;
			Vector3 force = new Vector3(0f, num3 * m_CombinedBodyMass, 0f);
			rbody.AddForceAtPosition(force, rbody.position + rbody.rotation * m_CombinedBodyCOMLocal);
		}
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		if (IsActive && Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() >= m_DamageCancelsBeamTime)
		{
			NetworkEnableBeam(enable: false, wasDamaged: true);
			EnableBeam(enable: false);
		}
	}

	private void OnTechResetPhysics()
	{
		if (IsActive)
		{
			RecalculateCombinedMassCOM();
			CalculateLiftVectorApproximation();
			CalculateBoosterPropellerThrustVectorApproximation();
		}
	}

	private void OnMultiplayerPhaseEnter(NetController.Phase gamePhase)
	{
		if (gamePhase == NetController.Phase.Playing && IsActive && Singleton.Manager<ManPurchases>.inst.IsPaletteAvailable() && !Singleton.Manager<ManPointer>.inst.IsPaintingBlocked && !Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded() && !Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect))
		{
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: true, UIShopBlockSelect.ExpandReason.Beam);
		}
	}

	private void OnPool()
	{
		tank = GetComponent<Tank>();
		m_BeamParticleSystems = beamBaseEffect.GetComponentsInChildren<ParticleSystem>();
		m_BeamLights = beamBaseEffect.GetComponentsInChildren<Light>();
		EnableBeam(enable: false, force: true);
		tank.UpdateEvent.Subscribe(OnUpdate);
		tank.FixedUpdateEvent.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		IsActive = false;
		m_ReAnchorState = ReAnchorState.Done;
		beamEffect.gameObject.SetActive(value: false);
		beamBaseEffect.gameObject.SetActive(value: false);
		tank.DamageEvent.Subscribe(OnDamaged);
		tank.ResetPhysicsEvent.Subscribe(OnTechResetPhysics);
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Subscribe(OnMultiplayerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Subscribe(OnMultiplayerPhaseEnter);
	}

	private void OnRecycle()
	{
		EnableBeam(enable: false);
		tank.DamageEvent.Unsubscribe(OnDamaged);
		tank.ResetPhysicsEvent.Unsubscribe(OnTechResetPhysics);
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Unsubscribe(OnMultiplayerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Unsubscribe(OnMultiplayerPhaseEnter);
	}

	private void OnUpdate()
	{
		bool flag = Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() || Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeGauntlet>();
		m_TutorialToggleBeamLocked = Mode<ModeMain>.inst.TutorialLockBeam && flag;
		m_SumoPrematchLock = IsActive && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeSumo>() && !Mode<ModeSumo>.inst.Designing && !Mode<ModeSumo>.inst.Playing;
		bool flag2 = false;
		if (IsActive && Singleton.playerTank == tank && !Singleton.Manager<ManGameMode>.inst.LockPlayerControls)
		{
			bool num = m_TutorialToggleBeamLocked || (Mode<ModeMain>.inst.TutorialLockBeamMove && flag);
			bool flag3 = Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying() || (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManGameMode>.inst.LockPlayerControls);
			bool flag4 = Singleton.Manager<CameraManager>.inst.IsCurrent<FirstPersonFlyCam>();
			flag2 = !num && !flag3 && !flag4;
		}
		if (flag2)
		{
			m_NudgeRotate = Singleton.Manager<ManInput>.inst.GetAxis(1);
			m_NudgeStrafe = Singleton.Manager<ManInput>.inst.GetAxis2D(68, 0).ToVector3XZ();
			if (Mathf.Abs(m_NudgeRotate) < nudgeDeadzone)
			{
				m_NudgeRotate = 0f;
			}
			if (m_NudgeStrafe.sqrMagnitude < nudgeDeadzone * nudgeDeadzone)
			{
				m_NudgeStrafe = Vector3.zero;
			}
			else if (m_NudgeStrafe.sqrMagnitude > 1f)
			{
				m_NudgeStrafe.Normalize();
			}
		}
		else
		{
			m_NudgeRotate = 0f;
			m_NudgeStrafe = Vector3.zero;
		}
		if (IsActive || m_ReAnchorState != ReAnchorState.Done)
		{
			UpdateBeamEffect();
		}
		if (IsActive)
		{
			UpdateObjectRepulsion();
		}
	}

	private void OnFixedUpdate()
	{
		if (IsActive)
		{
			tank.grounded = true;
			UpdateTankFloat();
			UpdateForceGizmos();
			return;
		}
		switch (m_ReAnchorState)
		{
		case ReAnchorState.Ready:
			if (!tank.grounded)
			{
				m_ReAnchorState = ReAnchorState.Trying;
			}
			break;
		case ReAnchorState.ReadySky:
			TryReAnchorBuilding(moveTech: false);
			if (!tank.IsAnchored)
			{
				tank.Anchors.RetryAnchorOnBeam = true;
				if (tank == Singleton.playerTank)
				{
					Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Send();
				}
			}
			m_ReAnchorState = ReAnchorState.Done;
			break;
		case ReAnchorState.Trying:
			if (!tank.grounded)
			{
				break;
			}
			TryReAnchorBuilding(moveTech: true);
			if (!tank.IsAnchored)
			{
				tank.Anchors.RetryAnchorOnBeam = true;
				if (tank == Singleton.playerTank)
				{
					Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Send();
				}
			}
			m_ReAnchorState = ReAnchorState.Done;
			break;
		case ReAnchorState.Done:
			if (beamEffect.gameObject.activeSelf)
			{
				beamEffect.gameObject.SetActive(value: false);
				beamBaseEffect.gameObject.SetActive(value: false);
			}
			minBoundsCentreHeight = 0f;
			break;
		}
	}
}
