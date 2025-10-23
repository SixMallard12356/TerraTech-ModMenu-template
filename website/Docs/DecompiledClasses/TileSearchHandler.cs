using UnityEngine;

public abstract class TileSearchHandler
{
	public abstract bool IsSearchTileValid();

	public abstract bool InitSearchOnTile(EncounterPlacementSearchParams.SearchTile searchTile, object context);

	public abstract bool MoveToNextSearchOnTile();

	public abstract Vector3 GetSearchPos();

	public abstract bool EvaluateSearchPos();

	public Quaternion GetDesiredHeading(Encounter encounterPrefab, Vector3 scenePos)
	{
		if (encounterPrefab.HasPreferredPlayerApproachDirection)
		{
			float num = (Singleton.playerPos - scenePos).HorizontalAngle() - encounterPrefab.PreferredPlayerApproachDirection;
			if (encounterPrefab.GetRequiredSetPiece() != null)
			{
				num = Mathf.Round(num / 90f) * 90f;
			}
			return Quaternion.Euler(0f, num, 0f);
		}
		return Quaternion.identity;
	}
}
