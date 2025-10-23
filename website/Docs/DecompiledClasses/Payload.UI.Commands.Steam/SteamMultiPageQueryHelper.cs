using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamMultiPageQueryHelper
{
	public Event<SteamDownloadData> OnQueryPageComplete;

	public EventNoParams OnQueryComplete;

	private CommandOperation<SteamDownloadData> m_SteamUGCDataCommand;

	private uint m_MinPageToFetch;

	private uint m_MaxPageToFetchInclusive;

	private uint m_CurrentFetchCount;

	private uint m_NextPageToFetch;

	public bool IsBusy => m_SteamUGCDataCommand.IsRunning;

	public SteamMultiPageQueryHelper(EUserUGCList ugcTypeToQuery = EUserUGCList.k_EUserUGCList_Subscribed, bool returnOnlyIDs = false, uint minPage = 1u, uint maxPageInclusive = 0u)
	{
		if (m_SteamUGCDataCommand == null)
		{
			m_SteamUGCDataCommand = new CommandOperation<SteamDownloadData>();
			SteamCreateQueryCommand command = new SteamCreateQueryCommand(ugcTypeToQuery, returnOnlyIDs);
			m_SteamUGCDataCommand.Add(command);
			m_SteamUGCDataCommand.Completed.Subscribe(OnFetchPageCompleted);
		}
		m_MinPageToFetch = minPage;
		m_MaxPageToFetchInclusive = maxPageInclusive;
	}

	public void StartQuery()
	{
		m_NextPageToFetch = m_MinPageToFetch;
		m_CurrentFetchCount = 0u;
		FetchNextPage();
	}

	public void Cancel()
	{
		m_NextPageToFetch = 0u;
	}

	private void FetchNextPage()
	{
		SteamDownloadData data = SteamDownloadData.Create(SteamItemCategory.Techs, m_NextPageToFetch);
		m_SteamUGCDataCommand.Execute(data);
		m_NextPageToFetch++;
	}

	private void OnFetchPageCompleted(SteamDownloadData data)
	{
		OnQueryPageComplete.Send(data);
		m_CurrentFetchCount += data.m_NumItems;
		if (m_NextPageToFetch != 0 && m_CurrentFetchCount < data.m_TotalItems && (m_MaxPageToFetchInclusive == 0 || m_NextPageToFetch <= m_MaxPageToFetchInclusive))
		{
			FetchNextPage();
		}
		else
		{
			OnQueryComplete.Send();
		}
	}
}
