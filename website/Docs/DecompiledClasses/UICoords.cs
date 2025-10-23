using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UICoords : MonoBehaviour
{
	private TextMeshProUGUI m_Text;

	private int m_ShowX = int.MaxValue;

	private int m_ShowZ = int.MaxValue;

	private void Update()
	{
		if ((bool)m_Text && (bool)Singleton.playerTank)
		{
			Vector3 vector = Singleton.playerPos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
			int num = (int)vector.x;
			int num2 = (int)vector.z;
			if (num != m_ShowX || num2 != m_ShowZ)
			{
				m_ShowX = num;
				m_ShowZ = num2;
				int num3 = (int)GameUnits.GetDistance(m_ShowX, forceBaseUnit: true);
				int num4 = (int)GameUnits.GetDistance(m_ShowZ, forceBaseUnit: true);
				m_Text.text = $"{num3} , {num4}";
			}
		}
	}

	private void OnPool()
	{
		m_Text = GetComponent<TextMeshProUGUI>();
	}
}
