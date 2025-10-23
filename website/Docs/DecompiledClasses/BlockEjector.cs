#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public class BlockEjector
{
	[SerializeField]
	private float m_ContentsEjectSpeed;

	[SerializeField]
	private float m_ContentsEjectAngularVelocity;

	[Tooltip("How long to expel an item")]
	[SerializeField]
	private float m_ExpelItemTime;

	private Visible.WeakReference m_SpawnedItem = new Visible.WeakReference();

	private float m_Timer;

	private bool m_Ejecting;

	public bool Ejecting => m_Ejecting;

	public float ItemExpelTime => m_ExpelItemTime;

	public TankBlock Eject(Transform locator, BlockTypes blockType)
	{
		if (m_Ejecting)
		{
			d.LogWarning("BlockEjector asked to eject block when already busy");
			StopEjecting();
		}
		Vector3 position = locator.position;
		Quaternion identity = Quaternion.identity;
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock(blockType, position, identity);
		if (tankBlock != null)
		{
			tankBlock.visible.trans.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			tankBlock.visible.EnablePhysics(enable: false);
			Rigidbody rbody = tankBlock.rbody;
			if (rbody != null)
			{
				rbody.velocity = locator.up * m_ContentsEjectSpeed;
				rbody.angularVelocity = locator.right * m_ContentsEjectAngularVelocity;
			}
			m_SpawnedItem.Set(tankBlock.visible);
			m_Timer = 0f;
			m_Ejecting = true;
		}
		return tankBlock;
	}

	public void StopEjecting()
	{
		Visible visible = m_SpawnedItem.Get();
		if (visible != null)
		{
			visible.trans.localScale = Vector3.one;
			visible.EnablePhysics(enable: true);
			m_SpawnedItem.Set(null);
		}
		m_Timer = 0f;
		m_Ejecting = false;
	}

	public void Update()
	{
		if (!m_Ejecting)
		{
			return;
		}
		m_Timer += Time.deltaTime;
		if (m_Timer < m_ExpelItemTime)
		{
			Visible visible = m_SpawnedItem.Get();
			if (visible != null)
			{
				visible.trans.localScale = Vector3.one * m_Timer / m_ExpelItemTime;
			}
		}
		else
		{
			StopEjecting();
		}
	}
}
