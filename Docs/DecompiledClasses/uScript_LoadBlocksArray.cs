using UnityEngine;

public class uScript_LoadBlocksArray : uScriptLogic
{
	private bool m_Loaded;

	public bool Out => true;

	public void In(string uniqueName, GameObject owner, ref BlockTypes[] data)
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
		m_Loaded = true;
	}
}
