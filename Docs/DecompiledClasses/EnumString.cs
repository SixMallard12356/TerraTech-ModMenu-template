#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnumString : IEquatable<EnumString>, ISerializationCallbackReceiver
{
	[JsonProperty]
	[SerializeField]
	[FormerlySerializedAs("m_TypeName")]
	private string m_EnumValueString;

	[JsonProperty]
	private Type m_EnumType;

	private int m_EnumValueInt = -1;

	[JsonIgnore]
	public bool IsValid => m_EnumValueInt >= 0;

	[JsonIgnore]
	public int Value => GetValue();

	[JsonConstructor]
	private EnumString(string m_EnumValueString, Type m_EnumType)
	{
		this.m_EnumValueString = m_EnumValueString;
		this.m_EnumType = m_EnumType;
		UpdateIntValueFromStringValue();
	}

	public EnumString(Type enumType, int enumValue)
	{
		SetType(enumType);
		SetValue(enumValue);
	}

	public void SetValue(int enumValue)
	{
		m_EnumValueString = Enum.GetName(m_EnumType, enumValue);
		if (m_EnumValueString != null)
		{
			m_EnumValueInt = enumValue;
			return;
		}
		d.LogError(string.Concat("EnumStringType.SetValue - Setting value ", enumValue, " on EnumString of type ", m_EnumType, " , but the value did not exist within the enum type!"));
		m_EnumValueInt = -1;
	}

	public int GetValue()
	{
		d.Assert(m_EnumValueInt >= 0, string.Concat("EnumStringType.GetValue ", m_EnumType, " with value string ", m_EnumValueString, " is not set to a valid int value!"));
		return m_EnumValueInt;
	}

	public static bool operator ==(EnumString a, EnumString b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		if (a.m_EnumType == b.m_EnumType && a.m_EnumValueInt == b.m_EnumValueInt)
		{
			return a.m_EnumValueString == b.m_EnumValueString;
		}
		return false;
	}

	public static bool operator !=(EnumString a, EnumString b)
	{
		return !(a == b);
	}

	public bool Equals(EnumString other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as EnumString);
	}

	public override int GetHashCode()
	{
		return m_EnumType.GetHashCode() ^ m_EnumValueInt;
	}

	private void SetType(Type enumType)
	{
		if (enumType.IsEnum)
		{
			m_EnumType = enumType;
		}
		else
		{
			m_EnumType = null;
			d.LogError(string.Concat("EnumStringType.SetType ", enumType, " is not an Enum Type"));
		}
		m_EnumValueString = null;
		m_EnumValueInt = -1;
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		if (Enum.IsDefined(m_EnumType, m_EnumValueString))
		{
			m_EnumValueInt = (int)Enum.Parse(m_EnumType, m_EnumValueString);
			return;
		}
		d.LogError(string.Concat("EnumStringType.OnAfterDeserialize - Loading EnumString ", m_EnumType, " with value string '", m_EnumValueString, "' did not resolve to a valid enum value!"));
		m_EnumValueInt = -1;
	}

	public void UpdateIntValueFromStringValue()
	{
		if (Enum.IsDefined(m_EnumType, m_EnumValueString))
		{
			m_EnumValueInt = (int)Enum.Parse(m_EnumType, m_EnumValueString);
		}
		else
		{
			m_EnumValueInt = -1;
		}
	}
}
