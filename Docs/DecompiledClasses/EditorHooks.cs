using System;
using UnityEngine;

public class EditorHooks
{
	public static bool Paused { get; private set; }

	public static GameObject SelectedObject { get; private set; }

	public static event Action Update;

	public static event Action PlayModeChanged;

	public static void SetDirty(UnityEngine.Object obj)
	{
	}

	public static bool SelectionContains(GameObject go)
	{
		return false;
	}
}
