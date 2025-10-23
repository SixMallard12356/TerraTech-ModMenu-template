using System.Collections.Generic;
using UnityEngine;

public class WorldPushbackBarrier : MonoBehaviour
{
	private struct ForceApplicationData
	{
		public Rigidbody targetRbody;

		public Vector3 force;
	}

	[SerializeField]
	[Range(-1f, 1f)]
	[Tooltip("0=teleport to centre, 1=teleport to where you hit the warning edge")]
	private float m_TeleportDistanceFromCenter = 0.9f;

	private List<ForceApplicationData> m_ForceApplicationList = new List<ForceApplicationData>();

	private void OnSpawn()
	{
	}

	private void OnRecycle()
	{
		m_ForceApplicationList.Clear();
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		m_ForceApplicationList.Clear();
		float dangerDistance = Singleton.Manager<ManNetwork>.inst.DangerDistance;
		float teleportDistance = Singleton.Manager<ManNetwork>.inst.TeleportDistance;
		float pushBackConst = Singleton.Manager<ManNetwork>.inst.PushBackConst;
		float pushBackDistance = Singleton.Manager<ManNetwork>.inst.PushBackDistance;
		float pushBackVelocityCancel = Singleton.Manager<ManNetwork>.inst.PushBackVelocityCancel;
		if (!Singleton.playerTank)
		{
			return;
		}
		Tank playerTank = Singleton.playerTank;
		Visible visible = playerTank.visible;
		Vector3 boundsCentreWorld = playerTank.boundsCentreWorld;
		Vector3 vector = (boundsCentreWorld - position).SetY(0f);
		float magnitude = vector.magnitude;
		if (magnitude >= teleportDistance)
		{
			Vector3 vector2 = position + (boundsCentreWorld - position).SetY(0f).normalized * dangerDistance * m_TeleportDistanceFromCenter;
			if (playerTank.grounded)
			{
				visible.Teleport(vector2, playerTank.beam.CalcHoverOrientation(), tankGrounded: true, stop: false);
			}
			else
			{
				vector2.y = Mathf.Max(Singleton.Manager<ManWorld>.inst.ProjectToGround(vector2, hitScenery: true).y + 2f, boundsCentreWorld.y);
				visible.Teleport(vector2, visible.trans.rotation, tankGrounded: false, stop: false);
			}
			Vector3 vector3 = (playerTank ? playerTank.boundsCentreWorldNoCheck : vector2);
			Singleton.Manager<CameraManager>.inst.ResetCamera(Singleton.cameraTrans.position + vector3 - boundsCentreWorld, Singleton.cameraTrans.rotation);
			return;
		}
		float num = (magnitude - dangerDistance) / (teleportDistance - dangerDistance);
		if (!(num > 0f))
		{
			return;
		}
		Vector3 normalized = vector.normalized;
		float num2 = num * (0f - pushBackDistance) - pushBackConst;
		if (pushBackVelocityCancel > 0f)
		{
			float num3 = Vector3.Dot(normalized, visible.rbody.velocity);
			if (num3 > 0f && Time.deltaTime > 0f)
			{
				num2 -= num3 * num3 * 0.5f / Mathf.Max(1f, teleportDistance - magnitude) * pushBackVelocityCancel;
			}
		}
		m_ForceApplicationList.Add(new ForceApplicationData
		{
			targetRbody = visible.rbody,
			force = num2 * normalized
		});
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < m_ForceApplicationList.Count; i++)
		{
			ForceApplicationData forceApplicationData = m_ForceApplicationList[i];
			forceApplicationData.targetRbody.AddForce(forceApplicationData.force, ForceMode.Acceleration);
		}
	}
}
