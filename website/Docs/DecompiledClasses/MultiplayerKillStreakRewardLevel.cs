using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class MultiplayerKillStreakRewardLevel
{
	[SerializeField]
	public int m_KillsRequired;

	[SerializeField]
	public BlockCount m_BlockReward;

	public void Serialize(NetworkWriter writer)
	{
		writer.Write(m_KillsRequired);
		writer.Write((int)m_BlockReward.m_BlockType);
		writer.Write(m_BlockReward.m_Quantity);
	}

	public void Deserialize(NetworkReader reader)
	{
		m_KillsRequired = reader.ReadInt32();
		m_BlockReward = new BlockCount((BlockTypes)reader.ReadInt32(), reader.ReadInt32());
	}
}
