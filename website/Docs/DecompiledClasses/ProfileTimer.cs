#define UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;

public class ProfileTimer
{
	public class Channel
	{
		public string name;

		public float timeMS;

		public int sampleCount;
	}

	private const float k_DampSpring = 0.1f;

	private const int k_InitChannelStorage = 20;

	private bool damped;

	private bool averaged;

	private Dictionary<string, Channel> m_Channels = new Dictionary<string, Channel>(20);

	private Stopwatch m_Stopwatch = new Stopwatch();

	private string m_CurrentChannelName;

	private bool m_ReportUpdateReady;

	public ProfileTimer(bool damped = true, bool averaged = false)
	{
		this.damped = damped;
		this.averaged = averaged;
	}

	public void Start(string channelName)
	{
		if (!m_CurrentChannelName.NullOrEmpty() || m_Stopwatch.IsRunning)
		{
			d.LogWarning("stopwatch already running!");
			return;
		}
		m_CurrentChannelName = channelName;
		m_Stopwatch.Reset();
		m_Stopwatch.Start();
	}

	public void Stop()
	{
		if (m_CurrentChannelName.NullOrEmpty() || !m_Stopwatch.IsRunning)
		{
			d.LogWarning("stopwatch not running!");
			return;
		}
		m_Stopwatch.Stop();
		float num = (float)m_Stopwatch.Elapsed.TotalMilliseconds;
		if (m_Channels.TryGetValue(m_CurrentChannelName, out var value))
		{
			if (averaged)
			{
				num += value.timeMS;
			}
			else if (damped)
			{
				num = num * 0.1f + value.timeMS * 0.9f;
			}
			value.timeMS = num;
			value.sampleCount++;
		}
		else
		{
			m_Channels[m_CurrentChannelName] = new Channel
			{
				name = m_CurrentChannelName,
				timeMS = num,
				sampleCount = 1
			};
		}
		m_CurrentChannelName = null;
		m_ReportUpdateReady = true;
	}

	public void UpdateReport(List<string> reportStrings)
	{
		if (!m_ReportUpdateReady)
		{
			return;
		}
		m_ReportUpdateReady = false;
		reportStrings.Clear();
		foreach (KeyValuePair<string, Channel> channel in m_Channels)
		{
			if (averaged)
			{
				reportStrings.Add(string.Format("{0}ms [{1}|{2}] {3}", channel.Value.timeMS.ToString("f3"), (channel.Value.timeMS / (float)channel.Value.sampleCount).ToString("#0.0e0"), channel.Value.sampleCount, channel.Value.name));
			}
			else
			{
				reportStrings.Add(string.Format("{0}ms {1}", channel.Value.timeMS.ToString("f3"), channel.Value.name));
			}
			channel.Value.sampleCount = 0;
			if (!damped)
			{
				channel.Value.timeMS = 0f;
			}
		}
	}

	public void Reset()
	{
		foreach (KeyValuePair<string, Channel> channel in m_Channels)
		{
			channel.Value.timeMS = 0f;
			channel.Value.sampleCount = 1;
		}
	}
}
