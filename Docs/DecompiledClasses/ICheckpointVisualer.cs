public interface ICheckpointVisualer
{
	void Initialise(Checkpoint checkpoint, int relativeIndex, float timeLimit, int numFutureGatesToShow);

	void RelativeIndexUpdated(int relativeIndex, int numFutureGatesToShow);

	void StartCleanup();

	bool IsReadyWithCleanup();
}
