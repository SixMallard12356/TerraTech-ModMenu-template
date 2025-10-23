using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManCircuits : Singleton.Manager<ManCircuits>
{
	[SerializeField]
	protected bool m_DrawDebugTimerUI;

	private UICheckpointChallengeHUD m_TimerHUD;

	private Dictionary<float, List<ModuleCircuit_WiFi_Transmitter>> m_WirelessTransmissions = new Dictionary<float, List<ModuleCircuit_WiFi_Transmitter>>();

	protected HashSet<ModuleCircuit_Time_Stopwatch> m_ActiveStopwatches = new HashSet<ModuleCircuit_Time_Stopwatch>();

	private OnGUICallback m_GuiCallback;

	protected GUIStyle m_StopwatchStyle_Default = GUIStyle.none;

	protected GUIStyle m_StopwatchStyle_Counting = GUIStyle.none;

	protected GUIStyle m_StopwatchStyle_Stopped = GUIStyle.none;

	private UICheckpointChallengeHUD TimerHUD
	{
		get
		{
			if (m_TimerHUD == null)
			{
				m_TimerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.CheckpointChallenge) as UICheckpointChallengeHUD;
			}
			return m_TimerHUD;
		}
	}

	public void RegisterTransmission(float frequency, ModuleCircuit_WiFi_Transmitter source)
	{
		if (!m_WirelessTransmissions.ContainsKey(frequency))
		{
			m_WirelessTransmissions.Add(frequency, new List<ModuleCircuit_WiFi_Transmitter> { source });
		}
		else if (!m_WirelessTransmissions[frequency].Contains(source))
		{
			m_WirelessTransmissions[frequency].Add(source);
		}
	}

	public void DeregisterTransmission(float frequency, ModuleCircuit_WiFi_Transmitter source)
	{
		if (m_WirelessTransmissions.ContainsKey(frequency) && m_WirelessTransmissions[frequency].Contains(source))
		{
			m_WirelessTransmissions[frequency].Remove(source);
			if (m_WirelessTransmissions[frequency].Count == 0)
			{
				m_WirelessTransmissions.Remove(frequency);
			}
		}
	}

	public int GetTransmissionStrengthOnFrequency(float frequency, Vector3 worldPosition, int team, float detectionRadius = -1f)
	{
		if (!m_WirelessTransmissions.ContainsKey(frequency))
		{
			return 0;
		}
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < m_WirelessTransmissions[frequency].Count; i++)
		{
			if (m_WirelessTransmissions[frequency][i].block.tank.Team == team)
			{
				num2 = Vector3.Distance(m_WirelessTransmissions[frequency][i].transform.position, worldPosition);
				if (m_WirelessTransmissions[frequency][i].TransmissionRadius >= num2 && (detectionRadius == -1f || num2 <= detectionRadius) && m_WirelessTransmissions[frequency][i].TransmissionStrength > num)
				{
					num = m_WirelessTransmissions[frequency][i].TransmissionStrength;
				}
			}
		}
		return num;
	}

	public void RegisterStopwatch(ModuleCircuit_Time_Stopwatch stopwatch)
	{
		bool num = m_ActiveStopwatches.Add(stopwatch);
		UpdateRegisteredStopwatches();
		if (num)
		{
			stopwatch.TagPickerField.OptionSetEvent.Subscribe(UpdateRegisteredStopwatches);
		}
	}

	public void DeregisterStopwatch(ModuleCircuit_Time_Stopwatch stopwatch)
	{
		bool num = m_ActiveStopwatches.Remove(stopwatch);
		UpdateRegisteredStopwatches();
		if (num)
		{
			stopwatch.TagPickerField.OptionSetEvent.Unsubscribe(UpdateRegisteredStopwatches);
		}
	}

	private void UpdateRegisteredStopwatches()
	{
		if (m_ActiveStopwatches.Count == 0 != (m_GuiCallback == null))
		{
			if ((bool)m_GuiCallback)
			{
				m_GuiCallback.OnGUIEvent.Unsubscribe(DrawStopwatchDebugGUI);
				m_GuiCallback = null;
			}
			else
			{
				m_GuiCallback = OnGUICallback.AddGUICallback(base.gameObject);
				m_GuiCallback.OnGUIEvent.Subscribe(DrawStopwatchDebugGUI);
			}
		}
		TimerHUD.SetCircuitTimers(m_ActiveStopwatches.OrderBy((ModuleCircuit_Time_Stopwatch r) => r.Timer.FirstTickTime).ToArray());
	}

	private void DrawStopwatchDebugGUI()
	{
		if (!m_DrawDebugTimerUI || m_ActiveStopwatches.Where((ModuleCircuit_Time_Stopwatch r) => r.Timer != null && r.Timer.TimeElapsed > 0f).Count() == 0)
		{
			return;
		}
		if (m_StopwatchStyle_Default == GUIStyle.none)
		{
			InitStopwatchGUIStyles();
		}
		Vector2Int vector2Int = new Vector2Int(250, Mathf.CeilToInt(m_StopwatchStyle_Default.lineHeight * (float)(m_ActiveStopwatches.Count + 3)));
		GUILayout.BeginArea(new Rect(new Vector2Int(Mathf.FloorToInt(0.5f * (float)(Screen.width - vector2Int.x)), 0), vector2Int));
		GUILayout.BeginVertical("Box");
		GUILayout.Label("__CIRCUIT_TIMERS__", m_StopwatchStyle_Default);
		foreach (ModuleCircuit_Time_Stopwatch activeStopwatch in m_ActiveStopwatches)
		{
			if (activeStopwatch.Timer.TimeElapsed > 0f)
			{
				GUILayout.Label($"{(int)activeStopwatch.Timer.TimeElapsed / 60:00}:{(int)activeStopwatch.Timer.TimeElapsed % 60:00}:{(int)(activeStopwatch.Timer.TimeElapsed * 100f) % 100:00}", activeStopwatch.Timer.IsTicking ? m_StopwatchStyle_Counting : m_StopwatchStyle_Stopped);
			}
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void InitStopwatchGUIStyles()
	{
		m_StopwatchStyle_Default = new GUIStyle(GUI.skin.label);
		m_StopwatchStyle_Default.fontSize = 14;
		m_StopwatchStyle_Default.wordWrap = false;
		m_StopwatchStyle_Default.alignment = TextAnchor.MiddleCenter;
		m_StopwatchStyle_Default.fontStyle = FontStyle.Normal;
		m_StopwatchStyle_Counting = new GUIStyle(m_StopwatchStyle_Default);
		m_StopwatchStyle_Counting.normal.textColor = Color.green;
		m_StopwatchStyle_Stopped = new GUIStyle(m_StopwatchStyle_Default);
		m_StopwatchStyle_Stopped.normal.textColor = Color.red;
	}

	private void Update()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame && ManNetwork.IsHost)
		{
			Circuits.DoCircuitLoop();
		}
	}
}
