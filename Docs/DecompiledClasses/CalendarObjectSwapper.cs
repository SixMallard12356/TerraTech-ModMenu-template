using System;
using UnityEngine;

public class CalendarObjectSwapper : MonoBehaviour
{
	[Serializable]
	public class ObjectSwapData
	{
		public string m_TimePeriodName = "Time Period";

		public Transform m_SwapParent;

		public Transform[] m_SwapObjects;

		public ParticleSystem m_Particles;
	}

	[SerializeField]
	private Transform[] m_DefaultObjects;

	[SerializeField]
	private ObjectSwapData[] m_SwapData;

	private Transform[] m_SwappedObjects;

	private string m_CurrentPeriod;

	public void SwapObject(string name)
	{
		TryTriggerSwap(name);
	}

	public bool TryTriggerSwap(string name)
	{
		bool result = false;
		ObjectSwapData objectSwapData = null;
		ManCalendar.TimePeriod timePeriodActive = Singleton.Manager<ManCalendar>.inst.GetTimePeriodActive();
		if (timePeriodActive != null && timePeriodActive.m_UserEnabled)
		{
			for (int i = 0; i < m_SwapData.Length; i++)
			{
				if ((name == null || m_SwapData[i].m_TimePeriodName == name) && timePeriodActive.m_Name == m_SwapData[i].m_TimePeriodName)
				{
					objectSwapData = m_SwapData[i];
					break;
				}
			}
		}
		if (objectSwapData != null)
		{
			if (objectSwapData.m_Particles != null)
			{
				ParticleSystem component = objectSwapData.m_Particles.transform.Spawn(objectSwapData.m_SwapParent.transform.position).GetComponent<ParticleSystem>();
				if (component != null)
				{
					component.Play();
				}
			}
			TriggerSwap(objectSwapData);
			result = true;
		}
		return result;
	}

	private void TriggerSwap(ObjectSwapData swapData)
	{
		RecycleSwappedObjects();
		m_SwappedObjects = new Transform[swapData.m_SwapObjects.Length];
		for (int i = 0; i < swapData.m_SwapObjects.Length; i++)
		{
			m_SwappedObjects[i] = swapData.m_SwapObjects[i].Spawn(swapData.m_SwapParent, swapData.m_SwapParent.transform.position, swapData.m_SwapParent.transform.rotation);
		}
		EnableDefaultObjects(enable: false);
		m_CurrentPeriod = swapData.m_TimePeriodName;
	}

	private void EnableDefaultObjects(bool enable)
	{
		for (int i = 0; i < m_DefaultObjects.Length; i++)
		{
			m_DefaultObjects[i].gameObject.SetActive(enable);
		}
	}

	private void RecycleSwappedObjects()
	{
		if (m_SwappedObjects != null)
		{
			for (int i = 0; i < m_SwappedObjects.Length; i++)
			{
				m_SwappedObjects[i].Recycle();
			}
		}
		m_SwappedObjects = null;
	}

	private void OnTimePeriodChange(ManCalendar.TimePeriod timePeriod, bool enable)
	{
		if (enable && timePeriod.m_UserEnabled)
		{
			TryTriggerSwap(timePeriod.m_Name);
		}
		else if (timePeriod.m_Name == m_CurrentPeriod)
		{
			RecycleSwappedObjects();
			EnableDefaultObjects(enable: true);
		}
	}

	private void OnSpawn()
	{
		TryTriggerSwap(null);
		Singleton.Manager<ManCalendar>.inst.UserTimePeriodChangedEvent.Subscribe(OnTimePeriodChange);
	}

	private void OnRecycle()
	{
		RecycleSwappedObjects();
		EnableDefaultObjects(enable: true);
		Singleton.Manager<ManCalendar>.inst.UserTimePeriodChangedEvent.Unsubscribe(OnTimePeriodChange);
	}
}
