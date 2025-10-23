using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class GlobalsRangePropAttribute : PropertyAttribute
{
	public string m_PropName;

	public GlobalsRangePropAttribute(string propName)
	{
		m_PropName = propName;
	}
}
