using System.Collections;
using UnityEngine;

public static class CoroutineWrapperExtensions
{
	public static CoroutineWrapper StartWrappedCoroutine(this MonoBehaviour obj, IEnumerator coroutine)
	{
		CoroutineWrapper coroutineWrapper = new CoroutineWrapper();
		coroutineWrapper.Start(obj, coroutine);
		return coroutineWrapper;
	}

	public static CoroutineWrapper<T> StartWrappedCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine)
	{
		CoroutineWrapper<T> coroutineWrapper = new CoroutineWrapper<T>();
		coroutineWrapper.Start(obj, coroutine);
		return coroutineWrapper;
	}
}
