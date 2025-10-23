#define UNITY_EDITOR
using UnityEngine;

public class uScript_IsAnimPlaying : uScriptLogic
{
	private bool m_Playing;

	public bool True => m_Playing;

	public bool False => !m_Playing;

	public void In(Animator animator, string animStateToCheck, bool removeOnFinsh)
	{
		if ((bool)animator)
		{
			m_Playing = animator.GetCurrentAnimatorStateInfo(0).IsName(animStateToCheck);
			if (!m_Playing && removeOnFinsh)
			{
				Object.Destroy(animator.gameObject);
			}
		}
		else
		{
			m_Playing = false;
			d.LogError("uScript_IsAnimPlaying - Animator is null");
		}
	}
}
