using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

[CreateAssetMenu(fileName = "JoystickGlyphDB", menuName = "Asset/Joystick Glyph DB")]
public class JoystickGlyphDB : ScriptableObject
{
	public enum Platforms
	{
		Keyboard,
		GenericJoypad,
		XboxOne,
		Xbox360,
		PS4,
		Switch
	}

	[SerializeField]
	[EnumArray(typeof(Platforms))]
	private List<JoystickGlyphIndex> m_Mappings;

	private JoystickGlyphIndex m_SelectedMapping;

	public void SetPlatform(ControllerType controllerType, Guid hardwareTypeGuid)
	{
		m_SelectedMapping = GetMapping(controllerType, hardwareTypeGuid);
	}

	public int GetSpriteID(int buttonElementID, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		if (m_SelectedMapping != null)
		{
			return m_SelectedMapping.GetSpriteID(buttonElementID, glyphType);
		}
		return -1;
	}

	public int GetSpriteID(ControllerType controllerType, Guid hardwareTypeGuid, int buttonElementID, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		JoystickGlyphIndex mapping = GetMapping(controllerType, hardwareTypeGuid);
		if (mapping != null)
		{
			return mapping.GetSpriteID(buttonElementID, glyphType);
		}
		return -1;
	}

	public string GetTMPSpriteAssetName()
	{
		if (m_SelectedMapping != null)
		{
			return m_SelectedMapping.GetSpriteAssetName();
		}
		return null;
	}

	public string GetTMPSpriteAssetName(ControllerType controllerType, Guid hardwareTypeGuid)
	{
		JoystickGlyphIndex mapping = GetMapping(controllerType, hardwareTypeGuid);
		if (mapping != null)
		{
			return mapping.GetSpriteAssetName();
		}
		return null;
	}

	private JoystickGlyphIndex GetMapping(ControllerType controllerType, Guid hardwareTypeGuid)
	{
		if (SKU.PS4UI)
		{
			return m_Mappings[4];
		}
		if (SKU.XboxOneUI)
		{
			return m_Mappings[2];
		}
		if (SKU.SwitchUI)
		{
			return m_Mappings[5];
		}
		if (controllerType == ControllerType.Joystick)
		{
			return FindJoystickMapping(hardwareTypeGuid);
		}
		return m_Mappings[0];
	}

	private JoystickGlyphIndex FindJoystickMapping(Guid hardwareID)
	{
		for (int i = 0; i < m_Mappings.Count; i++)
		{
			JoystickGlyphIndex joystickGlyphIndex = m_Mappings[i];
			if (joystickGlyphIndex != null && joystickGlyphIndex.HardwareGUID == hardwareID)
			{
				return joystickGlyphIndex;
			}
		}
		return m_Mappings[1];
	}
}
