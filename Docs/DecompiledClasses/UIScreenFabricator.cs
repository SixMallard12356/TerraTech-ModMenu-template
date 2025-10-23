#define UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenFabricator : UIScreen
{
	public Text m_Title;

	public Text m_Description;

	public RectTransform m_recipePanel;

	public RectTransform m_recipeLayoutPanel;

	public UICurrentItem m_CurrentItem;

	public VerticalLayoutGroup recipesGroup;

	public HorizontalLayoutGroup queueGroup;

	public UIQueueItem m_queueItemPrefab;

	public RectTransform m_queuePanel;

	public Scrollbar m_queueScrollbar;

	public UIItemInfo m_InfoItemPrefab;

	public int m_CreateElementsPerFrame = 4;

	public int m_emptyQueueSize = 5;

	private Scrollbar m_RecipeScrollBar;

	private List<UIQueueItem> m_QueueItems = new List<UIQueueItem>();

	private UIItemInfo m_InfoPopup;

	private IEnumerator SetupUI()
	{
		UpdateQueue();
		UpdateRecipes();
		yield return null;
	}

	public override void Show(bool fromStackPop)
	{
		if (base.state != State.Show)
		{
			base.Show(fromStackPop);
			OnQueueChanged();
			m_RecipeScrollBar.value = 1f;
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
			m_RecipeScrollBar.value += pointerEventData.scrollDelta.y * Time.unscaledDeltaTime;
		}
	}

	private void ClearRecipes()
	{
	}

	public void OnPointerEnter(UIRecipe recipe)
	{
		RectTransform component = m_InfoPopup.GetComponent<RectTransform>();
		RectTransform component2 = recipe.GetComponent<RectTransform>();
		Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(component2.position);
		vector.x = 0f - component.sizeDelta.x / 2f;
		vector.y = 0f;
		vector.z = 0f;
		component.anchoredPosition = vector;
	}

	public void OnPointerExit(UIRecipe recipe)
	{
		m_InfoPopup.Hide();
	}

	private void StartMe()
	{
		d.Assert(m_recipePanel, "UIScreenFabricator Recipe LayoutPanel is null");
		d.Assert(m_recipeLayoutPanel, "UIScreenFabricator Recipe LayoutPanel is null");
		d.Assert(m_queueItemPrefab, "UIScreenFabricator UIQueue prefab is null");
		d.Assert(m_queuePanel, "UIScreenFabricator Queue LayoutPanel is null");
		d.Assert(m_InfoItemPrefab, "UIScreenFabricator ItemInfo Prefab is null");
		m_RecipeScrollBar = m_recipePanel.GetComponentsInChildren<Scrollbar>(includeInactive: true)[0];
		SetupExitButton();
		m_queueItemPrefab.CreatePool(5);
		CreateEmptyQueue(m_emptyQueueSize);
		m_InfoPopup = m_InfoItemPrefab.Spawn();
		m_InfoPopup.transform.SetParent(m_recipePanel, worldPositionStays: false);
	}

	private void Awake()
	{
		Singleton.Manager<ManStartup>.inst.DoOnceAfterComponentPoolInitialised(StartMe);
	}

	private void CreateEmptyQueue(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			UIQueueItem uIQueueItem = m_queueItemPrefab.Spawn();
			uIQueueItem.SetEmpty();
			uIQueueItem.transform.SetParent(m_queuePanel.transform, worldPositionStays: false);
			uIQueueItem.transform.localScale = Vector3.one;
			m_QueueItems.Add(uIQueueItem);
		}
	}

	private void UpdateRecipes()
	{
	}

	private void UpdateQueue()
	{
	}

	private void FixedUpdate()
	{
	}
}
