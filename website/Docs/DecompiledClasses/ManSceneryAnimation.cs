#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManSceneryAnimation : Singleton.Manager<ManSceneryAnimation>
{
	public enum AnimTypes
	{
		None,
		Shake,
		Topple,
		Vanish,
		Regrow
	}

	private class AnimState
	{
		public GameObject targetGO;

		public AnimTypes animType;

		public float time;

		public Action animFinishedEvent;
	}

	[SerializeField]
	[EnumArray(typeof(AnimTypes))]
	private AnimationClip[] m_Animations;

	private List<AnimState> m_PlayingAnimations = new List<AnimState>();

	public void PlayAnimation(AnimTypes animType, GameObject targetGO, Action animFinishedEvent = null)
	{
		if (animType != AnimTypes.None)
		{
			m_PlayingAnimations.Add(new AnimState
			{
				targetGO = targetGO,
				animType = animType,
				time = 0f,
				animFinishedEvent = animFinishedEvent
			});
		}
	}

	public void StopAnimation(AnimTypes animType, GameObject targetGO)
	{
		if (animType == AnimTypes.None)
		{
			return;
		}
		for (int num = m_PlayingAnimations.Count - 1; num >= 0; num--)
		{
			AnimState animState = m_PlayingAnimations[num];
			if (animType == animState.animType && targetGO == animState.targetGO)
			{
				m_PlayingAnimations.RemoveAt(num);
				break;
			}
		}
	}

	public void StopAllAnimation(GameObject targetGO)
	{
		for (int num = m_PlayingAnimations.Count - 1; num >= 0; num--)
		{
			AnimState animState = m_PlayingAnimations[num];
			if (targetGO == animState.targetGO)
			{
				m_PlayingAnimations.RemoveAt(num);
			}
		}
	}

	public bool IsPlaying(AnimTypes animType, GameObject targetGO)
	{
		if (animType == AnimTypes.None)
		{
			return false;
		}
		foreach (AnimState playingAnimation in m_PlayingAnimations)
		{
			if (animType == playingAnimation.animType && targetGO == playingAnimation.targetGO)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsPlayingAny(GameObject targetGO)
	{
		foreach (AnimState playingAnimation in m_PlayingAnimations)
		{
			if (targetGO == playingAnimation.targetGO)
			{
				return true;
			}
		}
		return false;
	}

	public void SampleAnimation(AnimTypes animType, GameObject targetGO, float sampleTime = 0f)
	{
		if (animType != AnimTypes.None)
		{
			AnimationClip animationClip = m_Animations[(int)animType];
			float num = Mathf.Clamp(sampleTime, 0f, animationClip.length);
			d.Assert(sampleTime == num, $"SampleAnimation - Animation sample time {sampleTime} was outside the length of the animation {animationClip.length} for object {targetGO.name}!");
			animationClip.SampleAnimation(targetGO, num);
		}
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		for (int num = m_PlayingAnimations.Count - 1; num >= 0; num--)
		{
			AnimState animState = m_PlayingAnimations[num];
			float num2 = animState.time + deltaTime;
			AnimationClip animationClip = m_Animations[(int)animState.animType];
			float length = animationClip.length;
			bool flag = false;
			if (num2 >= length)
			{
				num2 = length;
				flag = true;
			}
			if (animState.targetGO != null)
			{
				animationClip.SampleAnimation(animState.targetGO, num2);
			}
			if (flag)
			{
				if (animState.animFinishedEvent != null)
				{
					animState.animFinishedEvent();
				}
				m_PlayingAnimations.RemoveAt(num);
			}
			else
			{
				animState.time = num2;
			}
		}
	}
}
