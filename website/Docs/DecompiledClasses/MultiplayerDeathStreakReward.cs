using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class MultiplayerDeathStreakReward
{
	[SerializeField]
	public BlockCount[] m_Rewards;

	public void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Rewards.Length);
		for (int i = 0; i < m_Rewards.Length; i++)
		{
			writer.Write((int)m_Rewards[i].m_BlockType);
			writer.Write(m_Rewards[i].m_Quantity);
		}
	}

	public void Deserialize(NetworkReader reader)
	{
		int num = reader.ReadInt32();
		m_Rewards = new BlockCount[num];
		for (int i = 0; i < num; i++)
		{
			m_Rewards[i] = new BlockCount((BlockTypes)reader.ReadInt32(), reader.ReadInt32());
		}
	}
}
