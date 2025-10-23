using System;
using System.Collections.Generic;

namespace DevCommands;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public abstract class DevParamAttribute : Attribute
{
	public virtual bool UseCustomParse => false;

	public abstract IEnumerable<string> GetAutoCompletionValues(string partialName);

	public virtual bool TryParse(string paramStr, out object wrappedParamVal)
	{
		wrappedParamVal = null;
		return false;
	}
}
