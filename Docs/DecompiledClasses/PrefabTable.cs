using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabTable", menuName = "Asset/Table/PrefabTable")]
public class PrefabTable : ScriptableObject
{
	[SerializeField]
	public ResourceFileList[] m_ObjectFolders;

	public IEnumerable<GameObject> GetAllObjects()
	{
		ResourceFileList[] objectFolders = m_ObjectFolders;
		foreach (ResourceFileList resourceFileList in objectFolders)
		{
			foreach (GameObject @object in resourceFileList.objects)
			{
				if (@object != null)
				{
					yield return @object;
				}
			}
		}
	}
}
