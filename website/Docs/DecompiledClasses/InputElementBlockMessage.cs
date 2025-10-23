using Rewired;
using UnityEngine;
using UnityEngine.Networking;

public class InputElementBlockMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public ModuleCircuit_Input_KeyBind_FromInput.InputElement value;

	uint IBlockMessage.BlockPoolID
	{
		get
		{
			return m_BlockPoolID;
		}
		set
		{
			m_BlockPoolID = value;
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_BlockPoolID);
		writer.Write((byte)value.controllerType);
		writer.Write((byte)value.elementType);
		writer.Write((byte)value.elementAxisPole);
		writer.Write((short)value.keyboardKey);
		writer.WritePackedInt32(value.elementIdentifierId);
		writer.WritePackedInt32(value.elementIndex);
		writer.Write(value.hardwareTypeGuidString);
		writer.Write(value.elementIdentifierName);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadUInt32();
		value = new ModuleCircuit_Input_KeyBind_FromInput.InputElement
		{
			controllerType = (ControllerType)reader.ReadByte(),
			elementType = (ControllerElementType)reader.ReadByte(),
			elementAxisPole = (Pole)reader.ReadByte(),
			keyboardKey = (KeyCode)reader.ReadInt16(),
			elementIdentifierId = reader.ReadPackedInt32(),
			elementIndex = reader.ReadPackedInt32(),
			hardwareTypeGuidString = reader.ReadString(),
			elementIdentifierName = reader.ReadString()
		};
	}
}
