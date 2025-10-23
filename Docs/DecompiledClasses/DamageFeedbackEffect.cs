using UnityEngine;

public class DamageFeedbackEffect : MonoBehaviour
{
	[SerializeField]
	private float m_DamageFeedbackDuration = 0.5f;

	[SerializeField]
	private float m_DamageFeedbackMaxDamage;

	[SerializeField]
	private float m_CameraShakeFrequency = 3f;

	[SerializeField]
	private float m_CameraShakeEffectStrength = 1f;

	[SerializeField]
	private AnimationCurve m_ChromaticAberrationIntensityOverTime;

	[SerializeField]
	private float m_ChromaticAberrationEffectStrength = 1f;

	private float m_DamageFeedbackCurrentDamageAmount;

	private float m_DamageFeedbackCurrentStartTime;

	public bool Enabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			if (base.enabled)
			{
				ClearPlayerDamageFeedback();
			}
			base.enabled = value;
		}
	}

	private void UpdatePlayerDamageFeedback()
	{
		if (!Singleton.Manager<ManPauseGame>.inst.IsPaused && m_DamageFeedbackCurrentDamageAmount > 0f)
		{
			float num = Time.time - m_DamageFeedbackCurrentStartTime;
			if (num >= m_DamageFeedbackDuration)
			{
				ClearPlayerDamageFeedback();
				return;
			}
			float time = num / m_DamageFeedbackDuration;
			float chromaticAberrationIntensity = m_DamageFeedbackCurrentDamageAmount * m_ChromaticAberrationEffectStrength * m_ChromaticAberrationIntensityOverTime.Evaluate(time);
			Singleton.Manager<CameraManager>.inst.SetChromaticAberrationIntensity(chromaticAberrationIntensity);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.CA, enabled: true);
		}
	}

	private void ClearPlayerDamageFeedback()
	{
		m_DamageFeedbackCurrentDamageAmount = 0f;
		Singleton.Manager<CameraManager>.inst.GetCamera<TankCamera>().SetCameraShake(0f, 0f, 0f);
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.CA, enabled: false);
	}

	private void OnTankDamage(Tank tank, ManDamage.DamageInfo info)
	{
		if (tank == Singleton.playerTank && m_DamageFeedbackDuration > 0f && m_DamageFeedbackMaxDamage > 0f && info.Damage > m_DamageFeedbackCurrentDamageAmount)
		{
			m_DamageFeedbackCurrentDamageAmount = Mathf.Min(info.Damage, m_DamageFeedbackMaxDamage);
			m_DamageFeedbackCurrentStartTime = Time.time;
			Singleton.Manager<CameraManager>.inst.GetCamera<TankCamera>().SetCameraShake(m_DamageFeedbackDuration, m_DamageFeedbackCurrentDamageAmount * m_CameraShakeEffectStrength, m_CameraShakeFrequency);
		}
	}

	private void OnCameraSwitch(CameraManager.Camera oldCamera, CameraManager.Camera newCamera)
	{
		ClearPlayerDamageFeedback();
	}

	private void Start()
	{
		Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Subscribe(OnTankDamage);
		Singleton.Manager<CameraManager>.inst.CameraSwitchEvent.Subscribe(OnCameraSwitch);
	}

	private void Update()
	{
		UpdatePlayerDamageFeedback();
	}
}
