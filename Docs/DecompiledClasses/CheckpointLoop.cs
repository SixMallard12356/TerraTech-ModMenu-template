using UnityEngine;

public class CheckpointLoop : CheckpointTriggerGate
{
	[SerializeField]
	private float m_InitialRadius = 0.5f;

	private float m_Radius;

	protected override void SetupCheckpoint(Vector3 position, Vector3 fwdDirection, Vector3 upDir, float width, float heightUnused)
	{
		base.Trans.position = position;
		base.Trans.rotation = Quaternion.LookRotation(fwdDirection, upDir);
		m_Radius = width / 2f;
		base.Trans.localScale = Vector3.one * (m_Radius / m_InitialRadius);
	}

	protected override bool TriggerEntryValid(Tank tank)
	{
		return base.TriggerEntryValid(tank);
	}

	private void OnSpawn()
	{
		m_Radius = m_InitialRadius;
	}
}
