#define UNITY_EDITOR
using UnityEngine;

public class FloatingTextOverlay : Overlay
{
	private FloatingTextOverlayData m_Data;

	private FloatingTextPanel m_PanelInst;

	private Visible m_Subject;

	private WorldPosition m_SourcePosition;

	private float m_SubjectHalfHeight;

	private bool m_IsAlive;

	private string m_MoneyString;

	private float m_Timer;

	public FloatingTextOverlay(FloatingTextOverlayData data)
	{
		m_Data = data;
	}

	public void Set(string moneyString, WorldPosition pos)
	{
		m_Subject = null;
		m_SourcePosition = pos;
		m_SubjectHalfHeight = 0f;
		m_MoneyString = moneyString;
		m_IsAlive = true;
	}

	public void Set(string moneyString, Visible subject)
	{
		d.Assert(subject != null, "Trying to Setup FloatingTextOverlay with null subject passed in!? This is not (currently) supported");
		m_Subject = subject;
		if (subject != null)
		{
			m_SourcePosition = WorldPosition.FromScenePosition(subject.centrePosition);
			m_SubjectHalfHeight = ((subject.type == ObjectTypes.Vehicle) ? (subject.tank.blockBounds.extents.y + 0.5f) : subject.Radius);
			m_MoneyString = moneyString;
			m_IsAlive = true;
		}
		else
		{
			m_IsAlive = false;
		}
	}

	public override void Update()
	{
		bool flag = false;
		if (m_IsAlive && m_Data.VisibleInCurrentMode)
		{
			if (m_PanelInst == null)
			{
				m_PanelInst = m_Data.m_PanelPrefab.Spawn();
				m_PanelInst.Text = m_MoneyString;
				m_PanelInst.Alpha = 1f;
				m_Timer = 0f;
			}
			if (m_Timer > m_Data.m_StayTime - m_Data.m_FadeOutTime)
			{
				m_PanelInst.Alpha = (m_Data.m_StayTime - m_Timer) / m_Data.m_FadeOutTime;
			}
			Vector3 scenePos;
			if (m_Subject == null || !m_Subject.isActive)
			{
				m_Subject = null;
				scenePos = m_SourcePosition.ScenePosition;
			}
			else
			{
				scenePos = m_Subject.centrePosition;
				m_SourcePosition = WorldPosition.FromScenePosition(in scenePos);
			}
			float magnitude = (Singleton.cameraTrans.position - scenePos).magnitude;
			magnitude = Mathf.Min(Mathf.Max(magnitude, m_Data.m_MinCameraResizeDist), m_Data.m_MaxCameraResizeDist) - m_Data.m_MinCameraResizeDist;
			float num = m_Data.m_CamResizeCurve.Evaluate(magnitude / (m_Data.m_MaxCameraResizeDist - m_Data.m_MinCameraResizeDist));
			if (num != m_PanelInst.LocalScale)
			{
				m_PanelInst.LocalScale = num;
			}
			Vector3 vector = scenePos + Vector3.up * m_SubjectHalfHeight;
			Vector3 a = Singleton.camera.WorldToViewportPoint(Vector3.Lerp(vector, vector + Vector3.zero.SetY(m_Data.m_AboveDist), m_Timer / m_Data.m_StayTime));
			if (a.z <= 0f)
			{
				a = new Vector3(0f, -2f, 0f);
			}
			Vector3 localPosition = Vector3.Scale(a, Singleton.Manager<ManUI>.inst.m_ReferenceResolution) - Singleton.Manager<ManUI>.inst.m_HalfReferenceResolution;
			Vector3 localPosition2 = m_PanelInst.LocalPosition;
			if (!localPosition.Equals(localPosition2))
			{
				m_PanelInst.LocalPosition = localPosition;
			}
			flag = m_PanelInst.Alpha > 0f;
			m_Timer += Time.deltaTime;
		}
		if (!flag)
		{
			PerformCleanup();
		}
	}

	public override bool HasExpired()
	{
		return !m_IsAlive;
	}

	public override void PerformCleanup()
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.Recycle();
			m_PanelInst = null;
		}
		m_Subject = null;
		m_IsAlive = false;
	}
}
