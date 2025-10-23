using System.Collections.Generic;
using UnityEngine;

public class DebugGui
{
	public enum BGMode
	{
		None,
		Boxed,
		Shadowed,
		BoxedShadowed
	}

	private static GUIStyle s_StyleTextFG = null;

	private static GUIStyle s_StyleTextBG = null;

	private static GUIStyle s_StyleTextBGBox = null;

	private static GUIStyle s_StyleBar = null;

	private static GUIContent s_Content = null;

	private static Dictionary<int, Texture2D> s_BarTextures = new Dictionary<int, Texture2D>();

	private static void UpdateStyles()
	{
		if (s_StyleTextFG == null)
		{
			s_StyleTextFG = new GUIStyle(GUI.skin.label);
			s_StyleTextFG.fontSize = 10;
			s_StyleTextFG.alignment = TextAnchor.MiddleCenter;
			s_StyleTextBG = new GUIStyle(GUI.skin.label);
			s_StyleTextBG.fontSize = 10;
			s_StyleTextBG.alignment = TextAnchor.MiddleCenter;
			s_StyleTextBG.normal.textColor = Color.black;
			s_StyleTextBGBox = new GUIStyle(GUI.skin.box);
			s_StyleTextBGBox.fontSize = 10;
			s_StyleTextBGBox.alignment = TextAnchor.MiddleCenter;
			s_StyleTextBGBox.normal.textColor = Color.black;
			s_StyleBar = new GUIStyle();
			s_Content = new GUIContent();
		}
	}

	public static void LabelWorld(string text, Color textColor, Vector3 position, BGMode bgMode = BGMode.BoxedShadowed, float range = 100f)
	{
		Vector3 rhs = position - Singleton.cameraTrans.position;
		if (!(Vector3.Dot(Singleton.cameraTrans.forward, rhs) < 0.1f) && !(rhs.sqrMagnitude > range * range))
		{
			Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
			LabelScreen(text, textColor, vector, bgMode);
		}
	}

	public static void LabelScreen(string text, Color textColor, Vector2 screenPos, BGMode bgMode = BGMode.Shadowed)
	{
		UpdateStyles();
		s_Content.text = text;
		float num = s_StyleTextFG.CalcSize(s_Content).x + 6f;
		Rect rect = new Rect(screenPos.x - num * 0.5f, (float)Screen.height - screenPos.y - 8f, num, 16f);
		Rect position = rect;
		position.center += new Vector2(1f, 1f);
		switch (bgMode)
		{
		case BGMode.Boxed:
			GUI.Box(position, GUIContent.none, s_StyleTextBGBox);
			break;
		case BGMode.Shadowed:
			s_StyleTextBG.normal.textColor = Color.black.SetAlpha(textColor.a * 0.75f);
			GUI.Label(position, text, s_StyleTextBG);
			break;
		case BGMode.BoxedShadowed:
			s_StyleTextBGBox.normal.textColor = Color.black.SetAlpha(textColor.a * 0.75f);
			GUI.Box(position, text, s_StyleTextBGBox);
			break;
		}
		s_StyleTextFG.normal.textColor = textColor;
		GUI.Label(rect, text, s_StyleTextFG);
	}

	public static bool MouseOverLabelWorld(string text, Vector3 position, float range = 100f)
	{
		Vector3 rhs = position - Singleton.cameraTrans.position;
		if (Vector3.Dot(Singleton.cameraTrans.forward, rhs) < 0.1f || rhs.sqrMagnitude > range * range)
		{
			return false;
		}
		Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
		return MouseOverLabelScreen(text, vector);
	}

	public static bool MouseOverLabelScreen(string text, Vector2 screenPos)
	{
		s_Content.text = text;
		float num = s_StyleTextFG.CalcSize(s_Content).x + 6f;
		return new Rect(screenPos.x - num * 0.5f, screenPos.y - 8f, num, 16f).Contains(Input.mousePosition);
	}

	public static void BarScreen(Vector2 size, float fullness, Color barColor, Vector2 screenPos)
	{
		UpdateStyles();
		Rect position = new Rect(screenPos.x - size.x * 0.5f, (float)Screen.height - screenPos.y - size.y * 0.5f, size.x * fullness, size.y);
		int hashCode = barColor.GetHashCode();
		if (!s_BarTextures.TryGetValue(hashCode, out var value))
		{
			value = new Texture2D(1, 1);
			value.SetPixel(0, 0, barColor);
			value.Apply();
			s_BarTextures[hashCode] = value;
		}
		s_StyleBar.normal.background = value;
		Color color = GUI.color;
		GUI.color = Color.white.SetAlpha(barColor.a);
		GUI.Box(position, GUIContent.none, s_StyleBar);
		GUI.color = color;
	}
}
