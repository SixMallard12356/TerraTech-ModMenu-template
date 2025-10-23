using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class SevenSegmentDisplay : MonoBehaviour
{
	[SerializeField]
	protected int m_NumDigits = 2;

	[SerializeField]
	protected int m_NumFractionSpaces;

	protected TextMeshPro m_Display;

	private static string _s_OutputString;

	private int NumChars => m_NumDigits + ((m_NumFractionSpaces > 0) ? 1 : 0);

	public void SetDisplay(float value)
	{
		_s_OutputString = value.ToString("F" + m_NumFractionSpaces);
		if (_s_OutputString.Length > NumChars)
		{
			_s_OutputString = _s_OutputString.Remove(0, _s_OutputString.Length - NumChars);
		}
		while (_s_OutputString.Length < NumChars)
		{
			_s_OutputString = "0" + _s_OutputString;
		}
		m_Display.text = _s_OutputString;
	}

	private void Awake()
	{
		m_Display = GetComponent<TextMeshPro>();
	}

	private void OnValidate()
	{
		if (m_NumDigits < m_NumFractionSpaces)
		{
			m_NumFractionSpaces = m_NumDigits;
		}
	}
}
