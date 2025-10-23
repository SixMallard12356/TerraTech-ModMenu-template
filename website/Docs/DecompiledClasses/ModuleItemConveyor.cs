#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleItemHolder))]
public class ModuleItemConveyor : Module, ItemSearchHandler, ManPointer.OpenMenuEventConsumer, INetworkedModule, IBlockPlacementPreview
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public IntVector3 fromPos;

		public IntVector3 toPos;

		public IntVector3 pushPos;
	}

	private enum RotationGeometry
	{
		Forward,
		LeftTurn,
		RightTurn,
		None
	}

	public enum BlockDirections
	{
		None,
		PosX,
		NegX,
		PosY,
		NegY,
		PosZ,
		NegZ,
		StepUpFwd,
		CornerCCUp
	}

	private struct Orientation
	{
		public RotationGeometry m_Rotation;

		public float m_Angle;

		public Orientation(RotationGeometry rotation, float angle)
		{
			m_Rotation = rotation;
			m_Angle = angle;
		}
	}

	[Serializable]
	public struct OppositeDirection
	{
		public BlockDirections m_Direction;

		public BlockDirections m_Opposite;
	}

	private enum RelativePriority
	{
		BoundOutputWprefIn,
		BoundInputWprefOut,
		BoundOutput,
		BoundInput,
		LooselyBoundOutput,
		BidirectionalLink,
		Unlinked
	}

	private struct DirectionalBlockPair : IEquatable<DirectionalBlockPair>
	{
		public TankBlock origin;

		public TankBlock target;

		public static bool operator ==(DirectionalBlockPair a, DirectionalBlockPair b)
		{
			if ((object)a.origin == b.origin)
			{
				return (object)a.target == b.target;
			}
			return false;
		}

		public static bool operator !=(DirectionalBlockPair a, DirectionalBlockPair b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj is DirectionalBlockPair)
			{
				return Equals((DirectionalBlockPair)obj);
			}
			return false;
		}

		bool IEquatable<DirectionalBlockPair>.Equals(DirectionalBlockPair other)
		{
			return this == other;
		}

		public override string ToString()
		{
			return origin.name + " > " + target.name;
		}

		public override int GetHashCode()
		{
			return (17 * 31 + origin.GetHashCode()) * 31 + target.GetHashCode();
		}
	}

	[SerializeField]
	[EnumArray(typeof(RotationGeometry))]
	private Transform[] m_RotationGeometry;

	[SerializeField]
	private OppositeDirection[] m_OppositeDirections;

	private ModuleItemHolder m_Holder;

	private ModuleItemConveyor m_InConveyor;

	private ModuleItemConveyor m_OutConveyor;

	private bool m_OutConveyorIsReciprocating;

	private bool m_ConnectionsDirty;

	private bool m_PrefersOutputConveyorConnection;

	private List<ModuleItemConveyor> m_ConveyorConnections = new List<ModuleItemConveyor>(4);

	private List<ModuleItemHolder.Stack> m_InputStacks = new List<ModuleItemHolder.Stack>(4);

	private List<ModuleItemHolder.Stack> m_OutputStacks = new List<ModuleItemHolder.Stack>(4);

	private int m_LastPassHeartbeat = -1;

	private bool m_FirstPasserThisHeartbeat;

	private Dictionary<int, int> m_OppositeLookup;

	private NetworkedProperty<ConveyorOrientationMessage> m_SyncedConveyorOrientation;

	private static Func<ModuleItemConveyor, int> s_StackConnectionPriorityFunc;

	private static Dictionary<int, Dictionary<int, int>> s_OppositeLookup = new Dictionary<int, Dictionary<int, int>>();

	private static readonly Vector3 k_PosX = new Vector3(0.5f, 0f, 0f);

	private static readonly Vector3 k_NegX = new Vector3(-0.5f, 0f, 0f);

	private static readonly Vector3 k_PosY = new Vector3(0f, 0.5f, 0f);

	private static readonly Vector3 k_NegY = new Vector3(0f, -0.5f, 0f);

	private static readonly Vector3 k_PosZ = new Vector3(0f, 0f, 0.5f);

	private static readonly Vector3 k_NegZ = new Vector3(0f, 0f, -0.5f);

	private static readonly Vector3 k_StepUpFwd = new Vector3(0f, 1f, 0.5f);

	private static readonly Vector3 k_CornerUp = new Vector3(0f, 1.5f, 1f);

	private static readonly Dictionary<Vector3, BlockDirections> k_EnumLookup = new Dictionary<Vector3, BlockDirections>
	{
		{
			k_PosX,
			BlockDirections.PosX
		},
		{
			k_NegX,
			BlockDirections.NegX
		},
		{
			k_PosY,
			BlockDirections.PosY
		},
		{
			k_NegY,
			BlockDirections.NegY
		},
		{
			k_PosZ,
			BlockDirections.PosZ
		},
		{
			k_NegZ,
			BlockDirections.NegZ
		},
		{
			k_StepUpFwd,
			BlockDirections.StepUpFwd
		},
		{
			k_CornerUp,
			BlockDirections.CornerCCUp
		}
	};

	private static Dictionary<int, Orientation> s_RotationalInfo = null;

	private static readonly int k_FromBitshift = 3;

	private static int kMaxLoopCycleCount;

	private static List<ModuleItemConveyor> s_FlippedConveyors = new List<ModuleItemConveyor>();

	private static HashSet<ModuleItemConveyor> s_ConveyorsSeen = new HashSet<ModuleItemConveyor>();

	private static List<ModuleItemConveyor> s_NewlyConnectedConveyors = new List<ModuleItemConveyor>();

	private static List<ModuleItemConveyor> s_PreviewConveyorConnections = new List<ModuleItemConveyor>(4);

	private static List<ModuleItemHolder.Stack> s_PreviewInputStacks = new List<ModuleItemHolder.Stack>(4);

	private static List<ModuleItemHolder.Stack> s_PreviewOutputStacks = new List<ModuleItemHolder.Stack>(4);

	private static Dictionary<ModuleItemConveyor, int> s_ConveyorNeighbourStackIdx = new Dictionary<ModuleItemConveyor, int>();

	private static Dictionary<DirectionalBlockPair, Vector3> s_PreviewNeighbourAPs = new Dictionary<DirectionalBlockPair, Vector3>();

	public static bool LinkOnAttach { get; set; }

	private bool HasBidirectionalConveyorConnection
	{
		get
		{
			if (m_InConveyor.IsNotNull() && m_OutConveyor.IsNotNull())
			{
				return m_OutConveyorIsReciprocating;
			}
			return false;
		}
	}

	private ModuleItemConveyor PushConveyor
	{
		get
		{
			if (!m_OutConveyorIsReciprocating)
			{
				return m_OutConveyor;
			}
			return null;
		}
	}

	public bool CanOpenMenu(bool isRadial)
	{
		if (isRadial ? true : false)
		{
			return !base.block.tank.IsEnemy();
		}
		return false;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowRadialMenu)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ConveyorMenu, openMenu);
			return true;
		}
		return false;
	}

	public void RequestFlipLoopDirection()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.FlipConveyorLoopDirection, new EmptyBlockMessage
			{
				m_BlockPoolID = base.block.blockPoolID
			}, base.block.tank.netTech.netId);
		}
		else
		{
			FlipLoopDirection();
		}
	}

	public void FlipLoopDirection()
	{
		ModuleItemConveyor inConveyor = m_InConveyor;
		if (!FlipConveyorDirection_Internal(s_FlippedConveyors, this) && inConveyor.IsNotNull())
		{
			FlipConveyorDirection_Internal(s_FlippedConveyors, inConveyor, forward: false);
		}
		foreach (ModuleItemConveyor s_FlippedConveyor in s_FlippedConveyors)
		{
			if (!s_FlippedConveyor.HasBidirectionalConveyorConnection)
			{
				s_FlippedConveyor.UpdateConnections();
			}
			else
			{
				s_FlippedConveyor.UpdateGeometry();
			}
			s_FlippedConveyor.m_ConnectionsDirty = false;
		}
		s_FlippedConveyors.Clear();
		base.block.tank.Holders.CraftingSetupChanged.Send();
	}

	private bool FlipConveyorDirection_Internal(List<ModuleItemConveyor> touchedConveyors, ModuleItemConveyor startPoint, bool forward = true)
	{
		ModuleItemConveyor moduleItemConveyor = startPoint;
		do
		{
			touchedConveyors.Add(moduleItemConveyor);
			s_ConveyorsSeen.Add(moduleItemConveyor);
			ModuleItemConveyor moduleItemConveyor2 = ((!forward) ? moduleItemConveyor.m_InConveyor : (moduleItemConveyor.m_OutConveyorIsReciprocating ? moduleItemConveyor.m_OutConveyor : null));
			ModuleItemConveyor inConveyor = moduleItemConveyor.m_InConveyor;
			moduleItemConveyor.m_InConveyor = (moduleItemConveyor.m_OutConveyorIsReciprocating ? moduleItemConveyor.m_OutConveyor : null);
			moduleItemConveyor.m_OutConveyor = inConveyor;
			moduleItemConveyor.m_OutConveyorIsReciprocating = moduleItemConveyor.m_OutConveyor.IsNotNull();
			moduleItemConveyor.m_ConnectionsDirty = true;
			if ((object)moduleItemConveyor2 == startPoint)
			{
				s_ConveyorsSeen.Clear();
				return true;
			}
			if (s_ConveyorsSeen.Contains(moduleItemConveyor2))
			{
				s_ConveyorsSeen.Clear();
				return true;
			}
			moduleItemConveyor = moduleItemConveyor2;
		}
		while (moduleItemConveyor.IsNotNull());
		s_ConveyorsSeen.Clear();
		return false;
	}

	private static int GetStackConnectionPriority(ModuleItemConveyor conveyor)
	{
		if (conveyor.m_InConveyor.IsNull() && conveyor.m_OutConveyor.IsNull())
		{
			return 6;
		}
		if (conveyor.m_OutConveyor.IsNotNull())
		{
			if (conveyor.m_InputStacks.Count == 1)
			{
				return 0;
			}
			return 3;
		}
		if (conveyor.m_InConveyor.IsNotNull())
		{
			if (conveyor.m_OutputStacks.Count == 1)
			{
				return 1;
			}
			return 2;
		}
		if (!conveyor.m_OutConveyorIsReciprocating)
		{
			return 4;
		}
		return 5;
	}

	private void UpdateConnections()
	{
		if (!m_ConnectionsDirty || HasBidirectionalConveyorConnection)
		{
			return;
		}
		if (m_ConveyorConnections.Count > 0)
		{
			m_ConveyorConnections.InsertionSort(s_StackConnectionPriorityFunc);
			if (m_InConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor = m_ConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => conv.m_PrefersOutputConveyorConnection);
				if (moduleItemConveyor.IsNotNull() && !m_ConveyorConnections.Any((ModuleItemConveyor conv) => conv.m_InConveyor.IsNotNull() || conv.m_OutConveyor.IsNotNull()))
				{
					EstablishConveyorLink(moduleItemConveyor, this);
					s_NewlyConnectedConveyors.Add(moduleItemConveyor);
				}
			}
			if (m_OutConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor2 = m_ConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => conv.m_InConveyor.IsNull() && (object)conv != m_InConveyor && (object)conv.m_OutConveyor != this);
				if (moduleItemConveyor2.IsNotNull() && (object)moduleItemConveyor2 != m_OutConveyor)
				{
					EstablishConveyorLink(this, moduleItemConveyor2);
					s_NewlyConnectedConveyors.Add(moduleItemConveyor2);
				}
			}
			if (m_InConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor3 = m_ConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => (conv.m_OutConveyor.IsNull() || !conv.m_OutConveyorIsReciprocating) && (object)conv != m_OutConveyor);
				if (moduleItemConveyor3.IsNotNull())
				{
					EstablishConveyorLink(moduleItemConveyor3, this);
					s_NewlyConnectedConveyors.Add(moduleItemConveyor3);
				}
			}
			if (m_OutConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor4 = m_ConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => (object)conv != m_InConveyor);
				if (moduleItemConveyor4.IsNotNull())
				{
					d.Assert(moduleItemConveyor4.m_InConveyor.IsNotNull() || (object)moduleItemConveyor4.PushConveyor == this, "Push target conveyor has a free bi-directional connection, we should have connected to that instead!");
					EstablishConveyorLink(this, moduleItemConveyor4, bidirectional: false);
				}
			}
		}
		m_PrefersOutputConveyorConnection = m_InConveyor.IsNull() && m_OutConveyor.IsNull() && m_InputStacks.Count == 1 && m_OutputStacks.Count != 1;
		d.Assert(!m_PrefersOutputConveyorConnection || m_ConveyorConnections.Count == 0, "Still have conveyors in the list, but somehow we failed to connect to them ?!");
		m_ConnectionsDirty = false;
		UpdateGeometry();
		foreach (ModuleItemConveyor s_NewlyConnectedConveyor in s_NewlyConnectedConveyors)
		{
			s_NewlyConnectedConveyor.UpdateGeometry();
		}
		s_NewlyConnectedConveyors.Clear();
	}

	private static void EstablishConveyorLink(ModuleItemConveyor sender, ModuleItemConveyor receiver, bool bidirectional = true)
	{
		if (bidirectional)
		{
			receiver.m_InConveyor = sender;
		}
		sender.m_OutConveyor = receiver;
		sender.m_OutConveyorIsReciprocating = receiver.m_InConveyor == sender;
	}

	private void UpdateGeometry(bool netSend = true)
	{
		UpdateLocalGeometry(m_InConveyor, m_OutConveyor, m_InputStacks, m_OutputStacks);
		if (netSend && Singleton.Manager<ManNetwork>.inst.IsServer && UpdateNetworkedState())
		{
			m_SyncedConveyorOrientation.Sync();
		}
	}

	private void UpdateLocalGeometry(ModuleItemConveyor inConveyor, ModuleItemConveyor outConveyor, IEnumerable<ModuleItemHolder.Stack> inStacks, IEnumerable<ModuleItemHolder.Stack> outStack)
	{
		UpdateLocalGeometry(inConveyor, outConveyor, inStacks, outStack, GetLocalAPForStackConnectedBlock);
	}

	private void UpdateLocalGeometry(ModuleItemConveyor inConveyor, ModuleItemConveyor outConveyor, IEnumerable<ModuleItemHolder.Stack> inStacks, IEnumerable<ModuleItemHolder.Stack> outStack, Func<TankBlock, Vector3> getBlockAPFunc)
	{
		BlockDirections blockDirections = GetDirection(inConveyor, inStacks);
		BlockDirections blockDirections2 = GetDirection(outConveyor, outStack);
		if (blockDirections2 == blockDirections && inConveyor.IsNull() && outConveyor.IsNull())
		{
			blockDirections = BlockDirections.None;
		}
		UpdateLocalGeometry(blockDirections, blockDirections2);
		BlockDirections GetDirection(ModuleItemConveyor conveyor, IEnumerable<ModuleItemHolder.Stack> possibleStacks)
		{
			if (conveyor.IsNotNull() || possibleStacks.Count() == 1)
			{
				TankBlock arg = (conveyor.IsNotNull() ? conveyor.block : possibleStacks.First().myHolder.block);
				Vector3 key = getBlockAPFunc(arg);
				k_EnumLookup.TryGetValue(key, out var value);
				return value;
			}
			return BlockDirections.None;
		}
	}

	private void UpdateLocalGeometry(BlockDirections inDir, BlockDirections outDir)
	{
		if (inDir == BlockDirections.None && outDir == BlockDirections.None)
		{
			ChangeGeometry(RotationGeometry.None);
			return;
		}
		if (inDir == BlockDirections.None || outDir == BlockDirections.None)
		{
			bool num = inDir != BlockDirections.None;
			BlockDirections blockDirections = (num ? inDir : outDir);
			ref BlockDirections reference = ref num ? ref outDir : ref inDir;
			d.AssertFormat(m_OppositeLookup.TryGetValue((int)blockDirections, out var value), this, "Failed to lookup opposite direction on conveyor {0} for dir: {1}", base.name, blockDirections);
			reference = (BlockDirections)value;
		}
		int num2 = ToFromEnumsToPackedInt(inDir, outDir);
		d.AssertFormat(GetGeometryOrientation(num2, out var orientaton), "ModuleItemConveyor.GetGeometryOrientation - k_RotationalInfo does not have a lookup for packed int: {0}, created from Directions In {1} > Out {2}", num2, inDir, outDir);
		ChangeGeometry(orientaton.m_Rotation, orientaton.m_Angle);
	}

	private void ChangeGeometry(RotationGeometry rotation, float angleYRot = 0f)
	{
		if (m_RotationGeometry == null)
		{
			return;
		}
		for (int i = 0; i < m_RotationGeometry.Length; i++)
		{
			bool flag = i == (int)rotation;
			if ((bool)m_RotationGeometry[i])
			{
				m_RotationGeometry[i].gameObject.SetActive(flag);
				if (flag)
				{
					m_RotationGeometry[i].SetLocalRotationIfChanged(Quaternion.Euler(0f, angleYRot, 0f));
				}
			}
		}
	}

	private bool GetGeometryOrientation(int packedInt, out Orientation orientaton)
	{
		return s_RotationalInfo.TryGetValue(packedInt, out orientaton);
	}

	private int ToFromAPsToPackedInt(Vector3 fromVec, Vector3 toVec)
	{
		int result = 0;
		if (k_EnumLookup.TryGetValue(fromVec, out var value))
		{
			if (k_EnumLookup.TryGetValue(toVec, out var value2))
			{
				result = ToFromEnumsToPackedInt(value, value2);
			}
			else
			{
				d.LogError("ModuleItemConveyor.ToFromAPsToPackedInt - k_EnumLookup does not have an entry for: " + toVec);
			}
		}
		else
		{
			d.LogError("ModuleItemConveyor.ToFromAPsToPackedInt - k_EnumLookup does not have an entry for: " + fromVec);
		}
		return result;
	}

	private int ToFromEnumsToPackedInt(BlockDirections from, BlockDirections to)
	{
		return ((int)from << k_FromBitshift) | (int)to;
	}

	private void InitRotationTable()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		int instanceID = base.block.transform.GetOriginalPrefab().GetInstanceID();
		d.AssertFormat(!s_OppositeLookup.TryGetValue(instanceID, out var _), this, "RotationTable lookup already exists for this prefab type.. ? {0}", base.name);
		s_OppositeLookup[instanceID] = dictionary;
		for (int i = 0; i < m_OppositeDirections.Length; i++)
		{
			dictionary.Add((int)m_OppositeDirections[i].m_Direction, (int)m_OppositeDirections[i].m_Opposite);
		}
		if (s_RotationalInfo == null)
		{
			s_RotationalInfo = new Dictionary<int, Orientation>
			{
				{
					ToFromAPsToPackedInt(k_NegZ, k_PosZ),
					new Orientation(RotationGeometry.Forward, 0f)
				},
				{
					ToFromAPsToPackedInt(k_PosZ, k_NegZ),
					new Orientation(RotationGeometry.Forward, 180f)
				},
				{
					ToFromAPsToPackedInt(k_PosX, k_NegX),
					new Orientation(RotationGeometry.Forward, -90f)
				},
				{
					ToFromAPsToPackedInt(k_NegX, k_PosX),
					new Orientation(RotationGeometry.Forward, 90f)
				},
				{
					ToFromAPsToPackedInt(k_NegZ, k_PosX),
					new Orientation(RotationGeometry.RightTurn, 0f)
				},
				{
					ToFromAPsToPackedInt(k_PosX, k_PosZ),
					new Orientation(RotationGeometry.RightTurn, -90f)
				},
				{
					ToFromAPsToPackedInt(k_PosZ, k_NegX),
					new Orientation(RotationGeometry.RightTurn, 180f)
				},
				{
					ToFromAPsToPackedInt(k_NegX, k_NegZ),
					new Orientation(RotationGeometry.RightTurn, 90f)
				},
				{
					ToFromAPsToPackedInt(k_NegZ, k_NegX),
					new Orientation(RotationGeometry.LeftTurn, 0f)
				},
				{
					ToFromAPsToPackedInt(k_NegX, k_PosZ),
					new Orientation(RotationGeometry.LeftTurn, 90f)
				},
				{
					ToFromAPsToPackedInt(k_PosZ, k_PosX),
					new Orientation(RotationGeometry.LeftTurn, 180f)
				},
				{
					ToFromAPsToPackedInt(k_PosX, k_NegZ),
					new Orientation(RotationGeometry.LeftTurn, -90f)
				},
				{
					ToFromAPsToPackedInt(k_NegZ, k_StepUpFwd),
					new Orientation(RotationGeometry.Forward, 0f)
				},
				{
					ToFromAPsToPackedInt(k_StepUpFwd, k_NegZ),
					new Orientation(RotationGeometry.Forward, 180f)
				},
				{
					ToFromAPsToPackedInt(k_NegZ, k_CornerUp),
					new Orientation(RotationGeometry.Forward, 0f)
				},
				{
					ToFromAPsToPackedInt(k_CornerUp, k_NegZ),
					new Orientation(RotationGeometry.Forward, 180f)
				},
				{
					ToFromAPsToPackedInt(k_NegY, k_PosZ),
					new Orientation(RotationGeometry.Forward, 0f)
				},
				{
					ToFromAPsToPackedInt(k_PosZ, k_NegY),
					new Orientation(RotationGeometry.Forward, 180f)
				}
			};
			kMaxLoopCycleCount = BlockManager.DefaultBlockLimit * BlockManager.DefaultBlockLimit * BlockManager.DefaultBlockLimit;
		}
	}

	private Vector3 GetLocalAPForStackConnectedBlock(TankBlock connectedBlock)
	{
		int[] apConnectionIndices = m_Holder.SingleStack.apConnectionIndices;
		foreach (int num in apConnectionIndices)
		{
			if (base.block.ConnectedBlocksByAP[num] == connectedBlock)
			{
				return base.block.attachPoints[num];
			}
		}
		return Vector3.zero;
	}

	private static Vector3 GetLocalAPForPreviewConnection(TankBlock neighbourBlock, ModuleItemConveyor sourceConveyor)
	{
		TankBlock localBlock = sourceConveyor.block;
		if (!s_PreviewNeighbourAPs.TryGetValue(GetConnection(localBlock, neighbourBlock), out var value))
		{
			return sourceConveyor.GetLocalAPForStackConnectedBlock(neighbourBlock);
		}
		return value;
	}

	private static DirectionalBlockPair GetConnection(TankBlock localBlock, TankBlock targetBlock)
	{
		return new DirectionalBlockPair
		{
			origin = localBlock,
			target = targetBlock
		};
	}

	private void OnAttached()
	{
		base.block.tank.Holders.RegisterOperation(m_Holder, OnCycle, -10);
		if (LinkOnAttach)
		{
			UpdateConnections();
		}
	}

	private void OnNeighbourAttached(TankBlock neighbourBlock)
	{
		if (LinkOnAttach)
		{
			UpdateConnections();
		}
	}

	private void OnNeighbourDetaching(TankBlock neighbourBlock)
	{
		if (!ManSaveGame.Storing && !Singleton.Manager<ManSpawn>.inst.IsRemovingAllBlocks)
		{
			UpdateConnections();
		}
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.UnregisterOperations(m_Holder);
		m_ConveyorConnections.Clear();
		m_InputStacks.Clear();
		m_OutputStacks.Clear();
		m_InConveyor = null;
		m_OutConveyor = null;
		m_OutConveyorIsReciprocating = false;
		m_ConnectionsDirty = true;
		ChangeGeometry(RotationGeometry.Forward);
	}

	private void OnStackConnect(ModuleItemHolder.Stack thisStack, ModuleItemHolder.Stack otherStack, Vector3 localAPPos, Vector3 otherLocalAP)
	{
		d.Assert(thisStack == m_Holder.SingleStack, "Conveyor does not currently support multiple stacks!", this);
		m_ConnectionsDirty = true;
		if (otherStack.myHolder.Conveyor.IsNotNull())
		{
			m_ConveyorConnections.Add(otherStack.myHolder.Conveyor);
			return;
		}
		ModuleItemHolder.IStackDirection component = otherStack.myHolder.GetComponent<ModuleItemHolder.IStackDirection>();
		if (component != null)
		{
			if (component.CanOutputTo(otherLocalAP, otherStack))
			{
				m_InputStacks.Add(otherStack);
			}
			if (component.CanReceiveOn(otherLocalAP, otherStack))
			{
				m_OutputStacks.Add(otherStack);
			}
		}
	}

	private void OnStackDisconnect(ModuleItemHolder.Stack thisStack, ModuleItemHolder.Stack otherStack, bool detachingSelf)
	{
		if (ManSaveGame.Storing)
		{
			return;
		}
		m_ConnectionsDirty = true;
		ModuleItemConveyor conveyor = otherStack.myHolder.Conveyor;
		if (conveyor.IsNotNull())
		{
			m_ConveyorConnections.Remove(conveyor);
			if ((object)conveyor == m_InConveyor)
			{
				m_InConveyor = null;
			}
			else if ((object)conveyor == m_OutConveyor)
			{
				m_OutConveyor = null;
				m_OutConveyorIsReciprocating = false;
			}
		}
		else
		{
			m_InputStacks.Remove(otherStack);
			m_OutputStacks.Remove(otherStack);
		}
	}

	void IBlockPlacementPreview.TryPreviewAttachments(IEnumerable<BlockPlacementPreviewHandler.APConnection> previewConnections)
	{
		if (previewConnections == null)
		{
			if (base.block.IsAttached)
			{
				UpdateLocalGeometry(m_InConveyor, m_OutConveyor, m_InputStacks, m_OutputStacks);
			}
			else
			{
				ChangeGeometry(RotationGeometry.Forward);
			}
			return;
		}
		if (base.block.IsAttached && previewConnections != null)
		{
			BlockPlacementPreviewHandler.APConnection aPConnection = previewConnections.First();
			if ((bool)aPConnection.otherBlock.GetComponent<ModuleItemConveyor>() || GetStackAtAP(m_Holder, aPConnection.blockAP) == null)
			{
				return;
			}
			ModuleItemHolder.IStackDirection component = aPConnection.otherBlock.GetComponent<ModuleItemHolder.IStackDirection>();
			if (component == null)
			{
				return;
			}
			Vector3 vector = BlockPlacementPreviewHandler.TransformBlockAPToTargetBlockAP(base.block, aPConnection.blockAP, aPConnection.otherBlock);
			ModuleItemHolder.Stack stack = GetStackAtAP(aPConnection.otherBlock.GetComponent<ModuleItemHolder>(), vector);
			if (stack == null)
			{
				return;
			}
			bool flag = component.CanOutputTo(vector, stack);
			if (flag)
			{
				s_PreviewInputStacks.Add(stack);
			}
			bool flag2 = component.CanReceiveOn(vector, stack);
			if (flag2)
			{
				s_PreviewOutputStacks.Add(stack);
			}
			if (flag || flag2)
			{
				s_PreviewNeighbourAPs.Add(GetConnection(base.block, aPConnection.otherBlock), aPConnection.blockAP);
				s_PreviewInputStacks.AddRange(m_InputStacks);
				s_PreviewOutputStacks.AddRange(m_OutputStacks);
				UpdateLocalGeometry(m_InConveyor, m_OutConveyor, s_PreviewInputStacks, s_PreviewOutputStacks, (TankBlock pb) => GetLocalAPForPreviewConnection(pb, this));
				s_PreviewInputStacks.Clear();
				s_PreviewOutputStacks.Clear();
				s_PreviewNeighbourAPs.Clear();
			}
			return;
		}
		ModuleItemConveyor previewInConveyor = null;
		ModuleItemConveyor previewOutConveyor = null;
		bool mayHaveConnectionChanges = false;
		if (previewConnections != null)
		{
			foreach (BlockPlacementPreviewHandler.APConnection previewConnection in previewConnections)
			{
				DirectionalBlockPair connection = GetConnection(base.block, previewConnection.otherBlock);
				if (s_PreviewNeighbourAPs.ContainsKey(connection))
				{
					d.LogWarning("Not explicitly handling multiple shared connections in Conveyor previews yet. Current geometry only has single connections, so we handle that and skip the rest!", this);
				}
				else
				{
					if (!HasStackConnectionOnAP(base.block, previewConnection.blockAP, previewConnection.otherBlock, out var otherLocalAP, out var otherStack))
					{
						continue;
					}
					s_PreviewNeighbourAPs.Add(connection, previewConnection.blockAP);
					s_PreviewNeighbourAPs.Add(GetConnection(previewConnection.otherBlock, base.block), otherLocalAP);
					ModuleItemConveyor component2 = previewConnection.otherBlock.GetComponent<ModuleItemConveyor>();
					if (component2.IsNotNull())
					{
						s_PreviewConveyorConnections.Add(component2);
						continue;
					}
					ModuleItemHolder.IStackDirection component3 = previewConnection.otherBlock.GetComponent<ModuleItemHolder.IStackDirection>();
					if (component3 != null)
					{
						if (component3.CanOutputTo(otherLocalAP, otherStack))
						{
							s_PreviewInputStacks.Add(otherStack);
						}
						if (component3.CanReceiveOn(otherLocalAP, otherStack))
						{
							s_PreviewOutputStacks.Add(otherStack);
						}
					}
				}
			}
			if (s_PreviewNeighbourAPs.Count == 0)
			{
				ChangeGeometry(RotationGeometry.None);
				s_PreviewConveyorConnections.Clear();
				s_PreviewInputStacks.Clear();
				s_PreviewOutputStacks.Clear();
				s_PreviewNeighbourAPs.Clear();
				return;
			}
			foreach (ModuleItemConveyor conveyor in s_PreviewConveyorConnections)
			{
				Vector3 blockAP = previewConnections.First((BlockPlacementPreviewHandler.APConnection apc) => apc.otherBlock == conveyor.block).blockAP;
				int num = 0;
				ModuleItemHolder.StackIterator.Enumerator enumerator3 = m_Holder.Stacks.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					ModuleItemHolder.Stack current2 = enumerator3.Current;
					for (int num2 = 0; num2 < current2.apConnectionIndices.Length; num2++)
					{
						int num3 = current2.apConnectionIndices[num2];
						if (base.block.attachPoints[num3].EqualsEpsilon(blockAP))
						{
							break;
						}
						num++;
					}
				}
				s_ConveyorNeighbourStackIdx.Add(conveyor, num);
			}
			s_PreviewConveyorConnections.Sort((ModuleItemConveyor a, ModuleItemConveyor b) => s_ConveyorNeighbourStackIdx[a].CompareTo(s_ConveyorNeighbourStackIdx[b]));
			s_ConveyorNeighbourStackIdx.Clear();
			s_PreviewConveyorConnections.InsertionSort(s_StackConnectionPriorityFunc);
			if (previewInConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor = s_PreviewConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => conv.m_PrefersOutputConveyorConnection);
				if (moduleItemConveyor.IsNotNull() && !s_PreviewConveyorConnections.Any((ModuleItemConveyor conv) => conv.m_InConveyor.IsNotNull() || conv.m_OutConveyor.IsNotNull()))
				{
					SetPreviewConveyorRef(ref previewInConveyor, moduleItemConveyor, s_PreviewConveyorConnections, isInput: true, isPush: false);
				}
			}
			if (previewOutConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor2 = s_PreviewConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => conv.m_InConveyor.IsNull() && (object)conv != previewInConveyor && (object)conv.m_OutConveyor != this);
				if (moduleItemConveyor2.IsNotNull() && (object)moduleItemConveyor2 != previewOutConveyor)
				{
					SetPreviewConveyorRef(ref previewOutConveyor, moduleItemConveyor2, s_PreviewConveyorConnections, isInput: false, isPush: false);
				}
			}
			if (previewInConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor3 = s_PreviewConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => (conv.m_OutConveyor.IsNull() || !conv.m_OutConveyorIsReciprocating) && (object)conv != previewOutConveyor);
				if (moduleItemConveyor3.IsNotNull())
				{
					SetPreviewConveyorRef(ref previewInConveyor, moduleItemConveyor3, s_PreviewConveyorConnections, isInput: true, isPush: false);
				}
			}
			if (previewOutConveyor.IsNull())
			{
				ModuleItemConveyor moduleItemConveyor4 = s_PreviewConveyorConnections.FirstOrDefault((ModuleItemConveyor conv) => (object)conv != previewInConveyor);
				if (moduleItemConveyor4.IsNotNull())
				{
					SetPreviewConveyorRef(ref previewOutConveyor, moduleItemConveyor4, s_PreviewConveyorConnections, isInput: false, isPush: true);
				}
			}
		}
		if (!mayHaveConnectionChanges)
		{
			mayHaveConnectionChanges = (previewInConveyor.IsNull() && s_PreviewInputStacks.Count > 0) || (previewOutConveyor.IsNull() && s_PreviewOutputStacks.Count > 0);
		}
		if (mayHaveConnectionChanges)
		{
			UpdateLocalGeometry(previewInConveyor, previewOutConveyor, s_PreviewInputStacks, s_PreviewOutputStacks, (TankBlock pb) => GetLocalAPForPreviewConnection(pb, this));
		}
		s_PreviewConveyorConnections.Clear();
		s_PreviewInputStacks.Clear();
		s_PreviewOutputStacks.Clear();
		s_PreviewNeighbourAPs.Clear();
		static ModuleItemHolder.Stack GetStackAtAP(ModuleItemHolder holder, Vector3 aPPosLocal)
		{
			ModuleItemHolder.StackIterator.Enumerator enumerator4 = holder.Stacks.GetEnumerator();
			while (enumerator4.MoveNext())
			{
				ModuleItemHolder.Stack current3 = enumerator4.Current;
				for (int i = 0; i < current3.apConnectionIndices.Length; i++)
				{
					int num4 = current3.apConnectionIndices[i];
					if (holder.block.attachPoints[num4].EqualsEpsilon(aPPosLocal, 0.01f))
					{
						return current3;
					}
				}
			}
			return null;
		}
		bool HasStackConnectionOnAP(TankBlock thisBlock, Vector3 aPPosLocal, TankBlock otherBlock, out Vector3 reference, out ModuleItemHolder.Stack reference2)
		{
			reference = default(Vector3);
			reference2 = null;
			ModuleItemHolder component4 = thisBlock.GetComponent<ModuleItemHolder>();
			if (component4.IsNull())
			{
				return false;
			}
			ModuleItemHolder component5 = otherBlock.GetComponent<ModuleItemHolder>();
			if (component5.IsNull())
			{
				return false;
			}
			if (GetStackAtAP(component4, aPPosLocal) == null)
			{
				return false;
			}
			reference = BlockPlacementPreviewHandler.TransformBlockAPToTargetBlockAP(base.block, aPPosLocal, otherBlock);
			reference2 = GetStackAtAP(component5, reference);
			if (reference2 == null)
			{
				return false;
			}
			return true;
		}
		void SetPreviewConveyorRef(ref ModuleItemConveyor previewConveyor, ModuleItemConveyor target, List<ModuleItemConveyor> connectionsList, bool isInput, bool isPush)
		{
			previewConveyor = target;
			connectionsList.Remove(target);
			mayHaveConnectionChanges = true;
			if (!isPush)
			{
				target.UpdateLocalGeometry(isInput ? target.m_InConveyor : this, isInput ? this : target.m_OutConveyor, target.m_InputStacks, target.m_OutputStacks, (TankBlock pb) => GetLocalAPForPreviewConnection(pb, target));
			}
		}
	}

	private TechHolders.OperationResult OnCycle()
	{
		int heartbeatCount = base.block.tank.Holders.HeartbeatCount;
		if (m_LastPassHeartbeat == heartbeatCount && !m_FirstPasserThisHeartbeat)
		{
			return TechHolders.OperationResult.None;
		}
		m_FirstPasserThisHeartbeat = true;
		m_LastPassHeartbeat = heartbeatCount;
		ModuleItemConveyor inConveyor = m_InConveyor;
		ModuleItemConveyor moduleItemConveyor = null;
		bool flag = false;
		bool flag2 = false;
		while (inConveyor.IsNotNull())
		{
			if (!flag)
			{
				flag = inConveyor.m_Holder.SingleStack.ReceivedThisHeartbeat;
			}
			if (moduleItemConveyor.IsNull() && inConveyor.m_Holder.SingleStack.IsEmpty)
			{
				moduleItemConveyor = inConveyor;
			}
			if ((object)inConveyor == this)
			{
				flag2 = true;
				break;
			}
			inConveyor.m_FirstPasserThisHeartbeat = false;
			inConveyor.m_LastPassHeartbeat = heartbeatCount;
			inConveyor = inConveyor.m_InConveyor;
		}
		bool flag3 = false;
		ModuleItemConveyor moduleItemConveyor2 = this;
		ModuleItemConveyor moduleItemConveyor3 = null;
		if (flag2)
		{
			if (moduleItemConveyor.IsNull() && flag)
			{
				return TechHolders.OperationResult.None;
			}
			if (moduleItemConveyor.IsNotNull())
			{
				moduleItemConveyor2 = moduleItemConveyor;
			}
			else
			{
				flag3 = true;
			}
		}
		else
		{
			int num = kMaxLoopCycleCount;
			ModuleItemConveyor moduleItemConveyor4 = this;
			while (moduleItemConveyor4.m_OutConveyor.IsNotNull() && moduleItemConveyor4.m_OutConveyorIsReciprocating && num-- > 0)
			{
				moduleItemConveyor4 = moduleItemConveyor4.m_OutConveyor;
				moduleItemConveyor4.m_FirstPasserThisHeartbeat = false;
				moduleItemConveyor4.m_LastPassHeartbeat = heartbeatCount;
			}
			if (num <= 0)
			{
				d.LogErrorFormat("Conveyor '{0}' was misconfigured somehow, resulting in an infinite loop. Resetting it in an attempt to bail out!", base.name);
			}
			moduleItemConveyor2 = moduleItemConveyor4.m_InConveyor;
			if (moduleItemConveyor4.PushConveyor.IsNotNull())
			{
				moduleItemConveyor2 = moduleItemConveyor4;
				bool flag4 = !moduleItemConveyor4.m_Holder.SingleStack.IsEmpty && !moduleItemConveyor4.m_Holder.SingleStack.ReceivedThisHeartbeat;
				while (flag4 && moduleItemConveyor4.IsNotNull())
				{
					moduleItemConveyor4 = moduleItemConveyor4.m_InConveyor;
					if (moduleItemConveyor4.IsNotNull() && (moduleItemConveyor4.m_Holder.SingleStack.IsEmpty || moduleItemConveyor4.m_Holder.SingleStack.ReceivedThisHeartbeat))
					{
						flag4 = false;
					}
					if ((object)moduleItemConveyor4 == moduleItemConveyor2.PushConveyor)
					{
						flag3 = flag4;
						break;
					}
				}
			}
		}
		if (flag3)
		{
			moduleItemConveyor3 = moduleItemConveyor2.m_OutConveyor;
		}
		ModuleItemConveyor moduleItemConveyor5 = moduleItemConveyor2;
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		while (moduleItemConveyor5.IsNotNull())
		{
			if (!moduleItemConveyor5.m_Holder.SingleStack.IsEmpty && ((object)moduleItemConveyor5 == moduleItemConveyor3 || !moduleItemConveyor5.m_Holder.SingleStack.ReceivedThisHeartbeat))
			{
				Visible firstItem = moduleItemConveyor5.m_Holder.SingleStack.FirstItem;
				ModuleItemConveyor outConveyor = moduleItemConveyor5.m_OutConveyor;
				bool flag5 = false;
				if (outConveyor.IsNotNull())
				{
					flag5 = outConveyor.m_Holder.SingleStack.Take(firstItem, flag3, insertAtBase: false);
				}
				else
				{
					d.LogErrorFormat("ModuleItemConveyor.OnCycle - '{0}' Did not have a valid conveyor target to pass to! How did this happen!?", base.name);
				}
				flag3 = false;
				if (flag5)
				{
					switch (operationResult)
					{
					case TechHolders.OperationResult.None:
						operationResult = TechHolders.OperationResult.Effect;
						break;
					case TechHolders.OperationResult.Retry:
						operationResult = TechHolders.OperationResult.EffectRetry;
						break;
					}
				}
				else
				{
					switch (operationResult)
					{
					case TechHolders.OperationResult.None:
						operationResult = TechHolders.OperationResult.Retry;
						break;
					case TechHolders.OperationResult.Effect:
						operationResult = TechHolders.OperationResult.EffectRetry;
						break;
					}
				}
			}
			moduleItemConveyor5 = moduleItemConveyor5.m_InConveyor;
			if ((object)moduleItemConveyor5 == moduleItemConveyor2)
			{
				break;
			}
		}
		return operationResult;
	}

	private ModuleItemConveyor GetAdjacentConveyorDuringSpawn(IntVector3 pos)
	{
		if (pos != IntVector3.invalid)
		{
			TankBlock tankBlock = Singleton.Manager<ManSpawn>.inst.GetBlockByLocalPositionDuringSpawn(pos);
			if (tankBlock.IsNotNull())
			{
				return tankBlock.GetComponent<ModuleItemConveyor>();
			}
		}
		return null;
	}

	private bool LoadConveyorReferences(IntVector3 inPos, IntVector3 outPos, bool outIsReciprocal)
	{
		bool result = false;
		if (m_InConveyor.IsNull())
		{
			ModuleItemConveyor adjacentConveyorDuringSpawn = GetAdjacentConveyorDuringSpawn(inPos);
			if (adjacentConveyorDuringSpawn.IsNotNull())
			{
				d.AssertFormat(adjacentConveyorDuringSpawn.m_OutConveyor.IsNull() || (object)adjacentConveyorDuringSpawn.m_OutConveyor == this, "Multiple To/From conveyor links referencing same block ('{2}'). Ignoring this one. this:{0} to:{1}", (IntVector3)base.block.trans.localPosition, inPos, base.name);
				EstablishConveyorLink(adjacentConveyorDuringSpawn, this);
				result = true;
			}
		}
		if (m_OutConveyor.IsNull())
		{
			ModuleItemConveyor adjacentConveyorDuringSpawn2 = GetAdjacentConveyorDuringSpawn(outPos);
			if (adjacentConveyorDuringSpawn2.IsNotNull())
			{
				if (!adjacentConveyorDuringSpawn2.m_InConveyor.IsNull() && outIsReciprocal)
				{
					d.LogErrorFormat(this, "Multiple To/From conveyor links referencing same block ('{2}'). Forcing a one-directional connection. this:{0} to:{1}", (IntVector3)base.block.trans.localPosition, outPos, base.name);
					outIsReciprocal = false;
				}
				EstablishConveyorLink(this, adjacentConveyorDuringSpawn2, outIsReciprocal);
				result = true;
			}
		}
		return result;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			IntVector3 fromPos = (m_InConveyor.IsNotNull() ? ((IntVector3)m_InConveyor.block.trans.localPosition) : IntVector3.invalid);
			bool num = !m_OutConveyorIsReciprocating;
			IntVector3 toPos = ((!num && m_OutConveyor.IsNotNull()) ? ((IntVector3)m_OutConveyor.block.trans.localPosition) : IntVector3.invalid);
			IntVector3 pushPos = ((num && m_OutConveyor.IsNotNull()) ? ((IntVector3)m_OutConveyor.block.trans.localPosition) : IntVector3.invalid);
			SerialData serialData = new SerialData();
			serialData.fromPos = fromPos;
			serialData.toPos = toPos;
			serialData.pushPos = pushPos;
			serialData.Store(blockSpec.saveState);
		}
		else
		{
			SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
			if (serialData2 != null)
			{
				IntVector3 fromPos2 = serialData2.fromPos;
				bool num2 = serialData2.pushPos != IntVector3.invalid;
				IntVector3 outPos = (num2 ? serialData2.pushPos : serialData2.toPos);
				bool outIsReciprocal = !num2;
				LoadConveyorReferences(fromPos2, outPos, outIsReciprocal);
				UpdateLocalGeometry(m_InConveyor, m_OutConveyor, m_InputStacks, m_OutputStacks);
			}
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			IntVector3 intVector = (m_InConveyor.IsNotNull() ? ((IntVector3)m_InConveyor.block.trans.localPosition) : IntVector3.invalid);
			IntVector3 intVector2 = (m_OutConveyor.IsNotNull() ? ((IntVector3)m_OutConveyor.block.trans.localPosition) : IntVector3.invalid);
			context.Store(GetType(), "from", intVector.ToString());
			context.Store(GetType(), "to", intVector2.ToString());
			context.Store(GetType(), "toIsReciprocal", m_OutConveyorIsReciprocating ? "true" : "false");
		}
		else if (onTech)
		{
			TryParseVector(context, "from", out var pos);
			TryParseVector(context, "to", out var pos2);
			TryParseVector(context, "push", out var pos3);
			bool num = pos3 != IntVector3.invalid;
			pos2 = (num ? pos3 : pos2);
			bool outIsReciprocal = !num;
			LoadConveyorReferences(pos, pos2, outIsReciprocal);
			UpdateLocalGeometry(m_InConveyor, m_OutConveyor, m_InputStacks, m_OutputStacks);
		}
		bool TryParseVector(TankPreset.BlockSpec blockSpecContext, string tagStr, out IntVector3 reference)
		{
			string outValue;
			bool flag = blockSpecContext.TryRetrieve(GetType(), tagStr, out outValue);
			d.AssertFormat(flag, this, "Failed to find tag '{0}' in savedata.", tagStr);
			reference = ((flag && !outValue.NullOrEmpty()) ? IntVector3.ConvertFromString(outValue) : IntVector3.invalid);
			return flag;
		}
	}

	public static bool TryCopyRuntimeState(TankPreset.BlockSpec blockspecSource, ref Dictionary<int, Module.SerialData> saveStateDest)
	{
		SerialData serialData = ((blockspecSource.saveState != null) ? SerialData<SerialData>.Retrieve(blockspecSource.saveState) : null);
		if (serialData != null)
		{
			if (saveStateDest == null)
			{
				saveStateDest = new Dictionary<int, Module.SerialData>();
			}
			serialData.Store(saveStateDest);
			return true;
		}
		return false;
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleItemConveyor;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		UpdateNetworkedState();
		m_SyncedConveyorOrientation.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_SyncedConveyorOrientation.Deserialise(reader);
	}

	private void OnConveyorOrientationSet(ConveyorOrientationMessage msg)
	{
		m_InConveyor = msg.InConveyor;
		m_OutConveyor = msg.OutConveyor;
		m_OutConveyorIsReciprocating = msg.OutConveyorIsReciprocating;
		UpdateGeometry(netSend: false);
	}

	private bool UpdateNetworkedState()
	{
		int num;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			if (!(m_SyncedConveyorOrientation.Data.InConveyor != m_InConveyor) && !(m_SyncedConveyorOrientation.Data.OutConveyor != m_OutConveyor))
			{
				num = ((m_SyncedConveyorOrientation.Data.OutConveyorIsReciprocating != m_OutConveyorIsReciprocating) ? 1 : 0);
				if (num == 0)
				{
					goto IL_00ac;
				}
			}
			else
			{
				num = 1;
			}
			m_SyncedConveyorOrientation.Data.InConveyor = m_InConveyor;
			m_SyncedConveyorOrientation.Data.OutConveyor = m_OutConveyor;
			m_SyncedConveyorOrientation.Data.OutConveyorIsReciprocating = m_OutConveyorIsReciprocating;
			goto IL_00ac;
		}
		return false;
		IL_00ac:
		return (byte)num != 0;
	}

	private void PrePool()
	{
		InitRotationTable();
		s_StackConnectionPriorityFunc = GetStackConnectionPriority;
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.NeighbourAttachedEvent.Subscribe(OnNeighbourAttached);
		base.block.NeighbourDetachingEvent.Subscribe(OnNeighbourDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.StackConnectEvent.Subscribe(OnStackConnect);
		m_Holder.StackDisconnectEvent.Subscribe(OnStackDisconnect);
		m_Holder.ItemRequestHandler = this;
		m_SyncedConveyorOrientation = new NetworkedProperty<ConveyorOrientationMessage>(this, TTMsgType.SetConveyorOrientation, OnConveyorOrientationSet);
		int instanceID = base.block.transform.GetOriginalPrefab().GetInstanceID();
		d.AssertFormat(s_OppositeLookup.TryGetValue(instanceID, out m_OppositeLookup), this, "Failed to initialise opposite lookup to a setup table?! {0}", this);
	}

	private void OnSpawn()
	{
		m_InConveyor = null;
		m_OutConveyor = null;
		m_OutConveyorIsReciprocating = false;
		ChangeGeometry(RotationGeometry.Forward);
	}

	private void OnRecycle()
	{
		d.Assert(m_InConveyor.IsNull(), "InConveyor Reference still assigned on recycle", this);
		d.Assert(m_OutConveyor.IsNull(), "OutConveyor Reference still assigned on recycle", this);
	}

	public void HandleExpandSearch(ItemSearcher builder, ModuleItemHolder.Stack entryStack, ModuleItemHolder.Stack prevStack, out ItemSearchAvailableItems availItems)
	{
		ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator = m_Holder.SingleStack.ConnectedStacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (m_OutConveyor.IsNull() || (object)current.myHolder != m_OutConveyor.m_Holder)
			{
				ModuleItemConveyor conveyor = current.myHolder.Conveyor;
				if (conveyor.IsNull() || (object)conveyor.m_OutConveyor == this)
				{
					builder.PushNode(current, m_Holder.SingleStack);
				}
			}
		}
		availItems = ItemSearchAvailableItems.Processed;
	}

	public void HandleSearchRequest()
	{
	}

	public bool WantsToKnowAboutSearchRequest()
	{
		return false;
	}

	public void HandleCollectItems(ItemSearchCollector collector, bool processed)
	{
		if (!m_Holder.SingleStack.IsEmpty)
		{
			collector.OfferItem(m_Holder.SingleStack.FirstItem);
		}
	}
}
