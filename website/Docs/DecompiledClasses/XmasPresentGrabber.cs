using UnityEngine;

[RequireComponent(typeof(Visible))]
[RequireComponent(typeof(SphereCollider))]
public class XmasPresentGrabber : MonoBehaviour
{
	private Visible m_Visible;

	private SphereCollider m_Collider;

	private ModuleItemHolder m_Holder;

	private float m_GrabRange;

	private static readonly Bitfield<ObjectTypes> k_MaskChunks = new Bitfield<ObjectTypes>(4);

	private void OnPool()
	{
		m_Visible = GetComponent<Visible>();
		m_Collider = GetComponent<SphereCollider>();
		ModuleItemHolder[] componentsInChildren = GetComponentsInChildren<ModuleItemHolder>(includeInactive: true);
		m_Holder = ((componentsInChildren != null && componentsInChildren.Length != 0) ? componentsInChildren[0] : null);
		m_GrabRange = m_Collider.RadiusWorld();
	}

	private void FixedUpdate()
	{
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(m_Visible.trans.position, m_GrabRange, k_MaskChunks))
		{
			if (item.holderStack != null)
			{
				if (item.holderStack.myHolder == m_Holder)
				{
					item.SetHolder(null);
				}
				else
				{
					ManSpawn.IsPlayerTeam(item.holderStack.myHolder.block.tank.Team);
				}
			}
		}
	}
}
