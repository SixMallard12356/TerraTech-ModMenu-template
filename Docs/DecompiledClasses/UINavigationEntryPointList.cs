#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UINavigationEntryPointList : MonoBehaviour
{
	[SerializeField]
	private bool m_JoypadModeOnly;

	[SerializeField]
	private bool m_SelectTarget;

	[SerializeField]
	private Selectable[] m_EntryPointPriorityOrder;

	private GameObject m_AddedEntryTarget;

	private void OnEnable()
	{
		if (m_JoypadModeOnly && !Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			return;
		}
		Selectable selectable = null;
		if (m_EntryPointPriorityOrder != null)
		{
			for (int i = 0; i < m_EntryPointPriorityOrder.Length; i++)
			{
				selectable = m_EntryPointPriorityOrder[i];
				if (selectable.interactable && selectable.gameObject.activeInHierarchy)
				{
					m_AddedEntryTarget = selectable.gameObject;
					break;
				}
			}
		}
		if (m_AddedEntryTarget != null)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_AddedEntryTarget);
			if (m_SelectTarget)
			{
				selectable.Select();
				selectable.OnSelect(null);
			}
		}
		else
		{
			d.LogErrorFormat("UINavigationEntryPointList - Failed to find valid target to set as selected game object on {0}!", base.name);
		}
	}

	private void OnDisable()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_AddedEntryTarget);
		m_AddedEntryTarget = null;
	}
}
