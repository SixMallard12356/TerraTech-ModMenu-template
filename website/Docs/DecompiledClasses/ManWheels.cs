#define UNITY_EDITOR
using System;
using System.Threading;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ManWheels : Singleton.Manager<ManWheels>
{
	[Serializable]
	public struct TorqueParams
	{
		public float torqueCurveMaxTorque;

		public float torqueCurveMaxRpm;

		public AnimationCurve torqueCurveDrive;

		public float passiveBrakeMaxTorque;

		public float reverseBrakeMaxRpm;

		public float basicFrictionTorque;

		public float fullCompressFrictionTorque;
	}

	[Serializable]
	public struct WheelParams
	{
		public global::TireProperties tireProperties;

		public float radius;

		public float thicknessAngular;

		public float suspensionSpring;

		public float suspensionDamper;

		[Tooltip("Max suspension force = maxSuspensionAcceleration * tech mass. (0 = no limit)")]
		public float maxSuspensionAcceleration;

		public bool suspensionQuadratic;

		public float suspensionTravel;

		public float steerAngleMax;

		public float steerSpeed;

		public float strafeSteeringSpeed;
	}

	[Serializable]
	public struct ModuleParams
	{
		public float driveTurnPower;

		public float driveTurnBrake;

		public float driveTurnDifferential;

		public float turnOnSpotPower;
	}

	[Serializable]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class TireProperties
	{
		public AnimationCurve frictionLong;

		public AnimationCurve frictionLat;

		[Range(0f, 1f)]
		public float frictionScaleLong = 1f;

		[Range(0f, 1f)]
		public float frictionScaleLat = 1f;

		public float gripFactorLong = 1f;

		public float gripFactorLat = 1f;

		public float LongitudinalGrip(float slip)
		{
			float num = frictionLong.Evaluate(slip);
			if (num > 1f)
			{
				d.Assert(num - 1f < 0.01f, "grip curve exceeds 1");
				num = 1f;
			}
			return num * frictionScaleLong * gripFactorLong;
		}

		public float LateralGrip(float slipAngle)
		{
			float num = frictionLat.Evaluate(slipAngle);
			if (num > 1f)
			{
				d.Assert(num - 1f < 0.01f, "grip curve exceeds 1");
				num = 1f;
			}
			return num * frictionScaleLat * gripFactorLat;
		}
	}

	private enum NetworkedPlayerState
	{
		Unknown,
		Local,
		Remote
	}

	private enum SimulationState
	{
		None,
		Prepared,
		Simulated,
		Flushed
	}

	private struct AttachedWheelState
	{
		public Wheel wheelInterface;

		public bool enabled;

		public bool m_Animated;

		public SimulationState simulationState;

		public Tank tech;

		public Transform trans;

		public Rigidbody rbody;

		public Transform wheelGeometry;

		public Transform tireFrame;

		public SphereCollider suspensionColliderLower;

		public ColliderSwapper colliderSwapper;

		private TorqueParams torqueParams;

		private WheelParams wheelParams;

		private ModuleParams moduleParams;

		private float inertia;

		public float driveTorque;

		public float frictionTorque;

		public float brake;

		public float steering;

		public float strafing;

		private const float drivetrainInertia = 0f;

		private Matrix4x4 tireFrameMatrix;

		private Vector3 wheelPosition;

		public Vector3 wheelVelocity;

		public Vector3 roadForce;

		public Vector3 suspensionUp;

		public Vector3 suspensionForce;

		public float suspensionNormalForce;

		public float angularVelocity;

		public float compression;

		public float slipRatio;

		public float angularSlip;

		public float tireRotation;

		private bool closeToCamera;

		private NetworkedPlayerState networkedState;

		public bool m_ControlBoosting;

		public Vector3 m_ControlRotation;

		public Vector3 m_ControlMovement;

		public Vector3 m_ControlThrottle;

		public float m_Clogged_FrictionTorque_Additional_Scaled;

		public float m_Clogged_FrictionTorque_Additional_Flat;

		public float m_Clogged_Drive_CounterForce_Scaled;

		public float m_Clogged_Drive_Counterforce_Flat;

		public float dotAxisUp;

		public float dotAxisRight;

		public float dotCOM;

		public float dotFwdCOM;

		public Vector3 m_LocalRotationContributionFromDrive;

		public Vector3 m_LocalMovementContributionFromDrive;

		public Vector3 m_LocalMovementContributionFromStrafe;

		public Vector3 m_LocalSteeringContribution;

		public float m_WheelForwardsVelocity;

		public float m_steeringBias;

		private float wheelThicknessDelta;

		private Vector3 tireCachedTransRight;

		public CachedRaycast rayCache;

		private float rayCacheDistance;

		private float currentTime;

		private float deltaTime;

		private float rbodyInvMass;

		private Vector3 techTransForward;

		private Vector3 techTransUp;

		private Vector3 groundRayDirection;

		private const float k_av2rpm = 30f / (float)Math.PI;

		private const float k_WheelSuspensionColliderRadiusRelative = 0.5f;

		private const float k_WheelSuspensionColliderEpsilonPct = 0.015f;

		private const float k_SlipSpeedDampingMax = 1f;

		private const float k_SlipSpeedDampingMaxSqr = 1f;

		private const float k_SlipSpeedDampingMaxInvSqr = 1f;

		private const float k_SlipAngleMaxSpeed = 2f;

		private const float k_SlipAngleMaxSpeedSqr = 4f;

		private const float k_SlipAngleMaxSpeedInvSqr = 0.25f;

		private const float k_FrictionConstant = 0.01f;

		private const float k_FrictionSpeedThreshold = 4f;

		private const float k_FrictionSpeedThresholdInv = 0.25f;

		private const float k_FrictionSpeedThresholdSqr = 16f;

		private const float k_UngroundedSuspensionDamping = 0.5f;

		private static readonly bool k_CacheRaycastPoint = false;

		private static Matrix4x4 s_SteerRotMat = Matrix4x4.identity;

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void Init(Wheel wheel, ModuleWheels.AttachData moduleData)
		{
			wheelInterface = wheel;
			trans = wheel.transform;
			wheelGeometry = wheel.wheelGeometry;
			tireFrame = wheel.tireFrame;
			colliderSwapper = wheel.colliderSwapper;
			suspensionColliderLower = wheel.suspensionColliders[1];
			tech = moduleData.tech;
			torqueParams = moduleData.torqueParams;
			torqueParams.torqueCurveMaxTorque /= moduleData.numWheels;
			wheelParams = moduleData.wheelParams;
			wheelThicknessDelta = 2f * Mathf.Sin(wheelParams.thicknessAngular * 0.5f * ((float)Math.PI / 180f));
			moduleParams = new ModuleParams
			{
				driveTurnPower = moduleData.driveTurnPower,
				driveTurnBrake = moduleData.driveTurnBrake,
				driveTurnDifferential = moduleData.driveTurnDifferential,
				turnOnSpotPower = moduleData.turnOnSpotPower
			};
			inertia = moduleData.inertia;
			rbody = tech.rbody;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void ResetState()
		{
			compression = 0f;
			angularVelocity = 0f;
			suspensionUp = Vector3.up;
			m_ControlBoosting = false;
			m_ControlRotation = Vector3.zero;
			m_ControlMovement = Vector3.zero;
			m_ControlThrottle = Vector3.zero;
			m_Clogged_FrictionTorque_Additional_Scaled = 0f;
			m_Clogged_FrictionTorque_Additional_Flat = 0f;
			m_Clogged_Drive_CounterForce_Scaled = 0f;
			m_Clogged_Drive_Counterforce_Flat = 0f;
			rayCache.Clear();
			networkedState = NetworkedPlayerState.Unknown;
			simulationState = SimulationState.None;
		}

		public void ResetNetworkedState()
		{
			networkedState = NetworkedPlayerState.Unknown;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void RecalculateDotProducts()
		{
			Vector3 rhs = trans.position - tech.trans.position - rbody.rotation * rbody.centerOfMass;
			dotCOM = Vector3.Dot(trans.right, rhs);
			dotFwdCOM = Vector3.Dot(tech.rootBlockTrans.forward, rhs);
			dotAxisRight = Vector3.Dot(trans.right, tech.rootBlockTrans.right);
			dotAxisUp = Vector3.Dot(trans.right, tech.rootBlockTrans.up);
			TankControl.GetInputEffect(tech, trans.position, trans.forward, out m_LocalRotationContributionFromDrive, out m_LocalMovementContributionFromDrive);
			if (wheelParams.strafeSteeringSpeed > 0f)
			{
				TankControl.GetInputEffect(tech, trans.position, trans.right, out var _, out m_LocalMovementContributionFromStrafe);
			}
			else
			{
				m_LocalMovementContributionFromStrafe = Vector3.zero;
			}
			TankControl.GetInputEffect(tech, trans.position, trans.right, out m_LocalSteeringContribution, out var _, TankControl.ControlContribution.Rotation);
			if (m_LocalSteeringContribution.y != 0f && Mathf.Abs(dotFwdCOM) > 0.1f)
			{
				m_steeringBias = ((m_LocalSteeringContribution.y * dotFwdCOM <= 0f) ? 0.1f : (-0.1f));
			}
			else
			{
				m_steeringBias = 0f;
			}
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void MainThread_PreUpdate()
		{
			wheelPosition = rbody.position + (trans.position - rbody.transform.position);
			wheelVelocity = rbody.GetPointVelocity(wheelPosition);
			currentTime = Time.time;
			deltaTime = Time.deltaTime;
			rbodyInvMass = 1f / rbody.mass;
			techTransForward = tech.rootBlockTrans.forward;
			techTransUp = tech.rootBlockTrans.up;
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				closeToCamera = (Singleton.cameraTrans.position - wheelPosition).sqrMagnitude < Globals.inst.m_WheelFarDist * Globals.inst.m_WheelFarDist;
			}
			else
			{
				closeToCamera = true;
			}
			tireCachedTransRight = trans.right;
			groundRayDirection = -(Vector3)tireFrameMatrix.GetColumn(1);
			if (m_Animated)
			{
				rayCache.Clear();
			}
			else
			{
				rayCache.CastIfNeeded(wheelPosition, groundRayDirection, wheelParams.suspensionTravel + wheelParams.radius, Singleton.Manager<ManWheels>.inst.k_GroundLayerMask, closeToCamera, compression != 0f, tech);
			}
			if (k_CacheRaycastPoint)
			{
				rayCacheDistance = (wheelPosition - rayCache.point).magnitude;
			}
			Vector3 normalized = Vector3.Cross(groundRayDirection, trans.right).normalized;
			m_WheelForwardsVelocity = Vector3.Dot(normalized, wheelVelocity);
			TankControl.GetInputEffect(tech, trans.position, normalized, out m_LocalRotationContributionFromDrive, out m_LocalMovementContributionFromDrive);
			Vector3 vector = tech.rootBlockTrans.InverseTransformDirection(groundRayDirection);
			if (Mathf.Abs(vector.x) < Mathf.Abs(vector.y))
			{
				m_ControlRotation.x = 0f;
			}
			else
			{
				m_ControlRotation.y = 0f;
			}
			if (Mathf.Abs(vector.x) < Mathf.Abs(vector.z))
			{
				m_ControlRotation.x = 0f;
			}
			else
			{
				m_ControlRotation.z = 0f;
			}
			if (Mathf.Abs(vector.y) < Mathf.Abs(vector.z))
			{
				m_ControlRotation.y = 0f;
			}
			else
			{
				m_ControlRotation.z = 0f;
			}
			if (networkedState != NetworkedPlayerState.Unknown)
			{
				return;
			}
			networkedState = NetworkedPlayerState.Local;
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return;
			}
			TTNetworkTransform componentInParent = rbody.gameObject.GetComponentInParent<TTNetworkTransform>();
			if (componentInParent != null)
			{
				NetTech component = componentInParent.gameObject.GetComponent<NetTech>();
				if ((bool)component && !((component.NetPlayer != null) ? component.NetPlayer.IsActuallyLocalPlayer : Singleton.Manager<ManNetwork>.inst.IsServer))
				{
					networkedState = NetworkedPlayerState.Remote;
				}
			}
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void WheelThread_Update()
		{
			if (s_UseControlSchemes)
			{
				ControlSchemes_UpdateSteeringAndTorques();
			}
			else
			{
				Old_UpdateSteeringAndTorques();
			}
			ReorientTire();
			if (rayCache.hasHit)
			{
				float num = (k_CacheRaycastPoint ? rayCacheDistance : rayCache.distance);
				float lastCompression = compression;
				compression = Mathf.Clamp01(1f - (num - wheelParams.radius) / wheelParams.suspensionTravel);
				if (networkedState == NetworkedPlayerState.Local)
				{
					suspensionForce = SuspensionForce(lastCompression);
				}
				else
				{
					suspensionForce = Vector3.zero;
				}
				float friction = rayCache.GetFriction();
				Vector3 vector = GripForceLocal(friction);
				if (networkedState == NetworkedPlayerState.Local)
				{
					float sqrMagnitude = wheelVelocity.sqrMagnitude;
					if (sqrMagnitude < 16f)
					{
						float num2 = 1f - Mathf.Sqrt(sqrMagnitude) * 0.25f;
						float num3 = ClampGripCorrection(suspensionForce.y * tireFrameMatrix.m10, wheelParams.tireProperties.props.gripFactorLat);
						float num4 = ClampGripCorrection(suspensionForce.y * tireFrameMatrix.m12, wheelParams.tireProperties.props.gripFactorLong);
						vector.x += num3 * num2;
						vector.z += num4 * num2;
					}
					Vector3 vector2 = tireFrameMatrix * vector;
					roadForce = vector2 * (1f - Globals.inst.m_WheelRoadForceDamping) + roadForce * Globals.inst.m_WheelRoadForceDamping;
				}
			}
			else
			{
				compression = 0f;
				suspensionForce = Vector3.zero;
				roadForce = Vector3.zero;
				wheelVelocity = Vector3.zero;
				slipRatio = 0f;
				angularSlip = 0f;
				float num5 = deltaTime / (inertia + 0f);
				float num6 = GetTotalFrictionTorque() * num5;
				angularVelocity += driveTorque * num5;
				if (Mathf.Abs(angularVelocity) > num6)
				{
					angularVelocity -= num6 * Mathf.Sign(angularVelocity);
				}
				else
				{
					angularVelocity = 0f;
				}
			}
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void MainThread_PostUpdate()
		{
			if (rayCache.hasHit)
			{
				tech.grounded = true;
				tech.wheelGrounded = true;
				Vector3 vector = roadForce + suspensionForce;
				if (networkedState == NetworkedPlayerState.Local && vector != Vector3.zero)
				{
					rbody.AddForceAtPosition(vector, wheelPosition);
				}
				float num = wheelParams.radius * 0.5f;
				float num2 = rayCache.distance - wheelParams.radius * 0.985f + num;
				suspensionColliderLower.center = Vector3.down * num2;
				bool flag = colliderSwapper == null || colliderSwapper.CollisionEnabled;
				if (suspensionColliderLower.enabled != flag)
				{
					suspensionColliderLower.enabled = flag;
				}
			}
			else if (suspensionColliderLower.enabled)
			{
				suspensionColliderLower.enabled = false;
			}
			if (wheelParams.strafeSteeringSpeed > 0f)
			{
				float f = -90f * strafing * ((float)Math.PI / 180f);
				float num3 = Mathf.Sin(f);
				float m = (s_SteerRotMat.m00 = Mathf.Cos(f));
				s_SteerRotMat.m02 = num3;
				s_SteerRotMat.m20 = 0f - num3;
				s_SteerRotMat.m22 = m;
				tireFrame.SetRotationIfChanged((tireFrameMatrix * s_SteerRotMat).rotation);
			}
			else
			{
				tireFrame.SetRotationIfChanged(tireFrameMatrix.rotation);
			}
			tireRotation += angularVelocity * Time.deltaTime;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private float GetTotalFrictionTorque()
		{
			float num = (tech.PassiveBrakesEnabled ? torqueParams.passiveBrakeMaxTorque : 0f);
			float num2 = num * brake;
			float num3 = m_Clogged_FrictionTorque_Additional_Scaled * num + m_Clogged_FrictionTorque_Additional_Flat;
			float num4 = ((compression == 1f) ? torqueParams.fullCompressFrictionTorque : 0f);
			return num2 + torqueParams.basicFrictionTorque + frictionTorque + num3 + num4;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		private void ControlSchemes_UpdateSteeringAndTorques()
		{
			float num = Vector3.Dot(m_ControlMovement, m_LocalMovementContributionFromDrive);
			float num2 = Vector3.Dot(m_ControlMovement, m_LocalMovementContributionFromStrafe);
			float num3 = Vector3.Dot(m_ControlRotation, m_LocalRotationContributionFromDrive);
			float num4 = Vector3.Dot(m_ControlThrottle, m_LocalMovementContributionFromDrive);
			float a = 0f;
			float num5 = Mathf.Abs(angularVelocity * (30f / (float)Math.PI));
			float num6 = num5 / torqueParams.torqueCurveMaxRpm;
			float num7 = torqueParams.torqueCurveDrive.Evaluate(num6) * torqueParams.torqueCurveMaxTorque;
			bool flag = false;
			float num8 = ((m_LocalMovementContributionFromDrive.z != 0f) ? m_LocalMovementContributionFromDrive.z : (0f - m_LocalMovementContributionFromDrive.x));
			float num9 = ((m_LocalMovementContributionFromStrafe.x != 0f) ? m_LocalMovementContributionFromStrafe.x : m_LocalMovementContributionFromStrafe.z);
			if (num4 != 0f)
			{
				num += num4 * num4 * Mathf.Sign(num4);
				num4 = 0f;
			}
			if (num != 0f)
			{
				num2 *= 0.5f;
			}
			else if (num2 != 0f && Mathf.Abs(strafing) > 0.9f)
			{
				num = num8;
				flag = true;
			}
			if (num3 != 0f)
			{
				if (num * num3 < 0f && angularVelocity * num3 < 0f)
				{
					a = Mathf.Clamp01(num6 * 10f) * moduleParams.driveTurnBrake * Mathf.Abs(num3);
				}
				num3 *= Mathf.Lerp(moduleParams.turnOnSpotPower, moduleParams.driveTurnDifferential, Mathf.Abs(num));
			}
			float num10 = Mathf.Clamp(num + num3, -1f, 1f);
			if (m_ControlBoosting && Mathf.Abs(num10) + Mathf.Abs(num3) < 0.1f)
			{
				a = 0f;
			}
			else if (num10 * angularVelocity < 0f || num10 == 0f)
			{
				float b = Mathf.Clamp(num5 / torqueParams.reverseBrakeMaxRpm, 0f, 1f);
				b = Mathf.Lerp(1f, b, num10 * num10 * 2f);
				a = Mathf.Max(a, b);
			}
			float value = num7 * m_Clogged_FrictionTorque_Additional_Scaled + m_Clogged_FrictionTorque_Additional_Flat;
			float num11 = num10 * num7;
			value = Mathf.Clamp(value, -1f * Mathf.Abs(num11), Mathf.Abs(num11));
			num11 += Mathf.Sign(num11) * -1f * value;
			SetTorques(num11, 0f, a);
			float num12 = num2 * num9;
			if (num * num8 < 0f)
			{
				num12 = 0f - num12;
			}
			StrafeTowards(num12, wheelParams.strafeSteeringSpeed * deltaTime);
			if (wheelParams.steerAngleMax > 0f)
			{
				float s;
				if (flag)
				{
					s = (0f - Mathf.Sign(num2)) * m_ControlRotation.y;
				}
				else
				{
					float num13 = Vector3.Dot(m_ControlRotation, m_LocalSteeringContribution);
					s = ((m_WheelForwardsVelocity * 1f + num10 + m_steeringBias < 0f) ? (0f - num13) : num13);
				}
				SteerTowards(s, wheelParams.steerSpeed * deltaTime);
			}
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private void Old_UpdateSteeringAndTorques()
		{
			float z = m_ControlMovement.z;
			float y = m_ControlRotation.y;
			Vector3 lhs = techTransForward;
			if (Mathf.Abs(Vector3.Dot(lhs, rayCache.normal)) > 0.5f)
			{
				lhs = -techTransUp;
			}
			Vector3 normalized = Vector3.Cross(lhs, groundRayDirection).normalized;
			float num = Vector3.Dot(tireCachedTransRight, normalized);
			bool flag = num > 0f;
			bool flag2 = Mathf.Abs(num) > 0.1f;
			bool flag3 = !(dotCOM > 0.1f);
			bool num2 = flag3 && dotCOM > -0.1f;
			bool flag4 = dotFwdCOM > 0f;
			float num3 = ((!flag2 || compression == 0f) ? 0f : (flag ? z : (0f - z)));
			float num4 = ((num2 || compression == 0f) ? 0f : (flag3 ? (0f - y) : y));
			bool flag5 = true;
			float num5 = ((z < -0.01f && flag5) ? (0f - y) : y);
			float num6 = ((z < -0.01f && !flag5) ? (0f - num4) : num4);
			float num7 = num5 * (float)((!flag4) ? 1 : (-1));
			SteerTowards(num7, wheelParams.steerSpeed * deltaTime);
			bool flag6 = num7 == 0f || Mathf.Abs(z) > Mathf.Abs(num7);
			float num8 = (flag6 ? z : num7);
			float num9 = 1f;
			if (Mathf.Abs(num8) > Mathf.Epsilon)
			{
				float num10 = Mathf.Abs((flag6 ? num7 : z) / num8);
				num9 = Mathf.Clamp01(num10);
				d.AssertFormat(num9 == num10, "Missmatch between quotient before and after clamping to 0,1 range ({0}/{1}. End result should always be [0, 1]!", num10, num9);
			}
			float num11 = Mathf.Lerp(num3, num3 + num6, moduleParams.driveTurnDifferential);
			float num12 = 1f - Mathf.Abs(num11 * 0.5f);
			float num13 = Mathf.Lerp((flag6 ? num3 : num6) * moduleParams.turnOnSpotPower, num11 * moduleParams.driveTurnPower, num9);
			float num14 = angularVelocity * (30f / (float)Math.PI);
			float a = 0f;
			if ((flag6 ? num3 : num6) * angularVelocity < 0f)
			{
				a = Mathf.Clamp(num14 / torqueParams.reverseBrakeMaxRpm, 0f, 1f);
			}
			float b = ((tech.Boosters.BoostersFiring && Mathf.Abs(z) + Mathf.Abs(num7) < 0.2f) ? 0f : Mathf.Lerp(a, num12 * moduleParams.driveTurnBrake, num9));
			b = ((!(flag6 ? (Mathf.Abs(num3) > 0.01f) : (Mathf.Abs(num6) > 0.01f))) ? Mathf.Lerp(1f, b, num3 * num3 + num6 * num6) : 0f);
			float num15 = torqueParams.torqueCurveDrive.Evaluate(Mathf.Abs(num14) / torqueParams.torqueCurveMaxRpm) * torqueParams.torqueCurveMaxTorque;
			SetTorques(num13 * num15, 0f, b);
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private void ReorientTire()
		{
			rayCache.DecayUngroundedNormal(Vector3.up, currentTime);
			Vector3 vector = Vector3.Cross(tireCachedTransRight, rayCache.normal);
			if (vector.sqrMagnitude < 0.01f)
			{
				vector = techTransForward;
			}
			Vector3 vector2 = Vector3.Cross(vector, tireCachedTransRight).normalized;
			if (!rayCache.hasHit)
			{
				vector2 = vector2 * 0.5f + suspensionUp * 0.5f;
			}
			suspensionUp = vector2;
			Vector3 up = Vector3.MoveTowards(suspensionUp, rayCache.normal, wheelThicknessDelta);
			float f = wheelParams.steerAngleMax * steering * ((float)Math.PI / 180f) + 90f * strafing * ((float)Math.PI / 180f);
			float num = Mathf.Sin(f);
			float m = (s_SteerRotMat.m00 = Mathf.Cos(f));
			s_SteerRotMat.m02 = num;
			s_SteerRotMat.m20 = 0f - num;
			s_SteerRotMat.m22 = m;
			tireFrameMatrix = Matrix4x4.LookAt(Vector3.zero, vector, up) * s_SteerRotMat;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private Vector3 SuspensionForce(float lastCompression)
		{
			float num = (lastCompression - compression) * wheelParams.suspensionTravel / deltaTime * wheelParams.suspensionDamper;
			suspensionNormalForce = compression * wheelParams.suspensionSpring;
			if (UsingLargeFixedTimeDelta)
			{
				num *= 0.85f;
			}
			if (wheelParams.suspensionQuadratic)
			{
				suspensionNormalForce *= compression;
			}
			if (wheelParams.maxSuspensionAcceleration > 0f && suspensionNormalForce * rbodyInvMass > wheelParams.maxSuspensionAcceleration)
			{
				float num2 = wheelParams.maxSuspensionAcceleration / (suspensionNormalForce * rbodyInvMass);
				suspensionNormalForce *= num2;
				num *= num2;
			}
			return Mathf.Max(suspensionNormalForce - num, 0f) * suspensionUp;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private Vector3 GripForceLocal(float contactFriction)
		{
			int a = (int)((100f - Mathf.Abs(angularVelocity)) * 0.1f);
			a = Mathf.Max(a, 1);
			float num = 1f / (float)a;
			float num2 = 1f / (inertia + 0f);
			float num3 = driveTorque * deltaTime * num * num2;
			float num4 = GetTotalFrictionTorque() * deltaTime * num * num2;
			float num5 = wheelParams.radius * num2;
			float num6 = 1f / deltaTime;
			Vector3 vector = tireFrameMatrix.transpose * wheelVelocity;
			Vector3 zero = Vector3.zero;
			float wheelSlipDampingNormal = Globals.inst.m_WheelSlipDampingNormal;
			float num7 = ((wheelSlipDampingNormal != 0f) ? (wheelSlipDampingNormal / suspensionNormalForce) : 1f);
			wheelSlipDampingNormal = Globals.inst.m_WheelSlipDampingNormalAngular;
			float num8 = ((wheelSlipDampingNormal != 0f) ? (wheelSlipDampingNormal / suspensionNormalForce) : 1f);
			float num9 = num * deltaTime * contactFriction * suspensionNormalForce;
			for (int i = 0; i < a; i++)
			{
				float num10 = Mathf.Abs(vector.z);
				float num11 = ((num10 > 1f) ? (1f / num10) : (num10 * 1f));
				slipRatio = (angularVelocity * wheelParams.radius - vector.z) * num11 * num7;
				float sqrMagnitude = vector.sqrMagnitude;
				float num12 = ((sqrMagnitude > 4f) ? 1f : (sqrMagnitude * 0.25f));
				angularSlip = (0f - Mathf.Atan2(vector.x, num10)) * num12 * num8;
				float magnitude = new Vector2(slipRatio, angularSlip).magnitude;
				Vector3 zero2 = Vector3.zero;
				if (magnitude > Mathf.Epsilon)
				{
					float num13 = angularSlip * wheelParams.tireProperties.props.LateralGrip(magnitude);
					float num14 = slipRatio * wheelParams.tireProperties.props.LongitudinalGrip(magnitude);
					float num15 = num9 / magnitude;
					zero2.x = num15 * num13;
					zero2.z = num15 * num14;
				}
				angularVelocity += num3 - zero2.z * num5;
				if (Mathf.Abs(angularVelocity) > num4)
				{
					angularVelocity -= num4 * Mathf.Sign(angularVelocity);
				}
				else
				{
					angularVelocity = 0f;
				}
				vector += zero2 * rbodyInvMass;
				zero += zero2 * num6;
			}
			return zero;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		private float ClampGripCorrection(float correctionStrength, float gripFactor)
		{
			float num = Globals.inst.m_WheelFrictionCorrectScaleMax * Globals.inst.m_WheelFrictionCorrectGripScale.Evaluate(gripFactor) * suspensionNormalForce;
			return Mathf.Clamp(correctionStrength * Globals.inst.m_WheelFrictionCorrectFactorFull, 0f - num, num);
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void SyncWheelGeometry()
		{
			if (!m_Animated && wheelGeometry != null && (bool)rbody)
			{
				Vector3 newWorldPos = trans.position - suspensionUp * ((1f - compression) * wheelParams.suspensionTravel);
				Quaternion newWorldRot = Quaternion.LookRotation(tireFrame.forward, suspensionUp) * Quaternion.Euler(57.29578f * tireRotation, 0f, 0f);
				wheelGeometry.SetPositionAndRotationIfChanged(newWorldPos, newWorldRot, "WheelSimulator.Update position and rotation");
			}
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public Vector3 GetGroundContactPoint()
		{
			return trans.position - tireFrame.up * (wheelParams.radius + compression * wheelParams.suspensionTravel);
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void SetControlInput(TankControl.ControlState drive)
		{
			m_ControlMovement = drive.InputMovement;
			m_ControlThrottle = drive.Throttle;
			m_ControlRotation = drive.InputRotation;
			m_ControlBoosting = drive.BoostJets || drive.BoostProps;
		}

		public void SetWheelClogged(float driveCountertorqueFlat, float driveCountertorqueScaled, float frictionTorqueFlat, float frictionTorqueScaled)
		{
			m_Clogged_Drive_Counterforce_Flat = driveCountertorqueFlat;
			m_Clogged_Drive_CounterForce_Scaled = driveCountertorqueScaled;
			m_Clogged_FrictionTorque_Additional_Flat = frictionTorqueFlat;
			m_Clogged_FrictionTorque_Additional_Scaled = frictionTorqueScaled;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void SetTorques(float drive, float friction, float brake)
		{
			driveTorque = drive;
			frictionTorque = friction;
			this.brake = brake;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void SteerTowards(float s, float maxDelta)
		{
			steering = Mathf.MoveTowards(steering, s, maxDelta);
		}

		public void StrafeTowards(float s, float maxDelta)
		{
			strafing = Mathf.MoveTowards(strafing, s, maxDelta);
		}

		public void DrawGizmos()
		{
			d.Assert(Application.isPlaying);
			Vector3 vector = -tireFrame.up;
			Vector3 vector2 = trans.position + vector * (1f - compression) * wheelParams.suspensionTravel;
			Gizmos.color = ((compression == 1f) ? Color.red : ((compression != 0f) ? Color.green : Color.cyan));
			float[] obj = new float[3] { -1f, 0f, 1f };
			_ = Quaternion.Euler(0f, wheelParams.steerAngleMax * steering, 0f) * trans.right;
			float[] array = obj;
			foreach (float num in array)
			{
				Mathf.Cos(wheelParams.thicknessAngular * num * ((float)Math.PI / 180f));
				Mathf.Sin(wheelParams.thicknessAngular * num * ((float)Math.PI / 180f));
			}
			if ((bool)rbody && angularVelocity == 0f && rbody.velocity.IsZeroEpsilon(0.2f))
			{
				Gizmos.color = Gizmos.color.ScaleRGB(0.5f);
			}
			DebugUtil.GizmosDrawArrow(trans.position, trans.position + vector * (wheelParams.radius + wheelParams.suspensionTravel), wheelParams.suspensionTravel);
			Gizmos.color = Color.red;
			DebugUtil.GizmosDrawArrow(trans.position, trans.position + roadForce * Globals.inst.m_WheelGizmoForceScale);
			Gizmos.color = Color.blue;
			DebugUtil.GizmosDrawArrow(trans.position, trans.position + suspensionForce * Globals.inst.m_WheelGizmoForceScale);
			Gizmos.color = Color.magenta;
			DebugUtil.GizmosDrawArrow(trans.position, trans.position + wheelVelocity);
			Gizmos.color = Color.white;
			DebugUtil.GizmosDrawArrow(vector2, vector2 + suspensionUp);
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class Wheel
	{
		private int attachedID;

		public Transform wheelGeometry;

		public Transform transform;

		public Transform tireFrame;

		public SphereCollider[] suspensionColliders;

		public ColliderSwapper colliderSwapper;

		public ParticleSystem dustParticles;

		public ParticleSystem suspensionSparkParticles;

		public float sparkStartupTimer;

		private static PhysicMaterial frictionless;

		private const float k_WheelSuspensionColliderRadiusRelative = 0.5f;

		private const float k_av2rpm = 30f / (float)Math.PI;

		public float Steering
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].steering;
			}
		}

		public float DriveTorque
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].driveTorque;
			}
		}

		public float BrakeTorque
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].brake;
			}
		}

		public bool Horizontal
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Mathf.Abs(Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].dotAxisUp) > 0.1f;
			}
		}

		public bool DriveAligned
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Mathf.Abs(Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].dotAxisRight) > 0.1f;
			}
		}

		public float RPM
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Mathf.Abs(Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].angularVelocity * (30f / (float)Math.PI));
			}
		}

		public float TireRotation
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].tireRotation;
			}
		}

		public float Compression
		{
			get
			{
				if (attachedID == -1)
				{
					return 0f;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].compression;
			}
		}

		public bool Enabled
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].enabled;
			}
		}

		public bool Grounded
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].compression != 0f;
			}
		}

		public bool FullyCompressed
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].compression == 1f;
			}
		}

		public bool Animated
		{
			get
			{
				if (attachedID == -1)
				{
					return false;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].m_Animated;
			}
		}

		public Vector3 RoadForce
		{
			get
			{
				if (attachedID == -1)
				{
					return Vector3.zero;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].roadForce;
			}
		}

		public Vector3 ContactPoint
		{
			get
			{
				if (attachedID == -1)
				{
					return Vector3.zero;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].rayCache.point;
			}
		}

		public Vector3 ContactNormal
		{
			get
			{
				if (attachedID == -1)
				{
					return Vector3.zero;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].rayCache.normal;
			}
		}

		public Collider ContactCollider
		{
			get
			{
				if (attachedID == -1)
				{
					return null;
				}
				return Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].rayCache.collider;
			}
		}

		public Wheel(Transform wheelGeom, ColliderSwapper blockColliderSwapper, float radius)
		{
			attachedID = -1;
			wheelGeometry = wheelGeom;
			transform = wheelGeom.parent;
			colliderSwapper = blockColliderSwapper;
			tireFrame = new GameObject("_tireFrame").transform;
			tireFrame.parent = transform;
			tireFrame.localPosition = Vector3.zero;
			if (!frictionless)
			{
				frictionless = new PhysicMaterial("_frictionless");
				frictionless.bounciness = 0f;
				frictionless.staticFriction = 0f;
				frictionless.dynamicFriction = 0f;
			}
			float num = radius * 0.5f;
			suspensionColliders = new SphereCollider[2];
			suspensionColliders[0] = tireFrame.gameObject.AddComponent<SphereCollider>();
			suspensionColliders[0].gameObject.layer = Globals.inst.layerWheelSuspension;
			suspensionColliders[0].radius = num;
			suspensionColliders[0].center = Vector3.up * num;
			suspensionColliders[0].sharedMaterial = frictionless;
			suspensionColliders[0].enabled = false;
			GameObject gameObject = new GameObject("_suspensionColliderLower");
			gameObject.transform.parent = tireFrame;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			suspensionColliders[1] = gameObject.AddComponent<SphereCollider>();
			gameObject.layer = Globals.inst.layerWheelSuspension;
			suspensionColliders[1].radius = num;
			suspensionColliders[1].center = -Vector3.up * num;
			suspensionColliders[1].sharedMaterial = frictionless;
			suspensionColliders[1].enabled = false;
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
		}

		public void Attach(ModuleWheels.AttachData moduleData)
		{
			if (Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex == Singleton.Manager<ManWheels>.inst.m_NumAttachedWheels - 1)
			{
				attachedID = Singleton.Manager<ManWheels>.inst.m_NumAttachedWheels;
				if (attachedID < Singleton.Manager<ManWheels>.inst.m_WheelState.Length)
				{
					Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].simulationState = SimulationState.None;
				}
				Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex = attachedID;
			}
			else
			{
				attachedID = -1;
				for (int i = 0; i < Singleton.Manager<ManWheels>.inst.m_NumAttachedWheels; i++)
				{
					if (Singleton.Manager<ManWheels>.inst.m_WheelState[i].wheelInterface == null)
					{
						attachedID = i;
						break;
					}
				}
				d.Assert(attachedID != -1, "failed to find a free wheel state slot in table");
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].simulationState = SimulationState.None;
			}
			Singleton.Manager<ManWheels>.inst.m_NumAttachedWheels++;
			lock (Singleton.Manager<ManWheels>.inst.m_WheelState)
			{
				if (attachedID == Singleton.Manager<ManWheels>.inst.m_WheelState.Length)
				{
					Array.Resize(ref Singleton.Manager<ManWheels>.inst.m_WheelState, Singleton.Manager<ManWheels>.inst.m_WheelState.Length * 2);
					d.LogWarning("wheel state array grew to size " + Singleton.Manager<ManWheels>.inst.m_WheelState.Length);
				}
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].Init(this, moduleData);
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].RecalculateDotProducts();
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].SetTorques(0f, 0f, 0f);
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].SteerTowards(0f, float.MaxValue);
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].StrafeTowards(0f, float.MaxValue);
				Enable(enabled: true);
			}
		}

		public void UpdateAttachData(ModuleWheels.AttachData moduleData)
		{
			d.LogError("ManWheels.Wheel.UpdateAttachData is only for use in Editor");
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].Init(this, moduleData);
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].RecalculateDotProducts();
		}

		public void Detach()
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].enabled = false;
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].simulationState = SimulationState.None;
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].wheelInterface = null;
			suspensionColliders[0].enabled = false;
			suspensionColliders[1].enabled = false;
			Singleton.Manager<ManWheels>.inst.m_NumAttachedWheels--;
			if (attachedID == Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex)
			{
				do
				{
					Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex--;
				}
				while (Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex >= 0 && Singleton.Manager<ManWheels>.inst.m_WheelState[Singleton.Manager<ManWheels>.inst.m_MaxAttachedWheelIndex].wheelInterface == null);
			}
			attachedID = -1;
		}

		public void Enable(bool enabled)
		{
			suspensionColliders[0].enabled = enabled;
			suspensionColliders[1].enabled = enabled;
			if (attachedID != -1)
			{
				if (enabled)
				{
					Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].ResetState();
				}
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].enabled = enabled;
			}
		}

		public void SetAnimated(bool animated)
		{
			if (attachedID != -1)
			{
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].m_Animated = animated;
			}
		}

		public void Reset()
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].ResetState();
		}

		public void ResetNetworkedState()
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].ResetNetworkedState();
		}

		public void RecalculateDotProducts()
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].RecalculateDotProducts();
		}

		public void SetControlInput(TankControl.ControlState drive)
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].SetControlInput(drive);
		}

		public void SetWheelClogged(float driveCountertorqueFlat, float driveCountertorqueScaled, float frictionTorqueFlat, float frictionTorqueScaled)
		{
			if (attachedID != -1)
			{
				Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].SetWheelClogged(driveCountertorqueFlat, driveCountertorqueScaled, frictionTorqueFlat, frictionTorqueScaled);
			}
		}

		public void DrawGizmos()
		{
			Singleton.Manager<ManWheels>.inst.m_WheelState[attachedID].DrawGizmos();
		}
	}

	public struct CachedRaycast
	{
		private bool hasData;

		private RaycastHit hitData;

		private bool rayFoundTarget;

		private Vector3 oldPos;

		private Vector3 oldDir;

		private float oldMaxDist;

		private int oldLayerMask;

		private bool oldGrounded;

		private float contactFriction;

		private float decayTimeStamp;

		private const float k_FrictionToBeGenerated = -1f;

		private const float k_PositionToleranceYScaling = 1f;

		private const float k_UngroundedNormalDecayRate = 0.2f;

		private const float k_UngroundedNormalDecayDelay = 0.5f;

		public bool hasHit => rayFoundTarget;

		public float distance => hitData.distance;

		public Vector3 point => hitData.point;

		public Vector3 normal => hitData.normal;

		public Collider collider => hitData.collider;

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public float GetFriction()
		{
			if (contactFriction == -1f)
			{
				contactFriction = Singleton.Manager<ManWheels>.inst.TerrainFrictionLookup(hitData.point);
			}
			return contactFriction;
		}

		public void Clear()
		{
			hasData = false;
			rayFoundTarget = false;
			hitData = default(RaycastHit);
			hitData.normal = Vector3.up;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void DecayUngroundedNormal(Vector3 defaultNormal, float currentTime)
		{
			if (rayFoundTarget)
			{
				decayTimeStamp = currentTime;
			}
			else if (currentTime - decayTimeStamp > 0.5f)
			{
				hitData.normal = (0.2f * defaultNormal + 0.8f * hitData.normal).normalized;
			}
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void CastIfNeeded(Vector3 position, Vector3 direction, float maxDist, int layerMask, bool closeToCamera, bool grounded, Tank tech)
		{
			float tolerance = (closeToCamera ? Globals.inst.m_WheelPosCloseTol : Globals.inst.m_WheelPosFarTol);
			float tolerance2 = (closeToCamera ? Globals.inst.m_WheelRotCloseTol : Globals.inst.m_WheelRotFarTol);
			if (hasData && !IsVecChanged(position, oldPos, tolerance) && !IsVecChanged(direction, oldDir, tolerance2) && oldMaxDist == maxDist && oldLayerMask == layerMask && oldGrounded && grounded)
			{
				return;
			}
			Vector3 vector = hitData.normal;
			rayFoundTarget = Physics.Raycast(position, direction, out hitData, maxDist, layerMask, QueryTriggerInteraction.Ignore);
			if (rayFoundTarget && hitData.rigidbody == tech.rbody)
			{
				rayFoundTarget = false;
				hitData.normal = vector;
			}
			hasData = true;
			oldPos = position;
			oldDir = direction;
			oldMaxDist = maxDist;
			oldLayerMask = layerMask;
			oldGrounded = grounded;
			contactFriction = 0f;
			if (rayFoundTarget)
			{
				d.Assert(hitData.collider);
				if (hitData.collider.IsTerrain())
				{
					contactFriction = -1f;
				}
				else if ((bool)hitData.collider.sharedMaterial)
				{
					contactFriction = hitData.collider.sharedMaterial.dynamicFriction;
				}
			}
			else
			{
				hitData.normal = vector;
			}
		}
	}

	[SerializeField]
	private bool m_ThreadedSimulation;

	[SerializeField]
	private bool m_SkipSimulation;

	[SerializeField]
	private bool m_UseControlSchemes = true;

	private AttachedWheelState[] m_WheelState = new AttachedWheelState[512];

	private int m_NumAttachedWheels;

	private int m_MaxAttachedWheelIndex = -1;

	private int m_MaxAttachedWheelIndexThisFrame = -1;

	private volatile int m_PreparedWheelIndex = -1;

	private volatile int m_SimulatedWheelIndex = -1;

	private int k_GroundLayerMask;

	private BiomeMap.MapCell m_SingleCellBlendLookupCache;

	private ThreadedJobProcessor m_SimulationThread = new ThreadedJobProcessor();

	private static bool s_UseControlSchemes;

	private EventWaitHandle m_StartWheelSimulationUpdate = new EventWaitHandle(initialState: false, EventResetMode.AutoReset);

	private EventWaitHandle m_ProcessSimulationWaitHandle = new EventWaitHandle(initialState: false, EventResetMode.AutoReset);

	private EventWaitHandle m_ApplySimulationResultWaitHandle = new EventWaitHandle(initialState: false, EventResetMode.AutoReset);

	public static bool UsingLargeFixedTimeDelta { get; set; }

	private static bool IsVecChanged(Vector3 newPos, Vector3 oldPos, float tolerance, float yScale = 1f)
	{
		Vector3 vector = newPos - oldPos;
		return vector.x * vector.x + vector.y * vector.y * yScale + vector.z * vector.z > tolerance * tolerance;
	}

	private float TerrainFrictionLookup(Vector3 scenePos)
	{
		return Singleton.Manager<ManWorld>.inst.GetWeightedFrictionAtScenePosition(scenePos);
	}

	private static bool UpdateSimulationWrapper(ThreadedJobProcessor.TestShouldExitDelegate testShouldExit)
	{
		return Singleton.Manager<ManWheels>.inst.UpdateSimulation(testShouldExit);
	}

	private bool UpdateSimulation(ThreadedJobProcessor.TestShouldExitDelegate testShouldExit)
	{
		while (true)
		{
			m_StartWheelSimulationUpdate.WaitOne();
			if (m_SimulatedWheelIndex != -1)
			{
				d.LogWarning($"[Wheel] UpdateSimulation:m_SimulatedWheelIndex not reset m_SimulatedWheelIndex={m_SimulatedWheelIndex}/{m_MaxAttachedWheelIndexThisFrame}");
			}
			if (testShouldExit(allowPause: false))
			{
				break;
			}
			try
			{
				for (int i = 0; i <= m_MaxAttachedWheelIndexThisFrame; i++)
				{
					while (i > m_PreparedWheelIndex)
					{
						m_ProcessSimulationWaitHandle.WaitOne();
						if (testShouldExit(allowPause: false))
						{
							return false;
						}
					}
					if (m_WheelState[i].enabled && m_WheelState[i].simulationState == SimulationState.Prepared)
					{
						lock (m_WheelState)
						{
							m_WheelState[i].WheelThread_Update();
							m_WheelState[i].simulationState = SimulationState.Simulated;
						}
					}
					m_SimulatedWheelIndex = i;
					m_ApplySimulationResultWaitHandle.Set();
				}
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("exception caught on simulation thread: threaded simulation disabled\n{0}", ex.StackTrace);
				m_ThreadedSimulation = false;
			}
		}
		return false;
	}

	private void OnFirstFixedUpdate()
	{
		s_UseControlSchemes = m_UseControlSchemes;
		m_MaxAttachedWheelIndexThisFrame = m_MaxAttachedWheelIndex;
		if (m_ThreadedSimulation && !m_SkipSimulation && !m_SimulationThread.JobInProgress)
		{
			m_SimulationThread.Start();
		}
		else if ((!m_ThreadedSimulation || m_SkipSimulation) && m_SimulationThread.JobInProgress)
		{
			m_SimulationThread.Stop();
		}
		m_PreparedWheelIndex = -1;
		m_SimulatedWheelIndex = -1;
		if (m_MaxAttachedWheelIndexThisFrame <= 0)
		{
			return;
		}
		m_StartWheelSimulationUpdate.Set();
		for (int i = 0; i <= m_MaxAttachedWheelIndexThisFrame; i++)
		{
			if (m_WheelState[i].enabled)
			{
				m_WheelState[i].MainThread_PreUpdate();
				m_WheelState[i].simulationState = SimulationState.Prepared;
				if (!m_SimulationThread.JobInProgress && !m_SkipSimulation)
				{
					m_WheelState[i].WheelThread_Update();
					m_WheelState[i].simulationState = SimulationState.Simulated;
					m_WheelState[i].MainThread_PostUpdate();
					m_WheelState[i].simulationState = SimulationState.Flushed;
				}
			}
			m_PreparedWheelIndex = i;
			m_ProcessSimulationWaitHandle.Set();
		}
	}

	private void OnLastFixedUpdate()
	{
		if (!m_SimulationThread.JobInProgress || m_MaxAttachedWheelIndexThisFrame <= 0)
		{
			return;
		}
		int i = 0;
		bool flag = false;
		for (; i <= m_MaxAttachedWheelIndexThisFrame; i++)
		{
			if (flag)
			{
				break;
			}
			while (i > m_SimulatedWheelIndex)
			{
				if (!m_ApplySimulationResultWaitHandle.WaitOne(3))
				{
					d.LogWarning($"[Wheel] Post update hit wait lock timeout! Wheel {i} will be skipped! m_SimulatedWheelIndex={m_SimulatedWheelIndex}/{m_MaxAttachedWheelIndexThisFrame}");
				}
			}
			if (m_ThreadedSimulation)
			{
				if (m_WheelState[i].enabled)
				{
					m_WheelState[i].MainThread_PostUpdate();
					m_WheelState[i].simulationState = SimulationState.Flushed;
				}
				continue;
			}
			break;
		}
	}

	private void Start()
	{
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.First, OnFirstFixedUpdate, 100);
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.Last, OnLastFixedUpdate, -100);
		k_GroundLayerMask = Globals.inst.layerTerrain.mask | Globals.inst.layerScenery.mask | Globals.inst.layerLandmark.mask | Globals.inst.layerTank.mask | Globals.inst.layerPickup.mask;
		System.Threading.ThreadPriority priority = System.Threading.ThreadPriority.Highest;
		m_SimulationThread.Initialise("ManWheels", UpdateSimulationWrapper, priority);
	}

	private void OnDestroy()
	{
		m_SimulationThread.RequestExit();
		m_StartWheelSimulationUpdate.Set();
		m_ProcessSimulationWaitHandle.Set();
		m_SimulationThread.Terminate(waitForExit: false);
	}

	private void Update()
	{
		for (int i = 0; i <= m_MaxAttachedWheelIndex; i++)
		{
			if (m_WheelState[i].wheelInterface != null && m_WheelState[i].enabled)
			{
				m_WheelState[i].SyncWheelGeometry();
			}
		}
	}
}
