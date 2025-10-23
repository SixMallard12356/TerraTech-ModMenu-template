#define UNITY_EDITOR
using UnityEngine;

public class UIScaleElementByPlatform : MonoBehaviour
{
	[SerializeField]
	private float m_SwitchHandheldScale = 1f;

	private RectTransform m_RectTrans;

	private void OnHandheldModeChanged(bool isHandheld)
	{
		Vector3 vector = Vector3.one;
		if (isHandheld)
		{
			vector = m_SwitchHandheldScale * Vector3.one;
		}
		if (vector != m_RectTrans.localScale)
		{
			m_RectTrans.localScale = vector;
		}
	}

	private void OnPool()
	{
		m_RectTrans = GetComponent<RectTransform>();
		d.Assert(m_RectTrans != null, "The component must have a rect transform!");
		if (SKU.SwitchUI)
		{
			ManNintendoSwitch.HandheldModeChangedEvent.Subscribe(OnHandheldModeChanged);
		}
		else
		{
			base.enabled = false;
		}
	}

	private void OnSpawn()
	{
		if (SKU.SwitchUI)
		{
			OnHandheldModeChanged(ManNintendoSwitch.IsHandheldMode);
		}
	}
}
