#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenDeliveryCannon : UIScreen
{
	public UISellItem m_sellItemPrefab;

	public RectTransform m_sellItemsPanel;

	public RectTransform m_sellItemsLayoutPanel;

	public UIQueueResource m_queueItemPrefab;

	public RectTransform m_queuePanel;

	public Scrollbar m_queueScrollbar;

	public HorizontalLayoutGroup m_queueGroup;

	public int m_emptyQueueSize = 8;

	private Scrollbar mResourcesScrollBar;

	private List<UIQueueResource> mQueueResources = new List<UIQueueResource>();

	private ResourceTable.Definition[] m_Resources;

	public override void Show(bool fromStackPop)
	{
		if (base.state != State.Show)
		{
			base.Show(fromStackPop);
			OnQueueChanged();
			Canvas.ForceUpdateCanvases();
			mResourcesScrollBar.value = 1f;
		}
	}

	private void OnQueueChanged()
	{
		UpdateQueue();
		UpdateRecipes();
	}

	public void OnScroll(BaseEventData scrollEvent)
	{
		if (scrollEvent is PointerEventData)
		{
			PointerEventData pointerEventData = scrollEvent as PointerEventData;
			mResourcesScrollBar.value += pointerEventData.scrollDelta.y * Time.unscaledDeltaTime;
		}
	}

	private void UpdateRecipes()
	{
	}

	private void ClearRecipes()
	{
	}

	private void UpdateQueue()
	{
	}

	private void CreateEmptyQueue(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			UIQueueResource uIQueueResource = m_queueItemPrefab.Spawn();
			uIQueueResource.SetEmpty();
			uIQueueResource.transform.SetParent(m_queuePanel.transform, worldPositionStays: false);
			uIQueueResource.transform.localScale = Vector3.one;
			mQueueResources.Add(uIQueueResource);
		}
	}

	private void StartMe()
	{
		d.Assert(m_sellItemPrefab, "UIScreenFabricator UIRecipe prefab is null");
		d.Assert(m_sellItemsPanel, "UIScreenFabricator Recipe LayoutPanel is null");
		d.Assert(m_sellItemsLayoutPanel, "UIScreenFabricator Recipe LayoutPanel is null");
		d.Assert(m_queueItemPrefab, "UIScreenFabricator UIQueue prefab is null");
		d.Assert(m_queuePanel, "UIScreenFabricator Queue LayoutPanel is null");
		mResourcesScrollBar = m_sellItemsPanel.GetComponentsInChildren<Scrollbar>(includeInactive: true)[0];
		SetupExitButton();
		m_sellItemPrefab.CreatePool(10);
		m_queueItemPrefab.CreatePool(5);
		CreateEmptyQueue(m_emptyQueueSize);
	}

	private void Awake()
	{
		Singleton.Manager<ManStartup>.inst.DoOnceAfterComponentPoolInitialised(StartMe);
	}

	private void FixedUpdate()
	{
	}
}
