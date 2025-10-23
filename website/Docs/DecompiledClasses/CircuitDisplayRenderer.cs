using UnityEngine;

public class CircuitDisplayRenderer : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	protected Renderer m_Renderer;

	public Renderer Renderer => m_Renderer;

	private void PrePool()
	{
		m_Renderer = GetComponent<Renderer>();
	}
}
