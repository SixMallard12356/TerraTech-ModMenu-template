#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Blocks")]
public class uScript_SpawnBlocksFromData : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In([FriendlyName("Block Data", "The data to spawn from")] SpawnBlockData[] blockData, [FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (blockData == null || blockData.Length == 0)
			{
				return;
			}
			for (int i = 0; i < blockData.Length; i++)
			{
				Vector3 scenePos = m_Encounter.GetPosition(blockData[i].m_PositionName);
				Quaternion rotation = m_Encounter.GetRotation(blockData[i].m_PositionName);
				ManSpawn.BlockSpawnParams blockSpawnParams = new ManSpawn.BlockSpawnParams
				{
					m_BlockType = blockData[i].m_BlockType,
					m_Rotation = rotation,
					m_SpawnVisualType = blockData[i].m_SpawnVisualType,
					m_SpawnVisualCustomEffectType = blockData[i].m_customSpawnEffectType
				};
				if (blockData[i].m_SpawnInFreeSpace)
				{
					float circleRadius = 1f;
					ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
					{
						m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
						m_CircleRadius = circleRadius,
						m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
						m_CircleIndex = 0,
						m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
						m_CheckSafeArea = false,
						m_RejectFunc = null
					};
					m_Encounter.SpawnObject(blockSpawnParams, freeSpaceParams, blockData[i].m_UniqueName);
				}
				else
				{
					m_Encounter.SpawnBlock(blockSpawnParams, scenePos, blockData[i].m_UniqueName);
				}
			}
		}
		else
		{
			string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
			d.LogError("ERROR: uScript_SpawnBlocks - " + text);
		}
	}
}
