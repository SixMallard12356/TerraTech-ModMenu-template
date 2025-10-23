#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIHUDText : UIHUDElement
{
	[SerializeField]
	private Text m_TextComponent;

	public override void Show(object context)
	{
		if (context != null)
		{
			string text = context as string;
			if (m_TextComponent.text != text)
			{
				m_TextComponent.text = text;
			}
		}
		base.Show(context);
	}

	private void OnSpawn()
	{
		d.AssertFormat(m_TextComponent != null, "UIHUDText {0} - Text component is not set up!", base.name);
	}
}
