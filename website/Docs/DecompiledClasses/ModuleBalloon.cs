#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ModuleBalloon : Module, TechAudio.IModuleAudioProvider
{
	private struct PeriodicDamageEventInfo
	{
		public float Damage;

		public float LapseTime;
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float m_Floatiness01;
	}

	[SerializeField]
	protected SkinnedMeshRenderer m_BagMesh;

	[SerializeField]
	[Tooltip("The density of the gas that fills this balloon to provide lift, in kg/m^3, and at maxmimum 'floatiness'.When at minimum floatiness the desnity of gas will instead be the air density of the current gamemode at sealevel. This value should be lower than the air density if we want the balloon to provide lift. For reference: Air is ~1.22, while hydrogen is ~0.083 at sea level in standard conditions in real life.")]
	protected float m_GasDensity = 0.083f;

	[Tooltip("The threshold of damage that if received within a 1 second window will cause the balloon to pop")]
	[SerializeField]
	private float m_PopAfterDPSReceivedThreshold = 50f;

	[Tooltip("How long the balloon will be unuseable for after a pop")]
	[SerializeField]
	private float m_PopRecoveryTime = 10f;

	[SerializeField]
	[Tooltip("How long it takes the bag to visually regrow after recovering from a pop. If 0, it will instantly regenerate")]
	private float m_BagRegenerationSpeed;

	[SerializeField]
	private Transform m_PopParticles;

	[SerializeField]
	private Transform m_PopVFXOrigin;

	[SerializeField]
	private TechAudio.SFXType m_InflationDeflationSFX = TechAudio.SFXType.SJBalloonInflate;

	[HideInInspector]
	[SerializeField]
	private Damageable m_Damageable;

	protected BalloonVolume[] m_Volumes = new BalloonVolume[0];

	protected const float k_BuoyancyMultiplier = 20f;

	protected const float k_BuoyancyDamping = 75f;

	public const float k_DefaultRestingFloatiness = 0.8f;

	protected const float k_DefaultSpawnFloatiness = 0f;

	protected const float k_FloatinessAdjustmentSpeed = 0.1f;

	protected float m_Floatiness01 = float.NegativeInfinity;

	protected float m_FloatinessDeltaThisFrame;

	protected float m_ControlInput_FloatinessTarget = float.NegativeInfinity;

	private Queue<PeriodicDamageEventInfo> m_LastSecondDamageInfo = new Queue<PeriodicDamageEventInfo>();

	private float m_PopTime = float.NegativeInfinity;

	private bool m_IsBagRegenerating;

	public TechAudio.SFXType SFXType => m_InflationDeflationSFX;

	public bool IsPopped { get; private set; }

	protected bool ShouldBePopped => m_PopTime + m_PopRecoveryTime > Time.time;

	protected bool IsFloatinessChanging => Mathf.Abs(m_FloatinessDeltaThisFrame) > 0.001f;

	protected bool IsInflating => m_FloatinessDeltaThisFrame > 0f;

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	public Vector3 CalculateLiftForce(out Vector3 centerOfLiftWorld)
	{
		d.AssertFormat(base.block.TryQueryActingRigidbody(out var actingBody), "Tried to query balloon [{0}] rigidbody but we got none! Why? (FIX THIS)", base.transform.GetTransformHeirarchyPath());
		return CalculateLiftForce(actingBody, out centerOfLiftWorld);
	}

	public Vector3 CalculateLiftForce(Rigidbody rbody, out Vector3 centerOfLiftWorld)
	{
		centerOfLiftWorld = Vector3.zero;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		float num = 0f;
		float currentModeAtmosphereDensityAtSeaLevel = Singleton.Manager<ManGameMode>.inst.GetCurrentModeAtmosphereDensityAtSeaLevel();
		BalloonVolume[] volumes = m_Volumes;
		foreach (BalloonVolume obj in volumes)
		{
			Vector3 buoyantForceFromGasDensity = obj.GetBuoyantForceFromGasDensity(Mathf.Lerp(currentModeAtmosphereDensityAtSeaLevel, m_GasDensity, m_Floatiness01));
			float magnitude = buoyantForceFromGasDensity.magnitude;
			Vector3 position = obj.transform.position;
			zero += position;
			if (!magnitude.Approximately(0f))
			{
				centerOfLiftWorld += position * magnitude;
				zero2 += buoyantForceFromGasDensity;
				num += magnitude;
			}
		}
		if (num.Approximately(0f))
		{
			centerOfLiftWorld = zero / m_Volumes.Length;
		}
		else
		{
			centerOfLiftWorld /= num;
		}
		Vector3 vector = zero2 * 20f;
		Vector3 vector2 = -(75f * rbody.GetSpeedInDirection(BalloonVolume.s_DirectionOfLift) * BalloonVolume.s_DirectionOfLift.normalized);
		return vector + vector2;
	}

	private void SetFloatinessInternal(float floatiness01, bool resetPhysics = true)
	{
		floatiness01 = Mathf.Clamp01(floatiness01);
		if (m_Floatiness01 != floatiness01)
		{
			m_Floatiness01 = floatiness01;
			m_BagMesh.SetBlendShapeWeight(0, m_Floatiness01 * 100f);
			if (resetPhysics && base.block.IsAttached)
			{
				base.block.tank.ResetPhysics(SendEventUpdate: true);
			}
		}
	}

	private float GetTargetFloatiness01()
	{
		if (IsPopped)
		{
			return 0f;
		}
		if (!float.IsNegativeInfinity(m_ControlInput_FloatinessTarget))
		{
			return m_ControlInput_FloatinessTarget;
		}
		if (base.block.IsAttached && !float.IsNegativeInfinity(base.block.tank.BlockStateController.BalloonFloatinessSetting01))
		{
			return base.block.tank.BlockStateController.BalloonFloatinessSetting01;
		}
		return 0.8f;
	}

	private void Pop()
	{
		IsPopped = true;
		m_PopTime = Time.time;
		m_BagMesh.gameObject.SetActive(value: false);
		m_PopParticles.Spawn(m_PopVFXOrigin.position, m_PopVFXOrigin.rotation);
		SetFloatinessInternal(0f);
	}

	private void UnPop(bool instantly = false)
	{
		IsPopped = false;
		m_PopTime = float.NegativeInfinity;
		m_BagMesh.gameObject.SetActive(value: true);
		if (instantly || m_BagRegenerationSpeed == 0f)
		{
			m_BagMesh.transform.localScale = Vector3.one;
			m_IsBagRegenerating = false;
		}
		else
		{
			m_BagMesh.transform.localScale = Vector3.zero;
			m_IsBagRegenerating = true;
		}
	}

	private void TryRegenerateBagMesh()
	{
		if (m_IsBagRegenerating)
		{
			m_BagMesh.transform.localScale = Vector3.Lerp(m_BagMesh.transform.localScale, Vector3.one, Time.deltaTime * m_BagRegenerationSpeed);
			if ((m_BagMesh.transform.localScale - Vector3.one).sqrMagnitude < 0.001f)
			{
				m_IsBagRegenerating = false;
			}
		}
	}

	private void EvaluateLastSecondDamage()
	{
		if (!IsPopped)
		{
			while (m_LastSecondDamageInfo.Count > 0 && m_LastSecondDamageInfo.Peek().LapseTime < Time.time)
			{
				m_LastSecondDamageInfo.Dequeue();
			}
			if (m_LastSecondDamageInfo.Sum((PeriodicDamageEventInfo r) => r.Damage) > m_PopAfterDPSReceivedThreshold)
			{
				Pop();
			}
		}
	}

	private void Reset()
	{
		m_ControlInput_FloatinessTarget = float.NegativeInfinity;
		SetFloatinessInternal(0.8f, resetPhysics: false);
		m_LastSecondDamageInfo.Clear();
		UnPop(instantly: true);
		m_FloatinessDeltaThisFrame = 0f;
	}

	private void OnDriveControlInput(TankControl.ControlState control)
	{
		if (control.InputMovement.y == 0f)
		{
			m_ControlInput_FloatinessTarget = float.NegativeInfinity;
			return;
		}
		Vector3 direction = control.InputMovement.y * Vector3.up;
		float num = Vector3.Dot(base.block.tank.transform.TransformDirection(direction), BalloonVolume.s_DirectionOfLift);
		m_ControlInput_FloatinessTarget = ((num > 0f) ? 1 : 0);
	}

	private void OnSFXUpdate()
	{
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, SFXType, IsFloatinessChanging, IsFloatinessChanging ? 1f : 0f, 0f);
			Extensions.Send(value2: new FMODEvent.FMODParams("Inflation", m_Floatiness01), action: this.OnAudioTickUpdate, value1: value);
		}
	}

	private void OnDamaged(ManDamage.DamageInfo damageInfo)
	{
		if (!IsPopped)
		{
			m_LastSecondDamageInfo.Enqueue(new PeriodicDamageEventInfo
			{
				Damage = damageInfo.Damage,
				LapseTime = Time.time + 1f
			});
		}
	}

	private void OnAttached()
	{
		base.block.tank.BlockStateController.RegisterBalloon(this);
		base.block.tank.TechAudio.AddModule(this);
		base.block.tank.control.driveControlEvent.Subscribe(OnDriveControlInput);
	}

	private void OnDetaching()
	{
		base.block.tank.BlockStateController.DeregisterBalloon(this);
		base.block.tank.TechAudio.RemoveModule(this);
		base.block.tank.control.driveControlEvent.Unsubscribe(OnDriveControlInput);
		Reset();
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.m_Floatiness01 = m_Floatiness01;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			SetFloatinessInternal(serialData2.m_Floatiness01);
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(GetType(), "ModuleBalloon_m_Floatiness", m_Floatiness01.ToString(CultureInfo.InvariantCulture));
			return;
		}
		string text = context.Retrieve(GetType(), "ModuleBalloon_m_Floatiness");
		if (!text.NullOrEmpty())
		{
			if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
			{
				SetFloatinessInternal(result);
				return;
			}
			d.LogError("ModuleBalloon.OnSerializeText - Failed to parse m_Floatiness setting from save data on block '" + base.block.name + "'. Expected float value 0-1 but got '" + text + "'. Leaving as default.");
		}
	}

	private void PrePool()
	{
		m_Damageable = GetComponent<Damageable>();
	}

	private void OnPool()
	{
		m_Volumes = GetComponentsInChildren<BalloonVolume>();
		m_Damageable.damageEvent.Subscribe(OnDamaged);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		Reset();
		SetFloatinessInternal(0f);
	}

	private void OnRecycle()
	{
		Reset();
	}

	private void OnUpdate()
	{
		m_FloatinessDeltaThisFrame = 0f;
		float targetFloatiness = GetTargetFloatiness01();
		if (m_Floatiness01 != targetFloatiness)
		{
			float f = targetFloatiness - m_Floatiness01;
			m_FloatinessDeltaThisFrame = Mathf.Min(Mathf.Abs(f), 0.1f * Time.deltaTime) * Mathf.Sign(f);
			SetFloatinessInternal(m_Floatiness01 + m_FloatinessDeltaThisFrame);
		}
		OnSFXUpdate();
		EvaluateLastSecondDamage();
		if (IsPopped && !ShouldBePopped)
		{
			UnPop();
		}
		TryRegenerateBagMesh();
	}

	private void OnFixedUpdate()
	{
		if (base.block.tank != null && base.block.tank.beam.IsActive)
		{
			return;
		}
		if (base.block.TryQueryActingRigidbody(out var actingBody))
		{
			Vector3 centerOfLiftWorld;
			Vector3 vector = CalculateLiftForce(actingBody, out centerOfLiftWorld);
			if (!base.block.IsAttached)
			{
				vector *= 0.005f;
			}
			actingBody.AddForceAtPosition(vector * Time.fixedDeltaTime, centerOfLiftWorld);
		}
		else
		{
			d.LogError("No rigidbody found for balloon update! No bueno!");
		}
	}
}
