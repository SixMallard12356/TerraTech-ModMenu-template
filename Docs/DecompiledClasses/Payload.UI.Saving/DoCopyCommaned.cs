#define UNITY_EDITOR
using System;
using System.IO;

namespace Payload.UI.Saving;

public class DoCopyCommaned : SaveCommand
{
	public override void Execute(SaveOperationData data)
	{
		try
		{
			string text = ManSaveGame.CreateGameSaveFilePath(data.m_GameType, data.m_PrevName);
			string destFileName = ManSaveGame.CreateGameSaveFilePath(data.m_GameType, data.m_Name);
			if (File.Exists(text))
			{
				File.Copy(text, destFileName, overwrite: true);
				SetComplete(data);
			}
			else
			{
				d.Log("DoCopyCommand - File '" + text + "' does NOT exist!");
				data.m_Error = "DoCopyCommand - invalid file path " + text;
				SetCancelled(data);
			}
		}
		catch (Exception ex)
		{
			data.m_Error = ex.Message;
			SetCancelled(data);
		}
	}
}
