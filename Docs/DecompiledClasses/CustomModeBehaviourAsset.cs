using UnityEngine;

public abstract class CustomModeBehaviourAsset : ScriptableObject
{
	public abstract void EnterPreMode();

	public abstract void UpdateMode();

	public abstract void ExitMode();
}
