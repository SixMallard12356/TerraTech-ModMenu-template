#define UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UISkinsPaletteController))]
public class UISkinsPaletteHUD : UIHUDElement
{
	[SerializeField]
	private ScrollRect m_ItemListScrollRect;

	[SerializeField]
	private GameObject m_SkinButtonTemplate;

	[SerializeField]
	private ToggleGroup m_SkinButtonParent;

	[SerializeField]
	private GameObject m_CorpButtonTemplate;

	[SerializeField]
	private ToggleGroup m_CorpButtonParent;

	[SerializeField]
	private Image m_PreviewImage;

	[SerializeField]
	private Image m_PreviewImageCorpIcon;

	[SerializeField]
	private TMP_Text m_PreviewCorpText;

	[SerializeField]
	private TMP_Text m_PreviewSkinText;

	[SerializeField]
	private Toggle m_PaintModeButton;

	[SerializeField]
	private Toggle m_FillModeButton;

	[SerializeField]
	private Toggle m_GrabModeButton;

	[SerializeField]
	private GameObject m_DLCSkinsButton;

	[SerializeField]
	private GameObject m_ModdedCorpsSection;

	[SerializeField]
	private GameObject m_ModdedCorpsFoldout;

	[SerializeField]
	private UICustomSkinCorpButton m_ModdedCorpButtonPrefab;

	[SerializeField]
	private GameObject m_ModdedCorpFilterHolder;

	[SerializeField]
	private string m_AnimatorOpenBoolParamName = "IsOpen";

	[SerializeField]
	private string m_AnimatorClosedStateName = "Close";

	private UISkinsPaletteController m_SkinsController;

	private List<Transform> m_CurrentSkinButtons;

	private Dictionary<FactionSubTypes, Transform> m_CurrentCorpButtons;

	private Animator m_Animator;

	private bool m_Hiding;

	private object m_HidingContext;

	private int m_NumModdedCorps;

	private UISkinsPaletteController.SkinEditType m_LastEditType = UISkinsPaletteController.SkinEditType.Fill;

	public override void Show(object context)
	{
		base.Show(context);
		if (m_SkinsController.IsNotNull())
		{
			foreach (KeyValuePair<FactionSubTypes, Transform> currentCorpButton in m_CurrentCorpButtons)
			{
				currentCorpButton.Value.gameObject.SetActive(m_SkinsController.ShowCorpInUI(currentCorpButton.Key));
			}
			m_SkinsController.SetSkinEditType(m_LastEditType);
		}
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Open);
		if (m_Animator.IsNotNull())
		{
			m_Animator.SetBool(m_AnimatorOpenBoolParamName, value: true);
		}
		if (m_SkinsController.IsNotNull())
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(m_SkinsController, uiModeEnable: true, UIInputMode.UISkinsPalettePanel);
			EventSystem.current.SetSelectedGameObject(m_SkinsController.gameObject);
			m_SkinsController.SetCurrentSelectedCorp(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
		}
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Subscribe(OnDLCChanged);
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Close);
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Unsubscribe(OnDLCChanged);
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetSkinEditType(UISkinsPaletteController.SkinEditType.Off);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(m_SkinsController, uiModeEnable: false, UIInputMode.UISkinsPalettePanel);
		}
		if (m_Animator.IsNotNull())
		{
			m_Animator.SetBool(m_AnimatorOpenBoolParamName, value: false);
			m_Hiding = true;
			m_HidingContext = context;
		}
		else
		{
			base.Hide(context);
		}
	}

	private void OnDLCChanged()
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetCurrentSelectedCorp(Singleton.Manager<ManCustomSkins>.inst.GetCurrentSelectedCorp());
		}
	}

	public void SetupSkinButtons(List<CorporationSkinUIInfo> skins)
	{
		RecycleButtons(m_CurrentSkinButtons);
		m_CurrentSkinButtons.Clear();
		for (int i = 0; i < skins.Count; i++)
		{
			Transform transform = m_SkinButtonTemplate.transform.Spawn(m_SkinButtonParent.transform);
			transform.localPosition = Vector3.zero;
			transform.localScale = Vector3.one;
			UICustomSkinButton component = transform.GetComponent<UICustomSkinButton>();
			component.SetupButton(i, skins[i].m_SkinButtonImage, skins[i].m_IsModded);
			component.SkinButtonClickedEvent.Subscribe(OnSkinButtonClicked);
			transform.gameObject.SetActive(!skins[i].m_SkinLocked);
			m_CurrentSkinButtons.Add(transform);
			Toggle component2 = transform.GetComponent<Toggle>();
			if (component2.IsNotNull())
			{
				component2.group = m_SkinButtonParent;
			}
		}
		if (m_DLCSkinsButton != null && Singleton.Manager<ManDLC>.inst.SupportsStore())
		{
			Transform transform2 = m_DLCSkinsButton.transform.Spawn(m_SkinButtonParent.transform);
			transform2.localPosition = Vector3.zero;
			transform2.localScale = Vector3.one;
			Button componentInChildren = transform2.GetComponentInChildren<Button>();
			if ((bool)componentInChildren)
			{
				componentInChildren.onClick.AddListener(OnButtonBuySkinClicked);
			}
			Toggle component3 = transform2.GetComponent<Toggle>();
			if (component3.IsNotNull())
			{
				component3.group = m_SkinButtonParent;
			}
			m_CurrentSkinButtons.Add(transform2);
		}
	}

	public void SetSelectedSkin(CorporationSkinUIInfo info, FactionSubTypes corp)
	{
		m_PreviewImage.sprite = info.m_PreviewImage;
		m_PreviewImageCorpIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(corp);
		m_CurrentCorpButtons[corp].GetComponent<UICustomSkinCorpButton>().SetButtonImage(info.m_SkinButtonImage);
		m_PreviewCorpText.text = StringLookup.GetCorporationName(corp);
		m_PreviewSkinText.text = ((info.m_LocalisedString != null) ? info.m_LocalisedString.Value : info.m_FallbackString);
	}

	public void SelectCorpButton(FactionSubTypes corp)
	{
		foreach (KeyValuePair<FactionSubTypes, Transform> currentCorpButton in m_CurrentCorpButtons)
		{
			Toggle component = currentCorpButton.Value.GetComponent<Toggle>();
			if ((bool)component)
			{
				component.isOn = false;
			}
		}
		Toggle component2 = m_CurrentCorpButtons[corp].GetComponent<Toggle>();
		if ((bool)component2)
		{
			component2.isOn = true;
		}
	}

	public void SelectSkinButton(int skinIndex, bool selectBuyButton)
	{
		foreach (Transform currentSkinButton in m_CurrentSkinButtons)
		{
			Toggle component = currentSkinButton.GetComponent<Toggle>();
			if ((bool)component)
			{
				component.isOn = false;
			}
		}
		int index = (selectBuyButton ? (m_CurrentSkinButtons.Count - 1) : skinIndex);
		Toggle component2 = m_CurrentSkinButtons[index].GetComponent<Toggle>();
		if ((bool)component2)
		{
			component2.isOn = true;
		}
	}

	public void TryShowSkinButton(int skinIndex, bool selectBuyButton)
	{
		int num = (selectBuyButton ? (m_CurrentSkinButtons.Count - 1) : skinIndex);
		if (num >= 0 && num < m_CurrentSkinButtons.Count)
		{
			Vector2 anchoredPosition = m_ItemListScrollRect.content.anchoredPosition;
			float height = m_ItemListScrollRect.viewport.rect.height;
			RectTransform rectTransform = m_CurrentSkinButtons[num] as RectTransform;
			float num2 = rectTransform.anchoredPosition.y - rectTransform.rect.position.y;
			float num3 = num2 - rectTransform.rect.height;
			float num4 = 0f - anchoredPosition.y;
			float num5 = num4 - height;
			bool flag = false;
			if (num2 > num4)
			{
				anchoredPosition.y = 0f - num2;
				flag = true;
			}
			else if (num3 < num5)
			{
				anchoredPosition.y = 0f - num3 - height;
				flag = true;
			}
			if (flag)
			{
				m_ItemListScrollRect.content.anchoredPosition = anchoredPosition;
			}
		}
		else
		{
			d.LogError($"Invalid select index {num}, only had {m_CurrentSkinButtons.Count} buttons!");
		}
	}

	public void ToggleModdedCorpsPanel()
	{
		if (m_NumModdedCorps > 0)
		{
			m_ModdedCorpsFoldout.SetActive(!m_ModdedCorpsFoldout.activeSelf);
		}
	}

	private void RecycleButtons(IEnumerable<Transform> buttons)
	{
		foreach (Transform button in buttons)
		{
			UICustomSkinCorpButton component = button.GetComponent<UICustomSkinCorpButton>();
			if ((bool)component)
			{
				component.CorpButtonClickedEvent.Unsubscribe(OnCorpButtonClicked);
			}
			UICustomSkinButton component2 = button.GetComponent<UICustomSkinButton>();
			if ((bool)component2)
			{
				component2.SkinButtonClickedEvent.Unsubscribe(OnSkinButtonClicked);
			}
			Button componentInChildren = button.GetComponentInChildren<Button>();
			if ((bool)componentInChildren)
			{
				componentInChildren.onClick.RemoveAllListeners();
			}
			button.Recycle();
		}
	}

	private void OnSkinButtonClicked(int skinIndex)
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetSelectedSkinForCurrentCorp(skinIndex);
		}
	}

	private void OnCorpButtonClicked(FactionSubTypes corp)
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetCurrentSelectedCorp(corp);
		}
	}

	public void OnButtonClickedFloodFill()
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetSkinEditType(UISkinsPaletteController.SkinEditType.Fill);
			m_LastEditType = UISkinsPaletteController.SkinEditType.Fill;
		}
	}

	public void OnButtonClickedPaintModeEnable()
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetSkinEditType(UISkinsPaletteController.SkinEditType.Paint);
			m_LastEditType = UISkinsPaletteController.SkinEditType.Paint;
		}
	}

	public void OnButtonClickedPaintModeDisable()
	{
		if (m_SkinsController.IsNotNull())
		{
			m_SkinsController.SetSkinEditType(UISkinsPaletteController.SkinEditType.Off);
			m_LastEditType = UISkinsPaletteController.SkinEditType.Off;
		}
	}

	public void OnButtonClickedBack()
	{
		HideSelf();
	}

	public void OnButtonBuySkinClicked()
	{
		Singleton.Manager<ManDLC>.inst.OpenStoreToDLCPageWithNotification();
	}

	private void OnPool()
	{
		m_CurrentSkinButtons = new List<Transform>();
		m_CurrentCorpButtons = new Dictionary<FactionSubTypes, Transform>(default(FactionSubTypesComparer));
		m_SkinsController = GetComponent<UISkinsPaletteController>();
		if (m_SkinsController.IsNull())
		{
			d.LogAssertion("Must have a UISkinsController component to match UISkinsPaletteHUD!");
		}
		m_Animator = GetComponent<Animator>();
		if (!m_Animator.IsNotNull())
		{
			d.LogError("No animator found in UISkinsPaletteHUD!");
		}
	}

	private void OnSpawn()
	{
		m_NumModdedCorps = 0;
		foreach (int allCorpID in Singleton.Manager<ManLicenses>.inst.GetAllCorpIDs())
		{
			if (Singleton.Manager<ManMods>.inst.IsModdedCorp((FactionSubTypes)allCorpID))
			{
				if (!(m_ModdedCorpButtonPrefab != null))
				{
					continue;
				}
				ModdedCorpDefinition corpDefinition = Singleton.Manager<ManMods>.inst.GetCorpDefinition((FactionSubTypes)allCorpID);
				if (corpDefinition != null)
				{
					m_NumModdedCorps++;
					Transform transform = m_ModdedCorpButtonPrefab.transform.Spawn(m_ModdedCorpFilterHolder.transform);
					UICustomSkinCorpButton component = transform.GetComponent<UICustomSkinCorpButton>();
					transform.localPosition = transform.localPosition.SetZ(0f);
					transform.localScale = Vector3.one;
					m_CurrentCorpButtons.Add((FactionSubTypes)allCorpID, transform);
					component.SetupButton((FactionSubTypes)allCorpID, null, Sprite.Create(corpDefinition.m_Icon, new Rect(0f, 0f, corpDefinition.m_Icon.width, corpDefinition.m_Icon.height), Vector2.zero));
					component.CorpButtonClickedEvent.Subscribe(OnCorpButtonClicked);
					Toggle component2 = component.GetComponent<Toggle>();
					if (component2.IsNotNull())
					{
						component2.group = m_CorpButtonParent;
					}
				}
				continue;
			}
			Transform transform2 = m_CorpButtonTemplate.transform.Spawn(m_CorpButtonParent.transform);
			transform2.localPosition = Vector3.zero;
			transform2.localScale = Vector3.one;
			m_CurrentCorpButtons.Add((FactionSubTypes)allCorpID, transform2);
			UICustomSkinCorpButton component3 = transform2.GetComponent<UICustomSkinCorpButton>();
			component3.SetupButton((FactionSubTypes)allCorpID, null, Singleton.Manager<ManUI>.inst.GetModernCorpIcon((FactionSubTypes)allCorpID));
			component3.CorpButtonClickedEvent.Subscribe(OnCorpButtonClicked);
			Toggle component4 = transform2.GetComponent<Toggle>();
			if (component4.IsNotNull())
			{
				component4.group = m_CorpButtonParent;
				if (allCorpID == 1)
				{
					component4.isOn = true;
				}
			}
		}
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			AddElementToGroup(ManHUD.HUDGroup.Main);
		}
		if (m_ModdedCorpsSection != null)
		{
			m_ModdedCorpsSection.SetActive(m_NumModdedCorps > 0);
		}
		else
		{
			d.LogWarning("Modded corps buttons missing");
		}
		if (m_ModdedCorpsFoldout != null)
		{
			m_ModdedCorpsFoldout.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		RecycleButtons(m_CurrentSkinButtons);
		RecycleButtons(m_CurrentCorpButtons.Values);
		m_CurrentSkinButtons.Clear();
		m_CurrentCorpButtons.Clear();
	}

	private void Update()
	{
		m_PaintModeButton.isOn = Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintSkin;
		m_FillModeButton.isOn = Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintSkinTech;
		m_GrabModeButton.isOn = Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.Grab;
		if (m_Hiding && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(m_AnimatorClosedStateName))
		{
			if (m_SkinsController.IsNotNull())
			{
				m_SkinsController.SetSkinEditType(UISkinsPaletteController.SkinEditType.Off);
			}
			m_Hiding = false;
			base.Hide(m_HidingContext);
		}
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_SkinsController.IsNotNull() && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
			if (uIMPChat.IsNull() || !uIMPChat.HasFocus())
			{
				EventSystem.current.SetSelectedGameObject(m_SkinsController.gameObject);
			}
		}
	}
}
