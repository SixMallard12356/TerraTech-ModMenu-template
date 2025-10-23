public abstract class ModBase
{
	public ModBase()
	{
	}

	public virtual void EarlyInit()
	{
	}

	public virtual bool HasEarlyInit()
	{
		return false;
	}

	public abstract void Init();

	public abstract void DeInit();

	public virtual void Update()
	{
	}

	public virtual void FixedUpdate()
	{
	}
}
