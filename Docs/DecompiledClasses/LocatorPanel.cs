using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class LocatorPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ManHUD.Focussable
{
	private enum BlendTarget
	{
		None,
		TopScreenEdge,
		BottomScreenEdge
	}

	[SerializeField]
	private GameObject m_NamesPanel;

	[SerializeField]
	private Text m_TopName;

	[SerializeField]
	private Text m_BottomName;

	[SerializeField]
	private Image m_FactionIcon;

	[SerializeField]
	private Animator m_Animator;

	[SerializeField]
	[FormerlySerializedAs("m_Waypoint")]
	private Image m_Pin;

	[SerializeField]
	private RectTransform m_WaypointMarkerEnd;

	[SerializeField]
	private float m_ScreenEdgeSize = 0.15f;

	[SerializeField]
	private Image m_SpeakerIcon;

	[SerializeField]
	private TextMeshProUGUI m_DistanceText;

	private RectTransform m_Rect;

	private RectTransform m_PinRectTrans;

	private RectTransform m_CanvasRectTransform;

	private Rect m_CanvasRect;

	private readonly int m_AnimatorTargetingStatusHash = Animator.StringToHash("Targeting_Status");

	private int m_DistanceShown;

	public string TopName
	{
		set
		{
			if ((bool)m_TopName)
			{
				m_TopName.text = value;
			}
		}
	}

	public string BottomName
	{
		set
		{
			if ((bool)m_BottomName)
			{
				m_BottomName.text = value;
			}
		}
	}

	public Color BottomColor
	{
		set
		{
			if ((bool)m_BottomName)
			{
				m_BottomName.color = value;
			}
		}
	}

	public bool PointerInside { get; private set; }

	public void ShowSpeakerIcon(bool b)
	{
		if (m_SpeakerIcon != null && m_SpeakerIcon.gameObject.activeSelf != b)
		{
			m_SpeakerIcon.gameObject.SetActive(b);
		}
	}

	public void SetNamesPanelVisible(bool visible)
	{
		if ((bool)m_NamesPanel && m_NamesPanel.activeSelf != visible)
		{
			m_NamesPanel.SetActive(visible);
		}
	}

	public void SetDistance(int distance, bool show)
	{
		m_DistanceText.gameObject.SetActive(show);
		if (show && distance != m_DistanceShown)
		{
			m_DistanceText.text = GameUnits.GetDistanceText(distance);
			m_DistanceShown = distance;
		}
	}

	public void Format(Sprite iconSprite, Color iconColour, Sprite pinSprite, Color pinColour, TechWeapon.ManualTargetingReticuleState manualTargetingReticuleState)
	{
		m_FactionIcon.sprite = iconSprite;
		m_FactionIcon.color = iconColour;
		m_FactionIcon.gameObject.SetActive(iconSprite != null);
		m_Pin.sprite = pinSprite;
		m_Pin.color = pinColour;
		m_Animator.SetInteger(m_AnimatorTargetingStatusHash, (int)manualTargetingReticuleState);
	}

	public bool PointToWorldPosition(Vector3 worldPos)
	{
		Vector2 localPos;
		float zPos;
		bool result = WorldToLocalPositionClamped(worldPos, out localPos, out zPos);
		Vector2 vector = Rect.PointToNormalized(m_CanvasRect, localPos);
		float num = Vector2.Angle(new Vector2(0f, -1f), vector - new Vector2(0.5f, 0.5f));
		float num2 = ((vector.x >= 0.5f) ? num : (0f - num));
		float a = ((vector.x < 0.5f) ? vector.x : (1f - vector.x));
		float b = ((vector.y < 0.5f) ? vector.y : (1f - vector.y));
		float value = Mathf.Min(a, b);
		float num3 = (m_ScreenEdgeSize - Mathf.Clamp(value, 0f, m_ScreenEdgeSize)) / Mathf.Max(m_ScreenEdgeSize, 0.001f);
		float z = num2 * num3;
		m_PinRectTrans.localRotation = Quaternion.Euler(0f, 0f, z);
		Vector3 position = m_WaypointMarkerEnd.position;
		Vector3 position2 = m_Rect.position;
		Vector3 vector2 = position - position2;
		Vector3 vector3 = m_Rect.InverseTransformVector(vector2);
		m_Rect.localPosition = new Vector3(localPos.x, localPos.y, Mathf.Max(zPos, 0f)) - vector3;
		return result;
	}

	private bool WorldToLocalPositionClamped(Vector3 worldPos, out Vector2 localPos, out float zPos)
	{
		zPos = Singleton.cameraTrans.forward.Dot(worldPos - Singleton.cameraTrans.position);
		localPos = UIHelpers.WorldToUILocalPosition(worldPos, Singleton.camera, Singleton.Manager<ManHUD>.inst.Canvas);
		int num;
		if (zPos >= 0f)
		{
			num = (m_CanvasRect.Contains(localPos) ? 1 : 0);
			if (num != 0)
			{
				goto IL_0086;
			}
		}
		else
		{
			num = 0;
		}
		localPos = GetEdgeOfScreenPosition(Singleton.cameraTrans.worldToLocalMatrix.MultiplyPoint(worldPos));
		goto IL_0086;
		IL_0086:
		return (byte)num != 0;
	}

	private Vector3 GetEdgeOfScreenPosition(Vector3 localPos)
	{
		Vector2 vector = localPos.ToVector2XY();
		vector.x *= m_CanvasRect.height / m_CanvasRect.width;
		if (Mathf.Abs(vector.x) <= Mathf.Epsilon && Mathf.Abs(vector.y) <= Mathf.Epsilon)
		{
			vector = new Vector2(0.5f, 0f);
		}
		float num = Mathf.Abs(vector.x);
		float num2 = Mathf.Abs(vector.y);
		float num3 = ((!(num2 >= num)) ? (0.5f / num) : (0.5f / num2));
		Vector2 normalizedRectCoordinates = new Vector2(0.5f, 0.5f) + num3 * vector;
		return Rect.NormalizedToPoint(m_CanvasRect, normalizedRectCoordinates);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PointerInside = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PointerInside = false;
	}

	private void OnPool()
	{
		m_Rect = GetComponent<RectTransform>();
		if (m_Pin != null)
		{
			m_PinRectTrans = m_Pin.GetComponent<RectTransform>();
		}
	}

	private void OnSpawn()
	{
		SetNamesPanelVisible(visible: false);
		TopName = "";
		BottomName = "";
		Format(null, Color.white, null, Color.white, TechWeapon.ManualTargetingReticuleState.NotTargeted);
		SetDistance(0, show: false);
		Singleton.Manager<ManHUD>.inst.AddOverlay(this);
		m_CanvasRectTransform = Singleton.Manager<ManHUD>.inst.Canvas.transform as RectTransform;
		m_CanvasRect = m_CanvasRectTransform.rect;
		if (m_SpeakerIcon != null)
		{
			m_SpeakerIcon.gameObject.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManHUD>.inst.RemoveOverlay(this);
	}

	public void SetFocusLevel(ManHUD.FocusLevel level)
	{
	}

	public Transform GetTransform()
	{
		return m_Rect;
	}
}
