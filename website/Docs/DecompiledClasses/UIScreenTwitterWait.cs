using UnityEngine;
using UnityEngine.UI;

public class UIScreenTwitterWait : UIScreen
{
	[SerializeField]
	private Text m_WaitText;

	[SerializeField]
	private RectTransform m_SpinnerImage;

	[SerializeField]
	private float m_SpinSpeed;

	private bool m_WaitForBrowser;

	public void Init(bool waitForBrowser)
	{
		m_WaitForBrowser = waitForBrowser;
		if (waitForBrowser)
		{
			m_SpinnerImage.gameObject.SetActive(value: true);
			m_WaitText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 2);
		}
		else
		{
			m_SpinnerImage.gameObject.SetActive(value: false);
			m_WaitText.text = "";
		}
	}

	public override void Hide()
	{
		if (m_WaitForBrowser)
		{
			Singleton.Manager<TwitterAPI>.inst.Cancel();
		}
		base.Hide();
	}

	private void Update()
	{
		m_SpinnerImage.transform.eulerAngles += Vector3.forward * Time.unscaledDeltaTime * m_SpinSpeed;
		if (!m_WaitForBrowser)
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
		}
	}
}
