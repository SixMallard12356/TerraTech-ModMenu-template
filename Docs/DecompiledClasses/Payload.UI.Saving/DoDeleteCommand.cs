#define UNITY_EDITOR
using System;
using System.IO;

namespace Payload.UI.Saving;

public class DoDeleteCommand : SaveCommand
{
	public override void Execute(SaveOperationData data)
	{
		try
		{
			string text = ManSaveGame.CreateGameSaveFilePath(data.m_GameType, data.m_PrevName);
			if (File.Exists(text))
			{
				File.Delete(text);
				SetComplete(data);
			}
			else
			{
				d.Log("DoDeleteCommand - File '" + text + "' does NOT exist!");
				data.m_Error = "DoDeleteCommand - invalid file path " + text;
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
