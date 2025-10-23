#define UNITY_EDITOR
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JSONWingLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Wing";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			ModuleWing component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.GSOWingMini_311).GetComponent<ModuleWing>();
			JObject jObject = (JObject)jToken;
			ModuleWing orAddComponent = GetOrAddComponent<ModuleWing>(block);
			orAddComponent.m_AttackAngleDamping = TryParse(jObject, "AttackAngleDamping", 1f);
			orAddComponent.m_TrailMinVelocity = TryParse(jObject, "TrailMinimumVelocity", 8f);
			orAddComponent.m_TrailAlphaStrength = TryParse(jObject, "TrailTransparency", 0.01f);
			orAddComponent.m_TrailFadeSpeed = TryParse(jObject, "TrailFadeSpeed", 0.5f);
			Transform transform = JSONModuleLoader.ChildMatching(block.transform, "CenterOfMass");
			if (transform == null)
			{
				transform = JSONModuleLoader.ChildMatching(block.transform, "CentreOfMass");
			}
			List<ModuleWing.Aerofoil> list = new List<ModuleWing.Aerofoil>();
			if (jObject.TryGetValue("Aerofoils", out var value))
			{
				if (value.Type == JTokenType.Array)
				{
					foreach (JToken item2 in (JArray)value)
					{
						if (item2.Type == JTokenType.Object)
						{
							JObject obj = (JObject)item2;
							Transform transform2 = JSONModuleLoader.ChildMatching(block.transform, TryParse(obj, "ObjectName", "_aerofoil"));
							if (transform2 != null)
							{
								Spinner orAddComponent2 = GetOrAddComponent<Spinner>(transform2);
								orAddComponent2.m_Speed = 0f;
								orAddComponent2.m_RotationAxis = new Axis(TryParseEnum(obj, "SpinnerRotationAxis", Axis.AxisType.X));
								orAddComponent2.m_SteerAxis = new Axis(TryParseEnum(obj, "SpinnerSteerAxis", Axis.AxisType.Y));
								orAddComponent2.m_AutoSpin = TryParse(obj, "SpinnerAutoSpin", defaultValue: false);
								orAddComponent2.m_SpinUpTime = TryParse(obj, "SpinnerSpinUpTime", 1f);
								ModuleWing.Aerofoil item = new ModuleWing.Aerofoil
								{
									trans = ((transform == null) ? transform2 : transform),
									liftCurve = component.m_Aerofoils[0].liftCurve,
									liftStrength = TryParse(obj, "LiftStrength", 1f),
									flap = orAddComponent2,
									flapAngleRangeActual = TryParse(obj, "FlapAngleRangeActual", 30f),
									flapAngleRangeVisual = TryParse(obj, "FlapAngleRangeVisual", 30f),
									flapTurnSpeed = TryParse(obj, "FlapTurnSpeed", 1f)
								};
								list.Add(item);
							}
						}
						else
						{
							d.LogWarning("[Mods] Aerofoil tag for " + def.name + " was not a JObject");
						}
					}
				}
				else
				{
					d.LogWarning("[Mods] Aerofoil tag for " + def.name + " was not an array of aerofoil data");
				}
			}
			orAddComponent.m_Aerofoils = list.ToArray();
			if (jObject.TryGetValue("SmokeTrails", out var value2) && value2.Type == JTokenType.Array)
			{
				foreach (JToken item3 in (JArray)value2)
				{
					if (item3.Type == JTokenType.Object)
					{
						JObject obj2 = (JObject)item3;
						string text = TryParse(obj2, "ObjectName", "_smokeTrail");
						Transform transform3 = JSONModuleLoader.ChildMatching(block.transform, text);
						if (transform3 == null)
						{
							d.LogWarning("[Mods] SmokeTrail tag for " + def.name + " referred to object " + text + ", which could not be found. Adding a default at block 0,0,0");
							transform3 = new GameObject("_smokeTrail").transform;
							transform3.SetParent(block.transform);
							transform3.localPosition = Vector3.zero;
							transform3.localRotation = Quaternion.identity;
						}
						LineRenderer orAddComponent3 = GetOrAddComponent<LineRenderer>(transform3);
						LineRenderer componentInChildren = component.GetComponentInChildren<LineRenderer>();
						orAddComponent3.shadowCastingMode = componentInChildren.shadowCastingMode;
						orAddComponent3.receiveShadows = componentInChildren.receiveShadows;
						orAddComponent3.allowOcclusionWhenDynamic = componentInChildren.allowOcclusionWhenDynamic;
						orAddComponent3.motionVectorGenerationMode = componentInChildren.motionVectorGenerationMode;
						orAddComponent3.rendererPriority = componentInChildren.rendererPriority;
						orAddComponent3.sharedMaterials = componentInChildren.sharedMaterials;
						orAddComponent3.useWorldSpace = componentInChildren.useWorldSpace;
						orAddComponent3.loop = componentInChildren.loop;
						Vector3[] positions = new Vector3[componentInChildren.positionCount];
						componentInChildren.GetPositions(positions);
						orAddComponent3.SetPositions(positions);
						orAddComponent3.widthCurve = componentInChildren.widthCurve;
						orAddComponent3.colorGradient = componentInChildren.colorGradient;
						orAddComponent3.numCornerVertices = componentInChildren.numCornerVertices;
						orAddComponent3.numCapVertices = componentInChildren.numCapVertices;
						orAddComponent3.alignment = componentInChildren.alignment;
						orAddComponent3.textureMode = componentInChildren.textureMode;
						orAddComponent3.shadowBias = componentInChildren.shadowBias;
						orAddComponent3.generateLightingData = componentInChildren.generateLightingData;
						orAddComponent3.sortingLayerID = componentInChildren.sortingLayerID;
						orAddComponent3.sortingOrder = componentInChildren.sortingOrder;
						orAddComponent3.lightProbeUsage = componentInChildren.lightProbeUsage;
						orAddComponent3.reflectionProbeUsage = componentInChildren.reflectionProbeUsage;
						orAddComponent3.probeAnchor = null;
						SmokeTrail orAddComponent4 = GetOrAddComponent<SmokeTrail>(transform3);
						orAddComponent4.numberOfPoints = TryParse(obj2, "NumberOfPoints", 10);
						orAddComponent4.updateSpeed = TryParse(obj2, "UpdateSpeed", 0.25f);
						orAddComponent4.riseSpeed = TryParse(obj2, "RiseSpeed", 0.25f);
						orAddComponent4.spread = TryParse(obj2, "Spread", 0.2f);
					}
				}
			}
			return true;
		}
		return false;
	}
}
