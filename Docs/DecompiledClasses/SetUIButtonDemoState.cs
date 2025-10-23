using UnityEngine;
using UnityEngine.Serialization;

public class SetUIButtonDemoState : MonoBehaviour
{
	[SerializeField]
	private bool m_Interactable;

	[FormerlySerializedAs("m_Visible")]
	[SerializeField]
	private bool m_VisibleInDemo = true;

	[FormerlySerializedAs("m_VisibleInBeta")]
	[SerializeField]
	private bool m_VisibleInMainGame;

	[SerializeField]
	private bool m_VisibleInRaD = true;

	[FormerlySerializedAs("m_VisibleInShow")]
	[SerializeField]
	private bool m_VisibleInShowNormal = true;

	private void Awake()
	{
		base.gameObject.SetActive(IsAvailableInSKU());
	}

	public bool IsAvailableInSKU()
	{
		if (Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
		{
			return m_VisibleInRaD;
		}
		return m_VisibleInMainGame;
	}
}
