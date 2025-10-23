using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockPlacementPreviewHandler : MonoBehaviour
{
	public struct APConnection
	{
		public Vector3 blockAP;

		public TankBlock otherBlock;
	}

	private TankBlock m_HeldBlock;

	private IBlockPlacementPreview m_HeldBlockPreview;

	private BlockPlacementCollector.Placement m_CurrentPlacement;

	private List<BlockManager.BlockAttachment> m_APConnections = new List<BlockManager.BlockAttachment>();

	private List<APConnection> m_BlockAPConnections = new List<APConnection>();

	private HashSet<IBlockPlacementPreview> m_LastPlacementPreviews = new HashSet<IBlockPlacementPreview>();

	private HashSet<IBlockPlacementPreview> m_CurrentPlacementPreviews = new HashSet<IBlockPlacementPreview>();

	public static Vector3 TransformBlockAPToTargetBlockAP(TankBlock currentBlock, Vector3 apOnCurrentBlock, TankBlock targetBlock)
	{
		return TechPlacementToBlockAP(BlockAPToTechPlacement(apOnCurrentBlock, currentBlock.cachedLocalPosition, currentBlock.cachedLocalRotation), targetBlock.cachedLocalPosition, targetBlock.cachedLocalRotation);
	}

	public static Vector3 TechPlacementToBlockAP(IntVector3 pos, Vector3 blockLocalPos, Quaternion blockLocalRot)
	{
		Vector3 vector = Quaternion.Inverse(blockLocalRot) * (pos.APtoLocal() - blockLocalPos);
		return new Vector3(Mathf.Round(vector.x * 2f), Mathf.Round(vector.y * 2f), Mathf.Round(vector.z * 2f)) * 0.5f;
	}

	public static IntVector3 BlockAPToTechPlacement(Vector3 blockAPPos, Vector3 blockLocalPos, Quaternion blockLocalRot)
	{
		return (blockLocalRot * blockAPPos + blockLocalPos).LocalToAP();
	}

	private void PrepareBlockAPConnections(IEnumerable<BlockManager.BlockAttachment> attachments, Vector3 blockLocalPos, Quaternion blockLocalRot, TankBlock overrideOther = null)
	{
		m_BlockAPConnections.Clear();
		foreach (BlockManager.BlockAttachment attachment in attachments)
		{
			m_BlockAPConnections.Add(new APConnection
			{
				blockAP = TechPlacementToBlockAP(attachment.apPosLocal, blockLocalPos, blockLocalRot),
				otherBlock = (overrideOther ?? attachment.other)
			});
		}
	}

	private void OnPlacementReadyToAttachChanged(TankBlock blockBeingPlaced, Tank targetTech, BlockPlacementCollector.Placement newPlacement)
	{
		bool flag = false;
		if (blockBeingPlaced != m_HeldBlock)
		{
			flag = true;
			if (blockBeingPlaced == null)
			{
				m_HeldBlockPreview?.TryPreviewAttachments(null);
			}
			m_HeldBlock = blockBeingPlaced;
			m_HeldBlockPreview = m_HeldBlock?.GetComponent<IBlockPlacementPreview>();
		}
		if (newPlacement != m_CurrentPlacement)
		{
			flag = true;
			m_CurrentPlacement = newPlacement;
			if (newPlacement == null)
			{
				m_HeldBlockPreview?.TryPreviewAttachments(null);
			}
		}
		if (!flag)
		{
			return;
		}
		if (m_HeldBlock != null && targetTech != null && newPlacement != null)
		{
			targetTech.blockman.TryGetBlockAttachments(m_HeldBlock, newPlacement.localPos, newPlacement.orthoRot, m_APConnections);
			if (m_HeldBlockPreview != null)
			{
				PrepareBlockAPConnections(m_APConnections, newPlacement.localPos, newPlacement.orthoRot);
				m_HeldBlockPreview.TryPreviewAttachments(m_BlockAPConnections);
			}
			foreach (IGrouping<TankBlock, BlockManager.BlockAttachment> item in from a in m_APConnections
				group a by a.other into a
				where a != null
				select a)
			{
				TankBlock key = item.Key;
				IBlockPlacementPreview component = key.GetComponent<IBlockPlacementPreview>();
				if (component != null)
				{
					PrepareBlockAPConnections(item, key.cachedLocalPosition, key.cachedLocalRotation, m_HeldBlock);
					component.TryPreviewAttachments(m_BlockAPConnections);
					m_CurrentPlacementPreviews.Add(component);
					m_LastPlacementPreviews.Remove(component);
				}
			}
		}
		m_BlockAPConnections.Clear();
		m_APConnections.Clear();
		foreach (IBlockPlacementPreview lastPlacementPreview in m_LastPlacementPreviews)
		{
			lastPlacementPreview?.TryPreviewAttachments(null);
		}
		m_LastPlacementPreviews.Clear();
		HashSet<IBlockPlacementPreview> lastPlacementPreviews = m_LastPlacementPreviews;
		m_LastPlacementPreviews = m_CurrentPlacementPreviews;
		m_CurrentPlacementPreviews = lastPlacementPreviews;
	}

	private void Start()
	{
		Singleton.Manager<ManTechBuilder>.inst.PlacementReadyToAttachChangedEvent.Subscribe(OnPlacementReadyToAttachChanged);
	}

	private void OnDestroy()
	{
		Singleton.Manager<ManTechBuilder>.inst.PlacementReadyToAttachChangedEvent.Unsubscribe(OnPlacementReadyToAttachChanged);
	}
}
