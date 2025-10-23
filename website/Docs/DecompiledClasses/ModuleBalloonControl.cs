using UnityEngine;

[RequireComponent(typeof(ModuleHUDSliderControl))]
public class ModuleBalloonControl : Module
{
	[SerializeField]
	[Tooltip("The readout dial that displays the current floatiness settings")]
	protected Transform m_GuageDial;

	[SerializeField]
	protected Vector3 m_MinFloatinessGuageLocalRotation;

	[SerializeField]
	protected Vector3 m_MaxFloatinessGuageLocalRotation;

	[SerializeField]
	private AnimationCurve m_GuageDialSlerpCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	[SerializeField]
	private float m_GuageDialSlerpDuration = 0.7f;

	[SerializeField]
	[HideInInspector]
	protected ModuleHUDSliderControl m_SliderControl;

	public Event<ModuleBalloonControl, float> FloatinessValueSetEvent;

	private bool m_SurpressSetEvent;

	private Quaternion m_GuageDial_Slerp_StartRotation = Quaternion.identity;

	private Quaternion m_GuageDial_Slerp_EndRotation = Quaternion.identity;

	private float m_SlerpTimeRemaining;

	private bool m_IsGuageDialSlerping;

	public float FloatinessSetting => m_SliderControl.AdjustableValueFulfillment01;

	public void PropogateFloatinessSetting(float floatinessValue)
	{
		m_SurpressSetEvent = true;
		m_SliderControl.SetValueLocally(m_SliderControl.GetValueFromRangeFulfillment(floatinessValue));
		m_SurpressSetEvent = false;
	}

	private Quaternion GetGuageDialLocalRotationForFloatiness(float floatiness01)
	{
		return Quaternion.Slerp(Quaternion.Euler(m_MinFloatinessGuageLocalRotation), Quaternion.Euler(m_MaxFloatinessGuageLocalRotation), floatiness01);
	}

	private void SetDialTargetFloatiness(float floatiness, bool instantly = false)
	{
		m_GuageDial_Slerp_StartRotation = m_GuageDial.localRotation;
		m_GuageDial_Slerp_EndRotation = GetGuageDialLocalRotationForFloatiness(floatiness);
		m_SlerpTimeRemaining = (instantly ? 0f : m_GuageDialSlerpDuration);
		m_IsGuageDialSlerping = true;
		UpdateGuageDialSlerp();
	}

	private void UpdateGuageDialSlerp()
	{
		if (m_IsGuageDialSlerping)
		{
			float num = 1f - ((m_GuageDialSlerpDuration != 0f) ? (m_SlerpTimeRemaining / m_GuageDialSlerpDuration) : 0f);
			m_GuageDial.localRotation = Quaternion.SlerpUnclamped(m_GuageDial_Slerp_StartRotation, m_GuageDial_Slerp_EndRotation, m_GuageDialSlerpCurve.Evaluate(num));
			m_SlerpTimeRemaining = Mathf.Max(0f, m_SlerpTimeRemaining - Time.deltaTime);
			if (num == 1f)
			{
				m_IsGuageDialSlerping = false;
			}
		}
	}

	private void OnFloatinessValueSet()
	{
		SetDialTargetFloatiness(FloatinessSetting);
		if (!m_SurpressSetEvent)
		{
			FloatinessValueSetEvent.Send(this, FloatinessSetting);
		}
	}

	private void OnFloatinessValueInstantRefresh()
	{
		SetDialTargetFloatiness(FloatinessSetting, instantly: true);
	}

	private void OnAttached()
	{
		base.block.tank.BlockStateController.RegisterBalloonController(this);
	}

	private void OnDetaching()
	{
		base.block.tank.BlockStateController.DeregisterBalloonController(this);
	}

	private void PrePool()
	{
		m_SliderControl = GetComponent<ModuleHUDSliderControl>();
	}

	private void OnPool()
	{
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_SliderControl.OptionSetEvent.Subscribe(OnFloatinessValueSet);
		m_SliderControl.InstantRefreshEvent.Subscribe(OnFloatinessValueInstantRefresh);
	}

	private void OnUpdate()
	{
		UpdateGuageDialSlerp();
	}
}
