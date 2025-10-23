using UnityEngine;

public abstract class UIOptions : MonoBehaviour
{
	public abstract void Setup(EventNoParams OnChangeEvent);

	public abstract UIScreenOptions.SaveFailureType CanSave();

	public abstract void SaveSettings();

	public abstract void ClearSettings();

	public abstract void ResetSettings();

	public abstract void OnCloseScreen();
}
