using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : AnimatorParameter
{
	public AnimatorTrigger(string name)
		: base(name)
	{
	}

	public bool SetOnAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		bool num = CheckIsValid(animator, paramLookup);
		if (num)
		{
			animator.SetTrigger(base.NameHash);
		}
		return num;
	}

	public void ResetOnAnimator(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		if (CheckIsValid(animator, paramLookup))
		{
			animator.ResetTrigger(base.NameHash);
		}
	}

	protected override AnimatorControllerParameterType GetParamType()
	{
		return AnimatorControllerParameterType.Trigger;
	}
}
