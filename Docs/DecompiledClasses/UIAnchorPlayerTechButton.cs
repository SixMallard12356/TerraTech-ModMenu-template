#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIAnchorPlayerTechButton : MonoBehaviour
{
	public enum TooltipReason
	{
		Nil,
		Anchor,
		Unanchor,
		EnemiesNearby
	}

	[SerializeField]
	private Selectable m_Selectable;

	[SerializeField]
	private Image m_ButtonImage;

	[SerializeField]
	private TooltipComponent m_Tooltip;

	[SerializeField]
	private Sprite m_AnchoredSprite;

	[SerializeField]
	private Sprite m_UnanchoredSprite;

	[SerializeField]
	private LocalisedString m_AnchoredTooltip;

	[SerializeField]
	private LocalisedString m_UnanchoredTooltip;

	[SerializeField]
	private LocalisedString m_EnemiesNearbyTooltip;

	[SerializeField]
	private bool m_DisableWhenNotAvailable = true;

	[SerializeField]
	private bool m_ShouldShowTooltipForHotkey;

	private TooltipReason m_CurrentTooltipContext;

	private float m_ForceActiveTimer;

	public bool CanAnchor { get; private set; }

	public void UpdateButton(bool forceUpdate = false)
	{
		UpdateButton(Singleton.playerTank, forceUpdate);
	}

	public bool TryToggleTechAnchor()
	{
		bool result = false;
		Tank playerTank = Singleton.playerTank;
		if (playerTank != null)
		{
			result = playerTank.TrySetAnchored(!playerTank.IsAnchored);
		}
		return result;
	}

	private void UpdateButton(Tank tech, bool forceUpdate)
	{
		if (tech != null)
		{
			bool isAnchored = tech.IsAnchored;
			bool flag = tech.Anchors.NumPossibleAnchors > 0;
			bool flag2 = false;
			TooltipReason tooltipContext;
			if (isAnchored)
			{
				tooltipContext = TooltipReason.Unanchor;
				flag2 = true;
			}
			else if (flag)
			{
				if (Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(tech.boundsCentreWorld, Globals.inst.m_TechStoreThreatDistance))
				{
					tooltipContext = TooltipReason.Anchor;
					flag2 = true;
				}
				else
				{
					tooltipContext = TooltipReason.EnemiesNearby;
					flag2 = false;
				}
			}
			else
			{
				tooltipContext = TooltipReason.Anchor;
				flag2 = false;
			}
			UpdateButton(isAnchored, flag2, tooltipContext, forceUpdate);
		}
		else
		{
			UpdateButton(anchored: false, enabled: false, TooltipReason.Anchor, forceUpdate);
		}
	}

	private void UpdateButton(bool anchored, bool enabled, TooltipReason tooltipContext, bool forceUpdate)
	{
		m_ButtonImage.sprite = (anchored ? m_UnanchoredSprite : m_AnchoredSprite);
		if (m_DisableWhenNotAvailable)
		{
			m_Selectable.interactable = enabled;
		}
		CanAnchor = enabled;
		UpdateTooltip(tooltipContext, forceUpdate);
	}

	private void UpdateTooltip(TooltipReason context, bool forceUpdate = false)
	{
		if (!(m_Tooltip == null) && (context != m_CurrentTooltipContext || forceUpdate))
		{
			m_CurrentTooltipContext = context;
			string text = string.Empty;
			UITooltipOptions mode = UITooltipOptions.Default;
			switch (context)
			{
			case TooltipReason.Anchor:
				text = m_AnchoredTooltip.Value;
				mode = UITooltipOptions.Default;
				break;
			case TooltipReason.Unanchor:
				text = m_UnanchoredTooltip.Value;
				mode = UITooltipOptions.Default;
				break;
			case TooltipReason.EnemiesNearby:
				text = m_EnemiesNearbyTooltip.Value;
				mode = UITooltipOptions.Warning;
				break;
			default:
				d.LogWarning("UIAnchorPlayerTechButton.UpdateTooltip - No tooltip specified for " + context);
				break;
			case TooltipReason.Nil:
				break;
			}
			m_Tooltip.SetText(text);
			m_Tooltip.SetMode(mode);
		}
	}

	private void OnButtonPressed()
	{
		TryToggleTechAnchor();
	}

	private void OnLanguageChanged()
	{
		UpdateTooltip(m_CurrentTooltipContext, forceUpdate: true);
	}

	private void OnPlayerFailedToAnchor()
	{
		if (base.gameObject.activeInHierarchy && m_ShouldShowTooltipForHotkey)
		{
			UpdateTooltip(TooltipReason.EnemiesNearby, forceUpdate: true);
			m_Tooltip.SetForceDisplay(active: true);
			m_ForceActiveTimer = 2f;
		}
	}

	private void OnSpawn()
	{
		Button button = m_Selectable as Button;
		if (button != null)
		{
			button.onClick.AddListener(OnButtonPressed);
		}
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
		Singleton.Manager<ManPlayer>.inst.OnPlayerFailedToAnchorWithEnemiesNearby.Subscribe(OnPlayerFailedToAnchor);
	}

	private void Update()
	{
		if (m_ForceActiveTimer > 0f)
		{
			m_ForceActiveTimer -= Time.deltaTime;
			if (m_ForceActiveTimer <= 0f)
			{
				m_Tooltip.SetForceDisplay(active: false);
			}
		}
		UpdateButton();
	}

	private void OnRecycle()
	{
		Button button = m_Selectable as Button;
		if (button != null)
		{
			button.onClick.RemoveAllListeners();
		}
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChanged);
		Singleton.Manager<ManPlayer>.inst.OnPlayerFailedToAnchorWithEnemiesNearby.Unsubscribe(OnPlayerFailedToAnchor);
	}
}
