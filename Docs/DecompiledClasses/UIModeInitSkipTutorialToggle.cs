using UnityEngine;

public class UIModeInitSkipTutorialToggle : UIModeInitToggle
{
	[SerializeField]
	private GameObject m_ToggleParentObject;

	public override void InitComponent()
	{
		bool completedTutorial = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_TutorialSkipSettings.m_CompletedTutorial;
		m_ToggleParentObject.SetActive(completedTutorial);
		base.InitComponent();
	}

	protected override bool IsToggledByDefault()
	{
		ManProfile.TutorialSkipSettings tutorialSkipSettings = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_TutorialSkipSettings;
		if (tutorialSkipSettings.m_CompletedTutorial)
		{
			return tutorialSkipSettings.m_WantsSkip;
		}
		return false;
	}
}
