public interface IEvent
{
	void Clear();

	bool HasSubscribers();

	int GetSubscriberCount();
}
