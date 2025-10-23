#define UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocalisedText : MonoBehaviour
{
	public enum StringDisplayType
	{
		Standard,
		Capitalised,
		LowerCase
	}

	public LocalisedString m_String;

	[SerializeField]
	private StringDisplayType m_StringDisplayType;

	private Text m_Text;

	private TextMeshProUGUI m_TextPro;

	private bool m_UseUnityText;

	public string GetDisplayString()
	{
		string text = m_String.Value;
		if (text != null)
		{
			if (m_StringDisplayType == StringDisplayType.Capitalised)
			{
				text = text.ToUpper();
			}
			else if (m_StringDisplayType == StringDisplayType.LowerCase)
			{
				text = text.ToLower();
			}
		}
		return text;
	}

	public void UpdateText()
	{
		if (Singleton.Manager<Localisation>.inst != null)
		{
			if (m_UseUnityText)
			{
				m_Text.text = GetDisplayString();
			}
			else
			{
				m_TextPro.text = GetDisplayString();
			}
		}
	}

	private void Setup()
	{
		m_Text = base.gameObject.GetComponent<Text>();
		m_TextPro = base.gameObject.GetComponent<TextMeshProUGUI>();
		if ((bool)m_Text)
		{
			m_UseUnityText = true;
		}
		else if ((bool)m_TextPro)
		{
			m_UseUnityText = false;
		}
		else
		{
			d.LogError("UILocalisedText - Component must be on a UIText Element or TextMeshPro Element, on object '" + base.gameObject.name + "'");
		}
	}

	public void Start()
	{
		Setup();
		UpdateText();
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
