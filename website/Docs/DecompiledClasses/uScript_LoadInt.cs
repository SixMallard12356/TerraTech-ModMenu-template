using UnityEngine;

public class uScript_LoadInt : uScriptLogic
{
	private bool m_Loaded;

	public bool Out => true;

	public void In(ref int data, GameObject owner, string uniqueName)
	{
		Set(uniqueName, owner, ref data);
	}

	private void Set<T>(string uniqueName, GameObject owner, ref T data)
	{
		if (!m_Loaded)
		{
			T dataForEncounter = Extensions.GetDataForEncounter<T>(owner, uniqueName);
			if (!object.Equals(dataForEncounter, default(T)))
			{
				data = dataForEncounter;
			}
			m_Loaded = true;
		}
	}

	public void OnDisable()
	{
		m_Loaded = false;
	}
}
