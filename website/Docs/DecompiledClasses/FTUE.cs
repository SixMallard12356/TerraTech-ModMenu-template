using System.Collections;
using UnityEngine;

public class FTUE : Singleton.Manager<FTUE>
{
	public Coroutine RunInSequence(IEnumerator sequence)
	{
		return StartCoroutine(sequence);
	}

	public void RunParallel(IEnumerator loop)
	{
		StartCoroutine(loop);
	}

	public void Execute(IEnumerator sequence)
	{
		Stop();
		StartCoroutine(sequence);
	}

	public void Stop()
	{
		StopAllCoroutines();
	}
}
