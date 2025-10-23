using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourButton : MonoBehaviour, ISubmitHandler, IEventSystemHandler
{
	[SerializeField]
	private Button m_Target;

	public Button GetButton()
	{
		return m_Target;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		m_Target.OnSubmit(eventData);
	}
}
