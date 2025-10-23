using UnityEngine;
using UnityEngine.UI;

public class UIGameModeButton : MonoBehaviour
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private float m_DesaturationLevel = 1f;

	private UIGameMode m_GameModeComponent;

	private void OnHighlightMode(UIGameMode highlightedMode)
	{
		if (m_Image != null)
		{
			float value = ((highlightedMode == m_GameModeComponent || highlightedMode == null) ? 0f : m_DesaturationLevel);
			m_Image.material.SetFloat("_Level", value);
		}
	}

	private void Start()
	{
		m_GameModeComponent = GetComponent<UIGameMode>();
		if (m_GameModeComponent != null)
		{
			m_GameModeComponent.ModeHighlightEvent.Subscribe(OnHighlightMode);
		}
		if ((bool)m_Image)
		{
			m_Image.material = new Material(m_Image.material);
		}
	}
}
