using UnityEngine;

public class PIDController
{
	public float previousError;

	public float integral;

	private float output;

	public void Update(float inputValue, float targetValue)
	{
		float num = targetValue - inputValue;
		integral += Singleton.Manager<ManFlyingAI>.inst.PIDKi * num * Time.deltaTime;
		float num2 = (num - previousError) / Time.deltaTime;
		output = Singleton.Manager<ManFlyingAI>.inst.PIDKp * num + integral + Singleton.Manager<ManFlyingAI>.inst.PIDKd * num2;
		previousError = num;
	}

	public void ResetError()
	{
		integral = 0f;
		previousError = 0f;
	}

	public float GetOutput()
	{
		return output;
	}
}
