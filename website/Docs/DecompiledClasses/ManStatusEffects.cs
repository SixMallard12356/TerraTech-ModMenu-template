#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManStatusEffects : Singleton.Manager<ManStatusEffects>
{
	[EnumArray(typeof(StatusEffect.EffectTypes))]
	[SerializeField]
	protected StatusEffect[] m_StatusEffectsLookup;

	private Dictionary<int, StatusEffect.State> m_ActiveEffectsHashLookup = new Dictionary<int, StatusEffect.State>();

	private List<StatusEffect.State> m_ActiveEffectIterator = new List<StatusEffect.State>();

	private StatusEffect.EffectTypes[] m_AllStatusEffectTypes;

	private static List<StatusEffect.State> _s_ActiveStatesMarkedEnded = new List<StatusEffect.State>();

	public void TryApplyUnnetworkedEffectOnVisible(StatusEffect.EffectTypes type, Visible visible, Visible source)
	{
		d.Assert(type != StatusEffect.EffectTypes.Unassigned, "Attempted to apply an unspecified status effect! No bueno!!!");
		StatusEffect effect = GetEffect(type);
		d.AssertFormat(effect != null, "Effect of [{0}] not assigned in ManStatusEffects! Fix this!", type);
		if (effect.CanApplyEffectOnVisible(visible, source))
		{
			int iD = StatusEffect.State.GetID(visible, type);
			if (m_ActiveEffectsHashLookup.ContainsKey(iD))
			{
				effect.StackExistingEffect(m_ActiveEffectsHashLookup[iD]);
				return;
			}
			StatusEffect.State newEffect = effect.GetNewEffect(visible, source);
			AddToActiveEffects(newEffect);
			newEffect.Start();
		}
	}

	public void TryRemoveEffectFromVisible(StatusEffect.EffectTypes type, Visible visible)
	{
		int iD = StatusEffect.State.GetID(visible, type);
		if (m_ActiveEffectsHashLookup.ContainsKey(iD))
		{
			StatusEffect.State state = m_ActiveEffectsHashLookup[iD];
			state.End();
			RemoveFromActiveEffects(state);
		}
	}

	public void ClearAllEffectsOnVisible(Visible visible)
	{
		StatusEffect.EffectTypes[] allStatusEffectTypes = m_AllStatusEffectTypes;
		foreach (StatusEffect.EffectTypes type in allStatusEffectTypes)
		{
			TryRemoveEffectFromVisible(type, visible);
		}
	}

	private StatusEffect GetEffect(StatusEffect.EffectTypes type)
	{
		return m_StatusEffectsLookup[(int)type];
	}

	private void AddToActiveEffects(StatusEffect.State effect)
	{
		m_ActiveEffectsHashLookup.Add(effect.GetID(), effect);
		m_ActiveEffectIterator.Add(effect);
	}

	private void RemoveFromActiveEffects(StatusEffect.State effect)
	{
		m_ActiveEffectsHashLookup.Remove(effect.GetID());
		m_ActiveEffectIterator.Remove(effect);
	}

	private void InitAllTypesArray()
	{
		Array values = Enum.GetValues(typeof(StatusEffect.EffectTypes));
		m_AllStatusEffectTypes = new StatusEffect.EffectTypes[values.Length - 1];
		for (int i = 0; i < m_AllStatusEffectTypes.Length; i++)
		{
			m_AllStatusEffectTypes[i] = (StatusEffect.EffectTypes)values.GetValue(i + 1);
		}
	}

	private void Awake()
	{
		InitAllTypesArray();
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < m_ActiveEffectIterator.Count; i++)
		{
			m_ActiveEffectIterator[i].FixedUpdate();
			if (m_ActiveEffectIterator[i].HasEnded)
			{
				_s_ActiveStatesMarkedEnded.Add(m_ActiveEffectIterator[i]);
			}
		}
		for (int j = 0; j < _s_ActiveStatesMarkedEnded.Count; j++)
		{
			RemoveFromActiveEffects(_s_ActiveStatesMarkedEnded[j]);
		}
		_s_ActiveStatesMarkedEnded.Clear();
	}
}
