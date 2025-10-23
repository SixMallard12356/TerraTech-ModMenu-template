using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Table/KitBashPanelPool", fileName = "New Kit Bash Panel Pool")]
public class KitBashPanelPoolPreset : ScriptableObject
{
	[SerializeField]
	protected KitBashPanelPool m_Pool;

	public KitBashPanelPool Pool => m_Pool;
}
