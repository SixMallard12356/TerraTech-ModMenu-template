using Newtonsoft.Json.Linq;

public class JSONWheelLoaderLegacy : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "ModuleWheels";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			ModuleWheels orAddComponent = GetOrAddComponent<ModuleWheels>(block);
			ModuleWheels component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.GSOWheel_111).GetComponent<ModuleWheels>();
			orAddComponent.m_DriveTurnPower = TryParse(obj, "m_DriveTurnPower", 1f);
			orAddComponent.m_DriveTurnBrake = TryParse(obj, "m_DriveTurnBrake", 0f);
			orAddComponent.m_DriveTurnDifferential = TryParse(obj, "m_DriveTurnDifferential", 1f);
			orAddComponent.m_TurnOnSpotPower = TryParse(obj, "m_TurnOnSpotPower", 1f);
			orAddComponent.m_DustMinimumRPM = TryParse(obj, "m_DustMinimumRPM", 20f);
			orAddComponent.m_UseTireTracks = TryParse(obj, "m_UseTireTracks", defaultValue: true);
			orAddComponent.m_WheelTrackType = (ManTireTracks.WheelType)TryParse(obj, "m_WheelTrackType", 0);
			orAddComponent.m_AudioType = (TechAudio.WheelTypes)TryParse(obj, "m_AudioType", 0);
			JObject jObject = TryGetObject(obj, "m_TorqueParams");
			if (jObject != null)
			{
				orAddComponent.m_TorqueParams = new ManWheels.TorqueParams
				{
					torqueCurveMaxTorque = TryParse(jObject, "torqueCurveMaxTorque", 100f),
					torqueCurveMaxRpm = TryParse(jObject, "torqueCurveMaxRpm", 400f),
					passiveBrakeMaxTorque = TryParse(jObject, "passiveBrakeMaxTorque", 50f),
					reverseBrakeMaxRpm = TryParse(jObject, "reverseBrakeMaxRpm", 50f),
					basicFrictionTorque = TryParse(jObject, "basicFrictionTorque", 5f),
					fullCompressFrictionTorque = TryParse(jObject, "fullCompressFrictionTorque", 50f)
				};
			}
			JObject jObject2 = TryGetObject(obj, "m_WheelParams");
			if (jObject2 != null)
			{
				orAddComponent.m_WheelParams = new ManWheels.WheelParams
				{
					radius = TryParse(jObject, "radius", 0.5f),
					thicknessAngular = TryParse(jObject, "thicknessAngular", 20f),
					suspensionSpring = TryParse(jObject, "suspensionSpring", 200f),
					suspensionDamper = TryParse(jObject, "suspensionDamper", 80f),
					suspensionQuadratic = TryParse(jObject, "basicFrictionTorque", defaultValue: true),
					suspensionTravel = TryParse(jObject, "suspensionTravel", 0.2f),
					steerAngleMax = TryParse(jObject, "steerAngleMax", 0f),
					steerSpeed = TryParse(jObject, "steerSpeed", 2f),
					maxSuspensionAcceleration = TryParse(jObject, "maxSuspensionAcceleration", 15f)
				};
				JObject jObject3 = TryGetObject(jObject2, "Instantiate|tireProperties");
				if (jObject3 != null)
				{
					JObject jObject4 = TryGetObject(jObject3, "Instantiate|props");
					if (jObject4 != null)
					{
						orAddComponent.m_WheelParams.tireProperties = TireProperties.CreateInstance();
						orAddComponent.m_WheelParams.tireProperties.props = new ManWheels.TireProperties
						{
							frictionScaleLong = TryParse(jObject4, "frictionScaleLong", 1f),
							frictionScaleLat = TryParse(jObject4, "frictionScaleLat", 1f),
							gripFactorLong = TryParse(jObject4, "gripFactorLong", 1f),
							gripFactorLat = TryParse(jObject4, "gripFactorLat", 1f)
						};
					}
				}
			}
			JSONWheelLoader.ApplyTemplate(blockID, orAddComponent, component);
			return true;
		}
		return false;
	}
}
