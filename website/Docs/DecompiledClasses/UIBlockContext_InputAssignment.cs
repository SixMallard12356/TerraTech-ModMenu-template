using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockContext_InputAssignment : UIBlockContextField
{
	[SerializeField]
	private Button m_StartPollingButton;

	[SerializeField]
	private TextMeshProUGUI m_AssignedButtonText;

	private ModuleCircuit_Input_KeyBind_FromInput m_TargetModule;

	private ModuleCircuit_Input_KeyBind_FromInput.InputElement m_CurrentSelection;

	private bool m_AcceptSelection;

	private string m_AssignedInputDisplay;

	public override Selectable GetDefaultHighlightElement()
	{
		return m_StartPollingButton;
	}

	public override Selectable GetFirstElement()
	{
		return m_StartPollingButton;
	}

	public override Selectable GetLastElement()
	{
		return m_StartPollingButton;
	}

	public override void ConfigureNavigation(Selectable elementAbove, Selectable elementBelow)
	{
		Navigation navigation = m_StartPollingButton.navigation;
		navigation.mode = Navigation.Mode.Explicit;
		navigation.selectOnUp = elementAbove;
		navigation.selectOnDown = elementBelow;
		m_StartPollingButton.navigation = navigation;
	}

	public override void Set(IHUDContextControlFieldModel targetModule)
	{
		m_TargetModule = targetModule as ModuleCircuit_Input_KeyBind_FromInput;
		m_AssignedInputDisplay = m_TargetModule.CurrentAssignedDisplay;
		m_AssignedButtonText.text = m_AssignedInputDisplay;
		m_CurrentSelection = m_TargetModule.CurrentAssignedInput;
		m_AcceptSelection = false;
	}

	public override void SetSelectionAsResult()
	{
		m_AcceptSelection = true;
	}

	public override void ApplyResult()
	{
		if (m_AcceptSelection)
		{
			m_TargetModule.AssignElement(m_CurrentSelection);
		}
	}

	public override void Reset()
	{
		m_TargetModule.CancelPollingForInput();
		m_AssignedButtonText.text = "[NOT SET]";
		m_TargetModule = null;
		m_CurrentSelection = default(ModuleCircuit_Input_KeyBind_FromInput.InputElement);
		m_AcceptSelection = false;
	}

	private void OnStartPollingButtonClicked()
	{
		m_TargetModule.StartPollingForInput(OnInputPollComplete);
		m_StartPollingButton.interactable = false;
		m_AssignedButtonText.text = "?";
	}

	private void OnInputPollComplete(bool assignSuccess, ModuleCircuit_Input_KeyBind_FromInput.InputElement assignedElement)
	{
		m_TargetModule.CancelPollingForInput(notifyListeners: false);
		m_StartPollingButton.interactable = true;
		if (assignSuccess)
		{
			m_CurrentSelection = assignedElement;
			m_AssignedButtonText.text = m_TargetModule.GetDisplayString(assignedElement);
		}
		else
		{
			m_AssignedButtonText.text = m_AssignedInputDisplay;
		}
	}

	private void OnPool()
	{
		m_StartPollingButton.onClick.AddListener(OnStartPollingButtonClicked);
	}
}
