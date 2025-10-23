using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseMenu
{
	private struct Label
	{
		public string text;

		public Color colour;

		public Color hiColour;

		public bool Spotlit;
	}

	public Color textColourNormal = Color.white;

	public Color textColourHilight = Color.green;

	public Color textColourBG = Color.black;

	private bool doubleBG;

	private bool followMouse;

	private Vector3 cursorOffset = Vector3.zero;

	private int itemWidth;

	private int itemHeight;

	private int fontHeight = 13;

	private bool newItems;

	private int rows;

	private int columns;

	private Rect rect;

	private GUIStyle styleNormal;

	private GUIStyle styleBG;

	private Label[] itemLabels = new Label[10];

	private Vector3 anchorPos;

	private bool active;

	private const int itemBoxPadding = 6;

	public bool allowCameraDragWhileVisible;

	public int numItems { get; private set; }

	public bool IsActive => active;

	public string SelectedItem { get; private set; }

	public int SelectedIndex { get; private set; }

	public int totalHeight { get; private set; }

	public int totalWidth { get; private set; }

	public string GetItemName(int index)
	{
		return itemLabels[index].text;
	}

	public MouseMenu()
	{
	}

	public MouseMenu(bool followMouse, Vector3 cursorOffset)
	{
		this.followMouse = followMouse;
		this.cursorOffset = cursorOffset;
	}

	public void Show(Vector3 anchorPosition)
	{
		anchorPos = anchorPosition + cursorOffset;
		active = true;
	}

	public void Hide()
	{
		active = false;
	}

	public void SetDefaultColours(Color textNormal, Color textHilight, bool doubleBG = false)
	{
		SetDefaultColours(textNormal, textHilight, Color.black, doubleBG);
	}

	public void SetDefaultColours(Color textNormal, Color textHilight, Color textShadow, bool doubleBG = false)
	{
		textColourNormal = textNormal;
		textColourHilight = textHilight;
		textColourBG = textShadow;
		styleNormal = null;
		this.doubleBG = doubleBG;
	}

	public void SetItems(string[] labels, int itemWidth = 150, int itemHeight = 17)
	{
		numItems = labels.Length;
		if (numItems > itemLabels.Length)
		{
			Array.Resize(ref itemLabels, numItems);
		}
		for (int i = 0; i < numItems; i++)
		{
			itemLabels[i].text = labels[i];
			itemLabels[i].colour = textColourNormal;
			itemLabels[i].hiColour = textColourHilight;
			itemLabels[i].Spotlit = false;
		}
		this.itemWidth = itemWidth;
		this.itemHeight = itemHeight;
		newItems = true;
	}

	public void SetItems(IEnumerable<string> labels, IEnumerable<Color> colours, IEnumerable<Color> hiColours, int itemWidth = 150, int itemHeight = 17)
	{
		numItems = labels.Count();
		if (numItems > itemLabels.Length)
		{
			Array.Resize(ref itemLabels, numItems);
		}
		for (int i = 0; i < numItems; i++)
		{
			itemLabels[i].text = labels.ElementAt(i);
			itemLabels[i].colour = colours.ElementAt(i);
			itemLabels[i].hiColour = hiColours.ElementAt(i);
			itemLabels[i].Spotlit = false;
		}
		this.itemWidth = itemWidth;
		this.itemHeight = itemHeight;
		newItems = true;
	}

	public void UpdateMousePosition(Vector3 anchorPosition)
	{
		if (followMouse && active)
		{
			anchorPos = anchorPosition + cursorOffset;
		}
	}

	public void UpdateItemText(int index, string label)
	{
		if (index < numItems)
		{
			itemLabels[index].text = label;
		}
	}

	public void UpdateItemSpotlit(int index, bool state)
	{
		if (index < numItems)
		{
			itemLabels[index].Spotlit = state;
		}
	}

	public void UpdateItemColour(int index, Color normalColour, Color hiColour)
	{
		if (index < numItems)
		{
			itemLabels[index].colour = normalColour;
			itemLabels[index].hiColour = hiColour;
		}
	}

	public void Draw(Vector3 pointerPosition)
	{
		if (!active)
		{
			return;
		}
		if (!allowCameraDragWhileVisible)
		{
			if (Singleton.Manager<CameraManager>.inst.IsCurrent<TankCamera>())
			{
				TankCamera.inst.EndSpinControlMouse();
			}
			else if (Singleton.Manager<CameraManager>.inst.IsCurrent<FramingCamera>())
			{
				FramingCamera.inst.EndSpinControl();
			}
		}
		Vector2 point = new Vector2(pointerPosition.x, (float)Screen.height - pointerPosition.y);
		rows = numItems;
		columns = 1;
		totalHeight = rows * itemHeight;
		totalWidth = columns * itemWidth;
		while (totalHeight > Screen.height)
		{
			columns++;
			rows = (numItems - 1) / columns + 1;
			totalHeight = rows * itemHeight;
			totalWidth = columns * itemWidth;
		}
		rect = new Rect(Mathf.Max(Mathf.Min(anchorPos.x, Screen.width - totalWidth), 0f), Mathf.Min((float)Screen.height - anchorPos.y, Screen.height - totalHeight), totalWidth, totalHeight);
		SelectedIndex = -1;
		if (rect.Contains(point))
		{
			int num = (int)((point.y - rect.yMin) / (float)itemHeight);
			int num2 = (int)((point.x - rect.xMin) / (float)itemWidth);
			SelectedIndex = num2 * rows + num;
			if (SelectedIndex >= numItems)
			{
				SelectedIndex = -1;
			}
		}
		if (styleNormal == null)
		{
			styleNormal = new GUIStyle(GUI.skin.label);
			styleNormal.alignment = TextAnchor.MiddleLeft;
			styleNormal.fontSize = fontHeight;
			styleBG = new GUIStyle(GUI.skin.box);
			styleBG.normal.textColor = textColourBG;
			styleBG.alignment = TextAnchor.MiddleLeft;
			styleBG.fontSize = fontHeight;
		}
		if (newItems)
		{
			for (int i = 0; i < numItems; i++)
			{
				itemWidth = Mathf.Max(itemWidth, (int)styleNormal.CalcSize(new GUIContent(itemLabels[i].text)).x + 6);
			}
			newItems = false;
		}
		SelectedItem = null;
		for (int j = 0; j < numItems; j++)
		{
			Label label = itemLabels[j];
			Rect position = new Rect(rect.xMin + (float)(j / rows * itemWidth), rect.yMin + (float)(j % rows * itemHeight), itemWidth, itemHeight);
			if (doubleBG)
			{
				GUI.Box(position, "", styleBG);
			}
			styleNormal.normal.textColor = ((j == SelectedIndex || label.Spotlit) ? label.hiColour : label.colour);
			GUI.Box(position, label.text, styleBG);
			position.xMin += 3f;
			position.yMin -= 2f;
			GUI.Box(position, label.text, styleNormal);
			if (j == SelectedIndex)
			{
				SelectedItem = label.text;
			}
		}
	}

	public void DrawExtraLabel(Vector2 offset, string text, Color mainColour)
	{
		float width = GUI.skin.label.CalcSize(new GUIContent(text)).x + 6f;
		Rect position = new Rect(rect.xMin + (float)((1 + SelectedIndex / rows) * itemWidth) + offset.x, rect.yMin + (float)(SelectedIndex % rows * itemHeight) + offset.y, width, itemHeight);
		if (position.xMax > (float)Screen.width)
		{
			position.xMin -= position.xMax - (float)Screen.width;
		}
		styleNormal.normal.textColor = mainColour;
		GUI.Box(position, "", styleBG);
		GUI.Box(position, text, styleBG);
		position.xMin += 3f;
		position.yMin -= 2f;
		GUI.Box(position, text, styleNormal);
	}
}
