#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DevCommands;
using Newtonsoft.Json;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Networking;

public class Circuits
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Time
	{
		public const int k_TotalTicksPerSecond = 50;

		public const int k_SlowUpdateTickCadence = 5;

		public const float k_FixedTickDeltaTime = 0.02f;

		private static float s_UpdateDurationDelta = 0f;

		private static int s_DebugManualAdvanceFrames = -1;

		private static float s_DebugExecutionSpeed = 1f;

		public static int tickCount { get; internal set; } = 0;

		public static int loopIteration { get; internal set; } = 0;

		public static bool isPreSlowUpdateFrame
		{
			get
			{
				if (loopIteration != 0)
				{
					return IsManuallyAdvancingFrames;
				}
				return true;
			}
		}

		public static bool isPostSlowUpdateFrame
		{
			get
			{
				if (loopIteration != 4)
				{
					return IsManuallyAdvancingFrames;
				}
				return true;
			}
		}

		internal static bool CanDoTick
		{
			get
			{
				if (!IsManuallyAdvancingFrames)
				{
					return s_UpdateDurationDelta - 0.02f >= 0f;
				}
				return TryAdvanceSingleFrame();
			}
		}

		private static bool IsManuallyAdvancingFrames => s_DebugManualAdvanceFrames >= 0;

		private static bool IsManualAdvancePaused => s_DebugManualAdvanceFrames == 0;

		internal static void IncrementLoopTime()
		{
			s_UpdateDurationDelta += UnityEngine.Time.deltaTime * s_DebugExecutionSpeed;
		}

		internal static void DecrementLoopTime()
		{
			s_UpdateDurationDelta -= 0.02f;
		}

		private static bool TryAdvanceSingleFrame()
		{
			if (!IsManualAdvancePaused)
			{
				return s_DebugManualAdvanceFrames-- > 0;
			}
			return false;
		}

		public static void SetExecutionPaused(bool pause = true)
		{
			if (pause)
			{
				s_DebugManualAdvanceFrames++;
				return;
			}
			s_UpdateDurationDelta = 0f;
			s_DebugManualAdvanceFrames = -1;
		}

		public static void PauseAndAdvanceFrame()
		{
			s_DebugManualAdvanceFrames++;
		}

		[DevCommand(Name = "Circuits.SetExecutionSpeed", Access = Access.DevCheat, Users = User.Any)]
		public static void SetExecutionSpeed(float speed)
		{
			s_DebugExecutionSpeed = speed;
		}
	}

	public class BlockChargeDataWrapper
	{
		private Module.NetworkedProperty<CircuitBlockChargeDataBlockMessage> NetProperty;

		public Event<BlockChargeData> SetEvent;

		public Event<BlockChargeData> ChangedEvent;

		public BlockChargeData Value { get; private set; }

		public bool IsNetPropertyDirty { get; private set; }

		public BlockChargeDataWrapper()
		{
			Value = BlockChargeData.empty;
		}

		public void Init(Module netModule, TTMsgType msgType)
		{
			NetProperty = new Module.NetworkedProperty<CircuitBlockChargeDataBlockMessage>(netModule, msgType, OnMPSyncEvent);
			NetProperty.Data.value.CopyFrom(Value);
		}

		public void Reset()
		{
			Set(BlockChargeData.empty);
		}

		public void Set(BlockChargeData value)
		{
			Set(value, fromMPSync: false);
		}

		private void Set(BlockChargeData value, bool fromMPSync)
		{
			d.Assert(value != null, "Should not be attempting to set charge data to null!");
			bool num = Value != value;
			if (num)
			{
				Value.CopyFrom(value);
				if (NetProperty != null)
				{
					NetProperty.Data.value.CopyFrom(value);
				}
			}
			if (ManNetwork.IsHost)
			{
				SetEvent.Send(Value);
			}
			if (num)
			{
				ChangedEvent.Send(Value);
				if (ManNetwork.IsNetworked && NetProperty != null && !fromMPSync)
				{
					IsNetPropertyDirty = true;
				}
			}
		}

		private void OnMPSyncEvent(CircuitBlockChargeDataBlockMessage msg)
		{
			if (!ManNetwork.IsHost)
			{
				Set(msg.value, fromMPSync: true);
			}
		}

		public void SyncNetProperty()
		{
			NetProperty.Sync();
			IsNetPropertyDirty = false;
		}

		public void Serialise(NetworkWriter writer)
		{
			NetProperty.Serialise(writer);
		}

		public void Deserialise(NetworkReader reader)
		{
			NetProperty.Deserialise(reader);
		}
	}

	public class BlockChargeData : IEquatable<BlockChargeData>
	{
		[JsonProperty("HighestChargeStrengthFromNetwork")]
		public int ChargeStrength;

		[JsonProperty("HighestChargeStrengthAPs")]
		public List<Vector3> ChargedAPs;

		[JsonConverter(typeof(Vec3DictionaryKeyConverter<int>))]
		public Dictionary<Vector3, int> AllChargeAPsAndCharges;

		[JsonIgnore]
		public static BlockChargeData empty => new BlockChargeData(0);

		public BlockChargeData()
		{
			SetDefault();
		}

		public BlockChargeData(int charge)
		{
			SetDefault();
			ChargeStrength = charge;
		}

		[JsonConstructor]
		public BlockChargeData(int HighestChargeStrengthFromNetwork, List<Vector3> HighestChargeStrengthAPs, Dictionary<Vector3, int> AllChargeAPsAndCharges)
		{
			ChargeStrength = HighestChargeStrengthFromNetwork;
			ChargedAPs = ((HighestChargeStrengthAPs == null) ? new List<Vector3>() : new List<Vector3>(HighestChargeStrengthAPs));
			this.AllChargeAPsAndCharges = ((AllChargeAPsAndCharges == null) ? new Dictionary<Vector3, int>() : new Dictionary<Vector3, int>(AllChargeAPsAndCharges));
		}

		public BlockChargeData(BlockChargeData srcCharge)
		{
			CopyFrom(srcCharge);
		}

		public void SetDefault()
		{
			ChargeStrength = 0;
			if (ChargedAPs == null)
			{
				ChargedAPs = new List<Vector3>(16);
			}
			else
			{
				ChargedAPs.Clear();
			}
			if (AllChargeAPsAndCharges == null)
			{
				AllChargeAPsAndCharges = new Dictionary<Vector3, int>(16);
			}
			else
			{
				AllChargeAPsAndCharges.Clear();
			}
		}

		public void CopyFrom(BlockChargeData srcCharge)
		{
			SetDefault();
			ChargeStrength = srcCharge.ChargeStrength;
			for (int i = 0; i < srcCharge.ChargedAPs.Count; i++)
			{
				ChargedAPs.Add(srcCharge.ChargedAPs[i]);
			}
			foreach (Vector3 key in srcCharge.AllChargeAPsAndCharges.Keys)
			{
				AllChargeAPsAndCharges[key] = srcCharge.AllChargeAPsAndCharges[key];
			}
		}

		public void Serialize(NetworkWriter writer)
		{
			writer.WritePackedInt32(ChargeStrength);
			writer.WritePackedInt32(ChargedAPs.Count);
			foreach (Vector3 chargedAP in ChargedAPs)
			{
				writer.Write(chargedAP);
			}
			writer.WritePackedInt32(AllChargeAPsAndCharges.Count);
			foreach (KeyValuePair<Vector3, int> allChargeAPsAndCharge in AllChargeAPsAndCharges)
			{
				writer.Write(allChargeAPsAndCharge.Key);
				writer.WritePackedInt32(allChargeAPsAndCharge.Value);
			}
		}

		public void Deserialize(NetworkReader reader)
		{
			ChargeStrength = reader.ReadPackedInt32();
			ChargedAPs.Clear();
			int num = reader.ReadPackedInt32();
			for (int i = 0; i < num; i++)
			{
				ChargedAPs.Add(reader.ReadVector3());
			}
			AllChargeAPsAndCharges.Clear();
			num = reader.ReadPackedInt32();
			for (int j = 0; j < num; j++)
			{
				AllChargeAPsAndCharges[reader.ReadVector3()] = reader.ReadPackedInt32();
			}
		}

		public static bool operator !=(BlockChargeData info, BlockChargeData otherInfo)
		{
			return !(info == otherInfo);
		}

		public static bool operator ==(BlockChargeData info, BlockChargeData otherInfo)
		{
			return info?.Equals(otherInfo) ?? ((object)otherInfo == null);
		}

		public override bool Equals(object obj)
		{
			if (obj is BlockChargeData other)
			{
				return Equals(other);
			}
			return false;
		}

		public static bool operator >(BlockChargeData info, int value)
		{
			return info.ChargeStrength > value;
		}

		public static bool operator <(BlockChargeData info, int value)
		{
			return info.ChargeStrength < value;
		}

		public static bool operator >=(BlockChargeData info, int value)
		{
			return info.ChargeStrength >= value;
		}

		public static bool operator <=(BlockChargeData info, int value)
		{
			return info.ChargeStrength <= value;
		}

		public bool Equals(BlockChargeData other)
		{
			if ((object)other == null)
			{
				return false;
			}
			if (ChargeStrength != other.ChargeStrength)
			{
				return false;
			}
			if (ChargedAPs.Count != other.ChargedAPs.Count || AllChargeAPsAndCharges.Count != other.AllChargeAPsAndCharges.Count)
			{
				return false;
			}
			for (int i = 0; i < ChargedAPs.Count; i++)
			{
				if (other.ChargedAPs[i] != ChargedAPs[i])
				{
					return false;
				}
			}
			foreach (KeyValuePair<Vector3, int> allChargeAPsAndCharge in AllChargeAPsAndCharges)
			{
				if (!other.AllChargeAPsAndCharges.TryGetValue(allChargeAPsAndCharge.Key, out var value) || allChargeAPsAndCharge.Value != value)
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return HashCodeUtility.CombineHashCodes(ChargeStrength.GetHashCode(), ChargedAPs.GetHashCode(), AllChargeAPsAndCharges.GetHashCode());
		}
	}

	[Serializable]
	public class Network
	{
		public TechCircuits TechCirc;

		public int ID;

		public HashSet<ModuleCircuitNode.Connexion> Connexions;

		public HashSet<ModuleCircuitNode> Nodes;

		public HashSet<ModuleCircuitReceiver> Receivers;

		public HashSet<ModuleCircuitDispensor> Dispensors;

		private bool IsDudNetwork;

		private HashSet<ModuleCircuitNode.Connexion> NetworkLinkerConnexions;

		private static Queue<ModuleCircuitNode.Connexion> _s_UncheckedConnexions = new Queue<ModuleCircuitNode.Connexion>();

		private ModuleCircuitNode.Connexion dequedConnexion;

		public int m_NextChargeStrength { get; private set; }

		public ModuleCircuitNode.Connexion m_NextChargeOriginConnexion { get; private set; }

		public Network(ModuleCircuitNode.Connexion connexion, int iD, TechCircuits techCircuits)
		{
			ID = iD;
			TechCirc = techCircuits;
			Connexions = new HashSet<ModuleCircuitNode.Connexion>();
			Nodes = new HashSet<ModuleCircuitNode>();
			Receivers = new HashSet<ModuleCircuitReceiver>();
			Dispensors = new HashSet<ModuleCircuitDispensor>();
			NetworkLinkerConnexions = new HashSet<ModuleCircuitNode.Connexion>();
			_s_UncheckedConnexions.Clear();
			_s_UncheckedConnexions.Enqueue(connexion);
			while (_s_UncheckedConnexions.Count > 0)
			{
				dequedConnexion = _s_UncheckedConnexions.Dequeue();
				if (!Connexions.Add(dequedConnexion))
				{
					continue;
				}
				dequedConnexion.NetworkID = ID;
				Nodes.Add(dequedConnexion.ParentNode);
				if ((dequedConnexion.Type & ModuleCircuitNode.ConnexionTypes.Output) != 0 && dequedConnexion.ParentNode.Dispensor.IsNotNull())
				{
					Dispensors.Add(dequedConnexion.ParentNode.Dispensor);
				}
				if ((dequedConnexion.Type & ModuleCircuitNode.ConnexionTypes.Input) != 0 && dequedConnexion.ParentNode.Receiver.IsNotNull())
				{
					Receivers.Add(dequedConnexion.ParentNode.Receiver);
				}
				if (dequedConnexion.ParentNode.IsChargeCarrier)
				{
					foreach (ModuleCircuitNode.Connexion item in dequedConnexion.ParentNode.ConnexionsInSameNetworkGroup(dequedConnexion))
					{
						if (item.NetworkID == ID || item == dequedConnexion || !ModuleCircuitNode.AreConnexionsCompatible(dequedConnexion, item))
						{
							continue;
						}
						ModuleCircuitNode.ConnexionTypes connexionTypes = ModuleCircuitNode.ConnexionTypes.Input | ModuleCircuitNode.ConnexionTypes.Output;
						if (dequedConnexion.Type == connexionTypes && item.Type == connexionTypes)
						{
							d.Assert(item.NetworkID == -1, "Trying to add a connexion that already belongs to another network after performing a intranodal connexion check! This shouldn't be possible!");
							if (!_s_UncheckedConnexions.Contains(item))
							{
								_s_UncheckedConnexions.Enqueue(item);
							}
						}
						else if ((dequedConnexion.Type & ModuleCircuitNode.ConnexionTypes.Input) != 0 && (item.Type & ModuleCircuitNode.ConnexionTypes.Output) != 0)
						{
							NetworkLinkerConnexions.Add(item);
						}
					}
				}
				if (dequedConnexion.LinkedConnexion != null && dequedConnexion.LinkedConnexion.NetworkID != ID && (dequedConnexion.Type | dequedConnexion.LinkedConnexion.Type) == (ModuleCircuitNode.ConnexionTypes.Input | ModuleCircuitNode.ConnexionTypes.Output))
				{
					d.Assert(dequedConnexion.LinkedConnexion.NetworkID == -1, "Trying to add a connexion that already belongs to another network after performing a internodal connexion check! This shouldn't be possible!");
					if (!_s_UncheckedConnexions.Contains(dequedConnexion.LinkedConnexion))
					{
						_s_UncheckedConnexions.Enqueue(dequedConnexion.LinkedConnexion);
					}
				}
			}
			IsDudNetwork = Connexions.Count < 2;
			ReceiveChargeUpdate.Subscribe(ReceiveCharge);
		}

		public void AddCharge(int charge, ModuleCircuitNode.Connexion originConnexion)
		{
			if (charge >= m_NextChargeStrength)
			{
				m_NextChargeStrength = charge;
				m_NextChargeOriginConnexion = originConnexion;
			}
		}

		public void PropagateFromHere()
		{
			PropagateLinkedCircuitCharge(m_NextChargeStrength, m_NextChargeOriginConnexion, propagateRegardless: true);
		}

		public void PropagateLinkedCircuitCharge(int incomingCharge, ModuleCircuitNode.Connexion originConnexion, bool propagateRegardless = false)
		{
			if (IsDudNetwork)
			{
				return;
			}
			if (incomingCharge > m_NextChargeStrength)
			{
				m_NextChargeStrength = incomingCharge;
				m_NextChargeOriginConnexion = originConnexion;
			}
			else if (!propagateRegardless)
			{
				return;
			}
			foreach (ModuleCircuitNode.Connexion networkLinkerConnexion in NetworkLinkerConnexions)
			{
				TechCirc.GetNetworkWithID(networkLinkerConnexion.NetworkID).PropagateLinkedCircuitCharge(incomingCharge, originConnexion);
			}
		}

		public void ReleaseCharge()
		{
			foreach (ModuleCircuitReceiver receiver in Receivers)
			{
				receiver.AddChargeToCache(m_NextChargeStrength, ID, m_NextChargeOriginConnexion);
			}
			m_NextChargeStrength = 0;
			m_NextChargeOriginConnexion = null;
		}

		private void ReceiveCharge()
		{
			foreach (ModuleCircuitReceiver receiver in Receivers)
			{
				receiver.ReceiveCharge();
			}
		}

		public void UnsubscribeFromReceiveEvent()
		{
			ReceiveChargeUpdate.Unsubscribe(ReceiveCharge);
		}
	}

	public static EventNoParams PreSlowUpdate;

	public static EventNoParams StartChargeUpdate;

	public static EventNoParams GenerateChargeUpdate;

	public static EventNoParams ReleaseChargeUpdate;

	public static EventNoParams ReceiveChargeUpdate;

	public static EventNoParams EndChargeUpdate;

	public static EventNoParams PostSlowUpdate;

	private static ProfilerMarker markerAll = new ProfilerMarker("Circuit.All");

	private static ProfilerMarker markerCircuitUpdate = new ProfilerMarker("Circuit.Update");

	private static ProfilerMarker markerPreSlow = new ProfilerMarker("Circuit.PreCharge");

	private static ProfilerMarker markerChargeUpdate = new ProfilerMarker("Circuit.AllChargeUpdates");

	private static ProfilerMarker markerChargeStart = new ProfilerMarker("Circuit.StartCharge");

	private static ProfilerMarker markerChargeGenerate = new ProfilerMarker("Circuit.GenerateCharge");

	private static ProfilerMarker markerChargeRelease = new ProfilerMarker("Circuit.ReleaseCharge");

	private static ProfilerMarker markerChargeReceive = new ProfilerMarker("Circuit.ReceiveCharge");

	private static ProfilerMarker markerChargeEnd = new ProfilerMarker("Circuit.EndCharge");

	private static ProfilerMarker markerPostSlow = new ProfilerMarker("Circuit.PostCharge");

	public static void DoCircuitLoop()
	{
		if (!ReleaseChargeUpdate.HasSubscribers())
		{
			return;
		}
		Time.IncrementLoopTime();
		while (Time.CanDoTick)
		{
			Time.loopIteration = (Time.loopIteration + 1) % 5;
			Time.tickCount++;
			if (Time.isPreSlowUpdateFrame)
			{
				PreSlowUpdate.Send();
			}
			StartChargeUpdate.Send();
			GenerateChargeUpdate.Send();
			ReleaseChargeUpdate.Send();
			ReceiveChargeUpdate.Send();
			EndChargeUpdate.Send();
			if (Time.isPostSlowUpdateFrame)
			{
				PostSlowUpdate.Send();
			}
			Time.DecrementLoopTime();
		}
	}
}
