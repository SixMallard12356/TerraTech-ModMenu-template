using UnityEngine;
using UnityEngine.UI;

namespace Snapshots;

public class UISnapshotTabHelper : MonoBehaviour
{
	[SerializeField]
	private VMSnapshotPanel m_ViewModel;

	[SerializeField]
	private VMSnapshotPanel.Platform m_Platform;

	[SerializeField]
	private Toggle m_Toggle;

	public Toggle Toggle => m_Toggle;

	private void OnPlatformChanged(VMSnapshotPanel.Platform platform)
	{
		m_Toggle.onValueChanged.RemoveListener(OnToggleChanged);
		m_Toggle.isOn = platform == m_Platform;
		m_Toggle.onValueChanged.AddListener(OnToggleChanged);
	}

	private void OnToggleChanged(bool isOn)
	{
		if (isOn)
		{
			m_ViewModel.SetSelectedPlatform(m_Platform);
		}
	}

	private void OnSpawn()
	{
		m_Toggle.onValueChanged.AddListener(OnToggleChanged);
		m_ViewModel.m_Platform.Bind(OnPlatformChanged);
	}

	private void OnRecycle()
	{
		m_ViewModel.m_Platform.Unbind(OnPlatformChanged);
	}
}
