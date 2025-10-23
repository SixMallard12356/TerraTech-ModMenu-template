#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleRadar : Module
{
	public enum RadarScanType
	{
		Techs = 1,
		Resources = 2,
		Terrain = 4
	}

	public class RadarScanTypeComparer : IEqualityComparer<RadarScanType>
	{
		public bool Equals(RadarScanType x, RadarScanType y)
		{
			return x == y;
		}

		public int GetHashCode(RadarScanType obj)
		{
			return (int)obj;
		}
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public ChunkTypes resType;

		public float scanRemainingSec;
	}

	[SerializeField]
	private int m_Priority;

	[SerializeField]
	private ManRadar.MiniMapType m_MiniMapType;

	[SerializeField]
	private FMODEvent m_RadarOnSfxEvent;

	[SerializeField]
	[EnumFlag]
	private RadarScanType m_ScanType = RadarScanType.Techs;

	[EnumArray(typeof(RadarScanType))]
	[SerializeField]
	private float[] m_Ranges;

	private Spinner[] m_Dishes = new Spinner[0];

	private ChunkTypes m_ResourceTypeToScan = ChunkTypes.Null;

	private ModuleItemConsume m_ItemConsumer;

	private WorldObjectOverlay m_ResourceOverlay;

	private ManTimedEvents.ManagedEvent m_ScanDurationEndEvent;

	public ManRadar.MiniMapType MiniMapType => m_MiniMapType;

	public FMODEvent RadarOnSFXEvent => m_RadarOnSfxEvent;

	public RadarScanType ScanType => m_ScanType;

	public bool ScansTech => (m_ScanType & RadarScanType.Techs) != 0;

	public bool ScansResources => (m_ScanType & RadarScanType.Resources) != 0;

	public bool ScansTerrain => (m_ScanType & RadarScanType.Terrain) != 0;

	public ChunkTypes ResourceType => m_ResourceTypeToScan;

	private int GetScanIndex(RadarScanType type)
	{
		return EnumValuesIterator<RadarScanType>.IndexOfFlag(type);
	}

	private void SetResourceToScanFor(ChunkTypes chunkType, float scanDuration = float.MinValue)
	{
		bool flag = chunkType != m_ResourceTypeToScan;
		if (!flag && !(scanDuration > 0f))
		{
			return;
		}
		m_ResourceTypeToScan = chunkType;
		if (m_ResourceTypeToScan != ChunkTypes.Null)
		{
			if (m_ResourceOverlay == null)
			{
				m_ResourceOverlay = Singleton.Manager<ManOverlay>.inst.AddRadarOverlay(this);
			}
			else
			{
				m_ResourceOverlay.RefreshPanel();
			}
			m_ScanDurationEndEvent.Clear();
			m_ScanDurationEndEvent.Set(scanDuration);
		}
		else
		{
			if (m_ResourceOverlay != null)
			{
				Singleton.Manager<ManOverlay>.inst.RemoveObjectOverlay(m_ResourceOverlay);
				m_ResourceOverlay = null;
			}
			m_ScanDurationEndEvent.Clear();
		}
		if (flag)
		{
			base.block.tank.Radar.MarkMappedChunkTypesDirty();
		}
	}

	public float GetRange(RadarScanType type)
	{
		int scanIndex = GetScanIndex(type);
		if (scanIndex >= 0 && scanIndex < m_Ranges.Length)
		{
			return m_Ranges[scanIndex];
		}
		return 0f;
	}

	private void OnAttached()
	{
		Spinner[] dishes = m_Dishes;
		for (int i = 0; i < dishes.Length; i++)
		{
			dishes[i].SetAutoSpin(enableAutoSpin: true);
		}
		base.block.tank.Radar.AddRadar(this);
		base.block.tank.HUDControl.AddHudElement(this, ManHUD.HUDElementType.Radar);
	}

	private void OnDetaching()
	{
		Spinner[] dishes = m_Dishes;
		for (int i = 0; i < dishes.Length; i++)
		{
			dishes[i].SetAutoSpin(enableAutoSpin: false);
		}
		base.block.tank.Radar.RemoveRadar(this);
		base.block.tank.HUDControl.RemoveHudElement(this);
		if (ScansResources)
		{
			SetResourceToScanFor(ChunkTypes.Null);
		}
	}

	private void OnAnimationTriggerFlagsUpdated(ModuleItemConsume.AnimationTriggers triggerFlags)
	{
		if ((triggerFlags & ModuleItemConsume.AnimationTriggers.StartOperatingTrigger) != 0)
		{
			if (m_ItemConsumer.Recipe != null)
			{
				d.Assert(m_ItemConsumer.Recipe.m_InputItems.Length == 1, "Invalid recipe started in ModuleRadar " + base.name + " - Expected exactly one input!");
				d.Assert(m_ItemConsumer.Recipe.m_InputItems[0].m_Item.ObjectType == ObjectTypes.Chunk, "Invalid recipe started in ModuleRadar " + base.name + " - Expected Chunk type input!");
				ChunkTypes itemType = (ChunkTypes)m_ItemConsumer.Recipe.m_InputItems[0].m_Item.ItemType;
				ChunkTypes rawResource = Singleton.Manager<ResourceManager>.inst.GetRawResource(itemType);
				SetResourceToScanFor(rawResource, m_ItemConsumer.Recipe.m_BuildTimeSeconds * m_ItemConsumer.GetDurationMultiplierAgnostic());
			}
			else
			{
				d.LogWarning("Recipe was null when handling OnAnimationTriggerFlagsUpdated - this seems to sometimes happen when loading a save. Is ModuleItemConsume calling this event under invalid circumstances?", this);
			}
		}
	}

	private void OnScanDurationEnd()
	{
		SetResourceToScanFor(ChunkTypes.Null);
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (!ScansResources)
		{
			return;
		}
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.resType = m_ResourceTypeToScan;
			serialData.scanRemainingSec = m_ScanDurationEndEvent.TimeRemaining;
			serialData.Store(blockSpec.saveState);
		}
		else
		{
			SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
			if (serialData2 != null)
			{
				SetResourceToScanFor(serialData2.resType, serialData2.scanRemainingSec);
			}
		}
	}

	private void OnValidate()
	{
		if (m_Ranges == null)
		{
			m_Ranges = new float[EnumIterator<RadarScanType>.Count];
		}
		for (int i = 0; i < EnumIterator<RadarScanType>.Count; i++)
		{
			bool num = ((uint)m_ScanType & (uint)(1 << i)) != 0;
			bool flag = m_Ranges[i] > 0f;
			d.Assert(num == flag, $"ModuleRadar on {base.name} has a missmatch between scantypes and scanranges provided: {(RadarScanType)(1 << i)} does not have both set! This radar will not scan on the channel!", base.gameObject);
		}
	}

	private void OnPool()
	{
		m_Dishes = GetComponentsInChildren<Spinner>(includeInactive: true);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_ItemConsumer = GetComponent<ModuleItemConsume>();
		if (m_ItemConsumer != null)
		{
			base.block.serializeEvent.Subscribe(OnSerialize);
			m_ItemConsumer.AnimationTriggerFlagsChangedEvent.Subscribe(OnAnimationTriggerFlagsUpdated);
			m_ScanDurationEndEvent = new ManTimedEvents.ManagedEvent(OnScanDurationEnd);
		}
	}

	private void OnSpawn()
	{
		Spinner[] dishes = m_Dishes;
		for (int i = 0; i < dishes.Length; i++)
		{
			dishes[i].SetAutoSpin(enableAutoSpin: false);
		}
	}
}
