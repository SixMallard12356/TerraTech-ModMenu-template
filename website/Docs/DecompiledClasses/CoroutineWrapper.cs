#define UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;

public class CoroutineWrapper
{
	public Exception exception { get; private set; }

	public void Start(MonoBehaviour b, IEnumerator coroutine)
	{
		b.StartCoroutine(CreateInternalRoutine(coroutine));
	}

	private IEnumerator CreateInternalRoutine(IEnumerator coroutine)
	{
		while (true)
		{
			try
			{
				if (!coroutine.MoveNext())
				{
					break;
				}
			}
			catch (Exception ex)
			{
				d.Log("coroutine exception: " + ex.Message);
				exception = ex;
				break;
			}
			if (CheckShouldExit(coroutine))
			{
				break;
			}
			yield return coroutine.Current;
		}
	}

	protected virtual bool CheckShouldExit(IEnumerator coroutine)
	{
		return false;
	}
}
public class CoroutineWrapper<T> : CoroutineWrapper
{
	private T returnVal;

	public T Value
	{
		get
		{
			if (base.exception != null)
			{
				throw base.exception;
			}
			return returnVal;
		}
	}

	protected override bool CheckShouldExit(IEnumerator coroutine)
	{
		if (coroutine.Current != null && coroutine.Current.GetType() == typeof(T))
		{
			returnVal = (T)coroutine.Current;
			return true;
		}
		return false;
	}
}
