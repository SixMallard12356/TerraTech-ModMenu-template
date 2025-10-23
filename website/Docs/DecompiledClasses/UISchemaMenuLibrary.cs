using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuLibrary : MonoBehaviour
{
	[SerializeField]
	private UISchemaMenuLibraryListElement m_ListElementPrefab;

	[SerializeField]
	private LayoutGroup m_ListLayoutGroup;

	private List<UISchemaMenuLibraryListElement> m_ListElements = new List<UISchemaMenuLibraryListElement>();

	private UISchemaMenuLibraryListElement m_CurrentElement;

	private UITableScroller m_TableScroller;

	public void Setup(ControlSchemeLibrary schemeLibrary, List<ControlScheme> techSchemes, Action<ControlScheme> clickedCallback, Action<ControlScheme, bool> toggledCallback)
	{
		CleanList();
		foreach (ControlScheme scheme in schemeLibrary.Schemes)
		{
			bool isAvailableOnTech = techSchemes?.Contains(scheme) ?? false;
			AddSchemeToList(scheme, isAvailableOnTech, clickedCallback, toggledCallback);
		}
	}

	public void UpdateTechSchemeToggles(List<ControlScheme> techSchemes)
	{
		foreach (UISchemaMenuLibraryListElement listElement in m_ListElements)
		{
			listElement.SetToggleToMatch(techSchemes);
		}
	}

	public void DisplayScheme(ControlScheme activeScheme)
	{
		foreach (UISchemaMenuLibraryListElement listElement in m_ListElements)
		{
			if (listElement.MatchesControlScheme(activeScheme))
			{
				m_CurrentElement = listElement;
			}
			else
			{
				listElement.SetHighlighted(highlight: false);
			}
		}
		m_CurrentElement.SetHighlighted(highlight: true);
		m_TableScroller.ScrollToEntry(m_ListElements.IndexOf(m_CurrentElement));
	}

	public void AddSchemeToList(ControlScheme scheme, bool isAvailableOnTech, Action<ControlScheme> clickedCallback, Action<ControlScheme, bool> toggledCallback)
	{
		Transform obj = m_ListElementPrefab.transform.Spawn();
		UISchemaMenuLibraryListElement component = obj.GetComponent<UISchemaMenuLibraryListElement>();
		component.Setup(scheme, isAvailableOnTech, clickedCallback, toggledCallback);
		m_ListElements.Add(component);
		if (m_ListElements.Count == 1)
		{
			component.GetButton().DisableDirection(CustomExplicitNavigationButton.Direction.UP, disabled: true);
		}
		else
		{
			m_ListElements[m_ListElements.Count - 2].GetButton().DisableDirection(CustomExplicitNavigationButton.Direction.DOWN, disabled: false);
		}
		component.GetButton().DisableDirection(CustomExplicitNavigationButton.Direction.DOWN, disabled: true);
		obj.SetParent(m_ListLayoutGroup.transform, worldPositionStays: false);
	}

	public void RemoveSchemeFromList(ControlScheme controlScheme)
	{
		foreach (UISchemaMenuLibraryListElement listElement in m_ListElements)
		{
			if (listElement.MatchesControlScheme(controlScheme))
			{
				listElement.transform.Recycle();
				m_ListElements.Remove(listElement);
				if (m_ListElements.Count > 0)
				{
					m_ListElements[0].GetButton().DisableDirection(CustomExplicitNavigationButton.Direction.UP, disabled: true);
					m_ListElements[m_ListElements.Count - 1].GetButton().DisableDirection(CustomExplicitNavigationButton.Direction.DOWN, disabled: true);
				}
				break;
			}
		}
	}

	public void CleanList()
	{
		foreach (UISchemaMenuLibraryListElement listElement in m_ListElements)
		{
			listElement.transform.Recycle();
		}
		m_ListElements.Clear();
	}

	public void UpdateTextForScheme(ControlScheme controlScheme)
	{
		foreach (UISchemaMenuLibraryListElement listElement in m_ListElements)
		{
			if (listElement.MatchesControlScheme(controlScheme))
			{
				listElement.UpdateText();
				break;
			}
		}
	}

	public UISchemaMenuLibraryListElement GetCurrentlySelectedElement()
	{
		return m_CurrentElement;
	}

	public void OnPool()
	{
		if (m_ListLayoutGroup != null)
		{
			m_TableScroller = m_ListLayoutGroup.GetComponent<UITableScroller>();
		}
	}
}
