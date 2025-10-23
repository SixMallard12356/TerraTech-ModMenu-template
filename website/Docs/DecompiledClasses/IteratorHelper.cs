using System.Collections.Generic;

public static class IteratorHelper
{
	public static IEnumerable<T> Iterate<T>(this IEnumerator<T> iterator)
	{
		while (iterator.MoveNext())
		{
			yield return iterator.Current;
		}
	}
}
