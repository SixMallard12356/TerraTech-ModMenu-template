using UnityEngine;

[CreateAssetMenu(fileName = "New Corp Skin Info", menuName = "New Corporation Skin Info", order = 51)]
public class CorporationSkinInfo : ScriptableObject
{
	[SerializeField]
	public SkinTextures m_SkinTextureInfo;

	[SerializeField]
	public CorporationSkinUIInfo m_SkinUIInfo;

	[SerializeField]
	public SkinMeshes m_SkinMeshes;

	[SerializeField]
	public int m_SkinUniqueID;

	[SerializeField]
	public FactionSubTypes m_Corporation;
}
