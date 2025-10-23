using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitBashPanelTable", menuName = "Asset/Table/KitBashPanelTable")]
public class KitBashPanelTable : ScriptableObject
{
	[SerializeField]
	public string m_FolderName;

	[SerializeField]
	public List<KitBashPanel> m_Panels;

	public void Rebuild()
	{
		if (m_Panels == null)
		{
			m_Panels = new List<KitBashPanel>();
		}
	}
}
