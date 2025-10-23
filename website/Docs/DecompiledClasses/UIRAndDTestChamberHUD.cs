using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIRAndDTestChamberHUD : UIHUDElement
{
	[SerializeField]
	[FormerlySerializedAs("m_Speed")]
	private Text m_SpeedLabel;

	[SerializeField]
	[FormerlySerializedAs("m_TopSpeed")]
	private Text m_TopSpeedLabel;

	private int m_TopSpeed;

	public override void Show(object context)
	{
		base.Show(context);
		SetTopSpeed(0f);
	}

	private void SetSpeed(float speed)
	{
		m_SpeedLabel.text = speed.ToString("000") + " mph";
	}

	private void SetTopSpeed(float speed)
	{
		m_TopSpeedLabel.text = speed.ToString("000") + " mph";
	}

	private void Update()
	{
		if ((bool)Singleton.playerTank)
		{
			int num = (int)(Singleton.playerTank.rbody.velocity.magnitude * 2.2369363f);
			SetSpeed(num);
			if (num > m_TopSpeed)
			{
				m_TopSpeed = num;
				SetTopSpeed(num);
			}
		}
	}
}
