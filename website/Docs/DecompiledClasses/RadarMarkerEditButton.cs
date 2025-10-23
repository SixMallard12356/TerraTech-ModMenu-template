#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadarMarkerEditButton : MonoBehaviour
{
	public enum EditingType
	{
		Color,
		Icon
	}

	[Tooltip("The value this selection represents in the enumeration (eg. defaultColor = 0, green = 1,...)")]
	[SerializeField]
	protected int RepresentedValue;

	[SerializeField]
	protected Image AssociatedImage;

	[SerializeField]
	[Tooltip("Which type this button is editting")]
	protected EditingType EditType;

	[SerializeField]
	protected Image SelectedFrame;

	[SerializeField]
	protected Image HilightedFrame;

	public Event<int> OnSelectedOptionUpdated;

	public int EnumerationValue => RepresentedValue;

	public Button Button { get; set; }

	public void UI_SelectOption()
	{
		OnSelectedOptionUpdated.Send(RepresentedValue);
	}

	public void SetOptionSelected(bool state)
	{
		SelectedFrame.gameObject.SetActive(state);
	}

	public void SetHilighted(bool state)
	{
		HilightedFrame.gameObject.SetActive(state);
	}

	private void Awake()
	{
		Button = GetComponent<Button>();
		SetAssociatedImage();
	}

	private void SetAssociatedImage()
	{
		if (AssociatedImage == null && EditType == EditingType.Color)
		{
			d.LogWarning("Edit RadarMarker UI: Associated Image not assigned to RadarMarkerEditButton, not fatal but it should be set in order to display correct color/icon");
		}
		else if (EditType == EditingType.Color)
		{
			AssociatedImage.color = Singleton.Manager<ManRadar>.inst.GetRadarMarkerColor((ManRadar.RadarMarkerColorType)RepresentedValue).SetAlpha(1f);
		}
	}
}
