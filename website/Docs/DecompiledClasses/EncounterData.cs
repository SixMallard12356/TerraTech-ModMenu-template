using System;
using UnityEngine.Serialization;

[Serializable]
public class EncounterData
{
	public string m_Name;

	public Encounter m_EncounterPrefab;

	public EncounterConditions m_SpawnConditions;

	public LocationConditions m_LocationConditions;

	public ManSpawn.CameraSpawnConditions m_CameraSpawnCondition;

	[FormerlySerializedAs("m_IgnoreRadius")]
	public bool m_HasNoPosition;

	public bool m_BaseTechIsRadarPosition;

	public bool m_CanSpawnOffTile;

	public bool m_IgnoreSceneryWhenSpawning;

	public bool m_BlockFutureEncountersInThisRadius = true;

	public bool m_ShowAreaOnMiniMap;

	public bool m_AddLog = true;

	public bool m_SetActiveInLog;

	public bool m_SpawnWithoutUserAccept;

	public bool m_CanAcceptFromQuestGiver = true;

	public bool m_ForceSpawnIfNew;

	public bool m_CanBeCancelled;

	public bool m_RecycleAllManagedObjectsOnCancel = true;

	public bool m_AllowsPointOfInterest;

	public bool m_SkippedByTutorialSkip;

	public EncounterDetails EncounterDetails => m_EncounterPrefab.EncounterDetails;

	public bool CheckSpawnConditions()
	{
		if ((bool)m_EncounterPrefab && (bool)m_EncounterPrefab.GetRequiredSetPiece())
		{
			if (!Globals.inst.m_AllowSetPieceMissions)
			{
				return false;
			}
			if (!Globals.inst.m_AllowSetPieceMissionsOnConsole && SKU.ConsoleUI)
			{
				return false;
			}
		}
		return m_SpawnConditions.Passes();
	}
}
