using UnityEngine;

public class uScript_FinishEncounter : uScriptLogic
{
	public bool Out => true;

	public void Succeed(GameObject owner)
	{
		Encounter componentInChildren = owner.GetComponentInChildren<Encounter>(includeInactive: true);
		Singleton.Manager<ManEncounter>.inst.FinishEncounter(componentInChildren, ManEncounter.FinishState.Completed);
	}

	public void Fail(GameObject owner)
	{
		Encounter componentInChildren = owner.GetComponentInChildren<Encounter>(includeInactive: true);
		Singleton.Manager<ManEncounter>.inst.FinishEncounter(componentInChildren, ManEncounter.FinishState.Failed);
	}
}
