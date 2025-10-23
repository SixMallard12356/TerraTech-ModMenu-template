using UnityEngine;

public class SetHoverActiveAnimBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private bool m_Active;

	private TankBlock m_Block;

	private ModuleHover m_Hover;

	private ModuleStabiliser m_Stabiliser;

	private ModuleGyro m_Gyro;

	private ModuleBooster m_Booster;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_Block == null)
		{
			m_Block = animator.transform.GetComponentInParents<TankBlock>(thisObjectFirst: true);
			m_Hover = m_Block.GetComponent<ModuleHover>();
			m_Stabiliser = m_Block.GetComponent<ModuleStabiliser>();
			m_Gyro = m_Block.GetComponent<ModuleGyro>();
			m_Booster = m_Block.GetComponent<ModuleBooster>();
		}
		if (m_Hover != null)
		{
			m_Hover.SetEnabled(m_Active);
		}
		if (m_Stabiliser != null)
		{
			m_Stabiliser.SetEnabled(m_Active);
		}
		if (m_Gyro != null)
		{
			m_Gyro.SetEnabled(m_Active);
		}
		if (m_Booster != null)
		{
			m_Booster.SetEnabled(m_Active);
		}
	}
}
