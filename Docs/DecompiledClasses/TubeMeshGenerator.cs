using System;
using System.Collections.Generic;
using UnityEngine;

public class TubeMeshGenerator
{
	public class MeshDefinition
	{
		private List<Vector3> m_Vertices;

		private int[] m_Indices;

		public MeshDefinition(List<Vector3> vertices, int[] indices)
		{
			m_Vertices = vertices;
			m_Indices = indices;
		}

		public Mesh GenerateMesh()
		{
			Mesh mesh = new Mesh();
			mesh.SetVertices(m_Vertices);
			mesh.SetIndices(m_Indices, MeshTopology.Triangles, 0);
			return mesh;
		}
	}

	public static Mesh GenerateMesh(Vector3[] points, float diameter, int numFacesAround)
	{
		return GenerateMeshDef(points, diameter, numFacesAround).GenerateMesh();
	}

	public static MeshDefinition GenerateMeshDef(Vector3[] points, float diameter, int numFacesAround)
	{
		float num = diameter / 2f;
		float num2 = (float)Math.PI * 2f / (float)numFacesAround;
		float outOffset = num * Mathf.Sin(num2 / 2f);
		float upOffset = num * Mathf.Cos(num2 / 2f);
		Vector3[] array = new Vector3[points.Length - 1];
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = points[i];
			Vector3 vector2 = points[i + 1];
			array[i] = (vector2 - vector).normalized;
		}
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		for (int j = 0; j < numFacesAround; j++)
		{
			float rotation = (float)Math.PI * 2f * (float)j / (float)numFacesAround;
			GenerateTubeFace(points, array, outOffset, upOffset, rotation, list, list2);
		}
		return new MeshDefinition(list, list2.ToArray());
	}

	private static void GenerateTubeFace(Vector3[] points, Vector3[] tangents, float outOffset, float upOffset, float rotation, List<Vector3> outVerts, List<int> outInds)
	{
		int num = Mathf.Max(points.Length - 1, 0);
		int count = outVerts.Count;
		for (int i = 0; i < points.Length; i++)
		{
			Vector3 vector = points[i];
			Vector3 forward = ((i != 0) ? ((i < tangents.Length) ? (tangents[i - 1] + tangents[i]).normalized : tangents[tangents.Length - 1]) : tangents[0]);
			Quaternion quaternion = Quaternion.LookRotation(forward);
			Quaternion quaternion2 = Quaternion.Euler(0f, 0f, rotation * 57.29578f);
			Quaternion quaternion3 = quaternion * quaternion2;
			outVerts.Add(vector + quaternion3 * new Vector3(0f - outOffset, upOffset, 0f));
			outVerts.Add(vector + quaternion3 * new Vector3(outOffset, upOffset, 0f));
		}
		int num2 = count;
		for (int j = 0; j < num; j++)
		{
			outInds.Add(num2);
			outInds.Add(num2 + 2);
			outInds.Add(num2 + 1);
			outInds.Add(num2 + 3);
			outInds.Add(num2 + 1);
			outInds.Add(num2 + 2);
			num2 += 2;
		}
	}
}
