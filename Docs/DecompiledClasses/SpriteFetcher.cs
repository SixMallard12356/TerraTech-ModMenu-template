#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteFetcher : MonoBehaviour
{
	private Dictionary<int, Sprite>[] m_ModSprites = new Dictionary<int, Sprite>[7];

	private Dictionary<ChunkCategory, Sprite> m_ChunkCatLookup;

	[SerializeField]
	private ItemSpriteTable m_ItemSpriteTable;

	[SerializeField]
	private Sprite[] m_ScenerySprites;

	[SerializeField]
	private Sprite m_UnknownBlockSprite;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private Sprite[] m_CorpIcons;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private Sprite[] m_SelectedCorpIcons;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private Sprite[] m_ModernCorpIcons;

	[SerializeField]
	[EnumArray(typeof(AICategories))]
	[FormerlySerializedAs("m_AIControlIcons")]
	private Sprite[] m_AICategoryIcons;

	[SerializeField]
	[EnumArray(typeof(BlockCategories))]
	private Sprite[] m_BlockCatIcons;

	[EnumArray(typeof(BlockRarity))]
	[SerializeField]
	private Sprite[] m_BlockRarityIcons;

	[SerializeField]
	[EnumArray(typeof(ManDamage.DamageType))]
	private Sprite[] m_DamageTypeIcons;

	[EnumArray(typeof(ManDamage.DamageableType))]
	[SerializeField]
	private Sprite[] m_DamageableTypeIcons;

	[EnumArray(typeof(BlockAttributes))]
	[SerializeField]
	private Sprite[] m_BlockAttrIcons;

	[EnumArray(typeof(ChunkCategory))]
	[SerializeField]
	private Sprite[] m_ChunkCatIcons;

	[EnumArray(typeof(BlockRarity))]
	[SerializeField]
	private Sprite[] m_ChunkRarityIcons;

	[SerializeField]
	[EnumArray(typeof(UndoTypes))]
	private Sprite[] m_UndoTypeIcons;

	public void PurgeModSprites()
	{
		SetModSprites(ObjectTypes.Chunk, null);
		SetModSprites(ObjectTypes.Block, null);
	}

	public void SetModSprites(ObjectTypes type, Dictionary<int, Sprite> sprites)
	{
		m_ModSprites[(int)type] = sprites;
	}

	public Sprite GetSprite(ItemTypeInfo itemTypeInfo)
	{
		return GetSprite(itemTypeInfo.ObjectType, itemTypeInfo.ItemType);
	}

	public Sprite GetSprite(ObjectTypes objectType, int itemType)
	{
		Sprite value = null;
		switch (objectType)
		{
		case ObjectTypes.Block:
			value = GetSprite(m_ItemSpriteTable.m_BlockSprites, itemType);
			break;
		case ObjectTypes.Scenery:
			value = GetSprite(m_ScenerySprites, itemType);
			break;
		case ObjectTypes.Chunk:
			value = GetSprite(m_ItemSpriteTable.m_ChunkSprites, itemType);
			break;
		default:
			d.LogError("Tried to get Sprite of type: " + objectType.ToString() + " but that type isn't supported yet");
			break;
		}
		if (value == null && m_ModSprites[(int)objectType] != null)
		{
			m_ModSprites[(int)objectType].TryGetValue(itemType, out value);
		}
		if (value == null)
		{
			value = m_UnknownBlockSprite;
		}
		return value;
	}

	public void SetSprite(ItemTypeInfo objectType, Texture2D texture)
	{
		Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
		bool flag = false;
		switch (objectType.ObjectType)
		{
		case ObjectTypes.Block:
			flag = SetSprite(ref m_ItemSpriteTable.m_BlockSprites, objectType.ItemType, sprite);
			break;
		case ObjectTypes.Scenery:
			flag = SetSprite(ref m_ScenerySprites, objectType.ItemType, sprite);
			break;
		case ObjectTypes.Chunk:
			flag = SetSprite(ref m_ItemSpriteTable.m_ChunkSprites, objectType.ItemType, sprite);
			break;
		default:
			d.LogError("Tried to get Sprite of type: " + objectType.ToString() + " but that type isn't supported yet");
			break;
		}
		if (!flag)
		{
			d.LogWarning("Failed to set sprite for item: " + objectType.name);
		}
	}

	public Sprite GetCorpIcon(FactionSubTypes corp)
	{
		if ((int)corp < m_CorpIcons.Length)
		{
			return m_CorpIcons[(int)corp];
		}
		ModdedCorpDefinition moddedCorpDefinition = Singleton.Manager<ManMods>.inst.FindCorp((int)corp);
		if (moddedCorpDefinition != null)
		{
			return Sprite.Create(moddedCorpDefinition.m_Icon, new Rect(0f, 0f, moddedCorpDefinition.m_Icon.width, moddedCorpDefinition.m_Icon.height), Vector2.zero);
		}
		d.LogError($"Could not find corp icon for corp {corp}");
		return null;
	}

	public Sprite GetSelectedCorpIcon(FactionSubTypes corp)
	{
		if ((int)corp < m_SelectedCorpIcons.Length)
		{
			return m_SelectedCorpIcons[(int)corp];
		}
		ModdedCorpDefinition moddedCorpDefinition = Singleton.Manager<ManMods>.inst.FindCorp((int)corp);
		if (moddedCorpDefinition != null)
		{
			return Sprite.Create(moddedCorpDefinition.m_Icon, new Rect(0f, 0f, moddedCorpDefinition.m_Icon.width, moddedCorpDefinition.m_Icon.height), Vector2.zero);
		}
		d.LogError($"Could not find corp icon for corp {corp}");
		return null;
	}

	public Sprite GetModernCorpIcon(FactionSubTypes corp)
	{
		if ((int)corp < m_ModernCorpIcons.Length)
		{
			return m_ModernCorpIcons[(int)corp];
		}
		ModdedCorpDefinition moddedCorpDefinition = Singleton.Manager<ManMods>.inst.FindCorp((int)corp);
		if (moddedCorpDefinition != null)
		{
			return Sprite.Create(moddedCorpDefinition.m_Icon, new Rect(0f, 0f, moddedCorpDefinition.m_Icon.width, moddedCorpDefinition.m_Icon.height), Vector2.zero);
		}
		d.LogError($"Could not find corp icon for corp {corp}");
		return null;
	}

	public Sprite GetAICategoryIcon(AICategories aiCategory)
	{
		return m_AICategoryIcons[(int)aiCategory];
	}

	public Sprite GetBlockCatIcon(BlockCategories blockCat)
	{
		return m_BlockCatIcons[(int)blockCat];
	}

	public Sprite GetBlockRarity(BlockRarity blockRarity)
	{
		return m_BlockRarityIcons[(int)blockRarity];
	}

	public Sprite GetDamageTypeIcon(ManDamage.DamageType damageType)
	{
		return m_DamageTypeIcons[(int)damageType];
	}

	public Sprite GetDamageableTypeIcon(ManDamage.DamageableType damageableType)
	{
		return m_DamageableTypeIcons[(int)damageableType];
	}

	public Sprite GetBlockAttributeIcon(BlockAttributes blockAttribute)
	{
		int num = (int)blockAttribute;
		if (num < m_BlockAttrIcons.Length)
		{
			return m_BlockAttrIcons[num];
		}
		d.LogErrorFormat("GetBlockAttributeIcon - Attribute {0} out of range of m_BlockAttrIcons list! Did the Enum change without updating the Prefab to match the new length of the Enum?", blockAttribute.ToString());
		return null;
	}

	public Sprite GetChunkRarity(ChunkRarity chunkRarity)
	{
		return m_ChunkRarityIcons[(int)chunkRarity];
	}

	public Sprite GetChunkCategoryIcon(ChunkCategory chunkCat)
	{
		ChunkCategory chunkDominantCategory = Singleton.Manager<RecipeManager>.inst.GetChunkDominantCategory(chunkCat);
		return m_ChunkCatLookup[chunkDominantCategory];
	}

	public Sprite GetUndoType(UndoTypes undoType)
	{
		return m_UndoTypeIcons[(int)undoType];
	}

	private Sprite GetSprite(Sprite[] spriteArray, int i)
	{
		Sprite result = null;
		if (spriteArray != null && i >= 0 && i < spriteArray.Length)
		{
			result = spriteArray[i];
		}
		return result;
	}

	private bool SetSprite(ref Sprite[] spriteArray, int i, Sprite sprite)
	{
		bool result = false;
		if (spriteArray.Length > i)
		{
			spriteArray[i] = sprite;
			result = true;
		}
		return result;
	}

	private void VerifyArraySize()
	{
		int count = EnumIterator<BlockTypes>.Count;
		int count2 = EnumIterator<ChunkTypes>.Count;
		if (m_ItemSpriteTable.m_BlockSprites.Length != count)
		{
			Array.Resize(ref m_ItemSpriteTable.m_BlockSprites, EnumIterator<BlockTypes>.Count);
			d.LogError("SpriteFetcher - ItemSpritesTable block sprites are out of date and need to be regenerated by running the main game in the editor - some items may be missing");
		}
		if (m_ItemSpriteTable.m_ChunkSprites.Length != count2)
		{
			Array.Resize(ref m_ItemSpriteTable.m_ChunkSprites, EnumIterator<ChunkTypes>.Count);
			d.LogError("SpriteFetcher - ItemSpritesTable chunk sprites are out of date and need to be regenerated by running the main game in the editor - some items may be missing");
		}
	}

	private void Awake()
	{
		Array.Resize(ref m_ScenerySprites, EnumIterator<SceneryTypes>.Count);
		GetComponent<AutoSpriteCreate>().DoOnceAfterSpritesLoaded(VerifyArraySize);
		m_ChunkCatLookup = new Dictionary<ChunkCategory, Sprite>(m_ChunkCatIcons.Length);
		Array values = Enum.GetValues(typeof(ChunkCategory));
		for (int i = 0; i < m_ChunkCatIcons.Length; i++)
		{
			m_ChunkCatLookup.Add((ChunkCategory)values.GetValue(i), m_ChunkCatIcons[i]);
		}
	}
}
