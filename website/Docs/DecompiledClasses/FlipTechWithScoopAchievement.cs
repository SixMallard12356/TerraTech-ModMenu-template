using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/FlipTechWithScoopAchievement")]
public class FlipTechWithScoopAchievement : AchievementObject
{
	private struct ScoopedTechData
	{
		public Tank tech;

		public Vector3 initialUpVectorLocal;

		public float scoopStartTime;
	}

	[SerializeField]
	private float m_FlipAngleThresholdDegrees = 110f;

	[SerializeField]
	private float m_ScoopTimeout = 4f;

	private List<ModuleScoop> m_ScoopsOnPlayerTech = new List<ModuleScoop>(8);

	private List<ScoopedTechData> m_TechInScoop = new List<ScoopedTechData>(4);

	private float m_FlipAngleThresholdCosine;

	private const float kScoopReentryTimeout = 1.5f;

	public override void Initialise()
	{
		base.Initialise();
		m_FlipAngleThresholdCosine = Mathf.Cos(m_FlipAngleThresholdDegrees * ((float)Math.PI / 180f));
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockAttached);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockDetached);
		m_TechInScoop.Clear();
		ClearScoopsOnPlayerTech();
	}

	public override void Update()
	{
		base.Update();
		if (!IsActive() || !(Singleton.playerTank != null))
		{
			return;
		}
		float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		for (int num = m_TechInScoop.Count - 1; num >= 0; num--)
		{
			ScoopedTechData scoopedTechData = m_TechInScoop[num];
			if (currentModeRunningTime > scoopedTechData.scoopStartTime + m_ScoopTimeout || !scoopedTechData.tech.gameObject.activeSelf)
			{
				m_TechInScoop.RemoveAt(num);
			}
			else if (Vector3.Dot(scoopedTechData.tech.trans.TransformDirection(scoopedTechData.initialUpVectorLocal), Vector3.up) < m_FlipAngleThresholdCosine)
			{
				CompleteAchievement();
				ClearScoopsOnPlayerTech();
				m_TechInScoop.Clear();
				Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
				Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnBlockAttached);
				Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Unsubscribe(OnBlockDetached);
				break;
			}
		}
	}

	private void AddScoop(ModuleScoop scoop)
	{
		m_ScoopsOnPlayerTech.Add(scoop);
		scoop.ContainmentTriggerEvent.Subscribe(OnScoopContainmentTrigger);
	}

	private void RemoveScoop(ModuleScoop scoop)
	{
		m_ScoopsOnPlayerTech.Remove(scoop);
		scoop.ContainmentTriggerEvent.Unsubscribe(OnScoopContainmentTrigger);
	}

	private void ClearScoopsOnPlayerTech()
	{
		for (int num = m_ScoopsOnPlayerTech.Count - 1; num >= 0; num--)
		{
			if (m_ScoopsOnPlayerTech[num] != null)
			{
				m_ScoopsOnPlayerTech[num].ContainmentTriggerEvent.Unsubscribe(OnScoopContainmentTrigger);
			}
		}
		m_ScoopsOnPlayerTech.Clear();
	}

	private void OnPlayerTankChanged(Tank tank, bool playerFocus)
	{
		if (IsActive() && playerFocus)
		{
			ClearScoopsOnPlayerTech();
			BlockManager.BlockIterator<ModuleScoop>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleScoop>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleScoop current = enumerator.Current;
				AddScoop(current);
			}
		}
	}

	private void OnBlockAttached(Tank tech, TankBlock block)
	{
		if (IsActive() && tech == Singleton.playerTank)
		{
			ModuleScoop component = block.GetComponent<ModuleScoop>();
			if (component != null)
			{
				AddScoop(component);
			}
		}
	}

	private void OnBlockDetached(Tank tech, TankBlock block)
	{
		if (IsActive() && tech == Singleton.playerTank)
		{
			ModuleScoop component = block.GetComponent<ModuleScoop>();
			if (component != null)
			{
				RemoveScoop(component);
			}
		}
	}

	private void OnScoopContainmentTrigger(ModuleScoop scoop, TriggerCatcher.Interaction eventType, Collider collider)
	{
		if (!IsActive() || !scoop.IsLifting || eventType == TriggerCatcher.Interaction.Exit)
		{
			return;
		}
		Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(collider);
		if (!(visible != null))
		{
			return;
		}
		Tank tech = ((visible.block != null) ? visible.block.tank : visible.tank);
		if (!(tech != null))
		{
			return;
		}
		int num = m_TechInScoop.FindIndex((ScoopedTechData x) => x.tech.visible.ID == tech.visible.ID);
		float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		if (num == -1 || currentModeRunningTime > m_TechInScoop[num].scoopStartTime + 1.5f)
		{
			if (num >= 0)
			{
				m_TechInScoop.RemoveAt(num);
			}
			Vector3 initialUpVectorLocal = tech.trans.InverseTransformDirection(Vector3.up);
			m_TechInScoop.Add(new ScoopedTechData
			{
				tech = tech,
				initialUpVectorLocal = initialUpVectorLocal,
				scoopStartTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime()
			});
		}
	}
}
