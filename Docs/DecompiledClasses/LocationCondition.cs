public abstract class LocationCondition
{
	public abstract bool TilePasses(WorldTile tile);

	public abstract bool Passes(WorldTile.TilePosInfo tilePosInfo);
}
