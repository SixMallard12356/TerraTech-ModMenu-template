using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
	private int m_LastCursorStateIndex = -1;

	private bool m_CursorChangedFlag;

	public bool HasSizeOptions()
	{
		return Singleton.Manager<ManUI>.inst.CursorDataTable.PlatformSets[CursorDataTable.PlatformSetTypes.PC].m_DataSets.Length > 1;
	}

	public void GetLocalisedSizeNames(List<string> names)
	{
		names.Clear();
		CursorDataTable.CursorDataSet[] dataSets = Singleton.Manager<ManUI>.inst.CursorDataTable.PlatformSets[CursorDataTable.PlatformSetTypes.PC].m_DataSets;
		foreach (CursorDataTable.CursorDataSet cursorDataSet in dataSets)
		{
			names.Add(cursorDataSet.m_LocalisedName.Value);
		}
	}

	private void OnCursorSizeChanged(int newSize)
	{
		m_CursorChangedFlag = true;
	}

	private void Start()
	{
		UpdateMouseConfinement();
		GameCursor.CursorSizeChangedEvent.Subscribe(OnCursorSizeChanged);
	}

	public void UpdateMouseConfinement()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		Cursor.lockState = ((currentUser != null && currentUser.m_GraphicsSettings.m_ConfineMouseToScreen) ? CursorLockMode.Confined : CursorLockMode.None);
		Debug.Log($"[MousePointer.UpdateMouseConfinement] Mouse lockState is now {Cursor.lockState}");
	}

	private void LateUpdate()
	{
		if (!SKU.ConsoleUI)
		{
			int cursorState = (int)GameCursor.GetCursorState();
			CursorMode mode;
			CursorDataTable.CursorData currentCursorData = Singleton.Manager<ManUI>.inst.CursorDataTable.GetCurrentCursorData(CursorDataTable.PlatformSetTypes.PC, out mode, cursorState);
			if (m_CursorChangedFlag || cursorState != m_LastCursorStateIndex)
			{
				m_LastCursorStateIndex = cursorState;
				if (currentCursorData.m_Texture != null)
				{
					Cursor.SetCursor(currentCursorData.m_Texture, currentCursorData.m_Hotspot, mode);
				}
				m_CursorChangedFlag = false;
			}
		}
		else
		{
			Cursor.visible = false;
		}
	}
}
