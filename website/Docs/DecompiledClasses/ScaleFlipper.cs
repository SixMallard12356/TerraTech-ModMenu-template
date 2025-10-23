#define UNITY_EDITOR
using UnityEngine;

public class ScaleFlipper : MonoBehaviour
{
	public Vector3 m_FlipVector = new Vector3(-1f, 1f, 1f);

	private Transform m_MyTrans;

	private Transform m_Trans
	{
		get
		{
			if (!m_MyTrans)
			{
				m_MyTrans = base.transform;
			}
			return m_MyTrans;
		}
		set
		{
			m_MyTrans = value;
		}
	}

	public void Flip()
	{
		if ((bool)m_Trans)
		{
			m_Trans.localScale = Vector3.Scale(m_Trans.localScale, m_FlipVector);
		}
		else
		{
			d.LogError("Scale flipper transform is null how could this happen");
		}
	}

	public void Init()
	{
		m_Trans = base.transform;
	}
}
