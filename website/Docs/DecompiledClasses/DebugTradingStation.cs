using TMPro;
using UnityEngine;

public class DebugTradingStation : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_Text;

	private void Update()
	{
		m_Text.text = $"{(base.transform.position - Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition).SetY(0f).magnitude:0.0}";
		base.transform.LookAt(Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition);
	}
}
