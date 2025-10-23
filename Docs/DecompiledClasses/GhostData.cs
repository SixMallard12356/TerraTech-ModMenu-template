#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GhostData
{
	public struct Data
	{
		public bool m_SnapToPos;

		public V3Serial m_Position;

		public QuatSerial m_Rotation;

		public bool m_UpdateBlockSpecs;

		public TankPreset.BlockSpec[] m_BlockSpecs;

		public float m_TimeStamp;

		public Data(bool snapToPos, Vector3 position, Quaternion rotation, bool updateBlockSpecs, TankPreset.BlockSpec[] blockSpec, float time)
		{
			m_SnapToPos = snapToPos;
			m_Position = position;
			m_Rotation = rotation;
			m_UpdateBlockSpecs = updateBlockSpecs;
			m_BlockSpecs = blockSpec;
			m_TimeStamp = time;
		}
	}

	[JsonProperty]
	private List<Data> m_GhostData = new List<Data>();

	[JsonProperty]
	private V3Serial m_InitialPosition;

	[JsonProperty]
	private QuatSerial m_InitialRotation;

	[JsonProperty]
	private TankPreset.BlockSpec[] m_CurrentBlockSpecs;

	[NonSerialized]
	private Tank m_ReplayTech;

	[NonSerialized]
	private bool m_Ghosted;

	[NonSerialized]
	private string m_TechName = "GhostReplay";

	[NonSerialized]
	private TankDescriptionData m_OverlayData;

	[NonSerialized]
	private TechData m_MostRecentlyUpdatedTechData;

	public Tank ReplayTech()
	{
		return m_ReplayTech;
	}

	public void AddGhostData(Tank tech, float timeStamp, bool snapToPos, bool checkBlockSpecsChanged)
	{
		Vector3 position = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		TechData techData = null;
		bool flag = m_GhostData.Count == 0;
		if ((bool)tech)
		{
			if (checkBlockSpecsChanged || flag)
			{
				techData = new TechData();
				techData.SaveTech(tech, saveRuntimeState: true);
			}
			if (flag)
			{
				m_InitialPosition = tech.trans.position;
				m_InitialRotation = tech.trans.rotation;
				m_CurrentBlockSpecs = techData.m_BlockSpecs.ToArray();
				m_MostRecentlyUpdatedTechData = techData;
			}
			position = tech.trans.position;
			rotation = tech.trans.rotation;
		}
		else if (m_GhostData.Count > 0)
		{
			position = m_GhostData[m_GhostData.Count - 1].m_Position;
			rotation = m_GhostData[m_GhostData.Count - 1].m_Rotation;
		}
		TankPreset.BlockSpec[] array = techData?.m_BlockSpecs.ToArray();
		bool flag2 = checkBlockSpecsChanged && (m_MostRecentlyUpdatedTechData == null || !TankPreset.CheckBlockSpecListsMatch(array, m_MostRecentlyUpdatedTechData.m_BlockSpecs.ToArray()));
		if (flag2 || flag)
		{
			m_MostRecentlyUpdatedTechData = techData;
		}
		else
		{
			array = null;
		}
		Data item = new Data(snapToPos, position, rotation, flag2, array, timeStamp);
		m_GhostData.Add(item);
	}

	public Tank StartReplay(bool ghostEffect)
	{
		m_Ghosted = ghostEffect;
		m_ReplayTech = SpawnOrUpdateTech(m_InitialPosition, m_InitialRotation, m_CurrentBlockSpecs);
		Singleton.Manager<ManWorld>.inst.TileManager.RemoveTileCache(m_ReplayTech.visible);
		m_ReplayTech.AI.enabled = false;
		return m_ReplayTech;
	}

	public bool ReplayData(float timeStamp)
	{
		bool flag = true;
		for (int i = 0; i < m_GhostData.Count; i++)
		{
			if (m_GhostData[i].m_TimeStamp > timeStamp)
			{
				flag = false;
				float num = 0f;
				bool flag2 = false;
				TankPreset.BlockSpec[] array = null;
				float timeStamp2 = m_GhostData[i].m_TimeStamp;
				Vector3 b = m_GhostData[i].m_Position;
				Quaternion b2 = m_GhostData[i].m_Rotation;
				bool num2 = !m_GhostData[i].m_SnapToPos;
				Vector3 vector;
				Quaternion quaternion;
				if (i > 0)
				{
					num = m_GhostData[i - 1].m_TimeStamp;
					vector = m_GhostData[i - 1].m_Position;
					quaternion = m_GhostData[i - 1].m_Rotation;
					flag2 = m_GhostData[i - 1].m_UpdateBlockSpecs;
					array = m_GhostData[i - 1].m_BlockSpecs;
				}
				else
				{
					num = 0f;
					vector = m_InitialPosition;
					quaternion = m_InitialRotation;
				}
				Vector3 vector2 = vector;
				Quaternion rotation = quaternion;
				if (num2)
				{
					float t = (timeStamp - num) / (timeStamp2 - num);
					vector2 = Vector3.Lerp(vector, b, t);
					rotation = Quaternion.Slerp(quaternion, b2, t);
				}
				if (flag2 && array != null && array != null && !TankPreset.CheckBlockSpecListsMatch(m_CurrentBlockSpecs, array))
				{
					m_ReplayTech = SpawnOrUpdateTech(vector2, rotation, array);
				}
				m_ReplayTech.visible.trans.position = vector2;
				m_ReplayTech.visible.trans.rotation = rotation;
				break;
			}
		}
		if (flag & (m_GhostData.Count > 0))
		{
			m_ReplayTech.visible.trans.position = m_GhostData[m_GhostData.Count - 1].m_Position;
			m_ReplayTech.visible.trans.rotation = m_GhostData[m_GhostData.Count - 1].m_Rotation;
		}
		return flag;
	}

	public void RevertAndRecycleTech()
	{
		if ((bool)m_ReplayTech)
		{
			RevertTankBlocks();
			m_ReplayTech.EnableGravity = false;
			m_ReplayTech.rbody.isKinematic = false;
			m_ReplayTech.AI.enabled = true;
			m_ReplayTech.visible.RemoveFromGame();
			m_ReplayTech = null;
		}
		else
		{
			d.LogError("GhostData.RevertAndRecycleTech - m_ReplayTech is null");
		}
	}

	public void SetTechName(string name)
	{
		m_TechName = name;
		if ((bool)m_ReplayTech)
		{
			m_ReplayTech.name = name;
		}
	}

	public void SetTechOverlayType(TankDescriptionData data)
	{
		m_OverlayData = data;
		if ((bool)m_ReplayTech)
		{
			m_ReplayTech.SetOverlayType(data);
		}
	}

	private Tank SpawnOrUpdateTech(Vector3 pos, Quaternion rotation, TankPreset.BlockSpec[] blockSpec)
	{
		TechData techData = new TechData();
		techData.Name = m_TechName;
		techData.m_BlockSpecs = new List<TankPreset.BlockSpec>();
		if (blockSpec != null)
		{
			techData.m_BlockSpecs.AddRange(blockSpec);
		}
		else
		{
			d.LogError("SpawnOrUpdateTech - Blockspec passed in was null. Is this an error? (Nullcheck was originally removed in 95f0f3fc885cb063ff136f07779897e30379745f - restored due to crash from null blockSpec on 7/6/18.");
		}
		m_CurrentBlockSpecs = blockSpec;
		if (m_ReplayTech == null)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = techData,
				teamID = -2,
				position = pos,
				rotation = rotation,
				grounded = false
			};
			m_ReplayTech = Singleton.Manager<ManSpawn>.inst.SpawnUnmanagedTank(param);
			if (m_OverlayData != null)
			{
				m_ReplayTech.SetOverlayType(m_OverlayData);
			}
		}
		else
		{
			RevertTankBlocks();
			uint[] techDataBlockPoolIDs = new uint[techData.m_BlockSpecs.Count];
			Singleton.Manager<ManSpawn>.inst.UpdateTankBlocks(ref m_ReplayTech, techData, techDataBlockPoolIDs, null);
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_ReplayTech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (m_Ghosted)
			{
				current.SetCustomMaterialOverride(ManTechMaterialSwap.MatType.Alpha);
			}
			current.visible.EnablePhysics(enable: false);
		}
		m_ReplayTech.EnableGravity = false;
		m_ReplayTech.rbody.isKinematic = true;
		return m_ReplayTech;
	}

	private void RevertTankBlocks()
	{
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_ReplayTech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (m_Ghosted)
			{
				current.RevertCustomMaterialOverride();
			}
			current.visible.EnablePhysics(enable: true);
		}
	}
}
