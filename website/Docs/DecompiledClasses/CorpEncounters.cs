using System;
using System.Collections.Generic;

[Serializable]
public class CorpEncounters
{
	public FactionSubTypes m_Corp;

	public List<EncounterGrade> m_Grades = new List<EncounterGrade>();
}
