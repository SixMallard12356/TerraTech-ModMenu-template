using System.Collections.Generic;
using UnityEngine;

public class AnimatorBool : AnimatorParameter
{
	public AnimatorBool(string name)
		: base(name)
	{
	}

	public bool SetOnAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup, bool value)
	{
		bool num = CheckIsValid(animator, paramLookup);
		if (num)
		{
			animator.SetBool(base.NameHash, value);
		}
		return num;
	}

	public bool GetFromAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		if (CheckIsValid(animator, paramLookup))
		{
			return animator.GetBool(base.NameHash);
		}
		return false;
	}

	protected override AnimatorControllerParameterType GetParamType()
	{
		return AnimatorControllerParameterType.Bool;
	}
}
