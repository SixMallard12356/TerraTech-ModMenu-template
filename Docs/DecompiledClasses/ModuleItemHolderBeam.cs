#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleItemHolder))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleItemHolderBeam : Module, INetworkedModule
{
	public enum ItemMovementType
	{
		Static,
		AccelerateTowardsFixedPositions,
		MoveWithAnimationDummies,
		Invisible
	}

	private struct StackData
	{
		public ModuleItemHolder.Stack stack;

		public float topItemHeight;

		public BeamRenderer beam;

		public ItemMovementType moveType;

		public bool beamEnabled;

		public int animDummyIndex;
	}

	[Serializable]
	private struct StackAnimDummySet
	{
		[SerializeField]
		public Transform[] m_ItemDummies;
	}

	private class BeamRenderer
	{
		private readonly struct QuadSection
		{
			public readonly Transform trans;

			public readonly Mesh mesh;

			public QuadSection(Transform _trans, Mesh _mesh)
			{
				trans = _trans;
				mesh = _mesh;
			}
		}

		private static Vector2[] s_UVs = new Vector2[4];

		private List<QuadSection> m_QuadSections = new List<QuadSection>();

		private int m_PointInd;

		private Vector3 m_LastPoint;

		private Transform m_ParentTransform;

		private Transform m_Prefab;

		private static Quaternion s_CorrectionRot = Quaternion.LookRotation(-Vector3.up, Vector3.forward);

		public BeamRenderer(Transform parentTransform, Transform beamQuadPrefabTransform)
		{
			m_ParentTransform = parentTransform;
			m_Prefab = beamQuadPrefabTransform;
			Clear();
		}

		public void Clear()
		{
			m_PointInd = 0;
		}

		public void AddPoint(Vector3 point)
		{
			if (m_PointInd > 0)
			{
				QuadSection quad;
				if (m_PointInd > m_QuadSections.Count)
				{
					Transform transform = m_Prefab.Spawn();
					transform.parent = m_ParentTransform;
					Mesh mesh = null;
					MeshFilter component = transform.GetComponent<MeshFilter>();
					if (component != null)
					{
						mesh = component.mesh;
					}
					quad = new QuadSection(transform, mesh);
					m_QuadSections.Add(quad);
				}
				else
				{
					quad = m_QuadSections[m_PointInd - 1];
				}
				OrientQuad(in quad, m_LastPoint, point);
			}
			m_PointInd++;
			m_LastPoint = point;
		}

		public void Commit()
		{
			int num = Mathf.Max(m_PointInd - 1, 0);
			while (num < m_QuadSections.Count)
			{
				int index = m_QuadSections.Count - 1;
				m_QuadSections[index].trans.Recycle();
				m_QuadSections.RemoveAt(index);
			}
		}

		private void OrientQuad(in QuadSection quad, Vector3 fromPos, Vector3 toPos)
		{
			Vector3 vector = toPos - fromPos;
			float magnitude = vector.magnitude;
			Vector3 forward = vector / Mathf.Max(magnitude, 0.01f);
			Vector3 normalized = (Singleton.cameraTrans.position - fromPos).normalized;
			Quaternion newWorldRot = Quaternion.LookRotation(forward, normalized) * s_CorrectionRot;
			quad.trans.SetLocalScaleIfChanged(new Vector3(1f, magnitude, 1f));
			Vector3 vector2 = vector * 0.5f;
			quad.trans.SetPositionAndRotationIfChanged(fromPos + vector2, newWorldRot);
			s_UVs[0] = new Vector2(0f, 0f);
			s_UVs[1] = new Vector2(1f, vector.y);
			s_UVs[2] = new Vector2(1f, 0f);
			s_UVs[3] = new Vector2(0f, vector.y);
			quad.mesh.uv = s_UVs;
		}
	}

	[SerializeField]
	[Range(0.01f, 10f)]
	private float m_BeamBaseHeight = 1f;

	[SerializeField]
	private float m_BeamColumnRadius = 1f;

	[SerializeField]
	private float m_BeamStrength = 250f;

	[SerializeField]
	private float m_HeightIncrementScale = 1f;

	[SerializeField]
	private GameObject m_BeamQuadPrefab;

	[SerializeField]
	private ParticleSystem m_WaitingParticlesPrefab;

	[SerializeField]
	private ParticleSystem m_HoldingParticlesPrefab;

	[SerializeField]
	private bool m_ShowParticlesWhenHeld = true;

	[SerializeField]
	private StackAnimDummySet[] m_StackDummySets;

	[SerializeField]
	private bool m_IsOmniDirectionalStack;

	[SerializeField]
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	private TechAudio.SFXType m_SFXGrabbed;

	[SerializeField]
	private bool m_UsePhysicsForHeldItems;

	private ModuleItemHolder m_Holder;

	private ModuleItemPickup m_Pickup;

	private StackData[] m_StackData;

	private float m_OverrideDropAfterMinTime = -1f;

	private float m_OverrideHeightCorrectionLiftFactor = -1f;

	private static List<StackData> s_StackDataArrayBuilder = new List<StackData>(10);

	private static List<Visible> s_ItemsToDrop = new List<Visible>(10);

	private bool m_ScaleChanged;

	private Dictionary<Visible, int> m_TakenHeartbeat = new Dictionary<Visible, int>();

	private HashSet<Visible> m_HeldPhysicsItems = new HashSet<Visible>();

	private const float k_ItemRadius = 0.5f;

	private const float m_SecondsToSpin = 5f;

	private const float kDropBelowEpsilon = 0.1f;

	private const float k_MaxAcceleration = 2000f;

	private NetworkedProperty<ConfigureItemStackMessage> m_NetworkedStackData;

	private static bool m_UsePhysicsForUnanchoredTechs = true;

	public int NumStacks => m_StackData.Length;

	public void ConfigureStack(int index, bool drawBeam, ItemMovementType moveType, bool netSend = true)
	{
		if (index < m_StackData.Length)
		{
			ref StackData reference = ref m_StackData[index];
			bool num = !drawBeam && reference.beamEnabled;
			reference.moveType = moveType;
			reference.beamEnabled = drawBeam;
			if (num)
			{
				reference.beam.Clear();
				reference.beam.Commit();
			}
			if (netSend && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				m_NetworkedStackData.Data._StackIndex = index;
				m_NetworkedStackData.Data._MoveType = (byte)reference.moveType;
				m_NetworkedStackData.Data._BeamEnabled = reference.beamEnabled;
				m_NetworkedStackData.Sync();
			}
		}
		else
		{
			d.LogError("ERROR ModuleItemHolderBeam has no such stack " + index);
		}
	}

	public void OverrideAnimDummies(int forStackIndex, int fromStackIndex)
	{
		if (forStackIndex < m_StackData.Length && fromStackIndex < m_StackData.Length)
		{
			m_StackData[forStackIndex].animDummyIndex = fromStackIndex;
		}
		else
		{
			d.LogError("ERROR ModuleItemHolderBeam has no such stack " + Math.Max(forStackIndex, fromStackIndex));
		}
	}

	public void OverrideDropAfterMinTime(float time)
	{
		m_OverrideDropAfterMinTime = time;
	}

	public void OverrideHeightCorrectionLiftFactor(float liftFactor)
	{
		m_OverrideHeightCorrectionLiftFactor = liftFactor;
	}

	private void UpdateItemMovement()
	{
		for (int i = 0; i < m_StackData.Length; i++)
		{
			ref StackData reference = ref m_StackData[i];
			Vector3 upDir = (m_IsOmniDirectionalStack ? Vector3.up : base.block.trans.TransformDirection(reference.stack.UpDir));
			float itemRadius = 0.5f * m_HeightIncrementScale;
			Vector3 vector = reference.stack.BasePosWorld();
			s_ItemsToDrop.Clear();
			int num = 0;
			ModuleItemHolder.Stack.ItemIterator enumerator = reference.stack.IterateItems().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Visible current = enumerator.Current;
				switch (reference.moveType)
				{
				case ItemMovementType.AccelerateTowardsFixedPositions:
				{
					if (!current.rbody.IsNotNull())
					{
						break;
					}
					ParticleSystem particleSystem = Singleton.Manager<ManVisible>.inst.GetParticles(current);
					ParticleSystem particleSystem2 = (particleSystem.IsNotNull() ? particleSystem.GetOriginalPrefab() : null);
					bool isPrePickup = current.IsPrePickup;
					ParticleSystem particleSystem3 = (isPrePickup ? m_WaitingParticlesPrefab : m_HoldingParticlesPrefab);
					if (m_TakenHeartbeat.TryGetValue(current, out var value))
					{
						if (!m_ShowParticlesWhenHeld && m_Holder.block.tank.Holders.HeartbeatCount > value)
						{
							Singleton.Manager<ManVisible>.inst.ClearParticles(current);
						}
						else if (particleSystem3 != particleSystem2)
						{
							if ((bool)particleSystem3)
							{
								particleSystem = Singleton.Manager<ManVisible>.inst.SpawnAttachParticles(current, particleSystem3);
								if (!isPrePickup)
								{
									TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, m_SFXGrabbed);
									base.block.tank.TechAudio.PlayOneshot(data);
								}
							}
							else
							{
								Singleton.Manager<ManVisible>.inst.ClearParticles(current);
							}
						}
					}
					if ((bool)particleSystem)
					{
						particleSystem.transform.LookAt(vector);
					}
					if (isPrePickup)
					{
						Vector3 centrePosition = current.centrePosition;
						if (current.rbody != null)
						{
							centrePosition += current.rbody.position - current.trans.position;
						}
						if ((centrePosition - vector).magnitude - m_Holder.HorizontalBoundsRadius > m_Pickup.PickupRange || Singleton.Manager<ManPointer>.inst.DraggingItem == current)
						{
							s_ItemsToDrop.Add(current);
						}
					}
					else if (UpdateFloat(current, in reference, upDir, itemRadius, num))
					{
						s_ItemsToDrop.Add(current);
					}
					if (m_ScaleChanged)
					{
						current.trans.SetLocalScaleIfChanged(new Vector3(1f, 1f, 1f));
						m_ScaleChanged = false;
					}
					break;
				}
				case ItemMovementType.MoveWithAnimationDummies:
					UpdateAnimDummyMovement(current, i, num);
					break;
				case ItemMovementType.Static:
					UpdateStaticMovement(current);
					break;
				case ItemMovementType.Invisible:
					UpdateInvisibleMovement(current);
					break;
				default:
					d.LogError("ERROR: unhandled beam movement type " + reference.moveType);
					break;
				}
				num++;
				_ = current.Radius;
			}
			for (int j = 0; j < s_ItemsToDrop.Count; j++)
			{
				Visible visible = s_ItemsToDrop[j];
				if ((bool)visible.rbody && visible.rbody.velocity.magnitude > 300f)
				{
					d.LogWarning($"Held item fixup: Dropping {visible.name} on holder {base.block.name} on {base.block.tank.name} with vel {visible.rbody.velocity}. Reseting velocity to {base.block.tank.rbody.velocity}");
					visible.rbody.velocity = base.block.tank.rbody.velocity;
				}
				visible.SetHolder(null);
			}
			float topItemHeight = 0f;
			if (num > 0)
			{
				num--;
				topItemHeight = GetItemHeightInStack(itemRadius, num) - m_BeamBaseHeight;
			}
			reference.topItemHeight = topItemHeight;
		}
	}

	private void UpdateAnimDummyMovement(Visible item, int stackIndex, int itemIndex)
	{
		if (MoveStackItemToAnimationDummy(item, stackIndex, itemIndex))
		{
			m_ScaleChanged = true;
		}
	}

	private void UpdateStaticMovement(Visible item)
	{
		if (m_ScaleChanged)
		{
			item.trans.SetLocalScaleIfChanged(new Vector3(1f, 1f, 1f));
			m_ScaleChanged = false;
		}
	}

	private void UpdateInvisibleMovement(Visible item)
	{
		item.trans.SetLocalScaleIfChanged(new Vector3(0.0001f, 0.0001f, 0.0001f));
		m_ScaleChanged = true;
	}

	private bool MoveStackItemToAnimationDummy(Visible item, int stackInd, int itemInd)
	{
		bool result = false;
		if (FindAnimationDummy(stackInd, itemInd, out var animDummy))
		{
			item.centrePosition = animDummy.position;
			item.trans.SetLocalScaleIfChanged(animDummy.localScale);
			result = true;
		}
		return result;
	}

	private bool FindAnimationDummy(int stackInd, int itemInd, out Transform animDummy)
	{
		StackAnimDummySet? stackAnimDummySet = null;
		int animDummyIndex = m_StackData[stackInd].animDummyIndex;
		if (m_StackDummySets.Length > animDummyIndex)
		{
			stackAnimDummySet = m_StackDummySets[animDummyIndex];
		}
		else if (m_StackDummySets.Length == 1)
		{
			stackAnimDummySet = m_StackDummySets[0];
		}
		if (stackAnimDummySet.HasValue && itemInd < stackAnimDummySet.Value.m_ItemDummies.Length)
		{
			animDummy = stackAnimDummySet.Value.m_ItemDummies[itemInd];
		}
		else
		{
			animDummy = null;
		}
		return animDummy != null;
	}

	private bool ReadyToDropItem(Visible item, float minTime)
	{
		float itemPickupTime = base.block.tank.Holders.GetItemPickupTime(item.ID);
		if (itemPickupTime == -1f)
		{
			return true;
		}
		return Time.time - itemPickupTime > minTime;
	}

	private void UpdateFloatPositions()
	{
		Vector3 upDir = (m_IsOmniDirectionalStack ? Vector3.up : base.block.trans.TransformDirection(m_Holder.UpDir));
		float itemRadius = 0.5f * m_HeightIncrementScale;
		s_ItemsToDrop.Clear();
		for (int i = 0; i < m_StackData.Length; i++)
		{
			SetPositionsInStack(i, upDir, itemRadius);
		}
		for (int j = 0; j < s_ItemsToDrop.Count; j++)
		{
			s_ItemsToDrop[j].SetHolder(null);
		}
	}

	private void SetPositionsInStack(int stackIndex, Vector3 upDir, float itemRadius)
	{
		ref StackData reference = ref m_StackData[stackIndex];
		Visible draggingItem = Singleton.Manager<ManPointer>.inst.DraggingItem;
		float num = 1f - (base.block.tank.Holders.NextHeartBeatTime - Time.time) / base.block.tank.Holders.CurrentHeartbeatInterval;
		float t = Maths.SinEaseInOut(num);
		float angle = Time.deltaTime * 72f;
		for (int i = 0; i < reference.stack.items.Count; i++)
		{
			Visible visible = reference.stack.items[i];
			if (!visible.rbody.IsNull())
			{
				continue;
			}
			switch (reference.moveType)
			{
			case ItemMovementType.AccelerateTowardsFixedPositions:
			{
				Vector3 posInStack = GetPosInStack(in reference, upDir, itemRadius, i);
				if ((object)visible == draggingItem)
				{
					visible.UsePrevHeldPos = false;
					Vector3 vector = visible.centrePosition - posInStack;
					float num2 = m_BeamColumnRadius * 2f;
					if (vector.sqrMagnitude > num2 * num2)
					{
						s_ItemsToDrop.Add(visible);
					}
					break;
				}
				Vector3 newWorldPos;
				if (visible.UsePrevHeldPos && ManNetwork.IsHost)
				{
					newWorldPos = Vector3.Lerp(visible.PrevHeldPos.ScenePosition, posInStack, t);
					if (num >= 0.9f)
					{
						visible.UsePrevHeldPos = false;
					}
				}
				else
				{
					newWorldPos = Vector3.Lerp(visible.centrePosition, posInStack, 0.1f);
				}
				if (visible.type == ObjectTypes.Block)
				{
					newWorldPos -= visible.trans.TransformVector(visible.block.CentreOfMass);
				}
				Quaternion newWorldRot = Quaternion.AngleAxis(angle, upDir) * visible.trans.rotation;
				visible.trans.SetPositionAndRotationIfChanged(newWorldPos, newWorldRot);
				break;
			}
			case ItemMovementType.MoveWithAnimationDummies:
				UpdateAnimDummyMovement(visible, stackIndex, i);
				break;
			case ItemMovementType.Static:
				UpdateStaticMovement(visible);
				break;
			case ItemMovementType.Invisible:
				UpdateInvisibleMovement(visible);
				break;
			default:
				d.LogError("ERROR: unhandled beam movement type " + reference.moveType);
				break;
			}
		}
	}

	private Vector3 GetPosInStack(in StackData stackData, Vector3 upDir, float itemRadius, int itemIndex)
	{
		Vector3 offset = m_Holder.UpDir * GetItemHeightInStack(itemRadius, itemIndex);
		return stackData.stack.BasePosWorldOffsetLocal(offset);
	}

	private float GetItemHeightInStack(float itemRadius, int itemIndex)
	{
		return (float)(itemIndex * 2 + 1) * itemRadius + m_BeamBaseHeight;
	}

	private bool UpdateFloat(Visible item, in StackData stackData, Vector3 upDir, float itemRadius, int itemIndex)
	{
		bool result = false;
		if (!item || !item.ColliderSwapper || !item.rbody)
		{
			DebugUtil.AssertRelease(item, base.name + " UpdateFloat() null item");
			DebugUtil.AssertRelease(!item || (bool)item.ColliderSwapper, base.name + " UpdateFloat() null ColliderSwapper: " + item.name);
			DebugUtil.AssertRelease(!item || (bool)item.rbody, base.name + " UpdateFloat() null rbody: " + item.name);
			return result;
		}
		ref Globals.HoldBeamFloatParams holdBeamFloatParams = ref Globals.inst.holdBeamFloatParams;
		Vector3 vector = stackData.stack.BasePosWorldOffsetLocal(stackData.stack.UpDir * m_BeamBaseHeight);
		Vector3 vector2 = item.rbody.worldCenterOfMass - vector;
		float num = vector2.Dot(upDir);
		float num2 = (m_IsOmniDirectionalStack ? (vector2.magnitude / m_BeamColumnRadius) : 0f);
		float num3 = Vector3.Cross(vector2, upDir).magnitude / m_BeamColumnRadius;
		if (num3 < 2f && num >= 0f - m_BeamBaseHeight - 0.1f)
		{
			item.InBeam = true;
		}
		if (num3 >= 2f && Singleton.Manager<ManPointer>.inst.DraggingItem == item)
		{
			return true;
		}
		float minTime = ((m_OverrideDropAfterMinTime >= 0f) ? m_OverrideDropAfterMinTime : holdBeamFloatParams.dropAfterMinTime) + m_Holder.PickupContentionPeriod;
		if (item.ColliderSwapper.CollisionEnabled && ReadyToDropItem(item, minTime))
		{
			float num4 = (m_IsOmniDirectionalStack ? num2 : num3);
			if ((num4 - 1f) * m_BeamColumnRadius > holdBeamFloatParams.dropRange || (num4 >= 1f && !item.InBeam) || (!m_IsOmniDirectionalStack && num < 0f - m_BeamBaseHeight - 0.1f))
			{
				return true;
			}
		}
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == item)
		{
			return result;
		}
		Vector3 normalized = (vector + upDir * num - item.rbody.worldCenterOfMass).normalized;
		item.rbody.velocity *= holdBeamFloatParams.velocityDamping;
		float time;
		if (m_IsOmniDirectionalStack)
		{
			time = Mathf.Clamp(num2 * m_BeamColumnRadius, 0f, 1f);
		}
		else
		{
			float time2 = 0f;
			if (num > 0f)
			{
				time2 = num / Mathf.Max(num + 0.001f, stackData.topItemHeight);
			}
			else if (num < 0f)
			{
				time2 = (0f - num) / m_BeamBaseHeight;
			}
			time = num3 * holdBeamFloatParams.inPullVsHeight.Evaluate(time2);
		}
		float num5 = m_BeamStrength * holdBeamFloatParams.pullProfile.Evaluate(time);
		Vector3 vector3 = normalized * num5;
		float num6 = GetItemHeightInStack(itemRadius, itemIndex) - m_BeamBaseHeight - num;
		if (num3 > 5f && item.rbody.velocity.sqrMagnitude < holdBeamFloatParams.stationaryVelocitySqrThreshold)
		{
			num6 = Mathf.Max(num6, holdBeamFloatParams.stationaryMinHeightDiff);
		}
		if (!m_IsOmniDirectionalStack && num6 > holdBeamFloatParams.dropAboveHeight && ReadyToDropItem(item, minTime))
		{
			return true;
		}
		float num7 = ((m_OverrideHeightCorrectionLiftFactor >= 0f) ? m_OverrideHeightCorrectionLiftFactor : holdBeamFloatParams.heightCorrectionLiftFactor);
		float num8 = num6 * Mathf.Abs(num6) * num7;
		if (m_IsOmniDirectionalStack)
		{
			float num9 = Mathf.Max(m_OverrideHeightCorrectionLiftFactor, num5 * 2f);
			float num10 = Mathf.Clamp(num8, 0f - num9, num9);
			float num11 = vector3.Dot(upDir);
			vector3 += (num10 - num11) * upDir;
		}
		else
		{
			vector3 += upDir * num8;
		}
		vector3 -= Physics.gravity;
		if (vector3.magnitude > 2000f)
		{
			vector3 = vector3.normalized * 2000f;
		}
		item.rbody.AddForce(vector3, ForceMode.Acceleration);
		if (item.TouchingHolder)
		{
			Vector3 centrePosition = item.centrePosition;
			if (item.rbody != null)
			{
				centrePosition += item.rbody.position - item.trans.position;
			}
			base.block.tank.rbody.AddForceAtPosition(-vector3 * item.rbody.mass, centrePosition, ForceMode.Force);
		}
		Globals.ScaleRatio spinTorqueRatio = holdBeamFloatParams.spinTorqueRatio;
		float y = spinTorqueRatio.Scale(holdBeamFloatParams.readySpinSpeed - item.rbody.angularVelocity.y);
		item.rbody.AddTorque(new Vector3(0f, y, 0f), ForceMode.Acceleration);
		if (item.InBeam && Time.time > item.HeldItemLastCollisionContact + 1f && ((item.pickup != null && item.pickup.rbody != null) || (item.block != null && item.block.rbody != null)) && (base.block.tank.Anchors.Fixed || !m_UsePhysicsForUnanchoredTechs) && !m_UsePhysicsForHeldItems)
		{
			if ((bool)item.pickup)
			{
				item.pickup.ClearRigidBody(immediate: true);
			}
			else if ((bool)item.block)
			{
				item.block.ClearRigidBody(immediate: true);
			}
			m_HeldPhysicsItems.Remove(item);
			if (item.UsePrevHeldPos)
			{
				item.PrevHeldPos = WorldPosition.FromScenePosition(item.trans.position);
			}
		}
		return result;
	}

	private void UpdateBeamEffects()
	{
		if (m_BeamQuadPrefab.IsNull())
		{
			return;
		}
		if (!base.block.IsAttached)
		{
			ClearAllBeams();
			return;
		}
		ref Globals.HoldBeamFloatParams holdBeamFloatParams = ref Globals.inst.holdBeamFloatParams;
		float num = holdBeamFloatParams.clipRange * holdBeamFloatParams.clipRange;
		for (int i = 0; i < m_Holder.NumStacks; i++)
		{
			ref StackData reference = ref m_StackData[i];
			reference.beam.Clear();
			if (reference.beamEnabled && !reference.stack.IsEmpty)
			{
				Vector3 vector = reference.stack.BasePosWorldOffsetLocal(Vector3.zero);
				if ((vector - Singleton.cameraTrans.position).sqrMagnitude < num)
				{
					reference.beam.AddPoint(vector);
					ModuleItemHolder.Stack.ItemIterator enumerator = reference.stack.IterateItems().GetEnumerator();
					while (enumerator.MoveNext())
					{
						Visible current = enumerator.Current;
						if (!current.IsPrePickup)
						{
							reference.beam.AddPoint(current.centrePosition);
						}
					}
				}
			}
			reference.beam.Commit();
		}
	}

	private void ClearAllBeams()
	{
		if (!m_BeamQuadPrefab.IsNull())
		{
			for (int i = 0; i < m_StackData.Length; i++)
			{
				ref StackData reference = ref m_StackData[i];
				reference.beam.Clear();
				reference.beam.Commit();
			}
		}
	}

	private void OnAttached()
	{
		base.block.tank.Anchors.AnchorEvent.Subscribe(OnTechAnchored);
	}

	private void OnDetaching()
	{
		base.block.tank.Anchors.AnchorEvent.Unsubscribe(OnTechAnchored);
	}

	private void OnTechAnchored(bool anchored, bool skyAnchor)
	{
		if (anchored || !m_UsePhysicsForUnanchoredTechs)
		{
			return;
		}
		for (int i = 0; i < m_StackData.Length; i++)
		{
			ref StackData reference = ref m_StackData[i];
			for (int j = 0; j < reference.stack.items.Count; j++)
			{
				Visible visible = reference.stack.items[j];
				if (visible.pickup != null && visible.pickup.rbody == null)
				{
					visible.pickup.InitRigidbody();
					m_HeldPhysicsItems.Add(visible);
				}
				else if (visible.block != null && visible.block.rbody == null)
				{
					visible.block.InitRigidbody();
					m_HeldPhysicsItems.Add(visible);
				}
			}
		}
	}

	public void ItemNowHasPhysics(Visible item)
	{
		m_HeldPhysicsItems.Add(item);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleItemHolderBeam;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		for (int i = 0; i < m_StackData.Length; i++)
		{
			ConfigureItemStackMessage data = m_NetworkedStackData.Data;
			data._StackIndex = i;
			data._MoveType = (byte)m_StackData[i].moveType;
			data._BeamEnabled = m_StackData[i].beamEnabled;
			m_NetworkedStackData.Serialise(writer);
		}
	}

	public void OnDeserialize(NetworkReader reader)
	{
		for (int i = 0; i < m_StackData.Length; i++)
		{
			m_NetworkedStackData.Deserialise(reader);
		}
	}

	private void OnMPStackConfigured(ConfigureItemStackMessage msg)
	{
		ConfigureStack(msg._StackIndex, msg._BeamEnabled, (ItemMovementType)msg._MoveType, netSend: false);
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.TakeItemEvent.Subscribe(OnTakeItem);
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Pickup = GetComponent<ModuleItemPickup>();
		if (m_BeamQuadPrefab == null)
		{
			m_BeamQuadPrefab = null;
		}
		s_StackDataArrayBuilder.Clear();
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			s_StackDataArrayBuilder.Add(new StackData
			{
				stack = current,
				beam = (m_BeamQuadPrefab.IsNotNull() ? new BeamRenderer(base.transform, m_BeamQuadPrefab.transform) : null)
			});
		}
		m_StackData = s_StackDataArrayBuilder.ToArray();
		m_NetworkedStackData = new NetworkedProperty<ConfigureItemStackMessage>(this, TTMsgType.ConfigureItemStack, OnMPStackConfigured);
	}

	private void OnSpawn()
	{
		m_ScaleChanged = false;
		for (int i = 0; i < m_StackData.Length; i++)
		{
			m_StackData[i].animDummyIndex = i;
			ConfigureStack(i, drawBeam: true, ItemMovementType.AccelerateTowardsFixedPositions, netSend: false);
		}
	}

	private void OnRecycle()
	{
		ClearAllBeams();
		m_TakenHeartbeat.Clear();
	}

	private void OnTakeItem(Visible item, ModuleItemHolder.Stack stack)
	{
		int num = 0;
		bool flag = false;
		while (!flag && num < m_StackData.Length)
		{
			if (m_StackData[num].stack == stack)
			{
				flag = true;
			}
			else
			{
				num++;
			}
		}
		if (item.rbody != null)
		{
			m_HeldPhysicsItems.Add(item);
		}
		m_TakenHeartbeat.Add(item, m_Holder.block.tank.Holders.HeartbeatCount);
		if (flag && m_StackData[num].moveType == ItemMovementType.MoveWithAnimationDummies)
		{
			int num2 = stack.items.IndexOf(item);
			if (num2 >= 0)
			{
				MoveStackItemToAnimationDummy(item, num, num2);
			}
		}
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		item.trans.SetLocalScaleIfChanged(new Vector3(1f, 1f, 1f));
		m_HeldPhysicsItems.Remove(item);
		m_TakenHeartbeat.Remove(item);
		Singleton.Manager<ManVisible>.inst.ClearParticles(item);
	}

	private void OnFixedUpdate()
	{
		if (base.block.IsAttached && m_HeldPhysicsItems.Count > 0)
		{
			UpdateItemMovement();
		}
	}

	private void OnUpdate()
	{
		if (base.block.IsAttached)
		{
			UpdateFloatPositions();
		}
		UpdateBeamEffects();
	}
}
