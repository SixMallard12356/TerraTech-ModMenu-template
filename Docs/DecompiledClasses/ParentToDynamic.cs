using UnityEngine;

public class ParentToDynamic : MonoBehaviour
{
	private bool m_Parent;

	private void OnEnable()
	{
		m_Parent = true;
	}

	private void Update()
	{
		if (m_Parent)
		{
			base.transform.parent = Singleton.dynamicContainer;
			m_Parent = false;
		}
	}
}
