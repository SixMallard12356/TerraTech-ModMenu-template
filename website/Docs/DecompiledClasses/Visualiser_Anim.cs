using System;
using System.Collections;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class Visualiser_Anim : MonoBehaviour
{
	[SerializeField]
	private Animation m_Animation;

	private bool m_IsOn;

	public bool IsOn => m_IsOn;

	public void SetOn(bool on, bool instant = false)
	{
		m_IsOn = on;
		bool flag = on;
		bool flag2 = false;
		IEnumerator enumerator = m_Animation.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.Current;
				flag2 = animationState.speed > 0f;
				if (!flag)
				{
					_ = animationState.normalizedTime;
				}
				else
				{
					_ = animationState.normalizedTime;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		if (flag2 != flag || instant)
		{
			foreach (AnimationState item in m_Animation)
			{
				if (instant)
				{
					item.normalizedTime = (flag ? 1f : 0f);
				}
				else
				{
					item.normalizedTime = Mathf.Clamp01(item.normalizedTime);
				}
				item.speed = (flag ? 1f : (-1f));
			}
		}
		if (!m_Animation.isPlaying)
		{
			m_Animation.Play();
		}
	}

	public void Reset()
	{
		SetOn(on: false, instant: true);
	}
}
