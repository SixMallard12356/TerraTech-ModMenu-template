#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ManWeather : Singleton.Manager<ManWeather>
{
	private class WeatherState
	{
		public Biome biome;

		public ParticleSystem weatherInstance;

		public bool active;

		public int frameTouched;
	}

	private Dictionary<int, WeatherState> m_ActiveWeatherStatus = new Dictionary<int, WeatherState>();

	private void StartBiomeWeather(Biome biome)
	{
		if (biome.WeatherParticles == null)
		{
			return;
		}
		WeatherState value = null;
		if (!m_ActiveWeatherStatus.TryGetValue(biome.GetHashCode(), out value))
		{
			value = new WeatherState
			{
				biome = biome
			};
			m_ActiveWeatherStatus[biome.GetHashCode()] = value;
		}
		if (!value.weatherInstance)
		{
			value.weatherInstance = biome.WeatherParticles.Spawn();
			if (value.weatherInstance.main.simulationSpace != ParticleSystemSimulationSpace.World)
			{
				d.LogWarning("weather particle system '" + value.weatherInstance.name + "' not simulating in world space");
			}
		}
		if ((bool)Singleton.playerTank && value.weatherInstance.transform.parent != Singleton.playerTank.trans)
		{
			value.weatherInstance.transform.parent = Singleton.playerTank.trans;
		}
		value.active = true;
		value.frameTouched = Time.frameCount;
		if (value.weatherInstance.isStopped)
		{
			value.weatherInstance.Play();
		}
	}

	private void StopBiomeWeather(Biome biome)
	{
		WeatherState value = null;
		if (!m_ActiveWeatherStatus.TryGetValue(biome.GetHashCode(), out value))
		{
			return;
		}
		value.frameTouched = Time.frameCount;
		if ((bool)value.weatherInstance)
		{
			if (value.weatherInstance.isPlaying)
			{
				value.weatherInstance.Stop();
			}
			if (!value.weatherInstance.IsAlive())
			{
				value.active = false;
				value.weatherInstance.Recycle();
				value.weatherInstance = null;
			}
		}
	}

	private void Update()
	{
		ManWorld.CachedBiomeBlendWeights currentBiomeWeights = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights;
		for (int num = currentBiomeWeights.NumWeights - 1; num >= 0; num--)
		{
			Biome biome = currentBiomeWeights.Biome(num);
			if ((bool)biome)
			{
				float num2 = currentBiomeWeights.Weight(num);
				if (num2 > 0.6f)
				{
					StartBiomeWeather(biome);
				}
				else if (num2 < 0.4f)
				{
					StopBiomeWeather(biome);
				}
			}
		}
		Dictionary<int, WeatherState>.Enumerator enumerator = m_ActiveWeatherStatus.GetEnumerator();
		while (enumerator.MoveNext())
		{
			WeatherState value = enumerator.Current.Value;
			if (value != null && value.frameTouched != Time.frameCount && value.active)
			{
				StopBiomeWeather(value.biome);
			}
		}
	}
}
