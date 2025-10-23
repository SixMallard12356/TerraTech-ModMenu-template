using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TransformParentChangedCatcher : MonoBehaviour
{
	public EventNoParams TransformParentChangedEvent;

	private bool m_IsEnabled = true;

	public static TransformParentChangedCatcher AddTransformParentChangedCatcher(GameObject parentObject)
	{
		return parentObject.AddComponent<TransformParentChangedCatcher>();
	}

	public static void RemoveTransformParentChangedCatcher(TransformParentChangedCatcher transformParentChangedCatcher)
	{
		transformParentChangedCatcher.m_IsEnabled = false;
		Object.Destroy(transformParentChangedCatcher);
	}

	private void OnTransformParentChanged()
	{
		if (m_IsEnabled)
		{
			TransformParentChangedEvent.Send();
		}
	}
}
