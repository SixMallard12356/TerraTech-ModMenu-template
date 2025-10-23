#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class MeshMerger : MonoBehaviour
{
	public delegate void PostMergeHook();

	private struct Combiner
	{
		public List<CombineInstance> meshes;

		public Material material;

		public uint numIndices;
	}

	public bool m_autoMergeOnStart = true;

	public bool m_mergeColliders;

	private bool m_hasMerged;

	private static List<Component> s_TempComponents = new List<Component>();

	private static List<MeshFilter> s_ChildMeshFiltersToDestroy = new List<MeshFilter>();

	public PostMergeHook postMergeHook { get; set; }

	private void Start()
	{
		if (m_autoMergeOnStart)
		{
			CombineMeshes();
		}
	}

	public static GameObject FindParentWithComponent<T>(GameObject obj)
	{
		Transform transform = obj.transform;
		while (transform.parent != null)
		{
			if (transform.parent.GetComponent<T>() != null)
			{
				return transform.parent.gameObject;
			}
			transform = transform.parent.transform;
		}
		return null;
	}

	private void MergeLOD(ref LOD lod, int lodIndex, Matrix4x4 origTX)
	{
		List<MeshFilter> list = new List<MeshFilter>();
		List<Renderer> list2 = new List<Renderer>();
		Renderer[] renderers = lod.renderers;
		foreach (Renderer renderer in renderers)
		{
			if (renderer is MeshRenderer && (bool)renderer.GetComponent<MeshFilter>())
			{
				list.Add(renderer.GetComponent<MeshFilter>());
			}
			else
			{
				list2.Add(renderer);
			}
		}
		if (list.Count > 1)
		{
			Merge(list.ToArray(), origTX, list2, $"LOD{lodIndex}_merged");
			lod.renderers = list2.ToArray();
		}
	}

	private void Merge(MeshFilter[] childMeshFilters, Matrix4x4 origTX, List<Renderer> outRenderers, string mergedName)
	{
		Dictionary<string, Combiner> dictionary = new Dictionary<string, Combiner>();
		foreach (MeshFilter meshFilter in childMeshFilters)
		{
			Renderer component = meshFilter.GetComponent<Renderer>();
			uint vertexCount = (uint)meshFilter.sharedMesh.vertexCount;
			string text = component.sharedMaterial.GetHashCode().ToString("X");
			if (dictionary.ContainsKey(text) && dictionary[text].numIndices + vertexCount > 65535)
			{
				text += "_Overflow";
			}
			if (!dictionary.TryGetValue(text, out var value))
			{
				value = (dictionary[text] = new Combiner
				{
					meshes = new List<CombineInstance>(),
					material = component.sharedMaterial,
					numIndices = 0u
				});
			}
			value.meshes.Add(new CombineInstance
			{
				mesh = meshFilter.sharedMesh,
				transform = meshFilter.transform.localToWorldMatrix
			});
			value.numIndices += vertexCount;
			if (!m_mergeColliders)
			{
				continue;
			}
			Collider[] array = meshFilter.GetComponentsInChildren<Collider>().ToArray();
			foreach (Collider collider in array)
			{
				Type type = collider.GetType();
				Component component2 = base.gameObject.AddComponent(type);
				BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
				foreach (PropertyInfo item in type.GetProperties(bindingAttr).Concat(typeof(Collider).GetProperties(bindingAttr)))
				{
					if (item.GetGetMethod() != null && item.GetSetMethod() != null)
					{
						item.SetValue(component2, item.GetValue(collider, null), null);
					}
				}
				BoxCollider boxCollider = component2 as BoxCollider;
				if (boxCollider != null)
				{
					Vector3 center = collider.gameObject.transform.TransformPoint(boxCollider.center);
					boxCollider.center = center;
				}
				SphereCollider sphereCollider = component2 as SphereCollider;
				if (sphereCollider != null)
				{
					Vector3 center2 = collider.gameObject.transform.TransformPoint(sphereCollider.center);
					sphereCollider.center = center2;
				}
				CapsuleCollider capsuleCollider = component2 as CapsuleCollider;
				if (capsuleCollider != null)
				{
					Vector3 center3 = collider.gameObject.transform.TransformPoint(capsuleCollider.center);
					capsuleCollider.center = center3;
				}
			}
		}
		MeshRenderer component3 = childMeshFilters.First().GetComponent<MeshRenderer>();
		d.Assert(component3, "merging meshes: no child MeshRenderer");
		GameObject gameObject = null;
		foreach (KeyValuePair<string, Combiner> item2 in dictionary)
		{
			_ = item2.Key;
			Combiner value2 = item2.Value;
			GameObject gameObject2 = base.gameObject;
			if (!gameObject && gameObject2.GetComponent<MeshFilter>() == null && mergedName == null)
			{
				gameObject = gameObject2;
			}
			else
			{
				gameObject = new GameObject((mergedName == null) ? "submesh" : mergedName);
				gameObject.transform.parent = gameObject2.transform;
			}
			MeshFilter meshFilter2 = gameObject.AddComponent<MeshFilter>();
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			outRenderers?.Add(meshRenderer);
			meshRenderer.shadowCastingMode = component3.shadowCastingMode;
			meshRenderer.receiveShadows = component3.receiveShadows;
			meshRenderer.sharedMaterial = value2.material;
			meshFilter2.mesh = new Mesh();
			meshFilter2.mesh.CombineMeshes(value2.meshes.ToArray());
		}
		s_ChildMeshFiltersToDestroy.AddRange(childMeshFilters);
	}

	private void DestroyMeshFiltersDeferred()
	{
		foreach (MeshFilter item in s_ChildMeshFiltersToDestroy)
		{
			if (!item)
			{
				continue;
			}
			GameObject gameObject = item.gameObject;
			gameObject.GetComponents(s_TempComponents);
			if (s_TempComponents.Count == 3)
			{
				Transform parent = gameObject.transform.parent;
				UnityEngine.Object.DestroyImmediate(gameObject);
				while (parent != base.transform && parent.childCount == 0)
				{
					parent.GetComponents(s_TempComponents);
					if (s_TempComponents.Count > 1)
					{
						break;
					}
					GameObject obj = parent.gameObject;
					parent = parent.parent;
					UnityEngine.Object.DestroyImmediate(obj);
				}
			}
			else
			{
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				UnityEngine.Object.DestroyImmediate(item);
				if ((bool)component)
				{
					UnityEngine.Object.DestroyImmediate(component);
				}
			}
		}
		s_ChildMeshFiltersToDestroy.Clear();
	}

	private string GetDebugName()
	{
		GameObject gameObject = FindParentWithComponent<TerrainObject>(base.gameObject);
		if (!gameObject)
		{
			return base.name;
		}
		return gameObject.name;
	}

	public void CombineMeshes()
	{
		if (Globals.inst.m_DisableMeshMergers || m_hasMerged)
		{
			return;
		}
		m_hasMerged = true;
		MeshFilter[] array = GetComponentsInChildren<MeshFilter>();
		if ((bool)GetComponent<MeshFilter>() || (bool)GetComponent<MeshRenderer>())
		{
			d.LogError(GetDebugName() + ": merge root already has a Meshfilter: skipping");
		}
		else if (array.Length > 1)
		{
			try
			{
				Vector3 position = base.transform.position;
				Quaternion rotation = base.transform.rotation;
				Matrix4x4 origTX = Matrix4x4.TRS(position, rotation, Vector3.one);
				base.transform.position = Vector3.zero;
				base.transform.rotation = Quaternion.identity;
				LODGroup componentInChildren = GetComponentInChildren<LODGroup>();
				if (componentInChildren != null)
				{
					LOD[] lODs = componentInChildren.GetLODs();
					int maximumLODLevel = QualitySettings.maximumLODLevel;
					List<MeshFilter> list = GetComponentsInChildren<MeshFilter>().ToList();
					if (list != null)
					{
						list.RemoveAll((MeshFilter i) => !i.GetComponent<MeshRenderer>().enabled);
						for (int num = 0; num < lODs.Length; num++)
						{
							Renderer[] renderers = lODs[num].renderers;
							for (int num2 = 0; num2 < renderers.Length; num2++)
							{
								MeshFilter component = renderers[num2].GetComponent<MeshFilter>();
								if (component != null)
								{
									list.Remove(component);
								}
								else
								{
									d.LogWarning("Renderer from LOD in " + GetDebugName() + " did not have any MeshFilter to remove");
								}
							}
						}
						array = list.ToArray();
					}
					for (int num3 = maximumLODLevel; num3 < lODs.Length; num3++)
					{
						MergeLOD(ref lODs[num3], num3, origTX);
					}
					for (int num4 = 0; num4 < maximumLODLevel; num4++)
					{
						Renderer[] renderers = lODs[num4].renderers;
						foreach (Renderer renderer in renderers)
						{
							if (renderer is MeshRenderer)
							{
								MeshFilter component2 = renderer.GetComponent<MeshFilter>();
								if (component2 != null)
								{
									s_ChildMeshFiltersToDestroy.Add(component2);
								}
							}
						}
						lODs[num4].renderers = ((lODs.Length > maximumLODLevel) ? lODs[maximumLODLevel].renderers : null);
					}
					componentInChildren.SetLODs(lODs);
					if (maximumLODLevel >= lODs.Length)
					{
						d.LogWarningFormat("[MeshMerger.CombineMeshes] Object {0} has no LODs, after skipping LODs", GetDebugName());
					}
					if (array != null && array.Length != 0)
					{
						d.LogWarning("Non-LOD controlled meshes when mesh merging object " + GetDebugName());
						MeshFilter[] array2 = array;
						foreach (MeshFilter meshFilter in array2)
						{
							d.LogWarning("-- " + meshFilter.name, meshFilter);
						}
					}
				}
				if (array.Length > 1)
				{
					Merge(array, origTX, null, null);
				}
				DestroyMeshFiltersDeferred();
				base.transform.position = position;
				base.transform.rotation = rotation;
			}
			catch (Exception ex)
			{
				d.LogError("[MeshMerger.CombineMeshes]  Investigate this object: " + GetDebugName() + ", causes this exception: " + ex.Message);
			}
		}
		if (postMergeHook != null)
		{
			postMergeHook();
		}
		UnityEngine.Object.Destroy(this);
	}

	private void PrePool()
	{
		CombineMeshes();
	}
}
