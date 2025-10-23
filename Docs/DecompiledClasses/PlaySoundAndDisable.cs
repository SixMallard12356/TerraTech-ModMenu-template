using UnityEngine;
using UnityEngine.Serialization;

public class PlaySoundAndDisable : MonoBehaviour
{
	[SerializeField]
	private string m_Sound;

	[SerializeField]
	private ManSFX.MiscSfxType m_SFXType;

	[FormerlySerializedAs("m_RecycleWhenFinished")]
	[SerializeField]
	private bool m_RecycleAfterSpawn = true;

	[SerializeField]
	private bool m_Use3DSound = true;

	private void OnSpawn()
	{
		if (m_Use3DSound)
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(m_SFXType, base.transform.position);
		}
		else
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(m_SFXType);
		}
	}

	private void Update()
	{
		if (m_RecycleAfterSpawn && !base.transform.IsBeingRecycled())
		{
			base.transform.Recycle();
		}
	}
}
