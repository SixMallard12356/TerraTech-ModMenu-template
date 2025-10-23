using System.Collections.Generic;
using UnityEngine;

public class LaserLabShutterSwitch : MonoBehaviour
{
	[SerializeField]
	private Transform m_Shutter;

	private bool m_WasSpawned;

	private static Dictionary<string, LaserLabShutterSwitch> k_LoadedSwitches = new Dictionary<string, LaserLabShutterSwitch>();

	public void SetAnimationTrigger(string triggerName)
	{
		if (m_Shutter != null)
		{
			Animator component = m_Shutter.gameObject.GetComponent<Animator>();
			if ((bool)component)
			{
				component.SetTrigger(triggerName);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.GetComponent<Tank>() == Singleton.playerTank)
		{
			SetAnimationTrigger("Open");
		}
	}

	public static void TriggerPressurePadOn(string padName)
	{
		if (k_LoadedSwitches.TryGetValue(padName, out var value))
		{
			value.SetAnimationTrigger("Open");
		}
		else
		{
			Debug.LogError("[LaserLabShutterSwitch.TriggerPressurePadOn] Couldn't find pressure-pad <b>" + padName + "</b>");
		}
	}

	public static void TriggerPressurePadOff(string padName)
	{
		if (k_LoadedSwitches.TryGetValue(padName, out var value))
		{
			value.SetAnimationTrigger("Close");
		}
		else
		{
			Debug.LogError("[LaserLabShutterSwitch.TriggerPressurePadOff] Couldn't find pressure-pad <b>" + padName + "</b>");
		}
	}

	private void OnSpawn()
	{
		if (k_LoadedSwitches.ContainsKey(base.name))
		{
			Debug.LogError("LaserLabShutterSwitch.OnSpawn - A LaserLabShutterSwitch with this name already exists and is registered.");
		}
		else
		{
			k_LoadedSwitches.Add(base.name, this);
			m_WasSpawned = true;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		if (m_WasSpawned)
		{
			k_LoadedSwitches.Remove(base.name);
			m_WasSpawned = false;
		}
	}
}
