using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechQuestGiver : TechComponent
{
	private new class SerialData : SerialData<SerialData>
	{
		public int numSideMissionsShownThisCycle;
	}

	[SerializeField]
	private int m_MaxMissionsToDisplayAtOnce = 6;

	[SerializeField]
	private int m_MaxCoreMissionsToDisplayAtOnce = 4;

	[SerializeField]
	private int m_MaxSideMissionsToDisplayAtOnce = 3;

	[SerializeField]
	private int m_MaxSideMissionsPerDay = 3;

	[SerializeField]
	private bool m_IncludeNearbyHiddenEncounters = true;

	[SerializeField]
	private float m_MaxNearbyHiddenEncounterRange = 600f;

	public Event<EncounterDisplayData> OnLocalEncounterFoundEvent;

	public EventNoParams OnFinishedCollectingLocalEncountersEvent;

	private bool m_BusyScanning;

	private UIMissionBoard m_ActiveMissionBoard;

	private List<int> m_RequestingConnections = new List<int>();

	public bool IsBusyScanning => m_BusyScanning;

	public int NumMissionsDisplayed
	{
		get
		{
			if (!(m_ActiveMissionBoard != null))
			{
				return 0;
			}
			return m_ActiveMissionBoard.NumMissions;
		}
	}

	public bool ShowMissionSelectUI()
	{
		bool result = false;
		if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.MissionBoard))
		{
			UIMissionBoard.Context context = new UIMissionBoard.Context
			{
				questGiver = this
			};
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MissionBoard, context);
			m_ActiveMissionBoard = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionBoard) as UIMissionBoard;
			Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Subscribe(OnHudElementHidden);
			if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCampaign())
			{
				RequestMissionList();
				m_ActiveMissionBoard.ForceUpdateUI();
			}
			result = true;
		}
		return result;
	}

	public void Refresh()
	{
		RequestMissionList();
	}

	public void HideMissionSelectUI()
	{
		StopSearching();
		if (m_ActiveMissionBoard != null)
		{
			m_ActiveMissionBoard = null;
		}
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Unsubscribe(OnHudElementHidden);
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.MissionBoard))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.MissionBoard);
		}
	}

	private void RequestMissionList()
	{
		if (!m_BusyScanning)
		{
			m_BusyScanning = true;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.Tech.netTech.netId, TTMsgType.PresentAvailableMission, OnClientAvailableMissionPresented);
				Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.Tech.netTech.netId, TTMsgType.FinishedRequestMissionListRequest, OnClientMissionSearchFinished);
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestMissionList, new EmptyMessage(), base.Tech.netTech.netId);
			}
			else
			{
				OnServerRequestMissionList(null);
			}
		}
	}

	public void OnServerRequestMissionList(NetworkMessage netMsg)
	{
		EncounterCollectionParams collectParams = new EncounterCollectionParams
		{
			centrePosition = base.Tech.boundsCentreWorld,
			maxTotalCount = m_MaxMissionsToDisplayAtOnce,
			maxCoreCount = m_MaxCoreMissionsToDisplayAtOnce,
			maxSideCount = Mathf.Min(m_MaxSideMissionsToDisplayAtOnce, m_MaxSideMissionsPerDay),
			includeNearbyHiddenEncounters = m_IncludeNearbyHiddenEncounters,
			maxNearbyHiddenEncounterRange = m_MaxNearbyHiddenEncounterRange
		};
		List<EncounterToSpawn> missionList = Singleton.Manager<ManProgression>.inst.GetMissionList(collectParams);
		bool flag = false;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			for (int i = 0; i < missionList.Count; i++)
			{
				EncounterMessage message = new EncounterMessage
				{
					m_EncounterSpawn = missionList[i]
				};
				Singleton.Manager<ManNetwork>.inst.SendToClient(netMsg.conn.connectionId, TTMsgType.PresentAvailableMission, message, base.Tech.netTech.netId);
			}
			flag = m_RequestingConnections.Count > 0;
			m_RequestingConnections.Add(netMsg.conn.connectionId);
		}
		else
		{
			for (int j = 0; j < missionList.Count; j++)
			{
				AvailableMissionPresented(missionList[j]);
			}
		}
		if (!flag && missionList.Count < m_MaxMissionsToDisplayAtOnce)
		{
			Singleton.Manager<ManProgression>.inst.StartSearchForEncounters(collectParams, base.Tech.visible.ID, OnServerLocalEncounterFound, OnServerFinishedCollectingLocalEncounters, missionList);
		}
		else if (!Singleton.Manager<ManEncounterPlacement>.inst.IsCollectingEncountersForRequester(base.Tech.visible.ID))
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManNetwork>.inst.SendToClient(netMsg.conn.connectionId, TTMsgType.FinishedRequestMissionListRequest, new EmptyMessage(), base.Tech.netTech.netId);
			}
			else
			{
				MissionSearchFinished();
			}
		}
	}

	private void StopSearching()
	{
		if (m_BusyScanning)
		{
			m_BusyScanning = false;
			if (base.Tech.netTech.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(base.Tech.netTech.netId, TTMsgType.PresentAvailableMission, OnClientAvailableMissionPresented);
				Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(base.Tech.netTech.netId, TTMsgType.FinishedRequestMissionListRequest, OnClientMissionSearchFinished);
			}
		}
		if (m_ActiveMissionBoard != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.StopRequestMissionList, new EmptyMessage(), base.Tech.netTech.netId);
			}
			else
			{
				Singleton.Manager<ManProgression>.inst.StopSearchForEncounters(base.Tech.visible.ID);
			}
		}
	}

	public void OnServerStopRequestMissionList(NetworkMessage netMsg)
	{
		m_RequestingConnections.Remove(netMsg.conn.connectionId);
		if (m_RequestingConnections.Count == 0)
		{
			Singleton.Manager<ManProgression>.inst.StopSearchForEncounters(base.Tech.visible.ID);
		}
	}

	private void OnHudElementHidden(UIHUDElement hiddenHudElement)
	{
		if (hiddenHudElement.HudElementType == ManHUD.HUDElementType.MissionBoard)
		{
			HideMissionSelectUI();
		}
	}

	private void OnServerLocalEncounterFound(EncounterToSpawn foundEncounter)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			EncounterMessage message = new EncounterMessage
			{
				m_EncounterSpawn = foundEncounter
			};
			{
				foreach (int requestingConnection in m_RequestingConnections)
				{
					Singleton.Manager<ManNetwork>.inst.SendToClient(requestingConnection, TTMsgType.PresentAvailableMission, message, base.Tech.netTech.netId);
				}
				return;
			}
		}
		AvailableMissionPresented(foundEncounter);
	}

	private void OnServerFinishedCollectingLocalEncounters()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			EmptyMessage message = new EmptyMessage();
			{
				foreach (int requestingConnection in m_RequestingConnections)
				{
					Singleton.Manager<ManNetwork>.inst.SendToClient(requestingConnection, TTMsgType.FinishedRequestMissionListRequest, message, base.Tech.netTech.netId);
				}
				return;
			}
		}
		MissionSearchFinished();
	}

	private void OnClientAvailableMissionPresented(NetworkMessage netMsg)
	{
		EncounterMessage encounterMessage = netMsg.ReadMessage<EncounterMessage>();
		AvailableMissionPresented(encounterMessage.m_EncounterSpawn);
	}

	private void AvailableMissionPresented(EncounterToSpawn presentedEncounter)
	{
		presentedEncounter.m_EncounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(presentedEncounter.m_EncounterDef);
		EncounterDisplayData paramA = EncounterDisplayData.CreateFromUnspawnedEncounter(presentedEncounter);
		OnLocalEncounterFoundEvent.Send(paramA);
	}

	private void OnClientMissionSearchFinished(NetworkMessage netMsg)
	{
		MissionSearchFinished();
	}

	private void MissionSearchFinished()
	{
		if (m_BusyScanning)
		{
			m_BusyScanning = false;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(base.Tech.netTech.netId, TTMsgType.PresentAvailableMission, OnClientAvailableMissionPresented);
				Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(base.Tech.netTech.netId, TTMsgType.FinishedRequestMissionListRequest, OnClientMissionSearchFinished);
			}
		}
		OnFinishedCollectingLocalEncountersEvent.Send();
	}

	private void OnStartServer()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.Tech.netTech.netId, TTMsgType.RequestMissionList, OnServerRequestMissionList);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.Tech.netTech.netId, TTMsgType.StopRequestMissionList, OnServerStopRequestMissionList);
	}

	private void OnPool()
	{
		if (base.Tech.netTech.IsNotNull())
		{
			base.Tech.netTech.StartServerEvent.Subscribe(OnStartServer);
		}
	}

	private void OnRecycle()
	{
		StopSearching();
	}
}
