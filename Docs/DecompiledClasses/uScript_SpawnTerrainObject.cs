#define UNITY_EDITOR
using UnityEngine;

[NodeToolTip("Prefab must be in the Resources\\Prefabs\\Scenery\\ResourceGivers Folder, or otherwise indexed in TerrainObjectTable")]
public class uScript_SpawnTerrainObject : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject ownerNode, TerrainObject terrainObjectPrefab, string posName, string uniqueName)
	{
		if (m_Encounter == null && ownerNode != null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (!(m_Encounter != null) || !(terrainObjectPrefab != null))
		{
			return;
		}
		Vector3 scenePos = m_Encounter.GetPosition(posName);
		Quaternion rotation = m_Encounter.GetRotation(posName);
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos);
		if (worldTile != null)
		{
			scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
			Vector2 normalisedPosInTile = Singleton.Manager<ManWorld>.inst.TileManager.GetNormalisedPosInTile(in scenePos, worldTile.Coord);
			Vector2 rotationHV = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
			Quaternion normalWeightedSpawnOrientation = terrainObjectPrefab.GetNormalWeightedSpawnOrientation(worldTile, normalisedPosInTile, rotationHV);
			normalWeightedSpawnOrientation = rotation * normalWeightedSpawnOrientation;
			bool flag = !uniqueName.NullOrEmpty();
			TerrainObject.SpawnedTerrainObjectData spawnedTerrainObjectData = terrainObjectPrefab.SpawnFromPrefabAndAddToSaveData(scenePos, normalWeightedSpawnOrientation, flag);
			if (flag)
			{
				m_Encounter.AddTrackableObjectLookup(uniqueName, spawnedTerrainObjectData.TrackedObjectRef);
			}
		}
		else
		{
			d.LogError(string.Concat("uScript_SpawnTerrainObject - Trying to spawn TerrainObject (", terrainObjectPrefab.name, ", ", uniqueName, ") but spawn pos (", scenePos, ") was not on a loaded Tile!"));
		}
	}

	public void OnDisable()
	{
		m_Encounter = null;
	}
}
