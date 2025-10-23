using System.Collections.Generic;
using UnityEngine;

public class HintDefinitionList : ScriptableObject
{
	[SerializeField]
	private List<ManHints.HintDefinition> m_HintsList = new List<ManHints.HintDefinition>();

	public ManHints.HintDefinition GetHintDefinition(GameHints.HintID hintId)
	{
		ManHints.HintDefinition result = null;
		for (int i = 0; i < m_HintsList.Count; i++)
		{
			if (m_HintsList[i].m_HintId.Value == (int)hintId)
			{
				result = m_HintsList[i];
				break;
			}
		}
		return result;
	}
}
