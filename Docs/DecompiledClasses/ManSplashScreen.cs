using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManSplashScreen : Singleton.Manager<ManSplashScreen>
{
	[Serializable]
	public class Splash
	{
		public enum State
		{
			Initialize,
			FadeIn,
			Hold,
			FadeOut,
			Finalize
		}

		public float fadeInTime;

		public float fadeOutTime;

		public float middleTime;

		public Color fadeInColor;

		public Color fadeOutColor;

		public Transform m_SplashScreenPrefab;

		public bool canSkip = true;

		private float timer;

		private float guiOpacity;

		private bool doSkip;

		private CanvasGroup m_SplashScreen;

		private State state;

		public bool IsState(State s)
		{
			return state == s;
		}

		private bool UpdateFadeIn()
		{
			if (timer <= fadeInTime)
			{
				guiOpacity = timer / fadeInTime;
				timer += Time.deltaTime;
				return true;
			}
			return false;
		}

		private bool UpdateFadeOut()
		{
			if (timer <= fadeOutTime)
			{
				guiOpacity = (fadeOutTime - timer) / fadeOutTime;
				timer += Time.deltaTime;
				return timer < fadeOutTime;
			}
			return false;
		}

		private bool UpdateHold()
		{
			if (timer <= middleTime)
			{
				timer += Time.deltaTime;
				return true;
			}
			return false;
		}

		public void DoGUI()
		{
			if (state != State.Initialize)
			{
				m_SplashScreen.alpha = guiOpacity;
			}
		}

		private void Initialize()
		{
			Transform transform = UnityEngine.Object.Instantiate(m_SplashScreenPrefab);
			m_SplashScreen = transform.GetComponentInChildren<CanvasGroup>(includeInactive: true);
			transform.SetParent(Singleton.Manager<ManSplashScreen>.inst.CanvasTrans, worldPositionStays: false);
			transform.SetAsLastSibling();
			Singleton.Manager<ManSplashScreen>.inst.BackgroundColour = fadeInColor;
			Singleton.camera.backgroundColor = fadeInColor;
			UpdateFadeIn();
		}

		private void AdvanceState()
		{
			state++;
			Singleton.Manager<ManStartup>.inst.ScreenStatic = state == State.Hold;
			timer = 0f;
		}

		public bool Run(bool forceSkip)
		{
			if (canSkip && (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1)))
			{
				doSkip = true;
			}
			switch (state)
			{
			case State.Initialize:
				Initialize();
				AdvanceState();
				break;
			case State.FadeIn:
				if (forceSkip || !UpdateFadeIn())
				{
					AdvanceState();
				}
				break;
			case State.Hold:
			{
				bool flag = !Singleton.Manager<ManSplashScreen>.inst.FinalSplashState(State.Hold) || Singleton.Manager<ManSplashScreen>.inst.CanExit;
				if ((forceSkip || !UpdateHold() || doSkip) && flag)
				{
					AdvanceState();
					Singleton.Manager<ManSplashScreen>.inst.BackgroundColour = fadeOutColor;
				}
				break;
			}
			case State.FadeOut:
				if (forceSkip || !UpdateFadeOut())
				{
					AdvanceState();
					UnityEngine.Object.Destroy(m_SplashScreen.gameObject);
					return false;
				}
				break;
			}
			return true;
		}
	}

	[SerializeField]
	private Splash m_SteamDisclaimer;

	[SerializeField]
	private Splash m_EpicGSDisclaimer;

	[SerializeField]
	private Splash m_PS4Disclaimer;

	[SerializeField]
	private Splash m_XBONEDisclaimer;

	[SerializeField]
	private Splash m_SwitchDisclaimer;

	[SerializeField]
	private Splash m_NetEaseDisclaimer;

	[SerializeField]
	private Image m_BackgroundPrefab;

	[SerializeField]
	private Canvas m_CanvasPrefab;

	[SerializeField]
	private Splash m_TeyonScreen;

	private int m_SplashScreenIndex;

	private List<IEnumerator> toRun = new List<IEnumerator>();

	private bool isCoroutineRunning;

	private Image m_Background;

	private Canvas m_MyCanvas;

	private List<Splash> m_SplashScreens = new List<Splash>();

	public bool CanExit { get; set; }

	public Color BackgroundColour
	{
		get
		{
			return m_Background.color;
		}
		set
		{
			m_Background.color = value;
		}
	}

	public Transform CanvasTrans => m_MyCanvas.transform;

	public bool IsBusy
	{
		get
		{
			if (!isCoroutineRunning)
			{
				return toRun.Count > 0;
			}
			return true;
		}
	}

	public bool AutoSkip { get; set; }

	public bool HasExited => m_SplashScreenIndex >= m_SplashScreens.Count;

	public void SetUICamera(Camera camera)
	{
		m_MyCanvas.worldCamera = camera;
	}

	public void AddCoroutine(IEnumerator toAdd)
	{
		toRun.Add(toAdd);
	}

	private IEnumerator RunInBackground()
	{
		isCoroutineRunning = true;
		while (toRun.Count > 0)
		{
			if (toRun[0].MoveNext())
			{
				yield return toRun[0].Current;
			}
			else
			{
				toRun.RemoveAt(0);
			}
		}
		isCoroutineRunning = false;
	}

	public bool FinalSplashState(Splash.State state)
	{
		if (m_SplashScreenIndex == m_SplashScreens.Count - 1)
		{
			return m_SplashScreens[m_SplashScreenIndex].IsState(state);
		}
		return false;
	}

	private void Setup()
	{
	}

	private void Awake()
	{
		if (SKU.IsTeyon)
		{
			m_SplashScreens.Add(m_TeyonScreen);
		}
		if (SKU.ConsoleUI)
		{
			if (SKU.PS4UI)
			{
				m_SplashScreens.Add(m_PS4Disclaimer);
			}
			if (SKU.XboxOneUI)
			{
				m_SplashScreens.Add(m_XBONEDisclaimer);
			}
			if (SKU.SwitchUI)
			{
				m_SplashScreens.Add(m_SwitchDisclaimer);
			}
		}
		else if (SKU.IsSteam)
		{
			m_SplashScreens.Add(m_SteamDisclaimer);
		}
		else if (SKU.IsEpicGS)
		{
			m_SplashScreens.Add(m_EpicGSDisclaimer);
		}
		else if (SKU.IsNetEase)
		{
			m_SplashScreens.Add(m_NetEaseDisclaimer);
		}
		CanExit = true;
		m_SplashScreenIndex = 0;
		isCoroutineRunning = false;
		m_MyCanvas = UnityEngine.Object.Instantiate(m_CanvasPrefab);
		m_Background = UnityEngine.Object.Instantiate(m_BackgroundPrefab);
		m_Background.rectTransform.SetParent(m_MyCanvas.transform, worldPositionStays: false);
		m_MyCanvas.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		if (!isCoroutineRunning && toRun.Count > 0)
		{
			StartCoroutine(RunInBackground());
		}
		int count = m_SplashScreens.Count;
		if (m_SplashScreenIndex < count && !m_SplashScreens[m_SplashScreenIndex].Run(AutoSkip))
		{
			m_SplashScreenIndex++;
		}
		if (m_MyCanvas.IsNotNull() && m_SplashScreenIndex >= count)
		{
			if (m_Background.color.a == 0f)
			{
				UnityEngine.Object.Destroy(m_MyCanvas.gameObject);
				m_MyCanvas = null;
			}
			else
			{
				m_Background.color = m_Background.color.SetAlpha(Mathf.Clamp01(m_Background.color.a - Time.deltaTime / 2f));
			}
			Singleton.Manager<ManStartup>.inst.ScreenStatic = false;
		}
		if (count > m_SplashScreenIndex)
		{
			m_SplashScreens[m_SplashScreenIndex].DoGUI();
		}
	}
}
