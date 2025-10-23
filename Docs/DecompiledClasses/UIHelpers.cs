using UnityEngine;
using UnityEngine.UI;

public class UIHelpers
{
	private static Vector3[] s_PanelCornerPositions = new Vector3[4];

	public static Vector3 WorldToUILocalPosition(Vector3 worldPos, Camera viewCamera, Canvas canvas, RectTransform inTransform = null)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(screenPoint: (canvas.renderMode != RenderMode.ScreenSpaceCamera && canvas.renderMode != RenderMode.WorldSpace) ? Singleton.Manager<ManUI>.inst.WorldToScreenPoint(null, worldPos) : Singleton.Manager<ManUI>.inst.WorldToScreenPoint(viewCamera, worldPos), rect: inTransform ?? (canvas.transform as RectTransform), cam: canvas.worldCamera, localPoint: out var localPoint);
		return localPoint;
	}

	public static Vector3 WorldToPixelPosition(Vector3 worldPos)
	{
		return ViewportToPixelPosition(Singleton.camera.WorldToViewportPoint(worldPos));
	}

	public static Vector3 ViewportToPixelPosition(Vector3 viewPos)
	{
		float aspectChangeRatio = Singleton.Manager<ManUI>.inst.GetAspectChangeRatio();
		return new Vector3(viewPos.x * Singleton.Manager<ManUI>.inst.m_ReferenceResolution.x * aspectChangeRatio, viewPos.y * Singleton.Manager<ManUI>.inst.m_ReferenceResolution.y, viewPos.z) - Singleton.Manager<ManUI>.inst.m_HalfReferenceResolution.SetX(Singleton.Manager<ManUI>.inst.m_HalfReferenceResolution.x * aspectChangeRatio);
	}

	public static bool TryMoveRectToWorldPos(RectTransform rectToMove, Vector3 worldPos, float zPos)
	{
		Vector3 input = WorldToPixelPosition(worldPos);
		if (input.z >= 0f)
		{
			rectToMove.GetLocalCorners(s_PanelCornerPositions);
			Vector3 vector = s_PanelCornerPositions[0];
			Vector3 vector2 = s_PanelCornerPositions[2];
			Vector3 vector3 = ViewportToPixelPosition(new Vector3(0f, 0f, 0f));
			Vector3 vector4 = ViewportToPixelPosition(new Vector3(1f, 1f, 0f));
			int num;
			if (!(input.x + vector2.x < vector3.x) && !(input.x + vector.x >= vector4.x) && !(input.y + vector2.y < vector3.y))
			{
				num = ((input.y + vector.y >= vector4.y) ? 1 : 0);
				if (num == 0)
				{
					Vector3 input2 = Extensions.Clamp(min: vector3 - vector, max: vector4 - vector2, input: input);
					input2 = input2.SetZ(zPos);
					rectToMove.localPosition = input2;
				}
			}
			else
			{
				num = 1;
			}
			return num == 0;
		}
		return false;
	}

	public static void VertScrollToItemCentered(RectTransform scrollTrans, RectTransform itemTrans, float viewportHeight)
	{
		float y = Mathf.Max(0f - itemTrans.anchoredPosition.y - viewportHeight / 2f, 0f);
		Vector2 anchoredPosition = scrollTrans.anchoredPosition;
		anchoredPosition.y = y;
		scrollTrans.anchoredPosition = anchoredPosition;
	}

	public static void VertScrollToItem(RectTransform scrollTrans, RectTransform itemTrans, float viewportHeight)
	{
		float num = 0f - (itemTrans.anchoredPosition.y + itemTrans.rect.yMax);
		float num2 = 0f - (itemTrans.anchoredPosition.y + itemTrans.rect.yMin);
		Vector2 anchoredPosition = scrollTrans.anchoredPosition;
		if (num < anchoredPosition.y)
		{
			anchoredPosition.y = num;
		}
		else if (num2 > anchoredPosition.y + viewportHeight)
		{
			anchoredPosition.y = num2 - viewportHeight;
		}
		scrollTrans.anchoredPosition = anchoredPosition;
	}

	public static void VertScrollToEmbeddedItem(RectTransform scrollTrans, RectTransform itemTrans, RectTransform viewportTrans)
	{
		Rect rect = viewportTrans.WorldRect();
		Rect rect2 = itemTrans.WorldRect();
		float num = viewportTrans.rect.width / rect.width;
		float num2 = Mathf.Max(0f, rect2.max.y - rect.max.y);
		float num3 = Mathf.Max(0f, rect.min.y - rect2.min.y);
		Vector2 vector = Vector2.up * ((num3 > num2) ? num3 : (0f - num2)) * num;
		scrollTrans.anchoredPosition += vector;
	}

	public static bool HideDropdown(Dropdown dropdown)
	{
		if (dropdown != null && (bool)dropdown.transform.Find("Dropdown List"))
		{
			dropdown.Hide();
			return true;
		}
		return false;
	}
}
