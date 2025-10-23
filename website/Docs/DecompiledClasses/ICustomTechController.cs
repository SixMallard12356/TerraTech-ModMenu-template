public interface ICustomTechController
{
	int DefaultPriority { get; }

	bool ExecuteControl(bool additive);
}
