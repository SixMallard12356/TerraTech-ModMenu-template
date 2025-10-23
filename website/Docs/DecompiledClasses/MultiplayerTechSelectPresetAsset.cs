using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerTechSelectPresetAsset : ScriptableObject
{
	[SerializeField]
	public TankPreset m_TankPreset;

	[SerializeField]
	public MultiplayerKillStreakRewardAsset m_KillStreakRewards;

	[SerializeField]
	public MultiplayerDeathStreakRewards m_DeathStreakRewards;

	[SerializeField]
	public InventoryBlockList m_InventoryBlockList1;

	[SerializeField]
	public InventoryBlockList m_InventoryBlockList2;

	[SerializeField]
	public LocalisedString m_TankName;

	public void Serialize(NetworkWriter writer)
	{
		m_TankPreset.NetSerialize(writer);
		m_KillStreakRewards.Serialize(writer);
		m_DeathStreakRewards.Serialize(writer);
		m_InventoryBlockList1.NetSerialize(writer);
		m_InventoryBlockList2.NetSerialize(writer);
		writer.Write(m_TankName);
	}

	public void Deserialize(NetworkReader reader)
	{
		m_TankPreset = TankPreset.CreateInstance();
		m_InventoryBlockList1 = new InventoryBlockList();
		m_InventoryBlockList2 = new InventoryBlockList();
		m_KillStreakRewards = ScriptableObject.CreateInstance<MultiplayerKillStreakRewardAsset>();
		m_DeathStreakRewards = ScriptableObject.CreateInstance<MultiplayerDeathStreakRewards>();
		m_TankPreset.NetDeserialize(reader);
		m_KillStreakRewards.Deserialize(reader);
		m_DeathStreakRewards.Deserialize(reader);
		m_InventoryBlockList1.NetDeserialize(reader);
		m_InventoryBlockList2.NetDeserialize(reader);
		m_TankName = reader.ReadLocalisedString();
	}
}
