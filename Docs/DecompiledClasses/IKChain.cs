#define UNITY_EDITOR
using System.Collections;
using UnityEngine;

public class IKChain : MonoBehaviour
{
	public int m_ChainLength = 2;

	public Transform m_Target;

	public Transform m_Pole;

	public int m_Iterations = 3;

	public float m_SnapBackStrength = 1f;

	private float m_TotalBoneLength;

	private Transform[] m_Bones = new Transform[0];

	private float[] m_BoneLengths = new float[0];

	private Vector3[] m_StartingDirections = new Vector3[0];

	private Quaternion[] m_StartingBoneRotations = new Quaternion[0];

	private Quaternion m_StartingTargetRotation;

	private Quaternion m_StartingRootRotation;

	private Vector3[] m_CachedPositions = new Vector3[0];

	private Plane m_CachedPlane;

	private Vector3 m_CachedProjectedPole;

	private Vector3 m_CachedProjectedPosition;

	private float m_CachedAngle;

	private Quaternion m_CachedRootRotation;

	private Quaternion m_CachedRootRotationDelta;

	private IEnumerator IKUpdator;

	private void Awake()
	{
		InitIKUpdate();
	}

	private void InitIKUpdate()
	{
		if (IKUpdator != null)
		{
			StopCoroutine(IKUpdator);
		}
		if (InitIK())
		{
			IKUpdator = UpdateIKCo();
			StartCoroutine(IKUpdator);
		}
	}

	private bool InitIK()
	{
		m_Bones = new Transform[m_ChainLength + 1];
		m_CachedPositions = new Vector3[m_ChainLength + 1];
		m_BoneLengths = new float[m_ChainLength];
		m_StartingDirections = new Vector3[m_ChainLength + 1];
		m_StartingBoneRotations = new Quaternion[m_ChainLength + 1];
		m_StartingTargetRotation = m_Target.rotation;
		m_TotalBoneLength = 0f;
		Transform parent = base.transform;
		string text = "";
		for (int num = m_Bones.Length - 1; num >= 0; num--)
		{
			m_Bones[num] = parent;
			m_StartingBoneRotations[num] = parent.rotation;
			if (num != m_Bones.Length - 1)
			{
				m_StartingDirections[num] = m_Bones[num + 1].position - parent.position;
				m_BoneLengths[num] = (m_Bones[num + 1].position - parent.position).magnitude;
				m_TotalBoneLength += m_BoneLengths[num];
			}
			else
			{
				m_StartingDirections[num] = m_Target.position - parent.position;
			}
			parent = parent.parent;
			if (parent == null)
			{
				if (num != 0)
				{
					text.Insert(0, "scene:");
					d.LogError("chainlength for ik on " + text + " is longer than possible chains! aborting ik initialisation!");
					return false;
				}
			}
			else
			{
				text = "/" + parent.name + text;
			}
		}
		return true;
	}

	private void ResolveIK()
	{
		if (m_Target == null)
		{
			return;
		}
		for (int i = 0; i < m_Bones.Length; i++)
		{
			m_CachedPositions[i] = m_Bones[i].position;
		}
		m_CachedRootRotation = ((m_Bones[0].parent != null) ? m_Bones[0].parent.rotation : Quaternion.identity);
		m_CachedRootRotationDelta = m_CachedRootRotation * Quaternion.Inverse(m_StartingRootRotation);
		if ((m_Target.position - m_Bones[0].position).sqrMagnitude >= m_TotalBoneLength * m_TotalBoneLength)
		{
			Vector3 normalized = (m_Target.position - m_CachedPositions[0]).normalized;
			for (int j = 1; j < m_CachedPositions.Length; j++)
			{
				m_CachedPositions[j] = m_CachedPositions[j - 1] + normalized * m_BoneLengths[j - 1];
			}
		}
		else
		{
			for (int k = 0; k < m_CachedPositions.Length - 1; k++)
			{
				m_CachedPositions[k + 1] = Vector3.Lerp(m_CachedPositions[k + 1], m_CachedPositions[k] + m_CachedRootRotationDelta * m_StartingDirections[k], m_SnapBackStrength);
			}
			for (int l = 0; l < m_Iterations; l++)
			{
				m_CachedPositions[m_CachedPositions.Length - 1] = m_Target.position;
				for (int num = m_CachedPositions.Length - 2; num >= 0; num--)
				{
					Vector3 normalized = (m_CachedPositions[num] - m_CachedPositions[num + 1]).normalized;
					m_CachedPositions[num] = m_CachedPositions[num + 1] + normalized * m_BoneLengths[num];
				}
				m_CachedPositions[0] = m_Bones[0].position;
				for (int m = 1; m < m_CachedPositions.Length; m++)
				{
					Vector3 normalized = (m_CachedPositions[m] - m_CachedPositions[m - 1]).normalized;
					m_CachedPositions[m] = m_CachedPositions[m - 1] + normalized * m_BoneLengths[m - 1];
				}
			}
		}
		if (m_Pole != null)
		{
			for (int n = 1; n < m_CachedPositions.Length - 1; n++)
			{
				m_CachedPlane = new Plane(m_CachedPositions[n + 1] - m_CachedPositions[n - 1], m_CachedPositions[n - 1]);
				m_CachedProjectedPole = m_CachedPlane.ClosestPointOnPlane(m_Pole.position) - m_CachedPositions[n - 1];
				m_CachedProjectedPosition = m_CachedPlane.ClosestPointOnPlane(m_CachedPositions[n]) - m_CachedPositions[n - 1];
				m_CachedAngle = Vector3.SignedAngle(m_CachedProjectedPosition, m_CachedProjectedPole, m_CachedPlane.normal);
				m_CachedPositions[n] = Quaternion.AngleAxis(m_CachedAngle, m_CachedPlane.normal) * (m_CachedPositions[n] - m_CachedPositions[n - 1]) + m_CachedPositions[n - 1];
			}
		}
		for (int num2 = 0; num2 < m_Bones.Length; num2++)
		{
			if (num2 == m_CachedPositions.Length - 1)
			{
				m_Bones[num2].rotation = m_Target.rotation * Quaternion.Inverse(m_StartingTargetRotation) * m_StartingBoneRotations[num2];
			}
			else
			{
				m_Bones[num2].rotation = Quaternion.FromToRotation(m_StartingDirections[num2], m_CachedPositions[num2 + 1] - m_CachedPositions[num2]) * m_StartingBoneRotations[num2];
			}
			m_Bones[num2].position = m_CachedPositions[num2];
		}
	}

	private IEnumerator UpdateIKCo()
	{
		while (true)
		{
			yield return null;
			ResolveIK();
		}
	}

	private void OnValidate()
	{
		if (m_ChainLength < 2)
		{
			m_ChainLength = 2;
			d.LogError("Cannot have a chainlength of less than 2 when using IK");
		}
	}
}
