#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SpawnTechData
{
	[SerializeField]
	private bool m_SpawnSpecificTech;

	[SerializeField]
	private TankPreset m_Preset;

	[SerializeField]
	private TechSpawnFilter m_Filter;

	[SerializeField]
	private string m_UniqueName;

	[SerializeField]
	private int m_TeamNo;

	[SerializeField]
	private string m_PositionName;

	[SerializeField]
	private float m_MaxPositionVariance;

	[SerializeField]
	private bool m_SpawnRegardlessOfObstructions;

	[SerializeField]
	[FormerlySerializedAs("m_UseDeliveryBomb")]
	public ManSpawn.SpawnVisualType m_SpawnTechVisualType;

	[SerializeField]
	public ManSpawn.CustomSpawnEffectType m_SpawnVisualCustomEffectType;

	[SerializeField]
	private bool m_AllowSpawnInSceneryBlocker;

	[SerializeField]
	private bool m_IgnoreGroundProjectionOnPlacement;

	[SerializeField]
	private bool m_SurvivesEncounter;

	[Tooltip("Whether killing this tech will award the player with XP and BB")]
	[SerializeField]
	private bool m_HasRewardValue = true;

	[Tooltip("Whether explode blocks when they detach from this tech or let them drop and be picked up")]
	[SerializeField]
	private bool m_ShouldExplodeDetachingBlocks;

	[Tooltip("Delay when exploding detaching blocks (only used when ShouldExplodeDetachingBlocks is enabled)")]
	[SerializeField]
	private float m_ExplodeDetachingBlocksDelay = 0.5f;

	[SerializeField]
	private bool m_Invulnerable;

	[SerializeField]
	private bool m_UseAI = true;

	[SerializeField]
	private AITreeType m_AITreeToUse;

	[EnumString(typeof(AITreeType.AITypes))]
	[SerializeField]
	private EnumString m_AITreeType = new EnumString(typeof(AITreeType.AITypes), 0);

	[SerializeField]
	private ItemTypeInfo m_AITargetType;

	[SerializeField]
	private bool m_AITargetAnyOfType;

	[SerializeField]
	private bool m_OverrideOnSwitch;

	[SerializeField]
	[InspectorVisibilityControl("m_OverrideOnSwitch", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private bool m_RemoveOnSwitch;

	[SerializeField]
	[InspectorVisibilityControl("m_OverrideOnSwitch", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private TechSpawnFilter m_FilterOnSwitch;

	[SerializeField]
	[InspectorVisibilityControl("m_OverrideOnSwitch", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private TankPreset m_PresetOnSwitch;

	[SerializeField]
	[Tooltip("Spawn with markers and radar markers visible")]
	private bool m_ShowMarkerOverlay = true;

	public string UniqueName => m_UniqueName;

	public TechData SpecificTechData
	{
		get
		{
			TankPreset tankPreset = ((m_OverrideOnSwitch && SKU.SwitchUI && m_PresetOnSwitch != null) ? m_PresetOnSwitch : m_Preset);
			if (!m_SpawnSpecificTech)
			{
				return null;
			}
			return tankPreset.GetTechDataFormatted();
		}
	}

	public bool CanSpawnOnCurrentSKU()
	{
		if (m_OverrideOnSwitch && SKU.SwitchUI && m_RemoveOnSwitch)
		{
			return false;
		}
		return true;
	}

	public bool SpawnTechInEncounter(Encounter encounterToSpawnInto, float spawnDelay = 0f, string nameOverride = null, bool allowResurrection = false)
	{
		if (!CanSpawnOnCurrentSKU())
		{
			return false;
		}
		string text = ((!nameOverride.NullOrEmpty()) ? nameOverride : UniqueName);
		Encounter.EncounterVisibleState visibleState = encounterToSpawnInto.GetVisibleState(text);
		if (visibleState != Encounter.EncounterVisibleState.NotFoundInEncounter)
		{
			if (!allowResurrection || visibleState != Encounter.EncounterVisibleState.Killed)
			{
				d.LogError($"SpawnTechInEncounter cannot spawn tech {text} because it already exists in encounter with state {visibleState}.");
				return false;
			}
			encounterToSpawnInto.RemoveDeadVisible(text);
		}
		bool result = false;
		TechSpawnFilter techSpawnFilter = m_Filter;
		if (m_OverrideOnSwitch && SKU.SwitchUI && m_FilterOnSwitch != null)
		{
			techSpawnFilter = m_FilterOnSwitch;
		}
		Vector3 scenePos = encounterToSpawnInto.GetPosition(m_PositionName);
		if (m_MaxPositionVariance > 0f)
		{
			Vector3 vector = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f) * Vector3.forward * UnityEngine.Random.value * m_MaxPositionVariance;
			scenePos += vector;
		}
		Quaternion rotation = encounterToSpawnInto.GetRotation(m_PositionName);
		TechData techData = SpecificTechData;
		if (techData == null && techSpawnFilter != null)
		{
			float radius = encounterToSpawnInto.GetRadius(m_PositionName);
			Singleton.Manager<ManEncounter>.inst.EncounterRadiusFilter.UseRadius(0f, radius);
			TechSpawnFilter[] filters = new TechSpawnFilter[2]
			{
				techSpawnFilter,
				Singleton.Manager<ManEncounter>.inst.EncounterRadiusFilter
			};
			techData = Singleton.Manager<ManPresetFilter>.inst.GetRandomTechDataFromFilterList(filters);
			if (techData == null)
			{
				TankPreset randomFallbackTech = techSpawnFilter.GetRandomFallbackTech();
				if (randomFallbackTech != null)
				{
					techData = randomFallbackTech.GetTechDataFormatted();
					d.Log("SpawnTechData.SpawnTech: SpawnData for Tech in " + encounterToSpawnInto.name + " is set to use a filter, but could not find any valid techs! Spawning Fallback tech " + techData.Name + " instead!");
				}
			}
		}
		if (techData != null)
		{
			ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
			{
				m_ObjectsToAvoid = (m_SpawnRegardlessOfObstructions ? new Bitfield<ObjectTypes>() : ManSpawn.AvoidSceneryVehiclesCrates),
				m_CircleRadius = techData.Radius,
				m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
				m_CircleIndex = 0,
				m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
				m_CheckSafeArea = false,
				m_RejectFunc = null,
				m_AllowSpawnInSceneryBlocker = m_AllowSpawnInSceneryBlocker,
				m_AllowUnloadedTiles = encounterToSpawnInto.GetRequiredSetPiece()
			};
			int team = ((m_TeamNo == 0 && Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? Singleton.Manager<ManPlayer>.inst.PlayerTeam : m_TeamNo);
			ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
			{
				m_TechToSpawn = techData,
				m_AIType = (m_UseAI ? m_AITreeToUse : null),
				m_Team = team,
				m_Rotation = rotation,
				m_Grounded = true,
				m_SpawnVisualType = m_SpawnTechVisualType,
				m_SpawnVisualCustomEffectType = m_SpawnVisualCustomEffectType,
				m_DelayBeforeBombSpawn = spawnDelay,
				m_HasRewardValue = m_HasRewardValue,
				m_ShouldExplodeDetachingBlocks = m_ShouldExplodeDetachingBlocks,
				m_ExplodeDetachingBlocksDelay = m_ExplodeDetachingBlocksDelay,
				m_IgnoreSceneryOnGroundProjection = m_IgnoreGroundProjectionOnPlacement,
				m_ShowMarkerOverlay = m_ShowMarkerOverlay,
				m_Invulnerable = m_Invulnerable
			};
			PerVisibleParams encounterParams = new PerVisibleParams(m_SurvivesEncounter);
			encounterToSpawnInto.SpawnObject(objectSpawnParams, freeSpaceParams, encounterParams, text);
			result = true;
		}
		else
		{
			string message = (m_SpawnSpecificTech ? ("ERROR: SpawnTechData.SpawnTech: SpawnData for Tech in " + encounterToSpawnInto.name + " Is set to SpawnSpecificTech, but Preset Field is empty") : ((!techSpawnFilter) ? ("ERROR: SpawnTechData.SpawnTech: SpawnData for Tech in " + encounterToSpawnInto.name + " Is set to use a filter, but the Filter Field is empty") : ("ERROR: SpawnTechData.SpawnTech: SpawnData for Tech in " + encounterToSpawnInto.name + " Is set to use filter " + techSpawnFilter.name + ", but found no Valid Techs")));
			d.LogError(message);
		}
		return result;
	}
}
