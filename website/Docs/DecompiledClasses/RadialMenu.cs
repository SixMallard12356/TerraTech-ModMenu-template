#define UNITY_EDITOR
using System;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_CenterDeadZoneSizeIndicator;

	[Tooltip("The angle at which the 'first' element is centered on, starting with 0 being oriented at the top")]
	[SerializeField]
	private float m_FirstElementCentreAngle;

	[SerializeField]
	private RectTransform m_TargetLine;

	[SerializeField]
	[Space(10f)]
	private UIRadialMenuOption m_CenterOption;

	[SerializeField]
	private bool m_CentreOptionForGamepad;

	[SerializeField]
	private RadialMenuSubmenu[] m_Submenus = new RadialMenuSubmenu[0];

	[HideInInspector]
	[SerializeField]
	private UIRadialMenuOption[] m_RadialOptions = new UIRadialMenuOption[0];

	[SerializeField]
	private RectTransform m_FakeSubMenuRect;

	public Event<int> OnOptionHovered;

	public Event<int> OnOptionSelected;

	public Event<object> OnClose;

	private int m_NumOptions;

	private float m_CachedDeadZoneWidth;

	private float m_CenterDeadZoneSqr;

	private Vector3 m_PlayerLocationAtMenuOpen;

	private IRadialInputController m_RadialInputController;

	private UIRadialMenuOption m_LastActiveControlOption;

	private bool m_DisableInputOnSubmenuOpen = true;

	private bool m_DisableInput;

	private bool m_FreezeCamera;

	private int m_ModalOptionIndex = -1;

	public static bool IsActiveMenuModal { get; private set; }

	public bool IsUsingGamePad
	{
		get
		{
			d.Assert(m_RadialInputController != null);
			return m_RadialInputController.IsGamePad();
		}
	}

	public void Show(ManInput.RadialInputController controller, bool freezeCamera)
	{
		m_RadialInputController = Singleton.Manager<ManInput>.inst.GetRadialInputController(controller);
		m_RadialInputController.Activate();
		m_FreezeCamera = freezeCamera;
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.Radial);
		InitMenu();
	}

	public void Hide()
	{
		SetModal(modal: false);
		DeInitMenu();
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.Radial);
		if (m_RadialInputController != null)
		{
			m_RadialInputController.Deactivate();
			m_RadialInputController = null;
		}
		for (int i = 0; i < m_Submenus.Length; i++)
		{
			m_Submenus[i].Close();
		}
	}

	public int GetOptionsCount()
	{
		return m_NumOptions;
	}

	public UIRadialMenuOption GetOption(int index)
	{
		d.Assert(index >= 0 && index < m_NumOptions);
		return m_RadialOptions[index];
	}

	public void OpenSubmenu(int index, Action<int> pressedCallback)
	{
		d.Assert(index >= 0 && index < m_Submenus.Length);
		if (m_DisableInputOnSubmenuOpen)
		{
			m_DisableInput = true;
		}
		m_Submenus[index].Open(m_RadialInputController, pressedCallback, OnSubmenuClosed);
	}

	public RadialMenuSubmenu GetSubmenu(int index)
	{
		d.Assert(index >= 0 && index < m_Submenus.Length);
		return m_Submenus[index];
	}

	public void CloseSubmenu(int index)
	{
		d.Assert(index >= 0 && index < m_Submenus.Length);
		m_Submenus[index].Close();
	}

	public void SetDisableInputOnSubmenuOpen(bool disable)
	{
		m_DisableInputOnSubmenuOpen = disable;
	}

	private void OnSubmenuClosed()
	{
		if (m_DisableInputOnSubmenuOpen)
		{
			m_DisableInput = false;
		}
	}

	public bool IsModal()
	{
		return m_ModalOptionIndex >= 0;
	}

	public void SetModal(bool modal, int optionIndex = -1, bool displayTooltipsRegardless = false)
	{
		if (modal)
		{
			d.Assert(m_RadialInputController != null);
			d.Assert(!IsActiveMenuModal);
			d.Assert(optionIndex >= 0);
			m_RadialOptions[optionIndex].TooltipEnabled = displayTooltipsRegardless;
			if (m_RadialInputController.IsGamePad())
			{
				m_DisableInput = true;
			}
			m_ModalOptionIndex = optionIndex;
			m_RadialInputController.SetModal(modal: true);
		}
		else if (m_ModalOptionIndex >= 0)
		{
			m_RadialOptions[m_ModalOptionIndex].TooltipEnabled = true;
			m_DisableInput = false;
			m_ModalOptionIndex = -1;
			m_RadialInputController.SetModal(modal: false);
		}
		IsActiveMenuModal = modal;
	}

	private void InitMenu()
	{
		(base.transform as RectTransform).anchoredPosition = m_RadialInputController.GetAnchorPosition();
		TankCamera.inst.SetMouseControlEnabled(enabled: false);
		if (m_FreezeCamera)
		{
			TankCamera.inst.FreezeCamera(freezeCamera: true);
		}
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: false, ManPointer.DragDisableReason.RadialMenu);
		m_PlayerLocationAtMenuOpen = Singleton.playerPos;
		ResetChildren();
		if (m_CenterOption != null)
		{
			bool flag = CheckCentreOptionAllowed();
			m_CenterOption.gameObject.SetActive(flag);
			m_CenterOption.SetIsInside(flag);
		}
		for (int i = 0; i < m_RadialOptions.Length; i++)
		{
			UIRadialMenuOptionWithWarning uIRadialMenuOptionWithWarning = m_RadialOptions[i] as UIRadialMenuOptionWithWarning;
			if ((bool)uIRadialMenuOptionWithWarning)
			{
				uIRadialMenuOptionWithWarning.SetIsAllowed(isAllowed: true);
			}
		}
		Singleton.Manager<ManNewFTUE>.inst.SetEvent(FTUEActions.HasOpenedAIMenu);
	}

	private void DeInitMenu()
	{
		ResetChildren();
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: true, ManPointer.DragDisableReason.RadialMenu);
		TankCamera.inst.SetMouseControlEnabled(enabled: true);
		if (m_FreezeCamera)
		{
			TankCamera.inst.FreezeCamera(freezeCamera: false);
		}
		Singleton.Manager<ManNewFTUE>.inst.SetEvent(FTUEActions.HasClosedAIMenu);
	}

	private void ResetChildren()
	{
		for (int i = 0; i < m_NumOptions; i++)
		{
			if (m_RadialOptions[i] != null)
			{
				m_RadialOptions[i].ResetState();
			}
		}
		if (m_CenterOption != null)
		{
			m_CenterOption.ResetState();
		}
		if (m_TargetLine != null)
		{
			m_TargetLine.gameObject.SetActive(value: false);
		}
		m_LastActiveControlOption = null;
	}

	private void Close()
	{
		for (int i = 0; i < m_Submenus.Length; i++)
		{
			m_Submenus[i].Close();
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && base.gameObject.activeSelf)
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextClose);
		}
		OnClose.Send(null);
	}

	private bool CheckCentreOptionAllowed()
	{
		bool result = false;
		if (m_CenterOption != null)
		{
			result = m_CentreOptionForGamepad || (m_RadialInputController != null && !m_RadialInputController.IsGamePad());
		}
		return result;
	}

	private void OnPool()
	{
		m_NumOptions = m_RadialOptions.Length;
		float num = (float)Math.PI * 2f / (float)m_NumOptions;
		for (int i = 0; i < m_NumOptions; i++)
		{
			d.Assert(m_RadialOptions[i] != null, "UIRadialTechControlMenu - Not all control options are assigned a RadialMenuOption!");
			if (m_RadialOptions[i] != null)
			{
				float centerAngle = m_FirstElementCentreAngle * ((float)Math.PI / 180f) + (float)i * num;
				m_RadialOptions[i].Init(centerAngle, num);
			}
		}
		ResetChildren();
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(ManBtnPrompt.PromptType.ContextClose);
		}
		for (int i = 0; i < m_Submenus.Length; i++)
		{
			m_Submenus[i].UpdateSubmenu();
		}
		if (m_DisableInput || !base.gameObject.activeSelf)
		{
			return;
		}
		UIRadialMenuOption uIRadialMenuOption = null;
		Vector2 relativePosition = m_RadialInputController.GetRelativePosition();
		if (m_CachedDeadZoneWidth != m_CenterDeadZoneSizeIndicator.rect.width)
		{
			m_CachedDeadZoneWidth = m_CenterDeadZoneSizeIndicator.rect.width;
			float num = m_CenterDeadZoneSizeIndicator.rect.width * 0.5f;
			m_CenterDeadZoneSqr = num * num;
		}
		uIRadialMenuOption = null;
		for (int j = 0; j < m_NumOptions; j++)
		{
			if (relativePosition.sqrMagnitude > m_CenterDeadZoneSqr && m_RadialOptions[j].IsInside(relativePosition))
			{
				uIRadialMenuOption = m_RadialOptions[j];
				break;
			}
		}
		bool flag = CheckCentreOptionAllowed();
		if (m_CenterOption != null && flag)
		{
			bool flag2 = uIRadialMenuOption == null;
			m_CenterOption.gameObject.SetActive(flag2);
			m_CenterOption.SetIsInside(flag2);
			if (flag2)
			{
				uIRadialMenuOption = m_CenterOption;
			}
		}
		bool flag3 = m_RadialInputController != null && m_RadialInputController.IsGamePad();
		if (!flag3 || uIRadialMenuOption != null)
		{
			for (int k = 0; k < m_NumOptions; k++)
			{
				if (m_RadialOptions[k] != null)
				{
					bool flag4 = m_RadialOptions[k] == uIRadialMenuOption;
					m_RadialOptions[k].SetIsInside(flag4);
					if (flag3 && !flag4)
					{
						m_RadialOptions[k].Deselect();
					}
				}
			}
		}
		if (uIRadialMenuOption != null)
		{
			m_LastActiveControlOption = uIRadialMenuOption;
		}
		else if (m_RadialInputController.IsGamePad())
		{
			uIRadialMenuOption = m_LastActiveControlOption;
			if (uIRadialMenuOption != null)
			{
				uIRadialMenuOption.SetIsInside(isInside: true);
			}
		}
		if (m_RadialInputController.IsSelecting())
		{
			if (m_TargetLine != null)
			{
				float magnitude = relativePosition.magnitude;
				m_TargetLine.sizeDelta = new Vector2(magnitude, m_TargetLine.sizeDelta.y);
				if (magnitude > 0f)
				{
					Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(relativePosition.y, relativePosition.x) * 57.29578f, Vector3.forward);
					m_TargetLine.rotation = rotation;
				}
				if (!m_TargetLine.gameObject.activeSelf)
				{
					m_TargetLine.gameObject.SetActive(value: true);
				}
			}
			if (OnOptionHovered.HasSubscribers())
			{
				int paramA = -1;
				if (uIRadialMenuOption != null)
				{
					paramA = Array.IndexOf(m_RadialOptions, uIRadialMenuOption);
				}
				OnOptionHovered.Send(paramA);
			}
		}
		if (m_RadialInputController.DidSelect())
		{
			if (uIRadialMenuOption != null)
			{
				if (m_FakeSubMenuRect == null || !m_FakeSubMenuRect.gameObject.activeInHierarchy || !m_RadialInputController.IsCursorInsideRect(m_FakeSubMenuRect))
				{
					OnOptionSelected.Send(Array.IndexOf(m_RadialOptions, uIRadialMenuOption));
				}
			}
			else
			{
				Close();
			}
		}
		else if (m_RadialInputController.DidCancel())
		{
			Close();
		}
		if (m_FreezeCamera)
		{
			float radialMenuMaxPlayerMoveDistance = Globals.inst.m_RadialMenuMaxPlayerMoveDistance;
			d.Assert(radialMenuMaxPlayerMoveDistance > 0f, "Globals.inst.m_RadialMenuMaxPlayerMoveDistance must be greater that 0");
			if ((m_PlayerLocationAtMenuOpen - Singleton.playerPos).sqrMagnitude > radialMenuMaxPlayerMoveDistance * radialMenuMaxPlayerMoveDistance)
			{
				Close();
			}
		}
	}
}
