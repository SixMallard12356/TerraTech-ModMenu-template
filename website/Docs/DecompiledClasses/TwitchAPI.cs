#define UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class TwitchAPI : Singleton.Manager<TwitchAPI>
{
	public enum StreamState
	{
		NoStream,
		TerraTechLive,
		OtherLive
	}

	public class StreamData
	{
		public string name;

		public string status;

		public string viewers;

		public string iconLink;

		public Texture2D icon;
	}

	[FormerlySerializedAs("refreshTime")]
	[SerializeField]
	private int m_RefreshTime = 60;

	[FormerlySerializedAs("channelNameToSearchFor")]
	[SerializeField]
	private string m_ChannelNameToSearchFor = "terratechgame";

	[FormerlySerializedAs("gameNameToSearchFor")]
	[SerializeField]
	private string m_GameNameToSearchFor = "TerraTech";

	private const string s_CheckStreamUrl = "https://api.twitch.tv/kraken/streams/";

	private const string s_SearchStreamsUrl = "https://api.twitch.tv/kraken/streams?game={0}";

	private const string s_ClientID = "7u6ldt76ttqngt33el0n4hdqjmwb6qa";

	private float m_Timer;

	public StreamState state { get; private set; }

	public List<StreamData> currentStreams { get; private set; }

	private IEnumerator CheckIfStreamLive(string streamName)
	{
		UnityWebRequest request = UnityWebRequest.Get("https://api.twitch.tv/kraken/streams/" + streamName);
		request.SetRequestHeader("Client-ID", "7u6ldt76ttqngt33el0n4hdqjmwb6qa");
		yield return request.SendWebRequest();
		if (request.isHttpError || request.isNetworkError || !request.error.NullOrEmpty())
		{
			d.Log(request.error);
			yield break;
		}
		currentStreams.Clear();
		Dictionary<string, object> dictionary = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
		object value = null;
		if (dictionary != null && dictionary.TryGetValue("stream", out value) && value != null)
		{
			state = StreamState.TerraTechLive;
			List<StreamData> streams = GetStreams(new List<object> { value });
			if (streams.Count != 0)
			{
				currentStreams.Add(streams[0]);
			}
		}
		if (currentStreams.Count != 0)
		{
			StartCoroutine(LoadStreamImages());
		}
		else
		{
			StartCoroutine(SearchForStreams(m_GameNameToSearchFor));
		}
	}

	private IEnumerator SearchForStreams(string game)
	{
		game = game.Replace(' ', '+');
		UnityWebRequest request = UnityWebRequest.Get($"https://api.twitch.tv/kraken/streams?game={game}");
		request.SetRequestHeader("Client-ID", "7u6ldt76ttqngt33el0n4hdqjmwb6qa");
		yield return request.SendWebRequest();
		if (request.isHttpError || request.isNetworkError || !request.error.NullOrEmpty())
		{
			d.Log(request.error);
		}
		else if (Json.Deserialize(request.downloadHandler.text) is Dictionary<string, object> dictionary)
		{
			currentStreams = GetStreams(dictionary["streams"] as List<object>, 300, 200);
			if (currentStreams.Count > 0)
			{
				StartCoroutine(LoadStreamImages());
				state = StreamState.OtherLive;
			}
			else
			{
				state = StreamState.NoStream;
			}
		}
		else
		{
			d.Log("there was a problem parsing the data");
		}
	}

	private List<StreamData> GetStreams(List<object> streamsData, int imageWidth = 150, int imageHeight = 80)
	{
		List<StreamData> list = new List<StreamData>();
		if (streamsData != null)
		{
			foreach (Dictionary<string, object> streamsDatum in streamsData)
			{
				if (streamsDatum == null || !(streamsDatum["channel"] is Dictionary<string, object> dictionary2) || !dictionary2.ContainsKey("status"))
				{
					continue;
				}
				StreamData streamData = new StreamData();
				streamData.name = (dictionary2["display_name"] as string) ?? string.Empty;
				streamData.status = (dictionary2["status"] as string) ?? string.Empty;
				object obj = streamsDatum["viewers"];
				streamData.viewers = ((obj != null) ? obj.ToString() : string.Empty);
				Dictionary<string, object> dictionary3 = streamsDatum["preview"] as Dictionary<string, object>;
				streamData.iconLink = string.Empty;
				if (dictionary3 != null)
				{
					object obj2 = dictionary3["template"];
					if (obj2 != null)
					{
						streamData.iconLink = obj2.ToString().Replace("{width}", string.Concat(imageWidth)).Replace("{height}", string.Concat(imageHeight));
					}
				}
				list.Add(streamData);
			}
		}
		return list;
	}

	private IEnumerator LoadStreamImages()
	{
		for (int i = 0; i < currentStreams.Count; i++)
		{
			StreamData data = currentStreams[i];
			UnityWebRequest getIconRequest = UnityWebRequestTexture.GetTexture(data.iconLink);
			getIconRequest.SetRequestHeader("Client-ID", "7u6ldt76ttqngt33el0n4hdqjmwb6qa");
			yield return getIconRequest.SendWebRequest();
			if (getIconRequest.isNetworkError || getIconRequest.isHttpError || !getIconRequest.error.NullOrEmpty())
			{
				d.Log("Could not load the icon: " + getIconRequest.error);
			}
			else if (data != null)
			{
				data.icon = ((DownloadHandlerTexture)getIconRequest.downloadHandler).texture;
			}
		}
	}

	private void OutDictionaryContent(Dictionary<string, object> toOut)
	{
		foreach (string key in toOut.Keys)
		{
			if (toOut[key] == null)
			{
				continue;
			}
			if (toOut[key].GetType() == typeof(Dictionary<string, object>))
			{
				OutDictionaryContent(toOut[key] as Dictionary<string, object>);
			}
			else if (toOut[key].GetType() == typeof(List<object>))
			{
				foreach (object item in (List<object>)toOut[key])
				{
					if (item.GetType() == typeof(Dictionary<string, object>))
					{
						OutDictionaryContent(item as Dictionary<string, object>);
					}
					else
					{
						d.Log(key + ": " + item);
					}
				}
			}
			else
			{
				d.Log(key + ": " + toOut[key]);
			}
		}
	}

	private void Awake()
	{
		currentStreams = new List<StreamData>();
		m_Timer = m_RefreshTime;
	}

	private void Update()
	{
		if (m_Timer >= (float)m_RefreshTime)
		{
			StartCoroutine(CheckIfStreamLive(m_ChannelNameToSearchFor));
			m_Timer = 0f;
		}
		m_Timer += Time.deltaTime;
	}
}
