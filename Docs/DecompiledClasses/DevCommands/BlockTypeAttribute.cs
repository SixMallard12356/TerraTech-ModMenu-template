#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCommands;

public class BlockTypeAttribute : DevParamAttribute
{
	private static string[] s_Values;

	private static Dictionary<string, int> s_ModBLockLookup;

	public override bool UseCustomParse => true;

	public override IEnumerable<string> GetAutoCompletionValues(string partialName)
	{
		if (s_Values == null)
		{
			IEnumerable<string> first = from b in EnumIterator<BlockTypes>.Values()
				where Singleton.Manager<ManSpawn>.inst.CanAccessBlockInCurrentMode(b) && (Singleton.Manager<ManSpawn>.inst.IsPlayerFacingBlock(b) || Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development))
				select b.ToString() into n
				where !n.StartsWith("_deprecated_")
				select n;
			if (s_ModBLockLookup == null)
			{
				s_ModBLockLookup = new Dictionary<string, int>();
				Singleton.Manager<ManMods>.inst.BlocksModifiedEvent.Subscribe(delegate
				{
					s_Values = null;
				});
			}
			s_ModBLockLookup.Clear();
			foreach (BlockTypes item in Singleton.Manager<ManMods>.inst.IterateModdedBlocks())
			{
				string text = Singleton.Manager<ManMods>.inst.FindBlockName((int)item);
				string modNameForBlockID = Singleton.Manager<ManMods>.inst.GetModNameForBlockID(item);
				text = text + "(" + modNameForBlockID + ")";
				text = text.Replace(' ', '_');
				int num = 1;
				while (s_ModBLockLookup.ContainsKey(text))
				{
					d.LogWarningFormat("Duplicate block name found in modded blocks! {0}", text);
					text += $"_({num})";
					num++;
				}
				s_ModBLockLookup.Add(text, (int)item);
			}
			first = first.Concat(s_ModBLockLookup.Keys);
			s_Values = first.OrderBy((string s) => s).ToArray();
		}
		return s_Values;
	}

	public override bool TryParse(string paramStr, out object wrappedParamVal)
	{
		if (Enum.TryParse<BlockTypes>(paramStr, ignoreCase: true, out var result))
		{
			wrappedParamVal = result;
			return true;
		}
		int value = 0;
		if ((s_ModBLockLookup.TryGetValue(paramStr, out value) || int.TryParse(paramStr, out value)) && Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)value))
		{
			wrappedParamVal = (BlockTypes)value;
			return true;
		}
		wrappedParamVal = null;
		return false;
	}
}
