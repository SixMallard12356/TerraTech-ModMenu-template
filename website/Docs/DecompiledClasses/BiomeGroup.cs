using UnityEngine;

public class BiomeGroup : ScriptableObject
{
	[HideInInspector]
	[SerializeField]
	private Biome[] m_Biomes;

	[SerializeField]
	[HideInInspector]
	private float[] m_BiomeWeights;

	[SerializeField]
	private AnimationCurve m_WeightingByDistance = AnimationCurve.Linear(0f, 1f, 100f, 0f);

	public Biome[] Biomes => m_Biomes;

	public float[] BiomeWeights => m_BiomeWeights;

	public AnimationCurve WeightingByDistance => m_WeightingByDistance;
}
