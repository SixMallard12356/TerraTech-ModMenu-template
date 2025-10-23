#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ManSpawn : Singleton.Manager<ManSpawn>, PrefabInstantiator.EditorPreInit, Mode.IManagerModeEvents
{
	[Serializable]
	public struct DeprecatedBlockReplacement
	{
		public ItemTypeInfo toReplace;

		public ItemTypeInfo replaceWith;
	}

	public enum CameraSpawnConditions
	{
		Anywhere,
		OnCamera,
		OffCamera
	}

	public enum SpawnVisualType
	{
		Default,
		Bomb,
		Effect
	}

	public enum CustomSpawnEffectType
	{
		None,
		Smoke
	}

	[Serializable]
	public struct CorporationCratePrefabGroup
	{
		[HideInInspector]
		public string Name;

		public FactionSubTypes[] FactionTypes;

		public Crate Prefab;
	}

	public abstract class ObjectSpawnParams
	{
		public float m_DelayBeforeBombSpawn;

		public abstract float ObjectSize { get; }

		public abstract TrackedVisible SpawnCurrentFreeSpaceObject(Vector3? position);

		public abstract DeliveryBombSpawner.ImpactMarkerType GetBombImpactMarkerType();

		public abstract SpawnVisualType GetSpawnVisualType();

		public virtual CustomSpawnEffectType GetCustomSpawnEffectType()
		{
			return CustomSpawnEffectType.None;
		}
	}

	public class TechSpawnParams : ObjectSpawnParams
	{
		public TechData m_TechToSpawn;

		public AITreeType m_AIType;

		public TechAI.AIVariables m_AIVariables;

		public int m_Team;

		public QuatSerial m_Rotation;

		public bool m_Grounded;

		public SpawnVisualType m_SpawnVisualType;

		public CustomSpawnEffectType m_SpawnVisualCustomEffectType;

		public bool m_TakeBlocksFromPlayerInventory;

		public bool m_IsPopulation;

		public bool m_HasRewardValue = true;

		public bool m_ShouldExplodeDetachingBlocks;

		public float m_ExplodeDetachingBlocksDelay;

		public bool m_ShowMarkerOverlay = true;

		public bool m_IgnoreSceneryOnGroundProjection;

		public bool m_Invulnerable;

		public override float ObjectSize => m_TechToSpawn.Radius;

		public override TrackedVisible SpawnCurrentFreeSpaceObject(Vector3? position)
		{
			TrackedVisible trackedVisible = null;
			if (m_IsPopulation && Singleton.Manager<ManPop>.inst.IsPaused())
			{
				d.Log("did not spawn population tech as population is paused");
				return null;
			}
			if (position.HasValue)
			{
				TankSpawnParams param = new TankSpawnParams
				{
					techData = m_TechToSpawn,
					blockIDs = null,
					teamID = m_Team,
					position = position.Value,
					rotation = m_Rotation,
					grounded = m_Grounded,
					isPopulation = m_IsPopulation,
					hasRewardValue = m_HasRewardValue,
					shouldExplodeDetachingBlocks = m_ShouldExplodeDetachingBlocks,
					explodeDetachingBlocksDelay = m_ExplodeDetachingBlocksDelay,
					ignoreSceneryOnSpawnProjection = m_IgnoreSceneryOnGroundProjection,
					hideMarker = !m_ShowMarkerOverlay,
					isInvulnerable = m_Invulnerable
				};
				if (m_TakeBlocksFromPlayerInventory && !Singleton.Manager<ManPlayer>.inst.InventoryIsUnrestricted)
				{
					param.inventory = Singleton.Manager<ManPlayer>.inst.PlayerInventory;
				}
				trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
				if (trackedVisible != null)
				{
					Tank tank = ((trackedVisible.visible != null) ? trackedVisible.visible.tank : null);
					if (m_AIType != null)
					{
						if (tank != null)
						{
							tank.AI.SetBehaviorType(m_AIType);
						}
						else
						{
							ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(trackedVisible.Position, createNewDataIfNotFound: false);
							if (storedTileIfNotSpawned != null)
							{
								storedTileIfNotSpawned.SetSavedTechAIType(trackedVisible.ID, m_AIType);
							}
							else
							{
								d.LogErrorFormat("Trying to change tech AI type, but tech did not resolve to a valid Tile, Loaded or not, at position {0}", trackedVisible.Position);
							}
						}
					}
				}
				else
				{
					d.LogWarning("TechSpawnParams.SpawnCurrentFreeSpaceObject Tried to spawn tech " + m_TechToSpawn.Name + " from inventory but there were spawn errors!");
				}
			}
			else
			{
				d.Log("SpawnObjectInFreeSpace (via Update): No spawn position found for Tech " + m_TechToSpawn.Name);
			}
			return trackedVisible;
		}

		public override DeliveryBombSpawner.ImpactMarkerType GetBombImpactMarkerType()
		{
			if (!IsPlayerTeam(m_Team))
			{
				return DeliveryBombSpawner.ImpactMarkerType.Tech;
			}
			return DeliveryBombSpawner.ImpactMarkerType.FriendlyTech;
		}

		public override SpawnVisualType GetSpawnVisualType()
		{
			return m_SpawnVisualType;
		}

		public override CustomSpawnEffectType GetCustomSpawnEffectType()
		{
			return m_SpawnVisualCustomEffectType;
		}
	}

	public class BlockSpawnParams : ObjectSpawnParams
	{
		public BlockTypes m_BlockType;

		public QuatSerial m_Rotation;

		public SpawnVisualType m_SpawnVisualType;

		public CustomSpawnEffectType m_SpawnVisualCustomEffectType;

		public override float ObjectSize => 4f;

		public override TrackedVisible SpawnCurrentFreeSpaceObject(Vector3? position)
		{
			TrackedVisible trackedVisible = null;
			if (position.HasValue)
			{
				trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Block, (int)m_BlockType), position.Value, m_Rotation, addToObjectManager: true, forceSpawn: false);
			}
			if (!position.HasValue || trackedVisible == null)
			{
				d.LogError("SpawnObjectInFreeSpaceNEW (via Update): No spawn position found for Block " + m_BlockType);
			}
			return trackedVisible;
		}

		public override DeliveryBombSpawner.ImpactMarkerType GetBombImpactMarkerType()
		{
			return DeliveryBombSpawner.ImpactMarkerType.Crate;
		}

		public override SpawnVisualType GetSpawnVisualType()
		{
			return m_SpawnVisualType;
		}

		public override CustomSpawnEffectType GetCustomSpawnEffectType()
		{
			return m_SpawnVisualCustomEffectType;
		}
	}

	public class CrateSpawnParams : ObjectSpawnParams
	{
		public Crate.Definition m_CrateDef;

		public FactionSubTypes m_CorpType;

		public string m_Name;

		public QuatSerial m_Rotation;

		public bool m_VisibleOnRadar;

		public override float ObjectSize => Singleton.Manager<ManSpawn>.inst.GetCrateSpawnClearance(m_CorpType);

		public override TrackedVisible SpawnCurrentFreeSpaceObject(Vector3? position)
		{
			TrackedVisible trackedVisible = null;
			bool flag = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
			if (flag && !ManNetwork.IsHost)
			{
				d.LogError("Cannot spawn crate in multiplayer except on host");
				position = null;
			}
			if (position.HasValue)
			{
				trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnEmptyCrateDef(m_Name, m_CrateDef, position.Value, m_Rotation, grounded: true, flag, forceSpawn: false, m_CorpType);
				if (trackedVisible != null)
				{
					if (!m_VisibleOnRadar)
					{
						trackedVisible.RadarType = RadarTypes.Hidden;
					}
					Singleton.Manager<ManVisible>.inst.TrackVisible(trackedVisible);
					if (flag && (bool)trackedVisible.visible)
					{
						NetworkServer.Spawn(trackedVisible.visible.crate.gameObject);
					}
				}
			}
			else
			{
				d.Log("SpawnObjectInFreeSpace (via Update): No spawn position found for Crate " + m_Name);
			}
			return trackedVisible;
		}

		public override DeliveryBombSpawner.ImpactMarkerType GetBombImpactMarkerType()
		{
			return DeliveryBombSpawner.ImpactMarkerType.Crate;
		}

		public override SpawnVisualType GetSpawnVisualType()
		{
			return SpawnVisualType.Bomb;
		}
	}

	public struct TankSpawnParams
	{
		public enum Placement
		{
			BaseCentredAtPosition,
			BoundsCentredAtPosition,
			PlacedAtPosition
		}

		public TechData techData;

		public int[] blockIDs;

		public int teamID;

		public Vector3 position;

		public Quaternion rotation;

		public Placement placement;

		public bool hideMarker;

		public IInventory<BlockTypes> inventory;

		public bool isPopulation;

		public bool isInvulnerable;

		private bool notGrounded;

		public bool forceSpawn;

		public bool ignoreSceneryOnSpawnProjection;

		private bool noRewardValue;

		public bool shouldExplodeDetachingBlocks;

		public float explodeDetachingBlocksDelay;

		public bool grounded
		{
			get
			{
				return !notGrounded;
			}
			set
			{
				notGrounded = !value;
			}
		}

		public bool hasRewardValue
		{
			get
			{
				return !noRewardValue;
			}
			set
			{
				noRewardValue = !value;
			}
		}
	}

	[Flags]
	public enum SceneryRemovalFlags
	{
		SpawnNoChunks = 1,
		PreventRegrow = 2,
		RemoveInstant = 4,
		RemovePersistentDamageStage = 8
	}

	private struct PrefabPair
	{
		public Transform stdPrefab;

		public Transform netPrefab;
	}

	public struct PopulateTechHelper : IDisposable
	{
		private Tank m_Tech;

		private int m_NumBlockInfos;

		private bool m_Valid;

		private bool m_RecycleOnFail;

		private Action<TankBlock, TankPreset.BlockSpec> AddFailedHandler;

		private IInventory<BlockTypes> m_Inventory;

		private bool m_AllowHeadlessTech;

		private bool m_TryAnchor;

		private bool m_ReportFailure;

		private bool m_AllowAttachBlocksWithoutLinks;

		public PopulateTechHelper(Tank tech, bool spawningNew, bool recycleFailedAdds, IInventory<BlockTypes> inventory, bool allowHeadlessTech = false, bool tryDeployAnchors = false, bool reportFailure = true, bool allowAttachBlocksWithoutLinks = false, Action<TankBlock, TankPreset.BlockSpec> addFailedHandler = null)
		{
			m_Tech = tech;
			m_NumBlockInfos = 0;
			m_RecycleOnFail = recycleFailedAdds;
			m_Inventory = inventory;
			m_AllowHeadlessTech = allowHeadlessTech;
			m_TryAnchor = tryDeployAnchors;
			m_ReportFailure = reportFailure;
			m_AllowAttachBlocksWithoutLinks = allowAttachBlocksWithoutLinks;
			AddFailedHandler = addFailedHandler;
			Singleton.Manager<ManSpawn>.inst.IsTechSpawning = spawningNew;
			m_Valid = !Singleton.Manager<ManSpawn>.inst.m_PopulateTechBufferInUse;
			d.Assert(m_Valid, "BlockAddSerialHelper invalid because a buffer operation is already in progress");
			Singleton.Manager<ManSpawn>.inst.m_PopulateTechBufferInUse = true;
		}

		public void AddBlock(TankBlock block, TankPreset.BlockSpec serialData, bool alreadyAttached, string overrideFailMsg = null)
		{
			if (m_Valid)
			{
				if (m_NumBlockInfos >= Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer.Length)
				{
					Array.Resize(ref Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer, Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer.Length * 2);
				}
				Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[m_NumBlockInfos++] = new PopulateTechBlockInfo
				{
					block = block,
					blockSpec = serialData,
					status = (alreadyAttached ? PopulateTechBlockInfo.Status.Attached : PopulateTechBlockInfo.Status.ToAttach),
					overrideFailMsg = overrideFailMsg
				};
			}
		}

		public TankBlock GetBlockByLocalPosition(IntVector3 pos)
		{
			for (int i = 0; i < m_NumBlockInfos; i++)
			{
				if (Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].blockSpec.HasValue && pos == Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].blockSpec.Value.position)
				{
					return Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].block;
				}
			}
			return null;
		}

		public void Dispose()
		{
			if (!m_Valid)
			{
				return;
			}
			Singleton.Manager<ManSpawn>.inst.GetBlockByLocalPositionDuringSpawn = GetBlockByLocalPosition;
			ModuleAnchor.AnchorOnAttach = (m_TryAnchor ? ModuleAnchor.OnAttachBehaviour.AlwaysTry : ModuleAnchor.OnAttachBehaviour.Never);
			ModuleItemConveyor.LinkOnAttach = false;
			TankBlock tankBlock = null;
			IntVector3 intVector = IntVector3.zero;
			int num;
			do
			{
				num = 0;
				for (int i = 0; i < m_NumBlockInfos; i++)
				{
					if (Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].status != PopulateTechBlockInfo.Status.ToAttach || Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].block == null)
					{
						continue;
					}
					if ((object)Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].block.tank == m_Tech)
					{
						d.Assert(condition: false, "ignoring duplicate block: how did this happen?");
						Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].status = PopulateTechBlockInfo.Status.Ignored;
						continue;
					}
					IntVector3 position = Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].blockSpec.Value.position;
					if ((bool)tankBlock)
					{
						position += new IntVector3(tankBlock.trans.localPosition) - intVector;
					}
					if (m_Tech.blockman.AddBlockToTech(Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].block, position, new OrthoRotation(Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].blockSpec.Value.orthoRotation), m_AllowAttachBlocksWithoutLinks))
					{
						if (!tankBlock)
						{
							tankBlock = Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].block;
							intVector = position;
						}
						num++;
						Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[i].status = PopulateTechBlockInfo.Status.Attached;
					}
				}
			}
			while (num != 0);
			if (ManNetwork.IsHost)
			{
				for (int j = 0; j < m_NumBlockInfos; j++)
				{
					if (Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[j].status == PopulateTechBlockInfo.Status.Attached)
					{
						Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[j].TrySerialize();
					}
				}
			}
			ModuleAnchor.AnchorOnAttach = ModuleAnchor.OnAttachBehaviour.FirstOrSameAs;
			ModuleItemConveyor.LinkOnAttach = true;
			Singleton.Manager<ManSpawn>.inst.IsTechSpawning = false;
			string text = string.Empty;
			int num2 = 0;
			for (int k = 0; k < m_NumBlockInfos; k++)
			{
				if (Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].status != PopulateTechBlockInfo.Status.ToAttach)
				{
					continue;
				}
				string arg;
				if (!Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].block)
				{
					arg = ((Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].overrideFailMsg == null) ? "SPAWN" : Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].overrideFailMsg);
				}
				else
				{
					AddFailedHandler?.Invoke(Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].block, Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].blockSpec.Value);
					if (m_RecycleOnFail)
					{
						Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].block.trans.Recycle();
						if (m_Inventory != null)
						{
							m_Inventory.HostAddItem(Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].blockSpec.Value.GetBlockType());
						}
					}
					arg = "ADD";
				}
				text += $" {Singleton.Manager<ManSpawn>.inst.m_PopulateTechBuffer[k].blockSpec.Value.GetBlockType()} {arg} failed,";
				num2++;
			}
			if (m_ReportFailure && text != string.Empty)
			{
				d.LogError($"{m_Tech.name} spawn errors:\n{text}");
			}
			Singleton.Manager<ManSpawn>.inst.GetBlockByLocalPositionDuringSpawn = null;
			Singleton.Manager<ManSpawn>.inst.m_PopulateTechBufferInUse = false;
			if (ManNetwork.IsHost)
			{
				m_Tech.blockman.FixupAfterRemovingBlocks(m_AllowHeadlessTech);
			}
			if (m_Tech.blockman.blockCount == 0)
			{
				m_Tech.visible.RemoveFromGame();
			}
		}
	}

	private struct PopulateTechBlockInfo
	{
		public enum Status
		{
			ToAttach,
			Attached,
			Ignored
		}

		public TankBlock block;

		public TankPreset.BlockSpec? blockSpec;

		public Status status;

		public string overrideFailMsg;

		public void TrySerialize()
		{
			block.SetSkinByUniqueID(blockSpec.Value.m_SkinID, blockSpec.Value.GetBlockType());
			if (blockSpec.Value.textSerialData != null && blockSpec.Value.textSerialData.Count != 0)
			{
				block.SerializeToText(saving: false, blockSpec.Value);
			}
			if (blockSpec.Value.saveState != null)
			{
				block.Serialize(saving: false, blockSpec.Value);
			}
		}
	}

	private class SpawnBlockMenuList
	{
		public string name;

		public Dictionary<string, BlockTypes> lookup = new Dictionary<string, BlockTypes>();

		public SpawnBlockMenuList nextList;

		public SpawnBlockMenuList prevList;

		public string[] menuStrings;
	}

	private enum DebugPresetList
	{
		Presets,
		Snapshots
	}

	private class SaveData
	{
		public int blockLimit;
	}

	[SerializeField]
	private Transform m_TankPrefab;

	[SerializeField]
	private CorporationCratePrefabGroup[] m_CorpCratePrefabs;

	[SerializeField]
	private TankPreset m_DispenserPreset;

	[SerializeField]
	private Waypoint m_WaypointPrefab;

	[SerializeField]
	public Waypoint m_NetWaypointPrefab;

	[SerializeField]
	private DeliveryBombSpawner m_DeliveryBombSpawnerPrefab;

	[EnumArray(typeof(CustomSpawnEffectType))]
	[SerializeField]
	private ParticleSystem[] m_CustomSpawnEffectPrefabs;

	[SerializeField]
	private Material m_LockedMaterial;

	[SerializeField]
	[Tooltip("Reference table to match TerrainObjects from a saved guid to their prefab")]
	private TerrainObjectTable m_TerrainObjectTable;

	[SerializeField]
	private List<string> m_StrippedTankTypesList;

	[SerializeField]
	private List<string> m_StrippedCrateTypesList;

	[SerializeField]
	private RewardSpawner m_RewardSpawner;

	public DeprecatedBlockReplacement[] m_DeprecatedBlockReplacement;

	[SerializeField]
	private BlockTable m_BlockTable;

	[SerializeField]
	private KitBashPanelTable m_KitBashPanelTable;

	[SerializeField]
	private ResourceFileList tankPresetList = new ResourceFileList("Presets/", ".asset");

	[SerializeField]
	private ResourceFileSelection m_EditorOnlyPresetSelection;

	[SerializeField]
	private NetBlock m_NetBlockPrefab;

	[SerializeField]
	private NetChunk m_NetChunkPrefab;

	public const int DefaultPlayerTeam = 0;

	public const int FirstMultiplayerTeam = 1073741824;

	public const int FirstEnemyTeam = 1;

	public const int NewEnemyTeam = -1;

	public const int NeutralTeam = -2;

	public const int InvalidTeam = int.MaxValue;

	public Dictionary<string, string> ReplacePartNames = new Dictionary<string, string>();

	private Dictionary<string, TankPreset> tankPresets = new Dictionary<string, TankPreset>();

	private SnapshotCollectionDisk m_DiskSnapshotCollection;

	private DebugPresetList m_SpawnPresetMode;

	private Dictionary<int, Transform> m_BlockPrefabs = new Dictionary<int, Transform>();

	private Dictionary<int, KitBashPanel> m_KitBashPanelPrefabs = new Dictionary<int, KitBashPanel>();

	private MouseMenu spawnPresetMenu;

	private MouseMenu spawnBlockMenu;

	private MouseMenu spawnChunkMenu;

	private MouseMenu spawnSceneryMenu;

	private MouseMenu debugNameDisplay;

	private OnGUICallback m_GUICallbackObject;

	private List<ChunkTypes> chunkTypesLookup;

	private List<Visible> spawnableScenery;

	private List<SpawnBlockMenuList> m_BlockMenuLists = new List<SpawnBlockMenuList>();

	private SpawnBlockMenuList m_CurrentBlockMenuList;

	private SpawnBlockMenuList m_SecretBlockMenuList;

	private bool m_SecretBlocksDebugSpawn;

	private List<BlockTypes> m_LoadedBlocks = new List<BlockTypes>();

	private List<BlockTypes> m_LoadedActiveBlocks = new List<BlockTypes>();

	private List<KitBashPanelTypes> m_LoadedKitBashPanels = new List<KitBashPanelTypes>();

	private Dictionary<ItemTypeInfo, ItemTypeInfo> m_DeprecatedBlockReplacementLookup = new Dictionary<ItemTypeInfo, ItemTypeInfo>();

	private int m_DispenserBlockIndex = -1;

	private PopulateTechBlockInfo[] m_PopulateTechBuffer = new PopulateTechBlockInfo[512];

	private bool m_PopulateTechBufferInUse;

	public static readonly Bitfield<ObjectTypes> AvoidSceneryVehiclesCrates = new Bitfield<ObjectTypes>(new ObjectTypes[3]
	{
		ObjectTypes.Scenery,
		ObjectTypes.Vehicle,
		ObjectTypes.Crate
	});

	public static readonly Bitfield<ObjectTypes> AvoidSceneryVehiclesCratesBlocks = new Bitfield<ObjectTypes>(new ObjectTypes[4]
	{
		ObjectTypes.Scenery,
		ObjectTypes.Vehicle,
		ObjectTypes.Crate,
		ObjectTypes.Block
	});

	public static readonly Bitfield<ObjectTypes> kVisibleMaskTech = new Bitfield<ObjectTypes>(1);

	public static readonly Bitfield<ObjectTypes> kVisibleMaskScenery = new Bitfield<ObjectTypes>(3);

	public static readonly Bitfield<ObjectTypes> kVisibleMaskTechAndCrates = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Vehicle,
		ObjectTypes.Crate
	});

	public static readonly Bitfield<ObjectTypes> kVisibleMaskBlocksAndTech = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Block,
		ObjectTypes.Vehicle
	});

	public static readonly Bitfield<ObjectTypes> kVisibleMaskBlocksAndChunks = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Block,
		ObjectTypes.Chunk
	});

	public static readonly Bitfield<ObjectTypes> kVisibleMaskBlocksTechAndChunks = new Bitfield<ObjectTypes>(new ObjectTypes[3]
	{
		ObjectTypes.Block,
		ObjectTypes.Vehicle,
		ObjectTypes.Chunk
	});

	public static string Debug_SpawningTechName;

	private int m_TeamCounter = 1;

	private Dictionary<BlockTypes, BlockTypes> m_BlockAliasing = new Dictionary<BlockTypes, BlockTypes>(new BlockTypesComparer())
	{
		{
			BlockTypes.GSOAIController_111,
			BlockTypes.GSOAIGuardController_111
		},
		{
			BlockTypes.GSOSiloBig_212,
			BlockTypes.GSOSilo_212
		},
		{
			BlockTypes.GSO_Exploder_A_111,
			BlockTypes.GSO_Exploder_A1_111
		}
	};

	private HashSet<Type> m_StrippedTankTypes;

	private HashSet<Type> m_StrippedCrateTypes;

	private Transform m_RuntimePrefabsContainer;

	private PrefabPair m_TankRuntimePrefabs;

	private Dictionary<FactionSubTypes, Crate> m_CorpCratePrefabsDict;

	private Dictionary<FactionSubTypes, PrefabPair> m_CorpCrateRuntimePrefabsDict;

	private List<TankPreset> m_AllTechPresets;

	private static readonly BlockTypes kDispenserBlock = BlockTypes.GSODispenserMini_111;

	private HashSet<BlockTypes> m_PlayerFacingBlockTypes;

	private Dictionary<Transform, Transform> m_OrigPrefabLookup = new Dictionary<Transform, Transform>();

	public int BlockLimit { get; set; }

	public TankPreset DispenserPreset => m_DispenserPreset;

	public Material LockedMaterial => m_LockedMaterial;

	public Func<IntVector3, TankBlock> GetBlockByLocalPositionDuringSpawn { get; private set; }

	public EnumDescriptorTable VisibleTypeInfo { get; private set; }

	public bool IsTechSpawning { get; private set; }

	public bool IsRemovingAllBlocks { get; set; }

	public bool BlocksLoaded { get; private set; }

	public RewardSpawner RewardSpawner => m_RewardSpawner;

	public bool IsSpawningDebugPaintingBlock { get; set; }

	public bool DebugSpawnMenuActive
	{
		get
		{
			if (!spawnPresetMenu.IsActive && !spawnBlockMenu.IsActive && !spawnChunkMenu.IsActive && !spawnSceneryMenu.IsActive)
			{
				return debugNameDisplay.IsActive;
			}
			return true;
		}
	}

	public static bool IsEnemyTeam(int team)
	{
		if (team >= 1 && team < 1073741824)
		{
			return team != int.MaxValue;
		}
		return false;
	}

	public static bool IsPlayerTeam(int team)
	{
		return team switch
		{
			int.MaxValue => false, 
			0 => true, 
			_ => team >= 1073741824, 
		};
	}

	public static int TechTeamIDFromLobbyTeamID(int lobbyTeamID)
	{
		return 1073741824 + lobbyTeamID;
	}

	public static int LobbyTeamIDFromTechTeamID(int techTeamID)
	{
		if (techTeamID == int.MaxValue || techTeamID < 1073741824)
		{
			return int.MaxValue;
		}
		return techTeamID - 1073741824;
	}

	public bool IsBlockTypeDeprecated(ItemTypeInfo type)
	{
		return m_DeprecatedBlockReplacementLookup.ContainsKey(type);
	}

	public float GetCrateSpawnClearance(FactionSubTypes type = FactionSubTypes.NULL)
	{
		if (m_CorpCratePrefabsDict.ContainsKey(type))
		{
			return m_CorpCratePrefabsDict[type].Radius;
		}
		if (m_CorpCratePrefabsDict.ContainsKey(FactionSubTypes.NULL))
		{
			return m_CorpCratePrefabsDict[FactionSubTypes.NULL].Radius;
		}
		return 0f;
	}

	private PrefabPair GetCorpCrateRuntimePrefab(FactionSubTypes type = FactionSubTypes.NULL)
	{
		d.Assert(m_CorpCrateRuntimePrefabsDict.ContainsKey(FactionSubTypes.NULL), "Attempted to get a generic faction for an unspecific crate but the generic crate with 'NULL' as the corporation was not set up in ManSpawn>CorpCratePrefabs!");
		if (!m_CorpCrateRuntimePrefabsDict.ContainsKey(type))
		{
			return m_CorpCrateRuntimePrefabsDict[FactionSubTypes.NULL];
		}
		return m_CorpCrateRuntimePrefabsDict[type];
	}

	public ParticleSystem GetCustomSpawnEffectPrefabs(CustomSpawnEffectType customType)
	{
		if (customType >= CustomSpawnEffectType.None && (int)customType < m_CustomSpawnEffectPrefabs.Length)
		{
			return m_CustomSpawnEffectPrefabs[(int)customType];
		}
		return null;
	}

	private void StoreBlockCorporation(BlockTypes blockType, FactionSubTypes corporation)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		VisibleTypeInfo.SetDescriptor(hashCode, corporation);
	}

	private void StoreBlockCategory(BlockTypes blockType, BlockCategories category)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		VisibleTypeInfo.SetDescriptor(hashCode, category);
	}

	private void StoreBlockRarity(BlockTypes blockType, BlockRarity rarity)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		VisibleTypeInfo.SetDescriptor(hashCode, rarity);
	}

	public FactionSubTypes GetCorporation(BlockTypes blockType)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		return (FactionSubTypes)VisibleTypeInfo.GetDescriptorFlags<FactionSubTypes>(hashCode);
	}

	public BlockCategories GetCategory(BlockTypes blockType)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		return (BlockCategories)VisibleTypeInfo.GetDescriptorFlags<BlockCategories>(hashCode);
	}

	public BlockRarity GetRarity(BlockTypes blockType)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		return (BlockRarity)VisibleTypeInfo.GetDescriptorFlags<BlockRarity>(hashCode);
	}

	public bool HasBlockDescriptorFlag(BlockTypes block, Type descriptorType, int descriptorValue)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)block);
		return VisibleTypeInfo.IsDescriptorFlag(hashCode, descriptorType, descriptorValue);
	}

	public bool HasBlockDescriptorEnum(BlockTypes block, Type descriptorType, int descriptorEnumValue)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)block);
		int descriptorValue = Bitfield.Add(0, descriptorEnumValue);
		return VisibleTypeInfo.IsDescriptorFlag(hashCode, descriptorType, descriptorValue);
	}

	public BlockAttributes[] GetBlockAttributes(BlockTypes block)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)block);
		return VisibleTypeInfo.GetDescriptorFlagsArray<BlockAttributes>(hashCode);
	}

	public BlockControlAttributes[] GetBlockControlAttributes(BlockTypes block)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)block);
		return VisibleTypeInfo.GetDescriptorFlagsArray<BlockControlAttributes>(hashCode);
	}

	public static FactionSubTypes DetermineFactionFromBlockTypeSlow(BlockTypes blockType)
	{
		return (FactionSubTypes)GetFactionIndexFromBlockType(blockType);
	}

	private void CacheCorporationBlocks()
	{
		HashSet<int> hashSet = new HashSet<int>();
		foreach (BlockTypes loadedBlock in m_LoadedBlocks)
		{
			if (GetCorporation(loadedBlock) == FactionSubTypes.NULL)
			{
				FactionSubTypes factionIndexFromBlockType = (FactionSubTypes)GetFactionIndexFromBlockType(loadedBlock);
				if (factionIndexFromBlockType != FactionSubTypes.NULL)
				{
					StoreBlockCorporation(loadedBlock, factionIndexFromBlockType);
				}
			}
			hashSet.Add((int)loadedBlock);
		}
		EnumValuesIterator<BlockTypes> enumerator2 = EnumIterator<BlockTypes>.Values().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			BlockTypes current2 = enumerator2.Current;
			if (!hashSet.Contains((int)current2))
			{
				FactionSubTypes factionIndexFromBlockType2 = (FactionSubTypes)GetFactionIndexFromBlockType(current2);
				if (factionIndexFromBlockType2 != FactionSubTypes.NULL)
				{
					StoreBlockCorporation(current2, factionIndexFromBlockType2);
				}
			}
		}
	}

	public static int GetFactionIndexFromBlockType(BlockTypes blockType)
	{
		string[] names = EnumNamesIterator<FactionSubTypes>.Names;
		string text = blockType.ToString();
		int result = 0;
		for (int i = 0; i < names.Length; i++)
		{
			if (text.StartsWith(names[i]))
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public bool IsBlockUsageRestrictedInGameMode(BlockTypes blockType)
	{
		bool flag = true;
		BlockFilterTable allowedBlocks = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory().m_AllowedBlocks;
		if (allowedBlocks != null)
		{
			flag = allowedBlocks.CheckBlockAllowed(blockType);
		}
		if (flag)
		{
			flag = Singleton.Manager<ManGameMode>.inst.CheckBlockAllowed(blockType);
		}
		return !flag;
	}

	public bool IsBlockAllowedInCurrentGameMode(BlockTypes blockType)
	{
		bool flag = true;
		if (flag && GetBlockAvailabilityFlags(blockType, out var flags))
		{
			flag = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? Bitfield.Contains(flags, 0) : Bitfield.Contains(flags, 1));
		}
		if (flag && Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockType))
		{
			flag = Singleton.Manager<ManDLC>.inst.AreRandDBlocksAllowedInGameType(Singleton.Manager<ManGameMode>.inst.GetCurrentGameType());
		}
		return flag;
	}

	public bool IsBlockAllowedInLaunchedConfig(BlockTypes blockType)
	{
		bool flag = true;
		if (GetBlockAvailabilityFlags(blockType, out var flags))
		{
			if (SKU.IsSteam && !Bitfield.Contains(flags, 2))
			{
				flag = false;
			}
			if (SKU.XboxOneUI && !Bitfield.Contains(flags, 4))
			{
				flag = false;
			}
			if (SKU.PS4UI && !Bitfield.Contains(flags, 5))
			{
				flag = false;
			}
			if (SKU.SwitchUI && !Bitfield.Contains(flags, 6))
			{
				flag = false;
			}
			if (SKU.IsNetEase && !Bitfield.Contains(flags, 7))
			{
				flag = false;
			}
			if (SKU.IsEpicGS && !Bitfield.Contains(flags, 8))
			{
				flag = false;
			}
		}
		if (flag && Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockType) && !Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
		{
			flag = false;
		}
		if (!m_BlockPrefabs.ContainsKey((int)blockType))
		{
			flag = false;
		}
		return flag;
	}

	public bool GetBlockAvailabilityFlags(BlockTypes blockType, out int flags)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
		return VisibleTypeInfo.TryGetDescriptor<ModulePlatformRestrictions.PlatformAvailability>(hashCode, out flags);
	}

	public DeliveryBombSpawner SpawnDeliveryBombNew(Vector3 targetImpactPosition, DeliveryBombSpawner.ImpactMarkerType impactMarkerType, float delayBeforeSpawn = 0f)
	{
		Vector3 vector = Vector3.up * 500f;
		Vector3 position = Singleton.Manager<ManWorld>.inst.ProjectToGround(targetImpactPosition) + vector;
		DeliveryBombSpawner deliveryBombSpawner = m_DeliveryBombSpawnerPrefab.Spawn(position);
		deliveryBombSpawner.SetSpawnParams(targetImpactPosition, impactMarkerType, delayBeforeSpawn);
		return deliveryBombSpawner;
	}

	public TrackedVisible SpawnDispenser(Vector3 pos, Quaternion rot, ItemTypeInfo itemToSpawn, int quantity)
	{
		TrackedVisible result = null;
		TechData techDataFormatted = m_DispenserPreset.GetTechDataFormatted();
		if (m_DispenserBlockIndex == -1)
		{
			m_DispenserBlockIndex = techDataFormatted.m_BlockSpecs.FindIndex((TankPreset.BlockSpec bs) => bs.GetBlockType() == kDispenserBlock);
		}
		Dictionary<int, Module.SerialData> saveState = new Dictionary<int, Module.SerialData>();
		if (m_DispenserBlockIndex != -1)
		{
			TankPreset.BlockSpec value = techDataFormatted.m_BlockSpecs[m_DispenserBlockIndex].SetSaveState(saveState);
			techDataFormatted.m_BlockSpecs[m_DispenserBlockIndex] = value;
			ModuleItemDispenser.AddSerialData(techDataFormatted.m_BlockSpecs[m_DispenserBlockIndex], itemToSpawn, quantity);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				uint[] array = new uint[techDataFormatted.m_BlockSpecs.Count];
				for (int num = 0; num < techDataFormatted.m_BlockSpecs.Count; num++)
				{
					array[num] = Singleton.Manager<ManNetwork>.inst.GetNextHostBlockPoolID();
				}
				result = Singleton.Manager<ManNetwork>.inst.SpawnNetworkedNonPlayerTech(techDataFormatted, array, pos, Quaternion.identity, grounded: true);
			}
			else
			{
				TankSpawnParams param = new TankSpawnParams
				{
					techData = techDataFormatted,
					blockIDs = null,
					teamID = -2,
					position = pos,
					rotation = rot,
					grounded = true
				};
				result = SpawnTankRef(param, addToObjectManager: true);
			}
		}
		else
		{
			d.LogError("ManSpawn.SpawnDispenser - No GSODispenserMini_111 found on prefab " + m_DispenserPreset.name);
		}
		return result;
	}

	public TrackedVisible SpawnTankRef(TankSpawnParams param, bool addToObjectManager)
	{
		if (param.techData == null)
		{
			d.LogError("ManSpawn.SpawnTankRef - null preset");
			return null;
		}
		TrackedVisible trackedVisible = null;
		int num = -1;
		Visible visible = null;
		if (!param.forceSpawn && !Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in param.position))
		{
			if (param.inventory != null)
			{
				d.LogError("Spawn from inventory doesn't support spawning into save data");
				return null;
			}
			num = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(ObjectTypes.Vehicle);
			Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(in param.position)?.AddSavedTech(param.techData, param.position, param.rotation, num, param.teamID, param.blockIDs, param.grounded, param.isPopulation, param.hasRewardValue, param.shouldExplodeDetachingBlocks, param.explodeDetachingBlocksDelay, !param.ignoreSceneryOnSpawnProjection);
		}
		else if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
		{
			TechData techData = param.techData;
			uint[] array = new uint[techData.m_BlockSpecs.Count];
			for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
			{
				array[i] = Singleton.Manager<ManNetwork>.inst.GetNextHostBlockPoolID();
			}
			Vector3 position = param.position;
			Bounds bounds = techData.CalculateBlockBounds();
			d.Log($"SpawnNetworkedTechRef for {techData.Name} placement={param.placement} bounds={bounds} pre-offset pos = {position}");
			switch (param.placement)
			{
			case TankSpawnParams.Placement.BaseCentredAtPosition:
				position -= param.rotation * (bounds.center - new Vector3(0f, bounds.extents.y + 0.5f, 0f));
				break;
			case TankSpawnParams.Placement.BoundsCentredAtPosition:
				position -= param.rotation * bounds.center;
				break;
			default:
				d.AssertFormat(false, "ManSpawn.SpawnTankRef does not know how to handle placement type {0} when networked", param.placement);
				break;
			case TankSpawnParams.Placement.PlacedAtPosition:
				break;
			}
			trackedVisible = SpawnNetworkedTechRef(techData, array, param.teamID, position, param.rotation, null, param.grounded, param.isPopulation, param.shouldExplodeDetachingBlocks, param.explodeDetachingBlocksDelay, param.hideMarker, null, null, addToObjectManager);
			if (!param.hasRewardValue)
			{
				trackedVisible.visible.tank.OriginalValue = 0f;
			}
		}
		else
		{
			Tank tank = SpawnTankFromTechData(param);
			if (tank != null)
			{
				num = tank.visible.ID;
				visible = tank.visible;
			}
		}
		if (num != -1)
		{
			RadarTypes radarType = ((!param.techData.CheckIsAnchored()) ? RadarTypes.Vehicle : RadarTypes.Base);
			trackedVisible = CreateTrackedVisibleForVehicle(num, visible, param.position, radarType);
			if (visible.IsNull())
			{
				trackedVisible.TeamID = param.teamID;
			}
			if (addToObjectManager)
			{
				Singleton.Manager<ManVisible>.inst.TrackVisible(trackedVisible);
			}
		}
		if (param.hideMarker)
		{
			TrackedVisible trackedVisible2 = (visible ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.ID) : null);
			if (trackedVisible2 != null)
			{
				trackedVisible2.RadarType = ((!param.hideMarker) ? trackedVisible2.DefaultRadarType : RadarTypes.Hidden);
			}
		}
		return trackedVisible;
	}

	public Tank SpawnUnmanagedTank(TankSpawnParams param)
	{
		Visible.DisableAddToTileOnSpawn = true;
		Tank tank = SpawnTankFromTechData(param);
		Visible.DisableAddToTileOnSpawn = false;
		if (tank != null && tank.visible != null)
		{
			tank.visible.SetManagedByTile(managed: false);
		}
		return tank;
	}

	private TrackedVisible CreateTrackedVisibleForVehicle(int id, Visible visible, Vector3 position, RadarTypes radarType)
	{
		TrackedVisible trackedVisible = new TrackedVisible(id, visible, ObjectTypes.Vehicle, radarType);
		trackedVisible.SetPos(position);
		return trackedVisible;
	}

	public Tank SpawnTank(TankSpawnParams param, bool addToObjectManager)
	{
		Tank result = null;
		TrackedVisible trackedVisible = SpawnTankRef(param, addToObjectManager);
		if (trackedVisible != null && (bool)trackedVisible.visible)
		{
			result = trackedVisible.visible.tank;
		}
		return result;
	}

	public TrackedVisible SpawnEmptyTechRef(int team, Vector3 position, Quaternion rotation, bool grounded, bool addToManager, string techName)
	{
		return SpawnEmptyTechRefInternal(m_TankRuntimePrefabs.stdPrefab, team, position, rotation, grounded, techName, addToManager);
	}

	public TrackedVisible SpawnNetEmptyTechRef(int team, Vector3 position, Quaternion rotation, bool grounded, string techName, bool addToObjectManager)
	{
		return SpawnEmptyTechRefInternal(m_TankRuntimePrefabs.netPrefab, team, position, rotation, grounded, techName, addToObjectManager);
	}

	public static bool TechPlacementValidator(Vector3 position, Quaternion orientation, float radius, bool placementIsAnchored, out PlacementSelection.InvalidResult invalidResult, bool replacingPlayer = false)
	{
		invalidResult = default(PlacementSelection.InvalidResult);
		if (Singleton.Manager<ManPurchases>.inst.IsLoadingTechs || Singleton.Manager<ManPurchases>.inst.IsHotswappingTechs)
		{
			invalidResult.m_Type = PlacementSelection.InvalidType.General;
			invalidResult.m_Flags |= PlacementSelection.InvalidReason.CurrentlyLoading;
			return false;
		}
		if (!Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(position, Globals.inst.m_TechStoreThreatDistance) && !Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(Singleton.playerPos, Globals.inst.m_TechStoreThreatDistance))
		{
			invalidResult.m_Type = PlacementSelection.InvalidType.General;
			invalidResult.m_Flags |= PlacementSelection.InvalidReason.EnemiesNearby;
			return false;
		}
		if (!replacingPlayer)
		{
			Vector3 vector = position - Singleton.playerPos;
			float techPlacementMaxDistance = Globals.inst.m_TechPlacementMaxDistance;
			techPlacementMaxDistance += radius;
			if (Singleton.playerTank.IsNotNull())
			{
				techPlacementMaxDistance += Singleton.playerTank.blockBounds.extents.magnitude;
			}
			if (vector.sqrMagnitude > techPlacementMaxDistance * techPlacementMaxDistance)
			{
				invalidResult.m_Type = PlacementSelection.InvalidType.Place;
				invalidResult.m_Flags |= PlacementSelection.InvalidReason.TooFarAway;
				return false;
			}
		}
		ManVisible.SearchIterator searchIterator = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(position, radius, AvoidSceneryVehiclesCrates);
		bool flag = false;
		foreach (Visible item in searchIterator)
		{
			if (item.resdisp != null && item.resdisp.IsIgnoredForPlacementCheck)
			{
				continue;
			}
			if (replacingPlayer)
			{
				Tank tank = (item.block ? item.block.tank : item.tank);
				if (tank != null && tank == Singleton.playerTank)
				{
					continue;
				}
			}
			flag = true;
			break;
		}
		if (!flag && Physics.CheckSphere(position, radius, Globals.inst.layerScenery.mask, QueryTriggerInteraction.Ignore))
		{
			flag = true;
		}
		Vector3 halfExtents = new Vector3(radius, radius * 0.5f, radius);
		halfExtents.x -= 0.5f;
		halfExtents.z -= 0.5f;
		Vector3 center = position + Vector3.up * (halfExtents.y * 0.5f);
		if (!flag && Physics.CheckBox(center, halfExtents, orientation, Globals.inst.layerLandmark.mask, QueryTriggerInteraction.Ignore))
		{
			flag = true;
		}
		if (!flag && placementIsAnchored && Singleton.Manager<ManWorld>.inst.QueryCurvature(position, radius, out float slopeInDegrees, out float _, out float largestHeightVarianceBetweenPoints) && (slopeInDegrees > 35f || largestHeightVarianceBetweenPoints > 8f))
		{
			flag = true;
		}
		if (flag)
		{
			invalidResult.m_Type = (replacingPlayer ? PlacementSelection.InvalidType.Swap : PlacementSelection.InvalidType.Place);
			invalidResult.m_Flags |= PlacementSelection.InvalidReason.PositionBlocked;
			return false;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Vector3 vector2 = Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition - position;
			vector2.y = 0f;
			if (vector2.magnitude + radius > Singleton.Manager<ManNetwork>.inst.DangerDistance)
			{
				invalidResult.m_Type = (replacingPlayer ? PlacementSelection.InvalidType.Swap : PlacementSelection.InvalidType.Place);
				invalidResult.m_Flags |= PlacementSelection.InvalidReason.OutOfBounds;
				return false;
			}
		}
		return true;
	}

	public void RepelVisiblesInRadius(Vector3 position, float repelRadius, Bitfield<ObjectTypes> objectTypes, float objectRepulsionMaximum = 1000f, float objectRepulsionDamping = 0.1f)
	{
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(position, repelRadius, objectTypes))
		{
			if ((item.block != null && item.block.tank != null) || item.holderStack != null)
			{
				continue;
			}
			if (item.rbody == null)
			{
				d.LogErrorFormat("ManSpawn.RepelVisibles - Visible '{0}' doesn't have a rigidBody, but is not attached to a tech or held in a stack.. What state is it in??", item.name);
				continue;
			}
			Vector3 vector = item.rbody.worldCenterOfMass - position;
			if (vector.y < 0f)
			{
				vector.y = 0f;
			}
			float num = repelRadius * 0.3f;
			float num2 = num * num;
			float num3 = repelRadius * repelRadius - num2;
			float num4 = 1f - Mathf.Clamp01((vector.sqrMagnitude - num2) / num3);
			if (num4 > 0f)
			{
				float num5 = 1f + item.rbody.velocity.sqrMagnitude * objectRepulsionDamping;
				float num6 = Mathf.Lerp(0f, objectRepulsionMaximum, num4) / num5;
				item.rbody.AddForce(num6 * vector.normalized, ForceMode.Acceleration);
			}
		}
	}

	public TrackedVisible SpawnEmptyCrateDef(string name, Crate.Definition crateDef, Vector3 position, Quaternion rotation, bool grounded, bool isNetVersion, bool forceSpawn = false, FactionSubTypes m_CorpType = FactionSubTypes.NULL)
	{
		Transform prefab = (isNetVersion ? m_CorpCrateRuntimePrefabsDict[m_CorpType].netPrefab : m_CorpCrateRuntimePrefabsDict[m_CorpType].stdPrefab);
		TrackedVisible trackedVisible = null;
		int num = -1;
		Visible v = null;
		if (grounded)
		{
			position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up;
		}
		if (!forceSpawn && !Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in position))
		{
			num = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(ObjectTypes.Crate);
			Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(in position)?.AddSavedCrate(name, crateDef, default(Crate.SaveData), position, rotation, num, m_CorpType);
		}
		else
		{
			Crate component = prefab.Spawn(position, rotation).GetComponent<Crate>();
			component.SetDefinition(crateDef);
			if (ManNetwork.IsHost && component.netCrate.IsNotNull())
			{
				component.netCrate.HostID = component.visible.ID;
			}
			num = component.visible.ID;
			v = component.visible;
		}
		if (num != -1)
		{
			trackedVisible = new TrackedVisible(num, v, ObjectTypes.Crate, RadarTypes.Crate);
			trackedVisible.SetPos(position);
		}
		return trackedVisible;
	}

	public TrackedVisible SpawnItemRef(ItemTypeInfo typeInfo, Vector3 position, Quaternion rotation, bool addToObjectManager, bool forceSpawn, bool initNew = true)
	{
		int id = -1;
		Visible visible = null;
		if (!forceSpawn && !Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in position))
		{
			id = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(typeInfo.ObjectType);
			Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(in position)?.AddSavedVisible(typeInfo, position, rotation, id);
		}
		else
		{
			visible = SpawnItemInternal(typeInfo, position, rotation, initNew);
			if ((bool)visible)
			{
				id = visible.ID;
				position = visible.trans.position;
			}
			else
			{
				addToObjectManager = false;
			}
		}
		RadarTypes radarType = ((typeInfo.ObjectType == ObjectTypes.Block) ? RadarTypes.Block : RadarTypes.Hidden);
		TrackedVisible trackedVisible = new TrackedVisible(id, visible, typeInfo.ObjectType, radarType);
		trackedVisible.SetPos(position);
		if (addToObjectManager)
		{
			Singleton.Manager<ManVisible>.inst.TrackVisible(trackedVisible);
		}
		return trackedVisible;
	}

	public Visible SpawnItem(ItemTypeInfo typeInfo, Vector3 position, Quaternion rotation, bool addToObjectManager = false, bool forceSpawn = false, bool initNew = true)
	{
		return SpawnItemRef(typeInfo, position, rotation, addToObjectManager, forceSpawn, initNew).visible;
	}

	public TankBlock GetBlockPrefab(BlockTypes type)
	{
		if (m_BlockPrefabs.TryGetValue((int)type, out var value))
		{
			return value.GetComponent<TankBlock>();
		}
		return null;
	}

	public KitBashPanel GetKitBashPanelPrefab(KitBashPanelTypes type)
	{
		if (!m_KitBashPanelPrefabs.TryGetValue((int)type, out var value))
		{
			return null;
		}
		return value;
	}

	public NetBlock SpawnNetBlock(uint blockPoolID, BlockTypes type, Vector3 pos, Quaternion rot, byte skinIndex)
	{
		NetBlock component = m_NetBlockPrefab.transform.Spawn(null, pos, rot).GetComponent<NetBlock>();
		component.OnServerSetBlockType(type);
		component.BlockPoolID = blockPoolID;
		component.name = m_NetBlockPrefab.name + "ID=" + blockPoolID;
		component.SkinID = Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(skinIndex, Singleton.Manager<ManSpawn>.inst.GetCorporation(type));
		return component;
	}

	public TankBlock SpawnBlock(BlockTypes type, Vector3 pos, Quaternion rot)
	{
		TankBlock tankBlock = null;
		if (m_BlockPrefabs.TryGetValue((int)type, out var value))
		{
			tankBlock = value.Spawn(null, pos, rot).GetComponent<TankBlock>();
			tankBlock.name = value.name;
			if (tankBlock.visible.m_ItemType.ItemType != (int)type)
			{
				d.LogError($"Block prefab {tankBlock.name} had ID {tankBlock.visible.m_ItemType.ItemType} when trying to spawn {type}");
			}
		}
		return tankBlock;
	}

	public KitBashPanel SpawnKitBashPanel(KitBashPanelTypes type, Vector3 pos, Quaternion rot, Transform parent = null)
	{
		KitBashPanel kitBashPanel = null;
		if (m_KitBashPanelPrefabs.TryGetValue((int)type, out var value))
		{
			kitBashPanel = value.Spawn(parent, pos, rot);
			kitBashPanel.name = value.name;
			if (kitBashPanel.PanelType != type)
			{
				d.LogError($"Kit Bash Panel prefab {kitBashPanel.name} had ID {kitBashPanel.PanelType} when trying to spawn {type}");
			}
		}
		return kitBashPanel;
	}

	public NetChunk SpawnNetChunk(uint blockPoolID, ChunkTypes type, Vector3 pos, Quaternion rot)
	{
		NetChunk component = m_NetChunkPrefab.transform.Spawn(null, pos, rot).GetComponent<NetChunk>();
		component.OnServerSetChunkType(type);
		component.BlockPoolID = blockPoolID;
		component.name = m_NetChunkPrefab.name + "ID=" + blockPoolID;
		return component;
	}

	public Tank WrapSingleBlock(NetPlayer owner, TankBlock block, int team, string label = "Cab", string author = "")
	{
		Tank tank = SpawnEmptyTechRef(team, block.trans.position, block.trans.rotation, grounded: false, !Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), label).visible.tank;
		tank.blockman.AddBlockToTech(block, IntVector3.zero);
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManLooseBlocks>.inst.HostDestroyNetBlock(block.netBlock);
			tank.Author = (owner.IsNotNull() ? owner.name : author);
			TechData techData = new TechData();
			techData.SaveTech(tank);
			d.Assert(techData.m_BlockSpecs.Count == 1);
			uint[] blockPoolIDs = new uint[1] { block.blockPoolID };
			tank.blockman.Detach(block, allowHeadlessTech: true, rootTransfer: false, propagate: false);
			tank.visible.RemoveFromGame();
			techData.SetNameToPlayerTechCount();
			tank = SpawnNetworkedTechRef(techData, blockPoolIDs, team, block.trans.position, block.trans.rotation, owner, grounded: false, isPopulation: false, shouldExplodeDetachingBlocks: false, 0.5f, hideMarker: false, label).visible.tank;
			d.Assert(block.tank == tank, "Our block has not ended up attached to our NetTech");
		}
		return tank;
	}

	public TrackedVisible SpawnNetworkedTechRef(TechData techData, uint[] blockPoolIDs, int teamID, Vector3 pos, Quaternion rot, NetPlayer playerOwner, bool grounded, bool isPopulation, bool shouldExplodeDetachingBlocks = false, float explodeDetachingBlocksDelay = 0.5f, bool hideMarker = false, string name = null, Action<NetTech> onBeforeServerSpawnTechWithAuthority = null, bool addToObjectManager = true, NetPlayer playerWhoSpawnedTech = null, bool recycleFailedAdds = true)
	{
		TrackedVisible trackedVisible = TTNetworkManager.SpawnEmptyTechRef(teamID, pos, rot, grounded, addToObjectManager);
		Tank tank = trackedVisible.visible.tank;
		NetTech netTech = tank.netTech;
		tank.ShouldExplodeDetachingBlocks = shouldExplodeDetachingBlocks;
		tank.ExplodeDetachingBlocksDelay = explodeDetachingBlocksDelay;
		netTech.OnServerSetupTech(techData, blockPoolIDs, recycleFailedAdds);
		netTech.OnServerSetTeam(tank.Team, isPopulation, justSerialisedValue: true);
		netTech.OnServerSetAuthor((techData != null && techData.Author != "") ? techData.Author : ((playerOwner != null) ? playerOwner.name : ((playerWhoSpawnedTech != null) ? playerWhoSpawnedTech.name : "")));
		if (name != null)
		{
			netTech.OnServerSetName(name);
		}
		netTech.OnServerSetRadarMarker(techData.RadarMarkerConfig);
		if (hideMarker)
		{
			tank.ShowMarker(show: false);
		}
		onBeforeServerSpawnTechWithAuthority?.Invoke(netTech);
		if (playerOwner == null)
		{
			NetworkServer.Spawn(tank.gameObject);
		}
		else
		{
			Singleton.Manager<ManNetTechs>.inst.HostMakePlayerControlTech(playerOwner, netTech);
			NetworkServer.SpawnWithClientAuthority(tank.gameObject, playerOwner.connectionToClient);
		}
		tank.SetTeam(tank.Team, isPopulation);
		return trackedVisible;
	}

	public TrackedVisible SpawnNetworkedTechRef(TechData techData, Vector3 pos, Quaternion rot, int teamID, bool isPopulation, NetPlayer playerOwner = null, Action<NetTech> onBeforeServerSpawnTechWithAuthority = null, NetPlayer playerWhoSpawnedTech = null, bool recycleFailedAdds = true)
	{
		int count = techData.m_BlockSpecs.Count;
		uint[] array = new uint[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = Singleton.Manager<ManNetwork>.inst.GetNextHostBlockPoolID();
		}
		TrackedVisible trackedVisible = SpawnNetworkedTechRef(techData, array, teamID, pos, rot, playerOwner, grounded: false, isPopulation, shouldExplodeDetachingBlocks: false, 0.5f, hideMarker: false, techData.Name, onBeforeServerSpawnTechWithAuthority, addToObjectManager: true, playerWhoSpawnedTech, recycleFailedAdds);
		if (trackedVisible.visible != null)
		{
			d.Assert(trackedVisible.visible.tank, "SpawnNetworkedTechRef Spawned a visible but it wasn't a tank!? Spawned '" + trackedVisible.visible.type.ToString() + "' instead");
			Singleton.Manager<ManLooseBlocks>.inst.RegisterBlockPoolIDsFromTank(trackedVisible.visible.tank);
		}
		return trackedVisible;
	}

	public void SetupNetTech(Tank tech, TechData techData, Dictionary<int, TechComponent.SerialData> saveData, uint[] techDataBlockPoolIDs, bool wrapBlock, bool recycleFailedAdds)
	{
		d.Assert(techData != null);
		d.Assert(techDataBlockPoolIDs != null);
		d.Assert(techData.m_BlockSpecs.Count == techDataBlockPoolIDs.Length);
		SpawnTech_SetNameAndCreator(tech, techData);
		UpdateTankBlocks(ref tech, techData, techDataBlockPoolIDs, null, recycleFailedAdds);
		tech.SerializeEvent.Send(paramA: false, saveData);
		tech.OriginalValue = techData.GetValue();
		tech.MainCorps = techData.GetMainCorporations();
		tech.trans.SetRotationIfChanged((tech.Anchors.NumAnchored > 0) ? Quaternion.Euler(0f, tech.trans.rotation.eulerAngles.y, 0f) : tech.trans.rotation);
		tech.FixupAnchors();
		tech.control.PostSetupNetTech();
	}

	public void UpdateTankBlocks(ref Tank tank, TechData techData, uint[] techDataBlockPoolIDs, int[] visibleIDs, bool recycleFailedAdds = true)
	{
		d.Assert(techData != null);
		d.Assert(techDataBlockPoolIDs != null);
		d.Assert(techData.m_BlockSpecs.Count == techDataBlockPoolIDs.Length);
		if (tank.blockman.blockCount > 0)
		{
			tank.blockman.RecycleAll();
		}
		SpawnAndAddBlocksToTech(ref tank, techData, techDataBlockPoolIDs, visibleIDs, null, recycleFailedAdds);
	}

	public static void RemoveAllBlocksAndTechAroundPosition(Vector3 position, float radius, int techTeam = int.MaxValue)
	{
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(position, radius, kVisibleMaskBlocksAndTech))
		{
			if (item.block != null && item.block.tank == null)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(item.block);
			}
			else if (item.tank != null && (techTeam == int.MaxValue || item.tank.Team == techTeam || (techTeam == -1 && IsEnemyTeam(item.tank.Team))))
			{
				item.RemoveFromGame();
			}
		}
	}

	public static void RemoveAllSceneryAroundPosition(Vector3 scenePos, float radius, SceneryRemovalFlags sceneryRemovalSettings)
	{
		bool spawnChunks = (sceneryRemovalSettings & SceneryRemovalFlags.SpawnNoChunks) == 0;
		bool neverRegrow = (sceneryRemovalSettings & SceneryRemovalFlags.PreventRegrow) != 0;
		bool removeInstant = (sceneryRemovalSettings & SceneryRemovalFlags.RemoveInstant) != 0;
		bool removePersistentDamageStage = (sceneryRemovalSettings & SceneryRemovalFlags.RemovePersistentDamageStage) != 0;
		ManVisible.SearchIterator searchIterator = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(scenePos, radius, kVisibleMaskScenery);
		bool flag = false;
		foreach (Visible item in searchIterator)
		{
			if (item.resdisp != null)
			{
				bool flag2 = true;
				if (Singleton.Manager<ManNetwork>.inst.IsServer && item.tileCache.tile != null && item.tileCache.tile.NetTile.IsNull())
				{
					flag2 = false;
					flag = true;
				}
				if (flag2)
				{
					Vector3 chunkSpawnPos = item.centrePosition + 3f * Vector3.up;
					Vector3 damageDir = item.centrePosition.SetY(0f) - scenePos.SetY(0f);
					item.resdisp.RemoveFromWorld(chunkSpawnPos, damageDir, spawnChunks, neverRegrow, removeInstant, removePersistentDamageStage);
				}
			}
		}
		if (flag)
		{
			d.LogWarning("ResourceDispenser.RemoveAllSceneryAroundPosition - Could not find tile or netTile for some loaded Scenery marked for destruction");
		}
	}

	public string[] GetTankBlockNames()
	{
		return EnumNamesIterator<BlockTypes>.Names;
	}

	public BlockTypes[] GetLoadedTankBlockNames()
	{
		return m_LoadedBlocks.ToArray();
	}

	public bool IsTankBlockLoaded(BlockTypes blockType, bool inlcudeInactiveDLC = false)
	{
		if (inlcudeInactiveDLC)
		{
			return m_LoadedBlocks.Contains(blockType);
		}
		return m_LoadedActiveBlocks.Contains(blockType);
	}

	public bool CanAccessBlockInCurrentMode(BlockTypes blockType)
	{
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory)
		{
			return true;
		}
		if (IsBlockAllowedInLaunchedConfig(blockType) && IsBlockAllowedInCurrentGameMode(blockType))
		{
			return !IsBlockUsageRestrictedInGameMode(blockType);
		}
		return false;
	}

	public bool IsPlayerFacingBlock(BlockTypes blockType)
	{
		if (m_PlayerFacingBlockTypes == null)
		{
			m_PlayerFacingBlockTypes = new HashSet<BlockTypes>();
			Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable().GetAllBlocksUnfiltered(m_PlayerFacingBlockTypes, addBonusBlocks: false);
		}
		return m_PlayerFacingBlockTypes.Contains(blockType);
	}

	public bool IsBlockAvailableForTechSpawn(BlockTypes blockType, InventoryMetaData inventory)
	{
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory))
		{
			return true;
		}
		return Singleton.Manager<ManSpawn>.inst.CanAccessBlockInCurrentMode(blockType) && !inventory.IsLocked && (inventory.IsUnlimited || inventory.m_Inventory.CanConsumeItem(-1, blockType));
	}

	public bool CanLoadTechInCurrentMode(TechData techData)
	{
		if (techData == null)
		{
			return false;
		}
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory))
		{
			return true;
		}
		bool flag = false;
		for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
		{
			BlockTypes blockType = techData.m_BlockSpecs[i].GetBlockType();
			if (!CanAccessBlockInCurrentMode(blockType))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return false;
		}
		return true;
	}

	public void ToggleSecretBlocksDebugSpawn()
	{
		m_SecretBlocksDebugSpawn = !m_SecretBlocksDebugSpawn;
		ResetBlockSpawnMenu();
	}

	public Tank TestSpawnTank(TechData techData, int team, bool replacePlayer)
	{
		bool grounded = true;
		Vector3 velocity = Vector3.zero;
		Vector3 angularVelocity = Vector3.zero;
		Quaternion rotation;
		Vector3 position;
		if ((bool)Singleton.playerTank)
		{
			if (IsPlayerTeam(team) && replacePlayer)
			{
				position = Singleton.playerTank.boundsCentreWorld;
				rotation = Singleton.playerTank.trans.rotation * Quaternion.Inverse(Singleton.playerTank.rootBlockTrans.localRotation);
				grounded = Singleton.playerTank.grounded;
				velocity = Singleton.playerTank.rbody.velocity;
				angularVelocity = Singleton.playerTank.rbody.angularVelocity;
			}
			else
			{
				float num = Singleton.playerTank.blockBounds.extents.magnitude + Mathf.Sqrt(techData.m_BoundsExtents.sqrMagnitude) + 6f;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = (Singleton.cameraTrans.position - Singleton.playerTank.boundsCentreWorld).SetY(0f);
				if (IsPlayerTeam(team))
				{
					rotation = Quaternion.LookRotation(-vector2);
				}
				else
				{
					vector = (new Vector3(UnityEngine.Random.value, 0f, UnityEngine.Random.value) * 2f - Vector3.one.SetY(0f)) * 20f;
					rotation = Quaternion.AngleAxis(UnityEngine.Random.value * 360f, Vector3.up);
				}
				Vector3 scenePos = Singleton.playerTank.boundsCentreWorld + vector2.normalized * num + vector;
				position = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos, hitScenery: true) + Vector3.up;
			}
		}
		else
		{
			position = Singleton.cameraTrans.position + Singleton.cameraTrans.forward * 10f;
			position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up;
			rotation = Quaternion.LookRotation(Singleton.cameraTrans.forward, Vector3.up);
		}
		TankSpawnParams param = new TankSpawnParams
		{
			techData = techData,
			blockIDs = null,
			teamID = team,
			position = position,
			rotation = rotation,
			grounded = grounded
		};
		TrackedVisible trackedVisible = SpawnTankRef(param, addToObjectManager: true);
		trackedVisible.visible.trans.rotation *= Quaternion.Inverse(trackedVisible.visible.tank.rootBlockTrans.localRotation);
		if (IsPlayerTeam(team) && replacePlayer)
		{
			if ((bool)Singleton.playerTank)
			{
				Singleton.playerTank.visible.RemoveFromGame();
				trackedVisible.visible.tank.rbody.velocity = velocity;
				trackedVisible.visible.tank.rbody.angularVelocity = angularVelocity;
			}
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(trackedVisible.visible.tank);
		}
		return trackedVisible.visible.tank;
	}

	public void TestSpawnFriendlyTech(string presetName, bool spawnInFreeSpace = false)
	{
		TankPreset presetFromName = GetPresetFromName(presetName);
		if (presetFromName != null)
		{
			if (spawnInFreeSpace)
			{
				ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
				{
					m_ObjectsToAvoid = AvoidSceneryVehiclesCrates,
					m_AvoidLandmarks = true,
					m_CircleRadius = presetFromName.Radius,
					m_CenterPosWorld = WorldPosition.FromScenePosition(Singleton.playerPos),
					m_CircleIndex = 0,
					m_CameraSpawnConditions = CameraSpawnConditions.Anywhere,
					m_CheckSafeArea = false,
					m_RejectFunc = null
				};
				TechSpawnParams objectSpawnParams = new TechSpawnParams
				{
					m_TechToSpawn = presetFromName.GetTechDataFormatted(),
					m_AIType = null,
					m_Team = 0,
					m_Rotation = Quaternion.identity,
					m_Grounded = true,
					m_SpawnVisualType = SpawnVisualType.Bomb,
					m_DelayBeforeBombSpawn = 0f
				};
				bool autoRetry = false;
				new ObjectSpawner().TrySpawn(objectSpawnParams, freeSpaceParams, null, "DebugTech", autoRetry);
			}
			else
			{
				TestSpawnTank(presetFromName.GetTechDataFormatted(), 0, replacePlayer: false);
			}
		}
		else
		{
			d.LogError("ManSpawn.TestSpawnFriendlyTech - no preset found with name " + presetName);
		}
	}

	public static void Debug_SpawnRecipeEndProduct(ItemTypeInfo output, ref Vector3 position, List<Visible> spawnedIngredientList = null)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && (output.ObjectType == ObjectTypes.Block || output.ObjectType == ObjectTypes.Chunk))
		{
			Singleton.Manager<ManLooseBlocks>.inst.RequestDebugSpawnItem(output.ObjectType, output.ItemType, position, Quaternion.identity);
			position += Vector3.up * 0.4f;
		}
		else if (ManNetwork.IsHost)
		{
			Visible item = Singleton.Manager<ManSpawn>.inst.SpawnItem(output, position, Quaternion.identity, addToObjectManager: false, forceSpawn: true);
			position += Vector3.up * 0.4f;
			spawnedIngredientList?.Add(item);
		}
		else
		{
			d.LogWarningFormat("Debug SpawnRecipeEndProduct call isn't supported for object type {0} on Clients", output.ObjectType);
		}
	}

	public static void Debug_SpawnIngredientsForItem(ItemTypeInfo output, bool recursive, ref Vector3 position, List<Visible> spawnedIngredientList = null)
	{
		RecipeTable.Recipe recipeByOutputType = Singleton.Manager<RecipeManager>.inst.GetRecipeByOutputType(output);
		for (int i = 0; i < recipeByOutputType.m_InputItems.Length; i++)
		{
			ItemTypeInfo item = recipeByOutputType.m_InputItems[i].m_Item;
			bool flag = !recursive || Singleton.Manager<RecipeManager>.inst.GetRecipeByOutputType(item) == null;
			int quantity = recipeByOutputType.m_InputItems[i].m_Quantity;
			for (int j = 0; j < quantity; j++)
			{
				if (flag)
				{
					Debug_SpawnRecipeEndProduct(item, ref position, spawnedIngredientList);
				}
				else
				{
					Debug_SpawnIngredientsForItem(item, recursive, ref position, spawnedIngredientList);
				}
			}
		}
	}

	public static void Debug_AddItemsToSilos(List<Visible> items, Tank targetTech = null)
	{
		if (targetTech.IsNull())
		{
			targetTech = Singleton.playerTank;
		}
		items.Sort((Visible vis1, Visible vis2) => vis1.ItemType.CompareTo(vis2.ItemType));
		BlockManager.BlockIterator<ModuleItemStore>.Enumerator enumerator = targetTech.blockman.IterateBlockComponents<ModuleItemStore>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder component = enumerator.Current.GetComponent<ModuleItemHolder>();
			bool flag = component.IsFlag(ModuleItemHolder.Flags.LinkedStacks);
			ModuleItemHolder.StackIterator.Enumerator enumerator2 = component.Stacks.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ModuleItemHolder.Stack current = enumerator2.Current;
				for (int num = items.Count - 1; num >= 0; num--)
				{
					Visible visible = items[num];
					ModuleItemHolder.Stack stack = current;
					if (flag)
					{
						int num2 = int.MaxValue;
						ModuleItemHolder.StackIterator.Enumerator enumerator3 = component.Stacks.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							ModuleItemHolder.Stack current2 = enumerator3.Current;
							if (current2.NumItems < num2)
							{
								stack = current2;
								num2 = current2.NumItems;
							}
						}
					}
					if (!stack.IsFull && (bool)stack.CanAccept(visible, null, ModuleItemHolder.PassType.Drop))
					{
						stack.Take(visible, force: true);
						visible.trans.position = stack.BasePosWorldOffsetLocal(Vector3.up * (1f + (float)stack.NumItems * 0.7f));
						items.RemoveAt(num);
					}
				}
				if (flag)
				{
					break;
				}
			}
		}
	}

	public static void Debug_RemoveLooseObjects(Vector3 centre, float radius, bool removeSingleCabs = true)
	{
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(centre, radius, removeSingleCabs ? kVisibleMaskBlocksTechAndChunks : kVisibleMaskBlocksAndChunks))
		{
			if (!(item != Singleton.Manager<ManPointer>.inst.DraggingItem))
			{
				continue;
			}
			if (item.block != null && item.block.tank == null)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(item.block, errorOnClientCall: false);
			}
			else if (item.pickup != null)
			{
				if (item.holderStack == null)
				{
					Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(item.pickup);
				}
			}
			else if (removeSingleCabs && item.tank != null && IsPlayerTeam(item.tank.Team) && item.tank != Singleton.playerTank && !item.tank.IsAnchored && item.tank.blockman.blockCount == 1)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(item.tank.blockman.GetRootBlock(), errorOnClientCall: false);
			}
		}
	}

	public static void Debug_ClearAllHeldVisibles()
	{
		if (!(Singleton.playerTank != null))
		{
			return;
		}
		List<Visible> list = new List<Visible>(100);
		TechHolders.HolderIterator enumerator = Singleton.playerTank.Holders.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.Contents.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Visible current = enumerator2.Current;
				list.Add(current);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			Visible visible = list[i];
			if (visible.block.IsNotNull())
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(visible.block, errorOnClientCall: false);
				continue;
			}
			if (visible.pickup.IsNotNull())
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(visible.pickup);
				continue;
			}
			d.LogError("Debug clearing held item that was not a block nor a resource pickup?!");
			visible.trans.Recycle();
		}
	}

	public Vector3 GetSpawnPosWorld(Vector3 screenPos)
	{
		Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(screenPos);
		float num = 10f;
		if ((bool)Singleton.playerTank)
		{
			num = (Singleton.playerPos - Singleton.cameraTrans.position).magnitude;
		}
		if (Singleton.Manager<ManWorld>.inst.RaycastGround(ray, out var hit, num))
		{
			num = hit.distance;
		}
		return ray.origin + ray.direction * num;
	}

	private TankPreset GetPresetFromName(string presetName)
	{
		TankPreset value = null;
		if (tankPresets != null && presetName != null)
		{
			tankPresets.TryGetValue(presetName.ToLower(), out value);
		}
		return value;
	}

	public TerrainObject GetTerrainObjectPrefabFromGUID(string guid)
	{
		return m_TerrainObjectTable.GetPrefabFromSavedGUID(guid);
	}

	public void RemoveBlockFromDictionary(int blockID)
	{
		Transform value;
		if (!Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)blockID))
		{
			d.LogError($"Calling RemoveBlockFromDictionary on a vanilla block with ID {blockID}. For obvious reasons, this is not allowed.");
		}
		else if (m_BlockPrefabs.TryGetValue(blockID, out value))
		{
			m_BlockPrefabs.Remove(blockID);
			int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, blockID);
			VisibleTypeInfo.RemoveDescriptors(hashCode);
		}
		else
		{
			d.LogError($"Could not find block with ID {blockID} to remove from dictionary");
		}
	}

	public void AddBlockToDictionary(GameObject blockPrefab, int blockID)
	{
		Visible component = blockPrefab.GetComponent<Visible>();
		if (!m_BlockPrefabs.ContainsKey(blockID))
		{
			m_BlockPrefabs[blockID] = blockPrefab.transform;
			TankBlock component2 = blockPrefab.GetComponent<TankBlock>();
			StoreBlockCategory((BlockTypes)blockID, component2.BlockCategory);
			StoreBlockRarity((BlockTypes)blockID, component2.BlockRarity);
			ModuleBlockAttributes component3 = component.GetComponent<ModuleBlockAttributes>();
			if (component3 != null)
			{
				component3.InitBlockAttributes(component);
			}
			ModulePlatformRestrictions component4 = component.GetComponent<ModulePlatformRestrictions>();
			if (component4 != null)
			{
				component4.InitPlatformRestrictions(component);
			}
			m_LoadedBlocks.Add((BlockTypes)blockID);
		}
		else
		{
			d.LogError("SpawnTank.FindTankBlocks Dictionary Key clash - Both " + blockPrefab.name + " and " + m_BlockPrefabs[component.m_ItemType.ItemType].name + " share the same types");
		}
	}

	public void AddBlockToDictionary(GameObject blockPrefab)
	{
		if (blockPrefab != null)
		{
			Visible component = blockPrefab.GetComponent<Visible>();
			if (component != null)
			{
				if (component.m_ItemType.ObjectType == ObjectTypes.Block)
				{
					AddBlockToDictionary(blockPrefab, component.m_ItemType.ItemType);
					return;
				}
				d.LogErrorFormat("Prefab {0} ObjectType isn't set to ObjectTypes.Block", blockPrefab.name);
			}
			else
			{
				d.LogErrorFormat("Prefab {0} Does not have a Visible Component", blockPrefab.name);
			}
		}
		else
		{
			d.LogError("SpawnTank.AddBlockToDictionary - blockPrefab is null");
		}
	}

	public void AddPanelToDictionary(KitBashPanel panelPrefab)
	{
		if (panelPrefab == null)
		{
			d.LogError("SpawnTank.AddPanelToDictionary - panelPrefab is null");
			return;
		}
		int panelType = (int)panelPrefab.PanelType;
		if (!m_KitBashPanelPrefabs.ContainsKey(panelType))
		{
			m_KitBashPanelPrefabs[panelType] = panelPrefab;
			m_LoadedKitBashPanels.Add(panelPrefab.PanelType);
			return;
		}
		d.LogError("SpawnTank.FindKitBashPanels Dictionary Key clash - Both " + panelPrefab.name + " and " + m_KitBashPanelPrefabs[(int)panelPrefab.PanelType].name + " share the same types");
	}

	private void FindTankBlocks()
	{
		m_LoadedBlocks.Clear();
		for (int i = 0; i < m_BlockTable.m_Blocks.Count; i++)
		{
			GameObject blockPrefab = m_BlockTable.m_Blocks[i];
			AddBlockToDictionary(blockPrefab);
		}
	}

	private void FindKitBashPanels()
	{
		m_LoadedKitBashPanels.Clear();
		for (int i = 0; i < m_KitBashPanelTable.m_Panels.Count; i++)
		{
			KitBashPanel panelPrefab = m_KitBashPanelTable.m_Panels[i];
			AddPanelToDictionary(panelPrefab);
		}
	}

	public void OnDLCLoadComplete()
	{
		CacheCorporationBlocks();
		GetLoadedActiveBlocks();
		spawnBlockMenu = new MouseMenu();
		spawnBlockMenu.SetDefaultColours(Color.white, Color.green, doubleBG: true);
		BuildSpawnBlockMenuLists();
		BlocksLoaded = true;
	}

	private void FindTankPresets()
	{
		foreach (TankPreset allTechPreset in m_AllTechPresets)
		{
			tankPresets[allTechPreset.name.ToLower()] = allTechPreset;
		}
	}

	private void InitTerrainObjectsLookup()
	{
		m_TerrainObjectTable.InitLookupTable();
	}

	private void InitDebugSpawnMenus()
	{
		spawnPresetMenu = new MouseMenu();
		spawnChunkMenu = new MouseMenu();
		spawnSceneryMenu = new MouseMenu();
		debugNameDisplay = new MouseMenu(followMouse: true, new Vector3((float)Screen.width * 0.02f, (float)Screen.height * 0.02f));
		debugNameDisplay.SetDefaultColours(new Color(255f, 165f, 0f), Color.green, doubleBG: true);
		debugNameDisplay.SetItems(new string[1], 300);
		spawnPresetMenu.SetDefaultColours(Color.white, Color.green, doubleBG: true);
		InitDebugSpawnTechList();
		InitChunkSpawnMenu();
		spawnableScenery = m_TerrainObjectTable._DebugGetSceneryPrefabs();
		string[] array = new string[spawnableScenery.Count];
		for (int i = 0; i < spawnableScenery.Count; i++)
		{
			array[i] = spawnableScenery[i].name;
		}
		spawnSceneryMenu.SetDefaultColours(Color.white, Color.green, doubleBG: true);
		spawnSceneryMenu.SetItems(array);
		Singleton.Manager<ManPointer>.inst.MouseEvent.Subscribe(OnDebugMouse);
	}

	private void BuildSpawnBlockMenuLists()
	{
		m_BlockMenuLists.Clear();
		foreach (FactionSubTypes availableCorporation in Singleton.Manager<ManPurchases>.inst.AvailableCorporations)
		{
			string value = availableCorporation.ToString();
			SpawnBlockMenuList spawnBlockMenuList = new SpawnBlockMenuList();
			spawnBlockMenuList.name = value;
			m_BlockMenuLists.Add(spawnBlockMenuList);
			foreach (BlockTypes loadedBlock in m_LoadedBlocks)
			{
				string text = loadedBlock.ToString();
				if (text.StartsWith(value) && !Secret.Blocktypes.Contains(loadedBlock) && !Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(loadedBlock))
				{
					spawnBlockMenuList.lookup[text] = loadedBlock;
				}
			}
		}
		if (Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
		{
			SpawnBlockMenuList spawnBlockMenuList2 = new SpawnBlockMenuList();
			spawnBlockMenuList2.name = "R&D Only";
			m_BlockMenuLists.Add(spawnBlockMenuList2);
			List<ManDLC.DLC> dLCPacks = Singleton.Manager<ManDLC>.inst.m_DLCTable.DLCPacks;
			for (int i = 0; i < dLCPacks.Count; i++)
			{
				ManDLC.DLC dLC = dLCPacks[i];
				if (dLC.DLCType == ManDLC.DLCType.RandD && Singleton.Manager<ManDLC>.inst.HasDLCEntitlement(dLC))
				{
					for (int j = 0; j < dLC.RestrictedBlocks.Count; j++)
					{
						BlockTypes value2 = dLC.RestrictedBlocks[j];
						string key = value2.ToString();
						spawnBlockMenuList2.lookup[key] = value2;
					}
				}
			}
		}
		m_SecretBlockMenuList = new SpawnBlockMenuList();
		m_SecretBlockMenuList.name = "SECRET";
		m_BlockMenuLists.Add(m_SecretBlockMenuList);
		foreach (BlockTypes item in Secret.Blocktypes.OrderBy((BlockTypes bt) => bt.ToString()))
		{
			string key2 = item.ToString();
			m_SecretBlockMenuList.lookup[key2] = item;
		}
		for (int num = 0; num < m_BlockMenuLists.Count; num++)
		{
			SpawnBlockMenuList spawnBlockMenuList3 = m_BlockMenuLists[num];
			spawnBlockMenuList3.nextList = m_BlockMenuLists[(num + 1) % m_BlockMenuLists.Count];
			spawnBlockMenuList3.prevList = m_BlockMenuLists[(num + m_BlockMenuLists.Count - 1) % m_BlockMenuLists.Count];
			List<string> list = new List<string>();
			list.Add("[[" + spawnBlockMenuList3.name + "]]");
			IOrderedEnumerable<string> collection = spawnBlockMenuList3.lookup.Keys.OrderBy((string k) => k.Replace("_", string.Empty));
			list.AddRange(collection);
			spawnBlockMenuList3.menuStrings = list.ToArray();
		}
		m_SecretBlocksDebugSpawn = false;
		ResetBlockSpawnMenu();
	}

	private void ResetBlockSpawnMenu()
	{
		int num = m_BlockMenuLists.IndexOf(m_SecretBlockMenuList);
		if (num >= 0)
		{
			int index = (num - 1) % m_BlockMenuLists.Count;
			int index2 = (num + 1) % m_BlockMenuLists.Count;
			if (m_SecretBlocksDebugSpawn)
			{
				m_BlockMenuLists[index].nextList = m_BlockMenuLists[num];
				m_BlockMenuLists[index2].prevList = m_BlockMenuLists[num];
			}
			else
			{
				m_BlockMenuLists[index].nextList = m_BlockMenuLists[index2];
				m_BlockMenuLists[index2].prevList = m_BlockMenuLists[index];
			}
		}
		m_CurrentBlockMenuList = m_BlockMenuLists[0];
		spawnBlockMenu.SetItems(m_CurrentBlockMenuList.menuStrings);
		spawnBlockMenu.UpdateItemColour(0, Color.yellow, Color.yellow);
	}

	private void OnDLCChanged()
	{
		BuildSpawnBlockMenuLists();
	}

	private void AdvanceDebugSpawnBlocksList(bool forward)
	{
		m_CurrentBlockMenuList = (forward ? m_CurrentBlockMenuList.nextList : m_CurrentBlockMenuList.prevList);
		spawnBlockMenu.SetItems(m_CurrentBlockMenuList.menuStrings);
		spawnBlockMenu.UpdateItemColour(0, Color.yellow, Color.cyan);
	}

	private void InitDebugSpawnTechList()
	{
		string[] array;
		if (m_SpawnPresetMode == DebugPresetList.Snapshots)
		{
			if (m_DiskSnapshotCollection == null)
			{
				m_DiskSnapshotCollection = Singleton.Manager<ManSnapshots>.inst.ServiceDisk.GetSnapshotCollectionDisk();
			}
			array = new string[m_DiskSnapshotCollection.Snapshots.Count + 1];
			array[0] = "[[ Snapshots ]]";
			for (int i = 0; i < m_DiskSnapshotCollection.Snapshots.Count; i++)
			{
				array[i + 1] = m_DiskSnapshotCollection.Snapshots[i].m_Name.Value;
			}
		}
		else
		{
			array = new string[m_AllTechPresets.Count + 1];
			array[0] = "[[ Presets ]]";
			for (int j = 0; j < m_AllTechPresets.Count; j++)
			{
				array[j + 1] = m_AllTechPresets[j].name;
			}
		}
		spawnPresetMenu.SetItems(array);
		spawnPresetMenu.UpdateItemColour(0, Color.yellow, Color.yellow);
	}

	private void TechPresetMenuSpawnTech(bool replacePlayer)
	{
		if (spawnPresetMenu.SelectedIndex > 0)
		{
			TechData techData = null;
			if (m_SpawnPresetMode == DebugPresetList.Presets)
			{
				techData = GetPresetFromName(spawnPresetMenu.SelectedItem).GetTechDataFormatted();
			}
			else
			{
				d.Assert(m_SpawnPresetMode == DebugPresetList.Snapshots);
				int index = spawnPresetMenu.SelectedIndex - 1;
				techData = m_DiskSnapshotCollection.Snapshots[index].techData;
			}
			DebugSpawnTech(techData, replacePlayer);
		}
	}

	public void DebugSpawnTech(TechData techDataToSpawn, bool replacePlayer, int teamID = 0)
	{
		if (techDataToSpawn == null)
		{
			return;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if ((bool)Singleton.Manager<ManNetwork>.inst.MyPlayer)
			{
				if (replacePlayer)
				{
					Singleton.Manager<ManNetwork>.inst.MyPlayer.ClientRequestReplaceTech(techDataToSpawn, bypassInventory: true);
					return;
				}
				Vector3 normalized = Singleton.cameraTrans.forward.SetY(0f).normalized;
				Vector3 scenePos = Singleton.cameraTrans.position + normalized * 50f;
				scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
				Quaternion rotation = Quaternion.LookRotation(normalized, Vector3.up);
				techDataToSpawn.ValidateBlockSkins();
				SpawnTechMessage message = new SpawnTechMessage
				{
					m_TechData = techDataToSpawn,
					m_Position = WorldPosition.FromScenePosition(in scenePos),
					m_Rotation = rotation,
					m_Team = ((teamID == 0) ? Singleton.Manager<ManNetwork>.inst.MyPlayer.TechTeamID : teamID),
					m_IsPopulation = false,
					m_PlayerNetID = NetworkInstanceId.Invalid,
					m_PlayerWhoCalledSpawn = Singleton.Manager<ManNetwork>.inst.MyPlayer.netId,
					m_CheatBypassInventory = true,
					m_SpawnTechWithUnavailableBlocksMissing = false,
					m_IsSpawnedByPlayer = true
				};
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SpawnTech, message);
			}
		}
		else if (replacePlayer)
		{
			TestSpawnTank(techDataToSpawn, 0, Singleton.Manager<DebugUtil>.inst.newPlayerTankReplacesPrevious);
		}
		else
		{
			Vector3 normalized2 = Singleton.cameraTrans.forward.SetY(0f).normalized;
			Vector3 scenePos2 = Singleton.cameraTrans.position + normalized2 * 50f;
			scenePos2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos2);
			Quaternion rotation2 = Quaternion.LookRotation(normalized2, Vector3.up);
			TankSpawnParams param = new TankSpawnParams
			{
				techData = techDataToSpawn,
				blockIDs = null,
				teamID = teamID,
				position = scenePos2,
				rotation = rotation2,
				grounded = true
			};
			SpawnTankRef(param, addToObjectManager: true);
		}
	}

	private void InitChunkSpawnMenu()
	{
		int num = EnumNamesIterator<ChunkTypes>.Names.Length;
		chunkTypesLookup = new List<ChunkTypes>(num);
		List<string> chunkNames = new List<string>(num);
		List<Color> chunkColours = new List<Color>(num);
		List<Color> chunkHiColours = new List<Color>(num);
		Action<string> action = delegate(string title)
		{
			chunkNames.Add("[[ " + title + " ]]");
			chunkColours.Add(Color.yellow);
			chunkHiColours.Add(Color.yellow);
			chunkTypesLookup.Add(ChunkTypes.Null);
		};
		bool flag = false;
		action("Resources");
		for (int num2 = 0; num2 < num; num2++)
		{
			string text = EnumNamesIterator<ChunkTypes>.Names[num2];
			if (num2 != 42 && !text.Contains("_deprecated"))
			{
				int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Chunk, num2);
				bool flag2 = VisibleTypeInfo.IsDescriptorFlag(hashCode, typeof(ChunkCategory), 4);
				if (flag != flag2)
				{
					d.Assert(!flag, "InitChunkSpawnMenu - Was expecting all Chunks to come before Components. Instead found non-Component " + text + " after previous Components! Debug spawn menu titles will be incorrect");
					flag = flag2;
					action("Components");
				}
				Color item = Color.white;
				Color green = Color.green;
				int enumIntValue2;
				if (VisibleTypeInfo.TryGetDescriptor<ComponentTier>(hashCode, out var enumIntValue))
				{
					item = (ComponentTier)enumIntValue switch
					{
						ComponentTier.Advanced => new Color(1f, 1f, 0.7f), 
						ComponentTier.Complex => new Color(1f, 0.7f, 0.7f), 
						ComponentTier.Exotic => new Color(1f, 0.7f, 1f), 
						_ => Color.white, 
					};
				}
				else if (VisibleTypeInfo.TryGetDescriptor<ChunkRarity>(hashCode, out enumIntValue2))
				{
					bool flag3 = VisibleTypeInfo.IsDescriptorFlag(hashCode, typeof(ChunkCategory), 2);
					item = (ChunkRarity)enumIntValue2 switch
					{
						ChunkRarity.Uncommon => flag3 ? new Color(1f, 1f, 0.5f) : new Color(1f, 1f, 0.7f), 
						ChunkRarity.Rare => flag3 ? new Color(1f, 0.6f, 0.6f) : new Color(1f, 0.8f, 0.8f), 
						_ => flag3 ? Color.white : Color.white.ScaleRGB(0.8f), 
					};
				}
				chunkNames.Add(text);
				chunkColours.Add(item);
				chunkHiColours.Add(green);
				chunkTypesLookup.Add((ChunkTypes)num2);
			}
		}
		spawnChunkMenu.SetDefaultColours(Color.white, Color.green, doubleBG: true);
		spawnChunkMenu.SetItems(chunkNames, chunkColours, chunkHiColours);
	}

	private void GetLoadedActiveBlocks()
	{
		m_LoadedActiveBlocks.Clear();
		for (int i = 0; i < m_LoadedBlocks.Count; i++)
		{
			BlockTypes blockTypes = m_LoadedBlocks[i];
			if (!Singleton.Manager<ManDLC>.inst.IsBlockDLC(blockTypes, out var dlcType) || Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(dlcType))
			{
				m_LoadedActiveBlocks.Add(blockTypes);
			}
		}
	}

	private void StripComponents(GameObject item, HashSet<Type> strippedTypes)
	{
		Component[] components = item.GetComponents<Component>();
		for (int num = components.Length - 1; num >= 0; num--)
		{
			Component component = components[num];
			if (strippedTypes.Contains(component.GetType()))
			{
				UnityEngine.Object.DestroyImmediate(component);
			}
		}
	}

	private void FixCollisionLayers(GameObject go, int layer, ref List<string> correctedObjectNames)
	{
		if (go.layer == 0 && layer != 0)
		{
			if (correctedObjectNames == null)
			{
				correctedObjectNames = new List<string>();
			}
			correctedObjectNames.Add(go.name);
			go.layer = layer;
		}
		foreach (Transform item in go.transform)
		{
			FixCollisionLayers(item.gameObject, layer, ref correctedObjectNames);
		}
	}

	private void FixOrphanMeshFilters(GameObject go, ref List<string> correctedObjectNames)
	{
		if ((bool)go.GetComponent<MeshFilter>() && !go.GetComponent<MeshRenderer>() && !go.GetComponent<MeshCollider>())
		{
			if (correctedObjectNames == null)
			{
				correctedObjectNames = new List<string>();
			}
			correctedObjectNames.Add(go.name);
		}
		foreach (Transform item in go.transform)
		{
			FixOrphanMeshFilters(item.gameObject, ref correctedObjectNames);
		}
	}

	public int GenerateAutomaticTeamID(int teamID)
	{
		if (teamID == -1)
		{
			teamID = m_TeamCounter;
			m_TeamCounter++;
		}
		return teamID;
	}

	public void SetTankController(Tank tank)
	{
		TankControl component = tank.GetComponent<TankControl>();
		component.targetType = ObjectTypes.Vehicle;
		if (tank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam)
		{
			int controllerIndex = tank.Team;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				controllerIndex = 0;
			}
			component.SetControllerIndex(controllerIndex);
		}
	}

	public BlockTypes GetAliasedBlockID(BlockTypes blockType)
	{
		if (m_BlockAliasing.TryGetValue(blockType, out var value))
		{
			blockType = value;
		}
		return blockType;
	}

	private void SpawnTech_SetNameAndCreator(Tank tank, TechData techData)
	{
		if (techData.HasLocalisedName)
		{
			tank.SetLocalisedName(techData.LocalisedName);
		}
		else
		{
			tank.name = techData.Name;
		}
		tank.m_Creator = techData.m_CreationData.m_Creator;
	}

	private Tank SpawnTankFromTechData(TankSpawnParams param)
	{
		TechData techData = param.techData;
		if (techData == null)
		{
			d.Assert(condition: false, "ManSpawn.SpawnTankFromPreset - no TechData,  and preset is null");
			return null;
		}
		if (param.grounded)
		{
			param.position = Singleton.Manager<ManWorld>.inst.ProjectToGround(param.position, hitScenery: true) + Vector3.up;
		}
		Debug_SpawningTechName = techData.Name;
		Tank tank = m_TankRuntimePrefabs.stdPrefab.Spawn(param.position, param.rotation).GetComponent<Tank>();
		Debug_SpawningTechName = null;
		int num = GenerateAutomaticTeamID(param.teamID);
		tank.SetTeam(num, param.isPopulation);
		tank.ShouldExplodeDetachingBlocks = param.shouldExplodeDetachingBlocks;
		tank.ExplodeDetachingBlocksDelay = param.explodeDetachingBlocksDelay;
		if (param.hideMarker)
		{
			tank.ShowMarker(show: false);
		}
		SpawnTech_SetNameAndCreator(tank, techData);
		tank.RadarMarker.RadarMarkerConfig = techData.RadarMarkerConfig;
		uint[] techDataBlockPoolIDs = new uint[param.techData.m_BlockSpecs.Count];
		Singleton.Manager<ManLooseBlocks>.inst.GenerateBlockPoolIDs(ref techDataBlockPoolIDs);
		SpawnAndAddBlocksToTech(ref tank, param.techData, techDataBlockPoolIDs, param.blockIDs, param.inventory);
		Singleton.Manager<ManLooseBlocks>.inst.RegisterBlockPoolIDsFromTank(tank);
		if (tank.blockman.blockCount == 0)
		{
			d.LogError("Failed to spawn tech '" + techData.Name + "' - Was unable to add any blocks to tech!");
			tank.visible.RemoveFromGame();
			tank = null;
		}
		else
		{
			if (param.isInvulnerable)
			{
				tank.SetInvulnerable(invulnerable: true, forever: true);
			}
			tank.SerializeEvent.Send(paramA: false, techData.m_TechSaveState);
			tank.OriginalValue = (param.hasRewardValue ? techData.GetValue() : 0);
			tank.MainCorps = techData.GetMainCorporations();
			tank.ShouldExplodeDetachingBlocks = param.shouldExplodeDetachingBlocks;
			tank.ExplodeDetachingBlocksDelay = param.explodeDetachingBlocksDelay;
			tank.trans.SetRotationIfChanged((tank.Anchors.NumAnchored > 0) ? Quaternion.Euler(0f, tank.trans.rotation.eulerAngles.y, 0f) : tank.trans.rotation);
			switch (param.placement)
			{
			case TankSpawnParams.Placement.BaseCentredAtPosition:
				tank.PositionBaseCentred(tank.trans.position);
				break;
			case TankSpawnParams.Placement.BoundsCentredAtPosition:
				tank.boundsCentreWorld = tank.trans.position;
				break;
			default:
				d.AssertFormat(false, "ManSpawn.SpawnTankFromTechData does not know how to handle placement type {0}", param.placement);
				break;
			case TankSpawnParams.Placement.PlacedAtPosition:
				break;
			}
			SetTankController(tank);
			if (IsPlayerTeam(num))
			{
				tank.DiscoverBlocksOnTech();
			}
			tank.FixupAnchors();
		}
		return tank;
	}

	private void SpawnAndAddBlocksToTech(ref Tank tank, TechData techData, uint[] techDataBlockPoolIDs, int[] visibleIDs, IInventory<BlockTypes> inventory, bool recycleFailedAdds = true)
	{
		d.Assert(techData != null);
		d.Assert(techDataBlockPoolIDs != null);
		d.Assert(techData.m_BlockSpecs.Count == techDataBlockPoolIDs.Length);
		if (visibleIDs != null)
		{
			bool num = visibleIDs.Length == techData.m_BlockSpecs.Count;
			d.Assert(num, "ManSpawn:AddPresetBlocks() has been past a visibleIDs array of incorrect length");
			if (!num)
			{
				visibleIDs = null;
			}
		}
		Action<TankBlock, TankPreset.BlockSpec> addFailedHandler = null;
		uint spawnShieldId;
		Transform techTrans;
		if (!recycleFailedAdds && Singleton.Manager<ManNetwork>.inst.IsServerOrWillBe)
		{
			spawnShieldId = (tank.netTech.IsNotNull() ? tank.netTech.InitialSpawnShieldID : 0u);
			techTrans = tank.trans;
			addFailedHandler = FixupFailedToAddBlocks;
		}
		using (PopulateTechHelper populateTechHelper = new PopulateTechHelper(tank, spawningNew: true, recycleFailedAdds, inventory, allowHeadlessTech: false, tryDeployAnchors: false, reportFailure: true, allowAttachBlocksWithoutLinks: false, addFailedHandler))
		{
			for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
			{
				BlockTypes blockType = techData.m_BlockSpecs[i].GetBlockType();
				blockType = GetAliasedBlockID(blockType);
				bool flag = (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory)) || (IsBlockAllowedInLaunchedConfig(blockType) && IsBlockAllowedInCurrentGameMode(blockType));
				TankBlock tankBlock = null;
				bool flag2 = true;
				string overrideFailMsg = null;
				if (!flag)
				{
					overrideFailMsg = "UNAVAILABLE";
					flag2 = false;
				}
				else if (inventory != null)
				{
					int quantity = inventory.GetQuantity(blockType);
					if (quantity != -1)
					{
						if (quantity > 0)
						{
							inventory.HostConsumeItem(-1, blockType);
						}
						else
						{
							overrideFailMsg = "INVENTORY CHECK";
							flag2 = false;
						}
					}
				}
				if (flag2)
				{
					if (visibleIDs != null)
					{
						d.Assert(visibleIDs[i] > 0, "Visible ID isn't valid");
						Singleton.Manager<ManSaveGame>.inst.CurrentState.OverrideNextVisibleID(visibleIDs[i]);
					}
					Visible.DisableAddToTileOnSpawn = true;
					tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindOrSpawnBlockForTech(blockType, techDataBlockPoolIDs[i]);
					if (tankBlock.IsNotNull())
					{
						tankBlock.SetSkinByUniqueID(techData.GetSkinID(i), blockType);
					}
					Visible.DisableAddToTileOnSpawn = false;
					if (visibleIDs != null)
					{
						Singleton.Manager<ManSaveGame>.inst.CurrentState.EnsureNextVisibleIDOverrideIsCleared();
					}
				}
				populateTechHelper.AddBlock(tankBlock, techData.m_BlockSpecs[i], alreadyAttached: false, overrideFailMsg);
			}
		}
		void FixupFailedToAddBlocks(TankBlock looseBlock, TankPreset.BlockSpec blockSpec)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostConvertToNetBlock(looseBlock, spawnShieldId);
			}
			Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(looseBlock.visible, forceNow: true);
			looseBlock.InitRigidbody();
			Vector3 position = techTrans.TransformPoint(blockSpec.position);
			Quaternion rotation = new OrthoRotation(blockSpec.orthoRotation) * techTrans.rotation;
			looseBlock.trans.SetPositionAndRotation(position, rotation);
		}
	}

	private Visible SpawnItemInternal(ItemTypeInfo typeInfo, Vector3 position, Quaternion rotation, bool initNew = true)
	{
		Visible visible = null;
		switch (typeInfo.ObjectType)
		{
		case ObjectTypes.Chunk:
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			ResourcePickup resourcePickup = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnChunk((ChunkTypes)typeInfo.ItemType, position, rotation, initNew, zero, zero2);
			if ((bool)resourcePickup)
			{
				visible = resourcePickup.visible;
			}
			break;
		}
		case ObjectTypes.Block:
		{
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock((BlockTypes)typeInfo.ItemType, position, rotation, initNew);
			if ((bool)tankBlock)
			{
				visible = tankBlock.visible;
			}
			break;
		}
		case ObjectTypes.Waypoint:
		{
			Vector3 position2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up * 1f;
			Waypoint waypoint = HostSpawnWaypoint(position2, rotation);
			if ((bool)waypoint)
			{
				visible = waypoint.visible;
			}
			break;
		}
		default:
			d.Assert(condition: false, "Visible.Spawn Type " + typeInfo.ObjectType.ToString() + " UNIMPLEMENTED");
			break;
		}
		if (!visible)
		{
			d.LogError("spawn failed: " + typeInfo.name);
		}
		return visible;
	}

	public Waypoint HostSpawnWaypoint(Vector3 position, Quaternion rotation)
	{
		d.Assert(ManNetwork.IsHost, "Can't call HostSpawnWaypoint on client");
		Waypoint component;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			component = m_NetWaypointPrefab.transform.Spawn(position, rotation).GetComponent<Waypoint>();
			if (ManNetwork.IsHost && component.netWaypoint.IsNotNull())
			{
				component.netWaypoint.HostID = component.visible.ID;
			}
		}
		else
		{
			component = m_WaypointPrefab.transform.Spawn(position, rotation).GetComponent<Waypoint>();
		}
		d.Assert(component != null, "ManSpawn.HostSpawnWaypoint - Failed to spawn Waypoint");
		return component;
	}

	private TrackedVisible SpawnEmptyTechRefInternal(Transform prefab, int team, Vector3 position, Quaternion rotation, bool grounded, string techName, bool addToObjectManager)
	{
		if (grounded)
		{
			position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up;
		}
		Debug_SpawningTechName = techName;
		Tank component = prefab.Spawn(null, position, rotation).GetComponent<Tank>();
		Debug_SpawningTechName = null;
		component.name = techName;
		int team2 = GenerateAutomaticTeamID(team);
		component.SetTeam(team2);
		SetTankController(component);
		TrackedVisible trackedVisible = CreateTrackedVisibleForVehicle(component.visible.ID, component.visible, position, RadarTypes.Vehicle);
		if (ManNetwork.IsHost && component.netTech.IsNotNull())
		{
			component.netTech.HostID = trackedVisible.ID;
		}
		if (addToObjectManager)
		{
			Singleton.Manager<ManVisible>.inst.TrackVisible(trackedVisible);
		}
		return trackedVisible;
	}

	public Transform LookupOrigPrefab(Transform prefab)
	{
		if (!m_OrigPrefabLookup.TryGetValue(prefab, out var value))
		{
			return prefab;
		}
		return value;
	}

	private PrefabPair SetupNetworkedPrefab(Transform original, HashSet<Type> strippedTypes)
	{
		if (m_OrigPrefabLookup.ContainsValue(original))
		{
			Transform[] array = m_OrigPrefabLookup.Where((KeyValuePair<Transform, Transform> r) => r.Value == original).ToDictionary((KeyValuePair<Transform, Transform> t) => t.Key, (KeyValuePair<Transform, Transform> t) => t.Value).Keys.ToArray();
			return new PrefabPair
			{
				stdPrefab = array[0],
				netPrefab = array[1]
			};
		}
		Transform transform = UnityEngine.Object.Instantiate(original);
		StripComponents(transform.gameObject, strippedTypes);
		transform.SetParent(m_RuntimePrefabsContainer);
		transform.name = original.name;
		Singleton.Manager<ComponentPool>.inst.RegisterStandardRuntimePrefab(transform, original);
		Transform transform2 = UnityEngine.Object.Instantiate(original);
		transform2.SetParent(m_RuntimePrefabsContainer);
		transform2.name = original.name + "(Net)";
		m_OrigPrefabLookup.Add(transform, original);
		m_OrigPrefabLookup.Add(transform2, original);
		Singleton.Manager<ComponentPool>.inst.RegisterNetworkRuntimePrefab(transform2, original);
		Singleton.Manager<ManNetwork>.inst.RegisterNetworkObject(transform2);
		return new PrefabPair
		{
			stdPrefab = transform,
			netPrefab = transform2
		};
	}

	private static HashSet<Type> CreateStrippedTypesSet(List<string> typeStrings)
	{
		HashSet<Type> hashSet = new HashSet<Type>();
		for (int i = 0; i < typeStrings.Count; i++)
		{
			try
			{
				bool throwOnError = true;
				Type type = Type.GetType(typeStrings[i], throwOnError);
				hashSet.Add(type);
			}
			catch (Exception)
			{
				d.LogError("ERROR Unable to find stripped component type " + typeStrings[i]);
			}
		}
		return hashSet;
	}

	public bool OnPreInitEditor()
	{
		return false;
	}

	private void OnDebugMouse(ManPointer.Event mouseEvent, bool touchDown, bool clicked)
	{
		switch (mouseEvent)
		{
		case ManPointer.Event.RMB:
			if (!Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development))
			{
				break;
			}
			if (!Singleton.Manager<ManPointer>.inst.DraggingItem)
			{
				if (touchDown)
				{
					if ((!Singleton.Manager<DebugUtil>.inst.DebugControlBlocked && Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl))
					{
						spawnPresetMenu.Show(Input.mousePosition + new Vector3(16f, -16f, 0f));
						Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.PresetMenu, blocked: true);
						if (spawnChunkMenu.IsActive)
						{
							spawnChunkMenu.Hide();
						}
						if (spawnBlockMenu.IsActive)
						{
							spawnBlockMenu.Hide();
						}
						if (spawnSceneryMenu.IsActive)
						{
							spawnSceneryMenu.Hide();
						}
						if (debugNameDisplay.IsActive)
						{
							debugNameDisplay.Hide();
						}
					}
				}
				else if (spawnPresetMenu.IsActive)
				{
					TechPresetMenuSpawnTech(replacePlayer: true);
					spawnPresetMenu.Hide();
					Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.PresetMenu, blocked: false);
				}
			}
			if (!touchDown && debugNameDisplay.IsActive)
			{
				debugNameDisplay.GetItemName(0).CopyToClipboard();
				debugNameDisplay.UpdateItemSpotlit(0, state: true);
			}
			break;
		case ManPointer.Event.LMB:
			if (!touchDown)
			{
				break;
			}
			if (spawnBlockMenu.IsActive)
			{
				if (spawnBlockMenu.SelectedIndex > 0)
				{
					BlockTypes blockType = m_CurrentBlockMenuList.lookup[spawnBlockMenu.SelectedItem];
					IsSpawningDebugPaintingBlock = true;
					Singleton.Manager<ManPointer>.inst.DebugSpawnBlock(blockType);
					IsSpawningDebugPaintingBlock = false;
				}
			}
			else if (spawnChunkMenu.IsActive)
			{
				if (spawnChunkMenu.SelectedIndex != -1)
				{
					ChunkTypes chunkTypes = chunkTypesLookup[spawnChunkMenu.SelectedIndex];
					if (chunkTypes != ChunkTypes.Null)
					{
						Vector3 scenePos = GetSpawnPosWorld(Input.mousePosition) + new Vector3(0f, 1f, 0f);
						Singleton.Manager<ManLooseBlocks>.inst.RequestDebugSpawnItem(ObjectTypes.Chunk, (int)chunkTypes, scenePos, Quaternion.identity);
					}
				}
			}
			else if (spawnSceneryMenu.IsActive)
			{
				int selectedIndex = spawnSceneryMenu.SelectedIndex;
				TerrainObject component = spawnableScenery[selectedIndex].GetComponent<TerrainObject>();
				if (component != null)
				{
					Vector3 scenePos2 = (Singleton.playerTank ? Singleton.playerPos : Singleton.cameraTrans.position);
					scenePos2 += Singleton.cameraTrans.forward.SetY(0f) * 15f;
					Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos2);
					Quaternion rot = component.GetNormalWeightedSpawnOrientation(rotationHV: new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)), groundNormal: Vector3.up);
					TerrainObject.SpawnedTerrainObjectData spawnedTerrainObjectData = component.SpawnFromPrefabAndAddToSaveData(scenePos2, rot);
					d.Assert(spawnedTerrainObjectData.TerrainObject != null && spawnedTerrainObjectData.TerrainObject.visible != null, "Failed to spawn Scenery '" + component.name + "'.");
				}
				else
				{
					d.LogError("Failed to find spawnable prefab for scenery '" + spawnSceneryMenu.SelectedItem + "'");
				}
			}
			else if (spawnPresetMenu.IsActive)
			{
				TechPresetMenuSpawnTech(replacePlayer: false);
			}
			else
			{
				if (!Input.GetKey(KeyCode.LeftAlt) || !Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development))
				{
					break;
				}
				UIPaletteBlockSelect uIPaletteBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
				if (!(uIPaletteBlockSelect == null) && uIPaletteBlockSelect.IsTemporaryPaintLockout)
				{
					break;
				}
				Tank targetTank = Singleton.Manager<ManPointer>.inst.targetTank;
				if (!(targetTank != null))
				{
					break;
				}
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
				{
					if (targetTank == Singleton.playerTank)
					{
						Singleton.Manager<ManNetwork>.inst.MyPlayer.RequestCycleTeam();
					}
					else if (targetTank.netTech.NetPlayer.IsNull())
					{
						targetTank.netTech.RequestCycleTeam();
					}
				}
				else if (targetTank != Singleton.playerTank)
				{
					if (!IsPlayerTeam(targetTank.Team))
					{
						targetTank.SetTeam(0);
						targetTank.AI.SetBehaviorType(AITreeType.AITypes.Idle);
					}
					else
					{
						targetTank.SetTeam(GenerateAutomaticTeamID(-1));
						targetTank.AI.SetOldBehaviour();
					}
				}
			}
			break;
		case ManPointer.Event.MWheel:
			if (spawnBlockMenu.IsActive)
			{
				AdvanceDebugSpawnBlocksList(!touchDown);
			}
			else if (spawnPresetMenu.IsActive)
			{
				m_SpawnPresetMode = ((m_SpawnPresetMode == DebugPresetList.Presets) ? DebugPresetList.Snapshots : DebugPresetList.Presets);
				InitDebugSpawnTechList();
			}
			break;
		case ManPointer.Event.MMB:
			break;
		}
	}

	private void OnClientSpawnDeliveryBombVisual(NetworkMessage netMsg)
	{
		SpawnDeliveryBombVisualMessage spawnDeliveryBombVisualMessage = netMsg.ReadMessage<SpawnDeliveryBombVisualMessage>();
		SpawnDeliveryBombNew(spawnDeliveryBombVisualMessage.m_Position.ScenePosition, spawnDeliveryBombVisualMessage.m_ImpactMarkerType, spawnDeliveryBombVisualMessage.m_Delay);
	}

	private void OnCustomSpawnEffectRequest(NetworkMessage netMsg)
	{
		CustomSpawnEffectRequest customSpawnEffectRequest = netMsg.ReadMessage<CustomSpawnEffectRequest>();
		ParticleSystem customSpawnEffectPrefabs = Singleton.Manager<ManSpawn>.inst.GetCustomSpawnEffectPrefabs(customSpawnEffectRequest.m_CustomSpawnEffectType);
		if ((bool)customSpawnEffectPrefabs)
		{
			customSpawnEffectPrefabs.transform.Spawn(customSpawnEffectRequest.m_Position);
		}
	}

	private void Awake()
	{
		VisibleTypeInfo = new EnumDescriptorTable();
		BlocksLoaded = false;
		List<UnityEngine.Object> objects = tankPresetList.objects;
		m_AllTechPresets = new List<TankPreset>(objects.Count);
		for (int i = 0; i < objects.Count; i++)
		{
			m_AllTechPresets.Add((TankPreset)objects[i]);
		}
		m_CorpCratePrefabsDict = new Dictionary<FactionSubTypes, Crate>(default(FactionSubTypesComparer));
		for (int j = 0; j < m_CorpCratePrefabs.Length; j++)
		{
			for (int k = 0; k < m_CorpCratePrefabs[j].FactionTypes.Length; k++)
			{
				if (!m_CorpCratePrefabsDict.ContainsKey(m_CorpCratePrefabs[j].FactionTypes[k]))
				{
					m_CorpCratePrefabsDict.Add(m_CorpCratePrefabs[j].FactionTypes[k], m_CorpCratePrefabs[j].Prefab);
				}
			}
		}
	}

	private void Start()
	{
		BlockLimit = BlockManager.DefaultBlockLimit;
		m_StrippedTankTypes = CreateStrippedTypesSet(m_StrippedTankTypesList);
		m_StrippedCrateTypes = CreateStrippedTypesSet(m_StrippedCrateTypesList);
		m_RuntimePrefabsContainer = new GameObject("_runtimeprefabs").transform;
		m_RuntimePrefabsContainer.parent = base.transform;
		m_RuntimePrefabsContainer.gameObject.SetActive(value: false);
		d.Assert(m_TankPrefab);
		Singleton.Manager<ManNetwork>.inst.RegisterNetworkObject(m_NetBlockPrefab.transform);
		Singleton.Manager<ManNetwork>.inst.RegisterNetworkObject(m_NetChunkPrefab.transform);
		Singleton.Manager<ManNetwork>.inst.RegisterNetworkObject(m_NetWaypointPrefab.transform);
		if (m_TankPrefab != null)
		{
			m_TankRuntimePrefabs = SetupNetworkedPrefab(m_TankPrefab, m_StrippedTankTypes);
		}
		else
		{
			d.LogError("ManSpawn - Missing TankPrefab");
		}
		if (m_CorpCratePrefabsDict != null && m_CorpCratePrefabsDict.Count > 0)
		{
			m_CorpCrateRuntimePrefabsDict = new Dictionary<FactionSubTypes, PrefabPair>(default(FactionSubTypesComparer));
			FactionSubTypes[] array = m_CorpCratePrefabsDict.Keys.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				PrefabPair value = SetupNetworkedPrefab(m_CorpCratePrefabsDict[array[i]].transform, m_StrippedCrateTypes);
				m_CorpCrateRuntimePrefabsDict.Add(array[i], value);
			}
		}
		else
		{
			d.LogError("ManSpawn - Missing CorpCratePrefabs");
		}
		FindTankBlocks();
		FindKitBashPanels();
		FindTankPresets();
		InitTerrainObjectsLookup();
		Singleton.instance.DoOnNextFrame(InitDebugSpawnMenus);
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Subscribe(OnDLCChanged);
		ReplacePartNames.Add("EXPFan_221", "GSOFan_221");
		ReplacePartNames.Add("EXPFuelTank_222", "GSOFuelTank_222");
		ReplacePartNames.Add("EXPWingTail_122", "GSOWingTail_122");
		ReplacePartNames.Add("EXPMegaBooster_112", "GSOMegaBooster_112");
		ReplacePartNames.Add("EXPWingLeft_312", "GSOWingLeft_312");
		ReplacePartNames.Add("EXPWingMini_311", "GSOWingMini_311");
		ReplacePartNames.Add("EXPWingRight_312", "GSOWingRight_312");
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Subscribe(OnModeSwitch);
		ModuleAnchor.AnchorOnAttach = ModuleAnchor.OnAttachBehaviour.FirstOrSameAs;
		ModuleItemConveyor.LinkOnAttach = true;
		DeprecatedBlockReplacement[] deprecatedBlockReplacement = m_DeprecatedBlockReplacement;
		for (int j = 0; j < deprecatedBlockReplacement.Length; j++)
		{
			DeprecatedBlockReplacement deprecatedBlockReplacement2 = deprecatedBlockReplacement[j];
			d.Assert(deprecatedBlockReplacement2.toReplace.ObjectType == ObjectTypes.Block && deprecatedBlockReplacement2.replaceWith.ObjectType == ObjectTypes.Block, "non-blocks in deprecated block replacement list");
			m_DeprecatedBlockReplacementLookup[deprecatedBlockReplacement2.toReplace] = deprecatedBlockReplacement2.replaceWith;
		}
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.SpawnDeliveryBombVisual, OnClientSpawnDeliveryBombVisual);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.CustomSpawnEffectRequest, OnCustomSpawnEffectRequest);
		OnDLCLoadComplete();
	}

	private void OnDestroy()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Unsubscribe(OnModeSwitch);
	}

	private void Update()
	{
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.PublicFacing))
		{
			bool showBlock = false;
			bool showChunk = false;
			bool showScenery = false;
			bool showDebugNameDisplay = false;
			if (!Singleton.Manager<ManPointer>.inst.DraggingItem && Input.GetKey(KeyCode.LeftControl))
			{
				if (Input.GetKey(Globals.inst.m_BlockSpawnKey))
				{
					showBlock = true;
				}
				if (Input.GetKey(Globals.inst.m_ChunkSpawnKey))
				{
					showChunk = true;
				}
				if (Input.GetKey(Globals.inst.m_ResourceSpawnKey))
				{
					showScenery = true;
				}
			}
			else if (Input.GetKey(Globals.inst.m_ShowDebugNameKey) && Singleton.Manager<ManPointer>.inst.targetVisible != null && (Globals.inst.m_ShowDebugNameKey != KeyCode.LeftAlt || !Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded()))
			{
				debugNameDisplay.UpdateItemText(0, Singleton.Manager<ManPointer>.inst.targetVisible.gameObject.name);
				debugNameDisplay.UpdateItemSpotlit(0, Singleton.Manager<ManPointer>.inst.targetVisible.gameObject.name == GUIUtility.systemCopyBuffer);
				showDebugNameDisplay = true;
			}
			ShowSpawnItemMenu(showBlock, showChunk, showScenery, showDebugNameDisplay);
			if (DebugSpawnMenuActive)
			{
				if (m_GUICallbackObject == null)
				{
					m_GUICallbackObject = OnGUICallback.AddGUICallback(base.gameObject);
					m_GUICallbackObject.OnGUIEvent.Subscribe(DebugGUIDraw);
				}
			}
			else if (m_GUICallbackObject != null)
			{
				m_GUICallbackObject.OnGUIEvent.Unsubscribe(DebugGUIDraw);
				OnGUICallback.RemoveGUICallback(m_GUICallbackObject);
			}
		}
		if (DebugSpawnMenuActive)
		{
			UpdateMousePosition();
		}
		m_RewardSpawner.Update();
	}

	private void ShowSpawnItemMenu(bool showBlock, bool showChunk, bool showScenery, bool showDebugNameDisplay)
	{
		showChunk = !showBlock && showChunk;
		showScenery = !(showBlock || showChunk) && showScenery;
		showDebugNameDisplay = !showBlock && !showChunk && !showScenery && showDebugNameDisplay;
		ShowMouseMenu(spawnBlockMenu, showBlock);
		ShowMouseMenu(spawnChunkMenu, showChunk);
		ShowMouseMenu(spawnSceneryMenu, showScenery);
		ShowMouseMenu(debugNameDisplay, showDebugNameDisplay);
		if ((showBlock || showChunk || showScenery || showDebugNameDisplay) && spawnPresetMenu.IsActive)
		{
			spawnChunkMenu.Hide();
			debugNameDisplay.Hide();
		}
		Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.BlockMenu, showBlock);
		Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.ChunkMenu, showChunk);
		Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.SceneryMenu, showScenery);
		Singleton.Manager<DebugUtil>.inst.SetDebugControlBlocked(DebugUtil.DebugControlBlockedReason.DebugBlockNameDisplay, showDebugNameDisplay);
	}

	private void ShowMouseMenu(MouseMenu mouseMenu, bool show)
	{
		if (show)
		{
			if (!mouseMenu.IsActive)
			{
				mouseMenu.Show(Input.mousePosition);
			}
		}
		else if (mouseMenu.IsActive)
		{
			mouseMenu.Hide();
		}
	}

	private void UpdateMousePosition()
	{
		spawnBlockMenu?.UpdateMousePosition(Input.mousePosition);
		spawnChunkMenu?.UpdateMousePosition(Input.mousePosition);
		spawnSceneryMenu?.UpdateMousePosition(Input.mousePosition);
		debugNameDisplay?.UpdateMousePosition(Input.mousePosition);
	}

	private void OnModeSwitch()
	{
		m_RewardSpawner.Clear();
	}

	void Mode.IManagerModeEvents.ModeStart(ManSaveGame.State optionalLoadState)
	{
		if (optionalLoadState == null || !optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManSpawn, out var saveData) || saveData == null)
		{
			return;
		}
		int num = Mathf.Clamp(saveData.blockLimit, 1, BlockManager.MaxBlockLimit);
		if (num == BlockLimit)
		{
			return;
		}
		BlockLimit = num;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			item.blockman.SetTableSize(num);
		}
	}

	void Mode.IManagerModeEvents.Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData
		{
			blockLimit = BlockLimit
		};
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManSpawn, saveData);
	}

	void Mode.IManagerModeEvents.ModeExit()
	{
	}

	private void DebugGUIDraw()
	{
		spawnPresetMenu.Draw(Input.mousePosition);
		spawnBlockMenu.Draw(Input.mousePosition);
		spawnChunkMenu.Draw(Input.mousePosition);
		spawnSceneryMenu.Draw(Input.mousePosition);
		debugNameDisplay.Draw(Input.mousePosition);
	}

	private void OnValidate()
	{
		int num = 0;
		while (m_CorpCratePrefabs != null && num < m_CorpCratePrefabs.Length)
		{
			if (m_CorpCratePrefabs[num].FactionTypes.Length == 0)
			{
				m_CorpCratePrefabs[num].FactionTypes = new FactionSubTypes[1];
			}
			m_CorpCratePrefabs[num].Name = m_CorpCratePrefabs[num].FactionTypes[0].ToString();
			for (int i = 1; i < m_CorpCratePrefabs[num].FactionTypes.Length; i++)
			{
				ref string reference = ref m_CorpCratePrefabs[num].Name;
				reference = reference + " + " + m_CorpCratePrefabs[num].FactionTypes[i];
			}
			num++;
		}
	}
}
