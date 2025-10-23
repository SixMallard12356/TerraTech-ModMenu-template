using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerGauge : UIHUDElement
{
	[SerializeField]
	private Image[] m_Segments;

	private Tank m_Tank;

	private List<ModulePowerGauge> m_Modules = new List<ModulePowerGauge>();

	private bool m_ShowingFromUI;

	[SerializeField]
	private Color[] m_Colours = new Color[6]
	{
		Color.black,
		Color.red,
		Color.yellow,
		Color.yellow,
		Color.green,
		Color.green
	};

	public override void Show(object moduleObject)
	{
		base.Show(moduleObject);
		ModulePowerGauge modulePowerGauge = moduleObject as ModulePowerGauge;
		if ((bool)modulePowerGauge)
		{
			m_Modules.Add(modulePowerGauge);
			m_Tank = modulePowerGauge.block.tank;
		}
		else
		{
			m_ShowingFromUI = true;
			m_Tank = Singleton.playerTank;
		}
	}

	public override void Hide(object moduleObject)
	{
		ModulePowerGauge modulePowerGauge = moduleObject as ModulePowerGauge;
		if ((bool)modulePowerGauge)
		{
			m_Modules.Remove(modulePowerGauge);
		}
		if (!m_ShowingFromUI && m_Modules.Count == 0)
		{
			base.Hide(moduleObject);
		}
	}

	private void Update()
	{
		if (m_ShowingFromUI)
		{
			m_Tank = Singleton.playerTank;
		}
		if (!m_Tank)
		{
			return;
		}
		float num;
		float num2;
		if (ManNetwork.IsHost)
		{
			TechEnergy.EnergyState energyState = m_Tank.EnergyRegulator.Energy(TechEnergy.EnergyType.Electric);
			num = energyState.previousAmount;
			num2 = energyState.storageTotal;
		}
		else
		{
			num = 0f;
			num2 = 0f;
			BlockManager.BlockIterator<ModuleEnergyStore>.Enumerator enumerator = m_Tank.blockman.IterateBlockComponents<ModuleEnergyStore>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleEnergyStore current = enumerator.Current;
				num += current.CurrentAmount;
				num2 += current.m_Capacity;
			}
		}
		float powerLevel = ((num2 == 0f) ? 0f : (num / num2));
		int num3 = m_Segments.Length;
		TechEnergy.CalculateGauge(powerLevel, m_Segments.Length, out var numLit, out var blinkSpeed);
		for (int i = 0; i < numLit; i++)
		{
			m_Segments[i].color = m_Colours[numLit];
		}
		if (numLit > 0 && blinkSpeed != 0f)
		{
			float num4 = 1f - (0.5f + Mathf.Sin(Time.time * blinkSpeed) / 2f);
			Color color = m_Colours[numLit] * num4;
			color.a = 1f;
			m_Segments[numLit - 1].color = color;
		}
		for (int j = numLit; j < num3; j++)
		{
			m_Segments[j].color = Color.black;
		}
	}

	private void OnRecycle()
	{
		m_Modules.Clear();
		m_ShowingFromUI = false;
	}
}
