#define UNITY_EDITOR
using System;
using BehaviorDesigner.Runtime;

public class ManAI : Singleton.Manager<ManAI>
{
	[EnumArray(typeof(AITreeType.AITypes))]
	public ExternalBehaviorTree[] m_AITrees;

	private string[] m_AITypeNames;

	public string[] AITypeNames => m_AITypeNames;

	public ExternalBehaviorTree GetAITree(AITreeType aiTreeType)
	{
		AITreeType.AITypes aIType = aiTreeType.GetAIType();
		return GetAITree(aIType);
	}

	public ExternalBehaviorTree GetAITree(AITreeType.AITypes aiType)
	{
		ExternalBehaviorTree result = null;
		if ((int)aiType < m_AITypeNames.Length)
		{
			result = m_AITrees[(int)aiType];
		}
		else
		{
			d.LogError("ManAI.GetAITree - Array Index Out of bounds");
		}
		return result;
	}

	public bool GetAIType(ExternalBehaviorTree tree, out AITreeType treeType)
	{
		bool result = false;
		treeType = new AITreeType();
		for (int i = 0; i < m_AITrees.Length; i++)
		{
			if (m_AITrees[i] == tree)
			{
				treeType.m_TypeName = m_AITypeNames[i];
				result = true;
				break;
			}
		}
		return result;
	}

	public AITreeType GetAIType(AITreeType.AITypes aiType)
	{
		AITreeType aITreeType = new AITreeType();
		if ((int)aiType < m_AITypeNames.Length)
		{
			aITreeType.m_TypeName = m_AITypeNames[(int)aiType];
		}
		else
		{
			d.LogError("ManAI.GetType - Array Index Out of bounds");
		}
		return aITreeType;
	}

	private void Start()
	{
		m_AITypeNames = Enum.GetNames(typeof(AITreeType.AITypes));
	}
}
