using UnityEngine;

public class RestorePointAnimBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private int m_RestoreStateToSet;

	private ModuleAnimator m_ModAnimator;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_ModAnimator == null)
		{
			m_ModAnimator = Visible.FindComponentOnVisibleUpwards<ModuleAnimator>(animator);
		}
		if ((bool)m_ModAnimator)
		{
			m_ModAnimator.SetRestoreState(m_RestoreStateToSet);
		}
	}
}
