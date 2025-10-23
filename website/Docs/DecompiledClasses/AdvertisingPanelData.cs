using System;
using UnityEngine;

[Obsolete]
public class AdvertisingPanelData : ScriptableObject
{
	[SerializeField]
	public LocalisedString m_TitleText;

	[SerializeField]
	public Sprite m_BackgroundImage;

	[SerializeField]
	public bool m_ShowNew;

	[SerializeField]
	public int m_DaysActiveAfterBuild;
}
