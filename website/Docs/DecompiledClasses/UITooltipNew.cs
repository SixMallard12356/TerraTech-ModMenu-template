#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.UI;

public class UITooltipNew : MonoBehaviour
{
	public struct TooltipInfo
	{
		public string Text;

		public UITooltipOptions Mode;

		public UITooltipAlignment Alignment;

		public Vector3 Position;

		public bool LocalPos;

		public TooltipInfo(TooltipComponent tooltipComponent)
		{
			Text = tooltipComponent.Text;
			Mode = tooltipComponent.Mode;
			Alignment = tooltipComponent.Alignment;
			Position = tooltipComponent.RectTrans.position + tooltipComponent.Offset;
			LocalPos = false;
		}

		public TooltipInfo(string text, UITooltipOptions mode, UITooltipAlignment alignment, Vector3 worldPosition)
		{
			Text = text;
			Mode = mode;
			Alignment = alignment;
			Position = UIHelpers.WorldToUILocalPosition(worldPosition, Singleton.camera, Singleton.Manager<ManUI>.inst.TooltipCanvas);
			LocalPos = true;
		}
	}

	[SerializeField]
	private UITooltipItem m_ItemDefault;

	[SerializeField]
	private UITooltipItem m_ItemWarning;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private HorizontalLayoutGroup m_HorizLayoutGroup;

	private TooltipComponent m_TooltipComponent;

	private RectTransform m_RectTrans;

	private bool m_ActiveBeforePause;

	public void Init()
	{
		m_RectTrans = base.transform as RectTransform;
		m_CanvasGroup.alpha = 0f;
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Combine(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
	}

	public void ShowToolTip(TooltipComponent tooltipComponent)
	{
		if (m_TooltipComponent != tooltipComponent)
		{
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.Changed.Unsubscribe(OnTooltipComponentChanged);
			}
			OnTooltipComponentChanged(tooltipComponent);
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.Changed.Subscribe(OnTooltipComponentChanged);
			}
		}
	}

	public void ShowToolTip(TooltipInfo tooltipInfo)
	{
		m_TooltipComponent = null;
		SetToolTip(tooltipInfo);
	}

	public void HideToolTip()
	{
		m_CanvasGroup.alpha = 0f;
	}

	public void HideToolTip(TooltipComponent tooltipComponent)
	{
		if (m_TooltipComponent == tooltipComponent)
		{
			m_CanvasGroup.alpha = 0f;
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.Changed.Unsubscribe(OnTooltipComponentChanged);
			}
			m_TooltipComponent = null;
		}
	}

	private void OnTooltipComponentChanged(TooltipComponent tooltipComponent)
	{
		if (tooltipComponent != null)
		{
			TooltipInfo toolTip = new TooltipInfo(tooltipComponent);
			SetToolTip(toolTip);
			m_TooltipComponent = tooltipComponent;
		}
	}

	private void OnScreenChanged(bool pushed, ManUI.ScreenType screenType)
	{
		if (screenType == ManUI.ScreenType.Pause && pushed && base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
			m_ActiveBeforePause = true;
		}
		if (screenType == ManUI.ScreenType.Pause && !pushed && m_ActiveBeforePause)
		{
			base.gameObject.SetActive(value: true);
			m_ActiveBeforePause = false;
		}
	}

	private void SetToolTip(TooltipInfo tooltipInfo)
	{
		m_CanvasGroup.alpha = 1f;
		m_ItemDefault.gameObject.SetActive(tooltipInfo.Mode == UITooltipOptions.Default);
		m_ItemWarning.gameObject.SetActive(tooltipInfo.Mode == UITooltipOptions.Warning);
		UITooltipItem uITooltipItem = m_ItemDefault;
		if (tooltipInfo.Mode == UITooltipOptions.Warning)
		{
			uITooltipItem = m_ItemWarning;
		}
		uITooltipItem.Text = tooltipInfo.Text;
		uITooltipItem.SetAlignment(tooltipInfo.Alignment);
		TextAnchor childAlignment = TextAnchor.LowerLeft;
		Vector2 pivot = Vector2.zero;
		switch (tooltipInfo.Alignment)
		{
		case UITooltipAlignment.TopLeft:
			childAlignment = TextAnchor.UpperLeft;
			pivot = new Vector2(1f, 0f);
			break;
		case UITooltipAlignment.TopRight:
			childAlignment = TextAnchor.UpperRight;
			pivot = new Vector2(1f, 1f);
			break;
		case UITooltipAlignment.BottomLeft:
			childAlignment = TextAnchor.LowerLeft;
			pivot = new Vector2(0f, 0f);
			break;
		case UITooltipAlignment.BottomRight:
			childAlignment = TextAnchor.LowerRight;
			pivot = new Vector2(1f, 0f);
			break;
		default:
			d.LogError("No alignment found for UITooltipAlignment. Default will be used instead. Type: " + tooltipInfo.Alignment);
			break;
		}
		m_HorizLayoutGroup.childAlignment = childAlignment;
		m_RectTrans.pivot = pivot;
		if (tooltipInfo.LocalPos)
		{
			m_RectTrans.localPosition = tooltipInfo.Position;
		}
		else
		{
			m_RectTrans.position = tooltipInfo.Position;
		}
	}

	private void OnRecycle()
	{
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.Changed.Unsubscribe(OnTooltipComponentChanged);
			ManUI inst = Singleton.Manager<ManUI>.inst;
			inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		}
	}
}
