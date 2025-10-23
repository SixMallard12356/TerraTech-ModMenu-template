using System;
using UnityEngine;

[FriendlyName("Has camera rotated")]
public class uScript_HasCameraRotated : uScriptLogic
{
	private bool m_True;

	private bool m_Started;

	private Vector3 m_ForwardDir;

	private float m_DotProductThreshold;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(float turnCameraMinDegrees)
	{
		if (!m_Started)
		{
			m_ForwardDir = Singleton.cameraTrans.forward.SetY(0f).normalized;
			m_DotProductThreshold = Mathf.Cos(turnCameraMinDegrees * ((float)Math.PI / 180f));
			m_Started = true;
		}
		else if ((bool)Singleton.camera && Vector3.Dot(Singleton.cameraTrans.forward.SetY(0f).normalized, m_ForwardDir) <= m_DotProductThreshold)
		{
			m_True = true;
		}
	}

	public void OnDisable()
	{
		m_True = false;
		m_Started = false;
	}
}
