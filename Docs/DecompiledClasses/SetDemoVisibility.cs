using UnityEngine;

public class SetDemoVisibility : MonoBehaviour
{
	[SerializeField]
	private bool m_ShowInDemo = true;

	private void Awake()
	{
		bool active = !m_ShowInDemo;
		base.gameObject.SetActive(active);
	}
}
