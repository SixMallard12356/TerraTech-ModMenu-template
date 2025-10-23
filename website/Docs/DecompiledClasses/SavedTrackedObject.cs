using System;

[Serializable]
public struct SavedTrackedObject
{
	public uint m_Id;

	public WorldPosition m_Position;

	public void Init(TrackedObjectReference trackedTerrainObj)
	{
		m_Id = trackedTerrainObj.TrackedId;
		m_Position = trackedTerrainObj.GetWorldPosition();
	}
}
