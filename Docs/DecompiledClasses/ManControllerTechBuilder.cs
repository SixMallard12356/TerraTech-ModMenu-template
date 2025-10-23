#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManControllerTechBuilder : Singleton.Manager<ManControllerTechBuilder>
{
	public enum CoordinateSpace
	{
		Screen,
		World
	}

	public enum ControlMode
	{
		Step2DTo3DClosestAxis,
		ScreenSpace_FirstHit,
		ScreenSpace_Smart,
		Pointer
	}

	public enum InteractionMode
	{
		SelectBlockOnTech,
		PlaceBlock,
		SelectInventoryBlock,
		None
	}

	public enum BucketDistanceMetric
	{
		FloorToInt,
		FloorToHalfUnit,
		SquaredFloorToInt
	}

	[Serializable]
	public class RayStepValues
	{
		public float m_InputThreshold;

		public CoordinateSpace m_RayStepDistanceMode = CoordinateSpace.World;

		public float m_ScreenSpaceStepSizePx = 10f;

		public float m_WorldSpaceStepSize = 0.15f;

		public bool m_ScreenSpaceCullPastCentreBounds;

		[Range(-1f, 1f)]
		public float m_ScreenSpaceCullCutOffValue = 0.25f;

		public bool m_UseSphereCast;

		public float m_SphereCastRadiusWorld = 0.15f;
	}

	private struct CachedPlacementGroupData
	{
		public float bestScore;

		public float scorePenalty;

		public IntVector3 apPos;

		public Vector3 apNormal;

		public IntVector3 localCellPos;

		public Vector3 refPosWorld;

		public float dist;

		public BlockPlacementCollector.Collection placementGroup;
	}

	public bool m_Enable;

	public bool m_InBuildMode;

	public InteractionMode m_InteractionMode = InteractionMode.None;

	[Header("Input")]
	public bool m_MoveSelectionOnConfirm;

	public float m_AxisInputTimeout = 0.3f;

	public float m_AxisOverlapDiscardThreshold = 0.8f;

	public bool m_AxisMagnitudeIncreasesStepSpeed;

	public float m_AxisTimeoutMagnitudeEffect = 0.5f;

	public bool m_DelayOnFirstAxisInput;

	public float m_FirstAxisInputTimeout = 0.3f;

	[Header("Block Selection on Tech")]
	public ControlMode m_ControlMode;

	public RayStepValues[] m_RayStepValues;

	public bool m_IgnoreBlockSelectOutsideScreen;

	public bool m_PreserveCellSelection;

	public int m_MinHitsForAutoAccept = 1;

	public bool m_GroundPickupBehindModifierKey;

	[Header("Block Placement")]
	public BlockTypes m_BlockToPlace;

	public List<BlockTypes> m_BlockToPlaceSelection;

	[Range(0f, 90f)]
	public float m_InputAngleCutoff = 90f;

	public bool m_UseApCellCentre;

	public bool m_UseInputStartOffset;

	public float m_InputStartOffset;

	public bool m_CullByDistanceBucket;

	public BucketDistanceMetric m_BucketDistanceMetric;

	public bool m_SortByDistanceFromSource;

	public bool m_ApplyCameraZBias;

	public bool m_CullOverlappingCells;

	public float m_OverlappingCellRadiusThreshold = 0.5f;

	[Space(6f)]
	public bool m_DiscardBelowScoreThreshold;

	public float m_MinScoreThreshold;

	public bool m_DiscardAboveScorePenaltyThreshold;

	public float m_MaxScorePenaltyThreshold;

	[Space(6f)]
	public bool m_DiscardInvisiblePositions;

	public bool m_UseVertexHalfPoints;

	[Space(6f)]
	public bool m_StepPlacementsOnSameFocalCell;

	[Range(0f, 90f)]
	public float m_PlacementChangeInputAngleAllowance = 15f;

	public bool m_StepRotationsOnSameFocalCell;

	[Range(0f, 90f)]
	public float m_OrientationChangeInputAngleAllowance = 60f;

	public bool m_CalcPlacementChangeAngleInScreenSpace = true;

	public float m_MinCentreChangeDistance = 0.3f;

	public float m_DistanceInfluenceOnScore;

	public float m_MaxDistanceInfluenceRange;

	public AnimationCurve m_DistanceScoreInfluenceCurve;

	[Header("Placement weight modifiers")]
	public bool m_UseWeightedScoring;

	public bool m_DiscardAPsBehindCamera;

	public bool m_UseWorldAngleOffsetInfluence;

	[Range(0f, 1f)]
	public float m_WorldAngleOffsetInfluence;

	public bool m_UseScreenAngleOffsetInfluence;

	[Range(0f, 1f)]
	public float m_ScreenAngleOffsetInfluence;

	public bool m_UseDistanceToInputPlaneInfluence;

	[Range(0f, 1f)]
	public float m_DistanceToInputPlaneInfluence;

	public bool m_UseDistanceToVerticalInputPlaneInfluence;

	[Range(0f, 1f)]
	public float m_DistanceToVerticalInputPlaneInfluence;

	public bool m_UseDistanceToInputPlaneCamAlignedInfluence;

	[Range(0f, 1f)]
	public float m_DistanceToInputPlaneCamAlignedInfluence;

	public bool m_UseDistanceToApInfluence;

	[Range(0f, 1f)]
	public float m_DistanceToApInfluence;

	public bool m_UseDistanceToApInfluenceScreen;

	[Range(0f, 1f)]
	public float m_DistanceToApInfluenceScreen;

	public bool m_UseMatchCurrentAPNormalInfluence;

	[Range(0f, 1f)]
	public float m_MatchCurrentAPNormalInfluence;

	public bool m_UseMatchCurrentAPNormalInCameraSpaceInfluence;

	[Range(0f, 1f)]
	public float m_MatchCurrentAPNormalInCameraSpaceInfluence;

	public bool m_UseDepthOnTechInfluence;

	[Range(0f, 1f)]
	public float m_DepthOnTechInfluence;

	[Header("Visualiser")]
	public bool m_ShowSelectedCell;

	public bool m_ShowSelectedTarget;

	public bool m_ShowInputDirection;

	public ObjectHighlight m_BlockHighlightPrefab;

	public Transform m_CellHighlightPrefab;

	public Material m_CellHighlightMaterialTemplate;

	[Header("Gizmo visualiser")]
	public bool m_ShowRaysCast;

	public bool m_ShowPlacementCellCandidates;

	public float m_PlacementCellCandidateBoxAlpha = 0.8f;

	public bool m_ShowCellValueLabels;

	public bool m_ShowInputPlane;

	public bool m_DrawDiscardedCellsBehindStartPoint;

	public bool m_DrawVisibilityTestRays;

	public bool m_DrawClosestApproach;

	public bool m_DrawAPNormals;

	private int m_NumHighlightCellsInUse;

	private List<Transform> m_CellHighlightObjects = new List<Transform>();

	private Dictionary<Color, Material> m_CellHighlightMaterials = new Dictionary<Color, Material>();

	private ObjectHighlight m_BlockHighlight;

	private float m_LastAxisInputTime;

	private bool m_MoveToPreviewControl;

	private Vector2 m_BuildVectorRaw;

	private Vector2 m_BuildVector;

	private Vector2 m_BuildVectorPrev;

	private RayStepValues m_CurrentRayStepValues;

	private GameObject m_DebugInputPointerObj;

	private bool m_PreviewMove;

	private int m_ContinuedAxisInputCount;

	private IntVector3 m_CurrentGridSelection;

	private IntVector3 m_NextGridSelection;

	private TankBlock m_LastValidSelectedBlock;

	private TankBlock m_SelectedBlock;

	private bool m_HoldingLooseBlock;

	private bool m_HeldBlockWasAttachedToTech;

	private Vector3 m_HeldDetachedBlockPreDetachLocalPos;

	private OrthoRotation m_HeldDetachedBlockPreDetachLocalRot;

	private BlockPlacementCollector.Placement m_CurrentPlacement;

	private BlockPlacementCollector.Placement m_NextPlacement;

	private CachedPlacementGroupData m_CurrentMatchedGroup;

	private CachedPlacementGroupData m_NextMatchedGroup;

	private OrthoRotation m_PreferredOrientation;

	private HashSet<IntVector3> m_CurrentPlacementFilledCellList = new HashSet<IntVector3>();

	private List<CachedPlacementGroupData> m_LastPlacementData;

	private bool m_GroundPickupEnabled;

	private TankBlock m_NearbyPickupBlock;

	private bool m_AutoSelectNextPlacement;

	private Vector2 m_AutoSelectInputDir;

	private List<BlockPlacementCollector.Placement> m_AvailablePlacementsAtCurrentCell = new List<BlockPlacementCollector.Placement>();

	private List<TankBlock> s_CachedNearbyBlocks = new List<TankBlock>();

	public BlockPlacementCollector.Placement CurrentPlacement => m_CurrentPlacement;

	public TankBlock SelectedBlock => m_SelectedBlock;

	public TankBlock NearbyPickupBlock => m_NearbyPickupBlock;

	private void HandleControlInput()
	{
		m_MoveToPreviewControl = false;
		bool enable = m_Enable;
		bool inBuildMode = m_InBuildMode;
		inBuildMode = Singleton.playerTank != null && Singleton.playerTank.beam.IsActive;
		if (!m_InBuildMode && inBuildMode)
		{
			inBuildMode = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		}
		enable = inBuildMode;
		if (enable != m_Enable)
		{
			EnableControllerBuilding(enable);
		}
		if (inBuildMode != m_InBuildMode)
		{
			m_InBuildMode = inBuildMode;
			InteractionMode interactionMode = ((!m_InBuildMode) ? InteractionMode.None : InteractionMode.SelectBlockOnTech);
			SetInteractionMode(interactionMode);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, m_InBuildMode, UIInputMode.BlockBuilding);
		}
		if (!m_Enable)
		{
			return;
		}
		Tank playerTank = Singleton.playerTank;
		if (!(playerTank != null))
		{
			return;
		}
		bool flag = false;
		if (!m_InBuildMode)
		{
			return;
		}
		m_BuildVectorPrev = m_BuildVector;
		m_BuildVector = Vector2.zero;
		Vector2 buildVectorRaw = m_BuildVectorRaw;
		m_BuildVectorRaw = Vector2.zero;
		if (!Singleton.Manager<ManInput>.inst.GetButton(64))
		{
			m_BuildVectorRaw = Singleton.Manager<ManInput>.inst.GetAxis2DRaw(9, 8);
		}
		if (m_BuildVectorRaw.sqrMagnitude > 1f)
		{
			m_BuildVectorRaw = m_BuildVectorRaw.normalized;
		}
		if (m_BuildVectorRaw.sqrMagnitude < 1.0000001E-06f || Vector2.Dot(m_BuildVectorRaw.normalized, buildVectorRaw.normalized) < 0.3f)
		{
			m_LastAxisInputTime = -1f;
			m_ContinuedAxisInputCount = 0;
		}
		float num = m_AxisInputTimeout;
		if (m_DelayOnFirstAxisInput && m_ContinuedAxisInputCount < 2)
		{
			num = m_FirstAxisInputTimeout;
		}
		else if (m_AxisMagnitudeIncreasesStepSpeed)
		{
			num -= m_BuildVectorRaw.magnitude * m_AxisTimeoutMagnitudeEffect * m_AxisInputTimeout;
		}
		if (Time.time > m_LastAxisInputTime + num)
		{
			m_LastAxisInputTime = Time.time;
			m_BuildVector = m_BuildVectorRaw;
			m_ContinuedAxisInputCount++;
		}
		m_GroundPickupEnabled = m_InteractionMode == InteractionMode.SelectBlockOnTech && (!m_GroundPickupBehindModifierKey || Singleton.Manager<ManInput>.inst.GetNegativeButton(12));
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(10))
		{
			if (m_InteractionMode == InteractionMode.SelectBlockOnTech)
			{
				if (m_GroundPickupEnabled && m_NearbyPickupBlock != null)
				{
					if (m_NearbyPickupBlock.tank != null)
					{
						DetachBlockForPlacement(m_NearbyPickupBlock);
					}
					else if (m_NearbyPickupBlock.visible.holderStack != null)
					{
						m_NearbyPickupBlock.visible.SetHolder(null);
					}
					PickupLooseBlockForPlacement(m_NearbyPickupBlock);
					SpawnNewPaintingBlock(m_CurrentMatchedGroup, m_CurrentPlacement);
				}
			}
			else if (m_InteractionMode == InteractionMode.PlaceBlock)
			{
				bool flag2 = true;
				IInventory<BlockTypes> inventory = null;
				if (!m_HeldBlockWasAttachedToTech && !m_HoldingLooseBlock)
				{
					inventory = Singleton.Manager<ManPurchases>.inst.GetInventory();
					if (inventory != null)
					{
						int quantity = inventory.GetQuantity(m_SelectedBlock.BlockType);
						flag2 = quantity == -1 || quantity > 0;
					}
				}
				if (!m_PreviewMove && m_SelectedBlock != null && flag2 && (m_HeldBlockWasAttachedToTech || (m_CurrentPlacement != null && Singleton.Manager<ManTechBuilder>.inst.IsPlacementAllowed(m_SelectedBlock, m_CurrentPlacement))))
				{
					Vector3 vector = ((m_CurrentPlacement != null) ? m_CurrentPlacement.localPos : m_HeldDetachedBlockPreDetachLocalPos);
					Quaternion quaternion = ((m_CurrentPlacement != null) ? m_CurrentPlacement.orthoRot : m_HeldDetachedBlockPreDetachLocalRot);
					m_SelectedBlock.trans.rotation = playerTank.trans.rotation * quaternion;
					m_SelectedBlock.trans.position = playerTank.trans.TransformPoint(vector);
					Singleton.Manager<ManLooseBlocks>.inst.RequestAttachBlock(playerTank, m_SelectedBlock, vector, new OrthoRotation(quaternion));
					m_SelectedBlock.RevertCustomMaterialOverride();
					m_SelectedBlock.visible.EnablePhysics(enable: true);
					bool flag3 = false;
					if (inventory != null)
					{
						flag3 = inventory.GetQuantity(m_SelectedBlock.BlockType) == 0;
					}
					if (m_HoldingLooseBlock || flag3)
					{
						m_HoldingLooseBlock = false;
						SetInteractionMode(InteractionMode.SelectBlockOnTech);
					}
					else
					{
						SpawnNewPaintingBlock(m_CurrentMatchedGroup, m_CurrentPlacement);
					}
				}
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(11))
		{
			if (m_InteractionMode == InteractionMode.SelectBlockOnTech)
			{
				if (m_SelectedBlock != null && m_SelectedBlock.tank.blockman.blockCount > 1 && (!m_SelectedBlock.tank.blockman.IsRootBlock(m_SelectedBlock) || m_SelectedBlock.tank.control.NumControllers + m_SelectedBlock.tank.Anchors.NumAnchored > ((m_SelectedBlock.IsController != (m_SelectedBlock.Anchor != null)) ? 1 : 2)))
				{
					m_HeldDetachedBlockPreDetachLocalPos = m_SelectedBlock.trans.localPosition;
					m_HeldDetachedBlockPreDetachLocalRot = new OrthoRotation(m_SelectedBlock.trans.localRotation);
					DetachBlockForPlacement(m_SelectedBlock);
					PickupLooseBlockForPlacement(m_SelectedBlock);
					m_HeldBlockWasAttachedToTech = true;
				}
			}
			else if (m_InteractionMode != InteractionMode.None)
			{
				if (m_InteractionMode == InteractionMode.PlaceBlock && m_SelectedBlock != null && m_HoldingLooseBlock)
				{
					DropHeldBlock();
				}
				SetInteractionMode(InteractionMode.SelectBlockOnTech);
			}
		}
		if (m_InteractionMode == InteractionMode.SelectInventoryBlock)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(21))
			{
				SetInteractionMode(InteractionMode.PlaceBlock);
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(13))
			{
				SetInteractionMode(InteractionMode.SelectBlockOnTech);
			}
		}
		if (!m_HoldingLooseBlock && Singleton.Manager<ManInput>.inst.GetButtonDown(13) && Singleton.Manager<ManPurchases>.inst.IsPaletteAvailable())
		{
			SetInteractionMode(InteractionMode.SelectInventoryBlock);
		}
		m_PreviewMove = false;
		if (m_InteractionMode == InteractionMode.SelectBlockOnTech || m_InteractionMode == InteractionMode.PlaceBlock)
		{
			flag = Singleton.Manager<ManInput>.inst.GetButton(49);
			m_PreviewMove = m_MoveSelectionOnConfirm || flag;
		}
		m_MoveToPreviewControl = m_PreviewMove && Singleton.Manager<ManInput>.inst.GetButtonDown(10);
		if (m_InteractionMode != InteractionMode.PlaceBlock)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(50) && m_BlockToPlaceSelection.Count > 0)
		{
			BlockTypes blockToPlace = m_BlockToPlace;
			int num2 = m_BlockToPlaceSelection.IndexOf(m_BlockToPlace);
			num2 = (num2 + 1 + m_BlockToPlaceSelection.Count) % m_BlockToPlaceSelection.Count;
			m_BlockToPlace = m_BlockToPlaceSelection[num2];
			if (m_BlockToPlace != blockToPlace)
			{
				Singleton.Manager<ManPointer>.inst.RemovePaintingBlock();
				SpawnNewPaintingBlock(m_CurrentMatchedGroup, m_CurrentPlacement);
				Singleton.Manager<ManTechBuilder>.inst.ResetAPCollection();
			}
		}
		if (m_CurrentPlacement == null || Mode<ModeMain>.inst.TutorialDisableBlockRotation)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(12))
		{
			BlockPlacementCollector.Placement nextRotation = GetNextRotation();
			if (nextRotation != null)
			{
				m_NextPlacement = nextRotation;
				m_MoveToPreviewControl = true;
			}
		}
		else if (Singleton.Manager<ManInput>.inst.GetNegativeButtonDown(12))
		{
			BlockPlacementCollector.Placement previousRotation = GetPreviousRotation();
			if (previousRotation != null)
			{
				m_NextPlacement = previousRotation;
				m_MoveToPreviewControl = true;
			}
		}
	}

	private void UpdateBlockSelection()
	{
		TankBlock tankBlock = null;
		Tank playerTank = Singleton.playerTank;
		Vector2 vector = (m_PreviewMove ? m_BuildVectorRaw : m_BuildVector);
		if (m_SelectedBlock == null || m_SelectedBlock.tank != playerTank)
		{
			TankBlock tankBlock2 = m_SelectedBlock ?? m_LastValidSelectedBlock;
			Vector3 vector2 = ((tankBlock2 == null) ? Singleton.cameraTrans.position : tankBlock2.trans.position);
			if (m_CurrentPlacement != null)
			{
				vector2 -= playerTank.trans.TransformDirection(m_CurrentMatchedGroup.apNormal * 0.5f);
			}
			float num = float.MaxValue;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = playerTank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				float sqrMagnitude = (current.centreOfMassWorld - vector2).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					tankBlock = current;
				}
			}
		}
		else if (vector != Vector2.zero)
		{
			m_NextGridSelection = StepToNextCell(m_SelectedBlock, vector, m_CurrentGridSelection);
		}
		if (m_NextGridSelection != m_CurrentGridSelection && (m_MoveToPreviewControl || (!m_PreviewMove && vector != Vector2.zero)))
		{
			tankBlock = playerTank.blockman.GetBlockAtPosition(m_NextGridSelection);
		}
		if (tankBlock != null && tankBlock != m_SelectedBlock && Singleton.Manager<ManPointer>.inst.ItemIsGrabbable(tankBlock.visible))
		{
			SetSelectedBlock(tankBlock);
			if (!m_PreserveCellSelection)
			{
				float num2 = float.MaxValue;
				Vector3 localPosition = tankBlock.trans.localPosition;
				IntVector3[] filledCells = tankBlock.filledCells;
				foreach (IntVector3 intVector in filledCells)
				{
					IntVector3 intVector2 = localPosition + tankBlock.trans.localRotation * intVector;
					float num3 = (intVector2 - m_CurrentGridSelection).sqrMagnitude;
					if (num3 < num2)
					{
						num2 = num3;
						m_CurrentGridSelection = intVector2;
					}
				}
			}
		}
		else if (m_SelectedBlock != null && m_SelectedBlock.tank != playerTank)
		{
			SetSelectedBlock(null);
		}
		if (m_SelectedBlock != null)
		{
			Matrix4x4.TRS(GetRenderBounds(m_SelectedBlock).center, m_SelectedBlock.trans.rotation, Vector3.one);
		}
	}

	private IntVector3 StepToNextCell(TankBlock currentBlock, Vector2 inputDirection, IntVector3 preferredSelectionCell)
	{
		IntVector3 result = preferredSelectionCell;
		switch (m_ControlMode)
		{
		case ControlMode.Step2DTo3DClosestAxis:
			result = StepToNextCell_AnalogInputToClosestAxis3D(currentBlock, inputDirection, preferredSelectionCell);
			break;
		case ControlMode.ScreenSpace_FirstHit:
			result = StepToNextCell_ScreenSpaceStep(currentBlock, inputDirection, preferredSelectionCell);
			break;
		case ControlMode.ScreenSpace_Smart:
			result = StepToNextCell_ScreenSpaceStepSmart(currentBlock, inputDirection, preferredSelectionCell);
			break;
		}
		return result;
	}

	private IntVector3 StepToNextCell_AnalogInputToClosestAxis3D(TankBlock currentBlock, Vector2 inputDirection, IntVector3 preferredSelectionCell)
	{
		Tank tank = currentBlock.tank;
		inputDirection.Normalize();
		Vector3 vector = tank.trans.position + tank.trans.rotation * preferredSelectionCell;
		Vector3 position = vector + tank.trans.rotation * Vector3.right;
		Vector3 position2 = vector + tank.trans.rotation * Vector3.up;
		Vector3 position3 = vector + tank.trans.rotation * Vector3.forward;
		Vector3 vector2 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector);
		Vector3 vector3 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
		Vector3 vector4 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position2);
		Vector3 vector5 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position3);
		Vector3 axis1Screen = vector3 - vector2;
		Vector3 axis2Screen = vector4 - vector2;
		Vector3 vector6 = vector5 - vector2;
		float num = Vector2.Dot(axis1Screen.normalized, inputDirection);
		float num2 = Vector2.Dot(axis2Screen.normalized, inputDirection);
		float num3 = Vector2.Dot(vector6.normalized, inputDirection);
		float axis1MatchValue = Mathf.Abs(num);
		float axis2MatchValue = Mathf.Abs(num2);
		float axis2MatchValue2 = Mathf.Abs(num3);
		CullDataIfAxisOverlappingInScreenSpace(axis1Screen, axis2Screen, ref axis1MatchValue, ref axis2MatchValue);
		CullDataIfAxisOverlappingInScreenSpace(axis1Screen, vector6, ref axis1MatchValue, ref axis2MatchValue2);
		CullDataIfAxisOverlappingInScreenSpace(vector6, axis2Screen, ref axis2MatchValue2, ref axis2MatchValue);
		IntVector3 intVector = ((axis1MatchValue > axis2MatchValue) ? ((!(axis1MatchValue > axis2MatchValue2)) ? ((IntVector3)((num3 < 0f) ? Vector3.back : Vector3.forward)) : ((IntVector3)((num < 0f) ? Vector3.left : Vector3.right))) : ((!(axis2MatchValue > axis2MatchValue2)) ? ((IntVector3)((num3 < 0f) ? Vector3.back : Vector3.forward)) : ((IntVector3)((num2 < 0f) ? Vector3.down : Vector3.up))));
		return preferredSelectionCell + intVector;
	}

	private void CullDataIfAxisOverlappingInScreenSpace(Vector3 axis1Screen, Vector3 axis2Screen, ref float axis1MatchValue, ref float axis2MatchValue)
	{
		Vector2 vector = new Vector2(axis1Screen.x, axis1Screen.y);
		Vector2 vector2 = new Vector2(axis2Screen.x, axis2Screen.y);
		if (Vector2.Dot(vector.normalized, vector2.normalized) > m_AxisOverlapDiscardThreshold)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			float sqrMagnitude2 = vector2.sqrMagnitude;
			if (sqrMagnitude > sqrMagnitude2)
			{
				axis2MatchValue = 0f;
			}
			else
			{
				axis1MatchValue = 0f;
			}
		}
	}

	private IntVector3 StepToNextCell_ScreenSpaceStep(TankBlock currentBlock, Vector2 inputDirection, IntVector3 preferredSelectionCell)
	{
		Tank tank = currentBlock.tank;
		Vector3 position = currentBlock.trans.TransformPoint(currentBlock.BlockCellBounds.center);
		Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
		Vector3 vector2 = vector;
		Vector3 normalized = new Vector3(inputDirection.x, inputDirection.y, 0f).normalized;
		m_CurrentRayStepValues = GetRayStepValues(inputDirection.magnitude);
		TankBlock tankBlock = null;
		Rect rect = new Rect(Mathf.Min(0f, vector.x), Mathf.Min(0f, vector.y), Mathf.Max(Screen.currentResolution.width, vector.x), Mathf.Max(Screen.currentResolution.height, vector.y));
		float num = (Singleton.cameraTrans.position - tank.trans.position).magnitude + tank.blockBounds.extents.magnitude;
		int layerMask = Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask;
		do
		{
			Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(vector2);
			RaycastHit hitInfo;
			bool flag = (m_CurrentRayStepValues.m_UseSphereCast ? Physics.SphereCast(ray, m_CurrentRayStepValues.m_SphereCastRadiusWorld, out hitInfo, num, layerMask, QueryTriggerInteraction.Ignore) : Physics.Raycast(ray, out hitInfo, num, layerMask, QueryTriggerInteraction.Ignore));
			if (flag && !hitInfo.transform.gameObject.IsTerrain())
			{
				Visible visible = Visible.FindVisibleUpwards(hitInfo.collider);
				TankBlock tankBlock2 = ((visible != null) ? visible.block : null);
				if (tankBlock2 != null && tankBlock2 != currentBlock && ValidateBlockHit(currentBlock, tankBlock2, inputDirection, vector2))
				{
					tankBlock = tankBlock2;
					break;
				}
			}
			DrawDebugRays(ray, flag, hitInfo, num);
			vector2 += normalized * m_CurrentRayStepValues.m_ScreenSpaceStepSizePx;
		}
		while (rect.Contains(vector2));
		IntVector3 result = preferredSelectionCell;
		if (tankBlock != null)
		{
			result = tankBlock.trans.localPosition;
		}
		return result;
	}

	private IntVector3 StepToNextCell_ScreenSpaceStepSmart(TankBlock currentBlock, Vector2 inputDirection, IntVector3 preferredSelectionCell)
	{
		Tank tank = currentBlock.tank;
		Vector3 position = currentBlock.trans.TransformPoint(currentBlock.BlockCellBounds.center);
		Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
		Vector3 vector2 = vector;
		float num = 0f;
		Vector3 normalized = new Vector3(inputDirection.x, inputDirection.y, 0f).normalized;
		m_CurrentRayStepValues = GetRayStepValues(inputDirection.magnitude);
		TankBlock tankBlock = null;
		Rect rect = new Rect(Mathf.Min(0f, vector.x), Mathf.Min(0f, vector.y), Mathf.Max(Screen.currentResolution.width, vector.x), Mathf.Max(Screen.currentResolution.height, vector.y));
		float screenSpaceStepDistancePx = GetScreenSpaceStepDistancePx();
		float magnitude = tank.blockBounds.extents.magnitude;
		float num2 = (Singleton.cameraTrans.position - tank.trans.position).magnitude + magnitude;
		int num3 = 0;
		TankBlock tankBlock2 = null;
		int num4 = 0;
		TankBlock tankBlock3 = null;
		int layerMask = Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask;
		do
		{
			Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(vector2);
			RaycastHit hitInfo;
			bool flag = (m_CurrentRayStepValues.m_UseSphereCast ? Physics.SphereCast(ray, m_CurrentRayStepValues.m_SphereCastRadiusWorld, out hitInfo, num2, layerMask, QueryTriggerInteraction.Ignore) : Physics.Raycast(ray, out hitInfo, num2, layerMask, QueryTriggerInteraction.Ignore));
			if (flag && !hitInfo.transform.gameObject.IsTerrain())
			{
				Visible visible = Visible.FindVisibleUpwards(hitInfo.collider);
				TankBlock tankBlock4 = ((visible != null) ? visible.block : null);
				if (tankBlock4 != null && tankBlock4 != currentBlock && tankBlock4.tank != null && tankBlock4.tank == tank && ValidateBlockHit(currentBlock, tankBlock4, inputDirection, vector2))
				{
					if (tankBlock4 == tankBlock2)
					{
						num3++;
					}
					else
					{
						tankBlock2 = tankBlock4;
						num3 = 0;
					}
					if (num3 >= m_MinHitsForAutoAccept)
					{
						tankBlock = tankBlock2;
						break;
					}
					if (tankBlock3 == null || num4 > num3)
					{
						tankBlock3 = tankBlock4;
						num4 = num3;
					}
				}
			}
			DrawDebugRays(ray, flag, hitInfo, num2);
			num += screenSpaceStepDistancePx;
			vector2 = vector + normalized * num;
		}
		while (rect.Contains(vector2));
		if (tankBlock == null)
		{
			tankBlock = tankBlock3;
		}
		IntVector3 result = preferredSelectionCell;
		if (tankBlock != null)
		{
			result = tankBlock.trans.localPosition;
		}
		return result;
	}

	private void SpawnNewPaintingBlock(CachedPlacementGroupData placementGroupData, BlockPlacementCollector.Placement prevPlacement = null)
	{
		Tank playerTank = Singleton.playerTank;
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == null)
		{
			Singleton.Manager<ManPointer>.inst.TrySpawnPaintingBlock(m_BlockToPlace, force: true);
		}
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == null)
		{
			d.LogError("Failed to spawn/grab painting block ??");
			return;
		}
		Singleton.Manager<ManPointer>.inst.DraggingItem.trans.position = (playerTank.boundsCentreWorld + Singleton.cameraTrans.position) / 2f;
		Singleton.Manager<ManPointer>.inst.DraggingFocusTech = playerTank;
		Singleton.Manager<ManTechBuilder>.inst.StartStopAttachPointCollection();
		Vector2 autoSelectInputDir = Vector2.right;
		if (prevPlacement != null || m_SelectedBlock != null)
		{
			Vector3 vector = ((prevPlacement != null) ? playerTank.trans.TransformDirection(placementGroupData.apNormal) : (Singleton.cameraTrans.position - m_SelectedBlock.visible.centrePosition));
			Vector3 vector2;
			if (prevPlacement != null)
			{
				Vector3 position = prevPlacement.localPos + prevPlacement.orthoRot * Singleton.Manager<ManPointer>.inst.DraggingItem.block.BlockCellBounds.center;
				vector2 = playerTank.trans.TransformPoint(position);
			}
			else
			{
				vector2 = m_SelectedBlock.visible.centrePosition;
			}
			Vector3 vector3 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector2);
			autoSelectInputDir = ((Vector2)(Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector2 + vector) - vector3)).normalized;
		}
		m_CurrentPlacement = null;
		m_CurrentMatchedGroup = default(CachedPlacementGroupData);
		m_AutoSelectNextPlacement = true;
		m_AutoSelectInputDir = autoSelectInputDir;
	}

	private void SetSelectedBlock(TankBlock selectedBlock)
	{
		if (m_SelectedBlock != null && selectedBlock == null)
		{
			m_LastValidSelectedBlock = m_SelectedBlock;
		}
		m_SelectedBlock = selectedBlock;
	}

	private void DetachBlockForPlacement(TankBlock block)
	{
		Singleton.Manager<ManUndo>.inst.OnBeforeDetachBlock(block);
		block.visible.EnablePhysics(enable: false, disableWithTrigger: true);
		Singleton.Manager<ManLooseBlocks>.inst.RequestDetachBlock(block, allowHeadlessTech: true);
	}

	private void PickupLooseBlockForPlacement(TankBlock block)
	{
		Singleton.Manager<ManPointer>.inst.SetGrabbedTarget(block.visible);
		m_HoldingLooseBlock = true;
		SetInteractionMode(InteractionMode.PlaceBlock);
		Singleton.Manager<ManTechBuilder>.inst.ResetAPCollection();
	}

	private void DropHeldBlock()
	{
		if (!m_HoldingLooseBlock || !(Singleton.Manager<ManPointer>.inst.DraggingItem != null) || !(m_SelectedBlock != null))
		{
			return;
		}
		m_HoldingLooseBlock = false;
		TankBlock removedBlock = m_SelectedBlock;
		Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + 0.2f, delegate
		{
			if (removedBlock != null && removedBlock.gameObject != null && removedBlock.gameObject.activeSelf && removedBlock.visible != null)
			{
				removedBlock.visible.EnablePhysics(enable: true);
			}
		});
		if (m_SelectedBlock.rbody != null)
		{
			m_SelectedBlock.rbody.AddForce((Vector3.up * 5f + UnityEngine.Random.insideUnitCircle.ToVector3XZ()) * 3f, ForceMode.VelocityChange);
		}
		SetSelectedBlock(null);
	}

	private void UpdateBlockPlacement()
	{
		TankBlock selectedBlock = ((Singleton.Manager<ManPointer>.inst.DraggingItem != null) ? Singleton.Manager<ManPointer>.inst.DraggingItem.block : null);
		SetSelectedBlock(selectedBlock);
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == null)
		{
			return;
		}
		Tank playerTank = Singleton.playerTank;
		Singleton.Manager<ManPointer>.inst.DraggingFocusTech = playerTank;
		if (m_AutoSelectNextPlacement)
		{
			BlockPlacementCollector.Placement placement = StepToNextPlacement(m_CurrentPlacement, playerTank, m_AutoSelectInputDir, allowUnpopularSelections: true, out m_NextMatchedGroup);
			if (placement != null)
			{
				m_AutoSelectNextPlacement = false;
				m_NextPlacement = placement;
				m_MoveToPreviewControl = true;
			}
		}
		Vector2 vector = (m_PreviewMove ? m_BuildVectorRaw : m_BuildVector);
		if (vector != Vector2.zero)
		{
			m_AutoSelectNextPlacement = false;
			BlockPlacementCollector.Placement placement2 = null;
			if (m_StepPlacementsOnSameFocalCell || m_StepRotationsOnSameFocalCell)
			{
				placement2 = GetNextPlacementInInputDir(vector);
			}
			m_NextPlacement = placement2 ?? StepToNextPlacement(m_CurrentPlacement, playerTank, vector, allowUnpopularSelections: false, out m_NextMatchedGroup);
		}
		if (m_NextPlacement == m_CurrentPlacement || m_NextPlacement == null || (!m_MoveToPreviewControl && (m_PreviewMove || !(vector != Vector2.zero))))
		{
			return;
		}
		m_CurrentPlacement = m_NextPlacement;
		m_CurrentMatchedGroup = m_NextMatchedGroup;
		m_NextPlacement = null;
		m_CurrentPlacementFilledCellList.Clear();
		if (m_CurrentPlacement != null)
		{
			m_PreferredOrientation = m_CurrentPlacement.orthoRot;
			IntVector3[] filledCells = m_SelectedBlock.filledCells;
			foreach (IntVector3 intVector in filledCells)
			{
				IntVector3 item = m_CurrentPlacement.localPos + m_CurrentPlacement.orthoRot * intVector;
				m_CurrentPlacementFilledCellList.Add(item);
			}
			m_SelectedBlock.trans.SetPositionIfChanged(playerTank.trans.TransformPoint(m_CurrentPlacement.localPos));
			m_SelectedBlock.trans.SetRotationIfChanged(playerTank.trans.rotation * m_CurrentPlacement.orthoRot);
			Singleton.Manager<ManTechBuilder>.inst.BlockPlacementCollector.UpdateBlockPosTechLocal(playerTank.trans.InverseTransformPoint(m_SelectedBlock.centreOfMassWorld));
		}
	}

	private BlockPlacementCollector.Placement StepToNextPlacement(BlockPlacementCollector.Placement currentPlacement, Tank focusTech, Vector2 inputDirection, bool allowUnpopularSelections, out CachedPlacementGroupData matchedGroup)
	{
		BlockPlacementCollector.Placement result = null;
		matchedGroup = default(CachedPlacementGroupData);
		Vector3 vector;
		Vector3 vector2;
		if (currentPlacement != null)
		{
			vector = currentPlacement.localPos + currentPlacement.orthoRot * Singleton.Manager<ManPointer>.inst.DraggingItem.block.BlockCellBounds.center;
			vector2 = focusTech.trans.TransformPoint(vector);
		}
		else
		{
			vector2 = Singleton.Manager<ManPointer>.inst.DraggingItem.trans.TransformPoint(Singleton.Manager<ManPointer>.inst.DraggingItem.block.BlockCellBounds.center);
			vector = focusTech.trans.InverseTransformPoint(vector2);
		}
		Vector3 vector3 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector2);
		Vector2 coord = inputDirection.normalized;
		Vector3 vector4 = new Vector3(coord.x, coord.y, 0f);
		if (m_UseInputStartOffset)
		{
			Vector3 position = vector2 + Singleton.cameraTrans.right;
			Vector3 vector5 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
			float magnitude = (vector3 - vector5).SetZ(0f).magnitude;
			vector3 += m_InputStartOffset * vector4 * magnitude;
		}
		Vector3 normalized = Singleton.cameraTrans.TransformDirection(coord.ToVector3XZ()).normalized;
		Vector3 position2 = vector3 + vector4 * 5f;
		Vector3 normalized2 = (Singleton.camera.ScreenToWorldPoint(position2) - vector2).normalized;
		float num = Mathf.Cos((float)Math.PI / 180f * m_InputAngleCutoff);
		Vector3 vector6 = vector3.SetZ(0f) + new Vector3(0f, 0f, Singleton.camera.nearClipPlane + 10f);
		Vector3 position3 = vector6 + vector4 * Screen.width;
		Singleton.camera.ScreenToWorldPoint(vector6);
		Singleton.camera.ScreenToWorldPoint(position3);
		Quaternion quaternion = Quaternion.AngleAxis(m_InputAngleCutoff, Vector3.forward);
		Singleton.cameraTrans.TransformDirection(quaternion * vector4);
		Singleton.cameraTrans.TransformDirection(Quaternion.Inverse(quaternion) * vector4);
		float num2 = float.MaxValue;
		Dictionary<IntVector3, CachedPlacementGroupData> dictionary = new Dictionary<IntVector3, CachedPlacementGroupData>();
		foreach (KeyValuePair<IntVector3, BlockPlacementCollector.Collection> item in Singleton.Manager<ManTechBuilder>.inst.BlockPlacementCollector)
		{
			if (currentPlacement != null && item.Value == m_CurrentMatchedGroup.placementGroup)
			{
				continue;
			}
			if (currentPlacement != null)
			{
				BlockPlacementCollector.Collection.Index index = item.Value.FirstPreferred(currentPlacement.orthoRot);
				BlockPlacementCollector.Placement placement = null;
				if (index.IsValid)
				{
					placement = index.Value;
				}
				if (placement == currentPlacement)
				{
					continue;
				}
			}
			IntVector3 key = item.Key;
			Vector3 vector7 = key.APtoLocal();
			Vector3 vector8 = key.APtoWorld(focusTech.trans);
			Vector3 vector9 = new Vector3(key.x % 2, key.y % 2, key.z % 2);
			d.Assert(vector9.sqrMagnitude == 1f, "Invalid Ap normal length!");
			if (focusTech.blockman.GetBlockAtPosition(vector7 + vector9 * 0.5f) != null)
			{
				vector9 = -vector9;
			}
			Vector3 vector10 = vector7 + vector9 * 0.5f;
			if (currentPlacement == null && m_HeldBlockWasAttachedToTech && vector10 == m_HeldDetachedBlockPreDetachLocalPos)
			{
				continue;
			}
			Vector3 obj = (m_UseApCellCentre ? vector10 : vector7);
			Vector3 vector11 = (m_UseApCellCentre ? (vector8 + focusTech.trans.rotation * vector9 * 0.5f) : vector8);
			Vector2 lhs = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector11) - vector3;
			Vector3 vector12 = obj - vector;
			float magnitude2 = vector12.magnitude;
			if (magnitude2 == 0f && !allowUnpopularSelections)
			{
				continue;
			}
			if (magnitude2 < num2)
			{
				num2 = magnitude2;
			}
			float num3 = Vector2.Dot(lhs, coord);
			if (num3 < 0f && !allowUnpopularSelections)
			{
				if (!m_DrawDiscardedCellsBehindStartPoint)
				{
				}
				continue;
			}
			float magnitude3 = lhs.magnitude;
			float num4 = num3 / magnitude3;
			if (num4 < num && !allowUnpopularSelections)
			{
				continue;
			}
			Vector3 pos = vector3 + vector4 * num3;
			Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(pos);
			float num5 = 0f;
			float num6 = 0f;
			float num7 = focusTech.visible.Radius * 2f;
			if (m_UseWeightedScoring)
			{
				bool flag = Vector3.Dot(vector11 - Singleton.cameraTrans.position, Singleton.cameraTrans.forward) > 0f;
				float num8 = Mathf.Max(Vector3.Dot(normalized2, (focusTech.trans.rotation * vector12).normalized), 0f);
				float num9 = Mathf.Max(num4, 0f);
				float distanceToPoint = new Plane(Vector3.Cross(Singleton.cameraTrans.TransformDirection(vector4).normalized, Singleton.cameraTrans.forward).normalized, vector2).GetDistanceToPoint(vector11);
				float num10 = 1f - Mathf.Abs(distanceToPoint) / 5f;
				float distanceToPoint2 = new Plane(Vector3.Cross(normalized, focusTech.trans.up).normalized, vector2).GetDistanceToPoint(vector11);
				float num11 = 1f - Mathf.Abs(distanceToPoint2) / 5f;
				float distanceToPoint3 = new Plane(Vector3.Cross(normalized, Singleton.cameraTrans.up).normalized, vector2).GetDistanceToPoint(vector11);
				float num12 = 1f - Mathf.Abs(distanceToPoint3) / 5f;
				float num13 = 1f - Mathf.Min(magnitude2 / num7, 1f);
				float num14 = (float)Screen.currentResolution.width * 0.5f;
				float num15 = 1f - Mathf.Min(lhs.magnitude / num14, 1f);
				float num16 = Vector3.Dot(m_CurrentMatchedGroup.apNormal, vector9) * 0.5f + 0.5f;
				float num17 = Vector3.Dot(focusTech.trans.rotation * vector12, Singleton.cameraTrans.forward) / 5f;
				float num18 = 1f - (num17 * 0.5f + 0.5f);
				num6 = GetPenaltyValue(m_DiscardAPsBehindCamera && !flag, 1f, 1f) + GetPenaltyValue(m_UseWorldAngleOffsetInfluence, m_WorldAngleOffsetInfluence, 1f - num8) + GetPenaltyValue(m_UseScreenAngleOffsetInfluence, m_ScreenAngleOffsetInfluence, 1f - num9) + GetPenaltyValue(m_UseDistanceToInputPlaneInfluence, m_DistanceToInputPlaneInfluence, 1f - num10) + GetPenaltyValue(m_UseDistanceToVerticalInputPlaneInfluence, m_DistanceToVerticalInputPlaneInfluence, 1f - num11) + GetPenaltyValue(m_UseDistanceToInputPlaneCamAlignedInfluence, m_DistanceToInputPlaneCamAlignedInfluence, 1f - num12) + GetPenaltyValue(m_UseDistanceToApInfluence, m_DistanceToApInfluence, 1f - num13) + GetPenaltyValue(m_UseDistanceToApInfluenceScreen, m_DistanceToApInfluenceScreen, 1f - num15) + GetPenaltyValue(m_UseMatchCurrentAPNormalInfluence, m_MatchCurrentAPNormalInfluence, 1f - num16) + GetPenaltyValue(m_UseDepthOnTechInfluence, m_DepthOnTechInfluence, 1f - num18);
				num5 = 1f - 1f * num6;
			}
			else
			{
				float num19 = -1000f;
				float magnitude4 = Vector3.Cross(vector11 - ray.origin, ray.direction).magnitude;
				if (magnitude4 <= 0.7f && Vector3.Dot(vector11 - Singleton.cameraTrans.position, Singleton.cameraTrans.forward) > 0f)
				{
					num19 = (0f - Vector3.Dot(vector11 - focusTech.trans.position, Singleton.cameraTrans.forward)) * 8f - magnitude4 * magnitude4 * 50f;
				}
				num5 = num19;
			}
			if (!dictionary.TryGetValue(key, out var value) || num5 > value.bestScore)
			{
				dictionary[key] = new CachedPlacementGroupData
				{
					bestScore = num5,
					scorePenalty = num6,
					apPos = key,
					apNormal = vector9,
					localCellPos = new IntVector3(vector10),
					refPosWorld = vector11,
					dist = magnitude2,
					placementGroup = item.Value
				};
			}
			Vector3 position4 = new Vector3(pos.x, pos.y, Singleton.camera.nearClipPlane + 10f);
			Singleton.camera.ScreenToWorldPoint(position4);
			_ = m_DrawClosestApproach;
			_ = m_DrawAPNormals;
		}
		m_LastPlacementData = null;
		List<CachedPlacementGroupData> value2 = new List<CachedPlacementGroupData>(dictionary.Values);
		for (int num20 = value2.Count - 1; num20 >= 0; num20--)
		{
			float num21 = Mathf.Asin(new Plane(Vector3.Cross(Singleton.cameraTrans.TransformDirection(vector4).normalized, Singleton.cameraTrans.forward).normalized, vector2).GetDistanceToPoint(value2[num20].refPosWorld) / value2[num20].dist) * 57.29578f;
			if (value2[num20].dist > num2 + 1f || num21 > 45f)
			{
				if (m_DiscardBelowScoreThreshold && value2[num20].bestScore <= m_MinScoreThreshold && !allowUnpopularSelections)
				{
					value2.RemoveAt(num20);
				}
				else if (m_DiscardAboveScorePenaltyThreshold && value2[num20].scorePenalty >= m_MaxScorePenaltyThreshold)
				{
					value2.RemoveAt(num20);
				}
			}
		}
		if (m_DiscardInvisiblePositions && !allowUnpopularSelections)
		{
			for (int num22 = value2.Count - 1; num22 >= 0; num22--)
			{
				Bounds blockCellBounds = m_SelectedBlock.BlockCellBounds;
				blockCellBounds.extents += Vector3.one * 0.45f;
				float num23 = 0f;
				if (m_UseVertexHalfPoints)
				{
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, 0f, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, 0f, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, blockCellBounds.extents.y, 0f), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, 0f), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, 0f, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, 0f, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, blockCellBounds.extents.y, 0f), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, 0f), focusTech) ? 1f : 0f);
				}
				else
				{
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, blockCellBounds.extents.y, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, blockCellBounds.extents.y, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(0f - blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, blockCellBounds.extents.y, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, blockCellBounds.extents.y, 0f - blockCellBounds.extents.z), focusTech) ? 1f : 0f);
					num23 += (IsPointVisible(value2[num22].refPosWorld + focusTech.trans.rotation * new Vector3(blockCellBounds.extents.x, 0f - blockCellBounds.extents.y, blockCellBounds.extents.z), focusTech) ? 1f : 0f);
				}
				num23 += (IsPointVisible(value2[num22].refPosWorld, focusTech) ? 1.5f : 0f);
				if (num23 < 2.2f)
				{
					value2.RemoveAt(num22);
				}
			}
		}
		if (value2.Count > 0)
		{
			if (m_CullByDistanceBucket)
			{
				Dictionary<int, List<CachedPlacementGroupData>> dictionary2 = new Dictionary<int, List<CachedPlacementGroupData>>();
				foreach (CachedPlacementGroupData item2 in value2)
				{
					int key2;
					switch (m_BucketDistanceMetric)
					{
					case BucketDistanceMetric.FloorToInt:
						key2 = Mathf.FloorToInt(item2.dist);
						break;
					case BucketDistanceMetric.FloorToHalfUnit:
						key2 = Mathf.FloorToInt(item2.dist * 2f);
						break;
					case BucketDistanceMetric.SquaredFloorToInt:
						key2 = Mathf.FloorToInt(item2.dist * item2.dist);
						break;
					default:
						d.LogError("BucketDistanceMetric not set!");
						key2 = 0;
						break;
					}
					if (!dictionary2.TryGetValue(key2, out var value3))
					{
						value3 = new List<CachedPlacementGroupData>();
						dictionary2.Add(key2, value3);
					}
					value3.Add(item2);
				}
				for (int i = 0; !dictionary2.TryGetValue(i, out value2); i++)
				{
				}
			}
			if (m_SortByDistanceFromSource)
			{
				value2.Sort(delegate(CachedPlacementGroupData x, CachedPlacementGroupData y)
				{
					if (m_ApplyCameraZBias && Mathf.Abs(x.dist - y.dist) < 0.1f)
					{
						float num26 = Vector3.Dot(x.refPosWorld - Singleton.cameraTrans.position, Singleton.cameraTrans.forward);
						float value4 = Vector3.Dot(y.refPosWorld - Singleton.cameraTrans.position, Singleton.cameraTrans.forward);
						return num26.CompareTo(value4);
					}
					return x.dist.CompareTo(y.dist);
				});
			}
			if (m_CullOverlappingCells)
			{
				for (int num24 = 0; num24 < value2.Count; num24++)
				{
					Vector3 vector13 = value2[num24].apPos.APtoWorld(focusTech.trans);
					Vector3 vector14 = vector13 + focusTech.trans.rotation * value2[num24].apNormal * 0.5f;
					Vector3 vector15 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector14);
					Vector3 position5 = vector14 + Singleton.cameraTrans.right;
					Vector3 vector16 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position5);
					_ = (vector15 - vector16).SetZ(0f).magnitude;
					Plane plane = new Plane(Singleton.cameraTrans.forward, vector14);
					for (int num25 = value2.Count - 1; num25 > num24; num25--)
					{
						Vector3 vector17 = vector13 + focusTech.trans.rotation * value2[num25].apNormal * 0.5f;
						Ray ray2 = new Ray(vector17, Singleton.cameraTrans.position - vector17);
						if (plane.Raycast(ray2, out var enter))
						{
							Vector3 b = vector17 + ray2.direction * enter;
							if (Vector3.Distance(vector14, b) < m_OverlappingCellRadiusThreshold)
							{
								value2.RemoveAt(num25);
							}
						}
					}
				}
			}
			List<CachedPlacementGroupData> list = value2;
			list.Sort((CachedPlacementGroupData x, CachedPlacementGroupData y) => GetScoreValue(y).CompareTo(GetScoreValue(x)));
			matchedGroup = list[0];
			result = GetPreferredPlacement(matchedGroup.localCellPos, m_PreferredOrientation);
			m_CurrentGridSelection = matchedGroup.localCellPos;
			m_LastPlacementData = list;
		}
		return result;
	}

	private float GetScoreValue(CachedPlacementGroupData placementData)
	{
		float num = placementData.bestScore;
		if (m_DistanceInfluenceOnScore > 0f)
		{
			num += m_DistanceScoreInfluenceCurve.Evaluate(Mathf.Clamp01(placementData.dist / m_MaxDistanceInfluenceRange)) * m_DistanceInfluenceOnScore;
		}
		return num;
	}

	private float GetPenaltyValue(bool useValue, float valueWeighting, float value)
	{
		if (!useValue)
		{
			return 0f;
		}
		return Mathf.Clamp01(value) * valueWeighting;
	}

	private bool IsPointVisible(Vector3 targetPoint, Tank focusTech)
	{
		bool flag = true;
		Vector3 position = Singleton.cameraTrans.position;
		Vector3 direction = targetPoint - position;
		int mask = Globals.inst.layerTank.mask;
		float maxDistance = (Singleton.cameraTrans.position - focusTech.boundsCentreWorld).magnitude + focusTech.visible.Radius * 1.2f;
		if (Physics.Raycast(position, direction, out var hitInfo, maxDistance, mask, QueryTriggerInteraction.Ignore))
		{
			float magnitude = direction.magnitude;
			flag = hitInfo.distance > magnitude - 0.1f;
		}
		if (m_DrawVisibilityTestRays)
		{
			if (!flag)
			{
				new Color(0.8f, 0.4f, 0.3f);
			}
			else
			{
				new Color(0.3f, 1f, 0.6f);
			}
		}
		return flag;
	}

	private Bounds GetRenderBounds(TankBlock block)
	{
		MeshRenderer[] componentsInChildren = block.GetComponentsInChildren<MeshRenderer>();
		Bounds result = default(Bounds);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshRenderer meshRenderer = componentsInChildren[i];
			if (!meshRenderer.transform.parent.name.ToLower().Contains("highlight"))
			{
				if (i == 0)
				{
					result = meshRenderer.bounds;
				}
				else
				{
					result.Encapsulate(meshRenderer.bounds);
				}
			}
		}
		return result;
	}

	private BlockPlacementCollector.Placement GetNextRotation()
	{
		BlockPlacementCollector.Placement result = null;
		if (m_CurrentPlacement != null)
		{
			OrthoRotation orthoRot = m_CurrentPlacement.orthoRot;
			int num = m_AvailablePlacementsAtCurrentCell.IndexOf(m_CurrentPlacement);
			num++;
			for (int i = 0; i < m_AvailablePlacementsAtCurrentCell.Count; i++)
			{
				int index = (num + i) % m_AvailablePlacementsAtCurrentCell.Count;
				BlockPlacementCollector.Placement placement = m_AvailablePlacementsAtCurrentCell[index];
				if (placement.orthoRot != orthoRot)
				{
					result = placement;
					break;
				}
			}
		}
		return result;
	}

	private BlockPlacementCollector.Placement GetPreviousRotation()
	{
		BlockPlacementCollector.Placement result = null;
		if (m_CurrentPlacement != null)
		{
			OrthoRotation orthoRot = m_CurrentPlacement.orthoRot;
			int num = m_AvailablePlacementsAtCurrentCell.IndexOf(m_CurrentPlacement);
			num--;
			int count = m_AvailablePlacementsAtCurrentCell.Count;
			for (int num2 = count; num2 > 0; num2--)
			{
				int index = (num + num2) % count;
				BlockPlacementCollector.Placement placement = m_AvailablePlacementsAtCurrentCell[index];
				if (placement.orthoRot != orthoRot)
				{
					result = placement;
					break;
				}
			}
		}
		return result;
	}

	private BlockPlacementCollector.Placement GetNextPlacementInInputDir(Vector2 inputDirScreen)
	{
		BlockPlacementCollector.Placement placement = null;
		if (m_CurrentPlacement != null && m_AvailablePlacementsAtCurrentCell.Count > 0)
		{
			OrthoRotation orthoRot = m_CurrentPlacement.orthoRot;
			int num = m_AvailablePlacementsAtCurrentCell.IndexOf(m_CurrentPlacement);
			num++;
			Vector3 position = m_SelectedBlock.trans.position + m_SelectedBlock.trans.rotation * m_SelectedBlock.BlockCellBounds.center;
			Vector3 referencePoint = Singleton.playerTank.trans.InverseTransformPoint(position);
			Vector3 normalized = Singleton.cameraTrans.TransformDirection(inputDirScreen).normalized;
			Vector3 vector = Singleton.playerTank.trans.InverseTransformDirection(normalized);
			Vector3 normalized2 = (m_CalcPlacementChangeAngleInScreenSpace ? ((Vector3)inputDirScreen) : vector).normalized;
			if (m_StepPlacementsOnSameFocalCell)
			{
				float angleThreshold = Mathf.Cos((float)Math.PI / 180f * m_PlacementChangeInputAngleAllowance);
				for (int i = 0; i < m_AvailablePlacementsAtCurrentCell.Count; i++)
				{
					int index = (i + num) % m_AvailablePlacementsAtCurrentCell.Count;
					BlockPlacementCollector.Placement placement2 = m_AvailablePlacementsAtCurrentCell[index];
					if (placement2.orthoRot == orthoRot && placement2 != m_CurrentPlacement && IsPlacementInLocalDir(placement2, m_SelectedBlock.filledCells, referencePoint, normalized2, angleThreshold))
					{
						placement = placement2;
						break;
					}
				}
			}
			if (m_StepRotationsOnSameFocalCell && placement == null)
			{
				float angleThreshold2 = Mathf.Cos((float)Math.PI / 180f * m_OrientationChangeInputAngleAllowance);
				for (int j = 0; j < m_AvailablePlacementsAtCurrentCell.Count; j++)
				{
					BlockPlacementCollector.Placement placement3 = m_AvailablePlacementsAtCurrentCell[j];
					if (placement3.orthoRot != orthoRot && placement3 != m_CurrentPlacement && IsPlacementInLocalDir(placement3, m_SelectedBlock.filledCells, referencePoint, normalized2, angleThreshold2))
					{
						placement = placement3;
						break;
					}
				}
			}
		}
		return placement;
	}

	private bool IsPlacementInLocalDir(BlockPlacementCollector.Placement placement, IntVector3[] filledCells, Vector3 referencePoint, Vector3 inputDir, float angleThreshold)
	{
		Vector3 center = GetBlockBoundsTechLocal(placement, m_SelectedBlock.filledCells).center;
		Vector3 vector = center - referencePoint;
		Vector3 vector2 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(Singleton.playerTank.trans.TransformPoint(referencePoint));
		Vector3 vector3 = (Singleton.Manager<ManUI>.inst.WorldToScreenPoint(Singleton.playerTank.trans.TransformPoint(center)) - vector2).SetZ(0f);
		if (Vector3.Dot((m_CalcPlacementChangeAngleInScreenSpace ? vector3 : vector).normalized, inputDir) > angleThreshold)
		{
			return vector.sqrMagnitude > m_MinCentreChangeDistance * m_MinCentreChangeDistance;
		}
		return false;
	}

	private Bounds GetBlockBoundsTechLocal(BlockPlacementCollector.Placement placement, IntVector3[] filledCells)
	{
		Bounds result = new Bounds(placement.localPos, Vector3.zero);
		IntVector3[] filledCells2 = m_SelectedBlock.filledCells;
		foreach (IntVector3 intVector in filledCells2)
		{
			result.Encapsulate(placement.localPos + placement.orthoRot * intVector);
		}
		return result;
	}

	private BlockPlacementCollector.Placement GetPreferredPlacement(IntVector3 focalCell, OrthoRotation preferredOrientation)
	{
		BlockPlacementCollector.Placement placement = null;
		m_AvailablePlacementsAtCurrentCell.Clear();
		HashSet<IntVector3> hashSet = new HashSet<IntVector3>();
		hashSet.Add(focalCell * 2 + new IntVector3(1, 0, 0));
		hashSet.Add(focalCell * 2 + new IntVector3(-1, 0, 0));
		hashSet.Add(focalCell * 2 + new IntVector3(0, 1, 0));
		hashSet.Add(focalCell * 2 + new IntVector3(0, -1, 0));
		hashSet.Add(focalCell * 2 + new IntVector3(0, 0, 1));
		hashSet.Add(focalCell * 2 + new IntVector3(0, 0, -1));
		foreach (IntVector3 item2 in hashSet)
		{
			Vector3 position = item2.APtoLocal();
			Singleton.playerTank.trans.TransformPoint(position);
		}
		foreach (KeyValuePair<IntVector3, BlockPlacementCollector.Collection> item3 in Singleton.Manager<ManTechBuilder>.inst.BlockPlacementCollector)
		{
			if (hashSet.Remove(item3.Key))
			{
				BlockPlacementCollector.Collection value = item3.Value;
				if (value.Count > 0)
				{
					BlockPlacementCollector.Collection.Index index = value.First();
					BlockPlacementCollector.Collection.Index index2 = index;
					do
					{
						m_AvailablePlacementsAtCurrentCell.Add(index.Value);
						++index;
					}
					while (index != index2);
				}
			}
			if (hashSet.Count == 0)
			{
				break;
			}
		}
		Vector3 b = m_SelectedBlock.trans.TransformPoint(m_SelectedBlock.BlockCellBounds.center);
		foreach (BlockPlacementCollector.Placement item4 in m_AvailablePlacementsAtCurrentCell)
		{
			if (item4.orthoRot == m_PreferredOrientation)
			{
				float num = Vector3.Distance(GetBlockBoundsTechLocal(item4, m_SelectedBlock.filledCells).center, b);
				if (placement == null)
				{
					placement = item4;
				}
			}
		}
		int num2 = -1;
		if (placement == null)
		{
			foreach (BlockPlacementCollector.Placement item5 in m_AvailablePlacementsAtCurrentCell)
			{
				int num3 = 0;
				IntVector3[] filledCells = m_SelectedBlock.filledCells;
				foreach (IntVector3 intVector in filledCells)
				{
					IntVector3 item = item5.localPos + item5.orthoRot * intVector;
					if (m_CurrentPlacementFilledCellList.Contains(item))
					{
						num3++;
					}
				}
				if (num3 >= num2)
				{
					float num4 = Vector3.Distance(GetBlockBoundsTechLocal(item5, m_SelectedBlock.filledCells).center, b);
					if (num3 > num2)
					{
						num2 = num3;
						placement = item5;
					}
				}
			}
		}
		return placement;
	}

	private void DrawDebugRays(Ray testRay, bool haveHit, RaycastHit hit, float maxRayDistance)
	{
		if (m_ShowRaysCast)
		{
			if (haveHit)
			{
				_ = hit.distance;
			}
			if (!haveHit)
			{
				new Color(0f, 0.3f, 0.4f);
			}
			else
			{
				new Color(0f, 1f, 1f);
			}
			_ = m_CurrentRayStepValues.m_UseSphereCast;
		}
	}

	private RayStepValues GetRayStepValues(float inputStrength)
	{
		RayStepValues result = m_RayStepValues[0];
		for (int i = 0; i < m_RayStepValues.Length && !(inputStrength < m_RayStepValues[i].m_InputThreshold); i++)
		{
			result = m_RayStepValues[i];
		}
		return result;
	}

	private bool ValidateBlockHit(TankBlock currentBlock, TankBlock hitBlock, Vector3 inputDirection, Vector3 screenHitPoint)
	{
		bool flag = true;
		if (m_CurrentRayStepValues.m_ScreenSpaceCullPastCentreBounds)
		{
			Vector3 normalized = new Vector3(inputDirection.x, inputDirection.y, 0f).normalized;
			Vector3 position = currentBlock.trans.TransformPoint(currentBlock.BlockCellBounds.center);
			Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
			Vector3 position2 = hitBlock.trans.TransformPoint(hitBlock.BlockCellBounds.center);
			flag = Vector3.Dot((Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position2) - vector).normalized, normalized) > m_CurrentRayStepValues.m_ScreenSpaceCullCutOffValue;
		}
		if (flag && m_IgnoreBlockSelectOutsideScreen && (!(screenHitPoint.x >= 0f) || !(screenHitPoint.x < (float)Screen.currentResolution.width) || !(screenHitPoint.y >= 0f) || !(screenHitPoint.y < (float)Screen.currentResolution.height)))
		{
			flag = true;
		}
		return flag && Singleton.Manager<ManPointer>.inst.ItemIsGrabbable(currentBlock.visible);
	}

	private float GetScreenSpaceStepDistancePx()
	{
		float result = 0f;
		switch (m_CurrentRayStepValues.m_RayStepDistanceMode)
		{
		case CoordinateSpace.Screen:
			result = m_CurrentRayStepValues.m_ScreenSpaceStepSizePx;
			break;
		case CoordinateSpace.World:
			if (m_SelectedBlock != null)
			{
				Vector3 vector = m_SelectedBlock.trans.TransformPoint(m_SelectedBlock.BlockCellBounds.center);
				Vector3 position = vector + Singleton.cameraTrans.right * m_CurrentRayStepValues.m_WorldSpaceStepSize;
				Vector3 vector2 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(vector);
				Vector3 vector3 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
				result = (vector2 - vector3).SetZ(0f).magnitude;
			}
			break;
		}
		return result;
	}

	private void EnableControllerBuilding(bool enable)
	{
		m_Enable = enable;
		Singleton.Manager<ManPointer>.inst.m_DebugDisableNormalManPointer = enable;
		if (enable)
		{
			Singleton.Manager<ManPointer>.inst.ChangeBuildMode(ManPointer.BuildingMode.Grab);
		}
	}

	private void UpdatePotentialPickups()
	{
		Tank playerTank = Singleton.playerTank;
		s_CachedNearbyBlocks.Clear();
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(playerTank.trans.position, Singleton.Manager<ManPointer>.inst.PickupRange, ManSpawn.kVisibleMaskBlocksAndTech))
		{
			TankBlock tankBlock = null;
			if (item.block != null && item.block.tank == null)
			{
				tankBlock = item.block;
			}
			else if (item.tank != null && item.tank != playerTank && item.tank.Team == playerTank.Team && item.tank.blockman.blockCount == 1)
			{
				tankBlock = item.tank.blockman.IterateBlocks().FirstOrDefault();
			}
			if (tankBlock != null && Singleton.Manager<ManPointer>.inst.ItemIsGrabbable(tankBlock.visible))
			{
				s_CachedNearbyBlocks.Add(tankBlock);
			}
		}
		float num = Mathf.Cos(Singleton.camera.fieldOfView);
		TankBlock nearbyPickupBlock = null;
		foreach (TankBlock s_CachedNearbyBlock in s_CachedNearbyBlocks)
		{
			float num2 = Vector3.Dot((s_CachedNearbyBlock.trans.position - Singleton.cameraTrans.position).normalized, Singleton.cameraTrans.forward);
			if (num2 > num)
			{
				num = num2;
				nearbyPickupBlock = s_CachedNearbyBlock;
			}
		}
		m_NearbyPickupBlock = nearbyPickupBlock;
	}

	private void UpdateHighlights()
	{
		Tank playerTank = Singleton.playerTank;
		if (m_Enable)
		{
			Singleton.Manager<ManPointer>.inst.HideHighlight();
		}
		m_NumHighlightCellsInUse = 0;
		if (m_Enable && m_InBuildMode && playerTank != null)
		{
			if (m_SelectedBlock != null)
			{
				if (m_InteractionMode == InteractionMode.SelectBlockOnTech)
				{
					if (m_ShowSelectedCell)
					{
						AddCellHighlight(m_CurrentGridSelection, Color.cyan);
					}
					IntVector3[] filledCells = m_SelectedBlock.filledCells;
					foreach (IntVector3 intVector in filledCells)
					{
						IntVector3 cellLocalPos = m_SelectedBlock.trans.localPosition + m_SelectedBlock.trans.localRotation * intVector;
						AddCellHighlight(cellLocalPos, Color.cyan, 0.25f);
					}
					if (m_ShowSelectedTarget && m_PreviewMove)
					{
						TankBlock blockAtPosition = playerTank.blockman.GetBlockAtPosition(m_NextGridSelection);
						if (blockAtPosition != null && blockAtPosition != m_SelectedBlock)
						{
							filledCells = blockAtPosition.filledCells;
							foreach (IntVector3 intVector2 in filledCells)
							{
								IntVector3 cellLocalPos2 = blockAtPosition.trans.localPosition + blockAtPosition.trans.localRotation * intVector2;
								Color highlightColour = new Color(0.4f, 1f, 0.6f);
								AddCellHighlight(cellLocalPos2, highlightColour, 0.3f);
							}
						}
					}
				}
				else if (m_InteractionMode == InteractionMode.PlaceBlock)
				{
					IntVector3[] filledCells;
					if (m_ShowSelectedTarget && m_PreviewMove && m_NextPlacement != null)
					{
						filledCells = m_SelectedBlock.filledCells;
						foreach (IntVector3 intVector3 in filledCells)
						{
							IntVector3 intVector4 = m_NextPlacement.localPos + m_NextPlacement.orthoRot * intVector3;
							Color highlightColour2 = ((intVector4 == m_CurrentGridSelection) ? new Color(1f, 0.6f, 0.2f) : Color.yellow);
							AddCellHighlight(intVector4, highlightColour2, 0.4f);
						}
					}
					Vector3 vector = ((m_CurrentPlacement != null) ? m_CurrentPlacement.localPos : m_HeldDetachedBlockPreDetachLocalPos);
					Quaternion quaternion = ((m_CurrentPlacement != null) ? m_CurrentPlacement.orthoRot : m_HeldDetachedBlockPreDetachLocalRot);
					Color highlightColour3 = ((m_HeldBlockWasAttachedToTech || (m_CurrentPlacement != null && Singleton.Manager<ManTechBuilder>.inst.IsPlacementAllowed(m_SelectedBlock, m_CurrentPlacement))) ? Color.green : Color.red);
					filledCells = m_SelectedBlock.filledCells;
					foreach (IntVector3 intVector5 in filledCells)
					{
						IntVector3 cellLocalPos3 = vector + quaternion * intVector5;
						AddCellHighlight(cellLocalPos3, highlightColour3, 0.4f);
					}
					if (m_ShowSelectedCell)
					{
						if (m_CurrentPlacement != null)
						{
							AddCellHighlight(m_CurrentMatchedGroup.localCellPos, Color.cyan);
						}
						else if (m_HeldBlockWasAttachedToTech)
						{
							AddCellHighlight(m_HeldDetachedBlockPreDetachLocalPos, Color.cyan);
						}
					}
				}
			}
			if (m_GroundPickupEnabled && m_NearbyPickupBlock != null && Singleton.Manager<ManPointer>.inst.DraggingItem == null)
			{
				IntVector3[] filledCells = m_NearbyPickupBlock.filledCells;
				foreach (IntVector3 intVector6 in filledCells)
				{
					Vector3 worldPos = m_NearbyPickupBlock.trans.position + m_NearbyPickupBlock.trans.rotation * intVector6;
					AddCellHighlight(worldPos, m_NearbyPickupBlock.trans.rotation, Color.magenta, 0.5f);
				}
			}
		}
		RecycleAllUnusedHighlightCells();
	}

	private void AddCellHighlight(IntVector3 cellLocalPos, Color highlightColour = default(Color), float highlightColourAlpha = -1f)
	{
		Tank playerTank = Singleton.playerTank;
		AddCellHighlight(playerTank.trans.position + playerTank.trans.rotation * cellLocalPos, playerTank.trans.rotation, highlightColour, highlightColourAlpha);
	}

	private void AddCellHighlight(Vector3 worldPos, Quaternion rotation, Color highlightColour = default(Color), float highlightColourAlpha = -1f)
	{
		if (!(m_CellHighlightPrefab != null))
		{
			return;
		}
		if (m_NumHighlightCellsInUse >= m_CellHighlightObjects.Count)
		{
			m_CellHighlightObjects.Add(m_CellHighlightPrefab.Spawn());
		}
		Transform transform = m_CellHighlightObjects[m_NumHighlightCellsInUse];
		m_NumHighlightCellsInUse++;
		transform.position = worldPos;
		transform.rotation = rotation;
		transform.localScale = Vector3.one * 1.1f;
		if (m_CellHighlightMaterialTemplate != null)
		{
			if (highlightColourAlpha > 0f)
			{
				highlightColour.a = highlightColourAlpha;
			}
			else if (Mathf.Approximately(highlightColour.a, 1f))
			{
				highlightColour.a = 0.15f;
			}
			if (!m_CellHighlightMaterials.TryGetValue(highlightColour, out var value))
			{
				value = new Material(m_CellHighlightMaterialTemplate);
				value.color = highlightColour;
				m_CellHighlightMaterials.Add(highlightColour, value);
			}
			transform.GetComponent<MeshRenderer>().material = value;
		}
	}

	private void RecycleAllUnusedHighlightCells()
	{
		for (int num = m_CellHighlightObjects.Count - 1; num >= m_NumHighlightCellsInUse; num--)
		{
			m_CellHighlightObjects[num].Recycle();
			m_CellHighlightObjects.RemoveAt(num);
		}
	}

	private void UpdateDebugInputDirectionIndicator()
	{
		if (m_DebugInputPointerObj != null)
		{
			m_DebugInputPointerObj.gameObject.SetActive(m_ShowInputDirection && m_Enable && m_InBuildMode && m_SelectedBlock != null);
			if (m_Enable && m_InBuildMode && m_ShowInputDirection && m_SelectedBlock != null)
			{
				Vector3 position = m_SelectedBlock.trans.TransformPoint(m_SelectedBlock.BlockCellBounds.center);
				Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
				Vector3 position2 = new Vector3(vector.x, vector.y, Singleton.camera.nearClipPlane + 10f);
				Vector3 position3 = Singleton.camera.ScreenToWorldPoint(position2);
				Vector3 normalized = new Vector3(m_BuildVectorRaw.x, m_BuildVectorRaw.y, 0f).normalized;
				Vector3 vector2 = vector + normalized * 100f;
				Vector3 position4 = new Vector3(vector2.x, vector2.y, Singleton.camera.nearClipPlane + 10f);
				Vector3 worldPosition = Singleton.camera.ScreenToWorldPoint(position4);
				m_DebugInputPointerObj.transform.position = position3;
				m_DebugInputPointerObj.transform.LookAt(worldPosition, Singleton.camera.transform.forward);
			}
		}
	}

	private void SetInteractionMode(InteractionMode newInteractionMode)
	{
		if (newInteractionMode != m_InteractionMode)
		{
			ExitInteractionMode(m_InteractionMode);
			m_InteractionMode = newInteractionMode;
			EnterInteractionMode(m_InteractionMode);
		}
	}

	private void EnterInteractionMode(InteractionMode interactionMode)
	{
		switch (interactionMode)
		{
		case InteractionMode.SelectBlockOnTech:
			SetSelectedBlock(null);
			break;
		case InteractionMode.PlaceBlock:
			if (!m_HoldingLooseBlock)
			{
				SpawnNewPaintingBlock(m_CurrentMatchedGroup, m_CurrentPlacement);
			}
			break;
		case InteractionMode.SelectInventoryBlock:
		{
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: true, UIShopBlockSelect.ExpandReason.Button);
			UIPaletteBlockSelect uIPaletteBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
			if ((bool)uIPaletteBlockSelect)
			{
				uIPaletteBlockSelect.TrySelectBlockType(m_BlockToPlace);
			}
			break;
		}
		default:
			d.LogError("Not implemented!");
			break;
		case InteractionMode.None:
			break;
		}
	}

	private void ExitInteractionMode(InteractionMode interactionMode)
	{
		switch (interactionMode)
		{
		case InteractionMode.SelectBlockOnTech:
			m_CurrentPlacement = null;
			m_CurrentMatchedGroup = default(CachedPlacementGroupData);
			break;
		case InteractionMode.PlaceBlock:
			if (m_HoldingLooseBlock && Singleton.Manager<ManPointer>.inst.DraggingItem != null)
			{
				DropHeldBlock();
			}
			Singleton.Manager<ManPointer>.inst.RemovePaintingBlock();
			Singleton.Manager<ManPointer>.inst.ClearHasBlockToSpawnFlag();
			break;
		case InteractionMode.SelectInventoryBlock:
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: false, UIShopBlockSelect.ExpandReason.Button);
			break;
		default:
			d.LogError("Not implemented!");
			break;
		case InteractionMode.None:
			break;
		}
	}

	private void OnPaintingBlockSelected(BlockTypes blockType, int blockQuantity)
	{
		if (blockType != m_BlockToPlace)
		{
			m_BlockToPlace = blockType;
			if (Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.playerTank != null)
			{
				Singleton.Manager<ManPointer>.inst.DraggingItem.trans.position = (Singleton.playerTank.boundsCentreWorld + Singleton.cameraTrans.position) / 2f;
			}
			Singleton.Manager<ManTechBuilder>.inst.ResetAPCollection();
		}
	}

	private void OnNoPaintingBlockSelected()
	{
		if (m_Enable && m_InBuildMode)
		{
			_ = Singleton.Manager<ManPointer>.inst.DraggingItem == null;
		}
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void NeverUpdate()
	{
		Tank playerTank = Singleton.playerTank;
		if (m_Enable && m_InBuildMode && m_InteractionMode == InteractionMode.PlaceBlock && playerTank != null && Singleton.Manager<ManPointer>.inst.DraggingItem != null)
		{
			if (m_CurrentPlacement != null)
			{
				Singleton.Manager<ManPointer>.inst.DraggingItem.trans.SetPositionIfChanged(playerTank.trans.TransformPoint(m_CurrentPlacement.localPos));
				Singleton.Manager<ManPointer>.inst.DraggingItem.trans.SetRotationIfChanged(playerTank.trans.rotation * m_CurrentPlacement.orthoRot);
			}
			else if (m_HeldBlockWasAttachedToTech)
			{
				Singleton.Manager<ManPointer>.inst.DraggingItem.trans.SetPositionIfChanged(playerTank.trans.TransformPoint(m_HeldDetachedBlockPreDetachLocalPos));
				Singleton.Manager<ManPointer>.inst.DraggingItem.trans.SetRotationIfChanged(playerTank.trans.rotation * m_HeldDetachedBlockPreDetachLocalRot);
			}
		}
		if (m_InteractionMode == InteractionMode.PlaceBlock && Singleton.Manager<ManPointer>.inst.DraggingItem == null)
		{
			SetInteractionMode(InteractionMode.SelectBlockOnTech);
		}
		if (m_HoldingLooseBlock && Singleton.Manager<ManPointer>.inst.DraggingItem == null)
		{
			m_HoldingLooseBlock = false;
		}
		m_HeldBlockWasAttachedToTech = m_HeldBlockWasAttachedToTech && m_HoldingLooseBlock;
		HandleControlInput();
		if (m_Enable && m_InBuildMode && playerTank != null)
		{
			switch (m_InteractionMode)
			{
			case InteractionMode.SelectBlockOnTech:
				UpdateBlockSelection();
				if (m_GroundPickupEnabled)
				{
					UpdatePotentialPickups();
				}
				break;
			case InteractionMode.PlaceBlock:
				UpdateBlockPlacement();
				break;
			default:
				d.LogError("Not Implemented!");
				break;
			case InteractionMode.SelectInventoryBlock:
				break;
			}
		}
		if (playerTank == null)
		{
			SetSelectedBlock(null);
			m_CurrentPlacement = null;
			m_CurrentMatchedGroup = default(CachedPlacementGroupData);
		}
		UpdateHighlights();
		UpdateDebugInputDirectionIndicator();
	}

	private void OnDrawGizmos()
	{
		Tank playerTank = Singleton.playerTank;
		if (playerTank != null)
		{
			if (m_ShowPlacementCellCandidates && m_LastPlacementData != null)
			{
				Gizmos.matrix = playerTank.trans.localToWorldMatrix;
				foreach (CachedPlacementGroupData lastPlacementDatum in m_LastPlacementData)
				{
					float num = GetScoreValue(lastPlacementDatum) / 2f;
					Gizmos.color = new Color(num, 1f - num, 0f, m_PlacementCellCandidateBoxAlpha);
					IntVector3 apPos = lastPlacementDatum.apPos;
					Gizmos.DrawCube(apPos.APtoLocal(), Vector3.one * 0.3f);
				}
			}
			if (m_ShowInputPlane && Singleton.Manager<ManPointer>.inst.DraggingItem != null && m_BuildVectorRaw.magnitude > 0.1f)
			{
				Vector3 pos;
				if (m_CurrentPlacement != null)
				{
					Vector3 position = m_CurrentPlacement.localPos + m_CurrentPlacement.orthoRot * Singleton.Manager<ManPointer>.inst.DraggingItem.block.BlockCellBounds.center;
					pos = playerTank.trans.TransformPoint(position);
				}
				else
				{
					pos = Singleton.Manager<ManPointer>.inst.DraggingItem.trans.TransformPoint(Singleton.Manager<ManPointer>.inst.DraggingItem.block.BlockCellBounds.center);
				}
				_ = Singleton.cameraTrans.TransformDirection(m_BuildVectorRaw.ToVector3XZ()).normalized;
				Vector3 direction = new Vector3(m_BuildVectorRaw.x, m_BuildVectorRaw.y, 0f);
				Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.LookRotation(Vector3.Cross(Singleton.cameraTrans.TransformDirection(direction).normalized, Singleton.cameraTrans.forward).normalized, Singleton.cameraTrans.forward), Vector3.one);
				Gizmos.color = new Color(1f, 0.7f, 0f, 0.4f);
				Gizmos.DrawCube(Vector3.zero, new Vector3(8f, 8f, 0.0001f));
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}
}
