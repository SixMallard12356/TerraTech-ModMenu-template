#define UNITY_EDITOR
using System;
using UnityEngine;

public static class GamepadVibration
{
	[Serializable]
	public struct VibrationSetting
	{
		[Range(0f, 1f)]
		public float largeMotorAmount;

		[Range(0f, 1f)]
		public float smallMotorAmount;

		public float duration;
	}

	public enum Type
	{
		PlayerTankDestroyed,
		PlayerTankHit
	}

	public static void VibratePad(Type type, bool hasPriority)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && Globals.inst.m_CurrentGamepadVibration && (!IsVibrating() || hasPriority))
		{
			if (IsVibrating())
			{
				StopVibration();
			}
			SetPadVibration(type);
		}
	}

	private static void SetPadVibration(Type type)
	{
		VibrationSetting vibrationSetting = default(VibrationSetting);
		switch (type)
		{
		case Type.PlayerTankDestroyed:
			vibrationSetting = Globals.inst.m_GamepadVibrationPlayerDestroyed;
			break;
		case Type.PlayerTankHit:
			vibrationSetting = Globals.inst.m_GamepadVibrationPlayerHit;
			break;
		default:
			d.LogError("SetPadVibration - Unimplemented GamepadVibration.Type {type} found!");
			break;
		}
		Singleton.Manager<ManInput>.inst.SetVibration(vibrationSetting.largeMotorAmount, vibrationSetting.smallMotorAmount, vibrationSetting.duration);
	}

	private static bool IsVibrating()
	{
		return Singleton.Manager<ManInput>.inst.IsVibrating();
	}

	public static void StopVibration()
	{
		if (Singleton.Manager<ManInput>.inst != null)
		{
			Singleton.Manager<ManInput>.inst.StopVibration();
		}
	}
}
