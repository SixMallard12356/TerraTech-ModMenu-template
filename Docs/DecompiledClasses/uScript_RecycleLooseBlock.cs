using UnityEngine;

[FriendlyName("Recycle Block")]
[NodePath("TerraTech/Actions/Blocks")]
[NodeToolTip("Recycle or 'Despawn' a provided block and optionally play particle effects when you do so")]
[NodeDescription("Recycle or 'Despawn' a provided block and optionally play particle effects when you do so")]
public class uScript_RecycleLooseBlock : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject owner, TankBlock block, [SocketState(false, false)] Transform despawnParticleEffect = null, [SocketState(false, false)] Vector3 particleSpawnOffset = default(Vector3))
	{
		if (CanRecycleBlock(block))
		{
			if (m_Encounter == null)
			{
				m_Encounter = owner.GetComponent<Encounter>();
			}
			if (despawnParticleEffect != null)
			{
				m_Encounter.SpawnParticles(despawnParticleEffect, block.centreOfMassWorld + particleSpawnOffset, Quaternion.identity, out var _);
			}
			block.trans.Recycle();
		}
	}

	private bool CanRecycleBlock(TankBlock block)
	{
		if (block != null && block.gameObject.activeSelf)
		{
			return !block.IsAttached;
		}
		return false;
	}

	public void OnDisable()
	{
		m_Encounter = null;
	}
}
