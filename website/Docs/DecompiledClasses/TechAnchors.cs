#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TechAnchors : TechComponent, IWorldTreadmill, INetworkedTechComponent
{
	[SerializeField]
	private AnimationCurve m_DriveTorqueFalloff;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_MaxDeltaAngVel = 0.5f;

	public bool dbg_TorqueGraphs;

	public Event<bool, bool> AnchorEvent;

	private List<ModuleAnchor> m_AnchoredAnchors = new List<ModuleAnchor>(10);

	private List<ModuleAnchor> m_AnchorsOnTech = new List<ModuleAnchor>(10);

	private ModuleAnchor m_PivotAnchor;

	private float m_TurnControl;

	private int m_NumSkyAnchors;

	private int m_NumAnchoredSkyAnchors;

	private ConfigurableJoint m_GroundJoint;

	private static Collider[] s_CheckColliders = new Collider[32];

	private List<uint> m_NetAnchoredBlocks = new List<uint>();

	private bool m_NetGroundJoint;

	private WorldPosition m_NetConnectedAnchor;

	private Vector3 m_NetAnchorPos;

	private Vector3 m_NetJointAxis;

	private Quaternion m_NetAnchorTechRotation;

	private static RaycastHit[] s_AnchorTestRaycastResults = new RaycastHit[32];

	private static RaycastHitSortComparer s_RaycastResultsSortComparer = new RaycastHitSortComparer();

	public int NumAnchored => m_AnchoredAnchors.Count;

	public int NumPossibleAnchors => m_AnchorsOnTech.Count;

	public int NumSkyAnchored => m_NumAnchoredSkyAnchors;

	public int NumPossibleSkyAnchors => m_NumSkyAnchors;

	public bool Fixed
	{
		get
		{
			if (NumAnchored != 0 && NumAnchored > m_NumAnchoredSkyAnchors)
			{
				return m_PivotAnchor == null;
			}
			return false;
		}
	}

	public float TurnControl => m_TurnControl;

	public ModuleAnchor FirstAnchored => m_AnchoredAnchors.FirstOrDefault();

	public bool RetryAnchorOnBeam { get; set; }

	public int NumIsAnchored
	{
		get
		{
			int num = 0;
			foreach (ModuleAnchor item in m_AnchorsOnTech)
			{
				if (item.IsAnchored)
				{
					num++;
				}
			}
			return num;
		}
	}

	public ModuleAnchor GetAnchored(int i)
	{
		return m_AnchoredAnchors[i];
	}

	public void AddPossibleAnchor(ModuleAnchor anchor)
	{
		m_AnchorsOnTech.Add(anchor);
		if (anchor.IsSkyAnchor)
		{
			m_NumSkyAnchors++;
		}
	}

	public void RemovePossibleAnchor(ModuleAnchor anchor)
	{
		m_AnchorsOnTech.Remove(anchor);
		if (anchor.IsSkyAnchor)
		{
			m_NumSkyAnchors--;
		}
	}

	public void AddAnchor(ModuleAnchor anchor)
	{
		if (!m_AnchoredAnchors.Contains(anchor))
		{
			m_AnchoredAnchors.Add(anchor);
			if (anchor.IsSkyAnchor)
			{
				m_NumAnchoredSkyAnchors++;
			}
			if (base.Tech.netTech.IsNotNull())
			{
				base.Tech.netTech.SetAnchorsDirty();
			}
		}
		if (NumAnchored == 1)
		{
			AnchorEvent.Send(paramA: true, anchor.IsSkyAnchor);
		}
		RetryAnchorOnBeam = false;
	}

	public void RemoveAnchor(ModuleAnchor anchor)
	{
		if (m_AnchoredAnchors.Remove(anchor))
		{
			if (base.Tech.netTech.IsNotNull())
			{
				base.Tech.netTech.SetAnchorsDirty();
			}
			if (anchor.IsSkyAnchor)
			{
				m_NumAnchoredSkyAnchors--;
			}
		}
		if (NumAnchored == 0)
		{
			AnchorEvent.Send(paramA: false, anchor.IsSkyAnchor);
		}
	}

	public bool IsAnchored(ModuleAnchor anchor)
	{
		if (anchor.block.tank != base.Tech)
		{
			d.Assert(condition: false, "Anchor module not attached to this tech");
			return false;
		}
		return m_AnchoredAnchors.Contains(anchor);
	}

	public void RecordAnchoredBlocks(List<TankBlock> blocks)
	{
		foreach (ModuleAnchor anchoredAnchor in m_AnchoredAnchors)
		{
			blocks.Add(anchoredAnchor.block);
		}
	}

	private bool AnythingBelowTech()
	{
		Vector3 boundsCentreWorld = base.Tech.boundsCentreWorld;
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(boundsCentreWorld);
		float num = boundsCentreWorld.y - vector.y;
		if (num <= 0f)
		{
			num = 1f;
			boundsCentreWorld.y = vector.y + num;
		}
		int num2 = 0;
		do
		{
			if (num2 == s_AnchorTestRaycastResults.Length)
			{
				Array.Resize(ref s_AnchorTestRaycastResults, s_AnchorTestRaycastResults.Length * 2);
			}
			num2 = Physics.SphereCastNonAlloc(boundsCentreWorld, base.Tech.visible.Radius, Vector3.down, s_AnchorTestRaycastResults, num, -5, QueryTriggerInteraction.Ignore);
			d.Assert(num2 != 0);
		}
		while (num2 == s_AnchorTestRaycastResults.Length);
		if (num2 > 1)
		{
			Array.Sort(s_AnchorTestRaycastResults, 0, num2, s_RaycastResultsSortComparer);
		}
		bool result = false;
		for (int i = 0; i < num2 && !s_AnchorTestRaycastResults[i].transform.gameObject.IsTerrain(); i++)
		{
			if (s_AnchorTestRaycastResults[i].transform.gameObject.layer == (int)Globals.inst.layerLandmark)
			{
				result = true;
				break;
			}
			Visible visible = Visible.FindVisibleUpwards(s_AnchorTestRaycastResults[i].collider);
			if ((bool)visible && (!(visible.block != null) || !(visible.block.tank == base.Tech)))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void UpdateAnchorsFromNetwork()
	{
		for (int i = 0; i < m_AnchorsOnTech.Count; i++)
		{
			ModuleAnchor moduleAnchor = m_AnchorsOnTech[i];
			if (!moduleAnchor)
			{
				continue;
			}
			if (m_NetAnchoredBlocks.Contains(moduleAnchor.block.blockPoolID))
			{
				if (!moduleAnchor.IsAnchored)
				{
					moduleAnchor.AnchorToGround();
				}
			}
			else if (moduleAnchor.IsAnchored)
			{
				moduleAnchor.UnAnchorFromGround(playAnim: false);
			}
		}
		ConfigureJoint();
		if (m_NetGroundJoint && (bool)m_GroundJoint)
		{
			base.Tech.trans.rotation = m_NetAnchorTechRotation;
			base.Tech.rbody.rotation = m_NetAnchorTechRotation;
			m_GroundJoint.axis = m_NetJointAxis;
			m_GroundJoint.anchor = m_NetAnchorPos;
			m_GroundJoint.connectedAnchor = m_NetConnectedAnchor.ScenePosition;
		}
		_ = m_GroundJoint != null;
		_ = m_NetGroundJoint;
	}

	private bool CheckSphereClearOfTerrainAndTechs(Vector3 cellPosWorld)
	{
		int num = Physics.OverlapSphereNonAlloc(cellPosWorld, 0.5f, s_CheckColliders, Globals.inst.layerTerrain.mask | Globals.inst.layerTank.mask);
		for (int i = 0; i < num; i++)
		{
			Collider collider = s_CheckColliders[i];
			if (collider.gameObject.layer == (int)Globals.inst.layerTerrain)
			{
				return false;
			}
			Visible visible = Visible.FindVisibleUpwards(collider);
			if (visible.IsNotNull() && visible.block.IsNotNull())
			{
				Tank tank = visible.block.tank;
				if (tank.IsNotNull() && tank != base.Tech)
				{
					d.Log("Anchoring of " + base.Tech.name + " blocked by other tech " + tank.name + ", block " + visible.block.name);
					return false;
				}
			}
		}
		return true;
	}

	private bool CheckAnchoredCollision(Vector3 techPosition, Quaternion techRotation, float maxUpwardsCheck, out float upMoveNeeded, float incrementSize = 0.25f)
	{
		upMoveNeeded = 0f;
		foreach (IntVector3 lowestOccupiedCell in base.Tech.blockman.GetLowestOccupiedCells())
		{
			Vector3 cellPosWorld = techPosition + techRotation * lowestOccupiedCell;
			while (!CheckSphereClearOfTerrainAndTechs(cellPosWorld))
			{
				if (upMoveNeeded >= maxUpwardsCheck - 0.01f)
				{
					return false;
				}
				float num = Mathf.Min(maxUpwardsCheck, upMoveNeeded + incrementSize) - upMoveNeeded;
				upMoveNeeded += num;
				cellPosWorld.y += num;
				techPosition.y += num;
			}
		}
		return true;
	}

	public bool TryAnchorAll(bool moveTech = false, bool fromAfterTechPopulate = false)
	{
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Vector3 vector = Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition - base.Tech.boundsCentreWorld;
			vector.y = 0f;
			if (vector.magnitude > Singleton.Manager<ManNetwork>.inst.DangerDistance)
			{
				flag = false;
			}
		}
		if (flag)
		{
			Vector3 position = base.Tech.trans.position;
			Quaternion rotation = base.Tech.trans.rotation;
			if (moveTech && !AnythingBelowTech())
			{
				base.Tech.trans.rotation = base.Tech.beam.CalcHoverOrientation();
				float num = float.MinValue;
				for (int i = 0; i < NumPossibleAnchors; i++)
				{
					float b = m_AnchorsOnTech[i].HeightOffGroundForMaxAnchor();
					num = Mathf.Max(num, b);
				}
				Vector3 position2 = base.Tech.trans.position;
				position2.y += num;
				base.Tech.trans.position = position2;
			}
			for (int j = 0; j < NumPossibleAnchors; j++)
			{
				ModuleAnchor moduleAnchor = m_AnchorsOnTech[j];
				if ((fromAfterTechPopulate || base.Tech.grounded) && !moduleAnchor.IsSkyAnchor && moduleAnchor.WouldAnchorToGround())
				{
					moduleAnchor.AnchorToGround(snapTechToGround: true, fromAfterTechPopulate);
					base.Tech.grounded = true;
				}
			}
			if (NumAnchored > 0)
			{
				float num2 = 0f;
				foreach (ModuleAnchor anchoredAnchor in m_AnchoredAnchors)
				{
					if (anchoredAnchor.IsAnchored && !anchoredAnchor.IsSkyAnchor)
					{
						num2 = Mathf.Max(num2, anchoredAnchor.HeightOffGroundForMaxExtension());
					}
				}
				if (CheckAnchoredCollision(base.Tech.trans.position, base.Tech.trans.rotation, num2, out var upMoveNeeded))
				{
					d.Log($"TechAnchor: move up by {upMoveNeeded}/{num2}");
					if (upMoveNeeded > 0f)
					{
						foreach (ModuleAnchor item in m_AnchorsOnTech)
						{
							if (!item.IsSkyAnchor && item.IsAnchored && item.HeightOffGroundForMaxExtension() + 0.01f < upMoveNeeded)
							{
								item.UnAnchorFromGround(playAnim: false);
							}
						}
						base.Tech.trans.position += Vector3.up * upMoveNeeded;
					}
				}
				else
				{
					d.Log($"TechAnchor: blocked (max up check = {num2})");
					UnanchorAll(playAnim: false, nonSkyOnly: true);
				}
			}
			ConfigureJoint();
			if (NumAnchored == 0)
			{
				if (moveTech)
				{
					base.Tech.trans.position = position;
					base.Tech.trans.rotation = rotation;
				}
				for (int k = 0; k < NumPossibleAnchors; k++)
				{
					ModuleAnchor moduleAnchor2 = m_AnchorsOnTech[k];
					if (moduleAnchor2.IsSkyAnchor && moduleAnchor2.WouldAnchorToGround())
					{
						moduleAnchor2.AnchorToGround(snapTechToGround: true, fromAfterTechPopulate);
					}
				}
				ConfigureJoint();
				if (moveTech && NumAnchored == 0 && NumPossibleSkyAnchors == 0)
				{
					base.Tech.trans.position = position + Vector3.up * 0.5f;
				}
			}
		}
		return NumAnchored > 0;
	}

	public void TryDeployUnanchoredAnchors()
	{
		if (!base.Tech.IsAnchored)
		{
			return;
		}
		for (int i = 0; i < NumPossibleAnchors; i++)
		{
			ModuleAnchor moduleAnchor = m_AnchorsOnTech[i];
			if (moduleAnchor.WouldAnchorToGround())
			{
				moduleAnchor.AnchorToGround(snapTechToGround: false);
			}
		}
	}

	public void UnanchorAll(bool playAnim, bool nonSkyOnly = false)
	{
		for (int i = 0; i < NumPossibleAnchors; i++)
		{
			ModuleAnchor moduleAnchor = m_AnchorsOnTech[i];
			if (moduleAnchor.IsAnchored && (!nonSkyOnly || !moduleAnchor.IsSkyAnchor))
			{
				moduleAnchor.UnAnchorFromGround(playAnim);
			}
		}
		ConfigureJoint();
	}

	public void UnanchorSingle(ModuleAnchor anchor, bool playAnim)
	{
		if (anchor.IsAnchored)
		{
			anchor.UnAnchorFromGround(playAnim);
			ConfigureJoint();
		}
	}

	private void ConfigureJoint()
	{
		if (NumAnchored != 1 || !m_AnchoredAnchors[0].AllowsRotation)
		{
			if (m_GroundJoint != null)
			{
				RemoveGroundJoint();
				m_PivotAnchor = null;
				base.Tech.netTech?.SetAnchorsDirty();
			}
		}
		else if (m_GroundJoint == null || m_PivotAnchor != m_AnchoredAnchors[0])
		{
			m_PivotAnchor = m_AnchoredAnchors[0];
			m_PivotAnchor.InitRotation();
			if (m_GroundJoint == null)
			{
				m_GroundJoint = base.gameObject.AddComponent<ConfigurableJoint>();
				Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
				base.Tech.blockman.BlockTableRecentreEvent.Subscribe(OnBlockTableRecentred);
			}
			m_GroundJoint.axis = base.Tech.trans.InverseTransformDirection(m_PivotAnchor.block.trans.up);
			m_GroundJoint.autoConfigureConnectedAnchor = false;
			m_GroundJoint.enableCollision = true;
			m_GroundJoint.xMotion = ConfigurableJointMotion.Locked;
			m_GroundJoint.yMotion = ConfigurableJointMotion.Locked;
			m_GroundJoint.zMotion = ConfigurableJointMotion.Locked;
			m_GroundJoint.angularXMotion = ConfigurableJointMotion.Free;
			m_GroundJoint.angularYMotion = ConfigurableJointMotion.Locked;
			m_GroundJoint.angularZMotion = ConfigurableJointMotion.Locked;
			m_GroundJoint.anchor = base.Tech.trans.InverseTransformVector(m_PivotAnchor.GroundPoint - base.Tech.trans.position);
			m_GroundJoint.connectedAnchor = m_PivotAnchor.GroundPoint;
			base.Tech.netTech?.SetAnchorsDirty();
		}
		base.Tech.CheckKinematic();
	}

	private void RemoveGroundJoint()
	{
		if (m_GroundJoint != null)
		{
			UnityEngine.Object.Destroy(m_GroundJoint);
			m_GroundJoint = null;
			Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
			base.Tech.blockman.BlockTableRecentreEvent.Unsubscribe(OnBlockTableRecentred);
		}
	}

	private void UpdateTorque()
	{
		float magnitude = base.Tech.rbody.angularVelocity.magnitude;
		Vector3 normalized = base.Tech.rbody.angularVelocity.normalized;
		float f = Vector3.Dot(m_GroundJoint.axis, normalized);
		magnitude *= Mathf.Sign(f);
		if (Mathf.Abs(f) < 0.8f && magnitude > 0.001f)
		{
			d.LogWarning(string.Concat(base.name, ": up vector misaligned: ", m_GroundJoint.axis, " vs ", normalized, " at speed ", magnitude * 57.29578f));
		}
		float f2 = magnitude * 57.29578f;
		float num = Mathf.Abs(f2);
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		num2 = TurnControl;
		if (num2 != 0f)
		{
			num2 *= 0f - m_PivotAnchor.MaxTorque;
			if (Mathf.Sign(f2) == Mathf.Sign(num2))
			{
				num2 *= m_DriveTorqueFalloff.Evaluate(num / m_PivotAnchor.MaxAngVel);
			}
		}
		if (num > m_PivotAnchor.MaxAngVel)
		{
			float num5 = (num - m_PivotAnchor.MaxAngVel) / m_PivotAnchor.MaxAngVel;
			num3 = (0f - Mathf.Sign(f2)) * m_PivotAnchor.BrakeTorque * num5 * num5;
		}
		if (num > 0.001f)
		{
			num4 = (0f - Mathf.Sign(f2)) * m_PivotAnchor.CloggedTorque;
		}
		float f3 = num2 + num3 + num4;
		float num6 = m_PivotAnchor.MaxAngVel * m_MaxDeltaAngVel * ((float)Math.PI / 180f);
		float f4 = base.Tech.rbody.inertiaTensor.y * num6 / Time.deltaTime;
		float num7 = Mathf.Min(Mathf.Abs(f3), Mathf.Abs(f4)) * Mathf.Sign(f3);
		base.Tech.rbody.AddTorque(m_GroundJoint.axis * num7, ForceMode.Force);
	}

	private void OnControlInput(TankControl.ControlState data)
	{
		m_TurnControl = data.InputRotation.y;
	}

	private void OnTechModified()
	{
		ConfigureJoint();
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		if (m_GroundJoint != null && m_PivotAnchor != null)
		{
			m_GroundJoint.connectedAnchor += amountToMove;
		}
	}

	private void OnBlockTableRecentred()
	{
		if (m_PivotAnchor != null && m_GroundJoint != null)
		{
			m_PivotAnchor.InitRotation();
			m_GroundJoint.anchor = base.Tech.trans.InverseTransformVector(m_PivotAnchor.GroundPoint - base.Tech.trans.position);
		}
	}

	private void OnModuleCategoryStateChanged(ModuleControlCategory controllerModuleType, bool enabled)
	{
		if (controllerModuleType == ModuleControlCategory.Anchor)
		{
			bool flag = NumPossibleAnchors > 0 && base.Tech.BlockStateController.IsCategoryActive(ModuleControlCategory.Anchor);
			if (NumAnchored > 0 != flag)
			{
				base.Tech.TrySetAnchored(flag);
			}
		}
	}

	public NetworkedTechComponentID GetTechComponentID()
	{
		return NetworkedTechComponentID.TechAnchors;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		ConfigureJoint();
		writer.WritePackedInt32(m_AnchoredAnchors.Count);
		if (m_AnchoredAnchors.Count > 0)
		{
			foreach (ModuleAnchor anchoredAnchor in m_AnchoredAnchors)
			{
				writer.Write(anchoredAnchor.block.blockPoolID);
			}
		}
		writer.Write(m_GroundJoint != null);
		if (m_GroundJoint != null)
		{
			writer.Write(base.Tech.trans.rotation);
			writer.Write(m_GroundJoint.axis);
			writer.Write(m_GroundJoint.anchor);
			writer.Write(WorldPosition.FromScenePosition(m_GroundJoint.connectedAnchor));
		}
	}

	public void OnDeserialize(NetworkReader reader)
	{
		int num = reader.ReadPackedInt32();
		m_NetAnchoredBlocks.Clear();
		for (int i = 0; i < num; i++)
		{
			m_NetAnchoredBlocks.Add(reader.ReadUInt32());
		}
		m_NetGroundJoint = reader.ReadBoolean();
		if (m_NetGroundJoint)
		{
			m_NetAnchorTechRotation = reader.ReadQuaternion();
			m_NetJointAxis = reader.ReadVector3();
			m_NetAnchorPos = reader.ReadVector3();
			m_NetConnectedAnchor = reader.ReadWorldPosition();
		}
		if (base.Tech.blockman.blockCount > 0)
		{
			UpdateAnchorsFromNetwork();
		}
	}

	private void OnPool()
	{
		base.Tech.control.driveControlEvent.Subscribe(OnControlInput);
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
		base.Tech.FixedUpdateEvent.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		m_AnchoredAnchors.Clear();
		m_NumSkyAnchors = 0;
		m_NumAnchoredSkyAnchors = 0;
		m_PivotAnchor = null;
		m_TurnControl = 0f;
		m_NetAnchoredBlocks.Clear();
		m_NetGroundJoint = false;
		base.Tech.ResetPhysicsEvent.Subscribe(OnTechModified);
		base.Tech.BlockStateController.CategoryActiveChangedEvent.Subscribe(OnModuleCategoryStateChanged);
	}

	private void OnRecycle()
	{
		RemoveGroundJoint();
		base.Tech.ResetPhysicsEvent.Unsubscribe(OnTechModified);
		base.Tech.BlockStateController.CategoryActiveChangedEvent.Unsubscribe(OnModuleCategoryStateChanged);
	}

	private void OnFixedUpdate()
	{
		if (NumAnchored != 0)
		{
			base.Tech.grounded = true;
		}
		if ((bool)m_PivotAnchor)
		{
			UpdateTorque();
		}
	}

	private void OnUpdate()
	{
		if ((bool)m_PivotAnchor)
		{
			m_PivotAnchor.ResetStaticGeometry();
		}
	}
}
