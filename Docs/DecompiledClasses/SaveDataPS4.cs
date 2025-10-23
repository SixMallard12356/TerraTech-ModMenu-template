using System;

public class SaveDataPS4
{
	public enum FileOpAction
	{
		NONE,
		SAVE,
		LOAD,
		DELETE,
		SEARCH
	}

	public enum OperationPriority
	{
		VERYLOW,
		LOW,
		MED,
		HIGH,
		URGENT
	}

	public enum OperationFileType
	{
		UNKNOWN,
		GAME,
		SNAPSHOT,
		SNAPSHOTCACHE
	}

	public OperationPriority Priority;

	public FileOpAction FileAction;

	public OperationFileType FileType;

	public string FileName;

	public string SlotName;

	public byte[] Data;

	public Action<bool, byte[]> OnCompletedCallback;

	public Action<string, string> OnFileFoundCallback;
}
