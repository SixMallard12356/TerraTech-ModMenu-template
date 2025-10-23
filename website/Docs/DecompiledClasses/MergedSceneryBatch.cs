using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MergedSceneryBatch : MonoBehaviour
{
	private MeshFilter m_MeshFilter;

	private void OnPool()
	{
		m_MeshFilter = GetComponent<MeshFilter>();
		m_MeshFilter.sharedMesh = new Mesh();
	}

	private void OnRecycle()
	{
		if (m_MeshFilter.sharedMesh != null)
		{
			m_MeshFilter.sharedMesh.Clear();
		}
	}
}
