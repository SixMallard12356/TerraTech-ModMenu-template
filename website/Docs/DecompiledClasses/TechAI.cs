#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(BehaviorTree))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TechAI : TechComponent, INetworkedTechComponent
{
	public enum AITypes
	{
		Harvest,
		Idle,
		Escort,
		Scavenge,
		Scout,
		Guard,
		Specific
	}

	[Serializable]
	public class AIVariables
	{
		public int m_VisibleToFleeFrom = -1;
	}

	[Serializable]
	public new class SerialData : SerialData<SerialData>
	{
		public AITreeType m_AIType;

		public AIVariables m_AIVariables;

		public int m_PathfindingTargetID;
	}

	public ExternalBehaviorTree[] m_Trees;

	private BehaviorTree m_BehaviorTree;

	private ExternalBehaviorTree m_CurrentBT;

	private List<ModuleAIBot> m_AIModules = new List<ModuleAIBot>();

	private bool m_UpdateAIModules;

	private bool m_Active;

	private AICategories m_DisplayCategory;

	private List<AITypes> m_AITypes = new List<AITypes>();

	private AITreeType m_CurrentAITreeType;

	private AIVariables m_CurrentAIVariables;

	private bool m_RightingTank;

	private ItemTypeInfo m_ObjectToFind = new ItemTypeInfo(ObjectTypes.Chunk, 0);

	private bool m_AnyItemType = true;

	private Tank m_LastAttacker;

	private float m_LastAttackTime;

	private bool m_SetPointOfInterestOnEnableTree = true;

	private WorldPosition m_PointOfInterest;

	public float m_MaxThrottleLimit = 1f;

	private bool m_OldBehaviour = true;

	private ModuleDriveBot m_DriveBot;

	private PIDController m_pitchPID = new PIDController();

	private PIDController m_rollPID = new PIDController();

	private float m_LastPitch;

	private float m_LastRoll;

	private TrackedVisible m_PathfindingTarget;

	private Vector3 m_PathfindingPos;

	private WorldPosition[] m_Path;

	private int m_PathIndex;

	private bool m_SeachingForPath;

	private WorldPosition m_ClosestPoint;

	private float m_FurthestDistanceAlongSegment;

	private float m_LookaheadDistance;

	private float m_ExtraRadius = 1.2f;

	private float m_ExtraLookahead = 1.5f;

	private float m_MinLookahead = 5f;

	private float m_ForceFleeBehaviourTime;

	private AITreeType m_OverriddenBehaviour;

	public bool TargetAnyItemType => m_AnyItemType;

	public ItemTypeInfo TargetType => m_ObjectToFind;

	public Visible LastAttacker
	{
		get
		{
			if (!m_LastAttacker)
			{
				return null;
			}
			return m_LastAttacker.visible;
		}
	}

	public float LastAttackedTime => m_LastAttackTime;

	public bool UsePOI { get; set; }

	public float POIDist { get; set; }

	public Vector3 PointOfInterest => m_PointOfInterest.ScenePosition;

	public float PaddedRadius => base.Tech.visible.Radius * m_ExtraRadius;

	public bool HasAIModules => m_AIModules.Count > 0;

	public List<AITypes> AvailableAITypes => m_AITypes;

	private bool IsForceFleeSet => Time.time <= m_ForceFleeBehaviourTime;

	public void AddAI(ModuleAIBot aiModule)
	{
		m_AIModules.Add(aiModule);
		m_UpdateAIModules = true;
	}

	public void RemoveAI(ModuleAIBot aiModule)
	{
		m_AIModules.Remove(aiModule);
		m_UpdateAIModules = true;
	}

	public bool ControlTech()
	{
		if (m_AIModules.Count == 0)
		{
			DisableCurrentTree();
			return false;
		}
		if (m_OldBehaviour)
		{
			if (m_Active)
			{
				DisableCurrentTree();
			}
			if (!m_DriveBot || !m_DriveBot.block || !m_DriveBot.block.isActiveAndEnabled || !m_DriveBot.block.tank || m_DriveBot.block.tank != base.Tech)
			{
				m_DriveBot = m_AIModules[0].GetComponent<ModuleDriveBot>();
			}
			if ((bool)m_DriveBot)
			{
				return m_DriveBot.Control();
			}
			return false;
		}
		if (!m_Active)
		{
			EnableCurrentTree();
		}
		if (BehaviorManager.instance != null && m_BehaviorTree != null && m_CurrentBT != null)
		{
			BehaviorManager.instance.Tick(m_BehaviorTree);
		}
		return true;
	}

	public void SetOldBehaviour()
	{
		m_CurrentAITreeType = null;
		ExternalBehaviorTree currentTree = null;
		SetCurrentTree(currentTree);
	}

	public void ForceFleeFromVisible(Visible visible, float range, float safeRange, float fleeTime)
	{
		d.Assert(BehaviorManager.instance != null && m_BehaviorTree != null, "ForceFleeFromVisible - Behaviour tree not initialised properly!");
		if (!IsForceFleeSet || m_CurrentAIVariables.m_VisibleToFleeFrom != visible.ID)
		{
			m_OverriddenBehaviour = m_CurrentAITreeType;
			SetBehaviorType(new AITreeType(AITreeType.AITypes.Flee), forceNew: true);
			EnableCurrentTree();
			m_BehaviorTree.SetVariableValue("ShortRange", range);
			m_BehaviorTree.SetVariableValue("LongRange", safeRange);
			m_BehaviorTree.SetVariableValue("Fleeing", true);
			m_ForceFleeBehaviourTime = Time.time + fleeTime;
			if (m_CurrentAIVariables == null)
			{
				m_CurrentAIVariables = new AIVariables();
			}
			m_CurrentAIVariables.m_VisibleToFleeFrom = visible.ID;
		}
		else
		{
			m_ForceFleeBehaviourTime = Time.time + fleeTime;
		}
	}

	public void SetBehaviorType(AITreeType.AITypes aiType)
	{
		AITreeType aiSaveType = new AITreeType(aiType);
		SetBehaviorType(aiSaveType);
	}

	public void SetBehaviorType(AITreeType aiSaveType, bool forceNew = false)
	{
		if ((m_CurrentAITreeType != aiSaveType && (aiSaveType == null || m_CurrentAITreeType == null || aiSaveType.m_TypeName != m_CurrentAITreeType.m_TypeName)) || forceNew)
		{
			ClearForceFleeBehaviour();
			DisableCurrentTree();
			SetCurrentTree(aiSaveType);
		}
	}

	public bool CheckAIAvailable()
	{
		if (m_OldBehaviour)
		{
			return true;
		}
		for (int i = 0; i < m_AIModules.Count; i++)
		{
			if (m_AIModules[i].AITypesEnabled.Length != 0)
			{
				return true;
			}
		}
		return false;
	}

	public AICategories GetAICategory()
	{
		return m_DisplayCategory;
	}

	private void UpdateAICategory()
	{
		AICategories displayCategory = m_DisplayCategory;
		if (!m_Active)
		{
			m_DisplayCategory = AICategories.Null;
		}
		else if (m_OldBehaviour)
		{
			m_DisplayCategory = AICategories.AIHostile;
		}
		else if (!CheckAIAvailable())
		{
			m_DisplayCategory = AICategories.Null;
		}
		else
		{
			AITreeType.AITypes aiType = AITreeType.AITypes.Idle;
			if (m_CurrentAITreeType != null)
			{
				aiType = m_CurrentAITreeType.GetAIType();
			}
			m_DisplayCategory = MapBehaviourToCategory(aiType);
		}
		if (displayCategory != m_DisplayCategory && (bool)base.Tech.netTech)
		{
			base.Tech.netTech.SetAIDisplayCategoryDirty();
		}
	}

	public bool TryGetCurrentAIType(out AITreeType.AITypes aiType)
	{
		bool result = false;
		if (m_CurrentAITreeType != null)
		{
			aiType = m_CurrentAITreeType.GetAIType();
			result = true;
		}
		else
		{
			aiType = AITreeType.AITypes.Idle;
		}
		return result;
	}

	private void ClearForceFleeBehaviour()
	{
		m_ForceFleeBehaviourTime = 0f;
		if (m_CurrentAIVariables != null)
		{
			m_CurrentAIVariables.m_VisibleToFleeFrom = -1;
		}
	}

	public void SetCustomBehaviourTree(ExternalBehaviorTree externalTree)
	{
		if (externalTree != null && externalTree != m_CurrentBT)
		{
			if (!Singleton.Manager<ManAI>.inst.GetAIType(externalTree, out var treeType))
			{
				ClearForceFleeBehaviour();
				DisableCurrentTree();
				SetCurrentTree(externalTree);
				m_CurrentAITreeType = new AITreeType(AITreeType.AITypes.Specific);
			}
			else
			{
				d.LogError("Trying to SetCustomBehaviourTree, but the tree is already known as an enumerated AIType: " + treeType.m_TypeName);
				SetBehaviorType(treeType);
			}
		}
	}

	public void SetAIVariables(AIVariables aiVariables)
	{
		m_CurrentAIVariables = aiVariables;
	}

	public void SetItemType(ItemTypeInfo objectToFind)
	{
		m_ObjectToFind = objectToFind;
		m_AnyItemType = false;
	}

	public void SetItemTypeAny(bool any)
	{
		m_AnyItemType = any;
	}

	public void SetPOI(bool usePOI, Vector3 position, float distance)
	{
		UsePOI = usePOI;
		m_PointOfInterest = WorldPosition.FromScenePosition(in position);
		POIDist = distance;
		if (!m_Active)
		{
			m_SetPointOfInterestOnEnableTree = false;
		}
	}

	public bool IsTankOverturned()
	{
		Vector3 up;
		if ((bool)base.Tech.blockman.GetRootBlock())
		{
			up = base.Tech.rootBlockTrans.up;
		}
		else
		{
			d.LogErrorFormat("TechAI.IsTankOverturned - Tech '{0}' has no root block? Falling back to using incorrect trans.up", base.Tech.name);
			up = base.Tech.trans.up;
		}
		return !(Vector3.Dot(up, Vector3.up) > Mathf.Cos(Globals.inst.tankOverturnThresholdDegrees * ((float)Math.PI / 180f)));
	}

	public bool IsTankMoving(float minSpeed)
	{
		return base.Tech.rbody.velocity.sqrMagnitude > minSpeed * minSpeed;
	}

	public bool RightTank()
	{
		if (!m_RightingTank)
		{
			m_RightingTank = base.Tech.beam.EnableBeam(enable: true);
		}
		Vector3 up;
		if ((bool)base.Tech.blockman.GetRootBlock())
		{
			up = base.Tech.rootBlockTrans.up;
		}
		else
		{
			d.LogErrorFormat("TechAI.RightTank - Trying to right tank but tech '{0}' has no root block? Falling back to using incorrect trans.up", base.Tech.name);
			up = base.Tech.trans.up;
		}
		bool num = Vector3.Dot(up, Vector3.up) > Mathf.Cos(0.17453292f);
		if (num)
		{
			base.Tech.beam.EnableBeam(enable: false);
			m_RightingTank = false;
		}
		return num;
	}

	public bool DriveToTarget(Visible target)
	{
		bool result = false;
		if (target != null)
		{
			m_PathfindingTarget = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(target.ID);
		}
		if (m_PathfindingTarget != null)
		{
			if (!m_PathfindingTarget.wasDestroyed)
			{
				if (m_Path == null)
				{
					if ((bool)m_PathfindingTarget.visible && !m_SeachingForPath)
					{
						FindPath(m_PathfindingTarget.Position);
					}
				}
				else
				{
					if ((bool)m_PathfindingTarget.visible)
					{
						RecheckPath();
					}
					result = FollowPath();
				}
			}
			else
			{
				m_PathfindingTarget = null;
				result = true;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	public void ClearPath()
	{
		m_Path = null;
	}

	public Visible GetTargetVisible()
	{
		Visible result = null;
		bool flag = false;
		if (m_CurrentAITreeType != null)
		{
			AITreeType.AITypes aIType = m_CurrentAITreeType.GetAIType();
			if (aIType == AITreeType.AITypes.Flee)
			{
				if (m_CurrentAIVariables != null && m_CurrentAIVariables.m_VisibleToFleeFrom != -1)
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_CurrentAIVariables.m_VisibleToFleeFrom);
					if (trackedVisible != null)
					{
						result = trackedVisible.visible;
					}
				}
				else if (ManNetwork.IsNetworked)
				{
					float num = float.MaxValue;
					Tank tank = null;
					foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
					{
						if (allPlayerTech.IsNotNull())
						{
							float sqrMagnitude = (allPlayerTech.trans.position - base.transform.position).sqrMagnitude;
							if (sqrMagnitude < num)
							{
								num = sqrMagnitude;
								tank = allPlayerTech;
							}
						}
					}
					if (tank.IsNotNull())
					{
						result = tank.visible;
					}
				}
				else
				{
					result = (Singleton.playerTank ? Singleton.playerTank.visible : null);
				}
				flag = true;
			}
		}
		if (!flag)
		{
			result = (Singleton.playerTank ? Singleton.playerTank.visible : null);
			d.LogError("Unhandled target visible for AI type:" + m_CurrentAITreeType.ToString() + " falling back to player tech");
		}
		return result;
	}

	public NetworkedTechComponentID GetTechComponentID()
	{
		return NetworkedTechComponentID.TechAI;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.WritePackedInt32((int)m_DisplayCategory);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_DisplayCategory = (AICategories)reader.ReadPackedInt32();
	}

	private void SetCurrentTree(AITreeType aiTreeType)
	{
		if (aiTreeType != null)
		{
			ExternalBehaviorTree aITree = Singleton.Manager<ManAI>.inst.GetAITree(aiTreeType);
			if (aITree != null)
			{
				SetCurrentTree(aITree);
				m_CurrentAITreeType = aiTreeType;
			}
		}
	}

	private void SetCurrentTree(ExternalBehaviorTree tree)
	{
		m_OldBehaviour = tree == null;
		m_CurrentBT = tree;
		m_BehaviorTree.ExternalBehavior = m_CurrentBT;
		UpdateAICategory();
	}

	private void EnableCurrentTree()
	{
		m_BehaviorTree.EnableBehavior();
		m_Active = true;
		if (m_SetPointOfInterestOnEnableTree)
		{
			m_PointOfInterest = WorldPosition.FromScenePosition(base.Tech.rbody.position);
		}
		m_SetPointOfInterestOnEnableTree = true;
		UpdateAICategory();
	}

	private void DisableCurrentTree()
	{
		m_BehaviorTree.DisableBehavior();
		m_BehaviorTree.ExternalBehavior = null;
		m_Active = false;
	}

	private void SetupBehaviorTree()
	{
		m_BehaviorTree = GetComponent<BehaviorTree>();
		m_BehaviorTree.ExternalBehavior = m_CurrentBT;
		m_BehaviorTree.StartWhenEnabled = false;
		m_BehaviorTree.RestartWhenComplete = true;
		SetCurrentTree(m_CurrentAITreeType);
	}

	private AICategories MapBehaviourToCategory(AITreeType.AITypes aiType)
	{
		switch (aiType)
		{
		case AITreeType.AITypes.Idle:
			return AICategories.AIIdle;
		case AITreeType.AITypes.Escort:
		case AITreeType.AITypes.Guard:
		case AITreeType.AITypes.FollowPassive:
		case AITreeType.AITypes.Invader:
		case AITreeType.AITypes.ChargeAtSKU:
		case AITreeType.AITypes.FacePlayer:
		case AITreeType.AITypes.Specific:
			return AICategories.AIHostile;
		case AITreeType.AITypes.Harvest:
			return AICategories.AIHarvest;
		case AITreeType.AITypes.Flee:
			return AICategories.AIFlee;
		default:
			d.LogError("No TechAI.ControlType relationship found to AITreeType.AITypes of " + aiType);
			return AICategories.Null;
		}
	}

	private void FindPath(Vector3 targetPos)
	{
		m_SeachingForPath = Singleton.Manager<ManPath>.inst.TryFindPath(base.Tech.visible.centrePosition, targetPos, PaddedRadius, PathFound);
		if (m_SeachingForPath)
		{
			m_PathfindingPos = targetPos;
			m_LookaheadDistance = Mathf.Max(m_MinLookahead, (float)Mathf.CeilToInt(PaddedRadius) * m_ExtraLookahead);
		}
	}

	private void PathFound(WorldPosition[] path)
	{
		m_Path = path;
		if (m_Path != null)
		{
			m_PathIndex = 1;
		}
		m_SeachingForPath = false;
	}

	private void RecheckPath()
	{
		if (m_SeachingForPath || m_PathfindingTarget == null)
		{
			return;
		}
		if (!m_PathfindingTarget.wasDestroyed)
		{
			if ((m_PathfindingTarget.Position - m_PathfindingPos).sqrMagnitude > 225f)
			{
				FindPath(m_PathfindingTarget.Position);
			}
		}
		else
		{
			ClearPath();
		}
	}

	private bool FollowPath()
	{
		bool result = false;
		if (m_PathIndex < m_Path.Length)
		{
			TankBlock rootBlock = base.Tech.blockman.GetRootBlock();
			Vector3 vector = base.Tech.visible.centrePosition.SetY(0f);
			Vector3 normalized = rootBlock.trans.forward.SetY(0f).normalized;
			Vector3 vector2 = (vector + normalized * m_LookaheadDistance).SetY(0f);
			Vector3 startPos = ((m_PathIndex > 0) ? m_Path[m_PathIndex - 1].ScenePosition : vector);
			Vector3 scenePosition = m_Path[m_PathIndex].ScenePosition;
			float distanceAlongLine;
			Vector3 scenePos = Maths.ClosestPointOnLine(startPos, scenePosition, vector2, out distanceAlongLine);
			if (distanceAlongLine > m_FurthestDistanceAlongSegment)
			{
				m_FurthestDistanceAlongSegment = distanceAlongLine;
				m_ClosestPoint = WorldPosition.FromScenePosition(in scenePos);
			}
			Vector3 vector3 = m_ClosestPoint.ScenePosition;
			if ((vector3 - vector2).magnitude > PaddedRadius)
			{
				vector3 = scenePosition;
			}
			if (distanceAlongLine < 1f || m_PathIndex == m_Path.Length - 1)
			{
				if (base.Tech.control.Movement.DriveAlongLine(base.Tech, startPos, scenePosition, vector3, m_PathIndex == m_Path.Length - 1, PaddedRadius))
				{
					m_FurthestDistanceAlongSegment = float.MinValue;
					m_PathIndex++;
				}
			}
			else
			{
				m_FurthestDistanceAlongSegment = float.MinValue;
				m_PathIndex++;
			}
		}
		else
		{
			ClearPath();
			result = true;
		}
		return result;
	}

	public void FlyDirectlyForward()
	{
		base.Tech.control.BoostControlProps = true;
		Vector3 right = base.transform.right;
		right.y = 0f;
		right *= Mathf.Sign(base.transform.up.y);
		float inputValue = Vector3.Angle(Vector3.Cross(right, Vector3.up).normalized, base.transform.forward) * Mathf.Sign(base.transform.forward.y);
		m_pitchPID.Update(inputValue, 0f);
		base.Tech.control.PitchControl = Mathf.Clamp(m_pitchPID.GetOutput(), -1f, 1f);
		Vector3 forward = base.transform.forward;
		forward.y = 0f;
		forward *= Mathf.Sign(base.transform.up.y);
		float inputValue2 = Vector3.Angle(Vector3.Cross(Vector3.up, forward).normalized, base.transform.right) * Mathf.Sign(base.transform.right.y);
		m_rollPID.Update(inputValue2, 0f);
		base.Tech.control.RollControl = Mathf.Clamp(0f - m_rollPID.GetOutput(), -1f, 1f);
	}

	public void FlyTowardsTargetLocation(Vector3 worldTargetLocation)
	{
		base.Tech.control.BoostControlProps = true;
		Vector3 vector = base.Tech.transform.InverseTransformPoint(worldTargetLocation);
		Vector3 vector2 = worldTargetLocation - base.Tech.transform.position;
		float num = Mathf.Abs(Vector3.Angle(to: new Vector3(vector.x, 0f, vector.z), from: base.Tech.transform.forward));
		float maxRollError = Singleton.Manager<ManFlyingAI>.inst.MaxRollError;
		if (vector.z < -20f)
		{
			num = maxRollError;
		}
		float num2 = Mathf.Lerp(0f, Singleton.Manager<ManFlyingAI>.inst.MaxRoll * (0f - Mathf.Sign(vector.x)), Mathf.Clamp01(num / maxRollError));
		if (num2 != m_LastRoll)
		{
			m_rollPID.ResetError();
		}
		m_LastRoll = num2;
		Vector3 forward = base.transform.forward;
		forward.y = 0f;
		forward *= Mathf.Sign(base.transform.up.y);
		float inputValue = Vector3.Angle(Vector3.Cross(Vector3.up, forward).normalized, base.transform.right) * Mathf.Sign(base.transform.right.y);
		m_rollPID.Update(inputValue, num2);
		base.Tech.control.RollControl = Mathf.Clamp(0f - m_rollPID.GetOutput(), -1f, 1f);
		float num3 = Mathf.Abs(Vector3.Angle(to: new Vector3(0f, vector2.y, Vector3.Distance(base.Tech.transform.position, new Vector3(vector2.x, base.Tech.transform.position.y, vector2.z))), from: Vector3.forward));
		float maxPitchError = Singleton.Manager<ManFlyingAI>.inst.MaxPitchError;
		float num4 = Mathf.Lerp(0f, Singleton.Manager<ManFlyingAI>.inst.MaxPitch * Mathf.Sign(vector2.y), Mathf.Clamp01(num3 / maxPitchError));
		if (m_LastPitch != num4)
		{
			m_pitchPID.ResetError();
		}
		m_LastPitch = num4;
		Vector3 right = base.transform.right;
		right.y = 0f;
		right *= Mathf.Sign(base.transform.up.y);
		float inputValue2 = Vector3.Angle(Vector3.Cross(right, Vector3.up).normalized, base.transform.forward) * Mathf.Sign(base.transform.forward.y);
		m_pitchPID.Update(inputValue2, num4);
		base.Tech.control.PitchControl = Mathf.Clamp(m_pitchPID.GetOutput(), -1f, 1f);
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		if ((bool)info.SourceTank)
		{
			m_LastAttacker = info.SourceTank;
			m_LastAttackTime = Time.time;
		}
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.m_AIType = m_CurrentAITreeType;
			serialData.m_AIVariables = m_CurrentAIVariables;
			serialData.m_PathfindingTargetID = ((m_PathfindingTarget != null) ? m_PathfindingTarget.ID : (-1));
			serialData.Store(saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(saveState);
		if (serialData2 != null)
		{
			if (serialData2.m_AIType != null)
			{
				SetBehaviorType(serialData2.m_AIType);
			}
			if (serialData2.m_AIVariables != null)
			{
				m_CurrentAIVariables = serialData2.m_AIVariables;
			}
			if (serialData2.m_PathfindingTargetID != -1)
			{
				m_PathfindingTarget = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(serialData2.m_PathfindingTargetID);
			}
		}
	}

	private void OnAnchor(ModuleAnchor anchor, bool anchored, bool fromAfterTechPopulate)
	{
		if (base.Tech.IsFriendly(0))
		{
			AITreeType.AITypes aITypes = (base.Tech.IsAnchored ? AITreeType.AITypes.Guard : AITreeType.AITypes.Idle);
			if (m_CurrentAITreeType == null || (!fromAfterTechPopulate && m_CurrentAITreeType.GetAIType() != aITypes))
			{
				SetBehaviorType(aITypes);
			}
		}
	}

	private void OnPool()
	{
		m_BehaviorTree = GetComponent<BehaviorTree>();
		SetupBehaviorTree();
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_DriveBot = null;
		m_RightingTank = false;
		m_OldBehaviour = true;
		m_SetPointOfInterestOnEnableTree = true;
		m_AIModules.Clear();
		base.Tech.DamageEvent.Subscribe(OnDamaged);
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
		base.Tech.AnchorEvent.Subscribe(OnAnchor);
	}

	private void OnRecycle()
	{
		base.Tech.DamageEvent.Unsubscribe(OnDamaged);
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
		base.Tech.AnchorEvent.Unsubscribe(OnAnchor);
		DisableCurrentTree();
		m_CurrentAITreeType = null;
		m_DisplayCategory = AICategories.Null;
		m_OverriddenBehaviour = null;
		ClearForceFleeBehaviour();
	}

	private void OnUpdate()
	{
		if (m_UpdateAIModules)
		{
			if (m_AIModules.Count == 0)
			{
				m_DriveBot = null;
				if (m_Active)
				{
					DisableCurrentTree();
				}
			}
			m_AITypes.Clear();
			for (int i = 0; i < m_AIModules.Count; i++)
			{
				AITypes[] aITypesEnabled = m_AIModules[i].AITypesEnabled;
				for (int j = 0; j < aITypesEnabled.Length; j++)
				{
					if (!m_AITypes.Contains(aITypesEnabled[j]))
					{
						m_AITypes.Add(aITypesEnabled[j]);
					}
				}
			}
			if (!m_AITypes.Contains(AITypes.Idle))
			{
				m_AITypes.Add(AITypes.Idle);
			}
			m_UpdateAIModules = false;
			UpdateAICategory();
		}
		if (ManSpawn.IsPlayerTeam(base.Tech.Team) && m_OldBehaviour && base.Tech != Singleton.playerTank)
		{
			SetBehaviorType(AITreeType.AITypes.Idle);
		}
		if (base.Tech.IsSleeping || !(m_BehaviorTree != null) || m_CurrentAITreeType == null || !m_CurrentAITreeType.IsType(AITreeType.AITypes.Flee))
		{
			return;
		}
		SharedVariable variable = m_BehaviorTree.GetVariable("Fleeing");
		if (variable == null)
		{
			return;
		}
		bool num = (bool)variable.GetValue();
		bool flag = m_ForceFleeBehaviourTime != 0f && !IsForceFleeSet;
		if (!num || flag)
		{
			if (m_OverriddenBehaviour != null)
			{
				SetBehaviorType(m_OverriddenBehaviour, forceNew: true);
			}
			else
			{
				SetOldBehaviour();
			}
			m_OverriddenBehaviour = null;
			ClearForceFleeBehaviour();
		}
	}

	private void OnDrawGizmos()
	{
		if (m_Path == null)
		{
			return;
		}
		float paddedRadius = PaddedRadius;
		Vector3 vector = m_ClosestPoint.ScenePosition.SetY(0f);
		for (int i = 1; i < m_Path.Length; i++)
		{
			Vector3 vector2 = m_Path[i - 1].ScenePosition.SetY(0f);
			Vector3 vector3 = m_Path[i].ScenePosition.SetY(0f);
			if (i == m_PathIndex)
			{
				Debug.DrawLine(vector2, vector, Color.magenta, 0f, depthTest: false);
				Debug.DrawLine(vector, vector3, Color.cyan, 0f, depthTest: false);
			}
			else
			{
				Debug.DrawLine(vector2, vector3, Color.blue, 0f, depthTest: false);
			}
			Vector3 vector4 = (vector3 - vector2).normalized.Cross(Vector3.up);
			Debug.DrawLine(vector2 + vector4 * paddedRadius, vector3 + vector4 * paddedRadius, Color.black, 0f, depthTest: false);
			Debug.DrawLine(vector2 - vector4 * paddedRadius, vector3 - vector4 * paddedRadius, Color.black, 0f, depthTest: false);
		}
	}
}
