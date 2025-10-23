using UnityEngine;

public class PlayAudioLoop : MonoBehaviour
{
	[SerializeField]
	private ManSFX.MiscSfxType m_SFXType;

	private void OnSpawn()
	{
		Singleton.Manager<ManSFX>.inst.PlayMiscLoopingSFX(m_SFXType, base.transform);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManSFX>.inst.StopMiscLoopingSFX(m_SFXType, base.transform);
	}
}
