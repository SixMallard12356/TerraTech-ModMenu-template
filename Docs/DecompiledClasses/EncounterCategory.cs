using System;
using System.Collections.Generic;

[Serializable]
public class EncounterCategory
{
	public string m_Name = "Category Name";

	public List<EncounterData> m_Encounters = new List<EncounterData>();
}
