using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[DisallowMultipleComponent]
public class ModuleBlockAttributes : Module
{
	[Bitfield(typeof(BlockAttributes))]
	[SerializeField]
	private int m_BlockAttributeFlags;

	[Bitfield(typeof(BlockControlAttributes))]
	[SerializeField]
	private int m_BlockControlFlags;

	public void SetAttributeFlag(BlockAttributes attribute, bool state)
	{
		if (state)
		{
			m_BlockAttributeFlags |= 1 << (int)attribute;
		}
		else
		{
			m_BlockAttributeFlags &= ~(1 << (int)attribute);
		}
	}

	public void InitBlockAttributes(Visible visible)
	{
		int hashCode = visible.m_ItemType.GetHashCode();
		Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptorFlags<BlockAttributes>(hashCode, m_BlockAttributeFlags);
		Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptorFlags<BlockControlAttributes>(hashCode, m_BlockControlFlags);
	}

	private void PrePool()
	{
		Object.Destroy(this);
	}
}
