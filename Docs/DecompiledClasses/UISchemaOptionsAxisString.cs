using UnityEngine;
using UnityEngine.UI;

public class UISchemaOptionsAxisString : MonoBehaviour
{
	[SerializeField]
	private InputAxisMapping m_Axis;

	[SerializeField]
	private Text m_Text;

	private void OnPool()
	{
		UpdateText();
	}

	public void SetAxis(InputAxisMapping a)
	{
		if (a != m_Axis)
		{
			m_Axis = a;
			UpdateText();
		}
	}

	private void UpdateText()
	{
		if (m_Axis != InputAxisMapping.Unmapped && (bool)m_Text)
		{
			m_Text.text = ControlScheme.GetAxisName(m_Axis);
		}
		else
		{
			m_Text.text = " ";
		}
	}

	private void Start()
	{
		if (Singleton.Manager<Localisation>.inst != null)
		{
			Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(UpdateText);
		}
	}

	private void OnDestroy()
	{
		if ((bool)Singleton.Manager<Localisation>.inst)
		{
			Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(UpdateText);
		}
	}
}
