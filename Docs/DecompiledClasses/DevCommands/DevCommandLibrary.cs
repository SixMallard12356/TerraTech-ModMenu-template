#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevCommands;

public static class DevCommandLibrary
{
	[DevCommand(Name = "Help", Access = Access.Public, Users = User.Any)]
	public static void PrintAllAvailableCommands()
	{
		Singleton.Manager<ManDevCommands>.inst.PrintAllAvailableCommands();
	}

	[DevCommand(Access = Access.Public, Users = User.Any)]
	public static void EnableBuildGizmos(bool enable = true)
	{
		Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_UseForceGizmosInBuildBeam = enable;
	}

	[DevCommand(Access = Access.Public, Users = User.Host)]
	public static CommandReturn EnableCheats(bool enable = true)
	{
		Access commandAccessLevel = Singleton.Manager<ManDevCommands>.inst.CommandAccessLevel;
		Singleton.Manager<ManDevCommands>.inst.SetCommandAccessLevel(enable ? Access.Cheat : Access.Public);
		string text = null;
		if (Singleton.Manager<ManDevCommands>.inst.CommandAccessLevel != commandAccessLevel)
		{
			text = "Cheats are " + (enable ? "enabled" : "disabled");
			if (enable && Singleton.Manager<ManAchievements>.inst.HasModeGotAchievements(Singleton.Manager<ManGameMode>.inst.GetCurrentGameType()))
			{
				Singleton.Manager<ManAchievements>.inst.ActiveInCurrentMode = false;
				string text2 = ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.MainGame) ? "Campaign" : Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().ToString());
				text = text + ". Achievements will be locked this " + text2 + ".";
			}
		}
		return new CommandReturn
		{
			message = text
		};
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void Teleport(float x, float y, float z, bool grounded = true, bool relative = false)
	{
		if (Singleton.playerTank != null)
		{
			Vector3 vector = Singleton.cameraTrans.position - Singleton.playerTank.boundsCentreWorld;
			Vector3 position = new Vector3(x, y, z);
			if (relative)
			{
				position += Singleton.playerPos;
			}
			else
			{
				position += Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			}
			Singleton.playerTank.visible.Teleport(position, Singleton.playerTank.trans.rotation, grounded);
			Vector3 vector2 = (Singleton.playerTank ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
			Singleton.Manager<CameraManager>.inst.ResetCamera(vector2 + vector, Singleton.cameraTrans.rotation);
		}
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void Cleanup(float radius = 1000f, bool includeScenery = false)
	{
		ManSpawn.Debug_RemoveLooseObjects(Singleton.playerPos, radius);
		if (includeScenery)
		{
			ManSpawn.SceneryRemovalFlags sceneryRemovalSettings = ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.RemoveInstant;
			ManSpawn.RemoveAllSceneryAroundPosition(Singleton.playerPos, radius, sceneryRemovalSettings);
		}
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void ClearHeld()
	{
		ManSpawn.Debug_ClearAllHeldVisibles();
	}

	[DevCommand(Name = "SetEnergy", Access = Access.Cheat)]
	public static void SetTechEnergyLevel(float level)
	{
		if (Singleton.playerTank != null)
		{
			Singleton.playerTank.EnergyRegulator.SetAllStoresAmount(level);
		}
	}

	[DevCommand(Name = "SetFuel", Access = Access.Cheat)]
	public static void SetTechFuelLevel(float level)
	{
		if (Singleton.playerTank != null)
		{
			Singleton.playerTank.Boosters.SetAllFuelAmount(level);
		}
	}

	[DevCommand(Name = "SkipPowerup", Access = Access.Cheat)]
	public static void SkipPowerupSequence(bool skip = true)
	{
		Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing = skip;
	}

	[DevCommand(Access = Access.Cheat, Users = User.Host)]
	public static void SetTime(float hour = 12f)
	{
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay((int)hour % 24, (int)(hour % 1f * 60f), 0);
	}

	[DevCommand(Access = Access.Cheat, Users = User.Host)]
	public static void EnableTime(bool enable = true)
	{
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable);
	}

	[DevCommand(Name = "Map.RevealArea", Access = Access.Cheat, Users = User.Any)]
	public static void MapExploreArea(float radius = 1000f, bool relative = true, float x = 0f, float y = 0f, float z = 0f)
	{
		Vector3 scenePos = new Vector3(x, y, z);
		if (relative)
		{
			scenePos += (Singleton.playerTank.IsNotNull() ? Singleton.playerPos : Singleton.cameraTrans.position);
		}
		Singleton.Manager<ManMap>.inst.ExploreArea(scenePos, radius, 1.5f);
	}

	[DevCommand(Name = "Map.Sync", Access = Access.Cheat, Users = User.Any)]
	public static void MapSync(bool fromServer = true, bool toServer = true)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if (fromServer)
			{
				Singleton.Manager<ManMap>.inst.RequestMapData();
			}
			if (toServer)
			{
				Singleton.Manager<ManMap>.inst.UploadMapData();
			}
		}
	}

	[DevCommand(Name = "Invulnerable", Access = Access.Cheat)]
	public static void EnablePlayerInvulnerable(bool enable = true)
	{
		Singleton.Manager<ManPlayer>.inst.PlayerIndestructible = enable;
	}

	[DevCommand(Name = "EnableDamage", Access = Access.Cheat, Users = User.Host)]
	public static void EnableAllDamage(bool enable = true)
	{
		Singleton.Manager<DebugUtil>.inst.m_Settings.SetDisableAllDamage(!enable);
	}

	[DevCommand(Name = "SetHealth", Access = Access.Cheat, Users = User.Host)]
	public static void SetTechHealth(float health)
	{
		if (!(Singleton.playerTank != null))
		{
			return;
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (health <= 0f)
			{
				current.damage.SelfDestruct(0.3f);
				continue;
			}
			Damageable damageable = current.visible.damageable;
			float num = damageable.Health / damageable.MaxHealth;
			if (health < num)
			{
				float damage = (num - health) * damageable.MaxHealth;
				ManDamage.DamageInfo info = new ManDamage.DamageInfo(damage, ManDamage.DamageType.Standard, Singleton.Manager<ManDevCommands>.inst, null, current.centreOfMassWorld);
				info.SetDetachDamageOverride(0.001f);
				damageable.TryToDamage(info, actuallyDealDamage: true, d_ignoreDamageDisables: true);
			}
			else
			{
				float amount = (health - num) * damageable.MaxHealth;
				damageable.Repair(amount);
			}
		}
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void SpawnBlock([BlockType] BlockTypes blockType, int quantity = 1)
	{
		if (Singleton.Manager<ManSpawn>.inst.CanAccessBlockInCurrentMode(blockType) && (Singleton.Manager<ManSpawn>.inst.IsPlayerFacingBlock(blockType) || Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development)))
		{
			Vector3 debugSpawnLocation = GetDebugSpawnLocation();
			TankBlock poolTemplate = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(blockType).GetPoolTemplate();
			float num = 0.5f + poolTemplate.BlockCellBounds.extents.y;
			float num2 = 0.5f + num * 2f;
			Singleton.Manager<ManSpawn>.inst.IsSpawningDebugPaintingBlock = true;
			for (int i = 0; i < quantity; i++)
			{
				Singleton.Manager<ManLooseBlocks>.inst.RequestDebugSpawnItem(ObjectTypes.Block, (int)blockType, debugSpawnLocation, Quaternion.identity);
				debugSpawnLocation += Vector3.up * num2;
			}
			Singleton.Manager<ManSpawn>.inst.IsSpawningDebugPaintingBlock = false;
		}
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void SpawnResource([ChunkType] ChunkTypes resourceType, int quantity = 1)
	{
		Vector3 debugSpawnLocation = GetDebugSpawnLocation();
		for (int i = 0; i < quantity; i++)
		{
			Singleton.Manager<ManLooseBlocks>.inst.RequestDebugSpawnItem(ObjectTypes.Chunk, (int)resourceType, debugSpawnLocation, Quaternion.identity);
			debugSpawnLocation += Vector3.up * 0.8f;
		}
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void SpawnIngredients([BlockType] BlockTypes blockType, bool baseIngredients = false, int recipeQuantity = 1)
	{
		Vector3 position = GetDebugSpawnLocation();
		ItemTypeInfo output = new ItemTypeInfo(ObjectTypes.Block, (int)blockType);
		Singleton.Manager<ManSpawn>.inst.IsSpawningDebugPaintingBlock = true;
		for (int i = 0; i < recipeQuantity; i++)
		{
			ManSpawn.Debug_SpawnIngredientsForItem(output, baseIngredients, ref position);
		}
		Singleton.Manager<ManSpawn>.inst.IsSpawningDebugPaintingBlock = false;
	}

	private static Vector3 GetDebugSpawnLocation()
	{
		return Singleton.Manager<ManSpawn>.inst.GetSpawnPosWorld(new Vector3((float)Screen.width * 0.5f, (float)Screen.width * 0.4f, 0f)) + Vector3.up * 4f;
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static CommandReturn SpawnTech([Snapshot] string snapshotName, bool asNewTech = false, int teamID = 0)
	{
		TechData techData = (Singleton.Manager<ManSnapshots>.inst.ServiceDisk?.GetSnapshotCollectionDisk()?.Snapshots.FirstOrDefault((SnapshotDisk s) => s.m_Name.Value == snapshotName))?.techData;
		bool replacePlayer = !asNewTech;
		if (techData != null)
		{
			Singleton.Manager<ManSpawn>.inst.DebugSpawnTech(techData, replacePlayer, teamID);
		}
		return new CommandReturn
		{
			success = (techData != null)
		};
	}

	[DevCommand(Access = Access.Cheat, Users = User.Host)]
	public static void SetMoney(int amount)
	{
		Singleton.Manager<ManPlayer>.inst.Debug_SetMoney(amount);
	}

	[DevCommand(Access = Access.Cheat, Users = User.Host)]
	public static void AddXP([Factions] FactionSubTypes corp, int experience)
	{
		Singleton.Manager<ManLicenses>.inst.AddXP(corp, experience);
	}

	[DevCommand(Access = Access.Cheat, Users = User.Any)]
	public static void CancelCurrentQuest(bool force = false)
	{
		if (!(Singleton.Manager<ManQuestLog>.inst != null))
		{
			return;
		}
		EncounterDisplayData trackedEncounterDisplayData = Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData();
		if (!(trackedEncounterDisplayData != null))
		{
			return;
		}
		bool flag = trackedEncounterDisplayData.CanBeCancelled;
		if (!force && flag && trackedEncounterDisplayData.HasPosition)
		{
			float num = Singleton.Manager<ManEncounter>.inst.MinEncounterCancelDistance * Singleton.Manager<ManEncounter>.inst.MinEncounterCancelDistance;
			foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
			{
				if ((trackedEncounterDisplayData.ScenePosition - allPlayerTech.boundsCentreWorld).ToVector2XZ().sqrMagnitude < num)
				{
					flag = false;
					break;
				}
			}
		}
		if (force || flag)
		{
			Singleton.Manager<ManQuestLog>.inst.CancelEncounter(Singleton.Manager<ManQuestLog>.inst.TrackedEncounterId);
		}
	}

	[DevCommand(Name = "Circuits.EnableDebugger", Access = Access.Cheat, Users = User.Host)]
	private static void EnableCircuitsDebugger(bool enable = true)
	{
		ManHUD.HUDElementType hudElemType = ManHUD.HUDElementType.CircuitsNSystemsDebugger;
		if (enable != Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hudElemType))
		{
			if (enable)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(hudElemType);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(hudElemType);
			}
		}
	}

	[DevCommand(Access = Access.DevCheat, Users = User.Any, RnDOnly = true)]
	private static CommandReturn SetBuildLimit(int buildSizeLimit)
	{
		if (buildSizeLimit > 0 && buildSizeLimit <= BlockManager.MaxBlockLimit)
		{
			Singleton.Manager<ManSpawn>.inst.BlockLimit = buildSizeLimit;
			return CommandReturn.SilentSuccess;
		}
		return new CommandReturn
		{
			message = $"Limit {buildSizeLimit} is outside the supported range [1, {BlockManager.MaxBlockLimit}]",
			success = false
		};
	}

	[DevCommand(Access = Access.DevCheat, Users = User.Any)]
	public static void SetTimeScale(float scale = 1f)
	{
		Time.timeScale = scale;
	}

	[DevCommand(Access = Access.DevCheat, Users = User.Any)]
	public static void ShowTileState(bool show = true)
	{
		DebugUtil.CurrentSettings.m_DisplayWorldTileStates = show;
	}

	[DevCommand(Name = "Mode.Goto", Access = Access.DevCheat, Users = User.Any)]
	private static void TriggerModeSwitch([GameType] ManGameMode.GameType gameMode)
	{
		string miscSubMode = ((gameMode == ManGameMode.GameType.Misc) ? "quick" : null);
		Singleton.Manager<ManGameMode>.inst.SetupModeSwitchAction(Singleton.Manager<ManGameMode>.inst.NextModeSetting, gameMode, miscSubMode);
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}

	[DevCommand(Access = Access.DevCheat, Users = User.Any)]
	private static void ReplaceTechBlocks([BlockType] BlockTypes toReplace, [BlockType] BlockTypes newBlockType, bool playerTechOnly = false, bool fillGaps = true, bool allowExceedBounds = false)
	{
		if (!Singleton.Manager<ManSpawn>.inst.CanAccessBlockInCurrentMode(newBlockType) || (!Singleton.Manager<ManSpawn>.inst.IsPlayerFacingBlock(newBlockType) && !Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development)))
		{
			return;
		}
		Vector3 vector = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(toReplace).GetPoolTemplate().BlockCellBounds.size + Vector3.one;
		TankBlock replacementTemplateBlock = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(newBlockType).GetPoolTemplate();
		Vector3 vector2 = replacementTemplateBlock.BlockCellBounds.size + Vector3.one;
		bool flag = vector == vector2;
		d.Assert(!fillGaps || flag, $"Trying to Debug replace {toReplace} with {newBlockType}, but the block bounds are not the same! ({dim(vector)}) vs ({dim(vector2)})");
		List<TankBlock> blocksToReplace = new List<TankBlock>(256);
		List<TankPreset.BlockSpec> replacements = new List<TankPreset.BlockSpec>(256);
		HashSet<Vector3Int> cellsToFill = new HashSet<Vector3Int>();
		if (playerTechOnly)
		{
			ReplaceBlocksOnTech(Singleton.playerTank, toReplace, newBlockType, blocksToReplace, replacements, cellsToFill, fillGaps, allowExceedBounds);
			return;
		}
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			ReplaceBlocksOnTech(item, toReplace, newBlockType, blocksToReplace, replacements, cellsToFill, fillGaps, allowExceedBounds);
		}
		void ReplaceBlocksOnTech(Tank tech, BlockTypes _toReplace, BlockTypes _newBlockType, List<TankBlock> _blocksToReplace, List<TankPreset.BlockSpec> _replacements, HashSet<Vector3Int> _cellsToFill, bool _fillGaps, bool _allowExceedBounds)
		{
			BlockTypes blockType = BlockTypes.SPEColourBlock16_Pink_111;
			_blocksToReplace.Clear();
			_replacements.Clear();
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				if (current.BlockType == toReplace)
				{
					_blocksToReplace.Add(current);
					Vector3Int blockPos = Vector3Int.RoundToInt(current.cachedLocalPosition);
					Quaternion blockRotation = current.cachedLocalRotation;
					_cellsToFill.Clear();
					current.filledCells.All((IntVector3 cellPos) => _cellsToFill.Add(Vector3Int.RoundToInt(blockPos + blockRotation * cellPos)));
					if (!replacementTemplateBlock.filledCells.All((IntVector3 cellPos) => _cellsToFill.Contains(Vector3Int.RoundToInt(blockPos + blockRotation * cellPos))) || allowExceedBounds)
					{
						replacementTemplateBlock.filledCells.All((IntVector3 cellPos) => _cellsToFill.Remove(Vector3Int.RoundToInt(blockPos + blockRotation * cellPos)));
						_replacements.Add(new TankPreset.BlockSpec
						{
							m_BlockType = newBlockType,
							position = current.cachedLocalPosition,
							orthoRotation = new OrthoRotation(blockRotation)
						});
					}
					if (fillGaps)
					{
						foreach (Vector3Int item2 in _cellsToFill)
						{
							IntVector3 position = item2;
							_replacements.Add(new TankPreset.BlockSpec
							{
								m_BlockType = blockType,
								position = position,
								orthoRotation = OrthoRotation.identity
							});
						}
					}
				}
			}
			if (_blocksToReplace.Count > 0 && _replacements.Count > 0)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostReplaceBlock(_blocksToReplace.ToArray(), _replacements.ToArray());
			}
		}
		static string dim(Vector3 v)
		{
			return $"{v.x}x{v.y}x{v.z}";
		}
	}
}
