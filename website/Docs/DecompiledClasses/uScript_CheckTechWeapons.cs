[NodePath("TerraTech/Actions/Blocks")]
public class uScript_CheckTechWeapons : uScriptLogic
{
	private bool m_AnyHasNoWeapons;

	private bool m_AnyHasOtherWeapons;

	public bool Out => true;

	public bool HasOnlyGivenWeapon
	{
		get
		{
			if (!m_AnyHasNoWeapons)
			{
				return !m_AnyHasOtherWeapons;
			}
			return false;
		}
	}

	public bool HasNoWeapons => m_AnyHasNoWeapons;

	public bool HasOtherWeapons => m_AnyHasOtherWeapons;

	public void In(Tank[] techs, BlockTypes WeaponBlockType)
	{
		m_AnyHasNoWeapons = false;
		m_AnyHasOtherWeapons = false;
		foreach (Tank tank in techs)
		{
			if (!(tank != null))
			{
				continue;
			}
			int num = 0;
			int num2 = 0;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				if (current.BlockCategory == BlockCategories.Weapons)
				{
					num2++;
					if (current.BlockType == WeaponBlockType)
					{
						num++;
					}
				}
			}
			if (num2 > num)
			{
				m_AnyHasOtherWeapons = true;
			}
			if (num2 == 0)
			{
				m_AnyHasNoWeapons = true;
			}
		}
	}
}
