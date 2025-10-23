using System;
using Rewired;
using UnityEngine;

public class TankCamera : CameraManager.Camera
{
	[Serializable]
	public struct FollowSpring
	{
		[HideInInspector]
		public float strength;

		public float speedDamper;

		public float speedDeadZone;

		public AnimationCurve speedFalloff;

		public float speedFalloffMaxIn;

		public float speedFalloffMaxOut;

		public float manualHoldDelay;

		public float[] elevationRange;

		public float elevationStrengthMultiplier;

		private float holdTimeout;

		private float smoothedTargetSpeed;

		public void Reset()
		{
			holdTimeout = 0f;
			smoothedTargetSpeed = 0f;
			if (elevationRange.Length != 2)
			{
				elevationRange = new float[2] { 10f, 35f };
			}
		}

		public void Hold()
		{
			holdTimeout = Time.time + manualHoldDelay;
		}

		public void ApplyAngleControl(Tank tank, float targetAngle, out float springAngleChange, float targetElevation, out float springElevationChange)
		{
			bool flag = tank != null;
			float deltaTime = Time.deltaTime;
			Vector3 vector = ((!flag) ? Vector3.forward : (tank.blockman.GetRootBlock() ? tank.rootBlockTrans.forward : tank.trans.forward));
			float b = 0f;
			if (holdTimeout < Time.time)
			{
				b = (flag ? Mathf.Max(tank.rbody.velocity.Dot(vector) - speedDeadZone, 0f) : 0f);
				b = speedFalloffMaxOut * speedFalloff.Evaluate(b / speedFalloffMaxIn);
			}
			smoothedTargetSpeed = Mathf.Lerp(smoothedTargetSpeed, b, speedDamper * deltaTime);
			float num = Mathf.Abs(smoothedTargetSpeed) * strength;
			float b2 = vector.HorizontalAngle() - targetAngle;
			springAngleChange = Mathf.LerpAngle(0f, b2, Mathf.Clamp01(num * deltaTime));
			float num2 = elevationRange[0] - targetElevation;
			float num3 = elevationRange[1] - targetElevation;
			float num4 = 0f;
			if (Mathf.Sign(num2) == Mathf.Sign(num3))
			{
				num4 = ((Mathf.Abs(num2) < Mathf.Abs(num3)) ? num2 : num3);
				num4 *= Mathf.Clamp01(num * elevationStrengthMultiplier * deltaTime);
			}
			springElevationChange = num4;
		}
	}

	[SerializeField]
	private float initialDist = 0.3f;

	[SerializeField]
	private float initialElev = 0.65f;

	[SerializeField]
	private float m_TargetPositionReactiveness = 3f;

	[SerializeField]
	private float m_AngleChangeResponsiveness = 4f;

	[SerializeField]
	private float m_ElevationChangeResponsiveness = 8f;

	[SerializeField]
	private float m_DistanceChangeResponsiveness = 8f;

	[SerializeField]
	private float elevationMinNormal = -20f;

	[SerializeField]
	private float elevationMinInBeamMode = -80f;

	[SerializeField]
	private float elevationMax = 60f;

	[SerializeField]
	private float distanceMin = 25f;

	[SerializeField]
	private float distanceMax = 70f;

	[SerializeField]
	private float groundClearance = 2f;

	[SerializeField]
	private float tankBoundsDistanceMultiplier = 2f;

	[SerializeField]
	private float tankBoundsDistanceMinMultiplier = 0.5f;

	[SerializeField]
	private float tankBoundsDistanceMaxMultiplier = 0.5f;

	[SerializeField]
	private float tankBoundsMaxZoomDeltaReduction;

	[SerializeField]
	private float tankSpeedDistanceMultiplier = 10f;

	[SerializeField]
	private float tankVelocityLookahead = 1f;

	[SerializeField]
	private float tankVelDamping = 0.05f;

	[SerializeField]
	private float transitionTime = 1f;

	[SerializeField]
	private AnimationCurve transitionInterpProfile;

	[SerializeField]
	private float transitionMultiplierRotation = 1.5f;

	[SerializeField]
	private FollowSpring m_FollowSpring;

	[SerializeField]
	private AnimationCurve m_CameraSpinStrengthCurve;

	public float m_InitialCurveValue;

	[SerializeField]
	private float m_TargetLockTurnStrength = 0.2f;

	[SerializeField]
	private float m_TargetLockMaxTurnSpeed = 5f;

	[SerializeField]
	private float m_TargetLockFOVPadH = 15f;

	[SerializeField]
	private float m_TargetLockFOVPadV = 3f;

	[SerializeField]
	private float m_MouseDragIgnoreZonePx = 10f;

	[SerializeField]
	[Header("Debug")]
	private bool m_AngleControlEnabled = true;

	[SerializeField]
	private bool m_ElevationControlEnabled = true;

	[SerializeField]
	private bool m_DistanceControlEnabled = true;

	private WorldPosition m_CurrentFocalPosition;

	private WorldPosition m_LastValidFocalTargetPosition;

	private float m_TargetAngle;

	private float m_TargetElevation;

	private float m_TargetDistance;

	private float m_CurrentAngle;

	private float m_CurrentElevation;

	private float m_CurrentDistance;

	private Vector3 m_TankVel2DSmoothed;

	private Vector3 m_CurrentActualFocalPosition;

	private float m_CurrentActualCameraDistance;

	private bool m_ControllingCameraWithMouse;

	private Vector2 m_CameraControlPreviousMousePos;

	private float m_TransitionProgress = 1f;

	private static int m_Transition_PrevFocussedVisibleID;

	private WorldPosition m_Transition_PrevTargetPosition;

	private Quaternion m_Transition_PrevCameraRotation;

	private float m_Transition_PrevActualCameraDistance;

	private bool m_UpdateCamera = true;

	private bool m_CameraControlEnabled = true;

	private bool m_hideHud;

	private Tank m_PassiveFollowTank;

	private TankBlock m_FocussedBuildBlock;

	private Vector3 m_FocussedBuildBlockLocalPosition;

	private Vector3 m_FocusBlockMouseDownPosition;

	private readonly KeySequence m_KeySeqLockAng = new KeySequence("loka", 1f);

	private readonly KeySequence m_KeySeqLockElev = new KeySequence("loke", 1f);

	private readonly KeySequence m_KeySeqLockDist = new KeySequence("lokd", 1f);

	private float m_PreOverriddenMaxDistance;

	private bool m_OverrideMaxDistanceEnabled;

	private Vector3 m_CameraShakeDirection = Vector3.zero;

	private float m_CameraShakeTimeRemaining;

	private float m_CameraShakeMagnitude;

	private float m_CameraShakeFrequency = 1f;

	public static TankCamera inst { get; private set; }

	public float FollowSpringStrength => m_FollowSpring.strength;

	private bool IsPassiveFollowCamera => m_PassiveFollowTank.IsNotNull();

	public void FreezeCamera(bool freezeCamera)
	{
		m_UpdateCamera = !freezeCamera;
	}

	public void SetMouseControlEnabled(bool enabled)
	{
		m_CameraControlEnabled = enabled;
		if (!enabled && m_ControllingCameraWithMouse)
		{
			EndSpinControlMouse();
		}
	}

	public void BeginSpinControlMouse()
	{
		if (m_CameraControlEnabled)
		{
			m_ControllingCameraWithMouse = true;
			m_CameraControlPreviousMousePos = Input.mousePosition.ToVector2XY();
		}
	}

	public void EndSpinControlMouse()
	{
		m_ControllingCameraWithMouse = false;
	}

	public void ManualZoom(float distDelta)
	{
		if (m_CameraControlEnabled && m_DistanceControlEnabled)
		{
			if (tankBoundsMaxZoomDeltaReduction > 0f && Singleton.playerTank.IsNotNull() && Singleton.playerTank.blockman.blockCount > 0)
			{
				distDelta *= 1f - tankBoundsMaxZoomDeltaReduction * (Singleton.playerTank.blockBounds.extents.magnitude / 256f);
			}
			m_TargetDistance += distDelta;
		}
	}

	public void SetFollowSpringStrength(float strengthValue)
	{
		m_FollowSpring.strength = m_CameraSpinStrengthCurve.Evaluate(strengthValue);
	}

	public void ClearCaches()
	{
		m_TankVel2DSmoothed = Vector3.zero;
		m_FollowSpring.Reset();
	}

	public void ClearPreviousInterpolationTarget()
	{
		m_Transition_PrevFocussedVisibleID = -1;
	}

	public float GetDistanceMax()
	{
		return distanceMax;
	}

	public void DistanceMaxOverride(float overrideValue)
	{
		if (!m_OverrideMaxDistanceEnabled)
		{
			m_PreOverriddenMaxDistance = distanceMax;
		}
		distanceMax = overrideValue;
		m_OverrideMaxDistanceEnabled = true;
	}

	public void DistanceMaxOverrideClear()
	{
		if (m_OverrideMaxDistanceEnabled)
		{
			distanceMax = m_PreOverriddenMaxDistance;
			m_OverrideMaxDistanceEnabled = false;
		}
	}

	public void SetFollowTech(Tank techToFollow)
	{
		m_PassiveFollowTank = techToFollow;
	}

	private void OnModeFinished(Mode mode)
	{
		DistanceMaxOverrideClear();
	}

	public override void Enable()
	{
		ClearCaches();
		ClearPreviousInterpolationTarget();
		m_TransitionProgress = 1f;
		Vector3 vector = Singleton.cameraTrans.rotation * Vector3.forward;
		m_CurrentAngle = (m_TargetAngle = vector.HorizontalAngle());
		m_CurrentElevation = (m_TargetElevation = Mathf.Lerp(elevationMinNormal, elevationMax, initialElev));
		m_CurrentActualCameraDistance = (m_CurrentDistance = (m_TargetDistance = Mathf.Lerp(distanceMin, distanceMax, initialDist)));
		Vector3 scenePos = (m_CurrentActualFocalPosition = Singleton.cameraTrans.position + vector * m_CurrentDistance);
		m_LastValidFocalTargetPosition = WorldPosition.FromScenePosition(in scenePos);
		m_CurrentFocalPosition = m_LastValidFocalTargetPosition;
		m_ControllingCameraWithMouse = false;
	}

	private void UpdateInput()
	{
		if (Singleton.Manager<ManPauseGame>.inst.IsPaused || Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying() || !m_CameraControlEnabled)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(96))
		{
			m_hideHud = !m_hideHud;
		}
		Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.PlayerHideHud, !m_hideHud);
		Singleton.Manager<ManOnScreenMessages>.inst.ShowCanvas(!m_hideHud);
		float deltaTime = Time.deltaTime;
		if (m_ControllingCameraWithMouse)
		{
			Vector2 vector = Input.mousePosition.ToVector2XY();
			Vector2 vector2 = vector - m_CameraControlPreviousMousePos;
			m_CameraControlPreviousMousePos = vector;
			Vector2 vector3 = vector2 / Screen.width * Globals.inst.m_CamSpinSpeedMouseMultiplier;
			if (m_AngleControlEnabled)
			{
				float num = vector3.x * Globals.inst.m_RuntimeCameraSpinSensHorizontal;
				m_TargetAngle += num;
			}
			if (m_ElevationControlEnabled)
			{
				float num2 = vector3.y * Globals.inst.m_RuntimeCameraSpinSensVertical;
				m_TargetElevation += num2;
			}
		}
		else
		{
			Vector2 axis2D = Singleton.Manager<ManInput>.inst.GetAxis2D(6, 7);
			axis2D *= deltaTime;
			if (m_AngleControlEnabled)
			{
				m_TargetAngle += axis2D.x * Globals.inst.m_CamSpinSpeedGamepadMultiplier.x * Globals.inst.m_RuntimeCameraSpinSensHorizontal;
			}
			if (m_ElevationControlEnabled)
			{
				m_TargetElevation += axis2D.y * Globals.inst.m_CamSpinSpeedGamepadMultiplier.y * Globals.inst.m_RuntimeCameraSpinSensVertical;
			}
		}
	}

	public void SetCameraShake(float duration, float magnitude, float frequency)
	{
		if (duration == 0f)
		{
			m_CameraShakeTimeRemaining = 0f;
			return;
		}
		m_CameraShakeDirection = UnityEngine.Random.onUnitSphere;
		m_CameraShakeTimeRemaining = duration;
		m_CameraShakeMagnitude = magnitude / duration;
		m_CameraShakeFrequency = (float)Math.PI * 2f * frequency;
	}

	private void UpdatePlayerControlledCamera(Tank tankToFollow)
	{
		bool flag = tankToFollow.IsNotNull();
		if (!flag && !Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab && (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || Singleton.Manager<ManNetwork>.inst.MyPlayer == null || !Singleton.Manager<ManNetwork>.inst.MyPlayer.HasTech()))
		{
			ClearPreviousInterpolationTarget();
			return;
		}
		if (m_UpdateCamera)
		{
			if (tankToFollow.IsNotNull())
			{
				InterpolateToTargetOnFocusChange(tankToFollow);
			}
			float deltaTime = Time.deltaTime;
			bool num = flag && (bool)tankToFollow.beam && tankToFollow.beam.IsActive;
			Vector3 scenePos;
			if (flag)
			{
				scenePos = ((m_FocussedBuildBlock != null) ? m_FocussedBuildBlock.centreOfMassWorld : tankToFollow.WorldCenterOfMass);
				m_LastValidFocalTargetPosition = WorldPosition.FromScenePosition(in scenePos);
			}
			else
			{
				scenePos = m_LastValidFocalTargetPosition.ScenePosition;
			}
			Vector3 scenePos2 = Vector3.Lerp(m_CurrentFocalPosition.ScenePosition, t: m_TargetPositionReactiveness * deltaTime, b: scenePos);
			m_CurrentFocalPosition = WorldPosition.FromScenePosition(in scenePos2);
			m_TankVel2DSmoothed = Vector3.Lerp(b: flag ? tankToFollow.rbody.velocity : Vector3.zero, a: m_TankVel2DSmoothed, t: tankVelDamping);
			m_CurrentActualFocalPosition = scenePos2 + m_TankVel2DSmoothed * tankVelocityLookahead;
			Vector3 position = Singleton.cameraTrans.position;
			float num2;
			if (!num)
			{
				Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(position) + Vector3.up * groundClearance;
				float f = Vector3.Dot((scenePos - vector).normalized, Vector3.down);
				num2 = Mathf.Max(b: 90f - Mathf.Acos(f) * 57.29578f, a: elevationMinNormal);
				if (num2 > elevationMax)
				{
					num2 = elevationMax;
				}
			}
			else
			{
				num2 = elevationMinInBeamMode;
			}
			float num3 = distanceMin;
			if (!num && flag && tankBoundsDistanceMinMultiplier > 0f && tankToFollow.blockman.blockCount > 0)
			{
				num3 += tankToFollow.blockBounds.extents.magnitude * tankBoundsDistanceMinMultiplier;
			}
			float num4 = distanceMax;
			if (flag && tankBoundsDistanceMaxMultiplier > 0f && tankToFollow.blockman.blockCount > 0)
			{
				num4 += tankToFollow.blockBounds.extents.magnitude * tankBoundsDistanceMaxMultiplier;
			}
			m_TargetElevation = Mathf.Clamp(m_TargetElevation, num2, elevationMax);
			m_TargetDistance = Mathf.Clamp(m_TargetDistance, num3, num4);
			float runtimeCameraSpinInterpSpeed = Globals.inst.m_RuntimeCameraSpinInterpSpeed;
			float t = Mathf.Min(m_AngleChangeResponsiveness * runtimeCameraSpinInterpSpeed * deltaTime, 1f);
			float t2 = Mathf.Min(m_ElevationChangeResponsiveness * runtimeCameraSpinInterpSpeed * deltaTime, 1f);
			float t3 = Mathf.Min(m_DistanceChangeResponsiveness * runtimeCameraSpinInterpSpeed * deltaTime, 1f);
			m_CurrentAngle = Mathf.Lerp(m_CurrentAngle, m_TargetAngle, t);
			m_CurrentElevation = Mathf.Lerp(m_CurrentElevation, m_TargetElevation, t2);
			m_CurrentDistance = Mathf.Lerp(m_CurrentDistance, m_TargetDistance, t3);
			Vector3 vector2 = m_CurrentActualFocalPosition - position;
			bool flag2 = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && Singleton.Manager<ManInput>.inst.GetAxis2D(6, 7) != Vector2.zero;
			float angleAdjustment;
			float elevationAdjustment;
			if (m_ControllingCameraWithMouse || flag2)
			{
				m_FollowSpring.Hold();
			}
			else if (TryKeepManualTargetInView(tankToFollow, position, vector2, out angleAdjustment, out elevationAdjustment))
			{
				m_TargetAngle += angleAdjustment;
				m_TargetElevation += elevationAdjustment;
				m_FollowSpring.Hold();
			}
			m_FollowSpring.ApplyAngleControl(tankToFollow, m_TargetAngle, out var springAngleChange, m_TargetElevation, out var springElevationChange);
			m_TargetAngle = Mathf.LerpAngle(m_TargetAngle, m_TargetAngle + springAngleChange, 1f);
			m_TargetElevation += springElevationChange;
			float num5 = m_CurrentDistance + m_TankVel2DSmoothed.magnitude * tankSpeedDistanceMultiplier;
			float num6 = Mathf.InverseLerp(distanceMin, num4, m_CurrentDistance);
			if (flag && tankToFollow.blockman.blockCount > 0)
			{
				num5 += num6 * (tankToFollow.blockBounds.extents.magnitude * tankBoundsDistanceMultiplier);
			}
			float t4 = m_TargetPositionReactiveness * deltaTime;
			m_CurrentActualCameraDistance = Mathf.Lerp(m_CurrentActualCameraDistance, num5, t4);
			float magnitude = vector2.magnitude;
			float f2 = Vector3.Dot(vector2, -Vector3.up) / magnitude;
			Quaternion quaternion = Quaternion.Euler(new Vector3(Mathf.Max(Mathf.Lerp(90f - Mathf.Acos(f2) * 57.29578f, m_CurrentElevation, t2), num2), m_CurrentAngle, 0f));
			Vector3 vector3 = m_CurrentActualFocalPosition + quaternion * new Vector3(0f, 0f, 0f - m_CurrentActualCameraDistance);
			if (m_CameraShakeTimeRemaining > 0f)
			{
				vector3 += m_CameraShakeDirection * Mathf.Sin(m_CameraShakeFrequency * m_CameraShakeTimeRemaining) * m_CameraShakeMagnitude * m_CameraShakeTimeRemaining;
			}
			quaternion = Quaternion.LookRotation(m_CurrentActualFocalPosition - vector3, Vector3.up);
			if (m_TransitionProgress >= 1f)
			{
				Singleton.cameraTrans.position = vector3;
				Singleton.cameraTrans.rotation = quaternion;
			}
			else
			{
				m_TransitionProgress = Mathf.Min(m_TransitionProgress + deltaTime / transitionTime, 1f);
				float t5 = transitionInterpProfile.Evaluate(m_TransitionProgress);
				Vector3 vector4 = Vector3.Lerp(m_Transition_PrevTargetPosition.ScenePosition, m_CurrentActualFocalPosition, t5);
				Quaternion quaternion2 = Quaternion.Slerp(m_Transition_PrevCameraRotation, quaternion, t5);
				float num7 = Mathf.Lerp(m_Transition_PrevActualCameraDistance, m_CurrentActualCameraDistance, t5);
				Vector3 vector5 = vector4 + quaternion2 * new Vector3(0f, 0f, 0f - num7);
				Singleton.cameraTrans.position = vector5;
				Quaternion b = Quaternion.LookRotation(m_CurrentActualFocalPosition - vector5);
				float t6 = transitionInterpProfile.Evaluate(m_TransitionProgress * transitionMultiplierRotation);
				Singleton.cameraTrans.rotation = Quaternion.Slerp(m_Transition_PrevCameraRotation, b, t6);
			}
		}
		UpdateDOFFocalDistance(tankToFollow);
	}

	private void GauntletCameraUpdate()
	{
		float t = m_TargetPositionReactiveness * Time.deltaTime;
		Vector3 worldCenterOfMass = m_PassiveFollowTank.WorldCenterOfMass;
		Vector3 scenePosition = m_CurrentFocalPosition.ScenePosition;
		scenePosition = ((!(m_CurrentFocalPosition == default(WorldPosition))) ? Vector3.Lerp(scenePosition, worldCenterOfMass, t) : worldCenterOfMass);
		m_CurrentFocalPosition = WorldPosition.FromScenePosition(in scenePosition);
		Vector3 position = Singleton.cameraTrans.position;
		float num = 10f;
		float num2 = 10f;
		Vector3 b = scenePosition - m_PassiveFollowTank.trans.forward * num + Vector3.up * num2;
		b = Vector3.Lerp(position, b, t);
		Quaternion rotation = Quaternion.LookRotation(scenePosition - b);
		Singleton.cameraTrans.position = b;
		Singleton.cameraTrans.rotation = rotation;
		UpdateDOFFocalDistance(m_PassiveFollowTank);
	}

	private void EnableDOFIfNeeded()
	{
		if (Singleton.Manager<CameraManager>.inst.DOF == null)
		{
			return;
		}
		if (Singleton.Manager<CameraManager>.inst.DOF.enabled && !Singleton.Manager<ManUI>.inst.IsStackEmpty() && !Singleton.Manager<ManScreenshot>.inst.TakingSnapshot)
		{
			Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance(0f);
		}
		if (!Singleton.Manager<CameraManager>.inst.DOF.enabled)
		{
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			if (currentUser == null || currentUser.m_GraphicsSettings.m_DOF)
			{
				Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: true);
			}
		}
	}

	private void UpdateDOFFocalDistance(Tank followTank)
	{
		if (Singleton.Manager<CameraManager>.inst.DOF != null && Singleton.Manager<CameraManager>.inst.DOF.enabled && followTank != null)
		{
			Vector3 vector = ((m_FocussedBuildBlock != null) ? m_FocussedBuildBlock.trans.localPosition : followTank.blockBounds.center);
			Vector3 vector2 = followTank.rbody.position + followTank.rbody.rotation * vector;
			Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance((vector2 - Singleton.cameraTrans.position).magnitude);
		}
	}

	private void UpdateBuildBeamBlockFocusControl(Tank tankToFollow, bool inBeamMode)
	{
		if (inBeamMode)
		{
			bool buttonDown = Singleton.Manager<ManInput>.inst.GetButtonDown(44);
			bool flag = Singleton.Manager<ManInput>.inst.IsCurrentInputSource(44, ControllerType.Mouse);
			if (buttonDown && flag)
			{
				m_FocusBlockMouseDownPosition = Input.mousePosition;
			}
			if (Singleton.Manager<ManPointer>.inst.DraggingItem == null && ((buttonDown && !flag) || (Singleton.Manager<ManInput>.inst.GetButtonUp(44) && (Input.mousePosition - m_FocusBlockMouseDownPosition).sqrMagnitude <= m_MouseDragIgnoreZonePx * m_MouseDragIgnoreZonePx)) && Singleton.Manager<ManPointer>.inst.targetVisible != null && Singleton.Manager<ManPointer>.inst.targetVisible.block != null && Singleton.Manager<ManPointer>.inst.targetVisible.block.tank == tankToFollow)
			{
				SetFocussedBuildBlock(Singleton.Manager<ManPointer>.inst.targetVisible.block);
			}
		}
		UpdateBuildBeamFocus(tankToFollow, inBeamMode);
	}

	private void UpdateBuildBeamFocus(Tank tankToFollow, bool inBeamMode)
	{
		if (!(m_FocussedBuildBlock != null))
		{
			return;
		}
		if (!inBeamMode)
		{
			SetFocussedBuildBlock(null);
		}
		else
		{
			if (!(m_FocussedBuildBlock.tank != tankToFollow))
			{
				return;
			}
			TankBlock focussedBuildBlock = null;
			float num = float.MaxValue;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tankToFollow.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				float sqrMagnitude = (m_FocussedBuildBlockLocalPosition - current.trans.localPosition).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					focussedBuildBlock = current;
					num = sqrMagnitude;
				}
			}
			SetFocussedBuildBlock(focussedBuildBlock);
		}
	}

	private void SetFocussedBuildBlock(TankBlock focalBlock)
	{
		m_FocussedBuildBlock = focalBlock;
		m_FocussedBuildBlockLocalPosition = ((m_FocussedBuildBlock != null) ? m_FocussedBuildBlock.trans.localPosition : Vector3.zero);
	}

	private void InterpolateToTargetOnFocusChange(Tank tankToFollow)
	{
		Visible visible = ((m_FocussedBuildBlock != null) ? m_FocussedBuildBlock.visible : tankToFollow.visible);
		if (visible.ID != m_Transition_PrevFocussedVisibleID && m_Transition_PrevFocussedVisibleID != -1)
		{
			StartInterpolationToNewTarget(visible);
		}
		m_Transition_PrevFocussedVisibleID = visible.ID;
	}

	private void StartInterpolationToNewTarget(Visible focalVisible)
	{
		Quaternion rotation = Singleton.cameraTrans.rotation;
		m_Transition_PrevTargetPosition = WorldPosition.FromScenePosition(Singleton.cameraTrans.position + rotation * Vector3.forward * m_CurrentActualCameraDistance);
		m_Transition_PrevCameraRotation = rotation;
		m_Transition_PrevActualCameraDistance = m_CurrentActualCameraDistance;
		m_TransitionProgress = 0f;
	}

	private bool TryKeepManualTargetInView(Tank tankToFollow, Vector3 currentCameraPosition, Vector3 toTarget, out float angleAdjustment, out float elevationAdjustment)
	{
		bool result = false;
		angleAdjustment = 0f;
		elevationAdjustment = 0f;
		Visible visible = tankToFollow?.Weapons.GetManualTarget();
		if ((bool)visible)
		{
			Vector3 aimPoint = visible.GetAimPoint(tankToFollow.boundsCentreWorld);
			float num = (aimPoint - currentCameraPosition).HorizontalAngle();
			float num2 = Singleton.camera.fieldOfView * 0.5f;
			float num3 = num2 - m_TargetLockFOVPadV;
			float num4 = num2 * Singleton.camera.aspect - m_TargetLockFOVPadH;
			float num5 = m_CurrentAngle.NormalizedAngle();
			float num6 = num5 - num4;
			float num7 = num5 + num4;
			float angle = num6 - num;
			float angle2 = num - num7;
			angle = angle.NormalizedAngle();
			angle2 = angle2.NormalizedAngle();
			if (angle > 0f || angle2 > 0f)
			{
				if (angle > 0f && angle2 > 0f)
				{
					if (angle2 > angle)
					{
						angle2 = 0f;
					}
					else
					{
						angle = 0f;
					}
				}
				float a = 0f;
				if (angle > 0f)
				{
					a = (0f - angle) * m_TargetLockTurnStrength;
				}
				else if (angle2 > 0f)
				{
					a = angle2 * m_TargetLockTurnStrength;
				}
				angleAdjustment = Mathf.Min(a, m_TargetLockMaxTurnSpeed);
				result = angleAdjustment != 0f;
			}
			Vector3 a2 = aimPoint - currentCameraPosition;
			float f = a2.Dot(Vector3.up) / a2.magnitude;
			float num8 = 90f - Mathf.Acos(f) * 57.29578f;
			f = toTarget.Dot(Vector3.up) / toTarget.magnitude;
			float num9 = 90f - Mathf.Acos(f) * 57.29578f + num3;
			float num10 = num8 - num9;
			if (num10 > 0f)
			{
				elevationAdjustment = 0f - num10;
			}
		}
		return result;
	}

	private void Awake()
	{
		inst = this;
	}

	private void Start()
	{
		Enable();
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeFinished);
	}

	private void Update()
	{
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development))
		{
			if (m_KeySeqLockAng.Completed())
			{
				m_AngleControlEnabled = !m_AngleControlEnabled;
			}
			if (m_KeySeqLockElev.Completed())
			{
				m_ElevationControlEnabled = !m_ElevationControlEnabled;
			}
			if (m_KeySeqLockDist.Completed())
			{
				m_DistanceControlEnabled = !m_DistanceControlEnabled;
			}
		}
		if (!Singleton.Manager<ManUI>.inst.IsStackEmpty() && !Singleton.Manager<ManScreenshot>.inst.TakingSnapshot && Singleton.Manager<CameraManager>.inst.DOF != null && Singleton.Manager<CameraManager>.inst.DOF.enabled)
		{
			Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance(0f);
		}
		Tank playerTank = Singleton.playerTank;
		if (m_UpdateCamera && playerTank.IsNotNull() && !IsPassiveFollowCamera)
		{
			bool inBeamMode = (bool)playerTank.beam && playerTank.beam.IsActive;
			UpdateBuildBeamBlockFocusControl(playerTank, inBeamMode);
		}
		if (m_CameraShakeTimeRemaining > 0f)
		{
			m_CameraShakeTimeRemaining -= Time.deltaTime;
		}
		UpdateInput();
		EnableDOFIfNeeded();
	}

	private void FixedUpdate()
	{
		if ((bool)m_PassiveFollowTank)
		{
			GauntletCameraUpdate();
			return;
		}
		Tank playerTank = Singleton.playerTank;
		UpdatePlayerControlledCamera(playerTank);
	}
}
