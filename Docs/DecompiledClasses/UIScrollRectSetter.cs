using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIScrollRectSetter : MonoBehaviour
{
	public void SetToTop()
	{
		ScrollRect component = GetComponent<ScrollRect>();
		if ((bool)component && (bool)component.content)
		{
			component.content.anchoredPosition = new Vector2(component.content.anchoredPosition.x, 0f);
		}
	}

	public void Scroll(float delta)
	{
		ScrollRect component = GetComponent<ScrollRect>();
		if ((bool)component && (bool)component.content && delta != 0f)
		{
			Vector2 anchoredPosition = component.content.anchoredPosition;
			anchoredPosition.y += delta;
			component.content.anchoredPosition = anchoredPosition;
			if (component.movementType == ScrollRect.MovementType.Elastic)
			{
				component.verticalNormalizedPosition = Mathf.Clamp01(component.verticalNormalizedPosition);
			}
		}
	}

	public void PageUpOrDown(bool up)
	{
		ScrollRect component = GetComponent<ScrollRect>();
		RectTransform component2 = GetComponent<RectTransform>();
		if ((bool)component && (bool)component.content)
		{
			float delta = (up ? component2.rect.height : (component2.rect.height * -1f));
			Scroll(delta);
		}
	}
}
