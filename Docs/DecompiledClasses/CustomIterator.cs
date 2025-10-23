using System;
using System.Collections.Generic;

public class CustomIterator<T> where T : class
{
	public int position = -1;

	private Func<int> count;

	private Func<int, T> getAtIndex;

	private Func<T, bool> skipItem;

	public T Current => getAtIndex(position);

	public CustomIterator(Func<int> count, Func<int, T> getAtIndex, Func<T, bool> skipItem = null)
	{
		this.count = count;
		this.getAtIndex = getAtIndex;
		this.skipItem = skipItem;
	}

	public bool MoveNext()
	{
		do
		{
			position++;
		}
		while (position < count() && skipItem != null && skipItem(getAtIndex(position)));
		return position < count();
	}

	public CustomIterator<T> GetEnumerator()
	{
		position = -1;
		return this;
	}

	public int Count()
	{
		return count();
	}

	public IEnumerable<TResult> Select<TResult>(Func<T, TResult> selector)
	{
		CustomIterator<T> customIterator = GetEnumerator();
		try
		{
			while (customIterator.MoveNext())
			{
				T current = customIterator.Current;
				yield return selector(current);
			}
		}
		finally
		{
			IDisposable disposable = customIterator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	public T FirstOrDefault()
	{
		if (count() <= 0)
		{
			return null;
		}
		return getAtIndex(0);
	}

	public bool Contains(T visible)
	{
		CustomIterator<T> customIterator = GetEnumerator();
		try
		{
			while (customIterator.MoveNext())
			{
				if (customIterator.Current == visible)
				{
					return true;
				}
			}
		}
		finally
		{
			IDisposable disposable = customIterator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return false;
	}
}
