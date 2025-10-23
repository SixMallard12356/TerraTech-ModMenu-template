using UnityEngine;

public abstract class OverlayPanel : MonoBehaviour, ManHUD.Focussable
{
	private RectTransform m_Rect;

	private RectTransform m_ParentRect;

	public RectTransform Rect => m_Rect;

	public RectTransform ParentRect => m_ParentRect;

	public abstract void SetContext(object context);

	private void OnPool()
	{
		m_Rect = GetComponent<RectTransform>();
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHUD>.inst.AddOverlay(this);
		m_ParentRect = m_Rect.parent as RectTransform;
	}

	private void OnRecycle()
	{
		SetContext(null);
		Singleton.Manager<ManHUD>.inst.RemoveOverlay(this);
	}

	public virtual void SetFocusLevel(ManHUD.FocusLevel level)
	{
	}

	public virtual void RefreshPanel(object context)
	{
	}

	public Transform GetTransform()
	{
		return m_Rect;
	}
}
