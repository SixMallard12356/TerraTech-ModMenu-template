using UnityEngine;
using UnityEngine.UI;

public class UITwitchStream : MonoBehaviour
{
	public Image m_Preview;

	public Button m_Button;

	public Text m_StreamName;

	public Text m_Viewers;

	private TwitchAPI.StreamData m_MyData;

	public void SetData(TwitchAPI.StreamData data)
	{
		m_MyData = data;
		m_Preview.sprite = null;
		if ((bool)data.icon)
		{
			m_Preview.sprite = Sprite.Create(data.icon, new Rect(0f, 0f, data.icon.width, data.icon.height), new Vector2(0.5f, 0.5f));
		}
		m_StreamName.text = data.name;
		m_Viewers.text = "Viewers: " + data.viewers;
	}

	private void OnClick()
	{
		ManUI.PauseType pauseType = ManUI.PauseType.None;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Attract)
		{
			pauseType = ManUI.PauseType.Pause;
		}
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitchScreenAccept) as UIScreenTwitchAccept).Init("http://www.twitch.tv/" + m_MyData.name);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.TwitchScreenAccept, pauseType);
	}

	private void Awake()
	{
		m_Button.onClick.AddListener(OnClick);
	}

	private void Update()
	{
		if (m_Preview.sprite == null && m_MyData.icon != null)
		{
			m_Preview.sprite = Sprite.Create(m_MyData.icon, new Rect(0f, 0f, m_MyData.icon.width, m_MyData.icon.height), new Vector2(0.5f, 0.5f));
		}
	}
}
