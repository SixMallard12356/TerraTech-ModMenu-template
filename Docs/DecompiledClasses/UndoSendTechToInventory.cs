#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class UndoSendTechToInventory : ManUndo.IUndoCommand
{
	[Serializable]
	public class Config
	{
		public float m_UndoTimeout;
	}

	private Config m_Config;

	private TechData m_TechData;

	private BlockCountList m_TechBlockCounts;

	private float m_Timestamp;

	private WorldPosition m_TechPos;

	private Quaternion m_TechRot;

	private bool m_TechAnchored;

	private bool m_Executing;

	private UIUndoButton.Context m_UIContext;

	public UndoTypes UndoType => UndoTypes.SendBlocksToInventory;

	public UIUndoButton.Context UIContext => m_UIContext;

	public UndoSendTechToInventory(Config config)
	{
		m_Config = config;
		m_UIContext = new UIUndoButton.Context
		{
			m_UndoTypes = UndoType
		};
	}

	public void Initialize(TechData techData, Tank tech)
	{
		Initialize(techData, tech.boundsCentreWorld, tech.trans.rotation, tech.IsAnchored);
	}

	public void Initialize(TechData techData, Vector3 scenePos, Quaternion rotation, bool isAnchored)
	{
		Reset();
		m_TechPos = WorldPosition.FromScenePosition(in scenePos);
		m_TechRot = rotation;
		m_TechAnchored = isAnchored;
		m_TechData = techData;
		m_TechBlockCounts = new BlockCountList(techData);
		m_UIContext.m_TechName = techData.Name;
	}

	public void ArmedStart()
	{
		m_Timestamp = Time.time;
	}

	public void ArmedRefresh()
	{
		m_Timestamp = Time.time;
	}

	public bool ArmedUpdateValid()
	{
		if (Time.time - m_Timestamp > m_Config.m_UndoTimeout)
		{
			return false;
		}
		InventoryMetaData referenceInventory = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory();
		if (referenceInventory.IsLocked)
		{
			return false;
		}
		if (!referenceInventory.IsUnlimited && referenceInventory.m_Inventory != null && !referenceInventory.m_Inventory.HasItemsToSpawnTech(m_TechBlockCounts))
		{
			return false;
		}
		return true;
	}

	public void ExecuteStart()
	{
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "No Multiplayer support for UndoSendTechToInventory");
		m_Executing = true;
		if (Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(m_TechPos.ScenePosition))
		{
			SpawnOntoLoadedTile();
		}
		else
		{
			SpawnIntoSaveData();
		}
	}

	private void OnRestoreTechComplete()
	{
		m_Executing = false;
	}

	public bool ExecuteUpdateValid()
	{
		return m_Executing;
	}

	public void Reset()
	{
		m_TechData = null;
	}

	public Tank GetBufferTech()
	{
		return null;
	}

	private void SpawnOntoLoadedTile()
	{
		ManTechSwapper.Operation newOperation = Singleton.Manager<ManTechSwapper>.inst.GetNewOperation();
		newOperation.InitSpawnTech(m_TechPos.ScenePosition, m_TechRot, m_TechData, Singleton.Manager<ManGameMode>.inst.GetReferenceInventory(), m_TechAnchored);
		newOperation.SubscribeToCompletionCallback(OnRestoreTechComplete);
	}

	private void SpawnIntoSaveData()
	{
		Vector3 scenePos = m_TechPos.ScenePosition;
		ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(in scenePos);
		if (storedTileIfNotSpawned != null)
		{
			int nextVisibleID = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(ObjectTypes.Vehicle);
			int num = 0;
			int[] array = new int[m_TechData.m_BlockSpecs.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(ObjectTypes.Block);
			}
			storedTileIfNotSpawned.AddSavedTech(m_TechData, scenePos, m_TechRot, nextVisibleID, num, array);
			RadarTypes radarType = ((!m_TechAnchored) ? RadarTypes.Vehicle : RadarTypes.Base);
			TrackedVisible trackedVisible = new TrackedVisible(nextVisibleID, null, ObjectTypes.Vehicle, radarType);
			trackedVisible.SetPos(scenePos);
			trackedVisible.TeamID = num;
			Singleton.Manager<ManVisible>.inst.TrackVisible(trackedVisible);
			InventoryMetaData referenceInventory = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory();
			if (referenceInventory.TakesAndStoresBlocks)
			{
				IInventory<BlockTypes> inventory = referenceInventory.m_Inventory;
				foreach (KeyValuePair<BlockTypes, int> techBlockCount in m_TechBlockCounts)
				{
					BlockTypes key = techBlockCount.Key;
					int value = techBlockCount.Value;
					inventory.HostConsumeItem(-1, key, value);
				}
			}
		}
		OnRestoreTechComplete();
	}
}
