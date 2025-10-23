#define UNITY_EDITOR
using BehaviorDesigner.Runtime.Tasks;

public class TankHasModule : Conditional
{
	private Tank m_Tank;

	private bool m_HasHolder;

	private void OnTankStructureChanged(TankBlock block, Tank tech)
	{
		d.Assert(tech == m_Tank);
		m_HasHolder = HasHolder();
	}

	private bool HasHolder()
	{
		return m_Tank.blockman.IterateBlockComponents<ModuleItemHolder>().FirstOrDefault();
	}

	public override void OnAwake()
	{
		ModuleAIBot component = GetComponent<ModuleAIBot>();
		m_Tank = component.block.tank;
		m_Tank.DetachEvent.Subscribe(OnTankStructureChanged);
		m_Tank.AttachEvent.Subscribe(OnTankStructureChanged);
		m_HasHolder = HasHolder();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_Tank)
		{
			if (m_HasHolder)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
		return TaskStatus.Failure;
	}
}
