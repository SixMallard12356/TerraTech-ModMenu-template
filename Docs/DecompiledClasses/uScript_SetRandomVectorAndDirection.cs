using System;
using UnityEngine;

public class uScript_SetRandomVectorAndDirection : uScriptLogic
{
	public bool Out => true;

	public Vector3 In(float minRange, float maxRange)
	{
		float f = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
		return new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f)) * UnityEngine.Random.Range(minRange, maxRange);
	}
}
