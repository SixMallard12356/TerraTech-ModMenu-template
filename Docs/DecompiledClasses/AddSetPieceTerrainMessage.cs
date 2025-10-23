using UnityEngine.Networking;

public class AddSetPieceTerrainMessage : MessageBase
{
	public ManWorld.SavedSetPiece m_SetPiece;

	public override void Deserialize(NetworkReader reader)
	{
		m_SetPiece.m_Name = reader.ReadString();
		m_SetPiece.m_WorldPosition = reader.ReadWorldPosition();
		m_SetPiece.m_BaseHeight = reader.ReadSingle();
		m_SetPiece.m_Rotation = reader.ReadPackedInt32();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_SetPiece.m_Name);
		writer.Write(m_SetPiece.m_WorldPosition);
		writer.Write(m_SetPiece.m_BaseHeight);
		writer.WritePackedInt32(m_SetPiece.m_Rotation);
	}
}
