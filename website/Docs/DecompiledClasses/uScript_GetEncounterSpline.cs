#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetEncounterSpline : uScriptLogic
{
	private TrackSpline m_Spline;

	public bool Out => true;

	public TrackSpline In(GameObject owner)
	{
		if (m_Spline == null)
		{
			Encounter component = owner.GetComponent<Encounter>();
			if (component != null)
			{
				CheckpointChallengeData checkpointChallengeData = component.ChallengeData as CheckpointChallengeData;
				if (checkpointChallengeData != null)
				{
					m_Spline = checkpointChallengeData.Track.spline;
				}
				else
				{
					d.LogError("uScript_GetEncounterSpline - Encounter does not have ChallengeData set, or ChallengeData is not of type CheckpointChallengeData! Spline not available!");
				}
			}
			else
			{
				d.LogError("uScript_GetEncounterSpline - null Encounter!");
			}
		}
		return m_Spline;
	}

	public void OnDisable()
	{
		m_Spline = null;
	}
}
