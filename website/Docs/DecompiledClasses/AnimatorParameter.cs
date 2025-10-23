using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorParameter
{
	private enum State
	{
		NotInitialised,
		NotFound,
		Valid
	}

	private State m_State;

	protected int NameHash { get; private set; }

	public AnimatorParameter(string name)
	{
		m_State = State.NotInitialised;
		NameHash = Animator.StringToHash(name);
	}

	public bool Exists(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		return CheckIsValid(animator, paramLookup);
	}

	protected bool CheckIsValid(Animator animator, Dictionary<int, AnimatorControllerParameterType> paramLookup)
	{
		if (m_State == State.NotInitialised)
		{
			if (paramLookup != null && paramLookup.TryGetValue(NameHash, out var value) && value == GetParamType())
			{
				m_State = State.Valid;
			}
			else
			{
				m_State = State.NotFound;
			}
		}
		if (m_State == State.Valid && animator != null)
		{
			return animator.isActiveAndEnabled;
		}
		return false;
	}

	protected abstract AnimatorControllerParameterType GetParamType();
}
