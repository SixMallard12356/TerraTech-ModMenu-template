using UnityEngine;

public class OnGUICallback : MonoBehaviour
{
	public EventNoParams OnGUIEvent;

	private bool m_IsActive;

	public static OnGUICallback AddGUICallback(GameObject parentObject)
	{
		OnGUICallback onGUICallback = parentObject.AddComponent<OnGUICallback>();
		onGUICallback.m_IsActive = true;
		return onGUICallback;
	}

	public static void RemoveGUICallback(OnGUICallback guiCallbackObject)
	{
		guiCallbackObject.m_IsActive = false;
		guiCallbackObject.OnGUIEvent.EnsureNoSubscribers();
		Object.Destroy(guiCallbackObject);
	}

	private void OnGUI()
	{
		if (m_IsActive)
		{
			OnGUIEvent.Send();
		}
	}
}
