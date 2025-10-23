#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManTechBuildingTutorial : Singleton.Manager<ManTechBuildingTutorial>
{
	private class GhostBlockObject
	{
		public TankBlock ghostBlock;

		public ObjectHighlight highlight;

		public TankBlock placedBlock;

		public Tank tech { get; private set; }

		public void SetOnTech(Tank tech)
		{
			ghostBlock.visible.trans.parent = tech.trans;
			this.tech = tech;
		}

		public void Clear()
		{
			if (ghostBlock != null)
			{
				ghostBlock.trans.Recycle();
			}
			if (highlight != null)
			{
				highlight.Recycle();
			}
			ghostBlock = null;
			highlight = null;
			tech = null;
			placedBlock = null;
		}
	}

	[SerializeField]
	private ObjectHighlight m_GhostBlockHighlightPrefab;

	private Vector3[] m_PlacementFilter;

	private TechData m_TutorialTargetTechPreset;

	private TrackedVisible m_TutorialBuildTech;

	private TankBlock m_TutorialBuildTechFirstBlock;

	private List<GhostBlockObject> m_GhostBlocks = new List<GhostBlockObject>(4);

	public Vector3[] PlacementFilter => m_PlacementFilter;

	public bool IsSpawningGhostBlock { get; private set; }

	private bool HasGhostBlocks => m_GhostBlocks.Count > 0;

	public void FilterPlacementPositions(Vector3[] filter)
	{
		m_PlacementFilter = filter;
	}

	public void SetTutorialTechToBuild(TechData targetTech, Tank tutorialBuildTech)
	{
		d.Assert(targetTech != null, "SetTutorialTechToBuild - Setting tutorial tech was passed in null param!");
		d.Assert(m_TutorialBuildTech == null, "SetTutorialTechToBuild - Setting tutorial tech was while tech was already set! Can only have one at a time!");
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.SetTutorialTech, new SetTutorialTechMessage
			{
				m_TechNetId = tutorialBuildTech.netTech.netId,
				m_TechData = targetTech
			});
		}
		ClearTutorialTechToBuild(netSend: false);
		m_TutorialTargetTechPreset = targetTech;
		m_TutorialBuildTech = (tutorialBuildTech ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tutorialBuildTech.visible.ID) : null);
		d.Assert(tutorialBuildTech == null || m_TutorialBuildTech != null, "SetTutorialTechToBuild - Setting tutorial tech but could not find tracked visible for tutorial tech!");
		if (m_TutorialBuildTech == null)
		{
			return;
		}
		CacheFirstBlockInTutorialTech();
		m_TutorialBuildTech.OnDespawnEvent.Subscribe(OnTutorialTechDespawn);
		m_TutorialBuildTech.OnRespawnEvent.Subscribe(OnTutorialTechRespawn);
		if (m_TutorialTargetTechPreset == null || !(m_TutorialBuildTechFirstBlock != null))
		{
			return;
		}
		d.Assert(tutorialBuildTech.blockman.blockCount > 0, "SetTutorialTechToBuild - Need at least one starting block to start a tutorial tech with!");
		IntVector3 currentBlockTableOffsetFromStored = m_TutorialTargetTechPreset.m_BlockSpecs[0].position - new IntVector3(m_TutorialBuildTechFirstBlock.trans.localPosition);
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_TutorialBuildTech.visible.tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (!BlockMatchesPlacementOnReferenceTech(current.BlockType, new IntVector3(current.trans.localPosition), current.cachedLocalRotation, m_TutorialTargetTechPreset, currentBlockTableOffsetFromStored))
			{
				d.LogError("SetTutorialTechToBuild - Missmatch between half built tech starting point and tutorial target tech! Starting tech does not match target data.");
				break;
			}
		}
	}

	public void ClearTutorialTechToBuild(bool netSend = true)
	{
		if (m_TutorialBuildTech != null)
		{
			m_TutorialBuildTech.OnDespawnEvent.Unsubscribe(OnTutorialTechDespawn);
			m_TutorialBuildTech.OnRespawnEvent.Unsubscribe(OnTutorialTechRespawn);
		}
		m_TutorialBuildTech = null;
		m_TutorialTargetTechPreset = null;
		m_TutorialBuildTechFirstBlock = null;
		if (netSend && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.SetTutorialTech, new SetTutorialTechMessage
			{
				m_TechNetId = NetworkInstanceId.Invalid
			});
		}
	}

	private void OnTutorialTechDespawn(Visible techVisible)
	{
		m_TutorialBuildTechFirstBlock = null;
	}

	private void OnTutorialTechRespawn(Visible techVisible)
	{
		CacheFirstBlockInTutorialTech();
	}

	private void CacheFirstBlockInTutorialTech()
	{
		m_TutorialBuildTechFirstBlock = null;
		if (m_TutorialTargetTechPreset != null && m_TutorialBuildTech != null && m_TutorialBuildTech.visible != null)
		{
			d.Assert(m_TutorialBuildTech.visible.tank.blockman.blockCount > 0, "CacheFirstBlockInTutorialTech - Need at least one starting block to start a tutorial tech with!");
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_TutorialBuildTech.visible.tank.blockman.IterateBlocks().GetEnumerator();
			if (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				m_TutorialBuildTechFirstBlock = current;
			}
		}
		d.Assert(m_TutorialBuildTechFirstBlock != null, "ManTechBuildingTutorial.CacheFirstBlockInTutorialTech - m_TutorialBuildTechFirstBlock was null while target tech was valid!?");
	}

	public bool IsPlacementAllowed(Tank targetTech, TankBlock blockToPlace, BlockPlacementCollector.Placement blockPlacement)
	{
		bool result = true;
		if (m_TutorialTargetTechPreset != null)
		{
			if (blockPlacement != null && m_TutorialBuildTech != null && m_TutorialBuildTech.visible != null && m_TutorialBuildTech.visible.tank != null && m_TutorialBuildTech.visible.tank == targetTech)
			{
				bool flag = m_TutorialBuildTechFirstBlock == null || blockToPlace == null;
				if (flag)
				{
					string text = $"m_TutorialBuildTech: {m_TutorialBuildTech.visible.name} \n  m_TutorialBuildTech.visible.tank.blockman.blockCount {m_TutorialBuildTech.visible.tank.blockman.blockCount}";
					DebugUtil.AssertRelease(!flag, "ManTechBuildingTutorial.IsPlacementAllowed - " + ((m_TutorialBuildTechFirstBlock == null) ? "m_TutorialBuildTechFirstBlock" : "blockToPlace") + " was null while target tech was valid!? " + text);
					if (m_TutorialBuildTechFirstBlock == null)
					{
						CacheFirstBlockInTutorialTech();
					}
				}
				if (m_TutorialBuildTechFirstBlock != null)
				{
					IntVector3 currentBlockTableOffsetFromStored = m_TutorialTargetTechPreset.m_BlockSpecs[0].position - new IntVector3(m_TutorialBuildTechFirstBlock.trans.localPosition);
					result = BlockMatchesPlacementOnReferenceTech(blockToPlace.BlockType, new IntVector3(blockPlacement.localPos), blockPlacement.orthoRot, m_TutorialTargetTechPreset, currentBlockTableOffsetFromStored);
				}
				else
				{
					DebugUtil.AssertRelease(m_TutorialBuildTechFirstBlock != null, "ManTechBuildingTutorial.IsPlacementAllowed - CacheFirstBlockInTutorialTech() still failed!");
					result = true;
				}
			}
			else
			{
				result = blockToPlace.CanAttachOutsideTutorial;
			}
		}
		return result;
	}

	private GhostBlockObject SpawnGhostBlock(BlockTypes type, Vector3 pos, Quaternion rot, uint blockPoolID = uint.MaxValue)
	{
		IsSpawningGhostBlock = true;
		Visible.DisableAddToTileOnSpawn = true;
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.SpawnNonNetworkedBlock(type, pos, rot, blockPoolID);
		Visible.DisableAddToTileOnSpawn = false;
		IsSpawningGhostBlock = false;
		Visible visible = tankBlock.visible;
		visible.EnablePhysics(enable: false);
		tankBlock.trans.position = pos;
		tankBlock.trans.rotation = rot;
		visible.StopManagingVisible();
		tankBlock.SetCustomMaterialOverride(ManTechMaterialSwap.MatType.Ghost);
		GhostBlockObject ghostBlockObject = new GhostBlockObject
		{
			ghostBlock = tankBlock
		};
		if (m_GhostBlockHighlightPrefab != null)
		{
			ObjectHighlight objectHighlight = m_GhostBlockHighlightPrefab.Spawn();
			objectHighlight.Highlight(visible);
			ghostBlockObject.highlight = objectHighlight;
		}
		m_GhostBlocks.Add(ghostBlockObject);
		return ghostBlockObject;
	}

	private GhostBlockObject SpawnGhostBlockOnTech(BlockTypes blockType, Vector3 localBlockPos, Vector3 blockRotation, Tank parentTech, uint blockPoolID = uint.MaxValue)
	{
		Vector3 pos = parentTech.trans.TransformPoint(localBlockPos);
		Quaternion rot = parentTech.trans.rotation * Quaternion.Euler(blockRotation);
		GhostBlockObject ghostBlockObject = SpawnGhostBlock(blockType, pos, rot, blockPoolID);
		ghostBlockObject.SetOnTech(parentTech);
		ghostBlockObject.ghostBlock.visible.WorldSpaceComponent.SetEnabled(enabled: false);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.AddGhostBlock, new AddGhostBlockMessage
			{
				m_TechNetId = parentTech.netTech.netId,
				m_BlockType = blockType,
				m_BlockPoolID = ghostBlockObject.ghostBlock.blockPoolID,
				m_BlockPosition = localBlockPos,
				m_BlockOrthoRotation = new OrthoRotation(blockRotation)
			});
		}
		return ghostBlockObject;
	}

	public TankBlock AddGhostBlock(BlockTypes blockType, Vector3 localBlockPos, Vector3 blockRotation, Tank parentTech = null)
	{
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || (parentTech.IsNotNull() && parentTech.IsAnchored && parentTech.Anchors.Fixed), "Currently spawning a Ghost block in MP is only supported on a non-moving tech (fixed anchors) only.");
		if (parentTech != null)
		{
			return SpawnGhostBlockOnTech(blockType, localBlockPos, blockRotation, parentTech).ghostBlock;
		}
		return SpawnGhostBlock(blockType, localBlockPos, Quaternion.Euler(blockRotation)).ghostBlock;
	}

	public void TryRemoveGhostBlock(TankBlock ghostBlock)
	{
		for (int i = 0; i < m_GhostBlocks.Count; i++)
		{
			if (m_GhostBlocks[i].ghostBlock == ghostBlock)
			{
				m_GhostBlocks[i].Clear();
				m_GhostBlocks.RemoveAt(i);
				break;
			}
		}
	}

	public void RemoveGhostBlocksOnTech(Tank targetTech, bool netSend = true)
	{
		for (int num = m_GhostBlocks.Count - 1; num >= 0; num--)
		{
			if (m_GhostBlocks[num].tech == targetTech)
			{
				m_GhostBlocks[num].Clear();
				m_GhostBlocks.RemoveAt(num);
			}
		}
		if (netSend && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.RemoveAllGhostBlocksOnTech, new RemoveGhostBlocksOnTechMessage
			{
				m_TechNetId = targetTech.netTech.netId
			});
		}
	}

	public void UpdateGhostBlocksDraggingBlock(TankBlock draggingBlock)
	{
		if (!HasGhostBlocks)
		{
			return;
		}
		for (int i = 0; i < m_GhostBlocks.Count; i++)
		{
			if (m_GhostBlocks[i].placedBlock == null)
			{
				bool flag = MatchesGhostBlockPlacement(draggingBlock, m_GhostBlocks[i]);
				m_GhostBlocks[i].ghostBlock.gameObject.SetActive(!flag);
			}
		}
	}

	private void OnBlockAttached(Tank tech, TankBlock attachedBlock)
	{
		if (!HasGhostBlocks)
		{
			return;
		}
		for (int i = 0; i < m_GhostBlocks.Count; i++)
		{
			if (m_GhostBlocks[i].placedBlock == null && MatchesGhostBlockPlacement(attachedBlock, m_GhostBlocks[i]))
			{
				m_GhostBlocks[i].placedBlock = attachedBlock;
				m_GhostBlocks[i].ghostBlock.gameObject.SetActive(value: false);
				break;
			}
		}
	}

	private void OnBlockDetached(Tank tech, TankBlock removedBlock)
	{
		if (!HasGhostBlocks)
		{
			return;
		}
		for (int i = 0; i < m_GhostBlocks.Count; i++)
		{
			if (m_GhostBlocks[i].placedBlock == removedBlock)
			{
				m_GhostBlocks[i].placedBlock = null;
				m_GhostBlocks[i].ghostBlock.gameObject.SetActive(value: true);
				break;
			}
		}
	}

	private void OnTankRecycledEvent(Tank tank)
	{
		RemoveGhostBlocksOnTech(tank, netSend: false);
	}

	private void OnTankDestroyedEvent(Tank tank, ManDamage.DamageInfo damageInfo)
	{
		RemoveGhostBlocksOnTech(tank, netSend: false);
	}

	private void OnClientAddGhostBlock(NetworkMessage netMsg)
	{
		AddGhostBlockMessage addGhostBlockMessage = netMsg.ReadMessage<AddGhostBlockMessage>();
		Tank component = ClientScene.FindLocalObject(addGhostBlockMessage.m_TechNetId).GetComponent<Tank>();
		d.Assert(component != null, "OnClientAddGhostBlock - Tech could not be found");
		Quaternion quaternion = new OrthoRotation(addGhostBlockMessage.m_BlockOrthoRotation);
		SpawnGhostBlockOnTech(addGhostBlockMessage.m_BlockType, addGhostBlockMessage.m_BlockPosition, quaternion.eulerAngles, component, addGhostBlockMessage.m_BlockPoolID);
	}

	private void OnClientRemoveAllGhostBlocksOnTech(NetworkMessage netMsg)
	{
		Tank component = ClientScene.FindLocalObject(netMsg.ReadMessage<RemoveGhostBlocksOnTechMessage>().m_TechNetId).GetComponent<Tank>();
		RemoveGhostBlocksOnTech(component, netSend: false);
	}

	private void OnClientSetTutorialTech(NetworkMessage netMsg)
	{
		SetTutorialTechMessage setTutorialTechMessage = netMsg.ReadMessage<SetTutorialTechMessage>();
		if (setTutorialTechMessage.HasData)
		{
			NetTech netTech = Singleton.Manager<ManNetTechs>.inst.FindTech(setTutorialTechMessage.m_TechNetId.Value);
			if (netTech != null)
			{
				Tank tech = netTech.tech;
				SetTutorialTechToBuild(setTutorialTechMessage.m_TechData, tech);
			}
			else
			{
				d.LogErrorFormat("Failed to find Client netTech with id {0} for tutorial tech build", setTutorialTechMessage.m_TechNetId.Value);
			}
		}
		else
		{
			ClearTutorialTechToBuild(netSend: false);
		}
	}

	private bool MatchesGhostBlockPlacement(TankBlock block, GhostBlockObject ghostBlock)
	{
		if (block.BlockType == ghostBlock.ghostBlock.BlockType && block.trans.position.EqualsEpsilon(ghostBlock.ghostBlock.trans.position, 0.01f))
		{
			return Quaternion.Dot(block.trans.rotation, ghostBlock.ghostBlock.trans.rotation) > 0.99f;
		}
		return false;
	}

	public void ShowHelpArrow(Transform targetTransform, RectTransform[] unmaskedTransforms)
	{
		UIBouncingArrow.BouncingArrowContext bouncingArrowContext = new UIBouncingArrow.BouncingArrowContext
		{
			targetTransform = targetTransform
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BouncingArrow, bouncingArrowContext);
		UIMaskCutout.UIMaskContext uIMaskContext = new UIMaskCutout.UIMaskContext
		{
			unmaskedTransforms = unmaskedTransforms
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.HUDMask, uIMaskContext);
	}

	public void HideHelpArrow()
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BouncingArrow);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.HUDMask);
	}

	public bool IsHelpArrowVisible()
	{
		return Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.BouncingArrow);
	}

	private bool BlockMatchesPlacementOnReferenceTech(BlockTypes blockType, IntVector3 blockLocalPos, OrthoRotation blockOrthoRotation, TechData referenceTech, IntVector3 currentBlockTableOffsetFromStored)
	{
		bool result = false;
		for (int i = 0; i < referenceTech.m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec blockSpec = referenceTech.m_BlockSpecs[i];
			if (blockType == blockSpec.GetBlockType())
			{
				IntVector3 intVector = blockSpec.position - currentBlockTableOffsetFromStored;
				if (blockLocalPos == intVector && blockOrthoRotation == new OrthoRotation(blockSpec.orthoRotation))
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private void OnModeExit(Mode mode)
	{
		m_PlacementFilter = null;
		m_TutorialTargetTechPreset = null;
		m_TutorialBuildTech = null;
		m_TutorialBuildTechFirstBlock = null;
		for (int i = 0; i < m_GhostBlocks.Count; i++)
		{
			m_GhostBlocks[i].Clear();
		}
		m_GhostBlocks.Clear();
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeExit);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockAttached);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockDetached);
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyedEvent);
		Singleton.Manager<ManTechs>.inst.TankRecycledEvent.Subscribe(OnTankRecycledEvent);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.AddGhostBlock, OnClientAddGhostBlock);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.RemoveAllGhostBlocksOnTech, OnClientRemoveAllGhostBlocksOnTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.SetTutorialTech, OnClientSetTutorialTech);
	}
}
