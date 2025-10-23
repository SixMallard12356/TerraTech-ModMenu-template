#define UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHints : UIHUDElement
{
	public enum IconType
	{
		None,
		KeyboardKey,
		ShiftKey,
		SpaceKey,
		Button
	}

	public enum HintType
	{
		TextOnly,
		TextAndIcon
	}

	public struct ShowContext
	{
		public GameHints.HintID hintID;

		public ManHints.HintDefinition hintDef;
	}

	public struct HintTimeout
	{
		public GameHints.HintID hintID;

		public float appearTime;
	}

	[EnumArray(typeof(HintType))]
	[SerializeField]
	private UIHintElement[] m_HintsPrefabs;

	[SerializeField]
	private Transform m_HintPanel;

	[SerializeField]
	private int m_MaxCount = -1;

	[SerializeField]
	private TextMeshProUGUI m_ExtraHintsText;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	[SerializeField]
	private Image m_ScrollBarImage;

	[SerializeField]
	private Image m_ScrollBarHandleImage;

	private Dictionary<int, UIHintElement> m_Hints = new Dictionary<int, UIHintElement>();

	private Dictionary<int, UIHintElement> m_HintsToRemove = new Dictionary<int, UIHintElement>();

	private UITableScroller m_TableScroller;

	private int m_LastFrameHintAdded;

	private const int m_MaxHintsBeforeShowExtraText = 3;

	private List<GameHints.HintID> m_HintsList = new List<GameHints.HintID>();

	private List<HintTimeout> m_HintsTimeoutList = new List<HintTimeout>();

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
		}
		if (context != null)
		{
			if (context is ShowContext showContext)
			{
				ShowHint(showContext.hintID, showContext.hintDef);
			}
		}
		else
		{
			d.LogError("UIHints.Show - Calling Show on UIHint with null context.");
		}
		if (SKU.ConsoleUI)
		{
			m_ScrollBarHandleImage.enabled = false;
			m_ScrollBarImage.enabled = false;
		}
	}

	public override void Hide(object context)
	{
		if (context != null)
		{
			if (context is GameHints.HintID hintId)
			{
				HideHint(hintId);
			}
			else
			{
				d.LogError("UIHints.Hide - Context passed in was not null, but not of the expected type either (GameHints.HintID).");
			}
		}
		else
		{
			base.Hide(context);
		}
	}

	public void ShowHint(GameHints.HintID hintId, ManHints.HintDefinition hintDefinition)
	{
		if (hintDefinition.m_UseDifferentHintMessageForPad && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			ShowHint(hintId, hintDefinition.m_HintMessagePad.Value, hintDefinition.m_HintIcon);
		}
		else
		{
			ShowHint(hintId, hintDefinition.m_HintMessage.Value, hintDefinition.m_HintIcon);
		}
	}

	public void ShowHint(GameHints.HintID hintId, string hintMessage, IconType hintIcon = IconType.None)
	{
		if (m_MaxCount != -1)
		{
			_ = m_Hints.Count;
			_ = m_MaxCount - 1;
		}
		if (!IsShowingHint(hintId))
		{
			int num = ((hintIcon != IconType.None) ? 1 : 0);
			UIHintElement uIHintElement = m_HintsPrefabs[num].Spawn();
			uIHintElement.trans.SetParent(m_HintPanel, worldPositionStays: false);
			uIHintElement.trans.SetSiblingIndex(0);
			uIHintElement.SetupHint(hintId, hintMessage);
			m_LastFrameHintAdded = Time.frameCount;
			m_Hints.Add((int)hintId, uIHintElement);
			m_HintsList.Add(hintId);
			m_HintsTimeoutList.Add(new HintTimeout
			{
				hintID = hintId,
				appearTime = Time.time
			});
		}
		else
		{
			d.LogError(string.Concat("UIHints.ShowHint - Hint with ID '", hintId, "' is already showing!"));
		}
	}

	public void HideHint(GameHints.HintID hintId)
	{
		if (!m_HintsToRemove.ContainsKey((int)hintId) && m_Hints.TryGetValue((int)hintId, out var value))
		{
			value.PlayHideAnimation();
			m_HintsToRemove.Add((int)hintId, value);
		}
	}

	public bool IsShowingHint(GameHints.HintID hintId)
	{
		return m_Hints.ContainsKey((int)hintId);
	}

	public bool IsAnyHintOnScreen()
	{
		return m_HintsList.Count > 0;
	}

	private void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.SkinsPalette);
		m_TableScroller = m_HintPanel.GetComponent<UITableScroller>();
	}

	private void OnRecycle()
	{
		foreach (KeyValuePair<int, UIHintElement> hint in m_Hints)
		{
			hint.Value.trans.SetParent(null, worldPositionStays: false);
			hint.Value.Recycle();
		}
		m_HintsList.Clear();
		m_Hints.Clear();
		m_HintsToRemove.Clear();
		m_HintsTimeoutList.Clear();
	}

	private void Update()
	{
		if (Time.frameCount == m_LastFrameHintAdded + 1)
		{
			m_TableScroller.ScrollToEntry(0);
		}
		foreach (KeyValuePair<int, UIHintElement> item in m_HintsToRemove)
		{
			UIHintElement value = item.Value;
			if (value != null && value.HasFinished())
			{
				int key = item.Key;
				m_Hints.Remove(key);
				m_HintsList.Remove((GameHints.HintID)key);
				value.trans.SetParent(null, worldPositionStays: false);
				value.Recycle();
				m_HintsToRemove.Remove(key);
				break;
			}
		}
		if (Singleton.Manager<ManHints>.inst.EnableHintTimeout)
		{
			float time = Time.time;
			float hintTimeoutTime = Singleton.Manager<ManHints>.inst.HintTimeoutTime;
			for (int num = m_HintsTimeoutList.Count - 1; num >= 0; num--)
			{
				HintTimeout hintTimeout = m_HintsTimeoutList[num];
				if (!Singleton.Manager<ManHints>.inst.IsShowingHint(hintTimeout.hintID))
				{
					m_HintsTimeoutList.RemoveAt(num);
				}
				else if (time - hintTimeout.appearTime >= hintTimeoutTime)
				{
					HideHint(hintTimeout.hintID);
					m_HintsTimeoutList.RemoveAt(num);
				}
			}
		}
		if (m_Hints.Count == 0)
		{
			HideSelf();
		}
		if (base.gameObject.activeSelf && Singleton.Manager<ManInput>.inst.GetButtonDown(65))
		{
			int count = m_HintsList.Count;
			if (count > 0)
			{
				int index = count - 1;
				HideHint(m_HintsList[index]);
				m_HintsList.RemoveAt(index);
			}
		}
		if (SKU.ConsoleUI)
		{
			if (m_Hints.Count > 3)
			{
				m_ScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
				m_ExtraHintsText.gameObject.SetActive(value: true);
				int num2 = m_Hints.Count - 3;
				m_ExtraHintsText.text = "+" + num2;
			}
			else
			{
				m_ExtraHintsText.gameObject.SetActive(value: false);
			}
		}
	}
}
