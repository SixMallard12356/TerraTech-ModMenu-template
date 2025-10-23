public struct ArrayIndexer<T>
{
	private T[] array;

	public T this[int index] => array[index];

	public ArrayIndexer(T[] array)
	{
		this.array = array;
	}
}
