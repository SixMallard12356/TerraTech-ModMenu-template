namespace DevCommands;

public struct CommandReturn
{
	public bool? success;

	public string message;

	public static CommandReturn SilentSuccess;
}
