using System;
using UnityEngine;

[Serializable]
public struct CorporationSkinUIInfo
{
	public Sprite m_PreviewImage;

	public Sprite m_SkinButtonImage;

	public Sprite m_SkinMiniPaletteImage;

	public LocalisedString m_LocalisedString;

	public string m_FallbackString;

	public bool m_SkinLocked;

	public bool m_IsModded;

	public bool m_AlwaysEmissive;
}
