using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleFuelTank : Module
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float detachedFuelAmount;
	}

	[SerializeField]
	[FormerlySerializedAs("capacity")]
	public float m_Capacity = 10f;

	[FormerlySerializedAs("refillRate")]
	[SerializeField]
	public float m_RefillRate = 1f;

	[FormerlySerializedAs("ledMaterialColorName")]
	[SerializeField]
	public string m_LedMaterialColorName = "_MainColor";

	[SerializeField]
	[FormerlySerializedAs("ledMaterialFloatName")]
	public string m_LedMaterialFloatName = "_Blink";

	[SerializeField]
	public Gauge m_Gauge = new Gauge();

	[SerializeField]
	[HideInInspector]
	private Material[] m_MaterialsToUpdate;

	private float m_DetachedFuelAmount;

	private float m_PrevFuelLevel = -1f;

	private static List<Renderer> s_LEDRenderersWorking = new List<Renderer>();

	private List<Gauge.SegmentState> m_segmentStates = new List<Gauge.SegmentState>();

	public float Capacity => m_Capacity;

	public float RefillRate => m_RefillRate;

	private static int CompareRenderersByName(Renderer a, Renderer b)
	{
		return string.Compare(a.name, b.name);
	}

	private void UpdateMaterials()
	{
		float num = (base.block.tank ? base.block.tank.Boosters.FuelLevel : (m_DetachedFuelAmount / Capacity));
		if (num != m_PrevFuelLevel)
		{
			m_PrevFuelLevel = num;
			m_segmentStates.Clear();
			m_Gauge.CalculateSegmentStates(num, ref m_segmentStates);
			for (int i = 0; i < m_MaterialsToUpdate.Length; i++)
			{
				Material obj = m_MaterialsToUpdate[i];
				Gauge.SegmentState segmentState = m_segmentStates[i];
				obj.SetColor(m_LedMaterialColorName, segmentState.colour);
				obj.SetFloat(m_LedMaterialFloatName, segmentState.blink);
			}
		}
	}

	private void OnAttached()
	{
		base.block.tank.Boosters.AddFuelTank(this, m_DetachedFuelAmount);
		m_DetachedFuelAmount = 0f;
		m_PrevFuelLevel = -1f;
	}

	private void OnDetaching()
	{
		m_DetachedFuelAmount = base.block.tank.Boosters.FuelLevel * m_Capacity;
		base.block.tank.Boosters.RemoveFuelTank(this, m_DetachedFuelAmount);
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.detachedFuelAmount = m_DetachedFuelAmount;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_DetachedFuelAmount = serialData2.detachedFuelAmount;
			m_PrevFuelLevel = -1f;
		}
	}

	private void OnPool()
	{
		s_LEDRenderersWorking.Clear();
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.name.Contains("LED"))
			{
				s_LEDRenderersWorking.Add(renderer);
			}
		}
		s_LEDRenderersWorking.Sort(CompareRenderersByName);
		m_MaterialsToUpdate = new Material[s_LEDRenderersWorking.Count];
		for (int j = 0; j < m_MaterialsToUpdate.Length; j++)
		{
			m_MaterialsToUpdate[j] = s_LEDRenderersWorking[j].material;
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_DetachedFuelAmount = m_Capacity;
	}

	private void OnUpdate()
	{
		UpdateMaterials();
	}
}
