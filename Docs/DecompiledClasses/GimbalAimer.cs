#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class GimbalAimer : MonoBehaviour
{
	public enum AxisConstraint
	{
		Free,
		X,
		Y
	}

	public AxisConstraint rotationAxis;

	public float[] rotationLimits = new float[2];

	public float aimClampMaxPercent = 1f;

	private Transform myTransform;

	private float currentAngle;

	private float currentResetAngle;

	private Quaternion restingOrientationLocal;

	[SerializeField]
	public float m_XAngleAimOffset;

	public bool Aim(Transform target, float rotateSpeed)
	{
		d.Assert(target);
		return Aim(target.position, rotateSpeed);
	}

	public bool Aim(Vector3 targetWorld, float rotateSpeed)
	{
		Vector3 targetRelative;
		float targetAngle;
		Vector3 vUpLocal;
		bool flag = CalcAim(targetWorld, out targetRelative, out targetAngle, out vUpLocal);
		if (flag && rotationAxis == AxisConstraint.Free)
		{
			AimFree(targetRelative, rotateSpeed);
			return true;
		}
		float num = targetAngle - currentAngle;
		if (num > 180f)
		{
			num -= 360f;
		}
		else if (num < -180f)
		{
			num += 360f;
		}
		float num2 = rotateSpeed * Time.deltaTime;
		num = Mathf.Clamp(num, 0f - num2, num2);
		currentAngle += num;
		myTransform.localRotation = restingOrientationLocal * Quaternion.AngleAxis(currentAngle, vUpLocal);
		if (currentAngle > 180f)
		{
			currentAngle -= 360f;
		}
		else if (currentAngle < -180f)
		{
			currentAngle += 360f;
		}
		return flag;
	}

	public void AimDefault(float rotateSpeed)
	{
		currentResetAngle = Quaternion.Angle(myTransform.localRotation, restingOrientationLocal);
		if (currentResetAngle > 2f)
		{
			float t = Mathf.Clamp01(rotateSpeed * Time.deltaTime / currentResetAngle);
			myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, restingOrientationLocal, t);
			if (rotationAxis != AxisConstraint.Free)
			{
				currentAngle = ((rotationAxis == AxisConstraint.X) ? myTransform.localRotation.eulerAngles.x : myTransform.localRotation.eulerAngles.y);
			}
		}
		else if (currentResetAngle != 0f)
		{
			myTransform.localRotation = restingOrientationLocal;
		}
	}

	public void AimFree(Vector3 toTargetLocal, float rotateSpeed)
	{
		Quaternion to = Quaternion.LookRotation(toTargetLocal);
		float maxDegreesDelta = Mathf.Max(Mathf.Abs(rotationLimits[0]), Mathf.Abs(rotationLimits[1]));
		myTransform.localRotation = Quaternion.RotateTowards(restingOrientationLocal, to, maxDegreesDelta);
	}

	public void ResetAngles()
	{
		currentAngle = 0f;
		currentResetAngle = 0f;
		myTransform.localRotation = restingOrientationLocal;
	}

	public bool CanAim(Vector3 targetWorld)
	{
		Vector3 targetRelative;
		float targetAngle;
		Vector3 vUpLocal;
		return CalcAim(targetWorld, out targetRelative, out targetAngle, out vUpLocal);
	}

	private bool CalcAim(Vector3 targetWorld, out Vector3 targetRelative, out float targetAngle, out Vector3 vUpLocal)
	{
		bool result = true;
		targetRelative = myTransform.parent.InverseTransformDirection(targetWorld - myTransform.position);
		switch (rotationAxis)
		{
		case AxisConstraint.X:
			targetAngle = m_XAngleAimOffset + Mathf.Atan2(0f - targetRelative.y, targetRelative.z) * 57.29578f;
			vUpLocal = Vector3.right;
			break;
		case AxisConstraint.Y:
			targetAngle = Mathf.Atan2(targetRelative.x, targetRelative.z) * 57.29578f;
			vUpLocal = Vector3.up;
			break;
		default:
			targetAngle = 0f;
			vUpLocal = Vector3.zero;
			return true;
		}
		if (rotationLimits[0] != rotationLimits[1])
		{
			float num = targetAngle / (1f + aimClampMaxPercent);
			if (num < rotationLimits[0] || num > rotationLimits[1])
			{
				targetAngle = 0f;
				result = false;
			}
			else
			{
				targetAngle = Mathf.Clamp(targetAngle, rotationLimits[0], rotationLimits[1]);
			}
		}
		return result;
	}

	private void PrePool()
	{
		if (rotationLimits[1] < rotationLimits[0])
		{
			float num = rotationLimits[0];
			rotationLimits[0] = rotationLimits[1];
			rotationLimits[1] = num;
		}
	}

	private void OnPool()
	{
		myTransform = base.transform;
		restingOrientationLocal = base.transform.localRotation;
		aimClampMaxPercent = Mathf.Max(aimClampMaxPercent, 0f);
	}

	private void OnSpawn()
	{
		currentAngle = 0f;
		currentResetAngle = 0f;
		base.transform.localRotation = restingOrientationLocal;
	}

	private void OnDrawGizmos()
	{
		if (!base.gameObject.EditorParentSelected())
		{
			return;
		}
		Vector3 vector = ((rotationAxis == AxisConstraint.X) ? Vector3.right : ((rotationAxis == AxisConstraint.Y) ? Vector3.up : Vector3.zero));
		if (!(vector != Vector3.zero) || rotationLimits[0] == rotationLimits[1])
		{
			return;
		}
		d.Assert(base.transform.parent, "GimbalAimer cannot be a root object");
		Vector3 direction = restingOrientationLocal * Vector3.forward;
		Gizmos.color = Color.green;
		Vector3 vector2 = base.transform.parent.TransformDirection(direction);
		float num = rotationLimits[1] - rotationLimits[0];
		int num2 = Mathf.Max(Mathf.Abs(Mathf.RoundToInt(num / 20f)), 1);
		float num3 = num / (float)num2;
		Vector3 axis = base.transform.parent.TransformDirection(vector);
		Vector3 vector3 = Vector3.zero;
		for (int i = 0; i <= num2; i++)
		{
			Quaternion quaternion = Quaternion.AngleAxis(rotationLimits[0] + num3 * (float)i, axis);
			Vector3 vector4 = base.transform.position + quaternion * vector2 * 2f;
			if (i != 0)
			{
				Gizmos.DrawLine(vector3, vector4);
			}
			if (i == 0 || i == num2)
			{
				Gizmos.DrawLine(base.transform.position, vector4);
			}
			vector3 = vector4;
		}
	}
}
