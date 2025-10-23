using System;
using System.Collections.Generic;

[Serializable]
public class EncounterGrade
{
	public string m_Name = "Grade No";

	public List<EncounterCategory> m_Categories = new List<EncounterCategory>();
}
