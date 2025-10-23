using UnityEngine;

[CreateAssetMenu(menuName = "TerraTech/Corp Definition")]
public class ModdedCorpDefinition : ModdedAsset
{
	public string m_ShortName = "CORP";

	public string m_DisplayName = "Custom Corp";

	public string m_RewardCorp = "GSO";

	public Texture2D m_Icon;

	public ModdedSkinDefinition[] m_DefaultSkinSlots;
}
