#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUDButton : UIHUDElement
{
	[SerializeField]
	private Button m_Button;

	public override void Show(object context)
	{
		if (context != null)
		{
			UnityAction call = context as UnityAction;
			m_Button.onClick.AddListener(call);
		}
		base.Show(context);
	}

	public override void Hide(object context)
	{
		m_Button.onClick.RemoveAllListeners();
		base.Hide(context);
	}

	private void OnSpawn()
	{
		d.Assert(m_Button != null, "UIHUDButton - Could not find Button component!");
	}
}
