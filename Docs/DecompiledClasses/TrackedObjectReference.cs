#define UNITY_EDITOR
using UnityEngine;

public class TrackedObjectReference
{
	public Event<TrackedObjectReference> ObjectRespawnEvent;

	private uint m_Id;

	private TrackableObject m_TrackedObject;

	private WorldPosition m_LastPosition;

	public uint TrackedId => m_Id;

	public TrackableObject TrackedObject => m_TrackedObject;

	public Vector3 ScenePosition
	{
		get
		{
			if (!(m_TrackedObject != null))
			{
				return m_LastPosition.ScenePosition;
			}
			return m_TrackedObject.transform.position;
		}
	}

	public TrackedObjectReference(TrackableObject trackableObj, uint uniqueId)
	{
		m_Id = uniqueId;
		m_TrackedObject = trackableObj;
		if (m_TrackedObject != null)
		{
			SetPosition(m_TrackedObject.transform.position);
			m_TrackedObject.OnRecycleEvent.Subscribe(OnObjectRecycled);
		}
	}

	public TrackedObjectReference(SavedTrackedObject savedTrackedObjectData)
	{
		m_Id = savedTrackedObjectData.m_Id;
		m_TrackedObject = null;
		m_LastPosition = savedTrackedObjectData.m_Position;
	}

	public void OnObjectRespawn(TrackableObject trackableObj)
	{
		d.Assert(trackableObj != null, "TrackedObjectReference.OnObjectRespawn - passed in object was null.");
		m_TrackedObject = trackableObj;
		m_TrackedObject.SetTrackingID(m_Id);
		m_TrackedObject.OnRecycleEvent.Subscribe(OnObjectRecycled);
		ObjectRespawnEvent.Send(this);
	}

	public WorldPosition GetWorldPosition()
	{
		if (m_TrackedObject != null)
		{
			return WorldPosition.FromScenePosition(m_TrackedObject.transform.position);
		}
		return m_LastPosition;
	}

	private void SetPosition(Vector3 scenePos)
	{
		m_LastPosition = WorldPosition.FromScenePosition(in scenePos);
	}

	private void OnObjectRecycled(TrackableObject obj)
	{
		if (m_TrackedObject != null)
		{
			SetPosition(m_TrackedObject.transform.position);
			m_TrackedObject.OnRecycleEvent.Unsubscribe(OnObjectRecycled);
		}
		m_TrackedObject = null;
	}
}
