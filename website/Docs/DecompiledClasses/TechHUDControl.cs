#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechHUDControl : TechComponent
{
	private struct TechHudElement
	{
		public Module provider;

		public ManHUD.HUDElementType hudType;

		public TechHudElement(Module hudElementProvider, ManHUD.HUDElementType hudElementType)
		{
			provider = hudElementProvider;
			hudType = hudElementType;
		}
	}

	private Dictionary<Module, ManHUD.HUDElementType> m_HudElements = new Dictionary<Module, ManHUD.HUDElementType>();

	private List<TechHudElement> m_HudElementsToAdd = new List<TechHudElement>();

	private List<Module> m_HudElementsToRemove = new List<Module>();

	public void AddHudElement(Module hudElementProvider, ManHUD.HUDElementType hudElementType)
	{
		m_HudElementsToAdd.Add(new TechHudElement(hudElementProvider, hudElementType));
	}

	public void RemoveHudElement(Module hudElementProvider)
	{
		m_HudElementsToRemove.Add(hudElementProvider);
	}

	private void Add(TechHudElement hudElement)
	{
		m_HudElements[hudElement.provider] = hudElement.hudType;
		if (base.Tech.IsPlayer)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(hudElement.hudType, hudElement.provider);
		}
	}

	private void Remove(Module hudElementModule)
	{
		if (base.Tech.IsPlayer)
		{
			ManHUD.HUDElementType value;
			bool num = m_HudElements.TryGetValue(hudElementModule, out value);
			d.Assert(num, "");
			if (num)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(value, hudElementModule);
			}
		}
		m_HudElements.Remove(hudElementModule);
	}

	private void OnPlayerTechChanged(Tank tank, bool setToPlayer)
	{
		if (!base.Tech.IsPlayer)
		{
			return;
		}
		if (setToPlayer)
		{
			foreach (KeyValuePair<Module, ManHUD.HUDElementType> hudElement in m_HudElements)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(hudElement.Value, hudElement.Key);
			}
			return;
		}
		foreach (KeyValuePair<Module, ManHUD.HUDElementType> hudElement2 in m_HudElements)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(hudElement2.Value, hudElement2.Key);
		}
	}

	private void RemoveAll()
	{
		if (base.Tech.IsPlayer)
		{
			foreach (KeyValuePair<Module, ManHUD.HUDElementType> hudElement in m_HudElements)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(hudElement.Value, hudElement.Key);
			}
		}
		m_HudElements.Clear();
	}

	private void OnUpdate()
	{
		for (int i = 0; i < m_HudElementsToAdd.Count; i++)
		{
			Add(m_HudElementsToAdd[i]);
		}
		m_HudElementsToAdd.Clear();
		for (int j = 0; j < m_HudElementsToRemove.Count; j++)
		{
			Remove(m_HudElementsToRemove[j]);
		}
		m_HudElementsToRemove.Clear();
	}

	private void OnPool()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTechChanged);
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
	}

	private void OnDepool()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTechChanged);
	}

	private void OnSpawn()
	{
		m_HudElements.Clear();
		m_HudElementsToAdd.Clear();
		m_HudElementsToRemove.Clear();
	}

	private void OnRecycle()
	{
		RemoveAll();
		m_HudElements.Clear();
		m_HudElementsToAdd.Clear();
		m_HudElementsToRemove.Clear();
	}
}
