using System;
using UnityEngine;

[Serializable]
public class StickInputInterpreter
{
	[Serializable]
	public struct AngleRemapping
	{
		[Range(-180f, 180f)]
		public float inputAngleDegrees;

		[Range(-180f, 180f)]
		public float outputAngleDegrees;
	}

	[SerializeField]
	[Range(0f, 1f)]
	private float m_OuterDeadzone = 0.9f;

	[SerializeField]
	private AngleRemapping[] m_InputAngleRemappings = new AngleRemapping[0];

	[SerializeField]
	private bool m_ApplyInitialInputDirectionBias;

	[SerializeField]
	[Range(0f, -1f)]
	[Tooltip("Arc Cosine of the maximum angle between the initial input direction and the current stick input before we lose our directional affinity. (How much before it snaps)")]
	private float m_InitialInputBiasMaxReach;

	private Vector2 m_InitialInputDirection;

	private const float kInputDirectionMinMagnitudeSquared = 0.040000003f;

	public Vector2 InterpretAnalogStickInput(Vector2 inputValue)
	{
		Vector2 vector = inputValue;
		float num = Mathf.Abs(vector.x);
		if (num > m_OuterDeadzone)
		{
			vector.x = Mathf.Sign(vector.x) * 1f;
		}
		else
		{
			float value = Mathf.InverseLerp(0f, m_OuterDeadzone, num);
			vector.x = Mathf.Sign(vector.x) * Mathf.Clamp01(value);
		}
		float num2 = Mathf.Abs(vector.y);
		if (num2 > m_OuterDeadzone)
		{
			vector.y = Mathf.Sign(vector.y) * 1f;
		}
		else
		{
			float value2 = Mathf.InverseLerp(0f, m_OuterDeadzone, num2);
			vector.y = Mathf.Sign(vector.y) * Mathf.Clamp01(value2);
		}
		int num3 = m_InputAngleRemappings.Length;
		if (num3 > 0 && vector.sqrMagnitude > 0f)
		{
			float num4 = Vector2.SignedAngle(vector, Vector2.up);
			float num5 = num4;
			float num6 = float.MaxValue;
			int num7 = -1;
			float num8 = float.MaxValue;
			int num9 = -1;
			for (int i = 0; i < num3; i++)
			{
				float num10 = Mathf.DeltaAngle(m_InputAngleRemappings[i].inputAngleDegrees, num4);
				float num11 = Mathf.Abs(num10);
				if (num10 > 0f && num11 < num6)
				{
					num6 = num11;
					num7 = i;
				}
				if (num10 < 0f && num11 < num8)
				{
					num8 = num11;
					num9 = i;
				}
			}
			AngleRemapping angleRemapping = m_InputAngleRemappings[num7];
			AngleRemapping angleRemapping2 = m_InputAngleRemappings[num9];
			float inputAngleDegrees = angleRemapping.inputAngleDegrees;
			float inputAngleDegrees2 = angleRemapping2.inputAngleDegrees;
			float b = Mathf.DeltaAngle(inputAngleDegrees, inputAngleDegrees2);
			float value3 = Mathf.DeltaAngle(inputAngleDegrees, num4);
			num5 = Mathf.LerpAngle(t: Mathf.InverseLerp(0f, b, value3), a: angleRemapping.outputAngleDegrees, b: angleRemapping2.outputAngleDegrees);
			if (num5 != num4)
			{
				float magnitude = vector.magnitude;
				float f = num5 * ((float)Math.PI / 180f);
				vector = new Vector2(Mathf.Sin(f), Mathf.Cos(f)) * magnitude;
				vector.x = Mathf.Clamp(vector.x, -1f, 1f);
				vector.y = Mathf.Clamp(vector.y, -1f, 1f);
				if (num5 == 90f || num5 == -90f)
				{
					vector.y = 0f;
				}
			}
		}
		if (m_ApplyInitialInputDirectionBias)
		{
			if (vector.sqrMagnitude < 0.040000003f)
			{
				m_InitialInputDirection = Vector2.zero;
			}
			else if (m_InitialInputDirection == Vector2.zero || Vector2.Dot(m_InitialInputDirection, inputValue.normalized) < m_InitialInputBiasMaxReach)
			{
				m_InitialInputDirection = ((vector.y < 0f) ? Vector2.down : Vector2.up);
			}
			else if (Mathf.Sign(vector.y) != m_InitialInputDirection.y)
			{
				float magnitude2 = vector.magnitude;
				vector.y = m_InitialInputDirection.y * 0.05f;
				vector = vector.normalized * magnitude2;
				vector.x = Mathf.Clamp(vector.x, -1f, 1f);
				vector.y = Mathf.Clamp(vector.y, -1f, 1f);
			}
		}
		return vector;
	}
}
