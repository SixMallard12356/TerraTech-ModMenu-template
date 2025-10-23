using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggleHighlightSwapper : MonoBehaviour
{
	[SerializeField]
	private Toggle m_ToggleButton;

	[SerializeField]
	private GameObject m_HighlightEffect;

	private void Update()
	{
		m_HighlightEffect.gameObject.SetActive(EventSystem.current.currentSelectedGameObject == m_ToggleButton.gameObject);
	}
}
