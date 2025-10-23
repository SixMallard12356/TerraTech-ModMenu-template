using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class AlignToGround : MonoBehaviour
{
	[SerializeField]
	private bool m_RotateToGroundNormal;

	private Transform m_Trans;

	private Transform[] m_ChildTransforms;

	private Vector3[] m_ChildInitialPos;

	private static List<bool> s_GameObjectsPrevActive = new List<bool>();

	private void AllignToGround(Transform trans, Vector3 initialLocalPos, bool rotateToGround)
	{
		Vector3 scenePos = m_Trans.TransformPoint(initialLocalPos);
		scenePos = (trans.position = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos));
		if (rotateToGround)
		{
			if (!Singleton.Manager<ManWorld>.inst.GetTerrainNormal(scenePos, out var outNormal))
			{
				outNormal = m_Trans.up;
			}
			trans.rotation = Quaternion.FromToRotation(Vector3.up, outNormal);
		}
	}

	private void OnPool()
	{
		m_Trans = base.transform;
		int childCount = m_Trans.childCount;
		m_ChildTransforms = new Transform[childCount];
		m_ChildInitialPos = new Vector3[childCount];
		for (int i = 0; i < childCount; i++)
		{
			Transform child = m_Trans.GetChild(i);
			m_ChildTransforms[i] = child;
			m_ChildInitialPos[i] = child.localPosition;
		}
	}

	private void OnSpawn()
	{
		for (int i = 0; i < m_ChildTransforms.Length; i++)
		{
			GameObject gameObject = m_ChildTransforms[i].gameObject;
			s_GameObjectsPrevActive.Add(gameObject.activeSelf);
			gameObject.SetActive(value: false);
		}
		for (int j = 0; j < m_ChildTransforms.Length; j++)
		{
			AllignToGround(m_ChildTransforms[j], m_ChildInitialPos[j], m_RotateToGroundNormal);
		}
		for (int k = 0; k < m_ChildTransforms.Length; k++)
		{
			if (s_GameObjectsPrevActive[k])
			{
				m_ChildTransforms[k].gameObject.SetActive(value: true);
			}
		}
		s_GameObjectsPrevActive.Clear();
	}
}
