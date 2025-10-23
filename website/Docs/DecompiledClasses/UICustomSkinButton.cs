#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Toggle))]
public class UICustomSkinButton : MonoBehaviour
{
	[SerializeField]
	private Image m_ButtonImage;

	[SerializeField]
	private Toggle m_Toggle;

	[SerializeField]
	private Image m_ModdedIcon;

	public Event<int> SkinButtonClickedEvent;

	private int m_SkinIndex;

	public void OnValueChanged(bool on)
	{
		if (on)
		{
			SkinButtonClickedEvent.Send(m_SkinIndex);
		}
	}

	public void SetupButton(int skinIndex, Sprite image, bool isModded)
	{
		m_SkinIndex = skinIndex;
		m_ButtonImage.sprite = image;
		m_ModdedIcon.gameObject.SetActive(isModded);
	}

	private void OnSpawn()
	{
		d.Assert(m_ButtonImage.IsNotNull());
		d.Assert(m_Toggle.IsNotNull());
		m_Toggle.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnRecycle()
	{
		m_Toggle.onValueChanged.RemoveListener(OnValueChanged);
		SkinButtonClickedEvent.EnsureNoSubscribers();
	}
}
