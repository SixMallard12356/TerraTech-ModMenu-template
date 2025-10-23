using UnityEngine;

public class UIHintFloatingElementConsole : UIHintFloatingElement
{
	[SerializeField]
	private GameObject PS4FloatingElement;

	[SerializeField]
	private GameObject PS4FloatingElementAlt;

	[SerializeField]
	private GameObject XB1FloatingElement;

	[SerializeField]
	private GameObject SwitchFloatingElement;

	public override void Show()
	{
		base.gameObject.SetActive(value: true);
		bool flag = SKU.PS4UI && PS4FloatingElementAlt.IsNotNull() && SKU.FlipEnterCancelButtons;
		PS4FloatingElement.SetActive(SKU.PS4UI && !flag);
		if (PS4FloatingElementAlt != null)
		{
			PS4FloatingElementAlt.SetActive(SKU.PS4UI && flag);
		}
		XB1FloatingElement.SetActive(SKU.XboxOneUI);
		SwitchFloatingElement.SetActive(SKU.SwitchUI);
	}
}
