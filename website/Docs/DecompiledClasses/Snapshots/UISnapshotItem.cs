using UnityEngine;
using UnityEngine.UI;

namespace Snapshots;

public class UISnapshotItem : MonoBehaviour, UISnapshotPanel.ISelectable
{
	[SerializeField]
	private Text m_VehicleName;

	[SerializeField]
	private Image m_SpriteImage;

	[SerializeField]
	private GameObject m_UnavailableBanner;

	[SerializeField]
	private GameObject m_UnavailableForSwapIcon;

	[SerializeField]
	private GameObject m_UnavailableForPlaceIcon;

	[SerializeField]
	private GameObject m_WarningIcon;

	[SerializeField]
	private Text m_Creator;

	[SerializeField]
	private RectTransform m_ReferenceSizeTransform;

	[SerializeField]
	private Toggle m_Toggle;

	[SerializeField]
	private Transform m_FavouriteIcon;

	[SerializeField]
	private RectTransform m_LoadingIconPrefab;

	public Event<SnapshotLiveData> OnToggledTrue;

	private SnapshotLiveData m_SnapshotData;

	private Vector2 m_InitialDeltaSize;

	private Transform m_LoadingIcon;

	public void SetData(SnapshotLiveData snapshotData)
	{
		Snapshot snapshot = m_SnapshotData.m_Snapshot;
		Snapshot snapshot2 = snapshotData.m_Snapshot;
		if (snapshot2 != snapshot)
		{
			Clear();
			m_SnapshotData = snapshotData;
			m_SpriteImage.sprite = null;
			m_SpriteImage.enabled = false;
			if (m_LoadingIconPrefab != null)
			{
				m_LoadingIcon = m_LoadingIconPrefab.Spawn(base.transform);
			}
			snapshotData.m_Snapshot.ResolveThumbnail(OnResolveThumbnail);
			if (m_VehicleName != null && snapshot2.techData != null)
			{
				m_VehicleName.text = snapshot2.techData.Name;
			}
			if (m_Creator != null)
			{
				m_Creator.text = GetCreator();
			}
			if (m_FavouriteIcon != null)
			{
				snapshot2.m_Meta.Bind(OnMetadataChanged);
			}
			if (snapshotData.m_ValidData != null)
			{
				snapshotData.m_ValidData.m_UnavailableSwap.Bind(OnSwapUnavailableChanged);
				snapshotData.m_ValidData.m_UnavailablePlace.Bind(OnPlaceUnavailableChanged);
			}
		}
	}

	public void SetSelected(bool isSelected)
	{
		if (m_Toggle != null)
		{
			m_Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
			m_Toggle.isOn = isSelected;
			m_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
		}
	}

	public void SetButtonGroup(ToggleGroup buttonToggleGroup)
	{
		if (m_Toggle != null)
		{
			m_Toggle.group = buttonToggleGroup;
		}
	}

	public SnapshotLiveData GetData()
	{
		return m_SnapshotData;
	}

	public void Clear()
	{
		if (m_SnapshotData.m_Snapshot != null)
		{
			if (m_FavouriteIcon != null)
			{
				m_SnapshotData.m_Snapshot.m_Meta.Unbind(OnMetadataChanged);
			}
			if (m_SnapshotData.m_ValidData != null)
			{
				m_SnapshotData.m_ValidData.m_UnavailableSwap.Unbind(OnSwapUnavailableChanged);
				m_SnapshotData.m_ValidData.m_UnavailablePlace.Unbind(OnPlaceUnavailableChanged);
			}
		}
		m_SnapshotData.m_Snapshot = null;
		m_SpriteImage.sprite = null;
		m_SpriteImage.enabled = false;
		if (m_VehicleName != null)
		{
			m_VehicleName.text = string.Empty;
		}
		if (m_Creator != null)
		{
			m_Creator.text = string.Empty;
		}
		CleanupLoadingIcon();
	}

	private string GetCreator()
	{
		if (m_SnapshotData.m_Snapshot != null && !m_SnapshotData.m_Snapshot.creator.NullOrEmpty())
		{
			return "@" + m_SnapshotData.m_Snapshot.creator;
		}
		return string.Empty;
	}

	private void UpdateUnavailableBanner()
	{
		if (m_UnavailableBanner != null)
		{
			bool active = m_SnapshotData.m_ValidData.UnavailablePlace && m_SnapshotData.m_ValidData.UnavailableSwap;
			m_UnavailableBanner.SetActive(active);
		}
	}

	private void CleanupLoadingIcon()
	{
		if (m_LoadingIcon != null)
		{
			m_LoadingIcon.SetParent(null, worldPositionStays: false);
			m_LoadingIcon.Recycle();
			m_LoadingIcon = null;
		}
	}

	private void OnResolveThumbnail(Sprite sprite)
	{
		m_SpriteImage.sprite = sprite;
		m_SpriteImage.preserveAspect = true;
		m_SpriteImage.enabled = true;
		CleanupLoadingIcon();
	}

	private void OnToggleValueChanged(bool isOn)
	{
		if (isOn)
		{
			OnToggledTrue.Send(m_SnapshotData);
		}
	}

	private void OnMetadataChanged(Snapshot.MetaData metadata)
	{
		m_FavouriteIcon.gameObject.SetActive(metadata.IsFavourite);
	}

	private void OnSwapUnavailableChanged(bool swapUnavailable)
	{
		if (m_UnavailableForSwapIcon != null)
		{
			m_UnavailableForSwapIcon.SetActive(swapUnavailable);
		}
		UpdateUnavailableBanner();
	}

	private void OnPlaceUnavailableChanged(bool placeUnavailable)
	{
		if (m_UnavailableForPlaceIcon != null)
		{
			m_UnavailableForPlaceIcon.SetActive(placeUnavailable);
		}
		if (m_WarningIcon != null)
		{
			bool flag = Globals.inst.m_AllowPlaceTechWithMissingBlocks && m_SnapshotData.m_ValidData.HasMissingBlocksPlace;
			bool flag2 = Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory);
			m_WarningIcon.SetActive(!placeUnavailable && flag && !flag2);
		}
		UpdateUnavailableBanner();
	}

	private void OnRecycle()
	{
		Clear();
	}

	private void Update()
	{
		if (m_SnapshotData.m_Snapshot == null || !(m_ReferenceSizeTransform != null) || !(m_InitialDeltaSize != m_ReferenceSizeTransform.rect.size))
		{
			return;
		}
		m_InitialDeltaSize = m_ReferenceSizeTransform.rect.size;
		m_SnapshotData.m_Snapshot.ResolveThumbnail(delegate(Sprite sprite)
		{
			float num = Mathf.Min(m_InitialDeltaSize.x / (float)sprite.texture.width, m_InitialDeltaSize.y / (float)sprite.texture.height);
			if (sprite != null)
			{
				m_SpriteImage.rectTransform.sizeDelta = new Vector2((float)sprite.texture.width * num, (float)sprite.texture.height * num);
				m_SpriteImage.rectTransform.position = m_ReferenceSizeTransform.position;
			}
		});
	}

	private void OnEnable()
	{
		if (m_Toggle != null)
		{
			m_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
		}
	}

	private void OnDisable()
	{
		m_InitialDeltaSize = Vector2.zero;
		if (m_Toggle != null)
		{
			m_Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}
	}
}
