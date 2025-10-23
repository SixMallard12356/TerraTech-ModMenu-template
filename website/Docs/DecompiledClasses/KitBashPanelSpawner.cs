using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class KitBashPanelSpawner : MonoBehaviour
{
	public struct PanelSpawnData
	{
		public int PanelTypeIndex;

		public float AxisRotation;

		public bool IsPerpendicular;

		[JsonIgnore]
		public static readonly PanelSpawnData invalid = new PanelSpawnData(-1, 0f, isPerpendicular: false);

		[JsonIgnore]
		public bool IsInvalid => PanelTypeIndex == -1;

		[JsonConstructor]
		public PanelSpawnData(int panelTypeIndex, float axisRotation, bool isPerpendicular)
		{
			PanelTypeIndex = panelTypeIndex;
			AxisRotation = axisRotation;
			IsPerpendicular = isPerpendicular;
		}

		public PanelSpawnData(KitBashPanel panel)
		{
			if (panel != null)
			{
				PanelTypeIndex = (int)panel.PanelType;
				panel.GetSpawnData(out AxisRotation, out IsPerpendicular);
			}
			else
			{
				PanelTypeIndex = 0;
				AxisRotation = 0f;
				IsPerpendicular = false;
			}
		}

		public override string ToString()
		{
			string[] value = new string[3]
			{
				PanelTypeIndex.ToString(),
				AxisRotation.ToString(),
				IsPerpendicular.ToString()
			};
			return string.Join(":", value);
		}

		public static bool TryParse(string value, out PanelSpawnData result)
		{
			string[] array = value.Split(':');
			result = default(PanelSpawnData);
			int num;
			if (int.TryParse(array[0], out result.PanelTypeIndex) && float.TryParse(array[1], out result.AxisRotation))
			{
				num = (bool.TryParse(array[2], out result.IsPerpendicular) ? 1 : 0);
				if (num != 0)
				{
					goto IL_0058;
				}
			}
			else
			{
				num = 0;
			}
			result = invalid;
			goto IL_0058;
			IL_0058:
			return (byte)num != 0;
		}

		public void NetSerialize(NetworkWriter writer)
		{
			writer.WritePackedInt32(PanelTypeIndex);
			writer.Write(AxisRotation);
			writer.Write(IsPerpendicular);
		}

		public void NetDeserialize(NetworkReader reader)
		{
			PanelTypeIndex = reader.ReadPackedInt32();
			AxisRotation = reader.ReadSingle();
			IsPerpendicular = reader.ReadBoolean();
		}
	}

	[SerializeField]
	protected Vector2Int m_Dimentions = Vector2Int.one;

	[Tooltip("The bash pool preset to use specifically for this panel, having this set will mean the bash pool set by the kitbasher module and the override bash pool will be ignored")]
	[Header("Overrides")]
	[SerializeField]
	protected KitBashPanelPoolPreset m_OverrideBashPoolPreset;

	[SerializeField]
	[Tooltip("The bash pool to use specifically for this panel, having this set will mean the bash pool set by the kitbasher module will be ignored")]
	protected KitBashPanelPool m_OverrideBashPool;

	protected KitBashPanel m_SpawnedPanel;

	private static Color _s_SelectedColor = Color.green;

	private static Color _s_UnselectedColor = Color.green * 0.5f;

	public void Bash(KitBashPanelPool bashPool, DRNG randomizer)
	{
		ClearBash();
		if (m_OverrideBashPoolPreset != null)
		{
			bashPool = m_OverrideBashPoolPreset.Pool;
		}
		else if (m_OverrideBashPool != null && !m_OverrideBashPool.NotSet)
		{
			bashPool = m_OverrideBashPool;
		}
		m_SpawnedPanel = bashPool.SpawnPanelFromPool(m_Dimentions, GetPanelWorldPosition(), base.transform.rotation, randomizer, base.transform);
		m_SpawnedPanel.RandomiseAxisRotation(randomizer);
	}

	public void SetPanel(PanelSpawnData spawnData)
	{
		ClearBash();
		m_SpawnedPanel = Singleton.Manager<ManSpawn>.inst.SpawnKitBashPanel((KitBashPanelTypes)spawnData.PanelTypeIndex, GetPanelWorldPosition(), base.transform.rotation, base.transform);
		m_SpawnedPanel.SetFromSpawnData(spawnData);
	}

	public bool TryGetPanelSpawnData(out PanelSpawnData spawnData)
	{
		spawnData = new PanelSpawnData(m_SpawnedPanel);
		return m_SpawnedPanel != null;
	}

	private Vector3 GetPanelWorldPosition()
	{
		return base.transform.position + base.transform.rotation * GetPanelLocalSpawnOffset();
	}

	private Vector3 GetPanelLocalSpawnOffset()
	{
		if (m_Dimentions.x == 0 || m_Dimentions.y == 0)
		{
			return Vector3.zero;
		}
		Vector2 vector = GetPanelSize(m_Dimentions) * 0.5f - Vector2.one * 0.88f * 0.5f;
		return new Vector3(vector.x, vector.y, 0f);
	}

	public static Vector2 GetPanelSize(Vector2Int dimentions)
	{
		if (dimentions.x == 0 || dimentions.y == 0)
		{
			return Vector2.zero;
		}
		return dimentions - Vector2Int.one + Vector2.one * 0.88f;
	}

	private void ClearBash()
	{
		if (m_SpawnedPanel != null)
		{
			m_SpawnedPanel.Recycle();
			m_SpawnedPanel = null;
		}
	}

	private void PrePool()
	{
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
		ClearBash();
	}

	private void OnDrawGizmos()
	{
		DrawGizmos(_s_UnselectedColor);
	}

	private void OnDrawGizmosSelected()
	{
		DrawGizmos(_s_SelectedColor);
	}

	private void DrawGizmos(Color color)
	{
		if (!Application.isPlaying)
		{
			Gizmos.color = color;
			Vector3 panelWorldPosition = GetPanelWorldPosition();
			GizmoExtensions.DrawLineSquare(panelWorldPosition, GetPanelSize(m_Dimentions), base.transform.rotation, color);
			Gizmos.DrawLine(panelWorldPosition, panelWorldPosition + base.transform.forward * 0.15f);
		}
	}
}
