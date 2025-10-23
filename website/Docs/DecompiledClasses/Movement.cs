#define UNITY_EDITOR
using System;
using UnityEngine;

public class Movement : ScriptableObject
{
	public class DriveValues
	{
		public float m_DriveControl;

		public float m_TurnControl;

		public float m_Throttle = 1f;

		public DriveValues()
		{
		}

		public DriveValues(float driveControl, float turnControl, float throttle)
		{
			m_DriveControl = driveControl;
			m_TurnControl = turnControl;
			m_Throttle = throttle;
		}
	}

	public float m_FullThrottleRange = 5f;

	public float m_FullThrottleAngularRange = 45f;

	public float m_TargetReachedTolerance = 2f;

	public float m_TurnToleranceInner = 30f;

	public float m_TurnToleranceOuter = 70f;

	public float m_TurnToleranceInnerReverse = 20f;

	public float m_TurnToleranceOuterReverse = 45f;

	public AnimationCurve throttleProfile;

	public float m_LookAheadDistance = 10f;

	public bool m_USE_AVOIDANCE;

	public bool m_AVOID_NEARBY;

	public bool m_UseDriveDest = true;

	public float m_ExtraRadius = 1.25f;

	public float m_DriveOnlyTurn = 0.125f;

	public float m_DriveTurn = 0.5f;

	public float m_AvoidancePadding;

	public float m_MinObstacleAvoidAngle;

	private static readonly Bitfield<ObjectTypes> k_SceneryAndTanks = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Scenery,
		ObjectTypes.Vehicle
	});

	public float m_PathInnerAngle = 10f;

	public float m_PathOuterAngle = 45f;

	public float m_PathOuterAngleDriveControl = 0.5f;

	public float m_PathCorrectionDriveControl;

	public float m_OffPathDriveControl = 0.5f;

	public float m_OffPathDriveScale = 0.25f;

	public float m_OffPathTurnAmount = 0.5f;

	public bool DriveToVisible(Tank tank, Visible visible, float throttleVal, TankControl.DriveRestriction restriction)
	{
		float arrivalDist = Mathf.Max(tank.blockBounds.extents.x, tank.blockBounds.extents.z) + 0.5f + visible.Radius;
		Vector3 vecToVisible = GetVecToVisible(tank, visible);
		Vector3 targetPos = tank.boundsCentreWorld + vecToVisible;
		return DriveToPosition(tank, targetPos, throttleVal, restriction, visible, arrivalDist);
	}

	public bool DriveToPosition(Tank tank, Vector3 targetPos, float throttleVal, TankControl.DriveRestriction restrict = TankControl.DriveRestriction.None, Visible targetVisible = null, float arrivalDist = 0f)
	{
		if (!Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(targetPos))
		{
			return false;
		}
		Vector3 vector = (targetPos - tank.boundsCentreWorld).SetY(0f);
		float num = arrivalDist + tank.visible.Radius + m_TargetReachedTolerance;
		if (vector.sqrMagnitude < num * num)
		{
			return true;
		}
		bool avoiding = false;
		bool onlyTurn = false;
		bool centreOverlap = false;
		TankBlock rootBlock = tank.blockman.GetRootBlock();
		float y = rootBlock.trans.position.y;
		Vector3 boundsCentreWorld = tank.boundsCentreWorld;
		float num2 = Mathf.Max(tank.blockBounds.extents.x, tank.blockBounds.extents.z) + 0.5f;
		num2 *= m_ExtraRadius;
		Vector3 zero = Vector3.zero;
		Vector3 forward = rootBlock.trans.forward;
		Vector3 vector2 = (m_UseDriveDest ? (vector.normalized * m_LookAheadDistance) : (forward * m_LookAheadDistance));
		Vector3 vector3 = (boundsCentreWorld + vector2).SetY(y);
		Vector3 vector4 = (boundsCentreWorld + vector2 * 0.5f).SetY(y);
		Vector3 vector5 = Vector3.zero;
		float num3 = float.MaxValue;
		float f = 0f;
		float num4 = 0f;
		Visible visible = (((bool)targetVisible && (bool)targetVisible.block && (bool)targetVisible.block.tank) ? targetVisible.block.tank.visible : null);
		TechVision.VisibleIterator enumerator = tank.Vision.IterateVisibles(k_SceneryAndTanks).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			num4 = (num2 + current.Radius) * (num2 + current.Radius);
			float sqrMagnitude = (current.centrePosition - boundsCentreWorld).sqrMagnitude;
			float sqrMagnitude2 = (current.centrePosition - vector3).sqrMagnitude;
			float sqrMagnitude3 = (current.centrePosition - vector4).sqrMagnitude;
			if ((sqrMagnitude <= num4 || sqrMagnitude2 <= num4 || sqrMagnitude3 <= num4) && (current.centrePosition - boundsCentreWorld).sqrMagnitude < num3 && current != targetVisible && current != visible)
			{
				vector5 = current.centrePosition.SetY(y);
				num3 = (current.trans.position - boundsCentreWorld).sqrMagnitude;
				f = num4;
			}
		}
		if (m_AVOID_NEARBY && num3 != float.MaxValue)
		{
			avoiding = true;
			float sqrMagnitude4 = (boundsCentreWorld - vector5).SetY(y).sqrMagnitude;
			float sqrMagnitude5 = (vector3 - vector5).SetY(y).sqrMagnitude;
			float sqrMagnitude6 = (vector4 - vector5).SetY(y).sqrMagnitude;
			Vector3 vector6;
			if (sqrMagnitude4 <= sqrMagnitude5 && sqrMagnitude4 <= sqrMagnitude6)
			{
				centreOverlap = true;
				onlyTurn = true;
				vector6 = boundsCentreWorld;
			}
			else if (sqrMagnitude5 <= sqrMagnitude4 && sqrMagnitude5 <= sqrMagnitude6)
			{
				vector6 = vector3;
			}
			else if (sqrMagnitude6 <= sqrMagnitude4 && sqrMagnitude6 <= sqrMagnitude5)
			{
				onlyTurn = true;
				vector6 = vector4;
			}
			else
			{
				_ = 0;
				vector6 = vector4;
			}
			vector6.y = y;
			Vector3 vector7 = (vector6 - vector5).SetY(0f).normalized;
			Vector3 normalized = (vector5 - boundsCentreWorld).SetY(0f).normalized;
			float num5 = Mathf.Abs(vector.normalized.Dot(normalized));
			if (num5 >= 0f && Mathf.Abs(Mathf.Acos(num5) * 57.29578f) < m_MinObstacleAvoidAngle)
			{
				vector7 = Quaternion.AngleAxis((Vector3.Cross(Vector3.up, vector.normalized).normalized.Dot(normalized) > 0f) ? m_MinObstacleAvoidAngle : (0f - m_MinObstacleAvoidAngle), Vector3.up) * vector7;
			}
			float num6 = Mathf.Sqrt(f) + m_AvoidancePadding;
			zero = (vector6 + vector7 * num6).SetY(y);
			Debug.DrawLine(boundsCentreWorld, vector5, Color.red);
			Debug.DrawLine(boundsCentreWorld, zero, Color.green);
			Debug.DrawLine(zero, targetPos, Color.green);
			Debug.DrawLine(vector5, vector6, Color.blue);
		}
		else
		{
			zero = targetPos;
			Debug.DrawLine(boundsCentreWorld.SetY(y), targetPos.SetY(y), Color.green);
		}
		SteerToPosition(tank, zero, avoiding, onlyTurn, centreOverlap, throttleVal, restrict);
		return false;
	}

	public bool DriveAlongLine(Tank tech, Vector3 startPos, Vector3 endPos, Vector3 targetPos, bool lastSegment, float paddedRadius)
	{
		Vector3 vector = tech.visible.centrePosition.SetY(0f);
		TankBlock rootBlock = tech.blockman.GetRootBlock();
		Vector3 normalized = rootBlock.trans.forward.SetY(0f).normalized;
		Vector3 normalized2 = rootBlock.trans.right.SetY(0f).normalized;
		Vector3 normalized3 = (endPos - startPos).normalized;
		float distanceAlongLine;
		Vector3 vector2 = Maths.ClosestPointOnLine(startPos, endPos, vector, out distanceAlongLine);
		bool flag = false;
		if (Mathf.Abs(Vector3.Dot(vector - vector2, Vector3.Cross(Vector3.up, normalized3).normalized)) > paddedRadius - tech.visible.Radius)
		{
			flag = true;
		}
		float num = 0f + tech.visible.Radius + tech.control.Movement.m_TargetReachedTolerance;
		bool flag2 = false;
		Vector3 vector3 = (targetPos - vector).SetY(0f);
		if (vector3.sqrMagnitude < num * num)
		{
			flag2 = true;
		}
		else
		{
			float driveControl = 1f;
			float num2 = 1f;
			Vector3 lhs = (flag ? vector3.normalized : normalized3);
			float num3 = Mathf.Acos(Mathf.Clamp(Vector3.Dot(lhs, normalized), -1f, 1f)) * 57.29578f;
			if (flag)
			{
				Debug.DrawLine(vector.SetY(0f), targetPos.SetY(0f), Color.green, 0f, depthTest: false);
				driveControl = (1f - num3 / 180f) * m_OffPathDriveScale;
				if (num3 <= m_PathInnerAngle)
				{
					num2 = num3 / m_PathInnerAngle * m_OffPathTurnAmount;
				}
				else
				{
					num2 = m_OffPathTurnAmount;
					driveControl = 0f;
				}
			}
			else if (!(num3 <= m_PathInnerAngle))
			{
				driveControl = ((!(num3 <= m_PathOuterAngle)) ? m_PathCorrectionDriveControl : m_PathOuterAngleDriveControl);
			}
			else
			{
				num2 = num3 / m_PathInnerAngle;
			}
			num2 = ((Vector3.Dot(lhs, normalized2) > 0f) ? (0f - num2) : num2);
			tech.control.TurnControl = num2;
			tech.control.DriveControl = driveControl;
		}
		bool result = false;
		if (flag2)
		{
			float num4 = Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized3, normalized), -1f, 1f)) * 57.29578f;
			float num5 = Vector3.Dot(normalized3, normalized2);
			if (num4 > tech.control.Movement.m_TurnToleranceInner)
			{
				tech.control.TurnControl = ((!(num5 >= 0f)) ? 1 : (-1));
			}
			else if ((endPos - vector).SetY(0f).sqrMagnitude < num * num)
			{
				result = true;
			}
			else
			{
				tech.control.DriveControl = 0.5f;
			}
		}
		return result;
	}

	private void SteerToPosition(Tank tank, Vector3 targetPos, bool avoiding, bool onlyTurn, bool centreOverlap, float throttleVal, TankControl.DriveRestriction restrict = TankControl.DriveRestriction.None)
	{
		TankBlock rootBlock = tank.blockman.GetRootBlock();
		float num = 0f;
		float num2 = 0f;
		float num3 = 1f;
		if (avoiding && m_USE_AVOIDANCE)
		{
			Vector3 normalized = (targetPos - tank.boundsCentreWorld).SetY(0f).normalized;
			float num4 = Vector3.Dot(normalized, rootBlock.trans.forward);
			float num5 = Mathf.Acos(Mathf.Clamp(num4, -1f, 1f)) * 57.29578f;
			float num6 = Vector3.Dot(normalized, rootBlock.trans.right);
			if (num4 < 0f && centreOverlap)
			{
				onlyTurn = false;
			}
			num = ((num5 > 30f) ? 0f : ((!onlyTurn) ? m_DriveTurn : m_DriveOnlyTurn));
			num2 = ((!(num6 > 0f)) ? 1 : (-1));
			num3 = 0.5f;
		}
		else
		{
			Vector3 vector = (targetPos - tank.boundsCentreWorld).SetY(0f);
			Vector3 normalized2 = vector.normalized;
			float num7 = Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized2, rootBlock.trans.forward), -1f, 1f)) * 57.29578f;
			float num8 = Vector3.Dot(normalized2, rootBlock.trans.right);
			float num9 = vector.magnitude / m_FullThrottleRange;
			if (num7 < m_TurnToleranceInner && restrict != TankControl.DriveRestriction.ReverseOnly)
			{
				num = 1f;
			}
			else if (num7 < m_TurnToleranceOuter && restrict != TankControl.DriveRestriction.ReverseOnly)
			{
				num = 1f;
				num2 = ((!(num8 > 0f)) ? 1 : (-1));
				float num10 = num7 / m_FullThrottleAngularRange;
				num9 = (num9 + num10) / 2f;
			}
			else if (num7 < 180f - m_TurnToleranceOuterReverse || restrict == TankControl.DriveRestriction.ForwardOnly)
			{
				num2 = ((!((num8 > 0f) ^ (restrict == TankControl.DriveRestriction.ReverseOnly))) ? 1 : (-1));
				num9 = Mathf.Min(num7 / m_FullThrottleAngularRange, 1f);
			}
			else if (num7 < 180f - m_TurnToleranceInnerReverse)
			{
				num = -1f;
				num2 = ((num8 > 0f) ? 1 : (-1));
				float num11 = num7 / m_FullThrottleAngularRange;
				num9 = (num9 + num11) / 2f;
			}
			else
			{
				num = -1f;
			}
			num3 = throttleVal * throttleProfile.Evaluate(num9);
			d.Assert(num3 >= 0f && num3 <= 1f, $"throttleControl expected to be in range [0, 1] but found {num9}");
		}
		tank.control.DriveControl = num * num3;
		tank.control.TurnControl = num2 * num3;
	}

	public bool DriveToDestination(Tank tank, Vector3 toTarget, float throttleD, float throttleT, TankControl.DriveRestriction restrict = TankControl.DriveRestriction.None)
	{
		if (tank.IsAnchored)
		{
			return true;
		}
		float num = 0f;
		float num2 = 0f;
		Vector3 vector = new Vector3(toTarget.x, 0f, toTarget.z);
		if (vector.sqrMagnitude < m_TargetReachedTolerance * m_TargetReachedTolerance)
		{
			return true;
		}
		TankBlock rootBlock = tank.blockman.GetRootBlock();
		Vector3 normalized = vector.normalized;
		float num3 = Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized, rootBlock.trans.forward), -1f, 1f)) * 57.29578f;
		float num4 = Vector3.Dot(normalized, rootBlock.trans.right);
		float num5 = vector.magnitude / m_FullThrottleRange;
		if (num3 < m_TurnToleranceInner && restrict != TankControl.DriveRestriction.ReverseOnly)
		{
			num = 1f;
		}
		else if (num3 < m_TurnToleranceOuter && restrict != TankControl.DriveRestriction.ReverseOnly)
		{
			num = 1f;
			num2 = ((!(num4 > 0f)) ? 1 : (-1));
			float num6 = num3 / m_FullThrottleAngularRange;
			num5 = (num5 + num6) / 2f;
		}
		else if (num3 < 180f - m_TurnToleranceOuterReverse || restrict == TankControl.DriveRestriction.ForwardOnly)
		{
			num2 = ((!((num4 > 0f) ^ (restrict == TankControl.DriveRestriction.ReverseOnly))) ? 1 : (-1));
			num5 = Mathf.Min(num3 / m_FullThrottleAngularRange, 1f);
		}
		else if (num3 < 180f - m_TurnToleranceInnerReverse)
		{
			num = -1f;
			num2 = ((num4 > 0f) ? 1 : (-1));
			float num7 = num3 / m_FullThrottleAngularRange;
			num5 = (num5 + num7) / 2f;
		}
		else
		{
			num = -1f;
		}
		tank.control.DriveControl = num * throttleD * throttleProfile.Evaluate(num5);
		tank.control.TurnControl = num2 * throttleT * throttleProfile.Evaluate(num5);
		return false;
	}

	public void FaceVisible(Tank tank, Visible visible, float throttleVal)
	{
		Vector3 vecToVisible = GetVecToVisible(tank, visible);
		FaceDirection(tank, vecToVisible, throttleVal);
	}

	public void FacePosition(Tank tank, Vector3 position, float throttleVal)
	{
		Vector3 direction = (position - tank.boundsCentreWorld).SetY(0f);
		FaceDirection(tank, direction, throttleVal);
	}

	public void FaceDirection(Tank tank, Vector3 direction, float throttleVal)
	{
		TankBlock rootBlock = tank.blockman.GetRootBlock();
		float num = Mathf.Acos(Mathf.Clamp(Vector3.Dot(direction.normalized, rootBlock.trans.forward), -1f, 1f)) * 57.29578f;
		float num2 = throttleVal * throttleProfile.Evaluate(num / m_FullThrottleAngularRange);
		d.Assert(num2 >= 0f && num2 <= 1f, $"Invalid Throttle: {base.name} {num} {direction} {throttleVal}");
		if (Vector3.Dot(new Vector3(0f - direction.z, 0f, direction.x), rootBlock.trans.forward) < 0f)
		{
			tank.control.TurnControl = num2;
		}
		else
		{
			tank.control.TurnControl = 0f - num2;
		}
	}

	public bool IsFacingVisible(Tank tank, Visible visible, float angleThreshold = 5f)
	{
		return IsFacingPos(tank, visible.trans.position, angleThreshold);
	}

	public bool IsFacingPos(Tank tank, Vector3 pos, float angle = 5f)
	{
		Vector3 direction = (pos - tank.boundsCentreWorld).SetY(0f);
		return IsFacingDirection(tank, direction, angle);
	}

	public bool IsFacingDirection(Tank tank, Vector3 direction, float facingAngle = 15f)
	{
		TankBlock rootBlock = tank.blockman.GetRootBlock();
		if (Mathf.Acos(Vector3.Dot(direction.normalized, rootBlock.trans.forward.SetY(0f).normalized)) <= facingAngle * ((float)Math.PI / 180f))
		{
			return true;
		}
		return false;
	}

	public void FireBoosters(Tank tank)
	{
		tank.control.BoostControlJets = true;
		tank.control.BoostControlProps = true;
	}

	private Vector3 GetVecToVisible(Tank tank, Visible visible)
	{
		return (visible.centrePosition - tank.boundsCentreWorld).SetY(0f);
	}
}
