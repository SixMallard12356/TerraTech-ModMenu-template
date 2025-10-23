#define UNITY_EDITOR
using System;
using TMPro;

public class TMP_UIUnicodeSafeInputField : TMP_InputField
{
	private bool m_PreventNextCharacter;

	private const uint m_UnicodeSurrogateRangeMin = 55296u;

	private const uint m_UnicodeSurrogateRangeMax = 56319u;

	protected override void Append(char input)
	{
		if ((uint)input >= 55296u && (uint)input <= 56319u)
		{
			m_PreventNextCharacter = true;
			return;
		}
		if (m_PreventNextCharacter)
		{
			m_PreventNextCharacter = false;
			return;
		}
		try
		{
			base.Append(input);
		}
		catch (ArgumentOutOfRangeException)
		{
			d.LogError($"UIUnicodeSafeInputField threw an ArgumentOutOfRangeException, caret pos={base.caretPosition} len={base.text.Length}");
		}
	}
}
