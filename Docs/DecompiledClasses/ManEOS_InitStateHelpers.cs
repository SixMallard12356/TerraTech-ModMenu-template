public static class ManEOS_InitStateHelpers
{
	public static bool Is(this ManEOS.InitState stateFlags, ManEOS.InitState requestState)
	{
		return (stateFlags & requestState) == requestState;
	}

	public static bool IsNot(this ManEOS.InitState stateFlags, ManEOS.InitState requestState)
	{
		return !stateFlags.Is(requestState);
	}
}
