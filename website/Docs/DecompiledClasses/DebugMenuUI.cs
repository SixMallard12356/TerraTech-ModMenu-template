using UnityEngine;

public abstract class DebugMenuUI : MonoBehaviour
{
	protected DebugMenuObject m_MenuData;

	public abstract void SetMenuObject(DebugMenuObject menuObject);

	public virtual void Show()
	{
	}
}
