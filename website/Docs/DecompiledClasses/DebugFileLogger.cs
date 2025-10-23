using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class DebugFileLogger : MonoBehaviour
{
	private StreamWriter m_ConsoleStream;

	private uint m_dumpStackTraceFlags;

	private bool m_keepLogs;

	private const uint DUMP_ERROR_STACK_TRACE = 1u;

	private const uint DUMP_WARNING_STACK_TRACE = 2u;

	private const uint DUMP_LOG_STACK_TRACE = 4u;

	private void HandleLogEvent(string _log, string _stackTrace, LogType _type)
	{
		m_ConsoleStream.WriteLine("{0}: {1}", DateTime.Now.ToString("HH:mm:ss.ffff"), _log);
		if ((_type == LogType.Log && (m_dumpStackTraceFlags & 4) != 0) || (_type == LogType.Warning && (m_dumpStackTraceFlags & 2) != 0) || (m_dumpStackTraceFlags & 1) != 0)
		{
			m_ConsoleStream.WriteLine(_stackTrace);
		}
	}

	private void Awake()
	{
		_checkStackTraceFlags();
		if (m_dumpStackTraceFlags != 0)
		{
			string text = "debug_log.txt";
			if (m_keepLogs)
			{
				KeepLog(text);
			}
			m_ConsoleStream = new StreamWriter(new FileStream(text, FileMode.Create));
			m_ConsoleStream.AutoFlush = true;
			Application.logMessageReceived += HandleLogEvent;
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			string text2 = "N/A";
			if (File.Exists(fileName))
			{
				text2 = File.GetLastWriteTime(fileName).ToString("F");
			}
			m_ConsoleStream.WriteLine("Application: " + fileName);
			m_ConsoleStream.WriteLine("Timestamp  : " + text2);
			m_ConsoleStream.WriteLine("Data Path  : " + Application.dataPath);
			m_ConsoleStream.WriteLine("Unity Ver  : " + Application.unityVersion);
			m_ConsoleStream.WriteLine("Time Now   : " + DateTime.Now.ToString("F"));
			m_ConsoleStream.WriteLine("");
		}
	}

	private void OnDestroy()
	{
		if (m_dumpStackTraceFlags != 0)
		{
			Application.logMessageReceived -= HandleLogEvent;
			m_ConsoleStream.Close();
			m_ConsoleStream = null;
		}
	}

	private void _checkStackTraceFlags()
	{
		string path = "_debugHelper.txt";
		if (File.Exists(path))
		{
			m_dumpStackTraceFlags |= 1u;
			string text = File.ReadAllText(path).ToLower();
			if (text.Contains("debuglog"))
			{
				m_dumpStackTraceFlags |= 4u;
			}
			if (text.Contains("warn"))
			{
				m_dumpStackTraceFlags |= 2u;
			}
			if (text.Contains("keeplogs"))
			{
				m_keepLogs = true;
			}
		}
	}

	public static void KeepLog(string theLogFileName)
	{
		if (!File.Exists(theLogFileName))
		{
			return;
		}
		string text = "_BackupLog";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		if (!Directory.Exists(text))
		{
			return;
		}
		int num = theLogFileName.LastIndexOf('.');
		if (num < 0)
		{
			return;
		}
		string text2 = theLogFileName.Substring(num);
		string text3 = theLogFileName.Substring(0, num);
		string text4 = text + "/" + text3;
		for (int i = 1; i < 999; i++)
		{
			string text5 = text4 + i + text2;
			if (!File.Exists(text5))
			{
				File.Move(theLogFileName, text5);
				break;
			}
		}
	}
}
