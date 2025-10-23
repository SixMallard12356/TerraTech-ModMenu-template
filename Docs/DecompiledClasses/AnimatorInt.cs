using System.Collections.Generic;
using UnityEngine;

public class AnimatorInt : AnimatorParameter
{
	public AnimatorInt(string name)
		: base(name)
	{
	}

	public bool SetOnAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup, int value)
	{
		bool num = CheckIsValid(animator, paramLookup);
		if (num)
		{
			animator.SetInteger(base.NameHash, value);
		}
		return num;
	}

	protected override AnimatorControllerParameterType GetParamType()
	{
		return AnimatorControllerParameterType.Int;
	}
}
