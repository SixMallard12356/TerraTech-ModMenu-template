#define UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;

public static class DebugNetworkLog
{
	public enum LogFilter
	{
		Log,
		Warning,
		Error
	}

	private static StreamWriter s_NetworkLog = null;

	private static bool s_IsNetLogEnabled = false;

	private static Bitfield<LogFilter> m_LogFilter = new Bitfield<LogFilter>();

	public static bool InitNetworkLog(bool keepLogs)
	{
		string text = "network_log.txt";
		if (keepLogs)
		{
			DebugFileLogger.KeepLog(text);
		}
		s_NetworkLog = new StreamWriter(new FileStream(text, FileMode.Create));
		if (s_NetworkLog == null)
		{
			return false;
		}
		s_IsNetLogEnabled = true;
		return true;
	}

	public static void Shutdown()
	{
		if (s_NetworkLog != null)
		{
			s_NetworkLog.Close();
			s_NetworkLog = null;
		}
		s_IsNetLogEnabled = false;
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void WriteToNetworkLog(string msg)
	{
		if (s_IsNetLogEnabled)
		{
			string value = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.ffff] ");
			s_NetworkLog.Write(value);
			s_NetworkLog.WriteLine(msg);
			s_NetworkLog.Flush();
		}
	}

	public static void SetLogLevel(LogFilter filter, bool set)
	{
		m_LogFilter.Set((int)filter, set);
	}

	public static void SetLogLevel(int filterIndex, bool set)
	{
		m_LogFilter.Set(filterIndex, set);
	}

	public static bool IsEnabled(int filterIndex)
	{
		return m_LogFilter.Contains(filterIndex);
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Log(string message)
	{
		if (ShouldLog(LogFilter.Log))
		{
			d.Log(message);
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogFormat(string format, params object[] args)
	{
		if (ShouldLog(LogFilter.Log))
		{
			d.LogFormat(format, args);
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogWarning(string message)
	{
		if (ShouldLog(LogFilter.Warning))
		{
			d.LogWarning(message);
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogWarningFormat(string format, params object[] args)
	{
		if (ShouldLog(LogFilter.Warning))
		{
			d.LogWarningFormat(format, args);
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogError(string message)
	{
		if (ShouldLog(LogFilter.Error))
		{
			d.LogError(message);
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogErrorFormat(string format, params object[] args)
	{
		if (ShouldLog(LogFilter.Error))
		{
			d.LogErrorFormat(format, args);
		}
	}

	private static bool ShouldLog(LogFilter level)
	{
		return m_LogFilter.Contains((int)level);
	}
}
