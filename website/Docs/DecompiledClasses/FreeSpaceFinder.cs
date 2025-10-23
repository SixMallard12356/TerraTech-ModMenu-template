using System.Collections.Generic;
using UnityEngine;

public class FreeSpaceFinder
{
	private const int k_NumTimesToAutoRetry = 10;

	public Event<WorldPosition?> FreeSpaceFoundEvent;

	private ManFreeSpace.FreeSpaceParams m_FreeSpaceParams;

	private ManFreeSpace.FreeSpaceEnumeratorParams m_SearchParams;

	private string m_DebugName;

	private int m_NumAutoRetriesRemaining;

	private IEnumerator<WorldPosition> m_LocationEnum;

	public bool IsValid => m_SearchParams != null;

	public ref readonly ManFreeSpace.FreeSpaceParams FreeSpaceParams => ref m_FreeSpaceParams;

	public ManFreeSpace.FreeSpaceEnumeratorParams SearchParams => m_SearchParams;

	public IEnumerator<WorldPosition> LocationEnum
	{
		get
		{
			return m_LocationEnum;
		}
		set
		{
			m_LocationEnum = value;
		}
	}

	public void Setup(ManFreeSpace.FreeSpaceParams freeParams, string debugName, bool autoRetry)
	{
		Cancel();
		m_FreeSpaceParams = freeParams;
		m_DebugName = debugName;
		m_NumAutoRetriesRemaining = (autoRetry ? 10 : 0);
		m_SearchParams = new ManFreeSpace.FreeSpaceEnumeratorParams(found: false, freeParams.m_CenterPosWorld, 0);
		Singleton.Manager<ManFreeSpace>.inst.SearchForFreeSpace(this);
	}

	public void Load(ManFreeSpace.FreeSpaceParams freeParams, ManFreeSpace.FreeSpaceEnumeratorParams searchParams, string debugName, bool autoRetry)
	{
		Cancel();
		if (searchParams != null && !searchParams.m_FoundPos)
		{
			m_FreeSpaceParams = freeParams;
			m_SearchParams = searchParams;
			m_DebugName = debugName;
			m_NumAutoRetriesRemaining = (autoRetry ? 10 : 0);
			if (m_FreeSpaceParams.m_CenterPosWorld == default(WorldPosition))
			{
				m_FreeSpaceParams.m_CenterPosWorld = WorldPosition.FromGameWorldPosition((Vector3)m_FreeSpaceParams.m_CenterPos);
			}
			Singleton.Manager<ManFreeSpace>.inst.ResumeFreeSpaceSearch(this);
		}
	}

	public void UpdateSearchParameters(ManFreeSpace.FreeSpaceEnumeratorParams searchParams)
	{
		m_SearchParams = searchParams;
	}

	public void Cancel()
	{
		if (m_SearchParams != null)
		{
			Singleton.Manager<ManFreeSpace>.inst.CancelFreeSpaceSearch(this);
			m_FreeSpaceParams = default(ManFreeSpace.FreeSpaceParams);
			m_SearchParams = null;
			m_DebugName = "";
			m_NumAutoRetriesRemaining = 0;
		}
	}

	public void FreeSpaceFound(WorldPosition? position)
	{
		if (position.HasValue || m_NumAutoRetriesRemaining <= 0)
		{
			m_FreeSpaceParams = default(ManFreeSpace.FreeSpaceParams);
			m_LocationEnum = null;
			m_SearchParams = null;
			m_DebugName = "";
			m_NumAutoRetriesRemaining = 0;
			FreeSpaceFoundEvent.Send(position);
		}
		else
		{
			m_NumAutoRetriesRemaining--;
			Singleton.Manager<ManFreeSpace>.inst.ResumeFreeSpaceSearch(this);
		}
	}
}
