using UnityEngine;

[CreateAssetMenu(menuName = "TerraTech/Skin Definition")]
public class ModdedSkinDefinition : ModdedAsset
{
	public string m_Corporation = "GSO";

	public string m_SkinDisplayName = "";

	public Texture2D m_Albedo;

	public Texture2D m_Emissive;

	public Texture2D m_Combined;

	public Texture2D m_Variable;

	public Texture2D m_PreviewImage;

	public Texture2D m_SkinButtonImage;

	public bool m_IsCorpDefault;

	public TextureSlot m_CorpTextureSlot;

	public void SetFallbacks()
	{
		if (m_Variable == null && m_Albedo != null)
		{
			m_Variable = GenerateBlankVariableMap();
		}
	}

	private Texture2D GenerateBlankVariableMap()
	{
		Texture2D texture2D = new Texture2D(m_Albedo.width, m_Albedo.height);
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				texture2D.SetPixel(i, j, Color.black);
			}
		}
		return texture2D;
	}
}
