public class Vendors
{
	public struct SaveData
	{
		public bool m_Active;

		public bool m_ShopActive;

		public bool m_MissionBoardActive;

		public bool m_SellingActive;

		public bool m_ScuActive;

		public bool m_BlockPickupActive;

		public bool m_HiddenOnRadar;

		public bool m_RemoteChargingActive;
	}

	public Event<bool, bool, bool, bool> OnActiveChanged;

	private bool m_ShopActive;

	private bool m_MissionBoardActive;

	private bool m_SellingActive;

	private bool m_ScuActive;

	private bool m_VisibleOnRadar;

	private bool m_RemoteChargingActive;

	public bool ShopActive => m_ShopActive;

	public bool MissionBoardActive => m_MissionBoardActive;

	public bool SellingActive => m_SellingActive;

	public bool ScuActive => m_ScuActive;

	public bool VisibleOnRadar => m_VisibleOnRadar;

	public bool RemoteChargingActive => m_RemoteChargingActive;

	public void Init()
	{
		SetAllActive(active: false);
		Singleton.Manager<ManProgression>.inst.OnEncountersLoaded.Subscribe(OnEncountersLoaded);
		m_VisibleOnRadar = false;
	}

	public void Deinit()
	{
		Singleton.Manager<ManProgression>.inst.OnEncountersLoaded.Unsubscribe(OnEncountersLoaded);
	}

	public void Load(SaveData loadedData)
	{
		m_ShopActive = loadedData.m_ShopActive;
		m_MissionBoardActive = loadedData.m_MissionBoardActive;
		m_SellingActive = loadedData.m_SellingActive;
		m_ScuActive = loadedData.m_ScuActive || loadedData.m_BlockPickupActive;
		m_RemoteChargingActive = loadedData.m_RemoteChargingActive;
		if (loadedData.m_Active)
		{
			m_ShopActive = true;
			m_MissionBoardActive = true;
			m_SellingActive = true;
			m_ScuActive = true;
		}
		m_VisibleOnRadar = !loadedData.m_HiddenOnRadar;
	}

	public void Save(ref SaveData outputData)
	{
		outputData.m_ShopActive = m_ShopActive;
		outputData.m_MissionBoardActive = m_MissionBoardActive;
		outputData.m_SellingActive = m_SellingActive;
		outputData.m_ScuActive = m_ScuActive;
		outputData.m_BlockPickupActive = false;
		outputData.m_HiddenOnRadar = !m_VisibleOnRadar;
	}

	public bool IsVendorSCU(BlockTypes blockType)
	{
		return blockType == BlockTypes.GSOVendor_SCU;
	}

	public void SetAllActive(bool active)
	{
		SetShopActive(active);
		SetMissionBoardActive(active);
		SetSellingActive(active);
		SetSCUActive(active);
		SetRemoteChargingActive(active);
	}

	public void SetShopActive(bool active)
	{
		if (m_ShopActive != active)
		{
			m_ShopActive = active;
			SendEvent();
		}
	}

	public void SetRemoteChargingActive(bool active)
	{
		if (m_RemoteChargingActive != active)
		{
			m_RemoteChargingActive = active;
			SendEvent();
		}
	}

	public void SetMissionBoardActive(bool active)
	{
		if (m_MissionBoardActive != active)
		{
			m_MissionBoardActive = active;
			SendEvent();
		}
	}

	public void SetSellingActive(bool active)
	{
		if (m_SellingActive != active)
		{
			m_SellingActive = active;
			SendEvent();
		}
	}

	public void SetSCUActive(bool active)
	{
		if (m_ScuActive != active)
		{
			m_ScuActive = active;
			SendEvent();
		}
	}

	public void SetVisibleOnRadar(bool visible)
	{
		m_VisibleOnRadar = visible;
	}

	private void SendEvent()
	{
		OnActiveChanged.Send(m_ShopActive, m_MissionBoardActive, m_SellingActive, m_ScuActive);
	}

	private void OnEncountersLoaded()
	{
		bool flag = Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(FactionSubTypes.GSO, 1, "1-X Vendor") || Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(FactionSubTypes.GSO, 1, "1-0 Co-op Campaign Init");
		if (flag != m_RemoteChargingActive)
		{
			m_RemoteChargingActive = flag;
			OnActiveChanged.Send(m_ShopActive, m_MissionBoardActive, m_SellingActive, m_ScuActive);
		}
	}
}
