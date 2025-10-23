using Newtonsoft.Json.Linq;
using UnityEngine;

public class JSONAnchorLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Anchor";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			ModuleAnchor orAddComponent = GetOrAddComponent<ModuleAnchor>(block);
			ModuleAnchor component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.BF_Anchor_Sky_222).GetComponent<ModuleAnchor>();
			orAddComponent.m_MaxAngularVelocity = TryParse(obj, "MaxAngularVelocity", 150f);
			orAddComponent.m_MaxTorque = TryParse(obj, "MaxTorque", 30000f);
			orAddComponent.m_BrakeTorque = TryParse(obj, "BrakeTorque", 20000f);
			orAddComponent.m_ForceHorizontal = TryParse(obj, "ForceHorizontal", defaultValue: true);
			orAddComponent.m_IsSkyAnchor = TryParse(obj, "IsSkyAnchor", defaultValue: false);
			Transform transform = JSONModuleLoader.ChildMatching(block.transform, "_anchor");
			if (transform == null)
			{
				return false;
			}
			Transform transform2 = Object.Instantiate(transform);
			BlockAnchor orAddComponent2 = GetOrAddComponent<BlockAnchor>(transform2);
			orAddComponent2.m_GroundPoint = JSONModuleLoader.ChildMatching(transform2, "_groundPoint");
			orAddComponent2.m_BeamPoint = JSONModuleLoader.ChildMatching(transform2, "_beam");
			orAddComponent2.m_AnchorGeometry = JSONModuleLoader.ChildMatching(transform2, "_geometry")?.gameObject;
			orAddComponent2.m_SnapToleranceUp = TryParse(obj, "SnapToleranceUp", 2f);
			orAddComponent2.m_SnapToleranceDown = TryParse(obj, "SnapToleranceDown", 80f);
			orAddComponent2.m_IsSkyAnchor = orAddComponent.m_IsSkyAnchor;
			if (orAddComponent.IsSkyAnchor)
			{
				orAddComponent2.m_SkyAnchorStrength = TryParse(obj, "SkyAnchorStrength", 1f);
				orAddComponent2.m_SkyAnchorDamping = TryParse(obj, "SkyAnchorDamping", 1f);
				orAddComponent2.m_SkyAnchorSpeed = TryParse(obj, "SkyAnchorSpeed", 100f);
				orAddComponent2.m_SkyAnchorRecoilForce = TryParse(obj, "SkyAnchorRecoilForce", 2f);
				orAddComponent2.m_DeathExplosion = component.m_SkyAnchorFirePoint.GetComponent<PrefabInstantiator>().m_Prefabs[0].GetComponent<BlockAnchor>().m_DeathExplosion;
				orAddComponent2.m_Corporation = Singleton.Manager<ManMods>.inst.GetCorpIndex(def.m_Corporation);
			}
			Object.DestroyImmediate(transform.gameObject, allowDestroyingAssets: true);
			GameObject gameObject = new GameObject("_anchor");
			gameObject.transform.SetParent(block.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			PrefabInstantiator orAddComponent3 = GetOrAddComponent<PrefabInstantiator>(gameObject.transform);
			orAddComponent3.m_Prefabs = new Transform[1] { transform2 };
			orAddComponent3.destroyAfterInstantiation = PrefabInstantiator.DestroyType.ThisComponent;
			orAddComponent3.instantiateAsChildren = true;
			if (orAddComponent.m_IsSkyAnchor)
			{
				Transform skyAnchorFirePoint = JSONModuleLoader.ChildMatching(block.transform, "_fire");
				Transform skyAnchorBeamAttachPoint = JSONModuleLoader.ChildMatching(block.transform, "_beamAttach");
				orAddComponent.m_SkyAnchorBeamAttachPoint = skyAnchorBeamAttachPoint;
				orAddComponent.m_SkyAnchorFirePoint = skyAnchorFirePoint;
			}
			return true;
		}
		return false;
	}

	public override bool InjectBlock(int blockID, ModdedBlockDefinition def, JToken jToken)
	{
		if (!base.InjectBlock(blockID, def, jToken))
		{
			return false;
		}
		return true;
	}
}
