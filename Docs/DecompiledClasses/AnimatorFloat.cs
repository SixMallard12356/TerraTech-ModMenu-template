using System.Collections.Generic;
using UnityEngine;

public class AnimatorFloat : AnimatorParameter
{
	public AnimatorFloat(string name)
		: base(name)
	{
	}

	public bool SetOnAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup, float value)
	{
		bool num = CheckIsValid(animator, paramLookup);
		if (num)
		{
			animator.SetFloat(base.NameHash, value);
		}
		return num;
	}

	public float GetFromAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		if (CheckIsValid(animator, paramLookup))
		{
			return animator.GetFloat(base.NameHash);
		}
		return 0f;
	}

	protected override AnimatorControllerParameterType GetParamType()
	{
		return AnimatorControllerParameterType.Float;
	}
}
