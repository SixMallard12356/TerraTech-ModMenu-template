using UnityEngine;

public class TestSafeArea : MonoBehaviour
{
	private ManFreeSpace.SafeArea m_SafeArea;

	private void Awake()
	{
		m_SafeArea = new ManFreeSpace.TechSafeArea(base.transform, 0f);
	}

	private void OnEnable()
	{
		if (Singleton.Manager<ManFreeSpace>.inst != null)
		{
			UpdateRadius();
			Singleton.Manager<ManFreeSpace>.inst.AddSafeArea(m_SafeArea);
		}
	}

	private void OnDisable()
	{
		if (Singleton.Manager<ManFreeSpace>.inst != null)
		{
			Singleton.Manager<ManFreeSpace>.inst.RemoveSafeArea(m_SafeArea);
		}
	}

	private void Update()
	{
		UpdateRadius();
	}

	private void UpdateRadius()
	{
		SphereCollider component = GetComponent<SphereCollider>();
		if (component != null)
		{
			m_SafeArea.m_Radius = component.radius;
		}
		else
		{
			m_SafeArea.m_Radius = 0f;
		}
	}
}
