using System.Collections.Generic;
using UnityEngine;

public class PrismMeshGenerator
{
	public class MeshDefinition
	{
		private List<Vector3> m_Vertices;

		private List<Vector2> m_UVs;

		private int[] m_Indices;

		public MeshDefinition(List<Vector3> vertices, List<Vector2> uvs, int[] indices)
		{
			m_Vertices = vertices;
			m_UVs = uvs;
			m_Indices = indices;
		}

		public Mesh GenerateMesh()
		{
			Mesh mesh = new Mesh();
			mesh.SetVertices(m_Vertices);
			mesh.SetUVs(0, m_UVs);
			mesh.SetIndices(m_Indices, MeshTopology.Triangles, 0);
			return mesh;
		}
	}

	public static Mesh GenerateMesh(Vector3[] points, Vector3 extrusion, float uvWorldScale)
	{
		return GenerateMeshDef(points, extrusion, uvWorldScale).GenerateMesh();
	}

	public static MeshDefinition GenerateMeshDef(Vector3[] points, Vector3 extrusion, float uvWorldScale)
	{
		float[] array = new float[points.Length + 1];
		float num = 0f;
		for (int i = 0; i < points.Length; i++)
		{
			Vector3 vector = points[(i + 1) % points.Length];
			array[i] = num;
			num += (vector - points[i]).magnitude;
		}
		array[points.Length] = num;
		float num2 = Mathf.Ceil(num * uvWorldScale) / num;
		List<Vector3> list = new List<Vector3>(points.Length * 2 + 2);
		List<Vector2> list2 = new List<Vector2>(points.Length * 2 + 2);
		List<int> list3 = new List<int>(points.Length * 6);
		for (int j = 0; j < points.Length; j++)
		{
			list.Add(points[j]);
			list.Add(points[j] + extrusion);
		}
		list.Add(list[0]);
		list.Add(list[1]);
		float y = extrusion.magnitude * uvWorldScale;
		for (int k = 0; k < array.Length; k++)
		{
			float x = array[k] * num2;
			list2.Add(new Vector2(x, 0f));
			list2.Add(new Vector2(x, y));
		}
		for (int l = 0; l < points.Length; l++)
		{
			list3.Add(l * 2);
			list3.Add(l * 2 + 2);
			list3.Add(l * 2 + 1);
			list3.Add(l * 2 + 3);
			list3.Add(l * 2 + 1);
			list3.Add(l * 2 + 2);
		}
		return new MeshDefinition(list, list2, list3.ToArray());
	}
}
