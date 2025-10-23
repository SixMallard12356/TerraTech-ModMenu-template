using UnityEngine;
using UnityEngine.Networking;

public class PromptClientRespawnMessage : MessageBase
{
	public MultiplayerTechSelectGroupAsset m_Loadouts;

	public override void Serialize(NetworkWriter writer)
	{
		m_Loadouts.Serialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Loadouts = ScriptableObject.CreateInstance<MultiplayerTechSelectGroupAsset>();
		m_Loadouts.Deserialize(reader);
	}
}
