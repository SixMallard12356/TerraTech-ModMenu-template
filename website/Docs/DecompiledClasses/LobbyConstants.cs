using UnityEngine;

[CreateAssetMenu(fileName = "LobbyConstants", menuName = "Asset/LobbyConstants")]
public class LobbyConstants : ScriptableObject
{
	[SerializeField]
	public Color32[] m_AllColours;

	[SerializeField]
	public Color32[] m_CoOpColours;

	[SerializeField]
	public Color32[] m_CoOpTextColours;

	[SerializeField]
	public Color32[] m_UnpilotedTechColours;

	[SerializeField]
	public NetOptionsAsset[] m_AvailableGameTypes;

	[SerializeField]
	public NetOptionsAsset m_TeamDeathMatchGameType;
}
