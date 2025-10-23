using UnityEngine;

public class UIButtonGoToScreen : MonoBehaviour
{
	[SerializeField]
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	private ManUI.ScreenType m_ScreenType;

	[SerializeField]
	private bool m_AsPopup;

	public virtual void OnButtonClicked()
	{
		if (m_AsPopup)
		{
			UIScreen screen = Singleton.Manager<ManUI>.inst.GetScreen(m_ScreenType);
			Singleton.Manager<ManUI>.inst.PushScreen(screen, ManUI.PauseType.None, asPopup: true);
		}
		else
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(m_ScreenType);
		}
	}
}
