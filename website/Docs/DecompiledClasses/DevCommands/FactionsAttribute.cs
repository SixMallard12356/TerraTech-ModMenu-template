using System.Collections.Generic;
using System.Linq;

namespace DevCommands;

public class FactionsAttribute : DevParamAttribute
{
	private static string[] s_Values;

	public override IEnumerable<string> GetAutoCompletionValues(string partialName)
	{
		if (s_Values == null)
		{
			s_Values = (from c in Singleton.Manager<ManPurchases>.inst.AvailableCorporations
				select c.ToString() into s
				orderby s
				select s).ToArray();
		}
		return s_Values;
	}
}
