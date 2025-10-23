using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScreenLeaderboard : UIScreen
{
	[SerializeField]
	private UILeaderboard m_Leaderboard;

	[SerializeField]
	private Transform m_MessageBox;

	[SerializeField]
	private Text m_MessageText;

	[SerializeField]
	private Button m_RetryButton;

	[SerializeField]
	private Button m_QuitButton;

	private GameObject m_LastSelectedGameObject;

	public UILeaderboard Leaderboard => m_Leaderboard;

	public void Setup(List<UILeaderboard.ScoreEntry> sortedLeaderboard, string scoreTypeName)
	{
		m_Leaderboard.Setup(sortedLeaderboard, scoreTypeName);
		if ((bool)m_MessageBox)
		{
			m_MessageBox.gameObject.SetActive(value: false);
		}
	}

	public void SetHighlightedIndex(int index)
	{
		m_Leaderboard.SetHighlightedIndex(index);
	}

	public void SetGhostedIndex(int index)
	{
		m_Leaderboard.SetGhostedIndex(index);
	}

	public void SetMessage(string message)
	{
		if ((bool)m_MessageBox)
		{
			if ((bool)m_MessageText)
			{
				m_MessageText.text = message;
			}
			m_MessageBox.gameObject.SetActive(value: true);
		}
	}

	public void SetButtonActions(UnityAction retryAction, UnityAction quitAction)
	{
		ClearButtonActions();
		m_RetryButton.onClick.AddListener(retryAction);
		m_QuitButton.onClick.AddListener(quitAction);
	}

	private void ClearButtonActions()
	{
		m_RetryButton.onClick.RemoveAllListeners();
		m_QuitButton.onClick.RemoveAllListeners();
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_LastSelectedGameObject = null;
		m_Leaderboard.SpawnEntries();
	}

	public override void Hide()
	{
		base.Hide();
		m_Leaderboard.RecycleEntries();
	}

	private void Update()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject != m_LastSelectedGameObject)
		{
			m_Leaderboard.ScrollToButtonIfValid(currentSelectedGameObject);
			m_LastSelectedGameObject = currentSelectedGameObject;
		}
	}
}
