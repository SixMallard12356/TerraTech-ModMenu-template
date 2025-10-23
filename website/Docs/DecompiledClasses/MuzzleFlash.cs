using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class MuzzleFlash : MonoBehaviour
{
	[FormerlySerializedAs("speedFactor")]
	[SerializeField]
	public float m_SpeedFactor = 1f;

	private Animation anim;

	private string firstAnimName;

	public void Fire(bool allowRestart = false)
	{
		if (!anim.isPlaying || allowRestart)
		{
			base.gameObject.SetActive(value: true);
			anim.Rewind();
			anim.Play();
		}
	}

	public void Hold(bool on)
	{
		if (base.gameObject.activeSelf != on)
		{
			base.gameObject.SetActive(on);
			AnimationState animationState = anim[firstAnimName];
			if (on)
			{
				animationState.time = animationState.clip.length;
				animationState.speed = 0f;
			}
			else
			{
				animationState.speed = 1f;
				animationState.time = 0f;
			}
		}
	}

	public void OnAnimEvent(int v)
	{
		if (v == 1)
		{
			anim.Stop();
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnPool()
	{
		GetComponentsInChildren<AnimEvent>(includeInactive: true).FirstOrDefault().HandleEvent.Subscribe(OnAnimEvent);
		anim = GetComponentsInChildren<Animation>(includeInactive: true).FirstOrDefault();
		foreach (AnimationState item in anim)
		{
			if (firstAnimName == null)
			{
				firstAnimName = item.name;
			}
			item.speed = m_SpeedFactor;
		}
	}
}
