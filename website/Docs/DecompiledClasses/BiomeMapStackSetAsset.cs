#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class BiomeMapStackSetAsset : ScriptableObject
{
	[Serializable]
	public struct Entry
	{
		[Tooltip("This is stored in save files, so DO NOT change it once it's been shipped to users")]
		public string m_UniqueID;

		public LocalisedString m_DisplayName;

		public BiomeMapStackAsset m_Stack;

		public bool m_HideFromUser;
	}

	[SerializeField]
	private List<Entry> m_BiomeStacks;

	public List<Entry> Entries => m_BiomeStacks;

	public BiomeMapStack GetStack(string biomeID)
	{
		if (biomeID.NullOrEmpty())
		{
			return m_BiomeStacks[0].m_Stack.MapStack;
		}
		foreach (Entry biomeStack in m_BiomeStacks)
		{
			if (biomeStack.m_UniqueID == biomeID)
			{
				return biomeStack.m_Stack.MapStack;
			}
		}
		return null;
	}

	public BiomeMap SelectCompatibleBiomeMap(string biomeID)
	{
		BiomeMapStack stack = GetStack(biomeID);
		if (stack != null)
		{
			return stack.SelectCompatibleBiomeMap();
		}
		d.LogError($"Failed to find biome type {biomeID} in set {base.name}. Reverting to default {m_BiomeStacks[0].m_Stack.name}", this);
		return m_BiomeStacks[0].m_Stack.MapStack.SelectCompatibleBiomeMap();
	}
}
