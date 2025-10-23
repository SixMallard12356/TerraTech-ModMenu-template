#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleAudioProvider : Module
{
	[SerializeField]
	private List<AudioProvider> m_LoopedAdsrSFX;

	private Dictionary<TechAudio.SFXType, AudioProvider> m_SFXLookup = new Dictionary<TechAudio.SFXType, AudioProvider>(new TechAudio.SFXTypeComparer());

	public void SetNoteOn(TechAudio.SFXType sfxType, bool isNoteOn)
	{
		SetNoteOn(sfxType, isNoteOn, FMODEvent.FMODParams.empty);
	}

	public void SetNoteOn(TechAudio.SFXType sfxType, bool isNoteOn, FMODEvent.FMODParams additionalParams)
	{
		if (sfxType != TechAudio.SFXType.Default)
		{
			if (m_SFXLookup.TryGetValue(sfxType, out var value))
			{
				value.NoteOn = isNoteOn;
				value.AdditionalParams = additionalParams;
				return;
			}
			d.LogError(string.Concat("ModuleAudioProvider.SetNoteOn: No SFXType (", sfxType, ") configured on ", base.block.name));
		}
	}

	private void OnAttached()
	{
		foreach (AudioProvider item in m_LoopedAdsrSFX)
		{
			base.block.tank.TechAudio.AddModule(item);
		}
	}

	private void OnDetaching()
	{
		foreach (AudioProvider item in m_LoopedAdsrSFX)
		{
			base.block.tank.TechAudio.RemoveModule(item);
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		foreach (AudioProvider item in m_LoopedAdsrSFX)
		{
			m_SFXLookup.Add(item.SFXType, item);
			item.SetParent(this);
		}
	}

	private void OnUpdate()
	{
		foreach (AudioProvider item in m_LoopedAdsrSFX)
		{
			item.Update();
		}
	}
}
