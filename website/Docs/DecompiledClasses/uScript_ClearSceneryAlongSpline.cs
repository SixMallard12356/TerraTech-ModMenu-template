#define UNITY_EDITOR
using UnityEngine;

public class uScript_ClearSceneryAlongSpline : uScriptLogic
{
	private TrackSpline m_Spline;

	private Transform m_SplineStartTrans;

	private TrackSpline.AreaIterator m_SplineAreaIterator;

	private bool m_StartedIteration;

	private bool m_CompletedIteration;

	private float m_LastSceneryClearTime;

	private bool m_HandledLastPosition;

	private bool m_HasShownBlockedWarning;

	private const ManSpawn.SceneryRemovalFlags kSceneryRemovalFlags = ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.PreventRegrow | ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage;

	public bool Out => true;

	public bool BusyClearing
	{
		get
		{
			if (m_StartedIteration)
			{
				return !m_CompletedIteration;
			}
			return false;
		}
	}

	public bool DoneClearing => m_CompletedIteration;

	public void In(Transform splineStartTrans, TrackSpline spline, [DefaultValue(0.1f)][SocketState(false, false)] float delayBetweenAreaClears, [DefaultValue(null)] Transform sceneryClearSFXPrefab, [DefaultValue(0.8f)][SocketState(false, false)] float stepSizeWidthPercentage, [SocketState(false, false)][DefaultValue(false)] bool clearUpToPenaltyWidth)
	{
		if (m_CompletedIteration)
		{
			return;
		}
		if (splineStartTrans != null && spline != null)
		{
			float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
			if (!m_StartedIteration)
			{
				m_Spline = spline;
				m_SplineStartTrans = splineStartTrans;
				m_StartedIteration = true;
				m_SplineAreaIterator = new TrackSpline.AreaIterator(m_Spline, stepSizeWidthPercentage);
				m_LastSceneryClearTime = currentModeRunningTime - delayBetweenAreaClears - Mathf.Epsilon;
				m_HandledLastPosition = true;
			}
			while (currentModeRunningTime >= m_LastSceneryClearTime + delayBetweenAreaClears)
			{
				if (m_HandledLastPosition && !m_SplineAreaIterator.MoveNext())
				{
					m_CompletedIteration = true;
					break;
				}
				Vector3 scenePos = m_SplineStartTrans.TransformPoint(m_SplineAreaIterator.Current.position);
				float radius = (clearUpToPenaltyWidth ? m_SplineAreaIterator.Current.penaltyWidth : m_SplineAreaIterator.Current.width) * 0.5f;
				if (Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(scenePos, radius))
				{
					scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
					ManSpawn.RemoveAllSceneryAroundPosition(scenePos, radius, ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.PreventRegrow | ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage);
					if (sceneryClearSFXPrefab != null)
					{
						sceneryClearSFXPrefab.Spawn(Singleton.dynamicContainer, scenePos);
					}
					m_LastSceneryClearTime = currentModeRunningTime;
					m_HandledLastPosition = true;
					continue;
				}
				if (!m_HasShownBlockedWarning)
				{
					d.LogWarning("uScript_ClearSceneryAlongSpline - Part of the challenge spline is off the loaded tiles!");
					m_HasShownBlockedWarning = true;
				}
				m_HandledLastPosition = false;
				break;
			}
		}
		else
		{
			d.LogError("uScript_ClearSceneryAlongSpline - Spline start Transform or the Spline passed in were null!");
		}
	}

	public void OnDisable()
	{
		m_Spline = null;
		m_SplineStartTrans = null;
		m_StartedIteration = false;
		m_CompletedIteration = false;
	}

	private void OnRecycle()
	{
		m_HasShownBlockedWarning = false;
	}
}
