using UnityEngine;

public class UIOpenURL : UIHUDElement
{
	public void OnButtonClicked(string URL)
	{
		Application.OpenURL(URL);
	}
}
