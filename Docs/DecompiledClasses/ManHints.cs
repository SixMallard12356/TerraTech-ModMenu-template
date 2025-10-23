#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManHints : Singleton.Manager<ManHints>
{
	[Serializable]
	public class HintDefinition
	{
		[EnumString(typeof(GameHints.HintID))]
		public EnumString m_HintId = new EnumString(typeof(GameHints.HintID), 0);

		public LocalisedString m_HintMessage;

		public bool m_UseDifferentHintMessageForPad;

		public LocalisedString m_HintMessagePad;

		public UIHints.IconType m_HintIcon;
	}

	[SerializeField]
	private HintDefinitionList m_HintDefinitions;

	[Tooltip("Allow hints to be automatically hidden after a period of time")]
	[SerializeField]
	[Header("Hint Timeout (Auto-Hide After Time)")]
	private bool m_EnableHintTimeout;

	[Tooltip("The amount of time in seconds before a hint is automatically hidden")]
	[SerializeField]
	private float m_HintTimeoutTime = 60f;

	[SerializeField]
	private GameHints m_GameHints = new GameHints();

	private List<UIHintFloatingElement> m_FloatingHintQueue = new List<UIHintFloatingElement>();

	private UIHintFloating m_HintFloatingContainer;

	private bool m_GameplayHintsActive;

	public bool HintsEnabled
	{
		get
		{
			if (m_GameplayHintsActive && Singleton.Manager<ManProfile>.inst.GetCurrentUser() != null && Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_ShowGameplayHints)
			{
				return !Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
			}
			return false;
		}
	}

	public bool EnableHintTimeout => m_EnableHintTimeout;

	public float HintTimeoutTime => m_HintTimeoutTime;

	public void ShowHint(GameHints.HintID hintId, bool forceShowHint = false)
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && (HintsEnabled || forceShowHint))
		{
			d.Assert(hintId != GameHints.HintID.Invalid, "ManHints.ShowHint - HintId passed in is Invalid!");
			HintDefinition hintDefinition = m_HintDefinitions.GetHintDefinition(hintId);
			if (hintDefinition != null)
			{
				UIHints.ShowContext showContext = new UIHints.ShowContext
				{
					hintID = hintId,
					hintDef = hintDefinition
				};
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Hint, showContext);
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().SetHintSeen(hintId);
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Hint);
			}
			else
			{
				d.LogError(string.Concat("ManHints.ShowHint - Could not find a definition for the hint by ID: '", hintId, "'!"));
			}
		}
	}

	public void HideHint(GameHints.HintID hintId)
	{
		d.Assert(hintId != GameHints.HintID.Invalid, "ManHints.HideHint - HintId passed in is Invalid!");
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Hint, hintId);
	}

	public bool IsShowingHint(GameHints.HintID hintId)
	{
		d.Assert(hintId != GameHints.HintID.Invalid, "ManHints.HideHint - HintId passed in is Invalid!");
		UIHints uIHints = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Hint) as UIHints;
		if (uIHints != null)
		{
			return uIHints.IsShowingHint(hintId);
		}
		return false;
	}

	public bool IsAnyHintOnScreen()
	{
		UIHints uIHints = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Hint) as UIHints;
		if (uIHints != null)
		{
			return uIHints.IsAnyHintOnScreen();
		}
		return false;
	}

	public void SetHintsEnabled(bool enable)
	{
		m_GameHints.EnableHints(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && enable);
	}

	public void HintFloatContainerReady(UIHintFloating container)
	{
		m_HintFloatingContainer = container;
		for (int i = 0; i < m_FloatingHintQueue.Count; i++)
		{
			UIHintFloatingElement hint = m_FloatingHintQueue[i];
			m_HintFloatingContainer.RegisterChildAsHint(hint);
		}
		m_FloatingHintQueue.Clear();
	}

	public void AddHintToFloatContainer(UIHintFloatingElement hint)
	{
		if (m_HintFloatingContainer == null)
		{
			m_FloatingHintQueue.Add(hint);
		}
		else
		{
			m_HintFloatingContainer.RegisterChildAsHint(hint);
		}
	}

	public void HintFloatContainerExpired(UIHintFloating uIHintFloating)
	{
		m_HintFloatingContainer = null;
		m_FloatingHintQueue.Clear();
	}

	private void InitialiseUserSettings(ManProfile.Profile activeProfile)
	{
		if (activeProfile != null && m_GameplayHintsActive)
		{
			SetHintsEnabled(activeProfile.m_GameplaySettings.m_ShowGameplayHints);
		}
	}

	private void OnModeSetup(Mode newMode)
	{
		ManHUD.HUDType defaultHUDType = newMode.GetDefaultHUDType();
		if (defaultHUDType == ManHUD.HUDType.MainGame || defaultHUDType == ManHUD.HUDType.Sumo)
		{
			m_GameplayHintsActive = true;
			if (Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_ShowGameplayHints)
			{
				SetHintsEnabled(enable: true);
			}
		}
	}

	private void OnModeCleanup(Mode modeToCleanup)
	{
		m_GameplayHintsActive = false;
		SetHintsEnabled(enable: false);
	}

	private void Start()
	{
		if (m_GameHints != null)
		{
			Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(OnModeSetup);
			Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
			Singleton.Manager<ManProfile>.inst.OnUserChanged.Subscribe(InitialiseUserSettings);
			Singleton.Manager<ManProfile>.inst.OnProfileSaved.Subscribe(InitialiseUserSettings);
		}
	}

	private void Update()
	{
		if (HintsEnabled && m_GameHints != null)
		{
			m_GameHints.UpdateHints();
		}
	}
}
