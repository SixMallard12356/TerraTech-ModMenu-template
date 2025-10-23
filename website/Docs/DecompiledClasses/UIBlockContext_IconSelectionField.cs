using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockContext_IconSelectionField : UIBlockContextField
{
	[Serializable]
	public struct Option
	{
		[Serializable]
		public struct Params
		{
			public int m_Return_Int;

			public int[] m_Return_Int_Array;

			public string m_Return_String;

			public bool m_Return_Bool;

			[ColorUsage(true, true)]
			public Color m_Return_Color;
		}

		public string ID;

		public UIIconSelectionField_Option.IconConfig m_IconConfig;

		public Params m_Params;
	}

	[SerializeField]
	protected GridLayoutGroup m_GridLayoutGroup;

	[SerializeField]
	protected TextMeshProUGUI m_SubTitleText;

	[SerializeField]
	protected UIIconSelectionField_Option m_IconTemplatePrefab;

	[SerializeField]
	protected Transform m_IconGrid;

	protected ModuleHUDContextControl_ColorPickerField m_TargetModule;

	protected List<UIIconSelectionField_Option> m_IconButtonPool;

	protected int m_ResultingOptionBitfield;

	protected int m_SelectedOptionBitfield;

	public override Selectable GetDefaultHighlightElement()
	{
		for (byte b = 0; b < m_TargetModule.SelectionFieldOptions.Length; b++)
		{
			if (m_TargetModule.IsOptionSelected(b))
			{
				return m_IconButtonPool[b].Button;
			}
		}
		return GetFirstElement();
	}

	public override Selectable GetFirstElement()
	{
		return m_IconButtonPool.First().Button;
	}

	public override Selectable GetLastElement()
	{
		return m_IconButtonPool.Last().Button;
	}

	public override void ConfigureNavigation(Selectable elementAbove, Selectable elementBelow)
	{
		UIIconSelectionField_Option[] array = m_IconButtonPool.Where((UIIconSelectionField_Option r) => r.gameObject.activeSelf).ToArray();
		int num = array.Length;
		int constraintCount = m_GridLayoutGroup.constraintCount;
		int num2 = Mathf.FloorToInt(num / constraintCount) * constraintCount;
		for (int num3 = 0; num3 < num; num3++)
		{
			UIIconSelectionField_Option obj = array[num3];
			int optionIndex = obj.OptionIndex;
			Navigation navigation = obj.Button.navigation;
			navigation.mode = Navigation.Mode.Explicit;
			bool flag = optionIndex < constraintCount;
			bool flag2 = optionIndex >= num2;
			navigation.selectOnDown = ((flag2 || optionIndex + constraintCount >= num) ? elementBelow : array[optionIndex + constraintCount].Button);
			navigation.selectOnUp = (flag ? elementAbove : array[optionIndex - constraintCount].Button);
			navigation.selectOnLeft = ((optionIndex == 0) ? null : array[optionIndex - 1].Button);
			navigation.selectOnRight = ((optionIndex == num - 1) ? null : array[optionIndex + 1].Button);
			obj.Button.navigation = navigation;
		}
	}

	[Obsolete]
	private Selectable GetActiveElement()
	{
		for (int i = 0; i < m_IconButtonPool.Count; i++)
		{
			if (m_IconButtonPool[i].gameObject.activeSelf && (m_TargetModule.MultiselectEnabled || i == m_TargetModule.FirstCurrentOptionIndex))
			{
				return m_IconButtonPool[i].Button;
			}
		}
		return null;
	}

	public override void Set(IHUDContextControlFieldModel targetModule)
	{
		Init();
		m_TargetModule = targetModule as ModuleHUDContextControl_ColorPickerField;
		m_SelectedOptionBitfield = m_TargetModule.CurrentOptionsBitfield;
		m_ResultingOptionBitfield = m_TargetModule.CurrentOptionsBitfield;
		if (m_SubTitleText != null)
		{
			m_SubTitleText.gameObject.SetActive(m_TargetModule.HUDTitle.Value != null);
			if (m_TargetModule.HUDTitle.Value != null)
			{
				m_SubTitleText.text = m_TargetModule.HUDTitle.Value;
			}
		}
		for (byte b = 0; b < m_TargetModule.SelectionFieldOptions.Length; b++)
		{
			if (m_IconButtonPool.Count < b + 1)
			{
				m_IconButtonPool.Add(UnityEngine.Object.Instantiate(m_IconTemplatePrefab, m_IconGrid));
				m_IconButtonPool[m_IconButtonPool.Count - 1].gameObject.SetActive(value: false);
			}
			m_IconButtonPool[b].Init();
			m_IconButtonPool[b].SetOption(b, m_TargetModule.SelectionFieldOptions[b].m_IconConfig);
			m_IconButtonPool[b].gameObject.SetActive(value: true);
			m_IconButtonPool[b].OptionSelectedEvent.Subscribe(OnOptionSelected);
			m_IconButtonPool[b].SetOptionSelected(m_TargetModule.IsOptionSelected(b));
			m_IconButtonPool[b].SetHilighted(state: false);
			m_IconButtonPool[b].transform.SetAsLastSibling();
		}
	}

	public override void SetSelectionAsResult()
	{
		m_ResultingOptionBitfield = m_SelectedOptionBitfield;
	}

	public override void ApplyResult()
	{
		m_TargetModule.SetOptionMultiplayerSafe(m_ResultingOptionBitfield);
	}

	public override void Reset()
	{
		for (int i = 0; i < m_IconButtonPool.Count; i++)
		{
			if (m_IconButtonPool[i].gameObject.activeSelf)
			{
				m_IconButtonPool[i].gameObject.SetActive(value: false);
			}
			m_IconButtonPool[i].OptionSelectedEvent.Unsubscribe(OnOptionSelected);
		}
		m_TargetModule = null;
	}

	private void OnOptionSelected(byte optionIndex)
	{
		m_TargetModule.CalculateSelectionBitfieldFromOptionSelection(optionIndex, ref m_SelectedOptionBitfield);
		foreach (UIIconSelectionField_Option item in m_IconButtonPool)
		{
			if (item.gameObject.activeInHierarchy)
			{
				item.SetOptionSelected(m_TargetModule.IsOptionSelected(item.OptionIndex, m_SelectedOptionBitfield));
			}
		}
		if (m_TargetModule.m_ApplyChangesRealtime)
		{
			m_TargetModule.SetOptionMultiplayerSafe(m_SelectedOptionBitfield);
		}
	}

	private void Init()
	{
		if (m_IconButtonPool != null)
		{
			return;
		}
		m_IconButtonPool = new List<UIIconSelectionField_Option>();
		for (int i = 0; i < m_IconGrid.childCount; i++)
		{
			UIIconSelectionField_Option component = m_IconGrid.GetChild(i).GetComponent<UIIconSelectionField_Option>();
			if (component != null)
			{
				m_IconButtonPool.Add(component);
			}
		}
		Reset();
	}
}
