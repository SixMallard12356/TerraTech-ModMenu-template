#define UNITY_EDITOR
using System;
using System.IO;
using DevCommands;
using UnityEngine;

public class CommunityEngagementEvent
{
	private struct IntervalCheck
	{
		private float m_Interval;

		private Func<bool> m_Check;

		private float m_NextCheckTime;

		private bool m_LastResult;

		public IntervalCheck(Func<bool> check, float interval, bool defaultValue = false)
		{
			m_Check = check;
			m_Interval = interval;
			m_NextCheckTime = -1f;
			m_LastResult = defaultValue;
		}

		public bool Check()
		{
			float time = Time.time;
			if (time > m_NextCheckTime)
			{
				m_LastResult = m_Check();
				m_NextCheckTime = time + m_Interval;
			}
			return m_LastResult;
		}
	}

	private enum Section
	{
		None = 0,
		World = 1,
		Location = 2,
		Block = 4,
		Tech = 8,
		Message = 16,
		Complete = 32,
		All = 23
	}

	private bool m_Initialised;

	private Section m_LifetimeAchievedSections;

	private Section m_ActiveSections;

	private CommunityEventData m_EventData;

	private IntervalCheck m_PosCheck;

	private EncounterIdentifier m_Handshake_EncounterIdentifier;

	private const string kEngagementSectionsAchievedKey = "ComEvent_SectionsAchieved";

	public static CommunityEngagementEvent inst { get; private set; }

	public CommunityEngagementEvent(CommunityEventData eventData)
	{
		m_EventData = eventData;
	}

	public void TrySendMessage(int message)
	{
		if (m_EventData.CheckMessage(message.ToString()))
		{
			MarkSectionReached(Section.Message);
		}
	}

	public void Update()
	{
		if (!m_Initialised && Singleton.Manager<ManGameMode>.inst != null)
		{
			d.Assert(inst == null, "Already have singleton CommunityEngagementEvent present! Only one allowed");
			inst = this;
			Initialise();
			m_Initialised = true;
		}
		if (!HasReachedSection(Section.World))
		{
			return;
		}
		bool flag = m_PosCheck.Check();
		bool flag2 = HasReachedSection(Section.Location);
		if (flag != flag2)
		{
			if (flag)
			{
				MarkSectionReached(Section.Location);
			}
			else
			{
				ClearSectionReached(Section.Location);
			}
		}
	}

	private void Initialise()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(OnModeSetup);
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		Singleton.Manager<ManMods>.inst.ModSessionLoadCompleteEvent.Subscribe(OnModsChanged);
		m_LifetimeAchievedSections = (Section)PlayerPrefs.GetInt("ComEvent_SectionsAchieved", 0);
		m_ActiveSections = Section.None;
		m_PosCheck = new IntervalCheck(() => m_EventData.CheckPos(Singleton.playerPos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld), 5f);
		m_Handshake_EncounterIdentifier = new EncounterIdentifier(FactionSubTypes.BF, 1, "manual", "arg_handshake_encounter");
	}

	private void OnModeSetup(Mode startedMode)
	{
		m_ActiveSections = Section.None;
	}

	private void OnModeStart(Mode startedMode)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Creative && m_EventData.CheckBiome(Singleton.Manager<ManWorld>.inst.BiomeChoice) && m_EventData.CheckWorld(Singleton.Manager<ManWorld>.inst.SeedString))
		{
			MarkSectionReached(Section.World);
		}
	}

	private void OnModsChanged()
	{
		ClearSectionReached(Section.Block);
		foreach (ModContainer item in Singleton.Manager<ManMods>.inst.IterateActiveMods())
		{
			if (!m_EventData.CheckBlockContainerId(item.ModID))
			{
				continue;
			}
			string name = new DirectoryInfo(item.AssetBundlePath).Parent.Name;
			if (!m_EventData.CheckBlockSourceId(name))
			{
				continue;
			}
			foreach (ModdedBlockDefinition block in item.Contents.m_Blocks)
			{
				string blockId = ModUtils.CreateCompoundId(item.ModID, block.name);
				if (m_EventData.CheckBlock(blockId))
				{
					GameObject gameObject = block.m_PhysicalPrefab.gameObject;
					if (gameObject.GetComponent<ModuleCommunityEvent>() == null)
					{
						gameObject.transform.DeletePool();
						gameObject.AddComponent<ModuleCommunityEvent>();
						gameObject.transform.CreatePool(4);
					}
					MarkSectionReached(Section.Block);
					break;
				}
			}
		}
	}

	private bool HasReachedSection(Section section)
	{
		return (m_ActiveSections & section) == section;
	}

	private void MarkSectionReached(Section section)
	{
		d.Assert(section != Section.None);
		m_ActiveSections |= section;
		bool num = (m_LifetimeAchievedSections & section) == 0;
		m_LifetimeAchievedSections |= section;
		if (num)
		{
			PlayerPrefs.SetInt("ComEvent_SectionsAchieved", (int)m_LifetimeAchievedSections);
			PlayerPrefs.Save();
		}
		if (section == Section.Message && HasReachedSection(Section.All))
		{
			if (!HasReachedSection(Section.Complete))
			{
				MarkSectionReached(Section.Complete);
			}
			SpawnEncounter();
		}
	}

	private void ClearSectionReached(Section section)
	{
		d.Assert(section != Section.None);
		m_ActiveSections &= ~section;
	}

	[DevCommand(Name = "CE.ResetEngagement", Access = Access.DevCheat, Users = User.Any)]
	public static void ResetEngagement()
	{
		inst.m_LifetimeAchievedSections = Section.None;
		PlayerPrefs.SetInt("ComEvent_SectionsAchieved", 0);
		PlayerPrefs.Save();
	}

	private void SpawnEncounter()
	{
		if (!Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(m_Handshake_EncounterIdentifier))
		{
			EncounterToSpawn encounterToSpawn = new EncounterToSpawn(m_EventData.Handshake_EncounterData, m_Handshake_EncounterIdentifier);
			if (Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounterToSpawn.m_EncounterDef, errorIfNotFound: false) == null)
			{
				Singleton.Manager<ManEncounter>.inst.RegisterEncounter(encounterToSpawn.m_EncounterDef, encounterToSpawn.m_EncounterData);
			}
			encounterToSpawn.m_Position = WorldPosition.FromScenePosition(Singleton.playerPos);
			Singleton.Manager<ManEncounter>.inst.SpawnEncounter(encounterToSpawn);
			Singleton.Manager<ManEncounter>.inst.SetEncountersVisibleInHud(visible: true);
			Singleton.Manager<ManEncounter>.inst.SetUpdateEnabled(ManEncounter.UpdateChannel.GameMode, enabled: true);
			Singleton.Manager<ManEncounter>.inst.EncounterCompletedEvent.Subscribe(OnEncounterComplete);
		}
	}

	private void OnEncounterComplete(Encounter encounter, bool succeeded)
	{
		if (encounter.EncounterDef == m_Handshake_EncounterIdentifier)
		{
			Singleton.Manager<ManEncounter>.inst.ResetEncounters();
			Singleton.Manager<ManEncounter>.inst.SetUpdateEnabled(ManEncounter.UpdateChannel.GameMode, enabled: false);
		}
		else
		{
			d.LogErrorFormat("Other encounter completed that was unexpected in comms event! {0}", encounter.EncounterDef);
		}
	}
}
