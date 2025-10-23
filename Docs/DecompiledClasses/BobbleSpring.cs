using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BobbleSpring : MonoBehaviour
{
	[SerializeField]
	private Transform m_AnchorPoint;

	[SerializeField]
	private Transform m_Geometry;

	private GameObject m_GameObject;

	private Transform m_Transform;

	private Rigidbody m_Body;

	private ConfigurableJoint m_Joint;

	private Vector3 m_DefaultPosition;

	private Quaternion m_DefaultRotation;

	private void OnAttach(ModuleGeneric module)
	{
		m_Joint.connectedBody = module.block.tank.rbody;
		m_Geometry.parent = m_Transform;
		m_GameObject.SetActive(value: true);
	}

	private void OnDetach(ModuleGeneric module)
	{
		m_Transform.localPosition = m_DefaultPosition;
		m_Transform.localRotation = m_DefaultRotation;
		m_Body.velocity = Vector3.zero;
		m_Body.angularVelocity = Vector3.zero;
		m_Joint.connectedBody = null;
		m_Geometry.parent = module.block.trans;
		m_GameObject.SetActive(value: false);
	}

	private void OnPool()
	{
		m_GameObject = base.gameObject;
		m_Transform = base.transform;
		m_Body = GetComponent<Rigidbody>();
		m_Joint = GetComponent<ConfigurableJoint>();
		m_Joint.anchor = m_Transform.InverseTransformPoint(m_AnchorPoint.position);
		m_DefaultPosition = m_Transform.localPosition;
		m_DefaultRotation = m_Transform.localRotation;
	}

	private void OnSpawn()
	{
		m_GameObject.SetActive(value: false);
	}

	private void OnRecycle()
	{
		m_GameObject.SetActive(value: false);
	}
}
