using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScreenExitConfirm : UIScreen
{
	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private Button m_YesButton;

	[SerializeField]
	private Button m_NoButton;

	[SerializeField]
	private Button m_BackButton;

	public string Text
	{
		set
		{
			if ((bool)m_Text)
			{
				m_Text.text = value;
			}
		}
	}

	public UnityAction YesAction
	{
		set
		{
			if ((bool)m_YesButton)
			{
				m_YesButton.onClick.RemoveAllListeners();
				if (value != null)
				{
					m_YesButton.onClick.AddListener(value);
				}
			}
		}
	}

	public UnityAction NoAction
	{
		set
		{
			if ((bool)m_NoButton)
			{
				m_NoButton.onClick.RemoveAllListeners();
				if (value != null)
				{
					m_NoButton.onClick.AddListener(value);
				}
			}
		}
	}

	public bool BackButtonEnabled
	{
		set
		{
			if ((bool)m_BackButton)
			{
				m_BackButton.gameObject.SetActive(value);
			}
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
	}
}
