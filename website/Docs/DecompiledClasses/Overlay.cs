public abstract class Overlay
{
	public abstract void Update();

	public abstract void PerformCleanup();

	public abstract bool HasExpired();
}
