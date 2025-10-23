#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ModuleBlockStateController : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public int categoriesControlled;

		[Obsolete("Use categoriesControlled instead. This field only remains for legacy load purposes.")]
		[JsonProperty]
		private List<ModuleControlCategory> categories
		{
			set
			{
				value.ForEach(delegate(ModuleControlCategory cat)
				{
					categoriesControlled = Bitfield.Add(categoriesControlled, (int)cat);
				});
			}
		}
	}

	[SerializeField]
	private List<ModuleControlCategory> m_SelectableCategories;

	[SerializeField]
	private bool m_FixedCategories;

	[EnumArray(typeof(ModuleControlCategory))]
	[SerializeField]
	private GameObject[] m_OnState;

	[SerializeField]
	[EnumArray(typeof(ModuleControlCategory))]
	private GameObject[] m_OffState;

	[SerializeField]
	[EnumArray(typeof(ModuleControlCategory))]
	private GameObject[] m_NotControlledState;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private List<ModuleControlCategory> m_CategoriesControlled;

	private List<ModuleControlCategory> m_DisabledCategories = new List<ModuleControlCategory>();

	private ModuleAnimator m_Animator;

	private bool m_IsCircuitControlled;

	private static readonly AnimatorBool[] k_CategorySelected = new AnimatorBool[10]
	{
		new AnimatorBool("___NotImplemented___"),
		new AnimatorBool("AntiGravCategorySelected"),
		new AnimatorBool("ShieldCategorySelected"),
		new AnimatorBool("RegenCategorySelected"),
		new AnimatorBool("HoverCategorySelected"),
		new AnimatorBool("GyroTrimCategorySelected"),
		new AnimatorBool("MagnetCategorySelected"),
		new AnimatorBool("AnchorCategorySelected"),
		new AnimatorBool("StabiliserCategorySelected"),
		new AnimatorBool("PassiveBrakeCategorySelected")
	};

	private static readonly AnimatorBool[] k_CategoryOn = new AnimatorBool[10]
	{
		new AnimatorBool("___NotImplemented___"),
		new AnimatorBool("AntiGravCategoryOn"),
		new AnimatorBool("ShieldCategoryOn"),
		new AnimatorBool("RegenCategoryOn"),
		new AnimatorBool("HoverCategoryOn"),
		new AnimatorBool("GyroTrimCategoryOn"),
		new AnimatorBool("MagnetCategoryOn"),
		new AnimatorBool("AnchorCategoryOn"),
		new AnimatorBool("StabiliserCategoryOn"),
		new AnimatorBool("PassiveBrakeCategoryOn")
	};

	public IEnumerable<ModuleControlCategory> SelectableCategories => m_SelectableCategories;

	public IEnumerable<ModuleControlCategory> ControlledCategories => m_CategoriesControlled;

	public bool HasFixedCategories => m_FixedCategories;

	private bool CircuitControlled => m_IsCircuitControlled;

	public void SetControlledCategoriesActive(bool active, bool fromCircuit = false)
	{
		foreach (ModuleControlCategory item in m_CategoriesControlled)
		{
			base.block.tank.BlockStateController.RequestSetCategoryActive(item, active, ignoreRegistration: false, fromCircuit);
		}
	}

	public bool CanSwitch(ModuleControlCategory cat)
	{
		if (!m_FixedCategories)
		{
			return m_SelectableCategories.Contains(cat);
		}
		return false;
	}

	public void RequestModifyControlledCategory(ModuleControlCategory cat, bool controlled)
	{
		if (!CanSwitch(cat))
		{
			d.Log($"ModuleBlockStateController on {this}: cannot switch category {cat}");
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ModifyControlledCategories, new ModifyControlledCategoriesMessage
			{
				m_BlockPoolID = base.block.blockPoolID,
				m_Category = cat,
				m_Controlled = controlled
			}, base.block.tank.netTech.netId);
		}
		else
		{
			ModifyControlledCategory(cat, controlled);
		}
	}

	public void ModifyControlledCategory(ModuleControlCategory cat, bool controlled)
	{
		if (!CanSwitch(cat))
		{
			d.Log($"ModuleBlockStateController on {this}: cannot switch category {cat}");
			return;
		}
		if (controlled)
		{
			base.block.tank.BlockStateController.RegisterCategoryControl(cat);
		}
		Internal_SetCategoryControlled(cat, controlled);
		if (!controlled)
		{
			base.block.tank.BlockStateController.DeregisterCategoryControl(cat);
		}
		UpdateVisualState(cat);
	}

	private void UpdateAllStateVisuals()
	{
		d.AssertFormat(base.gameObject.activeInHierarchy, "Gameobject wasn't active when updating ModuleBlockStateController visuals on block {0}. That shouldn't be allowed/done..", base.block);
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		foreach (ModuleControlCategory selectableCategory in m_SelectableCategories)
		{
			UpdateVisualState(selectableCategory);
		}
	}

	private void UpdateVisualState(ModuleControlCategory controllerModuleType)
	{
		d.AssertFormat(base.gameObject.activeInHierarchy, "Gameobject wasn't active when updating ModuleBlockStateController visuals on block {0}. That shouldn't be allowed/done..", base.block);
		d.AssertFormat(m_SelectableCategories.Contains(controllerModuleType), "Tried to change category ({0}) that is not selectable! Block {1}", controllerModuleType, base.block);
		bool flag = m_CategoriesControlled.Contains(controllerModuleType);
		bool flag2 = IsCategoryActive(controllerModuleType);
		SetVisualObjectActive(m_OnState, (int)controllerModuleType, flag && flag2);
		SetVisualObjectActive(m_OffState, (int)controllerModuleType, flag && !flag2);
		SetVisualObjectActive(m_NotControlledState, (int)controllerModuleType, !flag);
		if (m_Animator != null)
		{
			if ((int)controllerModuleType < k_CategorySelected.Length && (int)controllerModuleType < k_CategoryOn.Length)
			{
				m_Animator.Set(k_CategorySelected[(int)controllerModuleType], flag);
				m_Animator.Set(k_CategoryOn[(int)controllerModuleType], flag2);
			}
			else
			{
				d.LogError("ModuleControlCategory enum likely had a new value added, that was not added to k_CategorySelected or k_CategoryOn");
			}
		}
		static void SetVisualObjectActive(GameObject[] objectList, int index, bool active)
		{
			if (objectList != null && index < objectList.Length)
			{
				GameObject gameObject = objectList[index];
				if (gameObject != null)
				{
					gameObject.SetActive(active);
				}
			}
		}
	}

	private bool IsCategoryActive(ModuleControlCategory moduleType)
	{
		if (base.block.IsAttached)
		{
			return base.block.tank.BlockStateController.IsCategoryActive(moduleType);
		}
		return !m_DisabledCategories.Contains(moduleType);
	}

	private void ResetCategoriesSelected()
	{
		if (m_FixedCategories)
		{
			return;
		}
		Internal_ResetCategoriesControlled();
		foreach (ModuleControlCategory selectableCategory in m_SelectableCategories)
		{
			Internal_SetCategoryControlled(selectableCategory, controlled: true, checkForDupes: false);
		}
	}

	private void CacheDisabledCategories()
	{
		d.Assert(base.block.IsAttached, "CacheDisabledCategories called on block that was not attached!", this);
		m_DisabledCategories.Clear();
		foreach (ModuleControlCategory item in m_CategoriesControlled)
		{
			if (!base.block.tank.BlockStateController.IsCategoryActive(item))
			{
				m_DisabledCategories.Add(item);
			}
		}
	}

	private void ApplyCachedDisabledCategories()
	{
		d.Assert(base.block.IsAttached, "ApplyCachedDisabledCategories called on block that was not attached!", this);
		foreach (ModuleControlCategory disabledCategory in m_DisabledCategories)
		{
			if (!base.block.tank.BlockStateController.HasRegisteredCategoryControl(disabledCategory))
			{
				base.block.tank.BlockStateController.RequestSetCategoryActive(disabledCategory, setActive: false, ignoreRegistration: true);
			}
		}
		m_DisabledCategories.Clear();
	}

	private void Internal_ResetCategoriesControlled()
	{
		if (base.block.IsAttached && CircuitControlled)
		{
			foreach (ModuleControlCategory item in m_CategoriesControlled)
			{
				base.block.tank.BlockStateController.UnregisterCircuitControl(item, this);
			}
		}
		m_CategoriesControlled.Clear();
	}

	private void Internal_SetCategoryControlled(ModuleControlCategory category, bool controlled, bool checkForDupes = true)
	{
		if (controlled)
		{
			if (!checkForDupes || !m_CategoriesControlled.Contains(category))
			{
				m_CategoriesControlled.Add(category);
				if (m_IsCircuitControlled)
				{
					base.block.tank.BlockStateController.RegisterCircuitControl(category, this);
					bool setActive = base.block.CircuitReceiver.CurrentChargeData > 0;
					base.block.tank.BlockStateController.RequestSetCategoryActive(category, setActive, ignoreRegistration: false, fromCircuit: true);
				}
			}
		}
		else if (!checkForDupes || m_CategoriesControlled.Contains(category))
		{
			m_CategoriesControlled.Remove(category);
			if (m_IsCircuitControlled)
			{
				base.block.tank.BlockStateController.UnregisterCircuitControl(category, this);
			}
		}
	}

	private void SetIsCircuitControlled(bool isCircuitControlled)
	{
		if (isCircuitControlled == m_IsCircuitControlled)
		{
			return;
		}
		m_IsCircuitControlled = isCircuitControlled;
		if (isCircuitControlled)
		{
			foreach (ModuleControlCategory item in m_CategoriesControlled)
			{
				base.block.tank.BlockStateController.RegisterCircuitControl(item, this);
			}
			return;
		}
		foreach (ModuleControlCategory item2 in m_CategoriesControlled)
		{
			base.block.tank.BlockStateController.UnregisterCircuitControl(item2, this);
		}
	}

	private void UpdateActiveStateFromCircuit()
	{
		if (m_IsCircuitControlled)
		{
			bool active = base.block.CircuitReceiver.CurrentChargeData > 0;
			SetControlledCategoriesActive(active, fromCircuit: true);
		}
	}

	private void OnAttached()
	{
		ApplyCachedDisabledCategories();
		foreach (ModuleControlCategory item in m_CategoriesControlled)
		{
			base.block.tank.BlockStateController.RegisterCategoryControl(item);
		}
		base.block.tank.BlockStateController.CategoryActiveChangedEvent.Subscribe(OnCategoryStateChanged);
		UpdateAllStateVisuals();
	}

	private void OnDetaching()
	{
		SetIsCircuitControlled(isCircuitControlled: false);
		CacheDisabledCategories();
		base.block.tank.BlockStateController.CategoryActiveChangedEvent.Unsubscribe(OnCategoryStateChanged);
		foreach (ModuleControlCategory item in m_CategoriesControlled)
		{
			base.block.tank.BlockStateController.DeregisterCategoryControl(item);
		}
	}

	private void OnCategoryStateChanged(ModuleControlCategory controllerModuleType, bool active)
	{
		if (m_CategoriesControlled.Contains(controllerModuleType))
		{
			UpdateVisualState(controllerModuleType);
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (m_FixedCategories)
		{
			return;
		}
		if (saving)
		{
			int num = 0;
			foreach (ModuleControlCategory item in m_CategoriesControlled)
			{
				num = Bitfield.Add(num, (int)item);
			}
			SerialData serialData = new SerialData();
			serialData.categoriesControlled = num;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			foreach (ModuleControlCategory selectableCategory in m_SelectableCategories)
			{
				bool controlled = Bitfield.Contains(serialData2.categoriesControlled, (int)selectableCategory);
				Internal_SetCategoryControlled(selectableCategory, controlled);
			}
		}
		UpdateAllStateVisuals();
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			if (!m_FixedCategories)
			{
				string value = string.Join(",", m_CategoriesControlled.Select(delegate(ModuleControlCategory c)
				{
					int num = (int)c;
					return num.ToString();
				}));
				context.Store(GetType(), "blockControllerCategoriesControlled", value);
			}
			if (base.block.IsAttached)
			{
				CacheDisabledCategories();
			}
			string value2 = string.Join(",", m_DisabledCategories.Select(delegate(ModuleControlCategory c)
			{
				int num = (int)c;
				return num.ToString();
			}));
			context.Store(GetType(), "catsSwitchedOff", value2);
		}
		else
		{
			if (!m_FixedCategories && (!TryRetrieveEnumListT<ModuleControlCategory>(context, GetType(), "blockControllerCategoriesControlled", m_CategoriesControlled) || !m_CategoriesControlled.All((ModuleControlCategory c) => CanSwitch(c))))
			{
				ResetCategoriesSelected();
			}
			TryRetrieveEnumListT<ModuleControlCategory>(context, GetType(), "catsSwitchedOff", m_DisabledCategories);
			UpdateAllStateVisuals();
		}
		static bool TryRetrieveEnumListT<T>(TankPreset.BlockSpec blockSpec, Type type, string key, List<T> enumList) where T : struct
		{
			bool result = true;
			string text = blockSpec.Retrieve(type, key);
			enumList.Clear();
			if (text.Length > 0)
			{
				string[] array = text?.Split(',');
				if (array != null)
				{
					string[] array2 = array;
					foreach (string text2 in array2)
					{
						if (Enum.TryParse<T>(text2, out var result2))
						{
							enumList.Add(result2);
						}
						else
						{
							d.LogErrorFormat("Failed to parse {1} string: {0}", text2, key);
							result = false;
						}
					}
				}
			}
			return result;
		}
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		UpdateActiveStateFromCircuit();
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		d.Assert(base.block.IsAttached || !m_IsCircuitControlled, "Should already be set to not circuit controlled post-detach!");
		SetIsCircuitControlled(state);
		if (state)
		{
			UpdateActiveStateFromCircuit();
		}
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		m_Animator = GetComponent<ModuleAnimator>();
		m_CategoriesControlled = new List<ModuleControlCategory>(m_SelectableCategories);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		ResetCategoriesSelected();
		m_DisabledCategories.Clear();
		foreach (ModuleControlCategory item in m_CategoriesControlled)
		{
			if (!TechBlockStateController.IsCategoryActiveByDefault(item))
			{
				m_DisabledCategories.Add(item);
			}
		}
		UpdateAllStateVisuals();
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleBlockStateController;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		if (m_FixedCategories)
		{
			return;
		}
		foreach (ModuleControlCategory selectableCategory in m_SelectableCategories)
		{
			writer.Write(m_CategoriesControlled.Contains(selectableCategory));
		}
	}

	public void OnDeserialize(NetworkReader reader)
	{
		if (m_FixedCategories)
		{
			return;
		}
		foreach (ModuleControlCategory selectableCategory in m_SelectableCategories)
		{
			bool controlled = reader.ReadBoolean();
			Internal_SetCategoryControlled(selectableCategory, controlled);
		}
		UpdateAllStateVisuals();
	}
}
