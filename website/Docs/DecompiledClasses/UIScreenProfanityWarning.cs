using UnityEngine;
using UnityEngine.UI;

public class UIScreenProfanityWarning : UIScreen
{
	[SerializeField]
	private Button m_YesButton;

	[SerializeField]
	private Button m_NoButton;

	private bool m_WasAccepted;

	private void Awake()
	{
		m_YesButton.onClick.AddListener(HandleButtonYes);
		m_NoButton.onClick.AddListener(HandleButtonNo);
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
	}

	public override bool GoBack()
	{
		return base.GoBack();
	}

	public bool WasAccepted()
	{
		return m_WasAccepted;
	}

	private void HandleButtonYes()
	{
		m_WasAccepted = true;
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void HandleButtonNo()
	{
		m_WasAccepted = false;
		Singleton.Manager<ManUI>.inst.PopScreen();
	}
}
