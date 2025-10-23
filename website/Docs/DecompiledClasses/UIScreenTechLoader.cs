#define UNITY_EDITOR
using System;

public class UIScreenTechLoader : UIScreen
{
	private UITechSelector m_Selector;

	public Action<Snapshot> SelectorCallback { get; set; }

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (m_Selector == null)
		{
			bool includeInactive = true;
			m_Selector = GetComponentInChildren<UITechSelector>(includeInactive);
			d.Assert(m_Selector != null, "UIScreenTechLoader - cannot find child tech selector component");
		}
		m_Selector.Show();
		m_Selector.OnSelectionAcceptedEvent.Subscribe(OnTechSelected);
	}

	public override void Hide()
	{
		if (m_Selector != null)
		{
			m_Selector.Hide();
			m_Selector.OnSelectionAcceptedEvent.Unsubscribe(OnTechSelected);
		}
		SelectorCallback = null;
		base.Hide();
	}

	public void HandleCloseButton()
	{
		bool showPrev = false;
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev);
	}

	private void OnTechSelected(Snapshot capture)
	{
		Action<Snapshot> selectorCallback = SelectorCallback;
		bool showPrev = false;
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev);
		selectorCallback?.Invoke(capture);
	}
}
