#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ManOnScreenMessages : Singleton.Manager<ManOnScreenMessages>
{
	public enum MessageShowState
	{
		ShowMessage,
		HoldMessage,
		EndMessage
	}

	public enum MessagePriority
	{
		Low,
		Medium,
		High
	}

	public enum Speaker
	{
		None,
		GSOGeneric,
		GCGeneric,
		VENGeneric,
		HEGeneric,
		CraftyMike,
		AITurret,
		BFGeneric,
		BFNpc,
		BridgeTroll,
		SpiderKing,
		Groundhog,
		BomberCommand,
		AgentPow,
		SuzieVroom,
		Repulsor,
		MrPushy,
		Wimpy,
		Mighty,
		Minion,
		MinionGroup,
		Wheely,
		Atom,
		RRGeneric,
		TRUBL,
		BUBL,
		Zozo,
		Vana,
		Rusty,
		SJGeneric
	}

	public enum Side
	{
		Left,
		Right
	}

	[Serializable]
	public struct SpeakerData
	{
		public LocalisedString m_SpeakerTitle;

		public Sprite m_BehindSpeakerImage;

		public Sprite m_SpeakerImage;

		public Sprite m_InFrontOfSpeakerImage;
	}

	[Serializable]
	public struct OnScreenMessageLine
	{
		public string m_MessageBank;

		public string m_MessageID;

		public string m_RawText;

		public Localisation.GlyphInfo[] m_InlineGlyphs;

		public bool NullOrEmpty()
		{
			if (m_RawText.NullOrEmpty())
			{
				return m_MessageID.NullOrEmpty();
			}
			return false;
		}

		public string GetString()
		{
			if (!m_RawText.NullOrEmpty())
			{
				return m_RawText;
			}
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(m_MessageBank, m_MessageID, m_InlineGlyphs);
		}
	}

	[Serializable]
	public class OnScreenMessage
	{
		public OnScreenMessageLine[] m_Message;

		public Speaker m_Speaker = Speaker.GSOGeneric;

		public Side m_Side;

		public MessagePriority m_Priority;

		public bool m_Hold;

		public string m_Tag;

		public EventNoParams Callback;

		public OnScreenMessage()
		{
		}

		public OnScreenMessage(string[] messages, MessagePriority priority, bool hold = false, string tag = null, Speaker speaker = Speaker.GSOGeneric, Side side = Side.Left)
		{
			m_Message = new OnScreenMessageLine[messages.Length];
			for (int i = 0; i < messages.Length; i++)
			{
				m_Message[i].m_RawText = messages[i];
			}
			m_Priority = priority;
			m_Hold = hold;
			m_Speaker = speaker;
			m_Side = side;
			m_Tag = tag;
		}

		public OnScreenMessage(LocalisedString[] messages, MessagePriority priority, bool hold = false, string tag = null, Speaker speaker = Speaker.GSOGeneric, Side side = Side.Left)
		{
			m_Message = new OnScreenMessageLine[messages.Length];
			for (int i = 0; i < messages.Length; i++)
			{
				m_Message[i].m_MessageBank = messages[i].m_Bank;
				m_Message[i].m_MessageID = messages[i].m_Id;
				m_Message[i].m_InlineGlyphs = messages[i].m_InlineGlyphs;
			}
			m_Priority = priority;
			m_Hold = hold;
			m_Speaker = speaker;
			m_Side = side;
			m_Tag = tag;
		}

		public OnScreenMessage(LocalisationEnums.FTUEMessages message, MessagePriority priority, bool hold = false, string tag = null)
		{
			m_Message = new OnScreenMessageLine[1];
			m_Message[0].m_RawText = Singleton.Manager<Localisation>.inst.GetLocalisedString(message);
			m_Priority = priority;
			m_Hold = hold;
			m_Tag = tag;
		}
	}

	private class RTReplacement
	{
		public string stringValue;

		public int startPos;

		public int endPos;

		public bool closed;

		public RTReplacement(string val, int start, int end)
		{
			stringValue = val;
			startPos = start;
			endPos = end;
			closed = false;
		}
	}

	public class BlockExplainer
	{
		public OnScreenMessage m_Message;

		public Func<TankBlock, bool> RecogniseBlock;

		public bool m_HasExplained;

		public BlockExplainer(OnScreenMessage message, Func<TankBlock, bool> recogniserFn)
		{
			m_Message = message;
			RecogniseBlock = recogniserFn;
			m_HasExplained = false;
		}
	}

	[SerializeField]
	private UIMessageHUD m_MessageHUDPrefab;

	[SerializeField]
	private Transform m_IgnoreRaycastCanvas;

	[SerializeField]
	private float m_CharactersPerSecond = 5f;

	[SerializeField]
	private float m_MsgHoldTime = 5f;

	[SerializeField]
	private float m_NextLineFadeTime = 0.5f;

	[SerializeField]
	private float m_EndMsgFadeTime = 0.75f;

	[SerializeField]
	private FMODEvent m_TextBlipSfxEvent;

	[SerializeField]
	private FMODEvent m_NewLineSfxEvent;

	[SerializeField]
	[EnumArray(typeof(Speaker))]
	private SpeakerData[] m_SpeakerData;

	[SerializeField]
	private bool m_DEBUG_OutputMessageToLog;

	private OnScreenMessage m_Current;

	private List<OnScreenMessage>[] m_Messages;

	private MessageShowState m_MessageState;

	private int m_CurrentMsgIndex;

	private bool m_ClearCurrentMsg;

	private float m_Timer;

	private float m_MsgShowTime = 1f;

	private float m_FadeTime = 1f;

	private float m_LastMessageTime;

	private float m_TimeMultiplier = 1f;

	private string m_MessageToShow;

	private List<RTReplacement> m_RichTextReplacementStartTags = new List<RTReplacement>();

	private List<RTReplacement> m_RichTextReplacementEndTags = new List<RTReplacement>();

	private UIMessageHUD m_MessageHUD;

	private FMODEventInstance m_TextBlipSfxInstance;

	private readonly string[] m_RichTextStings = new string[4] { "color", "size", "b", "i" };

	public bool FastTalk
	{
		get
		{
			return m_TimeMultiplier > 1f;
		}
		set
		{
			m_TimeMultiplier = (value ? 4f : 1f);
		}
	}

	public SpeakerData GetSpeakerData(Speaker speaker)
	{
		if (speaker >= Speaker.None && (int)speaker < m_SpeakerData.Length)
		{
			return m_SpeakerData[(int)speaker];
		}
		d.LogError("ManOnScreenMessages.GetSpeakerData - Couldn't Find Speaker");
		return m_SpeakerData[0];
	}

	public bool IsCurrentMessage(OnScreenMessage message)
	{
		return m_Current == message;
	}

	public void ClearAllMessages(bool instant = false)
	{
		for (int i = 0; i < m_Messages.Length; i++)
		{
			m_Messages[i].Clear();
		}
		if (m_Current != null)
		{
			RemoveCurrentMessage(instant);
		}
	}

	public void ClearMessagesWithTag(string tag, bool clearCurrent, bool instant = false)
	{
		for (int i = 0; i < m_Messages.Length; i++)
		{
			for (int num = m_Messages[i].Count - 1; num >= 0; num--)
			{
				if (m_Messages[i][num].m_Tag == tag)
				{
					m_Messages[i].RemoveAt(num);
				}
			}
		}
		if (m_Current != null && clearCurrent && m_Current.m_Tag == tag)
		{
			RemoveCurrentMessage(instant);
		}
	}

	public void Hide(bool hide)
	{
		m_MessageHUD.Hide(hide);
	}

	public void REMOVE_ME_I_DO_NOTHING_AddMessage(OnScreenMessage message, bool boolVal)
	{
	}

	public bool IsValidMessage(OnScreenMessage message)
	{
		if (message.m_Message == null)
		{
			return false;
		}
		for (int i = 0; i < message.m_Message.Length; i++)
		{
			if (message.m_Message[i].NullOrEmpty())
			{
				return false;
			}
		}
		return true;
	}

	public int GetHighestUsedPriority()
	{
		int num = -1;
		if (m_Current != null && !m_ClearCurrentMsg)
		{
			num = (int)m_Current.m_Priority;
		}
		for (int num2 = m_Messages.Length - 1; num2 > num; num2--)
		{
			if (m_Messages[num2].Count > 0)
			{
				num = num2;
				break;
			}
		}
		return num;
	}

	public void AddMessage(OnScreenMessage message)
	{
		if (!IsValidMessage(message) || IsInQueue(message) || m_Current == message)
		{
			return;
		}
		bool flag = true;
		if (m_Current != null && !m_ClearCurrentMsg)
		{
			MessagePriority priority = m_Current.m_Priority;
			if (priority <= message.m_Priority)
			{
				switch (message.m_Priority)
				{
				case MessagePriority.High:
					m_Messages[(int)priority].Insert(0, m_Current);
					m_Messages[(int)message.m_Priority].Insert(0, message);
					m_Current = null;
					flag = false;
					break;
				case MessagePriority.Medium:
					if (message.m_Priority != priority)
					{
						m_Messages[(int)priority].Insert(0, m_Current);
					}
					m_Messages[(int)message.m_Priority].Clear();
					m_Current = null;
					break;
				}
			}
		}
		if (flag)
		{
			if (message.m_Priority == MessagePriority.Medium)
			{
				m_Messages[(int)message.m_Priority].Clear();
			}
			m_Messages[(int)message.m_Priority].Add(message);
		}
	}

	public void RemoveMessage(OnScreenMessage message, bool instant = false)
	{
		int priority = (int)message.m_Priority;
		if (message == m_Current)
		{
			RemoveCurrentMessage(instant);
			return;
		}
		for (int i = 0; i < m_Messages[priority].Count; i++)
		{
			if (m_Messages[priority][i] == message)
			{
				m_Messages[priority].RemoveAt(i);
				break;
			}
		}
	}

	public bool IsInQueue(OnScreenMessage message)
	{
		int priority = (int)message.m_Priority;
		for (int i = 0; i < m_Messages[priority].Count; i++)
		{
			if (m_Messages[priority][i] == message)
			{
				return true;
			}
		}
		return false;
	}

	public void ShowCanvas(bool show)
	{
		if (m_IgnoreRaycastCanvas.gameObject.activeSelf != show)
		{
			m_IgnoreRaycastCanvas.gameObject.SetActive(show);
		}
	}

	private void ShowMessage(string message)
	{
		m_MessageHUD.ShowMessage(message);
	}

	private void RemoveCurrentMessage(bool instant = false)
	{
		if (!m_ClearCurrentMsg)
		{
			m_ClearCurrentMsg = true;
			EnterMessageState(MessageShowState.EndMessage, instant);
		}
	}

	private void OnLanguageChanged()
	{
		if (m_Current != null && m_Current.m_Message != null && m_CurrentMsgIndex >= 0 && m_CurrentMsgIndex < m_Current.m_Message.Length)
		{
			if (m_MessageState == MessageShowState.ShowMessage)
			{
				GetRichTextReplacements(m_Current.m_Message[m_CurrentMsgIndex].GetString());
				ShowMessage("");
			}
			else
			{
				ShowMessage(m_Current.m_Message[m_CurrentMsgIndex].GetString());
			}
		}
	}

	private void EnterMessageState(MessageShowState messageState, bool instant = false)
	{
		switch (messageState)
		{
		case MessageShowState.ShowMessage:
			GetRichTextReplacements(m_Current.m_Message[m_CurrentMsgIndex].GetString());
			if (m_NewLineSfxEvent.IsValid())
			{
				m_NewLineSfxEvent.PlayOneShot();
			}
			if (m_TextBlipSfxEvent.IsValid() && !m_TextBlipSfxInstance.IsInited)
			{
				m_TextBlipSfxInstance = m_TextBlipSfxEvent.PlayEvent();
			}
			m_MsgShowTime = (instant ? 0f : ((float)m_MessageToShow.Length / m_CharactersPerSecond));
			break;
		case MessageShowState.HoldMessage:
			if (m_TextBlipSfxInstance.IsInited)
			{
				m_TextBlipSfxInstance.StopAndRelease();
			}
			ShowMessage(m_Current.m_Message[m_CurrentMsgIndex].GetString());
			break;
		case MessageShowState.EndMessage:
			if (m_TextBlipSfxInstance.IsInited)
			{
				m_TextBlipSfxInstance.StopAndRelease();
			}
			if (instant)
			{
				m_FadeTime = 0f;
			}
			else if ((m_CurrentMsgIndex < m_Current.m_Message.Length - 1 || m_Current.m_Hold) && !m_ClearCurrentMsg)
			{
				m_FadeTime = m_NextLineFadeTime;
			}
			else
			{
				m_FadeTime = m_EndMsgFadeTime;
			}
			break;
		}
		m_Timer = 0f;
		m_MessageState = messageState;
	}

	private void GetRichTextReplacements(string message)
	{
		m_RichTextReplacementStartTags.Clear();
		m_RichTextReplacementEndTags.Clear();
		StringBuilder stringBuilder = new StringBuilder(message.Length);
		char[] array = message.ToCharArray(0, message.Length);
		for (int i = 0; i < message.Length; i++)
		{
			bool flag = true;
			if (array[i] == '<' && i < message.Length - 1)
			{
				int num = i + 1;
				bool flag2 = false;
				if (array[i + 1] == '/')
				{
					flag2 = true;
					num++;
				}
				for (int j = num; j < message.Length; j++)
				{
					if (array[j] != '>')
					{
						continue;
					}
					bool flag3 = false;
					string text = message.Substring(i, j - i + 1);
					for (int k = 0; k < m_RichTextStings.Length; k++)
					{
						if (!text.Contains(m_RichTextStings[k]))
						{
							continue;
						}
						if (flag2)
						{
							int start = -1;
							for (int num2 = m_RichTextReplacementStartTags.Count - 1; num2 >= 0; num2--)
							{
								if (m_RichTextReplacementStartTags[num2].stringValue.Contains(m_RichTextStings[k]) && !m_RichTextReplacementStartTags[num2].closed)
								{
									start = m_RichTextReplacementStartTags[num2].startPos;
									m_RichTextReplacementStartTags[num2].closed = true;
									break;
								}
							}
							RTReplacement item = new RTReplacement(text, start, stringBuilder.Length);
							m_RichTextReplacementEndTags.Add(item);
						}
						else
						{
							RTReplacement item2 = new RTReplacement(text, stringBuilder.Length, 0);
							m_RichTextReplacementStartTags.Add(item2);
						}
						flag3 = true;
						break;
					}
					if (flag3)
					{
						flag = false;
						i = j;
						break;
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append(array[i]);
			}
		}
		m_MessageToShow = stringBuilder.ToString();
	}

	private void UpdateMessageState()
	{
		switch (m_MessageState)
		{
		case MessageShowState.ShowMessage:
			if (m_MessageHUD.Alpha != 1f)
			{
				m_MessageHUD.Alpha = 1f;
			}
			m_MessageHUD.SetSpeaker(m_Current.m_Speaker, m_Current.m_Side);
			if (m_MsgShowTime > Mathf.Epsilon && m_Timer <= m_MsgShowTime)
			{
				int num = (int)(m_Timer / m_MsgShowTime * (float)m_MessageToShow.Length);
				char[] array = m_MessageToShow.ToCharArray(0, num);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < m_RichTextReplacementStartTags.Count; j++)
					{
						if (m_RichTextReplacementStartTags[j].startPos == i)
						{
							stringBuilder.Append(m_RichTextReplacementStartTags[j].stringValue);
						}
					}
					for (int k = 0; k < m_RichTextReplacementEndTags.Count; k++)
					{
						if (m_RichTextReplacementEndTags[k].endPos == i)
						{
							stringBuilder.Append(m_RichTextReplacementEndTags[k].stringValue);
						}
					}
					stringBuilder.Append(array[i]);
				}
				if (num < m_MessageToShow.Length)
				{
					for (int l = 0; l < m_RichTextReplacementEndTags.Count; l++)
					{
						if (m_RichTextReplacementEndTags[l].startPos <= num - 1 && m_RichTextReplacementEndTags[l].endPos > num - 1)
						{
							stringBuilder.Append(m_RichTextReplacementEndTags[l].stringValue);
						}
					}
				}
				ShowMessage(stringBuilder.ToString());
				m_Timer += Time.deltaTime * m_TimeMultiplier;
			}
			else
			{
				EnterMessageState(MessageShowState.HoldMessage);
			}
			break;
		case MessageShowState.HoldMessage:
			if (m_Timer <= m_MsgHoldTime)
			{
				m_Timer += Time.deltaTime * m_TimeMultiplier;
			}
			else if (!m_Current.m_Hold || m_Current.m_Message.Length > 1)
			{
				EnterMessageState(MessageShowState.EndMessage);
			}
			break;
		case MessageShowState.EndMessage:
			if ((m_CurrentMsgIndex < m_Current.m_Message.Length - 1 || m_Current.m_Hold) && !m_ClearCurrentMsg)
			{
				m_CurrentMsgIndex++;
				if (m_CurrentMsgIndex >= m_Current.m_Message.Length)
				{
					m_CurrentMsgIndex = 0;
				}
				EnterMessageState(MessageShowState.ShowMessage);
				break;
			}
			if (m_FadeTime > Mathf.Epsilon && m_Timer <= m_FadeTime)
			{
				m_MessageHUD.Alpha = (m_FadeTime - m_Timer) / m_FadeTime;
				m_Timer += Time.deltaTime * m_TimeMultiplier;
				break;
			}
			m_MessageHUD.Alpha = 0f;
			if (!m_ClearCurrentMsg)
			{
				m_Current.Callback.Send();
			}
			m_Current = null;
			m_ClearCurrentMsg = false;
			break;
		}
	}

	private void Update()
	{
		bool isPaused = Singleton.Manager<ManPauseGame>.inst.IsPaused;
		m_MessageHUD.Hide(isPaused);
		if (isPaused)
		{
			return;
		}
		m_LastMessageTime += Time.deltaTime;
		if (m_Current == null)
		{
			for (int num = m_Messages.Length - 1; num >= 0; num--)
			{
				if (m_Messages[num].Count > 0)
				{
					m_Current = m_Messages[num][0];
					m_ClearCurrentMsg = false;
					m_Messages[num].RemoveAt(0);
					m_CurrentMsgIndex = 0;
					m_LastMessageTime = 0f;
					EnterMessageState(MessageShowState.ShowMessage);
					break;
				}
			}
		}
		if (m_Current != null && m_Current.m_Message != null)
		{
			UpdateMessageState();
		}
	}

	private void PostModeExit(Mode exitedMode)
	{
		ClearAllMessages();
		m_MessageHUD.ClearMessage();
		m_MessageHUD.Alpha = 0f;
	}

	private void Awake()
	{
		int count = EnumValuesIterator<MessagePriority>.Count;
		m_Messages = new List<OnScreenMessage>[count];
		for (int i = 0; i < count; i++)
		{
			m_Messages[i] = new List<OnScreenMessage>();
		}
		m_MessageHUD = UnityEngine.Object.Instantiate(m_MessageHUDPrefab);
		m_MessageHUD.transform.SetParent(m_IgnoreRaycastCanvas, worldPositionStays: false);
		m_MessageHUD.Alpha = 0f;
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(PostModeExit);
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
	}
}
