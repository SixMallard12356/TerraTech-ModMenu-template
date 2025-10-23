using System;
using UnityEngine;

public class GUIManager : Singleton.Manager<GUIManager>
{
	public class LabelWriter
	{
		private GUIStyle styleMain;

		private GUIStyle styleShadow;

		private float currentFade = 1f;

		public void SetFont(Font font, float height, TextAnchor align)
		{
			if (styleMain == null)
			{
				styleMain = new GUIStyle(GUI.skin.label);
				styleMain.alignment = align;
				styleShadow = new GUIStyle(styleMain);
			}
			styleMain.fontSize = (int)height;
			styleShadow.fontSize = (int)height;
			if (styleMain.font != font)
			{
				styleMain.font = font;
				styleShadow.font = font;
			}
		}

		public void SetColors(Color main, Color shadow)
		{
			SetColors(main, shadow, Color.clear, Color.clear, Color.clear);
		}

		public void SetColors(Color main, Color shadow, Color hover, Color active, Color focused)
		{
			styleMain.normal.textColor = main;
			styleShadow.normal.textColor = shadow;
			if (hover != Color.clear)
			{
				styleMain.hover.textColor = hover;
				styleMain.hover.background = Singleton.Manager<GUIManager>.inst.textBgTexture;
			}
			if (active != Color.clear)
			{
				styleMain.active.textColor = active;
				styleMain.active.background = Singleton.Manager<GUIManager>.inst.textBgTexture;
			}
			if (focused != Color.clear)
			{
				styleMain.focused.textColor = focused;
				styleMain.focused.background = Singleton.Manager<GUIManager>.inst.textBgTexture;
			}
		}

		public void SetFade(float fade)
		{
			currentFade = fade;
		}

		public void SetWrapMode(bool wrap)
		{
			if (styleMain != null)
			{
				styleMain.wordWrap = wrap;
			}
			if (styleShadow != null)
			{
				styleShadow.wordWrap = wrap;
			}
		}

		private void UpdateColours()
		{
			styleMain.normal.textColor = styleMain.normal.textColor.SetAlpha(currentFade);
			styleShadow.normal.textColor = styleShadow.normal.textColor.SetAlpha(currentFade);
			styleMain.hover.textColor = styleMain.hover.textColor.SetAlpha(currentFade);
			styleMain.active.textColor = styleMain.active.textColor.SetAlpha(currentFade);
			styleMain.focused.textColor = styleMain.focused.textColor.SetAlpha(currentFade);
		}

		public Vector2 CalcSize(string text)
		{
			return CalcSize(text, Vector2.zero);
		}

		public Vector2 CalcSize(string text, Vector2 maxSize)
		{
			GUIContent content = new GUIContent(text);
			Vector2 result = styleMain.CalcSize(content);
			if (maxSize.x != 0f)
			{
				result.x = Mathf.Min(result.x, maxSize.x);
				result.y = styleMain.CalcHeight(content, result.x);
			}
			if (maxSize.y != 0f)
			{
				result.y = Mathf.Min(result.y, maxSize.y);
			}
			return result;
		}

		public void DoLabel(Rect rect, string text)
		{
			DoLabel(rect, text, new Vector2(1f, 1f));
		}

		public void DoLabel(Rect rect, string text, Vector2 shadowOffset)
		{
			UpdateColours();
			GUI.Label(rect, text, styleShadow);
			rect.center -= shadowOffset;
			GUI.Label(rect, text, styleMain);
		}

		public bool DoButton(Rect rect, string text)
		{
			return DoButton(rect, text, new Vector2(1f, 1f));
		}

		public bool DoButton(Rect rect, string text, Vector2 shadowOffset)
		{
			UpdateColours();
			GUI.Label(rect, text, styleShadow);
			rect.center -= shadowOffset;
			return GUI.Button(rect, text, styleMain);
		}
	}

	[Serializable]
	public class LabelAnchor
	{
		public Vector2 screenPos;

		public Vector2 maxSize = Vector2.zero;

		public TextAnchor align;

		public void DoLabel(LabelWriter writer, string text)
		{
			writer.DoLabel(GetAnchoredRect(writer, text), text);
		}

		public bool DoButton(LabelWriter writer, string text)
		{
			return writer.DoButton(GetAnchoredRect(writer, text), text);
		}

		public Rect GetAnchoredRect(LabelWriter writer, string text)
		{
			Vector2 vector = writer.CalcSize(text, new Vector2(maxSize.x * (float)Screen.width, maxSize.y * (float)Screen.height));
			Rect result = new Rect(0f, 0f, vector.x, vector.y);
			Vector2 vector2 = new Vector2((float)Screen.width * screenPos.x, (float)Screen.height * screenPos.y);
			switch (align)
			{
			case TextAnchor.LowerLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y);
				break;
			case TextAnchor.UpperLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y + vector.y * 0.5f);
				break;
			case TextAnchor.LowerCenter:
				result.center = new Vector2(vector2.x, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleCenter:
				result.center = new Vector2(vector2.x, vector2.y);
				break;
			case TextAnchor.UpperCenter:
				result.center = new Vector2(vector2.x, vector2.y + vector.y * 0.5f);
				break;
			case TextAnchor.LowerRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y);
				break;
			case TextAnchor.UpperRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y + vector.y * 0.5f);
				break;
			}
			return result;
		}
	}

	[Serializable]
	public class ImageAnchor
	{
		public Vector2 screenPos;

		public Vector2 size;

		public Texture2D texture;

		public TextAnchor align = TextAnchor.MiddleCenter;

		public bool buttonBorder;

		public bool DoButton(Texture2D overrideTex = null)
		{
			return DoButton(Vector2.zero, overrideTex);
		}

		public void DoLabel(Texture2D overrideTex = null)
		{
			DoLabel(Vector2.zero, overrideTex);
		}

		public bool DoButton(Vector2 pixelOffset, Texture2D overrideTex = null)
		{
			LastWidgetRect = GetAnchoredRect(pixelOffset);
			if (buttonBorder)
			{
				return GUI.Button(LastWidgetRect, (overrideTex == null) ? texture : overrideTex);
			}
			return GUI.Button(LastWidgetRect, (overrideTex == null) ? texture : overrideTex, GUI.skin.label);
		}

		public void DoLabel(Vector2 pixelOffset, Texture2D overrideTex = null)
		{
			LastWidgetRect = GetAnchoredRect(pixelOffset);
			if (buttonBorder)
			{
				GUI.Label(LastWidgetRect, (overrideTex == null) ? texture : overrideTex);
			}
			else
			{
				GUI.Label(LastWidgetRect, (overrideTex == null) ? texture : overrideTex, GUI.skin.label);
			}
		}

		public void DrawTextureFit(Texture2D overrideTex = null)
		{
			GUI.DrawTexture(GetAnchoredRect(Vector2.zero), (overrideTex == null) ? texture : overrideTex, ScaleMode.ScaleToFit);
		}

		public void DrawTextureFit(Vector2 pixelOffset, Texture2D overrideTex = null)
		{
			GUI.DrawTexture(GetAnchoredRect(pixelOffset), (overrideTex == null) ? texture : overrideTex, ScaleMode.ScaleToFit);
		}

		public void DrawTextureFill(Texture2D overrideTex = null)
		{
			GUI.DrawTexture(GetAnchoredRect(Vector2.zero), (overrideTex == null) ? texture : overrideTex, ScaleMode.StretchToFill);
		}

		public void DrawTextureFill(Vector2 pixelOffset, Texture2D overrideTex = null)
		{
			GUI.DrawTexture(GetAnchoredRect(pixelOffset), (overrideTex == null) ? texture : overrideTex, ScaleMode.StretchToFill);
		}

		public Rect GetAnchoredRect(Vector2 pixelOffset)
		{
			Vector2 vector = new Vector2(size.x * (float)Screen.width, size.y * (float)Screen.height);
			Rect result = new Rect(0f, 0f, vector.x, vector.y);
			Vector2 vector2 = new Vector2(screenPos.x * (float)Screen.width, screenPos.y * (float)Screen.height) + pixelOffset;
			switch (align)
			{
			case TextAnchor.LowerLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y);
				break;
			case TextAnchor.UpperLeft:
				result.center = new Vector2(vector2.x + vector.x * 0.5f, vector2.y + vector.y * 0.5f);
				break;
			case TextAnchor.LowerCenter:
				result.center = new Vector2(vector2.x, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleCenter:
				result.center = new Vector2(vector2.x, vector2.y);
				break;
			case TextAnchor.UpperCenter:
				result.center = new Vector2(vector2.x, vector2.y + vector.y * 0.5f);
				break;
			case TextAnchor.LowerRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y - vector.y * 0.5f);
				break;
			case TextAnchor.MiddleRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y);
				break;
			case TextAnchor.UpperRight:
				result.center = new Vector2(vector2.x - vector.x * 0.5f, vector2.y + vector.y * 0.5f);
				break;
			}
			return result;
		}
	}

	[Serializable]
	public class LabelWrapper
	{
		public string text;

		public Font font;

		public LabelAnchor anchor;

		public float textHeightScreen;

		public Color textColorMain = new Color(1f, 1f, 1f, 1f);

		public Color textColorShadow = new Color(0f, 0f, 0f, 0.7f);

		private LabelWriter writer = new LabelWriter();

		public void DoLabel(string text = null)
		{
			writer.SetFont(font, (float)Screen.height * textHeightScreen, TextAnchor.MiddleCenter);
			writer.SetColors(textColorMain, textColorShadow);
			anchor.DoLabel(writer, (text == null) ? this.text : text);
		}

		public bool DoButton(string text = null)
		{
			writer.SetFont(font, (float)Screen.height * textHeightScreen, TextAnchor.MiddleCenter);
			writer.SetColors(textColorMain, textColorShadow);
			return anchor.DoButton(writer, (text == null) ? this.text : text);
		}

		public void SetFade(float fade)
		{
			writer.SetFade(fade);
		}

		public void SetWrap(bool wrap)
		{
			writer.SetWrapMode(wrap);
		}

		public Rect GetRect(string text = null)
		{
			return anchor.GetAnchoredRect(writer, (text == null) ? this.text : text);
		}
	}

	[Serializable]
	public class LabelGrid
	{
		public Vector2 screenOrigin;

		public Vector2 screenDims;

		public int columns;

		public int rows;

		public bool transpose;

		public Vector2 dilation;

		private LabelAnchor anchor = new LabelAnchor();

		private int currentIndex;

		public void Reset()
		{
			currentIndex = 0;
		}

		private void SetupAnchorNext(float yOffset)
		{
			float num = screenDims.x / (float)(columns + 1);
			float num2 = screenDims.y / (float)(rows + 1);
			int num3 = currentIndex % columns;
			int num4 = currentIndex / columns;
			if (transpose)
			{
				num4 = currentIndex % rows;
				num3 = currentIndex / rows;
			}
			float x = ((columns > 1) ? (dilation.x * (float)(2 * num3 / (columns - 1) - 1)) : 0f);
			float num5 = ((rows > 1) ? (dilation.y * (float)(2 * num4 / (rows - 1) - 1)) : 0f);
			Vector2 vector = screenOrigin + new Vector2(x, num5 - yOffset);
			anchor.screenPos = vector + new Vector2((float)(num3 + 1) * num, (float)(num4 + 1) * num2);
			anchor.align = TextAnchor.MiddleCenter;
			currentIndex++;
		}

		public bool DoButton(LabelWriter writer, string text, float yOffset = 0f)
		{
			SetupAnchorNext(yOffset);
			return anchor.DoButton(writer, text);
		}

		public void DoLabel(LabelWriter writer, string text, float yOffset = 0f)
		{
			SetupAnchorNext(yOffset);
			anchor.DoLabel(writer, text);
		}

		public Rect GetAnchoredRect(LabelWriter writer, string text)
		{
			return anchor.GetAnchoredRect(writer, text);
		}
	}

	[Serializable]
	public class ImageGrid
	{
		public Vector2 screenOrigin;

		public Vector2 screenDims;

		public int columns;

		public int rows;

		public bool transpose;

		public Vector2 dilation;

		public bool buttonBorders;

		private ImageAnchor anchor = new ImageAnchor();

		private int currentIndex;

		public void Reset()
		{
			currentIndex = 0;
		}

		private void SetupAnchorNext(float yOffset)
		{
			float num = screenDims.x / (float)(columns + 1);
			float num2 = screenDims.y / (float)(rows + 1);
			int num3 = currentIndex % columns;
			int num4 = currentIndex / columns;
			if (transpose)
			{
				num4 = currentIndex % rows;
				num3 = currentIndex / rows;
			}
			float x = ((columns > 1) ? (dilation.x * (float)(2 * num3 / (columns - 1) - 1)) : 0f);
			float num5 = ((rows > 1) ? (dilation.y * (float)(2 * num4 / (rows - 1) - 1)) : 0f);
			Vector2 vector = screenOrigin + new Vector2(x, num5 - yOffset);
			anchor.screenPos = vector + new Vector2((float)(num3 + 1) * num, (float)(num4 + 1) * num2);
			anchor.align = TextAnchor.MiddleCenter;
			anchor.buttonBorder = buttonBorders;
			currentIndex++;
		}

		public bool DoButton(float height, Texture2D overrideTex = null, float yOffset = 0f)
		{
			SetupAnchorNext(yOffset);
			Texture2D texture2D = (overrideTex ? overrideTex : anchor.texture);
			float num = height * (float)Screen.height;
			float num2 = num * (float)texture2D.width / (float)texture2D.height;
			anchor.size = new Vector2(num2 / (float)Screen.width, num / (float)Screen.height);
			return anchor.DoButton(overrideTex);
		}

		public void DoLabel(float height, Texture2D overrideTex = null, float yOffset = 0f)
		{
			SetupAnchorNext(yOffset);
			Texture2D texture2D = (overrideTex ? overrideTex : anchor.texture);
			float num = height * (float)Screen.height;
			float num2 = num * (float)texture2D.width / (float)texture2D.height;
			anchor.size = new Vector2(num2 / (float)Screen.width, num / (float)Screen.height);
			anchor.DoLabel(overrideTex);
		}

		public Rect GetAnchoredRect()
		{
			return anchor.GetAnchoredRect(Vector2.zero);
		}
	}

	public Texture2D textBgTexture;

	public float fadeSpeed = 1f;

	public static Rect LastWidgetRect { get; private set; }

	private static Rect newRect(Vector2 centre, Vector2 size)
	{
		return new Rect
		{
			width = size.x,
			height = size.y,
			center = centre
		};
	}
}
