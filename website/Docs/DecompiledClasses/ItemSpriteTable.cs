using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpriteAsset", menuName = "Asset/Table/ItemSpriteTable")]
public class ItemSpriteTable : ScriptableObject
{
	[SerializeField]
	public Sprite[] m_BlockSprites;

	[SerializeField]
	public Sprite[] m_ChunkSprites;
}
