#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDropdown : MonoBehaviour
{
	public delegate Button GetButtonCallback(int idx);

	public Button m_ButtonPrefab;

	public Text m_ButtonText;

	[SerializeField]
	private GameObject m_OpenArrow;

	[SerializeField]
	private GameObject m_CloseArrow;

	public RectTransform m_LayoutGroup;

	public Scrollbar m_Scrollbar;

	public RectTransform m_MaskRect;

	public RectTransform m_ScrollController;

	public float m_ExpandSpeed;

	public Event<int> OnValueChange;

	public int m_CurrentSelected;

	public int m_ShowScrollAfterElements = 8;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	private Toggle m_ExpandToggle;

	private float m_CurrentExpand;

	private bool m_Open;

	private Button[] m_MyButtons;

	private string[] m_MyStrings;

	private RectTransform m_ScrollbarRect;

	private Action OnOpenEvent;

	private float m_LayoutMinSize;

	private float m_LayoutElementSpacing;

	private float m_LayoutVerticalPadding;

	private bool m_ScrollUpdate;

	private bool m_ApplicationQuitting;

	private GetButtonCallback m_ButtonCallback;

	private void Awake()
	{
		Button component = GetComponent<Button>();
		if (component != null)
		{
			component.onClick.AddListener(ToggleOpen);
		}
		else
		{
			m_ExpandToggle = GetComponent<Toggle>();
			if (m_ExpandToggle != null)
			{
				m_ExpandToggle.isOn = false;
				m_ExpandToggle.onValueChanged.AddListener(SetOpen);
			}
			d.Assert(m_ExpandToggle, "UIDropdown has no expand Button or Toggle component");
		}
		m_Scrollbar.gameObject.SetActive(value: false);
		m_ScrollbarRect = m_Scrollbar.GetComponent<RectTransform>();
		UpdateArrowState();
	}

	private void Start()
	{
		m_LayoutMinSize = m_ButtonPrefab.GetComponent<LayoutElement>().minHeight;
		HorizontalOrVerticalLayoutGroup component = m_MaskRect.GetComponent<HorizontalOrVerticalLayoutGroup>();
		if (component != null)
		{
			m_LayoutVerticalPadding = component.padding.top + component.padding.bottom;
		}
		HorizontalOrVerticalLayoutGroup component2 = m_LayoutGroup.GetComponent<HorizontalOrVerticalLayoutGroup>();
		if (component2 != null)
		{
			m_LayoutVerticalPadding += component2.padding.top + component2.padding.bottom;
			m_LayoutElementSpacing = component2.spacing;
		}
	}

	private void Update()
	{
		int num = (int)m_CurrentExpand;
		if (m_MyStrings != null && m_CurrentExpand < (float)m_MyStrings.Length && m_Open && m_MyButtons[num] == null)
		{
			AddButtons();
		}
		UpdateDropdownSize();
		if (m_MyStrings != null && m_CurrentSelected >= 0)
		{
			SetLabelText(m_MyStrings[m_CurrentSelected]);
		}
		if (m_Open && Singleton.Manager<ManInput>.inst.GetButtonDown(22))
		{
			Close();
		}
		CloseIfClickedOutside();
	}

	private void OnApplicationQuit()
	{
		m_ApplicationQuitting = true;
	}

	private void OnDisable()
	{
		if (!m_ApplicationQuitting)
		{
			CloseImmediate();
		}
	}

	public void SubscribeToOpenEvent(Action eventToFire)
	{
		OnOpenEvent = (Action)Delegate.Combine(OnOpenEvent, eventToFire);
	}

	public void SetData(string[] elementsToHold, float expandSpeed, int current = 0)
	{
		m_CurrentSelected = current;
		m_MyStrings = elementsToHold;
		m_ExpandSpeed = expandSpeed;
		AddButtons();
	}

	public void Close()
	{
		SetOpen(open: false);
		m_ScrollUpdate = false;
	}

	public void CloseImmediate()
	{
		Close();
		m_CurrentExpand = 0f;
		UpdateDropdownSize();
		ClearButtons();
	}

	public void SetGetButtonCallback(GetButtonCallback buttonCallback)
	{
		m_ButtonCallback = buttonCallback;
	}

	public void SetLabelText(string text)
	{
		m_ButtonText.text = text;
	}

	private void ButtonClicked(int id)
	{
		m_CurrentSelected = id;
		SetOpen(open: false);
		OnValueChange.Send(id);
	}

	private void ToggleOpen()
	{
		SetOpen(!m_Open);
	}

	private void SetOpen(bool open)
	{
		m_Open = open;
		m_ScrollUpdate = true;
		if (m_Open)
		{
			if (OnOpenEvent != null)
			{
				OnOpenEvent();
			}
			bool active = m_MyStrings.Length > m_ShowScrollAfterElements;
			m_Scrollbar.gameObject.SetActive(active);
			base.transform.SetAsLastSibling();
			for (int i = 0; i < m_MyButtons.Length; i++)
			{
				m_MyButtons[i].interactable = true;
			}
			GameObject gameObject = null;
			if (m_CurrentSelected >= 0 && m_CurrentSelected < m_MyButtons.Length)
			{
				gameObject = m_MyButtons[m_CurrentSelected].gameObject;
			}
			else if (m_MyButtons.Length != 0)
			{
				gameObject = m_MyButtons[0].gameObject;
			}
			if (gameObject != null && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(gameObject);
			}
		}
		else
		{
			if (m_ExpandToggle != null)
			{
				m_ExpandToggle.isOn = false;
			}
			for (int j = 0; j < m_MyButtons.Length; j++)
			{
				m_MyButtons[j].interactable = false;
			}
			if (EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
		}
		UpdateArrowState();
	}

	private void AddButtons()
	{
		ClearButtons();
		m_MyButtons = new Button[m_MyStrings.Length];
		for (int i = 0; i < m_MyStrings.Length; i++)
		{
			if (!(m_MyButtons[i] == null))
			{
				continue;
			}
			Button prefab = m_ButtonPrefab;
			if (m_ButtonCallback != null)
			{
				Button button = m_ButtonCallback(i);
				if (button != null)
				{
					prefab = button;
				}
			}
			m_MyButtons[i] = prefab.Spawn(m_LayoutGroup);
			m_MyButtons[i].transform.SetParent(m_LayoutGroup, worldPositionStays: false);
			RectTransform obj = m_MyButtons[i].transform as RectTransform;
			obj.localScale = Vector3.one;
			obj.anchoredPosition3D = obj.anchoredPosition3D.SetZ(0f);
			int k = i;
			m_MyButtons[i].onClick.AddListener(delegate
			{
				ButtonClicked(k);
			});
			m_MyButtons[i].GetComponentInChildren<Text>().text = m_MyStrings[i];
			m_MyButtons[i].interactable = m_Open;
			ProfileButton profileButton = m_MyButtons[i] as ProfileButton;
			if (profileButton != null && !profileButton.Selected.HasSubscribers())
			{
				profileButton.Selected.Subscribe(OnProfileButtonSelected);
			}
			if (i > 0)
			{
				Button button2 = m_MyButtons[i - 1];
				Button button3 = m_MyButtons[i];
				Navigation navigation = button2.navigation;
				Navigation navigation2 = button3.navigation;
				navigation.mode = Navigation.Mode.Explicit;
				navigation2.mode = Navigation.Mode.Explicit;
				navigation.selectOnDown = button3;
				navigation2.selectOnUp = button2;
				if (profileButton != null)
				{
					Button component = button3.GetComponentInChildren<UIDeleteProfile>().GetComponent<Button>();
					navigation.selectOnRight = button2;
					navigation2.selectOnLeft = component;
				}
				else
				{
					navigation.selectOnRight = button3;
					navigation2.selectOnLeft = button2;
				}
				button2.navigation = navigation;
				button3.navigation = navigation2;
			}
		}
	}

	private void ClearButtons()
	{
		if (m_MyButtons == null)
		{
			return;
		}
		for (int i = 0; i < m_MyButtons.Length; i++)
		{
			if ((bool)m_MyButtons[i])
			{
				m_MyButtons[i].transform.SetParent(null, worldPositionStays: false);
				m_MyButtons[i].Recycle();
				m_MyButtons[i].onClick.RemoveAllListeners();
				m_MyButtons[i] = null;
			}
		}
	}

	private void CloseIfClickedOutside()
	{
		if (!m_Open || !Input.GetMouseButtonDown(0) || !(EventSystem.current != null))
		{
			return;
		}
		bool flag = true;
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Input.mousePosition;
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		for (int i = 0; i < list.Count && flag; i++)
		{
			Transform parent = list[i].gameObject.transform;
			while (parent != null)
			{
				if (parent == base.transform)
				{
					flag = false;
					break;
				}
				parent = parent.parent;
			}
		}
		if (flag)
		{
			SetOpen(open: false);
		}
	}

	private void UpdateArrowState()
	{
		if (m_OpenArrow != null)
		{
			m_OpenArrow.SetActive(!m_Open);
		}
		if (m_CloseArrow != null)
		{
			m_CloseArrow.SetActive(m_Open);
		}
	}

	private void UpdateDropdownSize()
	{
		float num = 0f;
		if (m_CurrentExpand > 0f)
		{
			float num2 = ((m_CurrentExpand < (float)m_ShowScrollAfterElements) ? m_CurrentExpand : ((float)m_ShowScrollAfterElements));
			num = m_LayoutVerticalPadding + num2 * m_LayoutMinSize + (num2 - 1f) * m_LayoutElementSpacing;
		}
		if (m_MaskRect.sizeDelta.y != num)
		{
			m_MaskRect.sizeDelta = new Vector2(m_MaskRect.sizeDelta.x, num);
			m_ScrollbarRect.sizeDelta = new Vector2(m_ScrollbarRect.sizeDelta.x, num);
			m_ScrollController.sizeDelta = m_MaskRect.sizeDelta;
		}
		if (m_Open && m_CurrentExpand < (float)m_MyStrings.Length)
		{
			m_Scrollbar.value = 1f;
			Canvas.ForceUpdateCanvases();
		}
		if (m_MyStrings != null && m_ScrollUpdate && m_CurrentExpand > (float)(m_MyStrings.Length - 1))
		{
			Canvas.ForceUpdateCanvases();
			m_Scrollbar.value = 1f;
			m_ScrollUpdate = false;
		}
		float num3 = Time.unscaledDeltaTime * m_ExpandSpeed * (m_Open ? 1f : (-1f));
		float num4 = m_CurrentExpand + num3;
		float min = Mathf.Max(m_CurrentExpand - 1f, 0f);
		float max = Mathf.Min(m_CurrentExpand + 1f, (m_MyStrings != null) ? ((float)m_MyStrings.Length) : 1f);
		if (!m_Open && num4 <= 0f && m_Scrollbar.gameObject.activeSelf)
		{
			m_Scrollbar.gameObject.SetActive(value: false);
		}
		m_CurrentExpand = Mathf.Clamp(num4, min, max);
	}

	private void OnProfileButtonSelected(ProfileButton button)
	{
		UIHelpers.VertScrollToItem(m_ScrollRect.content, button.GetComponent<RectTransform>(), m_ScrollRect.viewport.rect.height);
	}
}
