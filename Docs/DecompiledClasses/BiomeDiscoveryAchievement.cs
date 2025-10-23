using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/BiomeDiscoveryAchievement")]
public class BiomeDiscoveryAchievement : AchievementObject
{
	[SerializeField]
	private BiomeTypes m_BiomeToDiscover;

	[SerializeField]
	private float kBiomeDiscoveryThreshold = 0.2f;

	public override void Update()
	{
		if (IsActive() && Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Valid)
		{
			for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.NumWeights; i++)
			{
				Biome biome = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Biome(i);
				if (biome != null && biome.BiomeType == m_BiomeToDiscover && Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Weight(i) > kBiomeDiscoveryThreshold)
				{
					CompleteAchievement();
				}
			}
		}
		base.Update();
	}
}
