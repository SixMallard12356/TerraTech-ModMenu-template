#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Rewired;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Input_KeyBind_FromInput : ModuleCircuit_Input_KeyBind, IHUDContextControlFieldModel
{
	private enum InputMethod
	{
		ControllerElement
	}

	[Serializable]
	public struct InputElement
	{
		public ControllerType controllerType;

		public string hardwareTypeGuidString;

		public ControllerElementType elementType;

		public int elementIdentifierId;

		public Pole elementAxisPole;

		public string elementIdentifierName;

		public int elementIndex;

		public KeyCode keyboardKey;

		[JsonIgnore]
		public Guid hardwareTypeGuid
		{
			get
			{
				return Guid.Parse(hardwareTypeGuidString);
			}
			set
			{
				hardwareTypeGuidString = value.ToString();
			}
		}
	}

	[Serializable]
	public struct FontSizeOptions
	{
		public bool autoSize;

		[InspectorVisibilityControl("autoSize", false, InspectorVisibilityControlAttribute.ComparisonType.Equals)]
		public float size;

		[SerializeField]
		[InspectorVisibilityControl("autoSize", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
		private MinMaxFloat _size;

		public MinMaxFloat sizeMinMax => _size;

		public void ApplyTo(TextMeshPro tmpField)
		{
			tmpField.enableAutoSizing = autoSize;
			if (autoSize)
			{
				tmpField.fontSizeMin = _size.Min;
				tmpField.fontSizeMax = _size.Max;
			}
			else
			{
				tmpField.fontSize = size;
			}
		}
	}

	public enum CharContent
	{
		SingleCharacter,
		Text,
		Glyph
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public InputElement inputElement;
	}

	[SerializeField]
	[RewiredAction]
	private int m_DefaultActionID;

	[Header("Display")]
	[SerializeField]
	private TextMeshPro m_BoundKeyDisplay;

	[SerializeField]
	[EnumArray(typeof(CharContent))]
	private FontSizeOptions[] m_FontSizeOptions;

	private static ModuleCircuit_Input_KeyBind_FromInput s_ModulePollingForInput = null;

	private static Action<bool, InputElement> s_PollCompleteEvent = null;

	private bool m_KeyWasPressed;

	private bool m_SuppressKeyDown;

	private InputElement m_SelectedInput;

	private NetworkedProperty<InputElementBlockMessage> m_NetworkedProp;

	private static readonly HashSet<int> k_ExcludedElementIds = new HashSet<int>
	{
		GetElementHash(ControllerType.Mouse, 0),
		GetElementHash(ControllerType.Mouse, 1),
		GetElementHash(ControllerType.Mouse, 3),
		GetElementHash(ControllerType.Mouse, 4),
		GetElementHash(ControllerType.Keyboard, 60),
		GetElementHash(ControllerType.Joystick, 13)
	};

	private static readonly HashSet<string> k_ExcludedGamepadElementNames = new HashSet<string> { "start".ToLowerInvariant() };

	public InputElement CurrentAssignedInput => m_SelectedInput;

	public string CurrentAssignedDisplay => m_BoundKeyDisplay.text;

	Type IHUDContextControlFieldModel.BlockContextFieldType => typeof(UIBlockContext_InputAssignment);

	public void StartPollingForInput(Action<bool, InputElement> pollCompleteEvent)
	{
		d.AssertFormat(s_PollCompleteEvent == null && s_ModulePollingForInput == null, this, "Started polling for input on '{0}' when already doing just that for {1} ?!", base.name, s_ModulePollingForInput?.name);
		s_ModulePollingForInput = this;
		s_PollCompleteEvent = pollCompleteEvent;
		base.block.BlockUpdate.Subscribe(BlockUpdate_PollForAssignment);
	}

	public void CancelPollingForInput(bool notifyListeners = true)
	{
		if ((object)s_ModulePollingForInput == this)
		{
			base.block.BlockUpdate.Unsubscribe(BlockUpdate_PollForAssignment);
			if (notifyListeners)
			{
				bool arg = false;
				s_PollCompleteEvent(arg, default(InputElement));
			}
			s_ModulePollingForInput = null;
			s_PollCompleteEvent = null;
		}
	}

	public void AssignElement(InputElement inputElement, bool netSend = true)
	{
		m_SelectedInput = inputElement;
		SetRewiredActionID(m_SelectedInput.elementIdentifierId);
		string displayString = GetDisplayString(m_SelectedInput);
		m_BoundKeyDisplay.SetText(displayString);
		CharContent charContent = CharContent.Text;
		if (displayString.Length == 1)
		{
			charContent = CharContent.SingleCharacter;
		}
		else if (displayString.StartsWith("<sprite="))
		{
			charContent = CharContent.Glyph;
		}
		FontSizeOptions fontSizeOptions = m_FontSizeOptions[(int)charContent];
		fontSizeOptions.ApplyTo(m_BoundKeyDisplay);
		m_SuppressKeyDown = true;
		m_NetworkedProp.Data.value = m_SelectedInput;
		if (netSend)
		{
			m_NetworkedProp.Sync();
		}
	}

	public string GetDisplayString(InputElement inputElement)
	{
		string text = Singleton.Manager<Localisation>.inst.ElementIdentifierIdToString(inputElement.controllerType, inputElement.hardwareTypeGuid, inputElement.elementIdentifierId, inputElement.elementIdentifierName, inputElement.elementType, inputElement.elementIndex, inputElement.keyboardKey);
		if (text.NullOrEmpty())
		{
			text = inputElement.elementIdentifierName;
		}
		if (text.StartsWith("Numpad ", StringComparison.InvariantCultureIgnoreCase))
		{
			text = text.Substring("Numpad ".Length);
		}
		return text;
	}

	protected override void EnableKeyHandler_Internal(bool enable, int actionID)
	{
		if (actionID != int.MinValue)
		{
			SetButtonPressListenerEnabled(enable);
			if (!enable)
			{
				m_KeyWasPressed = false;
			}
		}
	}

	private void SetButtonPressListenerEnabled(bool enable)
	{
		if (enable)
		{
			base.block.BlockUpdate.Subscribe(BlockUpdate_ListenForButtonPress);
		}
		else
		{
			base.block.BlockUpdate.Unsubscribe(BlockUpdate_ListenForButtonPress);
		}
	}

	private bool TryGetValidControllerElement(ControllerPollingInfo pollingInfo, out InputElement polledForElement)
	{
		polledForElement = default(InputElement);
		if (k_ExcludedElementIds.Contains(GetElementHash(pollingInfo.controllerType, pollingInfo.elementIdentifierId)))
		{
			return false;
		}
		if (pollingInfo.controllerType == ControllerType.Joystick && k_ExcludedGamepadElementNames.Contains(pollingInfo.elementIdentifierName.ToLowerInvariant()))
		{
			return false;
		}
		polledForElement = new InputElement
		{
			controllerType = pollingInfo.controllerType,
			hardwareTypeGuid = ((pollingInfo.controller is Joystick joystick) ? joystick.hardwareTypeGuid : Guid.Empty),
			elementType = pollingInfo.elementType,
			elementIdentifierId = pollingInfo.elementIdentifierId,
			elementAxisPole = pollingInfo.axisPole,
			elementIdentifierName = pollingInfo.elementIdentifierName,
			elementIndex = pollingInfo.elementIndex,
			keyboardKey = pollingInfo.keyboardKey
		};
		return true;
	}

	private bool TryGetControllerElementForRewiredAction(int rewiredAction, out InputElement controllerElement)
	{
		ActionElementMap rewiredActionElement = Singleton.Manager<ManInput>.inst.GetRewiredActionElement(rewiredAction, Pole.Positive);
		if (rewiredActionElement != null)
		{
			controllerElement = new InputElement
			{
				controllerType = rewiredActionElement.controllerMap.controllerType,
				hardwareTypeGuid = ((rewiredActionElement.controllerMap.controller is Joystick joystick) ? joystick.hardwareTypeGuid : Guid.Empty),
				elementType = rewiredActionElement.elementType,
				elementIdentifierId = rewiredActionElement.elementIdentifierId,
				elementAxisPole = Pole.Positive,
				elementIdentifierName = rewiredActionElement.elementIdentifierName,
				elementIndex = rewiredActionElement.elementIndex,
				keyboardKey = rewiredActionElement.keyCode
			};
			return true;
		}
		controllerElement = default(InputElement);
		return false;
	}

	private bool ShouldProcessInput()
	{
		if (Singleton.Manager<ManInput>.inst.GetCurrentUIInputMode() == UIInputMode.Default && Singleton.Manager<ManUI>.inst.IsStackEmpty() && !Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking))
		{
			return !Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
		}
		return false;
	}

	private static int GetElementHash(ControllerType controllerType, int elementIdentifierId)
	{
		return ((int)controllerType << 30) | (elementIdentifierId & -1);
	}

	private void OnBlockDetached()
	{
		CancelPollingForInput();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec context)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				inputElement = m_SelectedInput
			};
			serialData.Store(context.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(context.saveState);
		if (serialData != null)
		{
			AssignElement(serialData.inputElement, netSend: false);
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		string strVal2;
		if (saving)
		{
			string strVal = JsonConvert.SerializeObject(m_SelectedInput, TankPreset.s_SerializationSettings);
			context.Store(this, "InputElement_json", strVal);
		}
		else if (context.TryRetrieve(this, "InputElement_json", out strVal2, null))
		{
			InputElement inputElement = JsonConvert.DeserializeObject<InputElement>(strVal2, TankPreset.s_SerializationSettings);
			AssignElement(inputElement, netSend: false);
		}
	}

	private void OnNetworkedValueChanged(InputElementBlockMessage msg)
	{
		AssignElement(msg.value, netSend: false);
	}

	private void OnPool()
	{
		base.block.DetachedEvent.Subscribe(OnBlockDetached);
		base.block.serializeEvent.Subscribe(OnSerialze);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		m_NetworkedProp = new NetworkedProperty<InputElementBlockMessage>(this, TTMsgType.InputElementSync, OnNetworkedValueChanged);
	}

	private void OnSpawn()
	{
		if (TryGetControllerElementForRewiredAction(m_DefaultActionID, out var controllerElement))
		{
			AssignElement(controllerElement, netSend: false);
		}
		else
		{
			d.LogError("Was unable to map RewiredAction ID '{0}' to a valid Controller Element... How ?");
		}
	}

	private void OnRecycle()
	{
		d.AssertFormat((object)s_ModulePollingForInput != this, this, "Recycling '{0}' but was still polling for input?!", base.name);
		CancelPollingForInput();
	}

	private void BlockUpdate_PollForAssignment()
	{
		foreach (Controller controller in ReInput.players.GetPlayer(0).controllers.Controllers)
		{
			ControllerPollingInfo pollingInfo = controller.PollForFirstElementDown();
			if (pollingInfo.success && TryGetValidControllerElement(pollingInfo, out var polledForElement))
			{
				bool arg = true;
				s_PollCompleteEvent(arg, polledForElement);
				break;
			}
		}
	}

	private void BlockUpdate_ListenForButtonPress()
	{
		bool flag = false;
		if (ShouldProcessInput())
		{
			foreach (Controller controller in ReInput.players.GetPlayer(0).controllers.Controllers)
			{
				if (controller.type != m_SelectedInput.controllerType)
				{
					continue;
				}
				switch (m_SelectedInput.elementType)
				{
				default:
					if (controller.GetButtonById(m_SelectedInput.elementIdentifierId))
					{
						flag = true;
					}
					break;
				case ControllerElementType.Axis:
					if (controller.GetElementById(m_SelectedInput.elementIdentifierId) is Controller.Axis axis && Mathf.Abs(axis.value) > 0.2f && axis.value > 0f == (m_SelectedInput.elementAxisPole == Pole.Positive))
					{
						flag = true;
					}
					break;
				case ControllerElementType.CompoundElement:
					d.LogError("Compound element type not yet supported!");
					break;
				}
			}
		}
		if (!m_SuppressKeyDown && m_KeyWasPressed != flag)
		{
			if (flag)
			{
				HandleKeyPressed();
			}
			else
			{
				HandleKeyReleased();
			}
			m_KeyWasPressed = flag;
		}
		if (m_SuppressKeyDown && !flag)
		{
			m_SuppressKeyDown = false;
		}
	}

	public override NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Input_KeyBind_Expanded;
	}

	public override void OnSerialize(NetworkWriter writer)
	{
		base.OnSerialize(writer);
		m_NetworkedProp.Serialise(writer);
	}

	public override void OnDeserialize(NetworkReader reader)
	{
		base.OnDeserialize(reader);
		m_NetworkedProp.Deserialise(reader);
	}
}
