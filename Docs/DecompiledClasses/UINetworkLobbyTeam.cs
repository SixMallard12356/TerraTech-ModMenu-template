using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINetworkLobbyTeam : MonoBehaviour
{
	[SerializeField]
	private Text m_NameField;

	private List<UINetworkLobbyPlayerEntry> m_Players = new List<UINetworkLobbyPlayerEntry>(8);

	private int m_Used;

	public void SetName(string teamName)
	{
		m_NameField.text = teamName;
	}

	public void AddPlayer(UINetworkLobbyPlayerEntry player)
	{
		m_Players.Add(player);
		m_Used++;
	}

	public UINetworkLobbyPlayerEntry GetUnsuedEntry()
	{
		UINetworkLobbyPlayerEntry result = null;
		if (m_Used < m_Players.Count)
		{
			result = m_Players[m_Used];
			m_Used++;
		}
		return result;
	}

	public void RecycleUnused()
	{
		for (int num = m_Players.Count - 1; num >= m_Used; num--)
		{
			Transform obj = m_Players[num].transform;
			obj.SetParent(null, worldPositionStays: false);
			obj.Recycle();
			m_Players.RemoveAt(num);
		}
	}

	public void ClearUseCount()
	{
		m_Used = 0;
	}
}
