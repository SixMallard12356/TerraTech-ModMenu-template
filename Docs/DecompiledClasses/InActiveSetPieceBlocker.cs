using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class InActiveSetPieceBlocker
{
	[JsonProperty]
	public WorldPosition WorldPos;

	[JsonProperty]
	public float EncounterRadius;

	[JsonIgnore]
	public Vector3 ScenePos => WorldPos.ScenePosition;

	[JsonConstructor]
	private InActiveSetPieceBlocker()
	{
	}

	public InActiveSetPieceBlocker(Encounter encounter)
	{
		WorldPos = WorldPosition.FromScenePosition(encounter.Position);
		EncounterRadius = encounter.EncounterRadius;
	}
}
