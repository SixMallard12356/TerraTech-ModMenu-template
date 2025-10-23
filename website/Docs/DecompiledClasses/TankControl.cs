#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Rewired;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TankControl : TechComponent, ManPointer.MouseEventConsumer, ManPointer.OpenMenuEventConsumer
{
	public enum DriveRestriction
	{
		None,
		ForwardOnly,
		ReverseOnly
	}

	public enum ControlContribution
	{
		Rotation = 1,
		Movement,
		Both
	}

	public class ControlState
	{
		public State m_State;

		public Vector3 InputRotation => m_State.m_InputRotation;

		public Vector3 InputMovement => m_State.m_InputMovement;

		public Vector3 Throttle => m_State.m_ThrottleValues;

		public bool BoostJets => m_State.m_BoostJets;

		public bool BoostProps => m_State.m_BoostProps;

		public bool Fire => m_State.m_Fire;

		public bool AnyMovementControl
		{
			get
			{
				if (!(InputRotation != Vector3.zero))
				{
					return InputMovement != Vector3.zero;
				}
				return true;
			}
		}

		public bool AnyControl
		{
			get
			{
				if (!(InputRotation != Vector3.zero) && !(InputMovement != Vector3.zero) && !Fire && !BoostProps)
				{
					return BoostJets;
				}
				return true;
			}
		}

		public bool AnyMovementOrBoostControl
		{
			get
			{
				if (!(InputRotation != Vector3.zero) && !(InputMovement != Vector3.zero) && !BoostProps)
				{
					return BoostJets;
				}
				return true;
			}
		}

		public void Reset()
		{
			m_State = default(State);
		}
	}

	public struct State
	{
		public Vector3 m_InputMovement;

		public Vector3 m_InputRotation;

		public Vector3 m_ThrottleValues;

		public bool m_BoostProps;

		public bool m_BoostJets;

		public bool m_Fire;

		public bool m_Beam;

		public float Drive => m_InputMovement.z;

		public float Turn => m_InputRotation.y;

		public float Pitch => m_InputRotation.x;

		public float Roll => m_InputRotation.z;

		public static bool operator ==(State s1, State s2)
		{
			if (s1.m_BoostProps == s2.m_BoostProps && s1.m_BoostJets == s2.m_BoostJets && s1.m_InputMovement == s2.m_InputMovement && s1.m_InputRotation == s2.m_InputRotation && s1.m_ThrottleValues == s2.m_ThrottleValues && s1.m_Fire == s2.m_Fire)
			{
				return s1.m_Beam == s2.m_Beam;
			}
			return false;
		}

		public static bool operator !=(State s1, State s2)
		{
			return !(s1 == s2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is State))
			{
				return false;
			}
			return (State)obj == this;
		}

		public override int GetHashCode()
		{
			return m_BoostProps.GetHashCode() ^ m_InputMovement.GetHashCode() ^ (m_InputRotation.GetHashCode() >> 1) ^ (m_BoostJets.GetHashCode() << 1) ^ (m_Fire.GetHashCode() << 2) ^ (m_Beam.GetHashCode() << 3);
		}

		private static void WriteThrottleOrMovement(ref Util.BitPacker packer, float throttle, float movement, int bitCount)
		{
			packer.Write(throttle != 0f);
			packer.WriteFloatPlusMinus1((throttle != 0f) ? throttle : movement, bitCount - 1);
		}

		private static void ReadThrottleOrMovement(ref Util.BitUnpacker unpacker, out float throttle, out float movement, int bitCount)
		{
			bool flag = unpacker.ReadBool();
			float num = unpacker.ReadFloatPlusMinus1(bitCount - 1);
			throttle = (flag ? num : 0f);
			movement = (flag ? 0f : num);
		}

		public void NetSerialize(NetworkWriter writer)
		{
			Util.BitPacker packer = default(Util.BitPacker);
			packer.Write(m_BoostProps);
			packer.Write(m_BoostJets);
			packer.Write(m_Fire);
			packer.Write(m_Beam);
			WriteThrottleOrMovement(ref packer, m_ThrottleValues.x, m_InputMovement.x, 5);
			WriteThrottleOrMovement(ref packer, m_ThrottleValues.y, m_InputMovement.y, 5);
			WriteThrottleOrMovement(ref packer, m_ThrottleValues.z, m_InputMovement.z, 5);
			packer.WriteFloatPlusMinus1(m_InputRotation.x, 4);
			packer.WriteFloatPlusMinus1(m_InputRotation.y, 4);
			packer.WriteFloatPlusMinus1(m_InputRotation.z, 4);
			writer.Write(packer.m_Data);
		}

		public void NetDeserialize(NetworkReader reader)
		{
			uint data = reader.ReadUInt32();
			Util.BitUnpacker unpacker = new Util.BitUnpacker(data);
			m_BoostProps = unpacker.ReadBool();
			m_BoostJets = unpacker.ReadBool();
			m_Fire = unpacker.ReadBool();
			m_Beam = unpacker.ReadBool();
			ReadThrottleOrMovement(ref unpacker, out m_ThrottleValues.x, out m_InputMovement.x, 5);
			ReadThrottleOrMovement(ref unpacker, out m_ThrottleValues.y, out m_InputMovement.y, 5);
			ReadThrottleOrMovement(ref unpacker, out m_ThrottleValues.z, out m_InputMovement.z, 5);
			m_InputRotation.x = unpacker.ReadFloatPlusMinus1(4);
			m_InputRotation.y = unpacker.ReadFloatPlusMinus1(4);
			m_InputRotation.z = unpacker.ReadFloatPlusMinus1(4);
		}
	}

	[Serializable]
	public new class SerialData : SerialData<SerialData>
	{
		[NonSerialized]
		[JsonIgnore]
		public bool m_FromSnapshot;

		public List<ControlScheme> m_Schemes;

		public Vector3 m_Throttle;
	}

	public Transform targetProjector;

	public Transform crosshairProjector;

	public float projectorHeight = 4f;

	public float stickRadius = 0.1f;

	public float stickRadiusInner = 0.07f;

	public Vector3 stickDefaultScreenPosLeft = new Vector3(0.25f, 0.75f);

	public Texture stickTexture;

	public Texture stickTextureFilled;

	public Movement m_Movement;

	public Weapons m_Weapons;

	[SerializeField]
	private float m_ThrottleSpeed = 0.2f;

	[SerializeField]
	private AnimationCurve m_ThrottleCurve;

	public Event<ControlState> driveControlEvent;

	public Event<bool, int> axesWarningEvent;

	public Event<int, bool> manualAimFireEvent;

	public Event<Vector3, float> targetedAimFireEvent;

	public EventNoParams[] explosiveBoltDetonateEvents = new EventNoParams[4];

	private ControlState m_ControlState = new ControlState();

	private bool m_BeamToggled;

	private bool m_DeferUpdateSchemeHUD;

	private float tankSelectDoubleTapTimer;

	private Player m_RewiredPlayer;

	private List<ControlScheme> m_SerialisedSchemes;

	private List<ControlScheme> m_NetSchemes;

	private List<ControlScheme> m_Schemes;

	private bool m_SerialisedSchemesFromSnapshot;

	private int aimControlManual;

	private bool m_ExplosiveBoltActivationControl;

	private bool m_AnchorToggleControl;

	private bool m_ExternalExplosiveBoltActivationControl;

	private bool m_HandlesPlayerInput;

	private List<ModuleTechController> m_AllControllers = new List<ModuleTechController>(5);

	private List<ModuleTechController> m_AdditiveControllers;

	private HashSet<int> m_ThrottleAxisEffectorHashes = new HashSet<int>();

	private int[] m_ThrottleAxisEnableCount = new int[6];

	private int[] m_DriveToThrottleAxisEnableCount = new int[6];

	private Vector3 m_ThrottleValues;

	private Vector3 m_ThrottleLastInput;

	private Vector3 m_ThrottleTiming;

	private int m_CollectedInputCount;

	private Vector3 m_CollectedMovementInput;

	private Vector3 m_CollectedRotationInput;

	private Vector3 m_CollectedThrottleInput;

	private bool m_CollectedBoostPropsInput;

	private bool m_CollectedBoostJetsInput;

	private bool m_PlayerTriggeredBoost;

	private bool m_PlayerTriggeredFire;

	public ObjectTypes targetType { get; set; }

	public bool HasController => m_AllControllers.Count != 0;

	public ModuleTechController FirstController
	{
		get
		{
			if (m_AllControllers.Count == 0)
			{
				return null;
			}
			return m_AllControllers[0];
		}
	}

	public int NumControllers => m_AllControllers.Count;

	public IEnumerable<ModuleTechController> AllControllers => m_AllControllers;

	public Movement Movement => m_Movement;

	public Weapons Weapons => m_Weapons;

	public List<ControlScheme> Schemes
	{
		get
		{
			return m_Schemes;
		}
		set
		{
			m_Schemes = value;
		}
	}

	public ControlScheme ActiveScheme
	{
		get
		{
			if (m_Schemes != null && m_Schemes.Count != 0)
			{
				return m_Schemes[0];
			}
			return null;
		}
	}

	public float DriveControl
	{
		set
		{
			m_CollectedMovementInput.z += value;
			m_CollectedInputCount++;
		}
	}

	public float TurnControl
	{
		set
		{
			m_CollectedRotationInput.y += value;
			m_CollectedInputCount++;
		}
	}

	public float PitchControl
	{
		set
		{
			m_CollectedRotationInput.x += value;
			m_CollectedInputCount++;
		}
	}

	public float RollControl
	{
		set
		{
			m_CollectedRotationInput.z += value;
			m_CollectedInputCount++;
		}
	}

	public bool BoostControlProps
	{
		set
		{
			m_CollectedBoostPropsInput |= value;
		}
	}

	public bool BoostControlJets
	{
		set
		{
			m_CollectedBoostJetsInput |= value;
		}
	}

	public bool FireControl
	{
		get
		{
			return m_ControlState.m_State.m_Fire;
		}
		set
		{
			m_ControlState.m_State.m_Fire = value;
		}
	}

	public Vector3 TargetPositionWorld { get; set; }

	public float TargetRadiusWorld { get; set; }

	public bool HandlesPlayerInput => m_HandlesPlayerInput;

	public State CurState
	{
		get
		{
			return m_ControlState.m_State;
		}
		set
		{
			m_ControlState.m_State = value;
		}
	}

	public bool HasAnyExplosiveBolts
	{
		get
		{
			for (int i = 0; i < explosiveBoltDetonateEvents.Length; i++)
			{
				if (HasExplosiveBolts(i + 1))
				{
					return true;
				}
			}
			return false;
		}
	}

	private bool Locked
	{
		get
		{
			if (base.Tech.IsNotNull() && base.Tech.netTech.IsNotNull())
			{
				return !base.Tech.netTech.NetIdentity.HasEffectiveAuthority();
			}
			return false;
		}
	}

	public bool HasExplosiveBolts(int order)
	{
		if (order > explosiveBoltDetonateEvents.Length || order < 1)
		{
			d.LogError("Attempted to get whether explosive bolt with order '" + order + "' exists, but that order is out of bounds");
			return false;
		}
		return explosiveBoltDetonateEvents[order - 1].HasSubscribers();
	}

	public void AddController(ModuleTechController controller)
	{
		int num = (HasController ? FirstController.GetPriority() : 0);
		m_AllControllers.Add(controller);
		m_AllControllers.Sort((ModuleTechController x, ModuleTechController y) => y.GetPriority() - x.GetPriority());
		if (controller.IsAdditive)
		{
			if (m_AdditiveControllers == null)
			{
				m_AdditiveControllers = new List<ModuleTechController>(2);
			}
			m_AdditiveControllers.Add(controller);
		}
		if (controller.GetPriority() > num)
		{
			base.Tech.blockman.SetRootBlock(controller.block);
		}
	}

	public void RemoveController(ModuleTechController controller)
	{
		m_AllControllers.Remove(controller);
		if (controller.IsAdditive)
		{
			m_AdditiveControllers.Remove(controller);
		}
	}

	public void SetControllerHandlesInput(bool handlesInput)
	{
		m_HandlesPlayerInput = handlesInput;
	}

	public bool GetThrottle(int axisPair, out float throttle)
	{
		int num = axisPair * 2;
		if (m_ThrottleAxisEnableCount[num] > 0 || m_ThrottleAxisEnableCount[num + 1] > 0)
		{
			throttle = m_ThrottleValues[axisPair];
			return true;
		}
		throttle = 0f;
		return false;
	}

	public bool AnyThrottleInAxes(Vector3 axes)
	{
		if (Mathf.Abs(axes.x) > 0.01f)
		{
			GetThrottle(0, out var throttle);
			if (throttle != 0f)
			{
				return true;
			}
		}
		if (Mathf.Abs(axes.y) > 0.01f)
		{
			GetThrottle(1, out var throttle2);
			if (throttle2 != 0f)
			{
				return true;
			}
		}
		if (Mathf.Abs(axes.z) > 0.01f)
		{
			GetThrottle(2, out var throttle3);
			if (throttle3 != 0f)
			{
				return true;
			}
		}
		return false;
	}

	public Vector3 ZeroWorldVelocityInThrottledAxes(Tank tank, Vector3 velWorld)
	{
		Transform rootBlockTrans = tank.rootBlockTrans;
		GetThrottle(0, out var throttle);
		if (throttle != 0f)
		{
			velWorld -= rootBlockTrans.right * Vector3.Dot(velWorld, rootBlockTrans.right);
		}
		GetThrottle(1, out throttle);
		if (throttle != 0f)
		{
			velWorld -= rootBlockTrans.up * Vector3.Dot(velWorld, rootBlockTrans.up);
		}
		GetThrottle(2, out throttle);
		if (throttle != 0f)
		{
			velWorld -= rootBlockTrans.forward * Vector3.Dot(velWorld, rootBlockTrans.forward);
		}
		return velWorld;
	}

	public Vector3 ZeroLocalVelocityInThrottledAxes(Vector3 velLocal)
	{
		GetThrottle(0, out var throttle);
		if (throttle != 0f)
		{
			velLocal.x = 0f;
		}
		GetThrottle(1, out throttle);
		if (throttle != 0f)
		{
			velLocal.y = 0f;
		}
		GetThrottle(2, out throttle);
		if (throttle != 0f)
		{
			velLocal.z = 0f;
		}
		return velLocal;
	}

	public void SetControllerIndex(int index)
	{
		if (index != -1)
		{
			m_RewiredPlayer = ReInput.players.GetPlayer(index);
		}
	}

	public void PlayerInput()
	{
		if (Singleton.Manager<CameraManager>.inst.IsCurrent<TankCamera>() || (Singleton.Manager<CameraManager>.inst.IsCurrent<DebugCamera>() && Singleton.Manager<CameraManager>.inst.GetCamera<DebugCamera>().IsLocked))
		{
			GetInput();
		}
	}

	public bool TestBoostControl()
	{
		if (!m_ControlState.m_State.m_BoostProps && !m_CollectedBoostPropsInput && !m_ControlState.m_State.m_BoostJets)
		{
			return m_CollectedBoostJetsInput;
		}
		return true;
	}

	public bool TestAnyControl()
	{
		if (!m_ControlState.AnyControl)
		{
			return m_BeamToggled;
		}
		return true;
	}

	public void OnMouseEvent(ManPointer.Event mouseEvent, bool down)
	{
		if ((bool)Singleton.Manager<ManPointer>.inst.targetTank)
		{
			bool flag = (mouseEvent == ManPointer.Event.LMB && Singleton.Manager<ManPointer>.inst.targetTank.ControllableByLocalPlayer) || (mouseEvent == ManPointer.Event.RMB && Input.GetKey(KeyCode.LeftAlt) && Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && DebugUtil.DebugCtrlKeyHeld());
			if (down && flag && !Singleton.Manager<ManPointer>.inst.IsInteractionBlocked && Singleton.Manager<ManNetTechs>.inst.CanSwitchToTech(Singleton.Manager<ManPointer>.inst.targetTank))
			{
				if (tankSelectDoubleTapTimer > 0f || mouseEvent != ManPointer.Event.LMB)
				{
					if (Singleton.playerTank == null || !Singleton.Manager<ManGameMode>.inst.LockPlayerControls)
					{
						d.Log("switching player control to: " + Singleton.Manager<ManPointer>.inst.targetTank.name);
						Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(Singleton.Manager<ManPointer>.inst.targetTank);
						Singleton.Manager<ManPointer>.inst.targetTank.control.targetType = ObjectTypes.Vehicle;
					}
				}
				else
				{
					tankSelectDoubleTapTimer = Singleton.instance.globals.doubleTapDelay;
				}
			}
			bool flag2 = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCreative || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Creative;
			if (!(mouseEvent == ManPointer.Event.LMB && down && flag2) || !m_RewiredPlayer.GetButton(80) || Singleton.Manager<ManPointer>.inst.IsInteractionBlocked || Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock)
			{
				return;
			}
			Tank targetTank = Singleton.Manager<ManPointer>.inst.targetTank;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (targetTank == Singleton.playerTank)
				{
					Singleton.Manager<ManNetwork>.inst.MyPlayer.RequestCycleTeam();
				}
				else if (targetTank.netTech.NetPlayer.IsNull())
				{
					targetTank.netTech.RequestCycleTeam(overrideHostRestrictions: true);
				}
			}
			else if (targetTank != Singleton.playerTank)
			{
				if (!ManSpawn.IsPlayerTeam(targetTank.Team))
				{
					targetTank.SetTeam(0);
					targetTank.AI.SetBehaviorType(AITreeType.AITypes.Idle);
				}
				else
				{
					targetTank.SetTeam(1);
					targetTank.AI.SetOldBehaviour();
				}
			}
		}
		else
		{
			d.LogError("Target tank is null while processing tank control mouse event, how did this happen?");
		}
	}

	public bool CanOpenContextMenuForBlock(TankBlock block)
	{
		if (!(block != null) || !(block.tank != null) || !(block.tank == Singleton.playerTank))
		{
			if (!block.ContextMenuForPlayerTechOnly)
			{
				return block.HasContextMenu;
			}
			return false;
		}
		return true;
	}

	public bool CanOpenMenu(bool isRadial)
	{
		bool result = false;
		if (isRadial)
		{
			bool num = base.Tech.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam;
			bool flag = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
			bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
			result = num && flag && !isInteractionBlocked;
		}
		return result;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData radialMenuPair)
	{
		bool result = false;
		if (radialMenuPair.m_AllowRadialMenu)
		{
			TankBlock targetTankBlock = radialMenuPair.m_TargetTankBlock;
			if (CanOpenContextMenuForBlock(targetTankBlock))
			{
				if (targetTankBlock.HasContextMenu)
				{
					Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(targetTankBlock.ContextMenuType, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
				}
				else if (radialMenuPair.m_RadialInputController == ManInput.RadialInputController.Gamepad || !Singleton.Manager<ManPointer>.inst.DraggingItem)
				{
					if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
					{
						Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.MPTechActions, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
					}
					else if (targetTankBlock.tank != null && targetTankBlock.tank.RadarMarker.RadarMarkerConfig.IsUsed)
					{
						Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.TechAndBlockActions_RadarMarker, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
					}
					else
					{
						Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.TechAndBlockActions, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
					}
					result = true;
				}
			}
			else
			{
				if (targetTankBlock.tank != null && targetTankBlock.tank.RadarMarker.RadarMarkerConfig.IsUsed)
				{
					Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.TechControlChoice_RadarMarker, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
				}
				else
				{
					Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.TechControlChoice, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), radialMenuPair);
				}
				result = true;
			}
		}
		return result;
	}

	public Vector3 GetWeaponTargetLocation(Vector3 origin)
	{
		if (base.Tech.CentralBlock.IsNull())
		{
			if (!base.Tech.FirstUpdateAfterSpawn && (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || base.Tech.blockman.blockCount != 0))
			{
				DebugUtil.AssertRelease(condition: false, "GetClosestController no CentralBlock: " + base.name + ", BlockCount " + base.Tech.blockman.blockCount + ", Active: " + base.gameObject.activeInHierarchy.ToString());
			}
			return origin;
		}
		Vector3 vector = base.Tech.CentralBlock.centreOfMassWorld;
		if (NumControllers > 1 || base.Tech.Anchors.NumSkyAnchored > 0)
		{
			float sqrMagnitude = (vector - origin).sqrMagnitude;
			foreach (ModuleTechController allController in m_AllControllers)
			{
				if ((object)base.Tech.CentralBlock != allController)
				{
					Vector3 centreOfMassWorld = allController.block.centreOfMassWorld;
					if ((centreOfMassWorld - origin).sqrMagnitude < sqrMagnitude)
					{
						vector = centreOfMassWorld;
					}
				}
			}
			int numAnchored = base.Tech.Anchors.NumAnchored;
			for (int i = 0; i < numAnchored; i++)
			{
				ModuleAnchor anchored = base.Tech.Anchors.GetAnchored(i);
				if (anchored.IsSkyAnchor)
				{
					Vector3 groundPoint = anchored.Anchor.GroundPoint;
					if ((groundPoint - origin).sqrMagnitude < sqrMagnitude)
					{
						vector = groundPoint;
					}
				}
			}
		}
		return vector;
	}

	public void ServerDetonateExplosiveBolt()
	{
		m_ExternalExplosiveBoltActivationControl = true;
	}

	private void DetonateExplosiveBoltsOrdered(int order)
	{
		if (ManNetwork.IsHost)
		{
			explosiveBoltDetonateEvents[order - 1].Send();
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.DetonateExplosiveBolts, new EmptyMessage(), Singleton.Manager<ManNetwork>.inst.MyPlayer.netId);
		}
	}

	public void CheckSchemeIsSet()
	{
		if (ActiveScheme == null)
		{
			d.Assert(Singleton.Manager<ManProfile>.inst.GetCurrentUser() != null, "Man Profile current user is NULL!");
			if (m_NetSchemes != null)
			{
				m_Schemes = new List<ControlScheme>(m_NetSchemes);
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.UpdateControlSchemesForTech(m_Schemes, base.Tech.name, createMissing: true);
			}
			else if (m_SerialisedSchemes != null)
			{
				m_Schemes = new List<ControlScheme>(m_SerialisedSchemes);
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.UpdateControlSchemesForTech(m_Schemes, base.Tech.name, m_SerialisedSchemesFromSnapshot);
			}
			if (ActiveScheme == null)
			{
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.AssignDefaultControlSchemes(base.Tech);
			}
			d.Assert(ActiveScheme != null, "Current active scheme is null and shouldn't be!");
		}
	}

	public void CopySchemesFrom(TankControl other)
	{
		if (other.m_Schemes != null)
		{
			m_Schemes = new List<ControlScheme>(other.m_Schemes);
			SetActiveScheme(other.ActiveScheme);
		}
		else
		{
			m_Schemes = null;
		}
	}

	public void MergeSchemesFrom(TankControl other)
	{
		if (m_Schemes == null)
		{
			CopySchemesFrom(other);
		}
		else
		{
			if (other.m_Schemes == null)
			{
				return;
			}
			foreach (ControlScheme scheme in other.Schemes)
			{
				if (!m_Schemes.Exists((ControlScheme s) => s.Equals(scheme)))
				{
					m_Schemes.Add(scheme);
				}
			}
		}
	}

	public int GetActiveSchemeID()
	{
		if (ActiveScheme != null)
		{
			return ActiveScheme.ID;
		}
		return 0;
	}

	public void SetActiveSchemeFromID(int id, bool addIfMissing = false)
	{
		if (id != 0)
		{
			ControlScheme controlSchemeByID = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.GetControlSchemeByID(id);
			if (controlSchemeByID != null)
			{
				SetActiveScheme(controlSchemeByID, addIfMissing);
			}
		}
	}

	public void SetActiveScheme(ControlScheme s, bool addIfMissing = true)
	{
		d.Assert(s != null, "Active control scheme cannot be null");
		if (ActiveScheme != s && s != null)
		{
			d.LogFormat("Switching control scheme on {0} to {1}", base.Tech.name, s.GetName());
			if (m_Schemes == null)
			{
				m_Schemes = new List<ControlScheme>();
			}
			if (addIfMissing || m_Schemes.Contains(s))
			{
				m_Schemes.Remove(s);
				m_Schemes.Insert(0, s);
			}
			else
			{
				d.Log("Switching control scheme failed");
			}
		}
		if (Singleton.playerTank == base.Tech)
		{
			UpdateSchemeHUD();
			Singleton.Manager<ManTechs>.inst.PlayerTankControlSchemeChangedEvent.Send(ActiveScheme);
		}
	}

	public void CycleActiveScheme()
	{
		ControlSchemeLibrary controlSchemeLibrary = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary;
		int displaySortOrder = controlSchemeLibrary.GetDisplaySortOrder(ActiveScheme);
		ControlScheme controlScheme = null;
		int num = int.MaxValue;
		ControlScheme controlScheme2 = null;
		int num2 = int.MaxValue;
		foreach (ControlScheme scheme in m_Schemes)
		{
			int displaySortOrder2 = controlSchemeLibrary.GetDisplaySortOrder(scheme);
			if (displaySortOrder2 > displaySortOrder && displaySortOrder2 < num2)
			{
				num2 = displaySortOrder2;
				controlScheme2 = scheme;
			}
			else if (displaySortOrder2 < num)
			{
				num = displaySortOrder2;
				controlScheme = scheme;
			}
		}
		if (controlScheme2 != null)
		{
			SetActiveScheme(controlScheme2);
		}
		else if (controlScheme != null)
		{
			SetActiveScheme(controlScheme);
		}
	}

	public static Vector3 CalculateDirectionalContributionForRotation(Tank tank, Vector3 worldForcePoint, Quaternion localRotation)
	{
		if (tank.IsNull())
		{
			return Vector3.zero;
		}
		Vector3 vector = tank.trans.InverseTransformPoint(tank.rbody.worldCenterOfMass);
		Vector3 vector2 = tank.trans.InverseTransformPoint(worldForcePoint);
		localRotation.ToAngleAxis(out var angle, out var axis);
		Vector3 vector3 = axis.normalized * angle * ((float)Math.PI / 180f);
		return Vector3.Cross((vector2 - vector).normalized, vector3.normalized).normalized;
	}

	public static void GetInputEffect(Tank tank, Vector3 boostPosition, Vector3 boostDirection, out Vector3 rotationContribution, out Vector3 localBoostDirection, ControlContribution contribution = ControlContribution.Both)
	{
		d.Assert(tank.IsNotNull(), "GetInputEffect - Invalid null Tank argument passed in!");
		if (tank.IsNotNull())
		{
			Transform rootBlockTrans = tank.rootBlockTrans;
			Vector3 vector = tank.rbody.position - tank.trans.position;
			Vector3 vector2 = tank.rbody.worldCenterOfMass - vector;
			localBoostDirection = rootBlockTrans.InverseTransformDirection(boostDirection);
			if (Mathf.Abs(localBoostDirection.x) < 0.01f)
			{
				localBoostDirection.x = 0f;
			}
			if (Mathf.Abs(localBoostDirection.y) < 0.01f)
			{
				localBoostDirection.y = 0f;
			}
			if (Mathf.Abs(localBoostDirection.z) < 0.01f)
			{
				localBoostDirection.z = 0f;
			}
			localBoostDirection.Normalize();
			if ((contribution & ControlContribution.Rotation) != 0)
			{
				Vector3 vector3 = rootBlockTrans.InverseTransformDirection(boostPosition - vector2);
				Vector3 vector4 = Vector3.Cross(localBoostDirection, vector3.normalized);
				float num = Mathf.Max(Mathf.Max(Mathf.Abs(vector4.x), Mathf.Abs(vector4.y)), Mathf.Abs(vector4.z));
				if (num > 0f)
				{
					if (Mathf.Abs(vector4.x) < 0.1f)
					{
						vector4.x = 0f;
					}
					if (Mathf.Abs(vector4.y) < 0.1f)
					{
						vector4.y = 0f;
					}
					if (Mathf.Abs(vector4.z) < 0.1f)
					{
						vector4.z = 0f;
					}
					vector4 /= num;
					if ((contribution & ControlContribution.Movement) == 0)
					{
						float num2 = 3f;
						vector4.x = Mathf.Clamp(vector4.x * num2, -1f, 1f);
						vector4.y = Mathf.Clamp(vector4.y * num2, -1f, 1f);
						vector4.z = Mathf.Clamp(vector4.z * num2, -1f, 1f);
					}
				}
				rotationContribution = vector4;
			}
			else
			{
				rotationContribution = default(Vector3);
			}
		}
		else
		{
			rotationContribution = default(Vector3);
			localBoostDirection = default(Vector3);
		}
	}

	public bool HasAnyThrottleControlAxes()
	{
		if (m_ThrottleAxisEnableCount[0] <= 0 && m_ThrottleAxisEnableCount[1] <= 0 && m_ThrottleAxisEnableCount[2] <= 0 && m_ThrottleAxisEnableCount[3] <= 0 && m_ThrottleAxisEnableCount[4] <= 0)
		{
			return m_ThrottleAxisEnableCount[5] > 0;
		}
		return true;
	}

	public void AddThrottleControlEnabler(Transform effector, Vector3 localDirection, bool respondsToDriveInput = true)
	{
		if (m_ThrottleAxisEffectorHashes.Add(GetThrottleEnablerHash(effector, localDirection)))
		{
			ChangeThrottleControlEnabler(localDirection, 1, respondsToDriveInput);
			if (base.Tech.IsPlayer)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Throttle, this);
			}
		}
	}

	public void RemoveThrottleControlEnabler(Transform effector, Vector3 localDirection, bool respondsToDriveInput = true)
	{
		if (m_ThrottleAxisEffectorHashes.Remove(GetThrottleEnablerHash(effector, localDirection)))
		{
			ChangeThrottleControlEnabler(localDirection, -1, respondsToDriveInput);
			if (base.Tech.IsPlayer && !HasAnyThrottleControlAxes())
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Throttle, this);
			}
		}
	}

	private int GetThrottleEnablerHash(Transform effector, Vector3 localDirection)
	{
		return (17 * 31 + effector.GetHashCode()) * 31 + localDirection.GetHashCode();
	}

	public static int GetInputAxisBitfieldForRotation(Vector3 axis)
	{
		return ((axis.x != 0f) ? 8 : 0) | ((axis.y != 0f) ? 16 : 0) | ((axis.z != 0f) ? 32 : 0);
	}

	public static int GetInputAxisBitfieldForMovement(Vector3 axis)
	{
		return (int)(((axis.x != 0f) ? 1u : 0u) | (uint)((axis.y != 0f) ? 2 : 0)) | ((axis.z != 0f) ? 4 : 0);
	}

	public void UpdateAxesWarnings(bool show)
	{
		if (!axesWarningEvent.HasSubscribers())
		{
			return;
		}
		int num = 0;
		foreach (ControlScheme scheme in m_Schemes)
		{
			num |= scheme.GetAxisMappingBitfield();
		}
		axesWarningEvent.Send(show, num);
	}

	private void ChangeThrottleControlEnabler(Vector3 localDirection, int count, bool respondsToDriveInput)
	{
		ChangeThrottleAxisComponent(localDirection.x, 0, respondsToDriveInput);
		ChangeThrottleAxisComponent(localDirection.y, 2, respondsToDriveInput);
		ChangeThrottleAxisComponent(localDirection.z, 4, respondsToDriveInput);
		void ChangeThrottleAxisComponent(float localComponent, int axisOffset, bool mapMoveInputToThrottle)
		{
			if (Mathf.Abs(localComponent) > 0.1f)
			{
				int num = ((!(localComponent < 0f)) ? 1 : 0);
				int num2 = axisOffset + num;
				d.Assert(m_ThrottleAxisEnableCount[num2] + count >= 0, "TankControl: Mismatched add/remove throttle control");
				m_ThrottleAxisEnableCount[num2] += count;
				if (mapMoveInputToThrottle)
				{
					m_DriveToThrottleAxisEnableCount[num2] += count;
				}
				if (mapMoveInputToThrottle)
				{
					int num3 = axisOffset + (num2 + 1) % 2;
					m_ThrottleAxisEnableCount[num3] += count;
					if (mapMoveInputToThrottle)
					{
						m_DriveToThrottleAxisEnableCount[num3] += count;
					}
				}
			}
		}
	}

	private void ApplyThrottle(bool negAxisEnabled, bool posAxisEnabled, float input, ref float throttleValue, ref float lastInput, ref float inputTiming)
	{
		bool flag = input < 0f || throttleValue < 0f;
		if ((negAxisEnabled && flag) || (posAxisEnabled && !flag))
		{
			if (input * lastInput <= 0f)
			{
				inputTiming = 0f;
			}
			if (input != 0f)
			{
				float a = m_ThrottleCurve.Evaluate(inputTiming);
				a = Mathf.Min(a, input * input);
				float num = throttleValue;
				float num2 = Mathf.Sign(input) * Time.deltaTime * m_ThrottleSpeed * a;
				throttleValue = Mathf.Clamp(throttleValue + num2, -1f, 1f);
				if (num * throttleValue <= 0f && lastInput * input > 0f)
				{
					throttleValue = 0f;
				}
				inputTiming += Time.deltaTime;
			}
			lastInput = input;
		}
		else
		{
			throttleValue = 0f;
			lastInput = 0f;
			inputTiming = 0f;
		}
	}

	private void GetInput()
	{
		ControlScheme activeScheme = ActiveScheme;
		if (m_RewiredPlayer == null || activeScheme == null)
		{
			return;
		}
		if (m_BeamToggled || base.Tech.beam.IsActive)
		{
			UpdateAxesWarnings(base.Tech.beam.IsActive);
		}
		if (m_RewiredPlayer.GetButtonDown(26))
		{
			Singleton.Manager<ManPauseGame>.inst.TogglePauseGame();
		}
		if (m_RewiredPlayer.GetButtonDown(73) && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ControlSchema))
		{
			CycleActiveScheme();
			activeScheme = ActiveScheme;
		}
		Vector3 inputMovement = new Vector3(activeScheme.GetAxisMapping(MovementAxis.MoveX_MoveRight).ReadRewiredInput(m_RewiredPlayer), activeScheme.GetAxisMapping(MovementAxis.MoveY_MoveUp).ReadRewiredInput(m_RewiredPlayer), activeScheme.GetAxisMapping(MovementAxis.MoveZ_MoveForward).ReadRewiredInput(m_RewiredPlayer));
		Vector3 inputRotation = new Vector3(activeScheme.GetAxisMapping(MovementAxis.RotateX_PitchUp).ReadRewiredInput(m_RewiredPlayer), activeScheme.GetAxisMapping(MovementAxis.RotateY_YawLeft).ReadRewiredInput(m_RewiredPlayer), activeScheme.GetAxisMapping(MovementAxis.RotateZ_RollRight).ReadRewiredInput(m_RewiredPlayer));
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			Vector2 inputValue = new Vector2(inputRotation.y, inputMovement.z);
			inputValue = Globals.inst.m_DriveStickInputInterpreter.InterpretAnalogStickInput(inputValue);
			inputRotation.y = inputValue.x;
			inputMovement.z = inputValue.y;
		}
		bool boostProps = activeScheme.GetAxisMapping(MovementAxis.BoostPropellers).ReadRewiredInput(m_RewiredPlayer) > 0.01f;
		bool boostJets = (m_PlayerTriggeredBoost = activeScheme.GetAxisMapping(MovementAxis.BoostJets).ReadRewiredInput(m_RewiredPlayer) > 0.01f);
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < m_DriveToThrottleAxisEnableCount.Length; i++)
		{
			if (m_DriveToThrottleAxisEnableCount[i] > 0 && m_DriveToThrottleAxisEnableCount[i] >= m_ThrottleAxisEnableCount[i])
			{
				int index = i / 2;
				bool num = inputMovement[index] >= 0f;
				bool flag = i % 2 == 1;
				if (num == flag)
				{
					zero[index] += inputMovement[index];
				}
			}
		}
		CollectMovementInput(inputMovement, inputRotation, zero, boostProps, boostJets);
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndInvulnerable() || Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch)
		{
			FireControl = m_RewiredPlayer.GetButton(2);
			m_PlayerTriggeredFire = FireControl;
		}
		if (m_RewiredPlayer.GetButtonDown(3))
		{
			bool suppressInventory = Singleton.Manager<ManInput>.inst.IsCurrentInputSource(3, ControllerType.Joystick);
			ToggleBeamActivated(suppressInventory);
		}
		else if (m_RewiredPlayer.GetButtonDown(25))
		{
			bool flag2 = true;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				flag2 = Singleton.Manager<ManNetwork>.inst.InventoryAvailable;
			}
			if (flag2)
			{
				Singleton.Manager<ManPurchases>.inst.TogglePalette();
			}
		}
		else if (m_RewiredPlayer.GetButtonDown(74) && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPalette))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPalette);
		}
		m_ExplosiveBoltActivationControl = m_RewiredPlayer.GetButtonDown(45);
		m_AnchorToggleControl = m_RewiredPlayer.GetButtonDown(78);
		if (!Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			return;
		}
		if (m_RewiredPlayer.GetButtonDown(5) && Singleton.Manager<ManInput>.inst.GetCurrentUIInputMode() != UIInputMode.UISkinsPalettePanel)
		{
			if (Singleton.Manager<ManPointer>.inst.DraggingItem == null || Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock)
			{
				bool enable = !Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled;
				Singleton.Manager<ManPointer>.inst.EnableInteractionMode(enable);
			}
		}
		else if (m_RewiredPlayer.GetButtonDown(48))
		{
			OpenMenuEventData openMenuEventData = new OpenMenuEventData
			{
				m_RadialInputController = ManInput.RadialInputController.Gamepad,
				m_AllowRadialMenu = true,
				m_AllowNonRadialMenu = false
			};
			if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.InteractionMode))
			{
				UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.InteractionMode);
				if ((bool)hudElement && Singleton.playerTank != null)
				{
					hudElement.GetComponent<UIInteractionHUD>().PointToPos(Singleton.playerTank.WorldCenterOfMass);
				}
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MPTechActions, openMenuEventData);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechAndBlockActions, openMenuEventData);
			}
		}
		else if (m_RewiredPlayer.GetButtonDown(62) && !Singleton.Manager<ManUndo>.inst.UndoAvailable && Singleton.Manager<ManPointer>.inst.BuildMode != ManPointer.BuildingMode.PaintBlock)
		{
			BoostControlProps = true;
		}
	}

	public void ApplyMovementInput(Vector3 inputMovement, Vector3 inputRotation, Vector3 inputThrottle, bool boostProps, bool boostJets)
	{
		if (base.Tech.ControlsActive)
		{
			ApplyThrottle(m_ThrottleAxisEnableCount[0] > 0, m_ThrottleAxisEnableCount[1] > 0, inputThrottle.x, ref m_ThrottleValues.x, ref m_ThrottleLastInput.x, ref m_ThrottleTiming.x);
			ApplyThrottle(m_ThrottleAxisEnableCount[2] > 0, m_ThrottleAxisEnableCount[3] > 0, inputThrottle.y, ref m_ThrottleValues.y, ref m_ThrottleLastInput.y, ref m_ThrottleTiming.y);
			ApplyThrottle(m_ThrottleAxisEnableCount[4] > 0, m_ThrottleAxisEnableCount[5] > 0, inputThrottle.z, ref m_ThrottleValues.z, ref m_ThrottleLastInput.z, ref m_ThrottleTiming.z);
		}
		if ((ActiveScheme == null || !ActiveScheme.ReverseSteering) && (inputMovement.z < -0.01f || m_ThrottleValues[2] < -0.01f))
		{
			Vector3 forward = base.Tech.rootBlockTrans.forward;
			if (Vector3.Dot(base.Tech.rbody.velocity, forward) < 0f)
			{
				inputRotation.y *= -1f;
			}
		}
		m_ControlState.m_State.m_InputMovement = inputMovement;
		m_ControlState.m_State.m_InputRotation = inputRotation;
		m_ControlState.m_State.m_ThrottleValues = m_ThrottleValues;
		m_ControlState.m_State.m_BoostProps = boostProps;
		m_ControlState.m_State.m_BoostJets = boostJets;
	}

	private void ResetCollectedMovementInputs()
	{
		m_CollectedInputCount = 0;
		m_CollectedMovementInput = Vector3.zero;
		m_CollectedRotationInput = Vector3.zero;
		m_CollectedThrottleInput = Vector3.zero;
		m_CollectedBoostPropsInput = false;
		m_CollectedBoostJetsInput = false;
	}

	public void CollectMovementInput(Vector3 inputMovement, Vector3 inputRotation, Vector3 inputThrottle, bool boostProps, bool boostJets)
	{
		m_CollectedMovementInput += inputMovement;
		m_CollectedRotationInput += inputRotation;
		m_CollectedThrottleInput += inputThrottle;
		m_CollectedBoostPropsInput |= boostProps;
		m_CollectedBoostJetsInput |= boostJets;
		m_CollectedInputCount++;
	}

	private void ApplyCollectedMovementInputs()
	{
		if (m_CollectedInputCount > 1)
		{
			m_CollectedMovementInput.Clamp01();
			m_CollectedRotationInput.Clamp01();
			m_CollectedThrottleInput.Clamp01();
		}
		ApplyMovementInput(m_CollectedMovementInput, m_CollectedRotationInput, m_CollectedThrottleInput, m_CollectedBoostPropsInput, m_CollectedBoostJetsInput);
	}

	private Visible.Type CycleTargetType(Visible.Type t)
	{
		return t switch
		{
			Visible.Type.Null => Visible.Type.Resource, 
			Visible.Type.Resource => Visible.Type.Tank, 
			_ => Visible.Type.Null, 
		};
	}

	public void ToggleBeamActivated(bool suppressInventory = false)
	{
		base.Tech.beam.EnableBeam(!base.Tech.beam.IsActive, force: false, suppressInventory);
	}

	public void SetBeamControlState(bool beamEnabled)
	{
		m_ControlState.m_State.m_Beam = beamEnabled;
	}

	private void ReturnToFreeDriveMode()
	{
		bool flag = base.Tech == Singleton.playerTank;
		if (base.Tech.beam.IsActive && (!flag || !Mode<ModeMain>.inst.TutorialLockBeam))
		{
			base.Tech.beam.EnableBeam(enable: false);
		}
		if (flag && Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			if (Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.Manager<ManPointer>.inst.BuildMode != ManPointer.BuildingMode.PaintBlock)
			{
				Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem(applyVelocity: false);
			}
			if (Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled)
			{
				Singleton.Manager<ManPointer>.inst.EnableInteractionMode(enable: false);
			}
		}
	}

	public void UpdateSchemeHUD()
	{
		d.Assert(m_Schemes.Count != 0, "We currently have no schemes!");
		UIControlSchemaHUD uIControlSchemaHUD = (UIControlSchemaHUD)Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.ControlSchema);
		if (uIControlSchemaHUD != null)
		{
			uIControlSchemaHUD.ShowSchema(m_Schemes[0]);
			m_DeferUpdateSchemeHUD = false;
		}
		else
		{
			m_DeferUpdateSchemeHUD = true;
		}
	}

	private void OnPlayerTank(Tank tank, bool nowPlayer)
	{
		if (!(tank == base.Tech) || m_RewiredPlayer == null)
		{
			return;
		}
		if (nowPlayer)
		{
			CheckSchemeIsSet();
			UpdateSchemeHUD();
			Singleton.Manager<ManTechs>.inst.PlayerTankControlSchemeChangedEvent.Send(ActiveScheme);
			if (HasAnyThrottleControlAxes())
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Throttle, this);
			}
			m_RewiredPlayer.AddInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, 15);
			m_RewiredPlayer.AddInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, 15);
			m_RewiredPlayer.AddInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.ButtonPressed, 15);
			m_RewiredPlayer.AddInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.NegativeButtonPressed, 15);
			m_RewiredPlayer.AddInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.AxisActive, 15);
			m_RewiredPlayer.AddInputEventDelegate(OnManualTargetingEvent, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, 67);
			m_RewiredPlayer.AddInputEventDelegate(OnManualTargetingHeld, UpdateLoopType.Update, InputActionEventType.ButtonPressed, 67);
		}
		else
		{
			if (HasAnyThrottleControlAxes())
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Throttle, this);
			}
			m_RewiredPlayer.RemoveInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, 15);
			m_RewiredPlayer.RemoveInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, 15);
			m_RewiredPlayer.RemoveInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.ButtonPressed, 15);
			m_RewiredPlayer.RemoveInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.NegativeButtonPressed, 15);
			m_RewiredPlayer.RemoveInputEventDelegate(OnMouseZoomEvent, UpdateLoopType.Update, InputActionEventType.AxisActive, 15);
			m_RewiredPlayer.RemoveInputEventDelegate(OnManualTargetingEvent, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, 67);
			m_RewiredPlayer.RemoveInputEventDelegate(OnManualTargetingHeld, UpdateLoopType.Update, InputActionEventType.ButtonPressed, 67);
		}
	}

	private void OnManualTargetingHeld(InputActionEventData obj)
	{
		if (!Singleton.Manager<ManPointer>.inst.IsInteractionBlocked && Singleton.playerTank == base.Tech)
		{
			base.Tech.Weapons.UpdateNextPotentialTarget();
		}
	}

	private void OnTechWeaponsFiring()
	{
		if (m_PlayerTriggeredFire)
		{
			ReturnToFreeDriveMode();
		}
	}

	private void OnTechBoostersFiring()
	{
		if (m_PlayerTriggeredBoost)
		{
			ReturnToFreeDriveMode();
		}
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (saving)
		{
			List<ControlScheme> list = m_Schemes;
			if (list == null)
			{
				list = m_NetSchemes;
			}
			if (list == null)
			{
				list = m_SerialisedSchemes;
			}
			if (list != null)
			{
				SerialData serialData = new SerialData();
				serialData.m_Schemes = new List<ControlScheme>(list);
				serialData.m_Throttle = m_ThrottleValues;
				serialData.Store(saveState);
			}
			return;
		}
		m_SerialisedSchemes = null;
		m_Schemes = null;
		m_ThrottleValues = Vector3.zero;
		SerialData serialData2 = SerialData<SerialData>.Retrieve(saveState);
		if (serialData2 != null)
		{
			if (serialData2.m_Schemes != null)
			{
				m_SerialisedSchemes = new List<ControlScheme>(serialData2.m_Schemes.Count);
				for (int i = 0; i < serialData2.m_Schemes.Count; i++)
				{
					m_SerialisedSchemes.Add(serialData2.m_Schemes[i].CreateCopy());
				}
				m_SerialisedSchemesFromSnapshot = serialData2.m_FromSnapshot;
			}
			m_ThrottleValues = serialData2.m_Throttle;
		}
		m_ControlState.m_State.m_ThrottleValues = m_ThrottleValues;
		if (Singleton.playerTank == base.Tech)
		{
			CheckSchemeIsSet();
			UpdateSchemeHUD();
		}
	}

	public void OnSerialiseControlSchemesInitial(NetworkWriter writer)
	{
		int num = ((m_NetSchemes != null) ? m_NetSchemes.Count : 0);
		writer.WritePackedInt32(num);
		for (int i = 0; i < num; i++)
		{
			m_NetSchemes[i].OnSerialise(writer);
		}
	}

	public void OnDeserialiseControlSchemesInitial(NetworkReader reader)
	{
		int num = reader.ReadPackedInt32();
		m_NetSchemes = ((num > 0) ? new List<ControlScheme>(num) : null);
		for (int i = 0; i < num; i++)
		{
			ControlScheme controlScheme = new ControlScheme();
			controlScheme.OnDeserialise(reader);
			m_NetSchemes.Add(controlScheme);
		}
	}

	public void PostSetupNetTech()
	{
		if (m_NetSchemes == null && m_SerialisedSchemes != null)
		{
			m_NetSchemes = m_SerialisedSchemes;
			m_SerialisedSchemes = null;
		}
	}

	private void OnPool()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTank);
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
	}

	private void OnDepool()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTank);
	}

	private void OnSpawn()
	{
		m_AllControllers.Clear();
		m_AdditiveControllers?.Clear();
		m_RewiredPlayer = ReInput.players.GetPlayer(0);
		m_ControlState.Reset();
		base.Tech.Boosters.FiringBoostersEvent.Subscribe(OnTechBoostersFiring);
		base.Tech.Weapons.WeaponsFiredEvent.Subscribe(OnTechWeaponsFiring);
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
		for (int i = 0; i < m_ThrottleAxisEnableCount.Length; i++)
		{
			d.Assert(m_ThrottleAxisEnableCount[i] == 0, "TankControl: Mismatched add/remove throttle control");
			m_ThrottleAxisEnableCount[i] = 0;
		}
		for (int j = 0; j < m_DriveToThrottleAxisEnableCount.Length; j++)
		{
			d.Assert(m_DriveToThrottleAxisEnableCount[j] == 0, "TankControl: Mismatched add/remove throttle remap");
			m_DriveToThrottleAxisEnableCount[j] = 0;
		}
		m_ThrottleValues = Vector3.zero;
		m_ThrottleTiming = Vector3.zero;
		m_ThrottleLastInput = Vector3.zero;
	}

	private void OnRecycle()
	{
		base.Tech.Boosters.FiringBoostersEvent.Unsubscribe(OnTechBoostersFiring);
		base.Tech.Weapons.WeaponsFiredEvent.Unsubscribe(OnTechWeaponsFiring);
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
		m_ExternalExplosiveBoltActivationControl = false;
		m_Schemes = null;
		m_SerialisedSchemes = null;
		m_NetSchemes = null;
		m_HandlesPlayerInput = false;
	}

	private void OnManualTargetingEvent(InputActionEventData eventData)
	{
		if (!Singleton.Manager<ManPointer>.inst.IsInteractionBlocked && (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() || !eventData.IsCurrentInputSource(ControllerType.Joystick)) && Singleton.playerTank == base.Tech)
		{
			base.Tech.Weapons.OnManualTargetingEvent();
		}
	}

	private void OnMouseZoomEvent(InputActionEventData eventData)
	{
		if (Singleton.playerTank == base.Tech && !Singleton.Manager<ManPointer>.inst.IsInteractionBlocked && !Singleton.Manager<ManSpawn>.inst.DebugSpawnMenuActive && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			float axis = eventData.GetAxis();
			if (eventData.eventType == InputActionEventType.ButtonJustPressed || eventData.eventType == InputActionEventType.NegativeButtonJustPressed)
			{
				TankCamera.inst.ManualZoom((0f - axis) * Globals.inst.m_CamZoomStepManual);
			}
			else if (eventData.IsCurrentInputSource(ControllerType.Joystick) || eventData.IsCurrentInputSource(ControllerType.Keyboard))
			{
				float num = Globals.inst.m_CamZoomStepManual * Globals.inst.m_CamZoomHoldMultiplier;
				TankCamera.inst.ManualZoom((0f - axis) * num * Time.deltaTime);
			}
		}
	}

	private void OnUpdate()
	{
		if (m_DeferUpdateSchemeHUD)
		{
			UpdateSchemeHUD();
		}
		if (tankSelectDoubleTapTimer > 0f)
		{
			tankSelectDoubleTapTimer -= Time.deltaTime;
		}
		ManGameMode.GameState modePhase = Singleton.Manager<ManGameMode>.inst.GetModePhase();
		if (!Singleton.Manager<ManPauseGame>.inst.IsPaused && (modePhase == ManGameMode.GameState.InGame || modePhase == ManGameMode.GameState.FadeToBlack))
		{
			if (!Locked)
			{
				m_ControlState.m_State.m_InputMovement = Vector3.zero;
				m_ControlState.m_State.m_InputRotation = Vector3.zero;
				m_ControlState.m_State.m_ThrottleValues = m_ThrottleValues;
				m_ControlState.m_State.m_BoostProps = false;
				m_ControlState.m_State.m_BoostJets = false;
				m_ControlState.m_State.m_Fire = false;
				TargetRadiusWorld = 0f;
				aimControlManual = 0;
				m_ExplosiveBoltActivationControl = false;
				m_AnchorToggleControl = false;
				m_PlayerTriggeredFire = false;
				m_PlayerTriggeredBoost = false;
			}
			if (base.Tech.IsSleeping)
			{
				return;
			}
			if (!Locked)
			{
				bool flag = true;
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying())
				{
					flag = false;
				}
				if (flag)
				{
					bool isActive = base.Tech.beam.IsActive;
					ResetCollectedMovementInputs();
					using (List<ModuleTechController>.Enumerator enumerator = m_AllControllers.GetEnumerator())
					{
						while (enumerator.MoveNext() && !enumerator.Current.ExecuteControl(additive: false))
						{
						}
					}
					if (m_AdditiveControllers != null)
					{
						foreach (ModuleTechController additiveController in m_AdditiveControllers)
						{
							additiveController.ExecuteControl(additive: true);
						}
					}
					ApplyCollectedMovementInputs();
					bool isActive2 = base.Tech.beam.IsActive;
					m_ControlState.m_State.m_Beam = isActive2;
					m_BeamToggled = isActive != isActive2;
				}
			}
			else
			{
				m_BeamToggled = m_ControlState.m_State.m_Beam != base.Tech.beam.IsActive;
				if (m_BeamToggled)
				{
					base.Tech.beam.EnableBeam(m_ControlState.m_State.m_Beam);
				}
			}
			manualAimFireEvent.Send(aimControlManual, FireControl);
			if (TargetRadiusWorld != 0f)
			{
				targetedAimFireEvent.Send(TargetPositionWorld, TargetRadiusWorld);
			}
			if (!base.Tech.ControlsActive)
			{
				m_ControlState.m_State.m_InputMovement = Vector3.zero;
				m_ControlState.m_State.m_InputRotation = Vector3.zero;
				m_ControlState.m_State.m_ThrottleValues = Vector3.zero;
			}
			driveControlEvent.Send(m_ControlState);
			if (m_AnchorToggleControl)
			{
				base.Tech.TrySetAnchored(!base.Tech.IsAnchored);
			}
			if (m_ExplosiveBoltActivationControl || m_ExternalExplosiveBoltActivationControl)
			{
				for (int i = 0; i < explosiveBoltDetonateEvents.Length; i++)
				{
					if (HasExplosiveBolts(i + 1))
					{
						DetonateExplosiveBoltsOrdered(i + 1);
						break;
					}
				}
			}
		}
		m_ExternalExplosiveBoltActivationControl = false;
	}
}
