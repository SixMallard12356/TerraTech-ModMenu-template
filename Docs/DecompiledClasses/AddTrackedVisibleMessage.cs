using UnityEngine.Networking;

public class AddTrackedVisibleMessage : TrackedVisibleMessage
{
	public ObjectTypes m_ObjectType;

	public int m_BlockLimitCost;

	public bool m_IsTrackedUnsaved;

	public AddTrackedVisibleMessage()
	{
	}

	public AddTrackedVisibleMessage(TrackedVisible trackedVis, bool trackUnsaved = false)
		: base(trackedVis)
	{
		m_ObjectType = trackedVis.ObjectType;
		m_BlockLimitCost = Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(trackedVis.ID, includeHeldItems: true);
		m_IsTrackedUnsaved = trackUnsaved;
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		m_ObjectType = (ObjectTypes)reader.ReadByte();
		m_BlockLimitCost = (int)reader.ReadPackedUInt32();
		m_IsTrackedUnsaved = reader.ReadBoolean();
	}

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.Write((byte)m_ObjectType);
		writer.WritePackedUInt32((uint)m_BlockLimitCost);
		writer.Write(m_IsTrackedUnsaved);
	}
}
