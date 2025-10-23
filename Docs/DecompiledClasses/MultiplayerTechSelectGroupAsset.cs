using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerTechSelectGroupAsset : ScriptableObject
{
	[SerializeField]
	private MultiplayerTechSelectPresetAsset m_GsoTechPreset;

	[SerializeField]
	private MultiplayerTechSelectPresetAsset m_GeoCorpTechPreset;

	[SerializeField]
	private MultiplayerTechSelectPresetAsset m_VentureTechPreset;

	[SerializeField]
	private MultiplayerTechSelectPresetAsset m_HawkeyeTechPreset;

	public List<MultiplayerTechSelectPresetAsset> GetTechPresets()
	{
		return new List<MultiplayerTechSelectPresetAsset> { m_GsoTechPreset, m_GeoCorpTechPreset, m_VentureTechPreset, m_HawkeyeTechPreset };
	}

	public void Serialize(NetworkWriter writer)
	{
		m_GsoTechPreset.Serialize(writer);
		m_GeoCorpTechPreset.Serialize(writer);
		m_VentureTechPreset.Serialize(writer);
		m_HawkeyeTechPreset.Serialize(writer);
	}

	public void Deserialize(NetworkReader reader)
	{
		m_GsoTechPreset = ScriptableObject.CreateInstance<MultiplayerTechSelectPresetAsset>();
		m_GeoCorpTechPreset = ScriptableObject.CreateInstance<MultiplayerTechSelectPresetAsset>();
		m_VentureTechPreset = ScriptableObject.CreateInstance<MultiplayerTechSelectPresetAsset>();
		m_HawkeyeTechPreset = ScriptableObject.CreateInstance<MultiplayerTechSelectPresetAsset>();
		m_GsoTechPreset.Deserialize(reader);
		m_GeoCorpTechPreset.Deserialize(reader);
		m_VentureTechPreset.Deserialize(reader);
		m_HawkeyeTechPreset.Deserialize(reader);
	}
}
