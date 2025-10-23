[NodePath("TerraTech/Actions/Blocks")]
public class uScript_GetBlockSpawnDataPositionName : uScriptLogic
{
	public bool Out => true;

	public void In([FriendlyName("Block Data", "The spawn data to retrieve the position from")] SpawnBlockData blockData, [FriendlyName("Position Name", "The name of the position inside the spawn data")] out string positionName)
	{
		positionName = blockData.m_PositionName;
	}
}
