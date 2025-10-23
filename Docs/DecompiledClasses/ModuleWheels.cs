#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleWheels : Module, ManVisible.StateVisualiser.Provider
{
	private new class SerialData : SerialData<SerialData>
	{
		public Dictionary<string, WarningHolder> warnings;
	}

	[Flags]
	public enum DbgPlotFlags
	{
		Torque = 1,
		Brake = 2,
		Compression = 4,
		RPM = 8
	}

	public struct AttachData
	{
		private ModuleWheels m_Module;

		public float inertia { get; private set; }

		public int numWheels { get; private set; }

		public Tank tech => m_Module.block.tank;

		public ManWheels.TorqueParams torqueParams => m_Module.m_TorqueParams;

		public ManWheels.WheelParams wheelParams => m_Module.m_WheelParams;

		public float driveTurnPower => m_Module.m_DriveTurnPower;

		public float driveTurnBrake => m_Module.m_DriveTurnBrake;

		public float driveTurnDifferential => m_Module.m_DriveTurnDifferential;

		public float turnOnSpotPower => m_Module.m_TurnOnSpotPower;

		public AttachData(ModuleWheels module, float i, int n)
		{
			m_Module = module;
			inertia = i;
			numWheels = n;
		}
	}

	[SerializeField]
	public ManWheels.TorqueParams m_TorqueParams = new ManWheels.TorqueParams
	{
		torqueCurveMaxTorque = 100f,
		torqueCurveMaxRpm = 400f,
		torqueCurveDrive = null,
		passiveBrakeMaxTorque = 50f,
		reverseBrakeMaxRpm = 50f,
		basicFrictionTorque = 5f,
		fullCompressFrictionTorque = 50f
	};

	[SerializeField]
	public ManWheels.WheelParams m_WheelParams = new ManWheels.WheelParams
	{
		tireProperties = null,
		radius = 0.5f,
		thicknessAngular = 20f,
		suspensionSpring = 200f,
		suspensionDamper = 80f,
		suspensionQuadratic = true,
		suspensionTravel = 0.2f,
		steerAngleMax = 0f,
		steerSpeed = 2f
	};

	[SerializeField]
	public float m_DriveTurnPower = 1f;

	[SerializeField]
	public float m_DriveTurnBrake;

	[SerializeField]
	public float m_DriveTurnDifferential = 1f;

	[SerializeField]
	public float m_TurnOnSpotPower = 1f;

	[SerializeField]
	public float m_DustMinimumRPM = 20f;

	[SerializeField]
	public ParticleSystem m_DustParticlesPrefab;

	[SerializeField]
	public ParticleSystem m_SuspensionSparkParticlesPrefab;

	[SerializeField]
	public Transform m_SuspensionSparksTransform;

	[SerializeField]
	public float m_SuspensionWarningWaitTime = 5f;

	[SerializeField]
	public Transform[] m_WheelGeometry;

	[SerializeField]
	public bool m_UseTireTracks = true;

	[SerializeField]
	public ManTireTracks.WheelType m_WheelTrackType;

	[SerializeField]
	public TechAudio.WheelTypes m_AudioType;

	private bool m_Enabled;

	private bool m_Animated;

	private float m_WarningWaitTimer;

	private Dictionary<string, WarningHolder> m_Warnings = new Dictionary<string, WarningHolder>();

	private List<ManWheels.Wheel> m_Wheels = new List<ManWheels.Wheel>();

	[HideInInspector]
	[SerializeField]
	private GameObject[] m_WheelGeometryColliderObjects;

	private float m_TireTrackWidth;

	private int[] m_PrevTireTrackIndex;

	private ManTireTracks.TrackGroup m_PrevTrackType;

	private float m_TracksSkidSpeed = 0.1f;

	private float m_TracksMaxSkidSpeed = 1f;

	private float m_TracksMinSpeed = 5f;

	private float m_TracksMaxSpeed = 20f;

	private bool m_HasWheelParticles;

	private WorldSpaceParticleSystem m_WheelParticlesWorldObject;

	private static readonly bool k_TracksUseForwardSpeed = true;

	private static readonly bool k_TracksUseIntensityFromSpeed = true;

	private static readonly bool k_TracksUseIntensityFromCompression = true;

	private static readonly bool k_TracksApplyVelocity = true;

	private static int s_LayerConnected;

	private static int s_LayerDisconnected;

	private List<GameObject> s_TmpPerWheelGeometryColliderObjects = new List<GameObject>();

	public bool Grounded
	{
		get
		{
			for (int i = 0; i < m_Wheels.Count; i++)
			{
				if (m_Wheels[i].Grounded)
				{
					return true;
				}
			}
			return false;
		}
	}

	public float FirstWheelTireRotation
	{
		get
		{
			if (m_Wheels.Count == 0)
			{
				return 0f;
			}
			return m_Wheels[0].TireRotation;
		}
	}

	public TechAudio.WheelTypes AudioType => m_AudioType;

	public void SetEnabled(bool enabled, bool andRecalculateDotProducts)
	{
		enabled = enabled && base.block.tank.IsNotNull() && !base.block.tank.IsSleeping;
		if (enabled != m_Enabled)
		{
			for (int i = 0; i < m_Wheels.Count; i++)
			{
				m_Wheels[i].Enable(enabled);
			}
			m_Enabled = enabled;
		}
		if (m_Enabled && andRecalculateDotProducts)
		{
			for (int j = 0; j < m_Wheels.Count; j++)
			{
				m_Wheels[j].RecalculateDotProducts();
			}
		}
	}

	public void SetAnimated(bool animated)
	{
		if (animated != m_Animated)
		{
			for (int i = 0; i < m_Wheels.Count; i++)
			{
				m_Wheels[i].SetAnimated(animated);
			}
			m_Animated = animated;
		}
	}

	public float AverageRoadForceSqr()
	{
		if (m_Wheels.Count == 1)
		{
			return m_Wheels[0].RoadForce.sqrMagnitude;
		}
		float num = 0f;
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			num += m_Wheels[i].RoadForce.sqrMagnitude;
		}
		return num / (float)m_Wheels.Count;
	}

	public void TrySetWheelClogged(float driveCountertorqueFlat, float driveCountertorqueScaled, float frictionTorqueFlat, float frictionTorqueScaled)
	{
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			m_Wheels[i].SetWheelClogged(driveCountertorqueFlat, driveCountertorqueScaled, frictionTorqueFlat, frictionTorqueScaled);
		}
	}

	private void ControlInput(TankControl.ControlState drive)
	{
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			m_Wheels[i].SetControlInput(drive);
		}
	}

	private void UpdateWheelParticles()
	{
		if (!Singleton.Manager<ManStartup>.inst.GameStarted || !QualitySettingsExtended.WheelParticlesEnabled)
		{
			return;
		}
		Vector3 lhs = base.block.trans.position - Singleton.cameraTrans.position;
		bool flag = Vector3.Dot(lhs, Singleton.cameraTrans.forward) < 0f || lhs.magnitude > Globals.inst.m_WheelDustCullingDistance;
		Color color = new Color(0f, 0f, 0f, 0f);
		bool isAttached = base.block.IsAttached;
		float time = Time.time;
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			ManWheels.Wheel wheel = m_Wheels[i];
			if ((bool)wheel.dustParticles)
			{
				bool flag2 = !flag && isAttached && wheel.Grounded && wheel.RPM > m_DustMinimumRPM;
				if (flag2)
				{
					wheel.dustParticles.transform.SetPositionIfChanged(wheel.ContactPoint);
					if (color.a == 0f)
					{
						ManWorld.CachedBiomeBlendWeights cachedBiomeBlendWeights = base.block.tank.BiomeWeightsAtPositionThisFrame();
						if (cachedBiomeBlendWeights.SetPieceBiomeWeight > 0.5f)
						{
							color = cachedBiomeBlendWeights.SetPieceBiome.DustVFXColor;
						}
						else
						{
							for (int num = cachedBiomeBlendWeights.NumWeights - 1; num >= 0; num--)
							{
								Biome biome = cachedBiomeBlendWeights.Biome(num);
								if ((bool)biome)
								{
									color = color.Add(biome.DustVFXColor.ScaleRGBA(cachedBiomeBlendWeights.Weight(num)));
								}
							}
						}
					}
					ParticleSystem.MainModule main = wheel.dustParticles.main;
					main.startColor = color;
				}
				if (flag2 != wheel.dustParticles.isPlaying)
				{
					if (flag2)
					{
						wheel.dustParticles.Play();
					}
					else
					{
						wheel.dustParticles.Stop();
					}
				}
			}
			if (!wheel.suspensionSparkParticles)
			{
				continue;
			}
			bool flag3 = isAttached && wheel.FullyCompressed && wheel.RPM > m_DustMinimumRPM;
			if (flag3)
			{
				flag3 = false;
				if (wheel.sparkStartupTimer == 0f)
				{
					wheel.sparkStartupTimer = time;
				}
				else if (time - wheel.sparkStartupTimer > Globals.inst.m_WheelSparksDelay)
				{
					flag3 = !flag;
				}
			}
			else
			{
				wheel.sparkStartupTimer = 0f;
			}
			if (flag3 != wheel.suspensionSparkParticles.isPlaying)
			{
				if (flag3)
				{
					wheel.suspensionSparkParticles.Play();
				}
				else
				{
					wheel.suspensionSparkParticles.Stop();
				}
			}
		}
	}

	private void UpdateTireTracks()
	{
		if (!m_UseTireTracks || !base.block.tank.IsNotNull())
		{
			return;
		}
		ManTireTracks.TrackGroup trackGroup = ((!base.block.tank.IsPlayer) ? ManTireTracks.TrackGroup.Other : ManTireTracks.TrackGroup.Player);
		if (trackGroup != m_PrevTrackType)
		{
			for (int i = 0; i < m_PrevTireTrackIndex.Length; i++)
			{
				m_PrevTireTrackIndex[i] = -1;
			}
			m_PrevTrackType = trackGroup;
		}
		for (int j = 0; j < m_Wheels.Count; j++)
		{
			if (m_Wheels[j].Grounded)
			{
				m_PrevTireTrackIndex[j] = TryAddTireTracks(m_Wheels[j], m_PrevTireTrackIndex[j], trackGroup);
			}
			else
			{
				m_PrevTireTrackIndex[j] = -1;
			}
		}
	}

	private int TryAddTireTracks(ManWheels.Wheel wheel, int prevTireTrackIndex, ManTireTracks.TrackGroup trackType)
	{
		float distFromCamera = Singleton.Manager<ManTireTracks>.inst.GetDistFromCamera(trackType);
		if ((base.block.tank.rbody.position - Singleton.cameraTrans.position).sqrMagnitude <= distFromCamera * distFromCamera)
		{
			Vector3 vector = base.transform.InverseTransformDirection(base.block.tank.rbody.velocity);
			float num;
			float num2;
			float num3;
			if (k_TracksUseForwardSpeed)
			{
				num = Mathf.Abs(vector.z);
				num2 = m_TracksMinSpeed;
				num3 = m_TracksMaxSpeed;
			}
			else
			{
				num = Mathf.Abs(vector.x);
				num2 = m_TracksSkidSpeed;
				num3 = m_TracksMaxSkidSpeed;
			}
			if (num >= num2 && Singleton.Manager<ManTireTracks>.inst.ColliderSupportsTireTracks(wheel.ContactCollider))
			{
				float num4 = (k_TracksUseIntensityFromSpeed ? Mathf.Clamp01(num / num3) : 1f);
				num4 = (k_TracksUseIntensityFromCompression ? (num4 * (wheel.Compression * wheel.Compression)) : num4);
				Vector3 contactPoint = wheel.ContactPoint;
				Vector3 contactNormal = wheel.ContactNormal;
				if (k_TracksApplyVelocity)
				{
					Vector3 velocity = base.block.tank.rbody.velocity;
					velocity -= contactNormal * Vector3.Dot(contactNormal, velocity);
					contactPoint += velocity * Time.fixedDeltaTime;
				}
				prevTireTrackIndex = Singleton.Manager<ManTireTracks>.inst.AddTireTrack(trackType, contactPoint, contactNormal, m_TireTrackWidth, num4, prevTireTrackIndex, GetMainBiome(), m_WheelTrackType);
			}
			else
			{
				prevTireTrackIndex = -1;
			}
		}
		else
		{
			prevTireTrackIndex = -1;
		}
		return prevTireTrackIndex;
	}

	private BiomeTypes GetMainBiome()
	{
		float num = 0f;
		BiomeTypes result = BiomeTypes.Grassland;
		ManWorld.CachedBiomeBlendWeights cachedBiomeBlendWeights = base.block.tank.BiomeWeightsAtPositionThisFrame();
		if (cachedBiomeBlendWeights.SetPieceBiomeWeight > 0.5f)
		{
			return cachedBiomeBlendWeights.SetPieceBiome.BiomeType;
		}
		for (int num2 = cachedBiomeBlendWeights.NumWeights - 1; num2 >= 0; num2--)
		{
			Biome biome = cachedBiomeBlendWeights.Biome(num2);
			if ((bool)biome)
			{
				float num3 = cachedBiomeBlendWeights.Weight(num2);
				if (num3 > num)
				{
					result = biome.BiomeType;
					num = num3;
				}
			}
		}
		return result;
	}

	private void InitGeometryColliders()
	{
		int num = m_WheelGeometry.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = m_WheelGeometry[i];
			if (!transform)
			{
				d.LogError("Missing wheel geometry on wheel " + base.name);
				continue;
			}
			s_TmpPerWheelGeometryColliderObjects.AddRange(from c in transform.GetComponentsInChildren<Collider>(includeInactive: true)
				select c.gameObject);
		}
		m_WheelGeometryColliderObjects = s_TmpPerWheelGeometryColliderObjects.ToArray();
		s_TmpPerWheelGeometryColliderObjects.Clear();
		if (Application.isPlaying)
		{
			s_LayerConnected = Globals.inst.layerTankIgnoreTerrain;
			s_LayerDisconnected = Globals.inst.layerTank;
		}
	}

	private void InitWheelParticles()
	{
		if (m_HasWheelParticles)
		{
			return;
		}
		d.Assert(QualitySettingsExtended.WheelParticlesEnabled, "ModuleWheel.InitWheelParticles - trying to init dust particles on wheels while QualitySettingsExtended.WheelDustParticlesEnabled is set to off!");
		if ((m_DustParticlesPrefab != null && m_DustParticlesPrefab.main.simulationSpace == ParticleSystemSimulationSpace.World) || (m_SuspensionSparkParticlesPrefab != null && m_SuspensionSparkParticlesPrefab.main.simulationSpace == ParticleSystemSimulationSpace.World))
		{
			m_WheelParticlesWorldObject = base.gameObject.AddComponent<WorldSpaceParticleSystem>();
		}
		int num = m_WheelGeometry.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = m_WheelGeometry[i];
			if ((bool)transform)
			{
				ManWheels.Wheel wheel = m_Wheels[i];
				if ((bool)m_DustParticlesPrefab)
				{
					ParticleSystem particleSystem = m_DustParticlesPrefab.UnpooledSpawn(transform, worldPosStays: false);
					particleSystem.transform.localPosition = Vector3.zero;
					particleSystem.transform.localRotation = Quaternion.identity;
					wheel.dustParticles = particleSystem;
				}
				if ((bool)m_SuspensionSparkParticlesPrefab)
				{
					ParticleSystem particleSystem2 = m_SuspensionSparkParticlesPrefab.UnpooledSpawn(m_SuspensionSparksTransform ? m_SuspensionSparksTransform : transform, worldPosStays: false);
					particleSystem2.transform.localPosition = Vector3.zero;
					particleSystem2.transform.localRotation = Quaternion.identity;
					wheel.suspensionSparkParticles = particleSystem2;
				}
			}
		}
		if (base.block.tank.IsNotNull())
		{
			EnableWheelParticleUpdate(enable: true);
		}
		m_HasWheelParticles = true;
	}

	private void RemoveWheelParticles()
	{
		if (!m_HasWheelParticles)
		{
			return;
		}
		if (m_WheelParticlesWorldObject != null)
		{
			UnityEngine.Object.Destroy(m_WheelParticlesWorldObject);
		}
		int num = m_WheelGeometry.Length;
		for (int i = 0; i < num; i++)
		{
			ManWheels.Wheel wheel = m_Wheels[i];
			if (wheel.dustParticles != null)
			{
				UnityEngine.Object.Destroy(wheel.dustParticles.gameObject);
				wheel.dustParticles = null;
			}
			if (wheel.suspensionSparkParticles != null)
			{
				UnityEngine.Object.Destroy(wheel.suspensionSparkParticles.gameObject);
				wheel.suspensionSparkParticles = null;
			}
		}
		if (base.block.tank.IsNotNull())
		{
			EnableWheelParticleUpdate(enable: false);
		}
		m_HasWheelParticles = false;
	}

	private void UpdateWarnings()
	{
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			ManWheels.Wheel wheel = m_Wheels[i];
			if (!wheel.Enabled || wheel.Horizontal)
			{
				continue;
			}
			if (wheel.Grounded && !wheel.DriveAligned)
			{
				m_Warnings["SideWheel"].TryRegisterWarning(LocalisationEnums.Warnings.warningTitleWheelsSideways, LocalisationEnums.Warnings.warningMsgWheelsSideways, 8);
			}
			if (wheel.FullyCompressed)
			{
				m_WarningWaitTimer += Time.deltaTime;
				if (m_WarningWaitTimer > m_SuspensionWarningWaitTime)
				{
					m_Warnings["Suspension"].TryRegisterWarning(LocalisationEnums.Warnings.warningTitleWheelSuspensionFullyCompressed, LocalisationEnums.Warnings.warningMsgWheelSuspensionFullyCompressed, 9);
				}
			}
			else
			{
				m_Warnings["Suspension"].Remove();
				m_WarningWaitTimer = 0f;
			}
		}
	}

	private void SetupTireTrackHandler(bool enable, bool isHQ = false)
	{
		Singleton.Manager<ManUpdate>.inst.RemoveAction(ManUpdate.Type.Update, ManUpdate.Order.Last, UpdateTireTracks);
		Singleton.Manager<ManUpdate>.inst.RemoveAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.Last, UpdateTireTracks);
		if (enable)
		{
			if (isHQ)
			{
				Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.Last, UpdateTireTracks, -150);
			}
			else
			{
				Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.Last, UpdateTireTracks);
			}
		}
	}

	private void EnableWheelParticleUpdate(bool enable)
	{
		if (enable)
		{
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.Last, UpdateWheelParticles);
			return;
		}
		Singleton.Manager<ManUpdate>.inst.RemoveAction(ManUpdate.Type.Update, ManUpdate.Order.Last, UpdateWheelParticles);
		UpdateWheelParticles();
	}

	private void EnablePlayerWarningsUpdate(bool enable)
	{
	}

	private void OnAttached()
	{
		float i = base.block.CurrentMass * 0.9f / (float)m_Wheels.Count * m_WheelParams.radius * m_WheelParams.radius;
		AttachData moduleData = new AttachData(this, i, m_Wheels.Count);
		for (int j = 0; j < m_Wheels.Count; j++)
		{
			m_Wheels[j].Attach(moduleData);
			m_Wheels[j].SetAnimated(m_Animated);
		}
		GameObject[] wheelGeometryColliderObjects = m_WheelGeometryColliderObjects;
		for (int k = 0; k < wheelGeometryColliderObjects.Length; k++)
		{
			wheelGeometryColliderObjects[k].layer = s_LayerConnected;
		}
		base.block.tank.control.driveControlEvent.Subscribe(ControlInput);
		base.block.tank.ResetPhysicsEvent.Subscribe(OnResetTechPhysics);
		base.block.tank.SleepEvent.Subscribe(OnSleep);
		base.block.tank.TechAudio.AddWheel(this);
		SetupTireTrackHandler(enable: true, QualitySettingsExtended.HQTyreTracks);
		for (int l = 0; l < m_PrevTireTrackIndex.Length; l++)
		{
			m_PrevTireTrackIndex[l] = -1;
		}
		if (m_HasWheelParticles)
		{
			EnableWheelParticleUpdate(enable: true);
		}
		if (base.block.tank.IsPlayer)
		{
			EnablePlayerWarningsUpdate(enable: true);
		}
	}

	private void OnDetaching()
	{
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			m_Wheels[i].Detach();
		}
		GameObject[] wheelGeometryColliderObjects = m_WheelGeometryColliderObjects;
		for (int j = 0; j < wheelGeometryColliderObjects.Length; j++)
		{
			wheelGeometryColliderObjects[j].layer = s_LayerDisconnected;
		}
		base.block.tank.control.driveControlEvent.Unsubscribe(ControlInput);
		base.block.tank.ResetPhysicsEvent.Unsubscribe(OnResetTechPhysics);
		base.block.tank.SleepEvent.Unsubscribe(OnSleep);
		m_Warnings["SideWheel"].Reset();
		m_Warnings["Suspension"].Reset();
		base.block.tank.TechAudio.RemoveWheel(this);
		SetupTireTrackHandler(enable: false);
		if (m_HasWheelParticles)
		{
			EnableWheelParticleUpdate(enable: false);
		}
		EnablePlayerWarningsUpdate(enable: true);
	}

	private void OnResetTechPhysics()
	{
		for (int i = 0; i < m_Wheels.Count; i++)
		{
			m_Wheels[i].RecalculateDotProducts();
		}
	}

	private void OnVisiblePhysicsEnabled(bool enabled)
	{
		SetEnabled(enabled, andRecalculateDotProducts: false);
	}

	private void OnSleep(bool sleep)
	{
		SetEnabled(!sleep, andRecalculateDotProducts: false);
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.warnings = m_Warnings;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 == null)
		{
			return;
		}
		foreach (string key in m_Warnings.Keys)
		{
			m_Warnings[key].Restore(serialData2.warnings[key]);
		}
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (!flags.Contains(0))
		{
			return;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		Color textColor = Color.white;
		foreach (ManWheels.Wheel wheel in m_Wheels)
		{
			num2 += wheel.RoadForce.magnitude;
			num3 += wheel.ContactNormal.magnitude;
			textColor = (wheel.FullyCompressed ? Color.red : (wheel.Grounded ? Color.green : Color.cyan));
			num += wheel.RPM;
		}
		num /= (float)m_Wheels.Count;
		DebugGui.LabelScreen(((int)num2).ToString(), Color.magenta, screenPos);
		screenPos += Vector2.down * 9f;
		DebugGui.LabelScreen(((int)num3).ToString(), textColor, screenPos);
		screenPos += Vector2.down * 9f;
		DebugGui.LabelScreen(((int)num).ToString(), Color.white, screenPos);
	}

	public void ResetNetworkedState()
	{
		if (!base.block.tank.IsNotNull())
		{
			return;
		}
		foreach (ManWheels.Wheel wheel in m_Wheels)
		{
			wheel.ResetNetworkedState();
		}
	}

	private void OnQualitySettingChanged()
	{
		bool wheelParticlesEnabled = QualitySettingsExtended.WheelParticlesEnabled;
		if (m_HasWheelParticles != wheelParticlesEnabled)
		{
			if (wheelParticlesEnabled)
			{
				InitWheelParticles();
			}
			else
			{
				RemoveWheelParticles();
			}
		}
		if (base.block.tank.IsNotNull())
		{
			SetupTireTrackHandler(enable: true, QualitySettingsExtended.HQTyreTracks);
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool isPlayer)
	{
		if ((object)base.block.tank == tank)
		{
			EnablePlayerWarningsUpdate(isPlayer);
		}
	}

	private void PrePool()
	{
		d.AssertFormat(m_WheelGeometry != null, this, "Block {0} has no wheel geometry set", base.name);
		InitGeometryColliders();
		if (m_WheelParams.maxSuspensionAcceleration <= 0f)
		{
			m_WheelParams.maxSuspensionAcceleration = 15f;
		}
		else if (m_WheelParams.maxSuspensionAcceleration > 15f)
		{
			d.LogError($"Wheel on {base.gameObject.name} has maxSuspensionAcceleration set above maximum value ({m_WheelParams.maxSuspensionAcceleration}/{15f})! Value is being clamped to max.");
			m_WheelParams.maxSuspensionAcceleration = 15f;
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		ColliderSwapper component = GetComponent<ColliderSwapper>();
		d.Assert(component.IsNotNull(), "No ColliderSwapper found on block with wheels " + base.block.name);
		int num = m_WheelGeometry.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = m_WheelGeometry[i];
			if (!transform)
			{
				d.LogError("missing wheel geometry on wheel " + base.name);
				continue;
			}
			ManWheels.Wheel item = new ManWheels.Wheel(transform, component, m_WheelParams.radius);
			m_Wheels.Add(item);
		}
		if (QualitySettingsExtended.WheelParticlesEnabled)
		{
			InitWheelParticles();
		}
		QualitySettingsExtended.QualitySettingChangedEvent.Subscribe(OnQualitySettingChanged);
		if (component.IsNotNull())
		{
			component.AddWheels(m_Wheels);
		}
		m_Warnings.Add("SideWheel", new WarningHolder(base.block.visible, WarningHolder.WarningType.WheelSideways));
		m_Warnings.Add("Suspension", new WarningHolder(base.block.visible, WarningHolder.WarningType.WheelOverloaded));
		m_PrevTireTrackIndex = new int[m_Wheels.Count];
	}

	private void OnSpawn()
	{
		for (int i = 0; i < m_PrevTireTrackIndex.Length; i++)
		{
			m_PrevTireTrackIndex[i] = -1;
		}
		m_Enabled = true;
		SetEnabled(enabled: false, andRecalculateDotProducts: false);
		m_Animated = true;
		SetAnimated(animated: false);
		GameObject[] wheelGeometryColliderObjects = m_WheelGeometryColliderObjects;
		for (int j = 0; j < wheelGeometryColliderObjects.Length; j++)
		{
			wheelGeometryColliderObjects[j].layer = s_LayerDisconnected;
		}
		float f = (float)Math.PI / 180f * m_WheelParams.thicknessAngular;
		m_TireTrackWidth = Mathf.Tan(f) * m_WheelParams.radius * 2f;
		base.block.visible.PhysicsEnabledEvent.Subscribe(OnVisiblePhysicsEnabled);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
	}

	private void OnRecycle()
	{
		base.block.visible.PhysicsEnabledEvent.Unsubscribe(OnVisiblePhysicsEnabled);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
	}

	private void OnDepool()
	{
		QualitySettingsExtended.QualitySettingChangedEvent.Unsubscribe(OnQualitySettingChanged);
	}

	private void Start()
	{
		if (!base.block.IsAttached)
		{
			return;
		}
		foreach (ManWheels.Wheel wheel in m_Wheels)
		{
			wheel.Reset();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (Application.isPlaying)
		{
			if (base.block.IsAttached)
			{
				for (int i = 0; i < m_Wheels.Count; i++)
				{
					m_Wheels[i].DrawGizmos();
				}
			}
			return;
		}
		for (int j = 0; j < m_WheelGeometry.Length; j++)
		{
			Gizmos.color = Color.cyan;
			Transform transform = m_WheelGeometry[j];
			_ = transform.right;
			_ = transform.position;
			float[] array = new float[3] { -1f, 0f, 1f };
			foreach (float num in array)
			{
				_ = m_WheelParams;
				Mathf.Cos(m_WheelParams.thicknessAngular * num * ((float)Math.PI / 180f));
				_ = m_WheelParams;
				Mathf.Sin(m_WheelParams.thicknessAngular * num * ((float)Math.PI / 180f));
			}
			DebugUtil.GizmosDrawArrow(transform.position, transform.position - transform.up * (m_WheelParams.radius + m_WheelParams.suspensionTravel), m_WheelParams.suspensionTravel);
		}
	}
}
