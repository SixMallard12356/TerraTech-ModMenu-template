using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Globals : ScriptableObject
{
	[Serializable]
	public struct HoldBeamFloatParams
	{
		public AnimationCurve inPullVsHeight;

		public AnimationCurve pullProfile;

		public float velocityDamping;

		public float dropRange;

		public float dropAfterMinTime;

		public float heightCorrectionLiftFactor;

		public ScaleRatio spinTorqueRatio;

		public float readySpinSpeed;

		public float dropAboveHeight;

		public float vectrosityLineThickness;

		public float clipRange;

		public float stationaryVelocitySqrThreshold;

		public float stationaryMinHeightDiff;
	}

	[Serializable]
	public struct ScaleRatio
	{
		public float maxOutput;

		public float maxInput;

		public float Scale(float input)
		{
			return Mathf.Clamp(input * (maxOutput / maxInput), 0f - maxOutput, maxOutput);
		}

		public Vector3 Scale(Vector3 input)
		{
			float magnitude = input.magnitude;
			if (magnitude > maxInput)
			{
				return input * maxOutput / magnitude;
			}
			return input * (maxOutput / maxInput);
		}
	}

	[Serializable]
	public struct ModuleDamageParams
	{
		public float lowHealthFlashThreshold;

		public float flashPeriodBelowThreshold;

		public float flashPeriodAboutToExplode;

		public float zeroHealthCabExplodeAfterTime;

		public float zeroHealthExplodeAfterTime;

		public float zeroHealthExplodeTimeVariance;

		public float explodeTimerReductionPerAdditionalHit;

		public float healFlashTime;

		public Color HealColour;

		public Color DamageColour;

		public Color ScavengeColour;

		public Color OutOfShieldColour;

		public Color CloggedColour;

		public float detachMeterFillFactor;

		public float detachMeterFillFactorPlayerPC;

		public float detachMeterFillFactorPlayerConsole;

		public float detachMeterDrainRate;

		public float detachMeterMinThreshold;

		public float detachMeterMaxThreshold;

		public float detachMeterConnectedAPBias;

		public float detachMeterConnectedMassBias;
	}

	public class ObjectLayer
	{
		public class Group
		{
			public enum Type
			{
				None,
				PhysicalTech,
				PhysicalTerrain,
				PhysicalScenery,
				PhysicalPickups
			}

			private ObjectLayer[] layers;

			private int[] layerIndices;

			private int mask = -1;

			private int[] LayerIndices
			{
				get
				{
					if (layerIndices == null)
					{
						layerIndices = new int[layers.Length];
						for (int i = 0; i < layers.Length; i++)
						{
							layerIndices[i] = layers[i].GetLayer();
						}
					}
					return layerIndices;
				}
			}

			public int LayerMask
			{
				get
				{
					if (mask == -1)
					{
						mask = 0;
						for (int i = 0; i < LayerIndices.Length; i++)
						{
							mask |= 1 << LayerIndices[i];
						}
					}
					return mask;
				}
			}

			public Group(params ObjectLayer[] layers)
			{
				this.layers = layers;
			}

			public static implicit operator int(Group group)
			{
				return group.LayerMask;
			}
		}

		private int layer;

		private string layerName;

		public int mask => 1 << GetLayer();

		public ObjectLayer(string name)
		{
			layer = -1;
			layerName = name;
		}

		private int GetLayer()
		{
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer(layerName);
			}
			return layer;
		}

		public static implicit operator int(ObjectLayer ol)
		{
			return ol.GetLayer();
		}
	}

	public bool m_DownloadOtherTrailers;

	public bool m_DisableMeshMergers;

	public bool m_MergeSceneryClutter;

	public bool m_InstanceSceneryClutter;

	public bool m_DisableLicenses;

	[Header("Encounters")]
	public bool m_UseUnfinishedGrades;

	public bool m_AllowSetPieceMissions;

	public bool m_AllowSetPieceMissionsOnConsole;

	[Header("Analytics")]
	public bool m_SendAnalyticsInEditor;

	[Header("Debug Spawn Keys")]
	public KeyCode m_BlockSpawnKey = KeyCode.Backslash;

	public KeyCode m_ChunkSpawnKey = KeyCode.LeftAlt;

	public KeyCode m_ResourceSpawnKey = KeyCode.CapsLock;

	public KeyCode m_ShowDebugNameKey = KeyCode.LeftAlt;

	[Header("XP & BB Awarding")]
	public bool m_AwardXPandBB;

	public bool m_TechKilledIfOnlyCabDamaged = true;

	public float m_TechDestroyedXPMultiplier = 0.01f;

	public float m_TechDestroyedBBMultiplier = 0.05f;

	[Header("Selling Blocks")]
	public float m_BlockSellingModifier = 1f / 9f;

	[Header("Multiplayer")]
	public bool Multiplayer_AnchorsOff = true;

	public float ScrapperCostMultiplier = 2f;

	public float MultiplayerBlockPaintingTimeout = 0.25f;

	public Vector2 MultiplayerSpawnCameraDistAndHeight = new Vector2(20f, 20f);

	[Header("Crafting")]
	public float m_TankHolderHeartbeatInterval = 2f;

	public float m_TankHolderCreationInterval = 0.4f;

	public float m_TankHolderDropStackTolerance = 0.7f;

	public bool m_TankHolderPassingOnlyToNext;

	public bool m_TankHolderPassingOneAtATime;

	public ParticleSystem m_DefaultConsumeParticles;

	public ParticleSystem m_DefaultProduceParticles;

	public ParticleSystem m_DefaultNewCraftingParticles;

	public AnimationCurve m_ChunkOverlapRepulsionCurve;

	public float m_ChunkOverlapRepelMax;

	public float m_PickupRangeIndicatorSpinSpeed = 1f;

	[Header("Holder Beams")]
	public HoldBeamFloatParams holdBeamFloatParams = new HoldBeamFloatParams
	{
		velocityDamping = 0.75f,
		dropRange = 5f,
		dropAfterMinTime = 1f,
		heightCorrectionLiftFactor = 0.5f,
		spinTorqueRatio = new ScaleRatio
		{
			maxInput = 1f,
			maxOutput = 10f
		},
		readySpinSpeed = 2f,
		dropAboveHeight = 10f,
		vectrosityLineThickness = 100f,
		clipRange = 50f,
		stationaryVelocitySqrThreshold = 1f,
		stationaryMinHeightDiff = 1f
	};

	public float m_MagnetVelocityDamping = 0.9f;

	[Header("Damage")]
	public ModuleDamageParams moduleDamageParams = new ModuleDamageParams
	{
		lowHealthFlashThreshold = 0.3f,
		flashPeriodBelowThreshold = 0.3f,
		flashPeriodAboutToExplode = 0.15f,
		zeroHealthExplodeAfterTime = 2f,
		zeroHealthExplodeTimeVariance = 0.2f,
		explodeTimerReductionPerAdditionalHit = 0.1f,
		healFlashTime = 0.5f,
		HealColour = new Color(0f, 1f, 0f, 0.5f),
		DamageColour = new Color(1f, 0f, 0f, 0.5f),
		ScavengeColour = new Color(1f, 0f, 1f, 0.5f),
		OutOfShieldColour = new Color(1f, 0f, 0f, 0.5f),
		CloggedColour = new Color(1f, 0f, 0f, 0.5f),
		detachMeterFillFactor = 5f,
		detachMeterFillFactorPlayerPC = 2f,
		detachMeterFillFactorPlayerConsole = 2f,
		detachMeterDrainRate = 50f,
		detachMeterMinThreshold = 100f,
		detachMeterMaxThreshold = 500f,
		detachMeterConnectedAPBias = 0.5f,
		detachMeterConnectedMassBias = 0.1f
	};

	public float playerDamageReceivedMultiplierPC = 1f;

	public float playerDamageReceivedMultiplierConsole = 1f;

	public float impactDamageSpeedThreshold = 5f;

	public float impactDamageThreshold;

	public float impactDamageMultiplier = 0.05f;

	public PhysicMaterial impactMaterialTerrain;

	public float m_SceneryHitEffectMinInterval = 0.3f;

	public Vector3 blockKickBasicVelocity = Vector3.up * 6f;

	public Vector3 blockKickRandomVelocity = Vector3.one * 9f;

	public float blockKickRandomAngVel = 45f;

	[Tooltip("Chance that a block won't be immediately destroyed if the enemy tech it's attached to blows up")]
	[Range(0f, 1f)]
	public float m_BlockSurvivalChance = 0.5f;

	[Range(0f, 1f)]
	[Tooltip("Additional chance for an undiscovered block to survive when the enemy tech it's attached to blows up")]
	public float m_NewBlockSurvivalBuff = 0.5f;

	[Header("Block settings")]
	public float airSpeedDrag = 1f;

	public float m_WingAirspeedIgnore = 20f;

	public float m_WingLiftIgnore = 10f;

	public float m_BoosterMinBurnTime = 1f;

	[Header("Wheels")]
	public float m_WheelRoadForceDamping = 0.5f;

	public float m_WheelDustCullingDistance = 100f;

	public float m_WheelGizmoForceScale = 0.2f;

	public AnimationCurve m_WheelFrictionCorrectGripScale;

	public float m_WheelFrictionCorrectScaleMax = 1f;

	public float m_WheelFrictionCorrectFactorFull = 1f;

	public float m_WheelSlipDampingNormal = 100f;

	public float m_WheelSlipDampingNormalAngular = 100f;

	public float m_WheelPosCloseTol = 0.005f;

	public float m_WheelPosFarTol = 0.01f;

	public float m_WheelRotCloseTol = 0.001f;

	public float m_WheelRotFarTol = 0.005f;

	public float m_WheelFarDist = 100f;

	public float m_WheelSparksDelay = 0.1f;

	[Header("Camera")]
	[Tooltip("NOTE - Default speed is hardcoded in ManProfile.cs")]
	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	public float m_RuntimeCameraSpinSensHorizontal = 1f;

	public MinMaxFloat m_CameraSpinSensHorizontal = new MinMaxFloat(0.1f, 2.5f);

	[Tooltip("NOTE - Default speed is hardcoded in ManProfile.cs. The Sign signifies whether Y is inverted or not, with negative being '_Not_ inverted'")]
	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	public float m_RuntimeCameraSpinSensVertical = -1f;

	public MinMaxFloat m_CameraSpinSensVertical = new MinMaxFloat(0.1f, 2.5f);

	[Tooltip("NOTE - Default speed is hardcoded in ManProfile.cs")]
	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	public float m_RuntimeCameraSpinInterpSpeed = 1f;

	[Tooltip("How fast the camera approaches the target rotation. Higher value is snappier camera")]
	public AnimationCurve m_CameraSpinInterpolationSpeedRange;

	public Vector2 m_CamSpinSpeedMouseMultiplier = new Vector2(1000f, 1000f);

	public Vector2 m_CamSpinSpeedGamepadMultiplier = new Vector2(280f, 280f);

	public float m_CamZoomStepManual = 1f;

	public float m_CamZoomHoldMultiplier = 5f;

	[Header("View")]
	public MinMaxFloat m_DrawDistanceRange = new MinMaxFloat(300f, 1000f);

	public MinMaxFloat m_DetailDistanceRange = new MinMaxFloat(300f, 1000f);

	public MinMaxFloat m_ShadowDistanceRange = new MinMaxFloat(300f, 1000f);

	[Header("Gamepad")]
	public MinMaxFloat m_GamepadCursorSpeed = new MinMaxFloat(0.2f, 2.2f);

	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	[Tooltip("NOTE - Default cursor speed is hardcoded in ManProfile.cs")]
	public float m_CurrentGamepadCursorSpeed = 1f;

	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	[Tooltip("NOTE - Default vibration setting is hardcoded in ManProfile.cs")]
	public bool m_CurrentGamepadVibration = true;

	public float m_GamepadCursorDragBorderFraction;

	[Tooltip("As a function of screen resolution")]
	public float m_CursorSelectBufferDistance = 0.05f;

	public StickInputInterpreter m_DriveStickInputInterpreter;

	public GamepadVibration.VibrationSetting m_GamepadVibrationPlayerDestroyed;

	public GamepadVibration.VibrationSetting m_GamepadVibrationPlayerHit;

	[Header("Auto expiry")]
	public float autoExpireTimeoutBlocks = 300f;

	public float autoExpireTimeoutChunks = 300f;

	public float autoExpireTimeoutCrates = 300f;

	public float autoExpireTimeoutBlocksRandD = 30f;

	public float autoExpireTimeoutChunksRandD = 30f;

	public float autoExpireTimeoutCratesRandD = 30f;

	public float autoExpireRangeCameraFacing = 100f;

	[Header("Tech Audio")]
	public float m_TechAudioOneshotCooldown = 0.4f;

	public float m_WheelSoundFadeIn = 0.25f;

	public float m_WheelSoundFadeOut = 0.25f;

	public float AudioMaxDistance = 75f;

	public float AudioMinDistance = 5f;

	public float AudioMaxRPM = 3000f;

	public int AudioMaxTechInstances = 6;

	public float m_DangerPlayerRadius = 100f;

	public float m_DangerPlayerMPRadius = 60f;

	public float m_DangerPlayerRaDRadius;

	[Tooltip("Set in QualitySettingsExtended")]
	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	[Header("Terrain")]
	public int m_TerrainLODPixelError = 5;

	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	[Tooltip("Set in QualitySettingsExtended")]
	public int m_TerrainBaseMapDistance = 1000;

	public int m_TerrainBaseMapResolution = 512;

	public int m_TerrainCollisionThickness = 1;

	public bool m_DrawTerrainInstanced = true;

	[Tooltip("Manually connecting seems to cause issues with stitching not working correctly, but auto connect seems fine..")]
	public bool m_ManuallyConnectTerrainTiles;

	[Header("Scenery Regrow")]
	public float m_DefaultSceneryRegrowTime = 3600f;

	public float m_BlockedSceneryRegrowInterval = 10f;

	[Header("Tutorial")]
	public float m_TutorialBlockNudgeDistanceWeight = 0.1f;

	public float m_TutorialBlockNudgeVelocityWeight = 0.02f;

	public float m_TutorialBlockNudgeVelocityBias = 2.5f;

	[Header("Multiplayer")]
	public string m_DebugMasterServerIP;

	public string m_MasterServerIP;

	public int m_MasterServerPort;

	public string m_MasterServerGameType;

	[Header("Curvature Testing")]
	public float m_FlatTerrainMaxSlope = 45f;

	public float m_FlatTerrainMinCurve;

	[Header("Tooltip")]
	public Color tooltipColourResourceChunk = Color.white;

	public Color tooltipColourResourceProducer = Color.white;

	public Color tooltipColourTankBlock = Color.white;

	public Color tooltipShadowColour = Color.black;

	public int tooltipTextSize = 13;

	public AnimationCurve tooltipFadeUpCurve;

	[Header("Building")]
	public Vector3 m_TechSpawnOffset = new Vector3(0f, 1f, 0f);

	public float baseModuleMaxAnchorSnapY = 1.25f;

	public float m_TechStoreThreatDistance = 100f;

	public float m_TechPlacementMaxDistance = 100f;

	public bool m_AllowPlaceTechWithMissingBlocks = true;

	[Header("ForceGizmos")]
	public ForceGizmo m_ForceGizmoPrefab;

	public Color m_ForceGizmo_Mass_Color;

	public Color m_ForceGizmo_Lift_Color;

	public Color m_ForceGizmo_Propellers_Color;

	[FormerlySerializedAs("m_ForceGizmo_Thrusters_Color")]
	public Color m_ForceGizmo_Boosters_Color;

	[Header("Hotswaping")]
	[Range(0f, 5f)]
	public int maxHotswapSlots = 3;

	[Header("UI")]
	public float m_RadialMenuMaxPlayerMoveDistance = 20f;

	public float doubleTapDelay = 0.2f;

	public float m_UIInteractionRange = 30f;

	public float m_StickScrollSensitivity = 200f;

	[Header("Other Stuff")]
	public float m_VisibleEmergencyKillHeight = -1000f;

	public float m_VisibleEmergencyKillMaxHeight = 100000f;

	public bool m_DisableControllers;

	[Obsolete("Deprecated and unused, including its UIOptionsGameplay counterpart. Superseded by Control Schema option of same behaviour")]
	public bool reversingSteerInversion = true;

	public float tankOverturnThresholdDegrees = 45f;

	public BlockPairsList m_BlockPairsList;

	public float m_TechGroundingThresholdDegrees = 70f;

	public bool m_DebugSavesEnabled;

	public bool DynamicMultiBoxBroadphaseRegions;

	private Dictionary<ObjectLayer.Group.Type, ObjectLayer.Group> _layerGroups;

	public readonly ObjectLayer layerTank = new ObjectLayer("Tank");

	public readonly ObjectLayer layerTankIgnoreTerrain = new ObjectLayer("TankIgnoreTerrain");

	public readonly ObjectLayer layerTerrain = new ObjectLayer("Terrain");

	public readonly ObjectLayer layerWater = new ObjectLayer("Water");

	public readonly ObjectLayer layerScenery = new ObjectLayer("Scenery");

	public readonly ObjectLayer layerSceneryCoarse = new ObjectLayer("SceneryCoarse");

	public readonly ObjectLayer layerIgnoreScenery = new ObjectLayer("SceneryIgnoreRaycast");

	public readonly ObjectLayer layerPickup = new ObjectLayer("Pickup");

	public readonly ObjectLayer layerBullet = new ObjectLayer("Bullet");

	public readonly ObjectLayer layerShieldPiercingBullet = new ObjectLayer("ShieldPiercingBullet");

	public readonly ObjectLayer layerContainer = new ObjectLayer("Container");

	public readonly ObjectLayer layerCosmetic = new ObjectLayer("Cosmetic");

	public readonly ObjectLayer layerShield = new ObjectLayer("Shield");

	public readonly ObjectLayer layerShieldBulletsFilter = new ObjectLayer("ShieldBulletFilter");

	public readonly ObjectLayer layerTrigger = new ObjectLayer("Trigger");

	public readonly ObjectLayer layerWheelSuspension = new ObjectLayer("WheelSuspension");

	public readonly ObjectLayer layerTerrainOnly = new ObjectLayer("TerrainOnly");

	public readonly ObjectLayer layerLandmark = new ObjectLayer("Landmarks");

	public readonly ObjectLayer layerDeliveryBlocker = new ObjectLayer("DeliveryBlocker");

	public readonly ObjectLayer layerSceneryFader = new ObjectLayer("SceneryFader");

	[SerializeField]
	public PlatformAssetBundles m_PlatformAssetBundles;

	public static System.Random NewEncryptionRandom => new System.Random(485348758);

	public Dictionary<ObjectLayer.Group.Type, ObjectLayer.Group> layerGroups
	{
		get
		{
			if (_layerGroups == null)
			{
				_layerGroups = new Dictionary<ObjectLayer.Group.Type, ObjectLayer.Group>
				{
					{
						ObjectLayer.Group.Type.PhysicalTech,
						new ObjectLayer.Group(layerTank, layerTankIgnoreTerrain)
					},
					{
						ObjectLayer.Group.Type.PhysicalTerrain,
						new ObjectLayer.Group(layerTerrain, layerTerrainOnly, layerLandmark)
					},
					{
						ObjectLayer.Group.Type.PhysicalScenery,
						new ObjectLayer.Group(layerScenery)
					},
					{
						ObjectLayer.Group.Type.PhysicalPickups,
						new ObjectLayer.Group(layerPickup)
					}
				};
			}
			return _layerGroups;
		}
	}

	public static Globals inst => Singleton.instance?.globals;
}
