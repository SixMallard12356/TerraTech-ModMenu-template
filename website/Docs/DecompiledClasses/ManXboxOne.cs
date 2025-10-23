using UnityEngine;

public class ManXboxOne : MonoBehaviour
{
	public enum VirtualKeyboardInputMode
	{
		AlphaNumeric = 40,
		Default = 0,
		Url = 1,
		EmailSmtpAddress = 5,
		Number = 29,
		Password = 31,
		TelephoneNumber = 32,
		Search = 50
	}

	public TextAsset EventManifest;

	private void Awake()
	{
		base.enabled = true;
	}

	private void Update()
	{
	}
}
