#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : Singleton.Manager<ResourceManager>
{
	public class ResourceDefWrapper
	{
		public ResourceTable.Definition def;

		public PhysicMaterial physMat;

		public void InitMaterials()
		{
			if ((bool)Singleton.Manager<ResourceManager>.inst.defaultPhysicMaterial)
			{
				physMat = new PhysicMaterial();
				physMat.bounceCombine = Singleton.Manager<ResourceManager>.inst.defaultPhysicMaterial.bounceCombine;
				physMat.frictionCombine = Singleton.Manager<ResourceManager>.inst.defaultPhysicMaterial.frictionCombine;
				physMat.staticFriction = def.frictionStatic;
				physMat.dynamicFriction = def.frictionDynamic;
				physMat.bounciness = def.restitution;
			}
		}
	}

	public ResourceTable resourceTable;

	public PhysicMaterial defaultPhysicMaterial;

	public float editorUpdateDataDelay = 0.5f;

	public int resourcePoolSize = 500;

	private Dictionary<ChunkTypes, ResourceDefWrapper> m_DefinitionTable;

	private float editorUpdateDataTimer;

	private static readonly string[] s_ChunkTypeNames = Enum.GetNames(typeof(ChunkTypes));

	private void RebuildResources()
	{
		d.Log("rebuilding resources");
		if (m_DefinitionTable == null)
		{
			m_DefinitionTable = new Dictionary<ChunkTypes, ResourceDefWrapper>();
		}
		ResourceTable.Definition[] resources = resourceTable.resources;
		foreach (ResourceTable.Definition definition in resources)
		{
			if (definition.m_ChunkType != ChunkTypes.Null && !m_DefinitionTable.ContainsKey(definition.m_ChunkType))
			{
				m_DefinitionTable[definition.m_ChunkType] = new ResourceDefWrapper
				{
					def = definition
				};
				Visible component = definition.basePrefab.GetComponent<Visible>();
				ResourcePickup component2 = component.GetComponent<ResourcePickup>();
				Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(ItemTypeInfo.GetHashCode(ObjectTypes.Chunk, component.m_ItemType.ItemType), component2.ChunkRarity);
			}
		}
		foreach (ResourceDefWrapper value in m_DefinitionTable.Values)
		{
			value.InitMaterials();
		}
	}

	private void OnEditorUpdate()
	{
	}

	public ResourcePickup SpawnResource(ChunkTypes resourceType, Vector3 pos, Quaternion rot)
	{
		if (resourceType == ChunkTypes.Null)
		{
			return null;
		}
		if (!m_DefinitionTable.TryGetValue(resourceType, out var value))
		{
			return null;
		}
		Visible component = value.def.basePrefab.Spawn(pos, rot).GetComponent<Visible>();
		d.Assert((bool)component && component.type == ObjectTypes.Chunk && (bool)component.ColliderSwapper);
		component.ColliderSwapper.SetPhysicMaterial(value.physMat);
		component.rbody.mass = value.def.mass;
		component.name = s_ChunkTypeNames[(int)resourceType];
		return component.pickup;
	}

	public ResourceTable.Definition GetResourceDef(ChunkTypes chunkType)
	{
		if (m_DefinitionTable.ContainsKey(chunkType))
		{
			return m_DefinitionTable[chunkType].def;
		}
		return null;
	}

	public string[] GetAllResources()
	{
		return resourceTable.resources.Select((ResourceTable.Definition r) => r.name).ToArray();
	}

	public IEnumerable<ResourceTable.Definition> ResourceDefinitions()
	{
		return resourceTable.resources;
	}

	public ChunkTypes GetRawResource(ChunkTypes inputType)
	{
		int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Chunk, (int)inputType);
		ChunkCategory descriptorFlags = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode);
		if ((descriptorFlags & ChunkCategory.Refined) != ChunkCategory.Null)
		{
			return inputType - 1;
		}
		d.Assert((descriptorFlags & ChunkCategory.Raw) != 0, $"GetRawResource was passed {inputType}, which was neither Raw nor Refined!");
		return inputType;
	}

	private void Start()
	{
		RebuildResources();
	}

	private void Awake()
	{
		EditorHooks.Update += OnEditorUpdate;
	}
}
