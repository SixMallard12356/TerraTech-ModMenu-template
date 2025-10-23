#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[RequireComponent(typeof(Visible))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleDamage))]
public class TankBlock : MonoBehaviour, ManVisible.StateVisualiser.Provider, IGravityAdjustmentTarget, IGravityApplicationTarget
{
	[Flags]
	public enum Flags
	{
		AllowGroundIntersection = 1,
		HasContextMenu = 2
	}

	[Flags]
	public enum FilledCellFlags
	{
		AllowOverlap = 1
	}

	public struct FreeAPIterator
	{
		private TankBlock m_Block;

		private int m_Index;

		public Vector3 Current => m_Block.trans.TransformPoint(m_Block.attachPoints[m_Index]);

		public FreeAPIterator(TankBlock block)
		{
			m_Block = block;
			m_Index = -1;
		}

		public bool MoveNext()
		{
			if (m_Block.APIgnoreFilter != null)
			{
				do
				{
					m_Index++;
					if (m_Index == m_Block.attachPoints.Length)
					{
						return false;
					}
				}
				while (m_Block.ConnectedBlocksByAP[m_Index].IsNotNull() || m_Block.APIgnoreFilter(m_Index));
			}
			else
			{
				do
				{
					m_Index++;
					if (m_Index == m_Block.attachPoints.Length)
					{
						return false;
					}
				}
				while (m_Block.ConnectedBlocksByAP[m_Index].IsNotNull());
			}
			return true;
		}

		public FreeAPIterator GetEnumerator()
		{
			return new FreeAPIterator(m_Block);
		}
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class ComponentIndexLookup
	{
		private Dictionary<int, int> m_ComponentIndexLookup = new Dictionary<int, int>();

		private int m_MaxIndex;

		private const int k_MinComponentLookupArraySize = 8;

		public int GetIndex(Type type)
		{
			int hashCode = type.GetHashCode();
			int value = int.MaxValue;
			m_ComponentIndexLookup.TryGetValue(hashCode, out value);
			return value;
		}

		public int GetOrAddIndex(Type type)
		{
			int hashCode = type.GetHashCode();
			if (!m_ComponentIndexLookup.TryGetValue(hashCode, out var value))
			{
				value = m_MaxIndex++;
				m_ComponentIndexLookup[hashCode] = value;
			}
			return value;
		}

		public void BuildLookup(TankBlock block)
		{
			int num = Mathf.Max(m_MaxIndex, 8);
			block.m_IndexedComponents = new Component[num];
			Component[] components = block.GetComponents(typeof(Component));
			foreach (Component component in components)
			{
				Type type = component.GetType();
				int orAddIndex = GetOrAddIndex(type);
				if (orAddIndex >= num)
				{
					num = orAddIndex + 1;
					Array.Resize(ref block.m_IndexedComponents, num);
				}
				block.m_IndexedComponents[orAddIndex] = component;
			}
		}
	}

	private enum BlockLockTimerTypes
	{
		AnchorFreely,
		Attachable,
		AttachableOutsideTutorial
	}

	public enum BlockLinkAudioType
	{
		StandardBlock,
		BFControl,
		BFComputer
	}

	public enum MassCategoryType
	{
		None,
		Magnet,
		VariableMass
	}

	[FormerlySerializedAs("mass")]
	public float m_DefaultMass = 1f;

	public int m_Tier;

	[SerializeField]
	public BlockCategories m_BlockCategory;

	[SerializeField]
	public BlockRarity m_BlockRarity;

	[SerializeField]
	public BlockLinkAudioType m_BlockLinkAudioType;

	[EnumFlag]
	[SerializeField]
	private Flags m_Flags;

	public bool m_PreventSelfDestructOnFirstDetach;

	public Vector3[] attachPoints;

	public IntVector3[] filledCells;

	public byte[] filledCellFlags;

	private INetworkedModule[] m_NetworkedModules;

	[SerializeField]
	[HideInInspector]
	private Bounds m_BlockCellBounds;

	public static bool debugDrawBlockLinks = false;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private ManHUD.HUDElementType m_ContextMenuType;

	[HideInInspector]
	[SerializeField]
	public bool m_ContextMenuForPlayerTechOnly = true;

	[SerializeField]
	[HideInInspector]
	private MaterialSwapper m_MaterialSwapper;

	private bool m_firstDetachment = true;

	public EventNoParams AttachingEvent;

	public Event<TankBlock> NeighbourAttachingEvent;

	public EventNoParams AttachedEvent;

	public Event<TankBlock> NeighbourAttachedEvent;

	public EventNoParams DetachingEvent;

	public Event<TankBlock> NeighbourDetachingEvent;

	public EventNoParams DetachedEvent;

	public Event<TankBlock> NeighbourDetachedEvent;

	public Event<uint> UniqueIDAssignedEvent;

	public EventNoParams MassChangedEvent;

	public Event<TankBlock, int> MouseDownEvent;

	public Event<ManPointer.DragAction, Vector3> DragEvent;

	public Event<bool, TankPreset.BlockSpec> serializeEvent;

	public Event<bool, TankPreset.BlockSpec, bool> serializeTextEvent;

	public Event<bool> PostSerializeEvent;

	public Event<bool> PostSerializeTextEvent;

	public MonoBehaviourEvent<MB_Update> BlockUpdate;

	public MonoBehaviourEvent<MB_FixedUpdate> BlockFixedUpdate;

	public MonoBehaviourEvent<MB_LateUpdate> BlockLateUpdate;

	public const uint k_BlockPoolID_Unset = uint.MaxValue;

	public const uint k_BlockPoolID_Pooled = 4294967294u;

	public const uint k_BlockPoolID_Spawned = 4294967293u;

	public const uint k_BlockPoolID_Recycled = 4294967292u;

	public const uint k_BlockPoolID_LastValid = 4294967290u;

	[HideInInspector]
	[SerializeField]
	private float m_CurrentMass;

	[HideInInspector]
	[SerializeField]
	private Vector3 m_CentreOfMass;

	[HideInInspector]
	[SerializeField]
	private Vector3 m_DefaultInertiaTensor;

	[HideInInspector]
	[SerializeField]
	private EventRelay[] m_CollisionRelays;

	[SerializeField]
	[HideInInspector]
	private Vector3 m_CentreOfGravity;

	[SerializeField]
	[HideInInspector]
	private int[] m_APFilledCells;

	private WarningHolder m_DeprecationWarning;

	private static int s_NextRecurseSearchIndex = 0;

	private int m_RecurseIndex = -1;

	private Tank m_Tank;

	private Dictionary<MassCategoryType, double> m_AdditionalMassCategories = new Dictionary<MassCategoryType, double>();

	private uint m_BlockPoolID = uint.MaxValue;

	private bool m_EnableTutorialCollision;

	private bool m_IsTerrainOnly;

	private bool m_HasRBodyInPrefab;

	private byte m_SkinIndex;

	private float[] m_LockTimers = new float[Enum.GetNames(typeof(BlockLockTimerTypes)).Length];

	private static int s_NumCategoryTypes = -1;

	private Component[] m_IndexedComponents;

	private static ComponentIndexLookup s_ComponentIndexLookup = new ComponentIndexLookup();

	private static IntVector3[] s_MyAPposCache = new IntVector3[128];

	private bool m_HasTriggerCatcher;

	private Event<TankBlock, Collider> m_TriggerStayEvent;

	private static IntVector3[] s_OtherAPposCache = new IntVector3[128];

	[SerializeField]
	[HideInInspector]
	private float[] FilledCellsGravityScaleFactors;

	private float m_AverageGravityScaleFactor;

	private bool m_NeedsCentreOfGravityUpdate;

	private bool m_AverageGravityScaleDirty;

	private bool m_GravityAdjustmentTouched;

	private bool m_GravityApplicationTouched;

	private float[] m_LastFilledCellsGravityScaleFactors;

	private static HashSet<TankBlock> s_VisitedBlocks = new HashSet<TankBlock>();

	private Dictionary<short, Action<MessageBase>> m_MessageHandlers;

	public uint blockPoolID => m_BlockPoolID;

	public Tank tank { get; private set; }

	public Tank lastTank { get; private set; }

	public NetBlock netBlock { get; set; }

	public INetworkedModule[] NetworkedModules => m_NetworkedModules;

	public bool HasContextMenu => (m_Flags & Flags.HasContextMenu) != 0;

	public ManHUD.HUDElementType ContextMenuType => m_ContextMenuType;

	public bool ContextMenuForPlayerTechOnly => m_ContextMenuForPlayerTechOnly;

	public int LastTechTeam { get; private set; } = int.MaxValue;

	public bool IsController { get; private set; }

	public ModuleAnchor Anchor { get; private set; }

	public ModuleCircuitNode CircuitNode { get; private set; }

	public ModuleCircuitReceiver CircuitReceiver => CircuitNode.Receiver;

	public ModuleItemHolderMagnet GluedToMagnet { get; private set; }

	public float CurrentMass
	{
		get
		{
			return m_CurrentMass;
		}
		private set
		{
			bool num = m_CurrentMass != value;
			m_CurrentMass = value;
			if (num)
			{
				MassChangedEvent.Send();
			}
		}
	}

	private double AllMass
	{
		get
		{
			if (m_AdditionalMassCategories == null)
			{
				return 0.0;
			}
			double num = 0.0;
			foreach (KeyValuePair<MassCategoryType, double> additionalMassCategory in m_AdditionalMassCategories)
			{
				num += additionalMassCategory.Value;
			}
			return (double)m_DefaultMass + num;
		}
	}

	public Vector3 CurrentInertiaTensor { get; private set; }

	public Vector3 CentreOfMass => m_CentreOfMass;

	public Vector3 CentreOfGravity
	{
		get
		{
			if (m_NeedsCentreOfGravityUpdate)
			{
				RecalculateCentreOfGravity();
			}
			return m_CentreOfGravity;
		}
	}

	public Vector3 centreOfMassWorld
	{
		get
		{
			return trans.TransformPoint(m_CentreOfMass);
		}
		set
		{
			SetPositionCOM(value);
		}
	}

	public float decayTimeout { get; private set; }

	public Vector3 cachedLocalPosition { get; private set; }

	public OrthoRotation cachedLocalRotation { get; private set; }

	public int NumConnectedAPs { get; private set; }

	public Rigidbody rbody { get; private set; }

	public Visible visible { get; private set; }

	public Transform trans { get; private set; }

	public ModuleDamage damage { get; private set; }

	public ManPointer.OpenMenuEventConsumer openMenuEventConsumer { get; private set; }

	public ModuleWeapon weapon { get; private set; }

	public ModuleWheels wheelsModule { get; private set; }

	public ModuleEnergy energyModule { get; private set; }

	public float AverageGravityScaleFactor
	{
		get
		{
			if (m_AverageGravityScaleDirty)
			{
				RecalculateAverageGravityScaleFactor();
			}
			return m_AverageGravityScaleFactor;
		}
	}

	public ManDamage.DamageInfo DamageInEffect { get; set; }

	public Bounds BlockCellBounds => m_BlockCellBounds;

	public TankBlock[] ConnectedBlocksByAP { get; private set; }

	public bool IsDeprecated => Singleton.Manager<ManSpawn>.inst.IsBlockTypeDeprecated(visible.m_ItemType);

	public bool IsBeingDragged => (object)Singleton.Manager<ManPointer>.inst.DraggingItem == visible;

	public BlockTypes BlockType => (BlockTypes)visible.ItemType;

	public BlockCategories BlockCategory => m_BlockCategory;

	public BlockRarity BlockRarity => m_BlockRarity;

	public bool IsInteractible => visible.IsInteractible;

	public bool CanAnchorFreely => !IsLocked(BlockLockTimerTypes.AnchorFreely);

	public bool CanAttach => !IsLocked(BlockLockTimerTypes.Attachable);

	public bool CanAttachOutsideTutorial => !IsLocked(BlockLockTimerTypes.AttachableOutsideTutorial);

	public bool IsAttached => tank.IsNotNull();

	public bool HasNeighbours => NumConnectedAPs > 0;

	public BlockLinkAudioType BlockConnectionAudioType => m_BlockLinkAudioType;

	public static int NumCategoryTypes
	{
		get
		{
			if (s_NumCategoryTypes < 0)
			{
				s_NumCategoryTypes = Enum.GetNames(typeof(BlockCategories)).Length;
			}
			return s_NumCategoryTypes;
		}
	}

	public Func<int, bool> APIgnoreFilter { get; private set; }

	public bool PreExplodePulse
	{
		get
		{
			return m_MaterialSwapper.PreExplodePulse;
		}
		set
		{
			m_MaterialSwapper.PreExplodePulse = value;
		}
	}

	public void SetBlockPoolID(uint n)
	{
		m_BlockPoolID = n;
		UniqueIDAssignedEvent.Send(n);
	}

	public bool HasValidBlockPoolID()
	{
		return IsBlockPoolIDValid(blockPoolID);
	}

	public static bool IsBlockPoolIDValid(uint blockPoolID)
	{
		return blockPoolID <= 4294967290u;
	}

	public bool IsFlag(Flags flag)
	{
		return (m_Flags & flag) != 0;
	}

	public Vector3 CalcFirstFilledCellLocalPos()
	{
		return cachedLocalPosition + cachedLocalRotation * filledCells[0];
	}

	public void SetCachedLocalPositionData(Vector3 pos, OrthoRotation orthoRot)
	{
		cachedLocalPosition = pos;
		cachedLocalRotation = orthoRot;
	}

	public void LinkTo(List<BlockManager.BlockAttachment> attachments, OrthoRotation rot, IntVector3 apCorrection)
	{
		NumConnectedAPs = 0;
		for (int i = 0; i < attachPoints.Length; i++)
		{
			ConnectedBlocksByAP[i] = null;
		}
		AttachingEvent.Send();
		cachedLocalPosition = trans.localPosition;
		cachedLocalRotation = rot;
		attachments.Sort(BlockManager.BlockAttachment.CompareBlockAttachments);
		if (attachPoints.Length > s_MyAPposCache.Length)
		{
			Array.Resize(ref s_MyAPposCache, attachPoints.Length);
		}
		for (int j = 0; j < attachPoints.Length; j++)
		{
			s_MyAPposCache[j] = (rot * attachPoints[j] + trans.localPosition).LocalToAP();
		}
		TankBlock tankBlock = null;
		for (int k = 0; k < attachments.Count; k++)
		{
			BlockManager.BlockAttachment blockAttachment = attachments[k];
			if (blockAttachment.other.IsNotNull() && (object)blockAttachment.other != tankBlock)
			{
				tankBlock = blockAttachment.other;
				d.Assert((object)tankBlock != this);
				if (tankBlock.attachPoints.Length > s_OtherAPposCache.Length)
				{
					Array.Resize(ref s_OtherAPposCache, tankBlock.attachPoints.Length);
				}
				for (int l = 0; l < tankBlock.attachPoints.Length; l++)
				{
					s_OtherAPposCache[l] = (tankBlock.cachedLocalRotation * tankBlock.attachPoints[l] + tankBlock.trans.localPosition).LocalToAP();
				}
			}
			IntVector3 intVector = blockAttachment.apPosLocal + apCorrection;
			for (int m = 0; m < attachPoints.Length; m++)
			{
				if (intVector == s_MyAPposCache[m])
				{
					d.Assert(ConnectedBlocksByAP[m] == null);
					ConnectedBlocksByAP[m] = tankBlock;
					NumConnectedAPs++;
					break;
				}
			}
			for (int n = 0; n < tankBlock.attachPoints.Length; n++)
			{
				if (intVector == s_OtherAPposCache[n])
				{
					if (!tankBlock.ConnectedBlocksByAP.Contains(this))
					{
						tankBlock.NeighbourAttachingEvent.Send(this);
					}
					tankBlock.ConnectedBlocksByAP[n] = this;
					tankBlock.NumConnectedAPs++;
					break;
				}
			}
		}
	}

	public void DetachRecursively(Action<TankBlock> preAction, Action<TankBlock> postAction)
	{
		d.Assert(!Recurse(preAction, postAction), "hit root while removing a block group that should be completely disconnected");
	}

	public void CleanupBlocksByAP(bool fromDetach = true)
	{
		d.AssertFormat(fromDetach || NumConnectedAPs == 0, this, "CleanupBlocksByAP called on {0} without detach param, while it still had {1} connected blocks!", this, NumConnectedAPs);
		Array.Clear(ConnectedBlocksByAP, 0, ConnectedBlocksByAP.Length);
		NumConnectedAPs = 0;
		if (lastTank != null)
		{
			DetachedEvent.Send();
			lastTank = null;
		}
	}

	public bool HasAP(Vector3 pos)
	{
		if (attachPoints.Contains(pos))
		{
			return true;
		}
		Vector3[] array = attachPoints;
		for (int i = 0; i < array.Length; i++)
		{
			if (Vector3.Distance(array[i], pos) < Mathf.Epsilon)
			{
				return true;
			}
		}
		return false;
	}

	public void AddAP(Vector3 ap)
	{
		if (!attachPoints.Contains(ap))
		{
			Vector3[] array = new Vector3[attachPoints.Length + 1];
			attachPoints.CopyTo(array, 0);
			array[attachPoints.Length] = ap;
			attachPoints = array;
		}
	}

	public void RemoveAP(Vector3 ap)
	{
		List<Vector3> list = new List<Vector3>(attachPoints.Length);
		for (int num = attachPoints.Length - 1; num >= 0; num--)
		{
			if (!(Vector3.Distance(ap, attachPoints[num]) < Mathf.Epsilon))
			{
				list.Add(attachPoints[num]);
			}
		}
		attachPoints = list.ToArray();
	}

	public Vector3 FindAPInDirection(Vector3 filledCell, Vector3 dir)
	{
		Vector3 vector = filledCell + dir * 0.5f;
		if (attachPoints.Contains(vector))
		{
			return vector;
		}
		Vector3[] array = attachPoints;
		foreach (Vector3 vector2 in array)
		{
			if (Vector3.Distance(vector2, vector) < Mathf.Epsilon)
			{
				return vector2;
			}
		}
		return Vector3.zero;
	}

	public void CleanupDeadAPs()
	{
		List<Vector3> list = new List<Vector3>(attachPoints.Length);
		for (int num = attachPoints.Length - 1; num >= 0; num--)
		{
			Vector3 vector = attachPoints[num];
			Vector3 vector2 = new Vector3(vector.x - Mathf.Floor(vector.x), vector.y - Mathf.Floor(vector.y), vector.z - Mathf.Floor(vector.z));
			IntVector3 value = new IntVector3(vector + vector2);
			IntVector3 value2 = new IntVector3(vector - vector2);
			if (filledCells.Contains(value) != filledCells.Contains(value2))
			{
				list.Add(vector);
			}
		}
		attachPoints = list.ToArray();
	}

	public void RemoveLinks()
	{
		TankBlock[] connectedBlocksByAP = ConnectedBlocksByAP;
		foreach (TankBlock tankBlock in connectedBlocksByAP)
		{
			if (tankBlock == null)
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < tankBlock.ConnectedBlocksByAP.Length; j++)
			{
				if ((object)tankBlock.ConnectedBlocksByAP[j] == this)
				{
					tankBlock.ConnectedBlocksByAP[j] = null;
					tankBlock.NumConnectedAPs--;
					flag = true;
				}
			}
			if (flag)
			{
				tankBlock.NeighbourDetachedEvent.Send(this);
			}
		}
	}

	public void TransferRoot(Tank xferRootTank)
	{
		TankBlock rootBlock = null;
		int num = 0;
		bool flag = xferRootTank.IsNotNull() && xferRootTank.blockman.GetRootBlock().IsNull();
		TankBlock[] connectedBlocksByAP = ConnectedBlocksByAP;
		foreach (TankBlock tankBlock in connectedBlocksByAP)
		{
			if (!(tankBlock == null) && flag && tankBlock.NumConnectedAPs > num)
			{
				rootBlock = tankBlock;
				num = tankBlock.NumConnectedAPs;
			}
		}
		if (flag)
		{
			xferRootTank.blockman.SetRootBlock(rootBlock);
		}
	}

	public void MoveLocalPositionWhileAttached(Vector3 delta)
	{
		d.Assert(tank.IsNotNull());
		trans.localPosition += delta;
		cachedLocalPosition += delta;
	}

	public bool CanReachRoot()
	{
		return Recurse(null, null);
	}

	private bool Recurse(Action<TankBlock> preAction, Action<TankBlock> postAction, bool first = true)
	{
		if (tank.IsNull())
		{
			d.LogError($"block {base.name} recurse has null tank: attempting to ignore");
			return false;
		}
		if (tank.blockman.IsRootBlock(this))
		{
			return true;
		}
		if (first)
		{
			s_NextRecurseSearchIndex++;
		}
		m_RecurseIndex = s_NextRecurseSearchIndex;
		preAction?.Invoke(this);
		TankBlock[] connectedBlocksByAP = ConnectedBlocksByAP;
		foreach (TankBlock tankBlock in connectedBlocksByAP)
		{
			if (tankBlock.IsNotNull() && tankBlock.m_RecurseIndex != s_NextRecurseSearchIndex && tankBlock.Recurse(preAction, postAction, first: false))
			{
				return true;
			}
		}
		postAction?.Invoke(this);
		return false;
	}

	private void RecalculateCentreOfGravity()
	{
		m_CentreOfGravity = Vector3.zero;
		float num = m_DefaultMass / (float)filledCells.Length;
		float num2 = 0f;
		for (int i = 0; i < filledCells.Length; i++)
		{
			float num3 = num * FilledCellsGravityScaleFactors[i];
			m_CentreOfGravity += (Vector3)filledCells[i] * num3;
			num2 += num3;
		}
		if (num2 == 0f)
		{
			m_CentreOfGravity = m_CentreOfMass;
		}
		else
		{
			m_CentreOfGravity /= num2;
		}
		m_NeedsCentreOfGravityUpdate = false;
	}

	private void RecalculateAverageGravityScaleFactor()
	{
		m_AverageGravityScaleFactor = 0f;
		for (int i = 0; i < FilledCellsGravityScaleFactors.Length; i++)
		{
			m_AverageGravityScaleFactor += FilledCellsGravityScaleFactors[i];
		}
		m_AverageGravityScaleFactor /= FilledCellsGravityScaleFactors.Length;
		m_AverageGravityScaleDirty = false;
	}

	private void CalculateDefaultPhysicsConstants()
	{
		m_DefaultInertiaTensor = Vector3.zero;
		m_CentreOfMass = Vector3.zero;
		m_CentreOfGravity = Vector3.zero;
		float num = m_DefaultMass / (float)filledCells.Length;
		float num2 = 0f;
		for (int i = 0; i < filledCells.Length; i++)
		{
			IntVector3 intVector = filledCells[i];
			float num3 = num * FilledCellsGravityScaleFactors[i];
			m_CentreOfMass += (Vector3)intVector * num;
			m_CentreOfGravity += (Vector3)intVector * num3;
			num2 += num3;
			Vector3 vector = num * new Vector3(1f / 6f, 1f / 6f, 1f / 6f);
			m_DefaultInertiaTensor += vector + num * new Vector3(intVector.y * intVector.y + intVector.z * intVector.z, intVector.z * intVector.z + intVector.x * intVector.x, intVector.x * intVector.x + intVector.y * intVector.y);
		}
		m_CentreOfMass /= m_DefaultMass;
		if (num2 == 0f)
		{
			m_CentreOfGravity = m_CentreOfMass;
		}
		else
		{
			m_CentreOfGravity /= num2;
		}
		m_NeedsCentreOfGravityUpdate = false;
		m_DefaultInertiaTensor -= m_DefaultMass * new Vector3(m_CentreOfMass.y * m_CentreOfMass.y + m_CentreOfMass.z * m_CentreOfMass.z, m_CentreOfMass.z * m_CentreOfMass.z + m_CentreOfMass.x * m_CentreOfMass.x, m_CentreOfMass.x * m_CentreOfMass.x + m_CentreOfMass.y * m_CentreOfMass.y);
		Transform transform = base.transform.Find("CentreOfMass");
		if (transform.IsNull())
		{
			transform = base.transform.Find("_centreOfMass");
		}
		if (transform.IsNull())
		{
			transform = base.transform.Find("_centerOfMass");
		}
		if (transform.IsNotNull())
		{
			m_CentreOfMass = transform.localPosition;
			m_DefaultInertiaTensor += m_DefaultMass * new Vector3(m_CentreOfMass.y * m_CentreOfMass.y + m_CentreOfMass.z * m_CentreOfMass.z, m_CentreOfMass.z * m_CentreOfMass.z + m_CentreOfMass.x * m_CentreOfMass.x, m_CentreOfMass.x * m_CentreOfMass.x + m_CentreOfMass.y * m_CentreOfMass.y);
		}
	}

	private void InitAPFilledCells()
	{
		m_APFilledCells = new int[attachPoints.Length];
		for (int i = 0; i < attachPoints.Length; i++)
		{
			IntVector3 intVector = attachPoints[i] * 2f;
			IntVector3 intVector2 = intVector.PadHalfDown();
			IntVector3 intVector3 = intVector2 + intVector.AxisUnit();
			int j;
			for (j = 0; j < filledCells.Length && !(filledCells[j] == intVector2) && !(filledCells[j] == intVector3); j++)
			{
			}
			m_APFilledCells[i] = j;
			if (j == filledCells.Length)
			{
				m_APFilledCells[i] = 0;
				d.LogError("Block " + base.name + " with orphan AP at " + attachPoints[i]);
			}
		}
	}

	public IntVector3 GetFilledCellForAPIndex(int index)
	{
		return filledCells[m_APFilledCells[index]];
	}

	public void ForeachConnectedBlock(Action<TankBlock> action)
	{
		d.AssertFormat(s_VisitedBlocks.Count == 0, "");
		for (int i = 0; i < ConnectedBlocksByAP.Length; i++)
		{
			TankBlock tankBlock = ConnectedBlocksByAP[i];
			if (tankBlock.IsNotNull() && !s_VisitedBlocks.Contains(tankBlock))
			{
				action(tankBlock);
				s_VisitedBlocks.Add(tankBlock);
			}
		}
		s_VisitedBlocks.Clear();
	}

	public void SetAdditionalMassCategory(MassCategoryType massCategory, double additionalMass)
	{
		if (m_AdditionalMassCategories == null)
		{
			m_AdditionalMassCategories = new Dictionary<MassCategoryType, double>();
		}
		if (!m_AdditionalMassCategories.ContainsKey(massCategory))
		{
			m_AdditionalMassCategories.Add(massCategory, 0.0);
		}
		m_AdditionalMassCategories[massCategory] = additionalMass;
		UpdateMass();
	}

	public double GetAdditionalMassCategoryValue(MassCategoryType massCategory)
	{
		if (!m_AdditionalMassCategories.ContainsKey(massCategory))
		{
			return 0.0;
		}
		return m_AdditionalMassCategories[massCategory];
	}

	private void UpdateMass(bool resetPhysics = true)
	{
		double allMass = AllMass;
		Vector3 vector = m_DefaultInertiaTensor * (float)(allMass / (double)m_DefaultMass);
		if (tank.IsNotNull())
		{
			tank.BlockMassChanged(this, (float)allMass, vector);
		}
		CurrentInertiaTensor = vector;
		CurrentMass = (float)allMass;
		if (rbody != null)
		{
			rbody.mass = CurrentMass;
			rbody.inertiaTensor = CurrentInertiaTensor;
		}
		if ((bool)tank && resetPhysics)
		{
			tank.ResetPhysics(SendEventUpdate: true);
		}
	}

	private bool IsExactMultiple(float v, float f)
	{
		float num = v / f;
		return (float)(int)num == num;
	}

	private void VerifyShape(Vector3[] vecs, string label)
	{
		for (int i = 0; i < vecs.Length; i++)
		{
			Vector3 vector = vecs[i];
			if (!IsExactMultiple(vector.x, 0.5f) || !IsExactMultiple(vector.y, 0.5f) || !IsExactMultiple(vector.z, 0.5f))
			{
				d.LogError($"block {base.name} {label} {vector} invalid");
			}
		}
	}

	public void SetGluedToMagnet(bool glued, ModuleItemHolderMagnet magnet, Transform gluedContainer)
	{
		GluedToMagnet = (glued ? magnet : null);
		if (glued)
		{
			d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer());
			visible.trans.parent = gluedContainer;
			ClearRigidBody(immediate: false);
			visible.WorldSpaceComponent.SetEnabled(enabled: false);
			return;
		}
		visible.trans.parent = null;
		if (rbody.IsNull())
		{
			InitRigidbody();
		}
		visible.WorldSpaceComponent.SetEnabled(enabled: true);
	}

	public bool TryQueryActingRigidbody(out Rigidbody actingBody)
	{
		actingBody = (IsAttached ? tank.rbody : rbody);
		if (actingBody.IsNull() && GluedToMagnet.IsNotNull())
		{
			GluedToMagnet.block.TryQueryActingRigidbody(out actingBody);
		}
		return actingBody.IsNotNull();
	}

	public void InitRigidbody()
	{
		if (!m_HasRBodyInPrefab)
		{
			Rigidbody rigidbody = null;
			if (Debug.isDebugBuild)
			{
				rigidbody = base.gameObject.GetComponent<Rigidbody>();
				if (rigidbody == null)
				{
					rigidbody = null;
				}
				if (rigidbody.IsNotNull())
				{
					d.LogError("InitRigidBody on Block " + base.name + " called but already has Rigidbody!  NetId=" + (netBlock.IsNotNull() ? netBlock.netId.ToString() : "N/A") + " VisibleId=" + visible.ID);
				}
			}
			Rigidbody rigidbody2 = (rigidbody.IsNotNull() ? rigidbody : base.gameObject.AddComponent<Rigidbody>());
			if (rigidbody2.IsNull())
			{
				d.LogError("InitRigidbody on Block " + base.name + " failed to add new body: Rigidbody already exists?");
				rigidbody2 = GetComponent<Rigidbody>();
			}
			if (rigidbody2 == null)
			{
				rigidbody2 = null;
			}
			rbody = rigidbody2;
		}
		UpdateMass(resetPhysics: false);
		rbody.inertiaTensor = CurrentInertiaTensor;
		rbody.useGravity = true;
		rbody.drag = Globals.inst.airSpeedDrag;
	}

	public void ClearRigidBody(bool immediate)
	{
		if (rbody.IsNotNull())
		{
			if (immediate)
			{
				UnityEngine.Object.DestroyImmediate(rbody);
			}
			else
			{
				UnityEngine.Object.Destroy(rbody);
			}
			rbody = null;
		}
	}

	public void OnAttach(Tank t)
	{
		if (t.netTech.IsNotNull())
		{
			d.Assert(HasValidBlockPoolID(), "TankBlock.OnAttach has unsequenced BlockPoolID=" + blockPoolID + " " + BlockType.ToString());
		}
		tank = t;
		lastTank = t;
		if (ManSpawn.IsPlayerTeam(tank.Team) && !ManSpawn.IsPlayerTeam(LastTechTeam))
		{
			Singleton.Manager<ManStats>.inst.BlockScavenged(BlockType);
		}
		visible.SetHolder(null);
		d.Assert(!m_HasRBodyInPrefab, "trying to attach a block with a rigid body in its prefab (CALL A CODER)");
		if (!m_HasRBodyInPrefab)
		{
			ClearRigidBody(immediate: true);
		}
		visible.StopManagingVisible();
		if (IsDeprecated)
		{
			m_DeprecationWarning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleDepBlock, LocalisationEnums.Warnings.warningMsgDepBlock, 8);
		}
		if (visible.ColliderSwapper.IsNotNull())
		{
			visible.ColliderSwapper.UseAttached(attached: true);
		}
		AttachedEvent.Send();
		ForeachConnectedBlock(delegate(TankBlock r)
		{
			r.NeighbourAttachedEvent.Send(this);
		});
	}

	public void CheckLooseDestruction(Tank prevTech)
	{
		if ((!visible.damageable.IsNull() && visible.damageable.Invulnerable) || !prevTech.IsNotNull())
		{
			return;
		}
		bool flag = false;
		float fuseTime = 1f;
		if (prevTech.ShouldExplodeDetachingBlocks)
		{
			flag = true;
			fuseTime = prevTech.ExplodeDetachingBlocksDelay;
		}
		else if ((object)prevTech != Singleton.playerTank && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>())
		{
			BlockTypes itemType = (BlockTypes)visible.ItemType;
			flag = !Singleton.Manager<ManSpawn>.inst.IsTankBlockLoaded(itemType);
			if (!flag && prevTech.IsEnemy(Singleton.Manager<ManPlayer>.inst.PlayerTeam))
			{
				float num = ((Singleton.Manager<ManLicenses>.inst.GetBlockState(itemType) != ManLicenses.BlockState.Discovered) ? Globals.inst.m_NewBlockSurvivalBuff : 0f);
				float num2 = (Singleton.Manager<ManPop>.inst.SaveEnemyDisconnectingBlocks ? 1f : (Globals.inst.m_BlockSurvivalChance + num));
				flag = UnityEngine.Random.Range(0f, 1f) > num2;
			}
		}
		if (m_PreventSelfDestructOnFirstDetach && prevTech.IsEnemy(Singleton.Manager<ManPlayer>.inst.PlayerTeam) && GetComponent<Damageable>().Health >= 0f)
		{
			flag = false;
		}
		if (flag)
		{
			damage.SelfDestruct(fuseTime);
		}
		m_firstDetachment = false;
	}

	public void OnDetach(Tank t, bool initRigidbody, bool resumeTileManagement)
	{
		if (initRigidbody)
		{
			CheckLooseDestruction(t);
		}
		d.Assert(!m_EnableTutorialCollision, "tutorial collision hack still enabled on block");
		if (visible.ColliderSwapper.IsNotNull())
		{
			visible.ColliderSwapper.UseAttached(attached: false);
		}
		DetachingEvent.Send();
		ForeachConnectedBlock(delegate(TankBlock r)
		{
			r.NeighbourDetachingEvent.Send(this);
		});
		if (initRigidbody)
		{
			InitRigidbody();
		}
		LastTechTeam = tank.Team;
		tank = null;
		m_DeprecationWarning.Remove();
		if (resumeTileManagement)
		{
			visible.SetManagedByTile(managed: true);
			visible.KeepAwake();
		}
		ResetGravityAdjustment();
		SetAntiGravVisualsActive(antiGravActive: false);
		SetCloggedVisualsActive(cloggedActive: false);
	}

	public void Separate(bool manualRemove = false, bool allowHeadlessTech = false, TechSplitNamer techSplitNamer = null)
	{
		d.Assert(tank.IsNotNull(), "trying to separate non-attached block: " + base.name);
		BlockManager blockman = tank.blockman;
		bool playerInitiatedRemoveBlock = blockman.PlayerInitiatedRemoveBlock;
		blockman.PlayerInitiatedRemoveBlock = manualRemove;
		Singleton.Manager<ManLooseBlocks>.inst.UnnetworkedSeparateCall(this, allowHeadlessTech, propagate: true);
		blockman.PlayerInitiatedRemoveBlock = playerInitiatedRemoveBlock;
	}

	private bool OnRemovedFromGame()
	{
		d.Assert(netBlock.IsNull(), "NetBlock still linked");
		d.Assert(Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(blockPoolID).IsNull(), "Block still in loose blocks lookup table");
		return true;
	}

	private void SetLockTimer(BlockLockTimerTypes type, float additionalTime = 1f)
	{
		m_LockTimers[(int)type] = Time.time + additionalTime;
	}

	private bool IsLocked(BlockLockTimerTypes type)
	{
		return m_LockTimers[(int)type] > Time.time;
	}

	public void LockBlockAnchor()
	{
		SetLockTimer(BlockLockTimerTypes.AnchorFreely);
	}

	public void LockBlockAttach()
	{
		SetLockTimer(BlockLockTimerTypes.Attachable);
	}

	public void UnlockBlockAttach()
	{
		SetLockTimer(BlockLockTimerTypes.Attachable, 0f);
	}

	public void LockTutorialBlockAttach()
	{
		SetLockTimer(BlockLockTimerTypes.AttachableOutsideTutorial);
	}

	public FreeAPIterator IterateFreeAttachPointsWorld()
	{
		return new FreeAPIterator(this);
	}

	public void SetAPIgnoreFilter(Func<int, bool> filterCallback)
	{
		APIgnoreFilter = filterCallback;
	}

	public void InitNew()
	{
		visible.damageable.InitHealth(visible.damageable.MaxHealth);
	}

	public void RestoreSaved(float health, int lastTechTeam)
	{
		visible.damageable.InitHealth(health);
		LastTechTeam = lastTechTeam;
	}

	public void Serialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		d.Assert(tank.IsNotNull(), "Attempting to serialize a non-attached block");
		serializeEvent.Send(saving, blockSpec);
		PostSerializeEvent.Send(saving);
	}

	public void SerializeToText(bool saving, TankPreset.BlockSpec context, bool onTech = true)
	{
		d.Assert(!onTech || tank.IsNotNull(), "Invalidly attempting to serialize a non-attached block");
		serializeTextEvent.Send(saving, context, onTech);
		PostSerializeTextEvent.Send(saving);
	}

	public static bool BlockNameToType(string name, out BlockTypes blockType)
	{
		blockType = BlockTypes.GSOAIController_111;
		string input = Regex.Replace(name, "[(]", "_");
		input = Regex.Replace(input, "[)]", string.Empty);
		if (Singleton.Manager<ManSpawn>.inst.ReplacePartNames.ContainsKey(input))
		{
			input = Singleton.Manager<ManSpawn>.inst.ReplacePartNames[input];
		}
		string[] names = EnumNamesIterator<BlockTypes>.Names;
		for (int i = 0; i < names.Length; i++)
		{
			if (input.ToLower() == names[i].ToLower())
			{
				blockType = (BlockTypes)i;
				return true;
			}
		}
		return false;
	}

	public bool HasAccessibleContextMenu(bool includeRadial = true, bool includeInteract = true)
	{
		bool result = false;
		if (tank != null && IsInteractible)
		{
			bool num = this.openMenuEventConsumer != null;
			ManPointer.OpenMenuEventConsumer openMenuEventConsumer = this.openMenuEventConsumer ?? tank.TechOpenMenuEventConsumer;
			bool flag = num || (HasContextMenu && tank.control.CanOpenContextMenuForBlock(this));
			bool flag2 = includeRadial && openMenuEventConsumer.CanOpenMenu(isRadial: true);
			bool flag3 = includeInteract && openMenuEventConsumer.CanOpenMenu(isRadial: false);
			result = openMenuEventConsumer != null && flag && (flag2 || flag3);
		}
		return result;
	}

	private void SetPositionCOM(Vector3 posWorld)
	{
		trans.position = posWorld - trans.TransformVector(m_CentreOfMass);
	}

	public void SetCustomMaterialOverride(ManTechMaterialSwap.MatType customMatType)
	{
		m_MaterialSwapper.SetCustomMaterialOverride(customMatType);
	}

	public ManTechMaterialSwap.MatType GetMaterialType()
	{
		return m_MaterialSwapper.GetCurrentMatType();
	}

	public Material GetCurrentMaterial()
	{
		return m_MaterialSwapper.GetCurrentMaterial();
	}

	public bool IsCustomMaterialOverride()
	{
		return m_MaterialSwapper.IsCustomMaterialOverride();
	}

	public void RevertCustomMaterialOverride()
	{
		m_MaterialSwapper.RevertCustomMaterialOverride();
	}

	public void ClearVariableColours(IEnumerable<Renderer> renderers)
	{
		m_MaterialSwapper.ClearVariableColours(renderers);
	}

	public void RegisterVariableColours(IEnumerable<Renderer> renderers, Color color, Color emissionColor)
	{
		m_MaterialSwapper.RegisterVariableColours(renderers, color, emissionColor);
	}

	public void SetNightTimeVisualsActive(bool nightEffectsActive)
	{
		m_MaterialSwapper.SwapMaterialTime(nightEffectsActive);
	}

	public void SetAntiGravVisualsActive(bool antiGravActive)
	{
		m_MaterialSwapper.SwapMaterialAntiGrav(antiGravActive);
	}

	public void SetCloggedVisualsActive(bool cloggedActive)
	{
		m_MaterialSwapper.SwapMaterialClogged(cloggedActive);
	}

	public void SetDamageVisualsActive(bool damageActive)
	{
		m_MaterialSwapper.SwapMaterialDamage(damageActive);
	}

	public void StartMaterialPulse(ManTechMaterialSwap.MaterialTypes matType, ManTechMaterialSwap.MaterialColour colour)
	{
		m_MaterialSwapper.StartMaterialPulse(matType, colour);
	}

	public bool HasMaterialPulse()
	{
		return m_MaterialSwapper.HasMaterialPulse();
	}

	private void KickTowardsPlayer(Vector3 relativeVelocity)
	{
		d.Assert(rbody);
		Vector3 vector = (Singleton.playerPos - centreOfMassWorld).SetY(0f);
		Vector3 normalized = vector.normalized;
		Vector3 vector2 = normalized * (rbody.velocity.SetY(0f).Dot(normalized) - Globals.inst.m_TutorialBlockNudgeVelocityBias);
		Vector3 vector3 = Globals.inst.m_TutorialBlockNudgeDistanceWeight * (1f - 1f / (vector.magnitude * 0.5f + 1f)) * normalized - vector2 * Globals.inst.m_TutorialBlockNudgeVelocityWeight;
		rbody.AddForce(vector3 * relativeVelocity.y, ForceMode.Impulse);
	}

	public void CollideTerrainOnly(bool makeTerrainOnly)
	{
		if (makeTerrainOnly == m_IsTerrainOnly)
		{
			return;
		}
		Collider[] componentsInChildren = GetComponentsInChildren<Collider>(includeInactive: true);
		foreach (Collider collider in componentsInChildren)
		{
			if (collider.gameObject.layer == (int)(makeTerrainOnly ? Globals.inst.layerTank : Globals.inst.layerTerrainOnly))
			{
				collider.gameObject.layer = (makeTerrainOnly ? Globals.inst.layerTerrainOnly : Globals.inst.layerTank);
			}
		}
		m_IsTerrainOnly = makeTerrainOnly;
	}

	public void EnableTutorialCollision(bool enable, float time)
	{
		m_EnableTutorialCollision = enable;
		Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + time, delegate
		{
			m_EnableTutorialCollision = false;
		});
	}

	public void IgnoreCollision(Collider otherCollider, bool ignore)
	{
		if (visible.ColliderSwapper.IsNotNull())
		{
			ColliderSwapper.ColliderSwapperEntry[] allColliders = visible.ColliderSwapper.AllColliders;
			for (int i = 0; i < allColliders.Length; i++)
			{
				ColliderSwapper.ColliderSwapperEntry colliderSwapperEntry = allColliders[i];
				Physics.IgnoreCollision(otherCollider, colliderSwapperEntry.collider, ignore);
			}
		}
		else
		{
			Collider[] componentsInChildren = visible.GetComponentsInChildren<Collider>(includeInactive: false);
			foreach (Collider collider in componentsInChildren)
			{
				Physics.IgnoreCollision(otherCollider, collider, ignore);
			}
		}
	}

	public void GetLocalAPsForAttachedBlock(TankBlock attachedBlock, List<Vector3> outResults)
	{
		d.AssertFormat(attachedBlock != null && attachedBlock != this, this, "Attempted to get AP positions for a neighbour block ({0}) of ({1}) that does not exist or is itself, this should not happen.", (attachedBlock == null) ? "null" : attachedBlock.name, base.name);
		outResults.Clear();
		for (int i = 0; i < ConnectedBlocksByAP.Length; i++)
		{
			if ((object)ConnectedBlocksByAP[i] == attachedBlock)
			{
				outResults.Add(attachPoints[i]);
			}
		}
		d.AssertFormat(outResults.Count > 0, this, "Attempted to find attached block APs but the given block ({0}) is not attached to the reference block ({1})! This check should only be performed on attached blocks", attachedBlock.name, base.name);
	}

	public void OnTankMouseDownEvent()
	{
		MouseDownEvent.Send(this, 0);
	}

	private void RemoveBlockFromTank()
	{
		if (tank.IsNotNull())
		{
			Separate();
		}
	}

	public T LookupComponent<T>() where T : Component
	{
		int index = s_ComponentIndexLookup.GetIndex(typeof(T));
		if (index >= m_IndexedComponents.Length)
		{
			return null;
		}
		return m_IndexedComponents[index] as T;
	}

	public static int LookupComponentIndex<T>() where T : Component
	{
		return s_ComponentIndexLookup.GetIndex(typeof(T));
	}

	public Component LookupComponentByIndex(int componentIndex)
	{
		if (componentIndex >= m_IndexedComponents.Length)
		{
			return null;
		}
		return m_IndexedComponents[componentIndex];
	}

	public void NotifyDrag(ManPointer.DragAction dragAction, Vector3 screenPos)
	{
		DragEvent.Send(dragAction, screenPos);
	}

	private bool IsInteger(float f)
	{
		return (float)(int)f == f;
	}

	public void SubscribeTriggerStay(Action<TankBlock, Collider> handler)
	{
		if (!m_HasTriggerCatcher)
		{
			TriggerCatcher.Subscribe(base.gameObject, TriggerCatcher.Interaction.Stay, OnTriggerStayEvent);
			m_HasTriggerCatcher = true;
		}
		m_TriggerStayEvent.Subscribe(handler);
	}

	public void UnsubscribeTriggerStay(Action<TankBlock, Collider> handler)
	{
		m_TriggerStayEvent.Unsubscribe(handler);
		if (m_TriggerStayEvent.GetSubscriberCount() == 0 && m_HasTriggerCatcher)
		{
			TriggerCatcher.Unsubscribe(base.gameObject, TriggerCatcher.Interaction.Stay, OnTriggerStayEvent);
			m_HasTriggerCatcher = false;
		}
	}

	public void EnsureNoTriggerStaySubscribers()
	{
		m_TriggerStayEvent.EnsureNoSubscribers();
	}

	public void OnMeshRenderersChanged()
	{
		m_MaterialSwapper.RefreshRenderers();
	}

	private void OnRelayCollisionEnter(Collision collision)
	{
		if (tank.IsNotNull())
		{
			tank.HandleCollision(collision, stay: false);
		}
	}

	private void OnRelayCollisionStay(Collision collision)
	{
		if (tank.IsNotNull())
		{
			tank.HandleCollision(collision, stay: true);
		}
	}

	private void OnTriggerStayEvent(TriggerCatcher.Interaction triggerType, Collider other)
	{
		m_TriggerStayEvent.Send(this, other);
	}

	private void PrePool()
	{
		s_ComponentIndexLookup.BuildLookup(this);
		d.AssertFormat(GetComponents<ManPointer.OpenMenuEventConsumer>().Length < 2, "Too many menu-open-event consumers on {0}! Highlander style there can only be one!", base.gameObject.name);
		if (filledCellFlags == null)
		{
			filledCellFlags = new byte[filledCells.Length];
		}
		else if (filledCellFlags.Length != filledCells.Length)
		{
			Array.Resize(ref filledCellFlags, filledCells.Length);
		}
		if (FilledCellsGravityScaleFactors == null)
		{
			FilledCellsGravityScaleFactors = new float[filledCells.Length];
		}
		else if (FilledCellsGravityScaleFactors.Length != filledCells.Length)
		{
			Array.Resize(ref FilledCellsGravityScaleFactors, filledCells.Length);
		}
		m_BlockCellBounds = new Bounds(Vector3.zero, Vector3.zero);
		IntVector3[] array = filledCells;
		foreach (IntVector3 intVector in array)
		{
			m_BlockCellBounds.Encapsulate(intVector);
		}
		m_MaterialSwapper = base.gameObject.AddComponent<MaterialSwapper>();
		CalculateDefaultPhysicsConstants();
		Rigidbody[] componentsInChildren = GetComponentsInChildren<Rigidbody>(includeInactive: false);
		List<EventRelay> list = new List<EventRelay>();
		Rigidbody[] array2 = componentsInChildren;
		foreach (Rigidbody rigidbody in array2)
		{
			if ((object)rigidbody.gameObject != base.gameObject && !rigidbody.isKinematic)
			{
				list.Add(rigidbody.gameObject.AddComponent<EventRelay>());
			}
		}
		if (list.Count != 0)
		{
			m_CollisionRelays = list.ToArray();
		}
		InitAPFilledCells();
	}

	private void OnPool()
	{
		s_ComponentIndexLookup.BuildLookup(this);
		BlockUpdate = new MonoBehaviourEvent<MB_Update>(base.gameObject, forceUnique: true);
		BlockFixedUpdate = new MonoBehaviourEvent<MB_FixedUpdate>(base.gameObject, forceUnique: true);
		BlockLateUpdate = new MonoBehaviourEvent<MB_LateUpdate>(base.gameObject, forceUnique: true);
		trans = base.transform;
		visible = GetComponent<Visible>();
		damage = GetComponent<ModuleDamage>();
		weapon = GetComponent<ModuleWeapon>();
		wheelsModule = GetComponent<ModuleWheels>();
		energyModule = GetComponent<ModuleEnergy>();
		visible.MesheRenderersUpdatedEvent.Subscribe(OnMeshRenderersChanged);
		openMenuEventConsumer = GetComponents<ManPointer.OpenMenuEventConsumer>().FirstOrDefault();
		ConnectedBlocksByAP = new TankBlock[attachPoints.Length];
		FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)visible.ItemType);
		m_MaterialSwapper.SetupMaterial(GetComponent<Damageable>(), corporation);
		IsController = GetComponent<ModuleTechController>().IsNotNull();
		Anchor = GetComponent<ModuleAnchor>();
		m_DeprecationWarning = new WarningHolder(visible, WarningHolder.WarningType.LowPower);
		CurrentMass = m_DefaultMass;
		CircuitNode = GetComponent<ModuleCircuitNode>();
		if (m_CollisionRelays != null)
		{
			EventRelay[] collisionRelays = m_CollisionRelays;
			foreach (EventRelay obj in collisionRelays)
			{
				obj.CollisionEnterEvent.Subscribe(OnRelayCollisionEnter);
				obj.CollisonStayEvent.Subscribe(OnRelayCollisionStay);
			}
		}
		visible.RegisterRemovalCallback(OnRemovedFromGame);
		rbody = GetComponent<Rigidbody>();
		if (rbody == null)
		{
			rbody = null;
		}
		m_HasRBodyInPrefab = rbody.IsNotNull();
		m_BlockPoolID = 4294967294u;
		if (m_LastFilledCellsGravityScaleFactors == null)
		{
			m_LastFilledCellsGravityScaleFactors = new float[filledCells.Length];
		}
		else if (m_LastFilledCellsGravityScaleFactors.Length != filledCells.Length)
		{
			Array.Resize(ref m_LastFilledCellsGravityScaleFactors, filledCells.Length);
		}
		for (int j = 0; j < FilledCellsGravityScaleFactors.Length; j++)
		{
			FilledCellsGravityScaleFactors[j] = 1f;
			m_LastFilledCellsGravityScaleFactors[j] = 1f;
		}
		m_AverageGravityScaleFactor = 1f;
		m_AverageGravityScaleDirty = false;
		m_NetworkedModules = GetComponents<INetworkedModule>();
	}

	private void OnSpawn()
	{
		if (!Singleton.Manager<ManSpawn>.inst.IsTechSpawning && !Singleton.Manager<ManTechBuildingTutorial>.inst.IsSpawningGhostBlock)
		{
			InitRigidbody();
		}
		m_AdditionalMassCategories?.Clear();
		UpdateMass();
		m_EnableTutorialCollision = false;
		DamageInEffect = ManDamage.DamageInfo.CreateNull();
		cachedLocalPosition = Vector3.zero;
		m_BlockPoolID = 4294967293u;
		ResetGravityAdjustment();
		if (rbody.IsNotNull())
		{
			rbody.useGravity = true;
		}
	}

	private void OnRecycle()
	{
		SetSkinIndex(0);
		d.Assert(tank.IsNull(), "Block still attached to tank on recycle");
		RemoveBlockFromTank();
		m_MaterialSwapper.ResetMaterialToDefault();
		if (!m_HasRBodyInPrefab)
		{
			ClearRigidBody(immediate: false);
		}
		LastTechTeam = int.MaxValue;
		Singleton.Manager<ManLooseBlocks>.inst.OnTankBlockRecycle(this);
		m_BlockPoolID = 4294967292u;
		m_TriggerStayEvent.EnsureNoSubscribers();
		if (m_HasTriggerCatcher)
		{
			TriggerCatcher.Unsubscribe(base.gameObject, TriggerCatcher.Interaction.Stay, OnTriggerStayEvent);
			m_HasTriggerCatcher = false;
		}
		RemoveLinks();
		CleanupBlocksByAP(fromDetach: false);
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		Vector3 vector = new Vector3(0f, -20f, 0f);
		if (flags.Contains(6))
		{
			DebugGui.LabelWorld(AverageGravityScaleFactor.ToString("+"), Color.green, trans.TransformPoint(CentreOfGravity), DebugGui.BGMode.Shadowed);
			Vector3 vector2 = Singleton.camera.WorldToScreenPoint(trans.TransformPoint(CentreOfGravity)) + vector;
			if (vector2.z >= 0f)
			{
				DebugGui.LabelScreen(AverageGravityScaleFactor.ToString("0.00"), Color.green, vector2, DebugGui.BGMode.BoxedShadowed);
			}
		}
		if (flags.Contains(7))
		{
			DebugGui.LabelWorld(CurrentMass.ToString("+"), Color.red, trans.TransformPoint(CentreOfMass), DebugGui.BGMode.Shadowed);
			Vector3 vector3 = Singleton.camera.WorldToScreenPoint(trans.TransformPoint(CentreOfMass)) + vector;
			if (vector3.z >= 0f)
			{
				DebugGui.LabelScreen(CurrentMass.ToString("0.00"), Color.red, vector3, DebugGui.BGMode.BoxedShadowed);
			}
		}
		if (flags.Contains(8) && filledCells != null && FilledCellsGravityScaleFactors != null)
		{
			for (int i = 0; i < filledCells.Length; i++)
			{
				DebugGui.LabelWorld(FilledCellsGravityScaleFactors[i].ToString("+"), Color.green, GetFilledCellWorldPos(i), DebugGui.BGMode.Shadowed);
				Vector3 vector4 = Singleton.camera.WorldToScreenPoint(GetFilledCellWorldPos(i)) + vector;
				if (vector4.z >= 0f)
				{
					DebugGui.LabelScreen(FilledCellsGravityScaleFactors[i].ToString("0.00"), Color.green, vector4, DebugGui.BGMode.BoxedShadowed);
				}
			}
		}
		if (!flags.Contains(9) || filledCells == null || FilledCellsGravityScaleFactors == null)
		{
			return;
		}
		for (int j = 0; j < filledCells.Length; j++)
		{
			DebugGui.LabelWorld((m_DefaultMass / (float)filledCells.Length).ToString("+"), Color.red, GetFilledCellWorldPos(j), DebugGui.BGMode.Shadowed);
			Vector3 vector5 = Singleton.camera.WorldToScreenPoint(GetFilledCellWorldPos(j)) + vector;
			if (vector5.z >= 0f)
			{
				DebugGui.LabelScreen((m_DefaultMass / (float)filledCells.Length).ToString("0.00"), Color.red, vector5, DebugGui.BGMode.BoxedShadowed);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		visible.KeepAwake();
		if (m_EnableTutorialCollision)
		{
			if (tank.IsNull() && collision.contacts.Length != 0 && collision.contacts[0].otherCollider.IsTerrain())
			{
				KickTowardsPlayer(collision.relativeVelocity);
			}
			CollideTerrainOnly(makeTerrainOnly: false);
		}
		Singleton.Manager<ManSFX>.inst.PlayBlockImpactSFX(this, collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		visible.KeepAwake();
	}

	public void ResetGravityAdjustment()
	{
		bool flag = false;
		for (int i = 0; i < FilledCellsGravityScaleFactors.Length; i++)
		{
			if (m_LastFilledCellsGravityScaleFactors[i] != 1f)
			{
				FilledCellsGravityScaleFactors[i] = 1f;
				m_LastFilledCellsGravityScaleFactors[i] = 1f;
				flag = true;
			}
		}
		if (flag)
		{
			m_AverageGravityScaleDirty = true;
			m_NeedsCentreOfGravityUpdate = true;
			if (tank.IsNotNull())
			{
				tank.RequestCentreOfGravityUpdate();
				SetAntiGravVisualsActive(antiGravActive: false);
			}
		}
	}

	public void PrimeForGravityAdjustment()
	{
		int num = filledCells.Length;
		for (int i = 0; i < num; i++)
		{
			FilledCellsGravityScaleFactors[i] = 1f;
		}
	}

	public float AdjustGravity(GravityManipulationZone zone)
	{
		float num = 0f;
		int num2 = filledCells.Length;
		float manipulationAmount = zone.m_ManipulationAmount;
		float num3 = zone.m_Radius * zone.m_Radius;
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector = (zone.m_PositionIsLocal ? (cachedLocalPosition + cachedLocalRotation * filledCells[i]) : GetFilledCellWorldPos(i));
			if ((zone.m_Position - vector).sqrMagnitude < num3)
			{
				float num4 = FilledCellsGravityScaleFactors[i];
				float num5 = Mathf.Max(num4 + manipulationAmount, 0f);
				FilledCellsGravityScaleFactors[i] = num5;
				num += num4 - num5;
			}
		}
		return num;
	}

	public void FinaliseGravityAdjustment()
	{
		int num = m_LastFilledCellsGravityScaleFactors.Length;
		for (int i = 0; i < num; i++)
		{
			float num2 = FilledCellsGravityScaleFactors[i];
			if (m_LastFilledCellsGravityScaleFactors[i] != num2)
			{
				m_AverageGravityScaleDirty = true;
			}
			m_LastFilledCellsGravityScaleFactors[i] = num2;
		}
		if (m_AverageGravityScaleDirty)
		{
			SetAntiGravVisualsActive(AverageGravityScaleFactor < 1f);
			m_NeedsCentreOfGravityUpdate = true;
			if (tank.IsNotNull())
			{
				tank.RequestCentreOfGravityUpdate();
			}
		}
	}

	public IGravityApplicationTarget GetGravityApplicationTarget()
	{
		if (tank.IsNotNull())
		{
			return tank;
		}
		return this;
	}

	public int GetFilledCellsInGravityZoneCount(GravityManipulationZone zone)
	{
		int num = 0;
		int num2 = filledCells.Length;
		for (int i = 0; i < num2; i++)
		{
			Vector3 filledCellWorldPos = GetFilledCellWorldPos(i);
			if ((zone.m_Position - filledCellWorldPos).sqrMagnitude < zone.m_Radius * zone.m_Radius)
			{
				num++;
			}
		}
		return num;
	}

	private Vector3 GetFilledCellWorldPos(int filledCell)
	{
		return trans.TransformPoint(filledCells[filledCell]);
	}

	public float GetGravityScale()
	{
		return AverageGravityScaleFactor;
	}

	public Rigidbody GetApplicationRigidbody()
	{
		return rbody;
	}

	public Vector3 GetWorldCentreOfGravity()
	{
		return trans.TransformPoint(CentreOfGravity);
	}

	public bool CanApplyGravity()
	{
		return true;
	}

	public void SetAdjustmentTouched(bool touched)
	{
		m_GravityAdjustmentTouched = touched;
	}

	public bool HasAdjustmentBeenTouched()
	{
		return m_GravityAdjustmentTouched;
	}

	public void SetApplicationTouched(bool touched)
	{
		m_GravityApplicationTouched = touched;
	}

	public bool HasApplicationBeenTouched()
	{
		return m_GravityApplicationTouched;
	}

	public byte GetSkinIndex()
	{
		return m_SkinIndex;
	}

	public void SetSkinByUniqueID(byte skinID, BlockTypes blockType)
	{
		SetSkinIndex(Singleton.Manager<ManCustomSkins>.inst.SkinIDToIndex(skinID, Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType)));
	}

	public void SetSkinByUniqueID(byte skinID)
	{
		SetSkinIndex(Singleton.Manager<ManCustomSkins>.inst.SkinIDToIndex(skinID, Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType)));
	}

	public void SetSkinByUniqueID(byte skinID, FactionSubTypes corp)
	{
		SetSkinIndex(Singleton.Manager<ManCustomSkins>.inst.SkinIDToIndex(skinID, corp));
	}

	public void SetSkinIndex(byte skinIndex)
	{
		if (skinIndex >= Singleton.Manager<ManCustomSkins>.inst.GetNumSkinsInCorp(Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType)))
		{
			d.AssertFormat(false, "Tried to set skin idx '{0}' for block '{1}' in TankBlock.SetSkinIndex to a skin greater than the num skins in that corp ({2})! Defaulting to skin index 0", skinIndex, BlockType, Singleton.Manager<ManCustomSkins>.inst.GetNumSkinsInCorp(Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType)));
			skinIndex = 0;
		}
		if (m_SkinIndex != skinIndex)
		{
			byte skinIndex2 = m_SkinIndex;
			m_SkinIndex = skinIndex;
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType);
			CorporationSkinUIInfo skinUIInfo = Singleton.Manager<ManCustomSkins>.inst.GetSkinUIInfo(corporation, skinIndex);
			m_MaterialSwapper.SetSkinIndex(skinIndex, skinUIInfo.m_AlwaysEmissive ? 1f : Singleton.Manager<ManTechMaterialSwap>.inst.GetMinEmissiveForCorporation(corporation));
			FactionSubTypes corporation2 = Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType);
			if (skinIndex2 != 0)
			{
				Singleton.Manager<ManCustomSkins>.inst.SwapSkinMeshes(this, corporation2, skinIndex2, swapToDefault: true);
			}
			Singleton.Manager<ManCustomSkins>.inst.SwapSkinMeshes(this, corporation2, skinIndex, swapToDefault: false);
		}
	}

	public void RegisterNetworkableProperty<TBlockMsg>(TTMsgType msgType, Action<MessageBase> localUpdatedCallback) where TBlockMsg : MessageBase, IBlockMessage, new()
	{
		if (m_MessageHandlers == null)
		{
			m_MessageHandlers = new Dictionary<short, Action<MessageBase>>();
		}
		m_MessageHandlers.Add((short)msgType, localUpdatedCallback);
		Singleton.Manager<ManLooseBlocks>.inst.RegisterBlockMessage<TBlockMsg>(msgType);
	}

	public void OnBlockMessageReceived(short msgType, MessageBase messageBase)
	{
		if (m_MessageHandlers != null && m_MessageHandlers.TryGetValue(msgType, out var value))
		{
			value(messageBase);
			return;
		}
		d.LogErrorFormat(this, "Failed to find callback for message type {0} on blockId {1}. It likely was never registered..?!", (TTMsgType)msgType, blockPoolID);
	}
}
