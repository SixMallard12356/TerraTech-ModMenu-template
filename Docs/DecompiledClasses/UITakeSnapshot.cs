#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UITakeSnapshot : MonoBehaviour
{
	[SerializeField]
	private Button m_SnapshotButton;

	private void OnButtonClicked()
	{
		d.Assert(Singleton.playerTank, "UITakeSnapshot::OnButtonClicked - No player tank available, cannot take snapshot!");
		if ((bool)Singleton.playerTank)
		{
			Singleton.Manager<ManScreenshot>.inst.TakeSnapshotAndShowUI();
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool isSwitchedTo)
	{
		bool snapshotButtonEnabled = (bool)tank && isSwitchedTo;
		SetSnapshotButtonEnabled(snapshotButtonEnabled);
	}

	private void SetSnapshotButtonEnabled(bool enabled)
	{
		if (m_SnapshotButton != null)
		{
			m_SnapshotButton.interactable = enabled;
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		d.Assert(m_SnapshotButton != null, "UITakeSnapshot '" + base.name + "' does not have SnapshotButton set!");
		m_SnapshotButton.onClick.AddListener(OnButtonClicked);
		SetSnapshotButtonEnabled(Singleton.playerTank != null);
	}

	private void OnRecycle()
	{
		m_SnapshotButton.onClick.RemoveListener(OnButtonClicked);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
	}
}
