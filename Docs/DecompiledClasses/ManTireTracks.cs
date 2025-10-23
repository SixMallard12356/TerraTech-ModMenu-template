#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ManTireTracks : Singleton.Manager<ManTireTracks>, Mode.IManagerModeEvents, IWorldTreadmill
{
	private class TireTracks
	{
		public int m_SectionIndex;

		public TrackSection[] m_TrackSections;

		public Mesh m_Mesh;

		public MeshRenderer m_MeshRenderer;

		public MeshFilter m_MeshFilter;

		public bool m_MeshDirty;

		public int m_MeshVertexIndex;

		public Vector3[] m_Vertices;

		public Vector3[] m_Normals;

		public Vector4[] m_Tangents;

		public Color32[] m_Colours;

		public Vector2[] m_UVs;

		public int[] m_Triangles;

		public int m_MaxMarks;

		private bool m_HaveSetBounds;

		public TireTracks(int maxMarks, GameObject gameObject, Material material)
		{
			m_MaxMarks = maxMarks;
			m_Mesh = new Mesh();
			m_Mesh.MarkDynamic();
			m_MeshFilter = gameObject.AddComponent<MeshFilter>();
			m_MeshFilter.sharedMesh = m_Mesh;
			m_MeshRenderer = gameObject.AddComponent<MeshRenderer>();
			m_MeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			m_MeshRenderer.receiveShadows = false;
			m_MeshRenderer.material = material;
			m_MeshRenderer.lightProbeUsage = LightProbeUsage.Off;
			Init();
		}

		private void Init()
		{
			m_TrackSections = new TrackSection[m_MaxMarks];
			for (int i = 0; i < m_MaxMarks; i++)
			{
				m_TrackSections[i] = new TrackSection();
			}
			m_Vertices = new Vector3[m_MaxMarks * 4];
			m_Normals = new Vector3[m_MaxMarks * 4];
			m_Tangents = new Vector4[m_MaxMarks * 4];
			m_Colours = new Color32[m_MaxMarks * 4];
			m_UVs = new Vector2[m_MaxMarks * 4];
			m_Triangles = new int[m_MaxMarks * 6];
			for (int j = 0; j < m_MaxMarks; j++)
			{
				int num = j * 4;
				int num2 = j * 6;
				m_Triangles[num2] = num;
				m_Triangles[num2 + 2] = num + 1;
				m_Triangles[num2 + 1] = num + 2;
				m_Triangles[num2 + 3] = num + 2;
				m_Triangles[num2 + 5] = num + 1;
				m_Triangles[num2 + 4] = num + 3;
			}
			m_SectionIndex = 0;
			m_MeshVertexIndex = 0;
			m_MeshDirty = false;
			m_HaveSetBounds = false;
		}

		private void ClearMeshData()
		{
			int num = m_Vertices.Length;
			for (int i = 0; i < num; i++)
			{
				m_Vertices[i] = Vector3.zero;
			}
			m_SectionIndex = 0;
			m_MeshVertexIndex = 0;
			m_MeshDirty = false;
			m_HaveSetBounds = false;
		}

		public void UpdateMesh()
		{
			if (m_MeshDirty)
			{
				m_Mesh.vertices = m_Vertices;
				m_Mesh.normals = m_Normals;
				m_Mesh.tangents = m_Tangents;
				m_Mesh.colors32 = m_Colours;
				m_Mesh.uv = m_UVs;
				if (!m_HaveSetBounds)
				{
					m_Mesh.triangles = m_Triangles;
					m_Mesh.bounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(10000f, 10000f, 10000f));
					m_HaveSetBounds = true;
					m_MeshFilter.sharedMesh = m_Mesh;
				}
				m_MeshDirty = false;
			}
		}

		public void Clear()
		{
			ClearMeshData();
			m_Mesh.Clear(keepVertexLayout: false);
		}
	}

	private class TrackSection
	{
		public Vector3 Pos;

		public Vector3 Normal;

		public Vector4 Tangent;

		public Vector3 LeftOffset;

		public Vector3 RightOffset;

		public float ScrollingUV;

		public byte Intensity;

		public int LastIndex;

		public Vector3 PosLeft => Pos + LeftOffset;

		public Vector3 PosRight => Pos + RightOffset;
	}

	public enum WheelType
	{
		SmoothWheels,
		StandardTreads,
		DeepTreads,
		TankTreads
	}

	public enum TrackGroup
	{
		Player,
		Other
	}

	[SerializeField]
	private bool m_Active;

	[SerializeField]
	private int m_NumBiomesInTexture = 9;

	[SerializeField]
	[EnumArray(typeof(TrackGroup))]
	private int[] m_MaxMarks;

	[EnumArray(typeof(TrackGroup))]
	[SerializeField]
	private float[] m_DistFromCamera;

	[SerializeField]
	private Material m_Material;

	[SerializeField]
	private float m_GroundOffset = 0.01f;

	private TireTracks[] m_TireTracks;

	private float[] m_WheelTypeXOffset;

	private float[] m_BiomeXOffsetL;

	private float[] m_BiomeXOffsetR;

	private const float k_MaxTrackDist = 2f;

	private const float k_MaxTrackDistSqr = 4f;

	private const int m_NumWheelTypes = 4;

	private int m_FrameIndex;

	private bool m_WorldMoved;

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
		if (m_Active)
		{
			Clear();
		}
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	public int AddTireTrack(TrackGroup trackType, Vector3 pos, Vector3 normal, float width, float intensity, int lastIndex, BiomeTypes biomeType, WheelType wheelType)
	{
		if (!m_Active)
		{
			return -1;
		}
		TireTracks tireTracks = m_TireTracks[(int)trackType];
		if (intensity > 1f)
		{
			intensity = 1f;
		}
		else if (intensity < 0f)
		{
			return -1;
		}
		if (lastIndex > 0 && (pos - tireTracks.m_TrackSections[lastIndex].Pos).sqrMagnitude > 4f)
		{
			return -1;
		}
		TrackSection trackSection = tireTracks.m_TrackSections[tireTracks.m_SectionIndex];
		trackSection.Pos = pos + normal * m_GroundOffset;
		trackSection.Normal = normal;
		trackSection.Intensity = (byte)(intensity * 255f);
		trackSection.LastIndex = lastIndex;
		if (lastIndex != -1)
		{
			TrackSection trackSection2 = tireTracks.m_TrackSections[lastIndex];
			Vector3 rhs = trackSection.Pos - trackSection2.Pos;
			if (rhs.sqrMagnitude <= 1.0000001E-06f)
			{
				return lastIndex;
			}
			trackSection.ScrollingUV = trackSection2.ScrollingUV + rhs.magnitude / width;
			Vector3 normalized = Vector3.Cross(normal, rhs).normalized;
			Vector3 vector = normalized * width * 0.5f;
			Vector3 vector2 = normal * m_GroundOffset;
			trackSection.LeftOffset = vector2 - vector;
			trackSection.RightOffset = vector2 + vector;
			trackSection.Tangent = new Vector4(normalized.x, normalized.y, normalized.z, 1f);
			if (trackSection2.LastIndex == -1)
			{
				trackSection2.LeftOffset = trackSection.LeftOffset;
				trackSection2.RightOffset = trackSection.RightOffset;
				trackSection2.Tangent = trackSection.Tangent;
			}
		}
		else
		{
			trackSection.ScrollingUV = 0f;
		}
		UpdateTrackMesh(biomeType, wheelType, tireTracks);
		int sectionIndex = tireTracks.m_SectionIndex;
		tireTracks.m_SectionIndex = ++tireTracks.m_SectionIndex % tireTracks.m_MaxMarks;
		return sectionIndex;
	}

	public float GetDistFromCamera(TrackGroup trackType)
	{
		float result = 0f;
		if ((int)trackType < m_DistFromCamera.Length)
		{
			result = m_DistFromCamera[(int)trackType];
		}
		else
		{
			d.LogWarning("ManTireTracks.DistFromCamera - No Distance set for TrackType " + trackType);
		}
		return result;
	}

	public bool ColliderSupportsTireTracks(Collider collider)
	{
		if (collider.gameObject.IsTerrain())
		{
			return true;
		}
		return false;
	}

	private void SetupUVLookups()
	{
		m_WheelTypeXOffset = new float[4];
		float num = 0.25f;
		float num2 = 0f;
		for (int i = 0; i < 4; i++)
		{
			m_WheelTypeXOffset[i] = num2;
			num2 += num;
		}
		d.Assert(Enum.GetNames(typeof(BiomeTypes)).Length <= m_NumBiomesInTexture, "ManTireTracks - We now have more biomes in Biome Types than in tire track texture. Fixup Texture and increase m_NumBiomesInTexture");
		m_BiomeXOffsetL = new float[m_NumBiomesInTexture];
		m_BiomeXOffsetR = new float[m_NumBiomesInTexture];
		float num3 = num / (float)m_NumBiomesInTexture;
		float num4 = 0f;
		for (int j = 0; j < m_NumBiomesInTexture; j++)
		{
			m_BiomeXOffsetL[j] = num4;
			num4 += num3;
			m_BiomeXOffsetR[j] = num4;
		}
	}

	private Vector3 OffsetToTerrainHeight(Vector3 pos)
	{
		if (Singleton.Manager<ManWorld>.inst.GetTerrainHeight(pos, out var outHeight))
		{
			pos.y = outHeight;
		}
		return pos;
	}

	private void UpdateTrackMesh(BiomeTypes biomeType, WheelType wheelType, TireTracks tracks)
	{
		_ = tracks.m_SectionIndex;
		TrackSection trackSection = tracks.m_TrackSections[tracks.m_SectionIndex];
		if (trackSection.LastIndex != -1)
		{
			TrackSection trackSection2 = tracks.m_TrackSections[trackSection.LastIndex];
			int num = tracks.m_MeshVertexIndex * 4;
			tracks.m_MeshVertexIndex = ++tracks.m_MeshVertexIndex % tracks.m_MaxMarks;
			tracks.m_Vertices[num] = trackSection2.PosLeft;
			tracks.m_Vertices[num + 1] = trackSection2.PosRight;
			tracks.m_Vertices[num + 2] = trackSection.PosLeft;
			tracks.m_Vertices[num + 3] = trackSection.PosRight;
			tracks.m_Normals[num] = trackSection2.Normal;
			tracks.m_Normals[num + 1] = trackSection2.Normal;
			tracks.m_Normals[num + 2] = trackSection.Normal;
			tracks.m_Normals[num + 3] = trackSection.Normal;
			tracks.m_Tangents[num] = trackSection2.Tangent;
			tracks.m_Tangents[num + 1] = trackSection2.Tangent;
			tracks.m_Tangents[num + 2] = trackSection.Tangent;
			tracks.m_Tangents[num + 3] = trackSection.Tangent;
			tracks.m_Colours[num] = new Color32(0, 0, 0, trackSection2.Intensity);
			tracks.m_Colours[num + 1] = new Color32(0, 0, 0, trackSection2.Intensity);
			tracks.m_Colours[num + 2] = new Color32(0, 0, 0, trackSection.Intensity);
			tracks.m_Colours[num + 3] = new Color32(0, 0, 0, trackSection.Intensity);
			float x = m_WheelTypeXOffset[(int)wheelType] + m_BiomeXOffsetL[(int)biomeType];
			float x2 = m_WheelTypeXOffset[(int)wheelType] + m_BiomeXOffsetR[(int)biomeType];
			tracks.m_UVs[num] = new Vector2(x, trackSection2.ScrollingUV);
			tracks.m_UVs[num + 1] = new Vector2(x2, trackSection2.ScrollingUV);
			tracks.m_UVs[num + 2] = new Vector2(x, trackSection.ScrollingUV);
			tracks.m_UVs[num + 3] = new Vector2(x2, trackSection.ScrollingUV);
			tracks.m_MeshDirty = true;
		}
	}

	private void Clear()
	{
		for (int i = 0; i < m_TireTracks.Length; i++)
		{
			m_TireTracks[i].Clear();
		}
	}

	public void OnMoveWorldOrigin(IntVector3 amountMoved)
	{
		for (int i = 0; i < m_TireTracks.Length; i++)
		{
			TireTracks tireTracks = m_TireTracks[i];
			for (int j = 0; j < tireTracks.m_MaxMarks; j++)
			{
				tireTracks.m_TrackSections[j].Pos += amountMoved;
				int num = j * 4;
				tireTracks.m_Vertices[num] += amountMoved;
				tireTracks.m_Vertices[num + 1] += amountMoved;
				tireTracks.m_Vertices[num + 2] += amountMoved;
				tireTracks.m_Vertices[num + 3] += amountMoved;
			}
			tireTracks.m_MeshDirty = true;
			m_WorldMoved = true;
		}
	}

	private void Start()
	{
		SetupUVLookups();
		string[] names = Enum.GetNames(typeof(TrackGroup));
		int num = names.Length;
		m_TireTracks = new TireTracks[num];
		Transform parent = base.transform;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = new GameObject("TrackType: " + names[i]);
			gameObject.transform.SetParent(parent);
			gameObject.layer = Globals.inst.layerTerrain;
			TireTracks tireTracks = new TireTracks(m_MaxMarks[i], gameObject, m_Material);
			m_TireTracks[i] = tireTracks;
		}
	}

	private void LateUpdate()
	{
		if (m_Active)
		{
			int num = ((m_WorldMoved || QualitySettingsExtended.HQTyreTracks) ? 1 : 2);
			m_WorldMoved = false;
			m_FrameIndex++;
			if (m_FrameIndex >= num)
			{
				m_FrameIndex = 0;
			}
			int num2 = m_TireTracks.Length;
			for (int i = m_FrameIndex; i < num2; i += num)
			{
				m_TireTracks[i].UpdateMesh();
			}
		}
	}
}
