using UnityEngine;

public class CheckpointGate : CheckpointTriggerGate
{
	[SerializeField]
	private BoxCollider m_Trigger;

	[SerializeField]
	private Transform m_LeftBase;

	[SerializeField]
	private Transform m_LeftBar;

	[SerializeField]
	private Transform m_LeftTop;

	[SerializeField]
	private Transform m_RightBase;

	[SerializeField]
	private Transform m_RightBar;

	[SerializeField]
	private Transform m_RightTop;

	[SerializeField]
	private Transform m_TopBar;

	protected override void SetupCheckpoint(Vector3 position, Vector3 fwdDirection, Vector3 upDir, float width, float height)
	{
		base.Trans.position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position);
		base.Trans.rotation = Quaternion.LookRotation(fwdDirection.SetY(0f));
		Vector3 vector = Vector3.up.Cross(fwdDirection);
		float num = 0.5f * width;
		Vector3 vector2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(position - vector * num);
		m_LeftBase.position = vector2;
		Vector3 vector3 = Singleton.Manager<ManWorld>.inst.ProjectToGround(position + vector * num);
		m_RightBase.position = vector3;
		float num2 = Mathf.Max(m_RightBase.position.y, m_LeftBase.position.y) + height;
		m_LeftTop.position = vector2.SetY(num2);
		m_RightTop.position = vector3.SetY(num2);
		float y = m_LeftTop.position.y - m_LeftBase.position.y;
		m_LeftBar.localScale = new Vector3(1f, y, 1f);
		float y2 = m_RightTop.position.y - m_RightBase.position.y;
		m_RightBar.localScale = new Vector3(1f, y2, 1f);
		Vector3 vector4 = new Vector3(base.Trans.position.x, num2, base.Trans.position.z);
		m_TopBar.position = vector4;
		m_TopBar.localScale = new Vector3(width, 1f, 1f);
		float num3 = Mathf.Min(m_RightBase.position.y, m_LeftBase.position.y);
		float num4 = num2 - num3;
		float newY = num2 - 0.5f * num4;
		base.Trans.rotation = Quaternion.LookRotation(fwdDirection.SetY(0f), upDir);
		m_Trigger.gameObject.transform.position = vector4.SetY(newY);
		m_Trigger.size = new Vector3(width, num4, m_Trigger.size.z);
	}
}
