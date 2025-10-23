using UnityEngine;

public class UIModeSpecificGameObject : MonoBehaviour
{
	[SerializeField]
	[EnumFlag]
	private ManGameMode.GameType m_VisibleInModes;

	[SerializeField]
	private bool m_InDemo;

	[SerializeField]
	private bool m_InShow;

	private void UpdateVisibility(Mode startedMode)
	{
		ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
		bool num = (int)((uint)m_VisibleInModes & (uint)(1 << (int)currentGameType)) > 0;
		bool flag = !m_InDemo && !m_InShow;
		bool flag2 = num && flag;
		if (flag2 != base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(flag2);
		}
	}

	private void OnPool()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(UpdateVisibility);
	}

	private void OnDepool()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Unsubscribe(UpdateVisibility);
	}
}
