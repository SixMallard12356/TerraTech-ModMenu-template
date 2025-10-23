#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TechBlockStateController : TechComponent, INetworkedTechComponent
{
	[Serializable]
	public new class SerialData : SerialData<SerialData>
	{
		public int activeModulesBitFlags;

		public int numCategoriesStored;
	}

	public Event<ModuleControlCategory, bool> CategoryActiveChangedEvent;

	private Bitfield<ModuleControlCategory> m_ActiveCategories = new Bitfield<ModuleControlCategory>();

	private int[] m_RegisteredCategorySwitches;

	private List<ModuleGyro> m_GyroModules = new List<ModuleGyro>();

	private List<ModuleHover> m_HoverModules = new List<ModuleHover>();

	private List<ModuleHoverControl> m_HoverControllerModules = new List<ModuleHoverControl>();

	private List<ModuleBalloon> m_BalloonModules = new List<ModuleBalloon>();

	private List<ModuleBalloonControl> m_BalloonControllerModules = new List<ModuleBalloonControl>();

	private Dictionary<ModuleControlCategory, HashSet<ModuleBlockStateController>> m_CircuitBasedCategoryControllers = new Dictionary<ModuleControlCategory, HashSet<ModuleBlockStateController>>(s_ControlCategoryEqualityComparer);

	private static readonly ModuleControlCategoryComparer s_ControlCategoryEqualityComparer = new ModuleControlCategoryComparer();

	private static readonly Dictionary<ModuleControlCategory, Func<Tank, bool, bool>> k_CanSetCategoryFuncs = new Dictionary<ModuleControlCategory, Func<Tank, bool, bool>>(s_ControlCategoryEqualityComparer) { 
	{
		ModuleControlCategory.Anchor,
		(Tank tech, bool setOn) => !setOn || tech.Anchors.NumPossibleAnchors > 0
	} };

	private static readonly Dictionary<ModuleControlCategory, bool> k_DefaultActive = new Dictionary<ModuleControlCategory, bool>(s_ControlCategoryEqualityComparer)
	{
		{
			ModuleControlCategory.NotImplemented,
			true
		},
		{
			ModuleControlCategory.AntiGravity,
			true
		},
		{
			ModuleControlCategory.Shield,
			true
		},
		{
			ModuleControlCategory.Regen,
			true
		},
		{
			ModuleControlCategory.Hover,
			true
		},
		{
			ModuleControlCategory.GyroTrim,
			true
		},
		{
			ModuleControlCategory.Magnet,
			true
		},
		{
			ModuleControlCategory.Anchor,
			false
		},
		{
			ModuleControlCategory.Stabiliser,
			true
		},
		{
			ModuleControlCategory.PassiveBrake,
			true
		}
	};

	public float GyroTrim
	{
		get
		{
			if (m_GyroModules.Count > 0)
			{
				return m_GyroModules[0].Trim;
			}
			return 0f;
		}
		set
		{
			foreach (ModuleGyro gyroModule in m_GyroModules)
			{
				gyroModule.Trim = value;
			}
		}
	}

	public float HoverPower
	{
		get
		{
			if (m_HoverModules.Count > 0)
			{
				return m_HoverModules[0].HoverPower;
			}
			return 0.5f;
		}
		set
		{
			foreach (ModuleHover hoverModule in m_HoverModules)
			{
				hoverModule.HoverPower = value;
			}
		}
	}

	public float BalloonFloatinessSetting01 { get; private set; } = float.NegativeInfinity;

	public int GyroTrimControllerCount => m_RegisteredCategorySwitches[5];

	public int HoverControllerCount => m_HoverControllerModules.Count;

	public int BalloonControllerCount => m_BalloonControllerModules.Count;

	public void SendHoverAndGyroToServer()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UpdateHoverAndGyro, new UpdateHoverAndGyroMessage
			{
				m_Hover = HoverPower,
				m_Gyro = GyroTrim
			}, base.Tech.netTech.netId);
		}
	}

	public bool RequestSetCategoryActive(ModuleControlCategory category, bool setActive, bool ignoreRegistration = false, bool fromCircuit = false)
	{
		if ((!ignoreRegistration && !HasRegisteredCategoryControl(category)) || (k_CanSetCategoryFuncs.TryGetValue(category, out var value) && !value(base.Tech, setActive)))
		{
			return false;
		}
		if (!fromCircuit && base.Tech.netTech.IsNotNull())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetCategoryActive, new SetCategoryActiveMessage
			{
				m_ModuleCategory = category,
				m_Active = setActive
			}, base.Tech.netTech.netId);
			return true;
		}
		return SetCategoryActive(category, setActive, ignoreRegistration, fromCircuit);
	}

	public bool SetCategoryActive(ModuleControlCategory category, bool active, bool ignoreRegistration = false, bool fromCircuit = false)
	{
		if (!ignoreRegistration && !HasRegisteredCategoryControl(category))
		{
			d.LogError($"No category control of type {category} was registered on tech {base.Tech}, yet it's being attempted to get toggled!", this);
			return false;
		}
		if (!fromCircuit && IsCategoryCircuitControlled(category))
		{
			return false;
		}
		if (IsCategoryActive(category) != active)
		{
			m_ActiveCategories.Set((int)category, active);
			CategoryActiveChangedEvent.Send(category, active);
		}
		return true;
	}

	public bool IsCategoryActive(ModuleControlCategory category)
	{
		return m_ActiveCategories.Contains((int)category);
	}

	public bool HasRegisteredCategoryControl(ModuleControlCategory category)
	{
		return m_RegisteredCategorySwitches[(int)category] > 0;
	}

	public void RegisterCategoryControl(ModuleControlCategory category)
	{
		m_RegisteredCategorySwitches[(int)category]++;
	}

	public void DeregisterCategoryControl(ModuleControlCategory category)
	{
		d.AssertFormat(HasRegisteredCategoryControl(category), this, "Missmatched category control registration! Deregistering {0} while none are registered.", category);
		m_RegisteredCategorySwitches[(int)category]--;
		if (m_RegisteredCategorySwitches[(int)category] == 0)
		{
			RequestSetCategoryActive(category, IsCategoryActiveByDefault(category), ignoreRegistration: true);
		}
	}

	public bool IsCategoryCircuitControlled(ModuleControlCategory category)
	{
		if (m_CircuitBasedCategoryControllers.TryGetValue(category, out var value))
		{
			return value.Count > 0;
		}
		return false;
	}

	public void AddGyro(ModuleGyro module)
	{
		m_GyroModules.Add(module);
	}

	public void RemoveGyro(ModuleGyro module)
	{
		m_GyroModules.Remove(module);
		if (GyroTrimControllerCount == 0)
		{
			GyroTrim = 0f;
		}
	}

	public void AddHover(ModuleHover module)
	{
		m_HoverModules.Add(module);
	}

	public void RemoveHover(ModuleHover module)
	{
		m_HoverModules.Remove(module);
	}

	public void AddHoverController(ModuleHoverControl module)
	{
		if (HoverControllerCount == 0)
		{
			HoverPower = module.HoverPower;
		}
		m_HoverControllerModules.Add(module);
	}

	public void RemoveHoverController(ModuleHoverControl module)
	{
		m_HoverControllerModules.Remove(module);
		if (HoverControllerCount == 0)
		{
			HoverPower = 0.5f;
		}
	}

	public void RegisterBalloon(ModuleBalloon balloon)
	{
		m_BalloonModules.Add(balloon);
	}

	public void DeregisterBalloon(ModuleBalloon balloon)
	{
		m_BalloonModules.Remove(balloon);
	}

	public void OnBalloonControllerSetFloatiness(ModuleBalloonControl balloonController, float floatiness01)
	{
		SetAndPropogateBalloonFloatinessSetting(balloonController, floatiness01);
	}

	public void SetAndPropogateBalloonFloatinessSetting(ModuleBalloonControl originController, float floatiness01)
	{
		BalloonFloatinessSetting01 = floatiness01;
		foreach (ModuleBalloonControl balloonControllerModule in m_BalloonControllerModules)
		{
			if (!(originController == balloonControllerModule))
			{
				balloonControllerModule.PropogateFloatinessSetting(floatiness01);
			}
		}
	}

	public void RegisterBalloonController(ModuleBalloonControl balloonController)
	{
		if (BalloonControllerCount == 0)
		{
			SetAndPropogateBalloonFloatinessSetting(balloonController, balloonController.FloatinessSetting);
		}
		else
		{
			balloonController.PropogateFloatinessSetting(BalloonFloatinessSetting01);
		}
		balloonController.FloatinessValueSetEvent.Subscribe(OnBalloonControllerSetFloatiness);
		m_BalloonControllerModules.Add(balloonController);
	}

	public void DeregisterBalloonController(ModuleBalloonControl balloonController)
	{
		m_BalloonControllerModules.Remove(balloonController);
		balloonController.FloatinessValueSetEvent.Unsubscribe(OnBalloonControllerSetFloatiness);
		if (BalloonControllerCount == 0)
		{
			SetAndPropogateBalloonFloatinessSetting(null, float.NegativeInfinity);
		}
	}

	public void RegisterCircuitControl(ModuleControlCategory category, ModuleBlockStateController module)
	{
		if (!m_CircuitBasedCategoryControllers.TryGetValue(category, out var value))
		{
			value = new HashSet<ModuleBlockStateController>();
			m_CircuitBasedCategoryControllers[category] = value;
		}
		d.AssertFormat(value.Add(module), "Duplicate adding CircuitControl for cat '{0}' module {1}!", category, module);
	}

	public void UnregisterCircuitControl(ModuleControlCategory category, ModuleBlockStateController module)
	{
		d.AssertFormat(m_CircuitBasedCategoryControllers.TryGetValue(category, out var value) && value.Remove(module), "Failed removal of CircuitControl for cat '{0}' module {1}! Either already removed, or never added!", category, module);
	}

	public static bool IsCategoryActiveByDefault(ModuleControlCategory category)
	{
		if (!k_DefaultActive.TryGetValue(category, out var value))
		{
			d.LogErrorFormat("IsActiveByDefault did not have default value for module control type {0}", category);
			return true;
		}
		return value;
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.activeModulesBitFlags = m_ActiveCategories.Field;
			serialData.numCategoriesStored = EnumIterator<ModuleControlCategory>.Count;
			serialData.Store(saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(saveState);
		if (serialData2 == null)
		{
			return;
		}
		int activeModulesBitFlags = serialData2.activeModulesBitFlags;
		bool flag = activeModulesBitFlags != 0;
		EnumValuesIterator<ModuleControlCategory> enumerator = EnumIterator<ModuleControlCategory>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleControlCategory current = enumerator.Current;
			if (current != ModuleControlCategory.Anchor)
			{
				bool active = ((!flag || (int)current >= serialData2.numCategoriesStored) ? IsCategoryActiveByDefault(current) : Bitfield.Contains(activeModulesBitFlags, (int)current));
				SetCategoryActive(current, active, ignoreRegistration: true);
			}
		}
	}

	private void OnAnchorStateChanged(bool isAnchored, bool isSkyAnchored)
	{
		SetCategoryActive(ModuleControlCategory.Anchor, isAnchored, ignoreRegistration: true, fromCircuit: true);
	}

	public NetworkedTechComponentID GetTechComponentID()
	{
		return NetworkedTechComponentID.TechBlockStateController;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(m_ActiveCategories.Field);
		writer.Write(GyroTrim);
		writer.Write(HoverPower);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		int flags = reader.ReadInt32();
		EnumValuesIterator<ModuleControlCategory> enumerator = EnumIterator<ModuleControlCategory>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleControlCategory current = enumerator.Current;
			bool active = Bitfield.Contains(flags, (int)current);
			SetCategoryActive(current, active, ignoreRegistration: true);
		}
		GyroTrim = reader.ReadSingle();
		HoverPower = reader.ReadSingle();
	}

	private void OnPool()
	{
		m_RegisteredCategorySwitches = new int[EnumValuesIterator<ModuleControlCategory>.Count];
		base.Tech.Anchors.AnchorEvent.Subscribe(OnAnchorStateChanged);
		m_RegisteredCategorySwitches[7] = 1;
	}

	private void OnSpawn()
	{
		for (int i = 0; i < EnumValuesIterator<ModuleControlCategory>.Count; i++)
		{
			m_ActiveCategories.Set(i, IsCategoryActiveByDefault((ModuleControlCategory)i));
		}
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
	}

	private void OnRecycle()
	{
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
	}
}
