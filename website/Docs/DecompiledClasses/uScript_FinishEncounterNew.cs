using UnityEngine;

[FriendlyName("[Deprecated] Finish Encounter")]
[NodeDeprecated(typeof(uScript_FinishEncounter))]
public class uScript_FinishEncounterNew : uScriptLogic
{
	public bool Out => true;

	public void In(GameObject owner)
	{
		Encounter encounter = owner.GetComponentsInChildren<Encounter>(includeInactive: true)[0];
		Singleton.Manager<ManEncounter>.inst.FinishEncounter(encounter, ManEncounter.FinishState.Completed);
	}
}
