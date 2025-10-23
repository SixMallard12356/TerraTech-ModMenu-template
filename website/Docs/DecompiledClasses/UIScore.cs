using UnityEngine;
using UnityEngine.UI;

public class UIScore : UIHUDElement
{
	public Text m_ScoreText;

	public Image m_Image;

	public float m_MinimumChangeSpeed = 1f;

	public float m_MaximumChangeSpeed;

	public float m_Modulation;

	private float m_TargetScore;

	private float m_CurrentScore;

	private string m_Format = "x {0:0}";

	private CanvasGroup m_MyCanvas;

	private bool m_AllowUnscalledTime;

	public void Init(int initalScore, float minSpeed = 1f, float maxSpeed = 1f, float modulation = 1f)
	{
		m_MinimumChangeSpeed = minSpeed;
		m_MaximumChangeSpeed = maxSpeed;
		m_Modulation = modulation;
		m_CurrentScore = initalScore;
		m_TargetScore = initalScore;
		SetText();
		if ((bool)m_Image)
		{
			m_Image.sprite = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Chunk, 26);
		}
	}

	public void SetIntial(int score)
	{
		m_TargetScore = score;
		m_CurrentScore = score;
		SetText();
	}

	public void SetMaxSpeed(float maxSpeed)
	{
		m_MaximumChangeSpeed = maxSpeed;
	}

	public void SetScore(int score)
	{
		m_TargetScore = score;
	}

	public void SetFormat(string format)
	{
		m_Format = format;
		SetText();
	}

	public void SetAlpha(float alpha)
	{
		if (!m_MyCanvas)
		{
			m_MyCanvas = GetComponentsInChildren<CanvasGroup>(includeInactive: true)[0];
		}
		m_MyCanvas.alpha = alpha;
	}

	public void AllowUnscaledTime(bool allow)
	{
		m_AllowUnscalledTime = allow;
	}

	private float GetDeltaTime()
	{
		if (m_AllowUnscalledTime)
		{
			return Time.unscaledDeltaTime;
		}
		return Time.deltaTime;
	}

	private void Update()
	{
		if (IsCounting())
		{
			float b = m_MaximumChangeSpeed;
			if (m_MaximumChangeSpeed != m_MinimumChangeSpeed)
			{
				b = Mathf.Min(m_MaximumChangeSpeed, Mathf.Abs(m_TargetScore - m_CurrentScore) * m_Modulation);
			}
			float num = Mathf.Max(1f / m_MinimumChangeSpeed, b);
			if (m_TargetScore < m_CurrentScore)
			{
				m_CurrentScore = Mathf.Max(m_CurrentScore - GetDeltaTime() * num, m_TargetScore);
			}
			else
			{
				m_CurrentScore = Mathf.Min(m_CurrentScore + GetDeltaTime() * num, m_TargetScore);
			}
			SetText();
		}
	}

	private void SetText()
	{
		string text = string.Format(m_Format, m_CurrentScore);
		if (m_ScoreText.text != text)
		{
			m_ScoreText.text = text;
		}
	}

	public bool IsCounting()
	{
		return m_CurrentScore != m_TargetScore;
	}

	private void OnDisable()
	{
		SetAlpha(1f);
	}
}
