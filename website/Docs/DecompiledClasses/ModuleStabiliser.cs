#define UNITY_EDITOR
public class ModuleStabiliser : Module
{
	private bool _isActive;

	public void SetEnabled(bool enable)
	{
		SetStabiliserActiveOnTech(enable);
	}

	private void SetStabiliserActiveOnTech(bool setActive)
	{
		if (setActive != _isActive)
		{
			if (setActive)
			{
				base.block.tank.AddActiveStabiliser(this);
			}
			else
			{
				base.block.tank.RemoveActiveStabiliser(this);
			}
			_isActive = setActive;
		}
	}

	private void OnAttached()
	{
		SetStabiliserActiveOnTech(setActive: true);
	}

	private void OnDetaching()
	{
		SetStabiliserActiveOnTech(setActive: false);
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnRecycle()
	{
		d.Assert(!_isActive, "Stabiliser was not disabled when block is recycled!?");
	}
}
