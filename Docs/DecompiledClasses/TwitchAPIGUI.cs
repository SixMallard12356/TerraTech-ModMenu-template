using UnityEngine;

public class TwitchAPIGUI : MonoBehaviour
{
	private enum DismissState
	{
		NoDismiss,
		DismissOthers,
		DismissAll
	}

	private enum TwitchUIComponents
	{
		TTStream,
		OtherStream,
		StreamList
	}

	[SerializeField]
	private LocalisedString topLabelTextTTstream;

	[SerializeField]
	private LocalisedString bottomLabelTextTTstream;

	[SerializeField]
	private LocalisedString topLabelTextOtherStreamsSingle;

	[SerializeField]
	private LocalisedString topLabelTextOtherStreamsMultiple;

	[SerializeField]
	private LocalisedString bottomLabelTextOtherStream;

	[SerializeField]
	private float showTwitchInterval = 300f;

	[SerializeField]
	private float showTTStreamInterval = 300f;

	[SerializeField]
	private float showTTStreamFor = 10f;

	[SerializeField]
	private float showTwitchFor = 60f;

	[SerializeField]
	private float dismissTime = 600f;

	private DismissState dismissState;

	private bool m_ShowTTStream;

	private bool m_ShowOtherStreams;

	private bool m_TemporarlyDismiss;

	private float m_TwitchTimer;

	private float m_TtTwitchTimer;

	private float m_DismissTimer;

	private bool m_WasShowingTwitchHUD;

	private Bitfield<TwitchUIComponents> m_ShowingTwitchHUD = new Bitfield<TwitchUIComponents>();

	public bool IsOn()
	{
		if (m_ShowOtherStreams || m_ShowTTStream)
		{
			return SKU.TwitchEnabled;
		}
		return false;
	}

	public void UpdateGUI()
	{
		if (!m_TemporarlyDismiss && dismissState != DismissState.DismissAll && ((m_ShowOtherStreams && dismissState != DismissState.DismissOthers) || (m_ShowTTStream && Singleton.Manager<TwitchAPI>.inst.state == TwitchAPI.StreamState.TerraTechLive)))
		{
			string topLabel;
			string value;
			if (Singleton.Manager<TwitchAPI>.inst.state == TwitchAPI.StreamState.TerraTechLive)
			{
				topLabel = topLabelTextTTstream.Value;
				value = bottomLabelTextTTstream.Value;
			}
			else
			{
				topLabel = ((Singleton.Manager<TwitchAPI>.inst.currentStreams.Count != 1) ? string.Format(topLabelTextOtherStreamsMultiple.Value, Singleton.Manager<TwitchAPI>.inst.currentStreams.Count) : topLabelTextOtherStreamsSingle.Value);
				value = bottomLabelTextOtherStream.Value;
			}
			if (Singleton.Manager<TwitchAPI>.inst.state == TwitchAPI.StreamState.TerraTechLive && !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TwitchButtonTT))
			{
				UITwitchButton.TwitchButtonParams twitchButtonParams = new UITwitchButton.TwitchButtonParams
				{
					topLabel = topLabel,
					bottomLabel = value,
					onClick = OpenTT,
					onClose = Dismiss
				};
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TwitchButtonTT, twitchButtonParams);
				m_ShowingTwitchHUD.Add(0);
				m_ShowingTwitchHUD.Remove(1);
			}
			else if (Singleton.Manager<TwitchAPI>.inst.state == TwitchAPI.StreamState.OtherLive && !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TwitchButtonOther))
			{
				UITwitchButton.TwitchButtonParams twitchButtonParams2 = new UITwitchButton.TwitchButtonParams
				{
					topLabel = topLabel,
					bottomLabel = value,
					onClick = ToggleList,
					onClose = Dismiss
				};
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TwitchButtonOther, twitchButtonParams2);
				m_ShowingTwitchHUD.Add(1);
				m_ShowingTwitchHUD.Remove(0);
			}
		}
		else
		{
			m_ShowingTwitchHUD.Clear();
		}
		if (m_WasShowingTwitchHUD != m_ShowingTwitchHUD.AnySet && !m_ShowingTwitchHUD.AnySet)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TwitchButtonTT);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TwitchButtonOther);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TwitchStreamList);
		}
		m_WasShowingTwitchHUD = m_ShowingTwitchHUD.AnySet;
	}

	private void ToggleList()
	{
		bool flag = Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TwitchStreamList);
		if (flag)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TwitchStreamList);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TwitchStreamList);
		}
		m_ShowingTwitchHUD.Set(1, !flag);
	}

	private void OpenTT()
	{
		Application.OpenURL("www.twitch.tv/" + Singleton.Manager<TwitchAPI>.inst.currentStreams[0].name);
		if (!Singleton.Manager<ManUI>.inst.IsScreenInStack(ManUI.ScreenType.MainMenu))
		{
			Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: true);
		}
	}

	private void Dismiss()
	{
		if (Singleton.Manager<TwitchAPI>.inst.state == TwitchAPI.StreamState.TerraTechLive)
		{
			dismissState = DismissState.DismissAll;
			return;
		}
		m_TemporarlyDismiss = true;
		dismissState = DismissState.DismissOthers;
	}

	private void Update()
	{
		if (!(Singleton.Manager<ManHUD>.inst.CurrentHUD != null))
		{
			return;
		}
		UpdateGUI();
		if (dismissState == DismissState.DismissAll || Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TwitchStreamList))
		{
			return;
		}
		if (m_TemporarlyDismiss)
		{
			m_DismissTimer += Time.deltaTime;
			if (m_DismissTimer >= dismissTime)
			{
				m_TemporarlyDismiss = false;
				m_DismissTimer = 0f;
			}
		}
		m_TwitchTimer += Time.deltaTime;
		m_TtTwitchTimer += Time.deltaTime;
		if (m_TwitchTimer > showTTStreamInterval && m_TwitchTimer < showTTStreamInterval + showTTStreamFor)
		{
			m_ShowTTStream = true;
		}
		else if (m_TwitchTimer > showTTStreamInterval + showTTStreamFor)
		{
			m_TtTwitchTimer = 0f;
			m_ShowTTStream = false;
		}
		if (m_TwitchTimer > showTwitchInterval && m_TwitchTimer < showTwitchInterval + showTwitchFor)
		{
			m_ShowOtherStreams = false;
		}
		else if (m_TwitchTimer > showTwitchInterval + showTwitchFor)
		{
			m_TwitchTimer = 0f;
			m_ShowOtherStreams = false;
		}
	}
}
