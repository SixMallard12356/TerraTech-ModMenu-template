#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamCreateQueryCommand : Command<SteamDownloadData>
{
	private CallResult<SteamUGCQueryCompleted_t> m_QueryCompleteCallResult;

	private CallResult<SteamUGCQueryCompleted_t> m_QueryCompleteCallResultAlt;

	private SteamDownloadData m_Data;

	private EUserUGCList m_UGCTypeToQuery;

	private bool m_ReturnOnlyIDs;

	private uint m_CurPageID;

	public SteamCreateQueryCommand(EUserUGCList ugcTypeToQuery = EUserUGCList.k_EUserUGCList_Subscribed, bool returnOnlyIDs = false)
	{
		m_UGCTypeToQuery = ugcTypeToQuery;
		m_ReturnOnlyIDs = returnOnlyIDs;
	}

	public override void Execute(SteamDownloadData data)
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("SteamCreateItem - Steamworks not initialised");
			SetCancelled(data);
			return;
		}
		if (data.m_Page < 1 && data.m_Page != 0)
		{
			d.LogErrorFormat("SteamCreateQueryCommand - page value is {0}, must 1 or higher, will cause SteamAPI to silently fail with an invalid query handle", data.m_Page);
			SetCancelled(data);
			return;
		}
		if (m_QueryCompleteCallResult == null)
		{
			m_QueryCompleteCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(OnQueryCompleted);
		}
		m_Data = data;
		m_Data.ClearItems();
		m_Data.m_NumItems = 0u;
		m_Data.m_TotalItems = 0u;
		if (m_Data.m_Page == 0)
		{
			m_CurPageID = 1u;
		}
		else
		{
			m_CurPageID = m_Data.m_Page;
		}
		SendUGCQuery();
	}

	private void SendUGCQuery(CallResult<SteamUGCQueryCompleted_t> resultHandler = null)
	{
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUserUGCRequest(Singleton.Manager<ManSteamworks>.inst.AccountID, m_UGCTypeToQuery, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items_ReadyToUse, EUserUGCListSortOrder.k_EUserUGCListSortOrder_SubscriptionDateDesc, Singleton.Manager<ManSteamworks>.inst.AppID, Singleton.Manager<ManSteamworks>.inst.AppID, m_CurPageID);
		SteamUGC.SetReturnOnlyIDs(handle, m_ReturnOnlyIDs);
		SteamUGC.AddRequiredTag(pTagName: Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueCategory(m_Data.m_Category), handle: handle);
		d.Assert(handle.m_UGCQueryHandle != 0, "Invalid query handle, see https://partner.steamgames.com/doc/api/ISteamUGC#CreateQueryUserUGCRequest");
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		(resultHandler ?? m_QueryCompleteCallResult).Set(hAPICall);
	}

	private void OnQueryCompleted(SteamUGCQueryCompleted_t result, bool ioFailure)
	{
		if (m_QueryCompleteCallResultAlt == null && m_Data.m_Page == 0 && result.m_unTotalMatchingResults >= 50)
		{
			m_QueryCompleteCallResultAlt = CallResult<SteamUGCQueryCompleted_t>.Create(OnQueryCompletedAlt);
		}
		HandleCompletedQuery(result, ioFailure, m_QueryCompleteCallResultAlt);
	}

	private void OnQueryCompletedAlt(SteamUGCQueryCompleted_t result, bool ioFailure)
	{
		HandleCompletedQuery(result, ioFailure, m_QueryCompleteCallResult);
	}

	private void HandleCompletedQuery(SteamUGCQueryCompleted_t result, bool ioFailure, CallResult<SteamUGCQueryCompleted_t> resultHandler)
	{
		if (ioFailure || result.m_eResult != EResult.k_EResultOK)
		{
			d.LogError("SteamQueryUGCRequest - Steamworks callback error " + result.m_eResult);
			SetCancelled(m_Data);
			return;
		}
		uint unNumResultsReturned = result.m_unNumResultsReturned;
		uint unTotalMatchingResults = result.m_unTotalMatchingResults;
		bool flag = m_Data.m_Page == 0 && unNumResultsReturned >= 50 && unTotalMatchingResults >= 50;
		if (flag)
		{
			m_CurPageID++;
			SendUGCQuery(resultHandler);
		}
		m_Data.m_NumItems += unNumResultsReturned;
		m_Data.m_TotalItems = unTotalMatchingResults;
		for (uint num = 0u; num < unNumResultsReturned; num++)
		{
			SteamDownloadItemData item = default(SteamDownloadItemData);
			bool flag2 = false;
			if (SteamUGC.GetQueryUGCResult(result.m_handle, num, out var pDetails))
			{
				d.Log($"SteamUGCQueryRequest - Received details for {pDetails.m_nPublishedFileId}: {pDetails.m_pchFileName}");
				item.m_Details = pDetails;
			}
			else
			{
				flag2 = true;
				d.LogErrorFormat("SteamQueryUGCRequest - Error retrieving QueryUGCResult at index {0} out of {1} items", num, m_Data.m_NumItems);
			}
			if (!flag2)
			{
				m_Data.AddItem(item);
			}
		}
		SteamUGC.ReleaseQueryUGCRequest(result.m_handle);
		if (!flag)
		{
			SetComplete(m_Data);
		}
	}
}
