using UnityEngine;

public class uScript_PlayerInRangeOfCurrentEncounter : uScriptLogic
{
	private bool m_InRange;

	private Encounter m_EncounterObject;

	public bool Out => true;

	public bool True => m_InRange;

	public bool False => !m_InRange;

	public void In(GameObject ownerNode)
	{
		if (!ownerNode)
		{
			return;
		}
		if (!m_EncounterObject)
		{
			m_EncounterObject = ownerNode.GetComponent<Encounter>();
		}
		m_InRange = true;
		if (m_EncounterObject.HasNoPosition)
		{
			return;
		}
		m_InRange = false;
		foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
		{
			if ((allPlayerTech.boundsCentreWorld - m_EncounterObject.Position).ToVector2XZ().magnitude <= m_EncounterObject.EncounterRadius)
			{
				m_InRange = true;
				break;
			}
		}
	}

	public void OnDisable()
	{
		m_InRange = false;
		m_EncounterObject = null;
	}
}
