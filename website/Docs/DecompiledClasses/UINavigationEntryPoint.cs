using UnityEngine;

public class UINavigationEntryPoint : MonoBehaviour
{
	[SerializeField]
	private bool m_JoypadModeOnly;

	private void OnEnable()
	{
		if (!m_JoypadModeOnly || Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		}
	}

	private void OnDisable()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
	}
}
