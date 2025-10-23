using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class ChallengeData : ScriptableObject
{
	[Serializable]
	public struct SafeAreaData
	{
		public V3Serial position;

		public WorldPosition worldPosition;

		public float radius;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (worldPosition == default(WorldPosition) && position != Vector3.zero)
			{
				worldPosition = WorldPosition.FromGameWorldPosition(position + Singleton.Manager<ManWorld>.inst.GameWorldToScene);
				position = Vector3.zero;
			}
		}
	}

	[SerializeField]
	private InventoryAsset m_InventoryData;

	public abstract void SpawnChallengeSceneryBlockers(Vector3 scenePosition, Quaternion sceneRotation);

	public abstract void RecycleChallengeSceneryBlockers();

	public abstract Challenge CreateChallenge(Challenge.PlacementInfo placeInfo);

	public void BuildInventory(IInventory<BlockTypes> inventory)
	{
		if (m_InventoryData != null)
		{
			m_InventoryData.BuildInventory(inventory);
		}
	}

	public abstract bool IsValidPlacementForEncounter(Vector3 scenePosition, Quaternion rotation, EncounterToSpawn encounterToSpawn);

	public abstract void GetSafeAreaList(Vector3 scenePosition, Quaternion rotation, List<SafeAreaData> safeAreaList);
}
