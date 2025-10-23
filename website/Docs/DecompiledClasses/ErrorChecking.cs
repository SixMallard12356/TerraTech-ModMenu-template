using System.Collections.Generic;
using UnityEngine;

public class ErrorChecking : MonoBehaviour
{
	[SerializeField]
	private bool m_ReportErrorsInEditor;

	private List<string> m_ErrorStrings = new List<string>();

	private bool m_ErrorReported;

	private bool m_DebugThrowError;

	private void HandleLogEntry(string logEntry, string stackTrace, LogType logType)
	{
		if (logType == LogType.Exception)
		{
			m_ErrorStrings.Add(logEntry + " \n " + stackTrace);
		}
	}

	private void OnEnable()
	{
		Application.logMessageReceived += HandleLogEntry;
	}

	private void Update()
	{
		if (!SKU.BugReporterEnabled)
		{
			return;
		}
		if (m_ErrorReported || (Application.isEditor && !m_ReportErrorsInEditor))
		{
			m_ErrorStrings.Clear();
			return;
		}
		if (m_ErrorStrings.Count > 0 || m_DebugThrowError)
		{
			string stack = string.Join("/n", m_ErrorStrings.ToArray());
			UIScreenBugReport uIScreenBugReport = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.BugReport) as UIScreenBugReport;
			if (uIScreenBugReport != null)
			{
				uIScreenBugReport.Set(stack, stackReport: true);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenBugReport, ManUI.PauseType.Pause);
			}
			m_ErrorReported = true;
		}
		m_ErrorStrings.Clear();
	}

	private void OnDisable()
	{
		Application.logMessageReceived -= HandleLogEntry;
	}
}
