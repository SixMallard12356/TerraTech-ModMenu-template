using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenNotificationMultiselect : UIScreen
{
	public struct ItemConfig
	{
		public string Title;

		public Sprite Icon;

		public bool PreSelected;

		public int UniqueID;
	}

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private Button m_ConfirmButton;

	[SerializeField]
	private Button m_CancelButton;

	[SerializeField]
	private Transform m_ButtonContainer;

	[SerializeField]
	private Transform m_GamepadPromptContainer;

	[SerializeField]
	private TextMeshProUGUI[] m_ConfirmButtonLabels;

	[SerializeField]
	private TextMeshProUGUI[] m_CancelButtonLabels;

	[SerializeField]
	private ToggleGroup m_ToggleGroup;

	[SerializeField]
	private RectTransform m_Selection_VLG;

	[SerializeField]
	private NotificationMultiselectItem m_ItemPrefab;

	public Action<ItemConfig[]> ConfirmAction;

	public Action CancelAction;

	private string m_TitleText;

	private bool m_AllowNoneResult;

	private bool m_ConfirmPossible;

	private List<NotificationMultiselectItem> m_Items = new List<NotificationMultiselectItem>();

	private GameObject m_EntryTarget;

	private bool m_UseNewInputHandler;

	private int m_FrameCountWhenShown;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_FrameCountWhenShown = Time.frameCount;
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
		m_Title.text = m_TitleText;
		if (m_Items.Count > 0)
		{
			m_EntryTarget = m_Items[0].gameObject;
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_EntryTarget);
		}
		if (m_UseNewInputHandler)
		{
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnUICancel);
		}
	}

	public override void Hide()
	{
		base.Hide();
		m_TitleText = null;
		m_AllowNoneResult = false;
		m_ConfirmPossible = false;
		if (m_EntryTarget != null)
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_EntryTarget);
			m_EntryTarget = null;
		}
		ClearToggles();
		if (m_UseNewInputHandler)
		{
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnUICancel);
		}
		m_UseNewInputHandler = false;
	}

	public void Configure(string title, IEnumerable<ItemConfig> options, bool allowMultiselect, bool allowNoneResult, string confirmLabelText, Action<ItemConfig[]> onConfirm, Action onCancel)
	{
		m_TitleText = title;
		m_AllowNoneResult = allowNoneResult;
		SetButtonUI(confirmLabelText);
		SpawnToggles(options.ToArray(), allowMultiselect ? null : m_ToggleGroup);
		ConfirmAction = onConfirm;
		CancelAction = onCancel;
	}

	private void SetButtonUI(string confirmLabelText)
	{
		bool flag = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		m_ButtonContainer.gameObject.SetActive(!flag);
		m_GamepadPromptContainer.gameObject.SetActive(flag);
		string text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 61);
		if (flag)
		{
			confirmLabelText = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders("{0} " + confirmLabelText, new Localisation.GlyphInfo(21));
			text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders("{0} " + text, new Localisation.GlyphInfo(22));
		}
		for (int i = 0; i < m_ConfirmButtonLabels.Length; i++)
		{
			m_ConfirmButtonLabels[i].text = confirmLabelText;
		}
		for (int j = 0; j < m_CancelButtonLabels.Length; j++)
		{
			m_CancelButtonLabels[j].text = text;
		}
	}

	private void SpawnToggles(ItemConfig[] configs, ToggleGroup toggleGroup)
	{
		ClearToggles();
		for (int i = 0; i < configs.Length; i++)
		{
			m_Items.Add(m_ItemPrefab.Spawn(m_Selection_VLG));
			m_Items[i].Set(i, configs[i], toggleGroup);
			m_Items[i].ToggledEvent.Subscribe(OnTogglesChanged);
			m_Items[i].transform.localPosition = Vector3.zero;
			m_Items[i].transform.localScale = Vector3.one;
		}
		Util.RebuildExplicitVerticalUINavigationBetweenElements(m_Items.Select((NotificationMultiselectItem r) => r.GetComponent<Selectable>()));
		OnTogglesChanged(-1);
	}

	private void ClearToggles()
	{
		for (int i = 0; i < m_Items.Count; i++)
		{
			m_Items[i].ToggledEvent.Unsubscribe(OnTogglesChanged);
			m_Items[i].Recycle();
		}
		m_Items.Clear();
	}

	private int GetNumberSelected()
	{
		return m_Items.Count((NotificationMultiselectItem r) => r.Toggle.isOn);
	}

	private ItemConfig[] GetSelectionResults()
	{
		return (from r in m_Items
			where r.Toggle.isOn
			select r.ItemConfig).ToArray();
	}

	private void UpdateConfirmActive()
	{
		if (!m_AllowNoneResult)
		{
			m_ConfirmPossible = GetNumberSelected() > 0;
			m_ConfirmButton.interactable = m_ConfirmPossible;
		}
	}

	public void SetUseNewInputHandler(bool useNewInputHandler)
	{
		m_UseNewInputHandler = useNewInputHandler;
	}

	private void RespondToConfirmInput()
	{
		OnConfirm();
	}

	private void RespondToCancelInput()
	{
		OnCancel();
	}

	public void OnTogglesChanged(int index)
	{
		UpdateConfirmActive();
	}

	public void OnConfirm()
	{
		if (m_ConfirmPossible && ConfirmAction != null)
		{
			ConfirmAction(GetSelectionResults());
		}
	}

	public void OnCancel()
	{
		if (CancelAction != null)
		{
			CancelAction();
		}
	}

	private void OnUIAccept(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown)
		{
			RespondToConfirmInput();
			eventData.Use();
		}
	}

	private void OnUICancel(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown)
		{
			RespondToCancelInput();
			eventData.Use();
		}
	}

	private void OnPool()
	{
		m_ConfirmButton.onClick.AddListener(OnConfirm);
		m_CancelButton.onClick.AddListener(OnCancel);
		m_ToggleGroup.allowSwitchOff = true;
	}

	public void Update()
	{
		if (!m_UseNewInputHandler)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
			{
				RespondToCancelInput();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
			{
				RespondToConfirmInput();
			}
		}
	}
}
