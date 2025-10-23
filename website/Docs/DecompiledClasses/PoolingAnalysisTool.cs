using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingAnalysisTool : MonoBehaviour
{
	private struct CategoryAnalyticsGroup
	{
		public float AveUtilisation;

		public float SumMemoryUsage;

		public float SumMemoryAllocation;

		public float InstantiationTime;

		public CategoryAnalyticsGroup(float defaultValue)
		{
			AveUtilisation = defaultValue;
			SumMemoryUsage = defaultValue;
			SumMemoryAllocation = defaultValue;
			InstantiationTime = defaultValue;
		}

		public CategoryAnalyticsGroup(float AveUtilisation, float SumMemoryUsage, float SumMemoryAllocation, float InstantiationTime)
		{
			this.AveUtilisation = AveUtilisation;
			this.SumMemoryUsage = SumMemoryUsage;
			this.SumMemoryAllocation = SumMemoryAllocation;
			this.InstantiationTime = InstantiationTime;
		}
	}

	private Dictionary<string, CategoryAnalyticsGroup> m_AnalyticsGroups = new Dictionary<string, CategoryAnalyticsGroup>();

	private float m_fillBar_TitleScale = 0.2f;

	private float m_fillBar_RawUsageScale = 0.15f;

	private float m_fillBar_BarScale = 0.5f;

	private float m_fillBar_PercScale = 0.07f;

	private float m_fillBar_DurationScale = 0.08f;

	private int m_fillBar_YSize = 18;

	private int m_fillBar_fontSize = 16;

	private GUIStyle m_sHeader1;

	private GUIStyle m_sFillBarTitle1;

	private GUIStyle m_sFillBarTitle2;

	private GUIStyle m_sBoxGreen1;

	private GUIStyle m_sBoxRed1;

	private GUIStyle m_sBoxInvis1;

	private float m_TimeBetweenChecks;

	private float m_TimeSinceLastCheck = -1f;

	private float m_MaxAllocationLastCheck;

	private float m_TotalUsageLastCheck;

	private float m_TotalAllocationLastCheck;

	private float m_MaxTotalUtilisationOverGame = -1f;

	private float m_AverageTotalUtilisationLastCheck = -1f;

	public float TimeBetweenChecks
	{
		get
		{
			return m_TimeBetweenChecks;
		}
		set
		{
			m_TimeBetweenChecks = value;
		}
	}

	private Vector2Int m_screenPosition => new Vector2Int(Screen.width - m_areaSize.x, 0);

	private Vector2Int m_areaSize => new Vector2Int(750, 1000);

	private bool m_stylesInstanced
	{
		get
		{
			if (m_sBoxGreen1 != null && m_sBoxGreen1.normal != null && m_sBoxGreen1.normal.background != null)
			{
				return m_sBoxGreen1.normal.background.GetPixel(1, 1) == Color.green;
			}
			return false;
		}
	}

	private Vector2 AreaSizePerc(float multiplier)
	{
		return (Vector2)m_areaSize * multiplier;
	}

	private void InstanciateStyles()
	{
		m_sHeader1 = new GUIStyle(GUI.skin.label);
		m_sHeader1.fontSize = 18;
		m_sHeader1.wordWrap = false;
		m_sHeader1.alignment = TextAnchor.MiddleCenter;
		m_sHeader1.fontStyle = FontStyle.Bold;
		m_sFillBarTitle1 = new GUIStyle(GUI.skin.label);
		m_sFillBarTitle1.fontSize = m_fillBar_fontSize;
		m_sFillBarTitle1.wordWrap = false;
		m_sFillBarTitle1.richText = true;
		m_sFillBarTitle1.alignment = TextAnchor.MiddleLeft;
		m_sFillBarTitle2 = new GUIStyle(GUI.skin.label);
		m_sFillBarTitle2.fontSize = m_fillBar_fontSize;
		m_sFillBarTitle2.wordWrap = false;
		m_sFillBarTitle2.richText = true;
		m_sFillBarTitle2.alignment = TextAnchor.MiddleRight;
		m_sBoxGreen1 = new GUIStyle(GUI.skin.label);
		m_sBoxGreen1.normal.textColor = Color.black;
		m_sBoxGreen1.fontSize = m_fillBar_fontSize;
		m_sBoxGreen1.wordWrap = false;
		m_sBoxGreen1.fontStyle = FontStyle.Italic;
		m_sBoxGreen1.alignment = TextAnchor.MiddleRight;
		m_sBoxGreen1.normal.background = new Texture2D(1, 1);
		m_sBoxGreen1.normal.background.SetPixel(1, 1, Color.green);
		m_sBoxGreen1.normal.background.wrapMode = TextureWrapMode.Repeat;
		m_sBoxGreen1.normal.background.Apply();
		m_sBoxRed1 = new GUIStyle(GUI.skin.label);
		m_sBoxRed1.normal.textColor = Color.black;
		m_sBoxRed1.fontSize = m_fillBar_fontSize;
		m_sBoxRed1.wordWrap = false;
		m_sBoxRed1.fontStyle = FontStyle.Italic;
		m_sBoxRed1.alignment = TextAnchor.MiddleRight;
		m_sBoxRed1.normal.background = new Texture2D(1, 1);
		m_sBoxRed1.normal.background.SetPixel(1, 1, Color.red);
		m_sBoxRed1.normal.background.wrapMode = TextureWrapMode.Repeat;
		m_sBoxRed1.normal.background.Apply();
		m_sBoxInvis1 = new GUIStyle(GUI.skin.label);
		m_sBoxInvis1.normal.background = new Texture2D(1, 1);
		m_sBoxInvis1.normal.background.SetPixel(1, 1, Color.clear);
		m_sBoxInvis1.normal.background.wrapMode = TextureWrapMode.Repeat;
		m_sBoxInvis1.normal.background.Apply();
	}

	private void UpdateAllocation()
	{
		m_TotalUsageLastCheck = 0f;
		m_TotalAllocationLastCheck = 0f;
		string text = "";
		m_MaxAllocationLastCheck = 0f;
		int i;
		for (i = 8; i < 31; i++)
		{
			text = LayerMask.LayerToName(i);
			if (text == "")
			{
				continue;
			}
			ComponentPool.Pool[] array = Singleton.Manager<ComponentPool>.inst.PoolLookup.Values.Where((ComponentPool.Pool r) => r.CategoryID == i).ToArray();
			if (array.Length != 0)
			{
				if (!m_AnalyticsGroups.ContainsKey(text))
				{
					m_AnalyticsGroups.Add(text, new CategoryAnalyticsGroup(0f));
				}
				m_AnalyticsGroups[text] = new CategoryAnalyticsGroup(0f);
				ComponentPool.Pool[] array2 = array;
				foreach (ComponentPool.Pool pool in array2)
				{
					m_AnalyticsGroups[text] = new CategoryAnalyticsGroup(m_AnalyticsGroups[text].AveUtilisation + pool.Utilisation, m_AnalyticsGroups[text].SumMemoryUsage + pool.MemoryUsage, m_AnalyticsGroups[text].SumMemoryAllocation + pool.MemoryAllocated, m_AnalyticsGroups[text].InstantiationTime + pool.instanciationTime);
				}
				m_MaxAllocationLastCheck = ((m_MaxAllocationLastCheck < m_AnalyticsGroups[text].SumMemoryAllocation) ? m_AnalyticsGroups[text].SumMemoryAllocation : m_MaxAllocationLastCheck);
				m_TotalUsageLastCheck += m_AnalyticsGroups[text].SumMemoryUsage;
				m_TotalAllocationLastCheck += m_AnalyticsGroups[text].SumMemoryAllocation;
				m_AnalyticsGroups[text] = new CategoryAnalyticsGroup(m_AnalyticsGroups[text].AveUtilisation / (float)array.Length, m_AnalyticsGroups[text].SumMemoryUsage, m_AnalyticsGroups[text].SumMemoryAllocation, m_AnalyticsGroups[text].InstantiationTime);
			}
		}
		m_AverageTotalUtilisationLastCheck = m_TotalUsageLastCheck / m_TotalAllocationLastCheck;
		m_MaxTotalUtilisationOverGame = (((m_MaxTotalUtilisationOverGame == -1f && m_TotalAllocationLastCheck != 0f) || m_MaxTotalUtilisationOverGame < m_TotalUsageLastCheck / m_TotalAllocationLastCheck) ? (m_TotalUsageLastCheck / m_TotalAllocationLastCheck) : m_MaxTotalUtilisationOverGame);
	}

	private void DrawFillBar(string title, CategoryAnalyticsGroup group, bool useScaleToMax, float instanciationTime)
	{
		DrawFillBar(title, group.AveUtilisation, group.SumMemoryUsage, group.SumMemoryAllocation, useScaleToMax, instanciationTime);
	}

	private void DrawFillBar(string title, float utilisation, float mbUsage, float mbAllocated, bool useScaleToMax, float instanciationTime)
	{
		float num = ((useScaleToMax && m_MaxAllocationLastCheck != 0f) ? (mbAllocated / (m_MaxAllocationLastCheck / 1000000f)) : 1f);
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent(title), m_sFillBarTitle1, GUILayout.Width(AreaSizePerc(m_fillBar_TitleScale).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent("<b>" + mbUsage.ToString("F1") + "</b>/<b>" + mbAllocated.ToString("F2") + "</b> MB"), m_sFillBarTitle2, GUILayout.Width(AreaSizePerc(m_fillBar_RawUsageScale).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent(""), m_sBoxGreen1, GUILayout.Width(AreaSizePerc(m_fillBar_BarScale * utilisation * num).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent(""), m_sBoxRed1, GUILayout.Width(AreaSizePerc(m_fillBar_BarScale * (1f - utilisation) * num).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent(""), m_sBoxInvis1, GUILayout.Width(AreaSizePerc(m_fillBar_BarScale * (1f - num)).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent((utilisation * 100f).ToString("F0") + "%"), m_sFillBarTitle1, GUILayout.Width(AreaSizePerc(m_fillBar_PercScale).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.Label(new GUIContent(instanciationTime.ToString("F2") + "s"), m_sFillBarTitle1, GUILayout.Width(AreaSizePerc(m_fillBar_DurationScale).x), GUILayout.Height(m_fillBar_YSize));
		GUILayout.EndHorizontal();
	}

	private void Update()
	{
		if (m_TimeSinceLastCheck > m_TimeBetweenChecks)
		{
			m_TimeSinceLastCheck = 0f;
			UpdateAllocation();
		}
		m_TimeSinceLastCheck += Time.unscaledDeltaTime;
	}

	private void OnGUI()
	{
		if (!m_stylesInstanced)
		{
			InstanciateStyles();
		}
		GUILayout.BeginArea(new Rect(m_screenPosition, m_areaSize));
		GUILayout.BeginVertical("Box");
		GUILayout.Label("Pooling Analytics", m_sHeader1);
		GUILayout.EndVertical();
		GUILayout.BeginVertical("Box");
		if (m_TimeSinceLastCheck == -1f)
		{
			UpdateAllocation();
			m_TimeSinceLastCheck = 0f;
		}
		string text = "";
		for (int i = 0; i < m_AnalyticsGroups.Count; i++)
		{
			text = m_AnalyticsGroups.Keys.ToArray()[i];
			DrawFillBar(text, m_AnalyticsGroups[text].AveUtilisation, m_AnalyticsGroups[text].SumMemoryUsage / 1000000f, m_AnalyticsGroups[text].SumMemoryAllocation / 1000000f, useScaleToMax: true, m_AnalyticsGroups[text].InstantiationTime);
		}
		GUILayout.EndVertical();
		GUILayout.BeginVertical("Box");
		DrawFillBar("Current Memory Usage", m_AverageTotalUtilisationLastCheck, m_TotalUsageLastCheck / 1000000f, m_TotalAllocationLastCheck / 1000000f, useScaleToMax: false, 0f);
		GUILayout.Label("Current Efficiency: <b>" + m_AverageTotalUtilisationLastCheck * 100f + "</b>%", m_sFillBarTitle1);
		GUILayout.Label("Highest Efficiency: <b>" + m_MaxTotalUtilisationOverGame * 100f + "</b>%", m_sFillBarTitle1);
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
