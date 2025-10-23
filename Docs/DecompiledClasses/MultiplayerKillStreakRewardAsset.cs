using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerKillStreakRewardAsset : ScriptableObject
{
	[SerializeField]
	public MultiplayerKillStreakRewardLevel[] m_RewardLevels;

	public void Serialize(NetworkWriter writer)
	{
		writer.Write(m_RewardLevels.Length);
		for (int i = 0; i < m_RewardLevels.Length; i++)
		{
			m_RewardLevels[i].Serialize(writer);
		}
	}

	public void Deserialize(NetworkReader reader)
	{
		int num = reader.ReadInt32();
		m_RewardLevels = new MultiplayerKillStreakRewardLevel[num];
		for (int i = 0; i < num; i++)
		{
			m_RewardLevels[i] = new MultiplayerKillStreakRewardLevel();
			m_RewardLevels[i].Deserialize(reader);
		}
	}
}
