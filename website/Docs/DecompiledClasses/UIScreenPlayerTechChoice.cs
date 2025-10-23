#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIScreenPlayerTechChoice : UIScreen
{
	[SerializeField]
	private Text m_MessageLabel;

	[SerializeField]
	[FormerlySerializedAs("m_PlayerDestroyedMessageHasRemainingTechs")]
	private LocalisedString m_MessageHasRemainingTechs;

	[FormerlySerializedAs("m_PlayerDestroyedMessageNoMoreTechs")]
	[SerializeField]
	private LocalisedString m_MessageNoMorePlayerTechs;

	[SerializeField]
	private IntVector2 m_TechImageSize = new IntVector2(268, 198);

	[Space(10f)]
	[SerializeField]
	private Transform m_FactionTechChoice;

	[SerializeField]
	private Button m_FactionTechButton;

	[SerializeField]
	private RawImage m_FactionTechImage;

	[SerializeField]
	private Text m_FactionTechName;

	[SerializeField]
	[Space(10f)]
	private Transform m_ExistingPlayerTechChoice;

	[SerializeField]
	private Button m_ExistingPlayerTechButton;

	[SerializeField]
	private RawImage m_ExistingPlayerTechImage;

	[SerializeField]
	[Space(10f)]
	private Button m_LastPlayerTechButton;

	[SerializeField]
	private RawImage m_LastPlayerTechImage;

	[SerializeField]
	private Text m_LastPlayerTechCostLabel;

	[SerializeField]
	private TooltipComponent m_LastPlayerTechTooltip;

	private TechData m_FactionTechData;

	private Tank m_ExistingPlayerTech;

	private PlayerTechRecoveryData m_LastPlayerTechRecoveryData;

	private GameObject m_DefaultSelectedJoypadElement;

	private Event<TechData> m_NewFactionTechSelectedEvent;

	private Event<Tank> m_ExistingTechSelectedEvent;

	private Event<PlayerTechRecoveryData> m_LastPlayerTechSelectedEvent;

	public Event<Tank> OnNearbyRespawnTechDestroyed;

	public IntVector2 TechImageSize => m_TechImageSize;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnMoneyChanged);
		if (m_ExistingPlayerTech != null)
		{
			m_ExistingPlayerTech.TankRecycledEvent.Unsubscribe(OnExistingPlayerTechDestroyed);
			m_ExistingPlayerTech.TankRecycledEvent.Subscribe(OnExistingPlayerTechDestroyed);
		}
		if (m_DefaultSelectedJoypadElement != null && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_DefaultSelectedJoypadElement);
			SelectButton(m_DefaultSelectedJoypadElement);
		}
	}

	public override void Hide()
	{
		base.Hide();
		if (m_DefaultSelectedJoypadElement != null)
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_DefaultSelectedJoypadElement);
		}
		BlockScreenExit(exitBlocked: false);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Unsubscribe(OnMoneyChanged);
		if (m_ExistingPlayerTech != null)
		{
			m_ExistingPlayerTech.TankRecycledEvent.Unsubscribe(OnExistingPlayerTechDestroyed);
		}
		ClearUI();
	}

	private void CheckEntryTargetForJoypad()
	{
		if (base.gameObject.activeInHierarchy && m_DefaultSelectedJoypadElement != null && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_DefaultSelectedJoypadElement);
			SelectButton(m_DefaultSelectedJoypadElement);
		}
	}

	public void SetFactionTechOption(TechData factionTechData, Texture2D image, string techName, Action<TechData> onSelectAction)
	{
		SetMessage(m_MessageNoMorePlayerTechs);
		m_FactionTechChoice.gameObject.SetActive(value: true);
		m_ExistingPlayerTechChoice.gameObject.SetActive(value: false);
		m_FactionTechData = factionTechData;
		d.Assert(m_ExistingPlayerTech == null, "UIScreenPlayerRespawnTechChoice.SetFactionTechOption - Already have m_ExistingPlayerTech set. Cannot have both!");
		SetImage(m_FactionTechImage, image);
		m_FactionTechName.text = techName;
		m_NewFactionTechSelectedEvent.Clear();
		m_NewFactionTechSelectedEvent.Subscribe(onSelectAction);
		if (m_DefaultSelectedJoypadElement == null)
		{
			m_DefaultSelectedJoypadElement = m_FactionTechButton.gameObject;
			CheckEntryTargetForJoypad();
		}
	}

	public void SetExistingPlayerTech(Tank playerControllableTech, Texture2D image, Action<Tank> onSelectAction)
	{
		SetMessage(m_MessageHasRemainingTechs);
		m_FactionTechChoice.gameObject.SetActive(value: false);
		m_ExistingPlayerTechChoice.gameObject.SetActive(value: true);
		m_ExistingPlayerTech = playerControllableTech;
		d.Assert(m_FactionTechData == null, "UIScreenPlayerRespawnTechChoice.SetExistingPlayerTech - Already have m_FactionTechPreset set. Cannot have both!");
		SetImage(m_ExistingPlayerTechImage, image);
		m_ExistingTechSelectedEvent.Clear();
		m_ExistingTechSelectedEvent.Subscribe(onSelectAction);
		if (m_DefaultSelectedJoypadElement == null)
		{
			m_DefaultSelectedJoypadElement = m_ExistingPlayerTechButton.gameObject;
			CheckEntryTargetForJoypad();
		}
	}

	public void SetLastPlayerTech(PlayerTechRecoveryData recoveryData, Texture2D image, Action<PlayerTechRecoveryData> onSelectAction)
	{
		m_LastPlayerTechRecoveryData = recoveryData;
		UpdateLastTechChoice();
		SetImage(m_LastPlayerTechImage, image);
		m_LastPlayerTechSelectedEvent.Clear();
		m_LastPlayerTechSelectedEvent.Subscribe(onSelectAction);
		if (m_LastPlayerTechButton.interactable)
		{
			if ((bool)m_DefaultSelectedJoypadElement)
			{
				Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_DefaultSelectedJoypadElement);
			}
			m_DefaultSelectedJoypadElement = m_LastPlayerTechButton.gameObject;
			CheckEntryTargetForJoypad();
		}
	}

	public void ClearExistingPlayerTech()
	{
		m_ExistingPlayerTechChoice.gameObject.SetActive(value: false);
		if (m_ExistingPlayerTech != null)
		{
			m_ExistingPlayerTech.TankRecycledEvent.Unsubscribe(OnExistingPlayerTechDestroyed);
		}
		m_ExistingPlayerTech = null;
		SetImage(m_ExistingPlayerTechImage, null);
		m_ExistingTechSelectedEvent.Clear();
	}

	private void SetMessage(LocalisedString text)
	{
		m_MessageLabel.text = text.Value;
	}

	private void SelectExistingPlayerTech()
	{
		m_ExistingTechSelectedEvent.Send(m_ExistingPlayerTech);
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void SelectFactionTech()
	{
		m_NewFactionTechSelectedEvent.Send(m_FactionTechData);
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void SelectLastPlayerTech()
	{
		m_LastPlayerTechSelectedEvent.Send(m_LastPlayerTechRecoveryData);
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void SetImage(RawImage imageUIElement, Texture2D image)
	{
		if (imageUIElement.texture != null)
		{
			UnityEngine.Object.DestroyImmediate(imageUIElement.texture);
			imageUIElement.texture = null;
		}
		imageUIElement.texture = image;
		m_LastPlayerTechImage.enabled = image != null;
		if (image != null)
		{
			Vector2 sizeDelta = imageUIElement.rectTransform.sizeDelta;
			float num = (float)image.width / (float)image.height;
			sizeDelta.x = sizeDelta.y * num;
			imageUIElement.rectTransform.sizeDelta = sizeDelta;
		}
	}

	private void OnMoneyChanged(int invalid)
	{
		UpdateLastTechChoice();
	}

	private void UpdateLastTechChoice()
	{
		if (m_LastPlayerTechRecoveryData != null)
		{
			string text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ChooseRespawnTech, 4), Singleton.Manager<Localisation>.inst.GetMoneyStringWithSymbol(m_LastPlayerTechRecoveryData.m_TotalRecoveryCost), m_LastPlayerTechRecoveryData.m_NumBlocksAvailableFromInventory);
			m_LastPlayerTechCostLabel.text = text;
			bool flag = Singleton.Manager<ManPlayer>.inst.CanAfford(m_LastPlayerTechRecoveryData.m_TotalRecoveryCost);
			m_LastPlayerTechButton.interactable = flag;
			if (m_LastPlayerTechTooltip != null)
			{
				string text2 = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ChooseRespawnTech, 5), Singleton.Manager<Localisation>.inst.GetMoneyStringWithSymbol(m_LastPlayerTechRecoveryData.m_BlockRecoveryCost), m_LastPlayerTechRecoveryData.m_NumBlocksRecoverable, m_LastPlayerTechRecoveryData.m_NumBlocksAvailableFromInventory, Singleton.Manager<Localisation>.inst.GetMoneyStringWithSymbol(m_LastPlayerTechRecoveryData.m_BlocksFromShopCost), m_LastPlayerTechRecoveryData.m_NumBlocksFromShop);
				m_LastPlayerTechTooltip.SetText(text2);
				m_LastPlayerTechTooltip.SetMode((!flag) ? UITooltipOptions.Warning : UITooltipOptions.Default);
			}
		}
	}

	private void ClearUI()
	{
	}

	private void OnExistingPlayerTechDestroyed(Tank destroyedTech)
	{
		d.Assert(destroyedTech == m_ExistingPlayerTech, "UIScreenPlayerTechChoice.OnExistingPlayerTechDestroyed - Missmatch between destroyed tech and set existing player tech.");
		ClearExistingPlayerTech();
		OnNearbyRespawnTechDestroyed.Send(destroyedTech);
	}

	private void OnPool()
	{
		ClearUI();
		m_FactionTechImage.texture = null;
		m_ExistingPlayerTechImage.texture = null;
		m_LastPlayerTechImage.texture = null;
	}

	private void Awake()
	{
		m_ExistingPlayerTechButton.onClick.AddListener(SelectExistingPlayerTech);
		m_FactionTechButton.onClick.AddListener(SelectFactionTech);
		m_LastPlayerTechButton.onClick.AddListener(SelectLastPlayerTech);
	}

	private void Update()
	{
		if (SKU.ConsoleUI)
		{
			if (EventSystem.current.currentSelectedGameObject == m_LastPlayerTechButton.gameObject)
			{
				m_LastPlayerTechTooltip.SetForceDisplay(active: true);
			}
			else
			{
				m_LastPlayerTechTooltip.SetForceDisplay(active: false);
			}
		}
	}
}
