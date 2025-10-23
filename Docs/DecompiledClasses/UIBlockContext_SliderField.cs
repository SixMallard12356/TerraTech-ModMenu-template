using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBlockContext_SliderField : UIBlockContextField
{
	[SerializeField]
	protected TextMeshProUGUI m_TitleText;

	[SerializeField]
	protected TextMeshProUGUI m_ValueUnitText;

	[SerializeField]
	protected TextMeshProUGUI m_GamepadEditValuePrompt;

	[SerializeField]
	protected TextMeshProUGUI m_ValueOverrideText;

	[SerializeField]
	protected TMP_InputField m_ValueInputField;

	[SerializeField]
	protected Button m_ValueInputFocusButton;

	[SerializeField]
	protected Slider m_Slider;

	[Range(0.01f, 1f)]
	[Tooltip("The amount of travel per second per single increase/decrease when there is no step defined by the slider module\nNote: 1 = the difference between the lowest and highest value.")]
	[SerializeField]
	protected float m_DefaultStepPercentage = 0.25f;

	[SerializeField]
	private AnimationCurve m_SliderGamepadSensitivityCurve = AnimationCurve.Linear(0f, 0f, 1f, 0.05f);

	[SerializeField]
	private AnimationCurve m_SliderHoldRampCurve = AnimationCurve.Linear(0f, 0f, 3f, 1f);

	[SerializeField]
	private float m_SliderFirstMoveTimeout = 0.2f;

	protected ModuleHUDSliderControl m_TargetModule;

	protected float m_ResultingSliderValue;

	protected float m_CurrentValue;

	private float m_SliderMoveDirSign;

	private float m_SliderMoveInitialisedTime = -1f;

	private float m_SliderMoveAppliedTime = -1f;

	private bool m_SliderFocusButtonSelected;

	private float m_DefaultStep;

	private float SingleStep
	{
		get
		{
			if (m_TargetModule.AdjustableValueStep != 0f)
			{
				return m_TargetModule.AdjustableValueStep;
			}
			return m_DefaultStep * Time.deltaTime;
		}
	}

	private bool GamepadShortcutsAllowed
	{
		get
		{
			if (SKU.ConsoleUI && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && m_Slider.gameObject.activeInHierarchy)
			{
				return m_SliderFocusButtonSelected;
			}
			return false;
		}
	}

	protected float CurrentValue
	{
		get
		{
			return m_CurrentValue;
		}
		set
		{
			m_CurrentValue = m_TargetModule.GetStandardisedValue(value);
			m_Slider.value = m_TargetModule.GetRangeFulfillmentFromValue(m_CurrentValue);
			TryApplyChangesRealtime();
			UpdateValueText();
		}
	}

	protected float SliderValue
	{
		get
		{
			return m_Slider.value;
		}
		set
		{
			m_Slider.value = value;
			m_CurrentValue = m_TargetModule.GetStandardisedValue(m_TargetModule.GetValueFromRangeFulfillment(m_Slider.value));
			TryApplyChangesRealtime();
			UpdateValueText();
		}
	}

	public override Selectable GetDefaultHighlightElement()
	{
		return GetFirstElement();
	}

	public override Selectable GetFirstElement()
	{
		return m_Slider;
	}

	public override Selectable GetLastElement()
	{
		return m_Slider;
	}

	public override void ConfigureNavigation(Selectable elementAbove, Selectable elementBelow)
	{
		Navigation navigation = m_Slider.navigation;
		navigation.mode = Navigation.Mode.Explicit;
		navigation.selectOnRight = null;
		navigation.selectOnLeft = null;
		navigation.selectOnUp = elementAbove;
		navigation.selectOnDown = elementBelow;
		m_Slider.navigation = navigation;
	}

	public override void Set(IHUDContextControlFieldModel targetModule)
	{
		m_TargetModule = targetModule as ModuleHUDSliderControl;
		if (m_TitleText != null)
		{
			m_TitleText.gameObject.SetActive(m_TargetModule.HUDTitle.Value != null);
			if (m_TargetModule.HUDTitle.Value != null)
			{
				m_TitleText.text = m_TargetModule.HUDTitle.Value;
			}
		}
		CurrentValue = m_TargetModule.Value;
		SetSelectionAsResult();
		m_ValueUnitText.text = string.Format(m_TargetModule.ValueFormat.Value, "");
		m_ValueUnitText.rectTransform.sizeDelta = new Vector2(m_ValueUnitText.GetPreferredValues().x, m_ValueUnitText.rectTransform.sizeDelta.y);
		m_DefaultStep = m_DefaultStepPercentage * Mathf.Abs(m_TargetModule.AdjustableValueRange.x - m_TargetModule.AdjustableValueRange.y);
		m_Slider.onValueChanged.AddListener(OnSliderValueSet);
		m_ValueInputField.onEndEdit.AddListener(OnInputFieldSet);
	}

	public override void SetSelectionAsResult()
	{
		m_ResultingSliderValue = m_Slider.value;
	}

	public override void ApplyResult()
	{
		m_TargetModule.AdjustableValueFulfillment01 = m_ResultingSliderValue;
	}

	private void TryApplyChangesRealtime()
	{
		if (m_TargetModule.ApplyChangesRealtime)
		{
			m_TargetModule.AdjustableValueFulfillment01 = m_Slider.value;
		}
	}

	public override void Reset()
	{
		m_Slider.onValueChanged.RemoveListener(OnSliderValueSet);
		m_ValueInputField.onEndEdit.RemoveListener(OnInputFieldSet);
		m_TargetModule = null;
		m_SliderFocusButtonSelected = false;
		UISetValueFieldActive(state: false);
		m_SliderMoveInitialisedTime = -1f;
		m_SliderMoveAppliedTime = -1f;
	}

	public void UIOnSliderFocusButtonSelected(bool state)
	{
		m_SliderFocusButtonSelected = state;
		if (!state)
		{
			m_SliderMoveInitialisedTime = -1f;
			m_SliderMoveAppliedTime = -1f;
		}
		bool gamepadShortcutsAllowed = GamepadShortcutsAllowed;
		if (gamepadShortcutsAllowed)
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(ManBtnPrompt.PromptType.ContextBlockContextMenuSliderSelected);
		}
		else
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextGrab);
		}
		if (m_GamepadEditValuePrompt != null)
		{
			m_GamepadEditValuePrompt.gameObject.SetActive(gamepadShortcutsAllowed);
			if (m_GamepadEditValuePrompt.gameObject.activeSelf)
			{
				m_GamepadEditValuePrompt.text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders("{0}", new Localisation.GlyphInfo(21));
			}
		}
	}

	public void UIOnIncrementValue()
	{
		CurrentValue += SingleStep;
		m_Slider.Select();
	}

	public void UIOnDecrementValue()
	{
		CurrentValue -= SingleStep;
		m_Slider.Select();
	}

	private void OnSliderValueSet(float value)
	{
		SliderValue = value;
	}

	private void OnInputFieldSet(string str)
	{
		CurrentValue = (Util.TryParseFloatInvariant(str, out var value) ? value : 0f);
		UISetValueFieldActive(state: false);
	}

	public void UISetValueFieldActive(bool state)
	{
		if (m_ValueInputField.interactable == state)
		{
			return;
		}
		if (EventSystem.current.alreadySelecting && EventSystem.current.currentSelectedGameObject == m_ValueInputField.gameObject)
		{
			m_ValueInputField.ActivateInputField();
			return;
		}
		if (state && VirtualKeyboard.IsRequired())
		{
			OpenVirtualKeyboard();
			return;
		}
		m_ValueInputField.interactable = state;
		m_ValueInputFocusButton.interactable = !state;
		if (state)
		{
			m_ValueInputField.Select();
			m_ValueInputField.ActivateInputField();
		}
		else
		{
			m_ValueInputField.DeactivateInputField();
			m_Slider.Select();
		}
	}

	public void OpenVirtualKeyboard()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 7);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 8);
		VirtualKeyboard.EntryCompleteDelegate onCompleteHandler = delegate(bool accepted, string result)
		{
			if (accepted && !string.IsNullOrEmpty(result) && Util.TryParseFloatInvariant(result, out var value))
			{
				CurrentValue = value;
			}
		};
		VirtualKeyboard.PlatformParams platformParams = VirtualKeyboard.PlatformParams.Default;
		platformParams.xb_vkInputMode = ManXboxOne.VirtualKeyboardInputMode.Number;
		VirtualKeyboard.PromptInput(localisedString, localisedString2, m_ValueInputField.text, onCompleteHandler, platformParams);
	}

	private void UpdateValueText()
	{
		string overrideText;
		string adjustableValueDisplayText = m_TargetModule.GetAdjustableValueDisplayText(m_CurrentValue, includeUnit: false, out overrideText);
		bool flag = overrideText != string.Empty;
		if (m_ValueInputField != null)
		{
			m_ValueInputField.text = adjustableValueDisplayText;
		}
		if (m_ValueOverrideText != null)
		{
			m_ValueOverrideText.gameObject.SetActive(flag);
			m_ValueOverrideText.text = overrideText;
		}
		m_ValueUnitText.gameObject.SetActive(!flag);
	}

	private void HandleGamepadSliderInput()
	{
		if (!GamepadShortcutsAllowed)
		{
			return;
		}
		float axis = Singleton.Manager<ManInput>.inst.GetAxis(19);
		float num = Mathf.Sign(axis);
		if (!Mathf.Approximately(axis, 0f) && num == m_SliderMoveDirSign)
		{
			if (m_SliderMoveInitialisedTime == -1f)
			{
				CurrentValue += num * SingleStep;
				m_SliderMoveInitialisedTime = Time.time;
			}
			if (!(Time.time < m_SliderMoveInitialisedTime + m_SliderFirstMoveTimeout))
			{
				if (m_SliderMoveAppliedTime == -1f)
				{
					m_SliderMoveAppliedTime = Time.time;
				}
				else
				{
					SliderValue += Mathf.Clamp01(m_SliderHoldRampCurve.Evaluate(Time.time - m_SliderMoveAppliedTime)) * m_SliderGamepadSensitivityCurve.Evaluate(Mathf.Abs(axis)) * num;
				}
			}
		}
		else
		{
			m_SliderMoveInitialisedTime = -1f;
			m_SliderMoveAppliedTime = -1f;
			m_SliderMoveDirSign = num;
		}
	}

	private void Update()
	{
		if (GamepadShortcutsAllowed)
		{
			HandleGamepadSliderInput();
		}
	}
}
