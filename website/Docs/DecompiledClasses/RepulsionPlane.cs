#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class RepulsionPlane : MonoBehaviour
{
	[Tooltip("The force per second applied upwards to rigidbodies that enter the repulsion area")]
	[SerializeField]
	protected float m_RepulsionForceUpwardDefault = 1f;

	[SerializeField]
	[Tooltip("The scale of the repulsion applied to rigidbodies derived from the repulsion upward force. Applied in the direction from the center of the repulsor to the mean center of the rigidbody")]
	protected float m_RepulsionForceOutwardScaleFactor = 0.0001f;

	[SerializeField]
	protected bool m_EnabledByDefault;

	[SerializeField]
	protected float m_SideLength = 1f;

	[Tooltip("Defines how much the current repulsion factor falls off over the distance away from the repulsion plane")]
	[FormerlySerializedAs("m_RepulsionDropoffCurve")]
	[SerializeField]
	protected AnimationCurve m_RepulsionVerticalDropoffCurve;

	[SerializeField]
	[Tooltip("Defines how strong the repulsion away from the center of the plane ramps up")]
	[FormerlySerializedAs("m_SideRepulsionRampCurve")]
	protected AnimationCurve m_OutwardRepulsionRampCurve;

	[SerializeField]
	protected ParticleSystem m_RespulsionParticles;

	[SerializeField]
	[Tooltip("The time it takes the repulsor plane to go between activation states")]
	protected float m_ActivationRampUpDuration = 1f;

	private static readonly Color _gizmoColor = new Color(0.137f, 0.655f, 1f);

	private static readonly int _gizmoIterations = 10;

	private const float k_RepulsionForceNormalScaleFactor = 10000f;

	protected GradientAlphaKey[] m_StartCOLAlphaKeys;

	protected bool m_RepulsingEnabled;

	protected float m_RepulsionActivationPerc;

	private Dictionary<Rigidbody, List<Vector3>> m_RepulsedBodies = new Dictionary<Rigidbody, List<Vector3>>();

	private TankBlock m_Block;

	private Collider[] m_TriggerVolumes;

	private static GradientAlphaKey[] _s_FrameAlphaKeys = new GradientAlphaKey[16];

	protected float RepulsionMaxDistance
	{
		get
		{
			if (m_RepulsionVerticalDropoffCurve != null)
			{
				return m_RepulsionVerticalDropoffCurve.keys[m_RepulsionVerticalDropoffCurve.length - 1].time;
			}
			return 0f;
		}
	}

	public float RepulsionUpwardForce { get; private set; }

	public float WeakRepulsionOutwardForce { get; private set; }

	public float CurrentRepulsionPenetration01 { get; private set; }

	public bool IsRepulsing => m_RepulsingEnabled;

	private float DesiredRepulsionActivationPerc
	{
		get
		{
			if (!m_RepulsingEnabled)
			{
				return 0f;
			}
			return 1f;
		}
	}

	public void Init(TankBlock block)
	{
		m_Block = block;
		block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		block.BlockFixedUpdate.Subscribe(OnUpdate);
		block.AttachedEvent.Subscribe(OnAttached);
		block.DetachingEvent.Subscribe(OnDetaching);
		m_StartCOLAlphaKeys = m_RespulsionParticles.colorOverLifetime.color.gradient.alphaKeys;
		InitTriggerStuff();
		SetRepulsionForce(m_RepulsionForceUpwardDefault);
		SetRepulsing(m_EnabledByDefault, forceSet: true);
	}

	public void SetRepulsionForce(float value)
	{
		RepulsionUpwardForce = 10000f * value;
		WeakRepulsionOutwardForce = RepulsionUpwardForce * m_RepulsionForceOutwardScaleFactor;
		if (m_RespulsionParticles != null)
		{
			ParticleSystem.MainModule main = m_RespulsionParticles.main;
			main.simulationSpeed = value / m_RepulsionForceUpwardDefault;
		}
	}

	public void SetRepulsing(bool state, bool forceSet = false)
	{
		if (m_RepulsingEnabled != state || forceSet)
		{
			m_RepulsingEnabled = state;
			SetTriggersEnabled(state);
		}
	}

	private void SetTriggersEnabled(bool state)
	{
		Collider[] triggerVolumes = m_TriggerVolumes;
		for (int i = 0; i < triggerVolumes.Length; i++)
		{
			triggerVolumes[i].enabled = state;
		}
	}

	private void UpdateVisuals()
	{
		if (!(m_RespulsionParticles == null))
		{
			if (_s_FrameAlphaKeys.Length != m_StartCOLAlphaKeys.Length)
			{
				Array.Resize(ref _s_FrameAlphaKeys, m_StartCOLAlphaKeys.Length);
			}
			for (int i = 0; i < m_StartCOLAlphaKeys.Length; i++)
			{
				_s_FrameAlphaKeys[i] = m_StartCOLAlphaKeys[i];
				_s_FrameAlphaKeys[i].alpha *= m_RepulsionActivationPerc;
			}
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = m_RespulsionParticles.colorOverLifetime;
			Gradient gradient = new Gradient();
			gradient.SetKeys(colorOverLifetime.color.gradient.colorKeys, _s_FrameAlphaKeys);
			colorOverLifetime.color = gradient;
		}
	}

	protected void OnTriggerAction(TriggerCatcher.Interaction type, Collider col)
	{
		if (!m_RepulsingEnabled || type != TriggerCatcher.Interaction.Stay || (col.gameObject.layer & (int)Globals.inst.layerTank) == 0)
		{
			return;
		}
		Visible visible = Visible.FindVisibleUpwards(col);
		if (visible == null || (visible.type != ObjectTypes.Vehicle && visible.type != ObjectTypes.Block && visible.type != ObjectTypes.Chunk))
		{
			return;
		}
		Rigidbody rbody = visible.rbody;
		if (rbody == null && visible.type == ObjectTypes.Block && visible.block.tank != null)
		{
			rbody = visible.block.tank.rbody;
		}
		if (!(rbody == null) && (!(m_Block != null) || (!(rbody == m_Block.rbody) && !(rbody == m_Block.tank.rbody))))
		{
			Vector3 originPoint = ((!(visible.block != null)) ? visible.centrePosition : visible.block.centreOfMassWorld);
			originPoint = GetClosestPointToTriggerVolume(originPoint);
			if (!m_RepulsedBodies.ContainsKey(rbody))
			{
				m_RepulsedBodies[rbody] = new List<Vector3>();
			}
			m_RepulsedBodies[rbody].Add(originPoint);
		}
	}

	private void InitTriggerStuff()
	{
		if (base.gameObject.layer != (int)Globals.inst.layerTrigger)
		{
			d.LogErrorFormat("Incorrect layer for repulsion plane on '{0}', setting to Trigger!", base.transform.GetTransformHeirarchyPath());
			base.gameObject.layer = Globals.inst.layerTrigger;
		}
		BoxCollider boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		boxCollider.isTrigger = true;
		boxCollider.size = new Vector3(m_SideLength, RepulsionMaxDistance, m_SideLength);
		boxCollider.center = Vector3.up * (RepulsionMaxDistance / 2f);
		m_TriggerVolumes = new Collider[1] { boxCollider };
	}

	private Vector3 GetClosestPointToTriggerVolume(Vector3 originPoint)
	{
		if (m_TriggerVolumes.Length == 0)
		{
			d.LogError("Attempted to get closest point in trigger volumes but there were no trigger volumes set! Returning origin point!");
			return originPoint;
		}
		for (int i = 0; i < m_TriggerVolumes.Length; i++)
		{
			if (m_TriggerVolumes[i].Contains(originPoint))
			{
				return originPoint;
			}
		}
		Vector3 vector = Vector3.negativeInfinity;
		for (int j = 0; j < m_TriggerVolumes.Length; j++)
		{
			Vector3 vector2 = m_TriggerVolumes[j].ClosestPoint(originPoint);
			if (vector == Vector3.negativeInfinity || Vector3.Distance(vector2, originPoint) < Vector3.Distance(vector, originPoint))
			{
				vector = vector2;
			}
		}
		return vector;
	}

	private void OnAttached()
	{
		TriggerCatcher.Subscribe(base.gameObject, TriggerCatcher.Interaction.Stay, OnTriggerAction);
	}

	private void OnDetaching()
	{
		TriggerCatcher.Unsubscribe(base.gameObject, TriggerCatcher.Interaction.Stay, OnTriggerAction);
	}

	private void OnUpdate()
	{
		if (m_RepulsionActivationPerc != DesiredRepulsionActivationPerc)
		{
			m_RepulsionActivationPerc = Mathf.Lerp(m_RepulsionActivationPerc, DesiredRepulsionActivationPerc, Time.deltaTime * (1f / m_ActivationRampUpDuration));
			if (m_RepulsionActivationPerc.Approximately(DesiredRepulsionActivationPerc, 0.01f))
			{
				m_RepulsionActivationPerc = DesiredRepulsionActivationPerc;
			}
		}
		UpdateVisuals();
	}

	private void OnFixedUpdate()
	{
		Vector3 vector = base.transform.up * RepulsionUpwardForce;
		Plane plane = new Plane(base.transform.up, base.transform.position);
		float num = float.MaxValue;
		CurrentRepulsionPenetration01 = 0f;
		foreach (KeyValuePair<Rigidbody, List<Vector3>> repulsedBody in m_RepulsedBodies)
		{
			if (repulsedBody.Key == null)
			{
				continue;
			}
			Vector3 zero = Vector3.zero;
			float num2 = float.MaxValue;
			for (int i = 0; i < repulsedBody.Value.Count; i++)
			{
				float distanceToPoint = plane.GetDistanceToPoint(repulsedBody.Value[i]);
				if (num2 > distanceToPoint)
				{
					num2 = distanceToPoint;
				}
				zero += repulsedBody.Value[i];
			}
			zero /= (float)repulsedBody.Value.Count;
			if (num > num2)
			{
				num = num2;
			}
			Vector3 vector2 = zero - base.transform.position;
			float time = vector2.magnitude / (m_SideLength / 2f);
			Vector3 vector3 = m_OutwardRepulsionRampCurve.Evaluate(time) * vector2.normalized * WeakRepulsionOutwardForce;
			float num3 = m_RepulsionVerticalDropoffCurve.Evaluate(num2) * m_RepulsionActivationPerc;
			repulsedBody.Key.AddForceAtPosition(num3 * vector + num3 * vector3, zero, ForceMode.Force);
		}
		if (num != float.MaxValue)
		{
			CurrentRepulsionPenetration01 = 1f - num / m_RepulsionVerticalDropoffCurve.keys.Last().time;
		}
		m_RepulsedBodies.Clear();
	}

	private void OnDrawGizmosSelected()
	{
	}
}
