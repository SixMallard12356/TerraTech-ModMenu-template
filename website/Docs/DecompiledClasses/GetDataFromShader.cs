#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class GetDataFromShader : Singleton.Manager<GetDataFromShader>
{
	private class InitialTerrainValues
	{
		public int dimension;

		public float terrainSteepness;

		public float cellScale;

		public Terrain tt;

		public float[,] h;

		public Vector2 position;

		public InitialTerrainValues(int dim, float trSt, float cS, Terrain t, float[,] heights, Vector2 p)
		{
			dimension = dim;
			terrainSteepness = trSt;
			cellScale = cS;
			tt = t;
			h = heights;
			position = p;
		}
	}

	public Material noiseMaterial;

	private RenderTexture renderTexture;

	private Texture2D getPixelsTexture;

	private Vector4 offset;

	private float initialOffset = 5000f;

	private List<InitialTerrainValues> toProcess = new List<InitialTerrainValues>();

	public void AssignHeightsData(int dimension, float terrainSteepness, float cellScale, Terrain tt, float[,] h, Vector2 position)
	{
		toProcess.Add(new InitialTerrainValues(dimension, terrainSteepness, cellScale, tt, h, position));
	}

	private void GetHeights(int dimension, float terrainSteepness, float cellScale, Terrain tt, float[,] h, Vector2 position)
	{
		if (((bool)renderTexture && renderTexture.width < dimension * 2 - 2) || !renderTexture)
		{
			d.Log("[GetDataFromShader] GetHeights - Allocating RenderTexture Size=" + (dimension * 2 - 2) + "x" + (dimension * 2 - 2));
			if (renderTexture != null)
			{
				Object.DestroyImmediate(renderTexture);
			}
			renderTexture = new RenderTexture(dimension * 2 - 2, dimension * 2 - 2, 32);
		}
		d.Log("[GetDataFromShader] GetHeights - Allocating PixelsTexture Size=" + renderTexture.width + "x" + renderTexture.height);
		getPixelsTexture = new Texture2D(renderTexture.width, renderTexture.height);
		Vector4 value = new Vector4(position.x / (float)(2 * dimension - 2) + initialOffset, position.y / (float)(2 * dimension - 2) + initialOffset, 0f, 0f);
		noiseMaterial.SetVector("_Offset", value);
		Graphics.Blit(null, renderTexture, noiseMaterial);
		RenderTexture.active = renderTexture;
		getPixelsTexture.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
		getPixelsTexture.Apply();
		for (int i = 0; i < h.GetLength(0); i++)
		{
			for (int j = 0; j < h.GetLength(1); j++)
			{
				h[j, i] = getPixelsTexture.GetPixel(i, j).r;
			}
		}
		tt.terrainData.heightmapResolution = dimension;
		tt.terrainData.SetHeights(0, 0, h);
		tt.terrainData.size = new Vector3((float)(dimension - 1) * cellScale, terrainSteepness, (float)(dimension - 1) * cellScale);
		Object.DestroyImmediate(getPixelsTexture);
	}

	private void LateUpdate()
	{
		if (toProcess.Count > 0)
		{
			GetHeights(toProcess[0].dimension, toProcess[0].terrainSteepness, toProcess[0].cellScale, toProcess[0].tt, toProcess[0].h, toProcess[0].position);
			toProcess.RemoveAt(0);
		}
	}
}
