#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using Unity.Profiling;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechCircuits : TechComponent
{
	private Dictionary<int, Circuits.Network> m_CircuitNetworkDictionary = new Dictionary<int, Circuits.Network>();

	private List<Circuits.Network> m_SortedNetworks = new List<Circuits.Network>();

	private List<ModuleCircuitNode.Connexion> m_DirtyCircuitNetworkConnX = new List<ModuleCircuitNode.Connexion>();

	private static Stack<ModuleCircuitNode> _s_NodesToRebuildNetworkIDListsOfStack = new Stack<ModuleCircuitNode>();

	private static HashSet<ModuleCircuitNode> _s_NodesToRebuildNetworkIDListsOfSet = new HashSet<ModuleCircuitNode>();

	private static ProfilerMarker markerSortNetworks = new ProfilerMarker("Circuit.ReleaseCharge.SortNetworks");

	private static ProfilerMarker markerPropagateCharge = new ProfilerMarker("Circuit.ReleaseCharge.PropagateCharge");

	private static ProfilerMarker markerReleaseCharge = new ProfilerMarker("Circuit.ReleaseCharge.ReleaseCharge");

	public void ClearNetworksAndMarkForDeferredNetworkRecalculation(ModuleCircuitNode originNode)
	{
		foreach (ModuleCircuitNode.Connexion connexion in originNode.Connexions)
		{
			m_DirtyCircuitNetworkConnX.Add(connexion);
			TryClearAndDeleteCircuitNetwork(connexion.NetworkID, m_DirtyCircuitNetworkConnX);
		}
		foreach (ModuleCircuitNode.Connexion linkedConnexion in originNode.ConnexionMetadata.LinkedConnexions)
		{
			m_DirtyCircuitNetworkConnX.Add(linkedConnexion);
			TryClearAndDeleteCircuitNetwork(linkedConnexion.NetworkID, m_DirtyCircuitNetworkConnX);
		}
	}

	public Circuits.Network GetNetworkWithID(int iD)
	{
		if (!m_CircuitNetworkDictionary.TryGetValue(iD, out var value))
		{
			return null;
		}
		return value;
	}

	public IEnumerable<Circuits.Network> GetNetworksWithIDs(IEnumerable<int> iDs)
	{
		foreach (int iD in iDs)
		{
			if (m_CircuitNetworkDictionary.TryGetValue(iD, out var value))
			{
				yield return value;
			}
		}
	}

	private int GetLowestUnusedID()
	{
		int i;
		for (i = 0; m_CircuitNetworkDictionary.ContainsKey(i); i++)
		{
		}
		return i;
	}

	private void RebuildCircuitNetworksForDirtyConnexions()
	{
		foreach (ModuleCircuitNode.Connexion item in m_DirtyCircuitNetworkConnX)
		{
			if (!_s_NodesToRebuildNetworkIDListsOfSet.Contains(item.ParentNode))
			{
				_s_NodesToRebuildNetworkIDListsOfStack.Push(item.ParentNode);
				_s_NodesToRebuildNetworkIDListsOfSet.Add(item.ParentNode);
			}
			if (item.NetworkID == -1 && item.ParentNode.block.IsAttached && !(item.ParentNode.block.tank != base.Tech))
			{
				CreateNewCircuitNetwork(item);
			}
		}
		while (_s_NodesToRebuildNetworkIDListsOfStack.Count > 0)
		{
			_s_NodesToRebuildNetworkIDListsOfStack.Pop().RefreshCircuitNetworksUpdated();
		}
		_s_NodesToRebuildNetworkIDListsOfStack.Clear();
		_s_NodesToRebuildNetworkIDListsOfSet.Clear();
		m_DirtyCircuitNetworkConnX.Clear();
	}

	private void CreateNewCircuitNetwork(ModuleCircuitNode.Connexion fromConnexion)
	{
		int lowestUnusedID = GetLowestUnusedID();
		m_CircuitNetworkDictionary.Add(lowestUnusedID, new Circuits.Network(fromConnexion, lowestUnusedID, this));
		m_SortedNetworks.Clear();
	}

	private void TryClearAndDeleteCircuitNetwork(int networkID, List<ModuleCircuitNode.Connexion> affectedConnexions)
	{
		if (networkID != -1 && m_CircuitNetworkDictionary.TryGetValue(networkID, out var value))
		{
			CleanupNetwork(value, affectedConnexions);
			m_CircuitNetworkDictionary.Remove(networkID);
			m_SortedNetworks.Clear();
		}
	}

	private void CleanupNetwork(Circuits.Network circuitNetwork, List<ModuleCircuitNode.Connexion> affectedConnexions = null)
	{
		foreach (ModuleCircuitNode.Connexion connexion in circuitNetwork.Connexions)
		{
			if (connexion.NetworkID != circuitNetwork.ID)
			{
				d.LogError("Could not find a network registered on a node connexion where the link should be apparent, this should not happen!");
				continue;
			}
			connexion.NetworkID = -1;
			affectedConnexions?.Add(connexion);
		}
		circuitNetwork.UnsubscribeFromReceiveEvent();
	}

	private void PropagateHighestNetworkChargesToLinkedNetworksThenReleaseCharges()
	{
		if (m_SortedNetworks.Count == 0)
		{
			m_SortedNetworks.AddRange(m_CircuitNetworkDictionary.Values);
		}
		m_SortedNetworks.Sort((Circuits.Network a, Circuits.Network b) => b.m_NextChargeStrength.CompareTo(a.m_NextChargeStrength));
		foreach (Circuits.Network sortedNetwork in m_SortedNetworks)
		{
			sortedNetwork.PropagateFromHere();
		}
		foreach (Circuits.Network sortedNetwork2 in m_SortedNetworks)
		{
			sortedNetwork2.ReleaseCharge();
		}
	}

	private void OnStartChargeUpdate()
	{
		if (m_DirtyCircuitNetworkConnX.Count > 0)
		{
			RebuildCircuitNetworksForDirtyConnexions();
		}
	}

	private void OnReleaseChargeUpdate()
	{
		PropagateHighestNetworkChargesToLinkedNetworksThenReleaseCharges();
	}

	private void OnSpawn()
	{
		Circuits.StartChargeUpdate.Subscribe(OnStartChargeUpdate);
		Circuits.ReleaseChargeUpdate.Subscribe(OnReleaseChargeUpdate);
	}

	private void OnRecycle()
	{
		Circuits.StartChargeUpdate.Unsubscribe(OnStartChargeUpdate);
		Circuits.ReleaseChargeUpdate.Unsubscribe(OnReleaseChargeUpdate);
		foreach (KeyValuePair<int, Circuits.Network> item in m_CircuitNetworkDictionary)
		{
			CleanupNetwork(item.Value);
		}
		m_CircuitNetworkDictionary.Clear();
		m_SortedNetworks.Clear();
	}
}
