using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterList", menuName = "Asset/Table/EncounterList")]
public class EncounterList : ScriptableObject
{
	[SerializeField]
	private List<CorpEncounters> m_Encounters = new List<CorpEncounters>();

	public List<CorpEncounters> Encounters => m_Encounters;
}
