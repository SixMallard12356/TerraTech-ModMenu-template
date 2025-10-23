using System;
using UnityEngine;

public class TwilightEffectsComponent : MonoBehaviour
{
	[Serializable]
	public class TwilightLightData
	{
		public Light[] m_Lights;

		public ManTimeOfDay.TwilightValues m_TwilightValues;
	}

	[SerializeField]
	private TwilightLightData m_LightData;

	[SerializeField]
	private ParticleSystem[] m_ParticleSystems;

	private bool m_DayEnding;

	private float m_TwilightTime;

	private void EnableTwilightEffect(bool enableEffect, bool instant)
	{
		ParticleSystemStopBehavior stopBehavior = ((!instant) ? ParticleSystemStopBehavior.StopEmitting : ParticleSystemStopBehavior.StopEmittingAndClear);
		for (int i = 0; i < m_ParticleSystems.Length; i++)
		{
			if (enableEffect)
			{
				m_ParticleSystems[i].Play();
			}
			else
			{
				m_ParticleSystems[i].Stop(withChildren: true, stopBehavior);
			}
		}
		if (enableEffect || instant)
		{
			for (int j = 0; j < m_LightData.m_Lights.Length; j++)
			{
				m_LightData.m_Lights[j].enabled = enableEffect;
			}
		}
		if (instant)
		{
			base.enabled = false;
		}
		else
		{
			UpdateLights(enableEffect);
		}
	}

	private void UpdateLights(bool turnOn)
	{
		m_TwilightTime = 0f;
		m_DayEnding = turnOn;
		base.enabled = true;
	}

	private void OnDayNightChanged(bool isBecomingNight)
	{
		EnableTwilightEffect(isBecomingNight, instant: false);
	}

	private void OnSpawn()
	{
		base.enabled = false;
		Singleton.Manager<ManTimeOfDay>.inst.DayNightChangedEvent.Subscribe(OnDayNightChanged);
		EnableTwilightEffect(Singleton.Manager<ManTimeOfDay>.inst.NightTime, instant: true);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManTimeOfDay>.inst.DayNightChangedEvent.Unsubscribe(OnDayNightChanged);
		EnableTwilightEffect(enableEffect: false, instant: true);
	}

	private void Update()
	{
		float a;
		float b;
		float num;
		if (m_DayEnding)
		{
			a = m_LightData.m_TwilightValues.m_DuskStartVal;
			b = m_LightData.m_TwilightValues.m_DuskEndVal;
			num = Singleton.Manager<ManTimeOfDay>.inst.DuskLength;
		}
		else
		{
			a = m_LightData.m_TwilightValues.m_DawnStartVal;
			b = m_LightData.m_TwilightValues.m_DawnEndVal;
			num = Singleton.Manager<ManTimeOfDay>.inst.DawnLength;
		}
		float num2 = Mathf.Clamp01(m_TwilightTime / num);
		float intensity = Mathf.Lerp(a, b, num2);
		bool flag = !m_DayEnding && num2 >= 1f;
		for (int i = 0; i < m_LightData.m_Lights.Length; i++)
		{
			m_LightData.m_Lights[i].intensity = intensity;
			if (flag)
			{
				m_LightData.m_Lights[i].enabled = false;
			}
		}
		m_TwilightTime += Time.deltaTime;
		if (num2 >= 1f)
		{
			base.enabled = false;
		}
	}
}
