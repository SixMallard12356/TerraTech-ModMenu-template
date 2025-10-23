using UnityEngine;

public abstract class ActionInStateAnimBehaviour : StateMachineBehaviour
{
	private bool m_Inited;

	private Transform m_ParentVisible;

	private ModuleAnimator m_ModAnimator;

	private bool m_RunningAction;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!m_Inited)
		{
			m_Inited = true;
			m_ParentVisible = Visible.FindComponentOnVisibleUpwards<Transform>(animator);
			m_ModAnimator = Visible.FindComponentOnVisibleUpwards<ModuleAnimator>(animator);
			OnInitialised();
			if (m_ModAnimator != null)
			{
				m_ModAnimator.OnRecycled.Subscribe(OnAnimatorRecycled);
			}
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		bool flag = !animator.IsInTransition(0);
		if (flag != m_RunningAction)
		{
			if (flag)
			{
				BeginAction(m_ParentVisible);
			}
			else
			{
				EndAction();
			}
			m_RunningAction = flag;
		}
		if (m_RunningAction)
		{
			UpdateAction(animator);
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		EndActionIfRunning();
	}

	protected abstract void BeginAction(Transform parentVisible);

	protected virtual void UpdateAction(Animator animator)
	{
	}

	protected virtual void EndAction()
	{
	}

	protected virtual void OnInitialised()
	{
	}

	protected virtual void OnRecycled()
	{
	}

	private void EndActionIfRunning()
	{
		if (m_RunningAction)
		{
			EndAction();
			m_RunningAction = false;
		}
	}

	private void OnAnimatorRecycled()
	{
		EndActionIfRunning();
		OnRecycled();
		if (m_ModAnimator != null)
		{
			m_ModAnimator.OnRecycled.Unsubscribe(OnAnimatorRecycled);
		}
		m_Inited = false;
	}
}
