using UnityEngine;

public class TerrainTexture : ScriptableObject
{
	[SerializeField]
	private Texture2D m_Texture;

	[SerializeField]
	private Texture2D m_NormalMap;

	[SerializeField]
	private int m_Tiling = 32;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_Metallic;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_Smoothness;

	public Texture2D Texture => m_Texture;

	public Texture2D NormalMap => m_NormalMap;

	public int Tiling => m_Tiling;

	public float Metallic => m_Metallic;

	public float Smoothness => m_Smoothness;

	public void EditorInit(Texture2D tex, Texture2D normal, int tiling, float metallic, float smoothness)
	{
		m_Texture = tex;
		m_NormalMap = normal;
		m_Tiling = tiling;
		m_Metallic = metallic;
		m_Smoothness = smoothness;
	}
}
