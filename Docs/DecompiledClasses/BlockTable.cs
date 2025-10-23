using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockTable", menuName = "Asset/Table/BlockTable")]
public class BlockTable : ScriptableObject
{
	[SerializeField]
	public string m_FolderName;

	[SerializeField]
	public List<GameObject> m_Blocks;

	public void Rebuild()
	{
		if (m_Blocks == null)
		{
			m_Blocks = new List<GameObject>();
		}
	}
}
