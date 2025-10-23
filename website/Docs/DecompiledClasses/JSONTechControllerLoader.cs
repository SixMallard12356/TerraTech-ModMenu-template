using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class JSONTechControllerLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Cab";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			GetOrAddComponent<ModuleTechController>(block).m_PlayerInput = TryParse(obj, "AcceptPlayerInput", defaultValue: true);
			ModuleVision orAddComponent = GetOrAddComponent<ModuleVision>(block);
			orAddComponent.SetRange(150f);
			orAddComponent.visionConeAngle = 360f;
			GetOrAddComponent<TargetAimer>(block);
			ModuleAIBot orAddComponent2 = GetOrAddComponent<ModuleAIBot>(block);
			JArray jArray = TryGetArray(obj, "EnabledAITypes");
			if (jArray != null)
			{
				List<TechAI.AITypes> list = new List<TechAI.AITypes>(4);
				foreach (JToken item in jArray)
				{
					if (item.Type == JTokenType.String)
					{
						list.Add((TechAI.AITypes)Enum.Parse(typeof(TechAI.AITypes), item.ToObject<string>()));
					}
				}
				orAddComponent2.m_AITypesEnabled = list.ToArray();
			}
			ModuleDriveBot orAddComponent3 = GetOrAddComponent<ModuleDriveBot>(block);
			orAddComponent3.m_DefaultThrottle = TryParse(obj, "DefaultThrottle", 1f);
			orAddComponent3.turnAngleFullThrottle = TryParse(obj, "TurnAngleFullThrottle", 45f);
			orAddComponent3.m_ThrottleD = TryParse(obj, "ThrottleD", 0f);
			orAddComponent3.m_ThrottleT = TryParse(obj, "ThrottleT", 0f);
			orAddComponent3.turnToleranceOuter = TryParse(obj, "OuterTurnTolerance", 45f);
			orAddComponent3.turnToleranceInner = TryParse(obj, "InnerTurnTolerance", 20f);
			orAddComponent3.poweredTurnInsideWheel = TryParse(obj, "PoweredTurnInsideWheel", 0.2f);
			orAddComponent3.stopCirclingDelay = TryParse(obj, "StopCirclingDelay", 2f);
			orAddComponent3.targetIdealRange = TryParse(obj, "IdealTargetRange", 10f);
			orAddComponent3.lostTargetMemoryTime = TryParse(obj, "LostTargetMemoryTime", 3f);
			orAddComponent3.holdTargetDuration = TryParse(obj, "HoldTargetDuration", 0.5f);
			orAddComponent3.waypointReachedTolerance = TryParse(obj, "WaypointReachedTolerance", 2f);
			orAddComponent3.waypointPlayerAngularBias = TryParse(obj, "WaypointPlayerAngularBias", 45f);
			orAddComponent3.waypointDistanceFullThrottle = TryParse(obj, "WaypointDistanceFullThrottle", 5f);
			orAddComponent3.lookAroundAngleMin = TryParse(obj, "LookAroundAngleMin", 10f);
			orAddComponent3.lookAroundAngleMax = TryParse(obj, "LookAroundAngleMax", 80f);
			orAddComponent3.lookAroundPauseMin = TryParse(obj, "LookAroundPauseMin", 0.25f);
			orAddComponent3.lookAroundPauseMax = TryParse(obj, "LookAroundPauseMax", 0.5f);
			orAddComponent3.lookAroundThrottle = TryParse(obj, "LookAroundThrottle", 0.5f);
			orAddComponent3.m_DefaultPatrolDistMin = TryParse(obj, "DefaultPatrolDistanceMin", 5f);
			orAddComponent3.m_DefaultPatrolDistMax = TryParse(obj, "DefaultPatrolDistanceMax", 15f);
			orAddComponent3.patrolThrottle = TryParse(obj, "PatrolThrottle", 0.95f);
			orAddComponent3.controlPriority = TryParse(obj, "ControlPriority", 50);
			orAddComponent3.recoverTimeout = TryParse(obj, "RecoverTimeout", 3f);
			orAddComponent3.forceUnCapsizeTimeout = TryParse(obj, "ForceUncapsizeTimeout", 5f);
			orAddComponent3.capsizedMinSpeed = TryParse(obj, "CapsizedMinSpeed", 1.5f);
			return true;
		}
		return false;
	}
}
