#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleDriveBot : Module
{
	public enum StateType
	{
		AIState_SumoCharge,
		AIState_GetBehindTarget,
		AIState_FaceTarget,
		AIState_Waypoint,
		AIState_LookAround,
		AIState_RecoverCapsize,
		AIState_WaitUntilSeen,
		AIState_ChargeTarget,
		None
	}

	public float targetIdealRange = 5f;

	public float turnToleranceOuter = 45f;

	public float turnToleranceInner = 20f;

	public float m_DefaultThrottle = 1f;

	public float poweredTurnInsideWheel;

	public float waypointDistanceFullThrottle = 5f;

	public float turnAngleFullThrottle = 45f;

	public AnimationCurve throttleProfile;

	public float waypointReachedTolerance = 1f;

	public float waypointPlayerAngularBias = 45f;

	public float lookAroundAngleMin = 30f;

	public float lookAroundAngleMax = 150f;

	public float m_DefaultPatrolDistMin = 5f;

	public float m_DefaultPatrolDistMax = 15f;

	public float patrolThrottle = 0.8f;

	public float lookAroundPauseMin = 1f;

	public float lookAroundPauseMax = 3f;

	public float lookAroundThrottle = 0.5f;

	public float lostTargetMemoryTime = 1f;

	public float holdTargetDuration = 0.5f;

	public float stopCirclingDelay = 2f;

	public int controlPriority = 50;

	public string debugReportAIState;

	public string debugReportAIDetail;

	public string debugReportAISteering;

	public float debugReportThrottle;

	public float recoverTimeout = 3f;

	public float forceUnCapsizeTimeout = 5f;

	public float capsizedMinSpeed = 1f;

	private ModuleVision visionModule;

	public float m_ThrottleD;

	public float m_ThrottleT;

	private float m_PatrolDistMin;

	private float m_PatrolDistMax;

	private Action AIStateProcessor;

	private Transform hostileTarget;

	private Transform friendlyTarget;

	private Vector3 driveVector;

	private Vector3 idealAimPoint;

	private Vector3 lookAroundTargetDirection;

	private float lookAroundWaitDecay;

	private int lookAroundState;

	private float lostTargetChaseDecay;

	private float holdTargetCountdown;

	private float stopCirclingTimer;

	private float attackTargetRadius;

	private float recoverTime;

	private float forceUnCapsizeTime;

	private bool sumoBoostOn;

	private Vector3 waypointPosition;

	private Quaternion waypointRotation;

	private bool m_WaitingForPostSpawnEvent;

	private ManDamage.DamageInfo m_LastTankDamage;

	private StateType m_CurrentState = StateType.None;

	public bool TankIsRightWayUp { get; private set; }

	public bool TankIsMoving { get; private set; }

	private Vector3 tankCentrePosition => base.block.tank.boundsCentreWorld;

	private void AIProcessorGetBehindTarget()
	{
		if (base.block.tank.IsAnchored || Singleton.Manager<DebugUtil>.inst.debugAIsDontMove)
		{
			SetState(StateType.AIState_FaceTarget);
			return;
		}
		if (stopCirclingTimer > 0f)
		{
			stopCirclingTimer -= Time.deltaTime;
			if (stopCirclingTimer <= 0f)
			{
				SetState(StateType.AIState_FaceTarget);
				return;
			}
		}
		driveVector = Vector3.zero;
		float num = targetIdealRange + attackTargetRadius;
		Vector3 vector = waypointPosition - tankCentrePosition;
		vector.y = 0f;
		idealAimPoint = waypointPosition - waypointRotation * new Vector3(0f, 0f, num);
		float magnitude = vector.magnitude;
		bool num2 = Vector3.Dot(waypointRotation * Vector3.forward, -vector) > 0f;
		string text = "";
		if (!num2)
		{
			driveVector = idealAimPoint - tankCentrePosition;
			driveVector.y = 0f;
			text = "behind";
		}
		else
		{
			Vector3 vector2 = idealAimPoint - tankCentrePosition;
			vector2.y = 0f;
			bool flag = Vector3.Dot(vector, new Vector3(0f - vector2.z, 0f, vector2.x)) > 0f;
			float num3;
			if (magnitude < num)
			{
				if (Vector3.Dot(base.block.tank.rootBlockTrans.forward, vector) > 0f)
				{
					num3 = 90f + 90f * magnitude / num;
					text = "front (facing): spiral";
				}
				else
				{
					num3 = (flag ? 90 : (-90));
					text = "front (away): flank";
				}
			}
			else
			{
				num3 = Mathf.Asin(num / magnitude) * 57.29578f;
				text = "front (far): tangent";
			}
			driveVector = Quaternion.Euler(0f, flag ? num3 : (0f - num3), 0f) * vector;
		}
		if (base.block.tank.control.Movement.DriveToDestination(base.block.tank, driveVector, m_ThrottleD, m_ThrottleT))
		{
			base.block.tank.control.Movement.FaceDirection(base.block.tank, vector, 1f);
			text += " (in position)";
		}
		if (Singleton.Manager<DebugUtil>.inst.debugReportAIState)
		{
			debugReportAIDetail = text;
		}
		if ((bool)hostileTarget)
		{
			base.block.tank.control.Weapons.AimAtTarget(base.block.tank, waypointPosition, attackTargetRadius);
			if (!ManSpawn.IsPlayerTeam(base.block.tank.Team))
			{
				base.block.tank.control.ServerDetonateExplosiveBolt();
			}
		}
	}

	private void AIProcessorFaceTarget()
	{
		float num = targetIdealRange + attackTargetRadius;
		Vector3 vector = waypointPosition - tankCentrePosition;
		vector.y = 0f;
		driveVector = vector - vector.normalized * num;
		driveVector.y = 0f;
		TankControl.DriveRestriction driveRestriction = TankControl.DriveRestriction.ForwardOnly;
		if (vector.sqrMagnitude < num * num)
		{
			driveRestriction = TankControl.DriveRestriction.ReverseOnly;
		}
		if (Singleton.Manager<DebugUtil>.inst.debugReportAIState)
		{
			debugReportAIDetail = ((driveRestriction == TankControl.DriveRestriction.ForwardOnly) ? "too far" : "too close");
		}
		if (Singleton.Manager<DebugUtil>.inst.debugAIsDontMove)
		{
			base.block.tank.control.Movement.FaceDirection(base.block.tank, vector, 1f);
		}
		else if (base.block.tank.IsAnchored || base.block.tank.control.Movement.DriveToDestination(base.block.tank, driveVector, m_ThrottleD, m_ThrottleT, driveRestriction))
		{
			base.block.tank.control.Movement.FaceDirection(base.block.tank, vector, 1f);
			if (Singleton.Manager<DebugUtil>.inst.debugReportAIState)
			{
				debugReportAIDetail = "ideal range";
			}
			if (!hostileTarget && !friendlyTarget)
			{
				SetState(StateType.AIState_LookAround);
				return;
			}
			if ((bool)hostileTarget && base.block.tank.Weapons.MeleeWeaponCount != 0)
			{
				SetState(StateType.AIState_ChargeTarget);
			}
		}
		if ((bool)hostileTarget)
		{
			base.block.tank.control.Weapons.AimAtTarget(base.block.tank, waypointPosition, attackTargetRadius);
			if (!ManSpawn.IsPlayerTeam(base.block.tank.Team))
			{
				base.block.tank.control.ServerDetonateExplosiveBolt();
			}
		}
	}

	private void AIProcessorChargeTarget()
	{
		driveVector = waypointPosition - tankCentrePosition;
		driveVector.y = 0f;
		base.block.tank.control.Weapons.FireWeapons(base.block.tank);
		if (base.block.tank.control.Movement.DriveToDestination(base.block.tank, driveVector, m_ThrottleD, m_ThrottleT, TankControl.DriveRestriction.ForwardOnly))
		{
			SetState(StateType.AIState_FaceTarget);
		}
	}

	private void AIProcessorWaypoint()
	{
		if ((bool)hostileTarget || (bool)friendlyTarget)
		{
			SetState(StateType.AIState_FaceTarget);
			return;
		}
		if (Singleton.Manager<DebugUtil>.inst.debugAIsDontMove)
		{
			SetState(StateType.AIState_LookAround);
			return;
		}
		Vector3 toTarget = waypointPosition - tankCentrePosition;
		if (base.block.tank.control.Movement.DriveToDestination(base.block.tank, toTarget, patrolThrottle, patrolThrottle, TankControl.DriveRestriction.ForwardOnly))
		{
			SetState(StateType.AIState_LookAround);
		}
	}

	private void AIProcessorRecoverCapsize()
	{
		if (TankIsRecovered())
		{
			if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeSumo>())
			{
				SetState(StateType.AIState_SumoCharge);
			}
			else
			{
				SetState(StateType.AIState_LookAround);
			}
		}
	}

	private void AIProcessorLookAround()
	{
		if ((bool)hostileTarget || (bool)friendlyTarget)
		{
			SetState(StateType.AIState_FaceTarget);
			return;
		}
		switch (lookAroundState)
		{
		case 0:
			if (LookAroundPause())
			{
				lookAroundState++;
			}
			break;
		case 1:
			LookAroundSetupTurn(-1f);
			lookAroundState++;
			break;
		case 2:
			base.block.tank.control.Movement.FaceDirection(base.block.tank, lookAroundTargetDirection, lookAroundThrottle);
			if (Vector3.Dot(new Vector3(0f - lookAroundTargetDirection.z, 0f, lookAroundTargetDirection.x), base.block.trans.forward) > 0f)
			{
				lookAroundState++;
			}
			break;
		case 3:
			if (LookAroundPause())
			{
				lookAroundState++;
			}
			break;
		case 4:
			LookAroundSetupTurn(1f);
			lookAroundState++;
			break;
		case 5:
			base.block.tank.control.Movement.FaceDirection(base.block.tank, lookAroundTargetDirection, lookAroundThrottle);
			if (Vector3.Dot(new Vector3(0f - lookAroundTargetDirection.z, 0f, lookAroundTargetDirection.x), base.block.trans.forward) < 0f)
			{
				lookAroundState++;
			}
			break;
		case 6:
			if (LookAroundPause())
			{
				lookAroundState++;
			}
			break;
		case 7:
			if (!ManSpawn.IsPlayerTeam(base.block.tank.Team))
			{
				float num = UnityEngine.Random.Range(m_PatrolDistMin, m_PatrolDistMax);
				if ((bool)Singleton.playerTank)
				{
					Vector3 vector = (Singleton.playerTank.boundsCentreWorld - tankCentrePosition).SetY(0f).normalized * num;
					waypointPosition = tankCentrePosition + Quaternion.Euler(0f, waypointPlayerAngularBias * 2f * (UnityEngine.Random.value - 0.5f), 0f) * vector;
				}
				else
				{
					waypointPosition = tankCentrePosition + Quaternion.Euler(0f, 360f * (UnityEngine.Random.value - 0.5f), 0f) * Vector3.forward * num;
				}
				SetState(StateType.AIState_Waypoint);
			}
			lookAroundState = 0;
			break;
		}
		debugReportAIDetail = "look state: " + lookAroundState;
	}

	private bool LookAroundPause()
	{
		if (lookAroundWaitDecay < 0f)
		{
			lookAroundWaitDecay = UnityEngine.Random.Range(lookAroundPauseMin, lookAroundPauseMax);
		}
		bool result = lookAroundWaitDecay < Time.deltaTime;
		lookAroundWaitDecay -= Time.deltaTime;
		return result;
	}

	private void LookAroundSetupTurn(float direction)
	{
		float num = UnityEngine.Random.Range(lookAroundAngleMin, lookAroundAngleMax);
		lookAroundTargetDirection = Quaternion.Euler(0f, num * direction, 0f) * base.block.trans.forward;
	}

	private void AIProcessorWaitUntilSeen()
	{
		if (Singleton.Manager<CameraManager>.inst.IsPosInsideCamFrustrum(base.block.tank.trans.position))
		{
			SetState(StateType.AIState_LookAround);
		}
	}

	private void AIProcessorSumoCharge()
	{
		if (base.block.tank.beam.IsActive)
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		if (!hostileTarget)
		{
			return;
		}
		Tank component = hostileTarget.GetComponent<Tank>();
		Vector3 vector = (component ? component.boundsCentreWorld : hostileTarget.position);
		attackTargetRadius = Mathf.Max(component.blockBounds.GetRadiusXZWorld(component.trans), 0.5f);
		zero = vector - base.block.tank.boundsCentreWorld;
		zero = zero.SetY(0f).normalized * 5f;
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeSumo>())
		{
			if (Mode<ModeSumo>.inst.Playing)
			{
				base.block.tank.control.Movement.DriveToDestination(base.block.tank, zero, 1f, 1f, TankControl.DriveRestriction.ForwardOnly);
			}
			else
			{
				sumoBoostOn = false;
			}
		}
		base.block.tank.control.Weapons.AimAtTarget(base.block.tank, vector, attackTargetRadius);
		base.block.tank.control.Weapons.FireWeapons(base.block.tank);
		if (sumoBoostOn)
		{
			if (base.block.tank.Boosters.FuelLevel > 0f)
			{
				base.block.tank.control.Movement.FireBoosters(base.block.tank);
			}
			else
			{
				sumoBoostOn = false;
			}
		}
		else if (base.block.tank.Boosters.FuelLevel == 1f)
		{
			sumoBoostOn = true;
		}
	}

	private void OnAttached()
	{
		base.block.tank.DamageEvent.Subscribe(OnDamaged);
		m_WaitingForPostSpawnEvent = Singleton.Manager<ManSpawn>.inst.IsTechSpawning;
		if (m_WaitingForPostSpawnEvent)
		{
			base.block.tank.PostSpawnEvent.Subscribe(SubscribeForBlockAttachedDetachedEvent);
		}
		else
		{
			SubscribeForBlockAttachedDetachedEvent();
		}
		waypointPosition = base.block.tank.trans.position;
		waypointRotation = base.block.tank.trans.rotation;
		if (Singleton.Manager<DebugUtil>.inst.aiTanksStartInactive)
		{
			SetState(StateType.AIState_WaitUntilSeen);
		}
	}

	private void OnDetaching()
	{
		if (m_WaitingForPostSpawnEvent)
		{
			m_WaitingForPostSpawnEvent = false;
			base.block.tank.PostSpawnEvent.Unsubscribe(SubscribeForBlockAttachedDetachedEvent);
		}
		base.block.tank.DamageEvent.Unsubscribe(OnDamaged);
		ResetDefaults();
		SetState(StateType.None);
	}

	private void SubscribeForBlockAttachedDetachedEvent()
	{
		m_WaitingForPostSpawnEvent = false;
		base.block.tank.PostSpawnEvent.Unsubscribe(SubscribeForBlockAttachedDetachedEvent);
	}

	public void SetThrottle(float throttle)
	{
		m_ThrottleD = throttle;
		m_ThrottleT = throttle;
	}

	public void SetThrottle(float throttleD, float throttleT)
	{
		m_ThrottleD = throttleD;
		m_ThrottleT = throttleT;
	}

	public void SetPatrolDist(float min, float max)
	{
		m_PatrolDistMin = min;
		m_PatrolDistMax = max;
	}

	private void ResetDefaults()
	{
		m_PatrolDistMin = m_DefaultPatrolDistMin;
		m_PatrolDistMax = m_DefaultPatrolDistMax;
		m_ThrottleD = m_DefaultThrottle;
		m_ThrottleT = m_DefaultThrottle;
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		m_LastTankDamage = info;
		if ((bool)info.SourceTank && info.SourceTank.IsEnemy(base.block.tank.Team) && base.block.tank.control.targetType == ObjectTypes.Vehicle)
		{
			hostileTarget = info.SourceTank.transform;
			holdTargetCountdown = holdTargetDuration;
			if (m_CurrentState != StateType.AIState_SumoCharge)
			{
				SetState(StateType.AIState_GetBehindTarget);
			}
		}
	}

	private bool TankIsRecovered()
	{
		return Vector3.Dot(base.block.tank.trans.up, Vector3.up) > Mathf.Cos(0.17453292f);
	}

	private bool TankIsOverturned()
	{
		TankIsRightWayUp = Vector3.Dot(base.block.tank.trans.up, Vector3.up) > Mathf.Cos(Globals.inst.tankOverturnThresholdDegrees * ((float)Math.PI / 180f));
		TankIsMoving = base.block.tank.rbody.velocity.sqrMagnitude > capsizedMinSpeed * capsizedMinSpeed;
		if (TankIsRightWayUp)
		{
			forceUnCapsizeTime = Time.time + forceUnCapsizeTimeout;
		}
		if (!TankIsRightWayUp)
		{
			if (TankIsMoving)
			{
				return Time.time >= forceUnCapsizeTime;
			}
			return true;
		}
		return false;
	}

	public void SetState(StateType state)
	{
		if (state == m_CurrentState)
		{
			return;
		}
		switch (m_CurrentState)
		{
		case StateType.AIState_GetBehindTarget:
			debugReportAIDetail = "";
			break;
		case StateType.AIState_RecoverCapsize:
			base.block.tank.beam.EnableBeam(enable: false);
			break;
		}
		m_CurrentState = state;
		Action aIStateProcessor = null;
		switch (m_CurrentState)
		{
		case StateType.AIState_SumoCharge:
		{
			aIStateProcessor = AIProcessorSumoCharge;
			ModuleScoop[] componentsInChildren = base.block.tank.GetComponentsInChildren<ModuleScoop>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetContinuousMode(continuous: true);
			}
			sumoBoostOn = true;
			break;
		}
		case StateType.AIState_GetBehindTarget:
			aIStateProcessor = AIProcessorGetBehindTarget;
			stopCirclingTimer = 0f;
			break;
		case StateType.AIState_FaceTarget:
			aIStateProcessor = AIProcessorFaceTarget;
			break;
		case StateType.AIState_Waypoint:
			aIStateProcessor = AIProcessorWaypoint;
			break;
		case StateType.AIState_LookAround:
			aIStateProcessor = AIProcessorLookAround;
			break;
		case StateType.AIState_RecoverCapsize:
			base.block.tank.beam.EnableBeam(enable: true);
			aIStateProcessor = AIProcessorRecoverCapsize;
			break;
		case StateType.AIState_WaitUntilSeen:
			aIStateProcessor = AIProcessorWaitUntilSeen;
			break;
		case StateType.AIState_ChargeTarget:
			aIStateProcessor = AIProcessorChargeTarget;
			break;
		default:
			d.LogError($"Unhandled state {state} in ModuleDriveBot.SetState");
			break;
		case StateType.None:
			break;
		}
		AIStateProcessor = aIStateProcessor;
	}

	private bool CheckRecoverTippedOver()
	{
		if (m_CurrentState != StateType.AIState_RecoverCapsize && TankIsOverturned())
		{
			if (Time.time >= recoverTime)
			{
				SetState(StateType.AIState_RecoverCapsize);
				return true;
			}
		}
		else
		{
			recoverTime = Time.time + recoverTimeout;
		}
		return false;
	}

	private bool ReactToTargets()
	{
		if (visionModule == null)
		{
			d.Assert(condition: false, "drive bot with no vision module: slooooow");
			base.block.tank.blockman.IterateBlockComponents<ModuleVision>().FirstOrDefault();
		}
		bool flag = hostileTarget != null;
		if (holdTargetCountdown > 0f)
		{
			holdTargetCountdown -= Time.deltaTime;
		}
		else
		{
			hostileTarget = null;
			friendlyTarget = null;
			float num = float.MaxValue;
			if (base.block.tank.control.targetType != ObjectTypes.Null)
			{
				TechVision vision = base.block.tank.Vision;
				foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
				{
					if (currentTech == base.block.tank)
					{
						continue;
					}
					if (currentTech.IsEnemy(base.block.tank.Team))
					{
						float sqrMagnitude = (currentTech.visible.trans.position - tankCentrePosition).sqrMagnitude;
						if (sqrMagnitude < num && vision.CanSee(currentTech.visible))
						{
							num = sqrMagnitude;
							hostileTarget = currentTech.trans;
							holdTargetCountdown = holdTargetDuration;
						}
					}
					if (currentTech.IsFriendly(base.block.tank.Team) && vision.CanSee(currentTech.visible))
					{
						friendlyTarget = currentTech.trans;
					}
				}
			}
			if ((bool)hostileTarget)
			{
				attackTargetRadius = 0.5f;
				Tank component = hostileTarget.GetComponent<Tank>();
				if ((bool)component)
				{
					attackTargetRadius = Mathf.Max(component.blockBounds.GetRadiusXZWorld(component.trans), attackTargetRadius);
					ModuleShieldGenerator moduleShieldGenerator = component.blockman.IterateBlockComponents<ModuleShieldGenerator>().FirstOrDefault();
					if ((bool)moduleShieldGenerator && moduleShieldGenerator.IsPowered)
					{
						attackTargetRadius = Mathf.Max(moduleShieldGenerator.m_Radius, attackTargetRadius);
					}
				}
			}
		}
		if ((bool)hostileTarget && !hostileTarget.gameObject.activeInHierarchy)
		{
			hostileTarget = null;
		}
		if ((bool)friendlyTarget && !friendlyTarget.gameObject.activeInHierarchy)
		{
			friendlyTarget = null;
		}
		if (!hostileTarget)
		{
			if ((bool)friendlyTarget)
			{
				waypointPosition = friendlyTarget.position;
				waypointRotation = friendlyTarget.rotation;
				Tank component2 = friendlyTarget.GetComponent<Tank>();
				if ((bool)component2)
				{
					waypointPosition = component2.boundsCentreWorld;
				}
			}
			else if (flag)
			{
				if (lostTargetChaseDecay <= 0f)
				{
					lostTargetChaseDecay = lostTargetMemoryTime;
				}
				lostTargetChaseDecay -= Time.deltaTime;
				if (lostTargetChaseDecay <= 0f && m_CurrentState != StateType.AIState_SumoCharge)
				{
					SetState(StateType.AIState_Waypoint);
					return true;
				}
			}
		}
		else
		{
			waypointPosition = hostileTarget.position;
			waypointRotation = hostileTarget.rotation;
			Tank component3 = hostileTarget.GetComponent<Tank>();
			if ((bool)component3)
			{
				waypointPosition = component3.boundsCentreWorld;
				bool flag2 = false;
				if (component3 == Singleton.playerTank)
				{
					if (Mode<ModeMain>.inst != null)
					{
						Mode<ModeMain>.inst.SetPlayerInDanger(inDanger: true, forceAutoSnapshot: true);
					}
					flag2 = true;
				}
				else if (component3.IsFriendly())
				{
					float sqrMagnitude2 = (Singleton.playerPos - waypointPosition).sqrMagnitude;
					float num2 = ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD) ? Globals.inst.m_DangerPlayerRaDRadius : Globals.inst.m_DangerPlayerRadius);
					float num3 = num2 * num2;
					if (sqrMagnitude2 < num3)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.Enemy, base.block.tank, component3);
				}
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					foreach (Tank currentTech2 in Singleton.Manager<ManTechs>.inst.CurrentTechs)
					{
						if (currentTech2 != Singleton.playerTank && currentTech2.IsFriendly() && currentTech2.netTech != null && currentTech2.netTech.NetPlayer != null && component3.IsFriendly())
						{
							float sqrMagnitude3 = (currentTech2.transform.position - waypointPosition).sqrMagnitude;
							float num4 = ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD) ? Globals.inst.m_DangerPlayerRaDRadius : Globals.inst.m_DangerPlayerRadius);
							float num5 = num4 * num4;
							if (sqrMagnitude3 < num5)
							{
								Singleton.Manager<ManMusic>.inst.SetDangerClient(ManMusic.DangerContext.Circumstance.Enemy, base.block.tank, currentTech2);
							}
						}
					}
				}
			}
		}
		return false;
	}

	public bool Control()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return true;
		}
		if (base.block.damage.AboutToDie)
		{
			return true;
		}
		if (CheckRecoverTippedOver())
		{
			return true;
		}
		if (ReactToTargets())
		{
			return true;
		}
		if (m_CurrentState == StateType.None)
		{
			SetState(StateType.AIState_LookAround);
		}
		AIStateProcessor();
		if (Singleton.Manager<DebugUtil>.inst.debugReportAIState)
		{
			debugReportAIState = AIStateProcessor.Method.Name;
		}
		return true;
	}

	private void OnPool()
	{
		visionModule = GetComponent<ModuleVision>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		ResetDefaults();
		hostileTarget = null;
		friendlyTarget = null;
	}

	private void OnRecycle()
	{
		d.AssertFormat(AIStateProcessor == null, "ModuleDriveBot AIStateProcessor was not null, instead set to '{0}'! It should have been cleared in OnDetach ??", AIStateProcessor);
		AIStateProcessor = null;
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle() && (bool)base.block)
		{
			if ((bool)base.block.tank && (m_CurrentState == StateType.AIState_GetBehindTarget || m_CurrentState == StateType.AIState_FaceTarget))
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(tankCentrePosition, waypointPosition);
				Gizmos.DrawWireSphere(waypointPosition, targetIdealRange);
				DebugUtil.GizmosDrawArrow(tankCentrePosition, tankCentrePosition + driveVector, 0.5f);
				Gizmos.DrawWireSphere(tankCentrePosition + driveVector, base.block.tank.control.Movement.m_TargetReachedTolerance);
			}
			if (m_CurrentState == StateType.AIState_Waypoint || (m_CurrentState == StateType.AIState_GetBehindTarget && hostileTarget == null))
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireSphere(waypointPosition, base.block.tank.control.Movement.m_TargetReachedTolerance);
				Gizmos.DrawRay(waypointPosition, waypointRotation * Vector3.forward * 3f);
			}
			if ((bool)base.block.tank && m_CurrentState == StateType.AIState_LookAround && lookAroundState >= 2 && lookAroundState <= 6)
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawRay(tankCentrePosition, lookAroundTargetDirection * 10f);
			}
			if (m_CurrentState == StateType.AIState_SumoCharge && (bool)hostileTarget)
			{
				Tank component = hostileTarget.GetComponent<Tank>();
				Gizmos.color = Color.red;
				Gizmos.DrawLine(component ? component.boundsCentreWorld : hostileTarget.position, base.block.tank.boundsCentreWorld);
			}
		}
	}
}
