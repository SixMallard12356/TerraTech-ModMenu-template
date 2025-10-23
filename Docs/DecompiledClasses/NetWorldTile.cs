#define UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorldTile : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private const byte k_EmptyCell = 0;

	private const byte k_DamageStateOffset = 1;

	private const byte k_DeadCell = byte.MaxValue;

	private byte[,] m_ResourceDamageStatesByGridCoord;

	private ulong m_DirtyRowFlags;

	private ulong m_DirtyColFlags;

	private IntVector2 m_TileCoord;

	private const uint kSer_Coords = 1u;

	private const uint kSer_ResourceDispensers = 2u;

	private const uint kSer_AllFlagMask = uint.MaxValue;

	private NetWorldTile()
	{
		if (Singleton.Manager<ManWorld>.inst != null)
		{
			m_ResourceDamageStatesByGridCoord = new byte[Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge];
		}
	}

	public void InitServer(WorldTile tile)
	{
		m_TileCoord = tile.Coord;
		foreach (KeyValuePair<int, Visible> item in tile.Visibles[3])
		{
			ResourceDispenser resdisp = item.Value.resdisp;
			if (!resdisp.WasPlacedManually)
			{
				byte b = ((!resdisp.IsDeactivated) ? ((byte)(resdisp.CurrentDamageState + 1)) : byte.MaxValue);
				m_ResourceDamageStatesByGridCoord[item.Value.resdisp.cellCoord.x, item.Value.resdisp.cellCoord.y] = b;
			}
		}
		Singleton.Manager<ManWorld>.inst.TileManager.AddNetTile(m_TileCoord, this);
	}

	[Server]
	public void OnServerSetResourceDamageState(IntVector2 cellCoord, int damageState)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetWorldTile::OnServerSetResourceDamageState(IntVector2,System.Int32)' called on client");
			return;
		}
		d.Assert(cellCoord != TerrainObject.kTCC_Invalid && cellCoord != TerrainObject.kTCC_ManualSaved);
		if (cellCoord != TerrainObject.kTCC_Invalid && cellCoord != TerrainObject.kTCC_ManualSaved)
		{
			m_ResourceDamageStatesByGridCoord[cellCoord.x, cellCoord.y] = (byte)(damageState + 1);
			m_DirtyColFlags |= (ulong)(1L << cellCoord.x);
			m_DirtyRowFlags |= (ulong)(1L << cellCoord.y);
			SetDirtyBit(2u);
		}
	}

	[Server]
	public void OnServerSetResourceDispenserDead(IntVector2 cellCoord)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetWorldTile::OnServerSetResourceDispenserDead(IntVector2)' called on client");
			return;
		}
		d.Assert(cellCoord != TerrainObject.kTCC_Invalid && cellCoord != TerrainObject.kTCC_ManualSaved);
		if (cellCoord != TerrainObject.kTCC_Invalid && cellCoord != TerrainObject.kTCC_ManualSaved)
		{
			m_ResourceDamageStatesByGridCoord[cellCoord.x, cellCoord.y] = byte.MaxValue;
			m_DirtyColFlags |= (ulong)(1L << cellCoord.x);
			m_DirtyRowFlags |= (ulong)(1L << cellCoord.y);
			SetDirtyBit(2u);
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? uint.MaxValue : base.syncVarDirtyBits);
		ulong num2 = (initialState ? ulong.MaxValue : m_DirtyRowFlags);
		ulong num3 = (initialState ? ulong.MaxValue : m_DirtyColFlags);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(m_TileCoord.x);
			writer.Write(m_TileCoord.y);
		}
		if ((num & 2) != 0)
		{
			writer.Write(num2);
			writer.Write(num3);
			for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; i++)
			{
				for (int j = 0; j < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; j++)
				{
					if ((num2 & (ulong)(1L << i)) != 0L && (num3 & (ulong)(1L << j)) != 0L)
					{
						writer.Write(m_ResourceDamageStatesByGridCoord[j, i]);
					}
				}
			}
		}
		if (!initialState)
		{
			m_DirtyRowFlags = 0uL;
			m_DirtyColFlags = 0uL;
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			int x = reader.ReadInt32();
			int y = reader.ReadInt32();
			if (initialState)
			{
				m_TileCoord = new IntVector2(x, y);
				Singleton.Manager<ManWorld>.inst.TileManager.AddNetTile(m_TileCoord, this);
			}
			else
			{
				d.LogError("Cannot change tile coords of a NetWorldTile after initial setup");
			}
		}
		if ((num & 2) == 0)
		{
			return;
		}
		ulong num2 = reader.ReadUInt64();
		ulong num3 = reader.ReadUInt64();
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in m_TileCoord);
		for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; i++)
		{
			for (int j = 0; j < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; j++)
			{
				if ((num2 & (ulong)(1L << i)) == 0L || (num3 & (ulong)(1L << j)) == 0L)
				{
					continue;
				}
				byte b = reader.ReadByte();
				m_ResourceDamageStatesByGridCoord[j, i] = b;
				if (!initialState && worldTile != null && worldTile.IsPopulated && b != 0)
				{
					ResourceDispenser resdisp = worldTile.GetResdisp(new IntVector2(j, i));
					if (resdisp != null)
					{
						ApplyDamageState(resdisp, b);
						continue;
					}
					d.LogErrorFormat("Failed to find dispenser on world tile [{0},{1}], cell [{2},{3}]", worldTile.Coord.x, worldTile.Coord.y, j, i);
				}
			}
		}
		if (initialState && worldTile != null && worldTile.IsPopulated)
		{
			ApplyToTile(worldTile);
		}
	}

	public void ApplyToTile(WorldTile tile)
	{
		d.Assert(tile.Coord == m_TileCoord);
		foreach (KeyValuePair<int, Visible> item in tile.Visibles[3])
		{
			ResourceDispenser resdisp = item.Value.resdisp;
			if (!resdisp.IsNotNull() || resdisp.WasPlacedManually)
			{
				continue;
			}
			IntVector2 cellCoord = resdisp.cellCoord;
			byte b = m_ResourceDamageStatesByGridCoord[cellCoord.x, cellCoord.y];
			if (b == 0)
			{
				if (resdisp.gameObject.activeInHierarchy)
				{
					d.Log($"[NetWorldTile] Killing scenery {resdisp.name} at tile:{tile.Coord} (cell:{cellCoord}) as host doesn't specify its state");
					resdisp.OnClientDeath();
				}
			}
			else
			{
				ApplyDamageState(resdisp, b);
			}
		}
	}

	private void ApplyDamageState(ResourceDispenser dispenser, byte damageState)
	{
		if (damageState == byte.MaxValue)
		{
			if (dispenser.gameObject.activeInHierarchy)
			{
				dispenser.OnClientDeath();
			}
		}
		else if (dispenser.gameObject.activeInHierarchy)
		{
			dispenser.OnClientUpdateDamageState(damageState - 1);
		}
		else
		{
			dispenser.OnClientRegrow();
		}
	}

	private void OnSpawn()
	{
		for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; i++)
		{
			for (int j = 0; j < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; j++)
			{
				m_ResourceDamageStatesByGridCoord[j, i] = 0;
			}
		}
		m_DirtyRowFlags = 0uL;
		m_DirtyColFlags = 0uL;
		m_TileCoord = new IntVector2(int.MaxValue, int.MaxValue);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManWorld>.inst.TileManager.RemoveNetTile(m_TileCoord, this);
	}

	public void Dump(StringBuilder builder)
	{
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in m_TileCoord);
		if (worldTile != null)
		{
			builder.AppendFormat("Coord [{0}, {1}] = {2}->{3}\n", m_TileCoord.x, m_TileCoord.y, worldTile.m_LoadStep, worldTile.m_RequestState);
		}
		else
		{
			builder.AppendFormat("Coord [{0}, {1}] = Null Tile!\n", m_TileCoord.x, m_TileCoord.y);
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		int length = m_ResourceDamageStatesByGridCoord.GetLength(0);
		int length2 = m_ResourceDamageStatesByGridCoord.GetLength(1);
		for (int i = 0; i < length2; i++)
		{
			for (int j = 0; j < length; j++)
			{
				byte b = m_ResourceDamageStatesByGridCoord[j, i];
				switch (b)
				{
				case byte.MaxValue:
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("[{0},{1}]", j, i);
					continue;
				case 0:
					continue;
				}
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Append(", ");
				}
				int num = b - 1;
				stringBuilder2.AppendFormat("[{0},{1}]:{2}", j, i, num);
			}
		}
		if (stringBuilder2.Length > 0)
		{
			builder.Append("Alive scenery at ");
			builder.Append(stringBuilder2);
			builder.AppendLine();
		}
		if (stringBuilder.Length > 0)
		{
			builder.Append("Dead scenery at ");
			builder.Append(stringBuilder);
			builder.AppendLine();
		}
		if (stringBuilder2.Length == 0 && stringBuilder.Length == 0)
		{
			builder.AppendLine("No scenery");
		}
	}

	private void UNetVersion()
	{
	}
}
