using UnityEngine.Networking;

public class OnScreenMessageNetworkMessage : MessageBase
{
	public int m_ID;

	public ManOnScreenMessages.OnScreenMessage m_Message;

	public WorldPosition m_Position;

	public float m_Radius;

	public OnScreenMessageNetworkMessage()
	{
	}

	public OnScreenMessageNetworkMessage(Encounter encounter, ManOnScreenMessages.OnScreenMessage message)
	{
		m_Position = WorldPosition.FromScenePosition(encounter.transform.position);
		if (encounter.MessageMode == Encounter.MultiplayerMessageMode.AllPlayersInEncounterRadius)
		{
			m_Radius = encounter.EncounterRadius;
		}
		else
		{
			m_Radius = float.MaxValue;
		}
		m_Message = message;
	}

	private void SerializeGlyphInfo(NetworkWriter writer, ref Localisation.GlyphInfo[] glyphInfo)
	{
		writer.WritePackedInt32((glyphInfo != null) ? glyphInfo.Length : 0);
		for (int i = 0; i < glyphInfo.Length; i++)
		{
			writer.WritePackedInt32((int)glyphInfo[i].m_GlyphType);
			writer.WritePackedInt32(glyphInfo[i].m_RewiredAction);
		}
	}

	private void DeserializeGlyphInfo(NetworkReader reader, ref Localisation.GlyphInfo[] glyphInfo)
	{
		int num = reader.ReadPackedInt32();
		if (num > 0)
		{
			glyphInfo = new Localisation.GlyphInfo[num];
			for (int i = 0; i < glyphInfo.Length; i++)
			{
				glyphInfo[i] = new Localisation.GlyphInfo();
				glyphInfo[i].m_GlyphType = (JoystickGlyphIndex.GlyphType)reader.ReadPackedInt32();
				glyphInfo[i].m_RewiredAction = reader.ReadPackedInt32();
			}
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_ID);
		writer.WritePackedInt32(m_Message.m_Message.Length);
		for (int i = 0; i < m_Message.m_Message.Length; i++)
		{
			bool flag = !m_Message.m_Message[i].m_MessageID.NullOrEmpty();
			writer.Write(flag);
			if (flag)
			{
				writer.Write(m_Message.m_Message[i].m_MessageBank);
				writer.Write(m_Message.m_Message[i].m_MessageID);
			}
			else
			{
				writer.Write(m_Message.m_Message[i].m_RawText);
			}
			SerializeGlyphInfo(writer, ref m_Message.m_Message[i].m_InlineGlyphs);
		}
		writer.WritePackedInt32((int)m_Message.m_Speaker);
		writer.WritePackedInt32((int)m_Message.m_Side);
		writer.WritePackedInt32((int)m_Message.m_Priority);
		writer.Write(m_Message.m_Hold);
		writer.Write(m_Message.m_Tag);
		writer.Write(m_Position);
		writer.Write(m_Radius);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Message = new ManOnScreenMessages.OnScreenMessage();
		m_ID = reader.ReadInt32();
		int num = reader.ReadPackedInt32();
		m_Message.m_Message = new ManOnScreenMessages.OnScreenMessageLine[num];
		for (int i = 0; i < num; i++)
		{
			if (reader.ReadBoolean())
			{
				m_Message.m_Message[i].m_MessageBank = reader.ReadString();
				m_Message.m_Message[i].m_MessageID = reader.ReadString();
			}
			else
			{
				m_Message.m_Message[i].m_RawText = reader.ReadString();
			}
			DeserializeGlyphInfo(reader, ref m_Message.m_Message[i].m_InlineGlyphs);
		}
		m_Message.m_Speaker = (ManOnScreenMessages.Speaker)reader.ReadPackedInt32();
		m_Message.m_Side = (ManOnScreenMessages.Side)reader.ReadPackedInt32();
		m_Message.m_Priority = (ManOnScreenMessages.MessagePriority)reader.ReadPackedInt32();
		m_Message.m_Hold = reader.ReadBoolean();
		m_Message.m_Tag = reader.ReadString();
		m_Position = reader.ReadWorldPosition();
		m_Radius = reader.ReadSingle();
	}
}
