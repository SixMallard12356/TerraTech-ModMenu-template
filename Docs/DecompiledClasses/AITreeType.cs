#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public class AITreeType
{
	public enum AITypes
	{
		Idle,
		Escort,
		Guard,
		Harvest,
		FollowPassive,
		Invader,
		Flee,
		ChargeAtSKU,
		FacePlayer,
		Specific
	}

	[SerializeField]
	public string m_TypeName;

	public AITreeType()
	{
	}

	public AITreeType(AITypes aiType)
	{
		m_TypeName = Singleton.Manager<ManAI>.inst.AITypeNames[(int)aiType];
	}

	public bool IsType(AITypes type)
	{
		return Singleton.Manager<ManAI>.inst.AITypeNames[(int)type] == m_TypeName;
	}

	public AITypes GetAIType()
	{
		AITypes result = AITypes.Idle;
		bool flag = false;
		for (int i = 0; i < Singleton.Manager<ManAI>.inst.AITypeNames.Length; i++)
		{
			if (Singleton.Manager<ManAI>.inst.AITypeNames[i] == m_TypeName)
			{
				result = (AITypes)i;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			d.LogError("AITreeSaveType.GetAIType - No AI Type found for " + m_TypeName);
		}
		return result;
	}
}
