using UnityEngine;

public class ModulePlatformRestrictions : Module
{
	public enum PlatformAvailability
	{
		Singleplayer,
		Multiplayer,
		Steam,
		Humble,
		XBoxOne,
		PS4,
		Switch,
		NetEase,
		EpicGamesStore
	}

	[SerializeField]
	[EnumFlag]
	private PlatformAvailability m_PlatformAvailability = (PlatformAvailability)(-1);

	public void InitPlatformRestrictions(Visible visible)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, visible.ItemType);
		Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptorFlags<PlatformAvailability>(hashCode, (int)m_PlatformAvailability);
	}

	private void PrePool()
	{
		Object.Destroy(this);
	}
}
