#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using cakeslice;

public class ManPointer : Singleton.Manager<ManPointer>
{
	public enum Event
	{
		LMB,
		RMB,
		MMB,
		MWheel
	}

	private enum TargetingMode
	{
		Mouse,
		Joypad,
		Nearby
	}

	private enum HighlightMethod
	{
		Box,
		Glow
	}

	public enum HighlightVariation
	{
		Normal,
		Attaching,
		Invalid,
		BlockLimited
	}

	public interface MouseEventConsumer
	{
		void OnMouseEvent(Event me, bool down);
	}

	public interface OpenMenuEventConsumer
	{
		bool CanOpenMenu(bool isRadial);

		bool OnOpenMenuEvent(OpenMenuEventData radialMenuPair);
	}

	public enum DragAction
	{
		Grab,
		Update,
		ReleaseLoose,
		ReleaseAllowPlace,
		PostRelease
	}

	public enum BuildingMode
	{
		Grab,
		PaintBlock,
		Placing,
		PaintSkin,
		PaintSkinTech,
		BlockDetach
	}

	public enum DragDisableReason
	{
		RadialMenu,
		AITargetSelect,
		HudMasked,
		SumoMode
	}

	public enum PreventChannel
	{
		HUD,
		Multiplayer,
		DevCommandWindow
	}

	public enum CursorEmulationEnabledReason
	{
		InteractionMode,
		DraggingItem,
		PlacingTech,
		PaintingBlock,
		PaintingBlockSkin,
		PaintingTechSkin,
		WorldMap
	}

	public bool m_DebugDisableNormalManPointer;

	[SerializeField]
	private float m_DefaultPickupRange = 50f;

	[SerializeField]
	private float m_DefaultDragPlaneDistance = 15f;

	[SerializeField]
	private float m_DraggingItemSpeedDamping = 0.5f;

	[SerializeField]
	private float m_DraggingRetainTechFocusRadius = 5f;

	[SerializeField]
	private float m_DraggingItemReleaseSpeedMul = 20f;

	[SerializeField]
	private float m_DraggingItemReleaseSpinMul = 3f;

	[SerializeField]
	private float m_AnchorBlockMaxPlaceDistance = 30f;

	[SerializeField]
	private float m_AnchorBlockMaxPlaceDistanceGamepad = 80f;

	[SerializeField]
	private ObjectHighlight m_HighlightPrefab;

	[SerializeField]
	private GameObject m_ChunkHighlightPrefab;

	[SerializeField]
	private PlacementSelection m_PlacementPrefab;

	[SerializeField]
	private float m_NearbyPickupRange = 10f;

	[SerializeField]
	private bool m_AllowResourcePickupFromNearby;

	[SerializeField]
	private Vector2 m_DefaultEmulatedCursorPosition = new Vector2(0.5f, 0.68f);

	[SerializeField]
	private Transform m_RemoveBlockToInventoryVFX;

	public Event<Event, bool, bool> MouseEvent;

	public Event<Visible, DragAction, Vector3> DragEvent;

	public Event<Visible> ReplaceHeldItemEvent;

	public Event<TankBlock> OnBlockPaintedEvent;

	public Event<bool, bool, bool> OnPlacingEvent;

	private Bitfield<DragDisableReason> m_DragDisabledReason = new Bitfield<DragDisableReason>();

	private Vector3 m_DraggingItemVelocity;

	private Vector3 m_DraggingItemScreenOffset;

	private float m_DragPlaneDistance;

	private bool m_DraggingItemFrozen;

	private float m_DraggingItemGrabTimestamp;

	private PlacementSelection m_Placement;

	private bool m_TargetValid;

	private float m_TimeSinceLastUpdate;

	private const float kNearbyPickupRepeatInterval = 0.15f;

	private const float k_DragItemClearHolderAfterSeconds = 0.5f;

	private ObjectHighlight m_Highlight;

	private ObjectHighlight m_DropHighlight;

	private GameObject m_ChunkHighlight;

	private Visible m_HighlightedVisible;

	private HighlightMethod m_HighlightMethod;

	private HighlightVariation m_HighlightType;

	private Vector3 m_LMBDownPos;

	private Vector3 m_RMBDownPos;

	private Vector3 m_MMBDownPos;

	private TargetingMode m_TargetingMode;

	private Vector2 m_EmulatedCursorScreenPosition;

	private Rect? m_EmulatedCursorBounds;

	private Bitfield<CursorEmulationEnabledReason> m_CursorEmulationEnabledReasons = new Bitfield<CursorEmulationEnabledReason>();

	private float m_CurrentPickupRange;

	private BuildingMode m_BuildingMode;

	private Bitfield<PreventChannel> m_PreventPaintingMaskMouse = new Bitfield<PreventChannel>();

	private Bitfield<PreventChannel> m_PreventInteractionMaskMouse = new Bitfield<PreventChannel>();

	private Bitfield<PreventChannel> m_PreventPaintingMaskGamepad = new Bitfield<PreventChannel>();

	private Bitfield<PreventChannel> m_PreventInteractionMaskGamepad = new Bitfield<PreventChannel>();

	private BlockTypes m_BlockToSpawn;

	private bool m_HasBlockToSpawn;

	private bool m_IsPlayerChosenRotation;

	private int m_PlacementPreferredOrientationIndex;

	private TankPreset.BlockSpec? m_BlockSerialState;

	private float m_BlockPaintTimestamp;

	private int m_LastPaintingBlockVisID = -1;

	private bool m_IsInteractionModeLocked;

	private bool m_PaintingSkin;

	private bool m_BlockDetachStraightToInventory = true;

	private static int s_RaycastHitResultBufferSize = 64;

	private static RaycastHit[] s_RaycastHitResults = new RaycastHit[s_RaycastHitResultBufferSize];

	private bool m_RadialMenuDisabled;

	private static Dictionary<Type, Component> objectCache = new Dictionary<Type, Component>();

	private bool m_PaintButGrab;

	public Transform targetObject { get; private set; }

	public Visible targetVisible { get; private set; }

	public Tank targetTank { get; private set; }

	public TankBlock targetBlock { get; private set; }

	public Vector3 targetPosition { get; private set; }

	public BuildingMode BuildMode => m_BuildingMode;

	public Vector3 DragHighlightScale => m_Highlight.HighlightScale;

	public bool IsPaintingBlocked
	{
		get
		{
			if (m_TargetingMode != TargetingMode.Mouse)
			{
				return m_PreventPaintingMaskGamepad.AnySet;
			}
			return m_PreventPaintingMaskMouse.AnySet;
		}
	}

	public bool IsInteractionBlocked
	{
		get
		{
			if (m_TargetingMode != TargetingMode.Mouse)
			{
				return m_PreventInteractionMaskGamepad.AnySet;
			}
			return m_PreventInteractionMaskMouse.AnySet;
		}
	}

	public float DraggingItemReleaseSpeedMul => m_DraggingItemReleaseSpeedMul;

	public float DraggingItemReleaseSpinMul => m_DraggingItemReleaseSpinMul;

	private static int ObjectPickerMask => Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask | Globals.inst.layerScenery.mask | Globals.inst.layerPickup.mask | Globals.inst.layerTerrain.mask;

	public Visible DraggingItem { get; private set; }

	public Vector3 DraggingItemVelocity => m_DraggingItemVelocity;

	public Tank DraggingFocusTech { get; set; }

	public ModuleItemHolder.Stack DropTarget { get; private set; }

	public bool IsDraggingController
	{
		get
		{
			if ((bool)DraggingItem && DraggingItem.type == ObjectTypes.Block)
			{
				return DraggingItem.block.IsController;
			}
			return false;
		}
	}

	public Vector3 DragPositionOnScreen
	{
		get
		{
			if (!IsCursorEmulationEnabled)
			{
				return Input.mousePosition;
			}
			return m_EmulatedCursorScreenPosition.ToVector3XY();
		}
	}

	public float PickupRange => m_CurrentPickupRange;

	public bool IsCursorEmulationEnabled
	{
		get
		{
			if (m_CursorEmulationEnabledReasons.AnySet)
			{
				return Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
			}
			return false;
		}
	}

	public bool IsEmulatedCursorVisible
	{
		get
		{
			if (!m_CursorEmulationEnabledReasons.Contains(0))
			{
				return m_CursorEmulationEnabledReasons.Contains(6);
			}
			return true;
		}
	}

	public bool IsInteractionModeEnabled => m_CursorEmulationEnabledReasons.Contains(0);

	public bool IsUsingMouse => m_TargetingMode == TargetingMode.Mouse;

	public bool HasBlockClipboard => m_BlockSerialState.HasValue;

	public void ReplaceHeldItem(Visible replacement)
	{
		if ((bool)replacement && replacement.type == ObjectTypes.Block && (bool)replacement.block)
		{
			replacement.block.EnsureNoTriggerStaySubscribers();
		}
		if ((bool)DraggingItem && DraggingItem.type == ObjectTypes.Block && (bool)DraggingItem.block)
		{
			DraggingItem.block.UnsubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
			DraggingItem.block.EnsureNoTriggerStaySubscribers();
		}
		DraggingItem = replacement;
		EnableCursorEmulation(replacement != null, CursorEmulationEnabledReason.DraggingItem);
		ReplaceHeldItemEvent.Send(DraggingItem);
	}

	public void ReleaseDraggingItem(bool applyVelocity = true)
	{
		ReleaseDrag();
	}

	public void ForceRemoveDraggedItem()
	{
		if ((bool)DraggingItem && (bool)DraggingItem.block)
		{
			RemovePaintingBlock();
			EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.DraggingItem);
		}
		SetTargetVisible(null);
		ClearHasBlockToSpawnFlag();
	}

	public void FreezeDraggingItem(bool freeze)
	{
		m_DraggingItemFrozen = freeze;
	}

	public void ResetDraggingItemCentrePosition()
	{
		DraggingItem.centrePosition = GetDragPosWorld(DragPositionOnScreen + m_DraggingItemScreenOffset);
	}

	public void SetDragEnabled(bool enabled, DragDisableReason reason)
	{
		bool flag = !enabled;
		m_DragDisabledReason.Set((int)reason, flag);
	}

	public bool IsDragEnabled()
	{
		return m_DragDisabledReason.IsNull;
	}

	public bool IsDragEnabled(DragDisableReason reason)
	{
		return !m_DragDisabledReason.Contains((int)reason);
	}

	public bool ItemIsGrabbable(Visible item)
	{
		if (!IsDragEnabled())
		{
			return false;
		}
		if (item.IsNull())
		{
			return false;
		}
		if (item.type != ObjectTypes.Block && item.type != ObjectTypes.Chunk)
		{
			return false;
		}
		if (!item.IsGrabbable)
		{
			return false;
		}
		if (item.holderStack != null && item.holderStack.IsPickupLocked)
		{
			return false;
		}
		bool flag = false;
		if (item.type == ObjectTypes.Block)
		{
			TankBlock block = item.block;
			if (block.IsAttached && (object)block.tank == Singleton.playerTank)
			{
				flag = true;
			}
		}
		else if (item.type == ObjectTypes.Chunk && item.holderStack != null)
		{
			TankBlock block2 = item.holderStack.myHolder.block;
			if (block2.IsAttached && (object)block2.tank == Singleton.playerTank)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			Vector3 vector = ((Singleton.playerTank.IsNotNull() && Singleton.playerTank.blockman.blockCount > 0) ? Singleton.playerTank.boundsCentreWorld : Singleton.cameraTrans.position);
			float num = m_CurrentPickupRange;
			if (Singleton.playerTank.IsNotNull())
			{
				num += Singleton.playerTank.blockBounds.extents.magnitude;
			}
			if ((item.centrePosition - vector).sqrMagnitude > num * num)
			{
				return false;
			}
		}
		if (item.type == ObjectTypes.Block)
		{
			TankBlock block3 = item.block;
			bool isAttached = block3.IsAttached;
			if (Singleton.Manager<ManUndo>.inst.UndoInProgress && !isAttached && (bool)item.rbody && !item.rbody.useGravity)
			{
				return false;
			}
			Tank tank = block3.tank;
			bool flag2 = !isAttached || (!Mode<ModeMain>.inst.TutorialDisableBlockRemoval && Singleton.playerTank.IsNotNull() && tank.Team == Singleton.playerTank.Team && !tank.blockman.locked);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (flag2 && isAttached && Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull())
				{
					flag2 = tank.netTech.CanPlayerModify(Singleton.Manager<ManNetwork>.inst.MyPlayer, block3.IsController);
				}
				if (flag2 && !isAttached && block3.netBlock.IsNotNull() && Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull() && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.IsNotNull())
				{
					uint initialSpawnShieldID = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.InitialSpawnShieldID;
					uint initialSpawnShieldID2 = block3.netBlock.InitialSpawnShieldID;
					if (initialSpawnShieldID2 != 0 && initialSpawnShieldID2 != initialSpawnShieldID)
					{
						NetSpawnPoint pNSP = null;
						if (Singleton.Manager<ManNetwork>.inst.IsSpawnShieldActive(initialSpawnShieldID2, ref pNSP))
						{
							flag2 = false;
						}
					}
				}
			}
			if (!flag2)
			{
				return false;
			}
		}
		return true;
	}

	public void SetPickupRange(float range)
	{
		m_CurrentPickupRange = range;
	}

	public void ResetPickupRange()
	{
		m_CurrentPickupRange = m_DefaultPickupRange;
	}

	public static void RegisterStaticMouseEvents<T>(bool grabUpEvent) where T : Component, MouseEventConsumer
	{
		d.Log(string.Format("RegisterStaticMouseEvents {0} {1}", grabUpEvent ? "(grabUp)" : "", typeof(T)));
		if (grabUpEvent)
		{
			Singleton.DoOnceAfterStart(delegate
			{
				Singleton.Manager<ManPointer>.inst.MouseEvent.Subscribe(StaticEventDispatchByObjectCached<T>);
			});
		}
		else
		{
			Singleton.DoOnceAfterStart(delegate
			{
				Singleton.Manager<ManPointer>.inst.MouseEvent.Subscribe(StaticEventDispatchByObject<T>);
			});
		}
	}

	public void PreventPainting(PreventChannel channel, bool prevent)
	{
		bool isPaintingBlocked = IsPaintingBlocked;
		if (channel != PreventChannel.HUD)
		{
			m_PreventPaintingMaskGamepad.Set((int)channel, prevent);
		}
		m_PreventPaintingMaskMouse.Set((int)channel, prevent);
		UpdatePreventPainting(isPaintingBlocked);
	}

	private void UpdatePreventPainting(bool wasBlocked)
	{
		bool isPaintingBlocked = IsPaintingBlocked;
		if (isPaintingBlocked != wasBlocked && m_BuildingMode == BuildingMode.PaintBlock && !m_DebugDisableNormalManPointer)
		{
			if (isPaintingBlocked)
			{
				RemovePaintingBlock();
			}
			else
			{
				TrySpawnPaintingBlock();
			}
		}
	}

	public void ReskinPaintingBlock()
	{
		if (m_BuildingMode == BuildingMode.PaintBlock && DraggingItem.IsNotNull())
		{
			RemovePaintingBlock();
			TrySpawnPaintingBlock();
		}
	}

	public void PreventInteraction(PreventChannel channel, bool preventInteraction)
	{
		bool isInteractionBlocked = IsInteractionBlocked;
		if (channel != PreventChannel.HUD)
		{
			m_PreventInteractionMaskGamepad.Set((int)channel, preventInteraction);
		}
		m_PreventInteractionMaskMouse.Set((int)channel, preventInteraction);
		UpdatePreventInteraction(isInteractionBlocked);
	}

	private void UpdatePreventInteraction(bool wasBlocked)
	{
		bool isInteractionBlocked = IsInteractionBlocked;
		if (!wasBlocked && isInteractionBlocked && m_BuildingMode == BuildingMode.Grab)
		{
			ReleaseDrag();
		}
		if (wasBlocked != isInteractionBlocked && m_BuildingMode == BuildingMode.Placing && m_Placement != null)
		{
			m_Placement.HiddenViaUI(isInteractionBlocked);
		}
	}

	public void EnablePlaceMode(float placementRadius, Action<Vector3, Quaternion> selectionCallback, Action cancelledCallback, PlacementSelection.ValidatorFunc validityCheck)
	{
		if (m_Placement == null)
		{
			m_Placement = m_PlacementPrefab.Spawn(targetPosition);
		}
		m_Placement.SetRadius(placementRadius);
		m_Placement.SetCallbacks(selectionCallback, cancelledCallback, validityCheck);
		if (IsInteractionBlocked && !Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_Placement.HiddenViaUI(hidden: true);
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIDeployTechPanel);
		EnableCursorEmulation(enable: true, CursorEmulationEnabledReason.PlacingTech, resetPosition: true);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, HandleJoypadOnPlacementSubmit);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, HandleJoypadOnPlacementCancel);
		ChangeBuildMode(BuildingMode.Placing);
	}

	public void DisablePlacementMode()
	{
		if (m_Placement != null)
		{
			m_Placement.ClearCallbacks();
			m_Placement.Recycle();
			m_Placement = null;
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIDeployTechPanel);
		EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.PlacingTech);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, HandleJoypadOnPlacementSubmit);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, HandleJoypadOnPlacementCancel);
		if (m_BuildingMode == BuildingMode.Placing)
		{
			ChangeBuildMode(BuildingMode.Grab);
		}
	}

	public string GetPlacementBlockedReasonText(PlacementSelection.InvalidReason failReason)
	{
		return m_PlacementPrefab.GetFailReasonText(failReason);
	}

	public void ChangeBuildMode(BuildingMode mode)
	{
		if (mode == m_BuildingMode)
		{
			return;
		}
		bool flag = (m_BuildingMode == BuildingMode.PaintSkin || m_BuildingMode == BuildingMode.PaintSkinTech) && (mode == BuildingMode.PaintSkin || mode == BuildingMode.PaintSkinTech);
		switch (m_BuildingMode)
		{
		case BuildingMode.Grab:
			if ((bool)DraggingItem)
			{
				ReleaseDrag();
			}
			break;
		case BuildingMode.PaintBlock:
			RemovePaintingBlock();
			break;
		case BuildingMode.PaintSkin:
			HideHighlight();
			m_PaintingSkin = false;
			if (!flag)
			{
				EnableInteractionMode(enable: false);
			}
			EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.PaintingBlockSkin);
			break;
		case BuildingMode.PaintSkinTech:
			HideHighlight();
			if (!flag)
			{
				EnableInteractionMode(enable: false);
			}
			EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.PaintingTechSkin);
			break;
		}
		m_BuildingMode = mode;
		switch (mode)
		{
		case BuildingMode.Grab:
			DraggingFocusTech = null;
			break;
		case BuildingMode.PaintBlock:
			TrySpawnPaintingBlock();
			break;
		case BuildingMode.Placing:
			d.Assert(m_Placement != null, "ManSpawn.ChangeBuildMode - Do not call this directly for BuildingMode.Placing, call EnablePlaceMode to setup callbacks");
			break;
		case BuildingMode.PaintSkin:
			if (!flag)
			{
				EnableInteractionMode(enable: true);
			}
			EnableCursorEmulation(enable: true, CursorEmulationEnabledReason.PaintingBlockSkin);
			break;
		case BuildingMode.PaintSkinTech:
			if (!flag)
			{
				EnableInteractionMode(enable: true);
			}
			EnableCursorEmulation(enable: true, CursorEmulationEnabledReason.PaintingTechSkin);
			break;
		case BuildingMode.BlockDetach:
			break;
		}
	}

	public void HighlightVisible(Visible visible)
	{
		bool flag = BuildMode == BuildingMode.PaintSkin || BuildMode == BuildingMode.PaintSkinTech;
		Outline.OutlineEnableReason reason = (flag ? Outline.OutlineEnableReason.CustomSkinHighlight : Outline.OutlineEnableReason.Pointer);
		bool flag2 = m_HighlightMethod == HighlightMethod.Glow || flag;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = m_HighlightType == HighlightVariation.BlockLimited;
		if (visible.tank.IsNotNull())
		{
			flag5 = flag2 && flag;
		}
		else if (visible.block.IsNotNull())
		{
			flag4 = !flag2 || m_HighlightType == HighlightVariation.Invalid;
		}
		else if (visible.pickup.IsNotNull())
		{
			flag3 = !flag2;
		}
		SetHighlightType(HighlightVariation.Normal);
		if (flag4)
		{
			m_Highlight.Highlight(visible);
		}
		else
		{
			m_Highlight.HideHighlight();
		}
		if (flag3)
		{
			if (m_ChunkHighlight.transform.parent != visible.trans)
			{
				m_ChunkHighlight.transform.SetParent(visible.trans, worldPositionStays: false);
				m_ChunkHighlight.transform.position = visible.centrePosition;
			}
			m_ChunkHighlight.SetActive(value: true);
		}
		else
		{
			m_ChunkHighlight.SetActive(value: false);
		}
		if (m_HighlightedVisible != null && (!flag2 || visible != m_HighlightedVisible))
		{
			m_HighlightedVisible.EnableOutlineGlow(enable: false, reason);
			m_HighlightedVisible.EnableOutlineGlow(enable: false, Outline.OutlineEnableReason.CustomSkinHighlight);
		}
		if (flag2)
		{
			if (flag6)
			{
				OutlineEffect.Instance.SetSkinPaintingColour(canPaint: false);
				visible.Outline.EnableOutline(enable: false, Outline.OutlineEnableReason.Pointer);
				visible.Outline.EnableOutline(enable: false, Outline.OutlineEnableReason.CustomSkinHighlight);
				visible.Outline.EnableOutline(enable: true, Outline.OutlineEnableReason.CustomSkinHighlight);
			}
			else
			{
				if (flag)
				{
					OutlineEffect.Instance.SetSkinPaintingColour(Singleton.Manager<ManCustomSkins>.inst.CanPaintVisible(visible));
				}
				if (flag5)
				{
					visible.EnableOutlineGlow(enable: true, Outline.OutlineEnableReason.CustomSkinHighlight);
				}
				else
				{
					visible.Outline.EnableOutline(enable: true, reason);
				}
			}
		}
		if (visible != m_HighlightedVisible)
		{
			if (m_HighlightedVisible != null)
			{
				m_HighlightedVisible.RecycledEvent.Unsubscribe(OnHighlightedVisibleRecycled);
			}
			if (visible != null)
			{
				visible.RecycledEvent.Subscribe(OnHighlightedVisibleRecycled);
			}
		}
		m_HighlightedVisible = visible;
	}

	public void HideHighlight()
	{
		if ((bool)m_Highlight)
		{
			m_Highlight.HideHighlight();
		}
		if ((bool)m_ChunkHighlight)
		{
			m_ChunkHighlight.SetActive(value: false);
			m_ChunkHighlight.transform.SetParent(base.transform, worldPositionStays: false);
		}
		if (m_HighlightedVisible != null)
		{
			m_HighlightedVisible.EnableOutlineGlow(enable: false, Outline.OutlineEnableReason.Pointer);
			m_HighlightedVisible.EnableOutlineGlow(enable: false, Outline.OutlineEnableReason.CustomSkinHighlight);
		}
		if (m_HighlightedVisible != null)
		{
			m_HighlightedVisible.RecycledEvent.Unsubscribe(OnHighlightedVisibleRecycled);
		}
		m_HighlightedVisible = null;
	}

	public void SetHighlightType(HighlightVariation type)
	{
		m_HighlightType = type;
		m_Highlight.SetHighlightType(type);
		_ = m_HighlightedVisible != null;
	}

	public void SetInteractionModeToggleLocked(bool locked)
	{
		m_IsInteractionModeLocked = locked;
	}

	public void EnableInteractionMode(bool enable)
	{
		if (!m_IsInteractionModeLocked)
		{
			bool resetPosition = true;
			if (enable && !IsInteractionModeEnabled && Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded())
			{
				Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: false, UIShopBlockSelect.ExpandReason.Button, forceClose: true);
				resetPosition = false;
			}
			EnableCursorEmulation(enable, CursorEmulationEnabledReason.InteractionMode, resetPosition);
		}
	}

	public void EnableCursorEmulation(bool enable, CursorEmulationEnabledReason enableReason, bool resetPosition = false)
	{
		bool anySet = m_CursorEmulationEnabledReasons.AnySet;
		bool num = m_CursorEmulationEnabledReasons.Contains(4);
		bool flag = !num && m_CursorEmulationEnabledReasons.Contains(5);
		int num2;
		int num3;
		if (!num)
		{
			num2 = (m_CursorEmulationEnabledReasons.Contains(3) ? 1 : 0);
			if (num2 != 0)
			{
				num3 = 0;
				goto IL_0051;
			}
		}
		else
		{
			num2 = 0;
		}
		num3 = (m_CursorEmulationEnabledReasons.Contains(1) ? 1 : 0);
		if (num3 == 0)
		{
			goto IL_0051;
		}
		int num4 = 0;
		goto IL_0063;
		IL_0072:
		int num5;
		bool flag2 = (byte)num5 != 0;
		m_CursorEmulationEnabledReasons.Set((int)enableReason, enable);
		bool flag3 = m_CursorEmulationEnabledReasons.Contains(4);
		bool flag4 = !flag3 && m_CursorEmulationEnabledReasons.Contains(5);
		bool flag5 = !flag4 && m_CursorEmulationEnabledReasons.Contains(3);
		bool flag6 = !flag5 && m_CursorEmulationEnabledReasons.Contains(1);
		bool flag7 = !flag6 && m_CursorEmulationEnabledReasons.Contains(0);
		bool flag8 = !flag7 && m_CursorEmulationEnabledReasons.Contains(6);
		if (num4 != (flag7 ? 1 : 0))
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag7, UIInputMode.Interaction);
		}
		if (num3 != (flag6 ? 1 : 0))
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag6, UIInputMode.ItemDragging);
		}
		if (num2 != (flag5 ? 1 : 0))
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag5, UIInputMode.UIInventoryPanel);
		}
		if (num != flag3)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag3, UIInputMode.UISkinsPalettePanel);
		}
		if (flag != flag4)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag4, UIInputMode.UISkinsPalettePanel);
		}
		if (flag2 != flag8)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, flag8, UIInputMode.WorldMap);
		}
		if (anySet != m_CursorEmulationEnabledReasons.AnySet && enable && resetPosition)
		{
			Rect cameraPixelRect = Singleton.Manager<ManUI>.inst.GetCameraPixelRect();
			Vector3 vector = new Vector2(m_DefaultEmulatedCursorPosition.x * cameraPixelRect.width, m_DefaultEmulatedCursorPosition.y * cameraPixelRect.height);
			SetEmulatedCursorPos(vector);
		}
		return;
		IL_0063:
		num5 = (m_CursorEmulationEnabledReasons.Contains(6) ? 1 : 0);
		goto IL_0072;
		IL_0051:
		num4 = (m_CursorEmulationEnabledReasons.Contains(0) ? 1 : 0);
		if (num4 == 0)
		{
			goto IL_0063;
		}
		num5 = 0;
		goto IL_0072;
	}

	private void DisableAllCursorEmulation()
	{
		m_CursorEmulationEnabledReasons.Clear();
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.Interaction);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.ItemDragging);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIInventoryPanel);
	}

	public Vector2 GetEmulatedCursorPos()
	{
		return m_EmulatedCursorScreenPosition;
	}

	public void SetEmulatedCursorPos(Vector2 screenPos)
	{
		m_EmulatedCursorScreenPosition = screenPos;
	}

	public void SetEmulatedCursorBounds(Rect cursorRect)
	{
		m_EmulatedCursorBounds = cursorRect;
		ClampEmulatedCursorToBounds();
	}

	public void ResetEmulatedCursorBounds()
	{
		m_EmulatedCursorBounds = null;
	}

	public bool CanPaintFromInventory(TankBlock block, IInventory<BlockTypes> inventory)
	{
		bool flag = true;
		if (inventory != null)
		{
			int quantity = inventory.GetQuantity(block.BlockType);
			flag = quantity == -1 || quantity > 0;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			flag = flag && Time.time > m_BlockPaintTimestamp + Globals.inst.MultiplayerBlockPaintingTimeout;
		}
		if (Singleton.Manager<ManBlockLimiter>.inst != null && !Singleton.Manager<ManBlockLimiter>.inst.AllowPlayerAttachBlock(block))
		{
			flag = false;
		}
		return flag;
	}

	public bool HandlePaintedBlock(TankBlock block, IInventory<BlockTypes> inventory)
	{
		bool result = true;
		if (inventory != null && inventory.GetQuantity(block.BlockType) != -1)
		{
			result = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? (inventory.HostConsumeItem(-1, block.BlockType) > 0) : (inventory.GetUnreservedQuantity(block.BlockType) > 0));
		}
		OnBlockPaintedEvent.Send(block);
		m_BlockPaintTimestamp = Time.time;
		return result;
	}

	public void DisableRadialMenu(bool disable)
	{
		m_RadialMenuDisabled = disable;
		d.Log("Disabling radial menu");
	}

	private static void StaticEventDispatchByObject<T>(Event me, bool down, bool clicked) where T : Component, MouseEventConsumer
	{
		T val = (Singleton.Manager<ManPointer>.inst.targetObject ? Singleton.Manager<ManPointer>.inst.targetObject.GetComponent<T>() : null);
		if (!val)
		{
			val = (Singleton.Manager<ManPointer>.inst.targetTank ? Singleton.Manager<ManPointer>.inst.targetTank.GetComponent<T>() : null);
		}
		if ((bool)val)
		{
			val.OnMouseEvent(me, down);
		}
	}

	private static void StaticEventDispatchByObjectCached<T>(Event me, bool down, bool clicked) where T : Component, MouseEventConsumer
	{
		T val;
		if (down)
		{
			val = (Singleton.Manager<ManPointer>.inst.targetObject ? Singleton.Manager<ManPointer>.inst.targetObject.GetComponent<T>() : null);
			if (!val)
			{
				val = (Singleton.Manager<ManPointer>.inst.targetTank ? Singleton.Manager<ManPointer>.inst.targetTank.GetComponent<T>() : null);
			}
			objectCache[typeof(T)] = val;
		}
		else
		{
			val = objectCache[typeof(T)] as T;
		}
		if ((bool)val)
		{
			val.OnMouseEvent(me, down);
		}
	}

	private int GetGlowHighlightColourID(HighlightVariation type)
	{
		int num = 1;
		return type switch
		{
			_ => num, 
		};
	}

	private void UpdateTargetsForCursorPosition(Vector3 cursorPosition)
	{
		int layerMask = ((m_BuildingMode == BuildingMode.Placing) ? Globals.inst.layerTerrain.mask : ObjectPickerMask);
		if (!IsInteractionBlocked && Physics.Raycast(Singleton.Manager<ManUI>.inst.ScreenPointToRay(cursorPosition), out var hitInfo, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
		{
			bool flag = hitInfo.transform.gameObject.IsTerrain();
			targetPosition = (flag ? hitInfo.point : hitInfo.collider.bounds.center);
			m_TargetValid = true;
			if (flag)
			{
				targetVisible = null;
				targetObject = null;
				targetTank = null;
				targetBlock = null;
			}
			else
			{
				targetVisible = Visible.FindVisibleUpwards(hitInfo.collider);
				targetObject = ((targetVisible != null) ? targetVisible.trans : hitInfo.collider.transform);
				targetTank = ((!(targetVisible != null)) ? null : (targetVisible.block.IsNotNull() ? targetVisible.block.tank : targetVisible.tank));
				targetBlock = ((targetVisible != null) ? targetVisible.block : null);
			}
		}
		else
		{
			m_TargetValid = false;
			SetTargetVisible(null);
		}
		bool flag2 = false;
		if (targetVisible != null)
		{
			bool flag3 = BuildMode == BuildingMode.PaintSkin || BuildMode == BuildingMode.PaintSkinTech;
			if (ItemIsGrabbable(targetVisible))
			{
				flag2 = true;
			}
			else if (flag3)
			{
				flag2 = targetVisible.block.IsNotNull() || targetVisible.tank.IsNotNull();
			}
			else if (targetVisible.block.IsNotNull() && targetVisible.block.IsAttached && targetVisible.block.IsInteractible)
			{
				OpenMenuEventConsumer component = targetVisible.block.GetComponent<OpenMenuEventConsumer>();
				flag2 = component != null && (component.CanOpenMenu(isRadial: false) || component.CanOpenMenu(isRadial: true));
			}
		}
		if (DraggingItem == null)
		{
			if (flag2)
			{
				bool flag4 = BuildMode == BuildingMode.PaintSkinTech;
				HighlightVisible((flag4 && targetTank.IsNotNull()) ? targetTank.visible : targetVisible);
			}
			else
			{
				HideHighlight();
			}
		}
	}

	private void UpdateTargetsFromNearby()
	{
		m_TimeSinceLastUpdate += Time.deltaTime;
		if (m_TimeSinceLastUpdate < 0.15f)
		{
			return;
		}
		m_TimeSinceLastUpdate = 0f;
		Visible visible = null;
		if ((bool)Singleton.playerTank)
		{
			Vector3 position = Singleton.playerTank.trans.position;
			int pickerMask = Globals.inst.layerTank.mask | Globals.inst.layerPickup.mask;
			float num = float.MaxValue;
			Rect cameraPixelRect = Singleton.Manager<ManUI>.inst.GetCameraPixelRect();
			foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(position, m_NearbyPickupRange, ManSpawn.kVisibleMaskBlocksAndChunks, includeTriggers: false, pickerMask))
			{
				if ((item.type == ObjectTypes.Block && item.block.IsAttached) || item.holderStack != null || (!m_AllowResourcePickupFromNearby && item.type == ObjectTypes.Chunk))
				{
					continue;
				}
				Vector3 centrePosition = item.centrePosition;
				Vector3 coord = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(centrePosition);
				Vector2 vector = coord.ToVector2XY();
				if (!(coord.z <= 0f) && cameraPixelRect.Contains(vector))
				{
					float sqrMagnitude = (cameraPixelRect.center - vector).sqrMagnitude;
					if (!(sqrMagnitude >= num) && ItemIsGrabbable(item))
					{
						num = sqrMagnitude;
						visible = item;
					}
				}
			}
		}
		SetTargetVisible(visible);
	}

	private void UpdateCursorProximityTarget(Vector3 cursorPosition)
	{
		Visible visible = null;
		if ((bool)Singleton.playerTank)
		{
			Rect cameraPixelRect = Singleton.Manager<ManUI>.inst.GetCameraPixelRect();
			float num = Globals.inst.m_CursorSelectBufferDistance * Mathf.Max(cameraPixelRect.width, cameraPixelRect.height);
			Vector2 vector = cursorPosition.ToVector2XY();
			Vector2 coord = vector + (cameraPixelRect.center - vector).normalized * num;
			Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(coord.ToVector3XY());
			Ray ray2 = Singleton.Manager<ManUI>.inst.ScreenPointToRay(cursorPosition);
			float f = Vector3.Angle(ray2.direction, ray.direction) * ((float)Math.PI / 180f);
			float num2 = (m_TargetValid ? Mathf.Max(Vector3.Distance(Singleton.cameraTrans.position, targetPosition), m_CurrentPickupRange) : m_CurrentPickupRange);
			float radius = Mathf.Tan(f) * num2;
			float num3 = num * num;
			int layerMask = Globals.inst.layerTank.mask | Globals.inst.layerPickup.mask;
			int num4 = 0;
			do
			{
				if (num4 >= s_RaycastHitResultBufferSize)
				{
					s_RaycastHitResultBufferSize *= 2;
					Array.Resize(ref s_RaycastHitResults, s_RaycastHitResultBufferSize);
				}
				num4 = Physics.SphereCastNonAlloc(ray2, radius, s_RaycastHitResults, num2, layerMask, QueryTriggerInteraction.Ignore);
			}
			while (num4 >= s_RaycastHitResultBufferSize);
			for (int i = 0; i < num4; i++)
			{
				RaycastHit raycastHit = s_RaycastHitResults[i];
				Visible visible2 = Singleton.Manager<ManVisible>.inst.FindVisible(raycastHit.collider);
				if (visible2 == null)
				{
					continue;
				}
				Vector3 centrePosition = visible2.centrePosition;
				Vector3 coord2 = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(centrePosition);
				if (!(coord2.z <= 0f) && cameraPixelRect.Contains(coord2.ToVector2XY()) && ItemIsGrabbable(visible2))
				{
					float num5 = Vector3.Distance(Singleton.cameraTrans.position, centrePosition);
					Vector3 position = ray2.origin + ray2.direction * num5;
					Vector3 position2 = raycastHit.collider.ClosestPoint(position);
					float sqrMagnitude = (vector - Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position2).ToVector2XY()).sqrMagnitude;
					if (!(sqrMagnitude >= num3))
					{
						num3 = sqrMagnitude;
						visible = visible2;
					}
				}
			}
		}
		SetTargetVisible(visible);
	}

	public void SetTargetVisible(Visible vis)
	{
		if (vis != targetVisible)
		{
			bool flag = vis != null;
			targetVisible = vis;
			targetObject = (flag ? vis.trans : null);
			targetTank = ((!flag) ? null : ((vis.type == ObjectTypes.Block) ? vis.block.tank : vis.tank));
			targetBlock = (flag ? vis.block : null);
			targetPosition = (flag ? vis.centrePosition : Vector3.zero);
			if (flag)
			{
				HighlightVisible(targetVisible);
			}
			else
			{
				HideHighlight();
			}
		}
	}

	private void HandleJoypadInputForGrabMode()
	{
		if (DraggingItem.IsNotNull())
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(54))
			{
				if (m_BuildingMode == BuildingMode.PaintBlock)
				{
					TryPlacePaintingBlock(allowAttach: true);
				}
				else if (m_BuildingMode == BuildingMode.Grab)
				{
					ReleaseDrag(allowPlace: true);
				}
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(11) && m_BuildingMode == BuildingMode.Grab)
			{
				ReleaseDrag();
			}
		}
		else if (m_BuildingMode == BuildingMode.Grab)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(54) || Singleton.Manager<ManInput>.inst.GetButtonDown(51))
			{
				if (ItemIsGrabbable(targetVisible))
				{
					TryGrabTarget(targetVisible);
				}
				else if (targetTank != null)
				{
					OpenMenuForTarget(ManInput.RadialInputController.Gamepad, allowRadialMenu: false);
				}
			}
		}
		else if (m_BuildingMode == BuildingMode.PaintSkin)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(76))
			{
				m_PaintingSkin = true;
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonUp(76))
			{
				m_PaintingSkin = false;
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(77))
			{
				ChangeBuildMode(BuildingMode.PaintSkinTech);
				PaintTech();
			}
		}
		else if (m_BuildingMode == BuildingMode.PaintSkinTech)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(77))
			{
				PaintTech();
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(76))
			{
				ChangeBuildMode(BuildingMode.PaintSkin);
				m_PaintingSkin = true;
			}
		}
	}

	private void HandleJoypadOnPlacementSubmit(PayloadUIEventData evt)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_BuildingMode == BuildingMode.Placing)
		{
			OnPlacingEvent.Send(paramA: true, m_TargetValid, paramC: false);
			evt.Use();
		}
	}

	private void HandleJoypadOnPlacementCancel(PayloadUIEventData evt)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_BuildingMode == BuildingMode.Placing)
		{
			OnPlacingEvent.Send(paramA: false, m_TargetValid, paramC: true);
			evt.Use();
		}
	}

	private void UpdateEmulatedCursorScreenPosition()
	{
		Vector2 vector = Vector2.zero;
		if (!Singleton.Manager<ManInput>.inst.GetButton(66))
		{
			vector = Singleton.Manager<ManInput>.inst.GetAxis2D(52, 53);
		}
		if (vector != Vector2.zero)
		{
			if (vector.sqrMagnitude > 1f)
			{
				vector.Normalize();
			}
			Rect cameraPixelRect = Singleton.Manager<ManUI>.inst.GetCameraPixelRect();
			m_EmulatedCursorScreenPosition += vector * Time.deltaTime * cameraPixelRect.height * Globals.inst.m_CurrentGamepadCursorSpeed;
			ClampEmulatedCursorToBounds();
		}
	}

	private void ClampEmulatedCursorToBounds()
	{
		Rect rect;
		if (m_EmulatedCursorBounds.HasValue)
		{
			rect = m_EmulatedCursorBounds.Value;
		}
		else
		{
			Rect cameraPixelRect = Singleton.Manager<ManUI>.inst.GetCameraPixelRect();
			rect = cameraPixelRect;
			if (Globals.inst.m_GamepadCursorDragBorderFraction != 0f)
			{
				float trim = Globals.inst.m_GamepadCursorDragBorderFraction * Mathf.Max(cameraPixelRect.width, cameraPixelRect.height);
				rect = cameraPixelRect.TrimFourSides(trim);
			}
		}
		m_EmulatedCursorScreenPosition = rect.Clamp(m_EmulatedCursorScreenPosition);
	}

	private bool OpenMenuForTarget(ManInput.RadialInputController radialInputController, bool allowRadialMenu)
	{
		bool flag = false;
		if (!Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking) && !RadialMenu.IsActiveMenuModal && !m_RadialMenuDisabled)
		{
			bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftAlt);
			bool num = radialInputController == ManInput.RadialInputController.Mouse && Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && flag2;
			TankBlock tankBlock = (targetVisible ? targetVisible.block : null);
			if (!num && tankBlock.IsNotNull() && tankBlock.IsAttached && tankBlock.IsInteractible && IsDragEnabled(DragDisableReason.HudMasked))
			{
				OpenMenuEventData radialMenuPair = new OpenMenuEventData
				{
					m_RadialInputController = radialInputController,
					m_TargetTankBlock = tankBlock,
					m_AllowRadialMenu = allowRadialMenu,
					m_AllowNonRadialMenu = (radialInputController == ManInput.RadialInputController.Mouse || !allowRadialMenu)
				};
				if (tankBlock.openMenuEventConsumer != null)
				{
					flag = tankBlock.openMenuEventConsumer.OnOpenMenuEvent(radialMenuPair);
				}
				if (!flag && tankBlock.IsAttached && (tankBlock.tank.visible.IsInteractible || tankBlock.visible.IsInteractible) && tankBlock.tank.TechOpenMenuEventConsumer != null)
				{
					flag = tankBlock.tank.TechOpenMenuEventConsumer.OnOpenMenuEvent(radialMenuPair);
				}
			}
			if (tankBlock != null)
			{
				tankBlock.OnTankMouseDownEvent();
			}
		}
		return flag;
	}

	private void TryCopyTargetStateForPainting()
	{
		if ((Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftAlt))) || IsInteractionBlocked || IsPaintingBlocked || !(targetVisible != null) || !(targetVisible.block != null) || !ItemIsGrabbable(targetVisible))
		{
			return;
		}
		BuildingMode buildingMode = m_BuildingMode;
		if ((uint)buildingMode > 1u)
		{
			_ = buildingMode - 3;
			_ = 1;
			return;
		}
		BlockTypes targetBlockType = (BlockTypes)targetVisible.ItemType;
		IInventory<BlockTypes> inventory = Singleton.Manager<ManPurchases>.inst.GetInventory();
		if (inventory != null && !inventory.IsAvailableToLocalPlayer(targetBlockType))
		{
			return;
		}
		if (!Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded())
		{
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: true, UIShopBlockSelect.ExpandReason.Button);
		}
		if (!Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded())
		{
			return;
		}
		UIPaletteBlockSelect uIPaletteBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
		if (uIPaletteBlockSelect != null)
		{
			TankBlock block = targetVisible.block;
			OrthoRotation targetRotation = block.cachedLocalRotation;
			int targetSkinIndex = block.GetSkinIndex();
			TankPreset.BlockSpec blockSerialState = TankPreset.BlockSpec.GetBlockConfigState(block);
			uIPaletteBlockSelect.TrySelectBlockTypeFilterAgnostic(targetBlockType, delegate
			{
				if (m_HasBlockToSpawn && m_BlockToSpawn == targetBlockType && DraggingItem != null && DraggingItem.block.IsNotNull() && DraggingItem.ItemType == (int)targetBlockType)
				{
					FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(targetBlockType);
					Singleton.Manager<ManCustomSkins>.inst.SetSelectedSkinForCorp(targetSkinIndex, corporation);
					Singleton.Manager<ManTechBuilder>.inst.SetPreferredBlockOrientation(targetRotation);
					m_IsPlayerChosenRotation = Singleton.Manager<ManTechBuilder>.inst.PlayerChosenRotation;
					m_PlacementPreferredOrientationIndex = Singleton.Manager<ManTechBuilder>.inst.PlacementPreferredOrientationIndex;
					m_BlockSerialState = blockSerialState;
					DraggingItem.block.SerializeToText(saving: false, m_BlockSerialState.Value, onTech: false);
				}
			});
		}
		else
		{
			d.LogError("Failed to get BlockPalette instance ??");
		}
	}

	private void UpdateMouseEvents()
	{
		if (GUIUtility.hotControl == 0)
		{
			CheckMouseEvent(0, Event.LMB, ref m_LMBDownPos);
			CheckMouseEvent(1, Event.RMB, ref m_RMBDownPos);
			CheckMouseEvent(2, Event.MMB, ref m_MMBDownPos);
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis > 0f)
			{
				MouseEvent.Send(Event.MWheel, paramB: true, paramC: false);
			}
			else if (axis < 0f)
			{
				MouseEvent.Send(Event.MWheel, paramB: false, paramC: false);
			}
		}
	}

	private void CheckMouseEvent(int button, Event eventType, ref Vector3 downPos)
	{
		if (Input.GetMouseButton(button))
		{
			if (downPos.x == -1f)
			{
				MouseEvent.Send(eventType, paramB: true, paramC: false);
				downPos = Input.mousePosition;
			}
		}
		else if (downPos.x != -1f)
		{
			MouseEvent.Send(eventType, paramB: false, Input.mousePosition == downPos);
			downPos.x = -1f;
		}
	}

	private bool TryGrabTarget(Visible target, bool force = false)
	{
		if (force || ItemIsGrabbable(target))
		{
			d.AssertFormat(DraggingItem == null, "ManPointer.TryGrabTarget - Grabbing target '{0}' while we're already holding an item! The current held item ('{1}') won't be released!", target.name, DraggingItem ? DraggingItem.name : string.Empty);
			if (DraggingItem != null && DraggingItem.type == ObjectTypes.Block)
			{
				DraggingItem.block.UnsubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
			}
			DraggingItem = target;
			EnableCursorEmulation(enable: true, CursorEmulationEnabledReason.DraggingItem);
			m_DraggingItemFrozen = false;
			m_DraggingItemGrabTimestamp = Time.time;
			if (DraggingItem.type == ObjectTypes.Block)
			{
				DraggingItem.block.SubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
			}
			Vector3 centrePosition = target.centrePosition;
			Vector3 vector = (IsCursorEmulationEnabled ? Singleton.Manager<ManUI>.inst.WorldToScreenPoint(centrePosition) : Input.mousePosition);
			m_DraggingItemScreenOffset = Vector3.zero;
			SetEmulatedCursorPos(vector);
			m_DragPlaneDistance = (centrePosition - Singleton.cameraTrans.position).magnitude;
			m_DraggingItemVelocity = Vector3.zero;
			DragEvent.Send(DraggingItem, DragAction.Grab, vector);
			if (DraggingItem.type != ObjectTypes.Block)
			{
				DraggingItem.EnablePhysics(enable: false);
			}
			else if (!DraggingItem.block.IsAttached)
			{
				DraggingItem.EnablePhysics(enable: false, disableWithTrigger: true);
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.AllowCollaboration && Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.IsBuildBeamOn && target.block.IsNotNull() && target.block.IsController && target.block.IsAttached && target.block.tank.blockman.blockCount == 1 && target.block.tank.beam.IsActive)
			{
				target.block.tank.beam.EnableBeam(enable: false, force: true);
			}
			return true;
		}
		return false;
	}

	public void SetGrabbedTarget(Visible visible)
	{
		d.LogError("Deprecated function SetGrabbedTarget created for stop-gap Gamepad building code - needs to be replaced with TryGrabTarget");
	}

	public bool CanSelectTargets()
	{
		bool result = true;
		if (m_DebugDisableNormalManPointer || (m_TargetingMode != TargetingMode.Mouse && m_BuildingMode == BuildingMode.Grab && Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded()))
		{
			result = false;
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.playerTank == null && !Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
		{
			result = false;
		}
		else if (Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.PreventCursorTargetSelection))
		{
			result = false;
		}
		return result;
	}

	public bool GetCanDetach(TankBlock targetBlock)
	{
		if (targetBlock == null)
		{
			return false;
		}
		if (!targetBlock.visible.IsGrabbable || !targetBlock.visible.CanBeSentToSCU)
		{
			return false;
		}
		if (!ItemIsGrabbable(targetBlock.visible))
		{
			return false;
		}
		if (targetBlock.IsAttached && targetBlock.IsController && (object)targetBlock.tank == Singleton.playerTank)
		{
			ModuleTechController moduleTechController = targetBlock.tank.control.AllControllers.Skip(1).FirstOrDefault();
			if (moduleTechController == null || !moduleTechController.HandlesPlayerInput)
			{
				return false;
			}
		}
		return true;
	}

	private void UpdateDrag()
	{
		d.Assert(DraggingItem);
		Vector3 vector = DragPositionOnScreen + m_DraggingItemScreenOffset;
		if (!DraggingItem.isActive)
		{
			ReleaseDrag();
			return;
		}
		DraggingItem.KeepAwake();
		if (Time.time - m_DraggingItemGrabTimestamp > 0.5f)
		{
			DraggingItem.SetHolder(null);
		}
		if (!m_DraggingItemFrozen)
		{
			UpdateTechFocus(vector);
			if ((bool)Singleton.Manager<ManTechBuilder>.inst.DraggingAnchor && Singleton.Manager<ManTechBuilder>.inst.DraggingAnchor.block.CanAnchorFreely)
			{
				if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
				{
					m_DragPlaneDistance = m_AnchorBlockMaxPlaceDistanceGamepad;
				}
				else
				{
					m_DragPlaneDistance = m_AnchorBlockMaxPlaceDistance;
				}
			}
			Vector3 vector2 = GetDragPosWorld(vector);
			DropTarget = CheckDropTarget(out var hitPos);
			if (DropTarget != null)
			{
				HideHighlight();
				m_DropHighlight.Highlight(DropTarget.myHolder.block.visible);
				vector2 = hitPos;
			}
			else
			{
				HighlightVisible(DraggingItem);
				m_DropHighlight.HideHighlight();
			}
			m_DraggingItemVelocity = Vector3.Lerp(m_DraggingItemVelocity, vector2 - DraggingItem.centrePosition, m_DraggingItemSpeedDamping);
			DraggingItem.centrePosition = vector2;
			if ((bool)DraggingItem.rbody)
			{
				DraggingItem.rbody.velocity = Vector3.zero;
				DraggingItem.rbody.angularVelocity = Vector3.zero;
			}
		}
		DragEvent.Send(DraggingItem, DragAction.Update, vector);
	}

	private void ReleaseDrag(bool allowPlace = false, bool applyVelocity = true)
	{
		if ((bool)DraggingItem)
		{
			if (DraggingItem != null && DraggingItem.type == ObjectTypes.Block)
			{
				DraggingItem.block.UnsubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
			}
			Visible draggingItem = DraggingItem;
			DragEvent.Send(DraggingItem, allowPlace ? DragAction.ReleaseAllowPlace : DragAction.ReleaseLoose, DragPositionOnScreen + m_DraggingItemScreenOffset);
			d.AssertFormat(DraggingItem == draggingItem, "ReleaseDrag - DragEvent.Send ({0}) caused DraggingItem to change! Post release cleanup won't be processed correctly!", allowPlace ? DragAction.ReleaseAllowPlace : DragAction.ReleaseLoose);
			if (DraggingItem != null)
			{
				bool flag = DraggingItem.type == ObjectTypes.Block;
				bool flag2 = flag && DraggingItem.block.IsAttached;
				bool flag3 = flag2 && DraggingItem.block.tank.blockman.blockCount == 1;
				bool flag4 = flag2 && DraggingItem.block.tank.IsAnchored;
				if (flag && flag3)
				{
					DraggingItem.block.tank.visible.MoveAboveGround();
				}
				DragEvent.Send(DraggingItem, DragAction.PostRelease, DraggingItem.centrePosition);
				DraggingItem.EnablePhysics(enable: true);
				if (DraggingItem.isActive)
				{
					if (applyVelocity && DropTarget == null && (!flag || !flag2 || (flag3 && !flag4)))
					{
						float num = m_DraggingItemReleaseSpeedMul * (Mode<ModeMain>.inst.ReduceBlockDragReleaseSpeed ? 0.1f : 1f);
						Rigidbody rigidbody = (flag2 ? DraggingItem.block.tank.rbody : DraggingItem.rbody);
						if ((bool)rigidbody)
						{
							int layerMask = Globals.inst.layerTerrain.mask | Globals.inst.layerScenery.mask | Globals.inst.layerLandmark.mask;
							if (Physics.CheckSphere(DraggingItem.centrePosition, DraggingItem.Radius * 0.7f, layerMask, QueryTriggerInteraction.Ignore))
							{
								DraggingItem.rbody.AddRandomVelocity(Vector3.up * 10f, Vector3.zero, 10f);
							}
							else
							{
								rigidbody.velocity = m_DraggingItemVelocity * num;
								rigidbody.angularVelocity = m_DraggingItemVelocity.magnitude * m_DraggingItemReleaseSpinMul * UnityEngine.Random.onUnitSphere;
							}
						}
						HideHighlight();
					}
					if (allowPlace && DropTarget != null && DropTarget != DraggingItem.holderStack && !flag2)
					{
						if ((bool)DropTarget.Take(DraggingItem))
						{
							TryShowCraftingInfoForHolder(DropTarget.myHolder);
							ModuleItemHolder myHolder = DropTarget.myHolder;
							myHolder.block.tank.Holders.SetItemPickupTime(DraggingItem.ID, Time.time - 1f - myHolder.PickupContentionPeriod);
						}
						m_DropHighlight.HideHighlight();
					}
				}
				if (flag)
				{
					DraggingItem.block.UnsubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
					DraggingItem.block.RevertCustomMaterialOverride();
				}
				DraggingItem = null;
				EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.DraggingItem);
			}
		}
		DropTarget = null;
		DraggingFocusTech = null;
	}

	private void TryShowCraftingInfoForHolder(ModuleItemHolder holder)
	{
	}

	private void UpdateTechFocus(Vector3 screenPos)
	{
		Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(screenPos);
		Tank draggingFocusTech = DraggingFocusTech;
		DraggingFocusTech = null;
		if (DraggingItem.IsNotNull() && DraggingItem.type == ObjectTypes.Block && targetBlock.IsNotNull() && targetBlock.IsAttached)
		{
			DraggingFocusTech = targetBlock.tank;
		}
		else
		{
			float num = float.MaxValue;
			foreach (RaycastHit item in PhysicsUtils.RaycastAllNonAlloc(ray, m_CurrentPickupRange, Globals.inst.layerTank.mask, QueryTriggerInteraction.Ignore))
			{
				float distance = item.distance;
				if (distance < num && item.rigidbody != null)
				{
					Tank component = item.rigidbody.GetComponent<Tank>();
					if ((bool)component)
					{
						DraggingFocusTech = component;
						num = distance;
					}
				}
			}
		}
		if (!DraggingFocusTech)
		{
			float num = float.MaxValue;
			foreach (RaycastHit item2 in PhysicsUtils.RaycastAllNonAlloc(ray, m_CurrentPickupRange, Globals.inst.layerCosmetic.mask, QueryTriggerInteraction.Collide))
			{
				float distance2 = item2.distance;
				if (distance2 < num && (bool)item2.rigidbody)
				{
					Tank component2 = item2.rigidbody.GetComponent<Tank>();
					if ((bool)component2)
					{
						DraggingFocusTech = component2;
						num = distance2;
					}
				}
			}
		}
		if ((bool)DraggingFocusTech)
		{
			m_DragPlaneDistance = (DraggingFocusTech.dragSphere.transform.position - Singleton.cameraTrans.position).magnitude;
		}
		if (!DraggingFocusTech && (bool)draggingFocusTech && draggingFocusTech.visible.isActive && ray.ClosestDistance(draggingFocusTech.boundsCentreWorld) - draggingFocusTech.dragSphere.radius < m_DraggingRetainTechFocusRadius)
		{
			DraggingFocusTech = draggingFocusTech;
			m_DragPlaneDistance = (DraggingFocusTech.dragSphere.transform.position - Singleton.cameraTrans.position).magnitude;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab && DraggingFocusTech.IsNotNull() && (Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition - DraggingFocusTech.boundsCentreWorld).ToVector2XZ().magnitude > Singleton.Manager<ManNetwork>.inst.DangerDistance)
		{
			DraggingFocusTech = null;
		}
	}

	private Vector3 GetSafeDragPosMultiplayer(Ray cameraRay, Vector3 targetPos)
	{
		Vector3 coord = targetPos - Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition;
		if (coord.ToVector2XZ().magnitude > Singleton.Manager<ManNetwork>.inst.DangerDistance)
		{
			Vector3 vector = cameraRay.direction.SetY(0f);
			if (vector.magnitude > 0.01f && Maths.IntersectRayWithSphere(cameraRay.origin.SetY(0f), vector.normalized, float.MaxValue, Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition.SetY(0f), Singleton.Manager<ManNetwork>.inst.DangerDistance, out var intersectDist))
			{
				return cameraRay.origin + cameraRay.direction * (intersectDist / vector.magnitude);
			}
			return (Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition + coord.SetY(0f).normalized * Singleton.Manager<ManNetwork>.inst.DangerDistance).SetY(targetPos.y);
		}
		return targetPos;
	}

	private Vector3 GetDragPosWorld(Vector3 screenPos)
	{
		Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(screenPos);
		if (Singleton.Manager<ManWorld>.inst.RaycastGround(ray, out var hit, m_DragPlaneDistance, Globals.inst.layerTerrain.mask | Globals.inst.layerScenery.mask))
		{
			m_DragPlaneDistance = hit.distance;
		}
		Vector3 vector = ray.origin + ray.direction * m_DragPlaneDistance;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
		{
			return GetSafeDragPosMultiplayer(ray, vector);
		}
		return vector;
	}

	private void StartCameraSpin()
	{
		if (Globals.inst.m_RuntimeCameraSpinSensHorizontal != 0f && Singleton.Manager<ManUI>.inst.IsStackEmpty() && Singleton.Manager<ManStartup>.inst.GameStarted)
		{
			Singleton.Manager<CameraManager>.inst.BeginSpinControl();
		}
	}

	private void StopCameraSpin()
	{
		Singleton.Manager<CameraManager>.inst.EndSpinControl();
	}

	private ModuleItemHolder.Stack CheckDropTarget(out Vector3 hitPos)
	{
		hitPos = Vector3.zero;
		if (Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
		{
			return null;
		}
		if (!targetVisible || !targetTank || targetTank.IsEnemy(Singleton.Manager<ManPlayer>.inst.PlayerTeam))
		{
			return null;
		}
		if (!DraggingItem.CanHoldInStack)
		{
			return null;
		}
		ModuleItemHolder component = targetVisible.block.GetComponent<ModuleItemHolder>();
		if (!component)
		{
			return null;
		}
		Ray ray = Singleton.Manager<ManUI>.inst.ScreenPointToRay(DragPositionOnScreen);
		if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, ObjectPickerMask, QueryTriggerInteraction.Ignore))
		{
			return null;
		}
		if (targetVisible.transform.up.Dot(hitInfo.normal) <= 0f)
		{
			return null;
		}
		ModuleItemHolder.Stack nearestStack = component.GetNearestStack(hitInfo.point);
		if (nearestStack == null || !nearestStack.CanAccept(DraggingItem, null, ModuleItemHolder.PassType.Drop | ModuleItemHolder.PassType.Test))
		{
			return null;
		}
		if (!Singleton.Manager<ManBlockLimiter>.inst.AllowPickupBy(targetVisible.block, component.Acceptance))
		{
			return null;
		}
		hitPos = hitInfo.point - ray.direction * 1f;
		return nearestStack;
	}

	private bool TryDetachTarget()
	{
		if (!GetCanDetach(targetBlock))
		{
			return false;
		}
		Vector3 zero = Vector3.zero;
		int num = 0;
		if (targetBlock.IsAttached)
		{
			for (int i = 0; i < targetBlock.attachPoints.Length; i++)
			{
				if (targetBlock.ConnectedBlocksByAP[i] != null)
				{
					zero += targetBlock.attachPoints[i].normalized;
					num++;
				}
			}
		}
		BlockTypes blockType = targetBlock.BlockType;
		Vector3 centreOfMassWorld = targetBlock.centreOfMassWorld;
		InventoryMetaData referenceInventory = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory();
		int num2;
		if (m_BlockDetachStraightToInventory)
		{
			num2 = ((!referenceInventory.IsLocked) ? 1 : 0);
			if (num2 != 0)
			{
				Singleton.Manager<ManLooseBlocks>.inst.RequestDespawnBlock(targetBlock, DespawnReason.ReturnToInventory);
				goto IL_00e5;
			}
		}
		else
		{
			num2 = 0;
		}
		if (targetBlock.IsAttached)
		{
			Singleton.Manager<ManTechBuilder>.inst.DetachBlock(targetBlock);
		}
		goto IL_00e5;
		IL_00e5:
		if (num2 != 0)
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManPlayer>.inst.AddBlockToInventory(blockType);
			}
			if (m_RemoveBlockToInventoryVFX != null)
			{
				m_RemoveBlockToInventoryVFX.Spawn(centreOfMassWorld);
			}
		}
		else if (!targetBlock.IsAttached && targetBlock.rbody.IsNotNull())
		{
			Vector3 position = targetBlock.visible.centrePosition + UnityEngine.Random.insideUnitSphere;
			Vector3 force = ((num == 0) ? (Vector3.up * 0.5f) : (-(zero / num) + Vector3.up * 0.5f)) * UnityEngine.Random.Range(0.7f, 1f) * 0.8f;
			targetBlock.rbody.AddForceAtPosition(force, position, ForceMode.Acceleration);
		}
		return true;
	}

	private void OnMouse(Event mouseEvent, bool touchDown, bool clicked)
	{
		switch (mouseEvent)
		{
		case Event.LMB:
			if (touchDown)
			{
				if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
				{
					break;
				}
				switch (m_BuildingMode)
				{
				case BuildingMode.BlockDetach:
					if (!IsInteractionBlocked)
					{
						TryDetachTarget();
					}
					break;
				case BuildingMode.Grab:
					if (!IsInteractionBlocked)
					{
						TryGrabTarget(targetVisible);
					}
					break;
				case BuildingMode.PaintBlock:
					if (!IsPaintingBlocked)
					{
						if (DraggingItem != null)
						{
							TryPlacePaintingBlock(allowAttach: true);
							break;
						}
						d.LogWarning("ManPointer.OnMouseLMBDown - Trying to release block while in PaintBlock mode, but not currently holding a block!? Falling back to Grabbing a block!");
						m_PaintButGrab = TryGrabTarget(targetVisible);
					}
					break;
				case BuildingMode.Placing:
					OnPlacingEvent.Send(paramA: true, m_TargetValid, paramC: false);
					break;
				case BuildingMode.PaintSkin:
					m_PaintingSkin = true;
					break;
				case BuildingMode.PaintSkinTech:
					break;
				}
				break;
			}
			switch (m_BuildingMode)
			{
			case BuildingMode.Grab:
				ReleaseDrag(allowPlace: true);
				break;
			case BuildingMode.PaintBlock:
				if (m_PaintButGrab)
				{
					ReleaseDrag(allowPlace: true);
					m_PaintButGrab = false;
				}
				break;
			case BuildingMode.PaintSkin:
				if (m_PaintingSkin)
				{
					m_PaintingSkin = false;
				}
				break;
			case BuildingMode.PaintSkinTech:
				PaintTech();
				break;
			case BuildingMode.Placing:
				break;
			}
			break;
		case Event.RMB:
			if (touchDown)
			{
				StartCameraSpin();
			}
			else
			{
				StopCameraSpin();
			}
			break;
		}
	}

	private void Init()
	{
		m_Highlight = UnityEngine.Object.Instantiate(m_HighlightPrefab);
		m_Highlight.gameObject.SetActive(value: false);
		m_DropHighlight = UnityEngine.Object.Instantiate(m_HighlightPrefab);
		m_DropHighlight.gameObject.SetActive(value: false);
		m_ChunkHighlight = UnityEngine.Object.Instantiate(m_ChunkHighlightPrefab);
		m_ChunkHighlight.SetActive(value: false);
		ResetPickupRange();
	}

	private void OnModeCleanup(Mode modeToCleanup)
	{
		ChangeBuildMode(BuildingMode.Grab);
		m_LastPaintingBlockVisID = -1;
		m_DragDisabledReason.Clear();
		m_PreventPaintingMaskMouse.Clear();
		m_PreventInteractionMaskMouse.Clear();
		m_PreventPaintingMaskGamepad.Clear();
		m_PreventInteractionMaskGamepad.Clear();
		m_IsInteractionModeLocked = false;
		m_RadialMenuDisabled = false;
		ResetEmulatedCursorBounds();
		DisableAllCursorEmulation();
	}

	private void OnActiveControllerTypeChanged(ControllerType activeType)
	{
		bool isInteractionBlocked = IsInteractionBlocked;
		bool isPaintingBlocked = IsPaintingBlocked;
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && activeType == ControllerType.Joystick)
		{
			if (m_TargetingMode == TargetingMode.Mouse && IsCursorEmulationEnabled)
			{
				SetEmulatedCursorPos(Input.mousePosition);
			}
			m_TargetingMode = (IsCursorEmulationEnabled ? TargetingMode.Joypad : TargetingMode.Nearby);
			m_HighlightMethod = HighlightMethod.Glow;
		}
		else
		{
			m_TargetingMode = TargetingMode.Mouse;
			m_HighlightMethod = HighlightMethod.Box;
		}
		UpdatePreventInteraction(isInteractionBlocked);
		UpdatePreventPainting(isPaintingBlocked);
	}

	private void OnHighlightedVisibleRecycled(Visible vis)
	{
		if (vis == m_HighlightedVisible)
		{
			HideHighlight();
		}
	}

	private void PaintTech()
	{
		if (targetTank.IsNotNull())
		{
			Singleton.Manager<ManCustomSkins>.inst.FloodFillTech(targetTank);
		}
	}

	private void Awake()
	{
		MouseEvent.Subscribe(OnMouse);
	}

	private void Start()
	{
		Singleton.Manager<ManStartup>.inst.DoOnceAfterComponentPoolInitialised(Init);
		RegisterStaticMouseEvents<TankControl>(grabUpEvent: false);
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		Singleton.Manager<ManInput>.inst.LastActiveControllerTypeChangedEvent.Subscribe(OnActiveControllerTypeChanged);
	}

	private void Update()
	{
		if (Singleton.Manager<ManGameMode>.inst.LockPlayerControls)
		{
			StopCameraSpin();
		}
		if (!Singleton.camera.IsNotNull() || (!SKU.ConsoleUI && !Singleton.Manager<ManUI>.inst.IsStackEmpty() && (!Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) || !Input.GetKey(KeyCode.LeftControl))))
		{
			SetTargetVisible(null);
			return;
		}
		bool flag = Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying();
		if (!flag)
		{
			if (m_TargetingMode == TargetingMode.Joypad)
			{
				UpdateEmulatedCursorScreenPosition();
			}
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				m_TargetingMode = (IsCursorEmulationEnabled ? TargetingMode.Joypad : TargetingMode.Nearby);
			}
		}
		bool num = CanSelectTargets();
		if (num && !flag)
		{
			if (m_TargetingMode == TargetingMode.Nearby)
			{
				if (DraggingItem.IsNull())
				{
					UpdateTargetsFromNearby();
				}
			}
			else if (DraggingItem.IsNull() || DraggingItem.type != ObjectTypes.Block || !DraggingItem.block.IsAttached)
			{
				UpdateTargetsForCursorPosition(DragPositionOnScreen);
				if (m_BuildingMode == BuildingMode.Grab && m_TargetingMode == TargetingMode.Joypad && targetVisible.IsNull())
				{
					UpdateCursorProximityTarget(DragPositionOnScreen);
				}
			}
			bool flag2 = m_BuildingMode == BuildingMode.Grab && DraggingItem.IsNull();
			if (!SKU.ConsoleUI && Input.GetMouseButtonDown(1))
			{
				OpenMenuForTarget(ManInput.RadialInputController.Mouse, allowRadialMenu: true);
			}
			else if (Singleton.Manager<ManInput>.inst.GetButtonDown(56))
			{
				if ((!flag2 || !OpenMenuForTarget(ManInput.RadialInputController.Gamepad, allowRadialMenu: false)) && Singleton.playerTank != null)
				{
					Singleton.Manager<ManPurchases>.inst.TogglePalette();
				}
			}
			else if (flag2 && Singleton.Manager<ManInput>.inst.GetButtonDown(59))
			{
				OpenMenuForTarget(ManInput.RadialInputController.Gamepad, allowRadialMenu: true);
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(98))
			{
				TryCopyTargetStateForPainting();
			}
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				HandleJoypadInputForGrabMode();
			}
		}
		if (MouseEvent.HasSubscribers() && (!Singleton.Manager<CameraManager>.inst.IsCurrent<FirstPersonFlyCam>() || Singleton.Manager<CameraManager>.inst.GetCamera<FirstPersonFlyCam>().IsLocked))
		{
			UpdateMouseEvents();
		}
		if (num)
		{
			if (DraggingItem.IsNotNull())
			{
				UpdateDrag();
			}
			if ((m_BuildingMode == BuildingMode.Grab || m_BuildingMode == BuildingMode.PaintBlock) && Singleton.Manager<ManInput>.inst.GetButton(121))
			{
				UIPaletteBlockSelect uIPaletteBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
				if (uIPaletteBlockSelect != null && uIPaletteBlockSelect.IsExpanded && m_HasBlockToSpawn)
				{
					uIPaletteBlockSelect.CacheAndClearSelection(UIPaletteBlockSelect.ClearSelectionReason.RemoveMode);
				}
				ChangeBuildMode(BuildingMode.BlockDetach);
			}
			if (m_BuildingMode == BuildingMode.BlockDetach && !Singleton.Manager<ManInput>.inst.GetButton(121))
			{
				UIPaletteBlockSelect uIPaletteBlockSelect2 = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
				if (uIPaletteBlockSelect2 != null && uIPaletteBlockSelect2.IsExpanded && uIPaletteBlockSelect2.TryRestoreCachedSelection(UIPaletteBlockSelect.ClearSelectionReason.RemoveMode))
				{
					ChangeBuildMode(BuildingMode.PaintBlock);
				}
				else
				{
					ChangeBuildMode(BuildingMode.Grab);
				}
			}
			if (m_BuildingMode == BuildingMode.Placing)
			{
				if (Singleton.Manager<ManInput>.inst.GetButtonRepeating(118))
				{
					m_Placement.AddRotationIncrement();
				}
				else if (Singleton.Manager<ManInput>.inst.GetNegativeButtonRepeating(118))
				{
					m_Placement.AddRotationIncrement(clockwise: false);
				}
				OnPlacingEvent.Send(paramA: false, m_TargetValid, paramC: false);
			}
			if (m_PaintingSkin && targetVisible.IsNotNull() && targetVisible.type == ObjectTypes.Block)
			{
				Singleton.Manager<ManCustomSkins>.inst.TryPaintBlock(targetVisible.block);
			}
		}
		else
		{
			SetTargetVisible(null);
		}
	}

	private void LateUpdate()
	{
		if (IsEmulatedCursorVisible && m_TargetingMode == TargetingMode.Joypad && !Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying() && !Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.GamepadCursorHidingElements))
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.InteractionMode);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.InteractionMode);
		}
	}

	public void DebugSpawnBlock(BlockTypes blockType)
	{
		if ((bool)DraggingFocusTech)
		{
			m_DragPlaneDistance = (DraggingFocusTech.dragSphere.transform.position - Singleton.cameraTrans.position).magnitude;
		}
		else
		{
			m_DragPlaneDistance = m_DefaultDragPlaneDistance;
		}
		Vector3 screenPos = DragPositionOnScreen + m_DraggingItemScreenOffset;
		Vector3 dragPosWorld = GetDragPosWorld(screenPos);
		Singleton.Manager<ManLooseBlocks>.inst.RequestDebugSpawnItem(ObjectTypes.Block, (int)blockType, dragPosWorld, Quaternion.identity);
	}

	public void TrySpawnPaintingBlock(BlockTypes blockType, bool force = false)
	{
		m_HasBlockToSpawn = true;
		m_BlockToSpawn = blockType;
		Visible visible = TrySpawnPaintingBlock();
		if (force && DraggingItem == null && visible != null)
		{
			SetGrabbedTarget(visible);
		}
	}

	private Visible TrySpawnPaintingBlock()
	{
		if (DraggingItem == null && m_HasBlockToSpawn && (!IsPaintingBlocked || m_DebugDisableNormalManPointer))
		{
			if ((bool)DraggingFocusTech)
			{
				m_DragPlaneDistance = (DraggingFocusTech.dragSphere.transform.position - Singleton.cameraTrans.position).magnitude;
			}
			else
			{
				m_DragPlaneDistance = m_DefaultDragPlaneDistance;
			}
			Vector3 screenPos = DragPositionOnScreen + m_DraggingItemScreenOffset;
			Vector3 dragPosWorld = GetDragPosWorld(screenPos);
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManSaveGame>.inst.CurrentState.GetCurrentHighestVisibleID(ObjectTypes.Block) == m_LastPaintingBlockVisID)
			{
				Singleton.Manager<ManSaveGame>.inst.CurrentState.OverrideNextVisibleID(m_LastPaintingBlockVisID);
			}
			m_LastPaintingBlockVisID = -1;
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.RequestSpawnPaintingBlock(m_BlockToSpawn, dragPosWorld, Quaternion.identity);
			Singleton.Manager<ManSaveGame>.inst.CurrentState.EnsureNextVisibleIDOverrideIsCleared();
			if (tankBlock.IsNotNull())
			{
				Singleton.Manager<ManCustomSkins>.inst.DoPaintBlock(tankBlock);
				if (m_BlockSerialState.HasValue)
				{
					tankBlock.SerializeToText(saving: false, m_BlockSerialState.Value, onTech: false);
				}
				Visible visible = tankBlock.visible;
				visible.centrePosition = dragPosWorld;
				if (TryGrabTarget(visible, force: true))
				{
					tankBlock.SetCustomMaterialOverride(ManTechMaterialSwap.MatType.Alpha);
					Singleton.Manager<ManTechBuilder>.inst.PlayerChosenRotation = m_IsPlayerChosenRotation;
					Singleton.Manager<ManTechBuilder>.inst.PlacementPreferredOrientationIndex = m_PlacementPreferredOrientationIndex;
				}
				return visible;
			}
		}
		return null;
	}

	private void TryPlacePaintingBlock(bool allowAttach)
	{
		if (!(DraggingItem != null))
		{
			return;
		}
		TankBlock block = DraggingItem.block;
		if (!CanPaintFromInventory(block, Singleton.Manager<ManPurchases>.inst.GetInventory()))
		{
			return;
		}
		m_IsPlayerChosenRotation = Singleton.Manager<ManTechBuilder>.inst.PlayerChosenRotation;
		m_PlacementPreferredOrientationIndex = Singleton.Manager<ManTechBuilder>.inst.PlacementPreferredOrientationIndex;
		ReleaseDrag(allowAttach);
		if (DraggingItem == null)
		{
			if (!HandlePaintedBlock(block, Singleton.Manager<ManPurchases>.inst.GetInventory()))
			{
				ChangeBuildMode(BuildingMode.Grab);
			}
			else
			{
				TrySpawnPaintingBlock();
			}
		}
		else
		{
			d.LogError("Failed to ReleaseDrag on Painting block!");
		}
	}

	public void RemovePaintingBlock()
	{
		if ((bool)DraggingItem && (bool)DraggingItem.block)
		{
			d.AssertFormat(m_BuildingMode == BuildingMode.PaintBlock, "Removing 'Painting Block' while BuildingMode is {0} (Expected PaintBlock). This will likely end up destroying a (valid) picked up block from the world!", m_BuildingMode);
			DraggingItem.block.UnsubscribeTriggerStay(Singleton.Manager<ManTechBuilder>.inst.PlacementTriggerCallback);
			Singleton.Manager<ManLooseBlocks>.inst.RequestDespawnBlock(DraggingItem.block, DespawnReason.PaintingBlock);
			m_LastPaintingBlockVisID = DraggingItem.ID;
			DraggingItem = null;
			EnableCursorEmulation(enable: false, CursorEmulationEnabledReason.DraggingItem);
		}
	}

	private void OnBlockSelect(BlockTypes blockToSpawn, int quantity)
	{
		m_BlockSerialState = null;
		if (m_BlockToSpawn != blockToSpawn || !m_HasBlockToSpawn)
		{
			m_HasBlockToSpawn = quantity > 0 || quantity == -1;
			if (m_HasBlockToSpawn)
			{
				m_HasBlockToSpawn = true;
				m_BlockToSpawn = blockToSpawn;
				if (m_BuildingMode == BuildingMode.PaintBlock)
				{
					RemovePaintingBlock();
					TrySpawnPaintingBlock();
				}
				else
				{
					ChangeBuildMode(BuildingMode.PaintBlock);
				}
				m_IsPlayerChosenRotation = false;
				Singleton.Manager<ManTechBuilder>.inst.PlayerChosenRotation = false;
				m_PlacementPreferredOrientationIndex = 0;
				Singleton.Manager<ManTechBuilder>.inst.PlacementPreferredOrientationIndex = 0;
				Singleton.Manager<ManTechBuilder>.inst.ResetAPCollection();
			}
		}
		else if (m_BuildingMode == BuildingMode.PaintBlock)
		{
			ChangeBuildMode(BuildingMode.Grab);
		}
		else
		{
			d.LogError("OnBlockSelect called when not in painting mode");
		}
	}

	private void OnNoBlockSelected()
	{
		m_BlockSerialState = null;
		if (m_BuildingMode == BuildingMode.PaintBlock)
		{
			RemovePaintingBlock();
			ChangeBuildMode(BuildingMode.Grab);
		}
		m_HasBlockToSpawn = false;
	}

	public void ClearHasBlockToSpawnFlag()
	{
		m_HasBlockToSpawn = false;
		m_BlockSerialState = null;
	}

	public void RegisterBlockPaintingCallbacks(UIPaletteBlockSelect blockSelect)
	{
		blockSelect.RegisterBlockSelectCallback(OnBlockSelect, OnNoBlockSelected);
	}

	public void UnregisterBlockPaintingCallbacks(UIPaletteBlockSelect blockSelect)
	{
		blockSelect.UnregisterBlockSelectCallback(OnBlockSelect, OnNoBlockSelected);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
		if (targetObject != null)
		{
			Bounds bounds = new Bounds(Vector3.zero, -Vector3.one);
			Collider[] componentsInChildren = targetObject.GetComponentsInChildren<Collider>();
			foreach (Collider collider in componentsInChildren)
			{
				if (bounds.size == -Vector3.one)
				{
					bounds = collider.bounds;
				}
				else
				{
					bounds.Encapsulate(collider.bounds);
				}
			}
			if (bounds.size != -Vector3.one)
			{
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
		}
		else
		{
			Gizmos.DrawWireSphere(targetPosition, 0.5f);
		}
	}
}
