using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public abstract class UIMiniMapLayer : MonoBehaviour
{
	protected RectTransform m_RectTrans;

	protected UIMiniMapDisplay m_MapDisplay;

	public virtual void Init(UIMiniMapDisplay mapDisplay)
	{
		m_MapDisplay = mapDisplay;
	}

	public abstract void UpdateLayer();

	protected bool CalculateIconDirectionFromScene(Vector3 scenePos, bool limitRange, float maxRangeSqr, float outVecLength, out Vector2 iconPosition2D)
	{
		Vector2 relativeVector2D = (scenePos - m_MapDisplay.FocalPoint.ScenePosition).ToVector2XZ();
		return CalculateIconDirection(relativeVector2D, limitRange, maxRangeSqr, outVecLength, out iconPosition2D);
	}

	protected bool CalculateIconDirectionFromWorld(Vector3 worldPos, float outVecLength, out Vector2 iconPosition2D)
	{
		return CalculateIconDirectionFromWorld(worldPos, limitRange: false, 0f, outVecLength, out iconPosition2D);
	}

	protected bool CalculateIconDirectionFromWorld(Vector3 worldPos, bool limitRange, float maxRangeSqr, float outVecLength, out Vector2 iconPosition2D)
	{
		Vector2 relativeVector2D = (worldPos - m_MapDisplay.FocalPoint.GameWorldPosition).ToVector2XZ();
		return CalculateIconDirection(relativeVector2D, limitRange, maxRangeSqr, outVecLength, out iconPosition2D);
	}

	private bool CalculateIconDirection(Vector2 relativeVector2D, bool limitRange, float maxRangeSqr, float outVecLength, out Vector2 iconPosition)
	{
		if (!limitRange || relativeVector2D.sqrMagnitude < maxRangeSqr)
		{
			iconPosition = relativeVector2D.normalized * outVecLength;
			return true;
		}
		iconPosition = default(Vector2);
		return false;
	}

	protected bool CalculateIconPositionFromScene(Vector3 scenePos, bool limitRange, float maxRangeSqr, float poiRadiusWorld, out Vector2 iconPosition2D)
	{
		Vector2 relativeVector2D = (scenePos - m_MapDisplay.FocalPoint.ScenePosition).ToVector2XZ();
		return CalculateIconPosition(relativeVector2D, limitRange, maxRangeSqr, poiRadiusWorld, out iconPosition2D);
	}

	protected bool CalculateIconPositionFromWorld(Vector3 worldPos, out Vector2 iconPosition2D)
	{
		return CalculateIconPositionFromWorld(worldPos, limitRange: false, 0f, 0f, out iconPosition2D);
	}

	protected bool CalculateIconPositionFromWorld(Vector3 worldPos, bool limitRange, float maxRangeSqr, float poiRadiusWorld, out Vector2 iconPosition2D)
	{
		Vector2 relativeVector2D = (worldPos - m_MapDisplay.FocalPoint.GameWorldPosition).ToVector2XZ();
		return CalculateIconPosition(relativeVector2D, limitRange, maxRangeSqr, poiRadiusWorld, out iconPosition2D);
	}

	protected bool CalculateIconPosition(Vector2 relativeVector2D, bool limitRange, float maxRangeSqr, float poiRadiusWorld, out Vector2 iconPosition2D)
	{
		bool flag = false;
		iconPosition2D = relativeVector2D;
		bool flag2 = !limitRange;
		Vector2 vector = Vector2.zero;
		if (!flag2)
		{
			vector = relativeVector2D - Vector2.one * poiRadiusWorld;
		}
		if (flag2 || vector.sqrMagnitude < maxRangeSqr)
		{
			flag = flag2 || !m_MapDisplay.CentredOnPlayer || !m_MapDisplay.LimitDisplayRange || vector.sqrMagnitude < m_MapDisplay.MaxDisplayRangeSqr;
			if (flag)
			{
				Vector2 vector2 = (m_MapDisplay.CentredOnPlayer ? relativeVector2D : (relativeVector2D + m_MapDisplay.FocalPoint.GameWorldPosition.ToVector2XZ()));
				vector2 *= m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel;
				iconPosition2D = vector2;
			}
		}
		return flag;
	}

	protected Vector3 GetIconPos3D(Vector2 iconPos2D, ManRadar.IconType iconType)
	{
		float priority = Singleton.Manager<ManRadar>.inst.GetPriority(iconType);
		return iconPos2D.ToVector3XY(priority);
	}

	private void OnPool()
	{
		m_RectTrans = GetComponent<RectTransform>();
	}

	private void OnRecycle()
	{
		m_MapDisplay = null;
	}
}
