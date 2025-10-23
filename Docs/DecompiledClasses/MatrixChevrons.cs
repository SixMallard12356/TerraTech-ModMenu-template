using UnityEngine;

public class MatrixChevrons : MonoBehaviour
{
	[SerializeField]
	private float m_ScrollSpeed = 1f;

	private Material m_ScrollMaterial;

	private float m_TimeDelay;

	private float m_TextureOffset;

	private void Start()
	{
		m_ScrollMaterial = GetComponent<Renderer>().material;
	}

	private void Update()
	{
		m_TimeDelay += Time.deltaTime;
		if (m_TimeDelay >= m_ScrollSpeed)
		{
			m_TextureOffset += 0.125f;
			if (m_TextureOffset == 0.75f)
			{
				m_TextureOffset = 0f;
			}
			m_ScrollMaterial.SetTextureOffset("_MainTex", new Vector2(0f, m_TextureOffset));
			m_TimeDelay -= m_ScrollSpeed;
		}
	}
}
