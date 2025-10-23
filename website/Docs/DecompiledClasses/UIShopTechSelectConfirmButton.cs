#define UNITY_EDITOR
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIShopTechSelectConfirmButton : MonoBehaviour
{
	public enum States
	{
		Disabled,
		Active,
		Loading
	}

	public enum TooltipReason
	{
		Nil,
		Default,
		MissingBlocks,
		EnemiesNearby
	}

	[SerializeField]
	private Button m_Button;

	[SerializeField]
	private Image m_ButtonImage;

	[SerializeField]
	private Color m_ActiveColor;

	[SerializeField]
	private Color m_DisabledColor;

	[SerializeField]
	private Text m_ButtonText;

	[SerializeField]
	private LocalisedString m_ActiveStr;

	[SerializeField]
	private LocalisedString m_LoadingStr;

	[SerializeField]
	private LocalisedString m_TooltipDefault;

	[SerializeField]
	private LocalisedString m_TooltipMissingBlocks;

	[SerializeField]
	private LocalisedString m_TooltipEnemiesNearby;

	public EventNoParams OnClick;

	private StateMachine<States> m_StateMachine;

	private TooltipReason m_TooltipContext;

	private TooltipReason m_PrevTooltipContext;

	private TooltipComponent m_TooltipComponent;

	public void SetInteractable(bool isInteractable, TooltipReason context = TooltipReason.Default)
	{
		switch (m_StateMachine.State)
		{
		case States.Disabled:
			m_TooltipContext = context;
			if (isInteractable)
			{
				m_StateMachine.ChangeState(States.Active);
			}
			break;
		case States.Active:
		case States.Loading:
			m_TooltipContext = TooltipReason.Default;
			if (!isInteractable)
			{
				m_StateMachine.ChangeState(States.Disabled);
			}
			break;
		default:
			d.LogErrorFormat("UIShopTechSelectConfirmButton.SetInteractable: no case provided for state " + m_StateMachine.State);
			break;
		}
	}

	private void UpdateTooltip()
	{
		if (m_PrevTooltipContext != m_TooltipContext)
		{
			string text = string.Empty;
			UITooltipOptions mode = UITooltipOptions.Default;
			switch (m_TooltipContext)
			{
			case TooltipReason.Default:
				text = m_TooltipDefault.Value;
				mode = UITooltipOptions.Default;
				break;
			case TooltipReason.MissingBlocks:
				text = m_TooltipMissingBlocks.Value;
				mode = UITooltipOptions.Warning;
				break;
			case TooltipReason.EnemiesNearby:
				text = m_TooltipEnemiesNearby.Value;
				mode = UITooltipOptions.Warning;
				break;
			default:
				d.LogErrorFormat("UIShopTechSelectConfirmButton.UpdateTooltip: no case provided for context " + m_TooltipContext);
				break;
			}
			m_TooltipComponent.SetText(text);
			m_TooltipComponent.SetMode(mode);
			m_PrevTooltipContext = m_TooltipContext;
		}
	}

	private void Disabled_Enter()
	{
		m_Button.interactable = false;
		m_ButtonImage.color = m_DisabledColor;
		m_ButtonText.text = m_ActiveStr.Value;
		UpdateTooltip();
	}

	private void Disabled_Update()
	{
		UpdateTooltip();
	}

	private void Active_Enter()
	{
		m_Button.interactable = true;
		m_ButtonImage.color = m_ActiveColor;
		m_ButtonText.text = m_ActiveStr.Value;
		m_Button.onClick.AddListener(Active_OnButtonClicked);
		UpdateTooltip();
	}

	private void Active_OnButtonClicked()
	{
		OnClick.Send();
		m_StateMachine.ChangeState(States.Loading);
	}

	private void Active_Exit()
	{
		m_Button.onClick.RemoveListener(Active_OnButtonClicked);
	}

	private void Loading_Enter()
	{
		m_Button.interactable = false;
		m_ButtonImage.color = m_DisabledColor;
		m_ButtonText.text = m_LoadingStr.Value;
	}

	private void Loading_Update()
	{
		if (!Singleton.Manager<ManPurchases>.inst.IsLoadingTechs)
		{
			m_StateMachine.ChangeState(States.Active);
		}
	}

	private void OnPool()
	{
		m_StateMachine = StateMachine<States>.Initialize(this);
		m_TooltipComponent = GetComponent<TooltipComponent>();
	}

	private void OnSpawn()
	{
		m_StateMachine.ChangeState(States.Disabled);
	}
}
