#define UNITY_EDITOR
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FindObject : Conditional
{
	public SharedVisible m_Target;

	public bool m_HarvestItem;

	public bool m_DontRetarget;

	public bool m_EnemyHeldObjects;

	public bool m_StayInRangeOfPOI;

	private TechAI m_AI;

	private Visible m_Closest;

	private float m_SqrDistClosest = float.MaxValue;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Target.Value != null && m_DontRetarget)
		{
			if (m_Target.Value.gameObject.activeInHierarchy)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
		if ((bool)m_AI)
		{
			m_Closest = null;
			m_SqrDistClosest = float.MaxValue;
			if ((bool)m_AI.Tech)
			{
				ObjectTypes searchType = m_AI.TargetType.ObjectType;
				if (m_HarvestItem)
				{
					searchType = ObjectTypes.Scenery;
				}
				UpdateVision(searchType);
				_ = m_Closest == null;
			}
			if ((bool)m_Closest)
			{
				m_Target.Value = m_Closest;
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}

	private void UpdateVision(ObjectTypes searchType)
	{
		m_Closest = null;
		m_SqrDistClosest = float.MaxValue;
		Vector3 position = m_AI.Tech.trans.position;
		TechVision.VisibleIterator enumerator = m_AI.Tech.Vision.IterateVisibles(searchType).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			bool flag = false;
			if (current.holderStack != null)
			{
				continue;
			}
			if (m_HarvestItem)
			{
				if (m_AI.TargetType.ObjectType == ObjectTypes.Chunk)
				{
					ResourceDispenser resdisp = current.resdisp;
					if (resdisp != null && (m_AI.TargetAnyItemType || resdisp.HasDispenseType((ChunkTypes)m_AI.TargetType.ItemType)))
					{
						flag = true;
					}
				}
				else
				{
					d.LogError("BehaviorDesigner - FindObject: Set to Harvest, but ModuleAIBot's ObjectToFind type isn't a Chunk");
				}
			}
			else
			{
				flag = m_AI.TargetAnyItemType || current.ItemType == m_AI.TargetType.ItemType;
			}
			if (flag && (!m_StayInRangeOfPOI || !((current.trans.position - m_AI.PointOfInterest).sqrMagnitude > m_AI.POIDist * m_AI.POIDist)))
			{
				float sqrMagnitude = (current.trans.position - position).sqrMagnitude;
				if (sqrMagnitude < m_SqrDistClosest)
				{
					m_Closest = current;
					m_SqrDistClosest = sqrMagnitude;
				}
			}
		}
	}
}
