#define UNITY_EDITOR
using System;
using Newtonsoft.Json;

public class EncounterVisibleData : IEquatable<EncounterVisibleData>
{
	public int m_VisibleId = -1;

	[JsonIgnore]
	private bool m_RegisteredDeathEvent;

	public readonly PerVisibleParams Params;

	[JsonProperty]
	public ObjectTypes ObjectType { get; private set; }

	[JsonConstructor]
	public EncounterVisibleData()
	{
	}

	public EncounterVisibleData(TrackedVisible trackedVis, ObjectTypes type, PerVisibleParams visibleParams)
	{
		Setup(trackedVis);
		ObjectType = type;
		Params = visibleParams;
	}

	public void Load()
	{
		if (m_VisibleId != -2)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_VisibleId);
			if (trackedVisible != null)
			{
				Setup(trackedVisible);
				return;
			}
			d.LogError("tracked visible couldn't be loaded for " + m_VisibleId);
			m_VisibleId = -2;
		}
	}

	private void Setup(TrackedVisible trackedVis)
	{
		m_VisibleId = trackedVis.ID;
		if (trackedVis.visible.IsNotNull())
		{
			RegisterDeathEvent(trackedVis.visible);
		}
		trackedVis.OnRespawnEvent.Subscribe(RegisterDeathEvent);
	}

	private void RegisterDeathEvent(Visible vis)
	{
		if (vis.IsNotNull() && !m_RegisteredDeathEvent)
		{
			vis.RecycledEvent.Subscribe(VisibleRecycledEvent);
			m_RegisteredDeathEvent = true;
		}
	}

	private void UnregisterDeathEvent(Visible vis)
	{
		if (vis.IsNotNull() && m_RegisteredDeathEvent)
		{
			vis.RecycledEvent.Unsubscribe(VisibleRecycledEvent);
			m_RegisteredDeathEvent = false;
		}
	}

	private void VisibleRecycledEvent(Visible vis)
	{
		UnregisterDeathEvent(vis);
		if (vis.ID == m_VisibleId && vis.Killed)
		{
			m_VisibleId = -2;
		}
	}

	public bool Equals(EncounterVisibleData other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if ((object)this == other)
		{
			return true;
		}
		return m_VisibleId == other.m_VisibleId;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Equals((EncounterVisibleData)obj);
	}

	public override int GetHashCode()
	{
		return m_VisibleId;
	}

	public static bool operator ==(EncounterVisibleData left, EncounterVisibleData right)
	{
		return object.Equals(left, right);
	}

	public static bool operator !=(EncounterVisibleData left, EncounterVisibleData right)
	{
		return !object.Equals(left, right);
	}

	public static implicit operator bool(EncounterVisibleData mo)
	{
		return mo != null;
	}
}
