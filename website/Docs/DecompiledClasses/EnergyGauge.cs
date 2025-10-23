using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class EnergyGauge : MonoBehaviour
{
	[SerializeField]
	private bool m_UseMeterRenderingOverride;

	[SerializeField]
	private TechEnergy.MeterRenderingProfile m_MeterRenderingOverrides;

	public int m_NumLeds;

	public bool m_UseCachedMaterials = true;

	private EnergyGauge m_GuageMaterialOwner;

	private int m_NumLit;

	private float m_BlinkSpeedActual;

	private static Dictionary<Tank, EnergyGauge> s_TankToMaterialMappings = new Dictionary<Tank, EnergyGauge>(64);

	private static Tank s_CachedTank;

	private static EnergyGauge s_CachedGauge;

	private Renderer[] m_Renderers;

	private Material m_LocalMat;

	private float m_powerLevel;

	public void UpdateGaugeLevel(Tank attachedTank, float fullness)
	{
		m_powerLevel = fullness;
		if (attachedTank.IsNotNull())
		{
			UpdateGaugeTankwise(attachedTank);
		}
		else
		{
			UpdateGaugeDirectly();
		}
	}

	public void OnDetach(Tank fromTank)
	{
		if (m_UseCachedMaterials && GetGaugeResponsibleForTank(fromTank) == this)
		{
			RemoveGaugeResponsibleForTank(fromTank);
		}
		Renderer[] renderers = m_Renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].sharedMaterial = m_LocalMat;
		}
		m_GuageMaterialOwner = null;
		UpdateGaugeDisplay();
	}

	private void UpdateGaugeTankwise(Tank tank)
	{
		EnergyGauge energyGauge = this;
		if (m_UseCachedMaterials)
		{
			energyGauge = GetGaugeResponsibleForTank(tank);
			if (energyGauge.IsNull())
			{
				AddGaugeResponsibleForTank(tank, this);
				energyGauge = this;
				Renderer[] renderers = m_Renderers;
				for (int i = 0; i < renderers.Length; i++)
				{
					renderers[i].sharedMaterial = m_LocalMat;
				}
				m_GuageMaterialOwner = energyGauge;
			}
		}
		if ((object)energyGauge == this)
		{
			UpdateGaugeDisplay();
		}
		else if ((object)energyGauge != m_GuageMaterialOwner)
		{
			Renderer[] renderers = m_Renderers;
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].sharedMaterial = energyGauge.m_LocalMat;
			}
			m_GuageMaterialOwner = energyGauge;
		}
	}

	private void UpdateGaugeDirectly()
	{
		UpdateGaugeDisplay();
	}

	private void UpdateGaugeDisplay()
	{
		int numLit;
		float blinkSpeed;
		if (m_UseMeterRenderingOverride)
		{
			TechEnergy.CalculateGauge(m_powerLevel, m_NumLeds, m_MeterRenderingOverrides, out numLit, out blinkSpeed);
		}
		else
		{
			TechEnergy.CalculateGauge(m_powerLevel, m_NumLeds, out numLit, out blinkSpeed);
		}
		if (numLit != m_NumLit)
		{
			m_LocalMat.SetFloat("_NumLit", numLit);
			m_NumLit = numLit;
		}
		if (blinkSpeed != m_BlinkSpeedActual)
		{
			m_LocalMat.SetFloat("_BlinkSpeed", blinkSpeed);
			m_BlinkSpeedActual = blinkSpeed;
		}
	}

	private static EnergyGauge GetGaugeResponsibleForTank(Tank tank)
	{
		EnergyGauge value = null;
		if ((object)s_CachedTank == tank)
		{
			value = s_CachedGauge;
		}
		else
		{
			s_TankToMaterialMappings.TryGetValue(tank, out value);
			s_CachedTank = tank;
			s_CachedGauge = value;
		}
		return value;
	}

	private static void AddGaugeResponsibleForTank(Tank tank, EnergyGauge gauge)
	{
		s_TankToMaterialMappings.Add(tank, gauge);
		s_CachedTank = tank;
		s_CachedGauge = gauge;
	}

	private static void RemoveGaugeResponsibleForTank(Tank tank)
	{
		s_TankToMaterialMappings.Remove(tank);
		if ((object)s_CachedTank == tank)
		{
			s_CachedTank = null;
			s_CachedGauge = null;
		}
	}

	private void OnPool()
	{
		m_Renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
		for (int i = 0; i < m_Renderers.Length; i++)
		{
			if (i == 0)
			{
				m_LocalMat = new Material(m_Renderers[0].sharedMaterial);
			}
			m_Renderers[i].sharedMaterial = m_LocalMat;
		}
	}

	private void OnSpawn()
	{
		m_powerLevel = 0f;
		m_NumLit = -1;
		m_BlinkSpeedActual = -1f;
		UpdateGaugeDisplay();
	}
}
