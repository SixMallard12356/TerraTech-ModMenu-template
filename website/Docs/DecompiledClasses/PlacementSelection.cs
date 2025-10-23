using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlacementSelection : MonoBehaviour
{
	public delegate bool ValidatorFunc(Vector3 position, Quaternion orientation, float radius, out InvalidResult invalidResult);

	public enum HiddenReason
	{
		OverUI,
		NoTarget
	}

	public enum InvalidType
	{
		General,
		Swap,
		Place
	}

	public enum InvalidReason
	{
		Valid = 0,
		NoIventory = 1,
		EnemiesNearby = 2,
		CurrentlyLoading = 4,
		PositionBlocked = 8,
		TooFarAway = 0x10,
		BlockLimited = 0x20,
		OutOfBounds = 0x40,
		ExceedsBuildLimit = 0x80
	}

	public struct InvalidResult
	{
		public InvalidType m_Type;

		public InvalidReason m_Flags;

		public bool IsValid => (m_Flags & (InvalidReason)(-1)) == 0;
	}

	[SerializeField]
	private Transform m_Sphere;

	[SerializeField]
	private float m_PlacementDirectionRotateSpeed = 180f;

	[SerializeField]
	private Transform m_PlacementForwardMarker;

	[Tooltip("Used to rotate projectors")]
	[SerializeField]
	private Transform m_ProjectorParent;

	[SerializeField]
	private Spinner m_Spinner;

	[SerializeField]
	private Projector m_ValidProjector;

	[SerializeField]
	private Projector m_InvalidProjector;

	[SerializeField]
	private LocalisedString m_DefaultTooltip;

	[SerializeField]
	private LocalisedString m_EnemiesNearbyTooltip;

	[SerializeField]
	private LocalisedString m_PositionBlockedTooltip;

	[SerializeField]
	private LocalisedString m_CurrentlyLoadingTooltip;

	[SerializeField]
	private LocalisedString m_TooFarAwayTooltip;

	[SerializeField]
	private LocalisedString m_OutOfBoundsTooltip;

	[SerializeField]
	private LocalisedString m_ExceedsBuildLimitTooltip;

	public Event<Vector3, Quaternion> PlacementSelectedEvent;

	public EventNoParams PlacementCancelledEvent;

	public ValidatorFunc CheckPlacementValid;

	private Transform m_Trans;

	private Material m_Mat;

	private bool m_Blocked;

	private float m_Radius;

	private InvalidResult m_InvalidResult;

	private Bitfield<HiddenReason> m_HiddenReasonBitfield = new Bitfield<HiddenReason>();

	public void AddRotationIncrement(bool clockwise = true)
	{
		float angle = m_PlacementDirectionRotateSpeed * Time.deltaTime * (float)(clockwise ? 1 : (-1));
		m_Trans.Rotate(Vector3.up, angle);
	}

	public void SetRadius(float radius)
	{
		m_ValidProjector.orthographicSize = radius;
		m_InvalidProjector.orthographicSize = radius;
		float num = radius * 2f;
		m_Sphere.localScale = new Vector3(num, num, num);
		m_Radius = radius;
	}

	public void SetCallbacks(Action<Vector3, Quaternion> selectionCallback, Action cancelledCallback, ValidatorFunc validityCheck)
	{
		ClearCallbacks();
		PlacementSelectedEvent.Subscribe(selectionCallback);
		PlacementCancelledEvent.Subscribe(cancelledCallback);
		CheckPlacementValid = validityCheck;
	}

	public void ClearCallbacks()
	{
		PlacementSelectedEvent.Clear();
		PlacementCancelledEvent.Clear();
		CheckPlacementValid = null;
	}

	public void HiddenViaUI(bool hidden)
	{
		if (hidden)
		{
			m_HiddenReasonBitfield.Add(0);
		}
		else
		{
			m_HiddenReasonBitfield.Remove(0);
		}
		RefreshVisibleState();
	}

	public string GetFailReasonText(InvalidReason tooltipReason)
	{
		string result = "";
		switch (tooltipReason)
		{
		case InvalidReason.Valid:
			result = m_DefaultTooltip.Value;
			break;
		case InvalidReason.EnemiesNearby:
			result = m_EnemiesNearbyTooltip.Value;
			break;
		case InvalidReason.PositionBlocked:
			result = m_PositionBlockedTooltip.Value;
			break;
		case InvalidReason.CurrentlyLoading:
			result = m_CurrentlyLoadingTooltip.Value;
			break;
		case InvalidReason.TooFarAway:
			result = m_TooFarAwayTooltip.Value;
			break;
		case InvalidReason.OutOfBounds:
			result = m_OutOfBoundsTooltip.Value;
			break;
		case InvalidReason.ExceedsBuildLimit:
			result = m_ExceedsBuildLimitTooltip.Value;
			break;
		}
		return result;
	}

	private void SetColour(bool blocked)
	{
		Color value = (blocked ? Color.red : Color.green);
		value.a = 0.5f;
		m_Mat.SetColor("_Color", value);
	}

	private void RefreshVisibleState()
	{
		bool isNull = m_HiddenReasonBitfield.IsNull;
		base.transform.gameObject.SetActive(isNull);
		if (!isNull)
		{
			Singleton.Manager<ManOverlay>.inst.RemoveToolTip();
		}
	}

	private void OnPlacement(bool placed, bool targetValid, bool cancelled)
	{
		if (cancelled)
		{
			PlacementCancelledEvent.Send();
			return;
		}
		if (targetValid)
		{
			m_HiddenReasonBitfield.Remove(1);
		}
		else
		{
			m_HiddenReasonBitfield.Add(1);
		}
		RefreshVisibleState();
		Vector3 targetPosition = Singleton.Manager<ManPointer>.inst.targetPosition;
		Quaternion rotation = m_Trans.rotation;
		if (m_PlacementForwardMarker.IsNotNull())
		{
			Vector3 scenePos = targetPosition;
			if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos, out var outNormal))
			{
				Vector3 forward = Vector3.Cross(m_Trans.TransformDirection(Vector3.right), outNormal);
				m_PlacementForwardMarker.rotation = Quaternion.LookRotation(forward, outNormal);
			}
			else
			{
				m_PlacementForwardMarker.localRotation = Quaternion.identity;
			}
		}
		if (!targetValid)
		{
			return;
		}
		m_Blocked = !base.transform.gameObject.activeInHierarchy;
		if (!m_Blocked && CheckPlacementValid != null)
		{
			m_Blocked = !CheckPlacementValid(targetPosition, rotation, m_Radius, out m_InvalidResult);
		}
		if (m_Spinner != null)
		{
			m_Spinner.SetAutoSpin(!m_Blocked);
		}
		if (m_ValidProjector != null)
		{
			m_ValidProjector.gameObject.SetActive(!m_Blocked);
		}
		if (m_InvalidProjector != null)
		{
			m_InvalidProjector.gameObject.SetActive(m_Blocked);
		}
		SetColour(m_Blocked);
		if (base.transform.gameObject.activeInHierarchy)
		{
			string failReasonText = GetFailReasonText(m_InvalidResult.m_Flags);
			UITooltipOptions mode = (m_Blocked ? UITooltipOptions.Warning : UITooltipOptions.Default);
			UITooltipAlignment alignment = UITooltipAlignment.BottomLeft;
			UITooltipNew.TooltipInfo tooltipInfo = new UITooltipNew.TooltipInfo(failReasonText, mode, alignment, targetPosition);
			Singleton.Manager<ManOverlay>.inst.AddToolTip(tooltipInfo);
		}
		if (placed)
		{
			if (!m_Blocked)
			{
				PlacementSelectedEvent.Send(targetPosition, rotation);
			}
		}
		else
		{
			m_Trans.SetPositionIfChanged(targetPosition);
		}
	}

	private void OnPool()
	{
		m_Trans = base.transform;
		m_Mat = m_Sphere.GetComponent<MeshRenderer>().material;
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManPointer>.inst.OnPlacingEvent.Subscribe(OnPlacement);
		SetRadius(1f);
		m_HiddenReasonBitfield.Clear();
		m_Blocked = false;
		if (QualitySettings.shadows == ShadowQuality.HardOnly)
		{
			m_Sphere.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
		}
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManPointer>.inst.OnPlacingEvent.Unsubscribe(OnPlacement);
		Singleton.Manager<ManOverlay>.inst.RemoveToolTip();
	}
}
