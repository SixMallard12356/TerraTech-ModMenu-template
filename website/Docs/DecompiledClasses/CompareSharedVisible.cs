using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedVisible : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedVisible variable;

	[Tooltip("The variable to compare to")]
	public SharedVisible compareTo;

	public override TaskStatus OnUpdate()
	{
		if (!variable.Value.Equals(compareTo.Value))
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}

	public override void OnReset()
	{
		variable = null;
		compareTo = null;
	}
}
