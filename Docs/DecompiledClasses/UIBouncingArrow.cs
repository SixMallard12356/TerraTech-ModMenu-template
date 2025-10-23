#define UNITY_EDITOR
using UnityEngine;

public class UIBouncingArrow : UIHUDElement
{
	public struct BouncingArrowContext
	{
		public Transform targetTransform;

		public Vector3 targetOffset;

		public float forTime;
	}

	[SerializeField]
	private RectTransform m_ArrowRoot;

	private Transform m_TargetTrans;

	private RectTransform m_TargetRectTrans;

	private Visible m_TargetVisible;

	private Vector3 m_TargetOffset = Vector3.zero;

	private RectTransform m_RectTrans;

	private float m_TimeLeft;

	public override void Show(object context)
	{
		base.Show(context);
		BouncingArrowContext bouncingArrowContext = (BouncingArrowContext)context;
		PointAtTargetTransform(bouncingArrowContext.targetTransform, bouncingArrowContext.targetOffset, bouncingArrowContext.forTime);
	}

	public override void Hide(object context)
	{
		ClearTarget();
		base.Hide(context);
	}

	private void Update()
	{
		if (m_TimeLeft > 0f)
		{
			m_TimeLeft -= Time.deltaTime;
			if (m_TimeLeft <= 0f)
			{
				ClearTarget();
			}
		}
		UpdateFromTarget();
	}

	private void PointAtTargetTransform(Transform targetTransform, Vector3 targetOffset, float forTime)
	{
		m_TimeLeft = forTime;
		if (targetTransform != m_TargetTrans)
		{
			m_TargetTrans = targetTransform;
			if (targetTransform != null)
			{
				m_TargetRectTrans = targetTransform as RectTransform;
				m_TargetVisible = Visible.FindVisibleUpwards(targetTransform);
			}
		}
		m_TargetOffset = targetOffset;
		UpdateFromTarget();
	}

	private void UpdateFromTarget()
	{
		if ((bool)m_TargetTrans && m_TargetTrans.gameObject.activeSelf)
		{
			if ((bool)m_TargetRectTrans)
			{
				m_RectTrans.position = m_TargetRectTrans.position;
				m_RectTrans.anchoredPosition += new Vector2(m_TargetRectTrans.rect.center.x, m_TargetRectTrans.rect.yMax);
				return;
			}
			Transform transform = null;
			Vector3 vector;
			if (m_TargetVisible != null)
			{
				vector = m_TargetVisible.centrePosition;
				vector.y += m_TargetVisible.Radius;
				transform = m_TargetVisible.trans;
			}
			else
			{
				vector = m_TargetTrans.position;
				transform = m_TargetTrans;
			}
			if ((bool)transform)
			{
				vector += transform.TransformVector(m_TargetOffset);
			}
			bool flag = Vector3.Dot(vector - Singleton.camera.transform.position, Singleton.camera.transform.forward) > 0f;
			if (flag != m_ArrowRoot.gameObject.activeSelf)
			{
				m_ArrowRoot.gameObject.SetActive(flag);
			}
			if (flag)
			{
				Vector2 vector2 = UIHelpers.WorldToUILocalPosition(vector, Singleton.camera, Singleton.Manager<ManHUD>.inst.Canvas, m_RectTrans.parent as RectTransform);
				m_RectTrans.localPosition = new Vector3(vector2.x, vector2.y, 1f);
			}
		}
		else
		{
			HideSelf();
		}
	}

	private void ClearTarget()
	{
		m_TargetTrans = null;
		m_TargetRectTrans = null;
		m_TargetVisible = null;
	}

	private void OnPool()
	{
		m_RectTrans = base.transform as RectTransform;
		d.Assert(m_ArrowRoot != null && m_ArrowRoot != m_RectTrans, "Must have arrow root that is child of UIBouncingArrow object in order to function correctly!");
	}
}
