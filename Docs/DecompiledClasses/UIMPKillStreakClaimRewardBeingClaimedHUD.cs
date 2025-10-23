using UnityEngine;
using UnityEngine.UI;

public class UIMPKillStreakClaimRewardBeingClaimedHUD : UIHUDElement
{
	public Text m_KillCountLabel;

	public Text m_QuantityLabel;

	public Text m_MaxedLabel;

	public Image m_BlockIcon;

	[Range(0.1f, 5f)]
	public float m_AnimationDuration = 0.5f;

	[Range(10f, 2880f)]
	public float m_SpinRatePerSec = 1440f;

	[Range(10f, 2000f)]
	public float m_SplinePathHorzDisplacement = 50f;

	[Range(10f, 2000f)]
	public float m_SplinePathVertDisplacement = 800f;

	private Vector3 m_splinePt1;

	private Vector3 m_splinePt2;

	private Vector3 m_splinePt3;

	private Vector3 m_splinePt4;

	private float m_splineLambda;

	public void InitKillStreakRewardBeingClaimed(Vector3 startLocalPos, Vector3 endLocalPos, int nKillStreak, bool isMaxed, Sprite blockIcon, int qty)
	{
		m_KillCountLabel.text = nKillStreak.ToString();
		m_QuantityLabel.text = HUD_KillStreakRewardItem.FormatKillStreakRewardQuantity(qty);
		m_MaxedLabel.gameObject.SetActive(isMaxed);
		m_BlockIcon.sprite = blockIcon;
		Vector3 vector = endLocalPos - startLocalPos;
		m_splinePt1 = startLocalPos;
		m_splinePt2 = startLocalPos - new Vector3(Mathf.Sign(vector.x) * m_SplinePathHorzDisplacement, m_SplinePathVertDisplacement, 0f);
		m_splinePt3 = endLocalPos - new Vector3(Mathf.Sign(vector.x) * m_SplinePathHorzDisplacement * -1f, m_SplinePathVertDisplacement, 0f);
		m_splinePt4 = endLocalPos;
		m_splineLambda = 0f;
		base.transform.localPosition = startLocalPos;
		base.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		Show(null);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
	}

	private void Update()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
		{
			float num = Time.deltaTime / m_AnimationDuration;
			m_splineLambda = Mathf.Clamp01(m_splineLambda + num);
			Vector3 localPosition = CatmullRom.CalcCatmullRomPos(m_splinePt2, m_splinePt1, m_splinePt4, m_splinePt3, m_splineLambda);
			base.transform.localPosition = localPosition;
			float num2 = Time.deltaTime * m_SpinRatePerSec;
			Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
			eulerAngles.z += num2;
			base.transform.localRotation = Quaternion.Euler(eulerAngles);
			float num3 = Mathf.Lerp(1f, 0.02f, m_splineLambda);
			base.transform.localScale = new Vector3(num3, num3, num3);
			if (m_splineLambda >= 1f)
			{
				HideSelf();
			}
		}
		else
		{
			HideSelf();
		}
	}
}
