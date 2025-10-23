#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Prefab must be in the Resources\\Prefabs\\Scenery\\ResourceGivers Folder, or otherwise indexed in TerrainObjectTable")]
public class uScript_SpawnScenery : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject ownerNode, TerrainObject sceneryPrefab, string posName, string uniqueName, out Vector3[] spawnPositions, int spawnNum = 1, float maxSpawnRange = 0f, float minSpawnRange = 0f)
	{
		List<Vector3> list = new List<Vector3>();
		if (m_Encounter == null && ownerNode != null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (m_Encounter != null && sceneryPrefab != null)
		{
			Vector3 position = m_Encounter.GetPosition(posName);
			int num = spawnNum * 5;
			int num2 = 0;
			while (num2 < spawnNum && num-- > 0)
			{
				Vector3 scenePos = position;
				if (maxSpawnRange > 0f)
				{
					float f = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
					Vector2 vector = new Vector2(Mathf.Sin(f), Mathf.Cos(f)) * UnityEngine.Random.Range(minSpawnRange, maxSpawnRange);
					scenePos += new Vector3(vector.x, 0f, vector.y);
				}
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos);
				if (worldTile != null)
				{
					if (maxSpawnRange <= Mathf.Epsilon || !Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, scenePos, 0f))
					{
						scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
						Vector2 normalisedPosInTile = Singleton.Manager<ManWorld>.inst.TileManager.GetNormalisedPosInTile(in scenePos, worldTile.Coord);
						Vector2 rotationHV = new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
						Quaternion normalWeightedSpawnOrientation = sceneryPrefab.GetNormalWeightedSpawnOrientation(worldTile, normalisedPosInTile, rotationHV);
						TerrainObject.SpawnedTerrainObjectData spawnedTerrainObjectData = sceneryPrefab.SpawnFromPrefabAndAddToSaveData(scenePos, normalWeightedSpawnOrientation, trackObject: true);
						d.Assert(spawnedTerrainObjectData.TerrainObject == null || spawnedTerrainObjectData.TerrainObject.visible != null, "uScript_SpawnScenery - Spawned scenery '" + sceneryPrefab.name + "' Does not contain a Visible component??");
						string uniqueName2 = ((spawnNum == 1) ? uniqueName : $"{uniqueName}_{num2 + 1}");
						m_Encounter.AddTrackableObjectLookup(uniqueName2, spawnedTerrainObjectData.TrackedObjectRef);
						num2++;
						list.Add(scenePos);
					}
				}
				else
				{
					d.LogError(string.Concat("uScript_SpawnScenery - Trying to spawn scenery (", sceneryPrefab.name, ", ", uniqueName, ") but spawn pos (", scenePos, ") was not on a loaded Tile!"));
				}
			}
			d.Assert(num2 == spawnNum, "uScript_SpawnScenery - Failed to spawn the desired number of scenery - managed to spawn " + num2 + " out of " + spawnNum);
		}
		spawnPositions = list.ToArray();
	}

	public void OnDisable()
	{
		m_Encounter = null;
	}
}
