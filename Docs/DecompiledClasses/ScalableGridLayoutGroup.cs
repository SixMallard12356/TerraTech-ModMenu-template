using UnityEngine;
using UnityEngine.UI;

public class ScalableGridLayoutGroup : GridLayoutGroup
{
	[SerializeField]
	private bool m_UniformScaling = true;

	[SerializeField]
	private bool m_StretchToFit = true;

	private Vector2 m_OrigCellSize;

	private Vector2 m_OrigSpacing;

	private float m_WToHCellRatio;

	private float m_WToHSpacingRatio;

	private bool m_Initialised;

	private bool m_IsPoolTemplateObject;

	protected override void Awake()
	{
		base.Awake();
		if (!m_Initialised)
		{
			m_OrigCellSize = m_CellSize;
			m_OrigSpacing = m_Spacing;
			m_WToHCellRatio = ((m_CellSize.y == 0f) ? m_CellSize.x : (m_CellSize.x / m_CellSize.y));
			m_WToHSpacingRatio = ((m_Spacing.y == 0f) ? m_Spacing.x : (m_Spacing.x / m_Spacing.y));
			m_Initialised = true;
		}
	}

	protected override void Start()
	{
		base.Start();
		ScaleElementsToFixedDimensions();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		ScaleElementsToFixedDimensions();
		base.OnRectTransformDimensionsChange();
	}

	private void ScaleElementsToFixedDimensions()
	{
		if (!m_Initialised || m_IsPoolTemplateObject)
		{
			return;
		}
		if (base.constraint == Constraint.FixedColumnCount)
		{
			float num = m_OrigCellSize.x * (float)base.constraintCount + m_OrigSpacing.x * (float)(base.constraintCount - 1) - (float)base.padding.horizontal;
			float num2 = base.rectTransform.rect.size.x - (float)base.padding.horizontal;
			if (num != 0f && (m_StretchToFit || num2 < num))
			{
				float num3 = m_OrigCellSize.x / num;
				float num4 = m_OrigSpacing.x / num;
				float num5 = num3 * num2;
				float num6 = num4 * num2;
				base.cellSize = new Vector2(num5, (m_UniformScaling && m_WToHCellRatio != 0f) ? (num5 / m_WToHCellRatio) : m_OrigCellSize.y);
				base.spacing = new Vector2(num6, (m_UniformScaling && m_WToHSpacingRatio != 0f) ? (num6 / m_WToHSpacingRatio) : m_OrigSpacing.y);
			}
		}
		else if (base.constraint == Constraint.FixedRowCount)
		{
			float num7 = m_OrigCellSize.y * (float)base.constraintCount + m_OrigSpacing.y * (float)(base.constraintCount - 1) - (float)base.padding.vertical;
			float num8 = base.rectTransform.rect.size.y - (float)base.padding.vertical;
			if (num7 != 0f && (m_StretchToFit || num8 < num7))
			{
				float num9 = m_OrigCellSize.y / num7;
				float num10 = m_OrigSpacing.y / num7;
				float num11 = num9 * num8;
				float num12 = num10 * num8;
				base.cellSize = new Vector2(m_UniformScaling ? (num11 * m_WToHCellRatio) : m_OrigCellSize.x, num11);
				base.spacing = new Vector2(m_UniformScaling ? (num12 * m_WToHSpacingRatio) : m_OrigSpacing.x, num12);
			}
		}
	}

	private void PrePool()
	{
		m_IsPoolTemplateObject = true;
	}
}
