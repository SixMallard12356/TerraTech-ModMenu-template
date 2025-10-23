using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIOptionsBehaviourDropdown))]
public class ColourSelector : MonoBehaviour
{
	public Event<Color32> OnChoice;

	private UIOptionsBehaviourDropdown m_DropdownWrapper;

	private List<Color32> m_Choices;

	private List<Color32> m_PendingChoices;

	private bool m_BlockEvents;

	private Color32 m_PlayerColour;

	public bool interactable
	{
		set
		{
			m_DropdownWrapper.innerDropdownInteractable = value;
		}
	}

	public void SetChoices(List<Color32> choices)
	{
		m_BlockEvents = true;
		if (m_DropdownWrapper.IsOpen())
		{
			m_PendingChoices = new List<Color32>(choices);
		}
		else
		{
			ApplyChoices(choices);
			Select(m_PlayerColour, isClosing: false);
		}
		m_BlockEvents = false;
	}

	public void Select(Color32 choice, bool isClosing)
	{
		m_BlockEvents = true;
		if (m_DropdownWrapper.IsOpen() && !isClosing)
		{
			m_PlayerColour = choice;
		}
		else
		{
			for (int i = 0; i < m_Choices.Count; i++)
			{
				if (m_Choices[i].Equals(choice))
				{
					m_DropdownWrapper.value = i;
					m_DropdownWrapper.RefreshShownValue();
					break;
				}
			}
			m_PlayerColour = choice;
		}
		m_BlockEvents = false;
	}

	private void OnValueChanged(int value)
	{
		if (!m_BlockEvents)
		{
			Color32 color = m_Choices[value];
			if (m_PendingChoices == null || m_PendingChoices.Contains(color))
			{
				m_PlayerColour = color;
				OnChoice.Send(color);
			}
		}
	}

	private void DropdownOpened()
	{
		m_PlayerColour = m_Choices[m_DropdownWrapper.value];
	}

	private void DropdownClosed()
	{
		if (m_PendingChoices != null)
		{
			ApplyChoices(m_PendingChoices);
			m_PendingChoices = null;
		}
		Select(m_PlayerColour, isClosing: true);
	}

	private void AddOption(Color32 col)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, col);
		texture2D.Apply();
		Sprite image = Sprite.Create(texture2D, new Rect(0f, 0f, 1f, 1f), Vector2.zero);
		m_DropdownWrapper.options.Add(new Dropdown.OptionData(string.Empty, image));
	}

	private void ApplyChoices(List<Color32> choices)
	{
		m_BlockEvents = true;
		m_Choices = choices;
		m_DropdownWrapper.ClearOptions();
		for (int i = 0; i < choices.Count; i++)
		{
			AddOption(choices[i]);
		}
		m_BlockEvents = false;
	}

	private void OnPool()
	{
		m_DropdownWrapper = GetComponent<UIOptionsBehaviourDropdown>();
		m_DropdownWrapper.onValueChanged.AddListener(OnValueChanged);
		m_DropdownWrapper.DropdownOpenedEvent.Subscribe(DropdownOpened);
		m_DropdownWrapper.DropdownClosedEvent.Subscribe(DropdownClosed);
	}
}
