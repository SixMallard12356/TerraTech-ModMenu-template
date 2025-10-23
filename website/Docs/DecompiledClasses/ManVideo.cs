using UnityEngine.UI;
using UnityEngine.Video;

public class ManVideo : Singleton.Manager<ManVideo>
{
	public RawImage m_Image;

	private VideoPlayer m_VideoPlayer;

	private bool m_Playing;

	public bool IsPlaying
	{
		get
		{
			if (m_VideoPlayer != null)
			{
				return m_VideoPlayer.isPlaying;
			}
			return false;
		}
	}

	public void Play(VideoPlayer videoPlayer)
	{
		m_VideoPlayer = videoPlayer;
		if (m_Image != null && m_VideoPlayer != null)
		{
			m_Image.texture = m_VideoPlayer.texture;
			base.gameObject.SetActive(value: true);
			m_VideoPlayer.Stop();
			m_VideoPlayer.Play();
			m_Playing = true;
		}
	}

	public void Pause()
	{
		m_VideoPlayer.Pause();
	}

	public void Stop()
	{
		m_VideoPlayer.Stop();
	}

	public void Awake()
	{
		Singleton.DoOnceAfterStart(delegate
		{
			base.gameObject.SetActive(value: false);
		});
	}

	private void Update()
	{
		if (m_Playing)
		{
			bool flag = true;
			if ((bool)m_VideoPlayer)
			{
				flag = !m_VideoPlayer.isPlaying;
			}
			if (flag)
			{
				m_Playing = false;
				Stop();
				base.gameObject.SetActive(value: false);
			}
		}
	}
}
