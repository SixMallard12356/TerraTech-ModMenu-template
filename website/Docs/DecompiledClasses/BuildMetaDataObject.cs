using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Build/Changlist Data (Build Meta Data)")]
public class BuildMetaDataObject : ScriptableObject
{
	public const string k_Path = "Tables/BuildMetaData";

	public BuildMetaData m_BuildMetaData;
}
