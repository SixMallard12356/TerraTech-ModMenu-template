#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManNavUI : Singleton.Manager<ManNavUI>
{
	public struct UIInputHandlerInfo
	{
		public int m_RewiredAction;

		public Action<PayloadUIEventData> m_Handler;

		public bool m_Expired;
	}

	private List<UIInputHandlerInfo> m_UIInputHandlers = new List<UIInputHandlerInfo>();

	private List<GameObject> m_SelectableTargets = new List<GameObject>();

	private Coroutine m_SelectTargetCoroutine;

	public List<UIInputHandlerInfo> UIInputHandlers => m_UIInputHandlers;

	public void AddEntryTarget(GameObject target)
	{
		m_SelectableTargets.Remove(target);
		m_SelectableTargets.Add(target);
	}

	public void AddAndSelectEntryTarget(GameObject target, Action onSelectedCallback = null)
	{
		AddEntryTarget(target);
		if (EventSystem.current != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
			DeferredSetSelected(target, onSelectedCallback);
		}
	}

	public void DeferredSetSelected(GameObject target, Action onSelectedCallback = null)
	{
		if (m_SelectTargetCoroutine != null)
		{
			StopCoroutine(m_SelectTargetCoroutine);
		}
		m_SelectTargetCoroutine = StartCoroutine(DeferredSetSelectedRoutine(target, onSelectedCallback));
	}

	private IEnumerator DeferredSetSelectedRoutine(GameObject target, Action onSelectedCallback = null)
	{
		yield return new WaitForEndOfFrame();
		if (target == null || target.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(target);
			onSelectedCallback?.Invoke();
		}
	}

	public void ForgetEntryTarget(GameObject target)
	{
		m_SelectableTargets.Remove(target);
		if (EventSystem.current != null && !EventSystem.current.alreadySelecting && EventSystem.current.currentSelectedGameObject == target)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void ReselectTopmostEntryTarget()
	{
		if (EventSystem.current != null)
		{
			GameObject gameObject = ((m_SelectableTargets.Count > 0) ? m_SelectableTargets[m_SelectableTargets.Count - 1] : null);
			d.AssertFormat(gameObject == null || gameObject.activeInHierarchy, "Setting selected game object to an inactive game object {0}. This will break navigation!", gameObject);
			EventSystem.current.SetSelectedGameObject(gameObject);
		}
	}

	public void RegisterUIInputHandler(int rewiredAction, Action<PayloadUIEventData> handler)
	{
		UIInputHandlerInfo item = new UIInputHandlerInfo
		{
			m_Handler = handler,
			m_RewiredAction = rewiredAction
		};
		m_UIInputHandlers.Add(item);
	}

	public void UnregisterUIInputHandler(int rewiredAction, Action<PayloadUIEventData> handler)
	{
		UIInputHandlerInfo uIInputHandlerInfo = new UIInputHandlerInfo
		{
			m_Handler = handler,
			m_RewiredAction = rewiredAction,
			m_Expired = true
		};
		for (int num = m_UIInputHandlers.Count - 1; num >= 0; num--)
		{
			if (m_UIInputHandlers[num].m_RewiredAction == rewiredAction && m_UIInputHandlers[num].m_Handler == handler)
			{
				uIInputHandlerInfo = m_UIInputHandlers[num];
				uIInputHandlerInfo.m_Expired = true;
				m_UIInputHandlers[num] = uIInputHandlerInfo;
			}
		}
	}

	public static void RecalculateLeftRightNavigation(Selectable[] buttons, Selectable selectOnUp = null, Selectable selectOnDown = null)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			Navigation navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnUp = selectOnUp,
				selectOnDown = selectOnDown,
				selectOnLeft = null,
				selectOnRight = null
			};
			int num = i - 1;
			while (navigation.selectOnLeft == null && num >= 0)
			{
				if (buttons[num].gameObject.activeSelf && buttons[num].interactable)
				{
					navigation.selectOnLeft = buttons[num];
				}
				num--;
			}
			num = i + 1;
			while (navigation.selectOnRight == null && num < buttons.Length)
			{
				if (buttons[num].gameObject.activeSelf && buttons[num].interactable)
				{
					navigation.selectOnRight = buttons[num];
				}
				num++;
			}
			buttons[i].navigation = navigation;
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		EventSystem current = EventSystem.current;
		if (current != null && m_SelectableTargets.Count > 0 && !current.alreadySelecting && (current.currentSelectedGameObject == null || !current.currentSelectedGameObject.activeInHierarchy) && Singleton.Manager<ManInput>.inst.GetAxis2D(19, 20).sqrMagnitude > 0.04f)
		{
			GameObject selectedGameObject = m_SelectableTargets[m_SelectableTargets.Count - 1];
			current.SetSelectedGameObject(selectedGameObject);
		}
	}
}
