using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerDeathStreakRewards : ScriptableObject
{
	[SerializeField]
	public MultiplayerDeathStreakReward[] m_RewardLevels;

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
		m_RewardLevels = new MultiplayerDeathStreakReward[num];
		for (int i = 0; i < num; i++)
		{
			m_RewardLevels[i] = new MultiplayerDeathStreakReward();
			m_RewardLevels[i].Deserialize(reader);
		}
	}
}
