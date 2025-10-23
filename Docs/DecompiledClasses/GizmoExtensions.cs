using UnityEngine;

public static class GizmoExtensions
{
	public static void DrawLineSquare(Vector3 worldPos, Vector2 dimentions, Quaternion rotation, Color color)
	{
		_ = Vector3.up;
		Vector3[] array = new Vector3[4];
		Vector2 vector = dimentions * 0.5f;
		array[0] = worldPos + rotation * new Vector3(vector.x, vector.y, 0f);
		array[1] = worldPos + rotation * new Vector3(0f - vector.x, vector.y, 0f);
		array[2] = worldPos + rotation * new Vector3(0f - vector.x, 0f - vector.y, 0f);
		array[3] = worldPos + rotation * new Vector3(vector.x, 0f - vector.y, 0f);
		DrawLinePolygon(color, array);
	}

	public static void DrawLinePolygon(Color color, params Vector3[] vertices)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawLine(vertices[i], (i == vertices.Length - 1) ? vertices[0] : vertices[i + 1]);
		}
		Gizmos.color = color2;
	}
}
