using System.Collections.Generic;
using System.Linq;

namespace DevCommands;

public class ChunkTypeAttribute : DevParamAttribute
{
	private static string[] s_Values;

	public override IEnumerable<string> GetAutoCompletionValues(string partialName)
	{
		if (s_Values == null)
		{
			s_Values = (from c in EnumIterator<ChunkTypes>.Values()
				where c != ChunkTypes.Null && !c.ToString().StartsWith("_deprecated_")
				select c.ToString() into s
				orderby s
				select s).ToArray();
		}
		return s_Values;
	}
}
