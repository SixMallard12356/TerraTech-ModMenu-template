#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public struct WeightedGroup<TEnum>
{
	private Dictionary<TEnum, float> m_Weights;

	private float m_WeightTotal;

	public int Count
	{
		get
		{
			if (m_Weights == null)
			{
				return 0;
			}
			return m_Weights.Count;
		}
	}

	public Dictionary<TEnum, float> Weights
	{
		set
		{
			m_Weights = value;
		}
	}

	public void SetWeight(TEnum index, float newWeight)
	{
		if (m_Weights == null)
		{
			m_Weights = new Dictionary<TEnum, float>();
		}
		float weight = GetWeight(index, errorIfNotFound: false);
		newWeight = Mathf.Max(0f, newWeight);
		if (newWeight == 0f)
		{
			m_Weights.Remove(index);
		}
		else
		{
			m_Weights[index] = newWeight;
		}
		m_WeightTotal += newWeight - weight;
	}

	public bool HasWeights()
	{
		if (m_Weights != null)
		{
			return m_Weights.Count > 0;
		}
		return false;
	}

	public float GetWeight(TEnum index, bool errorIfNotFound = true)
	{
		float value = 0f;
		if ((m_Weights == null || !m_Weights.TryGetValue(index, out value)) && errorIfNotFound)
		{
			d.LogError("WeightedGroup.GetWeight - Failed to get weight for value: " + index.ToString());
		}
		return value;
	}

	public TEnum GetRandom()
	{
		TEnum result = default(TEnum);
		d.Assert(m_Weights != null && m_Weights.Count > 0, "WeightedGroup.GetRandom - No elements in the list!");
		if (m_Weights != null && m_Weights.Count > 0)
		{
			float num = Random.Range(0f, m_WeightTotal);
			foreach (KeyValuePair<TEnum, float> weight in m_Weights)
			{
				if (num <= weight.Value)
				{
					result = weight.Key;
					return result;
				}
				num -= weight.Value;
			}
		}
		return result;
	}

	public void Clear()
	{
		if (m_Weights != null)
		{
			m_Weights.Clear();
		}
		m_WeightTotal = 0f;
	}
}
