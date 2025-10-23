using System;
using System.Collections.Generic;

public static class AOTUtility
{
	private static bool s_alwaysFalse = DateTime.UtcNow.Year < 0;

	public static void Ensure(Action action)
	{
		if (IsFalse())
		{
			try
			{
				action();
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("", innerException);
			}
		}
	}

	public static void EnsureType<T>() where T : new()
	{
		Ensure(delegate
		{
			new T();
		});
	}

	public static void EnsureStack<T>()
	{
		Ensure(delegate
		{
			Stack<T> stack = new Stack<T>();
			List<T> list = new List<T>();
			Stack<T> stack2 = new Stack<T>(list);
			if (stack != null && list == null)
			{
			}
		});
	}

	public static bool IsFalse()
	{
		return s_alwaysFalse;
	}
}
