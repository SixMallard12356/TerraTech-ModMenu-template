using UnityEngine.Networking;

public class ConveyorOrientationMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public bool OutConveyorIsReciprocating;

	public uint FromBlockPoolID;

	public uint ToBlockPoolID;

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

	public ModuleItemConveyor InConveyor
	{
		get
		{
			return GetFromBlockPoolID(FromBlockPoolID);
		}
		set
		{
			FromBlockPoolID = SetFromConveyor(value);
		}
	}

	public ModuleItemConveyor OutConveyor
	{
		get
		{
			return GetFromBlockPoolID(ToBlockPoolID);
		}
		set
		{
			ToBlockPoolID = SetFromConveyor(value);
		}
	}

	private uint SetFromConveyor(ModuleItemConveyor conveyor)
	{
		if (conveyor == null)
		{
			return uint.MaxValue;
		}
		return conveyor.block.blockPoolID;
	}

	private ModuleItemConveyor GetFromBlockPoolID(uint blockPoolID)
	{
		if (blockPoolID == uint.MaxValue)
		{
			return null;
		}
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(blockPoolID);
		if (tankBlock == null)
		{
			return null;
		}
		return tankBlock.GetComponent<ModuleItemConveyor>();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write(OutConveyorIsReciprocating);
		writer.WritePackedUInt32(FromBlockPoolID);
		writer.WritePackedUInt32(ToBlockPoolID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		OutConveyorIsReciprocating = reader.ReadBoolean();
		FromBlockPoolID = reader.ReadPackedUInt32();
		ToBlockPoolID = reader.ReadPackedUInt32();
	}
}
