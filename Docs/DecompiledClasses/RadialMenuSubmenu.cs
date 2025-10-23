#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RadialMenuSubmenu : MonoBehaviour
{
	[SerializeField]
	private bool m_JoypadModeOnly;

	[SerializeField]
	private bool m_SelectTarget;

	[SerializeField]
	[FormerlySerializedAs("m_EntryPointPriorityOrder")]
	private List<QueryableSelectable> m_Elements;

	[SerializeField]
	private LayoutGroup m_ListParent;

	private Event<int> OnOptionPressed;

	private EventNoParams OnClosed;

	private GameObject m_AddedEntryTarget;

	private bool m_Open;

	public bool IsOpen => m_Open;

	protected IRadialInputController m_Controller { get; private set; }

	public void Open(IRadialInputController controller, Action<int> pressedCallback, Action closeCallback)
	{
		if (!m_Open)
		{
			m_Controller = controller;
			if (pressedCallback != null)
			{
				OnOptionPressed.Subscribe(pressedCallback);
			}
			if (closeCallback != null)
			{
				OnClosed.Subscribe(closeCallback);
			}
			base.gameObject.SetActive(value: true);
			m_Open = true;
			Canvas.ForceUpdateCanvases();
			SelectFirstElement();
			OnOpen();
		}
	}

	public void Close()
	{
		if (m_Open)
		{
			OnOptionPressed.Clear();
			OnClosed.Send();
			OnClosed.Clear();
			base.gameObject.SetActive(value: false);
			m_Open = false;
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_AddedEntryTarget);
			m_AddedEntryTarget = null;
			OnClose();
		}
	}

	public void AddAt(QueryableSelectable selectable, int index)
	{
		d.Assert(m_ListParent != null);
		m_Elements.Insert(index, selectable);
		Transform obj = selectable.transform;
		obj.SetParent(m_ListParent.transform, worldPositionStays: false);
		obj.SetSiblingIndex(index);
	}

	public void RemoveAt(int index)
	{
		UnityEngine.Object.Destroy(m_Elements[index].gameObject);
		m_Elements.RemoveAt(index);
	}

	public QueryableSelectable GetOption(int index)
	{
		d.Assert(index >= 0 && index < m_Elements.Count);
		return m_Elements[index];
	}

	public int GetElementsCount()
	{
		return m_Elements.Count;
	}

	public void UpdateSubmenu()
	{
		if (!m_Open)
		{
			return;
		}
		if (m_Controller.DidCancel())
		{
			Close();
		}
		if (m_Controller.DidSelect())
		{
			QueryableSelectable option = null;
			for (int i = 0; i < m_Elements.Count; i++)
			{
				if (m_Elements[i].IsHighlighted)
				{
					option = m_Elements[i];
					break;
				}
			}
			OnOptionSelected(option);
		}
		OnUpdate();
	}

	private void SelectFirstElement()
	{
		if (m_Controller.IsGamePad())
		{
			Selectable selectable = null;
			if (m_Elements != null)
			{
				for (int i = 0; i < m_Elements.Count; i++)
				{
					selectable = m_Elements[i];
					if (selectable.interactable && selectable.gameObject.activeInHierarchy)
					{
						m_AddedEntryTarget = selectable.gameObject;
						break;
					}
				}
			}
			if (m_AddedEntryTarget != null)
			{
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_AddedEntryTarget);
				if (m_SelectTarget)
				{
					selectable.Select();
					selectable.OnSelect(null);
				}
			}
			return;
		}
		for (int j = 0; j < m_Elements.Count; j++)
		{
			if (m_Elements[j] is QueryableSelectableWithTooltip)
			{
				TooltipComponent tooltip = ((QueryableSelectableWithTooltip)m_Elements[j]).GetTooltip();
				if (tooltip != null)
				{
					tooltip.SetPointerEnabled(enabled: true);
				}
			}
		}
	}

	protected virtual void OnOpen()
	{
	}

	protected virtual void OnClose()
	{
	}

	protected virtual void OnUpdate()
	{
	}

	protected virtual void OnOptionSelected(QueryableSelectable option)
	{
		OnOptionPressed.Send(m_Elements.IndexOf(option));
	}
}
