using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class EnumStringAttribute : PropertyAttribute
{
	public readonly Type m_EnumType;

	public EnumStringAttribute(Type enumType)
	{
		m_EnumType = enumType;
	}
}
