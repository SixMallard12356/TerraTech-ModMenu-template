#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Rewired.Integration.UnityUI;

public class PayloadStandaloneInputModule : PointerInputModule
{
	private const string DEFAULT_ACTION_MOVE_HORIZONTAL = "UIHorizontal";

	private const string DEFAULT_ACTION_MOVE_VERTICAL = "UIVertical";

	private const string DEFAULT_ACTION_SUBMIT = "UISubmit";

	private const string DEFAULT_ACTION_CANCEL = "UICancel";

	private int[] playerIds;

	private bool recompiling;

	private bool isTouchSupported;

	[SerializeField]
	[Tooltip("Use all Rewired game Players to control the UI. This does not include the System Player. If enabled, this setting overrides individual Player Ids set in Rewired Player Ids.")]
	private bool useAllRewiredGamePlayers;

	[SerializeField]
	[Tooltip("Allow the Rewired System Player to control the UI.")]
	private bool useRewiredSystemPlayer;

	[SerializeField]
	[Tooltip("A list of Player Ids that are allowed to control the UI. If Use All Rewired Game Players = True, this list will be ignored.")]
	private int[] rewiredPlayerIds = new int[1];

	[SerializeField]
	[Tooltip("Allow only Players with Player.isPlaying = true to control the UI.")]
	private bool usePlayingPlayersOnly;

	[Tooltip("Makes an axis press always move only one UI selection. Enable if you do not want to allow scrolling through UI elements by holding an axis direction.")]
	[SerializeField]
	private bool moveOneElementPerAxisPress;

	private float m_PrevActionTime;

	private Vector2 m_LastMoveVector;

	private int m_ConsecutiveMoveCount;

	private Vector2 m_LastMousePosition;

	private Vector2 m_MousePosition;

	private bool m_HasFocus = true;

	private int m_HorizontalAxis = 19;

	private int m_VerticalAxis = 20;

	private int m_SubmitButton = 21;

	private int m_CancelButton = 22;

	private HashSet<int> m_UsedUIInputs = new HashSet<int>();

	private PayloadUIEventData m_ReUsableEventData = new PayloadUIEventData();

	private const float kSmallInputValue = 0.05f;

	[Tooltip("Number of selection changes allowed per second when a movement button/axis is held in a direction.")]
	[SerializeField]
	private float m_InputActionsPerSecond = 10f;

	[Tooltip("Delay in seconds before vertical/horizontal movement starts repeating continouously when a movement direction is held.")]
	[SerializeField]
	private float m_RepeatDelay;

	[Tooltip("Allows the mouse to be used to select elements.")]
	[SerializeField]
	private bool m_allowMouseInput = true;

	[SerializeField]
	[Tooltip("Allows the mouse to be used to select elements if the device also supports touch control.")]
	private bool m_allowMouseInputIfTouchSupported = true;

	[Tooltip("Forces the module to always be active.")]
	[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
	[SerializeField]
	private bool m_ForceModuleActive;

	private static ExecuteEvents.EventFunction<ITabChangePrevHandler> tabHandlerPrev = Execute;

	private static ExecuteEvents.EventFunction<ITabChangeNextHandler> tabHandlerNext = Execute;

	private static ExecuteEvents.EventFunction<IUIExtraButtonHandler1> m_OnUIExtraButton1 = Execute;

	private static ExecuteEvents.EventFunction<IUIExtraButtonHandler2> m_OnUIExtraButton2 = Execute;

	public bool UseAllRewiredGamePlayers
	{
		get
		{
			return useAllRewiredGamePlayers;
		}
		set
		{
			bool num = value != useAllRewiredGamePlayers;
			useAllRewiredGamePlayers = value;
			if (num)
			{
				SetupRewiredVars();
			}
		}
	}

	public bool UseRewiredSystemPlayer
	{
		get
		{
			return useRewiredSystemPlayer;
		}
		set
		{
			bool num = value != useRewiredSystemPlayer;
			useRewiredSystemPlayer = value;
			if (num)
			{
				SetupRewiredVars();
			}
		}
	}

	public int[] RewiredPlayerIds
	{
		get
		{
			return (int[])rewiredPlayerIds.Clone();
		}
		set
		{
			rewiredPlayerIds = ((value != null) ? ((int[])value.Clone()) : new int[0]);
			SetupRewiredVars();
		}
	}

	public bool UsePlayingPlayersOnly
	{
		get
		{
			return usePlayingPlayersOnly;
		}
		set
		{
			usePlayingPlayersOnly = value;
		}
	}

	public bool MoveOneElementPerAxisPress
	{
		get
		{
			return moveOneElementPerAxisPress;
		}
		set
		{
			moveOneElementPerAxisPress = value;
		}
	}

	public bool allowMouseInput
	{
		get
		{
			return m_allowMouseInput;
		}
		set
		{
			m_allowMouseInput = value;
		}
	}

	public bool allowMouseInputIfTouchSupported
	{
		get
		{
			return m_allowMouseInputIfTouchSupported;
		}
		set
		{
			m_allowMouseInputIfTouchSupported = value;
		}
	}

	private bool isMouseSupported
	{
		get
		{
			if (!Input.mousePresent)
			{
				return false;
			}
			if (!m_allowMouseInput)
			{
				return false;
			}
			if (!isTouchSupported)
			{
				return true;
			}
			return m_allowMouseInputIfTouchSupported;
		}
	}

	[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead")]
	public bool allowActivationOnMobileDevice
	{
		get
		{
			return m_ForceModuleActive;
		}
		set
		{
			m_ForceModuleActive = value;
		}
	}

	public bool forceModuleActive
	{
		get
		{
			return m_ForceModuleActive;
		}
		set
		{
			m_ForceModuleActive = value;
		}
	}

	public float inputActionsPerSecond
	{
		get
		{
			return m_InputActionsPerSecond;
		}
		set
		{
			m_InputActionsPerSecond = value;
		}
	}

	public float repeatDelay
	{
		get
		{
			return m_RepeatDelay;
		}
		set
		{
			m_RepeatDelay = value;
		}
	}

	protected PayloadStandaloneInputModule()
	{
	}

	protected override void Awake()
	{
		base.Awake();
		isTouchSupported = Input.touchSupported;
		TouchInputModule component = GetComponent<TouchInputModule>();
		if (component != null)
		{
			component.enabled = false;
		}
		InitializeRewired();
	}

	public override void UpdateModule()
	{
		CheckEditorRecompile();
		if (!recompiling && ReInput.isReady && (m_HasFocus || !ShouldIgnoreEventsOnNoFocus()) && isMouseSupported)
		{
			m_LastMousePosition = m_MousePosition;
			m_MousePosition = ReInput.controllers.Mouse.screenPosition;
		}
	}

	public override bool IsModuleSupported()
	{
		return true;
	}

	public override bool ShouldActivateModule()
	{
		if (!base.ShouldActivateModule())
		{
			return false;
		}
		if (recompiling)
		{
			return false;
		}
		if (!ReInput.isReady)
		{
			return false;
		}
		bool flag = m_ForceModuleActive;
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player != null && (!usePlayingPlayersOnly || player.isPlaying))
			{
				flag |= player.GetButtonDown(m_SubmitButton);
				flag |= player.GetButtonDown(m_CancelButton);
				if (moveOneElementPerAxisPress)
				{
					flag |= player.GetButtonDown(m_HorizontalAxis) || player.GetNegativeButtonDown(m_HorizontalAxis);
					flag |= player.GetButtonDown(m_VerticalAxis) || player.GetNegativeButtonDown(m_VerticalAxis);
				}
				else
				{
					flag |= Mathf.Abs(player.GetAxisRaw(m_HorizontalAxis)) >= 0.05f;
					flag |= Mathf.Abs(player.GetAxisRaw(m_VerticalAxis)) >= 0.05f;
				}
			}
		}
		if (isMouseSupported)
		{
			flag |= (m_MousePosition - m_LastMousePosition).sqrMagnitude > 0f;
			flag |= ReInput.controllers.Mouse.GetButtonDown(0);
		}
		if (isTouchSupported)
		{
			for (int j = 0; j < Input.touchCount; j++)
			{
				Touch touch = Input.GetTouch(j);
				flag |= touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
			}
		}
		return flag;
	}

	public override void ActivateModule()
	{
		if (m_HasFocus || !ShouldIgnoreEventsOnNoFocus())
		{
			base.ActivateModule();
			if (isMouseSupported)
			{
				m_LastMousePosition = (m_MousePosition = ReInput.controllers.Mouse.screenPosition);
			}
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, GetBaseEventData());
		}
	}

	public override void DeactivateModule()
	{
		base.DeactivateModule();
		ClearSelection();
	}

	public override void Process()
	{
		if (!ReInput.isReady || (!m_HasFocus && ShouldIgnoreEventsOnNoFocus()))
		{
			return;
		}
		int count = Singleton.Manager<ManNavUI>.inst.UIInputHandlers.Count;
		bool flag = SendUpdateEventToSelectedObject();
		if (base.eventSystem.sendNavigationEvents)
		{
			if (!flag)
			{
				flag |= SendMoveEventToSelectedObject();
			}
			if (!flag)
			{
				flag |= SendSubmitEventToSelectedObject();
			}
		}
		m_UsedUIInputs.Clear();
		if (!flag)
		{
			for (int num = count - 1; num >= 0; num--)
			{
				ManNavUI.UIInputHandlerInfo info = Singleton.Manager<ManNavUI>.inst.UIInputHandlers[num];
				if (!info.m_Expired)
				{
					if (!(info.m_Handler.Target as Component).gameObject.activeInHierarchy)
					{
						d.LogError("Component got deactivated in heirarchy without calling ManNavUI.UnregisterUIInputHandler");
					}
					else
					{
						flag |= ProcessUIInputHandler(info);
						if (flag)
						{
							break;
						}
					}
				}
			}
		}
		for (int num2 = Singleton.Manager<ManNavUI>.inst.UIInputHandlers.Count - 1; num2 >= 0; num2--)
		{
			if (Singleton.Manager<ManNavUI>.inst.UIInputHandlers[num2].m_Expired)
			{
				Singleton.Manager<ManNavUI>.inst.UIInputHandlers.RemoveAt(num2);
			}
		}
		bool flag2 = false;
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player != null && player.GetButtonDown(m_CancelButton))
			{
				flag2 = true;
				break;
			}
		}
		if (Singleton.Manager<ManGameMode>.inst != null && flag2)
		{
			Singleton.Manager<ManGameMode>.inst.SetCancelEventWasHandled(flag);
		}
		if (!ProcessTouchEvents() && isMouseSupported)
		{
			ProcessMouseEvent();
		}
	}

	public void HandleCustomPointerExitAndEnter<TIEnterEvent, TIExitEvent>(PointerEventData currentPointerData, GameObject newEnterTarget, ExecuteEvents.EventFunction<TIEnterEvent> enterHandler, ExecuteEvents.EventFunction<TIExitEvent> exitHandler) where TIEnterEvent : IEventSystemHandler where TIExitEvent : IEventSystemHandler
	{
		if (newEnterTarget == null || currentPointerData.pointerEnter == null)
		{
			for (int i = 0; i < currentPointerData.hovered.Count; i++)
			{
				ExecuteEvents.Execute(currentPointerData.hovered[i], currentPointerData, exitHandler);
			}
			currentPointerData.hovered.Clear();
			if (newEnterTarget == null)
			{
				currentPointerData.pointerEnter = newEnterTarget;
				return;
			}
		}
		if (currentPointerData.pointerEnter == newEnterTarget && (bool)newEnterTarget)
		{
			return;
		}
		GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
		if (currentPointerData.pointerEnter != null)
		{
			Transform parent = currentPointerData.pointerEnter.transform;
			while (parent != null && (!(gameObject != null) || !(gameObject.transform == parent)))
			{
				ExecuteEvents.Execute(parent.gameObject, currentPointerData, exitHandler);
				currentPointerData.hovered.Remove(parent.gameObject);
				parent = parent.parent;
			}
		}
		currentPointerData.pointerEnter = newEnterTarget;
		if (newEnterTarget != null)
		{
			Transform parent2 = newEnterTarget.transform;
			while (parent2 != null && parent2.gameObject != gameObject)
			{
				ExecuteEvents.Execute(parent2.gameObject, currentPointerData, enterHandler);
				currentPointerData.hovered.Add(parent2.gameObject);
				parent2 = parent2.parent;
			}
		}
	}

	private bool ProcessTouchEvents()
	{
		if (!isTouchSupported)
		{
			return false;
		}
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			if (touch.type != TouchType.Indirect)
			{
				bool pressed;
				bool released;
				PointerEventData touchPointerEventData = GetTouchPointerEventData(touch, out pressed, out released);
				ProcessTouchPress(touchPointerEventData, pressed, released);
				if (!released)
				{
					ProcessMove(touchPointerEventData);
					ProcessDrag(touchPointerEventData);
				}
				else
				{
					RemovePointerData(touchPointerEventData);
				}
			}
		}
		return Input.touchCount > 0;
	}

	private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
	{
		GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
		if (pressed)
		{
			pointerEvent.eligibleForClick = true;
			pointerEvent.delta = Vector2.zero;
			pointerEvent.dragging = false;
			pointerEvent.useDragThreshold = true;
			pointerEvent.pressPosition = pointerEvent.position;
			pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
			DeselectIfSelectionChanged(gameObject, pointerEvent);
			if (pointerEvent.pointerEnter != gameObject)
			{
				HandlePointerExitAndEnter(pointerEvent, gameObject);
				pointerEvent.pointerEnter = gameObject;
			}
			GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
			if (gameObject2 == null)
			{
				gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			}
			float unscaledTime = Time.unscaledTime;
			if (gameObject2 == pointerEvent.lastPress)
			{
				if (unscaledTime - pointerEvent.clickTime < 0.3f)
				{
					int clickCount = pointerEvent.clickCount + 1;
					pointerEvent.clickCount = clickCount;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.clickTime = unscaledTime;
			}
			else
			{
				pointerEvent.clickCount = 1;
			}
			pointerEvent.pointerPress = gameObject2;
			pointerEvent.rawPointerPress = gameObject;
			pointerEvent.clickTime = unscaledTime;
			pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
			if (pointerEvent.pointerDrag != null)
			{
				ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
			}
		}
		if (released)
		{
			ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
			{
				ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
			}
			else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, ExecuteEvents.dropHandler);
			}
			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
			}
			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;
			if (pointerEvent.pointerDrag != null)
			{
				ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
			}
			pointerEvent.pointerDrag = null;
			ExecuteEvents.ExecuteHierarchy(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
			pointerEvent.pointerEnter = null;
		}
	}

	private bool ProcessUIInputHandler(ManNavUI.UIInputHandlerInfo info)
	{
		if (recompiling)
		{
			return false;
		}
		if (m_UsedUIInputs.Contains(info.m_RewiredAction))
		{
			return false;
		}
		if (info.m_Expired)
		{
			return false;
		}
		m_ReUsableEventData.Reset();
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player != null && (!usePlayingPlayersOnly || player.isPlaying) && player.GetButtonDown(info.m_RewiredAction))
			{
				d.AssertFormat(info.m_Handler != null, "Null handler registered for rewired action {0}", info.m_RewiredAction);
				info.m_Handler(m_ReUsableEventData);
				if (m_ReUsableEventData.used)
				{
					m_UsedUIInputs.Add(info.m_RewiredAction);
					break;
				}
			}
		}
		return m_ReUsableEventData.used;
	}

	protected bool SendSubmitEventToSelectedObject()
	{
		if (base.eventSystem.currentSelectedGameObject == null)
		{
			return false;
		}
		if (recompiling)
		{
			return false;
		}
		BaseEventData baseEventData = GetBaseEventData();
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player != null && (!usePlayingPlayersOnly || player.isPlaying))
			{
				if (player.GetButtonDown(m_SubmitButton))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
					break;
				}
				if (player.GetButtonDown(m_CancelButton))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
					break;
				}
				if (player.GetButtonDown(42))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, tabHandlerPrev);
					break;
				}
				if (player.GetButtonDown(41))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, tabHandlerNext);
					break;
				}
				if (player.GetButtonDown(57))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, m_OnUIExtraButton1);
					break;
				}
				if (player.GetButtonDown(58))
				{
					ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, m_OnUIExtraButton2);
					break;
				}
			}
		}
		return baseEventData.used;
	}

	private Vector2 GetRawMoveVector()
	{
		if (recompiling)
		{
			return Vector2.zero;
		}
		Vector2 zero = Vector2.zero;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player == null || (usePlayingPlayersOnly && !player.isPlaying))
			{
				continue;
			}
			if (moveOneElementPerAxisPress)
			{
				float num = 0f;
				if (player.GetButtonDown(m_HorizontalAxis))
				{
					num = 1f;
				}
				else if (player.GetNegativeButtonDown(m_HorizontalAxis))
				{
					num = -1f;
				}
				float num2 = 0f;
				if (player.GetButtonDown(m_VerticalAxis))
				{
					num2 = 1f;
				}
				else if (player.GetNegativeButtonDown(m_VerticalAxis))
				{
					num2 = -1f;
				}
				zero.x += num;
				zero.y += num2;
			}
			else
			{
				zero.x += player.GetAxisRaw(m_HorizontalAxis);
				zero.y += player.GetAxisRaw(m_VerticalAxis);
			}
			flag |= player.GetButtonDown(m_HorizontalAxis) || player.GetNegativeButtonDown(m_HorizontalAxis);
			flag2 |= player.GetButtonDown(m_VerticalAxis) || player.GetNegativeButtonDown(m_VerticalAxis);
		}
		if (flag)
		{
			if (zero.x < 0f)
			{
				zero.x = -1f;
			}
			if (zero.x > 0f)
			{
				zero.x = 1f;
			}
		}
		if (flag2)
		{
			if (zero.y < 0f)
			{
				zero.y = -1f;
			}
			if (zero.y > 0f)
			{
				zero.y = 1f;
			}
		}
		return zero;
	}

	protected bool SendMoveEventToSelectedObject()
	{
		if (recompiling)
		{
			return false;
		}
		float unscaledTime = Time.unscaledTime;
		Vector2 rawMoveVector = GetRawMoveVector();
		if (Mathf.Abs(rawMoveVector.x) < 0.05f && Mathf.Abs(rawMoveVector.y) < 0.05f)
		{
			m_ConsecutiveMoveCount = 0;
			return false;
		}
		bool flag = Vector2.Dot(rawMoveVector, m_LastMoveVector) > 0f;
		bool flag2 = CheckButtonOrKeyMovement(unscaledTime);
		if (!flag2)
		{
			flag2 = ((!(m_RepeatDelay > 0f)) ? (unscaledTime > m_PrevActionTime + 1f / m_InputActionsPerSecond) : ((!flag || m_ConsecutiveMoveCount != 1) ? (unscaledTime > m_PrevActionTime + 1f / m_InputActionsPerSecond) : (unscaledTime > m_PrevActionTime + m_RepeatDelay)));
		}
		if (!flag2)
		{
			return false;
		}
		AxisEventData axisEventData = GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
		if (axisEventData.moveDir == MoveDirection.None)
		{
			return false;
		}
		ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
		if (!flag)
		{
			m_ConsecutiveMoveCount = 0;
		}
		m_ConsecutiveMoveCount++;
		m_PrevActionTime = unscaledTime;
		m_LastMoveVector = rawMoveVector;
		return axisEventData.used;
	}

	private bool CheckButtonOrKeyMovement(float time)
	{
		bool flag = false;
		for (int i = 0; i < playerIds.Length; i++)
		{
			Player player = ReInput.players.GetPlayer(playerIds[i]);
			if (player != null && (!usePlayingPlayersOnly || player.isPlaying))
			{
				flag |= player.GetButtonDown(m_HorizontalAxis) || player.GetNegativeButtonDown(m_HorizontalAxis);
				flag |= player.GetButtonDown(m_VerticalAxis) || player.GetNegativeButtonDown(m_VerticalAxis);
			}
		}
		return flag;
	}

	protected void ProcessMouseEvent()
	{
		ProcessMouseEvent(0);
	}

	protected void ProcessMouseEvent(int id)
	{
		MouseState mousePointerEventData = GetMousePointerEventData();
		MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
		ProcessMousePress(eventData);
		ProcessMove(eventData.buttonData);
		ProcessDrag(eventData.buttonData);
		ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
		ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
		ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
		ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
		if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
		{
			ExecuteEvents.ExecuteHierarchy(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
		}
	}

	protected bool SendUpdateEventToSelectedObject()
	{
		if (base.eventSystem.currentSelectedGameObject == null)
		{
			return false;
		}
		BaseEventData baseEventData = GetBaseEventData();
		ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
		return baseEventData.used;
	}

	protected void ProcessMousePress(MouseButtonEventData data)
	{
		PointerEventData buttonData = data.buttonData;
		GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
		if (data.PressedThisFrame())
		{
			buttonData.eligibleForClick = true;
			buttonData.delta = Vector2.zero;
			buttonData.dragging = false;
			buttonData.useDragThreshold = true;
			buttonData.pressPosition = buttonData.position;
			buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
			DeselectIfSelectionChanged(gameObject, buttonData);
			GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
			if (gameObject2 == null)
			{
				gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			}
			float unscaledTime = Time.unscaledTime;
			if (gameObject2 == buttonData.lastPress)
			{
				if (unscaledTime - buttonData.clickTime < 0.3f)
				{
					int clickCount = buttonData.clickCount + 1;
					buttonData.clickCount = clickCount;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.clickTime = unscaledTime;
			}
			else
			{
				buttonData.clickCount = 1;
			}
			buttonData.pointerPress = gameObject2;
			buttonData.rawPointerPress = gameObject;
			buttonData.clickTime = unscaledTime;
			buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
			if (buttonData.pointerDrag != null)
			{
				ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
			}
		}
		if (data.ReleasedThisFrame())
		{
			ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
			{
				ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
			}
			else if (buttonData.pointerDrag != null && buttonData.dragging)
			{
				ExecuteEvents.ExecuteHierarchy(gameObject, buttonData, ExecuteEvents.dropHandler);
			}
			buttonData.eligibleForClick = false;
			buttonData.pointerPress = null;
			buttonData.rawPointerPress = null;
			if (buttonData.pointerDrag != null && buttonData.dragging)
			{
				ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
			}
			buttonData.dragging = false;
			buttonData.pointerDrag = null;
			if (gameObject != buttonData.pointerEnter)
			{
				HandlePointerExitAndEnter(buttonData, null);
				HandlePointerExitAndEnter(buttonData, gameObject);
			}
		}
		buttonData.Reset();
	}

	protected virtual void OnApplicationFocus(bool hasFocus)
	{
		m_HasFocus = hasFocus;
	}

	private bool ShouldIgnoreEventsOnNoFocus()
	{
		if (!ReInput.isReady)
		{
			return true;
		}
		return ReInput.configuration.ignoreInputWhenAppNotInFocus;
	}

	private static void Execute(ITabChangePrevHandler handler, BaseEventData eventData)
	{
		handler.OnTabChangePrev(eventData);
	}

	private static void Execute(ITabChangeNextHandler handler, BaseEventData eventData)
	{
		handler.OnTabChangeNext(eventData);
	}

	private static void Execute(IUIExtraButtonHandler1 handler, BaseEventData eventData)
	{
		handler.OnUIExtraButton1(eventData);
	}

	private static void Execute(IUIExtraButtonHandler2 handler, BaseEventData eventData)
	{
		handler.OnUIExtraButton2(eventData);
	}

	private void InitializeRewired()
	{
		if (!ReInput.isReady)
		{
			d.LogError("Rewired is not initialized! Are you missing a Rewired Input Manager in your scene?");
			return;
		}
		ReInput.EditorRecompileEvent += OnEditorRecompile;
		SetupRewiredVars();
	}

	private void SetupRewiredVars()
	{
		if (useAllRewiredGamePlayers)
		{
			IList<Player> list = (useRewiredSystemPlayer ? ReInput.players.AllPlayers : ReInput.players.Players);
			playerIds = new int[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				playerIds[i] = list[i].id;
			}
			return;
		}
		int num = rewiredPlayerIds.Length + (useRewiredSystemPlayer ? 1 : 0);
		playerIds = new int[num];
		for (int j = 0; j < rewiredPlayerIds.Length; j++)
		{
			playerIds[j] = ReInput.players.GetPlayer(rewiredPlayerIds[j]).id;
		}
		if (useRewiredSystemPlayer)
		{
			playerIds[num - 1] = ReInput.players.GetSystemPlayer().id;
		}
	}

	private void CheckEditorRecompile()
	{
		if (recompiling && ReInput.isReady)
		{
			recompiling = false;
			InitializeRewired();
		}
	}

	private void OnEditorRecompile()
	{
		recompiling = true;
		ClearRewiredVars();
	}

	private void ClearRewiredVars()
	{
		Array.Clear(playerIds, 0, playerIds.Length);
	}
}
