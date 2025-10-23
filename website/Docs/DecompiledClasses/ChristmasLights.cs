using UnityEngine;

public class ChristmasLights : MonoBehaviour
{
	[SerializeField]
	private float m_FlashSpeed = 1f;

	private Material m_LightMaterial;

	private float m_TimeDelay;

	private float m_LightOffset;

	private void Start()
	{
		m_LightMaterial = GetComponent<Renderer>().material;
	}

	private void Update()
	{
		m_TimeDelay += Time.deltaTime;
		if (m_TimeDelay >= m_FlashSpeed)
		{
			m_LightOffset += 0.25f;
			if (m_LightOffset == 1f)
			{
				m_LightOffset = 0f;
			}
			m_LightMaterial.SetTextureOffset("_MainTex", new Vector2(0f, m_LightOffset));
			m_TimeDelay -= m_FlashSpeed;
		}
	}
}
