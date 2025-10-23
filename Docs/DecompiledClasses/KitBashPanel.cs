using UnityEngine;

public class KitBashPanel : MonoBehaviour
{
	public enum RotationTypes
	{
		None,
		HalfRotation,
		QuarterRotation
	}

	[SerializeField]
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	protected KitBashPanelTypes m_Type;

	[SerializeField]
	protected Vector2Int m_Dimentions = Vector2Int.one;

	[SerializeField]
	[Tooltip("How much of a full rotation can be made to this panel for randomisation? HalfRotation: Can be rotated 180 degrees and still fit its dimentions. QuarterRotation: Can be rotated 90 degrees and still fit its dimentions.")]
	protected RotationTypes m_RandomRotationGranulatiry;

	[Range(1f, 100f)]
	[SerializeField]
	protected int m_SpawnWeight = 100;

	public const float k_StandardCellSize = 0.88f;

	protected Quaternion m_DefaultLocalRotation = Quaternion.identity;

	protected float m_PanelAxisRotation;

	protected bool m_IsPerpendicular;

	public KitBashPanelTypes PanelType => m_Type;

	public Vector2Int Dimentions => m_Dimentions;

	public int SpawnWeight => m_SpawnWeight;

	public int SymmetryAxis => (int)m_RandomRotationGranulatiry;

	public void SetPerpendicularVariant()
	{
		m_IsPerpendicular = true;
		ApplyRotation();
	}

	public void RandomiseAxisRotation(DRNG randomizer)
	{
		int num = randomizer.Range(0, 100);
		float num2 = 360f / Mathf.Pow(2f, SymmetryAxis);
		if (SymmetryAxis != 0)
		{
			m_PanelAxisRotation = (float)num * num2 % 360f;
		}
		ApplyRotation();
	}

	public void GetSpawnData(out float axisRotation, out bool isPerpendicular)
	{
		axisRotation = m_PanelAxisRotation;
		isPerpendicular = m_IsPerpendicular;
	}

	public void SetFromSpawnData(KitBashPanelSpawner.PanelSpawnData spawnData)
	{
		m_PanelAxisRotation = spawnData.AxisRotation;
		m_IsPerpendicular = spawnData.IsPerpendicular;
		ApplyRotation();
	}

	private void ApplyRotation()
	{
		base.transform.localRotation = m_DefaultLocalRotation;
		base.transform.Rotate(Vector3.forward, m_PanelAxisRotation + (float)(m_IsPerpendicular ? 90 : 0));
	}

	private void PrePool()
	{
		m_DefaultLocalRotation = base.transform.localRotation;
	}

	private void OnPool()
	{
	}

	private void OnDepool()
	{
	}

	private void OnSpawn()
	{
	}

	private void OnRecycle()
	{
		m_IsPerpendicular = false;
		m_PanelAxisRotation = 0f;
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying && !(base.transform.parent != null))
		{
			Vector2 panelSize = KitBashPanelSpawner.GetPanelSize(m_Dimentions);
			Gizmos.color = Color.green;
			GizmoExtensions.DrawLineSquare(Vector3.zero, panelSize, Quaternion.identity, Color.green);
			Gizmos.DrawLine(Vector3.zero, base.transform.forward * 0.15f);
		}
	}
}
