using System;
using System.Collections.Generic;
using UnityEngine;

public class ManLooseChunkLimiter : Singleton.Manager<ManLooseChunkLimiter>, Mode.IManagerModeEvents
{
	private enum State
	{
		Counting,
		SearchingForVisibleToDelete
	}

	private struct VisibleSearcher
	{
		private ManLooseChunkLimiter m_Limiter;

		private TileManager.TileIterator m_TileIterator;

		private WorldTile m_Tile;

		private Dictionary<int, Visible>.Enumerator m_VisibleEnum;

		private ObjectTypes m_ObjectType;

		private Vector3 m_PlayerPos;

		private int m_SearchedThisEnum;

		public float m_BestScore;

		public Visible m_BestVisible;

		public int m_BestVisibleID;

		public int m_TotalSearched;

		private Dictionary<Vector2Int, int> m_ClumpCount;

		private float m_ClumpSize;

		private float m_ClumpWeighting;

		private int[] m_ChunkTypeCount;

		private int[] m_BlockTypeCount;

		private float m_DuplicateWeighting;

		private const ObjectTypes k_FirstObjectType = ObjectTypes.Chunk;

		public void Restart(ManLooseChunkLimiter limiter)
		{
			m_Limiter = limiter;
			if (m_ClumpCount == null)
			{
				m_ClumpCount = new Dictionary<Vector2Int, int>();
				m_ChunkTypeCount = new int[EnumValuesIterator<ChunkTypes>.Count];
				m_BlockTypeCount = new int[EnumValuesIterator<BlockTypes>.Count];
			}
			Restart();
		}

		public void Restart()
		{
			m_Tile = null;
			m_BestScore = float.MinValue;
			m_BestVisible = null;
			m_ObjectType = ObjectTypes.Chunk;
			m_PlayerPos = Singleton.playerPos;
			m_TotalSearched = 0;
			m_SearchedThisEnum = 0;
			m_ClumpSize = m_Limiter.m_ClumpSize;
			m_ClumpWeighting = ((m_ClumpSize > 0f) ? m_Limiter.m_ClumpWeighting : 0f);
			m_ClumpCount.Clear();
			m_DuplicateWeighting = m_Limiter.m_DuplicateWeighting;
			if (m_DuplicateWeighting != 0f)
			{
				Array.Clear(m_ChunkTypeCount, 0, m_ChunkTypeCount.Length);
				Array.Clear(m_BlockTypeCount, 0, m_BlockTypeCount.Length);
			}
			m_TileIterator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(WorldTile.State.Populated);
			if (m_TileIterator.MoveNext())
			{
				m_Tile = m_TileIterator.Current;
				m_VisibleEnum = m_Tile.Visibles[(int)m_ObjectType].GetEnumerator();
			}
		}

		private bool NextTile()
		{
			try
			{
				if (m_TileIterator.MoveNext())
				{
					m_Tile = m_TileIterator.Current;
					m_ObjectType = ObjectTypes.Chunk;
					m_VisibleEnum = m_Tile.Visibles[(int)m_ObjectType].GetEnumerator();
				}
				else
				{
					m_Tile = null;
				}
			}
			catch (InvalidOperationException)
			{
				Restart();
			}
			return m_Tile != null;
		}

		private void ScoreVisible(Visible visible)
		{
			if (visible.holderStack != null)
			{
				return;
			}
			Vector3 position = visible.trans.position;
			float num = (m_PlayerPos - position).sqrMagnitude;
			if (m_ObjectType == ObjectTypes.Chunk)
			{
				num *= m_Limiter.m_ChunkWeighting;
			}
			if (m_ClumpWeighting != 0f)
			{
				Vector2Int key = new Vector2Int(Mathf.FloorToInt(position.x / m_ClumpSize), Mathf.FloorToInt(position.z / m_ClumpSize));
				m_ClumpCount.TryGetValue(key, out var value);
				m_ClumpCount[key] = value + 1;
				num *= 1f + (float)value * m_ClumpWeighting;
			}
			if (m_DuplicateWeighting != 0f)
			{
				if (visible.type == ObjectTypes.Chunk)
				{
					num *= 1f + (float)m_ChunkTypeCount[visible.ItemType]++ * m_DuplicateWeighting;
				}
				else if (visible.type == ObjectTypes.Block)
				{
					num *= 1f + (float)m_BlockTypeCount[visible.ItemType]++ * m_DuplicateWeighting;
				}
			}
			Rigidbody rbody = visible.rbody;
			if (rbody != null && !rbody.IsSleeping() && (rbody.velocity.sqrMagnitude > 0.01f || rbody.angularVelocity.sqrMagnitude > 0.01f))
			{
				num *= 0.25f;
			}
			Vector3 vector = Singleton.camera.WorldToViewportPoint(position);
			if (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f)
			{
				num *= m_Limiter.m_OnScreenWeighting;
			}
			if (num > m_BestScore && Singleton.Manager<ManPointer>.inst.DraggingItem != visible)
			{
				m_BestScore = num;
				m_BestVisible = visible;
				m_BestVisibleID = visible.ID;
			}
		}

		public bool Tick(int numSteps)
		{
			for (int i = 0; i < numSteps; i++)
			{
				bool flag;
				if (i == 0)
				{
					try
					{
						flag = m_VisibleEnum.MoveNext();
					}
					catch (InvalidOperationException)
					{
						m_VisibleEnum = m_Tile.Visibles[(int)m_ObjectType].GetEnumerator();
						flag = m_VisibleEnum.MoveNext();
						m_SearchedThisEnum = 0;
					}
				}
				else
				{
					flag = m_VisibleEnum.MoveNext();
				}
				if (flag)
				{
					m_SearchedThisEnum++;
					ScoreVisible(m_VisibleEnum.Current.Value);
					continue;
				}
				m_TotalSearched += m_SearchedThisEnum;
				m_SearchedThisEnum = 0;
				if (m_ObjectType == ObjectTypes.Chunk)
				{
					m_ObjectType = ObjectTypes.Block;
					m_VisibleEnum = m_Tile.Visibles[(int)m_ObjectType].GetEnumerator();
				}
				else if (!NextTile())
				{
					return false;
				}
			}
			return true;
		}
	}

	[SerializeField]
	[Tooltip("Interval in seconds between checks on the total number of loose chunks/blocks")]
	private float m_CountInterval;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Maximum number of items to check per frame, as a multiplier on the current maximum")]
	private float m_SearchTicksPerFrameMultiplier;

	[Header("Weighting")]
	[SerializeField]
	[Tooltip("Mutliplier to distance weighting of block/chunk when it's on-screen")]
	private float m_OnScreenWeighting;

	[SerializeField]
	[Tooltip("Mutliplier to distance weighting of chunk as opposed to blocks")]
	private float m_ChunkWeighting;

	[SerializeField]
	[Tooltip("Size of tiles used to weight removal of items from denser areas")]
	private float m_ClumpSize;

	[Tooltip("Modifier to weighting for items with a lot close together. Multiplier of (1 + N.ClumpWeighting) is applied, where N is the number of items in one 'clump'.")]
	[SerializeField]
	private float m_ClumpWeighting;

	[Tooltip("Modifier to weighting for identical items. Multiplier of (1 + N.DuplicateWeighting) is applied, where N is the number of the same type of loose block/chunk.")]
	[SerializeField]
	private float m_DuplicateWeighting;

	[Header("Debug")]
	[SerializeField]
	[Tooltip("Effect to spawn when removing loose items (intended for debugging)")]
	private Transform m_RemoveParticles;

	private VisibleSearcher m_VisibleSearcher;

	private State m_State;

	private int m_VisiblesToRecycleCount;

	private float m_NextCountTime;

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		bool flag = !Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		base.enabled = flag;
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
		base.enabled = false;
	}

	private void Awake()
	{
		base.enabled = false;
	}

	private void Update()
	{
		if (!(Singleton.playerTank != null) || !(Singleton.camera != null))
		{
			return;
		}
		int maxLooseItemCount = QualitySettingsExtended.MaxLooseItemCount;
		switch (m_State)
		{
		case State.Counting:
			if (maxLooseItemCount > 0 && Time.realtimeSinceStartup >= m_NextCountTime)
			{
				int num = 0;
				int num2 = 0;
				TileManager.TileIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(WorldTile.State.Populated).GetEnumerator();
				while (enumerator.MoveNext())
				{
					WorldTile current = enumerator.Current;
					num += current.Visibles[2].Count;
					num2 += current.Visibles[4].Count;
				}
				if (num + num2 > maxLooseItemCount)
				{
					m_VisiblesToRecycleCount = num + num2 - maxLooseItemCount;
					m_State = State.SearchingForVisibleToDelete;
					m_VisibleSearcher.Restart(this);
				}
				m_NextCountTime = Time.realtimeSinceStartup + m_CountInterval;
			}
			break;
		case State.SearchingForVisibleToDelete:
		{
			int numSteps = Mathf.CeilToInt((float)maxLooseItemCount * m_SearchTicksPerFrameMultiplier);
			if (m_VisibleSearcher.Tick(numSteps))
			{
				break;
			}
			if (m_VisibleSearcher.m_TotalSearched < maxLooseItemCount)
			{
				m_VisiblesToRecycleCount = 0;
			}
			else
			{
				Visible bestVisible = m_VisibleSearcher.m_BestVisible;
				if (bestVisible != null && bestVisible.gameObject.activeSelf && bestVisible.ID == m_VisibleSearcher.m_BestVisibleID && bestVisible != Singleton.Manager<ManPointer>.inst.DraggingItem && bestVisible.holderStack == null && bestVisible.ManagedByTile && bestVisible.tileCache.tile != null && !bestVisible.InBeam && (bestVisible.block == null || bestVisible.block.tank == null) && (bestVisible.rbody == null || bestVisible.rbody.useGravity))
				{
					if (m_RemoveParticles != null)
					{
						m_RemoveParticles.Spawn(bestVisible.trans.position);
					}
					bestVisible.transform.Recycle();
					m_VisiblesToRecycleCount--;
				}
			}
			if (m_VisiblesToRecycleCount <= 0)
			{
				m_State = State.Counting;
			}
			else
			{
				m_VisibleSearcher.Restart();
			}
			break;
		}
		}
	}
}
