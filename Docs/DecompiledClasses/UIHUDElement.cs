#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHUDElement : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public enum EscapeKeyActionType
	{
		None,
		Hide,
		Collapse,
		Custom
	}

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private ManHUD.HUDElementType m_HudElementType;

	[SerializeField]
	private EscapeKeyActionType m_EscapeKeyAction;

	[SerializeField]
	private bool m_PreventInteraction;

	[SerializeField]
	private bool m_PreventPainting;

	[SerializeField]
	private bool m_HideWhileUsingJoypadControls;

	[SerializeField]
	private FMODEvent m_ShowSFX;

	[SerializeField]
	private FMODEvent m_HideSFX;

	private UIHUD m_ParentHUD;

	private bool m_IsShowing;

	private List<ManHUD.HUDElementType> m_Obscurers;

	private bool m_ObscurersLocked;

	public ManHUD.HUDElementType HudElementType => m_HudElementType;

	public bool IsVisible => base.gameObject.activeSelf;

	public bool IsExpanded { get; private set; }

	public List<ManHUD.HUDElementType> Obscurers => m_Obscurers;

	public bool IsShowing => m_IsShowing;

	public virtual void Init(object context)
	{
	}

	public virtual void DeInit(object context)
	{
	}

	public virtual void Show(object context)
	{
		if (!m_HideWhileUsingJoypadControls || !Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			SetVisible(visible: true);
		}
		if (!m_IsShowing)
		{
			Singleton.Manager<ManInput>.inst.OnInputModeChanged.Subscribe(OnInputModeChangedSelf);
			if (m_ShowSFX.IsValid())
			{
				m_ShowSFX.PlayOneShot();
			}
		}
		m_IsShowing = true;
	}

	public virtual void Hide(object context)
	{
		if (m_IsShowing)
		{
			if (m_HideSFX.IsValid())
			{
				m_HideSFX.PlayOneShot();
			}
			Singleton.Manager<ManInput>.inst.OnInputModeChanged.Unsubscribe(OnInputModeChangedSelf);
		}
		SetVisible(visible: false);
		OnPointerExit(null);
		m_IsShowing = false;
	}

	public virtual bool Expand(object context)
	{
		IsExpanded = true;
		return true;
	}

	public virtual bool Collapse(object context)
	{
		IsExpanded = false;
		return true;
	}

	public void SetVisible(bool visible)
	{
		if (visible != IsVisible)
		{
			base.gameObject.SetActive(visible);
		}
	}

	protected void ShowSelf(object context = null)
	{
		m_ParentHUD.ShowHudElement(m_HudElementType, context);
	}

	protected void HideSelf(object context = null)
	{
		m_ParentHUD.HideHudElement(m_HudElementType, context);
	}

	protected void ExpandSelf(object context = null)
	{
		m_ParentHUD.ExpandHudElement(m_HudElementType, context);
	}

	protected void CollapseSelf(object context = null)
	{
		m_ParentHUD.CollapseHudElement(m_HudElementType, context);
	}

	public void SetParentHUD(UIHUD parentHUD)
	{
		m_ParentHUD = parentHUD;
	}

	public virtual EscapeKeyActionType GetEscapeKeyAction()
	{
		return m_EscapeKeyAction;
	}

	public virtual bool HandleCustomEscapeKeyAction()
	{
		return false;
	}

	protected void AddElementToGroup(ManHUD.HUDGroup hudGroup, UIHUD.ShowAction showAction = UIHUD.ShowAction.Show)
	{
		m_ParentHUD.AddElementToGroup(hudGroup, m_HudElementType, showAction);
	}

	protected void RemoveElementFromGroup(ManHUD.HUDGroup hudGroup, UIHUD.ShowAction showAction = UIHUD.ShowAction.Show)
	{
		m_ParentHUD.RemoveElementFromGroup(hudGroup, m_HudElementType, showAction);
	}

	protected virtual void OnInputModeChanged(ManInput.InputMode inputMode)
	{
		if (m_HideWhileUsingJoypadControls)
		{
			switch (inputMode)
			{
			case ManInput.InputMode.Gamepad:
				SetVisible(visible: false);
				break;
			case ManInput.InputMode.KeyboardAndMouse:
				SetVisible(visible: true);
				break;
			}
		}
	}

	protected void RegisterObscuredBy(ManHUD.HUDElementType obscurer)
	{
		if (!m_ObscurersLocked)
		{
			if (m_Obscurers == null)
			{
				m_Obscurers = new List<ManHUD.HUDElementType>();
			}
			if (!m_Obscurers.Contains(obscurer))
			{
				m_Obscurers.Add(obscurer);
			}
		}
		else
		{
			d.LogError("Trying to register HUD obscurer after OnSpawn has been called -- won't be valid");
		}
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (m_PreventInteraction)
		{
			Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.HUD, preventInteraction: true);
		}
		if (m_PreventPainting)
		{
			Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.HUD, prevent: true);
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (m_PreventInteraction)
		{
			Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.HUD, preventInteraction: false);
		}
		if (m_PreventPainting)
		{
			Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.HUD, prevent: false);
		}
	}

	private void OnInputModeChangedSelf(ManInput.InputMode inputMode)
	{
		OnInputModeChanged(inputMode);
	}

	private void OnSpawn()
	{
		m_ObscurersLocked = true;
		Singleton.Manager<ManHUD>.inst.AddToCurrentSpawnHUD(this);
	}

	private void OnRecycle()
	{
		DeInit(null);
		m_ObscurersLocked = false;
	}
}
