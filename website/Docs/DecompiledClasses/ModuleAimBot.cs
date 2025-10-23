using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleAimBot : Module
{
	private Transform target;

	private Vector3 targetPositionWorld;

	private float targetRadiusWorld;

	private ModuleVision visionModule;

	private void OnUpdate()
	{
		if (!base.block.tank)
		{
			return;
		}
		if (visionModule == null)
		{
			base.block.tank.blockman.IterateBlockComponents<ModuleVision>().FirstOrDefault();
		}
		target = null;
		float num = float.MaxValue;
		TechVision.VisibleIterator enumerator = base.block.tank.Vision.IterateVisibles(ObjectTypes.Vehicle).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			float sqrMagnitude = (current.trans.position - base.block.trans.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				target = current.trans;
			}
		}
		if (!target)
		{
			return;
		}
		targetPositionWorld = target.position;
		targetRadiusWorld = 0.5f;
		Tank component = target.GetComponent<Tank>();
		if ((bool)component)
		{
			targetPositionWorld = component.boundsCentreWorld;
			targetRadiusWorld = Mathf.Max(component.blockBounds.GetRadiusXZWorld(target.transform), targetRadiusWorld);
			return;
		}
		Collider collider = (target.GetComponent<Collider>() ? target.GetComponent<Collider>() : target.GetComponentInChildren<Collider>());
		if ((bool)collider)
		{
			targetPositionWorld = collider.transform.TransformPoint(collider.bounds.center);
			targetRadiusWorld = Mathf.Max(collider.bounds.GetRadiusXZWorld(collider.transform), targetRadiusWorld);
		}
	}

	private void ControlTurret(ModuleWeapon turret)
	{
		turret.FireControl = true;
		_ = target == null;
	}

	private void OnPool()
	{
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle() && (bool)target)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(base.transform.position, targetPositionWorld);
			Gizmos.DrawWireSphere(targetPositionWorld, targetRadiusWorld);
		}
	}
}
