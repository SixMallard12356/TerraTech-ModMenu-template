#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TrackableObject : MonoBehaviour
{
	public Event<TrackableObject> OnRecycleEvent;

	private uint m_UniqueTrackedID = uint.MaxValue;

	public const uint kUniqueIDNotTracked = uint.MaxValue;

	public bool IsTracked => m_UniqueTrackedID != uint.MaxValue;

	public uint TrackedId => m_UniqueTrackedID;

	public TrackedObjectReference StartTrackingObject()
	{
		TrackedObjectReference trackedObjectReference = null;
		if (!IsTracked)
		{
			m_UniqueTrackedID = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextTrackableObjectID();
			trackedObjectReference = new TrackedObjectReference(this, m_UniqueTrackedID);
			Singleton.Manager<ManVisible>.inst.TrackObject(trackedObjectReference);
		}
		else
		{
			d.LogError("TrackableObject.StartTrackingObject - TrackableObject " + base.name + " is already being tracked!");
		}
		return trackedObjectReference;
	}

	public void StopTrackingObject()
	{
		if (IsTracked)
		{
			Singleton.Manager<ManVisible>.inst.StopTrackingObject(m_UniqueTrackedID);
			m_UniqueTrackedID = uint.MaxValue;
		}
		else
		{
			d.LogError("TrackableObject.StopTrackingObject - Trying to stop tracking object, but object was not tracked!");
		}
	}

	public void SetTrackingID(uint trackingID)
	{
		if (!IsTracked && trackingID != uint.MaxValue)
		{
			m_UniqueTrackedID = trackingID;
		}
		else
		{
			d.LogError("TerrainObject.SetTrackingID - TrackableObject " + base.name + " is already being tracked, or invalid tracking ID passed in!");
		}
	}

	public void ClearTrackingID()
	{
		m_UniqueTrackedID = uint.MaxValue;
	}

	private void OnRecycle()
	{
		OnRecycleEvent.Send(this);
		ClearTrackingID();
	}
}
