using UnityEngine;

public class PathingGrid
{
	private Rect m_Bounds;

	private int m_SquareSize;

	private int m_SquareCount;

	private bool[,] m_Grid;

	public PathingGrid(int squareSize, Rect bounds)
	{
		m_Bounds = bounds;
		m_SquareSize = squareSize;
		int num = (int)m_Bounds.width;
		int num2 = (int)m_Bounds.height;
		m_Grid = new bool[num, num2];
		m_SquareCount = (int)(m_Bounds.width / (float)m_SquareSize);
	}

	public bool Walkable(int x, int y)
	{
		if (x >= 0 && x < m_SquareCount && x >= 0 && y < m_SquareCount)
		{
			return !m_Grid[x, y];
		}
		return false;
	}

	public void Add(Rect rect)
	{
		float num = rect.x - m_Bounds.x;
		float num2 = rect.y - m_Bounds.y;
		int x = Mathf.FloorToInt(num / (float)m_SquareSize);
		int y = Mathf.FloorToInt(num2 / (float)m_SquareSize);
		IntVector2 intVector = new IntVector2(x, y);
		float num3 = rect.x + rect.width - m_Bounds.x;
		num2 = rect.y + rect.height - m_Bounds.y;
		int x2 = Mathf.CeilToInt(num3 / (float)m_SquareSize);
		int y2 = Mathf.CeilToInt(num2 / (float)m_SquareSize);
		IntVector2 intVector2 = new IntVector2(x2, y2);
		for (int i = intVector.y; i < intVector2.y; i++)
		{
			for (int j = intVector.x; j < intVector2.x; j++)
			{
				if (j >= 0 && j < m_SquareCount && i >= 0 && i < m_SquareCount)
				{
					m_Grid[j, i] = true;
				}
				else
				{
					_ = 0;
				}
			}
		}
	}

	public void Draw(Vector3 drawOrigin, bool showOnlyFilled)
	{
		float y = 150f;
		float num = drawOrigin.x + m_Bounds.x;
		float x = drawOrigin.x + m_Bounds.x + m_Bounds.width;
		float num2 = drawOrigin.z + m_Bounds.y;
		float z = drawOrigin.z + m_Bounds.y + m_Bounds.height;
		Vector3 vector = new Vector3(num, y, z);
		Vector3 vector2 = new Vector3(x, y, z);
		Vector3 vector3 = new Vector3(num, y, num2);
		Vector3 vector4 = new Vector3(x, y, num2);
		Debug.DrawLine(vector, vector2, Color.black);
		Debug.DrawLine(vector2, vector4, Color.black);
		Debug.DrawLine(vector4, vector3, Color.black);
		Debug.DrawLine(vector3, vector, Color.black);
		for (int i = 0; i < m_SquareCount; i++)
		{
			for (int j = 0; j < m_SquareCount; j++)
			{
				float x2 = num + (float)(m_SquareSize * j);
				float x3 = num + (float)(m_SquareSize * j) + (float)m_SquareSize;
				float z2 = num2 + (float)(m_SquareSize * i);
				float z3 = num2 + (float)(m_SquareSize * i) + (float)m_SquareSize;
				Vector3 vector5 = new Vector3(x2, y, z3);
				Vector3 end = new Vector3(x3, y, z3);
				Vector3 start = new Vector3(x2, y, z2);
				Vector3 vector6 = new Vector3(x3, y, z2);
				if (m_Grid[j, i] || !showOnlyFilled)
				{
					if (j > 0)
					{
						Color color = (m_Grid[j, i] ? Color.red : Color.black);
						Debug.DrawLine(start, vector5, color);
					}
					if (j < m_SquareCount - 1)
					{
						Color color2 = (m_Grid[j, i] ? Color.red : Color.black);
						Debug.DrawLine(vector6, end, color2);
					}
					if (i > 0)
					{
						Color color3 = (m_Grid[j, i] ? Color.red : Color.black);
						Debug.DrawLine(start, vector6, color3);
					}
					if (i < m_SquareCount - 1)
					{
						Color color4 = (m_Grid[j, i] ? Color.red : Color.black);
						Debug.DrawLine(vector5, end, color4);
					}
				}
			}
		}
	}
}
