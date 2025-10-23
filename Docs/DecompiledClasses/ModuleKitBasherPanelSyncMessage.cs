using UnityEngine.Networking;

public class ModuleKitBasherPanelSyncMessage : BlockMessage_Base
{
	private KitBashPanelSpawner.PanelSpawnData[] _panelSpawnData;

	public KitBashPanelSpawner.PanelSpawnData[] panelSpawnData
	{
		get
		{
			if (_panelSpawnData == null)
			{
				_panelSpawnData = new KitBashPanelSpawner.PanelSpawnData[0];
			}
			return _panelSpawnData;
		}
		set
		{
			_panelSpawnData = value;
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.WritePackedUInt32((uint)panelSpawnData.Length);
		for (int i = 0; i < panelSpawnData.Length; i++)
		{
			panelSpawnData[i].NetSerialize(writer);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		_panelSpawnData = new KitBashPanelSpawner.PanelSpawnData[reader.ReadPackedUInt32()];
		for (int i = 0; i < _panelSpawnData.Length; i++)
		{
			_panelSpawnData[i].NetDeserialize(reader);
		}
	}
}
