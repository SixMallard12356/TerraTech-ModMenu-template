using System.Diagnostics;

public static class NXCPUProfiler
{
	public enum HeartbeatType
	{
		Main = 0,
		Vsync = 1,
		User1 = 8,
		User2 = 9,
		User3 = 10,
		User4 = 11,
		User5 = 12,
		User6 = 13,
		User7 = 14,
		User8 = 15,
		MAX = 15
	}

	[Conditional("ALLOW_CPU_PROFILING")]
	public static void InitialiseCPUProfiler()
	{
	}

	[Conditional("ALLOW_CPU_PROFILING")]
	public static void RecordHeartbeat(HeartbeatType heartbeatType)
	{
	}

	[Conditional("ALLOW_CPU_PROFILING")]
	public static void EnterCodeBlock(string codeBlockIdentifier)
	{
	}

	[Conditional("ALLOW_CPU_PROFILING")]
	public static void ExitCodeBlock(string codeBlockIdentifier)
	{
	}
}
