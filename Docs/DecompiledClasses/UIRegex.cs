using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class UIRegex : MonoBehaviour
{
	public string m_Regex = "[^\\w\\s]";

	private InputField m_InputField;

	private void Awake()
	{
		m_InputField = GetComponent<InputField>();
	}

	private void Update()
	{
		if (!VirtualKeyboard.IsRequired())
		{
			m_InputField.text = Regex.Replace(m_InputField.text, m_Regex, string.Empty);
		}
	}
}
