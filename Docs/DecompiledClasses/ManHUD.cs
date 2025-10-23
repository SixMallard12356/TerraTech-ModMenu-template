#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ManHUD : Singleton.Manager<ManHUD>
{
	public struct HUDElementVisibilityChange
	{
		public HUDElementType elementType;

		public HUDElementState state;

		public object context;

		public HUDElementVisibilityChange(HUDElementType inpElementType, HUDElementState inpState, object inpContext)
		{
			elementType = inpElementType;
			state = inpState;
			context = inpContext;
		}
	}

	public class RadialOpenRequest
	{
		public HUDElementType elementType;

		public Vector2 mousePos;

		public OpenMenuEventData radialInput;

		public float timePressed;
	}

	public enum HUDType
	{
		None,
		MainGame,
		Sumo
	}

	public enum HUDGroup
	{
		Main,
		GamepadCursorHidingElements,
		ContextMenuBlocking,
		RightHandSide,
		GamepadQuickMenuHUDElements,
		PreventCursorTargetSelection
	}

	public enum GroupRules
	{
		Grouped,
		ExclusiveVisibility,
		Locked
	}

	public enum HUDElementType
	{
		Radar,
		ModeScore,
		ModeTimer,
		Announcement,
		MoneyCounter,
		CheckpointChallenge,
		FlyingChallenge,
		RaDTestChamber,
		Snapshot,
		ResetPosition,
		TweetThis,
		GetBeta,
		TwitchButtonTT,
		TwitchButtonOther,
		TwitchStreamList,
		InfoBox,
		RestartTutorial,
		UndoButton,
		NetworkPlayerList,
		Corporation,
		Speedo,
		Altimeter,
		BlockMenuSelection,
		StartChallenge,
		TechSavedMessage,
		BouncingArrow,
		PrintEnabledIcon,
		LaunchTutorialButton,
		TechLoaderButton,
		TechLoader,
		TechControlChoice,
		TechControlChoiceSetAITarget,
		GrabItIcon,
		LicenceLevelUp,
		LicenceMaxedNotification,
		HUDMissionTracker,
		FactionLicences,
		BlockPalette,
		BlockShop,
		TechShop,
		MissionLog,
		BlockRecipeSelect,
		HUDMask,
		FilterMenu,
		GrabItNotification,
		Hint,
		MissionBoard,
		ComponentRecipeSelect,
		HintFloating,
		Sumo,
		Pacemaker,
		AnchorTech,
		FuelGauge,
		PowerGauge,
		Multiplayer,
		OutOfBounds,
		Score,
		ScoreBoard,
		MPTimeRemaining,
		MPKillStreakClaimReward,
		MPKillStreakClaimRewardBeingClaimed,
		SelfDestruct,
		InteractionMode,
		TechAndBlockActions,
		MPChat,
		ButtonPrompt,
		BlockLimit,
		MPTechActions,
		ConveyorMenu,
		_deprecated_HoverControl,
		BlockControl,
		GyroControl,
		TrimControl,
		Throttle,
		ControlSchema,
		SkinsPaletteButton,
		SkinsPalette,
		TechManager,
		CoopPlayerInfo,
		PlayerInfo,
		GamepadQuickMenu,
		TechManagerButton,
		BlockLimiterWarning,
		VoiceIndicator,
		TeleportMenu,
		MPMissionUpdates,
		ReturnToTeleporter,
		BlockControlOnOff,
		TechControlChoice_RadarMarker,
		TechAndBlockActions_RadarMarker,
		_deprecated_MassControl,
		WorldMap,
		WorldMapButton,
		_deprecated_CircuitWiFiFrequencyControl,
		_deprecated_CircuitAccumulatorControl,
		_deprecated_CircuitAmplifierControl,
		SliderControlRadialMenu,
		PowerToggleBlockMenu,
		BlockOptionsContextMenu,
		CircuitsNSystemsDebugger,
		SimpleOnOffRadial
	}

	public enum HUDElementState
	{
		Show,
		Hide
	}

	public enum HideReason
	{
		Paused,
		Cheats,
		NotInGame,
		PlayerHideHud
	}

	public enum FocusLevel
	{
		Dimmed,
		Default,
		Highlighted
	}

	public interface Focussable
	{
		void SetFocusLevel(FocusLevel level);

		Transform GetTransform();
	}

	[SerializeField]
	private RectTransform m_MainHudPanel;

	[SerializeField]
	private RectTransform m_OverlayHudPanel;

	[SerializeField]
	private RectTransform m_TooltipPanel;

	[EnumArray(typeof(HUDType))]
	[SerializeField]
	private UIHUD[] m_HudPresets = new UIHUD[0];

	[SerializeField]
	private float m_RadialShowDelay = 0.5f;

	[SerializeField]
	private int m_RadialMouseDistanceThreshold = 50;

	public Event<UIHUDElement> OnShowHUDElementEvent;

	public Event<UIHUDElement> OnHideHUDElementEvent;

	public Event<UIHUDElement> OnExpandHUDElementEvent;

	public Event<UIHUDElement> OnCollapseHUDElementEvent;

	private UIHUD m_CurrentHUD;

	private UIHUD[] m_HUDTypes;

	private UIHUD m_HUDSpawn;

	private Canvas m_Canvas;

	private Bitfield<HideReason> m_HiddenChannels = new Bitfield<HideReason>();

	private List<Focussable> m_ChildrenOverlays = new List<Focussable>();

	private Focussable m_FocussedOverlay;

	private bool m_RequestedHUDSpawn;

	private List<HUDElementVisibilityChange> m_HUDElementVisibilityChangesWaiting = new List<HUDElementVisibilityChange>();

	private RadialOpenRequest m_RadialOpenRequest;

	public bool IsVisible { get; private set; }

	public UIHUD CurrentHUD => m_CurrentHUD;

	public Canvas Canvas => m_Canvas;

	public Focussable HighlightedOverlay => m_FocussedOverlay;

	public bool IsSettingUp => m_RequestedHUDSpawn;

	public bool QuickMenuDisabled { get; set; }

	public void Show(HideReason channel, bool show)
	{
		if (!show)
		{
			m_HiddenChannels.Add((int)channel);
		}
		else
		{
			m_HiddenChannels.Remove((int)channel);
		}
		bool flag = m_HiddenChannels.Field == 0;
		if (IsVisible != flag)
		{
			IsVisible = flag;
			m_MainHudPanel.GetComponent<Canvas>().enabled = IsVisible;
			Canvas[] componentsInChildren = m_MainHudPanel.GetComponentsInChildren<Canvas>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = IsVisible;
			}
			m_OverlayHudPanel.GetComponent<Canvas>().enabled = IsVisible;
			m_Canvas.enabled = IsVisible;
			Singleton.Manager<ManLineRenderer>.inst.SetEnabled(IsVisible);
			UIThrottle.SetEnabled(IsVisible);
		}
	}

	public void SetCurrentHUD(HUDType hudType)
	{
		m_HUDElementVisibilityChangesWaiting.Clear();
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.gameObject.SetActive(value: false);
		}
		if (m_HUDTypes[(int)hudType] == null)
		{
			m_HUDTypes[(int)hudType] = m_HudPresets[(int)hudType].Spawn(m_MainHudPanel.transform);
			m_RequestedHUDSpawn = true;
		}
		m_CurrentHUD = m_HUDTypes[(int)hudType];
		m_CurrentHUD.gameObject.SetActive(value: true);
	}

	public void InitialiseHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.InitialiseHudElement(hudElemType, context);
		}
		else
		{
			d.LogError(string.Concat("ManHUD.InitialiseHudElement - Trying to show HUD element of type '", hudElemType, "' but there is no active HUD!"));
		}
	}

	public void DeInitialiseHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.DeInitialiseHudElement(hudElemType, context);
		}
	}

	public void ShowHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			if (m_CurrentHUD.IsSettingUp)
			{
				m_HUDElementVisibilityChangesWaiting.Add(new HUDElementVisibilityChange(hudElemType, HUDElementState.Show, context));
				return;
			}
			bool flag = true;
			if (flag && !SKU.TwitchEnabled)
			{
				flag = hudElemType != HUDElementType.TwitchButtonTT && hudElemType != HUDElementType.TwitchButtonOther && hudElemType != HUDElementType.TwitchStreamList;
			}
			if (flag)
			{
				flag = hudElemType != HUDElementType.TweetThis;
			}
			if (flag)
			{
				m_CurrentHUD.ShowHudElement(hudElemType, context);
			}
		}
		else
		{
			d.LogError(string.Concat("ManHUD.ShowHudElement - Trying to show HUD element of type '", hudElemType, "' but there is no active HUD!"));
		}
	}

	public void HideHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			if (m_CurrentHUD.IsSettingUp)
			{
				m_HUDElementVisibilityChangesWaiting.Add(new HUDElementVisibilityChange(hudElemType, HUDElementState.Hide, context));
			}
			else
			{
				m_CurrentHUD.HideHudElement(hudElemType, context);
			}
		}
	}

	public void ToggleHudElementShown(HUDElementType hudElemType)
	{
		if (IsHudElementVisible(hudElemType))
		{
			HideHudElement(hudElemType);
		}
		else
		{
			ShowHudElement(hudElemType);
		}
	}

	public bool IsHudElementVisible(HUDElementType hudElemType)
	{
		if (!(m_CurrentHUD != null))
		{
			return false;
		}
		return m_CurrentHUD.IsHudElementVisible(hudElemType);
	}

	public void ExpandHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.ExpandHudElement(hudElemType, context);
		}
		else
		{
			d.LogError(string.Concat("ManHUD.ExpandHudElement - Trying to show HUD element of type '", hudElemType, "' but there is no active HUD!"));
		}
	}

	public void CollapseHudElement(HUDElementType hudElemType, object context = null)
	{
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.CollapseHudElement(hudElemType, context);
		}
	}

	public bool IsHudElementExpanded(HUDElementType hudElemType)
	{
		if (!(m_CurrentHUD != null))
		{
			return false;
		}
		return m_CurrentHUD.IsHudElementExpanded(hudElemType);
	}

	public UIHUDElement GetHudElement(HUDElementType hudElemType)
	{
		if (!(m_CurrentHUD != null))
		{
			return null;
		}
		return m_CurrentHUD.GetHudElement(hudElemType);
	}

	public void SetHUDGroupLocked(HUDGroup group, bool locked)
	{
		m_CurrentHUD.SetGroupRule(group, GroupRules.Locked, locked);
	}

	public bool CheckShowActionAllowed(HUDElementType type, UIHUD.ShowAction action)
	{
		return m_CurrentHUD.CheckShowActionAllowed(type, action);
	}

	public bool IsAnyElementInGroupVisible(HUDGroup group)
	{
		if (!(m_CurrentHUD != null))
		{
			return false;
		}
		return m_CurrentHUD.IsAnyElementInGroupVisible(group);
	}

	public void AddOverlay(Focussable overlay)
	{
		overlay.GetTransform().SetParent(m_OverlayHudPanel, worldPositionStays: false);
		m_ChildrenOverlays.Add(overlay);
		overlay.SetFocusLevel((m_FocussedOverlay == null) ? FocusLevel.Default : FocusLevel.Dimmed);
	}

	public void RemoveOverlay(Focussable overlay)
	{
		if (m_FocussedOverlay == overlay)
		{
			ClearFocus(overlay);
		}
		overlay.GetTransform().SetParent(null, worldPositionStays: false);
		m_ChildrenOverlays.Remove(overlay);
	}

	public void AddTooltip(RectTransform overlay)
	{
		overlay.SetParent(m_TooltipPanel, worldPositionStays: false);
	}

	public Vector2 GetMousePositionOnScreen()
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(m_Canvas.transform as RectTransform, Input.mousePosition, m_Canvas.worldCamera, out var localPoint);
		return localPoint;
	}

	public Vector2 GetMousePositionInRect(RectTransform rect)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, m_Canvas.worldCamera, out var localPoint);
		return localPoint;
	}

	public Rect GetTransformRectInLocalTransform(RectTransform rectTransform, RectTransform referenceTransform)
	{
		Vector3[] array = new Vector3[4];
		rectTransform.GetWorldCorners(array);
		Vector2 vector = UIHelpers.WorldToUILocalPosition(array[1], m_Canvas.worldCamera, m_Canvas, referenceTransform);
		Vector2 vector2 = UIHelpers.WorldToUILocalPosition(array[3], m_Canvas.worldCamera, m_Canvas, referenceTransform);
		Vector2 size = new Vector2(vector2.x - vector.x, vector.y - vector2.y);
		return new Rect(new Vector2(vector.x, vector.y - size.y), size);
	}

	public void SetCurrentSpawnHUD(UIHUD spawningHUD)
	{
		m_HUDSpawn = spawningHUD;
	}

	public void AddToCurrentSpawnHUD(UIHUDElement hudElement)
	{
		if (m_HUDSpawn != null)
		{
			m_HUDSpawn.AddElement(hudElement);
		}
		else
		{
			d.LogError("ManHUD.AddToCurrentSpawnHUD - Cannot add HUD element '" + hudElement.name + "' because m_HUDSpawn is null. Is the HUD Element trying to initialise outside our spawning setup.");
		}
	}

	public bool HandleEscapeKey()
	{
		bool result = false;
		if (m_CurrentHUD != null)
		{
			result = m_CurrentHUD.HandleEscapeKey();
		}
		return result;
	}

	public void SetFocus(Focussable focus)
	{
		for (int i = 0; i < m_ChildrenOverlays.Count; i++)
		{
			Focussable focussable = m_ChildrenOverlays[i];
			if (focussable != focus)
			{
				focussable.SetFocusLevel(FocusLevel.Dimmed);
			}
		}
		m_FocussedOverlay = focus;
		m_FocussedOverlay.SetFocusLevel(FocusLevel.Highlighted);
	}

	public void ClearFocus(Focussable focus)
	{
		if (m_FocussedOverlay == focus)
		{
			m_FocussedOverlay = null;
			for (int i = 0; i < m_ChildrenOverlays.Count; i++)
			{
				m_ChildrenOverlays[i].SetFocusLevel(FocusLevel.Default);
			}
		}
	}

	public void AddRadialOpenRequest(HUDElementType elementType, Vector2 mousePos, OpenMenuEventData radialInput)
	{
		if (radialInput.m_RadialInputController == ManInput.RadialInputController.Gamepad)
		{
			ShowHudElement(elementType, radialInput);
			return;
		}
		m_RadialOpenRequest = new RadialOpenRequest
		{
			elementType = elementType,
			mousePos = mousePos,
			radialInput = radialInput,
			timePressed = Time.time
		};
	}

	public static int OverlaySortComparer(Focussable a, Focussable b)
	{
		int num;
		if (Singleton.Manager<ManHUD>.inst.m_FocussedOverlay == a)
		{
			num = 1;
		}
		else if (Singleton.Manager<ManHUD>.inst.m_FocussedOverlay == b)
		{
			num = -1;
		}
		else
		{
			num = -a.GetTransform().position.z.CompareTo(b.GetTransform().position.z);
			if (num == 0)
			{
				num = a.GetTransform().GetSiblingIndex().CompareTo(b.GetTransform().GetSiblingIndex());
			}
		}
		return num;
	}

	private void Start()
	{
		m_Canvas = GetComponent<Canvas>();
		RectTransform obj = base.gameObject.transform as RectTransform;
		obj.sizeDelta = Vector2.zero;
		obj.anchoredPosition = Vector2.zero;
		obj.localScale = Vector3.one;
		m_HUDTypes = new UIHUD[m_HudPresets.Length];
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(OnModeSetup);
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
	}

	private void Update()
	{
		SortOverlays();
		bool flag = (bool)m_CurrentHUD && !m_CurrentHUD.IsSettingUp;
		if (m_RequestedHUDSpawn && flag)
		{
			FinishHUDSetup();
		}
		if (flag && m_HUDElementVisibilityChangesWaiting.Count > 0)
		{
			foreach (HUDElementVisibilityChange item in m_HUDElementVisibilityChangesWaiting)
			{
				switch (item.state)
				{
				case HUDElementState.Show:
					ShowHudElement(item.elementType, item.context);
					break;
				case HUDElementState.Hide:
					HideHudElement(item.elementType, item.context);
					break;
				default:
					d.LogError("ManHUD.Update - Unexpected HUDElementState found");
					break;
				}
			}
			m_HUDElementVisibilityChangesWaiting.Clear();
		}
		CheckRadialOpenRequests();
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.UpdateObscuredElements();
		}
	}

	private void FinishHUDSetup()
	{
		SetCurrentSpawnHUD(null);
		RectTransform obj = m_CurrentHUD.transform as RectTransform;
		obj.sizeDelta = Vector2.zero;
		obj.anchoredPosition3D = Vector3.zero;
		obj.localScale = Vector3.one;
		m_RequestedHUDSpawn = false;
	}

	private void OnModeSetup(Mode newMode)
	{
		HUDType defaultHUDType = newMode.GetDefaultHUDType();
		if (defaultHUDType != HUDType.None)
		{
			SetCurrentHUD(defaultHUDType);
		}
	}

	private void OnModeCleanup(Mode modeToCleanup)
	{
		m_HUDElementVisibilityChangesWaiting.Clear();
		if (m_CurrentHUD != null)
		{
			m_CurrentHUD.gameObject.SetActive(value: false);
			m_CurrentHUD = null;
		}
		for (int i = 0; i < m_HUDTypes.Length; i++)
		{
			if (m_HUDTypes[i] != null)
			{
				m_HUDTypes[i].Recycle();
				m_HUDTypes[i] = null;
			}
		}
	}

	private void SortOverlays()
	{
		if (m_ChildrenOverlays.Count > 0)
		{
			m_ChildrenOverlays.Sort(OverlaySortComparer);
		}
		for (int i = 0; i < m_ChildrenOverlays.Count; i++)
		{
			Transform transform = m_ChildrenOverlays[i].GetTransform();
			if (transform.GetSiblingIndex() != i)
			{
				transform.SetSiblingIndex(i);
			}
		}
	}

	private void CheckRadialOpenRequests()
	{
		if (m_RadialOpenRequest == null || !(Time.time > m_RadialOpenRequest.timePressed + m_RadialShowDelay))
		{
			return;
		}
		if (Vector2.Distance(GetMousePositionOnScreen(), m_RadialOpenRequest.mousePos) < (float)m_RadialMouseDistanceThreshold)
		{
			IRadialInputController radialInputController = Singleton.Manager<ManInput>.inst.GetRadialInputController(m_RadialOpenRequest.radialInput.m_RadialInputController);
			if (radialInputController.IsSelecting())
			{
				radialInputController.SetCustomPosition(m_RadialOpenRequest.mousePos);
				ShowHudElement(m_RadialOpenRequest.elementType, m_RadialOpenRequest.radialInput);
			}
		}
		m_RadialOpenRequest = null;
	}
}
