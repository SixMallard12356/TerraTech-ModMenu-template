using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FindBaseObject : Conditional
{
	public SharedVisible m_Target;

	public BlockTypes m_BlockType;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		Visible visible = null;
		float num = float.MaxValue;
		Vector3 position = transform.position;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsAITeam))
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = item.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				if (current.visible.ItemType == (int)m_BlockType)
				{
					float sqrMagnitude = (position - current.trans.position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						visible = current.visible;
						num = sqrMagnitude;
					}
				}
			}
		}
		if ((bool)visible)
		{
			m_Target.Value = visible;
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}

	private bool TechIsAITeam(Tank tech)
	{
		return tech.Team == m_AI.Tech.Team;
	}
}
