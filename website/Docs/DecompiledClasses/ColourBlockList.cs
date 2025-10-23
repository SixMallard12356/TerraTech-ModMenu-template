using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ColourBlockList", menuName = "Asset/Table/ColourBlockList")]
public class ColourBlockList : ScriptableObject
{
	[Serializable]
	public struct BlockPairs
	{
		public BlockTypes m_Block;

		public Color32 m_colour;
	}

	[FormerlySerializedAs("m_BlockPairs")]
	public BlockPairs[] m_BlockColour;

	private int GetDistance(Color32 current, Color32 match)
	{
		int num = current.r - match.r;
		int num2 = current.g - match.g;
		int num3 = current.b - match.b;
		return num * num + num2 * num2 + num3 * num3;
	}

	public BlockTypes FindNearestColour(Color32 current)
	{
		BlockTypes result = BlockTypes.SPEColourBlock02_Grey_111;
		int num = int.MaxValue;
		for (int i = 0; i < m_BlockColour.Length; i++)
		{
			Color32 colour = m_BlockColour[i].m_colour;
			int distance = GetDistance(current, colour);
			if (distance < num)
			{
				result = m_BlockColour[i].m_Block;
				num = distance;
			}
		}
		return result;
	}
}
