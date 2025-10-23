using System.Collections.Generic;
using UnityEngine;

public class UITwitchChannelList : UIHUDElement
{
	public UITwitchStream m_StreamPrefab;

	public RectTransform m_LayoutGroup;

	private List<UITwitchStream> m_Streams = new List<UITwitchStream>();

	public override void Show(object context)
	{
		foreach (TwitchAPI.StreamData currentStream in Singleton.Manager<TwitchAPI>.inst.currentStreams)
		{
			UITwitchStream uITwitchStream = m_StreamPrefab.Spawn();
			uITwitchStream.GetComponent<RectTransform>().SetParent(m_LayoutGroup, worldPositionStays: false);
			uITwitchStream.transform.localScale = Vector3.one;
			uITwitchStream.SetData(currentStream);
			m_Streams.Add(uITwitchStream);
		}
		base.Show(context);
	}

	public override void Hide(object context)
	{
		foreach (UITwitchStream stream in m_Streams)
		{
			stream.transform.SetParent(null, worldPositionStays: false);
			stream.Recycle();
		}
		m_Streams.Clear();
		base.Hide(context);
	}
}
