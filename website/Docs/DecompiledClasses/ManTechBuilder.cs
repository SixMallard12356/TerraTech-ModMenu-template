#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManTechBuilder : Singleton.Manager<ManTechBuilder>
{
	private class PlacementValidator
	{
		private int triggerStayFrameIndex;

		private bool collidingObjectIsNonTerrain;

		public bool FilledCellsIntersectTerrain(TankBlock block)
		{
			IntVector3[] filledCells = block.filledCells;
			foreach (IntVector3 intVector in filledCells)
			{
				Vector3 scenePos = block.trans.TransformPoint(intVector);
				if (Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos).y > scenePos.y)
				{
					return true;
				}
			}
			return false;
		}

		public bool BlockedByGeometry(TankBlock block, bool testTerrain)
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem.block == block, "inconsistent block test in PlacementValidator");
			}
			if (triggerStayFrameIndex == Singleton.instance.FixedFrameCount)
			{
				if (!testTerrain)
				{
					return collidingObjectIsNonTerrain;
				}
				return true;
			}
			return false;
		}

		public void OnDraggingBlockTriggerStay(TankBlock block, Collider other)
		{
			if (Singleton.Manager<ManPointer>.inst.DraggingItem == null)
			{
				return;
			}
			d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem.block == block, "inconsistent block test in PlacementValidator");
			if (other.isTrigger)
			{
				return;
			}
			if (triggerStayFrameIndex != Singleton.instance.FixedFrameCount)
			{
				collidingObjectIsNonTerrain = false;
			}
			bool flag = false;
			bool flag2 = Singleton.Manager<ManTechBuilder>.inst.m_PlacementReadyToAttach != null;
			Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(other);
			if (visible != null)
			{
				if (visible.type == ObjectTypes.Block)
				{
					if (visible.block.tank == null)
					{
						if (visible.rbody != null)
						{
							visible.rbody.WakeUp();
							flag = true;
						}
						else if (visible.holderStack == null)
						{
						}
					}
					else if (flag2)
					{
						if (visible.block.tank == Singleton.Manager<ManPointer>.inst.DraggingFocusTech)
						{
							flag = true;
							if (other.gameObject.layer == LayerMask.NameToLayer("TankIgnoreTerrain") && visible.block.Anchor != null)
							{
								flag = false;
							}
						}
						else if (Singleton.Manager<ManPointer>.inst.DraggingFocusTech != null && visible.block.tank.IsFriendly(Singleton.Manager<ManPointer>.inst.DraggingFocusTech.Team) && (!Singleton.Manager<ManPointer>.inst.DraggingFocusTech.IsAnchored || !visible.block.tank.IsAnchored))
						{
							flag = true;
						}
					}
				}
				else if (visible.type == ObjectTypes.Chunk)
				{
					if (visible.rbody != null)
					{
						visible.rbody.WakeUp();
					}
					flag = true;
				}
			}
			bool flag3 = other.IsTerrain();
			if (flag3 && block.IsFlag(TankBlock.Flags.AllowGroundIntersection) && (!flag2 || (Singleton.Manager<ManPointer>.inst.DraggingFocusTech != null && Singleton.Manager<ManPointer>.inst.DraggingFocusTech.IsAnchored && Singleton.Manager<ManPointer>.inst.DraggingFocusTech.rbody.isKinematic)))
			{
				flag = true;
			}
			if (!flag)
			{
				triggerStayFrameIndex = Singleton.instance.FixedFrameCount;
				collidingObjectIsNonTerrain = collidingObjectIsNonTerrain || !flag3;
				if (DebugIntersections)
				{
					string text = (visible.IsNotNull() ? visible.name : "null");
					string text2 = ((visible.IsNotNull() && visible.block.IsNotNull()) ? visible.block.name : "null");
					string arg = ((visible.IsNotNull() && visible.block.IsNotNull() && visible.block.tank.IsNotNull()) ? visible.block.tank.name : "null");
					string arg2 = (Singleton.Manager<ManPointer>.inst.DraggingFocusTech.IsNotNull() ? Singleton.Manager<ManPointer>.inst.DraggingFocusTech.name : "null");
					if (other.gameObject.layer == LayerMask.NameToLayer("TankIgnoreTerrain"))
					{
						if (visible.IsNotNull() && visible.block.IsNotNull())
						{
							_ = visible.block.Anchor != null;
						}
						else
							_ = 0;
					}
					else
						_ = 0;
					d.Log($"Intersection: block {block} intersects with {other}, otherVis={text}, otherBlock={text2}, " + $"otherTank={arg}, hasPlacement={flag2}, focusTank={arg2}, ");
				}
			}
		}
	}

	[SerializeField]
	private BlockRotationTable m_BlockRotationTable;

	[SerializeField]
	private float m_PlacementScreenDistWeight = 100f;

	[SerializeField]
	private float m_PlacementForwardnessWeight = 100f;

	[SerializeField]
	private float m_PlacementThresholdIgnoreWorld = 0.7f;

	[SerializeField]
	private float m_PlacementThresholdUnsnapScreen = 0.02f;

	[SerializeField]
	private float m_PlacementChangeFocusClearance = 10f;

	[SerializeField]
	private ParticleSystem m_AttachParticles;

	[SerializeField]
	private float m_AttachParticleSize = 0.35f;

	[SerializeField]
	private float m_AttachParticleLife = 10000f;

	[SerializeField]
	private float m_AttachParticleZoff = 0.2f;

	[SerializeField]
	private float m_AttachParticleCullAngle = 80f;

	[SerializeField]
	[EnumArray(typeof(TankBlock.BlockLinkAudioType))]
	[Header("Building SFX")]
	private FMODEvent[] m_BlockAttachSFXEvents = new FMODEvent[1];

	[EnumArray(typeof(TankBlock.BlockLinkAudioType))]
	[SerializeField]
	private FMODEvent[] m_BlockDetachSFXEvents = new FMODEvent[1];

	[SerializeField]
	private FMODEvent m_BlockDragSFXEvent;

	[SerializeField]
	private FMODEvent m_BlockPickupSFXEvent;

	[SerializeField]
	private FMODEvent m_BlockPaintingPlaceSFXEvent;

	[SerializeField]
	private float m_DragTimeBetweenSFX = 0.08f;

	[SerializeField]
	private bool m_IgnoreFirstPlaceEventAfterAttach;

	public static bool DebugIntersections;

	public EventNoParams OnBlockRotatedEvent;

	public Event<TankBlock, Tank, BlockPlacementCollector.Placement> PlacementReadyToAttachChangedEvent;

	private Tank m_PreviousFocusTech;

	private Tank m_TechBlockWasRemovedFrom;

	private AITreeType m_AITypeOfTechBlockWasRemovedFrom;

	private int m_FocusedTechNumBlocks;

	private TankBlock m_ReadyToDragBlock;

	private Vector3 m_ScreenPosDragStart;

	private TankBlock m_PlacementBlock;

	private BlockPlacementCollector.Placement m_PlacementReadyToAttach;

	private KeyValuePair<IntVector3, BlockPlacementCollector.Collection> m_PlacementCandidates;

	private BlockPlacementCollector.Collection.Index m_PlacementCandidateChoice;

	private Quaternion m_NoFocusTechBaseOrientation;

	private bool m_ResetAPCollection;

	private ParticleSystem.Particle[] m_AttachPointParticles;

	private Dictionary<IntVector3, IntVector3> m_CachedAPNormals;

	private List<IntVector3> m_AttachPointsToShow;

	private PlacementValidator m_PlacementValidator;

	private BlockPlacementCollector m_Collector = new BlockPlacementCollector();

	private float m_SFXLastDragSoundTime;

	private bool m_IgnoringNextPlaceEventSFX;

	private const string kNumBlocksDetachedSFXParamName = "NumBlocksDetached";

	private GameObject m_AttachParticlesGo;

	private Dictionary<int, BlockRotationTable.RotationGroup> m_RotationGroupOverrides = new Dictionary<int, BlockRotationTable.RotationGroup>();

	public int PlacementPreferredOrientationIndex { get; set; }

	public bool PlayerChosenRotation { get; set; }

	public bool DraggingPlayerCab { get; private set; }

	public ModuleAnchor DraggingAnchor { get; private set; }

	public Action<TankBlock, Collider> PlacementTriggerCallback => m_PlacementValidator.OnDraggingBlockTriggerStay;

	public BlockPlacementCollector BlockPlacementCollector => m_Collector;

	public BlockPlacementCollector.Placement PlacementReadyToAttach => m_PlacementReadyToAttach;

	public KeyValuePair<IntVector3, BlockPlacementCollector.Collection> CurrentAPGroup => m_PlacementCandidates;

	public bool IsPlacingBlockRotated()
	{
		return !m_PlacementCandidateChoice.IsFirst();
	}

	public bool IsPlacingBlock(TankBlock block = null)
	{
		if (m_PlacementReadyToAttach == null || Singleton.Manager<ManPointer>.inst.DraggingItem.IsNull() || Singleton.Manager<ManPointer>.inst.DraggingItem.type != ObjectTypes.Block)
		{
			return false;
		}
		if (block.IsNotNull() && (object)block != Singleton.Manager<ManPointer>.inst.DraggingItem.block)
		{
			return false;
		}
		return true;
	}

	public bool IsBlockHeldInPosition(TankBlock block)
	{
		if (!(m_ReadyToDragBlock != null) && (m_PlacementReadyToAttach == null || !IsPlacementAllowed(block, m_PlacementReadyToAttach)))
		{
			if (m_PlacementReadyToAttach == null && (bool)DraggingAnchor && DraggingAnchor.block.CanAnchorFreely && DraggingAnchor.WouldAnchorToGround())
			{
				return !m_PlacementValidator.BlockedByGeometry(block, testTerrain: false);
			}
			return false;
		}
		return true;
	}

	public void RotatePlacingBlock(int direction)
	{
		if (direction == 0 || Mode<ModeMain>.inst.TutorialDisableBlockRotation)
		{
			return;
		}
		if (IsPlacingBlock())
		{
			d.Assert(m_PlacementCandidateChoice.IsValid);
			if (direction > 0)
			{
				++m_PlacementCandidateChoice;
			}
			else
			{
				--m_PlacementCandidateChoice;
			}
			SetPreferredBlockOrientation(m_PlacementCandidateChoice.Value.orthoRot);
			OnBlockRotatedEvent.Send();
		}
		else if ((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block)
		{
			if (direction > 0)
			{
				PlacementPreferredOrientationIndex++;
			}
			else
			{
				PlacementPreferredOrientationIndex--;
			}
			PlayerChosenRotation = true;
			OrthoRotation.r[] blockRotationOrder = GetBlockRotationOrder(Singleton.Manager<ManPointer>.inst.DraggingItem.block);
			PlacementPreferredOrientationIndex = (PlacementPreferredOrientationIndex + blockRotationOrder.Length) % blockRotationOrder.Length;
			OnBlockRotatedEvent.Send();
		}
	}

	public OrthoRotation GetPreferredBlockOrientation()
	{
		if (!Singleton.Manager<ManPointer>.inst.DraggingItem || Singleton.Manager<ManPointer>.inst.DraggingItem.type != ObjectTypes.Block)
		{
			return OrthoRotation.invalid;
		}
		d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block);
		return new OrthoRotation(GetBlockRotationOrder(Singleton.Manager<ManPointer>.inst.DraggingItem.block)[PlacementPreferredOrientationIndex]);
	}

	public void SetPreferredBlockOrientation(OrthoRotation rot, bool setIsPlayerChosen = true)
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == null || Singleton.Manager<ManPointer>.inst.DraggingItem.type != ObjectTypes.Block)
		{
			PlacementPreferredOrientationIndex = 0;
			return;
		}
		OrthoRotation.r[] blockRotationOrder = GetBlockRotationOrder(Singleton.Manager<ManPointer>.inst.DraggingItem.block);
		PlacementPreferredOrientationIndex = Array.FindIndex(blockRotationOrder, (OrthoRotation.r r) => new OrthoRotation(r) == rot);
		if (PlacementPreferredOrientationIndex == -1)
		{
			PlacementPreferredOrientationIndex = 0;
		}
		if (setIsPlayerChosen)
		{
			PlayerChosenRotation = true;
		}
	}

	public OrthoRotation.r[] GetBlockRotationOrder(TankBlock block)
	{
		OrthoRotation.r[] array = null;
		if (m_RotationGroupOverrides.TryGetValue(block.visible.ID, out var value))
		{
			return value.rotations;
		}
		return m_BlockRotationTable.GetBlockRotationOrder(block.visible.ItemType);
	}

	public bool BlockRotationTableIsCurrent(BlockRotationTable table)
	{
		return table == m_BlockRotationTable;
	}

	public void ResetAPCollection()
	{
		m_ResetAPCollection = true;
	}

	public void OverrideBlockRotationGroup(TankBlock theBlock, BlockRotationTable.RotationGroup rotation)
	{
		if (theBlock != null && rotation != null)
		{
			if (!m_RotationGroupOverrides.ContainsKey(theBlock.visible.ID))
			{
				m_RotationGroupOverrides.Add(theBlock.visible.ID, rotation);
				theBlock.visible.RecycledEvent.Subscribe(RemoveRotationGroupOverrideForBlock);
			}
			else
			{
				m_RotationGroupOverrides[theBlock.visible.ID] = rotation;
			}
		}
		else
		{
			d.LogError("ERROR: OverrideBlockRotationGroup given null block or rotation");
		}
	}

	public void ClearBlockRotationOverride(TankBlock theBlock)
	{
		if (theBlock != null)
		{
			RemoveRotationGroupOverrideForBlock(theBlock.visible);
		}
		else
		{
			d.LogError("ERROR: ClearBlockRotationOverride given null block");
		}
	}

	private bool IsValidTech(Tank tech)
	{
		if (tech.IsNull())
		{
			return false;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return tech.netTech.CanPlayerModify(Singleton.Manager<ManNetwork>.inst.MyPlayer, DraggingPlayerCab);
		}
		return tech.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam;
	}

	private bool CanBlockAttach()
	{
		bool result = true;
		if ((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && (bool)Singleton.Manager<ManPointer>.inst.DraggingItem.block)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				result = Singleton.Manager<ManPointer>.inst.DraggingItem.block.netBlock != null;
				if (Singleton.Manager<ManPointer>.inst.DraggingFocusTech != null && Singleton.Manager<ManPointer>.inst.DraggingFocusTech.netTech.NetPlayer != null && Singleton.Manager<ManPointer>.inst.DraggingFocusTech.netTech.NetPlayer != Singleton.Manager<ManNetwork>.inst.MyPlayer)
				{
					if (!Singleton.Manager<ManNetwork>.inst.CoOpAllowPlayerTechMods && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Deathmatch)
					{
						result = false;
					}
					if (DraggingPlayerCab)
					{
						result = false;
					}
				}
			}
			if (!Singleton.Manager<ManBlockLimiter>.inst.AllowPlayerAttachBlock(Singleton.Manager<ManPointer>.inst.DraggingItem.block))
			{
				result = false;
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	private float CalculateAPWeight(Vector3 worldPos, Ray screenPointRay, Vector3 focalTechPos)
	{
		Vector3 lhs = worldPos - screenPointRay.origin;
		if (Vector3.Dot(lhs, screenPointRay.direction) <= 0f)
		{
			return -1000f;
		}
		if (lhs.sqrMagnitude < 1f)
		{
			return -1000f;
		}
		float magnitude = Vector3.Cross(lhs, screenPointRay.direction).magnitude;
		if (magnitude > m_PlacementThresholdIgnoreWorld)
		{
			return -1000f;
		}
		return (0f - Vector3.Dot(worldPos - focalTechPos, screenPointRay.direction)) * m_PlacementForwardnessWeight - magnitude * magnitude * m_PlacementScreenDistWeight;
	}

	private void StartDraggingBlock(TankBlock block)
	{
		if ((bool)block.tank)
		{
			m_ReadyToDragBlock = block;
			Singleton.Manager<ManPointer>.inst.FreezeDraggingItem(freeze: true);
		}
		else if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.Grab)
		{
			m_BlockPickupSFXEvent.PlayOneShot(block.trans.position);
		}
		PlayerChosenRotation = false;
		PlacementPreferredOrientationIndex = 0;
		m_NoFocusTechBaseOrientation = Quaternion.identity;
		DraggingPlayerCab = false;
		DraggingAnchor = block.Anchor;
	}

	private void ReplaceBlock(TankBlock block)
	{
		bool draggingPlayerCab = DraggingPlayerCab;
		StartDraggingBlock(block);
		DraggingPlayerCab = draggingPlayerCab;
	}

	private void UpdateDraggingBlock(TankBlock block, Vector3 screenPos)
	{
		Tank draggingFocusTech = Singleton.Manager<ManPointer>.inst.DraggingFocusTech;
		if ((bool)m_ReadyToDragBlock)
		{
			d.Assert(m_ReadyToDragBlock == block);
			if (!m_ReadyToDragBlock.tank)
			{
				m_ReadyToDragBlock = null;
				Singleton.Manager<ManPointer>.inst.FreezeDraggingItem(freeze: false);
			}
			else if ((screenPos - m_ScreenPosDragStart).SetZ(0f).magnitude > m_PlacementThresholdUnsnapScreen * (float)Screen.width)
			{
				DetachHeldBlock(block);
			}
			if (m_ReadyToDragBlock == null)
			{
				bool flag = block.IsController && m_TechBlockWasRemovedFrom.IsNotNull() && m_TechBlockWasRemovedFrom.blockman.blockCount == 0;
				SetPreferredBlockOrientation(block.cachedLocalRotation, !flag);
			}
		}
		ManPointer.HighlightVariation highlightType = ManPointer.HighlightVariation.Normal;
		BlockPlacementCollector.Placement placementReadyToAttach = m_PlacementReadyToAttach;
		bool flag2 = false;
		if (IsValidTech(draggingFocusTech) && CanBlockAttach() && m_Collector.PlacementsValid && Singleton.Manager<ManPointer>.inst.DropTarget == null)
		{
			Ray screenPointRay = Singleton.Manager<ManUI>.inst.ScreenPointToRay(Singleton.Manager<ManPointer>.inst.DragPositionOnScreen);
			float num = -1000f;
			float num2 = num;
			Vector3 position = Singleton.Manager<ManPointer>.inst.DraggingFocusTech.trans.position;
			if (m_PlacementReadyToAttach != null)
			{
				Vector3 worldPos = m_PlacementCandidates.Key.APtoWorld(draggingFocusTech.trans);
				float num3 = CalculateAPWeight(worldPos, screenPointRay, position);
				if (num3 > num)
				{
					num2 = num3 + m_PlacementChangeFocusClearance;
				}
			}
			foreach (KeyValuePair<IntVector3, BlockPlacementCollector.Collection> item in m_Collector)
			{
				Vector3 worldPos2 = item.Key.APtoWorld(draggingFocusTech.trans);
				float num4 = CalculateAPWeight(worldPos2, screenPointRay, position);
				if (num4 > num2 && num4 > num)
				{
					m_PlacementCandidates = item;
					num2 = num4;
					if (!PlayerChosenRotation && m_PlacementCandidates.Value.HasPreferentialPlacements)
					{
						m_PlacementCandidateChoice = m_PlacementCandidates.Value.First();
						SetPreferredBlockOrientation(m_PlacementCandidateChoice.Value.orthoRot, setIsPlayerChosen: false);
					}
					else
					{
						m_PlacementCandidateChoice = m_PlacementCandidates.Value.FirstPreferred(GetPreferredBlockOrientation());
					}
				}
			}
			m_Collector.UpdateBlockPosTechLocal(draggingFocusTech.trans.InverseTransformPoint(Singleton.Manager<ManPointer>.inst.targetPosition));
			if (num2 != num && m_PlacementCandidateChoice.IsValid)
			{
				SetPlacementReadyToAttach(block, m_PlacementCandidateChoice.Value);
				block.trans.SetPositionIfChanged(draggingFocusTech.trans.TransformPoint(m_PlacementReadyToAttach.localPos));
				block.trans.SetRotationIfChanged(draggingFocusTech.trans.rotation * m_PlacementReadyToAttach.orthoRot);
				flag2 = IsPlacementAllowed(block, m_PlacementReadyToAttach);
				highlightType = (flag2 ? ManPointer.HighlightVariation.Attaching : ManPointer.HighlightVariation.Invalid);
			}
			else
			{
				SetPlacementReadyToAttach(block, null);
				block.trans.SetRotationIfChanged(draggingFocusTech.trans.rotation * GetPreferredBlockOrientation());
				Singleton.Manager<ManPointer>.inst.ResetDraggingItemCentrePosition();
			}
			Singleton.Manager<ManTechBuildingTutorial>.inst.UpdateGhostBlocksDraggingBlock(block);
			m_NoFocusTechBaseOrientation = draggingFocusTech.trans.rotation;
		}
		else
		{
			SetPlacementReadyToAttach(Singleton.Manager<ManPointer>.inst.DraggingItem.block, null);
			if (!m_ReadyToDragBlock && Singleton.Manager<ManPointer>.inst.DropTarget == null)
			{
				block.trans.SetRotationIfChanged(m_NoFocusTechBaseOrientation * GetPreferredBlockOrientation());
				Singleton.Manager<ManPointer>.inst.ResetDraggingItemCentrePosition();
			}
		}
		if (m_PlacementReadyToAttach != placementReadyToAttach)
		{
			if (m_IgnoreFirstPlaceEventAfterAttach && m_IgnoringNextPlaceEventSFX)
			{
				m_SFXLastDragSoundTime = 0f;
				m_IgnoringNextPlaceEventSFX = false;
			}
			else if (Time.time > m_SFXLastDragSoundTime + m_DragTimeBetweenSFX)
			{
				m_SFXLastDragSoundTime = Time.time;
				m_BlockDragSFXEvent.PlayOneShot(block.trans.position);
			}
		}
		if (DraggingAnchor != null && m_ReadyToDragBlock == null)
		{
			bool flag3;
			if (m_PlacementReadyToAttach == null)
			{
				flag3 = block.CanAnchorFreely && Singleton.Manager<ManPointer>.inst.DropTarget == null;
				if (flag3)
				{
					block.trans.SetRotationIfChanged(Quaternion.LookRotation(block.trans.forward.SetY(0f)));
					DraggingAnchor.SnapGroundPositionUpwards();
					if (m_PlacementValidator.BlockedByGeometry(block, testTerrain: true))
					{
						highlightType = ManPointer.HighlightVariation.Invalid;
					}
				}
			}
			else
			{
				flag3 = draggingFocusTech.IsAnchored && flag2;
			}
			DraggingAnchor.ActivateAnchorGeometry(flag3 && DraggingAnchor.WouldAnchorToGround(), enableCollision: false);
		}
		if (Singleton.Manager<ManBlockLimiter>.inst != null && !Singleton.Manager<ManBlockLimiter>.inst.AllowPlayerAttachBlock(block))
		{
			highlightType = ManPointer.HighlightVariation.BlockLimited;
		}
		Singleton.Manager<ManPointer>.inst.SetHighlightType(highlightType);
	}

	public void DetachHeldBlock(TankBlock block)
	{
		d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.Manager<ManPointer>.inst.DraggingItem.block == block, "DetachHeldBlock was passed a block that is not currently held!?");
		d.AssertFormat(block.IsAttached, "DetachBlock was passed a block '{0}' that is not currently attached to a tech!", block.name);
		m_ReadyToDragBlock = null;
		Singleton.Manager<ManPointer>.inst.FreezeDraggingItem(freeze: false);
		bool flag = block.tank == Singleton.playerTank;
		Singleton.Manager<ManUndo>.inst.OnBeforeDetachBlock(block);
		m_TechBlockWasRemovedFrom = block.tank;
		int num = 0;
		if (m_TechBlockWasRemovedFrom.IsNotNull())
		{
			m_AITypeOfTechBlockWasRemovedFrom = (block.tank.AI.TryGetCurrentAIType(out var aiType) ? new AITreeType(aiType) : null);
			num = block.tank.blockman.blockCount;
		}
		else
		{
			m_AITypeOfTechBlockWasRemovedFrom = null;
		}
		block.visible.EnablePhysics(enable: false, disableWithTrigger: true);
		Singleton.Manager<ManLooseBlocks>.inst.RequestDetachBlock(block, allowHeadlessTech: true, manualRemove: true);
		if ((bool)m_TechBlockWasRemovedFrom)
		{
			int num2 = num - m_TechBlockWasRemovedFrom.blockman.blockCount;
			FMODEvent fMODEvent = m_BlockDetachSFXEvents[(int)block.BlockConnectionAudioType];
			d.AssertFormat(fMODEvent.IsValid(), "Block Detach SFX for type {0} was not set to a valid FMODEvent!", block.BlockConnectionAudioType);
			if (fMODEvent.IsValid())
			{
				fMODEvent.PlayOneShot(singleParam: new FMODEvent.FMODParams("NumBlocksDetached", num2), position: block.trans.position);
			}
			Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(m_TechBlockWasRemovedFrom);
		}
		if (flag && (!Singleton.playerTank || (block.IsController && !Singleton.playerTank.ControllableByLocalPlayer)))
		{
			DraggingPlayerCab = true;
		}
	}

	public void DetachBlock(TankBlock block)
	{
		d.AssertFormat(block.IsAttached, "DetachBlock was passed a block '{0}' that is not currently attached to a tech!", block.name);
		bool flag = (object)block.tank == Singleton.playerTank;
		Singleton.Manager<ManUndo>.inst.OnBeforeDetachBlock(block);
		m_TechBlockWasRemovedFrom = block.tank;
		int num = 0;
		if (m_TechBlockWasRemovedFrom.IsNotNull())
		{
			m_AITypeOfTechBlockWasRemovedFrom = (block.tank.AI.TryGetCurrentAIType(out var aiType) ? new AITreeType(aiType) : null);
			num = block.tank.blockman.blockCount;
		}
		else
		{
			m_AITypeOfTechBlockWasRemovedFrom = null;
		}
		Singleton.Manager<ManLooseBlocks>.inst.RequestDetachBlock(block, allowHeadlessTech: false, manualRemove: true);
		if ((bool)m_TechBlockWasRemovedFrom)
		{
			int num2 = num - m_TechBlockWasRemovedFrom.blockman.blockCount;
			FMODEvent fMODEvent = m_BlockDetachSFXEvents[(int)block.BlockConnectionAudioType];
			d.AssertFormat(fMODEvent.IsValid(), "Block Detach SFX for type {0} was not set to a valid FMODEvent!", block.BlockConnectionAudioType);
			if (fMODEvent.IsValid())
			{
				fMODEvent.PlayOneShot(singleParam: new FMODEvent.FMODParams("NumBlocksDetached", num2), position: block.trans.position);
			}
			Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(m_TechBlockWasRemovedFrom);
		}
		bool num3 = flag && (!Singleton.playerTank || (block.IsController && !Singleton.playerTank.ControllableByLocalPlayer));
		AITreeType aiTypeUnanchored = null;
		AITreeType aiTypeAnchored = null;
		if (block.IsController && m_TechBlockWasRemovedFrom != null)
		{
			if (Singleton.playerTank != null && m_TechBlockWasRemovedFrom.visible.ID == Singleton.playerTank.visible.ID)
			{
				aiTypeUnanchored = new AITreeType(AITreeType.AITypes.Escort);
				aiTypeAnchored = new AITreeType(AITreeType.AITypes.Guard);
			}
			else
			{
				aiTypeUnanchored = m_AITypeOfTechBlockWasRemovedFrom;
				aiTypeAnchored = m_AITypeOfTechBlockWasRemovedFrom;
			}
		}
		if (num3 || (block.IsController && Singleton.Manager<ManBlockLimiter>.inst.AllowCreateSimplePlayerTech(block)))
		{
			CreateTechFromSingleBlock(block, aiTypeUnanchored, aiTypeAnchored);
		}
		if (num3)
		{
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(block.tank);
		}
	}

	public bool IsPlacementAllowed(TankBlock blockToPlace, BlockPlacementCollector.Placement blockPlacement)
	{
		bool result = true;
		if ((bool)Singleton.Manager<ManPointer>.inst.DraggingFocusTech)
		{
			result = blockToPlace.CanAttach;
			Tank draggingFocusTech = Singleton.Manager<ManPointer>.inst.DraggingFocusTech;
			result = result && Singleton.Manager<ManTechBuildingTutorial>.inst.IsPlacementAllowed(draggingFocusTech, blockToPlace, blockPlacement);
			bool testTerrain = draggingFocusTech.IsAnchored && draggingFocusTech.Anchors.NumAnchored != draggingFocusTech.Anchors.NumSkyAnchored;
			result = result && !m_PlacementValidator.BlockedByGeometry(blockToPlace, testTerrain);
		}
		return result;
	}

	private static bool TechIsPlayerTeam(Tank tech)
	{
		return tech.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam;
	}

	private void CreateTechFromSingleBlock(TankBlock block, AITreeType aiTypeUnanchored, AITreeType aiTypeAnchored)
	{
		int num = Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsPlayerTeam).Count();
		string label = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 74), num);
		Tank tank = Singleton.Manager<ManSpawn>.inst.WrapSingleBlock(null, block, Singleton.Manager<ManPlayer>.inst.PlayerTeam, label);
		Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(tank);
		AITreeType aITreeType = (tank.IsAnchored ? aiTypeAnchored : aiTypeUnanchored);
		if (aITreeType != null)
		{
			tank.AI.SetBehaviorType(aITreeType);
		}
	}

	private void CancelCreateTech(TankBlock block)
	{
		ModuleAnchor anchor = block.Anchor;
		if (anchor != null)
		{
			anchor.ActivateAnchorGeometry(active: false, enableCollision: false);
		}
	}

	private void ReleaseDraggingBlock(TankBlock block, Vector3 screenPos, bool allowPlace)
	{
		Tank draggingFocusTech = Singleton.Manager<ManPointer>.inst.DraggingFocusTech;
		bool flag = allowPlace && IsPlacementAllowed(block, m_PlacementReadyToAttach);
		if (block.visible.isActive)
		{
			if (m_PlacementReadyToAttach != null && (bool)Singleton.Manager<ManPointer>.inst.DraggingFocusTech && Singleton.Manager<ManPointer>.inst.DropTarget == null && flag)
			{
				block.trans.rotation = draggingFocusTech.trans.rotation * m_PlacementReadyToAttach.orthoRot;
				block.trans.position = draggingFocusTech.trans.TransformPoint(m_PlacementReadyToAttach.localPos);
				if (Singleton.Manager<ManLooseBlocks>.inst.RequestAttachBlock(draggingFocusTech, block, m_PlacementReadyToAttach.localPos, m_PlacementReadyToAttach.orthoRot))
				{
					FMODEvent fMODEvent = m_BlockAttachSFXEvents[(int)block.BlockConnectionAudioType];
					d.Assert(fMODEvent.IsValid(), string.Concat("Block Attach SFX for type ", block.BlockConnectionAudioType, " was not set to a valid FMODEvent!"));
					if (fMODEvent.IsValid())
					{
						fMODEvent.PlayOneShot(block.trans.position);
					}
					m_IgnoringNextPlaceEventSFX = m_IgnoreFirstPlaceEventAfterAttach && Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock;
					Singleton.Manager<ManStats>.inst.BlockAttached(block.BlockType);
					Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(draggingFocusTech);
				}
				if (!Singleton.Manager<ManPointer>.inst.DraggingFocusTech.IsAnchored && m_PlacementValidator.FilledCellsIntersectTerrain(block))
				{
					MoveTechAboveGround(draggingFocusTech);
				}
			}
			else if (m_ReadyToDragBlock == null && Singleton.Manager<ManPointer>.inst.DropTarget == null)
			{
				if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && (block.IsController || (allowPlace && IsBlockHeldInPosition(block))))
				{
					AITreeType aiTypeUnanchored = null;
					AITreeType aiTypeAnchored = null;
					if (block.IsController && m_TechBlockWasRemovedFrom != null)
					{
						if (Singleton.playerTank != null && m_TechBlockWasRemovedFrom.visible.ID == Singleton.playerTank.visible.ID)
						{
							aiTypeUnanchored = new AITreeType(AITreeType.AITypes.Escort);
							aiTypeAnchored = new AITreeType(AITreeType.AITypes.Guard);
						}
						else
						{
							aiTypeUnanchored = m_AITypeOfTechBlockWasRemovedFrom;
							aiTypeAnchored = m_AITypeOfTechBlockWasRemovedFrom;
						}
					}
					if (Singleton.Manager<ManBlockLimiter>.inst.AllowCreateSimplePlayerTech(block) || DraggingPlayerCab)
					{
						CreateTechFromSingleBlock(block, aiTypeUnanchored, aiTypeAnchored);
						if (m_TechBlockWasRemovedFrom == null || m_TechBlockWasRemovedFrom.visible.isActive)
						{
							Singleton.Manager<ManStats>.inst.BlockAttached(block.BlockType);
						}
					}
					else
					{
						CancelCreateTech(block);
					}
				}
				else
				{
					if ((bool)DraggingAnchor)
					{
						DraggingAnchor.ActivateAnchorGeometry(active: false, enableCollision: false);
					}
					if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock)
					{
						m_BlockPaintingPlaceSFXEvent.PlayOneShot(block.trans.position);
					}
				}
			}
		}
		if (DraggingPlayerCab && (bool)block.tank && block.tank != Singleton.playerTank)
		{
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(block.tank);
		}
		if ((bool)m_TechBlockWasRemovedFrom)
		{
			if (m_TechBlockWasRemovedFrom.visible.isActive)
			{
				m_TechBlockWasRemovedFrom.blockman.FixupOnRelease();
			}
			m_TechBlockWasRemovedFrom = null;
		}
		m_AITypeOfTechBlockWasRemovedFrom = null;
		PlayerChosenRotation = false;
	}

	private void CleanUpDraggingBlock(TankBlock block, Vector3 screenPos)
	{
		m_ReadyToDragBlock = null;
		SetPlacementReadyToAttach(null, null);
		DraggingPlayerCab = false;
		DraggingAnchor = null;
		m_PreviousFocusTech = null;
		m_Collector.Stop();
		m_CachedAPNormals.Clear();
		Singleton.Manager<ManPointer>.inst.SetHighlightType(ManPointer.HighlightVariation.Normal);
	}

	private void MoveTechAboveGround(Tank tech)
	{
		float num = tech.blockBounds.extents.sqrMagnitude + 1f;
		Vector3 vector = tech.trans.InverseTransformDirection(Vector3.down * num);
		float distance = num;
		tech.blockBounds.IntersectRay(new Ray(tech.blockBounds.center + vector, -vector), out distance);
		Vector3 position = tech.blockBounds.center + vector * (1f - distance / num);
		Vector3 scenePos = tech.trans.TransformPoint(position) - new Vector3(0f, 0.5f, 0f);
		float num2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos).y - scenePos.y;
		if (num2 > 0f)
		{
			tech.rbody.position += new Vector3(0f, num2, 0f);
		}
	}

	public void StartStopAttachPointCollection()
	{
		Tank tank = Singleton.Manager<ManPointer>.inst.DraggingFocusTech;
		if ((bool)tank && tank.blockman.locked)
		{
			tank = null;
		}
		if (!Singleton.Manager<ManPointer>.inst.DraggingItem || Singleton.Manager<ManPointer>.inst.DraggingItem.type != ObjectTypes.Block)
		{
			tank = null;
		}
		int num = ((tank != null && tank.visible.CanBlockAttachToTech) ? tank.blockman.blockCount : 0);
		if (tank != m_PreviousFocusTech || m_FocusedTechNumBlocks != num || m_ResetAPCollection)
		{
			m_ResetAPCollection = false;
			m_Collector.Stop();
			m_CachedAPNormals.Clear();
			if (tank != null && tank.visible.CanBlockAttachToTech)
			{
				m_CachedAPNormals.Clear();
				m_Collector.Start(Singleton.Manager<ManPointer>.inst.DraggingItem.block, tank, Singleton.Manager<ManTechBuildingTutorial>.inst.PlacementFilter, GetPreferredBlockOrientation());
				m_Collector.UpdateBlockPosTechLocal(tank.trans.InverseTransformPoint(Singleton.Manager<ManPointer>.inst.targetPosition));
			}
			SetPlacementReadyToAttach((!(tank != null)) ? null : Singleton.Manager<ManPointer>.inst.DraggingItem?.block, null);
		}
		m_FocusedTechNumBlocks = num;
		m_PreviousFocusTech = tank;
	}

	private void UpdateAttachParticles()
	{
		int num = 0;
		int num2 = 0;
		TankBlock tankBlock = (((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block) ? Singleton.Manager<ManPointer>.inst.DraggingItem.block : null);
		if (IsValidTech(Singleton.Manager<ManPointer>.inst.DraggingFocusTech) && m_Collector.PlacementsValid)
		{
			int count = m_Collector.Count;
			foreach (KeyValuePair<IntVector3, BlockPlacementCollector.Collection> item in m_Collector)
			{
				if (num2 == count)
				{
					break;
				}
				IntVector3 key = item.Key;
				if (!m_CachedAPNormals.ContainsKey(key))
				{
					Vector3 vector = new Vector3(key.x % 2, key.y % 2, key.z % 2);
					if (Singleton.Manager<ManPointer>.inst.DraggingFocusTech.blockman.GetBlockAtPosition((key + vector) * 0.5f) != null)
					{
						vector = -vector;
					}
					m_CachedAPNormals[key] = vector;
				}
			}
			m_AttachPointsToShow.Clear();
			Vector3 forward = Singleton.cameraTrans.forward;
			Quaternion rotation = Singleton.Manager<ManPointer>.inst.DraggingFocusTech.trans.rotation;
			foreach (KeyValuePair<IntVector3, IntVector3> cachedAPNormal in m_CachedAPNormals)
			{
				IntVector3 key2 = cachedAPNormal.Key;
				IntVector3 value = cachedAPNormal.Value;
				Vector3 to = rotation * value;
				if (Vector3.Angle(forward, to) > m_AttachParticleCullAngle)
				{
					m_AttachPointsToShow.Add(key2);
				}
			}
			num += m_AttachPointsToShow.Count;
		}
		if ((bool)tankBlock)
		{
			int num3 = tankBlock.attachPoints.Length - tankBlock.NumConnectedAPs;
			num += num3;
		}
		bool flag = num > 0 && Singleton.Manager<ManBlockLimiter>.inst.AllowPlayerAttachBlock(tankBlock);
		if (m_AttachParticlesGo.activeSelf != flag)
		{
			m_AttachParticlesGo.SetActive(flag);
		}
		if (!flag)
		{
			return;
		}
		if (num != m_AttachParticles.particleCount)
		{
			if (num != m_AttachParticles.main.maxParticles)
			{
				ParticleSystem.MainModule main = m_AttachParticles.main;
				main.maxParticles = num;
			}
			ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
			{
				position = new Vector3(0f, -1000f, 0f),
				velocity = Vector3.zero,
				startSize = m_AttachParticleSize,
				startLifetime = m_AttachParticleLife,
				startColor = Color.white
			};
			m_AttachParticles.Emit(emitParams, num);
			m_AttachPointParticles = new ParticleSystem.Particle[num];
			m_AttachParticles.GetParticles(m_AttachPointParticles);
		}
		num2 = 0;
		if (IsValidTech(Singleton.Manager<ManPointer>.inst.DraggingFocusTech) && m_Collector.PlacementsValid)
		{
			foreach (IntVector3 item2 in m_AttachPointsToShow)
			{
				Vector3 worldPos = item2.APtoWorld(Singleton.Manager<ManPointer>.inst.DraggingFocusTech.trans);
				m_AttachPointParticles[num2++].position = CameraOffset(worldPos, m_AttachParticleZoff);
			}
		}
		if ((bool)tankBlock)
		{
			TankBlock.FreeAPIterator enumerator4 = tankBlock.IterateFreeAttachPointsWorld().GetEnumerator();
			while (enumerator4.MoveNext())
			{
				Vector3 current3 = enumerator4.Current;
				if (num2 >= m_AttachPointParticles.Length)
				{
					break;
				}
				m_AttachPointParticles[num2++].position = CameraOffset(current3, m_AttachParticleZoff);
			}
		}
		for (int i = num2; i < num; i++)
		{
			m_AttachPointParticles[i].position = m_AttachPointParticles[0].position - Vector3.up * 1000f;
		}
		m_AttachParticles.SetParticles(m_AttachPointParticles, num);
	}

	private void SetPlacementReadyToAttach(TankBlock block, BlockPlacementCollector.Placement newPlacement)
	{
		if (m_PlacementBlock != null && !m_PlacementBlock.IsAttached && block != m_PlacementBlock)
		{
			m_PlacementBlock.SetCachedLocalPositionData(Vector3.zero, OrthoRotation.identity);
		}
		m_PlacementBlock = block;
		m_PlacementReadyToAttach = newPlacement;
		if (m_PlacementBlock != null && !m_PlacementBlock.IsAttached)
		{
			Vector3 pos = newPlacement?.localPos ?? Vector3.zero;
			OrthoRotation orthoRot = newPlacement?.orthoRot ?? OrthoRotation.identity;
			m_PlacementBlock.SetCachedLocalPositionData(pos, orthoRot);
		}
		PlacementReadyToAttachChangedEvent.Send(block, Singleton.Manager<ManPointer>.inst.DraggingFocusTech, m_PlacementReadyToAttach);
	}

	private Vector3 CameraOffset(Vector3 worldPos, float offset)
	{
		Vector3 vector = (Singleton.cameraTrans.position - worldPos).normalized * offset;
		return worldPos + vector;
	}

	private void OnModeExit(Mode mode)
	{
		SetPlacementReadyToAttach(null, null);
	}

	private void OnMouse(ManPointer.Event mouseEvent, bool down, bool clicked)
	{
		if (mouseEvent == ManPointer.Event.RMB && !down && clicked)
		{
			RotatePlacingBlock(1);
		}
	}

	private void OnDragItem(Visible item, ManPointer.DragAction dragAction, Vector3 screenPos)
	{
		if (item.type != ObjectTypes.Block)
		{
			return;
		}
		TankBlock block = item.block;
		switch (dragAction)
		{
		case ManPointer.DragAction.Grab:
			if ((bool)block.tank)
			{
				m_ScreenPosDragStart = screenPos;
			}
			StartDraggingBlock(block);
			break;
		case ManPointer.DragAction.Update:
			UpdateDraggingBlock(block, screenPos);
			break;
		case ManPointer.DragAction.ReleaseAllowPlace:
			ReleaseDraggingBlock(block, screenPos, allowPlace: true);
			break;
		case ManPointer.DragAction.ReleaseLoose:
			ReleaseDraggingBlock(block, screenPos, allowPlace: false);
			break;
		case ManPointer.DragAction.PostRelease:
			CleanUpDraggingBlock(block, screenPos);
			break;
		}
		block.NotifyDrag(dragAction, screenPos);
	}

	private void OnReplaceHeldItem(Visible item)
	{
		if (item.type == ObjectTypes.Block)
		{
			ReplaceBlock(item.block);
		}
	}

	private void RemoveRotationGroupOverrideForBlock(Visible vis)
	{
		m_RotationGroupOverrides.Remove(vis.ID);
		vis.RecycledEvent.Unsubscribe(RemoveRotationGroupOverrideForBlock);
	}

	private void Start()
	{
		Singleton.Manager<ManPointer>.inst.MouseEvent.Subscribe(OnMouse);
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDragItem);
		Singleton.Manager<ManPointer>.inst.ReplaceHeldItemEvent.Subscribe(OnReplaceHeldItem);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeExit);
		m_Collector.Initialise();
		m_AttachPointsToShow = new List<IntVector3>();
		m_CachedAPNormals = new Dictionary<IntVector3, IntVector3>();
		m_TechBlockWasRemovedFrom = null;
		m_AITypeOfTechBlockWasRemovedFrom = null;
		d.Assert(m_BlockRotationTable, "Block Rotation Table is null");
		m_BlockRotationTable.InitRuntime();
		m_PlacementValidator = new PlacementValidator();
		m_AttachParticlesGo = m_AttachParticles.gameObject;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
		m_Collector.Terminate(waitForExit: false);
	}

	private void Update()
	{
		StartStopAttachPointCollection();
		UpdateAttachParticles();
		if (Singleton.Manager<ManInput>.inst.GetButtonRepeating(16))
		{
			RotatePlacingBlock(1);
		}
		else if (Singleton.Manager<ManInput>.inst.GetNegativeButtonRepeating(16))
		{
			RotatePlacingBlock(-1);
		}
	}
}
