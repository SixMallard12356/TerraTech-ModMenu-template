using UnityEngine;

public struct ButtonCombo
{
	private int[] m_RewiredActions;

	private int m_AllPressedFlag;

	private int m_LastUpdateFrame;

	private int m_ActionDownFlags;

	private float m_FirstButtonDownTime;

	private int m_AllPressedFrame;

	private float m_FirstButtonReleaseTime;

	private int m_AllReleasedFrame;

	private const float k_TimeForCombinationPress = 0.25f;

	private const float k_TimeForCombinationRelease = 0.25f;

	public ButtonCombo(params int[] rewiredActions)
	{
		m_RewiredActions = rewiredActions;
		m_AllPressedFlag = (int)Mathf.Pow(2f, m_RewiredActions.Length) - 1;
		m_LastUpdateFrame = -1;
		m_ActionDownFlags = 0;
		m_FirstButtonDownTime = 0f;
		m_AllPressedFrame = 0;
		m_FirstButtonReleaseTime = 0f;
		m_AllReleasedFrame = 0;
	}

	public bool IsJustPressed()
	{
		UpdateIfNeeded();
		return m_AllPressedFrame == Time.frameCount;
	}

	public bool IsPressed()
	{
		UpdateIfNeeded();
		if (m_AllReleasedFrame < m_AllPressedFrame)
		{
			return Time.frameCount >= m_AllPressedFrame;
		}
		return false;
	}

	public bool IsJustReleased()
	{
		UpdateIfNeeded();
		return m_AllReleasedFrame == Time.frameCount;
	}

	public void Reset()
	{
		m_ActionDownFlags = 0;
		m_FirstButtonDownTime = 0f;
		m_AllPressedFrame = 0;
		m_FirstButtonReleaseTime = 0f;
		m_AllReleasedFrame = 0;
	}

	private void UpdateIfNeeded()
	{
		int frameCount = Time.frameCount;
		if (m_LastUpdateFrame == frameCount)
		{
			return;
		}
		m_LastUpdateFrame = frameCount;
		int num = 0;
		bool flag = true;
		for (int i = 0; i < m_RewiredActions.Length; i++)
		{
			int rewiredAction = m_RewiredActions[i];
			if (Singleton.Manager<ManInput>.inst.GetButton(rewiredAction))
			{
				num |= 1 << i;
				flag = flag && Singleton.Manager<ManInput>.inst.GetButtonDown(rewiredAction);
			}
		}
		bool num2 = (num & ~m_ActionDownFlags) > 0;
		bool flag2 = (m_ActionDownFlags & ~num) > 0;
		float time = Time.time;
		if (num2)
		{
			bool flag3 = false;
			if (m_ActionDownFlags == 0 && flag)
			{
				m_FirstButtonDownTime = time;
			}
			else if (time > m_FirstButtonDownTime + 0.25f)
			{
				flag3 = true;
				m_FirstButtonDownTime = 0f;
				m_ActionDownFlags = 0;
			}
			if (!flag3)
			{
				m_ActionDownFlags = num;
				if (m_ActionDownFlags == m_AllPressedFlag)
				{
					m_FirstButtonDownTime = 0f;
					m_AllPressedFrame = Time.frameCount;
				}
			}
		}
		else if (flag2)
		{
			if (m_ActionDownFlags == m_AllPressedFlag)
			{
				m_FirstButtonReleaseTime = time;
			}
			if (time > m_FirstButtonReleaseTime + 0.25f)
			{
				m_FirstButtonReleaseTime = 0f;
				m_AllReleasedFrame = 0;
			}
			else if (num == 0)
			{
				m_FirstButtonReleaseTime = 0f;
				m_AllReleasedFrame = Time.frameCount;
			}
			m_ActionDownFlags = num;
		}
	}
}
