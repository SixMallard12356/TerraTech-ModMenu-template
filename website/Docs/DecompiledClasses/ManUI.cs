#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ManUI : Singleton.Manager<ManUI>
{
	public enum PauseType
	{
		None,
		Pause
	}

	public enum ScreenType
	{
		Null,
		Fabricator,
		DeliveryCannon,
		SumoRankedAccept,
		SumoRankedVS,
		SumoRankedResults,
		SumoRankedChampion,
		MainMenu,
		SelectChallengeMenu,
		LoadVehiclesMenu,
		Refinery,
		RacingTrackSelect,
		FlyingTrackSelect,
		BaseHelper,
		SelectLoadOption,
		Options,
		Pause,
		SumoVersus,
		SumoSelectMode,
		OptionsSound,
		OptionsControls,
		OptionsGraphics,
		OptionsCamera,
		ExitConfirmMenu,
		SumoRankedLoadOrDesign,
		SumoLoadVehicles,
		SumoRankedEnemies,
		DefenseOver,
		NameVehicle,
		TwitterWaitScreen,
		TwitchScreenAccept,
		TwitterAuth,
		TwitterInfo,
		SendInvader,
		UserSelect,
		HumbleUpdateNotification,
		NotificationScreen,
		ForumRegister,
		Shop,
		Multiplayer,
		JoinMultiplayer,
		SumoDesignOptions,
		SumoVersusResult,
		BugReport,
		CanaryLogin,
		StartNew,
		LoadSave,
		About,
		PauseChallenge,
		EnterName,
		LeaderBoard,
		GauntletAttract,
		GauntletReplay,
		NewGame,
		NewUser,
		RespawnTechChoice,
		InvaderSentTechChoice,
		WhatIsSnapshot,
		WhatIsTwitter,
		WhatIsGrabIt,
		RenameTech,
		SumoRankedMenu,
		SaveGame,
		SaveGameRename,
		SteamUpload,
		WhatIsSteam,
		MultiplayerSetupTEMP,
		MatchmakingLobbyList,
		MatchmakingLobbyScreen,
		TechLoaderScreen,
		MultiplayerTechSelect,
		ProfanityWarningScreen,
		UserEngagement,
		ControllerDisconnected,
		MultiplayerScoreboard,
		NotificationInput,
		ControlSchema,
		RenameSchema,
		NewMultiplayerScreen,
		CoopCreativeScreen,
		RenameTech_MarkerBlock,
		BannedPlayerList,
		NotificationMultiselect
	}

	private struct StackEntry
	{
		public UIScreen screen;

		public PauseType pauseType;

		public bool isPopup;
	}

	public enum LockTimerTypes
	{
		SendToSCU
	}

	[SerializeField]
	private Canvas m_MainCanvas;

	[SerializeField]
	private Canvas m_TooltipCanvas;

	[SerializeField]
	private UIHighlight m_HighlightPrefab;

	[SerializeField]
	private CursorDataTable m_CursorDataTable;

	[SerializeField]
	private Text m_FpsText;

	[HideInInspector]
	public UIScreen[] m_NewScreenPrefabList;

	public Transform m_MainCanvasTrans;

	public RectTransform m_ActiveUI;

	public RectTransform m_InactiveUI;

	public SpriteFetcher m_SpriteFetcher;

	public Camera m_UICamera;

	public Image m_FadeScreen;

	public Image m_FlashScreen;

	public Image m_InputBlocker;

	public Image m_BorderScreen;

	public Image m_ResizeScreen;

	public Vector3 m_ReferenceResolution = new Vector2(1920f, 1080f);

	[NonSerialized]
	public float m_ReferenceAspect = 1.7777778f;

	[NonSerialized]
	public Vector3 m_HalfReferenceResolution = new Vector2(960f, 540f);

	[SerializeField]
	private GameObject m_GetBetaButton;

	[SerializeField]
	private ScreenBtnPrompts m_ScreenBtnPromptsPrefab;

	private ScreenBtnPrompts m_ScreenBtnPrompts;

	[SerializeField]
	private Canvas m_RescaledMainCanvas;

	[SerializeField]
	private RawImage m_RescaledMainView;

	public const float kDefaultFadeTime = 3f;

	public Dictionary<string, bool> m_InteractionLocks;

	public Action<bool, ScreenType> OnScreenChangeEvent;

	private bool m_ForceFront;

	private bool m_HoldFade;

	private float m_FadeTarget;

	private float m_FadeTime;

	private float m_CurrentFade;

	private float m_FadeStartTime;

	private float m_AspectChangeRatio;

	private int m_PauseCount;

	private bool m_HasInitialised;

	private GameObject m_animContainer;

	private bool m_DisableScreenChange;

	private static bool s_HidingScreen;

	private float[] m_LockTimers = new float[Enum.GetNames(typeof(LockTimerTypes)).Length];

	private List<string> m_ErrorPopupQueue = new List<string>();

	private UIScreen[] m_Screens;

	private List<StackEntry> m_ScreenStack = new List<StackEntry>();

	private UIHighlight m_TutorialHighlight;

	private Transform m_TutorialHighlightedTransform;

	private RenderTexture m_OffscreenRenderTarget;

	private float m_OffscreenTargetScaling = 1f;

	public Canvas TooltipCanvas => m_TooltipCanvas;

	public bool HasInitialised => m_HasInitialised;

	public Text FpsText => m_FpsText;

	public bool ResizeScreenActive { get; set; }

	public CursorDataTable CursorDataTable => m_CursorDataTable;

	public void ShowTutorialHighlight(RectTransform transToHighlight)
	{
		if (m_TutorialHighlightedTransform != transToHighlight)
		{
			HideTutorialHighlight();
			if (transToHighlight != null)
			{
				m_TutorialHighlight = m_HighlightPrefab.Spawn(transToHighlight);
				RectTransform obj = m_TutorialHighlight.transform as RectTransform;
				obj.localScale = Vector3.one;
				obj.anchoredPosition3D = Vector3.zero;
				m_TutorialHighlight.SetSize(transToHighlight.rect);
				m_TutorialHighlightedTransform = transToHighlight;
			}
		}
	}

	public void HideTutorialHighlight()
	{
		if ((bool)m_TutorialHighlight)
		{
			m_TutorialHighlight.transform.SetParent(null, worldPositionStays: false);
			m_TutorialHighlight.Recycle();
			m_TutorialHighlight = null;
			m_TutorialHighlightedTransform = null;
		}
	}

	public Sprite GetSprite(ItemTypeInfo itemTypeInfo)
	{
		return m_SpriteFetcher.GetSprite(itemTypeInfo);
	}

	public Sprite GetSprite(ObjectTypes objectType, int itemType)
	{
		return m_SpriteFetcher.GetSprite(objectType, itemType);
	}

	public void SetSprite(ItemTypeInfo info, Texture2D texture)
	{
		m_SpriteFetcher.SetSprite(info, texture);
	}

	public Sprite GetCorpIcon(FactionSubTypes faction)
	{
		return m_SpriteFetcher.GetCorpIcon(faction);
	}

	public Sprite GetSelectedCorpIcon(FactionSubTypes faction)
	{
		return m_SpriteFetcher.GetSelectedCorpIcon(faction);
	}

	public Sprite GetModernCorpIcon(FactionSubTypes corp)
	{
		return m_SpriteFetcher.GetModernCorpIcon(corp);
	}

	public Sprite GetAICategoryIcon(AICategories aiCategory)
	{
		return m_SpriteFetcher.GetAICategoryIcon(aiCategory);
	}

	public Sprite GetBlockCatIcon(BlockCategories blockCat)
	{
		return m_SpriteFetcher.GetBlockCatIcon(blockCat);
	}

	public Sprite GetBlockAttributeIcon(BlockAttributes blockAttr)
	{
		return m_SpriteFetcher.GetBlockAttributeIcon(blockAttr);
	}

	public Sprite GetBlockRarityIcon(BlockRarity blockRarity)
	{
		return m_SpriteFetcher.GetBlockRarity(blockRarity);
	}

	public Sprite GetDamageTypeIcon(ManDamage.DamageType damageType)
	{
		return m_SpriteFetcher.GetDamageTypeIcon(damageType);
	}

	public Sprite GetDamageableTypeIcon(ManDamage.DamageableType damageableType)
	{
		return m_SpriteFetcher.GetDamageableTypeIcon(damageableType);
	}

	public Sprite GetChunkCategoryIcon(ChunkCategory chunkCat)
	{
		return m_SpriteFetcher.GetChunkCategoryIcon(chunkCat);
	}

	public Sprite GetChunkRarityIcon(ChunkRarity chunkRarity)
	{
		return m_SpriteFetcher.GetChunkRarity(chunkRarity);
	}

	public Sprite GetUndoTypeIcon(UndoTypes undoType)
	{
		return m_SpriteFetcher.GetUndoType(undoType);
	}

	public void EnableGetBetaButton()
	{
		m_GetBetaButton.SetActive(value: true);
	}

	public void PushScreen(UIScreen screen, PauseType pauseType = PauseType.None, bool asPopup = false)
	{
		if (m_DisableScreenChange)
		{
			if (IsScreenShowing(ScreenType.ControllerDisconnected))
			{
				d.Log("[UIScreenControllerDisconnected] Pushing go to screen as controller disconnected is showing");
				UIScreenControllerDisconnected uIScreenControllerDisconnected = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected != null)
				{
					uIScreenControllerDisconnected.GoToScreen(screen);
					return;
				}
			}
			d.LogWarning("Screen change was prevented through SetEnableScreenChange(false)!");
			return;
		}
		d.Assert(!s_HidingScreen, "Trying to show screen from another's screen Hide!");
		if (screen != null)
		{
			if (screen.Type == ScreenType.ControllerDisconnected)
			{
				d.Log("[UIScreenControllerDisconnected] Pushing screen Controller disconnected, grabbing existing popups for re-display once controller disconnect is dismissed");
				UIScreenControllerDisconnected uIScreenControllerDisconnected2 = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected2 != null)
				{
					for (int i = 0; i < m_ScreenStack.Count; i++)
					{
						if (m_ScreenStack[i].isPopup)
						{
							uIScreenControllerDisconnected2.AddPopup(m_ScreenStack[i].screen);
						}
					}
				}
			}
			PopAllPopups();
			if (m_ScreenStack.Count > 0)
			{
				StackEntry stackEntry = m_ScreenStack[m_ScreenStack.Count - 1];
				if (stackEntry.screen != null)
				{
					if (asPopup)
					{
						stackEntry.screen.HideBehindPopup();
					}
					else
					{
						HideScreen(stackEntry.screen);
					}
				}
			}
			for (int j = 0; j < m_ScreenStack.Count; j++)
			{
				if (m_ScreenStack[j].screen == screen)
				{
					d.LogErrorFormat("ManUI.PushScreen - Trying to show screen {0} but screen is already in the stack! This object will be reused (and any state held by it will be lost)!", screen);
					break;
				}
			}
			m_ScreenStack.Add(new StackEntry
			{
				screen = screen,
				pauseType = pauseType,
				isPopup = asPopup
			});
			if (pauseType == PauseType.Pause)
			{
				if (m_PauseCount == 0 && !Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
				{
					Singleton.Manager<ManPauseGame>.inst.Pause();
				}
				m_PauseCount++;
			}
			ShowScreen(screen, fromStackPop: false, asPopup);
			if (OnScreenChangeEvent != null)
			{
				OnScreenChangeEvent(arg1: true, screen.Type);
			}
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		}
		else
		{
			d.LogError("ManUI.PushScreen - Trying to push null screen");
		}
	}

	public void UpdateScreenPrompt(ManBtnPrompt.PromptData data)
	{
		if (m_ScreenBtnPrompts != null)
		{
			m_ScreenBtnPrompts.UpdateCurrentPrompt(data.prompts[0]);
		}
	}

	public void UpdateScreenPrompt(ManBtnPrompt.PromptType promptType)
	{
		if (m_ScreenBtnPrompts != null)
		{
			ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(promptType);
			if (promptDataByType != null)
			{
				UpdateScreenPrompt(promptDataByType);
			}
		}
	}

	public void ToggleBtnPrompt(bool active, Localisation.GlyphInfo[] rewiredActionIds)
	{
		if (m_ScreenBtnPrompts != null)
		{
			m_ScreenBtnPrompts.ToggleBtnPrompt(active, rewiredActionIds);
		}
	}

	public void ToggleBtnPrompt(bool active, ManBtnPrompt.PromptType promptType)
	{
		if (!(m_ScreenBtnPrompts != null))
		{
			return;
		}
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(promptType);
		if (promptDataByType != null)
		{
			LocalisedString[] prompts = promptDataByType.prompts;
			foreach (LocalisedString localisedString in prompts)
			{
				ToggleBtnPrompt(active, localisedString.m_InlineGlyphs);
			}
		}
	}

	public void ShowScreenPrompt(ScreenType screenType)
	{
		if (m_ScreenBtnPrompts != null)
		{
			ManBtnPrompt.PromptData screenPromptData = Singleton.Manager<ManBtnPrompt>.inst.GetScreenPromptData(screenType);
			if (screenPromptData != null)
			{
				m_ScreenBtnPrompts.ShowPrompt(screenPromptData);
				m_ScreenBtnPrompts.gameObject.SetActive(value: true);
				ParentUIScreen(m_ScreenBtnPrompts.transform, m_ActiveUI);
				m_ScreenBtnPrompts.transform.SetAsLastSibling();
			}
		}
	}

	public void HideScreenPrompt()
	{
		if ((bool)m_ScreenBtnPrompts)
		{
			m_ScreenBtnPrompts.gameObject.SetActive(value: false);
			m_ScreenBtnPrompts.HidePrompt();
			ParentUIScreen(m_ScreenBtnPrompts.transform, m_InactiveUI);
		}
	}

	public void PopScreen(bool showPrev = true)
	{
		if (m_DisableScreenChange)
		{
			d.LogWarning("Screen change was prevented through SetEnableScreenChange(false)!");
			if (IsScreenShowing(ScreenType.ControllerDisconnected))
			{
				d.Log("[UIScreenControllerDisconnected] Can't popscreen whilst controller disconnected is showing");
				UIScreenControllerDisconnected uIScreenControllerDisconnected = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected != null)
				{
					uIScreenControllerDisconnected.ExitAllScreens();
				}
			}
		}
		else if (m_ScreenStack.Count > 0)
		{
			StackEntry stackEntry = m_ScreenStack[m_ScreenStack.Count - 1];
			m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
			HideScreen(stackEntry.screen);
			if (stackEntry.isPopup)
			{
				m_InputBlocker.gameObject.SetActive(value: false);
				if (m_ScreenStack.Count > 0)
				{
					StackEntry stackEntry2 = m_ScreenStack[m_ScreenStack.Count - 1];
					if (stackEntry2.screen != null)
					{
						stackEntry2.screen.ReturnFromPopup();
					}
				}
			}
			if (stackEntry.pauseType == PauseType.Pause)
			{
				m_PauseCount--;
				d.Assert(m_PauseCount >= 0, "Negative screen pause count");
				if (m_PauseCount == 0 && !Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
				{
					Singleton.Manager<ManPauseGame>.inst.Resume();
				}
			}
			if (OnScreenChangeEvent != null)
			{
				OnScreenChangeEvent(arg1: false, stackEntry.screen.Type);
			}
			if (!stackEntry.isPopup)
			{
				if (showPrev && m_ScreenStack.Count > 0)
				{
					stackEntry = m_ScreenStack[m_ScreenStack.Count - 1];
					if (stackEntry.screen != null)
					{
						ShowScreen(stackEntry.screen, fromStackPop: true, stackEntry.isPopup);
					}
				}
			}
			else if (SKU.ConsoleUI && m_ScreenStack.Count > 0)
			{
				stackEntry = m_ScreenStack[m_ScreenStack.Count - 1];
				HideScreenPrompt();
				ShowScreenPrompt(stackEntry.screen.Type);
			}
			if (m_ScreenStack.Count == 0)
			{
				Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
				if (SKU.ConsoleUI)
				{
					HideScreenPrompt();
				}
			}
		}
		else
		{
			d.LogError("ManUI.PopScreen - Trying to pop screen when stack is empty");
		}
	}

	public void ShowErrorPopup(string text)
	{
		if (m_HasInitialised)
		{
			AddErrorPopupToQueue(text);
			if (!IsScreenShowing(ScreenType.NotificationScreen))
			{
				NextErrorPopup();
			}
		}
		else
		{
			AddErrorPopupToQueue(text);
		}
	}

	private void AddErrorPopupToQueue(string text)
	{
		if (!m_ErrorPopupQueue.Contains(text))
		{
			m_ErrorPopupQueue.Add(text);
		}
	}

	private void NextErrorPopup()
	{
		if (m_ErrorPopupQueue.Count > 0)
		{
			Action accept = delegate
			{
				RemovePopup();
				NextErrorPopup();
			};
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(m_ErrorPopupQueue[0], accept, localisedString);
			PushScreenAsPopup(uIScreenNotifications);
			m_ErrorPopupQueue.RemoveAt(0);
		}
	}

	public void PushScreenAsPopup(UIScreen popupScreen, PauseType pauseType = PauseType.None)
	{
		PushScreen(popupScreen, pauseType, asPopup: true);
	}

	public void RemovePopup()
	{
		PopScreen(showPrev: false);
	}

	public void PopAllPopups()
	{
		if (m_DisableScreenChange)
		{
			d.LogWarning("Screen change was prevented through SetEnableScreenChange(false)!");
			if (IsScreenShowing(ScreenType.ControllerDisconnected))
			{
				d.Log("[UIScreenControllerDisconnected] Can't popscreen whilst controller disconnected is showing");
				UIScreenControllerDisconnected uIScreenControllerDisconnected = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected != null)
				{
					uIScreenControllerDisconnected.ExitAllScreens();
				}
			}
		}
		else
		{
			bool flag = false;
			while (m_ScreenStack.Count > 0 && m_ScreenStack[m_ScreenStack.Count - 1].isPopup)
			{
				PopScreen(showPrev: false);
				flag = true;
			}
			if (flag)
			{
				m_InputBlocker.gameObject.SetActive(value: false);
			}
		}
	}

	public void GoBack()
	{
		if (m_ScreenStack.Count > 0 && m_ScreenStack[m_ScreenStack.Count - 1].screen.GoBack())
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}

	public void FadeToBlack(float time = 3f, bool forceFront = false)
	{
		FadeToColour(Color.black, time, forceFront);
	}

	public void FadeToColour(Color colour, float time = 3f, bool forceFront = false, bool showAnim = true)
	{
		if (m_FadeTarget != 1f)
		{
			m_animContainer.SetActive(value: false);
			m_FadeTime = time;
			m_ForceFront = forceFront;
			m_FadeTarget = 1f;
			m_FadeStartTime = Time.realtimeSinceStartup;
			m_CurrentFade = m_FadeScreen.color.a;
		}
		m_FadeScreen.color = colour;
		if (showAnim)
		{
			SetLoadingAnim();
		}
	}

	private void SetLoadingAnim()
	{
		m_animContainer.SetActive(value: true);
	}

	public void ClearFade(float time)
	{
		if (m_FadeTarget != 0f)
		{
			m_FadeTime = time;
			m_ForceFront = false;
			m_FadeTarget = 0f;
			m_FadeStartTime = Time.realtimeSinceStartup;
			m_CurrentFade = m_FadeScreen.color.a;
		}
	}

	public void SetFadeLayer(bool atFront)
	{
		m_ForceFront = atFront;
	}

	public bool IsFadingDown()
	{
		return m_FadeTarget == 1f;
	}

	public bool FadeFinished()
	{
		return m_CurrentFade == m_FadeTarget;
	}

	public void GoToScreen(ScreenType screenType, PauseType pauseType = PauseType.None)
	{
		UIScreen screen = GetScreen(screenType);
		if (screen != null)
		{
			GoToScreen(screen, pauseType);
		}
		else
		{
			d.LogError("ManUI.GoToScreen - ScreenType " + screenType.ToString() + " is null");
		}
	}

	public void GoToScreen(UIScreen screen, PauseType pauseType = PauseType.None)
	{
		if (m_DisableScreenChange)
		{
			d.LogWarning("Screen change was prevented through SetEnableScreenChange(false)!");
			if (IsScreenShowing(ScreenType.ControllerDisconnected))
			{
				UIScreenControllerDisconnected uIScreenControllerDisconnected = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected != null)
				{
					d.Log("[UIScreenControllerDisconnected] GoToScreen set as controller disconnect screen is showing");
					uIScreenControllerDisconnected.GoToScreen(screen);
				}
			}
		}
		else if (screen != null)
		{
			if (IsScreenInStack(screen))
			{
				PopToScreen(screen);
			}
			else
			{
				PushScreen(screen, pauseType);
			}
		}
		else
		{
			d.LogError("ManUI.GoToScreen - Screen is null");
		}
	}

	public void ExitAllScreens()
	{
		if (m_DisableScreenChange)
		{
			d.LogWarning("Screen change was prevented through SetEnableScreenChange(false)!");
			if (IsScreenShowing(ScreenType.ControllerDisconnected))
			{
				d.Log("[UIScreenControllerDisconnected] Can't popscreen whilst controller disconnected is showing");
				UIScreenControllerDisconnected uIScreenControllerDisconnected = GetScreen(ScreenType.ControllerDisconnected) as UIScreenControllerDisconnected;
				if (uIScreenControllerDisconnected != null)
				{
					uIScreenControllerDisconnected.ExitAllScreens();
				}
			}
		}
		else
		{
			while (m_ScreenStack.Count > 0)
			{
				PopScreen(showPrev: false);
			}
		}
	}

	public bool IsScreenInStack(ScreenType screenType)
	{
		UIScreen screen = GetScreen(screenType);
		return IsScreenInStack(screen);
	}

	public bool IsScreenInStack(UIScreen screen)
	{
		bool result = false;
		for (int i = 0; i < m_ScreenStack.Count; i++)
		{
			if (m_ScreenStack[i].screen == screen)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static float GetNormalisedScroll(PointerEventData scrollData, RectTransform contentTransform)
	{
		return scrollData.scrollDelta.y / contentTransform.sizeDelta.y;
	}

	public UIScreen CurrentScreen()
	{
		UIScreen result = null;
		if (m_ScreenStack.Count > 0)
		{
			result = m_ScreenStack[m_ScreenStack.Count - 1].screen;
		}
		return result;
	}

	public void DoFlash(float inTime, float outTime)
	{
		StartCoroutine(Flash(inTime, outTime));
	}

	public void ParentUIScreen(Transform screen, RectTransform parent)
	{
		if (screen.parent == parent)
		{
			screen.SetAsLastSibling();
		}
		else
		{
			screen.SetParent(parent, worldPositionStays: false);
		}
	}

	public bool IsStackEmpty()
	{
		return m_ScreenStack.Count == 0;
	}

	public bool IsPopupShowing()
	{
		if (m_ScreenStack.Count > 1)
		{
			return m_ScreenStack[m_ScreenStack.Count - 1].isPopup;
		}
		return false;
	}

	public UIScreen GetScreen(ScreenType toGet)
	{
		return m_Screens[(int)toGet];
	}

	public UIScreen GetPreviousScreen()
	{
		if (m_ScreenStack.Count > 1)
		{
			return m_ScreenStack[m_ScreenStack.Count - 2].screen;
		}
		return null;
	}

	public bool IsScreenShowing(ScreenType toCheck)
	{
		UIScreen uIScreen = m_Screens[(int)toCheck];
		if (uIScreen != null)
		{
			return uIScreen.state == UIScreen.State.Show;
		}
		return false;
	}

	public bool IsExitBlockedOnCurrentScreen()
	{
		bool result = false;
		if (m_ScreenStack.Count > 0)
		{
			result = !m_ScreenStack[m_ScreenStack.Count - 1].screen.CanExit;
		}
		return result;
	}

	public float GetAspectChangeRatio()
	{
		if (m_AspectChangeRatio == 0f)
		{
			m_AspectChangeRatio = 1f - (Singleton.Manager<ManUI>.inst.m_ReferenceAspect - (float)Screen.width / (float)Screen.height) / 2f;
		}
		return m_AspectChangeRatio;
	}

	private void PopToScreen(UIScreen screen)
	{
		bool flag = false;
		while (m_ScreenStack.Count > 0 && m_ScreenStack[m_ScreenStack.Count - 1].screen != screen)
		{
			flag = flag || !m_ScreenStack[m_ScreenStack.Count - 1].isPopup;
			PopScreen(showPrev: false);
		}
		if (m_ScreenStack.Count != 0)
		{
			if (flag)
			{
				StackEntry stackEntry = m_ScreenStack[m_ScreenStack.Count - 1];
				d.Assert(!stackEntry.isPopup, "ManUI.PopToScreen - Popping the screen stack to a popup screen previously in the stack. This is unexpected (Popups can only be at the top of the stack!)");
				ShowScreen(screen, fromStackPop: true, stackEntry.isPopup);
			}
		}
		else
		{
			d.LogError(string.Concat("ManUI.PopToScreen - Trying to pop to screen ", screen.Type, " that isn't in the stack"));
		}
	}

	public void SetEnableScreenChange(bool enable)
	{
		bool flag = !enable;
		d.Assert(m_DisableScreenChange != flag, "Screen change disabled flag was already set! Double setting it is likely to cause issues down the line with removing the flag");
		m_DisableScreenChange = flag;
	}

	private void ShowScreen(UIScreen screen, bool fromStackPop = false, bool asPopup = false)
	{
		if (asPopup)
		{
			m_InputBlocker.gameObject.SetActive(value: true);
			m_InputBlocker.transform.SetAsLastSibling();
		}
		if (SKU.ConsoleUI)
		{
			HideScreenPrompt();
			ManBtnPrompt.PromptData screenPromptData = Singleton.Manager<ManBtnPrompt>.inst.GetScreenPromptData(screen.Type);
			if (screenPromptData != null)
			{
				m_ScreenBtnPrompts.ShowPrompt(screenPromptData);
			}
		}
		screen.Show(fromStackPop);
		ParentUIScreen(screen.transform, m_ActiveUI);
		if (SKU.ConsoleUI)
		{
			m_ScreenBtnPrompts.gameObject.SetActive(value: true);
			ParentUIScreen(m_ScreenBtnPrompts.transform, m_ActiveUI);
			m_ScreenBtnPrompts.transform.SetAsLastSibling();
		}
	}

	private void HideScreen(UIScreen screen)
	{
		s_HidingScreen = true;
		screen.Hide();
		s_HidingScreen = false;
		ParentUIScreen(screen.transform, m_InactiveUI);
	}

	private IEnumerator Flash(float time, float outTime)
	{
		float timer = 0f;
		m_FlashScreen.color = m_FlashScreen.color.SetAlpha(0f);
		m_FlashScreen.gameObject.SetActive(value: true);
		while (timer < time)
		{
			timer += Time.unscaledDeltaTime;
			m_FlashScreen.color = m_FlashScreen.color.SetAlpha(timer / time);
			m_FlashScreen.rectTransform.SetAsLastSibling();
			yield return null;
		}
		timer = 0f;
		while (timer < outTime)
		{
			timer += Time.unscaledDeltaTime;
			m_FlashScreen.color = m_FlashScreen.color.SetAlpha(outTime / timer);
			m_FlashScreen.rectTransform.SetAsLastSibling();
			yield return null;
		}
		m_FlashScreen.gameObject.SetActive(value: false);
	}

	public void SetUILockTimer(LockTimerTypes type, float delay)
	{
		m_LockTimers[(int)type] = Time.time + delay;
	}

	public bool IsUILocked(LockTimerTypes type)
	{
		return m_LockTimers[(int)type] > Time.time;
	}

	private void UpdateScreenScalingForSwitch(bool handheld)
	{
		bool flag = false;
		if (handheld || flag)
		{
			m_RescaledMainCanvas.enabled = false;
			Singleton.camera.targetTexture = null;
			m_OffscreenTargetScaling = 1f;
			d.Log($"[UpdateScreenScalingForSwitch] scaling={m_OffscreenTargetScaling} screen size={Screen.width}x{Screen.height}");
			return;
		}
		int num = 720;
		int num2 = 1280;
		m_OffscreenTargetScaling = (float)num / 1080f;
		if (m_OffscreenRenderTarget == null || num2 != m_OffscreenRenderTarget.width || num != m_OffscreenRenderTarget.height)
		{
			d.Log($"[UpdateScreenScalingForSwitch] Create offscreen render target {num2}x{num}");
			if (m_OffscreenRenderTarget != null)
			{
				UnityEngine.Object.DestroyImmediate(m_OffscreenRenderTarget);
			}
			m_OffscreenRenderTarget = new RenderTexture(num2, num, 24, RenderTextureFormat.ARGB32);
		}
		m_RescaledMainCanvas.enabled = true;
		m_RescaledMainView.texture = m_OffscreenRenderTarget;
		Singleton.camera.targetTexture = m_OffscreenRenderTarget;
		d.Log($"[UpdateScreenScalingForSwitch] scaling={m_OffscreenTargetScaling} screen size={Screen.width}x{Screen.height} buffer size={m_OffscreenRenderTarget.width}x{m_OffscreenRenderTarget.height}");
	}

	public Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
	{
		return RectTransformUtility.WorldToScreenPoint(cam, worldPoint) / m_OffscreenTargetScaling;
	}

	public Vector3 WorldToScreenPoint(Vector3 position)
	{
		Vector3 result = Singleton.camera.WorldToScreenPoint(position);
		result.x /= m_OffscreenTargetScaling;
		result.y /= m_OffscreenTargetScaling;
		return result;
	}

	public Rect GetCameraPixelRect()
	{
		Rect pixelRect = Singleton.camera.pixelRect;
		pixelRect.min /= m_OffscreenTargetScaling;
		pixelRect.max /= m_OffscreenTargetScaling;
		return pixelRect;
	}

	public Ray ScreenPointToRay(Vector3 pos)
	{
		pos.x *= m_OffscreenTargetScaling;
		pos.y *= m_OffscreenTargetScaling;
		return Singleton.camera.ScreenPointToRay(pos);
	}

	private void Start()
	{
		m_MainCanvasTrans = m_MainCanvas.transform;
		UILoadingScreenHints.SuppressNextHint = true;
		m_CurrentFade = 1f;
		m_FadeTarget = 1f;
		m_FadeScreen = UnityEngine.Object.Instantiate(m_FadeScreen);
		m_FadeScreen.color = m_FadeScreen.color.SetAlpha(m_CurrentFade);
		ParentUIScreen(m_FadeScreen.transform, m_ActiveUI);
		d.Assert(m_FadeScreen.GetComponentInChildren<Animator>(), "Missing Animator component on " + m_FadeScreen);
		m_animContainer = m_FadeScreen.GetComponentInChildren<Animator>().gameObject;
		m_BorderScreen = UnityEngine.Object.Instantiate(m_BorderScreen);
		ParentUIScreen(m_BorderScreen.transform, m_ActiveUI);
		m_ResizeScreen = UnityEngine.Object.Instantiate(m_ResizeScreen);
		ParentUIScreen(m_ResizeScreen.transform, m_ActiveUI);
		m_ResizeScreen.gameObject.SetActive(value: false);
		m_FlashScreen = UnityEngine.Object.Instantiate(m_FlashScreen);
		ParentUIScreen(m_FlashScreen.transform, m_ActiveUI);
		m_FlashScreen.gameObject.SetActive(value: false);
		m_InputBlocker = UnityEngine.Object.Instantiate(m_InputBlocker);
		ParentUIScreen(m_InputBlocker.transform, m_ActiveUI);
		m_InputBlocker.gameObject.SetActive(value: false);
		m_Screens = new UIScreen[m_NewScreenPrefabList.Length];
		for (int i = 0; i < m_NewScreenPrefabList.Length; i++)
		{
			UIScreen uIScreen = m_NewScreenPrefabList[i];
			if (uIScreen != null)
			{
				uIScreen.gameObject.SetActive(value: false);
				UIScreen uIScreen2 = uIScreen.UnpooledSpawn(m_InactiveUI, worldPosStays: false);
				m_Screens[i] = uIScreen2;
				if (uIScreen2 != null)
				{
					uIScreen2.ScreenInitialize((ScreenType)i);
					uIScreen2.gameObject.SetActive(value: false);
				}
			}
		}
		m_GetBetaButton.SetActive(value: false);
		m_SpriteFetcher = GetComponent<SpriteFetcher>();
		m_HalfReferenceResolution = m_ReferenceResolution / 2f;
		m_ReferenceAspect = m_ReferenceResolution.x / m_ReferenceResolution.y;
		if (SKU.ConsoleUI)
		{
			m_ScreenBtnPrompts = m_ScreenBtnPromptsPrefab.UnpooledSpawn();
			m_ScreenBtnPrompts.gameObject.SetActive(value: false);
			ParentUIScreen(m_ScreenBtnPrompts.transform, m_InactiveUI);
		}
		if (SKU.SwitchUI)
		{
			ManNintendoSwitch.HandheldModeChangedEvent.Subscribe(UpdateScreenScalingForSwitch);
			UpdateScreenScalingForSwitch(ManNintendoSwitch.IsHandheldMode);
		}
		m_HasInitialised = true;
		NextErrorPopup();
	}

	private void Update()
	{
		if (m_FadeScreen.color.a == 0f)
		{
			if (m_FadeScreen.gameObject.activeSelf)
			{
				m_FadeScreen.gameObject.SetActive(value: false);
			}
		}
		else if (!m_FadeScreen.gameObject.activeSelf)
		{
			m_FadeScreen.gameObject.SetActive(value: true);
		}
		if (m_ResizeScreen.gameObject.activeSelf != ResizeScreenActive)
		{
			m_ResizeScreen.gameObject.SetActive(ResizeScreenActive);
		}
		if (!FadeFinished())
		{
			float num = Time.realtimeSinceStartup - m_FadeStartTime;
			m_CurrentFade = Mathf.MoveTowards(m_CurrentFade, m_FadeTarget, num * m_FadeTime);
			m_FadeScreen.color = m_FadeScreen.color.SetAlpha(m_CurrentFade);
			m_FadeStartTime = Time.realtimeSinceStartup;
		}
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && Singleton.Manager<DebugUtil>.inst.hideTheGUI == m_ActiveUI.gameObject.activeSelf)
		{
			m_ActiveUI.gameObject.SetActive(!Singleton.Manager<DebugUtil>.inst.hideTheGUI);
		}
		if (m_HasInitialised && !IsScreenInStack(ScreenType.NotificationScreen))
		{
			NextErrorPopup();
		}
	}

	private void LateUpdate()
	{
		m_AspectChangeRatio = 0f;
		if (m_FadeScreen.color.a != 0f)
		{
			UpdateFadeLayer();
		}
	}

	private void UpdateFadeLayer()
	{
		Transform transform = m_FadeScreen.transform;
		int siblingIndex = transform.GetSiblingIndex();
		if (m_ForceFront)
		{
			if (siblingIndex != transform.parent.childCount - 1)
			{
				m_FadeScreen.transform.SetAsLastSibling();
			}
		}
		else if (siblingIndex != 0)
		{
			m_FadeScreen.transform.SetAsFirstSibling();
		}
	}
}
