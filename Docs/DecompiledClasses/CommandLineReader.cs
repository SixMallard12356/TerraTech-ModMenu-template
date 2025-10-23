#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

public class CommandLineReader
{
	private const string CUSTOM_ARGS_PREFIX = "-CustomArgs:";

	private const char CUSTOM_ARGS_SEPARATOR = ';';

	public static string[] GetCommandLineArgs()
	{
		return Environment.GetCommandLineArgs();
	}

	public static string GetCommandLine()
	{
		string[] commandLineArgs = GetCommandLineArgs();
		if (commandLineArgs.Length != 0)
		{
			return string.Join(" ", commandLineArgs);
		}
		d.LogError("CommandLineReader.cs - GetCommandLine() - Can't find any command line arguments!");
		return "";
	}

	public static Dictionary<string, string> GetCustomArguments()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] commandLineArgs = GetCommandLineArgs();
		string text = "";
		try
		{
			text = commandLineArgs.Where((string row) => row.Contains("-CustomArgs:")).Single();
		}
		catch (Exception ex)
		{
			d.LogError(string.Concat("CommandLineReader.cs - GetCustomArguments() - Can't retrieve any custom arguments in the command line [", commandLineArgs, "]. Exception: ", ex));
			return dictionary;
		}
		text = text.Replace("-CustomArgs:", "");
		string[] array = text.Split(';');
		foreach (string text2 in array)
		{
			string[] array2 = text2.Split('=');
			if (array2.Length == 2)
			{
				dictionary.Add(array2[0], array2[1]);
			}
			else
			{
				d.LogWarning("CommandLineReader.cs - GetCustomArguments() - The custom argument [" + text2 + "] seem to be malformed.");
			}
		}
		return dictionary;
	}

	public static string GetCustomArgument(string argumentName, bool allowNull = false)
	{
		Dictionary<string, string> customArguments = GetCustomArguments();
		if (customArguments.ContainsKey(argumentName))
		{
			return customArguments[argumentName];
		}
		if (!allowNull)
		{
			d.LogError("CommandLineReader.cs - GetCustomArgument() - Can't retrieve any custom argument named [" + argumentName + "] in the command line [" + GetCommandLine() + "].");
		}
		return "";
	}

	public static string GetArgument(string argumentName)
	{
		string[] commandLineArgs = GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == argumentName && i < commandLineArgs.Length - 1)
			{
				return commandLineArgs[i + 1];
			}
		}
		return null;
	}

	public static string GetArgumentValue(string argumentName, char separator = '=')
	{
		if (separator == ' ')
		{
			return GetArgument(argumentName);
		}
		string[] commandLineArgs = GetCommandLineArgs();
		foreach (string text in commandLineArgs)
		{
			int num = text.IndexOf(separator);
			if (num >= 0 && text.Substring(0, num) == argumentName)
			{
				if (text.Length <= num + 1)
				{
					return string.Empty;
				}
				return text.Substring(num + 1);
			}
		}
		return null;
	}

	public static bool HasArgument(string argumentName)
	{
		string[] commandLineArgs = GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == argumentName)
			{
				return true;
			}
		}
		return false;
	}
}
