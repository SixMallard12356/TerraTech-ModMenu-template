using System;
using UnityEngine;

public class CheatCodesEncryptedAsset : ScriptableObject
{
	[Serializable]
	public struct CheatCode
	{
		[SerializeField]
		public string m_EncryptedPassPhrase;

		[SerializeField]
		public int m_SourcePassPhraseLength;

		[SerializeField]
		public float m_InputTime;
	}

	[SerializeField]
	[ReadOnly(ReadOnlyAttribute.EnabledState.Always)]
	public CheatCode[] m_CheatCodes = new CheatCode[0];

	public CheatCode GetCheatCodeData(DebugUtil.CheatCode cheat)
	{
		return m_CheatCodes[(int)cheat];
	}

	public float GetMaxSequenceDuration()
	{
		float num = 0f;
		for (int i = 0; i < m_CheatCodes.Length; i++)
		{
			num = Mathf.Max(num, m_CheatCodes[i].m_InputTime);
		}
		return num;
	}
}
