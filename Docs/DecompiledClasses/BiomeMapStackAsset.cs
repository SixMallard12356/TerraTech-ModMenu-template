using UnityEngine;

public class BiomeMapStackAsset : ScriptableObject
{
	[SerializeField]
	private BiomeMapStack m_BiomeMapStack;

	public BiomeMapStack MapStack => m_BiomeMapStack;
}
