using UnityEngine;

public class UISystemButtons : MonoBehaviour
{
	[SerializeField]
	private Transform m_ButtonsParent;

	private bool m_ParentVisible;

	private void OnSpawn()
	{
		if (m_ButtonsParent.IsNotNull())
		{
			m_ParentVisible = m_ButtonsParent.gameObject.activeSelf;
		}
	}

	private void Update()
	{
		if (!m_ButtonsParent.IsNotNull())
		{
			return;
		}
		bool flag = false;
		for (int num = m_ButtonsParent.childCount - 1; num >= 0; num--)
		{
			if (m_ButtonsParent.GetChild(num).gameObject.activeSelf)
			{
				flag = true;
				break;
			}
		}
		if (m_ParentVisible != flag)
		{
			m_ButtonsParent.gameObject.SetActive(flag);
			m_ParentVisible = flag;
		}
	}
}
