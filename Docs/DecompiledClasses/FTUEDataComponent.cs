using UnityEngine;

public class FTUEDataComponent : MonoBehaviour
{
	public FTUESequence m_MySequence;

	public int m_MyId;

	public void SetData(FTUESequence sequence, int id)
	{
		m_MySequence = sequence;
		m_MyId = id;
	}
}
