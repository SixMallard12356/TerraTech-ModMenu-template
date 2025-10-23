using UnityEngine;

public class SetWheelsActiveAnimBehaviour : StateMachineBehaviour
{
	public enum EnabledState
	{
		On,
		Off,
		FromAnimation
	}

	[Tooltip("When using FromAnimation, the script will use the value of the Animation Parameter 'WheelsEnabled'. This can be set from script, or in Curves on the Animation itself.")]
	[SerializeField]
	private EnabledState m_WheelsEnabledState = EnabledState.FromAnimation;

	[SerializeField]
	[Header("When to apply state")]
	private bool m_UpdateOnEnter;

	[SerializeField]
	private bool m_UpdateEveryFrame;

	private ModuleWheels m_ModWheels;

	private int wheelsEnabledID = Animator.StringToHash("WheelsEnabled");

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_ModWheels.IsNull())
		{
			m_ModWheels = Visible.FindComponentOnVisibleUpwards<ModuleWheels>(animator);
		}
		if (m_ModWheels.IsNotNull() && m_UpdateOnEnter)
		{
			SetWheelsEnabled(animator, recalculateDot: true);
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_ModWheels.IsNotNull() && m_UpdateEveryFrame)
		{
			SetWheelsEnabled(animator, recalculateDot: false);
		}
	}

	private void SetWheelsEnabled(Animator animator, bool recalculateDot)
	{
		bool enabled = m_WheelsEnabledState switch
		{
			EnabledState.On => true, 
			EnabledState.Off => false, 
			EnabledState.FromAnimation => animator.GetFloat(wheelsEnabledID) > 0.5f, 
			_ => true, 
		};
		m_ModWheels.SetEnabled(enabled, recalculateDot);
	}
}
