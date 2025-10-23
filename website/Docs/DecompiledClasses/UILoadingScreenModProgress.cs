using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreenModProgress : MonoBehaviour
{
	public GameObject loadingBar;

	public Text loadingProgressText;

	public Image loadingProgressImage;

	private void Start()
	{
	}

	private void Update()
	{
		if (Singleton.Manager<ManMods>.inst.IsPollingWorkshop())
		{
			loadingBar.SetActive(value: true);
			loadingProgressText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 50);
			loadingProgressImage.fillAmount = 0f;
		}
		else if (Singleton.Manager<ManMods>.inst.HasPendingLoads())
		{
			string currentlyLoadingName = Singleton.Manager<ManMods>.inst.GetCurrentlyLoadingName();
			float currentlyLoadingProgress = Singleton.Manager<ManMods>.inst.GetCurrentlyLoadingProgress();
			loadingBar.SetActive(value: true);
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 52);
			loadingProgressText.text = string.Format(localisedString, currentlyLoadingName, currentlyLoadingProgress * 100f);
			loadingProgressImage.fillAmount = currentlyLoadingProgress;
		}
		else
		{
			loadingBar.SetActive(value: false);
		}
	}
}
