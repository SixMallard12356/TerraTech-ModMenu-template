#define UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using DevCommands;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AdvertisingPanel : MonoBehaviour
{
	[Serializable]
	public struct BannerData
	{
		public class ImageConverter : JsonConverter
		{
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.Value == null)
				{
					return null;
				}
				byte[] data = Convert.FromBase64String((string)reader.Value);
				Texture2D texture2D = new Texture2D(1, 1);
				if (!texture2D.LoadImage(data))
				{
					d.LogError("Banner image read failure!");
				}
				return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero, 128f);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				byte[] value2 = ((Sprite)value).texture.EncodeToPNG();
				writer.WriteValue(value2);
			}

			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof(Texture2D);
			}
		}

		public string Name;

		[JsonConverter(typeof(ImageConverter))]
		public Sprite Image;

		public bool DebugOnly;

		public string URLToOpen;

		public bool AvailableOnConsole;

		public void Dispose()
		{
			UnityEngine.Object.Destroy(Image);
		}
	}

	[Tooltip("The URL to the Json file containing the live banners data")]
	[SerializeField]
	private string m_GetBannersWebRequestURI = "https://terratechpublicstorage.blob.core.windows.net/announcement-banners/banners.json";

	[SerializeField]
	private BannerData m_FallbackBannerData;

	[SerializeField]
	protected float m_BannerCycleDuration = 5f;

	[SerializeField]
	private GameObject m_BannerDisplay;

	[SerializeField]
	private Image m_BannerImageDisplay;

	[SerializeField]
	private RectTransform m_BannerIndexButtonContainer;

	[SerializeField]
	private AdvertisingPanelIndexButton m_IndexButtonPrefab;

	[SerializeField]
	[HideInInspector]
	private Button m_BannerButton;

	[HideInInspector]
	[SerializeField]
	private AdvertisingPanelData m_PanelData;

	[HideInInspector]
	[SerializeField]
	private TextMeshProUGUI m_TitleText;

	[HideInInspector]
	[SerializeField]
	private GameObject m_NewTag;

	protected static AdvertisingPanel inst;

	protected bool m_Active;

	protected bool m_MouseHovering;

	protected bool m_BannerSelected;

	protected bool m_ShowDebugBanners;

	protected float m_BannerCycleTimeRemaining;

	protected BannerData[] m_LoadedBannerData;

	protected bool m_HasBannerData;

	protected bool m_HasMultipleBanners;

	protected int m_CurrentBannerIndex = -1;

	protected bool m_UseFallback;

	protected AdvertisingPanelIndexButton[] m_IndexButtons;

	protected Vector2 m_IndexButtonDims;

	private const int k_WebRequestTimeoutSeconds = 5;

	private const string k_SaveDataFileName = "announcementBanners.bin";

	private IEnumerator BannersFromWebUpdater;

	private string SaveDataPath => ManSaveGame.GetSaveDataFolder() + "/announcementBanners.bin";

	private bool LiveFeedAvailableOnCurrentSku
	{
		get
		{
			if (SKU.GenericPCPlatform)
			{
				return !SKU.ConsoleUI;
			}
			return false;
		}
	}

	private bool StoreAvailableOnCurrentSku => Singleton.Manager<ManDLC>.inst.SupportsStore();

	private bool StoringAnnouncementsAvailableOnCurrentSku
	{
		get
		{
			if (SKU.GenericPCPlatform)
			{
				return !SKU.ConsoleUI;
			}
			return false;
		}
	}

	private bool HasSelectedBanner
	{
		get
		{
			if (m_HasBannerData)
			{
				return m_CurrentBannerIndex > -1;
			}
			return false;
		}
	}

	private BannerData CurrentBannerData
	{
		get
		{
			if (!HasSelectedBanner)
			{
				return default(BannerData);
			}
			return m_LoadedBannerData[m_CurrentBannerIndex];
		}
	}

	public void Init()
	{
		UISetElementFromSKU component = GetComponent<UISetElementFromSKU>();
		if (component.IsNotNull() && !component.IsAvailableInSKU())
		{
			EnableBanner(state: false);
			Debug.Log("Advertising panel hidden because it is being hidden in this SKU");
		}
		else if (m_LoadedBannerData == null)
		{
			TryUpdateBannersFromWeb();
			if (!SKU.AllowsExternalLinks)
			{
				m_BannerButton.SetNavigationMode(Navigation.Mode.None);
			}
		}
	}

	public void DeInit()
	{
		if (BannersFromWebUpdater != null)
		{
			StopCoroutine(BannersFromWebUpdater);
			BannersFromWebUpdater = null;
		}
	}

	[DevCommand(Name = "MainMenu.ShowDebugBanners", Access = Access.DevCheat)]
	private static void Debug_ShowDebugBanners(bool state)
	{
		inst.m_ShowDebugBanners = state;
		inst.DeInit();
		inst.m_LoadedBannerData = null;
		inst.Init();
	}

	public void UI_OnBannerClicked()
	{
		d.Assert(HasSelectedBanner, "Banner button has been clicked but no current banner has been set! We don't have a URL to go to!");
		if (SKU.AllowsExternalLinks && !string.IsNullOrEmpty(CurrentBannerData.URLToOpen))
		{
			Application.OpenURL(CurrentBannerData.URLToOpen);
		}
	}

	public void UI_OnBannerSelected(bool state)
	{
		m_BannerSelected = state;
	}

	public void UI_SetMouseHovering(bool state)
	{
		m_MouseHovering = state;
	}

	public void SelectBanner(int index)
	{
		m_BannerCycleTimeRemaining = m_BannerCycleDuration;
		if (!m_HasBannerData)
		{
			m_CurrentBannerIndex = -1;
			return;
		}
		m_CurrentBannerIndex = index;
		d.Assert(m_CurrentBannerIndex >= 0 && m_CurrentBannerIndex < m_LoadedBannerData.Length, "Trying to select advertising banner that lies outside of the range of available banners!");
		if (m_HasMultipleBanners)
		{
			for (int i = 0; i < m_IndexButtons.Length; i++)
			{
				m_IndexButtons[i].SetHighlighted(i == m_CurrentBannerIndex);
			}
		}
		RefreshBannerDisplay();
	}

	private void RefreshBannerDisplay()
	{
		m_BannerImageDisplay.sprite = CurrentBannerData.Image;
	}

	private void InitIndexButtons()
	{
		if (!m_HasMultipleBanners)
		{
			m_IndexButtons = null;
			return;
		}
		m_IndexButtons = new AdvertisingPanelIndexButton[m_LoadedBannerData.Length];
		float num = m_IndexButtonDims.x * (float)m_IndexButtons.Length;
		for (int i = 0; i < m_IndexButtons.Length; i++)
		{
			m_IndexButtons[i] = m_IndexButtonPrefab.Spawn(m_BannerIndexButtonContainer);
			m_IndexButtons[i].Init(this, i);
			RectTransform obj = m_IndexButtons[i].transform as RectTransform;
			obj.localScale = Vector3.one;
			float x = m_IndexButtonDims.x * (float)i - num / 2f + m_IndexButtonDims.x / 2f;
			obj.localPosition = new Vector3(x, 0f, 0f);
		}
	}

	private void ClearIndexButtons()
	{
		if (m_IndexButtons != null)
		{
			for (int i = 0; i < m_IndexButtons.Length; i++)
			{
				m_IndexButtons[i].Recycle();
			}
			m_IndexButtons = null;
		}
	}

	private void EnableBanner(bool state)
	{
		m_Active = state;
		m_BannerDisplay.SetActive(m_Active);
	}

	private void StoreCurrentBannerData()
	{
		if (StoringAnnouncementsAvailableOnCurrentSku)
		{
			JsonConvert.SerializeObject(m_LoadedBannerData).WriteToBinaryFile(SaveDataPath);
		}
	}

	private BannerData[] LoadBannerData()
	{
		if (!StoringAnnouncementsAvailableOnCurrentSku)
		{
			return null;
		}
		BannerData[] result = null;
		try
		{
			result = JsonConvert.DeserializeObject<BannerData[]>("".ReadFromBinaryFile(SaveDataPath));
		}
		catch
		{
			d.LogError("Failed to load stored banner data... Aborting...");
		}
		return result;
	}

	private void SetBannersFromStored()
	{
		BannerData[] array = LoadBannerData();
		if (array != null)
		{
			SetBanners(array);
		}
		else
		{
			SetBannersFromFallback();
		}
	}

	private void SetBannersFromFallback()
	{
		SetBanners(null);
	}

	private void SetBannersFromJson(string loadedBannersSerialized)
	{
		if (loadedBannersSerialized.NullOrEmpty())
		{
			d.LogError("Failed to load banners from Json, aborting...");
			SetBannersFromFallback();
			return;
		}
		try
		{
			SetBanners(JsonConvert.DeserializeObject<BannerData[]>(loadedBannersSerialized));
		}
		catch
		{
			d.LogError("Attempted to load banners from JSON data and despite our best efforts, we have failed... Time to abort in disgrace...");
			SetBannersFromFallback();
		}
	}

	private void SetBanners(BannerData[] banners)
	{
		if (m_LoadedBannerData != null)
		{
			for (int i = 0; i < m_LoadedBannerData.Length; i++)
			{
				m_LoadedBannerData[i].Dispose();
			}
			m_LoadedBannerData = null;
		}
		m_LoadedBannerData = banners?.Where((BannerData banner) => CanDisplay(banner)).ToArray();
		m_HasBannerData = m_LoadedBannerData != null && m_LoadedBannerData.Length != 0;
		if (!m_HasBannerData && m_UseFallback)
		{
			m_LoadedBannerData = new BannerData[1] { m_FallbackBannerData };
			m_HasBannerData = true;
		}
		m_HasMultipleBanners = m_LoadedBannerData != null && m_LoadedBannerData.Length > 1;
		ClearIndexButtons();
		InitIndexButtons();
		EnableBanner(m_HasBannerData);
		SelectBanner(0);
	}

	private bool CanDisplay(BannerData bannerData)
	{
		if (!m_ShowDebugBanners && bannerData.DebugOnly)
		{
			return false;
		}
		if (SKU.ConsoleUI && !bannerData.AvailableOnConsole)
		{
			return false;
		}
		if (bannerData.Image == null)
		{
			return false;
		}
		return true;
	}

	private void TryUpdateBannersFromWeb()
	{
		if (BannersFromWebUpdater != null)
		{
			StopCoroutine(BannersFromWebUpdater);
		}
		BannersFromWebUpdater = TryUpdateBannersFromWebCo();
		StartCoroutine(BannersFromWebUpdater);
	}

	private IEnumerator TryUpdateBannersFromWebCo()
	{
		EnableBanner(state: false);
		if (!LiveFeedAvailableOnCurrentSku)
		{
			SetBannersFromStored();
			yield break;
		}
		using UnityWebRequest webRequest = UnityWebRequest.Get(m_GetBannersWebRequestURI);
		webRequest.timeout = 5;
		yield return webRequest.SendWebRequest();
		if (webRequest.isNetworkError || webRequest.isHttpError)
		{
			SetBannersFromStored();
			yield break;
		}
		SetBannersFromJson(webRequest.downloadHandler.text);
		StoreCurrentBannerData();
	}

	private void PrePool()
	{
		m_BannerButton = GetComponentInChildren<Button>();
	}

	private void OnPool()
	{
		inst = this;
		m_UseFallback = CanDisplay(m_FallbackBannerData);
		if (!m_UseFallback)
		{
			d.Log("Invalid fallback banner on main menu, will remain hidding from players");
		}
		m_IndexButtonDims = (m_IndexButtonPrefab.transform as RectTransform).rect.size;
	}

	private void Update()
	{
		if (m_Active && m_HasMultipleBanners && !m_MouseHovering && !m_BannerSelected)
		{
			m_BannerCycleTimeRemaining -= Time.deltaTime;
			if (m_BannerCycleTimeRemaining < 0f)
			{
				m_BannerCycleTimeRemaining = m_BannerCycleDuration;
				int index = ((m_CurrentBannerIndex + 1 < m_LoadedBannerData.Length) ? (m_CurrentBannerIndex + 1) : 0);
				SelectBanner(index);
			}
		}
	}

	[Obsolete("The old method of setting the banners, directly from the built-in scriptable object data. We use BannerData now")]
	private void DisplayFallbackBanner()
	{
		if (!StoreAvailableOnCurrentSku)
		{
			Debug.Log("Advertising panel hidden because store not supported on this platform");
			EnableBanner(state: false);
			return;
		}
		if (m_PanelData == null)
		{
			EnableBanner(state: false);
			Debug.LogError("No panel data found for advertising panel");
		}
		int versionInt;
		bool num = SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out versionInt);
		long num2 = (long)m_PanelData.m_DaysActiveAfterBuild * 24L * 60 * 60;
		long num3 = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - versionInt;
		if (!num || num3 < num2)
		{
			EnableBanner(state: true);
			m_BannerImageDisplay.sprite = m_PanelData.m_BackgroundImage;
			m_TitleText.text = m_PanelData.m_TitleText.ToString();
			m_NewTag.SetActive(m_PanelData.m_ShowNew);
		}
		else
		{
			EnableBanner(state: false);
		}
	}
}
