#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public class FTUEEnumType
{
	private static string[] k_EnumNames;

	[SerializeField]
	public string m_TypeName;

	public FTUEEnumType()
	{
		if (k_EnumNames == null)
		{
			k_EnumNames = Enum.GetNames(typeof(FTUEActions));
		}
	}

	public FTUEEnumType(FTUEActions enumType)
	{
		if (k_EnumNames == null)
		{
			k_EnumNames = Enum.GetNames(typeof(FTUEActions));
		}
		m_TypeName = k_EnumNames[(int)enumType];
	}

	public bool IsValid(out FTUEActions enumType)
	{
		enumType = FTUEActions.DefeatFirstEnemy;
		bool result = false;
		for (int i = 0; i < Singleton.Manager<ManAI>.inst.AITypeNames.Length; i++)
		{
			if (k_EnumNames[i] == m_TypeName)
			{
				enumType = (FTUEActions)i;
				result = true;
				break;
			}
		}
		return result;
	}

	public FTUEActions GetEnumType()
	{
		if (!IsValid(out var enumType))
		{
			d.LogError("FtueEnumType.GetEnumType - No Type found for " + m_TypeName);
		}
		return enumType;
	}
}
