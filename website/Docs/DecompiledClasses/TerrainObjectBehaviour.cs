using UnityEngine;

public class TerrainObjectBehaviour : MonoBehaviour
{
	[SerializeField]
	private float m_WindSwayScale = 1f;

	[SerializeField]
	private int m_MinInstances = 1;

	[SerializeField]
	private int m_MaxInstances = 1;

	[SerializeField]
	private float m_SpreadFactor = 1f;

	public float WindSwayScale => m_WindSwayScale;

	public int MinInstances => m_MinInstances;

	public int MaxInstances => m_MaxInstances;

	public float SpreadFactor => m_SpreadFactor;
}
