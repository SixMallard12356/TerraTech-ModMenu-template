#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManTechSwapper : Singleton.Manager<ManTechSwapper>, IWorldTreadmill
{
	public class Operation
	{
		private int m_ID;

		private OperationType m_OperationType;

		private Tank m_Tech;

		private InventoryMetaData m_Inventory;

		private Dictionary<int, TechComponent.SerialData> m_SerialData;

		private EventNoParams m_OperationCompletedEvent;

		private float m_Progress;

		private float m_Duration;

		private float m_StartTime;

		private List<int> m_ContextIndices = new List<int>();

		private Vector3 m_TargetTechPos;

		private Matrix4x4 m_TechTransform;

		private Vector3 m_InitialTechPosition;

		private Vector3 m_FinalTechPosition;

		private Vector3 m_InitialTechCentre;

		private Vector3 m_BlockAnimEpicentre;

		private Quaternion m_InitialTechRot;

		private Quaternion m_FinalTechRot;

		private bool m_DetachBlocksBeforeInterp;

		private bool m_ConsolidateContextsByBlockType;

		private bool m_SpawnAtExactHeight;

		private bool m_WasAnchored;

		private bool m_Grounded;

		private bool m_ForcePlayerTech;

		private bool m_KeepBeamEnabled;

		private bool m_SpawnAnchored;

		private bool m_SerialiseRuntimeState;

		private Vector3 m_Velocity;

		private float m_MaxRadialThreshold;

		private float m_TechOriginalValue;

		private List<FactionSubTypes> m_TechMainCorps;

		public bool InProgress => m_Progress != 1f;

		public Operation(int ID)
		{
			m_ID = ID;
		}

		public bool IsOperatingOnTech(Tank tech)
		{
			if (InProgress && m_Tech.IsNotNull())
			{
				return m_Tech == tech;
			}
			return false;
		}

		public void InitSpawnTech(Vector3 scenePos, Quaternion sceneRot, TechData techData, InventoryMetaData inventory, bool exactHeight = false)
		{
			d.Assert(techData != null);
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnEmptyTechRef(0, scenePos, sceneRot, grounded: false, addToManager: true, techData.Name);
			Singleton.Manager<ManSpawn>.inst.SetTankController(trackedVisible.visible.tank);
			InitInternal(OperationType.SpawnTech, trackedVisible.visible.tank, techData.m_TechSaveState, inventory);
			BuildBlockListsFromTechData(techData, scenePos, sceneRot);
			m_TechOriginalValue = techData.GetValue();
			m_TechMainCorps = techData.GetMainCorporations();
			m_SpawnAtExactHeight = exactHeight;
			m_SpawnAnchored = techData.CheckIsAnchored();
			FinaliseInitInternal(trackedVisible.visible.tank);
		}

		public void InitDespawnTech(Tank tech, InventoryMetaData inventory)
		{
			InitInternal(OperationType.DespawnTech, tech, null, inventory);
			ReuseOrRecycleAttachedBlocks();
			FinaliseInitInternal(tech);
		}

		public void InitSwapTech(Tank tech, TechData techData, InventoryMetaData inventory)
		{
			InitInternal(OperationType.SwapTech, tech, techData.m_TechSaveState, inventory);
			m_ConsolidateContextsByBlockType = true;
			BuildBlockListsFromTechData(techData, tech.boundsCentreWorld, tech.trans.rotation);
			ReuseOrRecycleAttachedBlocks();
			m_Tech.name = techData.Name;
			m_TechOriginalValue = techData.GetValue();
			m_TechMainCorps = techData.GetMainCorporations();
			m_SpawnAnchored = techData.CheckIsAnchored();
			FinaliseInitInternal(tech);
		}

		public void InitAttachToTech(Tank tech, Vector3 centreWorld, Quaternion sceneRot, bool wasAnchored, List<TankBlock> looseBlocks, Dictionary<TankBlock, TankPreset.BlockSpec> specLookup, Dictionary<int, TechComponent.SerialData> serialData)
		{
			InitInternal(OperationType.AttachToTech, tech, serialData, InventoryMetaData.kUnrestrictedIntenvory);
			m_BlockAnimEpicentre = centreWorld;
			m_WasAnchored = wasAnchored;
			if (wasAnchored)
			{
				m_FinalTechPosition = centreWorld;
			}
			if (wasAnchored || tech.beam.IsActive || !m_Grounded)
			{
				m_FinalTechRot = sceneRot;
			}
			if (tech.blockman.blockCount > 0)
			{
				m_InitialTechRot = tech.trans.rotation;
			}
			m_TechOriginalValue = tech.OriginalValue;
			m_TechMainCorps = ((tech.MainCorps != null) ? new List<FactionSubTypes>(tech.MainCorps) : null);
			m_KeepBeamEnabled = tech.beam.IsActive;
			Singleton.Manager<ManTechSwapper>.inst.m_DoubleInclusionGuard.Clear();
			foreach (TankBlock looseBlock in looseBlocks)
			{
				if (!Singleton.Manager<ManTechSwapper>.inst.m_DoubleInclusionGuard.Contains(looseBlock))
				{
					Singleton.Manager<ManTechSwapper>.inst.m_DoubleInclusionGuard.Add(looseBlock);
					int num = Singleton.Manager<ManTechSwapper>.inst.AcquireContextBlock(m_ID);
					m_ContextIndices.Add(num);
					ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
					reference.block = looseBlock;
					reference.blockType = looseBlock.BlockType;
					d.Assert(specLookup.TryGetValue(looseBlock, out reference.serialData));
					reference.startPosScene = looseBlock.trans.position;
					reference.startRotScene = looseBlock.trans.rotation;
					reference.startScale = 1f;
					reference.endPos = reference.serialData.position;
					reference.endRot = new OrthoRotation(reference.serialData.orthoRotation);
					reference.endScale = 1f;
					reference.action = BlockAction.Detach_Reattach;
					bool suppressAnimation = wasAnchored && looseBlock.tank != null && looseBlock.tank.IsAnchored && reference.startPosScene.Approximately(m_FinalTechPosition + m_FinalTechRot * reference.endPos) && reference.startRotScene == m_FinalTechRot * reference.endRot;
					reference.suppressAnimation = suppressAnimation;
				}
			}
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = m_Tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator2.MoveNext())
			{
				TankBlock current2 = enumerator2.Current;
				if (!Singleton.Manager<ManTechSwapper>.inst.m_DoubleInclusionGuard.Contains(current2))
				{
					Singleton.Manager<ManTechSwapper>.inst.m_DoubleInclusionGuard.Add(current2);
					int num2 = Singleton.Manager<ManTechSwapper>.inst.AcquireContextBlock(m_ID);
					m_ContextIndices.Add(num2);
					ref ContextBlock reference2 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num2];
					reference2.block = current2;
					reference2.blockType = current2.BlockType;
					d.Assert(specLookup.TryGetValue(current2, out reference2.serialData));
					reference2.startPosScene = current2.trans.position;
					reference2.startRotScene = current2.trans.rotation;
					reference2.startScale = 1f;
					reference2.endPos = reference2.serialData.position;
					reference2.endRot = new OrthoRotation(reference2.serialData.orthoRotation);
					reference2.endScale = 1f;
					reference2.action = BlockAction.Reserialise;
					reference2.suppressAnimation = true;
				}
			}
			FinaliseInitInternal(tech);
		}

		public void SubscribeToCompletionCallback(Action completionHandler)
		{
			m_OperationCompletedEvent.Subscribe(completionHandler);
		}

		public void Reset()
		{
			m_OperationCompletedEvent.Clear();
		}

		public void UpdateAnimation()
		{
			if (m_Progress == -1f)
			{
				BeforeFirstUpdate();
				float maxDegreesDelta = Singleton.Manager<ManTechSwapper>.inst.m_BlockRotPerSecond / 10f;
				for (int i = 0; i < m_ContextIndices.Count; i++)
				{
					int num = m_ContextIndices[i];
					ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
					if (reference.ownerID == m_ID)
					{
						reference.TransformEndPos(m_TechTransform);
						Vector3 spawnPos = ((m_OperationType == OperationType.SpawnTech) ? m_BlockAnimEpicentre : m_InitialTechCentre);
						reference.SetSpawnPos(spawnPos);
						reference.rotationIncrement = Quaternion.RotateTowards(Quaternion.identity, UnityEngine.Random.rotation, maxDegreesDelta);
						float magnitude = (reference.startPosScene - m_BlockAnimEpicentre).magnitude;
						float magnitude2 = (reference.endPos - m_BlockAnimEpicentre).magnitude;
						reference.radialDistStart = magnitude;
						reference.radialDistEnd = magnitude2;
						float num2 = (magnitude + magnitude2) * 0.5f;
						reference.radialScale = ((num2 > m_MaxRadialThreshold) ? (m_MaxRadialThreshold / num2) : 1f);
						reference.varianceSeed = UnityEngine.Random.value * 2f - 1f;
					}
				}
			}
			float progress = m_Progress;
			float num3 = Time.time - m_StartTime;
			if (m_Duration - num3 < 0.005f)
			{
				m_Progress = 1f;
			}
			else
			{
				m_Progress = num3 / m_Duration;
			}
			m_Tech.trans.SetPositionIfChanged(Vector3.Lerp(m_InitialTechPosition, m_FinalTechPosition, m_Progress));
			m_Tech.trans.SetRotationIfChanged(Quaternion.Lerp(m_InitialTechRot, m_FinalTechRot, m_Progress));
			for (int j = 0; j < m_ContextIndices.Count; j++)
			{
				int num4 = m_ContextIndices[j];
				ref ContextBlock reference2 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num4];
				if (reference2.ownerID == m_ID)
				{
					reference2.Interpolate(m_Progress, m_BlockAnimEpicentre);
					if (progress < 0.5f && m_Progress >= 0.5f && reference2.block.BlockType == reference2.serialData.GetBlockType())
					{
						reference2.block.SetSkinByUniqueID(reference2.serialData.m_SkinID, reference2.serialData.GetBlockType());
					}
				}
			}
			if (m_Progress == 1f)
			{
				AfterLastUpdate();
			}
		}

		public void OffsetSceneCoords(Vector3 offset)
		{
			for (int i = 0; i < m_ContextIndices.Count; i++)
			{
				int num = m_ContextIndices[i];
				ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
				if (reference.ownerID == m_ID)
				{
					reference.OffsetSceneCoords(offset);
				}
			}
			m_TargetTechPos += offset;
			m_InitialTechCentre += offset;
			m_BlockAnimEpicentre += offset;
			m_InitialTechPosition += offset;
			m_FinalTechPosition += offset;
		}

		private void InitInternal(OperationType opType, Tank tech, Dictionary<int, TechComponent.SerialData> serialData, InventoryMetaData inventory)
		{
			d.Assert(tech, "null tech");
			d.Assert(opType == OperationType.DespawnTech || serialData != null, "null SerialData");
			m_Duration = Singleton.Manager<ManTechSwapper>.inst.m_DefaultDuration;
			m_Progress = -1f;
			m_StartTime = Time.time;
			m_ContextIndices.Clear();
			m_ConsolidateContextsByBlockType = false;
			m_DetachBlocksBeforeInterp = false;
			m_SpawnAtExactHeight = false;
			m_WasAnchored = false;
			m_Grounded = tech.grounded;
			m_ForcePlayerTech = false;
			m_KeepBeamEnabled = false;
			m_SpawnAnchored = false;
			m_SerialiseRuntimeState = false;
			m_Velocity = tech.rbody.velocity;
			m_MaxRadialThreshold = tech.blockBounds.extents.magnitude;
			m_OperationType = opType;
			m_Tech = tech;
			m_SerialData = serialData;
			m_Inventory = inventory;
			m_TargetTechPos = (m_Grounded ? Singleton.Manager<ManWorld>.inst.ProjectToGround(tech.boundsCentreWorld) : tech.boundsCentreWorld);
			m_InitialTechCentre = tech.WorldCenterOfMass;
			m_BlockAnimEpicentre = m_InitialTechCentre;
			m_InitialTechPosition = tech.trans.position;
			m_FinalTechPosition = tech.trans.position;
			m_InitialTechRot = tech.rootBlockTrans.rotation;
			m_FinalTechRot = tech.rootBlockTrans.rotation;
		}

		private void FinaliseInitInternal(Tank tech)
		{
			if (m_Grounded && !m_WasAnchored)
			{
				Vector3 outNormal;
				if (m_SpawnAnchored)
				{
					outNormal = Vector3.up;
				}
				else
				{
					Singleton.Manager<ManWorld>.inst.GetTerrainNormal(m_FinalTechPosition, out outNormal);
				}
				m_FinalTechRot = Maths.LookRotationUpInvariant(tech.rootBlockTrans.forward, outNormal);
			}
		}

		private void BuildBlockListsFromTechData(TechData techData, Vector3 techPosScene, Quaternion techRotScene)
		{
			for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
			{
				int num = Singleton.Manager<ManTechSwapper>.inst.AcquireContextBlock(m_ID);
				m_ContextIndices.Add(num);
				BlockTypes blockType = techData.m_BlockSpecs[i].GetBlockType();
				blockType = Singleton.Manager<ManSpawn>.inst.GetAliasedBlockID(blockType);
				if (m_ConsolidateContextsByBlockType)
				{
					Singleton.Manager<ManTechSwapper>.inst.GetContextListForBlockType(blockType).Add(num);
				}
				ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
				reference.blockType = blockType;
				reference.serialData = techData.m_BlockSpecs[i];
				reference.serialData.m_SkinID = techData.GetSkinID(i);
				reference.startPosScene = techPosScene;
				reference.startRotScene = techRotScene;
				reference.startScale = 0f;
				reference.endPos = techData.m_BlockSpecs[i].position;
				reference.endRot = new OrthoRotation(techData.m_BlockSpecs[i].orthoRotation);
				reference.endScale = 1f;
				reference.action = BlockAction.Spawn_Attach;
			}
		}

		private void ReuseOrRecycleAttachedBlocks()
		{
			d.Assert(m_Tech != null);
			_ = m_Tech.blockBounds.center;
			Vector3 boundsCentreWorld = m_Tech.boundsCentreWorld;
			m_DetachBlocksBeforeInterp = true;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_Tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				int num = -1;
				List<int> list = null;
				if (m_ConsolidateContextsByBlockType)
				{
					list = Singleton.Manager<ManTechSwapper>.inst.GetContextListForBlockType(current.BlockType);
					for (int i = 0; i < list.Count; i++)
					{
						ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[list[i]];
						switch (reference.action)
						{
						case BlockAction.Spawn_Attach:
							if (reference.endPos == current.trans.localPosition)
							{
								num = list[i];
								reference.action = BlockAction.Detach_Reattach;
								reference.block = current;
								reference.startPosScene = current.trans.position;
								reference.startRotScene = current.trans.rotation;
								reference.startScale = 1f;
								i = list.Count;
							}
							break;
						case BlockAction.Detach_Recycle:
							i = list.Count;
							break;
						}
					}
				}
				if (num == -1)
				{
					num = Singleton.Manager<ManTechSwapper>.inst.AcquireContextBlock(m_ID);
					m_ContextIndices.Add(num);
					list?.Add(num);
					ref ContextBlock reference2 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
					reference2.block = current;
					reference2.startPosScene = current.trans.position;
					reference2.startRotScene = current.trans.rotation;
					reference2.startScale = 1f;
					reference2.endPos = boundsCentreWorld;
					reference2.endRot = UnityEngine.Random.rotation;
					reference2.endScale = 0f;
					reference2.action = BlockAction.Detach_Recycle;
				}
			}
			if (!m_ConsolidateContextsByBlockType)
			{
				return;
			}
			for (int j = 0; j < Singleton.Manager<ManTechSwapper>.inst.m_ContextsByBlockType.Length; j++)
			{
				List<int> list2 = Singleton.Manager<ManTechSwapper>.inst.m_ContextsByBlockType[j];
				if (list2 == null || list2.Count == 0)
				{
					continue;
				}
				int num2 = list2.Count - 1;
				for (int k = 0; k < list2.Count; k++)
				{
					ref ContextBlock reference3 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[list2[num2]];
					if (reference3.action != BlockAction.Detach_Recycle)
					{
						break;
					}
					ref ContextBlock reference4 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[list2[k]];
					switch (reference4.action)
					{
					case BlockAction.Spawn_Attach:
						reference4.block = reference3.block;
						reference4.startPosScene = reference3.startPosScene;
						reference4.startRotScene = reference3.startRotScene;
						reference4.startScale = 1f;
						reference4.action = BlockAction.Detach_Reattach;
						Singleton.Manager<ManTechSwapper>.inst.ReleaseContextBlock(list2[num2]);
						num2--;
						break;
					case BlockAction.Detach_Recycle:
						k = list2.Count;
						break;
					}
				}
				list2.Clear();
			}
		}

		private void BeforeFirstUpdate()
		{
			Vector3 boundsCentreWorld = m_Tech.boundsCentreWorld;
			if (m_DetachBlocksBeforeInterp)
			{
				m_Tech.blockman.Disintegrate(applyPhysicsKick: false, allowEmpty: true);
				m_Tech.Anchors.UnanchorAll(playAnim: false);
			}
			if (!m_KeepBeamEnabled)
			{
				m_Tech.beam.EnableBeam(enable: false, force: true);
			}
			Bounds bounds = new Bounds(Vector3.zero, Vector3.one * float.MinValue);
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < m_ContextIndices.Count; i++)
			{
				ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[m_ContextIndices[i]];
				if (reference.ownerID != m_ID || reference.action == BlockAction.Detach_Recycle)
				{
					continue;
				}
				TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(reference.blockType);
				int num3 = 0;
				if (blockPrefab != null)
				{
					IntVector3[] filledCells = blockPrefab.filledCells;
					foreach (IntVector3 intVector in filledCells)
					{
						bounds.Encapsulate(reference.endPos + reference.endRot * intVector);
					}
					ModuleTechController component;
					if ((object)(component = blockPrefab.GetComponent<ModuleTechController>()) != null)
					{
						num3 = component.GetPriority();
					}
				}
				if (num == -1 || num3 > num2)
				{
					num = i;
					num2 = num3;
				}
			}
			if (bounds.extents.x < 0f)
			{
				bounds.extents = Vector3.zero;
			}
			Quaternion quaternion = Quaternion.identity;
			if (num != -1 && !m_WasAnchored)
			{
				quaternion = Quaternion.Inverse(Singleton.Manager<ManTechSwapper>.inst.m_Context[m_ContextIndices[num]].endRot);
				bounds.center = quaternion * bounds.center;
				bounds.extents = quaternion * bounds.extents;
			}
			float num4 = m_FinalTechPosition.y;
			for (int k = 0; k < m_ContextIndices.Count; k++)
			{
				int num5 = m_ContextIndices[k];
				ref ContextBlock reference2 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num5];
				if (reference2.ownerID != m_ID)
				{
					continue;
				}
				if (reference2.action == BlockAction.Spawn_Attach)
				{
					BlockTypes blockType = reference2.blockType;
					if (!Singleton.Manager<ManSpawn>.inst.IsBlockAvailableForTechSpawn(blockType, m_Inventory))
					{
						Singleton.Manager<ManTechSwapper>.inst.ReleaseContextBlock(num5);
						continue;
					}
					TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock(blockType, boundsCentreWorld, UnityEngine.Random.rotation);
					d.Assert(tankBlock.IsNotNull(), "Block spawn failure: deprecated? " + blockType);
					reference2.block = tankBlock;
					tankBlock.SetSkinByUniqueID(reference2.serialData.m_SkinID, reference2.serialData.GetBlockType());
					if (m_Inventory.TakesAndStoresBlocks)
					{
						m_Inventory.m_Inventory?.HostConsumeItem(-1, blockType);
					}
				}
				else if (reference2.action != BlockAction.Reserialise && (bool)reference2.block.tank)
				{
					m_ForcePlayerTech = m_Tech == Singleton.playerTank;
					bool num6 = reference2.block.Anchor.IsNotNull() && reference2.block.Anchor.IsAnchored;
					reference2.block.Separate(manualRemove: true, allowHeadlessTech: true);
					if (num6 && reference2.suppressAnimation)
					{
						reference2.block.Anchor.ActivateAnchorGeometry(active: true, enableCollision: false, playAnim: false);
					}
				}
				if (reference2.action == BlockAction.Spawn_Attach && m_SpawnAnchored && reference2.block.Anchor != null)
				{
					TankBlock block = reference2.block;
					ModuleAnchor anchor = block.Anchor;
					if (!anchor.IsSkyAnchor && Vector3.Dot(m_FinalTechRot * reference2.endRot * Vector3.up, Vector3.up) > 0.9f)
					{
						Vector3 vector = m_FinalTechPosition - m_FinalTechRot * bounds.center;
						Vector3 vector2 = Vector3.up * (anchor.Anchor.m_SnapToleranceUp - 0.1f);
						Vector3 vector3 = block.trans.InverseTransformPoint(anchor.Anchor.GroundPointInitial) + vector2;
						Vector3 vector4 = m_FinalTechRot * (reference2.endPos + reference2.endRot * vector3);
						Vector3 scenePos = vector + vector4;
						num4 = Mathf.Max(num4, (Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos) - vector4).y);
					}
				}
				reference2.block.visible.EnablePhysics(enable: false);
			}
			if (m_OperationType != OperationType.AttachToTech)
			{
				m_BlockAnimEpicentre = m_TargetTechPos;
				if (!m_SpawnAtExactHeight)
				{
					float num7 = Mathf.Max(num4 - m_TargetTechPos.y, bounds.extents.y + Singleton.Manager<ManTechSwapper>.inst.m_WheelClearancePad);
					m_BlockAnimEpicentre += m_FinalTechRot * Vector3.up * num7;
				}
				m_FinalTechPosition = m_BlockAnimEpicentre - m_FinalTechRot * bounds.center;
			}
			if (m_Grounded)
			{
				m_BlockAnimEpicentre -= m_FinalTechRot * Vector3.up * (bounds.extents.y + 0.5f);
			}
			m_MaxRadialThreshold = Mathf.Max(m_MaxRadialThreshold, bounds.extents.magnitude);
			m_FinalTechRot *= quaternion;
			m_TechTransform = Matrix4x4.TRS(m_FinalTechPosition, m_FinalTechRot, Vector3.one);
			m_Tech.visible.SetLockTimout(Visible.LockTimerTypes.Interactible, m_Duration);
			m_Tech.visible.SetLockTimout(Visible.LockTimerTypes.BlocksAttachable, m_Duration);
			m_Tech.visible.SetLockTimout(Visible.LockTimerTypes.Grabbable, m_Duration);
			m_Tech.visible.EnablePhysics(enable: false);
		}

		private void AfterLastUpdate()
		{
			d.Assert(m_Progress == 1f);
			if (m_OperationType == OperationType.DespawnTech)
			{
				for (int i = 0; i < m_ContextIndices.Count; i++)
				{
					int num = m_ContextIndices[i];
					ref ContextBlock reference = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num];
					if (reference.ownerID == m_ID)
					{
						d.Assert(reference.action == BlockAction.Detach_Recycle);
						TankBlock block = reference.block;
						block.trans.localScale = Vector3.one;
						block.visible.EnablePhysics(enable: true);
						Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(block);
						if (m_Inventory.TakesAndStoresBlocks)
						{
							m_Inventory.m_Inventory.HostAddItem(block.BlockType);
						}
						Singleton.Manager<ManTechSwapper>.inst.ReleaseContextBlock(m_ContextIndices[i]);
					}
				}
				m_Tech.visible.RemoveFromGame();
			}
			else
			{
				bool allowHeadlessTech = m_OperationType == OperationType.AttachToTech;
				ManSpawn.PopulateTechHelper populateTechHelper = new ManSpawn.PopulateTechHelper(m_Tech, spawningNew: true, recycleFailedAdds: false, null, allowHeadlessTech, m_SpawnAnchored || m_WasAnchored);
				try
				{
					for (int j = 0; j < m_ContextIndices.Count; j++)
					{
						int num2 = m_ContextIndices[j];
						ref ContextBlock reference2 = ref Singleton.Manager<ManTechSwapper>.inst.m_Context[num2];
						if (reference2.ownerID != m_ID)
						{
							continue;
						}
						reference2.block.visible.EnablePhysics(enable: true);
						if (reference2.action == BlockAction.Detach_Recycle)
						{
							TankBlock block2 = reference2.block;
							block2.trans.localScale = Vector3.one;
							Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(block2);
							if (m_Inventory.TakesAndStoresBlocks)
							{
								m_Inventory.m_Inventory.HostAddItem(block2.BlockType);
							}
						}
						else
						{
							bool alreadyAttached = reference2.action == BlockAction.Reserialise;
							TankPreset.BlockSpec serialData = reference2.serialData;
							if (!m_SerialiseRuntimeState)
							{
								Dictionary<int, Module.SerialData> saveStateDest = null;
								if (reference2.block.GetComponent<ModuleItemConveyor>().IsNotNull())
								{
									ModuleItemConveyor.TryCopyRuntimeState(serialData, ref saveStateDest);
								}
								serialData.saveState = saveStateDest;
							}
							populateTechHelper.AddBlock(reference2.block, serialData, alreadyAttached);
						}
						Singleton.Manager<ManTechSwapper>.inst.ReleaseContextBlock(m_ContextIndices[j]);
					}
				}
				finally
				{
					((IDisposable)populateTechHelper/*cast due to .constrained prefix*/).Dispose();
				}
				if (!m_Tech.visible.Killed)
				{
					m_Tech.visible.EnablePhysics(enable: true);
					m_Tech.SerializeEvent.Send(paramA: false, m_SerialData);
					m_Tech.OriginalValue = m_TechOriginalValue;
					m_Tech.MainCorps = m_TechMainCorps;
					m_Tech.DiscoverBlocksOnTech();
					if (m_Grounded)
					{
						m_Tech.grounded = true;
					}
					m_Tech.FixupAnchors(forceAnchor: false, m_SpawnAnchored);
					if (m_Tech.IsAnchored && m_Tech.beam.IsActive)
					{
						m_Tech.beam.EnableBeam(enable: false, force: true);
					}
					if (m_ForcePlayerTech)
					{
						Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(m_Tech);
					}
					m_Tech.rbody.velocity = m_Velocity;
					Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(m_Tech);
				}
			}
			m_ContextIndices.Clear();
			m_OperationCompletedEvent.Send();
			m_OperationCompletedEvent.Clear();
		}
	}

	private enum OperationType
	{
		NULL,
		SpawnTech,
		DespawnTech,
		SwapTech,
		AttachToTech
	}

	private enum BlockAction
	{
		Spawn_Attach,
		Detach_Recycle,
		Detach_Reattach,
		Reserialise
	}

	private struct ContextBlock
	{
		public int ownerID;

		public BlockAction action;

		public BlockTypes blockType;

		public TankBlock block;

		public TankPreset.BlockSpec serialData;

		public Vector3 startPosScene;

		public Quaternion startRotScene;

		public float startScale;

		public Vector3 endPos;

		public Quaternion endRot;

		public float endScale;

		public Quaternion rotationIncrement;

		public float radialDistStart;

		public float radialDistEnd;

		public float radialScale;

		public float varianceSeed;

		public bool suppressAnimation;

		public void SetSpawnPos(Vector3 newPos)
		{
			if (action == BlockAction.Spawn_Attach)
			{
				startPosScene = newPos;
			}
		}

		public void TransformEndPos(Matrix4x4 transform)
		{
			if (action != BlockAction.Detach_Recycle)
			{
				endPos = transform.MultiplyPoint3x4(endPos);
				endRot = transform.rotation * endRot;
			}
		}

		public void Interpolate(float paramBase, Vector3 epicentre)
		{
			if (!suppressAnimation)
			{
				float num = paramBase + varianceSeed * Singleton.Manager<ManTechSwapper>.inst.m_InterpVariance * ParabolaLUT(paramBase) * (1f / (float)Math.PI);
				block.trans.SetLocalScaleIfChanged(Vector3.one * Mathf.Lerp(startScale, endScale, num));
				Quaternion a = Quaternion.Slerp(block.trans.rotation, block.trans.rotation * rotationIncrement, Time.deltaTime * 10f);
				block.trans.rotation = Quaternion.Slerp(a, endRot, Singleton.Manager<ManTechSwapper>.inst.m_BlockRotBlendRamp.Evaluate(num));
				Vector3 normalized = (Vector3.Lerp(startPosScene, endPos, num) - epicentre).normalized;
				float num2 = Mathf.Lerp(radialDistStart, radialDistEnd, num);
				float num3 = Mathf.Lerp(1f, 1f + Singleton.Manager<ManTechSwapper>.inst.m_BlockMotionExpansionPercent * 0.01f * radialScale, ParabolaLUT(num));
				block.trans.position = epicentre + normalized * num2 * num3;
			}
		}

		public void OffsetSceneCoords(Vector3 offset)
		{
			startPosScene += offset;
			endPos += offset;
		}
	}

	[SerializeField]
	private float m_DefaultDuration = 1f;

	[SerializeField]
	private float m_WheelClearancePad = 1.5f;

	[SerializeField]
	private float m_BlockRotPerSecond = 300f;

	[SerializeField]
	private AnimationCurve m_BlockRotBlendRamp;

	[SerializeField]
	private float m_BlockMotionExpansionPercent = 100f;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_InterpVariance = 1f;

	private List<Operation> m_Operations = new List<Operation>();

	private ContextBlock[] m_Context = new ContextBlock[128];

	private int m_FirstFreeContextBlock;

	private List<int>[] m_ContextsByBlockType = new List<int>[EnumValuesIterator<BlockTypes>.Count];

	private HashSet<TankBlock> m_DoubleInclusionGuard = new HashSet<TankBlock>();

	private const float k_CompletionTimeEpsilon = 0.005f;

	private const float k_BlockRotInterpGranularity = 10f;

	private const float k_InversePI = 1f / (float)Math.PI;

	private const int k_ParabolaLUTResolution = 256;

	private float[] m_ParabolaLUT;

	public float WheelClearancePad => m_WheelClearancePad;

	public bool CheckOperationInProgress()
	{
		for (int i = 0; i < m_Operations.Count; i++)
		{
			if (m_Operations[i].InProgress)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckOperatingOnTech(Tank tech)
	{
		for (int i = 0; i < m_Operations.Count; i++)
		{
			if (m_Operations[i].IsOperatingOnTech(tech))
			{
				return true;
			}
		}
		return false;
	}

	public bool TryGetInProgressTechOperation(Tank targetTech, out Operation outOp)
	{
		for (int i = 0; i < m_Operations.Count; i++)
		{
			if (m_Operations[i].IsOperatingOnTech(targetTech))
			{
				outOp = m_Operations[i];
				return true;
			}
		}
		outOp = null;
		return false;
	}

	public Operation GetNewOperation()
	{
		Operation operation = null;
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "this code is not network-aware");
		for (int i = 0; i < m_Operations.Count; i++)
		{
			if (!m_Operations[i].InProgress)
			{
				operation = m_Operations[i];
				operation.Reset();
				break;
			}
		}
		if (operation == null)
		{
			operation = new Operation(m_Operations.Count + 1);
			m_Operations.Add(operation);
		}
		return operation;
	}

	private int AcquireContextBlock(int operationID)
	{
		while (m_FirstFreeContextBlock < m_Context.Length)
		{
			if (m_Context[m_FirstFreeContextBlock].ownerID == 0)
			{
				m_Context[m_FirstFreeContextBlock].ownerID = operationID;
				return m_FirstFreeContextBlock++;
			}
			m_FirstFreeContextBlock++;
		}
		Array.Resize(ref m_Context, m_Context.Length * 2);
		m_Context[m_FirstFreeContextBlock].ownerID = operationID;
		return m_FirstFreeContextBlock++;
	}

	private void ReleaseContextBlock(int index)
	{
		m_Context[index].ownerID = 0;
		m_Context[index].block = null;
		m_Context[index].suppressAnimation = false;
		m_FirstFreeContextBlock = Mathf.Min(m_FirstFreeContextBlock, index);
	}

	private List<int> GetContextListForBlockType(BlockTypes blockType)
	{
		if ((int)blockType >= m_ContextsByBlockType.Length)
		{
			int newSize = m_ContextsByBlockType.Length + (int)(blockType - m_ContextsByBlockType.Length + 1) * 2;
			Array.Resize(ref m_ContextsByBlockType, newSize);
		}
		List<int> list = m_ContextsByBlockType[(int)blockType];
		if (list == null)
		{
			list = new List<int>();
			Singleton.Manager<ManTechSwapper>.inst.m_ContextsByBlockType[(int)blockType] = list;
		}
		return list;
	}

	private static float ParabolaLUT(float t)
	{
		d.Assert(Singleton.Manager<ManTechSwapper>.inst);
		int num = (int)(t * 256f + 0.5f);
		return Singleton.Manager<ManTechSwapper>.inst.m_ParabolaLUT[num];
	}

	public void OnMoveWorldOrigin(IntVector3 offset)
	{
		foreach (Operation operation in m_Operations)
		{
			if (operation.InProgress)
			{
				operation.OffsetSceneCoords(offset);
			}
		}
	}

	private void OnManagersCreated()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
	}

	private void Awake()
	{
		Singleton.DoOnceAfterStart(OnManagersCreated);
		m_ParabolaLUT = new float[257];
		m_ParabolaLUT[256] = 0f;
		for (int i = 0; i < 256; i++)
		{
			m_ParabolaLUT[i] = Mathf.Sin((float)i * (float)Math.PI / 256f);
		}
	}

	private void Update()
	{
		foreach (Operation operation in m_Operations)
		{
			if (operation.InProgress)
			{
				operation.UpdateAnimation();
			}
		}
	}
}
