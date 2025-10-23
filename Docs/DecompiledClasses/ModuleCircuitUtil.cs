#define UNITY_EDITOR
using System;
using UnityEngine;

public static class ModuleCircuitUtil
{
	public static void AutoGenerateCircuitChargePoints(ref Vector3[] chargePoints, Vector3[] aPs, bool setEmpty)
	{
		if (aPs == null)
		{
			Debug.LogError("Tried to generate charge points but missing any Attach Points to generate them on! Aborting...");
			return;
		}
		chargePoints = (setEmpty ? new Vector3[0] : new Vector3[aPs.Length]);
		for (int i = 0; i < chargePoints.Length; i++)
		{
			chargePoints[i] = aPs[i];
		}
	}

	public static void SetupForCircuitInput(this Module module)
	{
		GameObject gameObject = module.gameObject;
		ModuleCircuitNode component = gameObject.GetComponent<ModuleCircuitNode>();
		if (component == null)
		{
			gameObject.AddComponent<ModuleCircuitReceiver>();
			component = gameObject.GetComponent<ModuleCircuitNode>();
			component.AutoGenChargePointsForAllAPs = ModuleCircuitNode.AutoGenChargePoints.AsInputs;
		}
		else
		{
			d.AssertFormat(component.ChargeInPoints.Length != 0 || (component.AutoGenChargePointsForAllAPs & ModuleCircuitNode.AutoGenChargePoints.AsInputs) != 0, module, "SetupForCircuitInput called on {0}; block already contained a ModuleCircuitNode, but no inputs were configured!", module.name);
		}
	}

	[Obsolete("Use of SetupForCircuitOutput is heavily discouraged, as this will make _all_ APs on a block output a circuit signal. Often leading to unexpected C&S networks and therefore behaviour.")]
	public static void SetupForCircuitOutput(this Module module)
	{
		GameObject gameObject = module.gameObject;
		ModuleCircuitNode component = gameObject.GetComponent<ModuleCircuitNode>();
		if (component == null)
		{
			gameObject.AddComponent<ModuleCircuitDispensor>();
			component = gameObject.GetComponent<ModuleCircuitNode>();
			component.AutoGenChargePointsForAllAPs = ModuleCircuitNode.AutoGenChargePoints.AsOutputs;
		}
		else
		{
			d.AssertFormat(component.ChargeOutPoints.Length != 0, module, "SetupForCircuitOutput called on {0}; block already contained a ModuleCircuitNode, but no outputs were configured!", module.name);
		}
	}
}
