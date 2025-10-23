#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitNode))]
public class ModuleCircuit_AdaptiveGeometry : Module, IBlockPlacementPreview
{
	[Serializable]
	public struct AdaptiveColliders
	{
		public Vector3 AP;

		public Collider Collider;
	}

	[Serializable]
	public class AdaptiveGeometryGroup
	{
		[SerializeField]
		public Transform geometryPrefab;

		[SerializeField]
		public Transform parent;

		[SerializeField]
		public Vector3[] supportedAPs;

		protected List<Transform> SpawnedSegments = new List<Transform>();

		public HashSet<Vector3> ActiveAPs = new HashSet<Vector3>();

		protected int debug_NumChildrenDefault = -1;

		public bool SupportsAP(Vector3 ap)
		{
			return supportedAPs.Any((Vector3 r) => ap == r);
		}

		public void SpawnActiveAPSegments()
		{
			if (debug_NumChildrenDefault == -1)
			{
				debug_NumChildrenDefault = parent.childCount;
			}
			ClearSegments();
			foreach (Vector3 activeAP in ActiveAPs)
			{
				Transform item = geometryPrefab.SpawnWithLocalTransform(parent, Vector3.zero, Quaternion.LookRotation(activeAP));
				SpawnedSegments.Add(item);
			}
		}

		public void ClearSegments()
		{
			foreach (Transform spawnedSegment in SpawnedSegments)
			{
				spawnedSegment.Recycle();
			}
			SpawnedSegments.Clear();
		}
	}

	[Serializable]
	public struct APSpawnInfo
	{
		public Vector3 apConnection;

		public bool showByDefault;
	}

	[SerializeField]
	protected AdaptiveColliders[] m_AdaptiveColliders = new AdaptiveColliders[0];

	[SerializeField]
	protected AdaptiveGeometryGroup m_SupportsGroup = new AdaptiveGeometryGroup();

	[SerializeField]
	protected AdaptiveGeometryGroup m_WiresGroup = new AdaptiveGeometryGroup();

	[SerializeField]
	protected AdaptiveGeometryGroup m_WireCoresGroup = new AdaptiveGeometryGroup();

	[SerializeField]
	protected AdaptiveGeometryGroup m_APCoversGroup = new AdaptiveGeometryGroup();

	[SerializeField]
	[Tooltip("Which APs should have wires connected when we demonstrate the look of an adaptive geometry wire that isn't connected to anything")]
	protected Vector3[] m_DemoAPs = new Vector3[2]
	{
		Vector3.forward,
		Vector3.back
	};

	[SerializeField]
	private GameObject m_CentralObject;

	[SerializeField]
	private bool m_ShowGeometryPreviewWhenHeld;

	protected AdaptiveGeometryGroup[] AllGroups;

	public static HashSet<Vector3> s_Wire_PreviewAPs = new HashSet<Vector3>();

	public static HashSet<Vector3> s_WireCore_PreviewAPs = new HashSet<Vector3>();

	public static HashSet<Vector3> s_Support_PreviewAPs = new HashSet<Vector3>();

	public static HashSet<Vector3> s_Cover_PreviewAPs = new HashSet<Vector3>();

	private bool m_SupportGeometryRequiresUpdate;

	private bool m_WireGeometryRequiresUpdate;

	private const int _k_ApConnectionTestCacheSize = 4;

	private static Vector3[] _s_ApConnectionTestCache = new Vector3[4];

	private void MarkSupportGeometryForDeferredUpdate()
	{
		m_SupportGeometryRequiresUpdate = true;
	}

	private void InstantlyUpdateSupportGeometry(bool setDefaultState = false, bool refreshVisibleGeometry = true)
	{
		m_SupportGeometryRequiresUpdate = false;
		m_SupportsGroup.ActiveAPs.Clear();
		Vector3[] supportedAPs = m_SupportsGroup.supportedAPs;
		foreach (Vector3 vector in supportedAPs)
		{
			if (setDefaultState)
			{
				continue;
			}
			if (s_Support_PreviewAPs.Contains(vector))
			{
				m_SupportsGroup.ActiveAPs.Add(vector);
				continue;
			}
			for (int j = 0; j < base.block.attachPoints.Length; j++)
			{
				Vector3 vector2 = base.block.attachPoints[j];
				if (vector2 == vector && base.block.ConnectedBlocksByAP[j] != null && !m_WiresGroup.ActiveAPs.Contains(vector2))
				{
					m_SupportsGroup.ActiveAPs.Add(vector2);
					break;
				}
			}
		}
		m_SupportsGroup.SpawnActiveAPSegments();
		if (refreshVisibleGeometry)
		{
			base.block.visible.RefreshMeshRendererList();
		}
		if (m_CentralObject != null)
		{
			bool active = (m_SupportsGroup.ActiveAPs.Count > 0 && (m_WiresGroup.ActiveAPs.Count == 0 || (m_WiresGroup.ActiveAPs.Count == 1 && m_SupportsGroup.ActiveAPs.Contains(m_WiresGroup.ActiveAPs.First() * -1f)))) || !m_WiresGroup.ActiveAPs.Any((Vector3 ap) => m_WiresGroup.ActiveAPs.Contains(ap * -1f));
			m_CentralObject.SetActive(active);
		}
	}

	private void MarkWireGeometryForDeferredUpdate()
	{
		m_WireGeometryRequiresUpdate = true;
	}

	private void InstantlyUpdateWireAndSupportGeometry(bool setToDefaultState = false)
	{
		m_WireGeometryRequiresUpdate = false;
		m_WiresGroup.ActiveAPs.Clear();
		m_WireCoresGroup.ActiveAPs.Clear();
		m_APCoversGroup.ActiveAPs.Clear();
		Vector3[] supportedAPs = m_WiresGroup.supportedAPs;
		foreach (Vector3 vector in supportedAPs)
		{
			if (setToDefaultState)
			{
				if (m_DemoAPs.Contains(vector))
				{
					m_WiresGroup.ActiveAPs.Add(vector);
					m_WireCoresGroup.ActiveAPs.Add(vector);
				}
				continue;
			}
			if (s_Wire_PreviewAPs.Contains(vector))
			{
				m_WiresGroup.ActiveAPs.Add(vector);
				if (s_Cover_PreviewAPs.Contains(vector))
				{
					m_APCoversGroup.ActiveAPs.Add(vector);
				}
				continue;
			}
			bool flag = false;
			for (int j = 0; j < base.block.ConnectedBlocksByAP.Length; j++)
			{
				if (base.block.ConnectedBlocksByAP[j] == null || base.block.ConnectedBlocksByAP[j].CircuitNode == null)
				{
					continue;
				}
				int num = base.block.CircuitNode.TryGetAPsForConnectedNode(base.block.ConnectedBlocksByAP[j].CircuitNode, ModuleCircuitNode.ConnexionTypes.Input | ModuleCircuitNode.ConnexionTypes.Output, _s_ApConnectionTestCache);
				if (4 < num)
				{
					d.LogWarning("Warning! We've hit out maximum supported circuit AP connections threshold per nodal connection!\nWe need to either scale the cache to support more connections or rework the blocks involved to not have funky connection points!\n" + $"For now, we'll default to only displaying the maximum ({4}) connections with no particular priority...\n" + "Blocks involved: " + base.block.name + " & " + base.block.ConnectedBlocksByAP[j].name);
					num = 4;
				}
				for (int k = 0; k < num; k++)
				{
					if (!(_s_ApConnectionTestCache[k] != vector))
					{
						m_WiresGroup.ActiveAPs.Add(vector);
						if ((base.block.ConnectedBlocksByAP[j].CircuitNode.ConfigFlags & ModuleCircuitNode.ConfigurationFlags.SuppressAPConnectionCovers) == 0)
						{
							m_APCoversGroup.ActiveAPs.Add(vector);
						}
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
		}
		InstantlyUpdateSupportGeometry(setToDefaultState, refreshVisibleGeometry: false);
		m_WiresGroup.SpawnActiveAPSegments();
		m_WireCoresGroup.SpawnActiveAPSegments();
		m_APCoversGroup.SpawnActiveAPSegments();
		SetAdaptiveCollidersActive(m_WiresGroup.ActiveAPs);
		base.block.visible.RefreshMeshRendererList();
	}

	private void SetAdaptiveCollidersActive(IEnumerable<Vector3> forAPs)
	{
		for (int i = 0; i < m_AdaptiveColliders.Length; i++)
		{
			m_AdaptiveColliders[i].Collider.gameObject.SetActive(forAPs.Contains(m_AdaptiveColliders[i].AP));
		}
	}

	private void RemovePreviews()
	{
		AdaptiveGeometryGroup[] allGroups = AllGroups;
		foreach (AdaptiveGeometryGroup adaptiveGeometryGroup in allGroups)
		{
			for (int num = adaptiveGeometryGroup.parent.childCount - 1; num >= 0; num--)
			{
				if (adaptiveGeometryGroup.parent.GetChild(num).name == adaptiveGeometryGroup.geometryPrefab.name)
				{
					UnityEngine.Object.DestroyImmediate(adaptiveGeometryGroup.parent.GetChild(num).gameObject);
				}
			}
		}
		for (int num2 = m_WiresGroup.parent.childCount - 1; num2 >= 0; num2--)
		{
			if (m_WiresGroup.parent.GetChild(num2).name == m_WiresGroup.geometryPrefab.name)
			{
				UnityEngine.Object.DestroyImmediate(m_WiresGroup.parent.GetChild(num2).gameObject);
			}
		}
	}

	private void OnDetached()
	{
		if (!ManSaveGame.Storing)
		{
			InstantlyUpdateWireAndSupportGeometry(setToDefaultState: true);
		}
	}

	private void OnAttached()
	{
		MarkWireGeometryForDeferredUpdate();
	}

	private void OnNeighbourDetached(TankBlock _)
	{
		MarkSupportGeometryForDeferredUpdate();
	}

	private void OnNeighbourAttached(TankBlock _)
	{
		MarkSupportGeometryForDeferredUpdate();
	}

	private void OnConnexionsUpdated()
	{
		MarkWireGeometryForDeferredUpdate();
	}

	public void TryPreviewAttachments(IEnumerable<BlockPlacementPreviewHandler.APConnection> previewConnections)
	{
		if (!base.block.tank.IsNotNull() && !m_ShowGeometryPreviewWhenHeld)
		{
			return;
		}
		if (previewConnections != null)
		{
			foreach (BlockPlacementPreviewHandler.APConnection previewConnection in previewConnections)
			{
				if (base.block.CircuitNode.IsNotNull() && previewConnection.otherBlock.CircuitNode.IsNotNull() && ModuleCircuitNode.Theoretics.GetCircuitConnectionFlagsWithUnattachedBlockOnAP(base.block.CircuitNode, previewConnection.blockAP, previewConnection.otherBlock.CircuitNode) != 0)
				{
					if (m_WiresGroup.SupportsAP(previewConnection.blockAP))
					{
						s_Wire_PreviewAPs.Add(previewConnection.blockAP);
					}
					if ((previewConnection.otherBlock.CircuitNode.ConfigFlags & ModuleCircuitNode.ConfigurationFlags.SuppressAPConnectionCovers) == 0 && m_APCoversGroup.SupportsAP(previewConnection.blockAP))
					{
						s_Cover_PreviewAPs.Add(previewConnection.blockAP);
					}
				}
				else if (m_SupportsGroup.SupportsAP(previewConnection.blockAP))
				{
					s_Support_PreviewAPs.Add(previewConnection.blockAP);
				}
			}
		}
		InstantlyUpdateWireAndSupportGeometry(!base.block.IsAttached && s_Wire_PreviewAPs.Count == 0 && s_Support_PreviewAPs.Count == 0);
		s_Support_PreviewAPs.Clear();
		s_Wire_PreviewAPs.Clear();
		s_WireCore_PreviewAPs.Clear();
		s_Cover_PreviewAPs.Clear();
	}

	private void PrePool()
	{
		AllGroups = new AdaptiveGeometryGroup[4] { m_APCoversGroup, m_SupportsGroup, m_WireCoresGroup, m_WiresGroup };
		RemovePreviews();
	}

	private void OnSpawn()
	{
		InstantlyUpdateWireAndSupportGeometry(setToDefaultState: true);
	}

	private void OnRecycle()
	{
		AdaptiveGeometryGroup[] allGroups = AllGroups;
		for (int i = 0; i < allGroups.Length; i++)
		{
			allGroups[i].ClearSegments();
		}
		m_WiresGroup.ClearSegments();
		base.block.visible.RefreshMeshRendererList();
	}

	private void OnPool()
	{
		AllGroups = new AdaptiveGeometryGroup[4] { m_APCoversGroup, m_SupportsGroup, m_WireCoresGroup, m_WiresGroup };
		base.block.DetachedEvent.Subscribe(OnDetached);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.NeighbourAttachedEvent.Subscribe(OnNeighbourAttached);
		base.block.NeighbourDetachedEvent.Subscribe(OnNeighbourDetached);
		base.block.CircuitNode.ConnexionsUpdatedEvent.Subscribe(OnConnexionsUpdated);
		base.block.BlockLateUpdate.Subscribe(OnLateUpdate);
	}

	private void OnDepool()
	{
		base.block.DetachedEvent.Unsubscribe(OnDetached);
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.NeighbourAttachedEvent.Unsubscribe(OnNeighbourAttached);
		base.block.NeighbourDetachedEvent.Unsubscribe(OnNeighbourDetached);
		base.block.CircuitNode.ConnexionsUpdatedEvent.Unsubscribe(OnConnexionsUpdated);
		base.block.BlockLateUpdate.Unsubscribe(OnLateUpdate);
	}

	private void OnLateUpdate()
	{
		if (m_WireGeometryRequiresUpdate)
		{
			InstantlyUpdateWireAndSupportGeometry();
		}
		if (m_SupportGeometryRequiresUpdate)
		{
			InstantlyUpdateSupportGeometry();
		}
	}
}
