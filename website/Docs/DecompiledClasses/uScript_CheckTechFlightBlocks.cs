[NodePath("TerraTech/Actions/Blocks")]
public class uScript_CheckTechFlightBlocks : uScriptLogic
{
	private bool m_AnyHasFlightBlocks;

	public bool Out => true;

	public bool HasFlightBlocks => m_AnyHasFlightBlocks;

	public bool DoesntHaveFlightBlocks => !m_AnyHasFlightBlocks;

	public void In(Tank[] techs)
	{
		m_AnyHasFlightBlocks = false;
		foreach (Tank tank in techs)
		{
			if (!(tank != null))
			{
				continue;
			}
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				if (current.BlockCategory == BlockCategories.Flight)
				{
					if (current.gameObject.GetComponent<ModuleAntiGravityEngine>() != null)
					{
						m_AnyHasFlightBlocks = true;
						break;
					}
					if (current.gameObject.GetComponent<ModuleHover>() != null)
					{
						m_AnyHasFlightBlocks = true;
						break;
					}
					ModuleWing component = current.gameObject.GetComponent<ModuleWing>();
					if (component != null && component.LiftStrength > 0.5f)
					{
						m_AnyHasFlightBlocks = true;
						break;
					}
					ModuleBooster component2 = current.gameObject.GetComponent<ModuleBooster>();
					if (component2 != null && !component2.IsRocketBooster)
					{
						m_AnyHasFlightBlocks = true;
						break;
					}
				}
			}
		}
	}
}
