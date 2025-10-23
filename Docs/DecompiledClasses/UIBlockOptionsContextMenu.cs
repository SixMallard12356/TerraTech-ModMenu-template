#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockOptionsContextMenu : UIHUDElement
{
	[Tooltip("The header UI element that all prefab instances will be created under, on the same parent")]
	[SerializeField]
	protected Transform m_HeaderUIElement;

	[SerializeField]
	protected UIBlockContextField[] m_BlockContextFieldPrefabs;

	[SerializeField]
	protected RectTransform m_FocusArea;

	[SerializeField]
	protected RectTransform m_ReservedArea;

	[SerializeField]
	protected RectTransform m_LineOriginPoint;

	[SerializeField]
	protected Button m_PriorityBottomButton;

	[SerializeField]
	protected Button m_AcceptButton;

	[SerializeField]
	protected Button m_CancelButton;

	private List<IHUDContextControlFieldModel> m_TargetModules = new List<IHUDContextControlFieldModel>();

	private TankBlock m_TargetBlock;

	private const bool k_FreezePlayerCameraWhenOpen = false;

	protected Vector3 m_PlayerLocationAtMenuOpen;

	protected IRadialInputController m_InputController;

	private byte m_MenuActiveState;

	protected Dictionary<Type, UIBlockContextField> m_TypePrefabs = new Dictionary<Type, UIBlockContextField>();

	protected List<UIBlockContextField> m_Active_BlockContextFields = new List<UIBlockContextField>();

	private Transform[] m_Sandwiching_HeaderObjects;

	private Transform[] m_Sandwiching_FooterObjects;

	private Tank TargetTank
	{
		get
		{
			if (!(m_TargetBlock == null))
			{
				return m_TargetBlock.tank;
			}
			return null;
		}
	}

	private bool IsMenuAvailableForTech
	{
		get
		{
			bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
			bool flag = TargetTank != null && (TargetTank == Singleton.playerTank || !m_TargetBlock.ContextMenuForPlayerTechOnly) && ManSpawn.IsPlayerTeam(TargetTank.Team) && m_TargetBlock.IsInteractible;
			bool flag2 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
			bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
			if (num && flag && !isInteractionBlocked)
			{
				return !flag2;
			}
			return false;
		}
	}

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_TargetBlock = openMenuEventData.m_TargetTankBlock;
		if (!IsMenuAvailableForTech)
		{
			return;
		}
		SetMenuActive(menuActiveState: true, openMenuEventData.m_RadialInputController);
		base.Show(context);
		d.Assert(m_Active_BlockContextFields.Count == 0);
		m_TargetBlock.GetComponents(m_TargetModules);
		for (int i = 0; i < m_TargetModules.Count; i++)
		{
			d.AssertFormat(m_TypePrefabs.ContainsKey(m_TargetModules[i].BlockContextFieldType), "Don't have field type requested in BlockContextMenu: {0}", m_TargetModules[i].BlockContextFieldType.ToString());
			UIBlockContextField uIBlockContextField = m_TypePrefabs[m_TargetModules[i].BlockContextFieldType].Spawn(m_HeaderUIElement.transform.parent);
			RectTransform obj = uIBlockContextField.transform as RectTransform;
			obj.localScale = Vector3.one;
			obj.anchoredPosition3D = Vector3.zero;
			uIBlockContextField.Set(m_TargetModules[i]);
			m_Active_BlockContextFields.Add(uIBlockContextField);
			if (i == 0)
			{
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(uIBlockContextField.GetDefaultHighlightElement().gameObject);
			}
		}
		d.Assert(m_Active_BlockContextFields.Count > 0);
		RefreshUISandwich();
		for (int j = 0; j < m_Active_BlockContextFields.Count; j++)
		{
			Selectable elementAbove = ((j == 0) ? null : m_Active_BlockContextFields[j - 1].GetLastElement());
			Selectable elementBelow = ((j == m_Active_BlockContextFields.Count - 1) ? m_PriorityBottomButton : m_Active_BlockContextFields[j + 1].GetFirstElement());
			m_Active_BlockContextFields[j].ConfigureNavigation(elementAbove, elementBelow);
		}
		bool flag = m_CancelButton.transform.position.x < m_AcceptButton.transform.position.x;
		Navigation navigation = m_AcceptButton.navigation;
		navigation.mode = Navigation.Mode.Explicit;
		navigation.selectOnDown = null;
		navigation.selectOnUp = m_Active_BlockContextFields.Last().GetLastElement();
		navigation.selectOnRight = (flag ? null : m_CancelButton);
		navigation.selectOnLeft = (flag ? m_CancelButton : null);
		m_AcceptButton.navigation = navigation;
		navigation = m_CancelButton.navigation;
		navigation.mode = Navigation.Mode.Explicit;
		navigation.selectOnDown = null;
		navigation.selectOnUp = m_Active_BlockContextFields.Last().GetLastElement();
		navigation.selectOnRight = (flag ? m_AcceptButton : null);
		navigation.selectOnLeft = (flag ? null : m_AcceptButton);
		m_CancelButton.navigation = navigation;
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_Active_BlockContextFields.First().GetDefaultHighlightElement().gameObject);
		foreach (UIBlockContextField active_BlockContextField in m_Active_BlockContextFields)
		{
			active_BlockContextField.ApplyResult();
			active_BlockContextField.Reset();
			active_BlockContextField.transform.SetParent(null, worldPositionStays: false);
			active_BlockContextField.Recycle();
		}
		m_Active_BlockContextFields.Clear();
		base.Hide(context);
		SetMenuActive(menuActiveState: false);
	}

	private void SetMenuActive(bool menuActiveState, ManInput.RadialInputController controller = ManInput.RadialInputController.Mouse)
	{
		m_PlayerLocationAtMenuOpen = Singleton.playerPos;
		m_MenuActiveState = (byte)(menuActiveState ? 1u : 0u);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, menuActiveState, UIInputMode.FullscreenUI);
		TankCamera.inst.SetMouseControlEnabled(!menuActiveState);
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(!menuActiveState, ManPointer.DragDisableReason.RadialMenu);
		Singleton.Manager<ManNewFTUE>.inst.SetEvent(menuActiveState ? FTUEActions.HasOpenedAIMenu : FTUEActions.HasClosedAIMenu);
		if (menuActiveState)
		{
			m_InputController = Singleton.Manager<ManInput>.inst.GetRadialInputController(controller);
			SetMenuAnchorPosition(m_InputController.GetAnchorPosition());
			m_InputController.Activate();
			m_InputController.SetModal(modal: true);
			m_TargetBlock.visible.RecycledEvent.Subscribe(OnTargetBlockRecycled);
		}
		else
		{
			if (m_InputController != null)
			{
				m_InputController.Deactivate();
				m_InputController = null;
			}
			m_TargetBlock.visible.RecycledEvent.Unsubscribe(OnTargetBlockRecycled);
		}
	}

	private void SetMenuAnchorPosition(Vector2 anchorPos)
	{
		(base.transform as RectTransform).anchoredPosition = anchorPos;
		if (!(m_ReservedArea == null))
		{
			float num = m_ReservedArea.anchoredPosition.x + m_ReservedArea.rect.width * (1f - m_ReservedArea.pivot.x) * m_ReservedArea.localScale.x;
			float num2 = (Singleton.Manager<ManHUD>.inst.Canvas.transform as RectTransform).rect.width * 0.5f - anchorPos.x - num;
			if (num2 < 0f)
			{
				(base.transform as RectTransform).anchoredPosition += Vector2.right * num2;
			}
		}
	}

	private void Cancel()
	{
		HideSelf();
	}

	private void Accept()
	{
		for (int i = 0; i < m_Active_BlockContextFields.Count; i++)
		{
			m_Active_BlockContextFields[i].SetSelectionAsResult();
		}
		HideSelf();
	}

	private void InitUISandwich()
	{
		Transform parent = m_HeaderUIElement.transform.parent;
		int siblingIndex = m_HeaderUIElement.GetSiblingIndex();
		List<Transform> list = new List<Transform>();
		List<Transform> list2 = new List<Transform>();
		List<GameObject> list3 = new List<GameObject>();
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.GetComponent<UIBlockContextField>() != null)
			{
				list3.Add(child.gameObject);
			}
			else if (i <= siblingIndex)
			{
				list.Add(child);
			}
			else
			{
				list2.Add(child);
			}
		}
		m_Sandwiching_HeaderObjects = list.ToArray();
		m_Sandwiching_FooterObjects = list2.ToArray();
		GameObject[] array = list3.ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			UnityEngine.Object.Destroy(array[j]);
		}
	}

	private void RefreshUISandwich()
	{
		for (int i = 0; i < m_Sandwiching_HeaderObjects.Length; i++)
		{
			m_Sandwiching_HeaderObjects[i].SetAsLastSibling();
		}
		foreach (UIBlockContextField active_BlockContextField in m_Active_BlockContextFields)
		{
			active_BlockContextField.transform.SetAsLastSibling();
		}
		for (int j = 0; j < m_Sandwiching_FooterObjects.Length; j++)
		{
			m_Sandwiching_FooterObjects[j].SetAsLastSibling();
		}
	}

	private void InitPrefabsDict()
	{
		m_TypePrefabs.Clear();
		for (int i = 0; i < m_BlockContextFieldPrefabs.Length; i++)
		{
			m_TypePrefabs.Add(m_BlockContextFieldPrefabs[i].GetType(), m_BlockContextFieldPrefabs[i]);
		}
	}

	private void OnTargetBlockRecycled(Visible blockVis)
	{
		Cancel();
	}

	public void OnAcceptButtonClicked()
	{
		Accept();
	}

	public void OnCancelButtonClicked()
	{
		Cancel();
	}

	private void OnPool()
	{
		InitPrefabsDict();
		InitUISandwich();
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}

	private void Update()
	{
		if (m_MenuActiveState == 1 && m_InputController.DidSelect())
		{
			m_MenuActiveState++;
		}
		else if (m_MenuActiveState == 2 && (m_InputController.DidSelect() || m_InputController.DidCancel()) && !m_InputController.IsGamePad() && m_FocusArea != null && m_FocusArea.gameObject.activeInHierarchy && !m_InputController.IsCursorInsideRect(m_FocusArea))
		{
			Accept();
		}
		if (m_MenuActiveState > 0)
		{
			Singleton.Manager<ManLineRenderer>.inst.AddLineForOverlay(m_LineOriginPoint.position, 1f, m_TargetBlock.trans.position);
		}
	}
}
