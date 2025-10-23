#define UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CheckpointChallengeData : ChallengeData
{
	private struct BlockerData
	{
		public WorldTile Tile;

		public SceneryBlocker Blocker;

		public IntVector2 TilePos;
	}

	[SerializeField]
	private CheckpointChallenge.Track m_Track;

	[SerializeField]
	[Tooltip("Is this a challenge where the timer ticks down to reach the checkpoint before time is up. If False time ticks up - get fastest time.")]
	private bool m_TimeTicksDown;

	[SerializeField]
	[Tooltip("Do all gates have the same time, or a unique time per checkpoint?")]
	private bool m_FixedTimePerCheckpoint;

	[SerializeField]
	[Tooltip("When passing a checkpoint is the next checkpoint's time added to the current time, or do we start fresh and use only the checkpoint's time?")]
	private bool m_CarryOverTime = true;

	[Tooltip("Time player has to reach the Checkpoint")]
	[SerializeField]
	private float[] m_TimePerCheckpoint;

	[SerializeField]
	private bool m_PlayDangerMusic;

	[SerializeField]
	[Tooltip("How long to leave the Timer UI up after the challenge has ended successfully")]
	private float m_ShowResultsTime;

	private static Collider[] s_SphereCastResult = new Collider[1];

	private List<BlockerData> m_SceneryBlockerData = new List<BlockerData>();

	private List<BlockerData> m_BlockersWaitingForTileLoadList = new List<BlockerData>();

	private bool m_SubscribedToTileCallback;

	private static List<Vector4> s_PrevPositions = new List<Vector4>();

	public CheckpointChallenge.Track Track => m_Track;

	public bool TimeTicksDown => m_TimeTicksDown;

	public bool CarryOverTime => m_CarryOverTime;

	public bool PlayDangerMusic => m_PlayDangerMusic;

	public float ShowResultsTime => m_ShowResultsTime;

	public float GetTimeForCheckpoint(int checkpointIndex)
	{
		checkpointIndex = ((!m_FixedTimePerCheckpoint) ? checkpointIndex : 0);
		return m_TimePerCheckpoint[checkpointIndex];
	}

	public float GetTimeLimit()
	{
		float num = 0f;
		if (m_TimeTicksDown)
		{
			for (int i = 0; i < m_TimePerCheckpoint.Length; i++)
			{
				num += GetTimeForCheckpoint(i);
			}
		}
		return num;
	}

	public override void SpawnChallengeSceneryBlockers(Vector3 scenePosition, Quaternion sceneRotation)
	{
		if (m_Track == null)
		{
			return;
		}
		TrackSpline.AreaIterator enumerator = new TrackSpline.AreaIterator(m_Track.spline, 1.25f).GetEnumerator();
		while (enumerator.MoveNext())
		{
			TrackSpline.AreaIterator.PositionInfo current = enumerator.Current;
			Vector3 scenePos = scenePosition + sceneRotation * current.position;
			SceneryBlocker blocker = SceneryBlocker.CreateSphereBlocker(SceneryBlocker.BlockMode.Regrow, WorldPosition.FromScenePosition(in scenePos), current.width * 1.5f);
			IntVector2 tilePos = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in scenePos);
			WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos);
			BlockerData blockerData = new BlockerData
			{
				Blocker = blocker,
				Tile = worldTile,
				TilePos = tilePos
			};
			if (worldTile == null)
			{
				if (!m_SubscribedToTileCallback)
				{
					m_SubscribedToTileCallback = true;
					Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnTileLoaded);
				}
				m_BlockersWaitingForTileLoadList.Add(blockerData);
			}
			else
			{
				AddToBlockerData(blockerData);
			}
		}
	}

	public override Challenge CreateChallenge(Challenge.PlacementInfo placeInfo)
	{
		return new CheckpointChallenge(m_Track, this, placeInfo);
	}

	public override void RecycleChallengeSceneryBlockers()
	{
		foreach (BlockerData sceneryBlockerDatum in m_SceneryBlockerData)
		{
			sceneryBlockerDatum.Tile.RemoveSceneryBlocker(sceneryBlockerDatum.Blocker);
		}
		m_SceneryBlockerData.Clear();
		if (m_SubscribedToTileCallback)
		{
			m_SubscribedToTileCallback = false;
			Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
		}
	}

	public override bool IsValidPlacementForEncounter(Vector3 position, Quaternion rotation, EncounterToSpawn encounterToSpawn)
	{
		bool flag = true;
		float num = float.MaxValue;
		float num2 = float.MinValue;
		Vector3 vector = Vector3.negativeInfinity;
		TrackSpline.AreaIterator enumerator = new TrackSpline.AreaIterator(m_Track.spline).GetEnumerator();
		while (enumerator.MoveNext())
		{
			TrackSpline.AreaIterator.PositionInfo current = enumerator.Current;
			Vector3 scenePos = position + rotation * current.position;
			float num3 = current.width * 0.5f;
			bool flag2 = m_Track.spline.IsFreeform || Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos);
			if (flag2)
			{
				flag2 = flag2 && Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(scenePos, num3, WorldTile.LoadStep.Populated) && !Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, scenePos, num3);
				if (flag2 && m_Track.spline.IsFreeform)
				{
					Singleton.Manager<ManWorld>.inst.GetTerrainHeight(scenePos, out var outHeight);
					flag2 = scenePos.y > outHeight - 2f;
				}
				WorldTile worldTile = null;
				if (flag2)
				{
					worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos);
					WorldTile.TilePosInfo nearestTilePosInfo = worldTile.GetNearestTilePosInfo(scenePos);
					flag2 = encounterToSpawn.m_EncounterData.m_LocationConditions.Passes(nearestTilePosInfo);
				}
				flag2 = flag2 && !ManEncounterPlacement.IsOverlappingEncounter(scenePos, num3);
				float radius = num3;
				flag2 = flag2 && Physics.OverlapSphereNonAlloc(scenePos, radius, s_SphereCastResult, Globals.inst.layerLandmark.mask, QueryTriggerInteraction.Ignore) == 0;
				if (flag2 && worldTile.IsLoaded)
				{
					flag2 = !Singleton.Manager<ManTechs>.inst.IsAnyTechOverlappingRange(scenePos, num3);
				}
				if (flag2 && !m_Track.spline.IsFreeform)
				{
					if (scenePos.y < num)
					{
						num = scenePos.y;
					}
					if (scenePos.y > num2)
					{
						num2 = scenePos.y;
					}
					if (vector != Vector3.negativeInfinity)
					{
						Vector3 input = scenePos - vector;
						float y = input.y;
						float magnitude = input.SetY(0f).magnitude;
						float num4 = Mathf.Atan2(y, magnitude) * 57.29578f;
						if (num4 > m_Track.m_MaxInclineDegrees || num4 < m_Track.m_MaxDeclineDegrees)
						{
							flag2 = false;
						}
					}
					vector = scenePos;
				}
			}
			if (!flag2)
			{
				flag = false;
				break;
			}
		}
		if (flag && num2 - num > m_Track.m_MaximumHeightVariation)
		{
			flag = false;
		}
		return flag;
	}

	public override void GetSafeAreaList(Vector3 position, Quaternion rotation, List<SafeAreaData> outSafeAreaList)
	{
		outSafeAreaList.Clear();
		TrackSpline.AreaIterator enumerator = new TrackSpline.AreaIterator(m_Track.spline, 1.25f).GetEnumerator();
		while (enumerator.MoveNext())
		{
			TrackSpline.AreaIterator.PositionInfo current = enumerator.Current;
			Vector3 scenePos = position + rotation * current.position;
			d.Assert(Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos), "CheckpointChallengeData.GetSafeAreaList - Failed to project to ground at position " + scenePos);
			outSafeAreaList.Add(new SafeAreaData
			{
				worldPosition = WorldPosition.FromScenePosition(in scenePos),
				radius = current.width * 0.5f + 16f
			});
		}
	}

	private void AddToBlockerData(BlockerData data)
	{
		d.Assert(data.Tile != null, "Should not be adding blocker data on a tile that hasn't loaded yet");
		Singleton.Manager<ManWorld>.inst.AddSceneryBlockerToTile(data.Blocker, data.Tile);
		m_SceneryBlockerData.Add(data);
	}

	private void OnTileLoaded(WorldTile tile)
	{
		for (int i = 0; i < m_BlockersWaitingForTileLoadList.Count; i++)
		{
			BlockerData data = m_BlockersWaitingForTileLoadList[i];
			if (tile.Coord == data.TilePos)
			{
				data.Tile = tile;
				AddToBlockerData(data);
				m_BlockersWaitingForTileLoadList.RemoveAt(i--);
				if (m_BlockersWaitingForTileLoadList.Count == 0)
				{
					m_SubscribedToTileCallback = false;
					Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
				}
			}
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void DrawDebugFeedback(Vector3 pos, bool valid, float radius)
	{
		if (!valid)
		{
			foreach (Vector4 s_PrevPosition in s_PrevPositions)
			{
				_ = s_PrevPosition;
			}
			s_PrevPositions.Clear();
		}
		else
		{
			s_PrevPositions.Add(new Vector4(pos.x, pos.y, pos.z, radius));
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void DrawFeedbackPoint(Vector3 pos, float radius, Color colour)
	{
		_ = m_Track.spline.IsFreeform;
	}
}
