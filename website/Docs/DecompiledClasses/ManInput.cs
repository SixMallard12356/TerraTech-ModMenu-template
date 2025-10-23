#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Rewired;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ManInput : Singleton.Manager<ManInput>
{
	public enum InputMode
	{
		None,
		KeyboardAndMouse,
		Gamepad
	}

	private struct UIInputRequests
	{
		public Component m_OwningComponent;

		public UIInputMode m_Mode;
	}

	public enum GamepadElementID
	{
		Invalid = -1,
		LeftStickX = 0,
		LeftStickY = 1,
		RightStickX = 2,
		RightStickY = 3,
		LeftTrigger = 4,
		RightTrigger = 5,
		AButton = 6,
		BButton = 7,
		XButton = 8,
		YButton = 9,
		LeftShoulder = 10,
		RightShoulder = 11,
		Back = 12,
		Start = 13,
		Guide = 22,
		LeftStickButton = 14,
		RightStickButton = 15,
		DPadUp = 16,
		DPadRight = 17,
		DPadDown = 18,
		DPadLeft = 19,
		LeftStick = 20,
		RightStick = 21
	}

	public delegate void OnKeyAssigned(bool changed, ActionElementMap map);

	public class ElementAssignmentChange
	{
		public int uiIndex { get; private set; }

		public int playerId { get; private set; }

		public int controllerId { get; private set; }

		public int actionElementMapId
		{
			get
			{
				if (actionElementMap != null)
				{
					return actionElementMap.id;
				}
				return -1;
			}
		}

		public int actionId { get; private set; }

		public Pole actionAxisContribution { get; private set; }

		public InputActionType actionType { get; private set; }

		public bool assignFullAxis { get; private set; }

		public bool invert { get; private set; }

		public ActionElementMap actionElementMap { get; private set; }

		public ControllerPollingInfo pollingInfo { get; set; }

		public ModifierKeyFlags modifierKeyFlags { get; set; }

		public ControllerType controllerType { get; set; }

		public bool invalidKey { get; set; }

		public AxisRange AssignedAxisRange
		{
			get
			{
				if (!pollingInfo.success)
				{
					return AxisRange.Positive;
				}
				ControllerElementType elementType = pollingInfo.elementType;
				Pole axisPole = pollingInfo.axisPole;
				AxisRange result = AxisRange.Positive;
				if (elementType == ControllerElementType.Axis)
				{
					result = ((actionType != InputActionType.Axis) ? ((axisPole == Pole.Positive) ? AxisRange.Positive : AxisRange.Negative) : ((!assignFullAxis) ? ((axisPole == Pole.Positive) ? AxisRange.Positive : AxisRange.Negative) : AxisRange.Full));
				}
				return result;
			}
		}

		public ElementAssignmentChange(int uiIndex, int playerId, int controllerId, ControllerType controllerType, ActionElementMap actionElementMap, int actionId, Pole actionAxisContribution, InputActionType actionType, bool assignFullAxis, bool invert)
		{
			this.uiIndex = uiIndex;
			this.playerId = playerId;
			this.controllerId = controllerId;
			this.controllerType = controllerType;
			this.actionElementMap = actionElementMap;
			this.actionId = actionId;
			this.actionAxisContribution = actionAxisContribution;
			this.actionType = actionType;
			this.assignFullAxis = assignFullAxis;
			this.invert = invert;
		}

		public ElementAssignment ToElementAssignment()
		{
			return new ElementAssignment(controllerType, pollingInfo.elementType, pollingInfo.elementIdentifierId, AssignedAxisRange, pollingInfo.keyboardKey, modifierKeyFlags, actionId, actionAxisContribution, invert, actionElementMapId);
		}
	}

	private struct SaveData
	{
		public PlayerControllerSaveData[] PlayerData;

		public Dictionary<string, string> JoystickData;

		public int Version;
	}

	private struct PlayerControllerSaveData
	{
		public Dictionary<string, string> InputBehavior;

		public Dictionary<string, string> ControllerMap;
	}

	public enum RadialInputController
	{
		Mouse,
		Gamepad
	}

	[SerializeField]
	private List<KeyCode> m_InvalidKeys;

	[EnumArray(typeof(UIInputMode))]
	[SerializeField]
	private string[] m_InputMapCategoryForMode;

	[SerializeField]
	private int m_InputMapVersion;

	[SerializeField]
	[Tooltip("Whether switching input mode is supported after startup.")]
	private bool m_CanSwitchInputModeAtRuntime;

	[SerializeField]
	private JoystickGlyphDB m_GlyphDB;

	[SerializeField]
	private DefaultControlSchemes m_DefaultControlSchemes;

	[SerializeField]
	private DefaultControlSchemes m_DefaultControlSchemesJoypad;

	public Event<ActionElementMap> OnControlMapRemoved;

	public Event<int, int, Pole, ActionElementMap> OnControlMapAssigned;

	public Event<InputMode> OnInputModeChanged;

	public Event<UIInputMode> GamepadMapsChangedEvent;

	public Event<ControllerType> LastActiveControllerTypeChangedEvent;

	public Event<ElementAssignmentChange> KeyAssignmentEvent;

	private bool m_CheckKeyAssign;

	private Player m_Player;

	private int m_ControllerID;

	private ElementAssignmentChange m_ElementAssignmentChange;

	private Dictionary<int, string> m_CategoryNameLookup = new Dictionary<int, string>();

	private List<UIInputRequests> m_ControllerUiElements = new List<UIInputRequests>();

	private IRadialInputController m_MouseInputRadialController = new MouseRadialInputController();

	private IRadialInputController m_GamepadInputRadialController = new GamePadRadialInputController();

	private int[] m_MapCategoryForInputMode;

	private InputMode m_CurrentInputMode = InputMode.KeyboardAndMouse;

	private InputMode m_CurrentPlayerInputDevice = InputMode.KeyboardAndMouse;

	private bool m_HasDoneEmulatedUserEngagement;

	private HashSet<Controller> m_FlippedControllers = new HashSet<Controller>();

	public DefaultControlSchemes DefaultControlSchemes
	{
		get
		{
			if (!IsGamepadUseEnabled())
			{
				return m_DefaultControlSchemes;
			}
			return m_DefaultControlSchemesJoypad;
		}
	}

	public bool IsPollingForKeyAssignment => m_CheckKeyAssign;

	public bool GetAnyButtonDown()
	{
		if (m_Player != null)
		{
			return m_Player.GetAnyButtonDown();
		}
		return false;
	}

	public float GetAxis(int rewiredActionConst)
	{
		if (m_Player != null)
		{
			return m_Player.GetAxis(rewiredActionConst);
		}
		return 0f;
	}

	public float GetAxisPrev(int rewiredActionConst)
	{
		if (m_Player != null)
		{
			return m_Player.GetAxisPrev(rewiredActionConst);
		}
		return 0f;
	}

	public float GetAxisRawTimeActive(int rewiredActionConst)
	{
		if (m_Player != null)
		{
			return m_Player.GetAxisRawTimeActive(rewiredActionConst);
		}
		return 0f;
	}

	public Vector2 GetAxis2D(int xAxisActionConst, int yAxisActionConst)
	{
		if (m_Player != null)
		{
			return m_Player.GetAxis2D(xAxisActionConst, yAxisActionConst);
		}
		return Vector2.zero;
	}

	public Vector2 GetAxis2DRaw(int xAxisActionConst, int yAxisActionConst)
	{
		if (m_Player != null)
		{
			return m_Player.GetAxis2DRaw(xAxisActionConst, yAxisActionConst);
		}
		return Vector2.zero;
	}

	public bool GetButton(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButton(rewiredAction);
		}
		return false;
	}

	public bool GetButtonDown(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonDown(rewiredAction);
		}
		return false;
	}

	public bool GetButtonDown(int rewiredAction, ControllerType controllerType)
	{
		bool result = false;
		if (m_Player != null)
		{
			result = m_Player.GetButtonDown(rewiredAction) && m_Player.IsCurrentInputSource(rewiredAction, controllerType);
		}
		return result;
	}

	public bool GetButtonUp(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonUp(rewiredAction);
		}
		return false;
	}

	public bool GetButtonRepeating(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonRepeating(rewiredAction);
		}
		return false;
	}

	public bool GetNegativeButton(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetNegativeButton(rewiredAction);
		}
		return false;
	}

	public bool GetNegativeButtonDown(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetNegativeButtonDown(rewiredAction);
		}
		return false;
	}

	public bool GetNegativeButtonUp(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetNegativeButtonUp(rewiredAction);
		}
		return false;
	}

	public bool GetNegativeButtonRepeating(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetNegativeButtonRepeating(rewiredAction);
		}
		return false;
	}

	public bool GetButtonDoublePressDown(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonDoublePressDown(rewiredAction);
		}
		return false;
	}

	public bool GetButtonDoublePressHold(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonDoublePressHold(rewiredAction);
		}
		return false;
	}

	public bool GetButtonDoublePressUp(int rewiredAction)
	{
		if (m_Player != null)
		{
			return m_Player.GetButtonDoublePressUp(rewiredAction);
		}
		return false;
	}

	public void RegisterInputEventDelegate(int rewiredAction, InputActionEventType eventType, Action<InputActionEventData> callback, UpdateLoopType updateLoop = UpdateLoopType.Update)
	{
		if (m_Player != null)
		{
			m_Player.AddInputEventDelegate(callback, updateLoop, eventType, rewiredAction);
		}
		else
		{
			d.LogError("RegisterInputEventDelegate - Failed to register input delegate because there was no Player!");
		}
	}

	public void UnregisterInputEventDelegate(int rewiredAction, InputActionEventType eventType, Action<InputActionEventData> callback, UpdateLoopType updateLoop = UpdateLoopType.Update)
	{
		if (m_Player != null)
		{
			m_Player.RemoveInputEventDelegate(callback, updateLoop, eventType, rewiredAction);
		}
		else
		{
			d.LogError("UnregisterInputEventDelegate - Failed to register input delegate because there was no Player!");
		}
	}

	public void SetVibration(float motor1Intensity, float motor2Intensity, float duration = 0f)
	{
		if (m_Player != null)
		{
			m_Player.SetVibration(0, motor1Intensity, duration);
			m_Player.SetVibration(1, motor2Intensity, duration);
		}
	}

	public void StopVibration()
	{
		if (m_Player != null)
		{
			m_Player.StopVibration();
		}
	}

	public bool IsVibrating()
	{
		if (m_Player != null)
		{
			if (!(m_Player.GetVibration(0) > 0f))
			{
				return m_Player.GetVibration(1) > 0f;
			}
			return true;
		}
		return false;
	}

	public string GetKeyBoundPrimaryName(int rewiredAction)
	{
		return FindKeyNameForAction(rewiredAction, GetCurrentControllerType());
	}

	public string GetKeyBoundPrimaryNames(int rewiredAction, bool flipOrder = false)
	{
		string outNegName;
		string outPosName;
		if (flipOrder)
		{
			GetKeyBoundPrimaryNamesPosNeg(rewiredAction, out outNegName, out outPosName);
		}
		else
		{
			GetKeyBoundPrimaryNamesPosNeg(rewiredAction, out outPosName, out outNegName);
		}
		if (outPosName.NullOrEmpty() && outNegName.NullOrEmpty())
		{
			return string.Empty;
		}
		if (outPosName.NullOrEmpty() && !outNegName.NullOrEmpty())
		{
			return outNegName;
		}
		if (!outPosName.NullOrEmpty() && outNegName.NullOrEmpty())
		{
			return outPosName;
		}
		return outPosName + "/" + outNegName;
	}

	public string GetKeyBoundPrimaryNames(AxisMapping axisMapping, bool flipOrder = false)
	{
		string keyBoundPrimaryNames = GetKeyBoundPrimaryNames(AxisMapping.GetRewiredAction(axisMapping.m_InputAxis), flipOrder != axisMapping.m_Invert);
		if (keyBoundPrimaryNames == "")
		{
			keyBoundPrimaryNames = GetKeyBoundPrimaryNames(AxisMapping.GetRewiredAction(axisMapping.m_InputAxis2), flipOrder != axisMapping.m_Invert2);
		}
		return keyBoundPrimaryNames;
	}

	public void GetKeyBoundPrimaryNamesPosNeg(int rewiredAction, out string outPosName, out string outNegName)
	{
		outPosName = string.Empty;
		outNegName = string.Empty;
		if (rewiredAction < 0)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		foreach (ActionElementMap item in m_Player.controllers.maps.ElementMapsWithAction(ControllerType.Keyboard, rewiredAction, skipDisabledMaps: false))
		{
			if (!flag && item.axisContribution == Pole.Positive)
			{
				flag = true;
				outPosName = Singleton.Manager<Localisation>.inst.ActionElementMapToString(item);
			}
			if (!flag2 && item.axisContribution == Pole.Negative)
			{
				flag2 = true;
				outNegName = Singleton.Manager<Localisation>.inst.ActionElementMapToString(item);
			}
		}
	}

	private ControllerType GetCurrentControllerType()
	{
		return m_CurrentPlayerInputDevice switch
		{
			InputMode.KeyboardAndMouse => ControllerType.Keyboard, 
			InputMode.Gamepad => ControllerType.Joystick, 
			_ => ControllerType.Keyboard, 
		};
	}

	public bool IsCurrentInputSource(int rewiredAction, ControllerType controllerType)
	{
		if (m_Player != null)
		{
			return m_Player.IsCurrentInputSource(rewiredAction, controllerType);
		}
		return false;
	}

	public ActionElementMap GetRewiredActionElement(int actionId, Pole axisContribution)
	{
		ControllerType controllerType = (IsGamepadUseEnabled() ? ControllerType.Joystick : ControllerType.Keyboard);
		int controllerId = (IsGamepadUseEnabled() ? m_Player.controllers.Joysticks[0].id : 0);
		foreach (ActionElementMap allMap in m_Player.controllers.maps.GetFirstMapInCategory(controllerType, controllerId, 0).AllMaps)
		{
			if (allMap.actionId == actionId && allMap.axisContribution == axisContribution && (controllerType != ControllerType.Keyboard || allMap.keyCode != KeyCode.None))
			{
				return allMap;
			}
		}
		return null;
	}

	public void CancelKeyAssign()
	{
		m_ElementAssignmentChange = null;
		m_CheckKeyAssign = false;
	}

	public bool StartPollingForKeyAssignment(int uiIndex, int actionID, Pole actionAxisContribution, ActionElementMap assignedActionMap)
	{
		if (!m_CheckKeyAssign)
		{
			bool assignFullAxis = true;
			bool invert = false;
			m_ElementAssignmentChange = null;
			foreach (InputAction action in ReInput.mapping.Actions)
			{
				if (action.id == actionID)
				{
					if (action.type == InputActionType.Axis)
					{
						assignFullAxis = false;
					}
					m_ElementAssignmentChange = new ElementAssignmentChange(uiIndex, m_Player.id, m_ControllerID, ControllerType.Keyboard, assignedActionMap, actionID, actionAxisContribution, action.type, assignFullAxis, invert);
					break;
				}
			}
			if (m_ElementAssignmentChange != null)
			{
				m_CheckKeyAssign = true;
				return true;
			}
		}
		return false;
	}

	public void SetInitialControllerMaps()
	{
		SetMapsEnabled(enable: true, ControllerType.Keyboard, UIInputMode.Default);
		SetMapsEnabled(enable: true, ControllerType.Joystick, UIInputMode.Default);
		SetMapsEnabled(enable: true, ControllerType.Mouse, UIInputMode.Default);
		SetMapsEnabled(enable: true, ControllerType.Keyboard, UIInputMode.FullscreenUI);
		foreach (Controller controller in m_Player.controllers.Controllers)
		{
			FlipControllerButtonsIfNecessary(controller);
		}
	}

	public void EnableAllControllerMapsOfType(bool enabled, ControllerType controllerType)
	{
		if (m_Player != null)
		{
			m_Player.controllers.maps.SetAllMapsEnabled(enabled, controllerType);
		}
		else
		{
			d.LogError("ManInput.EnableAllControllerMapsOfType - Null player when called!");
		}
	}

	public UIInputMode GetCurrentUIInputMode()
	{
		UIInputMode uIInputMode = UIInputMode.Default;
		for (int i = 0; i < m_ControllerUiElements.Count; i++)
		{
			uIInputMode = m_ControllerUiElements[i].m_Mode;
			if (uIInputMode == UIInputMode.FullscreenUI)
			{
				break;
			}
		}
		if (uIInputMode != UIInputMode.FullscreenUI && m_CurrentInputMode != InputMode.Gamepad)
		{
			uIInputMode = UIInputMode.Default;
		}
		return uIInputMode;
	}

	public void SetControllerMapsForUI(Component owningComponent, bool uiModeEnable, UIInputMode mode)
	{
		UIInputMode currentUIInputMode = GetCurrentUIInputMode();
		for (int num = m_ControllerUiElements.Count - 1; num >= 0; num--)
		{
			if (m_ControllerUiElements[num].m_Mode == mode && (m_ControllerUiElements[num].m_OwningComponent == owningComponent || m_ControllerUiElements[num].m_OwningComponent == null))
			{
				m_ControllerUiElements.RemoveAt(num);
			}
		}
		if (uiModeEnable && mode != UIInputMode.Default)
		{
			m_ControllerUiElements.Add(new UIInputRequests
			{
				m_OwningComponent = owningComponent,
				m_Mode = mode
			});
		}
		UIInputMode currentUIInputMode2 = GetCurrentUIInputMode();
		if (currentUIInputMode != currentUIInputMode2)
		{
			SetUIInputMode(currentUIInputMode, currentUIInputMode2);
		}
	}

	private void SetUIInputMode(UIInputMode oldMode, UIInputMode newMode)
	{
		d.Log($"Switching input type to {newMode}  (from {oldMode})");
		if (oldMode == UIInputMode.FullscreenUI)
		{
			SetMapsEnabled(enable: false, ControllerType.Keyboard, UIInputMode.FullscreenUI);
			SetMapsEnabled(enable: true, ControllerType.Keyboard, UIInputMode.Default);
		}
		if (newMode == UIInputMode.FullscreenUI)
		{
			SetMapsEnabled(enable: false, ControllerType.Keyboard, UIInputMode.Default);
			SetMapsEnabled(enable: true, ControllerType.Keyboard, UIInputMode.FullscreenUI);
		}
		if (m_CurrentInputMode == InputMode.Gamepad)
		{
			SetMapsEnabled(enable: false, ControllerType.Joystick, oldMode);
			SetMapsEnabled(enable: true, ControllerType.Joystick, newMode);
			GamepadMapsChangedEvent.Send(newMode);
		}
	}

	public string GetTMPSpriteAssetName()
	{
		return m_GlyphDB.GetTMPSpriteAssetName();
	}

	public string GetTMPSpriteAssetName(ControllerType controllerType, Guid hardwareTypeGuid)
	{
		return m_GlyphDB.GetTMPSpriteAssetName(controllerType, hardwareTypeGuid);
	}

	public GamepadElementID FindButtonIDForAction(int rewiredAction)
	{
		ActionElementMap firstElementMapWithAction = m_Player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, rewiredAction, skipDisabledMaps: false);
		if (firstElementMapWithAction != null)
		{
			return (GamepadElementID)firstElementMapWithAction.elementIdentifierId;
		}
		d.LogErrorFormat("ManInput.FindButtonIDForAction : no joystick action element map found for rewired action {0}. Map might be disabled or not exist for joysticks", rewiredAction);
		return GamepadElementID.Invalid;
	}

	public int FindGlyphIDForJoystickElementID(int elementIdentifierId, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		return m_GlyphDB.GetSpriteID(elementIdentifierId, glyphType);
	}

	public int FindGlyphIDForJoystickElementID(ControllerType controllerType, Guid hardwareTypeGuid, int elementIdentifierId, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		return m_GlyphDB.GetSpriteID(controllerType, hardwareTypeGuid, elementIdentifierId, glyphType);
	}

	public int FindGlyphIDForAction(int rewiredAction, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		ActionElementMap firstElementMapWithAction = m_Player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, rewiredAction, skipDisabledMaps: false);
		if (firstElementMapWithAction != null)
		{
			return m_GlyphDB.GetSpriteID(firstElementMapWithAction.elementIdentifierId, glyphType);
		}
		d.LogErrorFormat("ManInput.FindGlyphIDForAction: no joystick action element map found for rewired action {0}. Map might be disabled or not exist for joysticks", rewiredAction);
		return -1;
	}

	public string FindKeyNameForAction(int rewiredAction, ControllerType controllerType)
	{
		ActionElementMap firstElementMapWithAction = m_Player.controllers.maps.GetFirstElementMapWithAction(controllerType, rewiredAction, skipDisabledMaps: false);
		if (firstElementMapWithAction == null && controllerType == ControllerType.Keyboard)
		{
			firstElementMapWithAction = m_Player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Mouse, rewiredAction, skipDisabledMaps: false);
		}
		if (firstElementMapWithAction != null)
		{
			return Singleton.Manager<Localisation>.inst.ActionElementMapToString(firstElementMapWithAction);
		}
		d.LogErrorFormat("ManInput.FindKeyNameForAction: no action element map found for rewired action {0}. Map might be disabled or not exist for {1}", rewiredAction, controllerType);
		return "";
	}

	public void RestoreDefaults()
	{
		IList<Player> players = ReInput.players.Players;
		for (int i = 0; i < players.Count; i++)
		{
			Player player = players[i];
			player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
			player.controllers.maps.LoadDefaultMaps(ControllerType.Keyboard);
			player.controllers.maps.LoadDefaultMaps(ControllerType.Mouse);
		}
	}

	public string SaveControllerMaps()
	{
		IList<Player> allPlayers = ReInput.players.AllPlayers;
		SaveData saveData = new SaveData
		{
			Version = m_InputMapVersion,
			PlayerData = new PlayerControllerSaveData[allPlayers.Count]
		};
		int num = -1;
		int num2 = 0;
		string text = string.Empty;
		for (int i = 0; i < allPlayers.Count; i++)
		{
			Player player = allPlayers[i];
			PlayerSaveData saveData2 = player.GetSaveData(userAssignableMapsOnly: true);
			try
			{
				num = -1;
				num2 = saveData2.inputBehaviors.Length;
				text = string.Empty;
				Dictionary<string, string> dictionary = new Dictionary<string, string>(num2);
				InputBehavior[] inputBehaviors = saveData2.inputBehaviors;
				foreach (InputBehavior inputBehavior in inputBehaviors)
				{
					num++;
					text = GetInputBehaviorKey(player, inputBehavior.id);
					string value = inputBehavior.ToXmlString();
					dictionary.Add(text, value);
				}
				saveData.PlayerData[i].InputBehavior = dictionary;
			}
			catch (Exception ex)
			{
				d.LogError("ManInput.SaveControllerMaps - Could not save Input Behaviours. " + ex.Message);
				d.LogErrorFormat("ManInput.SaveControllerMaps - Current state at failure playerID {0} key {1} index {2} length {3} version {4}", player.id, text, num, num2, SKU.ChangelistVersion);
				return null;
			}
			try
			{
				num = -1;
				num2 = saveData2.AllControllerMapSaveData.Count();
				text = string.Empty;
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(num2);
				foreach (ControllerMapSaveData allControllerMapSaveDatum in saveData2.AllControllerMapSaveData)
				{
					num++;
					text = GetControllerMapKey(player, allControllerMapSaveDatum.controllerType, allControllerMapSaveDatum.categoryId, allControllerMapSaveDatum.layoutId, allControllerMapSaveDatum.controller);
					string value2 = allControllerMapSaveDatum.map.ToXmlString();
					dictionary2.Add(text, value2);
				}
				saveData.PlayerData[i].ControllerMap = dictionary2;
			}
			catch (Exception ex2)
			{
				d.LogError("ManInput.SaveControllerMaps - Could not save Contorler Maps. " + ex2.Message);
				d.LogErrorFormat("ManInput.SaveControllerMaps - Current state at failure playerID {0} key {1} index {2} length {3} version {4}", player.id, text, num, num2, SKU.ChangelistVersion);
				return null;
			}
		}
		num = -1;
		num2 = ReInput.controllers.Joysticks.Count();
		text = string.Empty;
		try
		{
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>(num2);
			foreach (Joystick joystick in ReInput.controllers.Joysticks)
			{
				num++;
				text = GetJoystickCalibrationMapKey(joystick);
				string value3 = joystick.GetCalibrationMapSaveData().map.ToXmlString();
				if (!dictionary3.ContainsKey(text))
				{
					dictionary3.Add(text, value3);
				}
			}
			saveData.JoystickData = dictionary3;
		}
		catch (Exception ex3)
		{
			d.LogError("ManInput.SaveControllerMaps - Could not save Contorler Maps. " + ex3.Message);
			d.LogErrorFormat("ManInput.SaveControllerMaps - Current state at failure key {0} index {1} length {2} version {3}", text, num, num2, SKU.ChangelistVersion);
			return null;
		}
		return JsonConvert.SerializeObject(saveData);
	}

	public void LoadControllerMaps(string controllerData)
	{
		if (controllerData.NullOrEmpty())
		{
			d.LogError("ManInput.LoadControllerMaps, controllerData Null or Empty");
			return;
		}
		try
		{
			SaveData saveData;
			try
			{
				saveData = JsonConvert.DeserializeObject<SaveData>(controllerData);
			}
			catch (Exception ex)
			{
				d.LogError("ManInput.LoadControllerMaps could not parse json string. Aborting " + ex.Message);
				return;
			}
			if (saveData.Version < m_InputMapVersion)
			{
				d.LogFormat("ManInput.LoadControllerMaps - controller bindings out of date - reverting to defaults. Version: current {0} loaded {1}", m_InputMapVersion, saveData.Version);
				return;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (i < saveData.PlayerData.Length)
				{
					PlayerControllerSaveData playerControllerSaveData = saveData.PlayerData[i];
					d.Assert(playerControllerSaveData.ControllerMap != null, "ManInput.LoadControllerMaps playerSaveData ControllerMap lookup is null. This will probalby cause severe errors");
					d.Assert(playerControllerSaveData.InputBehavior != null, "ManInput.LoadControllerMaps playerSaveData InputBehaviour lookup is null. This will probalby cause severe errors");
					IList<InputBehavior> inputBehaviors = ReInput.mapping.GetInputBehaviors(player.id);
					for (int j = 0; j < inputBehaviors.Count; j++)
					{
						string inputBehaviorKey = GetInputBehaviorKey(player, inputBehaviors[j].id);
						if (playerControllerSaveData.InputBehavior.TryGetValue(inputBehaviorKey, out var value) && value != null && value != string.Empty)
						{
							inputBehaviors[j].ImportXmlString(value);
						}
					}
					List<string> allControllerMapsXml = GetAllControllerMapsXml(player, userAssignableMapsOnly: true, ControllerType.Keyboard, ReInput.controllers.Keyboard, playerControllerSaveData.ControllerMap);
					List<string> allControllerMapsXml2 = GetAllControllerMapsXml(player, userAssignableMapsOnly: true, ControllerType.Mouse, ReInput.controllers.Mouse, playerControllerSaveData.ControllerMap);
					bool flag = false;
					List<List<string>> list = new List<List<string>>();
					foreach (Joystick joystick in player.controllers.Joysticks)
					{
						List<string> allControllerMapsXml3 = GetAllControllerMapsXml(player, userAssignableMapsOnly: true, ControllerType.Joystick, joystick, playerControllerSaveData.ControllerMap);
						list.Add(allControllerMapsXml3);
						if (allControllerMapsXml3.Count > 0)
						{
							flag = true;
						}
					}
					if (allControllerMapsXml.Count > 0)
					{
						player.controllers.maps.ClearMaps(ControllerType.Keyboard, userAssignableOnly: true);
						player.controllers.maps.LoadDefaultMaps(ControllerType.Keyboard);
						player.controllers.maps.AddMapsFromXml(ControllerType.Keyboard, 0, allControllerMapsXml);
						HashSet<int> hashSet = new HashSet<int>();
						foreach (ControllerMap map in player.controllers.maps.GetMaps(ControllerType.Keyboard, 0))
						{
							hashSet.Clear();
							ActionElementMap[] elementMaps = map.GetElementMaps();
							foreach (ActionElementMap actionElementMap in elementMaps)
							{
								hashSet.Add(actionElementMap.actionId);
							}
							elementMaps = ReInput.mapping.GetKeyboardMapInstance(map.categoryId, map.layoutId).GetElementMaps();
							foreach (ActionElementMap actionElementMap2 in elementMaps)
							{
								if (!hashSet.Contains(actionElementMap2.actionId))
								{
									map.CreateElementMap(actionElementMap2.actionId, actionElementMap2.axisContribution, actionElementMap2.keyCode, actionElementMap2.modifierKeyFlags);
								}
							}
						}
					}
					if (flag)
					{
						player.controllers.maps.ClearMaps(ControllerType.Joystick, userAssignableOnly: true);
					}
					int num = 0;
					foreach (Joystick joystick2 in player.controllers.Joysticks)
					{
						player.controllers.maps.AddMapsFromXml(ControllerType.Joystick, joystick2.id, list[num]);
						num++;
					}
					if (allControllerMapsXml2.Count <= 0)
					{
						continue;
					}
					player.controllers.maps.ClearMaps(ControllerType.Mouse, userAssignableOnly: true);
					player.controllers.maps.LoadDefaultMaps(ControllerType.Mouse);
					player.controllers.maps.AddMapsFromXml(ControllerType.Mouse, 0, allControllerMapsXml2);
					HashSet<int> hashSet2 = new HashSet<int>();
					foreach (ControllerMap map2 in player.controllers.maps.GetMaps(ControllerType.Mouse, 0))
					{
						hashSet2.Clear();
						ActionElementMap[] elementMaps = map2.GetElementMaps();
						foreach (ActionElementMap actionElementMap3 in elementMaps)
						{
							hashSet2.Add(actionElementMap3.actionId);
						}
						elementMaps = ReInput.mapping.GetMouseMapInstance(map2.categoryId, map2.layoutId).GetElementMaps();
						foreach (ActionElementMap actionElementMap4 in elementMaps)
						{
							if (!hashSet2.Contains(actionElementMap4.actionId))
							{
								map2.CreateElementMap(actionElementMap4.actionId, actionElementMap4.axisContribution, actionElementMap4.elementIdentifierId, actionElementMap4.elementType, actionElementMap4.axisRange, actionElementMap4.invert);
							}
						}
					}
				}
				else
				{
					d.LogWarning("ManInput.LoadControllerMaps - No saved data found, skipping playerID " + player.id);
				}
			}
			foreach (Joystick joystick3 in ReInput.controllers.Joysticks)
			{
				string joystickCalibrationMapKey = GetJoystickCalibrationMapKey(joystick3);
				if (saveData.JoystickData.TryGetValue(joystickCalibrationMapKey, out var value2))
				{
					joystick3.ImportCalibrationMapFromXmlString(value2);
				}
			}
		}
		catch (Exception ex2)
		{
			d.LogError("Error Loading Controller Maps - " + ex2);
		}
	}

	public void SetUseConsoleStyleJoypad(bool useGamepad)
	{
		if (CanSwitchInputModeAtRuntime())
		{
			SetInputModeAndUpdateControllerMaps(useGamepad);
		}
	}

	public bool IsGamepadUseEnabled()
	{
		return m_CurrentInputMode == InputMode.Gamepad;
	}

	public bool IsCurrentlyUsingGamepad()
	{
		return m_CurrentPlayerInputDevice == InputMode.Gamepad;
	}

	[Conditional("UNITY_EDITOR")]
	public void SetEmulatedUserEngagementDone()
	{
		m_HasDoneEmulatedUserEngagement = true;
	}

	public bool UseUserEngagementScreen()
	{
		return false;
	}

	public bool CanSwitchInputModeAtRuntime()
	{
		return m_CanSwitchInputModeAtRuntime;
	}

	private void SetInputModeAndUpdateControllerMaps(bool useGamepad)
	{
		SetMapsEnabled(enable: false, ControllerType.Joystick, GetCurrentUIInputMode());
		SetInputMode((!useGamepad) ? InputMode.KeyboardAndMouse : InputMode.Gamepad);
		SetMapsEnabled(enable: true, ControllerType.Joystick, GetCurrentUIInputMode());
	}

	private void SetInputMode(InputMode inputMode)
	{
		if (m_CurrentInputMode != inputMode)
		{
			m_CurrentInputMode = inputMode;
			m_CurrentPlayerInputDevice = m_CurrentInputMode;
			if (m_CurrentInputMode == InputMode.Gamepad && m_Player.controllers.joystickCount > 0)
			{
				Joystick joystick = m_Player.controllers.Joysticks[0];
				m_GlyphDB.SetPlatform(joystick.type, joystick.hardwareTypeGuid);
			}
			else
			{
				m_GlyphDB.SetPlatform(ControllerType.Keyboard, Guid.Empty);
			}
			OnInputModeChanged.Send(m_CurrentInputMode);
		}
	}

	private string GetBaseKey(Player player)
	{
		return "&playerName=" + player.name;
	}

	private string GetControllerMapKey(Player player, ControllerType controllerType, int categoryId, int layoutId, Controller controller)
	{
		string baseKey = GetBaseKey(player);
		baseKey += "&dataType=ControllerMap";
		baseKey = baseKey + "&controllerMapType=" + controller.mapTypeString;
		baseKey = baseKey + "&categoryId=" + categoryId;
		baseKey = baseKey + "&layoutId=" + layoutId;
		baseKey = baseKey + "&hardwareIdentifier=" + controller.hardwareIdentifier;
		if (controllerType == ControllerType.Joystick)
		{
			Joystick joystick = (Joystick)controller;
			baseKey = baseKey + "&hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}
		return baseKey;
	}

	private List<string> GetAllControllerMapsXml(Player player, bool userAssignableMapsOnly, ControllerType controllerType, Controller controller, Dictionary<string, string> dictionary)
	{
		List<string> list = null;
		if (dictionary != null)
		{
			List<string> list2 = new List<string>();
			IList<InputMapCategory> mapCategories = ReInput.mapping.MapCategories;
			for (int i = 0; i < mapCategories.Count; i++)
			{
				InputMapCategory inputMapCategory = mapCategories[i];
				if (userAssignableMapsOnly && !inputMapCategory.userAssignable)
				{
					continue;
				}
				IList<InputLayout> list3 = ReInput.mapping.MapLayouts(controllerType);
				for (int j = 0; j < list3.Count; j++)
				{
					InputLayout inputLayout = list3[j];
					string controllerMapKey = GetControllerMapKey(player, controllerType, inputMapCategory.id, inputLayout.id, controller);
					if (controllerMapKey != string.Empty)
					{
						list2.Add(controllerMapKey);
					}
				}
			}
			list = new List<string>(list2.Count);
			for (int k = 0; k < list2.Count; k++)
			{
				if (dictionary.TryGetValue(list2[k], out var value))
				{
					list.Add(value);
				}
			}
		}
		return list;
	}

	private string GetJoystickCalibrationMapKey(Joystick joystick)
	{
		return string.Concat(string.Concat("&dataType=CalibrationMap" + "&controllerType=" + joystick.type, "&hardwareIdentifier=", joystick.hardwareIdentifier), "&hardwareGuid=", joystick.hardwareTypeGuid.ToString());
	}

	private string GetInputBehaviorKey(Player player, int id)
	{
		return string.Concat(GetBaseKey(player) + "&dataType=InputBehavior", "&id=", id);
	}

	public IRadialInputController GetRadialInputController(RadialInputController radialInputController)
	{
		return radialInputController switch
		{
			RadialInputController.Mouse => m_MouseInputRadialController, 
			RadialInputController.Gamepad => m_GamepadInputRadialController, 
			_ => null, 
		};
	}

	private bool PollForInput(ElementAssignmentChange entry, InputMode inputMode = InputMode.KeyboardAndMouse)
	{
		bool flag = false;
		if ((inputMode & InputMode.KeyboardAndMouse) > InputMode.None)
		{
			if (PollKeyboardForAssignment(entry))
			{
				flag = true;
			}
			else if (PollMouseForAssignment(entry))
			{
				flag = true;
			}
		}
		if (!flag && (inputMode & InputMode.Gamepad) > InputMode.None && PollJoystickForAssignment(entry))
		{
			flag = true;
		}
		if (flag)
		{
			return true;
		}
		return false;
	}

	private bool PollKeyboardForAssignment(ElementAssignmentChange entry)
	{
		foreach (ControllerPollingInfo item in ReInput.controllers.Keyboard.PollForAllKeys())
		{
			KeyCode keyboardKey = item.keyboardKey;
			d.Log(string.Concat("Key pressed ", keyboardKey, " -- elementType=", item.elementType, " elementIdentifierId = ", item.elementIdentifierId, " elementIndex=", item.elementIndex));
			if (keyboardKey != KeyCode.None)
			{
				entry.controllerType = ControllerType.Keyboard;
				entry.pollingInfo = item;
				entry.invalidKey = m_InvalidKeys.Contains(keyboardKey);
				return true;
			}
		}
		return false;
	}

	private bool PollJoystickForAssignment(ElementAssignmentChange entry)
	{
		entry.pollingInfo = m_Player.controllers.polling.PollControllerForFirstElementDown(ControllerType.Joystick, m_ControllerID);
		if (entry.pollingInfo.success)
		{
			entry.controllerType = ControllerType.Joystick;
			_ = entry.actionType;
			return true;
		}
		return false;
	}

	private bool PollMouseForAssignment(ElementAssignmentChange entry)
	{
		ControllerPollingInfo pollingInfo = m_Player.controllers.polling.PollControllerForFirstButtonDown(ControllerType.Mouse, m_ControllerID);
		if (pollingInfo.success && pollingInfo.elementIndex > 1)
		{
			entry.pollingInfo = pollingInfo;
			entry.controllerType = ControllerType.Mouse;
			return true;
		}
		return false;
	}

	public bool PollPlayerNewJoystick()
	{
		if (!ReInput.isReady)
		{
			return false;
		}
		IList<Joystick> joysticks = ReInput.controllers.Joysticks;
		for (int i = 0; i < joysticks.Count; i++)
		{
			Joystick joystick = joysticks[i];
			if (!ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id) && joystick.GetAnyButtonDown())
			{
				d.Assert(m_Player != null, "ManInput.PollForNewPlayerJoystick. m_player is null. Race condition?");
				m_Player.controllers.ClearAllControllers();
				m_Player.controllers.AddController(joystick, removeFromOtherPlayers: true);
				SetMapsEnabled(enable: true, ControllerType.Joystick, GetCurrentUIInputMode());
				return true;
			}
		}
		return false;
	}

	public void ReassignController()
	{
		d.Log("[ManInput] assigning the gamepad " + ReInput.controllers.Joysticks[0].name + " and ID: " + ReInput.controllers.Joysticks[0].id);
		m_Player.controllers.AddController(ReInput.controllers.Joysticks[0], removeFromOtherPlayers: true);
		SetMapsEnabled(enable: true, ControllerType.Joystick, GetCurrentUIInputMode());
	}

	public bool ReassignControllerWithID(int id)
	{
		foreach (Joystick joystick in ReInput.controllers.Joysticks)
		{
			d.Log("[ManInput] ReassignController found gamepad " + joystick.name + " with ID: " + joystick.id + " (looking for ID=" + id + ")");
			if (id == joystick.id)
			{
				m_Player.controllers.ClearAllControllers();
				m_Player.controllers.AddController(joystick, removeFromOtherPlayers: true);
				SetMapsEnabled(enable: true, ControllerType.Joystick, GetCurrentUIInputMode());
				return true;
			}
		}
		d.Log("[ManInput] ReassignController found no matching gamepad for id = " + id);
		return false;
	}

	public int GetPlayerControllerCount()
	{
		return m_Player.controllers.joystickCount;
	}

	private void FlipControllerButtonsIfNecessary(Controller controller)
	{
		bool flipEnterCancelButtons = SKU.FlipEnterCancelButtons;
		bool flipExtraButtons = SKU.FlipExtraButtons;
		if ((!flipEnterCancelButtons && !flipExtraButtons) || controller.type != ControllerType.Joystick || m_FlippedControllers.Contains(controller))
		{
			return;
		}
		m_FlippedControllers.Add(controller);
		ControllerMap map = m_Player.controllers.maps.GetMap(controller, 2, 0);
		int elementIdentifierId = map.GetFirstButtonMapWithAction(57, skipDisabledMaps: false).elementIdentifierId;
		int elementIdentifierId2 = map.GetFirstButtonMapWithAction(58, skipDisabledMaps: false).elementIdentifierId;
		int elementIdentifierId3 = map.GetFirstButtonMapWithAction(21, skipDisabledMaps: false).elementIdentifierId;
		int elementIdentifierId4 = map.GetFirstButtonMapWithAction(22, skipDisabledMaps: false).elementIdentifierId;
		d.Log($"FlipButtonsForSwitch maps for {controller.name} = {m_Player.controllers.maps.GetMaps(controller).Count()} type = {controller.type} button IDs: {elementIdentifierId3}, {elementIdentifierId4}; {elementIdentifierId}, {elementIdentifierId2}");
		foreach (ControllerMap map2 in m_Player.controllers.maps.GetMaps(controller))
		{
			Dictionary<int, ActionElementMap> dictionary = new Dictionary<int, ActionElementMap>();
			foreach (ActionElementMap buttonMap in map2.ButtonMaps)
			{
				if (buttonMap.elementType == ControllerElementType.Button)
				{
					dictionary[buttonMap.elementIdentifierId] = buttonMap;
				}
			}
			if (flipExtraButtons)
			{
				RemapButtonForFlip(dictionary, map2, elementIdentifierId, elementIdentifierId2);
				RemapButtonForFlip(dictionary, map2, elementIdentifierId2, elementIdentifierId);
			}
			if (flipEnterCancelButtons)
			{
				RemapButtonForFlip(dictionary, map2, elementIdentifierId3, elementIdentifierId4);
				RemapButtonForFlip(dictionary, map2, elementIdentifierId4, elementIdentifierId3);
			}
		}
	}

	private void RemapButtonForFlip(Dictionary<int, ActionElementMap> buttons, ControllerMap controllerMap, int originalButtonID, int remappedID)
	{
		if (buttons.TryGetValue(originalButtonID, out var value))
		{
			ElementAssignment elementAssignment = ElementAssignment.ButtonAssignment(remappedID, value.actionId, value.axisContribution);
			controllerMap.DeleteElementMap(value.id);
			if (!controllerMap.CreateElementMap(elementAssignment, out var _))
			{
				d.LogWarning($"FlipButtonsForSwitch category:{controllerMap.categoryId} Failed to replace mapping for {elementAssignment.elementIdentifierId}");
			}
		}
	}

	public List<ControllerMap> GetAllRebindableMaps()
	{
		List<ControllerMap> list = new List<ControllerMap>();
		list.AddRange(GetAllRebindableMaps(ControllerType.Keyboard));
		list.AddRange(GetAllRebindableMaps(ControllerType.Mouse));
		return list;
	}

	public List<ControllerMap> GetAllRebindableMaps(ControllerType controllerType)
	{
		List<ControllerMap> list = new List<ControllerMap>();
		foreach (ControllerMap allMap in m_Player.controllers.maps.GetAllMaps(controllerType))
		{
			if (allMap.categoryId != 2)
			{
				list.Add(allMap);
			}
		}
		return list;
	}

	private void SetMapsEnabled(bool enable, ControllerType controllerType, UIInputMode mode)
	{
		if (mode >= UIInputMode.Default && (int)mode < m_MapCategoryForInputMode.Length && m_MapCategoryForInputMode[(int)mode] != -1)
		{
			if (m_Player != null)
			{
				m_Player.controllers.maps.SetMapsEnabled(enable, controllerType, m_MapCategoryForInputMode[(int)mode]);
			}
		}
		else
		{
			d.LogError("No input category found for " + mode, this);
		}
	}

	private void OnModeSwitch()
	{
		UIInputMode currentUIInputMode = GetCurrentUIInputMode();
		if (m_ControllerUiElements.Count > 0)
		{
			string text = null;
			for (int num = m_ControllerUiElements.Count - 1; num >= 0; num--)
			{
				UIInputRequests uIInputRequests = m_ControllerUiElements[num];
				bool flag = uIInputRequests.m_Mode == UIInputMode.FullscreenUI && (uIInputRequests.m_OwningComponent is UIScreen || uIInputRequests.m_OwningComponent is ManUI || uIInputRequests.m_OwningComponent is ManPauseGame);
				if (uIInputRequests.m_OwningComponent == null || !flag)
				{
					if (text == null)
					{
						text = "The following components still have a UI input mode set when switching modes! (These will be cleared now) : ";
					}
					text += string.Format("'{0}' with mode '{1}'\n", uIInputRequests.m_OwningComponent ? (uIInputRequests.m_OwningComponent.name + " (Type=" + uIInputRequests.m_OwningComponent.GetType().Name + ")") : "null", uIInputRequests.m_Mode.ToString());
					m_ControllerUiElements.RemoveAt(num);
				}
			}
			if (text != null)
			{
				d.LogError(text);
			}
		}
		UIInputMode currentUIInputMode2 = GetCurrentUIInputMode();
		if (currentUIInputMode2 != currentUIInputMode)
		{
			SetUIInputMode(currentUIInputMode, currentUIInputMode2);
		}
	}

	private void OnPlayerActiveControllerChanged(Player player, Controller controller)
	{
		d.Assert(player == m_Player, "OnPlayerActiveControllerChanged - Unexpected player passed in.");
		InputMode inputMode = ((controller != null) ? ((controller.type != ControllerType.Joystick) ? InputMode.KeyboardAndMouse : InputMode.Gamepad) : InputMode.None);
		if (m_CurrentPlayerInputDevice != inputMode)
		{
			d.LogFormat("OnPlayerActiveControllerChanged {0}", controller?.type.ToString());
		}
		if (controller != null)
		{
			if (IsGamepadUseEnabled() && controller.type == ControllerType.Joystick)
			{
				m_CurrentPlayerInputDevice = InputMode.Gamepad;
				FlipControllerButtonsIfNecessary(controller);
			}
			else
			{
				m_CurrentPlayerInputDevice = InputMode.KeyboardAndMouse;
			}
			LastActiveControllerTypeChangedEvent.Send(controller.type);
		}
	}

	private void OnControllerConnected(ControllerStatusChangedEventArgs evtArgs)
	{
		m_Player.controllers.RemoveLastActiveControllerChangedDelegate(OnPlayerActiveControllerChanged);
		m_Player.controllers.AddLastActiveControllerChangedDelegate(OnPlayerActiveControllerChanged);
		d.Log($"ManInput.OnControllerConnected controllerType={evtArgs.controllerType} controllerId={evtArgs.controllerId}");
	}

	private void OnControllerPreDisconnect(ControllerStatusChangedEventArgs evtArgs)
	{
		if (!SKU.ConsoleUI || evtArgs.controllerType != ControllerType.Joystick)
		{
			return;
		}
		for (int i = 0; i < m_Player.controllers.joystickCount; i++)
		{
			Joystick joystick = m_Player.controllers.Joysticks[i];
			if (joystick == null || joystick.id != evtArgs.controllerId)
			{
				continue;
			}
			m_Player.controllers.RemoveLastActiveControllerChangedDelegate(OnPlayerActiveControllerChanged);
			if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen))
			{
				UIScreenNetworkLobby uIScreenNetworkLobby = (UIScreenNetworkLobby)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen);
				if (uIScreenNetworkLobby != null)
				{
					uIScreenNetworkLobby.HiddenByGamePadDisconnect(hide: true);
				}
			}
			bool flag = true;
			if (true)
			{
				UIScreenControllerDisconnected screen = (UIScreenControllerDisconnected)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.ControllerDisconnected);
				Singleton.Manager<ManUI>.inst.GoToScreen(screen, flag ? ManUI.PauseType.Pause : ManUI.PauseType.None);
			}
			break;
		}
	}

	private void Awake()
	{
		d.Assert(ReInput.isReady, "ManInput.Awake - ReInput.isReady returned false. This possibly means the script execution order has changed or some other race condition has been introduced");
		m_MapCategoryForInputMode = new int[m_InputMapCategoryForMode.Length];
		for (int i = 0; i < m_InputMapCategoryForMode.Length; i++)
		{
			m_MapCategoryForInputMode[i] = -1;
			if (string.IsNullOrEmpty(m_InputMapCategoryForMode[i]))
			{
				d.LogError("InputMapCategoryForMode has an empty entry in position " + i);
				continue;
			}
			m_MapCategoryForInputMode[i] = ReInput.mapping.GetMapCategoryId(m_InputMapCategoryForMode[i]);
			if (m_MapCategoryForInputMode[i] == -1)
			{
				d.LogError($"InputMapCategoryForMode: {i} ({(UIInputMode)i} -> {m_InputMapCategoryForMode[i]}) doesn't match a controller map name");
			}
		}
		foreach (InputMapCategory mapCategory in ReInput.mapping.MapCategories)
		{
			m_CategoryNameLookup.Add(mapCategory.id, mapCategory.name);
		}
		m_Player = ReInput.players.GetPlayer(0);
		if (UseUserEngagementScreen())
		{
			ReInput.configuration.autoAssignJoysticks = false;
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int j = 0; j < joysticks.Count; j++)
			{
				int id = joysticks[j].id;
				ReInput.controllers.RemoveJoystickFromAllPlayers(id);
			}
		}
		m_Player.controllers.AddLastActiveControllerChangedDelegate(OnPlayerActiveControllerChanged);
		ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
		InputMode inputMode = InputMode.KeyboardAndMouse;
		SetInputMode(inputMode);
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Subscribe(OnModeSwitch);
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
		if (m_CheckKeyAssign && PollForInput(m_ElementAssignmentChange))
		{
			KeyAssignmentEvent.Send(m_ElementAssignmentChange);
			m_CheckKeyAssign = false;
		}
		m_MouseInputRadialController.Update();
		m_GamepadInputRadialController.Update();
	}

	public ControllerMap GetControllerMapFor(ControllerType controllerType, int actionId)
	{
		foreach (ControllerMap allRebindableMap in GetAllRebindableMaps(controllerType))
		{
			foreach (ActionElementMap allMap in allRebindableMap.AllMaps)
			{
				if (allMap.actionId == actionId)
				{
					return allRebindableMap;
				}
			}
		}
		return m_Player.controllers.maps.GetFirstMapInCategory(controllerType, 0, 0);
	}
}
