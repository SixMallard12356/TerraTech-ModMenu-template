#define UNITY_EDITOR
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;

public class UIVoiceIndicatorHUD : UIHUDElement
{
	[SerializeField]
	private UIVoiceIndicatorComponent m_TemplateComponent;

	[SerializeField]
	private Transform m_ParentComponent;

	[SerializeField]
	private bool m_DebugPlayers;

	private List<UIVoiceIndicatorComponent> m_VoiceIndicatorComponents = new List<UIVoiceIndicatorComponent>();

	private bool m_LastDebugPlayers;

	public override void Show(object context)
	{
		base.Show(context);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
	}

	private void Update()
	{
		if (m_DebugPlayers && !m_LastDebugPlayers)
		{
			foreach (UIVoiceIndicatorComponent voiceIndicatorComponent in m_VoiceIndicatorComponents)
			{
				voiceIndicatorComponent.Recycle();
			}
			m_VoiceIndicatorComponents.Clear();
			for (int i = 0; i < 8; i++)
			{
				m_VoiceIndicatorComponents.Add(m_TemplateComponent.Spawn());
				m_VoiceIndicatorComponents[i].transform.SetParent(m_ParentComponent, worldPositionStays: false);
				m_VoiceIndicatorComponents[i].SetData(Singleton.Manager<ManNetwork>.inst.GetPlayer(0).Sprite, Singleton.Manager<ManNetwork>.inst.GetPlayer(0).name + i, default(TTNetworkID));
				m_VoiceIndicatorComponents[i].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		else if (!m_DebugPlayers)
		{
			if (m_LastDebugPlayers)
			{
				foreach (UIVoiceIndicatorComponent voiceIndicatorComponent2 in m_VoiceIndicatorComponents)
				{
					voiceIndicatorComponent2.Recycle();
				}
				m_VoiceIndicatorComponents.Clear();
			}
			List<TTNetworkID> playersThatAreTalking = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetPlayersThatAreTalking();
			for (int num = m_VoiceIndicatorComponents.Count - 1; num >= 0; num--)
			{
				if (playersThatAreTalking == null || !playersThatAreTalking.Contains(m_VoiceIndicatorComponents[num].GetNetID()))
				{
					m_VoiceIndicatorComponents[num].Recycle();
					m_VoiceIndicatorComponents.RemoveAt(num);
				}
			}
			if (playersThatAreTalking != null)
			{
				foreach (TTNetworkID item in playersThatAreTalking)
				{
					NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerByNetworkID(item);
					bool flag = false;
					foreach (UIVoiceIndicatorComponent voiceIndicatorComponent3 in m_VoiceIndicatorComponents)
					{
						if (voiceIndicatorComponent3.GetNetID() == item || (netPlayer != null && voiceIndicatorComponent3.GetName() == netPlayer.name))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						UIVoiceIndicatorComponent uIVoiceIndicatorComponent = m_TemplateComponent.Spawn();
						uIVoiceIndicatorComponent.transform.SetParent(m_ParentComponent, worldPositionStays: false);
						uIVoiceIndicatorComponent.transform.localScale = new Vector3(1f, 1f, 1f);
						if (netPlayer != null)
						{
							uIVoiceIndicatorComponent.SetData(netPlayer.Sprite, netPlayer.name, item);
						}
						else
						{
							d.LogError("UIVoiceIndicatorHUD: No net player found for talking player net ID:" + item.ToString());
						}
						m_VoiceIndicatorComponents.Add(uIVoiceIndicatorComponent);
					}
				}
			}
		}
		m_LastDebugPlayers = m_DebugPlayers;
	}
}
