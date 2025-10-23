#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleFasteningLink : Module, IHUDPowerToggleControlledModule
{
	private enum State
	{
		Unlinked,
		LinkRequested,
		Linked,
		UnlinkRequested
	}

	[Serializable]
	public struct LinkParts
	{
		public BlockTypes type;

		public Vector3 localPos;

		public Vector3 localRot;
	}

	[SerializeField]
	private BlockTypes m_CombinedBlock;

	[SerializeField]
	private LinkParts[] m_DetachedBlocks;

	[SerializeField]
	private float m_MaxLinkDist = 3f;

	[SerializeField]
	private float m_MaxLinkAngleOffsetDegrees = 35f;

	[SerializeField]
	private float m_LinkLerpDuration = 1.3f;

	[SerializeField]
	private Vector3 m_LocalLinkForward = Vector3.forward;

	[SerializeField]
	private Gradient m_LinkGradient;

	[SerializeField]
	private Gradient m_UnlinkGradient;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private float m_MaxLinkAngleOffset;

	private bool m_ContinuousLinkActive;

	private float m_SearchInterval = 1f;

	private float m_EvaluateInterval = 0.1f;

	private float m_LastSearch = float.MinValue;

	private float m_LastEvaluate = float.MinValue;

	private List<ModuleFasteningLink> m_NearbyFasteners;

	private ModuleFasteningLink m_LinkInProgressPair;

	private Coroutine m_LinkInProgressCoroutine;

	private State m_LinkState;

	private Bitfield<ObjectTypes> kSearchTypes = new Bitfield<ObjectTypes>(2);

	public bool CanLink
	{
		get
		{
			if (CircuitControlled)
			{
				if (base.block.CircuitReceiver.ShouldProcessInput)
				{
					return base.block.CircuitReceiver.CurrentHighestChargeFromNetwork > 0;
				}
				return false;
			}
			return true;
		}
	}

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private bool IsLinked => base.block.BlockType == m_CombinedBlock;

	bool IHUDPowerToggleControlledModule.PowerControlSetting
	{
		get
		{
			return !IsLinked;
		}
		set
		{
			if (IsLinked)
			{
				Unlink();
			}
			else
			{
				TryLink();
			}
		}
	}

	Gradient IHUDPowerToggleControlledModule.ToggleGradientOverride
	{
		get
		{
			if (!IsLinked)
			{
				return m_LinkGradient;
			}
			return m_UnlinkGradient;
		}
	}

	bool IHUDPowerToggleControlledModule.AutoCloseMenuOnComplete => false;

	bool IHUDPowerToggleControlledModule.CanOpenMenuOnBlock
	{
		get
		{
			if (m_LinkInProgressPair.IsNull())
			{
				return !CircuitControlled;
			}
			return false;
		}
	}

	private void Unlink()
	{
		if (base.block.IsAttached && base.block.BlockType == m_CombinedBlock)
		{
			TankPreset.BlockSpec[] array = new TankPreset.BlockSpec[m_DetachedBlocks.Length];
			Vector3 cachedLocalPosition = base.block.cachedLocalPosition;
			OrthoRotation cachedLocalRotation = base.block.cachedLocalRotation;
			byte skinIndex = base.block.GetSkinIndex();
			for (int i = 0; i < m_DetachedBlocks.Length; i++)
			{
				Quaternion rotation = cachedLocalRotation * Quaternion.Euler(m_DetachedBlocks[i].localRot);
				array[i] = new TankPreset.BlockSpec
				{
					m_BlockType = m_DetachedBlocks[i].type,
					position = cachedLocalPosition + cachedLocalRotation * m_DetachedBlocks[i].localPos,
					orthoRotation = new OrthoRotation(rotation),
					m_SkinID = skinIndex
				};
			}
			Singleton.Manager<ManLooseBlocks>.inst.HostReplaceBlock(new TankBlock[1] { base.block }, array);
		}
	}

	private void Link(ModuleFasteningLink counterpart)
	{
		if (base.block.IsAttached && counterpart.block.IsAttached && base.block.tank == counterpart.block.tank && base.block.BlockType == m_DetachedBlocks[0].type)
		{
			ReplacePartsWithWhole(counterpart);
		}
	}

	private void AddBlocksToTech(Tank sourceT, Tank destT, TankBlock sourceBlock, TankBlock destBlock)
	{
		_ = Quaternion.Inverse(sourceT.trans.rotation) * destT.trans.rotation;
		Vector3 cachedLocalPosition = sourceBlock.cachedLocalPosition;
		_ = destBlock.cachedLocalPosition;
		_ = -cachedLocalPosition + destBlock.cachedLocalRotation * m_DetachedBlocks[1].localPos;
		(TankBlock, TankPreset.BlockSpec)[] array = new(TankBlock, TankPreset.BlockSpec)[sourceT.blockman.blockCount];
		int num = 0;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = sourceT.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			TankPreset.BlockSpec item = default(TankPreset.BlockSpec);
			item.InitFromBlockState(current, saveRuntimeState: true);
			Vector3 vector = sourceT.trans.position + sourceT.trans.rotation * current.cachedLocalPosition;
			Vector3 vector2 = Quaternion.Inverse(destT.trans.rotation) * (vector - destT.trans.position);
			item.position = vector2;
			Quaternion quaternion = sourceT.trans.rotation * current.cachedLocalRotation;
			Quaternion rotation = (Quaternion.Inverse(destT.trans.rotation) * quaternion).AlignToAxis();
			OrthoRotation orthoRotation = new OrthoRotation(rotation);
			item.orthoRotation = orthoRotation;
			array[num++] = (current, item);
		}
		sourceT.blockman.Disintegrate(applyPhysicsKick: false);
		using ManSpawn.PopulateTechHelper populateTechHelper = new ManSpawn.PopulateTechHelper(destT, spawningNew: false, recycleFailedAdds: false, null, allowHeadlessTech: false, tryDeployAnchors: false, reportFailure: true, allowAttachBlocksWithoutLinks: true);
		(TankBlock, TankPreset.BlockSpec)[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			var (tankBlock, serialData) = array2[i];
			populateTechHelper.AddBlock(tankBlock, serialData, alreadyAttached: false);
		}
	}

	private void ReplacePartsWithWhole(ModuleFasteningLink counterpart)
	{
		TankBlock[] blocksToRemove = new TankBlock[2] { base.block, counterpart.block };
		TankPreset.BlockSpec[] array = new TankPreset.BlockSpec[1];
		LinkParts linkParts = m_DetachedBlocks[0];
		Vector3 cachedLocalPosition = base.block.cachedLocalPosition;
		OrthoRotation cachedLocalRotation = base.block.cachedLocalRotation;
		byte skinIndex = base.block.GetSkinIndex();
		Quaternion rotation = cachedLocalRotation * Quaternion.Inverse(Quaternion.Euler(linkParts.localRot));
		array[0] = new TankPreset.BlockSpec
		{
			m_BlockType = m_CombinedBlock,
			position = cachedLocalPosition + cachedLocalRotation * linkParts.localPos,
			orthoRotation = new OrthoRotation(rotation),
			m_SkinID = skinIndex
		};
		Singleton.Manager<ManLooseBlocks>.inst.HostReplaceBlock(blocksToRemove, array);
	}

	private void TryLink()
	{
		if (base.block.BlockType != m_DetachedBlocks[0].type)
		{
			return;
		}
		ManVisible.SearchIterator searchIterator = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(base.block.trans.position, m_MaxLinkDist, kSearchTypes);
		ModuleFasteningLink moduleFasteningLink = null;
		float num = float.MaxValue;
		foreach (Visible item in searchIterator)
		{
			ModuleFasteningLink component;
			if (item.block.IsNotNull() && (object)item.block != base.block && (object)(component = item.block.GetComponent<ModuleFasteningLink>()) != null && CanPairTo(component) && CanAccomodateBlocks(component) && Vector3.Dot(base.block.trans.rotation * m_LocalLinkForward, item.block.trans.rotation * -component.m_LocalLinkForward) > m_MaxLinkAngleOffset)
			{
				Vector3 vector = component.block.trans.position - base.block.trans.position;
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude < num && Vector3.Dot(base.block.trans.rotation * m_LocalLinkForward, vector.normalized) > m_MaxLinkAngleOffset)
				{
					moduleFasteningLink = component;
					num = sqrMagnitude;
				}
			}
		}
		if (moduleFasteningLink != null && m_LinkInProgressCoroutine == null)
		{
			m_LinkInProgressCoroutine = StartCoroutine(ProcessLinking(base.block, moduleFasteningLink.block));
		}
	}

	private IEnumerator ProcessLinking(TankBlock instigator, TankBlock complement)
	{
		bool onSameTech = (object)instigator.tank == complement.tank;
		bool destAnchored = instigator.tank.IsAnchored;
		bool isAnchored = complement.tank.IsAnchored;
		ModuleFasteningLink pairFasterner = complement.GetComponent<ModuleFasteningLink>();
		float num = m_LinkLerpDuration;
		if (onSameTech)
		{
			num = 0f;
		}
		else if (destAnchored && isAnchored)
		{
			Quaternion quaternion = complement.tank.trans.rotation.AlignToAxis(instigator.tank.trans.rotation);
			Vector3 vector = complement.tank.trans.position + quaternion * complement.trans.localPosition;
			Vector3 vector2 = instigator.trans.position + instigator.trans.rotation * m_DetachedBlocks[1].localPos;
			Vector3 other = complement.tank.trans.position + (vector2 - vector);
			if (!complement.tank.trans.position.Approximately(in other, 0.05f) || !complement.tank.trans.rotation.eulerAngles.Approximately(quaternion.eulerAngles, 0.5f))
			{
				m_LinkInProgressCoroutine = null;
				yield break;
			}
			num = 0f;
		}
		else if (isAnchored)
		{
			TankBlock tankBlock = complement;
			complement = instigator;
			instigator = tankBlock;
			destAnchored = isAnchored;
		}
		if (instigator.tank.beam.IsActive)
		{
			instigator.tank.beam.EnableBeam(enable: false);
		}
		if (complement.tank.beam.IsActive)
		{
			complement.tank.beam.EnableBeam(enable: false);
		}
		Vector3 startPos = complement.tank.trans.position;
		Quaternion startRot = complement.tank.trans.rotation;
		float endTime = Time.time + num;
		m_LinkInProgressPair = pairFasterner;
		pairFasterner.m_LinkInProgressPair = this;
		if (!onSameTech)
		{
			complement.tank.rbody.isKinematic = true;
		}
		do
		{
			if (!CanPairTo(pairFasterner))
			{
				if (complement.IsAttached)
				{
					complement.tank.rbody.isKinematic = false;
				}
				m_LinkInProgressPair = null;
				pairFasterner.m_LinkInProgressPair = null;
				m_LinkInProgressCoroutine = null;
				yield break;
			}
			Quaternion quaternion2 = complement.tank.trans.rotation.AlignToAxis(instigator.tank.trans.rotation);
			Vector3 vector3 = startPos + quaternion2 * complement.trans.localPosition;
			Vector3 vector4 = instigator.trans.position + instigator.trans.rotation * m_DetachedBlocks[1].localPos;
			Vector3 b = startPos + (vector4 - vector3);
			float t = Mathf.InverseLerp(endTime - m_LinkLerpDuration, endTime, Time.time);
			complement.tank.trans.position = Vector3.Lerp(startPos, b, t);
			complement.tank.trans.rotation = Quaternion.Slerp(startRot, quaternion2, t);
			yield return null;
		}
		while (Time.time <= endTime);
		complement.tank.rbody.isKinematic = false;
		m_LinkInProgressPair = null;
		pairFasterner.m_LinkInProgressPair = null;
		if (!CanAccomodateBlocks(pairFasterner))
		{
			m_LinkInProgressCoroutine = null;
			yield break;
		}
		if (onSameTech || !complement.IsAttached)
		{
			ModuleFasteningLink component = instigator.GetComponent<ModuleFasteningLink>();
			ModuleFasteningLink component2 = complement.GetComponent<ModuleFasteningLink>();
			component.ReplacePartsWithWhole(component2);
		}
		else
		{
			bool isPlayer = complement.tank.IsPlayer;
			TankBlock rootBlock = null;
			if (isPlayer)
			{
				rootBlock = complement.tank.blockman.GetRootBlock();
			}
			instigator.tank.control.MergeSchemesFrom(complement.tank.control);
			AddBlocksToTech(complement.tank, instigator.tank, complement, instigator);
			if (isPlayer)
			{
				instigator.tank.blockman.SetRootBlock(rootBlock);
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(instigator.tank);
			}
			Tank tank = instigator.tank;
			ModuleFasteningLink component3 = instigator.GetComponent<ModuleFasteningLink>();
			ModuleFasteningLink component4 = complement.GetComponent<ModuleFasteningLink>();
			component3.ReplacePartsWithWhole(component4);
			tank.blockman.CleanupInvalidTechBlocks();
			if (destAnchored)
			{
				tank.Anchors.TryDeployUnanchoredAnchors();
			}
		}
		m_LinkInProgressCoroutine = null;
	}

	private bool CanPairTo(ModuleFasteningLink pair)
	{
		if (CanLink && pair.CanLink && base.block.IsAttached && pair.block.IsAttached && base.block.tank.IsFriendly(pair.block.tank.Team) && (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || base.block.tank.netTech.NetPlayer.IsNull() || pair.block.tank.netTech.NetPlayer.IsNull()) && (m_LinkInProgressPair.IsNull() || (object)m_LinkInProgressPair == pair))
		{
			if (!pair.m_LinkInProgressPair.IsNull())
			{
				return (object)pair.m_LinkInProgressPair == this;
			}
			return true;
		}
		return false;
	}

	private bool CanAccomodateBlocks(ModuleFasteningLink pair)
	{
		Tank tank = pair.block.tank;
		Tank tank2 = base.block.tank;
		if ((object)tank == tank2)
		{
			return true;
		}
		Quaternion quaternion = tank.trans.rotation.AlignToAxis(tank2.trans.rotation);
		Quaternion rotation = Quaternion.Inverse(tank2.trans.rotation) * quaternion;
		OrthoRotation orthoRotation = new OrthoRotation(rotation);
		Vector3 vector = base.block.cachedLocalPosition + base.block.cachedLocalRotation * m_DetachedBlocks[1].localPos;
		Vector3 vector2 = vector - tank2.blockBounds.center;
		Vector3 vector3 = orthoRotation * (pair.block.cachedLocalPosition - tank.blockBounds.center);
		Vector3 vector4 = vector - orthoRotation * pair.block.cachedLocalPosition;
		Bounds blockBounds = tank.blockBounds;
		blockBounds.extents = orthoRotation * blockBounds.extents;
		Bounds blockBounds2 = tank2.blockBounds;
		blockBounds2.center = Vector3.zero;
		blockBounds.center = vector2 - vector3;
		blockBounds2.Encapsulate(blockBounds);
		blockBounds2.size += Vector3.one;
		if (blockBounds2.size.x > (float)Singleton.Manager<ManSpawn>.inst.BlockLimit || blockBounds2.size.y > (float)Singleton.Manager<ManSpawn>.inst.BlockLimit || blockBounds2.size.z > (float)Singleton.Manager<ManSpawn>.inst.BlockLimit)
		{
			d.Log("Joining tech '" + tank.name + "' and '" + tank2.name + "' would exceed maximum build size! Not allowed");
			return false;
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			Vector3 vector5 = vector4 + orthoRotation * current.cachedLocalPosition;
			Quaternion quaternion2 = (Quaternion)orthoRotation * (Quaternion)current.cachedLocalRotation;
			IntVector3[] filledCells = current.filledCells;
			foreach (IntVector3 intVector in filledCells)
			{
				Vector3 vector6 = vector5 + quaternion2 * intVector;
				if (tank2.blockman.GetBlockAtPosition(vector6) != null)
				{
					d.Log("Joining tech '" + tank.name + "' and '" + tank2.name + "' would force multiple blocks in the same cell! Not allowed");
					return false;
				}
			}
		}
		return true;
	}

	private void OnAttached()
	{
	}

	private void OnDetaching()
	{
		if (m_LinkInProgressCoroutine != null)
		{
			StopCoroutine(m_LinkInProgressCoroutine);
			m_LinkInProgressCoroutine = null;
			ModuleFasteningLink linkInProgressPair = m_LinkInProgressPair;
			if (linkInProgressPair != null)
			{
				linkInProgressPair.block.tank.rbody.isKinematic = false;
				linkInProgressPair.m_LinkInProgressPair = null;
			}
		}
		m_LinkInProgressPair = null;
		m_ContinuousLinkActive = false;
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		if (charge > 0)
		{
			m_ContinuousLinkActive = true;
			m_LastSearch = float.MinValue;
			m_LastEvaluate = float.MinValue;
		}
		else if (m_ContinuousLinkActive)
		{
			m_ContinuousLinkActive = false;
			if (IsLinked)
			{
				Unlink();
			}
		}
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		m_ContinuousLinkActive = false;
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(ContinuouslyTryLinkNearby);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
		}
		m_MaxLinkAngleOffset = Mathf.Cos(m_MaxLinkAngleOffsetDegrees * ((float)Math.PI / 180f));
	}

	private void OnSpawn()
	{
		m_LinkInProgressPair = null;
		m_ContinuousLinkActive = false;
	}

	private void ContinuouslyTryLinkNearby()
	{
		if (!m_ContinuousLinkActive || !base.block.CircuitReceiver.ShouldProcessInput || m_LinkInProgressCoroutine != null || base.block.BlockType != m_DetachedBlocks[0].type)
		{
			return;
		}
		float time = Time.time;
		if (time > m_LastSearch + m_SearchInterval)
		{
			ManVisible.SearchIterator searchIterator = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(base.block.trans.position, m_MaxLinkDist, kSearchTypes);
			m_LastSearch = time;
			if (m_NearbyFasteners == null)
			{
				m_NearbyFasteners = new List<ModuleFasteningLink>();
			}
			m_NearbyFasteners.Clear();
			foreach (Visible item in searchIterator)
			{
				ModuleFasteningLink component;
				if (item.block.IsNotNull() && (object)item.block != base.block && (object)(component = item.block.GetComponent<ModuleFasteningLink>()) != null)
				{
					m_NearbyFasteners.Add(component);
				}
			}
		}
		if (!(time > m_LastEvaluate + m_EvaluateInterval))
		{
			return;
		}
		m_LastEvaluate = time;
		ModuleFasteningLink moduleFasteningLink = null;
		float num = float.MaxValue;
		foreach (ModuleFasteningLink nearbyFastener in m_NearbyFasteners)
		{
			if (CanPairTo(nearbyFastener) && Vector3.Dot(base.block.trans.rotation * m_LocalLinkForward, nearbyFastener.block.trans.rotation * -nearbyFastener.m_LocalLinkForward) > m_MaxLinkAngleOffset)
			{
				Vector3 vector = nearbyFastener.block.trans.position - base.block.trans.position;
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude < num && Vector3.Dot(base.block.trans.rotation * m_LocalLinkForward, vector.normalized) > m_MaxLinkAngleOffset)
				{
					moduleFasteningLink = nearbyFastener;
					num = sqrMagnitude;
				}
			}
		}
		if (moduleFasteningLink.IsNotNull() && CanAccomodateBlocks(moduleFasteningLink))
		{
			m_LinkInProgressCoroutine = StartCoroutine(ProcessLinking(base.block, moduleFasteningLink.block));
		}
	}
}
