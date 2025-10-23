[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("uScript_IsCoreEncounterCompleted", "Is the specified core mission completed?")]
public class uScript_IsCoreEncounterCompleted : uScriptLogic
{
	private bool m_Completed;

	public bool True => m_Completed;

	public bool False => !m_Completed;

	public void In([FriendlyName("Corp", "The Corporation to check")] FactionSubTypes corp, [FriendlyName("Grade", "The Grade to check. Starts at 1")] int grade, [FriendlyName("Encounter Name", "Name of the encounter")] string encounterName)
	{
		m_Completed = Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(corp, grade, encounterName);
	}

	public void OnEnable()
	{
		m_Completed = false;
	}
}
