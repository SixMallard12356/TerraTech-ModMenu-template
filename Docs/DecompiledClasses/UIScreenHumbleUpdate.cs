using UnityEngine;
using UnityEngine.UI;

public class UIScreenHumbleUpdate : UIScreen
{
	public Text m_Notes;

	public Text m_Title;

	public Button m_GetItNow;

	public Button m_CloseButton;

	private float m_Timer;

	private float m_DisableCloseTime;

	private string m_Link;

	public void Setup(UpdateNotifier.UpdateNote updateInfo, float dismissTime)
	{
		m_Title.text = updateInfo.m_Title;
		m_Notes.text = updateInfo.m_Notes.Replace('~', '\n');
		m_Link = updateInfo.m_Link;
		m_DisableCloseTime = dismissTime;
	}

	private void GetItNow()
	{
		Application.OpenURL(m_Link);
	}

	private void Cancel()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_Timer = 0f;
		m_CloseButton.gameObject.SetActive(value: false);
		bool active = m_Link != null;
		m_GetItNow.gameObject.SetActive(active);
		BlockScreenExit(exitBlocked: true);
	}

	private void Start()
	{
		m_GetItNow.onClick.AddListener(GetItNow);
		m_CloseButton.onClick.AddListener(Cancel);
	}

	private void Update()
	{
		if (m_Timer >= m_DisableCloseTime)
		{
			m_CloseButton.gameObject.SetActive(value: true);
			BlockScreenExit(exitBlocked: false);
		}
		m_Timer += Time.deltaTime;
	}
}
