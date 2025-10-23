using UnityEngine;
using UnityEngine.UI;

public class UIOptionsSound : UIOptions
{
	[SerializeField]
	private Slider m_MasterVolume;

	[SerializeField]
	private Slider m_SFXVolume;

	[SerializeField]
	private Slider m_MusicVolume;

	private bool m_Initialised;

	private EventNoParams ChangesMadeEventToCall;

	public override void Setup(EventNoParams onChangeEvent)
	{
		ChangesMadeEventToCall = onChangeEvent;
		Init();
		m_MasterVolume.value = Singleton.Manager<ManMusic>.inst.GetMasterMixerVolume();
		m_MusicVolume.value = Singleton.Manager<ManMusic>.inst.GetMusicMixerVolume();
		m_SFXVolume.value = Singleton.Manager<ManMusic>.inst.GetSFXMixerVolume();
	}

	public override void SaveSettings()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			ManProfile.SoundSettings soundSettings = currentUser.m_SoundSettings;
			soundSettings.m_MasterVolume = Singleton.Manager<ManMusic>.inst.GetMasterMixerVolume();
			soundSettings.m_MusicVolume = Singleton.Manager<ManMusic>.inst.GetMusicMixerVolume();
			soundSettings.m_SFXVolume = Singleton.Manager<ManMusic>.inst.GetSFXMixerVolume();
			Singleton.Manager<ManSFX>.inst.SFXVolume = soundSettings.m_SFXVolume;
		}
	}

	public override UIScreenOptions.SaveFailureType CanSave()
	{
		return UIScreenOptions.SaveFailureType.None;
	}

	public override void ClearSettings()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			ManProfile.SoundSettings soundSettings = currentUser.m_SoundSettings;
			Singleton.Manager<ManMusic>.inst.SetMasterMixerVolume(soundSettings.m_MasterVolume);
			Singleton.Manager<ManMusic>.inst.SetMusicMixerVolume(soundSettings.m_MusicVolume);
			Singleton.Manager<ManMusic>.inst.SetSFXMixerVolume(soundSettings.m_SFXVolume);
			Singleton.Manager<ManSFX>.inst.SFXVolume = soundSettings.m_SFXVolume;
		}
	}

	public override void ResetSettings()
	{
	}

	public override void OnCloseScreen()
	{
	}

	private void Init()
	{
		if (!m_Initialised)
		{
			m_MasterVolume.onValueChanged.AddListener(OnMasterVolumeChange);
			m_MasterVolume.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_SFXVolume.onValueChanged.AddListener(OnSFXVolumeChange);
			m_SFXVolume.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_MusicVolume.onValueChanged.AddListener(OnMusicVolumeChange);
			m_MusicVolume.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_Initialised = true;
		}
	}

	private void OnMasterVolumeChange(float value)
	{
		Singleton.Manager<ManMusic>.inst.SetMasterMixerVolume(value);
	}

	private void OnMusicVolumeChange(float value)
	{
		Singleton.Manager<ManMusic>.inst.SetMusicMixerVolume(value);
	}

	private void OnSFXVolumeChange(float value)
	{
		Singleton.Manager<ManMusic>.inst.SetSFXMixerVolume(value);
		Singleton.Manager<ManSFX>.inst.SFXVolume = value;
	}
}
