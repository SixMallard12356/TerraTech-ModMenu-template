public class ConsumableFlag
{
	private bool m_Consumed;

	public bool HasBeenConsumed => m_Consumed;

	public bool CanConsume => !HasBeenConsumed;

	public void Consume()
	{
		m_Consumed = true;
	}
}
