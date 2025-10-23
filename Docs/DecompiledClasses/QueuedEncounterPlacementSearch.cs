using System;
using System.Collections.Generic;

public struct QueuedEncounterPlacementSearch
{
	public EncounterCollectionParams collectionParams;

	public int requesterID;

	public List<EncounterToSpawn> potentialCoreEncounters;

	public Dictionary<FactionSubTypes, List<EncounterToSpawn>> potentialRandomEncounters;

	public WeightedGroup<FactionSubTypes> corpWeights;

	public Action<EncounterToSpawn> encounterCollectedEvent;

	public Action finishedCollectingEvent;

	public List<EncounterToSpawn> initialList;
}
