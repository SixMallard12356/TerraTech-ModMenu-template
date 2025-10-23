using BehaviorDesigner.Runtime.Tasks;

public class AllHoldingBeamsFull : Conditional
{
	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		bool flag = false;
		BlockManager.BlockIterator<ModuleItemHolder>.Enumerator enumerator = m_AI.Tech.blockman.IterateBlockComponents<ModuleItemHolder>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder current = enumerator.Current;
			flag = true;
			if (!current.IsFull)
			{
				return TaskStatus.Failure;
			}
		}
		if (flag)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
