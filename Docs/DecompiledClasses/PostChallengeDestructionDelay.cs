#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class PostChallengeDestructionDelay : MonoBehaviour
{
	public class SaveData
	{
		public HashSet<uint> DelayedDestructionIDs = new HashSet<uint>();

		public void Clear()
		{
			DelayedDestructionIDs.Clear();
		}
	}

	[SerializeField]
	private float m_TriggerRadius = 1f;

	[SerializeField]
	private float m_CountdownTime = 5f;

	private bool m_Inited;

	private uint m_TrackedObjectID;

	private Transform m_TransformToRecycle;

	private float m_ImminentTimeOfDestruction;

	private WorldPosition m_TriggerPos;

	private static Dictionary<uint, List<PostChallengeDestructionDelay>> s_ActiveDelayedDestructionObjectsByParentID = new Dictionary<uint, List<PostChallengeDestructionDelay>>();

	private static List<PostChallengeDestructionDelay> s_TempComponents = new List<PostChallengeDestructionDelay>();

	public static SaveData s_SaveData { get; private set; } = new SaveData();

	public static bool StaticTryInitDelayedDestruction(uint trackedObjectID, bool errorIfNotFound = true)
	{
		bool flag = false;
		TrackedObjectReference trackedObject = Singleton.Manager<ManVisible>.inst.GetTrackedObject(trackedObjectID);
		if (trackedObject != null)
		{
			if (trackedObject.TrackedObject.IsNotNull())
			{
				flag = Obj_InitDelayedDestruction(trackedObject);
			}
			if (flag)
			{
				d.Assert(!s_SaveData.DelayedDestructionIDs.Contains(trackedObjectID), $"StaticInitDelayedDestruction - Object with tracked ID {trackedObjectID} was already added to the list!");
				s_SaveData.DelayedDestructionIDs.Add(trackedObjectID);
			}
		}
		else
		{
			d.LogError($"StaticInitDelayedDestruction - Tracked ID {trackedObjectID} did not resolve to a valid TrackedObject!");
		}
		return flag;
	}

	public static void StaticUpdateDelayedDestructionObjects()
	{
		foreach (KeyValuePair<uint, List<PostChallengeDestructionDelay>> item in s_ActiveDelayedDestructionObjectsByParentID)
		{
			bool flag = true;
			for (int i = 0; i < item.Value.Count; i++)
			{
				if (!item.Value[i].TryUpdateAndExpire())
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				item.Value[0].m_TransformToRecycle.Recycle();
				s_SaveData.DelayedDestructionIDs.Remove(item.Key);
				s_ActiveDelayedDestructionObjectsByParentID.Remove(item.Key);
				break;
			}
		}
	}

	private static bool Obj_InitDelayedDestruction(TrackedObjectReference trackedObjectRef, bool errorIfNotFound = true)
	{
		bool result = false;
		trackedObjectRef.TrackedObject.GetComponentsInChildren(s_TempComponents);
		if (s_TempComponents.Count > 0)
		{
			foreach (PostChallengeDestructionDelay s_TempComponent in s_TempComponents)
			{
				s_TempComponent.InitDelayedDestruction(trackedObjectRef);
			}
			result = true;
		}
		else if (errorIfNotFound)
		{
			d.LogError($"StaticInitDelayedDestruction - Trying to initialise delayed destruction but target object {trackedObjectRef.TrackedObject} does not contain PostChallengeDestructionDelay component!");
		}
		s_TempComponents.Clear();
		return result;
	}

	private void InitDelayedDestruction(TrackedObjectReference trackedObjectRef)
	{
		d.Assert(!m_Inited, "PostChallengeDestructionDelay " + base.name + " was already initialised!");
		if (trackedObjectRef.TrackedObject.IsNotNull())
		{
			m_Inited = true;
			m_ImminentTimeOfDestruction = Time.time + m_CountdownTime;
			m_TriggerPos = WorldPosition.FromScenePosition(base.transform.position);
			m_TrackedObjectID = trackedObjectRef.TrackedId;
			m_TransformToRecycle = trackedObjectRef.TrackedObject.transform;
			if (!s_ActiveDelayedDestructionObjectsByParentID.TryGetValue(trackedObjectRef.TrackedId, out var value))
			{
				value = new List<PostChallengeDestructionDelay>();
				s_ActiveDelayedDestructionObjectsByParentID.Add(trackedObjectRef.TrackedId, value);
			}
			value.Add(this);
		}
		else
		{
			d.LogError("PostChallengeDestructionDelay.Init - Called on tracked object that has already been removed from the world!? Obliterating object now!");
			Singleton.Manager<ManVisible>.inst.ObliterateTrackedObjectFromWorld(trackedObjectRef);
		}
	}

	private bool TryUpdateAndExpire()
	{
		bool result = false;
		if ((Singleton.playerPos - m_TriggerPos.ScenePosition).magnitude >= m_TriggerRadius)
		{
			result = Time.time > m_ImminentTimeOfDestruction;
		}
		else
		{
			m_ImminentTimeOfDestruction = Time.time + m_CountdownTime;
		}
		return result;
	}

	private void OnSpawn()
	{
		m_Inited = false;
		m_TrackedObjectID = 0u;
		m_TransformToRecycle = null;
	}

	private void OnRecycle()
	{
		if (m_Inited && s_ActiveDelayedDestructionObjectsByParentID.TryGetValue(m_TrackedObjectID, out var value))
		{
			value.Remove(this);
			if (value.Count == 0)
			{
				s_ActiveDelayedDestructionObjectsByParentID.Remove(m_TrackedObjectID);
			}
		}
	}
}
