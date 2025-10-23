using UnityEngine;

public class ConfigureItemHolderBeamAnimBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private ModuleItemHolderBeam.ItemMovementType m_ItemMovementType;

	[SerializeField]
	private bool m_DrawBeam;

	[SerializeField]
	private int m_StackIndex = -1;

	private ModuleItemHolderBeam m_HolderBeam;

	private const int kAllStacksIndex = -1;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (m_HolderBeam == null)
		{
			m_HolderBeam = Visible.FindComponentOnVisibleUpwards<ModuleItemHolderBeam>(animator);
		}
		if (!m_HolderBeam)
		{
			return;
		}
		if (m_StackIndex == -1)
		{
			for (int i = 0; i < m_HolderBeam.NumStacks; i++)
			{
				m_HolderBeam.ConfigureStack(i, m_DrawBeam, m_ItemMovementType);
			}
		}
		else
		{
			m_HolderBeam.ConfigureStack(m_StackIndex, m_DrawBeam, m_ItemMovementType);
		}
	}
}
