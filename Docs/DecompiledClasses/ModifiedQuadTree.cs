using System.Collections.Generic;
using UnityEngine;

public struct ModifiedQuadTree
{
	private enum QuadPos
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	private const int MAX_OBJECTS = 0;

	private const int MAX_LEVELS = 7;

	private int m_Level;

	private List<Rect> m_Objects;

	private Rect m_Bounds;

	private bool m_IsSplit;

	private ModifiedQuadTree[] m_Nodes;

	private static readonly BitfieldNonAlloc<QuadPos> k_TopArea = new BitfieldNonAlloc<QuadPos>(new QuadPos[2]
	{
		QuadPos.TopLeft,
		QuadPos.TopRight
	});

	private static readonly BitfieldNonAlloc<QuadPos> k_BottomArea = new BitfieldNonAlloc<QuadPos>(new QuadPos[2]
	{
		QuadPos.BottomLeft,
		QuadPos.BottomRight
	});

	private static readonly BitfieldNonAlloc<QuadPos> k_LeftArea = new BitfieldNonAlloc<QuadPos>(new QuadPos[2]
	{
		QuadPos.TopLeft,
		QuadPos.BottomLeft
	});

	private static readonly BitfieldNonAlloc<QuadPos> k_RightArea = new BitfieldNonAlloc<QuadPos>(new QuadPos[2]
	{
		QuadPos.TopRight,
		QuadPos.BottomRight
	});

	private static readonly BitfieldNonAlloc<QuadPos> k_All = new BitfieldNonAlloc<QuadPos>(new QuadPos[4]
	{
		QuadPos.TopLeft,
		QuadPos.TopRight,
		QuadPos.BottomLeft,
		QuadPos.BottomRight
	});

	public ModifiedQuadTree(int level, Rect bounds)
	{
		m_Level = level;
		m_Objects = null;
		m_Bounds = bounds;
		m_IsSplit = false;
		m_Nodes = new ModifiedQuadTree[4];
	}

	public void Insert(Rect rect)
	{
		if (m_IsSplit)
		{
			BitfieldNonAlloc<QuadPos> quadsContaining = GetQuadsContaining(rect);
			if (!quadsContaining.IsNull)
			{
				if (quadsContaining.Contains(0))
				{
					m_Nodes[0].Insert(rect);
				}
				if (quadsContaining.Contains(1))
				{
					m_Nodes[1].Insert(rect);
				}
				if (quadsContaining.Contains(2))
				{
					m_Nodes[2].Insert(rect);
				}
				if (quadsContaining.Contains(3))
				{
					m_Nodes[3].Insert(rect);
				}
				return;
			}
		}
		if (m_Objects == null)
		{
			m_Objects = new List<Rect>();
		}
		m_Objects.Add(rect);
		if (m_Objects.Count <= 0 || m_Level >= 7 || m_IsSplit)
		{
			return;
		}
		Split();
		for (int num = m_Objects.Count - 1; num >= 0; num--)
		{
			BitfieldNonAlloc<QuadPos> quadsContaining2 = GetQuadsContaining(m_Objects[num]);
			if (!quadsContaining2.IsNull)
			{
				if (quadsContaining2.Contains(0))
				{
					m_Nodes[0].Insert(m_Objects[num]);
				}
				if (quadsContaining2.Contains(1))
				{
					m_Nodes[1].Insert(m_Objects[num]);
				}
				if (quadsContaining2.Contains(2))
				{
					m_Nodes[2].Insert(m_Objects[num]);
				}
				if (quadsContaining2.Contains(3))
				{
					m_Nodes[3].Insert(m_Objects[num]);
				}
				m_Objects.RemoveAt(num);
			}
		}
	}

	public void Split()
	{
		int num = (int)(m_Bounds.width / 2f);
		int num2 = (int)(m_Bounds.height / 2f);
		int num3 = (int)m_Bounds.x;
		int num4 = (int)m_Bounds.y;
		m_IsSplit = true;
		m_Nodes[0] = new ModifiedQuadTree(m_Level + 1, new Rect(num3, num4 + num2, num, num2));
		m_Nodes[1] = new ModifiedQuadTree(m_Level + 1, new Rect(num3 + num, num4 + num2, num, num2));
		m_Nodes[2] = new ModifiedQuadTree(m_Level + 1, new Rect(num3, num4, num, num2));
		m_Nodes[3] = new ModifiedQuadTree(m_Level + 1, new Rect(num3 + num, num4, num, num2));
	}

	public bool IsWalkable(int x, int y, int radius)
	{
		int gridSquareSize = Singleton.Manager<ManPath>.inst.GridSquareSize;
		int num = gridSquareSize / 2;
		int num2 = (int)((float)(x * gridSquareSize) + m_Bounds.x - (float)radius);
		int num3 = (int)((float)(y * gridSquareSize) + m_Bounds.y - (float)radius);
		float num4 = (float)radius * 2f;
		Rect rect = new Rect(num2 + num, num3 + num, num4, num4);
		return !NotWalkable(rect, 6);
	}

	public void Draw(Vector3 drawOrigin, bool showOnlyFilled)
	{
		float y = 150f;
		float x = drawOrigin.x + m_Bounds.x;
		float x2 = drawOrigin.x + m_Bounds.x + m_Bounds.width;
		float z = drawOrigin.z + m_Bounds.y;
		float z2 = drawOrigin.z + m_Bounds.y + m_Bounds.height;
		Vector3 vector = new Vector3(x, y, z2);
		Vector3 vector2 = new Vector3(x2, y, z2);
		Vector3 vector3 = new Vector3(x, y, z);
		Vector3 vector4 = new Vector3(x2, y, z);
		if (showOnlyFilled)
		{
			if (m_Objects != null && m_Objects.Count != 0)
			{
				new Vector3(drawOrigin.x + m_Bounds.center.x, y, drawOrigin.z + m_Bounds.center.y);
				new Vector3(m_Bounds.width, 1f, m_Bounds.height);
			}
			if (m_Level == 0)
			{
				Debug.DrawLine(vector, vector2, Color.black);
				Debug.DrawLine(vector2, vector4, Color.black);
				Debug.DrawLine(vector4, vector3, Color.black);
				Debug.DrawLine(vector3, vector, Color.black);
			}
		}
		else
		{
			Debug.DrawLine(vector, vector2, Color.black);
			Debug.DrawLine(vector2, vector4, Color.black);
			Debug.DrawLine(vector4, vector3, Color.black);
			Debug.DrawLine(vector3, vector, Color.black);
		}
		if (m_IsSplit)
		{
			for (int i = 0; i < m_Nodes.Length; i++)
			{
				m_Nodes[i].Draw(drawOrigin, showOnlyFilled);
			}
		}
	}

	private bool NotWalkable(Rect rect, int maxLevel)
	{
		bool flag = false;
		if (m_Level > maxLevel)
		{
			if (m_IsSplit)
			{
				flag = true;
			}
		}
		else if (m_IsSplit)
		{
			BitfieldNonAlloc<QuadPos> quadsContaining = GetQuadsContaining(rect);
			if (!quadsContaining.IsNull)
			{
				if (quadsContaining.Contains(0))
				{
					flag = (m_Nodes[0].m_Objects != null && m_Nodes[0].m_Objects.Count > 0) || flag || m_Nodes[0].NotWalkable(rect, maxLevel);
				}
				if (quadsContaining.Contains(1))
				{
					flag = (m_Nodes[1].m_Objects != null && m_Nodes[1].m_Objects.Count > 0) || flag || m_Nodes[1].NotWalkable(rect, maxLevel);
				}
				if (quadsContaining.Contains(2))
				{
					flag = (m_Nodes[2].m_Objects != null && m_Nodes[2].m_Objects.Count > 0) || flag || m_Nodes[2].NotWalkable(rect, maxLevel);
				}
				if (quadsContaining.Contains(3))
				{
					flag = (m_Nodes[3].m_Objects != null && m_Nodes[3].m_Objects.Count > 0) || flag || m_Nodes[3].NotWalkable(rect, maxLevel);
				}
			}
			else
			{
				flag = true;
			}
		}
		return flag;
	}

	private BitfieldNonAlloc<QuadPos> GetQuadsContaining(Rect rect)
	{
		BitfieldNonAlloc<QuadPos> result = default(BitfieldNonAlloc<QuadPos>);
		double num = m_Bounds.x + m_Bounds.width / 2f;
		double num2 = m_Bounds.y + m_Bounds.height / 2f;
		if (!(rect.y < m_Bounds.y) || !(rect.y + rect.height > m_Bounds.y + m_Bounds.height) || !(rect.x < m_Bounds.x) || !(rect.x + rect.width > m_Bounds.x + m_Bounds.width))
		{
			bool flag = (double)rect.y > num2;
			bool flag2 = (double)rect.y < num2 && (double)(rect.y + rect.height) < num2;
			if ((double)rect.x < num && (double)(rect.x + rect.width) < num)
			{
				if (flag)
				{
					return result.Add(0);
				}
				if (flag2)
				{
					return result.Add(2);
				}
				return k_LeftArea;
			}
			if ((double)rect.x > num)
			{
				if (flag)
				{
					return result.Add(1);
				}
				if (flag2)
				{
					return result.Add(3);
				}
				return k_RightArea;
			}
			if (flag)
			{
				return k_TopArea;
			}
			if (flag2)
			{
				return k_BottomArea;
			}
			return k_All;
		}
		return result;
	}
}
