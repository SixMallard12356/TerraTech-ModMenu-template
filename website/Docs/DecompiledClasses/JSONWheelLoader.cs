#define UNITY_EDITOR
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JSONWheelLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Wheel";
	}

	public static void ApplyTemplate(int blockID, ModuleWheels target, ModuleWheels template)
	{
		target.m_TorqueParams.torqueCurveDrive = template.m_TorqueParams.torqueCurveDrive;
		target.m_WheelParams.tireProperties.props.frictionLat = template.m_WheelParams.tireProperties.props.frictionLat;
		target.m_WheelParams.tireProperties.props.frictionLong = template.m_WheelParams.tireProperties.props.frictionLong;
		target.m_DustParticlesPrefab = template.m_DustParticlesPrefab;
		target.m_SuspensionSparkParticlesPrefab = template.m_SuspensionSparkParticlesPrefab;
		target.m_SuspensionSparksTransform = target.transform.Find("_sparksLocator");
		if (target.m_SuspensionSparksTransform == null)
		{
			target.m_SuspensionSparksTransform = target.transform;
		}
		List<Transform> list = new List<Transform>();
		foreach (Transform item in JSONModuleLoader.ChildrenMatching(target.transform, "_wheel"))
		{
			list.Add(item);
		}
		if (list.Count == 0)
		{
			d.LogError($"Could not find _wheel sub-object on wheel {blockID}:{target.name}");
		}
		target.m_WheelGeometry = list.ToArray();
		_ = target.transform.Find("_axle") != null;
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			ModuleWheels orAddComponent = GetOrAddComponent<ModuleWheels>(block);
			ModuleWheels component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.GSOWheel_111).GetComponent<ModuleWheels>();
			orAddComponent.m_TorqueParams = new ManWheels.TorqueParams
			{
				torqueCurveMaxTorque = TryParse(obj, "MaxTorque", 100f),
				torqueCurveMaxRpm = TryParse(obj, "MaxRPM", 400f),
				passiveBrakeMaxTorque = TryParse(obj, "PassiveBrakeMaxTorque", 50f),
				reverseBrakeMaxRpm = TryParse(obj, "ReverseMaxRPM", 50f),
				basicFrictionTorque = TryParse(obj, "BasicFrictionTorque", 5f),
				fullCompressFrictionTorque = TryParse(obj, "CompressedFrictionTorque", 50f)
			};
			orAddComponent.m_UseTireTracks = TryParse(obj, "UseTireTracks", defaultValue: true);
			orAddComponent.m_WheelTrackType = TryParseEnum(obj, "WheelTrackType", ManTireTracks.WheelType.StandardTreads);
			orAddComponent.m_AudioType = TryParseEnum(obj, "AudioType", TechAudio.WheelTypes.RubberWheel);
			orAddComponent.m_WheelParams = new ManWheels.WheelParams
			{
				radius = TryParse(obj, "WheelRadius", 0.5f),
				thicknessAngular = TryParse(obj, "AngularThickness", 20f),
				suspensionSpring = TryParse(obj, "SuspensionSpringStrength", 200f),
				suspensionDamper = TryParse(obj, "SuspensionDamperStrength", 80f),
				suspensionQuadratic = TryParse(obj, "QuadraticSuspension", defaultValue: true),
				suspensionTravel = TryParse(obj, "SuspensionDistance", 0.2f),
				steerAngleMax = TryParse(obj, "MaxSteerAngle", 0f),
				steerSpeed = TryParse(obj, "SteerSpeed", 2f),
				maxSuspensionAcceleration = TryParse(obj, "MaxSuspensionAcceleration", 15f)
			};
			orAddComponent.m_WheelParams.tireProperties = TireProperties.CreateInstance();
			orAddComponent.m_WheelParams.tireProperties.props = new ManWheels.TireProperties
			{
				frictionScaleLong = TryParse(obj, "FrictionScaleLongitudinal", 1f),
				frictionScaleLat = TryParse(obj, "FrictionScaleLatitudinal", 1f),
				gripFactorLong = TryParse(obj, "GripFactorLongitudinal", 2f),
				gripFactorLat = TryParse(obj, "GripFactorLatitudinal", 3f)
			};
			orAddComponent.m_DriveTurnPower = TryParse(obj, "DriveTurnPower", 1f);
			orAddComponent.m_DriveTurnBrake = TryParse(obj, "DriveTurnBrake", 0f);
			orAddComponent.m_DriveTurnDifferential = TryParse(obj, "DriveTurnDifferential", 1f);
			orAddComponent.m_TurnOnSpotPower = TryParse(obj, "TurnOnSpotPower", 1f);
			orAddComponent.m_DustMinimumRPM = TryParse(obj, "MinimumRPMForDust", 20f);
			ApplyTemplate(blockID, orAddComponent, component);
			return true;
		}
		return false;
	}
}
