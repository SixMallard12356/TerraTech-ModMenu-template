using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
	private enum QuadPos
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	private int MAX_OBJECTS;

	private int MAX_LEVELS = 20;

	private int m_Level;

	private List<Rect> m_Objects;

	private Rect m_Bounds;

	private QuadTree[] m_Nodes;

	public QuadTree(int level, Rect bounds)
	{
		m_Level = level;
		m_Objects = new List<Rect>();
		m_Bounds = bounds;
		m_Nodes = new QuadTree[4];
	}

	public void Insert(Rect rect)
	{
		if (m_Nodes[0] != null)
		{
			int index = GetIndex(rect);
			if (index != -1)
			{
				m_Nodes[index].Insert(rect);
				return;
			}
		}
		m_Objects.Add(rect);
		if (m_Objects.Count <= MAX_OBJECTS || m_Level >= MAX_LEVELS || m_Nodes[0] != null)
		{
			return;
		}
		Split();
		for (int num = m_Objects.Count - 1; num >= 0; num--)
		{
			int index2 = GetIndex(m_Objects[num]);
			if (index2 != -1)
			{
				m_Nodes[index2].Insert(m_Objects[num]);
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
		m_Nodes[0] = new QuadTree(m_Level + 1, new Rect(num3, num4 + num2, num, num2));
		m_Nodes[1] = new QuadTree(m_Level + 1, new Rect(num3 + num, num4 + num2, num, num2));
		m_Nodes[2] = new QuadTree(m_Level + 1, new Rect(num3, num4, num, num2));
		m_Nodes[3] = new QuadTree(m_Level + 1, new Rect(num3 + num, num4, num, num2));
	}

	public void Draw(Vector3 drawOrigin, bool showOnlyFilled)
	{
		float y = 150f;
		float x = m_Bounds.x;
		float x2 = m_Bounds.x + m_Bounds.width;
		float y2 = m_Bounds.y;
		float z = m_Bounds.y + m_Bounds.height;
		if (showOnlyFilled)
		{
			if (m_Objects != null && m_Objects.Count != 0)
			{
				_ = drawOrigin + new Vector3(m_Bounds.center.x, y, m_Bounds.center.y);
				new Vector3(m_Bounds.width, 1f, m_Bounds.height);
			}
		}
		else
		{
			Vector3 vector = drawOrigin + new Vector3(x, y, z);
			Vector3 vector2 = drawOrigin + new Vector3(x2, y, z);
			Vector3 vector3 = drawOrigin + new Vector3(x, y, y2);
			Vector3 vector4 = drawOrigin + new Vector3(x2, y, y2);
			Debug.DrawLine(vector, vector2, Color.black);
			Debug.DrawLine(vector2, vector4, Color.black);
			Debug.DrawLine(vector4, vector3, Color.black);
			Debug.DrawLine(vector3, vector, Color.black);
		}
		if (m_Nodes == null)
		{
			return;
		}
		for (int i = 0; i < m_Nodes.Length; i++)
		{
			if (m_Nodes[i] != null)
			{
				m_Nodes[i].Draw(drawOrigin, showOnlyFilled);
			}
		}
	}

	private int GetIndex(Rect rect)
	{
		int result = -1;
		double num = m_Bounds.x + m_Bounds.width / 2f;
		double num2 = m_Bounds.y + m_Bounds.height / 2f;
		bool flag = (double)rect.y > num2;
		bool flag2 = (double)rect.y < num2 && (double)(rect.y + rect.height) < num2;
		if ((double)rect.x < num && (double)(rect.x + rect.width) < num)
		{
			if (flag)
			{
				result = 0;
			}
			else if (flag2)
			{
				result = 2;
			}
		}
		else if ((double)rect.x > num)
		{
			if (flag)
			{
				result = 1;
			}
			else if (flag2)
			{
				result = 3;
			}
		}
		return result;
	}
}
