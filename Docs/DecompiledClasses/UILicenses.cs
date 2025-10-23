#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class UILicenses : UIHUDElement
{
	[SerializeField]
	private GameObject m_LicensePrefab;

	[SerializeField]
	private GameObject m_LicensePrefabSwitchVariant;

	private UICorpLicense[] m_CorpLicenses;

	public override void Init(object context)
	{
		base.Init(context);
		int count = EnumValuesIterator<FactionSubTypes>.Count;
		m_CorpLicenses = new UICorpLicense[count];
		Dictionary<FactionSubTypes, FactionLicense> dictionary = context as Dictionary<FactionSubTypes, FactionLicense>;
		d.Assert(m_LicensePrefabSwitchVariant != null, "Missing Switch variant on UILicenses");
		GameObject gameObject = ((SKU.SwitchUI && (bool)m_LicensePrefabSwitchVariant) ? m_LicensePrefabSwitchVariant : m_LicensePrefab);
		d.Assert(gameObject.GetComponent<UICorpLicense>(), "Missing UICorpLicense on prefab " + gameObject.name, gameObject);
		foreach (FactionSubTypes key in dictionary.Keys)
		{
			if (Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Contains(key))
			{
				int num = (int)key;
				m_CorpLicenses[num] = gameObject.GetComponent<UICorpLicense>().Spawn(base.transform);
				m_CorpLicenses[num].transform.localPosition = m_CorpLicenses[num].transform.localPosition.SetZ(0f);
				m_CorpLicenses[num].transform.localScale = Vector3.one;
				m_CorpLicenses[num].Init(dictionary[key]);
			}
		}
	}

	public override void DeInit(object context)
	{
		if (m_CorpLicenses != null)
		{
			for (int i = 0; i < m_CorpLicenses.Length; i++)
			{
				if ((bool)m_CorpLicenses[i])
				{
					m_CorpLicenses[i].transform.SetParent(null, worldPositionStays: false);
					m_CorpLicenses[i].Recycle();
					m_CorpLicenses[i] = null;
				}
			}
		}
		base.DeInit(context);
	}

	public void ShowCorpLicense(FactionSubTypes corp)
	{
		m_CorpLicenses[(int)corp].Show(show: true);
	}
}
