#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuitNode : Module, ManVisible.StateVisualiser.Provider
{
	[Flags]
	public enum ConfigurationFlags
	{
		SuppressAPConnectionCovers = 1,
		IsConnectedToOtherNodes = 2,
		IsConnexionLayerBus = 4
	}

	[Flags]
	public enum ConnexionTypes
	{
		Input = 1,
		Output = 2
	}

	[Flags]
	public enum AutoGenChargePoints
	{
		None = 0,
		AsInputs = 2,
		AsOutputs = 4
	}

	public class Connexion
	{
		public Vector3 AttachPoint;

		public ModuleCircuitNode ParentNode;

		private ushort configBits;

		private const ushort kTypeBits = 61440;

		private const ushort kGroupBits = 3840;

		private const ushort kLayerBits = 255;

		private const int kTypeBitOffset = 12;

		private const int kGroupBitOffset = 8;

		private const int kLayerBitOffset = 0;

		public int NetworkID;

		public Connexion LinkedConnexion;

		public ConnexionTypes Type
		{
			get
			{
				return (ConnexionTypes)GetFromConfigBits(61440, 12);
			}
			set
			{
				AssignConfigBits((int)value, 61440, 12);
			}
		}

		public CircuitLayer Layer
		{
			get
			{
				return (CircuitLayer)GetFromConfigBits(255, 0);
			}
			set
			{
				AssignConfigBits((int)value, 255, 0);
			}
		}

		public int NetworkGroup
		{
			get
			{
				return GetFromConfigBits(3840, 8);
			}
			set
			{
				AssignConfigBits(value, 3840, 8);
			}
		}

		public Vector3 LocalTankPosition => ParentNode.block.cachedLocalPosition + ParentNode.block.cachedLocalRotation * AttachPoint;

		private int GetFromConfigBits(ushort mask, int offset)
		{
			return (configBits & mask) >> offset;
		}

		private void AssignConfigBits(int input, ushort mask, int offset)
		{
			configBits = (ushort)((configBits & ~mask) | ((input << offset) & mask));
		}

		public Connexion(Vector3 attachPoint, ConnexionTypes type, CircuitLayer layer, int networkGroup, ModuleCircuitNode node)
		{
			AttachPoint = attachPoint;
			Type = type;
			Layer = layer;
			NetworkGroup = networkGroup;
			ParentNode = node;
			NetworkID = -1;
			LinkedConnexion = null;
		}

		public bool TrySetNetworkID(int value)
		{
			bool result = NetworkID != value;
			NetworkID = value;
			return result;
		}
	}

	public class ConnexionLinkMetadata
	{
		private static List<ModuleCircuitNode> _s_CheckedNodes = new List<ModuleCircuitNode>();

		public int ConnectionCount { get; private set; }

		public int InputCount { get; private set; }

		public int OutputCount { get; private set; }

		public int ConnectedNodeCount { get; private set; }

		public int InputNodeCount { get; private set; }

		public int OutputNodeCount { get; private set; }

		public HashSet<Connexion> LinkedConnexions { get; private set; }

		public Dictionary<int, List<Connexion>> NetworkInputConnexionsLookup { get; private set; }

		public Dictionary<int, List<Connexion>> NetworkOutputConnexionsLookup { get; private set; }

		public List<Connexion> ConnexionsIterableList { get; private set; }

		public static ConnexionLinkMetadata empty => new ConnexionLinkMetadata
		{
			ConnectionCount = 0,
			InputCount = 0,
			OutputCount = 0,
			ConnectedNodeCount = 0,
			InputNodeCount = 0,
			OutputNodeCount = 0,
			LinkedConnexions = new HashSet<Connexion>(),
			ConnexionsIterableList = new List<Connexion>(),
			NetworkInputConnexionsLookup = new Dictionary<int, List<Connexion>>(),
			NetworkOutputConnexionsLookup = new Dictionary<int, List<Connexion>>()
		};

		private void Clear()
		{
			ConnectionCount = 0;
			InputCount = 0;
			OutputCount = 0;
			ConnectedNodeCount = 0;
			InputNodeCount = 0;
			OutputNodeCount = 0;
			LinkedConnexions.Clear();
			ConnexionsIterableList.Clear();
			NetworkInputConnexionsLookup.Clear();
			NetworkOutputConnexionsLookup.Clear();
		}

		public void RefreshConnexionsData(IEnumerable<Connexion> connexions)
		{
			Clear();
			_s_CheckedNodes.Clear();
			foreach (Connexion connexion in connexions)
			{
				ConnexionsIterableList.Add(connexion);
				if (!(connexion.ParentNode == null) && connexion.LinkedConnexion != null)
				{
					LinkedConnexions.Add(connexion.LinkedConnexion);
					bool flag = !_s_CheckedNodes.Contains(connexion.LinkedConnexion.ParentNode);
					if (flag)
					{
						_s_CheckedNodes.Add(connexion.LinkedConnexion.ParentNode);
					}
					ConnectedNodeCount += (flag ? 1 : 0);
					ConnectionCount++;
					if ((connexion.Type & ConnexionTypes.Input) != 0)
					{
						InputNodeCount += (flag ? 1 : 0);
						InputCount++;
					}
					if ((connexion.Type & ConnexionTypes.Output) != 0)
					{
						OutputNodeCount += (flag ? 1 : 0);
						OutputCount++;
					}
				}
			}
		}

		public void RefreshNetworkData()
		{
			NetworkInputConnexionsLookup.Clear();
			NetworkOutputConnexionsLookup.Clear();
			foreach (Connexion connexionsIterable in ConnexionsIterableList)
			{
				if ((connexionsIterable.Type & ConnexionTypes.Input) != 0)
				{
					if (!NetworkInputConnexionsLookup.TryGetValue(connexionsIterable.NetworkID, out var value))
					{
						NetworkInputConnexionsLookup[connexionsIterable.NetworkID] = new List<Connexion>();
						value = NetworkInputConnexionsLookup[connexionsIterable.NetworkID];
					}
					value.Add(connexionsIterable);
				}
				if ((connexionsIterable.Type & ConnexionTypes.Output) != 0)
				{
					if (!NetworkOutputConnexionsLookup.TryGetValue(connexionsIterable.NetworkID, out var value2))
					{
						NetworkOutputConnexionsLookup[connexionsIterable.NetworkID] = new List<Connexion>();
						value2 = NetworkOutputConnexionsLookup[connexionsIterable.NetworkID];
					}
					value2.Add(connexionsIterable);
				}
			}
		}
	}

	public static class Theoretics
	{
		public static bool CanReceiveChargeThroughAP(ModuleCircuitNode node, Vector3 localAP)
		{
			return node.ChargeInPoints.Contains(localAP);
		}

		public static bool CanOutputChargeThroughAP(ModuleCircuitNode node, Vector3 localAP)
		{
			return node.ChargeOutPoints.Contains(localAP);
		}

		public static bool CanReceiveChargeThroughAPs(ModuleCircuitNode node, IEnumerable<Vector3> localAPs)
		{
			return localAPs.Any((Vector3 r) => CanReceiveChargeThroughAP(node, r));
		}

		public static bool CanOutputChargeThroughAPs(ModuleCircuitNode node, IEnumerable<Vector3> localAPs)
		{
			return localAPs.Any((Vector3 r) => CanOutputChargeThroughAP(node, r));
		}

		public static bool CanFormInputLinkWithOther(Connexion connexion, Connexion otherConnexion)
		{
			if (AreConnexionsCompatible(connexion, otherConnexion) && (connexion.Type & ConnexionTypes.Input) != 0)
			{
				return (otherConnexion.Type & ConnexionTypes.Output) != 0;
			}
			return false;
		}

		public static bool CanFormOutputLinkWithOther(Connexion connexion, Connexion otherConnexion)
		{
			return CanFormInputLinkWithOther(otherConnexion, connexion);
		}

		public static ConnexionTypes GetCircuitConnectionFlagsWith(Connexion connexion, Connexion otherConnexion)
		{
			return (ConnexionTypes)(0 | (CanFormInputLinkWithOther(connexion, otherConnexion) ? 1 : 0) | (CanFormOutputLinkWithOther(connexion, otherConnexion) ? 2 : 0));
		}

		public static ConnexionTypes GetCircuitConnectionFlagsWithUnattachedBlockOnAP(ModuleCircuitNode node, Vector3 blockAP, ModuleCircuitNode unattachedNode)
		{
			Vector3 vector = BlockPlacementPreviewHandler.TransformBlockAPToTargetBlockAP(node.block, blockAP, unattachedNode.block);
			ConnexionTypes connexionTypes = (ConnexionTypes)0;
			if (node.ConnexionLookupByAP.TryGetValue(blockAP, out var value) && unattachedNode.ConnexionLookupByAP.TryGetValue(vector, out var value2) && AreConnexionsCompatible(value, value2))
			{
				connexionTypes = (ConnexionTypes)((int)connexionTypes | ((CanOutputChargeThroughAP(unattachedNode, vector) && CanReceiveChargeThroughAP(node, blockAP)) ? 1 : 0));
				connexionTypes = (ConnexionTypes)((int)connexionTypes | ((CanOutputChargeThroughAP(node, blockAP) && CanReceiveChargeThroughAP(unattachedNode, vector)) ? 2 : 0));
			}
			return connexionTypes;
		}
	}

	[Flags]
	public enum CircuitLayer
	{
		Red = 1,
		Blue = 2,
		Green = 4,
		Yellow = 8
	}

	[SerializeField]
	[Tooltip("The default setting of whether or not a charge can pass through this node, this is overwritten to false if there are any charge dispensor components on this block")]
	protected bool m_CarryChargeDefault = true;

	[SerializeField]
	[EnumFlag]
	private ConfigurationFlags m_ConfigFlags;

	[SerializeField]
	[EnumFlag]
	protected CircuitLayer m_LayerFlags = (CircuitLayer)(-1);

	[SerializeField]
	protected CircuitLayer[] m_APLayerFlagOverrides;

	[SerializeField]
	protected int[] m_APNetworkGroups;

	[HideInInspector]
	[SerializeField]
	public Vector3[] ChargeInPoints = new Vector3[0];

	[SerializeField]
	[HideInInspector]
	public Vector3[] ChargeOutPoints = new Vector3[0];

	[HideInInspector]
	[SerializeField]
	protected ModuleCircuitReceiver m_Receiver;

	[HideInInspector]
	[SerializeField]
	protected ModuleCircuitDispensor m_Dispensor;

	public Event<bool> ConnectedToOtherNodesEvent;

	public EventNoParams ConnexionsUpdatedEvent;

	private ConnexionLinkMetadata m_ConnexionMetadata = ConnexionLinkMetadata.empty;

	private static Vector3[] _s_TryGetConnectedNodeOneSizeArray = new Vector3[1];

	private static HashSet<Connexion> _s_ConnectedCircuitNodeConnexions = new HashSet<Connexion>();

	private static HashSet<ModuleCircuitNode> _s_NodesWithDirtyLinks = new HashSet<ModuleCircuitNode>();

	public List<Connexion> Connexions { get; private set; } = new List<Connexion>();

	public Dictionary<Vector3, Connexion> ConnexionLookupByAP { get; private set; } = new Dictionary<Vector3, Connexion>();

	public ConnexionLinkMetadata ConnexionMetadata => m_ConnexionMetadata;

	public ConfigurationFlags ConfigFlags => m_ConfigFlags;

	public ModuleCircuitReceiver Receiver => m_Receiver;

	public ModuleCircuitDispensor Dispensor => m_Dispensor;

	public int CurrentHighestCharge => Mathf.Max((Receiver != null) ? Receiver.CurrentHighestChargeFromNetwork : 0, (Dispensor != null) ? Dispensor.CurrentHighestChargeFromHere : 0);

	public bool IsChargeCarrier => m_CarryChargeDefault;

	public bool IsNetworkBus => (m_ConfigFlags & ConfigurationFlags.IsConnexionLayerBus) != 0;

	public bool IsConnectedToOtherNodes
	{
		get
		{
			return (m_ConfigFlags & ConfigurationFlags.IsConnectedToOtherNodes) != 0;
		}
		private set
		{
			m_ConfigFlags = (ConfigurationFlags)((int)(m_ConfigFlags & ~ConfigurationFlags.IsConnectedToOtherNodes) | (value ? 2 : 0));
		}
	}

	public HashSet<int> CircuitNetworkIDs { get; set; } = new HashSet<int>();

	public Dictionary<Vector3, int> OutputAPNetworkIDs { get; set; } = new Dictionary<Vector3, int>();

	public AutoGenChargePoints AutoGenChargePointsForAllAPs { get; set; }

	public void RefreshConnexionLinksUpdated()
	{
		m_ConnexionMetadata.RefreshConnexionsData(Connexions);
		if (IsConnectedToOtherNodes != ConnexionMetadata.ConnectedNodeCount > 0)
		{
			IsConnectedToOtherNodes = ConnexionMetadata.ConnectedNodeCount > 0;
			ConnectedToOtherNodesEvent.Send(IsConnectedToOtherNodes);
		}
		ConnexionsUpdatedEvent.Send();
	}

	public void RefreshCircuitNetworksUpdated()
	{
		CircuitNetworkIDs.Clear();
		OutputAPNetworkIDs.Clear();
		foreach (Connexion connexion in Connexions)
		{
			if (connexion.NetworkID != -1)
			{
				CircuitNetworkIDs.Add(connexion.NetworkID);
				if ((connexion.Type & ConnexionTypes.Output) != 0)
				{
					OutputAPNetworkIDs[connexion.AttachPoint] = connexion.NetworkID;
				}
			}
		}
		m_ConnexionMetadata.RefreshNetworkData();
	}

	public bool IsCompatibleWithBlock(ModuleCircuitNode otherNode)
	{
		return (otherNode.m_LayerFlags & m_LayerFlags) != 0;
	}

	public static bool AreConnexionsCompatible(Connexion connexion, Connexion otherConnexion)
	{
		return (connexion.Layer & otherConnexion.Layer) != 0;
	}

	public IEnumerable<Connexion> ConnexionsInSameNetworkGroup(Connexion connexion)
	{
		if (m_APNetworkGroups == null || m_APNetworkGroups.Length == 0)
		{
			return Connexions;
		}
		return SiblingConnexions(connexion);
		IEnumerable<Connexion> SiblingConnexions(Connexion rootConnexion)
		{
			int group = rootConnexion.NetworkGroup;
			foreach (Connexion connexion2 in Connexions)
			{
				if (connexion2.NetworkGroup == group)
				{
					yield return connexion2;
				}
			}
		}
	}

	public bool TryGetAPForConnectedNode(ModuleCircuitNode otherNode, ConnexionTypes connectionType, out Vector3 attachPoint)
	{
		int num = TryGetAPsForConnectedNode(otherNode, connectionType, _s_TryGetConnectedNodeOneSizeArray);
		attachPoint = _s_TryGetConnectedNodeOneSizeArray[0];
		return num > 0;
	}

	public int TryGetAPsForConnectedNode(ModuleCircuitNode otherNode, ConnexionTypes connectionType, Vector3[] nonAllocArray)
	{
		d.Assert(otherNode != null, "Tried to get a the relative APs for a null node when that isn't possible! Report to code!");
		int num = 0;
		foreach (Connexion connexion in Connexions)
		{
			if (connexion.LinkedConnexion != null && !(connexion.LinkedConnexion.ParentNode != otherNode) && (connectionType & connexion.Type) != 0)
			{
				if (nonAllocArray != null && num < nonAllocArray.Length)
				{
					nonAllocArray[num] = connexion.AttachPoint;
				}
				num++;
			}
		}
		return num;
	}

	public bool HasConnectionOnAP(Vector3 localAP, ConnexionTypes connexionType = ConnexionTypes.Input | ConnexionTypes.Output)
	{
		if (ConnexionLookupByAP.TryGetValue(localAP, out var value) && value.LinkedConnexion != null)
		{
			return (value.Type & connexionType) != 0;
		}
		return false;
	}

	public bool HasSharedNetworkWith(ModuleCircuitNode otherNode)
	{
		foreach (int circuitNetworkID in otherNode.CircuitNetworkIDs)
		{
			if (CircuitNetworkIDs.Contains(circuitNetworkID))
			{
				return true;
			}
		}
		return false;
	}

	private void InitConnexions()
	{
		AddChargePtsToConnexions(ChargeInPoints, ConnexionTypes.Input);
		AddChargePtsToConnexions(ChargeOutPoints, ConnexionTypes.Output);
		foreach (Connexion value in ConnexionLookupByAP.Values)
		{
			Connexions.Add(value);
		}
		RefreshConnexionLinksUpdated();
		void AddChargePtsToConnexions(Vector3[] chargePts, ConnexionTypes type)
		{
			for (int i = 0; i < chargePts.Length; i++)
			{
				int apIndex;
				CircuitLayer layer = GetCircuitLayer(chargePts[i], out apIndex);
				if (ConnexionLookupByAP.ContainsKey(chargePts[i]))
				{
					ConnexionLookupByAP[chargePts[i]].Type |= type;
				}
				else
				{
					int networkGroup = 0;
					if (m_APNetworkGroups != null && m_APNetworkGroups.Length != 0)
					{
						if (apIndex == -1)
						{
							apIndex = Array.IndexOf(base.block.attachPoints, chargePts[i]);
						}
						networkGroup = m_APNetworkGroups[apIndex];
					}
					ConnexionLookupByAP[chargePts[i]] = new Connexion(chargePts[i], type, layer, networkGroup, this);
				}
			}
		}
		CircuitLayer GetCircuitLayer(Vector3 apPos, out int apIndex)
		{
			apIndex = -1;
			if (m_APLayerFlagOverrides == null || m_APLayerFlagOverrides.Length == 0)
			{
				return m_LayerFlags;
			}
			apIndex = Array.IndexOf(base.block.attachPoints, apPos);
			d.AssertFormat(apIndex >= 0 && apIndex < m_APLayerFlagOverrides.Length, this, "Invalid ap Index {0} found for AP {1} on block {2} when looking up m_APLayerFlagOverrides", apIndex, apPos, base.name);
			return m_APLayerFlagOverrides[apIndex];
		}
	}

	private void UpdateConnexionLinks()
	{
		_s_ConnectedCircuitNodeConnexions.Clear();
		if (base.block.IsAttached)
		{
			for (int i = 0; i < base.block.attachPoints.Length; i++)
			{
				TankBlock tankBlock = base.block.ConnectedBlocksByAP[i];
				if (tankBlock.IsNull() || !tankBlock.IsAttached || tankBlock.CircuitNode.IsNull())
				{
					continue;
				}
				Vector3 vector = base.block.attachPoints[i];
				ConnexionLookupByAP.TryGetValue(vector, out var value);
				if (value == null)
				{
					continue;
				}
				Vector3 key = BlockPlacementPreviewHandler.TransformBlockAPToTargetBlockAP(base.block, vector, tankBlock);
				tankBlock.CircuitNode.ConnexionLookupByAP.TryGetValue(key, out var value2);
				if (value2 == null || Theoretics.GetCircuitConnectionFlagsWith(value, value2) == (ConnexionTypes)0)
				{
					continue;
				}
				foreach (Connexion item in tankBlock.CircuitNode.ConnexionsInSameNetworkGroup(value2))
				{
					_s_ConnectedCircuitNodeConnexions.Add(item);
				}
			}
		}
		foreach (Connexion connexion in Connexions)
		{
			if (connexion.LinkedConnexion != null)
			{
				Connexion linkedConnexion = connexion.LinkedConnexion;
				connexion.LinkedConnexion = null;
				linkedConnexion.LinkedConnexion = null;
				_s_NodesWithDirtyLinks.Add(connexion.ParentNode);
				_s_NodesWithDirtyLinks.Add(linkedConnexion.ParentNode);
			}
			if (!base.block.IsAttached)
			{
				continue;
			}
			foreach (Connexion s_ConnectedCircuitNodeConnexion in _s_ConnectedCircuitNodeConnexions)
			{
				if (AreConnexionsCompatible(connexion, s_ConnectedCircuitNodeConnexion) && (connexion.LocalTankPosition - s_ConnectedCircuitNodeConnexion.LocalTankPosition).IsZeroEpsilon(0.1f))
				{
					s_ConnectedCircuitNodeConnexion.LinkedConnexion = connexion;
					connexion.LinkedConnexion = s_ConnectedCircuitNodeConnexion;
					_s_NodesWithDirtyLinks.Add(connexion.ParentNode);
					_s_NodesWithDirtyLinks.Add(s_ConnectedCircuitNodeConnexion.ParentNode);
				}
			}
		}
		_s_NodesWithDirtyLinks.Add(this);
		foreach (ModuleCircuitNode s_NodesWithDirtyLink in _s_NodesWithDirtyLinks)
		{
			s_NodesWithDirtyLink.RefreshConnexionLinksUpdated();
		}
		_s_NodesWithDirtyLinks.Clear();
		base.block.lastTank.Circuits.ClearNetworksAndMarkForDeferredNetworkRecalculation(this);
	}

	private void ConfigureCircuitInputOnAllAPs()
	{
		ModuleCircuitUtil.AutoGenerateCircuitChargePoints(ref ChargeInPoints, base.block.attachPoints, setEmpty: false);
	}

	private void ConfigureCircuitOutputOnAllAPs()
	{
		ModuleCircuitUtil.AutoGenerateCircuitChargePoints(ref ChargeOutPoints, base.block.attachPoints, setEmpty: false);
	}

	private void OnDetached()
	{
		if (!ManSaveGame.Storing)
		{
			UpdateConnexionLinks();
		}
	}

	private void OnAttached()
	{
		UpdateConnexionLinks();
	}

	private void PrePool()
	{
		if ((AutoGenChargePointsForAllAPs & AutoGenChargePoints.AsInputs) != AutoGenChargePoints.None)
		{
			ConfigureCircuitInputOnAllAPs();
		}
		if ((AutoGenChargePointsForAllAPs & AutoGenChargePoints.AsOutputs) != AutoGenChargePoints.None)
		{
			ConfigureCircuitOutputOnAllAPs();
			m_CarryChargeDefault = false;
		}
		m_Receiver = GetComponent<ModuleCircuitReceiver>();
		m_Dispensor = GetComponent<ModuleCircuitDispensor>();
		if (Dispensor != null && m_CarryChargeDefault)
		{
			d.LogError("Issue on block: <b>" + base.block.gameObject.name + "</b>, Carry Charge Default is on, but this block has a charge dispensor and cannot carry charge! Please change in block prefab.");
		}
		if (m_APLayerFlagOverrides != null && m_APLayerFlagOverrides.Length != 0)
		{
			CircuitLayer circuitLayer = (CircuitLayer)0;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < m_APLayerFlagOverrides.Length; i++)
			{
				if (m_APLayerFlagOverrides[i] == (CircuitLayer)0)
				{
					m_APLayerFlagOverrides[i] = m_LayerFlags;
					flag = true;
				}
				else
				{
					circuitLayer |= m_APLayerFlagOverrides[i];
					flag2 = flag2 || m_LayerFlags != m_APLayerFlagOverrides[i];
				}
			}
			d.AssertFormat(circuitLayer != (CircuitLayer)0, this, "Block {0} added circuit layer overrides list, but no values were set!", base.name);
			d.AssertFormat(flag2, this, "Block {0} defined circuit layer overrides, but no values deviated from the default!", base.name);
			d.AssertFormat(flag || (m_LayerFlags & circuitLayer) == m_LayerFlags, this, "Block {0} added circuit layer overrides list that completely removed the original layer set on the block!", base.name);
			if (m_LayerFlags != circuitLayer)
			{
				d.LogWarningFormat("Rewriting circuit layer flags for block {0} from {1} to {2} due to specific AP overrides.", base.name, m_LayerFlags, circuitLayer);
				m_LayerFlags = circuitLayer;
			}
		}
		d.AssertFormat(m_APNetworkGroups == null || m_APNetworkGroups.Length == 0 || m_APNetworkGroups.Length == base.block.attachPoints.Length, this, "m_APNetworkGroups on '{0}' has missmatched lenght ({1}) with number of block AP points ({2})", base.name, m_APNetworkGroups?.Length, base.block.attachPoints.Length);
	}

	private void OnPool()
	{
		if (GetComponent<ModuleCircuit_AdaptiveGeometry>() != null)
		{
			m_ConfigFlags |= ConfigurationFlags.SuppressAPConnectionCovers;
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachedEvent.Subscribe(OnDetached);
		InitConnexions();
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachedEvent.Unsubscribe(OnDetached);
	}

	private void OnValidate()
	{
		d.AssertFormat((m_ConfigFlags & ~(ConfigurationFlags.SuppressAPConnectionCovers | ConfigurationFlags.IsConnexionLayerBus)) == 0, "Flag {0} on block {1} is valid at runtime only, and cannot be pre-configured!", m_ConfigFlags, base.name);
		m_ConfigFlags &= ConfigurationFlags.SuppressAPConnectionCovers | ConfigurationFlags.IsConnexionLayerBus;
		d.AssertFormat(m_APLayerFlagOverrides == null || m_APLayerFlagOverrides.Length == 0 || m_APLayerFlagOverrides.Length == GetComponent<TankBlock>().attachPoints.Length, this, "m_APLayerFlagOverrides on '{0}' has missmatched lenght ({1}) with number of block AP points ({2})", base.name, m_APLayerFlagOverrides?.Length, GetComponent<TankBlock>().attachPoints.Length);
		d.AssertFormat(m_APNetworkGroups == null || m_APNetworkGroups.Length == 0 || m_APNetworkGroups.Length == GetComponent<TankBlock>().attachPoints.Length, this, "m_APNetworkGroups on '{0}' has missmatched lenght ({1}) with number of block AP points ({2})", base.name, m_APNetworkGroups?.Length, GetComponent<TankBlock>().attachPoints.Length);
		if (m_APNetworkGroups != null)
		{
			int[] aPNetworkGroups = m_APNetworkGroups;
			foreach (int num in aPNetworkGroups)
			{
				d.Assert(num >= 0 && num <= 15, $"APNetworkGroups on {base.name} has GroupID {num} but value must be within 0 and {15}!");
			}
		}
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (flags.Contains(0))
		{
			int a = ((Dispensor != null) ? Dispensor.CurrentHighestChargeFromHere : 0);
			int b = ((Receiver != null) ? Receiver.CurrentHighestChargeFromNetwork : 0);
			Color textColor = Color.Lerp(new Color(0.1f, 0f, 0f, 1f), Color.red, Mathf.Max(a, b));
			DebugGui.LabelScreen(string.Format("H-{1} N-{2}", Mathf.Max(a, b).ToString("F0"), a.ToString(), b.ToString()), textColor, screenPos + Vector2.down * 10f);
		}
	}
}
