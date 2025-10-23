#define UNITY_EDITOR
using UnityEngine;

[NodeToolTip("Any enemy tech inside the encounter area will be directed to flee the area, and then return to its original state")]
public class uScript_DirectEnemiesOutOfEncounter : uScriptLogic
{
	private Encounter m_Encounter;

	private float m_ScareTimer;

	private float m_EncounterRadiusSqr;

	private const float s_BufferDistance = 50f;

	private const float s_ScareInterval = 2f;

	private static readonly Bitfield<ObjectTypes> m_VisibleTypes = new Bitfield<ObjectTypes>(1);

	public bool Out => true;

	public void In(GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (m_ScareTimer <= Mathf.Epsilon)
			{
				m_ScareTimer = 2f;
				Visible visible = null;
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_Encounter.DefaultWaypoint.ID);
				if (trackedVisible != null)
				{
					visible = trackedVisible.visible;
				}
				if (visible != null)
				{
					m_EncounterRadiusSqr = m_Encounter.EncounterRadius * m_Encounter.EncounterRadius;
					foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(m_Encounter.Position, m_Encounter.EncounterRadius, m_VisibleTypes))
					{
						if ((bool)item.tank && item.tank.IsEnemy(0) && !item.tank.IsAnchored && (m_Encounter.Position - item.centrePosition).sqrMagnitude < m_EncounterRadiusSqr)
						{
							item.tank.AI.ForceFleeFromVisible(visible, m_Encounter.EncounterRadius, m_Encounter.EncounterRadius + 50f, 2.5f);
						}
					}
				}
			}
			m_ScareTimer -= Time.deltaTime;
		}
		else
		{
			d.LogErrorFormat("uScript_DirectEnemiesOutOfEncounter - No Encounter object found on passed in owner node {0}. Cannot direct enemies away!", ownerNode ? ownerNode.name : "<null>");
		}
	}

	public void OnEnable()
	{
		m_ScareTimer = 0f;
	}
}
