using System.Collections.Generic;
using UnityEngine;

public class DLCTable : ScriptableObject
{
	[SerializeField]
	private List<ManDLC.DLC> m_DLCPacks = new List<ManDLC.DLC>();

	public List<ManDLC.DLC> DLCPacks => m_DLCPacks;
}
