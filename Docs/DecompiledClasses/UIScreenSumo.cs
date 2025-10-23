using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumo : UIScreen
{
	[SerializeField]
	private GameObject m_ContestantLayoutParent;

	[SerializeField]
	private UISumoPreset m_UIPresetPrefab;

	[SerializeField]
	private Button m_FightButton;

	[SerializeField]
	private int m_MaxNumContestants = 4;

	private UISumoPreset[] m_Contestants;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.SumoShowdown)
		{
			ClearContestants();
			IInventory<BlockTypes> inventory = new SingleplayerInventory();
			if (Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("Inventory", out var settingData) && settingData != null)
			{
				(settingData as InventoryAsset).BuildInventory(inventory);
			}
			for (int i = 0; i < m_Contestants.Length; i++)
			{
				m_Contestants[i].SetAvailableInventory(inventory);
			}
		}
		UpdateStartButton();
	}

	public override void Hide()
	{
		base.Hide();
		if (m_Contestants == null)
		{
			return;
		}
		for (int i = 0; i < m_Contestants.Length; i++)
		{
			if (m_Contestants[i].State == UISumoPreset.ContestantState.Selecting)
			{
				m_Contestants[i].ClearContestant();
			}
		}
	}

	public void SetContestants()
	{
		ModeSumo.Contestants contestants = new ModeSumo.Contestants();
		for (int i = 0; i < m_Contestants.Length; i++)
		{
			if (m_Contestants[i].State == UISumoPreset.ContestantState.Ready)
			{
				contestants.AddContestant(m_Contestants[i].Snapshot, i);
			}
		}
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("contestants", contestants);
	}

	private void ClearContestants()
	{
		if (m_Contestants != null)
		{
			for (int i = 0; i < m_Contestants.Length; i++)
			{
				m_Contestants[i].ClearContestant();
			}
		}
	}

	private void UpdateStartButton()
	{
		bool flag = false;
		int num = 0;
		if (m_Contestants != null)
		{
			for (int i = 0; i < m_Contestants.Length; i++)
			{
				switch (m_Contestants[i].State)
				{
				case UISumoPreset.ContestantState.Selecting:
					break;
				case UISumoPreset.ContestantState.Ready:
					num++;
					continue;
				default:
					continue;
				}
				flag = true;
				break;
			}
		}
		bool flag2 = !flag && num >= 2;
		m_FightButton.gameObject.SetActive(flag2);
		m_FightButton.interactable = flag2;
	}

	private void OnContestantStateChanged(UISumoPreset preset, UISumoPreset.ContestantState newState)
	{
		UpdateStartButton();
	}

	private void Awake()
	{
		m_Contestants = new UISumoPreset[m_MaxNumContestants];
		for (int i = 0; i < m_Contestants.Length; i++)
		{
			m_Contestants[i] = m_UIPresetPrefab.Spawn();
			m_Contestants[i].GetComponent<RectTransform>().SetParent(m_ContestantLayoutParent.transform, worldPositionStays: false);
			m_Contestants[i].OnContestantStateChanged.Subscribe(OnContestantStateChanged);
		}
	}
}
