#define UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageTypeDisplay : MonoBehaviour
{
	protected enum DisplayType
	{
		Either,
		DamageType,
		DamageableType
	}

	protected enum LayoutBehaviourWhenHidden
	{
		DisplayAsEmpty,
		HideContent,
		CollapseElement
	}

	[SerializeField]
	protected TextMeshProUGUI m_NameText;

	[SerializeField]
	protected Image m_IconDisplay;

	[SerializeField]
	private TooltipComponent m_Tooltip;

	[SerializeField]
	protected DisplayType m_DisplayType;

	[SerializeField]
	protected LayoutBehaviourWhenHidden m_BehaviourWhenUnused;

	protected CanvasGroup m_CanvGroup;

	public void Hide()
	{
		Show(state: false);
	}

	private void Show(bool state)
	{
		switch (m_BehaviourWhenUnused)
		{
		case LayoutBehaviourWhenHidden.HideContent:
			if (state != (m_CanvGroup.alpha == 1f))
			{
				m_CanvGroup.alpha = (state ? 1f : 0f);
			}
			return;
		case LayoutBehaviourWhenHidden.CollapseElement:
			base.gameObject.SetActive(state);
			return;
		}
		m_IconDisplay.enabled = state;
		if (m_NameText != null && !state)
		{
			m_NameText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Purchasing.BlockDamageDisplayNone);
		}
	}

	public void SetBlock(BlockTypes blockType)
	{
		if (m_DisplayType == DisplayType.Either)
		{
			d.LogError("Attempted to display block damage/damageable type for block " + blockType.ToString() + " but the damage type display is set to ambiguous! This should not be the case when setting block values! Aborting!");
		}
		else if (m_DisplayType == DisplayType.DamageType)
		{
			ManDamage.DamageType damageType;
			bool hasDamageType = Singleton.Manager<ManDamage>.inst.TryGetBlockDamageType(blockType, out damageType);
			Set(damageType, hasDamageType);
		}
		else
		{
			Set(Singleton.Manager<ManDamage>.inst.GetBlockDamageableType(blockType));
		}
	}

	public void Set(ManDamage.DamageableType damageableType)
	{
		Show(state: true);
		Set(StringLookup.GetDamageableTypeName(damageableType), Singleton.Manager<ManUI>.inst.GetDamageableTypeIcon(damageableType));
	}

	public void Set(ManDamage.DamageType damageType, bool hasDamageType)
	{
		Set(StringLookup.GetDamageTypeName(damageType), Singleton.Manager<ManUI>.inst.GetDamageTypeIcon(damageType));
		Show(hasDamageType);
	}

	private void Set(string displayName, Sprite icon)
	{
		if (m_NameText != null)
		{
			m_NameText.text = displayName;
		}
		if (m_IconDisplay != null)
		{
			m_IconDisplay.sprite = icon;
		}
		if (m_Tooltip != null)
		{
			m_Tooltip.SetText(displayName);
		}
	}

	private void Awake()
	{
		m_CanvGroup = GetComponent<CanvasGroup>();
	}

	private void OnValidate()
	{
		if (m_BehaviourWhenUnused == LayoutBehaviourWhenHidden.HideContent)
		{
			d.Assert(GetComponent<CanvasGroup>() != null, "Must have a CanvasGroup component in order to use LayoutBehaviour.HideContent!");
		}
	}
}
