using System.Collections.Generic;
using UnityEngine;

public class OverlayBaseData : MonoBehaviour
{
	[SerializeField]
	public List<ManGameMode.GameType> m_HiddenInModes;

	public bool VisibleInCurrentMode
	{
		get
		{
			bool result = true;
			ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
			for (int i = 0; i < m_HiddenInModes.Count; i++)
			{
				if (m_HiddenInModes[i] == currentGameType)
				{
					result = false;
					break;
				}
			}
			return result;
		}
	}
}
