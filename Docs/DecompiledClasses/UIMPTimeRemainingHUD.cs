#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIMPTimeRemainingHUD : UIHUDElement
{
	[SerializeField]
	private Text m_TimeRemaining;

	[SerializeField]
	private Color m_NormalTextColour = Color.white;

	[SerializeField]
	private Color m_TimeRunningOutTextColour = Color.red;

	[SerializeField]
	private float m_TimeRunningOutThreshold = 15f;

	private string m_lastTimeRemainingString;

	private float m_lastTimeRemainingSecs;

	private UIMultiplayerHUD m_MultiHud;

	public void InitTimeRemaining()
	{
		m_MultiHud = (UIMultiplayerHUD)Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Multiplayer);
		d.Assert(m_MultiHud != null);
		m_lastTimeRemainingString = "";
		m_lastTimeRemainingSecs = _getTimeRemainingInSecs();
		string tm = _getTimeRemaining();
		_setTimeRemaining(tm);
		m_TimeRemaining.color = m_NormalTextColour;
	}

	public override void Hide(object context)
	{
		base.Hide(context);
	}

	private void Update()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
		{
			float num = _getTimeRemainingInSecs();
			string text = _getTimeRemaining();
			if (!text.Equals(m_lastTimeRemainingString))
			{
				_setTimeRemaining(text);
				if (m_lastTimeRemainingSecs > m_TimeRunningOutThreshold && num <= m_TimeRunningOutThreshold)
				{
					m_TimeRemaining.color = m_TimeRunningOutTextColour;
					m_lastTimeRemainingSecs = num;
				}
			}
			if (num < m_TimeRunningOutThreshold)
			{
				bool flag = num % 1f > 0.5f;
				if (m_TimeRemaining.gameObject.activeSelf != flag)
				{
					m_TimeRemaining.gameObject.SetActive(flag);
				}
			}
		}
		else
		{
			m_TimeRemaining.gameObject.SetActive(value: true);
		}
	}

	private void _setTimeRemaining(string tm)
	{
		m_TimeRemaining.text = tm;
		m_lastTimeRemainingString = tm;
	}

	private float _getTimeRemainingInSecs()
	{
		return Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhaseTimer;
	}

	private string _getTimeRemaining()
	{
		float num = _getTimeRemainingInSecs();
		int num2 = Mathf.FloorToInt(num / 60f);
		int num3 = Mathf.FloorToInt(num % 60f);
		return num2 + ":" + num3.ToString("D2");
	}

	private void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.SkinsPalette);
	}
}
