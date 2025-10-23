#define UNITY_EDITOR
using System.Collections;
using UnityEngine;

public class uScript_SpawnBlockAbovePlayer : uScriptLogic
{
	private Encounter m_DataComponent;

	private TankBlock m_FirstBlock;

	public bool Out => true;

	public TankBlock In(BlockTypes blockType, string uniqueName = "", GameObject owner = null)
	{
		if ((bool)Singleton.playerTank)
		{
			if ((bool)owner && !m_DataComponent)
			{
				m_DataComponent = owner.GetComponent<Encounter>();
			}
			Plane plane = Util.CalculateFrustumPlanes(Singleton.camera)[3];
			float enter = 0f;
			plane.Raycast(new Ray(Singleton.playerTank.boundsCentreWorld, Vector3.up), out enter);
			enter += 1f;
			enter *= 2f;
			Visible visible = null;
			bool flag = false;
			if ((bool)m_DataComponent)
			{
				EncounterVisibleData visible2 = m_DataComponent.GetVisible(uniqueName);
				if (visible2 != null)
				{
					if (visible2.m_VisibleId != -2)
					{
						TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible2.m_VisibleId);
						if (trackedVisible == null)
						{
							d.LogError($"uScript_SpawnBlockAbovePlayer: TrackedVisible {visible2.m_VisibleId} no longer exists (from uniqueName={uniqueName})");
							flag = true;
						}
						else
						{
							visible = trackedVisible.visible;
						}
					}
					else if (visible2.m_VisibleId == -2)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					TrackedVisible trackedVisible2 = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Block, (int)blockType), Singleton.playerTank.boundsCentreWorld + enter * Vector3.up + Vector3.up * 2f, Quaternion.identity, addToObjectManager: true, forceSpawn: false);
					visible = trackedVisible2.visible;
					m_DataComponent.AddVisible(uniqueName, trackedVisible2, ObjectTypes.Block);
					Singleton.instance.StartCoroutine(RegisterAfterTime(visible));
					TankBlock block = visible.block;
					block.rbody.AddRandomVelocity(Vector3.zero, Singleton.instance.globals.blockKickRandomVelocity * 0.5f, Singleton.instance.globals.blockKickRandomAngVel);
					Singleton.Manager<ManEncounter>.inst.SpawnFireTrail(visible.trans, visible.centrePosition);
					if (Mode<ModeMain>.inst.TutorialLockBeam)
					{
						block.rbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
						block.CollideTerrainOnly(makeTerrainOnly: true);
						block.EnableTutorialCollision(enable: true, 5f);
					}
				}
				if ((bool)visible)
				{
					m_FirstBlock = visible.block;
				}
			}
		}
		return m_FirstBlock;
	}

	private IEnumerator RegisterAfterTime(Visible visible)
	{
		yield return new WaitForSeconds(2.5f);
		if ((bool)visible && visible.gameObject.activeInHierarchy)
		{
			Singleton.Manager<ManOverlay>.inst.AddBlockSpawnedOverlay(visible);
		}
	}

	public void OnDisable()
	{
		m_FirstBlock = null;
		m_DataComponent = null;
	}
}
