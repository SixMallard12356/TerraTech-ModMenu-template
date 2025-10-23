#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeySequence
{
	private struct KeyEntry
	{
		public KeyCode keyCode;

		public bool caseSensitive;

		public bool upperCase;
	}

	private KeyEntry[] keyCodes;

	private int currentIndex;

	private float maxInterval;

	private float lastAcceptTime;

	private const bool kCaseSensitive = true;

	private static List<KeyEntry> s_KeycodeParser = new List<KeyEntry>();

	public KeySequence(string keySequence, float interval)
	{
		keyCodes = ParseKeyCodes(keySequence);
		currentIndex = 0;
		maxInterval = interval;
	}

	public bool Completed()
	{
		if (Time.time > lastAcceptTime + maxInterval)
		{
			currentIndex = 0;
		}
		bool flag = false;
		KeyEntry keyEntry = keyCodes[currentIndex];
		flag = Input.GetKeyDown(keyEntry.keyCode) && (!keyEntry.caseSensitive || keyEntry.upperCase == (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));
		if (flag)
		{
			currentIndex++;
			lastAcceptTime = Time.time;
			if (currentIndex == keyCodes.Length)
			{
				currentIndex = 0;
				Singleton.Manager<ManSFX>.inst.PlayMiscSFX(ManSFX.MiscSfxType.CheatCode);
				return true;
			}
		}
		if (Input.anyKeyDown && !flag && !Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.RightShift))
		{
			currentIndex = 0;
		}
		return false;
	}

	private KeyEntry[] ParseKeyCodes(string keySequence, bool errorOnUnmatchedChar = true)
	{
		for (int i = 0; i < keySequence.Length; i++)
		{
			KeyCode keyCode = KeyCode.None;
			bool caseSensitive = false;
			bool upperCase = false;
			char c = keySequence[i];
			char c2 = char.ToUpper(c);
			string value = c2.ToString();
			if (Enum.IsDefined(typeof(KeyCode), value))
			{
				keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), value);
				caseSensitive = true;
				upperCase = c == c2;
			}
			else if (c == '\'')
			{
				keyCode = KeyCode.Quote;
			}
			if (keyCode != KeyCode.None)
			{
				s_KeycodeParser.Add(new KeyEntry
				{
					keyCode = keyCode,
					caseSensitive = caseSensitive,
					upperCase = upperCase
				});
			}
			else if (errorOnUnmatchedChar)
			{
				d.LogErrorFormat("KeySequence - Failed to parse character '{0}' to a valid KeyCode in KeySequence '{1}'", c, keySequence);
			}
		}
		KeyEntry[] result = s_KeycodeParser.ToArray();
		s_KeycodeParser.Clear();
		return result;
	}
}
