#define UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UISumoPlayerInfo : MonoBehaviour
{
	public Image m_PlayerIcon;

	public Text m_PlayerName;

	public Text m_VehicleName;

	public Text m_VehicleLevel;

	public Image m_VehicleImage;

	public void SetPlayerInfo(string profileImageURL, string twitterHandle, string vehicleName, Texture2D vehicleImage, int vehicleLevel)
	{
		m_PlayerName.text = "@" + twitterHandle;
		m_VehicleName.text = vehicleName;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 23);
		m_VehicleLevel.text = string.Format(localisedString, vehicleLevel);
		if (m_VehicleImage != null && vehicleImage != null)
		{
			m_VehicleImage.sprite = Sprite.Create(vehicleImage, new Rect(0f, 0f, vehicleImage.width, vehicleImage.height), new Vector2(0.5f, 0.5f));
		}
		if (!string.IsNullOrEmpty(profileImageURL))
		{
			string url = profileImageURL.Replace("_normal", "");
			Singleton.Manager<ManUI>.inst.StartCoroutine(LoadImage(url));
		}
	}

	private IEnumerator LoadImage(string url)
	{
		if (m_PlayerIcon != null)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError || !www.error.NullOrEmpty())
			{
				d.Log("Could not load sumo image: " + www.error);
				yield break;
			}
			Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
			m_PlayerIcon.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		}
	}
}
