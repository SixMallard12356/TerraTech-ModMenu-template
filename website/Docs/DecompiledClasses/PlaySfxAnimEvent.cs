#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaySfxAnimEvent : MonoBehaviour
{
	[FormerlySerializedAs("m_UseTransform")]
	[SerializeField]
	private bool m_PlayAtTransformPosition;

	[SerializeField]
	[Tooltip("Instead of playing a miscSFX sound event at the camera or at a position, play a Transform Oneshot SFX event following a transform")]
	private bool m_UseFollowTransformEvent;

	public void PlaySFXAtCamera(string sfxEnumName)
	{
		if (m_UseFollowTransformEvent)
		{
			if (!Enum.TryParse<ManSFX.TransformOneshotSFXTypes>(sfxEnumName, out var result))
			{
				d.LogErrorFormat("PlaySfxAnimEvent - Could not parse anim event string {0} on game object {1}. As we're using 'm_UseFollowTransformEvent', it must match exactly one of the enum values in ManSFX.TransformOneshotSFXTypes", sfxEnumName, base.gameObject.name);
			}
			else
			{
				Singleton.Manager<ManSFX>.inst.PlayTransformOneshotSFX(result, base.transform);
			}
			return;
		}
		ManSFX.MiscSfxType sfxType;
		try
		{
			sfxType = (ManSFX.MiscSfxType)Enum.Parse(typeof(ManSFX.MiscSfxType), sfxEnumName);
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("PlaySfxAnimEvent - Could not parse anim event string {0} on game object {1}. It must match exactly one of the enum values in ManSFX.MiscSFXType. Original Execption: {2}", sfxEnumName, base.gameObject.name, ex.Message);
			return;
		}
		if (m_PlayAtTransformPosition)
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(sfxType, base.transform.position);
		}
		else
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(sfxType);
		}
	}
}
