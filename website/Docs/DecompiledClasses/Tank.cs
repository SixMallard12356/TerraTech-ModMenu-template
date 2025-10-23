#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(TechBooster))]
[RequireComponent(typeof(TechEnergy))]
[RequireComponent(typeof(TechHUDControl))]
[RequireComponent(typeof(Visible))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BlockManager))]
[RequireComponent(typeof(TankControl))]
[RequireComponent(typeof(TankBeam))]
[RequireComponent(typeof(TechAnchors))]
[RequireComponent(typeof(TechVision))]
[RequireComponent(typeof(TechWeapon))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(TechRadarMarker))]
[RequireComponent(typeof(TechAudio))]
[RequireComponent(typeof(TechQuestGiver))]
[RequireComponent(typeof(TechAI))]
public class Tank : MonoBehaviour, ManVisible.StateVisualiser.Provider, IGravityApplicationTarget
{
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class CollisionInfo
	{
		public struct Obj
		{
			public Collider collider;

			public Visible visible;

			[Il2CppSetOption(Option.NullChecks, false)]
			[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
			public TankBlock block
			{
				get
				{
					if (!visible.IsNotNull())
					{
						return null;
					}
					return visible.block;
				}
			}

			[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
			[Il2CppSetOption(Option.NullChecks, false)]
			public Tank tank
			{
				get
				{
					if (!visible.IsNotNull() || !visible.block.IsNotNull())
					{
						return null;
					}
					return visible.block.tank;
				}
			}

			public Obj(Collider c)
			{
				collider = c;
				visible = Visible.FindVisibleUpwards(c);
			}
		}

		public enum Event
		{
			Enter,
			Stay,
			NonAttached
		}

		public Obj a { get; private set; }

		public Obj b { get; private set; }

		public Vector3 point { get; private set; }

		public Vector3 normal { get; private set; }

		public Vector3 relVelocity { get; private set; }

		public Vector3 impulse { get; private set; }

		public bool DealImpactDamage { get; set; }

		public void Init(Collision collision)
		{
			ContactPoint contactPoint = collision.contacts[0];
			a = new Obj(contactPoint.thisCollider);
			b = new Obj(contactPoint.otherCollider);
			point = contactPoint.point;
			normal = contactPoint.normal;
			relVelocity = collision.relativeVelocity;
			impulse = collision.impulse;
			DealImpactDamage = true;
		}

		public void Flip()
		{
			Obj obj = a;
			a = b;
			b = obj;
			relVelocity = -relVelocity;
			impulse = -impulse;
		}

		public bool Same(CollisionInfo other)
		{
			if ((object)a.collider == other.a.collider && (object)a.visible == other.a.visible && (object)b.collider == other.b.collider)
			{
				return (object)b.visible == other.b.visible;
			}
			return false;
		}

		private float GetCombinedRestitution(Collider a, Collider b)
		{
			PhysicMaterial physicMaterial = (a.IsTerrain() ? Globals.inst.impactMaterialTerrain : a.sharedMaterial);
			PhysicMaterial physicMaterial2 = (b.IsTerrain() ? Globals.inst.impactMaterialTerrain : b.sharedMaterial);
			return (PhysicMaterialCombine)Mathf.Max((int)physicMaterial.bounceCombine, (int)physicMaterial2.bounceCombine) switch
			{
				PhysicMaterialCombine.Average => (physicMaterial.bounciness + physicMaterial2.bounciness) * 0.5f, 
				PhysicMaterialCombine.Multiply => physicMaterial.bounciness * physicMaterial2.bounciness, 
				PhysicMaterialCombine.Maximum => Mathf.Max(physicMaterial.bounciness, physicMaterial2.bounciness), 
				PhysicMaterialCombine.Minimum => Mathf.Min(physicMaterial.bounciness, physicMaterial2.bounciness), 
				_ => 0f, 
			};
		}

		private float GetDivisorPart(Rigidbody body, Vector3 point, Vector3 n)
		{
			if (body.IsNull())
			{
				return 0f;
			}
			Matrix4x4 matrix4x = new Matrix4x4
			{
				m00 = body.inertiaTensor.x,
				m11 = body.inertiaTensor.y,
				m22 = body.inertiaTensor.z,
				m33 = 1f
			};
			Vector3 vector = point - body.transform.TransformPoint(body.centerOfMass);
			return 1f / body.mass + Vector3.Dot(Vector3.Cross(matrix4x.inverse.MultiplyVector(Vector3.Cross(vector, n)), vector), n);
		}

		[Obsolete("Deprecated in favour of builtin collision.impulse (introduced in unity 5.2)")]
		public void CalculateImpulse(Rigidbody body, float multipilier)
		{
			d.Assert(condition: false, "CollisionInfo.CalculateImpulse NO LONGER USED");
			float combinedRestitution = GetCombinedRestitution(a.collider, b.collider);
			Vector3 vector = normal;
			if (!b.collider.gameObject.IsTerrain())
			{
				vector = (normal + relVelocity.normalized) * 0.5f;
			}
			Rigidbody rigidbody = (b.visible.IsNotNull() ? b.visible.rbody : b.collider.attachedRigidbody);
			if (rigidbody.IsNotNull() && rigidbody.mass == 0.00123f)
			{
				impulse = Vector3.zero;
				return;
			}
			float num = (0f - (1f + combinedRestitution)) * Vector3.Dot(relVelocity, vector) / (GetDivisorPart(body, point, vector) + GetDivisorPart(rigidbody, point, vector));
			d.Log($"calculated impulse {vector * num} vs {impulse}");
			impulse = vector * num * multipilier;
		}

		private IEnumerable<object> EchoParts()
		{
			yield return "A:";
			yield return a.collider;
			yield return a.visible;
			yield return "\nB:";
			yield return b.collider;
			yield return b.visible;
			yield return "\n";
			yield return point.ToString();
			yield return normal.ToString();
			yield return relVelocity.ToString();
		}

		public override string ToString()
		{
			return " ".JoinStrings(EchoParts());
		}
	}

	public class WeakReference
	{
		private Visible.WeakReference m_VisWeakRef = new Visible.WeakReference();

		public void Set(Tank tank)
		{
			m_VisWeakRef.Set(tank.IsNotNull() ? tank.visible : null);
		}

		public Tank Get()
		{
			Visible visible = m_VisWeakRef.Get();
			if (!visible.IsNotNull())
			{
				return null;
			}
			return visible.tank;
		}
	}

	private struct ForceOverTime
	{
		public Vector3 Force;

		public Vector3 Position;

		public float Duration;
	}

	public float airSpeedDragFactor = 0.001f;

	public float airSpeedAngularDragFactor = 0.001f;

	public float constantAngularDragFactor = 1f;

	public float antiGravAirSpeedAngularDragFactor = 0.001f;

	public float antiGravConstantAngularDragFactor = 1f;

	public float massScaleFactor = 0.1f;

	public float inertiaTensorScaleFactor = 1f;

	public Vector3 kickVelocityBasic;

	public Vector3 kickVelocityRandom;

	public float kickSpinRandom;

	public float debugReportCurrentDrag;

	public float debugReportVolumeDrag;

	public string debugReportInertiaTensor;

	public string debugReportAngAccel;

	public float tankHitMinInterval = 0.25f;

	public SphereCollider dragSphere;

	public float dragSphereEnclosePadding = 1.5f;

	public float m_HealHeartbeatInterval = 1.5f;

	public string m_Creator;

	public string Author;

	public const float k_MassIgnoreImpactDamage = 0.00123f;

	public Event<ManDamage.DamageInfo> DamageEvent;

	public Event<TankBlock, Tank> AttachEvent;

	public Event<TankBlock, Tank> DetachEvent;

	public Event<ModuleAnchor, bool, bool> AnchorEvent;

	public Event<bool> SleepEvent;

	public Event<int> ResetEvent;

	public Event<bool, bool> FixupAnchorEvent;

	public EventNoParams ResetPhysicsEvent;

	public Event<bool, Dictionary<int, TechComponent.SerialData>> SerializeEvent;

	public Event<Collider, int> TriggerEvent;

	public Event<CollisionInfo, CollisionInfo.Event> CollisionEvent;

	public EventNoParams PostSpawnEvent;

	public Event<Tank> TankRecycledEvent;

	public EventNoParams UpdateEvent;

	public EventNoParams FixedUpdateEvent;

	private CollisionInfo collision = new CollisionInfo();

	private CollisionInfo previousCollision = new CollisionInfo();

	private float lastCollisionTime;

	private float lastGroundHitTime;

	private float lastWheelGroundHitTime;

	private float lastCollideTerrainTime;

	private Vector3 prevVelocity;

	private Vector3 prevAngVelocity;

	private float preserveMomentum;

	private float m_HealHeartbeatTime;

	private int m_Team;

	private bool m_IsPopulation;

	private ManWorld.CachedBiomeBlendWeights m_BiomeWeightCache;

	private int m_BiomeWeightCacheFrame;

	private ManFreeSpace.SafeArea m_SafeArea;

	private TankDescriptionOverlay m_Overlay;

	private int m_TechTotalValue;

	private LandingGear m_LandingGear;

	private int m_ActiveStabilisers;

	private LocalisedString m_LocalisedName;

	private int m_NameIndex = -1;

	private Vector3 m_CogPos;

	private float m_TotalWeightScale;

	private bool m_AwaitingPhysicsReset;

	private bool m_AwaitingCentreOfGravityUpdate;

	private bool m_GravityApplicationTouched;

	private List<ForceOverTime> forcesOverTime = new List<ForceOverTime>();

	public TankPreset preset { get; set; }

	public float OriginalValue { get; set; }

	public List<FactionSubTypes> MainCorps { get; set; }

	public Bounds blockBounds => blockman.blockCentreBounds;

	public Vector3 boundsCentreWorld
	{
		get
		{
			return trans.TransformPoint(blockBounds.center);
		}
		set
		{
			PositionBoundsCentreWorld(value);
		}
	}

	public Vector3 WorldTop => trans.TransformPoint(blockBounds.center + new Vector3(0f, blockBounds.extents.y + 0.5f, 0f));

	public Vector3 boundsCentreWorldNoCheck => trans.TransformPoint(blockBounds.center);

	public bool ControlsActive { get; set; }

	public bool FirstUpdateAfterSpawn { get; private set; }

	public TankControl control { get; private set; }

	public TankBeam beam { get; private set; }

	public BlockManager blockman { get; private set; }

	public TechEnergy EnergyRegulator { get; private set; }

	public TechVision Vision { get; private set; }

	public TechAnchors Anchors { get; private set; }

	public TechHolders Holders { get; private set; }

	public TechWeapon Weapons { get; private set; }

	public TechBooster Boosters { get; private set; }

	public TechHUDControl HUDControl { get; private set; }

	public TechAI AI { get; private set; }

	public TechRadar Radar { get; private set; }

	public TechQuestGiver QuestGiver { get; private set; }

	public TechAudio TechAudio { get; private set; }

	public TechSequencer Sequencer { get; private set; }

	public TechBlockStateController BlockStateController { get; private set; }

	public TechRadarMarker RadarMarker { get; private set; }

	public TechCircuits Circuits { get; private set; }

	public ManPointer.OpenMenuEventConsumer TechOpenMenuEventConsumer { get; private set; }

	public TankBlock CentralBlock { get; private set; }

	public ManDamage.DamageInfo DamageInEffect { get; set; }

	public Vector3 CenterOfMass => rbody.centerOfMass;

	public Vector3 WorldCenterOfMass => rbody.worldCenterOfMass;

	public int Team => m_Team;

	public bool IsPopulation => m_IsPopulation;

	public bool IsPlayer => (object)this == Singleton.playerTank;

	public bool DamagedByPlayer { get; private set; }

	public bool PlayerFocused
	{
		get
		{
			if (!IsPlayer)
			{
				return control.HandlesPlayerInput;
			}
			return true;
		}
	}

	public bool ControllableByAnyPlayer
	{
		get
		{
			if (control.HasController && control.FirstController.HandlesPlayerInput)
			{
				return ManSpawn.IsPlayerTeam(Team);
			}
			return false;
		}
	}

	public bool ControllableByLocalPlayer
	{
		get
		{
			if (control.HasController && control.FirstController.HandlesPlayerInput)
			{
				return Singleton.Manager<ManPlayer>.inst.PlayerTeam == Team;
			}
			return false;
		}
	}

	public bool IsBase => Anchors.NumAnchored > 0;

	public Rigidbody rbody { get; private set; }

	public Transform trans { get; private set; }

	public Visible visible { get; private set; }

	public NetTech netTech { get; private set; }

	public Vector3 acceleration { get; set; }

	public Transform rootBlockTrans
	{
		get
		{
			TankBlock rootBlock = blockman.GetRootBlock();
			if (!(rootBlock != null))
			{
				return trans;
			}
			return rootBlock.trans;
		}
	}

	public bool grounded
	{
		get
		{
			return Time.time - lastGroundHitTime < 0.1f;
		}
		set
		{
			lastGroundHitTime = Time.time;
		}
	}

	public bool wheelGrounded
	{
		get
		{
			return Time.time - lastWheelGroundHitTime < 0.1f;
		}
		set
		{
			lastWheelGroundHitTime = Time.time;
		}
	}

	public bool touchingTerrain => Time.time - lastCollideTerrainTime < 0.1f;

	public bool IsAnchored => Anchors.NumAnchored > 0;

	public bool IsSkyAnchored => Anchors.NumSkyAnchored > 0;

	public bool IsSleeping { get; private set; }

	public bool ShouldShowOverlay { get; set; }

	public ManDamage.DamageInfo FatalDamage { get; set; }

	public bool WasPlayerControlledAtFatalDamageTime { get; set; }

	public int ConnectionIdOnFatalDamage { get; set; }

	public bool IsLandingGearDeployed
	{
		get
		{
			if (m_LandingGear.IsNotNull())
			{
				return m_LandingGear.IsDeployed;
			}
			return false;
		}
	}

	public bool ShouldAutoStabilise
	{
		get
		{
			if (m_ActiveStabilisers > 0)
			{
				if (!BlockStateController.IsNull())
				{
					return BlockStateController.IsCategoryActive(ModuleControlCategory.Stabiliser);
				}
				return true;
			}
			return false;
		}
	}

	public bool PassiveBrakesEnabled
	{
		get
		{
			if (!BlockStateController.IsNull())
			{
				return BlockStateController.IsCategoryActive(ModuleControlCategory.PassiveBrake);
			}
			return true;
		}
	}

	public bool EnableGravity { get; set; }

	public bool HasNameIndex => m_NameIndex >= 0;

	public bool ShouldExplodeDetachingBlocks { get; set; }

	public float ExplodeDetachingBlocksDelay { get; set; }

	private bool NeedsSafeArea => ManSpawn.IsPlayerTeam(m_Team);

	public void RequestPhysicsReset()
	{
		m_AwaitingPhysicsReset = true;
	}

	public void RequestCentreOfGravityUpdate()
	{
		m_AwaitingCentreOfGravityUpdate = true;
	}

	public void SetLocalisedName(LocalisedString newName, int nameIndex = -1)
	{
		if (newName != null && newName.Value != null)
		{
			if (m_LocalisedName == null)
			{
				Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChangedUpdateName);
			}
			m_LocalisedName = newName;
			m_NameIndex = nameIndex;
			base.name = TechData.CreateNameWithIndex(m_LocalisedName, m_NameIndex);
		}
		else
		{
			d.LogError("Attempting to set null name on tech");
		}
	}

	public LocalisedString GetLocalisedName()
	{
		return m_LocalisedName;
	}

	public int GetNameIndex()
	{
		return m_NameIndex;
	}

	public void SetName(string newName)
	{
		if (m_LocalisedName != null)
		{
			Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChangedUpdateName);
		}
		m_LocalisedName = null;
		m_NameIndex = -1;
		base.name = newName;
	}

	private void OnLanguageChangedUpdateName()
	{
		if (m_LocalisedName != null)
		{
			base.name = TechData.CreateNameWithIndex(m_LocalisedName, m_NameIndex);
			Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(this, null);
		}
	}

	public int GetValue()
	{
		if (m_TechTotalValue < 0)
		{
			m_TechTotalValue = 0;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				m_TechTotalValue += Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(current.BlockType);
			}
		}
		return m_TechTotalValue;
	}

	public bool IsAIControlled()
	{
		if (control.IsNotNull() && control.HasController)
		{
			return control.FirstController.DoesAIControl();
		}
		return false;
	}

	public bool IsFriendly()
	{
		return IsFriendly(Singleton.Manager<ManPlayer>.inst.PlayerTeam);
	}

	public bool IsFriendly(int teamID)
	{
		return IsFriendly(m_Team, teamID);
	}

	public bool IsEnemy()
	{
		return IsEnemy(Singleton.Manager<ManPlayer>.inst.PlayerTeam);
	}

	public bool IsNeutral()
	{
		return m_Team == -2;
	}

	public bool IsEnemy(int teamID)
	{
		return IsEnemy(m_Team, teamID);
	}

	public static bool IsFriendly(int teamID1, int teamID2)
	{
		if (teamID1 == teamID2)
		{
			return true;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp() && ((teamID1 == 0 && teamID2 == 1073741824) || (teamID2 == 0 && teamID1 == 1073741824)))
		{
			return true;
		}
		return false;
	}

	public static bool IsEnemy(int teamID1, int teamID2)
	{
		bool result = !IsFriendly(teamID1, teamID2) && teamID1 != -2 && teamID2 != -2;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp() && (teamID1 == 1073741828 || teamID2 == 1073741828))
		{
			result = false;
		}
		return result;
	}

	public FactionSubTypes GetMainCorp()
	{
		FactionSubTypes factionSubTypes = FactionSubTypes.GSO;
		if (MainCorps != null && MainCorps.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, MainCorps.Count);
			factionSubTypes = MainCorps[index];
		}
		else
		{
			string text = ((MainCorps == null) ? "null" : ("count:" + MainCorps.Count));
			d.LogWarningFormat("Tank.SelectMainCorp - tank.MainCorp is {2}. Tech {0} will always return default faction type {1}", preset ? preset.name : base.name, factionSubTypes, text);
			MainCorps = new List<FactionSubTypes> { factionSubTypes };
		}
		return factionSubTypes;
	}

	public static Vector3 ConstrainAPPosition(Vector3 localPos)
	{
		Vector3 vector = localPos - new Vector3(0.5f, 0.5f, 0.5f);
		Vector3 vector2 = new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
		Vector3 vector3 = vector - vector2;
		float num = Mathf.Min(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
		if (Mathf.Abs(vector3.x) == num)
		{
			vector3.x = 0f;
			vector3.y *= 0.5f / Mathf.Abs(vector3.y);
			vector3.z *= 0.5f / Mathf.Abs(vector3.z);
		}
		else if (Mathf.Abs(vector3.y) == num)
		{
			vector3.x *= 0.5f / Mathf.Abs(vector3.x);
			vector3.y = 0f;
			vector3.z *= 0.5f / Mathf.Abs(vector3.z);
		}
		else
		{
			vector3.x *= 0.5f / Mathf.Abs(vector3.x);
			vector3.y *= 0.5f / Mathf.Abs(vector3.y);
			vector3.z = 0f;
		}
		return vector2 + vector3 + new Vector3(0.5f, 0.5f, 0.5f);
	}

	public void SetInvulnerable(bool invulnerable, bool forever)
	{
		if (this.IsBeingRecycled())
		{
			d.LogWarningFormat("SetInvulnerable on a tech that's being recycled '{0}' ", base.name);
		}
		if (IsFriendly())
		{
			d.LogWarningFormat("SetInvulnerable on a friendly tech '{0}' ", base.name);
		}
		BlockManager.BlockIterator<Damageable>.Enumerator enumerator = blockman.IterateBlockComponents<Damageable>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.SetInvulnerable(invulnerable, forever);
		}
	}

	public void NotifyDamage(ManDamage.DamageInfo info, TankBlock blockDamaged)
	{
		if (!DamagedByPlayer && (!Globals.inst.m_TechKilledIfOnlyCabDamaged || blockDamaged.IsController) && blockDamaged.tank.IsEnemy(0) && info.SourceTank.IsNotNull() && info.SourceTank.IsFriendly(0))
		{
			DamagedByPlayer = true;
		}
		DamageEvent.Send(info);
		if (IsPlayer)
		{
			GamepadVibration.VibratePad(GamepadVibration.Type.PlayerTankHit, hasPriority: false);
		}
	}

	public void NotifyBlock(TankBlock block, bool attached)
	{
		if (attached)
		{
			AttachEvent.Send(block, this);
		}
		else
		{
			DetachEvent.Send(block, this);
		}
		m_TechTotalValue = -1;
	}

	public void NotifyAnchor(ModuleAnchor anchor, bool anchored, bool fromAfterTechPopulate = false)
	{
		AnchorEvent.Send(anchor, anchored, fromAfterTechPopulate);
	}

	public bool BoundsIntersectSphere(Vector3 centreWorld, float radius)
	{
		return blockBounds.IntersectSphere(trans.InverseTransformPoint(centreWorld), radius);
	}

	private void PositionBoundsCentreWorld(Vector3 worldPos)
	{
		trans.position = worldPos - trans.TransformDirection(blockBounds.center);
	}

	public void PositionBaseCentred(Vector3 worldPos)
	{
		d.Assert(!IsAnchored, "moving a tech that is anchored. If this is REALLY needed then PositionBaseCentred needs to de-anchor and re-anchor");
		trans.position = worldPos - trans.TransformDirection(blockBounds.center - new Vector3(0f, blockBounds.extents.y + 0.5f, 0f));
	}

	public Quaternion FacingOrientation(Vector3 direction)
	{
		return Quaternion.LookRotation(direction) * Quaternion.Inverse(rootBlockTrans.localRotation);
	}

	public void CheckResetPhysics()
	{
		if (blockman.CheckChangedAndReset() || FirstUpdateAfterSpawn)
		{
			CentralBlock = null;
			if ((bool)blockman.GetRootBlock())
			{
				ResetPhysics(SendEventUpdate: true);
			}
			UpdateSceneryBlockerSize();
			if (FirstUpdateAfterSpawn)
			{
				Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(visible, forceNow: true);
				PostSpawnEvent.Send();
			}
			FirstUpdateAfterSpawn = false;
		}
	}

	private void CalculateCentreOfGravity()
	{
		m_AwaitingCentreOfGravityUpdate = false;
		if (blockman.blockCount > 0)
		{
			m_TotalWeightScale = 0f;
			Vector3 zero = Vector3.zero;
			_ = Vector3.zero;
			m_CogPos = Vector3.zero;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				_ = current.CurrentInertiaTensor;
				Vector3 centreOfMass = current.CentreOfMass;
				Vector3 centreOfGravity = current.CentreOfGravity;
				Vector3 vector = current.trans.localPosition + current.trans.localRotation * centreOfMass;
				zero += current.CurrentMass * vector;
				float num = current.CurrentMass * massScaleFactor * current.AverageGravityScaleFactor;
				Vector3 vector2 = current.trans.localPosition + current.trans.localRotation * centreOfGravity;
				m_TotalWeightScale += num;
				m_CogPos += num * vector2;
			}
			if (m_TotalWeightScale == 0f)
			{
				m_CogPos = Vector3.zero;
			}
			else
			{
				m_CogPos /= m_TotalWeightScale;
			}
		}
	}

	public void ResetPhysics(bool SendEventUpdate = false)
	{
		if (blockman.blockCount <= 0)
		{
			if (SendEventUpdate)
			{
				ResetPhysicsEvent.Send();
			}
			return;
		}
		m_AwaitingPhysicsReset = false;
		float num = 0f;
		m_TotalWeightScale = 0f;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		m_CogPos = Vector3.zero;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			Vector3 currentInertiaTensor = current.CurrentInertiaTensor;
			Vector3 centreOfMass = current.CentreOfMass;
			Vector3 centreOfGravity = current.CentreOfGravity;
			Vector3 vector = current.cachedLocalPosition + current.cachedLocalRotation * centreOfMass;
			num += current.CurrentMass;
			zero += current.CurrentMass * vector;
			float num2 = current.CurrentMass * massScaleFactor * current.AverageGravityScaleFactor;
			Vector3 vector2 = current.cachedLocalPosition + current.cachedLocalRotation * centreOfGravity;
			m_TotalWeightScale += num2;
			m_CogPos += num2 * vector2;
			zero2 += currentInertiaTensor + current.CurrentMass * new Vector3(vector.y * vector.y + vector.z * vector.z, vector.z * vector.z + vector.x * vector.x, vector.x * vector.x + vector.y * vector.y);
		}
		d.Assert(num != 0f, "Tank has zero mass");
		d.Assert(zero2 != Vector3.zero, "Tank has zero moment");
		rbody.mass = num * massScaleFactor;
		if (num == 0f)
		{
			zero = Vector3.zero;
		}
		else
		{
			zero /= num;
		}
		rbody.centerOfMass = zero;
		if (m_TotalWeightScale == 0f)
		{
			m_CogPos = Vector3.zero;
		}
		else
		{
			m_CogPos /= m_TotalWeightScale;
		}
		zero2 -= num * new Vector3(zero.y * zero.y + zero.z * zero.z, zero.z * zero.z + zero.x * zero.x, zero.x * zero.x + zero.y * zero.y);
		rbody.inertiaTensor = zero2 * massScaleFactor * inertiaTensorScaleFactor;
		rbody.inertiaTensorRotation = Quaternion.identity;
		float num3 = 1f;
		float num4 = float.MaxValue;
		CentralBlock = null;
		enumerator = blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current2 = enumerator.Current;
			float sqrMagnitude = (current2.cachedLocalPosition + current2.cachedLocalRotation * current2.CentreOfMass - zero).sqrMagnitude;
			num3 = Mathf.Max(num3, sqrMagnitude);
			if (sqrMagnitude < num4)
			{
				num4 = sqrMagnitude;
				CentralBlock = current2;
			}
		}
		dragSphere.transform.position = rbody.worldCenterOfMass;
		dragSphere.radius = Mathf.Sqrt(num3) + dragSphereEnclosePadding;
		beam.SetHoverBase();
		if (m_LandingGear.IsNotNull())
		{
			m_LandingGear.Recalculate(blockBounds);
		}
		if (BlockStateController.IsNotNull())
		{
			BlockStateController.HoverPower = BlockStateController.HoverPower;
		}
		if (SendEventUpdate)
		{
			ResetPhysicsEvent.Send();
		}
	}

	public void BlockMassChanged(TankBlock block, float newMass, Vector3 newTensor)
	{
		float mass = rbody.mass;
		float num = rbody.mass / massScaleFactor;
		Vector3 centerOfMass = rbody.centerOfMass;
		Vector3 vector = rbody.inertiaTensor / (massScaleFactor * inertiaTensorScaleFactor);
		float num2 = newMass;
		float currentMass = block.CurrentMass;
		float num3 = block.CurrentMass * massScaleFactor;
		newMass *= massScaleFactor;
		Vector3 currentInertiaTensor = block.CurrentInertiaTensor;
		Vector3 centreOfMass = block.CentreOfMass;
		Vector3 vector2 = block.trans.localPosition + block.trans.localRotation * centreOfMass;
		Vector3 vector3 = vector2 - centerOfMass;
		Vector3 vector4 = new Vector3(vector3.x * vector3.x, vector3.y * vector3.y, vector3.z * vector3.z);
		Vector3 vector5 = new Vector3(vector4.y + vector4.z, vector4.z + vector4.x, vector4.x + vector4.y);
		Vector3 vector6 = currentInertiaTensor + currentMass * vector5;
		vector -= vector6;
		Vector3 vector7 = centerOfMass;
		float num4 = newMass * block.AverageGravityScaleFactor - num3 * block.AverageGravityScaleFactor;
		m_CogPos = m_CogPos * m_TotalWeightScale + num4 * vector2;
		centerOfMass = centerOfMass * mass + (newMass - num3) * vector2;
		mass += newMass - num3;
		num += num2 - currentMass;
		if (mass == 0f)
		{
			centerOfMass = Vector3.zero;
		}
		else
		{
			centerOfMass /= mass;
		}
		m_TotalWeightScale += num4;
		if (m_TotalWeightScale == 0f)
		{
			m_CogPos = centerOfMass;
		}
		else
		{
			m_CogPos /= m_TotalWeightScale;
		}
		vector6 = newTensor + num2 * vector5;
		vector += vector6;
		Vector3 vector8 = centerOfMass - vector7;
		Vector3 vector9 = new Vector3(vector8.x * vector8.x, vector8.y * vector8.y, vector8.z * vector8.z);
		vector -= num * new Vector3(vector9.y + vector9.z, vector9.z + vector9.x, vector9.x + vector9.y);
		rbody.mass = mass;
		rbody.centerOfMass = centerOfMass;
		rbody.inertiaTensor = vector * massScaleFactor * inertiaTensorScaleFactor;
	}

	public void HandleCollision(Collision collisionData, bool stay)
	{
		ContactPoint[] contacts = collisionData.contacts;
		if (contacts.Length == 0)
		{
			return;
		}
		ContactPoint contactPoint = contacts[0];
		if (stay && (contactPoint.thisCollider.gameObject.IsTerrain() || contactPoint.otherCollider.gameObject.IsTerrain()))
		{
			lastGroundHitTime = (lastCollideTerrainTime = Time.time);
			return;
		}
		int layer = contactPoint.otherCollider.gameObject.layer;
		if (layer == (int)Globals.inst.layerBullet || layer == (int)Globals.inst.layerShieldPiercingBullet)
		{
			return;
		}
		collision.Init(collisionData);
		if ((object)collision.a.tank != this && (object)collision.b.tank != this)
		{
			if (stay)
			{
				CollisionEvent.Send(collision, CollisionInfo.Event.NonAttached);
			}
			return;
		}
		if ((object)collision.a.tank != this)
		{
			collision.Flip();
		}
		if ((object)collision.a.tank != this)
		{
			d.LogError("non-tank collision: " + collision);
		}
		else
		{
			if (collision.b.collider.gameObject.layer == (int)Globals.inst.layerPickup || collision.b.collider.gameObject.layer == (int)Globals.inst.layerShieldPiercingBullet || (object)collision.b.tank == this || (!stay && collision.Same(previousCollision) && Time.time < lastCollisionTime + tankHitMinInterval))
			{
				return;
			}
			if (collision.b.block.IsNull())
			{
				d.Assert(collision.b.collider.gameObject.layer == (int)Globals.inst.layerScenery || collision.b.collider.gameObject.layer == (int)Globals.inst.layerLandmark || collision.b.collider.gameObject.layer == (int)Globals.inst.layerIgnoreScenery || collision.b.collider.gameObject.layer == (int)Globals.inst.layerTerrain || collision.b.collider.GetComponent<TankSubstituteVolume>().IsNotNull(), "Expected Scenery or Terrain hit, got layer: " + LayerMask.LayerToName(collision.b.collider.gameObject.layer) + " on hitting " + collision.b.collider.gameObject.name + (collision.b.tank.IsNotNull() ? (" (" + collision.b.tank.name + ")") : ""));
				lastGroundHitTime = Time.time;
				if (contactPoint.thisCollider.gameObject.IsTerrain() || contactPoint.otherCollider.gameObject.IsTerrain())
				{
					lastCollideTerrainTime = Time.time;
				}
				if (collision.a.tank.beam.IsActive)
				{
					return;
				}
			}
			else if (collision.b.tank.IsNotNull() && Vector3.Angle(collision.normal, Vector3.up) < Globals.inst.m_TechGroundingThresholdDegrees)
			{
				lastGroundHitTime = Time.time;
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.CollideToScavengeBlocks && Singleton.Manager<ManNetwork>.inst.NetController.IsNotNull() && (object)collision.a.tank == this && collision.b.tank.IsNull() && collision.b.block.IsNotNull() && !collision.b.visible.Killed && collision.b.block.netBlock.IsNotNull() && netTech.IsNotNull() && netTech.NetPlayer.IsNotNull() && netTech.NetPlayer.IsActuallyLocalPlayer && collision.b.block.netBlock.CanPlayerScavenge(netTech.NetPlayer.netId))
			{
				Singleton.Manager<ManLooseBlocks>.inst.RequestDespawnBlock(collision.b.block, DespawnReason.ScavengeBlock);
			}
			Vector3 vector = collision.impulse;
			if (!stay)
			{
				Rigidbody rigidbody = (collision.b.visible.IsNotNull() ? collision.b.visible.rbody : collision.b.collider.attachedRigidbody);
				if (rigidbody != null && rigidbody.mass == 0.00123f)
				{
					vector = Vector3.zero;
				}
			}
			CollisionEvent.Send(collision, stay ? CollisionInfo.Event.Stay : CollisionInfo.Event.Enter);
			if (stay)
			{
				return;
			}
			float num = vector.magnitude * Globals.inst.impactDamageMultiplier;
			float num2 = 0f;
			if (collision.DealImpactDamage && num > Globals.inst.impactDamageThreshold && collision.relVelocity.magnitude > Globals.inst.impactDamageSpeedThreshold && (collision.b.visible.IsNull() || (collision.b.visible.type != ObjectTypes.Chunk && (collision.b.visible.type != ObjectTypes.Block || collision.b.visible.block.tank.IsNotNull()) && collision.b.visible.type != ObjectTypes.Crate)))
			{
				if (collision.b.visible.IsNotNull() && ManNetwork.IsHost)
				{
					Damageable damageable = collision.b.visible.damageable;
					if (collision.b.collider.CompareTag("_B"))
					{
						damageable = collision.b.collider.GetComponentInParent<Damageable>();
					}
					if (damageable.IsNotNull())
					{
						num2 = Singleton.Manager<ManDamage>.inst.DealImpactDamage(damageable, num, this, this, collision.point, collision.normal);
					}
				}
				Damageable damageable2 = collision.a.visible.damageable;
				if (damageable2.IsNotNull() && ManNetwork.IsHost)
				{
					Singleton.Manager<ManDamage>.inst.DealImpactDamage(damageable2, num, collision.b.visible, collision.b.tank, collision.point, -collision.normal);
				}
			}
			if (num2 != 0f && collision.b.collider.GetComponent<Rigidbody>() == null)
			{
				preserveMomentum = ((preserveMomentum == 0f) ? 1f : preserveMomentum) * num2;
			}
			CollisionInfo collisionInfo = previousCollision;
			previousCollision = collision;
			collision = collisionInfo;
			lastCollisionTime = Time.time;
		}
	}

	public static Tank GetTechFromCollider(Collider collider)
	{
		Visible visible = Visible.FindVisibleUpwards(collider);
		Tank result = null;
		if (visible != null)
		{
			if (visible.type == ObjectTypes.Vehicle)
			{
				result = visible.tank;
			}
			else if (visible.type == ObjectTypes.Block)
			{
				result = visible.block.tank;
			}
		}
		return result;
	}

	public void SetTeam(int teamID)
	{
		SetTeam(teamID, m_IsPopulation);
	}

	public static void SetTechTeamMultiplayerSafe(Tank tech, int teamID)
	{
		if (!(tech == null))
		{
			if (tech.netTech != null)
			{
				tech.netTech.OnServerSetTeam(teamID);
			}
			else
			{
				tech.SetTeam(teamID);
			}
		}
	}

	public void SetTeam(int teamID, bool isPopulation)
	{
		d.AssertFormat(teamID != -1, this, "Trying to set invalid team ID 'NewEnemyTeam' directly on Tank '{0}'. Use ManSpawn.GenerateAutomaticTeamID instead.", base.name);
		bool needsSafeArea = NeedsSafeArea;
		if (ManSpawn.IsPlayerTeam(teamID))
		{
			isPopulation = false;
		}
		if (teamID != m_Team || isPopulation != m_IsPopulation)
		{
			ManTechs.TeamChangeInfo paramB = new ManTechs.TeamChangeInfo
			{
				m_OldTeam = m_Team,
				m_OldIsPopulation = m_IsPopulation,
				m_NewTeam = teamID,
				m_NewIsPopulation = isPopulation
			};
			Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Send(this, paramB);
			BlockManager.BlockIterator<ModuleShieldGenerator>.Enumerator enumerator = blockman.IterateBlockComponents<ModuleShieldGenerator>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.UpdateCollisionCache(enable: false);
			}
			m_Team = teamID;
			m_IsPopulation = isPopulation;
			enumerator = blockman.IterateBlockComponents<ModuleShieldGenerator>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.UpdateCollisionCache(enable: true);
			}
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			bool needsSafeArea2 = NeedsSafeArea;
			if (needsSafeArea2 != needsSafeArea)
			{
				if (needsSafeArea2)
				{
					Singleton.Manager<ManFreeSpace>.inst.AddSafeArea(m_SafeArea);
				}
				else
				{
					Singleton.Manager<ManFreeSpace>.inst.RemoveSafeArea(m_SafeArea);
				}
			}
		}
		if (ManSpawn.IsPlayerTeam(teamID))
		{
			DiscoverBlocksOnTech();
		}
	}

	public void SetSafeArea()
	{
		Singleton.Manager<ManFreeSpace>.inst.AddSafeArea(m_SafeArea);
	}

	public void DiscoverBlocksOnTech()
	{
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock(current.BlockType);
		}
	}

	public ManWorld.CachedBiomeBlendWeights BiomeWeightsAtPositionThisFrame()
	{
		if (Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.IsNull())
		{
			return ManWorld.CachedBiomeBlendWeights.CreateInvalid();
		}
		if (m_BiomeWeightCacheFrame != Singleton.instance.FixedFrameCount)
		{
			m_BiomeWeightCacheFrame = Singleton.instance.FixedFrameCount;
			Vector3 scenePos = rbody.position + rbody.rotation * blockBounds.center;
			m_BiomeWeightCache = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos);
		}
		return m_BiomeWeightCache;
	}

	public bool TrySetAnchored(bool setAnchored)
	{
		if (IsAnchored == setAnchored)
		{
			return false;
		}
		bool flag = false;
		if (!setAnchored)
		{
			Anchors.UnanchorAll(playAnim: true);
			flag = true;
		}
		else if (Anchors.NumPossibleAnchors > 0)
		{
			if (Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(boundsCentreWorld, Globals.inst.m_TechStoreThreatDistance))
			{
				if (beam.IsActive)
				{
					beam.DisableAndAnchor(force: true);
				}
				else
				{
					bool moveTech = true;
					flag = Anchors.TryAnchorAll(moveTech);
					if (!flag)
					{
						Singleton.Manager<ManTechs>.inst.PlayerTankAnchorFailedEvent.Send();
					}
				}
			}
			else
			{
				Singleton.Manager<ManPlayer>.inst.OnPlayerFailedToAnchorWithEnemiesNearby.Send();
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.AnchorFailed);
			}
		}
		return flag;
	}

	public void FixupAnchors(bool forceAnchor = true, bool tryAnchorAnyValid = false)
	{
		if (Anchors.NumIsAnchored > 0)
		{
			Physics.SyncTransforms();
		}
		FixupAnchorEvent.Send(forceAnchor, tryAnchorAnyValid);
	}

	public void SetSleeping(bool sleep)
	{
		if (sleep != IsSleeping)
		{
			IsSleeping = sleep;
			SleepEvent.Send(IsSleeping);
			CheckKinematic();
		}
	}

	public void CheckKinematic()
	{
		bool flag = IsSleeping || Anchors.Fixed;
		if (flag != rbody.isKinematic)
		{
			rbody.isKinematic = flag;
		}
	}

	public void SetOverlayType(TankDescriptionData data)
	{
		if (m_Overlay != null)
		{
			Singleton.Manager<ManOverlay>.inst.RemoveTankOverlay(m_Overlay);
		}
		m_Overlay = Singleton.Manager<ManOverlay>.inst.AddTankOverlay(this, data);
	}

	public float GetForwardSpeed()
	{
		float num = 0f;
		if (grounded)
		{
			return Mathf.Abs(rootBlockTrans.InverseTransformDirection(rbody.velocity).z);
		}
		return rbody.velocity.magnitude;
	}

	private bool OnRemoveFromGame()
	{
		NetPlayer previouslyOwningPlayer = (netTech.IsNotNull() ? netTech.NetPlayer : null);
		if ((object)Singleton.playerTank == this)
		{
			Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(null);
			if (!Singleton.Manager<ManPointer>.inst.IsDraggingController)
			{
				GamepadVibration.VibratePad(GamepadVibration.Type.PlayerTankDestroyed, hasPriority: true);
			}
		}
		bool result = true;
		if (netTech.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			result = netTech.RequestRemoveFromGame(previouslyOwningPlayer, wasKilled: true);
		}
		return result;
	}

	private void UpdateSceneryBlockerSize()
	{
		float magnitude = blockBounds.extents.SetY(0f).magnitude;
		Singleton.Manager<ManWorld>.inst.UpdateDynamicSceneryBlockerBoundsSphere(visible.ID, magnitude);
	}

	public void ShowMarker(bool show)
	{
		TrackedVisible trackedVisible = (visible ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.ID) : null);
		if (trackedVisible != null)
		{
			trackedVisible.RadarType = (show ? trackedVisible.DefaultRadarType : RadarTypes.Hidden);
			trackedVisible.RadarType = (show ? trackedVisible.DefaultRadarType : RadarTypes.Hidden);
		}
		if (ShouldShowOverlay != show)
		{
			ShouldShowOverlay = show;
			if (netTech != null && ManNetwork.IsNetworked)
			{
				netTech.OnServerSetShowOverlayDirty();
			}
		}
	}

	public void AddLandingGearImpl(ModuleLandingGear landingGear)
	{
		if (m_LandingGear.IsNull())
		{
			GameObject gameObject = new GameObject("LandingGear");
			gameObject.transform.SetParent(base.transform, worldPositionStays: false);
			m_LandingGear = gameObject.AddComponent<LandingGear>();
		}
		m_LandingGear.Add(landingGear);
	}

	public void RemoveLandingGearImpl(ModuleLandingGear landingGear)
	{
		m_LandingGear.Remove(landingGear);
		if (m_LandingGear.Count == 0)
		{
			UnityEngine.Object.Destroy(m_LandingGear.gameObject);
			m_LandingGear = null;
		}
	}

	public void AddActiveStabiliser(ModuleStabiliser stabiliser)
	{
		m_ActiveStabilisers++;
	}

	public void RemoveActiveStabiliser(ModuleStabiliser stabiliser)
	{
		m_ActiveStabilisers--;
	}

	private void UpdateForceOverTime()
	{
		for (int num = forcesOverTime.Count - 1; num >= 0; num--)
		{
			ForceOverTime value = forcesOverTime[num];
			visible.rbody.AddForceAtPosition(value.Force, value.Position, ForceMode.Force);
			value.Duration -= Time.fixedDeltaTime;
			if (value.Duration <= 0f)
			{
				forcesOverTime.RemoveAt(num);
			}
			else
			{
				forcesOverTime[num] = value;
			}
		}
	}

	public void ApplyForceOverTime(Vector3 impulse, Vector3 position, float duration)
	{
		if (duration <= 0f)
		{
			visible.rbody.AddForceAtPosition(impulse, position, ForceMode.Impulse);
			return;
		}
		forcesOverTime.Add(new ForceOverTime
		{
			Force = impulse / duration,
			Position = position,
			Duration = duration
		});
	}

	private void OnPool()
	{
		rbody = GetComponent<Rigidbody>();
		if (rbody == null)
		{
			rbody = null;
		}
		EnableGravity = true;
		trans = base.transform;
		visible = GetComponent<Visible>();
		netTech = GetComponent<NetTech>();
		ShouldExplodeDetachingBlocks = false;
		ExplodeDetachingBlocksDelay = 1f;
		control = GetComponent<TankControl>();
		beam = GetComponent<TankBeam>();
		blockman = GetComponent<BlockManager>();
		EnergyRegulator = GetComponent<TechEnergy>();
		Vision = GetComponent<TechVision>();
		Anchors = GetComponent<TechAnchors>();
		Holders = GetComponent<TechHolders>();
		Weapons = GetComponent<TechWeapon>();
		Boosters = GetComponent<TechBooster>();
		HUDControl = GetComponent<TechHUDControl>();
		AI = GetComponent<TechAI>();
		Radar = GetComponent<TechRadar>();
		QuestGiver = GetComponent<TechQuestGiver>();
		TechAudio = GetComponent<TechAudio>();
		Sequencer = GetComponent<TechSequencer>();
		BlockStateController = GetComponent<TechBlockStateController>();
		RadarMarker = GetComponent<TechRadarMarker>();
		Circuits = GetComponent<TechCircuits>();
		TechOpenMenuEventConsumer = GetComponents<ManPointer.OpenMenuEventConsumer>().FirstOrDefault();
		visible.RegisterRemovalCallback(OnRemoveFromGame);
		m_SafeArea = new ManFreeSpace.TechSafeArea(base.transform, Singleton.Manager<ManFreeSpace>.inst.DefaultSafeAreaRadius);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManTechs>.inst.RegisterTank(this);
		m_Overlay = Singleton.Manager<ManOverlay>.inst.AddTankOverlay(this);
		FirstUpdateAfterSpawn = true;
		rbody.isKinematic = false;
		EnableGravity = true;
		rbody.velocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
		rbody.useGravity = true;
		prevVelocity = Vector3.zero;
		prevAngVelocity = Vector3.zero;
		preserveMomentum = 0f;
		ShouldExplodeDetachingBlocks = false;
		ExplodeDetachingBlocksDelay = 1f;
		ResetEvent.Send(0);
		ControlsActive = true;
		IsSleeping = false;
		CentralBlock = null;
		DamagedByPlayer = false;
		OriginalValue = 0f;
		MainCorps = null;
		m_Team = int.MaxValue;
		ShouldShowOverlay = true;
		m_Creator = "";
		FatalDamage = ManDamage.DamageInfo.CreateNull();
		m_SafeArea.m_Radius = Singleton.Manager<ManGameMode>.inst.GetSafeAreaRadius();
		SceneryBlocker blocker = SceneryBlocker.CreateSphereBlocker(SceneryBlocker.BlockMode.Regrow, WorldPosition.FromScenePosition(Vector3.zero), 0f, this);
		Singleton.Manager<ManWorld>.inst.AddDynamicSceneryBlocker(visible.ID, blocker);
	}

	private void OnRecycle()
	{
		if (!visible.Killed)
		{
			List<Visible> list = null;
			TechHolders.HolderIterator enumerator = Holders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.Contents.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Visible current = enumerator2.Current;
					if (list == null)
					{
						list = new List<Visible>();
					}
					list.Add(current);
				}
			}
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Visible obj = list[i];
					obj.SetHolder(null, notifyRelease: true, isBeingRecycled: true);
					obj.trans.Recycle();
				}
			}
		}
		if (blockman.blockCount != 0)
		{
			blockman.RecycleAll();
		}
		blockman.ClearLastRemovedBlocks();
		Singleton.Manager<ManWorld>.inst.RemoveDynamicSceneryBlocker(visible.ID);
		Singleton.Manager<ManOverlay>.inst.RemoveTankOverlay(m_Overlay);
		m_Overlay = null;
		Singleton.Manager<ManTechs>.inst.UnregisterTank(this);
		if (NeedsSafeArea || Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManFreeSpace>.inst.RemoveSafeArea(m_SafeArea);
		}
		TankRecycledEvent.Send(this);
		if ((object)this == Singleton.playerTank)
		{
			Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(null);
		}
		SetName("recycled tech");
		Author = "";
		if (m_LandingGear.IsNotNull())
		{
			UnityEngine.Object.Destroy(m_LandingGear.gameObject);
			m_LandingGear = null;
		}
		d.AssertFormat(m_ActiveStabilisers == 0, "Tank being recycled ('{0}') still has active stabilisers. This should not be possible!", this);
		m_ActiveStabilisers = 0;
		AttachEvent.EnsureNoSubscribers();
		DetachEvent.EnsureNoSubscribers();
		AnchorEvent.EnsureNoSubscribers();
		SleepEvent.EnsureNoSubscribers();
		ResetEvent.EnsureNoSubscribers();
		FixupAnchorEvent.EnsureNoSubscribers();
		ResetPhysicsEvent.EnsureNoSubscribers();
		SerializeEvent.EnsureNoSubscribers();
		TriggerEvent.EnsureNoSubscribers();
		CollisionEvent.EnsureNoSubscribers();
		PostSpawnEvent.EnsureNoSubscribers();
		DamageEvent.EnsureNoSubscribers();
		TankRecycledEvent.Clear();
		d.Assert(!TankRecycledEvent.HasSubscribers());
	}

	private void OnDepool()
	{
		visible.RegisterRemovalCallback(null);
		rbody = null;
		trans = null;
		visible = null;
		netTech = null;
		control = null;
		beam = null;
		blockman = null;
		EnergyRegulator = null;
		Vision = null;
		Anchors = null;
		Holders = null;
		Weapons = null;
		Boosters = null;
		HUDControl = null;
		AI = null;
		Radar = null;
		QuestGiver = null;
		TechAudio = null;
		Sequencer = null;
		BlockStateController = null;
		RadarMarker = null;
		Circuits = null;
		m_SafeArea = null;
	}

	private void Update()
	{
		Holders.DoHeartbeat();
		if (rbody.IsSleeping())
		{
			rbody.WakeUp();
		}
		if (!IsFriendly(0))
		{
			Singleton.Manager<ManTechs>.inst.CheckSleepRange(this);
		}
		if (visible.KillVolumeCheck())
		{
			d.LogError("Tank '" + base.name + "' hit KillVolumeCheck!");
			bool flag = true;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				flag = netTech.IsNull() || (netTech.NetPlayer.IsNull() && ManNetwork.IsHost) || (netTech.NetPlayer.IsNotNull() && netTech.NetPlayer.IsActuallyLocalPlayer);
			}
			if (flag)
			{
				if (blockman.blockCount == 0)
				{
					d.LogError("Tech '" + base.name + "' hit Emergency kill volume! Tech had no blocks in it; Remove it from the game!");
					visible.RemoveFromGame();
					return;
				}
				Vector3 scenePos = trans.position;
				if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos))
				{
					d.LogError("Tech '" + base.name + "' hit Emergency kill volume! (Tech was still on tile - teleporting back to ground level)");
					PositionBaseCentred(scenePos);
				}
				else
				{
					d.LogError("Tech '" + base.name + "' hit Emergency kill volume! (Tech was not on any tile.. !?)");
				}
			}
		}
		Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(visible);
		UpdateEvent.Send();
	}

	private void FixedUpdate()
	{
		if (m_AwaitingPhysicsReset)
		{
			ResetPhysics();
		}
		if (m_AwaitingCentreOfGravityUpdate)
		{
			CalculateCentreOfGravity();
		}
		if (rbody.isKinematic)
		{
			prevVelocity = Vector3.zero;
			prevAngVelocity = Vector3.zero;
			acceleration = Vector3.zero;
		}
		else
		{
			UpdateForceOverTime();
			float num = m_TotalWeightScale / rbody.mass;
			if (EnableGravity)
			{
				if (Mathf.Approximately(num, 1f))
				{
					rbody.useGravity = true;
				}
				else
				{
					rbody.useGravity = false;
				}
			}
			else
			{
				rbody.useGravity = false;
			}
			rbody.drag = rbody.velocity.sqrMagnitude * airSpeedDragFactor;
			float num2 = Mathf.Lerp(antiGravAirSpeedAngularDragFactor, airSpeedAngularDragFactor, num);
			float num3 = Mathf.Lerp(antiGravConstantAngularDragFactor, constantAngularDragFactor, num);
			rbody.angularDrag = rbody.angularVelocity.sqrMagnitude * rbody.angularVelocity.sqrMagnitude * num2 + num3;
			if (preserveMomentum != 0f)
			{
				rbody.velocity = Vector3.Lerp(rbody.velocity, prevVelocity, preserveMomentum);
				rbody.angularVelocity = Vector3.Lerp(rbody.angularVelocity, prevAngVelocity, preserveMomentum);
				preserveMomentum = 0f;
			}
			else
			{
				acceleration = (rbody.velocity - prevVelocity) / Time.deltaTime;
				prevVelocity = rbody.velocity;
				prevAngVelocity = rbody.angularVelocity;
			}
		}
		FixedUpdateEvent.Send();
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		d.Assert((object)otherCollider.GetComponent<Rigidbody>() != rbody);
		TriggerEvent.Send(otherCollider, 0);
	}

	private void OnTriggerExit(Collider otherCollider)
	{
		d.Assert((object)otherCollider.GetComponent<Rigidbody>() != rbody);
		TriggerEvent.Send(otherCollider, 2);
	}

	private void OnCollisionEnter(Collision collision)
	{
		HandleCollision(collision, stay: false);
	}

	private void OnCollisionStay(Collision collision)
	{
		HandleCollision(collision, stay: true);
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		Vector3 vector = new Vector3(0f, -20f, 0f);
		if (flags.Contains(3))
		{
			DebugGui.LabelWorld((m_TotalWeightScale / rbody.mass).ToString("+"), Color.green, trans.TransformPoint(m_CogPos), DebugGui.BGMode.Shadowed);
			Vector3 vector2 = Singleton.camera.WorldToScreenPoint(trans.TransformPoint(m_CogPos)) + vector;
			if (vector2.z >= 0f)
			{
				DebugGui.LabelScreen((m_TotalWeightScale / rbody.mass).ToString("0.00"), Color.green, vector2, DebugGui.BGMode.BoxedShadowed);
			}
		}
		if (flags.Contains(4))
		{
			DebugGui.LabelWorld(rbody.mass.ToString("+"), Color.red, trans.TransformPoint(rbody.centerOfMass), DebugGui.BGMode.Shadowed);
			Vector3 vector3 = Singleton.camera.WorldToScreenPoint(trans.TransformPoint(rbody.centerOfMass)) + vector;
			if (vector3.z >= 0f)
			{
				DebugGui.LabelScreen(rbody.mass.ToString("0.00"), Color.red, vector3, DebugGui.BGMode.BoxedShadowed);
			}
		}
		if (flags.Contains(5))
		{
			Vector3 vector4 = Singleton.camera.WorldToScreenPoint(trans.TransformPoint(rbody.centerOfMass)) + vector + vector;
			if (vector4.z >= 0f)
			{
				DebugGui.LabelScreen($"{rbody.inertiaTensor.x:0.00} {rbody.inertiaTensor.y:0.00} {rbody.inertiaTensor.z:0.00}", Color.cyan, vector4, DebugGui.BGMode.BoxedShadowed);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		if (Application.IsPlaying(base.gameObject) && base.gameObject.EditorSelectedSingle())
		{
			Gizmos.DrawSphere(WorldTop, 0.2f);
			Matrix4x4 matrix = (Gizmos.matrix = trans.localToWorldMatrix);
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(blockBounds.center, blockBounds.size);
			Gizmos.matrix = matrix;
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(rbody.centerOfMass, 0.15f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(m_CogPos, 0.15f);
		}
	}

	public float GetGravityScale()
	{
		if (EnableGravity)
		{
			return m_TotalWeightScale / rbody.mass;
		}
		return 0f;
	}

	public Rigidbody GetApplicationRigidbody()
	{
		return rbody;
	}

	public Vector3 GetWorldCentreOfGravity()
	{
		return trans.TransformPoint(m_CogPos);
	}

	public bool CanApplyGravity()
	{
		return true;
	}

	public void SetApplicationTouched(bool touched)
	{
		m_GravityApplicationTouched = touched;
	}

	public bool HasApplicationBeenTouched()
	{
		return m_GravityApplicationTouched;
	}

	public void SetCustomMaterialOverride(bool enable, ManTechMaterialSwap.MatType materialType)
	{
		if (enable)
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.SetCustomMaterialOverride(materialType);
			}
		}
		else
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.RevertCustomMaterialOverride();
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && netTech != null)
		{
			netTech.SetMaterialOverrideType(enable, materialType);
		}
	}
}
