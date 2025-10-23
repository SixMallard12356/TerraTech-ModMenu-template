using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class UIUserTextInputHelper : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerClickHandler
{
	public Event<string> EndEditEvent;

	private InputField m_InputField;

	private bool m_Activated;

	public bool HasFocus => m_Activated;

	public void OnSelect(BaseEventData data)
	{
		m_Activated = true;
		Singleton.Manager<ManInput>.inst.EnableAllControllerMapsOfType(enabled: false, ControllerType.Keyboard);
		Singleton.Manager<DebugUtil>.inst.DisableCheatInput = true;
	}

	public void OnEndEdit(string newText)
	{
		Singleton.Manager<ManInput>.inst.EnableAllControllerMapsOfType(enabled: true, ControllerType.Keyboard);
		Singleton.Manager<DebugUtil>.inst.DisableCheatInput = false;
		Singleton.Manager<ManGameMode>.inst.SetCancelEventWasHandled(eventWasHandled: true);
		m_Activated = false;
		EndEditEvent.Send(newText);
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (!m_Activated)
		{
			OnSelect(null);
		}
	}

	private void Start()
	{
		m_InputField = GetComponent<InputField>();
		m_InputField.onEndEdit.AddListener(OnEndEdit);
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (m_InputField != null && m_InputField.isFocused && !hasFocus)
		{
			m_InputField.DeactivateInputField();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!m_Activated)
		{
			OnSelect(null);
		}
	}
}
