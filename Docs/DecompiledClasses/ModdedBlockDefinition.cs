using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TerraTech/Block Definition")]
public class ModdedBlockDefinition : ModdedAsset
{
	[Header("Block Palette Information")]
	public string m_Corporation = "GSO";

	public string m_BlockIdentifier = "";

	public string m_BlockDisplayName = "";

	public string m_BlockDescription = "";

	[Header("Campaign Progression")]
	public int m_Grade = 1;

	public BlockRarity m_Rarity;

	public int m_Price = 1;

	public BlockCategories m_Category = BlockCategories.Base;

	public bool m_UnlockWithLicense;

	[Header("Render Data")]
	public TankBlockTemplate m_PhysicalPrefab;

	public Texture2D m_Icon;

	[Header("Other Properties")]
	public int m_MaxHealth;

	public float m_Mass;

	public TextAsset m_Json;

	public ManDamage.DamageableType m_DamageableType;

	[Header("Module Data")]
	public List<ModdedModuleDefinition> m_ModuleData = new List<ModdedModuleDefinition>();
}
