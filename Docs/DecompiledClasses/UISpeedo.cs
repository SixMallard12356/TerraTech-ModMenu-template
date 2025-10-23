using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpeedo : UIHUDElement
{
	private enum IconState
	{
		Set,
		FadingOut,
		FadingIn
	}

	[SerializeField]
	private Text m_Speed;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private Sprite m_GroundIcon;

	[SerializeField]
	private Sprite m_AirIcon;

	[SerializeField]
	private float m_ModeSwitchWaitTime;

	[SerializeField]
	private float m_IconFadeTime = 0.5f;

	private bool m_IsGroundSpeed;

	private float m_Timer;

	private float m_IconSwitchTimer;

	private IconState m_IconState;

	private Tank m_Tank;

	private List<ModuleSpeedo> m_Speedos = new List<ModuleSpeedo>();

	private bool m_ShowingFromUI;

	public override void Show(object moduleObject)
	{
		base.Show(moduleObject);
		ModuleSpeedo moduleSpeedo = moduleObject as ModuleSpeedo;
		if ((bool)moduleSpeedo)
		{
			m_Speedos.Add(moduleSpeedo);
			m_Tank = moduleSpeedo.block.tank;
		}
		else
		{
			m_ShowingFromUI = true;
			m_Tank = Singleton.playerTank;
		}
		m_Timer = 0f;
		m_IconState = IconState.Set;
		m_IconSwitchTimer = 0f;
		if ((bool)m_Tank)
		{
			m_IsGroundSpeed = m_Tank.grounded;
		}
	}

	public override void Hide(object moduleObject)
	{
		ModuleSpeedo moduleSpeedo = moduleObject as ModuleSpeedo;
		if ((bool)moduleSpeedo)
		{
			m_Speedos.Remove(moduleSpeedo);
		}
		if (!m_ShowingFromUI && m_Speedos.Count == 0)
		{
			base.Hide(moduleObject);
		}
	}

	private void SetIcon()
	{
		switch (m_IconState)
		{
		case IconState.Set:
			m_Icon.sprite = (m_IsGroundSpeed ? m_GroundIcon : m_AirIcon);
			m_Icon.color = m_Icon.color.SetAlpha(1f);
			m_IconSwitchTimer = m_IconFadeTime;
			break;
		case IconState.FadingOut:
			m_Icon.color = m_Icon.color.SetAlpha(m_IconSwitchTimer / m_IconFadeTime);
			if (m_IconSwitchTimer <= 0f)
			{
				m_IconState = IconState.FadingIn;
				m_Icon.sprite = (m_IsGroundSpeed ? m_GroundIcon : m_AirIcon);
			}
			m_IconSwitchTimer = Mathf.Max(m_IconSwitchTimer - Time.deltaTime, 0f);
			break;
		case IconState.FadingIn:
			m_Icon.color = m_Icon.color.SetAlpha(m_IconSwitchTimer / m_IconFadeTime);
			if (m_IconSwitchTimer >= m_IconFadeTime)
			{
				m_IconState = IconState.Set;
			}
			m_IconSwitchTimer = Mathf.Min(m_IconSwitchTimer + Time.deltaTime, m_IconFadeTime);
			break;
		}
	}

	private void Update()
	{
		if (m_ShowingFromUI)
		{
			m_Tank = Singleton.playerTank;
		}
		if (!m_Tank)
		{
			return;
		}
		if (m_IsGroundSpeed != m_Tank.grounded)
		{
			if (m_Timer >= m_ModeSwitchWaitTime)
			{
				m_IsGroundSpeed = m_Tank.grounded;
				switch (m_IconState)
				{
				case IconState.Set:
					m_IconState = IconState.FadingOut;
					break;
				case IconState.FadingOut:
					m_IconState = IconState.FadingIn;
					break;
				case IconState.FadingIn:
					m_IconState = IconState.FadingOut;
					break;
				}
			}
			m_Timer += Time.deltaTime;
		}
		else
		{
			m_Timer = 0f;
		}
		SetIcon();
		float speed = GameUnits.GetSpeed(m_Tank.GetForwardSpeed());
		m_Speed.text = $"{Mathf.RoundToInt(speed)}";
	}

	private void OnRecycle()
	{
		m_Speedos.Clear();
		m_ShowingFromUI = false;
	}
}
