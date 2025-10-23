#define UNITY_EDITOR
using BehaviorDesigner.Runtime.Tasks;

public class HoldingBeamsContain : Conditional
{
	public enum Contains
	{
		Empty,
		Any,
		Full
	}

	public Contains m_Contains;

	private Tank m_Tank;

	public override void OnAwake()
	{
		ModuleAIBot component = GetComponent<ModuleAIBot>();
		m_Tank = component.block.tank;
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_Tank)
		{
			bool flag = true;
			bool flag2 = true;
			BlockManager.BlockIterator<ModuleItemHolder>.Enumerator enumerator = m_Tank.blockman.IterateBlockComponents<ModuleItemHolder>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder current = enumerator.Current;
				if (!current.IsEmpty)
				{
					flag2 = false;
				}
				if (!current.IsFull)
				{
					flag = false;
				}
			}
			TaskStatus result = TaskStatus.Inactive;
			switch (m_Contains)
			{
			case Contains.Empty:
				result = ((!flag2) ? TaskStatus.Failure : TaskStatus.Success);
				break;
			case Contains.Any:
				result = (flag2 ? TaskStatus.Failure : TaskStatus.Success);
				break;
			case Contains.Full:
				result = ((!flag) ? TaskStatus.Failure : TaskStatus.Success);
				break;
			default:
				d.LogError("HoldingBeamsContain - Case not handled");
				break;
			}
			return result;
		}
		return TaskStatus.Failure;
	}
}
