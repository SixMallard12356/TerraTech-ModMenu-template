using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class BitfieldAttribute : PropertyAttribute
{
	public readonly Type m_EnumType;

	public BitfieldAttribute(Type enumType)
	{
		m_EnumType = enumType;
	}
}
