using UnityEngine;
using UnityEngine.UI;

public class UIMoneyHUD : UIHUDElement
{
	[SerializeField]
	private Text m_Text;

	private int m_WantedMoney;

	private float m_CurrentMoney;

	private float m_Speed;

	public bool IsCounting => (int)m_CurrentMoney != m_WantedMoney;

	public override void Show(object context)
	{
		base.Show(context);
		m_WantedMoney = Singleton.Manager<ManPlayer>.inst.GetCurrentMoney();
		m_CurrentMoney = m_WantedMoney;
		m_Text.text = Singleton.Manager<Localisation>.inst.GetMoneyString((int)m_CurrentMoney);
	}

	private void Update()
	{
		if (m_WantedMoney != Singleton.Manager<ManPlayer>.inst.GetCurrentMoney())
		{
			m_WantedMoney = Singleton.Manager<ManPlayer>.inst.GetCurrentMoney();
			m_Speed = Mathf.Abs((float)m_WantedMoney - m_CurrentMoney) / 3f;
		}
		if (IsCounting)
		{
			if ((float)m_WantedMoney < m_CurrentMoney)
			{
				m_CurrentMoney = Mathf.Clamp(m_CurrentMoney - Time.deltaTime * m_Speed, m_WantedMoney, m_CurrentMoney);
			}
			else
			{
				m_CurrentMoney = Mathf.Clamp(m_CurrentMoney + Time.deltaTime * m_Speed, m_CurrentMoney, m_WantedMoney);
			}
			m_Text.text = Singleton.Manager<Localisation>.inst.GetMoneyString((int)m_CurrentMoney);
		}
	}
}
