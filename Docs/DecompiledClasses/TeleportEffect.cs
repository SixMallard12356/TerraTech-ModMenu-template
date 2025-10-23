#define UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
	[SerializeField]
	private float m_TeleportingDuration;

	[SerializeField]
	private float m_PreWhiteFadeDuration;

	[SerializeField]
	private float m_PauseAtWhiteDuration = 1.5f;

	[SerializeField]
	private float m_BloomIntensity;

	[SerializeField]
	private float m_LensDirtIntensity;

	[SerializeField]
	private AnimationCurve m_BloomIntensityCurve;

	[SerializeField]
	private float m_FOVIntensity;

	[SerializeField]
	private AnimationCurve m_FOVCurve;

	private Coroutine m_EffectCoroutine;

	private float m_OriginalFOV;

	public bool IsFadedOut { get; private set; }

	public bool TryStartTeleportationEffect(Action effectEndAction)
	{
		if (m_EffectCoroutine == null)
		{
			m_EffectCoroutine = StartCoroutine(TeleportationEffectCoroutine(effectEndAction));
			return true;
		}
		d.LogError("TeleportEffect.StartTeleportationEffect - Teleportation effect was already in progress!");
		return false;
	}

	public void CancelTeleportationEffect()
	{
		if (m_EffectCoroutine != null)
		{
			StopCoroutine(m_EffectCoroutine);
			m_EffectCoroutine = null;
			CleanUpEffect();
		}
	}

	private IEnumerator TeleportationEffectCoroutine(Action effectEndAction)
	{
		float elapsedTime = 0f;
		m_OriginalFOV = Singleton.camera.fieldOfView;
		bool startedFadeToWhite = false;
		while (elapsedTime <= m_TeleportingDuration)
		{
			float time = Mathf.InverseLerp(0f, m_TeleportingDuration, elapsedTime);
			float num = m_BloomIntensityCurve.Evaluate(time);
			Singleton.Manager<CameraManager>.inst.SetBloomIntensity(m_BloomIntensity * num, m_LensDirtIntensity * num);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.Bloom, enabled: true);
			Singleton.camera.fieldOfView = m_OriginalFOV + m_FOVIntensity * m_FOVCurve.Evaluate(time);
			if (!startedFadeToWhite && elapsedTime >= m_PreWhiteFadeDuration)
			{
				UILoadingScreenHints.SuppressNextHint = true;
				Singleton.Manager<ManUI>.inst.FadeToColour(Color.white, 3f, forceFront: false, showAnim: false);
				startedFadeToWhite = true;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		CleanUpEffect();
		effectEndAction();
		GC.Collect();
		IsFadedOut = true;
		yield return new WaitForSeconds(m_PauseAtWhiteDuration);
		IsFadedOut = false;
		Singleton.Manager<ManUI>.inst.ClearFade(3f);
		m_EffectCoroutine = null;
	}

	private void CleanUpEffect()
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.Bloom, enabled: false);
		Singleton.camera.fieldOfView = m_OriginalFOV;
	}
}
