using UnityEngine;

public abstract class WorldSpaceObjectBase : MonoBehaviour, IWorldTreadmill
{
	internal int m_WorldTreadmillIndex;

	public abstract void OnMoveWorldOrigin(IntVector3 offset);

	public virtual void OnForceSyncToRigidBody()
	{
	}
}
