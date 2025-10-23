using UnityEngine;

public class InstancedSceneryBatch : MonoBehaviour, IWorldTreadmill
{
	private struct WindSway
	{
		public byte offset;

		public float scale;
	}

	[SerializeField]
	private Material m_Material;

	[SerializeField]
	private float m_CullDistance = 120f;

	private Mesh m_Mesh;

	private Matrix4x4[] m_Transforms;

	private WindSway[] m_WindSway;

	private Bounds m_Bounds;

	private float m_BoundsRadius;

	private static float Fract(float x)
	{
		return x - Mathf.Floor(x);
	}

	public void Init(Mesh mesh, Matrix4x4[] transforms, bool anyWind)
	{
		m_Mesh = mesh;
		m_Transforms = transforms;
		if (anyWind)
		{
			m_WindSway = new WindSway[transforms.Length];
			for (int i = 0; i < transforms.Length; i++)
			{
				m_WindSway[i].scale = transforms[i][0, 1];
				transforms[i][0, 1] = 0f;
				m_WindSway[i].offset = (byte)(16f * Fract(transforms[i][0, 3] * 0.1f + transforms[i][2, 3] * 0.2f));
			}
		}
		Vector4 lhs = transforms[0].GetColumn(3);
		Vector4 lhs2 = transforms[0].GetColumn(3);
		for (int j = 0; j < transforms.Length; j++)
		{
			Matrix4x4 matrix4x = transforms[j];
			lhs = Vector4.Min(lhs, matrix4x.GetColumn(3));
			lhs2 = Vector4.Max(lhs2, matrix4x.GetColumn(3));
		}
		lhs -= new Vector4(1f, 1f, 1f);
		lhs2 += new Vector4(1f, 1f, 1f);
		m_Bounds.SetMinMax(new Vector3(lhs.x, lhs.y, lhs.z), new Vector3(lhs2.x, lhs2.y, lhs2.z));
		m_BoundsRadius = m_Bounds.extents.magnitude;
		base.transform.position = m_Bounds.center;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_Bounds.center, m_BoundsRadius);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
		m_Transforms = null;
		m_WindSway = null;
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		Vector4 vector = new Vector4(amountToMove.x, amountToMove.y, amountToMove.z, 0f);
		for (int i = 0; i < m_Transforms.Length; i++)
		{
			m_Transforms[i].SetColumn(3, m_Transforms[i].GetColumn(3) + vector);
		}
		Vector3 vector2 = new Vector3(amountToMove.x, amountToMove.y, amountToMove.z);
		m_Bounds.center += vector2;
	}

	private void LateUpdate()
	{
		Vector3 lhs = m_Bounds.center - Singleton.cameraTrans.position;
		if (!(lhs.magnitude - m_BoundsRadius < m_CullDistance) || !(Vector3.Dot(lhs, Singleton.cameraTrans.forward) > 0f - m_BoundsRadius))
		{
			return;
		}
		float[] windTurbulence = Singleton.Manager<ManWorld>.inst.WindTurbulence;
		if (m_WindSway != null)
		{
			for (int i = 0; i < m_WindSway.Length; i++)
			{
				m_Transforms[i][0, 1] = windTurbulence[m_WindSway[i].offset] * m_WindSway[i].scale;
			}
		}
		Graphics.DrawMeshInstanced(m_Mesh, 0, m_Material, m_Transforms);
	}
}
