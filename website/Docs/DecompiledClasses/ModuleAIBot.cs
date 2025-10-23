using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleAIBot : Module
{
	[SerializeField]
	public TechAI.AITypes[] m_AITypesEnabled;

	private Vector3 m_AimPos;

	public TechAI.AITypes[] AITypesEnabled => m_AITypesEnabled;

	private void OnAttached()
	{
		base.block.tank.AI.AddAI(this);
	}

	private void OnDetaching()
	{
		base.block.tank.AI.RemoveAI(this);
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}
}
