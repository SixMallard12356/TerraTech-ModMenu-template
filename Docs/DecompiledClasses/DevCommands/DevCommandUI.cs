using System;
using System.Collections.Generic;
using System.Linq;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DevCommands;

public class DevCommandUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private InputField m_InputField;

	[SerializeField]
	private UIUserTextInputHelper m_InputFieldHelper;

	[SerializeField]
	private Text m_PreviewText;

	[SerializeField]
	private Text m_CommandLog;

	[SerializeField]
	private ScrollRect m_LogScrollRect;

	[SerializeField]
	private int m_MaxHistoryCount = 10;

	private List<(string msg, bool isCmd)> m_LogHistory = new List<(string, bool)>();

	private List<string> m_CommandHistory = new List<string>();

	private int m_RecentCommandsIdx;

	private bool m_HasSuggestedCompletion;

	private string m_CurrentSuggestedCompletion;

	private int m_CurrentSuggestedCompletionIndex;

	private string m_LastInput;

	private bool m_ReselectInputWorkaround;

	private int m_CheatCodeInputFrame;

	private int m_CheatCodeEntryCounter;

	private int m_DelayedScrollToEnd;

	private static readonly string[] kLineBreaks = new string[2] { "\n", "\r\n" };

	public bool HasInputFocus => m_InputFieldHelper.HasFocus;

	public void SetActive(bool active)
	{
		base.gameObject.SetActive(active);
		if (active)
		{
			m_LogScrollRect.normalizedPosition = Vector2.zero;
			SelectInputField();
		}
		else
		{
			ClearInput();
			((IPointerExitHandler)this).OnPointerExit((PointerEventData)null);
		}
	}

	public void ClearInput()
	{
		m_InputField.text = string.Empty;
		m_PreviewText.text = string.Empty;
		m_RecentCommandsIdx = -1;
		m_HasSuggestedCompletion = false;
	}

	public void AddToLog(string message, Color msgColour)
	{
		string arg = ColourConverter.ColourToString(msgColour);
		string message2 = $"<color=#{arg}>{message}</color>";
		AddToLog(message2);
	}

	public void SelectInputField()
	{
		m_InputField.ActivateInputField();
		m_InputField.Select();
		m_InputFieldHelper.OnSelect(new BaseEventData(EventSystem.current));
		m_InputField.caretPosition = m_InputField.text.Length;
	}

	private void LogCommand(string command)
	{
		AddToLog(command, isCommand: true);
		m_RecentCommandsIdx = -1;
	}

	public void AddToLog(string message, bool isCommand = false)
	{
		m_LogHistory.Add((message, isCommand));
		if (m_LogHistory.Count > m_MaxHistoryCount)
		{
			m_LogHistory.RemoveAt(0);
			m_CommandLog.text = string.Join("\n", m_LogHistory.Select(((string msg, bool isCmd) p) => p.msg));
		}
		else
		{
			Text commandLog = m_CommandLog;
			commandLog.text = commandLog.text + "\n" + message;
		}
		if (isCommand && m_CommandHistory.LastOrDefault() != message)
		{
			m_CommandHistory.Remove(message);
			m_CommandHistory.Add(message);
		}
		if (m_DelayedScrollToEnd > 0)
		{
			m_LogScrollRect.normalizedPosition = Vector2.zero;
		}
		m_DelayedScrollToEnd = Time.frameCount;
	}

	private void SetInputText(string newText)
	{
		m_InputField.text = newText;
		m_InputField.caretPosition = newText.Length;
		TrySetInputPrediction(newText);
	}

	private void TrySetInputPrediction(string value)
	{
		if (!value.NullOrEmpty() && Singleton.Manager<ManDevCommands>.inst.TryGetSuggestionForInput(value, out var outSuggestedText, out m_CurrentSuggestedCompletion, out m_CurrentSuggestedCompletionIndex))
		{
			m_HasSuggestedCompletion = m_CurrentSuggestedCompletion != null;
			m_PreviewText.text = outSuggestedText;
		}
		else
		{
			m_HasSuggestedCompletion = false;
			m_PreviewText.text = string.Empty;
		}
	}

	private void ReportCommandResult(bool success, string customMessage = null)
	{
		string text = " - ";
		if (!success)
		{
			text += (customMessage.NullOrEmpty() ? "Error" : "Error: ");
		}
		if (!customMessage.NullOrEmpty())
		{
			text += customMessage;
		}
		AddToLog(text, new Color(0.8f, 0.7f, 0.7f));
	}

	private void TryExecuteInput(string inputStr)
	{
		LogCommand(inputStr);
		if (inputStr.StartsWith("\\"))
		{
			Singleton.Manager<DebugUtil>.inst.AddTextToCheatInputBuffer(inputStr.Substring(1));
			m_CheatCodeInputFrame = Time.frameCount;
			if (m_CheatCodeEntryCounter == 0 && m_InputFieldHelper.HasFocus)
			{
				Singleton.Manager<DebugUtil>.inst.DisableCheatInput = false;
			}
			m_CheatCodeEntryCounter++;
			return;
		}
		string resultStr;
		bool flag = Singleton.Manager<ManDevCommands>.inst.TryExecuteInput(inputStr, out resultStr);
		if (!flag || !resultStr.NullOrEmpty())
		{
			ReportCommandResult(flag, resultStr);
		}
		if (flag && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull())
		{
			string text = "Used Command: " + inputStr;
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SendChat(text, -1, Singleton.Manager<ManNetwork>.inst.MyPlayer.netId.Value);
		}
	}

	private void OnInputValueChanged(string value)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!m_LastInput.NullOrEmpty())
			{
				ClearInput();
				Singleton.Manager<ManGameMode>.inst.SetCancelEventWasHandled(eventWasHandled: true);
				m_ReselectInputWorkaround = true;
			}
		}
		else
		{
			TrySetInputPrediction(value);
			m_LastInput = value;
		}
	}

	private void OnInputEndEdit(string value)
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			if (!value.NullOrEmpty())
			{
				string[] array = value.Split(kLineBreaks, StringSplitOptions.RemoveEmptyEntries);
				foreach (string inputStr in array)
				{
					TryExecuteInput(inputStr);
				}
				ClearInput();
				m_ReselectInputWorkaround = true;
			}
		}
		else if (m_LastInput.NullOrEmpty() && base.gameObject.activeSelf)
		{
			SetActive(active: false);
			Singleton.Manager<ManGameMode>.inst.SetCancelEventWasHandled(eventWasHandled: true);
		}
		m_LastInput = value;
	}

	private void OnPostInputEndEdit(string text)
	{
		if (m_ReselectInputWorkaround)
		{
			SelectInputField();
			m_ReselectInputWorkaround = false;
			if (m_CheatCodeEntryCounter > 0)
			{
				Singleton.Manager<DebugUtil>.inst.DisableCheatInput = false;
			}
		}
	}

	private void OnCheatCompleted(DebugUtil.CheatCode cheatCodeUsed)
	{
		ReportCommandResult(success: true, cheatCodeUsed.ToString());
		m_CheatCodeEntryCounter--;
		if (m_CheatCodeEntryCounter <= 0)
		{
			m_CheatCodeInputFrame = 0;
			m_CheatCodeEntryCounter = 0;
			if (m_InputFieldHelper.HasFocus)
			{
				Singleton.Manager<DebugUtil>.inst.DisableCheatInput = true;
			}
		}
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.DevCommandWindow, preventInteraction: true);
		Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.DevCommandWindow, prevent: true);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.DevCommandWindow, preventInteraction: false);
		Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.DevCommandWindow, prevent: false);
	}

	private void Awake()
	{
		GetComponent<Canvas>().worldCamera = Singleton.Manager<ManUI>.inst.m_UICamera;
		m_InputField.onValueChanged.AddListener(OnInputValueChanged);
		m_InputField.onEndEdit.AddListener(OnInputEndEdit);
		m_InputFieldHelper.EndEditEvent.Subscribe(OnPostInputEndEdit);
		Singleton.Manager<DebugUtil>.inst.CheatCompletedEvent.Subscribe(OnCheatCompleted);
		m_CommandLog.text = string.Empty;
		m_InputField.text = string.Empty;
		m_PreviewText.text = string.Empty;
	}

	private void Update()
	{
		int frameCount = Time.frameCount;
		if (m_CheatCodeEntryCounter > 0 && m_CheatCodeInputFrame > 0 && frameCount >= m_CheatCodeInputFrame + 5)
		{
			ReportCommandResult(success: false);
			m_CheatCodeInputFrame = 0;
			m_CheatCodeEntryCounter = 0;
			if (m_InputFieldHelper.HasFocus)
			{
				Singleton.Manager<DebugUtil>.inst.DisableCheatInput = true;
			}
		}
		if (m_DelayedScrollToEnd != 0 && frameCount > m_DelayedScrollToEnd + 1)
		{
			m_LogScrollRect.normalizedPosition = Vector2.zero;
			m_DelayedScrollToEnd = 0;
		}
		if (!m_InputFieldHelper.HasFocus)
		{
			return;
		}
		if (m_HasSuggestedCompletion && Input.GetKeyDown(KeyCode.Tab))
		{
			string inputText = m_InputField.text.Substring(0, m_CurrentSuggestedCompletionIndex) + m_CurrentSuggestedCompletion + " ";
			SetInputText(inputText);
		}
		else
		{
			if (!Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow))
			{
				return;
			}
			if (!m_HasSuggestedCompletion || m_CurrentSuggestedCompletion.NullOrEmpty())
			{
				int recentCommandsIdx = m_RecentCommandsIdx;
				recentCommandsIdx += (Input.GetKeyDown(KeyCode.UpArrow) ? 1 : (-1));
				recentCommandsIdx = Mathf.Clamp(recentCommandsIdx, -1, m_CommandHistory.Count - 1);
				if (recentCommandsIdx != m_RecentCommandsIdx)
				{
					m_RecentCommandsIdx = recentCommandsIdx;
					if (recentCommandsIdx >= 0)
					{
						int index = m_CommandHistory.Count - 1 - recentCommandsIdx;
						SetInputText(m_CommandHistory[index]);
					}
					else
					{
						ClearInput();
					}
				}
			}
			else
			{
				bool keyDown = Input.GetKeyDown(KeyCode.DownArrow);
				Singleton.Manager<ManDevCommands>.inst.ChangeSuggestion(keyDown);
				TrySetInputPrediction(m_InputField.text);
			}
		}
	}
}
