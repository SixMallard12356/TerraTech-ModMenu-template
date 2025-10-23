#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[RequireComponent(typeof(ModuleItemHolder))]
[RequireComponent(typeof(ModuleAnimator))]
public class ModuleItemConsume : Module, ItemSearchHandler, ItemSearchConverter, ManPointer.OpenMenuEventConsumer, INetworkedModule, ModuleItemHolder.IStackDirection
{
	[Serializable]
	public struct CorporationDurationMultipliers
	{
		public FactionSubTypes Corporation;

		public float DurationMultiplier;

		public CorporationDurationMultipliers(FactionSubTypes Corporation, float DurationMultiplier)
		{
			this.Corporation = Corporation;
			this.DurationMultiplier = DurationMultiplier;
		}
	}

	[Serializable]
	public struct CorporationOutputEffeciency
	{
		public FactionSubTypes Corporation;

		public MinMaxFloat EfficiencyScale;

		public CorporationOutputEffeciency(FactionSubTypes Corporation, MinMaxFloat EfficiencyScale)
		{
			this.Corporation = Corporation;
			this.EfficiencyScale = EfficiencyScale;
		}
	}

	[Flags]
	public enum AnimationTriggers : byte
	{
		StartConsumingTrigger = 1,
		FinishConsumingTrigger = 2,
		StartOperatingTrigger = 4,
		FinishOperatingTrigger = 8,
		StartProducingTrigger = 0x10,
		FinishProducingTrigger = 0x20,
		StartBurningWasteTrigger = 0x40,
		FinishBurningWasteTrigger = 0x80
	}

	[Serializable]
	public struct Progress
	{
		public enum RecipeRequester
		{
			None,
			User,
			Block
		}

		public bool repeatRecipe;

		public List<ItemTypeInfo> inputsRemaining;

		public Bitfield<int> wantedItemsWarnings;

		public Stack<ItemTypeInfo> outputQueue;

		public float energySupplyRate;

		public float energySupplyRemaining;

		public TechEnergy.EnergyType energySupplyType;

		[JsonProperty]
		public RecipeTable.Recipe currentRecipe { get; private set; }

		[JsonProperty]
		public RecipeRequester requester { get; private set; }

		public static Progress CreateNew()
		{
			return new Progress
			{
				currentRecipe = null,
				requester = RecipeRequester.None,
				repeatRecipe = false,
				inputsRemaining = new List<ItemTypeInfo>(),
				wantedItemsWarnings = new Bitfield<int>(),
				outputQueue = new Stack<ItemTypeInfo>(),
				energySupplyRemaining = 0f,
				energySupplyRate = 0f,
				energySupplyType = TechEnergy.EnergyType.Electric
			};
		}

		[JsonConstructor]
		private Progress(bool __unusedBypassNoParameterlessConstructorLimitation)
		{
			currentRecipe = null;
			requester = RecipeRequester.None;
			repeatRecipe = false;
			inputsRemaining = new List<ItemTypeInfo>();
			wantedItemsWarnings = new Bitfield<int>();
			outputQueue = new Stack<ItemTypeInfo>();
			energySupplyRemaining = 0f;
			energySupplyRate = 0f;
			energySupplyType = TechEnergy.EnergyType.Electric;
		}

		public void ClearRecipe()
		{
			SetRecipe(null, RecipeRequester.None);
		}

		public void ClearOutputs()
		{
			outputQueue.Clear();
			energySupplyRate = 0f;
			energySupplyRemaining = 0f;
		}

		public void SetRecipe(RecipeTable.Recipe recipe, RecipeRequester requester)
		{
			currentRecipe = recipe;
			this.requester = requester;
			ResetRemainingInputs();
		}

		public bool CheckHasTakenInputs()
		{
			bool result = false;
			if (currentRecipe != null)
			{
				int num = 0;
				for (int i = 0; i < currentRecipe.m_InputItems.Length; i++)
				{
					num += currentRecipe.m_InputItems[i].m_Quantity;
				}
				result = inputsRemaining.Count < num;
			}
			return result;
		}

		public void ResetRemainingInputs()
		{
			inputsRemaining.Clear();
			wantedItemsWarnings.Clear();
			if (currentRecipe == null)
			{
				return;
			}
			for (int i = 0; i < currentRecipe.m_InputItems.Length; i++)
			{
				RecipeTable.Recipe.ItemSpec itemSpec = currentRecipe.m_InputItems[i];
				for (int j = 0; j < itemSpec.m_Quantity; j++)
				{
					inputsRemaining.Add(itemSpec.m_Item);
				}
			}
		}
	}

	private class RecipeIterator : IEnumerable<RecipeTable.Recipe>, IEnumerable
	{
		private List<RecipeTable.Recipe> recipeList;

		public RecipeIterator(List<RecipeTable.Recipe> recipes)
		{
			recipeList = recipes;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<RecipeTable.Recipe> GetEnumerator()
		{
			return recipeList.GetEnumerator();
		}
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public Progress consumeProgress;

		public int operatingBeatsDone;

		public int setAnimationTriggers;
	}

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Input;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Input2;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Input3;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Input4;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Consume;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Operate;

	[SerializeField]
	private ModuleItemHolder.StackHandle m_Output;

	[SerializeField]
	private bool m_NeedsToBeAnchored;

	[SerializeField]
	private float m_EnergyMultiplier = 1f;

	[SerializeField]
	private Transform m_PullArrowPrefab;

	[SerializeField]
	private Transform m_PushArrowPrefab;

	[SerializeField]
	[FormerlySerializedAs("m_LaunchesCraftingMenu")]
	private bool m_ManualControl;

	[SerializeField]
	private ManHUD.HUDElementType m_MenuType = ManHUD.HUDElementType.BlockRecipeSelect;

	[SerializeField]
	private FactionSubTypes m_CraftingFaction;

	[SerializeField]
	private bool m_KickOutItem;

	[SerializeField]
	private Transform m_KickLocator;

	[SerializeField]
	private float m_KickSpeed;

	[SerializeField]
	private Vector3 m_KickAngularVelocity;

	[SerializeField]
	private bool m_BreakBlockOutputDownToChunks;

	[SerializeField]
	private bool m_UseRecipeBuildTime;

	[SerializeField]
	[FormerlySerializedAs("m_DurationMultiplier")]
	private float m_GenericDurationMultiplier = 1f;

	[SerializeField]
	private CorporationDurationMultipliers[] m_CorpDurationMultipliers = new CorporationDurationMultipliers[0];

	[SerializeField]
	[Tooltip("The range of potential output % that determines how much of a recipes result will be rewarded")]
	private MinMaxFloat m_GenericOutputEfficiency = MinMaxFloat.One;

	[Tooltip("The range of potential output % that determines how much of a recipes result will be rewarded")]
	[SerializeField]
	private CorporationOutputEffeciency[] m_CorpOutputEfficiencies = new CorporationOutputEffeciency[0];

	[SerializeField]
	private bool m_ActivityBlocksInput;

	[SerializeField]
	private bool m_OperationBlocksConsume;

	[SerializeField]
	private bool m_AllowMultItemsOnInput;

	[SerializeField]
	private bool m_AcceptsRecipeDonges;

	[FormerlySerializedAs("m_SFXType")]
	[SerializeField]
	private TechAudio.SFXType m_OperatingSFXType;

	[SerializeField]
	private TechAudio.SFXType m_ConsumeSFXType;

	[SerializeField]
	private TechAudio.SFXType m_ProduceSFXType;

	[SerializeField]
	private float m_ProduceSFXDelay;

	[SerializeField]
	private bool m_AddSoldBlocksToShop;

	public EventNoParams OnRecycled;

	public Event<ModuleItemConsume> OnConsumerRecipesChanged;

	public Event<AnimationTriggers> AnimationTriggerFlagsChangedEvent;

	private static List<ModuleItemHolder.Stack> s_RequestExploreList = new List<ModuleItemHolder.Stack>();

	private ModuleItemHolder m_Holder;

	private ModuleItemHolderBeam m_HolderBeam;

	private ModuleItemPickup m_Pickup;

	private ModuleEnergy m_ModuleEnergy;

	private ItemListRequest m_ItemListRequest;

	private Dictionary<FactionSubTypes, CorporationDurationMultipliers> m_DurationMultipliersCorpLookup = new Dictionary<FactionSubTypes, CorporationDurationMultipliers>();

	private Dictionary<FactionSubTypes, CorporationOutputEffeciency> m_OutputEfficienciesCorpLookup = new Dictionary<FactionSubTypes, CorporationOutputEffeciency>();

	private ModuleItemHolder.Stack[] m_InputStacks;

	private int m_NextInputIndex;

	private ModuleRecipeProvider m_BaseRecipeProvider;

	private List<ModuleRecipeDongle> m_AttachedRecipeDongles = new List<ModuleRecipeDongle>();

	private bool m_TechDonglesDirty;

	private List<RecipeTable.Recipe> m_RecipeList = new List<RecipeTable.Recipe>();

	private Dictionary<int, int> m_RecipeDuplicateCount = new Dictionary<int, int>();

	private RecipeIterator m_AllRecipes;

	private Dictionary<RecipeManager.RecipeDefinition, RecipeTable.Recipe> m_RecipeLookup = new Dictionary<RecipeManager.RecipeDefinition, RecipeTable.Recipe>();

	private HashSet<ItemTypeInfo> m_RecipeInputs;

	private Dictionary<ItemTypeInfo, RecipeTable.Recipe> m_RecipeOutputs;

	private bool m_RefreshBaseRecipes;

	private bool m_CanHonourRequests;

	private Progress m_ConsumeProgress;

	private int m_FirstOperatingHeartbeat;

	private int m_LastRequestReceivedHeartbeat;

	private int m_LastBuildRequestHeartbeat;

	private bool m_Purging;

	private AnimationTriggers m_NextHeartbeatTriggers;

	private AnimationTriggers m_CurrentlyActiveTriggers;

	private Dictionary<int, int> m_TriggerCounters = new Dictionary<int, int>();

	private static ModuleItemConsume s_MenuController = null;

	private WorldObjectOverlay m_Overlay;

	private ModuleAnimator m_Animator;

	private ModuleAudioProvider m_AudioProvider;

	private List<ItemTypeInfo> m_WantedItems = new List<ItemTypeInfo>();

	private List<ItemTypeInfo> m_ConsumedInputs = new List<ItemTypeInfo>();

	private AnimatorTrigger[] m_AnimationTriggers = new AnimatorTrigger[8]
	{
		new AnimatorTrigger("StartConsuming"),
		new AnimatorTrigger("FinishConsuming"),
		new AnimatorTrigger("StartOperating"),
		new AnimatorTrigger("FinishOperating"),
		new AnimatorTrigger("StartProducing"),
		new AnimatorTrigger("FinishProducing"),
		new AnimatorTrigger("StartBurningWaste"),
		new AnimatorTrigger("FinishBurningWaste")
	};

	private AnimatorBool m_ReadyBool = new AnimatorBool("Ready");

	private ModuleItemHolder m_OperateItemInterceptedBy;

	private Vector3 m_OperateItemInterceptAxis;

	private float m_OperateItemInterceptPos;

	private float m_OperateItemInterceptedPrevPos;

	private bool m_HasDirtyNetState;

	private NetworkedProperty<ItemConsumeProgressMessage> m_SyncedRecipeProgress;

	private NetworkedProperty<SetCraftingRecipeRepeatingMessage> m_SyncedRecipeRepeating;

	private NetworkedProperty<IntParamBlockMessage> m_SyncedConsumeAnimations;

	private bool m_NetClientIsOperating;

	private static HashSet<FactionSubTypes> _s_RecipeFactionsCache = new HashSet<FactionSubTypes>();

	private List<ItemTypeInfo> s_RequestedItems = new List<ItemTypeInfo>();

	private HashSet<ItemTypeInfo> s_MissingItems = new HashSet<ItemTypeInfo>();

	public RecipeTable.Recipe Recipe => m_ConsumeProgress.currentRecipe;

	public bool RecipeRepeating
	{
		get
		{
			return m_ConsumeProgress.repeatRecipe;
		}
		set
		{
			m_ConsumeProgress.repeatRecipe = value;
			m_SyncedRecipeRepeating.Data.m_RecipeRepeating = value;
		}
	}

	public List<ItemTypeInfo> InputsRemaining => m_ConsumeProgress.inputsRemaining;

	public Bitfield<int> WantedItemWarningBitfield => m_ConsumeProgress.wantedItemsWarnings;

	public bool IsOperating
	{
		get
		{
			if (!ManNetwork.IsHost)
			{
				return m_NetClientIsOperating;
			}
			return OperatingBeatsLeft > 0;
		}
	}

	public bool IsProducingItem
	{
		get
		{
			if (m_Output.stack != null && !m_Output.stack.IsEmpty)
			{
				return m_Output.stack.ReceivedThisHeartbeat;
			}
			return false;
		}
	}

	public bool DisableClosingCraftingUIWhenTooFar { get; set; }

	public bool CanHonourRequests => m_CanHonourRequests;

	public FactionSubTypes CraftingFaction => m_CraftingFaction;

	public IEnumerable<RecipeTable.Recipe> AllRecipes => m_AllRecipes;

	private int CurRecipeBeats => Math.Max((int)(((m_ConsumeProgress.currentRecipe != null) ? (m_ConsumeProgress.currentRecipe.m_BuildTimeSeconds * GetDurationMultiplierForCurrentRecipe() / Globals.inst.m_TankHolderHeartbeatInterval) : 0f) + 0.5f), 1);

	private int OperatingBeatsLeft
	{
		get
		{
			int num = ((!m_UseRecipeBuildTime) ? 1 : CurRecipeBeats);
			if (!(base.block.tank != null))
			{
				return -1;
			}
			return m_FirstOperatingHeartbeat + num - base.block.tank.Holders.HeartbeatCount;
		}
	}

	public bool HasWantedItems => m_WantedItems.Count > 0;

	public List<ItemTypeInfo> WantedItems => m_WantedItems;

	private bool OneRecipeAtATime => m_ManualControl;

	public void RequestBeginCraftingRecipe(RecipeTable.Recipe recipe)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetCraftingRecipe, new SetCraftingRecipeMessage
			{
				m_BlockPoolID = base.block.blockPoolID,
				m_HasRecipe = true,
				m_RecipeDef = (RecipeManager.RecipeDefinition)recipe
			}, base.block.tank.netTech.netId);
		}
		else
		{
			BeginCraftingRecipe(recipe);
		}
	}

	public void RequestCancelRecipe()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetCraftingRecipe, new SetCraftingRecipeMessage
			{
				m_BlockPoolID = base.block.blockPoolID,
				m_HasRecipe = false
			}, base.block.tank.netTech.netId);
		}
		else
		{
			CancelRecipe();
		}
	}

	public void RequestSetRecipeRepeating(bool repeat)
	{
		if (RecipeRepeating != repeat)
		{
			RecipeRepeating = repeat;
			m_SyncedRecipeRepeating.Sync();
		}
	}

	public void BeginCraftingRecipe(RecipeTable.Recipe recipe)
	{
		if (m_ConsumeProgress.currentRecipe != recipe)
		{
			CancelRecipe();
			m_ConsumeProgress.SetRecipe(recipe, Progress.RecipeRequester.User);
		}
	}

	public void CancelRecipe()
	{
		m_Purging = m_Purging || CheckHasItemsInSystem();
		m_ConsumeProgress.ClearRecipe();
		m_ConsumeProgress.ClearOutputs();
	}

	private bool CanShowMenu()
	{
		if (base.block.IsInteractible && m_ManualControl)
		{
			return CheckAnchoredCondition();
		}
		return false;
	}

	public bool CanOpenMenu(bool isRadial)
	{
		if (!isRadial && CanShowMenu())
		{
			return !base.block.tank.IsEnemy();
		}
		return false;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowNonRadialMenu && CanShowMenu() && (s_MenuController != this || !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(m_MenuType)))
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(m_MenuType, this);
			s_MenuController = this;
			return true;
		}
		return false;
	}

	public MinMaxFloat GetOutputEfficiencyForCurrentRecipe()
	{
		return GetOutputEfficiency(m_ConsumeProgress.currentRecipe);
	}

	public MinMaxFloat GetOutputEfficiency(RecipeTable.Recipe recipe)
	{
		MinMaxFloat result = MinMaxFloat.Infinity;
		RecipeTable.Recipe.ItemSpec[] inputItems = recipe.m_InputItems;
		for (int i = 0; i < inputItems.Length; i++)
		{
			ItemTypeInfo item = inputItems[i].m_Item;
			if (item.ObjectType == ObjectTypes.Block)
			{
				_s_RecipeFactionsCache.Add(Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)item.ItemType));
			}
		}
		if (_s_RecipeFactionsCache.Count > 0)
		{
			foreach (FactionSubTypes item2 in _s_RecipeFactionsCache)
			{
				MinMaxFloat outputEfficiency = GetOutputEfficiency(item2);
				if (outputEfficiency.Mean < result.Mean)
				{
					result = outputEfficiency;
				}
			}
		}
		else
		{
			result = GetOutputEfficiency(FactionSubTypes.NULL);
		}
		_s_RecipeFactionsCache.Clear();
		return result;
	}

	public MinMaxFloat GetOutputEfficiency(FactionSubTypes faction)
	{
		if (!m_OutputEfficienciesCorpLookup.ContainsKey(faction))
		{
			return m_GenericOutputEfficiency;
		}
		return m_OutputEfficienciesCorpLookup[faction].EfficiencyScale;
	}

	public float GetDurationMultiplierForCurrentRecipe()
	{
		return GetDurationMultiplier(m_ConsumeProgress.currentRecipe);
	}

	public float GetDurationMultiplier(RecipeTable.Recipe recipe)
	{
		float num = -1f;
		RecipeTable.Recipe.ItemSpec[] inputItems = recipe.m_InputItems;
		for (int i = 0; i < inputItems.Length; i++)
		{
			ItemTypeInfo item = inputItems[i].m_Item;
			if (item.ObjectType == ObjectTypes.Block)
			{
				_s_RecipeFactionsCache.Add(Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)item.ItemType));
			}
		}
		if (_s_RecipeFactionsCache.Count > 0)
		{
			foreach (FactionSubTypes item2 in _s_RecipeFactionsCache)
			{
				float durationMultiplier = GetDurationMultiplier(item2);
				if (durationMultiplier > num)
				{
					num = durationMultiplier;
				}
			}
		}
		else
		{
			num = GetDurationMultiplierAgnostic();
		}
		_s_RecipeFactionsCache.Clear();
		return num;
	}

	public float GetDurationMultiplierAgnostic()
	{
		return GetDurationMultiplier(FactionSubTypes.NULL);
	}

	public float GetDurationMultiplier(BlockTypes blockType)
	{
		return GetDurationMultiplier(Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType));
	}

	public float GetDurationMultiplier(FactionSubTypes faction)
	{
		if (!m_DurationMultipliersCorpLookup.ContainsKey(faction))
		{
			return m_GenericDurationMultiplier;
		}
		return m_DurationMultipliersCorpLookup[faction].DurationMultiplier;
	}

	private void ResetState()
	{
		m_ConsumeProgress.ClearRecipe();
		m_ConsumeProgress.ClearOutputs();
		m_WantedItems.Clear();
		for (int i = 0; i < m_InputStacks.Length; i++)
		{
			DropAllStackItems(m_InputStacks[i]);
		}
		ClearOutStack(m_Consume.stack);
		ClearOutStack(m_Operate.stack);
		DropAllStackItems(m_Output.stack);
		m_LastRequestReceivedHeartbeat = -1;
		m_LastBuildRequestHeartbeat = -1;
		m_FirstOperatingHeartbeat = -1;
		m_Purging = false;
		if (m_Animator.gameObject.activeInHierarchy)
		{
			m_Animator.ResetAllTriggers();
			m_Animator.SetSpawnedTrigger();
		}
		m_CurrentlyActiveTriggers = ~(AnimationTriggers.StartConsumingTrigger | AnimationTriggers.FinishConsumingTrigger | AnimationTriggers.StartOperatingTrigger | AnimationTriggers.FinishOperatingTrigger | AnimationTriggers.StartProducingTrigger | AnimationTriggers.FinishProducingTrigger | AnimationTriggers.StartBurningWasteTrigger | AnimationTriggers.FinishBurningWasteTrigger);
		m_TriggerCounters.Clear();
		m_SyncedConsumeAnimations.Data.value = 0;
		m_NextHeartbeatTriggers = ~(AnimationTriggers.StartConsumingTrigger | AnimationTriggers.FinishConsumingTrigger | AnimationTriggers.StartOperatingTrigger | AnimationTriggers.FinishOperatingTrigger | AnimationTriggers.StartProducingTrigger | AnimationTriggers.FinishProducingTrigger | AnimationTriggers.StartBurningWasteTrigger | AnimationTriggers.FinishBurningWasteTrigger);
		m_NetClientIsOperating = false;
		m_NextInputIndex = 0;
	}

	private void DestroyItem(Visible item)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			item.ServerDestroy();
		}
		else
		{
			item.trans.Recycle();
		}
	}

	private void SetAnimationTrigger(AnimationTriggers triggers, bool fromNetwork = false)
	{
		if (triggers == ~(AnimationTriggers.StartConsumingTrigger | AnimationTriggers.FinishConsumingTrigger | AnimationTriggers.StartOperatingTrigger | AnimationTriggers.FinishOperatingTrigger | AnimationTriggers.StartProducingTrigger | AnimationTriggers.FinishProducingTrigger | AnimationTriggers.StartBurningWasteTrigger | AnimationTriggers.FinishBurningWasteTrigger))
		{
			return;
		}
		for (int i = 0; i < m_AnimationTriggers.Length; i++)
		{
			AnimationTriggers animationTriggers = (AnimationTriggers)(1 << i);
			if ((triggers & animationTriggers) != 0)
			{
				m_Animator.Set(m_AnimationTriggers[i]);
				bool flag = i % 2 == 0;
				AnimationTriggers key = (AnimationTriggers)(1 << (flag ? i : (i - 1)));
				if (!m_TriggerCounters.TryGetValue((int)key, out var value))
				{
					value = 0;
				}
				value = (flag ? (value + 1) : (value - 1));
				m_TriggerCounters[(int)key] = value;
				if (value > 0)
				{
					m_CurrentlyActiveTriggers |= (AnimationTriggers)(byte)(1 << i);
				}
				else
				{
					m_CurrentlyActiveTriggers &= (AnimationTriggers)(byte)(~(byte)(1 << i - 1));
				}
			}
		}
		AnimationTriggerFlagsChangedEvent.Send(triggers);
		if (!fromNetwork && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			if (AnimationTriggerFlagsChangedEvent.HasSubscribers())
			{
				UpdateNetworkedState(triggerSync: true);
			}
			m_SyncedConsumeAnimations.Data.value |= (int)triggers;
			m_SyncedConsumeAnimations.Sync();
		}
	}

	private void ClearOutStack(ModuleItemHolder.Stack stack)
	{
		if (stack != null)
		{
			while (!stack.IsEmpty)
			{
				Visible firstItem = stack.FirstItem;
				bool notifyRelease = true;
				firstItem.SetHolder(null, notifyRelease);
				DestroyItem(firstItem);
			}
		}
	}

	private void DropAllStackItems(ModuleItemHolder.Stack stack)
	{
		if (stack != null)
		{
			while (!stack.IsEmpty)
			{
				bool notifyRelease = true;
				stack.FirstItem.SetHolder(null, notifyRelease);
			}
		}
	}

	private void UpdateItemRequests()
	{
		if (!ThisConsumerMakingAnyItemRequests())
		{
			return;
		}
		d.Assert(m_InputStacks.Length == 1, "Item requests only work for 1 input stack");
		if (m_InputStacks.Length != 1)
		{
			return;
		}
		ModuleItemHolder.Stack stack = m_InputStacks[0];
		s_RequestExploreList.Clear();
		for (int i = 0; i < stack.connectedNeighbourStacks.Length; i++)
		{
			ModuleItemHolder.Stack stack2 = stack.connectedNeighbourStacks[i];
			if (stack2 != null && stack2.myHolder != m_Holder)
			{
				s_RequestExploreList.Add(stack2);
			}
		}
		s_RequestedItems.AddRange(m_ConsumeProgress.inputsRemaining);
		if (!stack.IsEmpty)
		{
			s_RequestedItems.Remove(stack.FirstItem.m_ItemType);
		}
		m_ItemListRequest.LookForItems(s_RequestExploreList, s_RequestedItems, s_MissingItems);
		m_ConsumeProgress.wantedItemsWarnings.Clear();
		for (int j = 0; j < m_ConsumeProgress.currentRecipe.m_InputItems.Length; j++)
		{
			if (s_MissingItems.Contains(m_ConsumeProgress.currentRecipe.m_InputItems[j].m_Item))
			{
				m_ConsumeProgress.wantedItemsWarnings.Set(j, enabled: true);
			}
		}
		s_RequestedItems.Clear();
		s_MissingItems.Clear();
	}

	private void GetOutputItems(RecipeTable.Recipe.ItemSpec itemSpecToDesconstruct, ref List<ItemTypeInfo> constituentChunks)
	{
		if (!m_BreakBlockOutputDownToChunks || itemSpecToDesconstruct.m_Item.ObjectType != ObjectTypes.Block)
		{
			for (int i = 0; i < itemSpecToDesconstruct.m_Quantity; i++)
			{
				constituentChunks.Add(itemSpecToDesconstruct.m_Item);
			}
			return;
		}
		foreach (RecipeTable.Recipe allRecipe in m_AllRecipes)
		{
			if (allRecipe.m_OutputType != RecipeTable.Recipe.OutputType.Items || !allRecipe.InputsContain(itemSpecToDesconstruct.m_Item) || allRecipe.m_InputItems.Length != 1)
			{
				continue;
			}
			for (int j = 0; j < itemSpecToDesconstruct.m_Quantity; j++)
			{
				RecipeTable.Recipe.ItemSpec[] outputItems = allRecipe.m_OutputItems;
				foreach (RecipeTable.Recipe.ItemSpec itemSpecToDesconstruct2 in outputItems)
				{
					GetOutputItems(itemSpecToDesconstruct2, ref constituentChunks);
				}
			}
			break;
		}
	}

	private void InitRecipeOutput()
	{
		switch (m_ConsumeProgress.currentRecipe.m_OutputType)
		{
		case RecipeTable.Recipe.OutputType.Money:
		{
			m_OperateItemInterceptedBy = null;
			m_OperateItemInterceptAxis = base.transform.TransformVector(Vector3.up);
			if (m_Operate.stack != null)
			{
				Visible firstItem = m_Operate.stack.FirstItem;
				if (Physics.Raycast(firstItem.trans.position, m_OperateItemInterceptAxis, out var hitInfo, 23f, Globals.inst.layerDeliveryBlocker.mask, QueryTriggerInteraction.Collide))
				{
					d.Assert(m_Operate.stack.NumItems == 1);
					d.Assert(m_OperateItemInterceptedBy == null);
					m_OperateItemInterceptedBy = hitInfo.collider.GetComponentInParent<ModuleItemHolder>();
					m_OperateItemInterceptPos = Vector3.Dot(m_OperateItemInterceptAxis, hitInfo.point);
					m_OperateItemInterceptedPrevPos = Vector3.Dot(m_OperateItemInterceptAxis, firstItem.trans.position);
				}
			}
			if (!(m_OperateItemInterceptedBy == null) || !(base.block.tank != null) || base.block.tank.IsEnemy(0))
			{
				break;
			}
			RecipeTable.Recipe.ItemSpec itemSpec = m_ConsumeProgress.currentRecipe.m_InputItems[0];
			bool num3 = itemSpec.m_Item.ObjectType == ObjectTypes.Block;
			int num4 = m_ConsumeProgress.currentRecipe.m_MoneyOutput;
			if (num3)
			{
				num4 = Singleton.Manager<RecipeManager>.inst.ConvertBlockBuyToSellPrice(num4);
				if (m_AddSoldBlocksToShop)
				{
					BlockTypes itemType = (BlockTypes)itemSpec.m_Item.ItemType;
					BlockManager.BlockIterator<ModuleShopInventory>.Enumerator enumerator2 = base.block.tank.blockman.IterateBlockComponents<ModuleShopInventory>().GetEnumerator();
					if (enumerator2.MoveNext())
					{
						enumerator2.Current.GetInventory().HostAddItem(itemType);
					}
				}
			}
			d.Assert(m_ConsumeProgress.currentRecipe.m_InputItems.Count() == 1 && itemSpec.m_Quantity == 1, "ModuleItemConsume.cs - Outputting money from a recipe, currently only support single input items!");
			Singleton.Manager<ManStats>.inst.ItemSold(base.block, itemSpec.m_Item, num4);
			Singleton.Manager<ManPlayer>.inst.AddMoney(num4);
			WorldPosition position = Singleton.Manager<ManOverlay>.inst.WorldPositionForFloatingText(base.block.visible);
			Singleton.Manager<ManOverlay>.inst.AddFloatingTextOverlay(Singleton.Manager<Localisation>.inst.GetMoneyStringWithSymbol(num4), position);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				PopupNumberMessage message = new PopupNumberMessage
				{
					m_Type = PopupNumberMessage.Type.Money,
					m_Number = num4,
					m_Position = position
				};
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.AddFloatingNumberPopupMessage, message);
			}
			break;
		}
		case RecipeTable.Recipe.OutputType.Items:
		{
			_ = m_ConsumeProgress.outputQueue.Count;
			List<ItemTypeInfo> constituentChunks = new List<ItemTypeInfo>();
			for (int num = m_ConsumeProgress.currentRecipe.m_OutputItems.Length - 1; num >= 0; num--)
			{
				RecipeTable.Recipe.ItemSpec itemSpecToDesconstruct = m_ConsumeProgress.currentRecipe.m_OutputItems[num];
				GetOutputItems(itemSpecToDesconstruct, ref constituentChunks);
			}
			int num2 = constituentChunks.Count - (int)Mathf.Round(GetOutputEfficiencyForCurrentRecipe().Random * (float)constituentChunks.Count);
			for (int i = 0; i < num2; i++)
			{
				constituentChunks.RemoveAt(UnityEngine.Random.Range(0, constituentChunks.Count));
			}
			{
				foreach (ItemTypeInfo item in constituentChunks)
				{
					m_ConsumeProgress.outputQueue.Push(item);
				}
				break;
			}
		}
		default:
			ResetState();
			break;
		case RecipeTable.Recipe.OutputType.Energy:
			break;
		}
	}

	private bool CheckHasItemsInSystem()
	{
		bool flag = false;
		for (int i = 0; i < m_InputStacks.Length; i++)
		{
			if (!m_InputStacks[i].IsEmpty)
			{
				flag = true;
				break;
			}
		}
		return flag || !m_Consume.stack.IsEmpty || (m_Operate.stack != null && !m_Operate.stack.IsEmpty) || (m_Output.stack != null && !m_Output.stack.IsEmpty && m_Output.stack.ReceivedThisHeartbeat) || m_ConsumeProgress.CheckHasTakenInputs() || m_ConsumeProgress.outputQueue.Count > 0;
	}

	private bool CheckForChunkToSingleOutputChunkRecipe()
	{
		bool result = false;
		foreach (RecipeTable.Recipe allRecipe in m_AllRecipes)
		{
			bool flag = false;
			for (int i = 0; i < allRecipe.m_InputItems.Length; i++)
			{
				if (allRecipe.m_InputItems[i].m_Item.ObjectType == ObjectTypes.Chunk)
				{
					flag = true;
					break;
				}
			}
			bool flag2 = allRecipe.m_OutputItems.Length == 1 && allRecipe.m_OutputItems[0].m_Item.ObjectType == ObjectTypes.Chunk;
			if (flag && flag2)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void UpdateRecipesFromAttachedDongles()
	{
		HashSet<ModuleRecipeDongle> hashSet = new HashSet<ModuleRecipeDongle>();
		HashSet<TankBlock> hashSet2 = new HashSet<TankBlock>();
		hashSet2.Add(base.block);
		GetAttachedRecipeDongles(base.block, hashSet, hashSet2);
		HashSet<ModuleRecipeDongle> hashSet3 = new HashSet<ModuleRecipeDongle>();
		for (int i = 0; i < m_AttachedRecipeDongles.Count; i++)
		{
			if (!hashSet.Remove(m_AttachedRecipeDongles[i]))
			{
				hashSet3.Add(m_AttachedRecipeDongles[i]);
			}
		}
		bool flag = false;
		bool flag2 = false;
		foreach (ModuleRecipeDongle item in hashSet)
		{
			m_AttachedRecipeDongles.Add(item);
			flag |= AddRecipes(item.recipeProvider, rebuildCache: false);
		}
		foreach (ModuleRecipeDongle item2 in hashSet3)
		{
			m_AttachedRecipeDongles.Remove(item2);
			flag2 |= RemoveRecipes(item2.recipeProvider, rebuildCache: false);
		}
		if (flag || flag2)
		{
			RebuildRecipeInOutCache();
		}
		if (flag2 && m_ConsumeProgress.currentRecipe != null && !m_RecipeLookup.ContainsKey((RecipeManager.RecipeDefinition)m_ConsumeProgress.currentRecipe))
		{
			CancelRecipe();
			UpdateNetworkedState();
		}
		if (flag || flag2)
		{
			OnConsumerRecipesChanged.Send(this);
		}
	}

	private void GetAttachedRecipeDongles(TankBlock sourceBlock, HashSet<ModuleRecipeDongle> foundDongles, HashSet<TankBlock> traversedBlocks)
	{
		for (int i = 0; i < sourceBlock.ConnectedBlocksByAP.Length; i++)
		{
			TankBlock tankBlock = sourceBlock.ConnectedBlocksByAP[i];
			if (!(tankBlock != null) || traversedBlocks.Contains(tankBlock))
			{
				continue;
			}
			traversedBlocks.Add(tankBlock);
			if (tankBlock.GetComponent<ModuleItemConsume>() == null)
			{
				ModuleRecipeDongle component = tankBlock.GetComponent<ModuleRecipeDongle>();
				if (component != null)
				{
					foundDongles.Add(component);
					GetAttachedRecipeDongles(tankBlock, foundDongles, traversedBlocks);
				}
			}
		}
	}

	private void DetermineConsumedInputs()
	{
		m_ConsumedInputs.Clear();
		if (!m_ManualControl || m_ConsumeProgress.currentRecipe == null)
		{
			return;
		}
		for (int i = 0; i < m_ConsumeProgress.currentRecipe.m_InputItems.Length; i++)
		{
			ItemTypeInfo item = m_ConsumeProgress.currentRecipe.m_InputItems[i].m_Item;
			int quantity = m_ConsumeProgress.currentRecipe.m_InputItems[i].m_Quantity;
			int num = 0;
			for (int j = 0; j < m_ConsumeProgress.inputsRemaining.Count; j++)
			{
				if (m_ConsumeProgress.inputsRemaining[j] == item)
				{
					num++;
				}
			}
			int num2 = quantity - num;
			for (int k = 0; k < num2; k++)
			{
				m_ConsumedInputs.Add(item);
			}
		}
	}

	private void OnAttached()
	{
		base.block.tank.Holders.HBEvent.Subscribe(OnHeartbeat);
		base.block.tank.Holders.CraftingSetupChanged.Subscribe(OnCraftingSetupChanged);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPullInput, 6);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnConsumeInput, 7);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnOperate, 8);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnSpawnOutput, 9);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPushOutput, 10);
		if (m_AcceptsRecipeDonges)
		{
			base.block.tank.AttachEvent.Subscribe(OnBlockAttachedToTank);
			base.block.tank.DetachEvent.Subscribe(OnBlockDetachedToTank);
		}
		ResetState();
		UpdateNetworkedState();
		if (m_ManualControl)
		{
			d.Assert(m_Overlay == null, "ModuleItemConsume: Already have an overlay before attach");
			m_Overlay = Singleton.Manager<ManOverlay>.inst.AddCraftingOverlay(this);
		}
		else if (CanHonourRequests)
		{
			d.Assert(m_Overlay == null, "ModuleItemConsume: Already have an overlay before attach");
			m_Overlay = Singleton.Manager<ManOverlay>.inst.AddRefiningOverlay(this);
		}
		if (m_AcceptsRecipeDonges)
		{
			m_TechDonglesDirty = true;
			if (m_BaseRecipeProvider != null)
			{
				AddRecipes(m_BaseRecipeProvider);
			}
		}
	}

	private TechHolders.OperationResult OnPullInput()
	{
		if (m_Purging)
		{
			return TechHolders.OperationResult.None;
		}
		if (m_ActivityBlocksInput)
		{
			if (!m_Consume.stack.IsEmpty)
			{
				if (!m_Consume.stack.ReceivedThisHeartbeat)
				{
					return TechHolders.OperationResult.Retry;
				}
				return TechHolders.OperationResult.None;
			}
			if (IsOperating)
			{
				return TechHolders.OperationResult.None;
			}
		}
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		ModuleItemHolder.Stack[] inputStacks = m_InputStacks;
		foreach (ModuleItemHolder.Stack stack in inputStacks)
		{
			TechHolders.OperationResult operationResult2 = TechHolders.OperationResult.None;
			ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator = stack.ConnectedStacks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.IterateItemsIncludingLinkedStacks().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Visible current = enumerator2.Current;
					operationResult2 = stack.TryTakeOnHeartbeat(current);
					if (operationResult2 != TechHolders.OperationResult.None)
					{
						break;
					}
				}
				if (operationResult2 != TechHolders.OperationResult.None)
				{
					break;
				}
			}
			operationResult = TechHolders.CombineOperationResults(operationResult, operationResult2);
		}
		return operationResult;
	}

	private TechHolders.OperationResult OnConsumeInput()
	{
		if (!m_Consume.stack.IsEmpty && !m_Consume.stack.ReceivedThisHeartbeat)
		{
			return TechHolders.OperationResult.Retry;
		}
		if (OperatingBeatsLeft > 1)
		{
			return TechHolders.OperationResult.None;
		}
		if (m_OperationBlocksConsume && OperatingBeatsLeft == 1)
		{
			return TechHolders.OperationResult.None;
		}
		bool flag = false;
		bool flag2 = false;
		int num = 0;
		for (int i = 0; i < m_InputStacks.Length; i++)
		{
			int num2 = (m_NextInputIndex + i) % m_InputStacks.Length;
			ModuleItemHolder.Stack stack = m_InputStacks[num2];
			if (!stack.ReceivedThisHeartbeat)
			{
				if (!stack.IsEmpty)
				{
					flag = true;
					num = num2;
					break;
				}
				flag2 = true;
			}
		}
		if (!flag)
		{
			if (!flag2)
			{
				return TechHolders.OperationResult.None;
			}
			return TechHolders.OperationResult.Retry;
		}
		Visible firstItem = m_InputStacks[num].FirstItem;
		if (!m_Purging && m_ManualControl && m_ConsumeProgress.currentRecipe != null)
		{
			bool condition = false;
			for (int j = 0; j < m_ConsumeProgress.inputsRemaining.Count; j++)
			{
				if (m_ConsumeProgress.inputsRemaining[j] == firstItem.m_ItemType)
				{
					m_ConsumeProgress.inputsRemaining.RemoveAt(j);
					condition = true;
					break;
				}
			}
			d.Assert(condition);
		}
		if (m_InputStacks.Length > 1)
		{
			m_HolderBeam.OverrideAnimDummies(m_Consume.stack.GetStackIndex(), m_InputStacks[num].GetStackIndex());
		}
		m_HolderBeam.ConfigureStack(m_Consume.stack.GetStackIndex(), drawBeam: false, ModuleItemHolderBeam.ItemMovementType.Static);
		bool force = true;
		m_Consume.stack.Take(firstItem, force);
		SetAnimationTrigger(AnimationTriggers.StartConsumingTrigger);
		m_NextHeartbeatTriggers |= AnimationTriggers.FinishConsumingTrigger;
		OnStartConsuming();
		firstItem.EnablePhysics(enable: false);
		m_NextInputIndex = (num + 1) % m_InputStacks.Length;
		if (base.block.tank != null && base.block.tank.IsFriendly(0))
		{
			Singleton.Manager<ManStats>.inst.ItemConsumed(base.block, firstItem);
		}
		return TechHolders.OperationResult.Effect;
	}

	private TechHolders.OperationResult OnOperate()
	{
		TechHolders.OperationResult result = TechHolders.OperationResult.None;
		if (!m_Purging)
		{
			if (!m_Consume.stack.IsEmpty && !m_Consume.stack.ReceivedThisHeartbeat)
			{
				bool drawBeam = false;
				m_HolderBeam.ConfigureStack(m_Consume.stack.GetStackIndex(), drawBeam, ModuleItemHolderBeam.ItemMovementType.Invisible);
			}
			if (OperatingBeatsLeft <= 0)
			{
				result = TryBeginOperating();
			}
			if (OperatingBeatsLeft == 1)
			{
				result = FinishOperating();
			}
		}
		else
		{
			if (!m_Consume.stack.ReceivedThisHeartbeat && !m_Consume.stack.IsEmpty)
			{
				Visible firstItem = m_Consume.stack.FirstItem;
				DestroyItem(firstItem);
				result = TechHolders.OperationResult.Effect;
			}
			if (!CheckHasItemsInSystem())
			{
				AnimationTriggers animationTriggers = AnimationTriggers.StartBurningWasteTrigger;
				if (OperatingBeatsLeft > 0)
				{
					animationTriggers |= AnimationTriggers.FinishOperatingTrigger;
					m_FirstOperatingHeartbeat = -1;
				}
				SetAnimationTrigger(animationTriggers);
				m_NextHeartbeatTriggers |= AnimationTriggers.FinishBurningWasteTrigger;
				m_Purging = false;
			}
		}
		return result;
	}

	private TechHolders.OperationResult TryBeginOperating()
	{
		TechHolders.OperationResult result = TechHolders.OperationResult.None;
		if (m_ConsumeProgress.outputQueue.Count > 0)
		{
			return TechHolders.OperationResult.Retry;
		}
		if (!m_Consume.stack.IsEmpty && !m_Consume.stack.ReceivedThisHeartbeat)
		{
			Visible firstItem = m_Consume.stack.FirstItem;
			if (!m_Purging)
			{
				if (!m_ManualControl)
				{
					foreach (RecipeTable.Recipe allRecipe in m_AllRecipes)
					{
						d.Assert(allRecipe.m_InputItems.Length == 1 && allRecipe.m_InputItems[0].m_Quantity == 1, "multiple-recipe selection only supports single-input recipes");
						if (allRecipe.m_InputItems[0].m_Item == firstItem.m_ItemType)
						{
							m_ConsumeProgress.SetRecipe(allRecipe, Progress.RecipeRequester.None);
							m_ConsumeProgress.inputsRemaining.RemoveAt(0);
							break;
						}
					}
					d.Assert(m_ConsumeProgress.currentRecipe != null, "Failed to find recipe to match input");
				}
				if (m_ConsumeProgress.currentRecipe != null && m_ConsumeProgress.inputsRemaining.Count == 0)
				{
					m_FirstOperatingHeartbeat = base.block.tank.Holders.HeartbeatCount;
					SetAnimationTrigger(AnimationTriggers.StartOperatingTrigger);
					if (m_ConsumeProgress.currentRecipe.m_OutputType == RecipeTable.Recipe.OutputType.Energy)
					{
						m_ConsumeProgress.energySupplyRemaining = m_ConsumeProgress.currentRecipe.m_EnergyOutput * m_EnergyMultiplier;
						m_ConsumeProgress.energySupplyRate = m_ConsumeProgress.energySupplyRemaining / (m_ConsumeProgress.currentRecipe.m_BuildTimeSeconds * GetDurationMultiplierForCurrentRecipe());
						m_ConsumeProgress.energySupplyType = m_ConsumeProgress.currentRecipe.m_EnergyType;
					}
				}
			}
			if (m_Operate.stack != null)
			{
				bool force = true;
				bool insertAtBase = false;
				m_Operate.stack.Take(firstItem, force, insertAtBase);
				m_HolderBeam.ConfigureStack(m_Operate.stack.GetStackIndex(), drawBeam: false, ModuleItemHolderBeam.ItemMovementType.Static);
			}
			else
			{
				DestroyItem(firstItem);
			}
			result = TechHolders.OperationResult.Effect;
		}
		return result;
	}

	private TechHolders.OperationResult FinishOperating()
	{
		InitRecipeOutput();
		if (m_ConsumeProgress.repeatRecipe)
		{
			m_ConsumeProgress.ResetRemainingInputs();
		}
		else
		{
			m_ConsumeProgress.ClearRecipe();
		}
		m_NextHeartbeatTriggers |= AnimationTriggers.FinishOperatingTrigger;
		return TechHolders.OperationResult.None;
	}

	private TechHolders.OperationResult OnSpawnOutput()
	{
		if (m_Purging)
		{
			return TechHolders.OperationResult.None;
		}
		if (m_Output.stack != null && !m_Output.stack.IsEmpty)
		{
			if (!m_Output.stack.ReceivedThisHeartbeat)
			{
				return TechHolders.OperationResult.Retry;
			}
			return TechHolders.OperationResult.None;
		}
		if (IsOperating)
		{
			return TechHolders.OperationResult.None;
		}
		if ((bool)m_OperateItemInterceptedBy)
		{
			CatchInterceptedItem(snap: true);
		}
		ClearOutStack(m_Operate.stack);
		if (m_ConsumeProgress.outputQueue.Count == 0)
		{
			return TechHolders.OperationResult.None;
		}
		if (Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(base.block.trans.position))
		{
			ItemTypeInfo itemTypeInfo = m_ConsumeProgress.outputQueue.Pop();
			if (itemTypeInfo != null)
			{
				Visible visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(itemTypeInfo, base.block.trans.position, Quaternion.identity);
				if (visible != null)
				{
					visible.EnablePhysics(enable: false);
					SetAnimationTrigger(AnimationTriggers.StartProducingTrigger);
					m_NextHeartbeatTriggers |= AnimationTriggers.FinishProducingTrigger;
					OnStartProducing();
					visible.trans.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
					m_HolderBeam.ConfigureStack(m_Output.stack.GetStackIndex(), drawBeam: false, ModuleItemHolderBeam.ItemMovementType.Static);
					m_Output.stack.Take(visible, force: true);
					if (base.block.tank != null && base.block.tank.IsFriendly(0))
					{
						Singleton.Manager<ManStats>.inst.ItemProduced(base.block, visible);
					}
				}
				else
				{
					d.LogErrorFormat("ModuleItemConsume.OnSpawnOutput on tank {0}, block {1} unable to spawn item {2} -- possibly because it is off tile", base.block.tank.name, base.block.name, itemTypeInfo);
				}
			}
			return TechHolders.OperationResult.Effect;
		}
		return TechHolders.OperationResult.None;
	}

	private TechHolders.OperationResult OnPushOutput()
	{
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		Visible visible = null;
		if (m_Output.stack != null && !m_Output.stack.IsEmpty && !m_Output.stack.ReceivedThisHeartbeat)
		{
			Visible firstItem = m_Output.stack.FirstItem;
			firstItem.EnablePhysics(enable: true);
			m_HolderBeam.ConfigureStack(m_Output.stack.GetStackIndex(), drawBeam: true, ModuleItemHolderBeam.ItemMovementType.AccelerateTowardsFixedPositions);
			if (!m_KickOutItem)
			{
				ModuleItemHolder.Stack stack = ((m_Holder.block.tank != null) ? m_Holder.block.tank.Holders.GetRequestedItemNextHop(firstItem) : null);
				if (stack != null)
				{
					TechHolders.OperationResult operationResult2 = stack.TryTakeOnHeartbeat(firstItem);
					d.AssertFormat(operationResult2 != TechHolders.OperationResult.EffectRetry, "ModuleItemConsume.OnPushOutput returned EffectRetry");
					operationResult = TechHolders.CombineOperationResults(operationResult, operationResult2);
					if (operationResult2 == TechHolders.OperationResult.Effect)
					{
						visible = firstItem;
					}
				}
				if (visible == null)
				{
					ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator = m_Output.stack.ConnectedStacks.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ModuleItemHolder.Stack current = enumerator.Current;
						if (!current.ReceivedThisHeartbeat && current != stack)
						{
							TechHolders.OperationResult operationResult3 = current.TryTakeOnHeartbeat(firstItem);
							d.AssertFormat(operationResult3 != TechHolders.OperationResult.EffectRetry, "ModuleItemConsume.OnPushOutput returned EffectRetry");
							operationResult = TechHolders.CombineOperationResults(operationResult, operationResult3);
							if (operationResult3 == TechHolders.OperationResult.Effect)
							{
								visible = firstItem;
							}
							if (operationResult3 != TechHolders.OperationResult.None)
							{
								break;
							}
						}
					}
				}
			}
			else
			{
				bool notifyRelease = true;
				firstItem.SetHolder(null, notifyRelease);
				if (firstItem.rbody != null && m_KickLocator != null)
				{
					firstItem.rbody.position = m_KickLocator.position;
					firstItem.rbody.velocity = m_KickLocator.forward * m_KickSpeed;
					firstItem.rbody.angularVelocity = m_KickAngularVelocity;
				}
				operationResult = TechHolders.OperationResult.Effect;
				visible = firstItem;
			}
		}
		if (visible != null && base.block.tank != null && base.block.tank.IsFriendly(0) && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() && visible.m_ItemType.ObjectType == ObjectTypes.Block)
		{
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock((BlockTypes)visible.ItemType);
		}
		return operationResult;
	}

	private void OnHeartbeat(int hbCount, TechHolders.Heartbeat hbStep)
	{
		switch (hbStep)
		{
		case TechHolders.Heartbeat.PrePass:
			DetermineConsumedInputs();
			UpdateItemRequests();
			if (m_NextHeartbeatTriggers != 0)
			{
				SetAnimationTrigger(m_NextHeartbeatTriggers);
				m_NextHeartbeatTriggers = ~(AnimationTriggers.StartConsumingTrigger | AnimationTriggers.FinishConsumingTrigger | AnimationTriggers.StartOperatingTrigger | AnimationTriggers.FinishOperatingTrigger | AnimationTriggers.StartProducingTrigger | AnimationTriggers.FinishProducingTrigger | AnimationTriggers.StartBurningWasteTrigger | AnimationTriggers.FinishBurningWasteTrigger);
			}
			break;
		case TechHolders.Heartbeat.PostPass:
			m_ConsumedInputs.Clear();
			if (hbCount > m_LastBuildRequestHeartbeat)
			{
				if (OneRecipeAtATime)
				{
					if (m_ConsumeProgress.currentRecipe != null && m_ConsumeProgress.requester == Progress.RecipeRequester.Block && m_ConsumeProgress.inputsRemaining.Count > 0)
					{
						CancelRecipe();
					}
				}
				else
				{
					m_WantedItems.Clear();
				}
			}
			UpdateNetworkedState(triggerSync: true);
			break;
		}
	}

	private void OnStartConsuming()
	{
		TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, m_ConsumeSFXType);
		base.block.tank.TechAudio.PlayOneshot(data);
	}

	private void OnStartProducing()
	{
		Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + m_ProduceSFXDelay, PlayProduceSFX);
	}

	private void PlayProduceSFX()
	{
		if (base.block != null && base.block.tank != null)
		{
			TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, m_ProduceSFXType);
			base.block.tank.TechAudio.PlayOneshot(data);
		}
	}

	private void OnCraftingSetupChanged()
	{
		m_ItemListRequest.FlagStructureAsDirty();
	}

	private void OnPreDetach(int dummy)
	{
		ResetState();
		UpdateNetworkedState();
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.HBEvent.Unsubscribe(OnHeartbeat);
		base.block.tank.Holders.CraftingSetupChanged.Unsubscribe(OnCraftingSetupChanged);
		base.block.tank.Holders.UnregisterOperations(m_Holder);
		if (m_AcceptsRecipeDonges)
		{
			base.block.tank.AttachEvent.Unsubscribe(OnBlockAttachedToTank);
			base.block.tank.DetachEvent.Unsubscribe(OnBlockDetachedToTank);
		}
		if (s_MenuController == this)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(m_MenuType);
			s_MenuController = null;
		}
		if (m_ManualControl || CanHonourRequests)
		{
			d.Assert(m_Overlay != null, "ModuleItemConsume: Don't have an overlay before detach");
			Singleton.Manager<ManOverlay>.inst.RemoveObjectOverlay(m_Overlay);
			m_Overlay = null;
		}
		if (m_AcceptsRecipeDonges)
		{
			m_RecipeList.Clear();
			m_RecipeDuplicateCount.Clear();
			m_AttachedRecipeDongles.Clear();
		}
		m_TechDonglesDirty = false;
	}

	private void OnBlockAttachedToTank(TankBlock attachedBlock, Tank tank)
	{
		if (attachedBlock != base.block && attachedBlock.GetComponent<ModuleRecipeProvider>() != null)
		{
			m_TechDonglesDirty = true;
		}
	}

	private void OnBlockDetachedToTank(TankBlock detachedBlock, Tank tank)
	{
		if (detachedBlock != base.block && detachedBlock.GetComponent<ModuleRecipeProvider>() != null)
		{
			m_TechDonglesDirty = true;
		}
	}

	private bool CheckAnchoredCondition()
	{
		if (base.block.tank != null)
		{
			if (m_NeedsToBeAnchored)
			{
				return base.block.tank.IsAnchored;
			}
			return true;
		}
		return false;
	}

	private bool ThisConsumerIsReceivingAnyItemRequests()
	{
		bool result = false;
		if ((bool)base.block.tank)
		{
			result = m_LastRequestReceivedHeartbeat == base.block.tank.Holders.HeartbeatCount;
		}
		return result;
	}

	private bool ThisConsumerMakingAnyItemRequests()
	{
		if (m_ManualControl && m_ConsumeProgress.requester == Progress.RecipeRequester.User)
		{
			return m_ConsumeProgress.currentRecipe != null;
		}
		return false;
	}

	private bool InputStacksContain(ModuleItemHolder.Stack stack)
	{
		bool result = false;
		for (int i = 0; i < m_InputStacks.Length; i++)
		{
			if (m_InputStacks[i] == stack)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		d.Assert(base.block.tank, "ModuleItemConsume.CanAcceptItem isn't part of a tank");
		if (toStack == m_Consume.stack || toStack == m_Operate.stack || toStack == m_Output.stack)
		{
			return false;
		}
		if (!CheckAnchoredCondition())
		{
			return false;
		}
		if (passType == (ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.Test) && item == null)
		{
			return true;
		}
		if (InputStacksContain(toStack) && (m_AllowMultItemsOnInput ? toStack.IsFull : (!toStack.IsEmpty)))
		{
			return false;
		}
		bool flag = true;
		if (ThisConsumerIsReceivingAnyItemRequests())
		{
			flag = base.block.tank.Holders.GetRequestedItemNextHop(item) == toStack;
		}
		if (flag && ConvertsFromType(item.m_ItemType))
		{
			if (m_ConsumeProgress.inputsRemaining.Count != 0)
			{
				for (int i = 0; i < m_ConsumeProgress.inputsRemaining.Count; i++)
				{
					if (m_ConsumeProgress.inputsRemaining[i] == item.m_ItemType)
					{
						return true;
					}
				}
			}
			else if (!m_ManualControl)
			{
				foreach (RecipeTable.Recipe allRecipe in m_AllRecipes)
				{
					if (allRecipe.InputsContain(item.m_ItemType))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool CanReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if (m_Output.stack != null && fromStack == m_Output.stack && !m_Output.stack.ReceivedThisHeartbeat)
		{
			return !m_KickOutItem;
		}
		return false;
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem != item && (toStack == null || toStack.myHolder != m_Holder))
		{
			item.EnablePhysics(enable: true);
		}
	}

	private bool HandlePickupFilterCallback(Visible visible)
	{
		if (m_Purging)
		{
			return false;
		}
		if (m_ActivityBlocksInput && (IsOperating || !m_Consume.stack.IsEmpty))
		{
			return false;
		}
		ModuleItemHolder.Stack[] inputStacks = m_InputStacks;
		foreach (ModuleItemHolder.Stack toStack in inputStacks)
		{
			if (!CanAcceptItem(visible, null, toStack, ModuleItemHolder.PassType.Pick))
			{
				return false;
			}
		}
		ItemTypeInfo targetType;
		if (m_ConsumeProgress.currentRecipe != null)
		{
			int _countNeeded = 0;
			targetType = visible.m_ItemType;
			foreach (ItemTypeInfo item in m_ConsumeProgress.inputsRemaining)
			{
				if (item == targetType)
				{
					_countNeeded++;
				}
			}
			ModuleItemHolder.StackIterator.Enumerator enumerator2 = m_Holder.Stacks.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ModuleItemHolder.Stack current = enumerator2.Current;
				if (current != m_Operate.stack && current != m_Output.stack)
				{
					CountItems(current, ref _countNeeded);
				}
			}
			if (_countNeeded <= 0)
			{
				return false;
			}
		}
		return true;
		void CountItems(ModuleItemHolder.Stack stack, ref int reference)
		{
			int num = 0;
			while (reference > 0 && num < stack.items.Count)
			{
				if (stack.items[num].m_ItemType == targetType)
				{
					reference--;
				}
				num++;
			}
		}
	}

	private void OnUpdateSupplyEnergy()
	{
		if (m_ConsumeProgress.energySupplyRemaining > 0f)
		{
			float num = Time.deltaTime * m_ConsumeProgress.energySupplyRate;
			m_ModuleEnergy.Supply(m_ConsumeProgress.energySupplyType, num);
			if (base.block.tank != null && base.block.tank.IsFriendly(0))
			{
				Singleton.Manager<ManStats>.inst.EnergyGenerated(base.block, num);
			}
			m_ConsumeProgress.energySupplyRemaining -= num;
		}
	}

	private void InitStackAndAddToList(ModuleItemHolder.StackHandle handle, List<ModuleItemHolder.Stack> toList)
	{
		handle.InitReference(m_Holder);
		if (handle.stack != null)
		{
			toList.Add(handle.stack);
		}
	}

	private bool AddRecipes(ModuleRecipeProvider provider, bool rebuildCache = true)
	{
		bool flag = false;
		foreach (RecipeTable.RecipeList item in provider)
		{
			foreach (RecipeTable.Recipe item2 in item)
			{
				int hashCode = item2.GetHashCode();
				int value = 0;
				if (!m_RecipeDuplicateCount.TryGetValue(hashCode, out value))
				{
					flag = true;
					m_RecipeList.Add(item2);
				}
				value++;
				m_RecipeDuplicateCount[hashCode] = value;
			}
		}
		if (flag && rebuildCache)
		{
			RebuildRecipeInOutCache();
		}
		return flag;
	}

	private bool RemoveRecipes(ModuleRecipeProvider provider, bool rebuildCache = true)
	{
		bool flag = false;
		foreach (RecipeTable.RecipeList item in provider)
		{
			foreach (RecipeTable.Recipe item2 in item)
			{
				int hashCode = item2.GetHashCode();
				int value = 0;
				if (m_RecipeDuplicateCount.TryGetValue(hashCode, out value))
				{
					value--;
					if (value <= 0)
					{
						flag = true;
						m_RecipeList.Remove(item2);
						m_RecipeDuplicateCount.Remove(hashCode);
					}
					else
					{
						m_RecipeDuplicateCount[hashCode] = value;
					}
				}
				else
				{
					d.LogError("ModuleItemConsume.RemoveRecipeProvider - Tried to remove recipe " + item2.ToString() + " but was not found in the list!?");
				}
			}
		}
		if (flag && rebuildCache)
		{
			RebuildRecipeInOutCache();
		}
		return flag;
	}

	private void RebuildRecipeInOutCache()
	{
		m_CanHonourRequests = CheckForChunkToSingleOutputChunkRecipe();
		if (m_CanHonourRequests)
		{
			if (m_RecipeInputs == null)
			{
				m_RecipeInputs = new HashSet<ItemTypeInfo>();
				m_RecipeOutputs = new Dictionary<ItemTypeInfo, RecipeTable.Recipe>();
			}
			m_RecipeInputs.Clear();
			m_RecipeOutputs.Clear();
			foreach (RecipeTable.Recipe allRecipe in m_AllRecipes)
			{
				for (int i = 0; i < allRecipe.m_InputItems.Length; i++)
				{
					m_RecipeInputs.Add(allRecipe.m_InputItems[i].m_Item);
				}
				for (int j = 0; j < allRecipe.m_OutputItems.Length; j++)
				{
					m_RecipeOutputs.Add(allRecipe.m_OutputItems[j].m_Item, allRecipe);
				}
			}
		}
		else
		{
			if (m_RecipeInputs == null)
			{
				m_RecipeInputs = new HashSet<ItemTypeInfo>();
			}
			m_RecipeInputs.Clear();
			foreach (RecipeTable.Recipe allRecipe2 in m_AllRecipes)
			{
				for (int k = 0; k < allRecipe2.m_InputItems.Length; k++)
				{
					m_RecipeInputs.Add(allRecipe2.m_InputItems[k].m_Item);
				}
			}
		}
		foreach (RecipeTable.Recipe allRecipe3 in m_AllRecipes)
		{
			if (!m_RecipeLookup.TryGetValue((RecipeManager.RecipeDefinition)allRecipe3, out var _))
			{
				m_RecipeLookup.Add((RecipeManager.RecipeDefinition)allRecipe3, allRecipe3);
			}
		}
	}

	public RecipeTable.Recipe GetRecipe(RecipeManager.RecipeDefinition recipeDef)
	{
		if (m_RecipeLookup.TryGetValue(recipeDef, out var value))
		{
			return value;
		}
		return null;
	}

	private void CatchInterceptedItem(bool snap)
	{
		Visible firstItem = m_Operate.stack.FirstItem;
		float num = Vector3.Dot(m_OperateItemInterceptAxis, firstItem.trans.position);
		if (snap)
		{
			firstItem.trans.position = firstItem.trans.position + (m_OperateItemInterceptPos - num) * m_OperateItemInterceptAxis;
		}
		m_OperateItemInterceptedBy = null;
		firstItem.SetHolder(null);
		firstItem.EnablePhysics(enable: true);
		Rigidbody component = firstItem.GetComponent<Rigidbody>();
		if ((bool)component)
		{
			component.velocity = new Vector3(0f, snap ? 20f : ((num - m_OperateItemInterceptedPrevPos) / Time.deltaTime * 0.25f), 0f);
		}
	}

	private void UpdateInterceptedDeliveryItem()
	{
		if (!(m_OperateItemInterceptedBy != null))
		{
			return;
		}
		if (m_Operate.stack.IsEmpty)
		{
			m_OperateItemInterceptedBy = null;
			return;
		}
		Visible firstItem = m_Operate.stack.FirstItem;
		float num = Vector3.Dot(m_OperateItemInterceptAxis, firstItem.trans.position);
		if (num >= m_OperateItemInterceptPos)
		{
			CatchInterceptedItem(snap: false);
		}
		else
		{
			m_OperateItemInterceptedPrevPos = num;
		}
	}

	private void OnModBlocksChanged()
	{
		d.Assert(!base.block.visible.isActive, "Mod blocks changed while block was active in the world? Expect this to only happen during loading, where all blocks are in the Pool!");
		m_RefreshBaseRecipes = true;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData
			{
				consumeProgress = m_ConsumeProgress,
				operatingBeatsDone = CurRecipeBeats - OperatingBeatsLeft,
				setAnimationTriggers = (int)m_CurrentlyActiveTriggers
			};
			serialData.consumeProgress.inputsRemaining = new List<ItemTypeInfo>(serialData.consumeProgress.inputsRemaining);
			serialData.consumeProgress.outputQueue = new Stack<ItemTypeInfo>(serialData.consumeProgress.outputQueue);
			serialData.Store(blockSpec.saveState);
		}
		else
		{
			SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
			if (serialData2 != null)
			{
				m_ConsumeProgress = serialData2.consumeProgress;
				UpdateNetworkedState(triggerSync: true);
				m_FirstOperatingHeartbeat = -Mathf.Min(serialData2.operatingBeatsDone, CurRecipeBeats);
				SetAnimationTrigger((AnimationTriggers)serialData2.setAnimationTriggers);
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleItemConsume;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		m_SyncedRecipeProgress.Serialise(writer);
		m_SyncedRecipeRepeating.Serialise(writer);
		m_SyncedConsumeAnimations.Serialise(writer);
		m_SyncedConsumeAnimations.Data.value = 0;
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_SyncedRecipeProgress.Deserialise(reader);
		m_SyncedRecipeRepeating.Deserialise(reader);
		m_SyncedConsumeAnimations.Deserialise(reader);
	}

	public void UpdateNetworkedState(bool triggerSync = false)
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return;
		}
		bool flag = m_ConsumeProgress.currentRecipe != null;
		bool flag2 = !flag || (m_SyncedRecipeProgress.Data.m_RecipeRequester == (byte)m_ConsumeProgress.requester && m_SyncedRecipeProgress.Data.m_WantedInputItemWarningsFlags == m_ConsumeProgress.wantedItemsWarnings.Field && GetRecipe(m_SyncedRecipeProgress.Data.m_RecipeDef) == m_ConsumeProgress.currentRecipe && ListEquals<ItemTypeInfo>(m_SyncedRecipeProgress.Data.m_InputsRemaining, m_ConsumeProgress.inputsRemaining) && ListEquals<ItemTypeInfo>(m_SyncedRecipeProgress.Data.m_OutputQueue, m_ConsumeProgress.outputQueue));
		bool flag3 = ListEquals<ItemTypeInfo>(m_SyncedRecipeProgress.Data.m_WantedItems, m_WantedItems);
		if (!(m_SyncedRecipeProgress.Data.m_HasRecipe == flag && flag2 && flag3))
		{
			m_SyncedRecipeProgress.Data.m_HasRecipe = flag;
			if (flag)
			{
				m_SyncedRecipeProgress.Data.m_RecipeDef = (RecipeManager.RecipeDefinition)m_ConsumeProgress.currentRecipe;
				m_SyncedRecipeProgress.Data.m_RecipeRequester = (byte)m_ConsumeProgress.requester;
				m_SyncedRecipeProgress.Data.m_InputsRemaining = m_ConsumeProgress.inputsRemaining;
				m_SyncedRecipeProgress.Data.m_WantedInputItemWarningsFlags = m_ConsumeProgress.wantedItemsWarnings.Field;
				m_SyncedRecipeProgress.Data.m_OutputQueue = m_ConsumeProgress.outputQueue;
			}
			m_SyncedRecipeProgress.Data.m_WantedItems = m_WantedItems;
			m_HasDirtyNetState = true;
		}
		if (triggerSync && m_HasDirtyNetState)
		{
			m_SyncedRecipeProgress.Sync();
			m_HasDirtyNetState = false;
		}
		static bool ListEquals<T>(IEnumerable<T> a, IEnumerable<T> b)
		{
			int num = a?.Count() ?? 0;
			int num2 = ((b != null) ? b.Count() : 0);
			if (num == num2)
			{
				if (num != 0)
				{
					return a.All((T s) => b.Contains(s));
				}
				return true;
			}
			return false;
		}
	}

	private void OnMPRecipeProgressMessageReceived(ItemConsumeProgressMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			if (msg.m_HasRecipe)
			{
				RecipeTable.Recipe recipe = GetRecipe(msg.m_RecipeDef);
				d.AssertFormat(recipe != null, "Failed to find Recipe for received recipeDef {0}", msg.m_RecipeDef);
				m_ConsumeProgress.SetRecipe(recipe, (Progress.RecipeRequester)msg.m_RecipeRequester);
				m_ConsumeProgress.inputsRemaining = msg.m_InputsRemaining;
				m_ConsumeProgress.wantedItemsWarnings.SetFlags(msg.m_WantedInputItemWarningsFlags);
				m_ConsumeProgress.outputQueue = msg.m_OutputQueue;
				m_ConsumeProgress.energySupplyRemaining = m_ConsumeProgress.currentRecipe.m_EnergyOutput * m_EnergyMultiplier;
				m_ConsumeProgress.energySupplyRate = m_ConsumeProgress.energySupplyRemaining / (m_ConsumeProgress.currentRecipe.m_BuildTimeSeconds * GetDurationMultiplierForCurrentRecipe());
				m_ConsumeProgress.energySupplyType = m_ConsumeProgress.currentRecipe.m_EnergyType;
			}
			else
			{
				m_ConsumeProgress.ClearRecipe();
			}
			m_WantedItems.Clear();
			if (msg.m_WantedItems != null)
			{
				m_WantedItems.AddRange(msg.m_WantedItems);
			}
		}
	}

	private void OnMPRecipeRepeatingChanged(SetCraftingRecipeRepeatingMessage msg)
	{
		RecipeRepeating = msg.m_RecipeRepeating;
	}

	private void OnSetAnimationTriggerFromNetworkMsg(IntParamBlockMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			AnimationTriggers animationTriggers = (AnimationTriggers)msg.value;
			SetAnimationTrigger(animationTriggers, fromNetwork: true);
			m_NetClientIsOperating = (m_CurrentlyActiveTriggers & AnimationTriggers.StartOperatingTrigger) != 0;
			if ((animationTriggers & AnimationTriggers.StartConsumingTrigger) != 0)
			{
				OnStartConsuming();
			}
			if ((animationTriggers & AnimationTriggers.StartProducingTrigger) != 0)
			{
				OnStartProducing();
			}
		}
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		for (int i = 0; i < m_CorpDurationMultipliers.Length; i++)
		{
			d.AssertFormat(m_CorpDurationMultipliers[i].Corporation != FactionSubTypes.NULL, "Corp Duration Multiplier specified with NULL corporation set on Item consumer [{0}]! Cannot set an override on a NULL corp!", base.gameObject.name);
			d.AssertFormat(!m_DurationMultipliersCorpLookup.ContainsKey(m_CorpDurationMultipliers[i].Corporation), "Corp [{0}] Duration Multiplier specified more than once on module item consumer [{1}]! Can only have one!", m_CorpDurationMultipliers[i].Corporation, base.gameObject.name);
			m_DurationMultipliersCorpLookup.Add(m_CorpDurationMultipliers[i].Corporation, m_CorpDurationMultipliers[i]);
		}
		for (int j = 0; j < m_CorpOutputEfficiencies.Length; j++)
		{
			d.AssertFormat(m_CorpOutputEfficiencies[j].Corporation != FactionSubTypes.NULL, "Corp Output Efficiency specified with NULL corporation set on Item consumer [{0}]! Cannot set an override on a NULL corp!", base.gameObject.name);
			d.AssertFormat(!m_OutputEfficienciesCorpLookup.ContainsKey(m_CorpOutputEfficiencies[j].Corporation), "Corp [{0}] Output Efficiency specified more than once on module item consumer [{1}]! Can only have one!", m_CorpOutputEfficiencies[j].Corporation, base.gameObject.name);
			m_OutputEfficienciesCorpLookup.Add(m_CorpOutputEfficiencies[j].Corporation, m_CorpOutputEfficiencies[j]);
		}
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.PreDetachEvent.Subscribe(OnPreDetach);
		m_Holder.SetAcceptFilterCallback(CanAcceptItem);
		m_Holder.SetReleaseFilterCallback(CanReleaseItem);
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Holder.ItemRequestHandler = this;
		m_HolderBeam = GetComponent<ModuleItemHolderBeam>();
		m_Pickup = GetComponent<ModuleItemPickup>();
		if ((bool)m_Pickup)
		{
			m_Pickup.SetPickupFilterCallback(HandlePickupFilterCallback);
		}
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		if ((bool)m_ModuleEnergy)
		{
			m_ModuleEnergy.UpdateSupplyEvent.Subscribe(OnUpdateSupplyEnergy);
		}
		m_Animator = GetComponent<ModuleAnimator>();
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
		m_AllRecipes = new RecipeIterator(m_RecipeList);
		m_BaseRecipeProvider = GetComponent<ModuleRecipeProvider>();
		m_SyncedRecipeProgress = new NetworkedProperty<ItemConsumeProgressMessage>(this, TTMsgType.UpdateItemConsumeProgress, OnMPRecipeProgressMessageReceived);
		m_SyncedRecipeRepeating = new NetworkedProperty<SetCraftingRecipeRepeatingMessage>(this, TTMsgType.SetCraftingRecipeRepeating, OnMPRecipeRepeatingChanged);
		m_SyncedConsumeAnimations = new NetworkedProperty<IntParamBlockMessage>(this, TTMsgType.PlayConsumeAnimation, OnSetAnimationTriggerFromNetworkMsg);
		if (m_BaseRecipeProvider != null && !m_AcceptsRecipeDonges)
		{
			AddRecipes(m_BaseRecipeProvider);
			Singleton.Manager<ManMods>.inst.BlocksModifiedEvent.Subscribe(OnModBlocksChanged);
		}
		List<ModuleItemHolder.Stack> list = new List<ModuleItemHolder.Stack>();
		InitStackAndAddToList(m_Input, list);
		InitStackAndAddToList(m_Input2, list);
		InitStackAndAddToList(m_Input3, list);
		InitStackAndAddToList(m_Input4, list);
		m_InputStacks = list.ToArray();
		d.Assert(m_InputStacks.Count() > 0, "ERROR: Consume block \"" + base.block.name + "\" has no valid input stack");
		m_Consume.InitReference(m_Holder);
		d.Assert(m_Consume.stack != null, "ERROR: Consume block \"" + base.block.name + "\" cannot find its consume stack");
		m_Operate.InitReference(m_Holder);
		m_Output.InitReference(m_Holder);
		m_ItemListRequest = new ItemListRequest(m_Input.stack);
	}

	private void OnSpawn()
	{
		m_ConsumeProgress = Progress.CreateNew();
		if (m_RefreshBaseRecipes)
		{
			m_RecipeList.Clear();
			m_RecipeDuplicateCount.Clear();
			AddRecipes(m_BaseRecipeProvider);
			m_RefreshBaseRecipes = false;
		}
		ResetState();
		UpdateNetworkedState();
	}

	private void OnRecycle()
	{
		OnRecycled.Send();
		m_ItemListRequest.Clear();
		ResetState();
		m_TechDonglesDirty = false;
		m_HasDirtyNetState = false;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnUpdate()
	{
		if (base.block.IsAttached)
		{
			for (int i = 0; i < m_InputStacks.Length; i++)
			{
				ModuleItemHolder.Stack stack = m_InputStacks[i];
				for (int j = 0; j < stack.connectedNeighbourStacks.Length; j++)
				{
					if (stack.connectedNeighbourStacks[j] != null)
					{
						base.block.tank.Holders.UpdateStackArrow(stack, j, isPullArrow: true, m_PullArrowPrefab, 15);
					}
				}
			}
			if (m_Output.stack != null)
			{
				for (int k = 0; k < m_Output.stack.connectedNeighbourStacks.Length; k++)
				{
					if (m_Output.stack.connectedNeighbourStacks[k] != null)
					{
						base.block.tank.Holders.UpdateStackArrow(m_Output.stack, k, isPullArrow: false, m_PushArrowPrefab, 15);
					}
				}
			}
			if (s_MenuController == this && !DisableClosingCraftingUIWhenTooFar)
			{
				float num = Globals.inst.m_UIInteractionRange * Globals.inst.m_UIInteractionRange;
				if (Singleton.playerTank == null || (Singleton.playerPos - base.block.centreOfMassWorld).SetY(0f).sqrMagnitude > num)
				{
					Singleton.Manager<ManHUD>.inst.HideHudElement(m_MenuType);
					s_MenuController = null;
				}
			}
		}
		m_Animator.Set(m_ReadyBool, CheckAnchoredCondition());
		if (ManNetwork.IsHost && m_Holder.Antenna != null)
		{
			bool active = ThisConsumerIsReceivingAnyItemRequests() || ThisConsumerMakingAnyItemRequests();
			m_Holder.Antenna.SetActive(active);
		}
		if (m_TechDonglesDirty)
		{
			UpdateRecipesFromAttachedDongles();
			m_TechDonglesDirty = false;
		}
		if (m_AudioProvider != null)
		{
			m_AudioProvider.SetNoteOn(m_OperatingSFXType, IsOperating);
		}
		UpdateInterceptedDeliveryItem();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		if (m_Output.IsValidPos)
		{
			Gizmos.DrawWireCube(base.transform.TransformPoint(m_Output.localPos), Vector3.one * 0.3f);
		}
		if (m_ItemListRequest != null)
		{
			m_ItemListRequest.OnDrawGizmos();
		}
	}

	public void HandleExpandSearch(ItemSearcher builder, ModuleItemHolder.Stack entryStack, ModuleItemHolder.Stack prevStack, out ItemSearchAvailableItems availItems)
	{
		if (entryStack == m_Output.stack)
		{
			availItems = ItemSearchAvailableItems.ProcessedAndNonProcessed;
			if (!m_CanHonourRequests)
			{
				return;
			}
			builder.PushConverter(this);
			for (int i = 0; i < m_InputStacks.Length; i++)
			{
				ModuleItemHolder.Stack stack = m_InputStacks[i];
				ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator = stack.ConnectedStacks.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ModuleItemHolder.Stack current = enumerator.Current;
					builder.PushNode(current, stack);
				}
			}
		}
		else
		{
			availItems = ItemSearchAvailableItems.None;
		}
	}

	public void HandleSearchRequest()
	{
		if (m_CanHonourRequests)
		{
			m_LastRequestReceivedHeartbeat = base.block.tank.Holders.HeartbeatCount;
		}
	}

	public bool WantsToKnowAboutSearchRequest()
	{
		return true;
	}

	public void HandleCollectItems(ItemSearchCollector collector, bool processed)
	{
		if (processed)
		{
			if (m_Output.stack != null && !m_Output.stack.IsEmpty)
			{
				collector.OfferItem(m_Output.stack.FirstItem);
			}
			{
				foreach (ItemTypeInfo item in m_ConsumeProgress.outputQueue)
				{
					collector.OfferAnonItem(item);
				}
				return;
			}
		}
		if (!m_CanHonourRequests)
		{
			return;
		}
		if (m_ManualControl && m_ConsumeProgress.currentRecipe != null)
		{
			for (int i = 0; i < m_ConsumedInputs.Count; i++)
			{
				collector.OfferAnonItem(m_ConsumedInputs[i]);
			}
		}
		else if (!m_Consume.stack.IsEmpty)
		{
			collector.OfferItem(m_Consume.stack.FirstItem);
		}
		for (int j = 0; j < m_InputStacks.Length; j++)
		{
			ModuleItemHolder.Stack stack = m_InputStacks[j];
			if (!stack.IsEmpty)
			{
				collector.OfferItem(stack.FirstItem);
			}
		}
	}

	bool ModuleItemHolder.IStackDirection.CanReceiveOn(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return m_InputStacks.Contains(ownStack);
	}

	bool ModuleItemHolder.IStackDirection.CanOutputTo(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return ownStack == m_Output.stack;
	}

	IEnumerable<RecipeTable.Recipe> ItemSearchConverter.GetAllRecipes()
	{
		return m_AllRecipes;
	}

	public RecipeTable.Recipe GetRecipeProducing(ItemTypeInfo outputType)
	{
		RecipeTable.Recipe value = null;
		m_RecipeOutputs.TryGetValue(outputType, out value);
		return value;
	}

	public bool ConvertsFromType(ItemTypeInfo inputType)
	{
		if (m_RecipeInputs != null)
		{
			return m_RecipeInputs.Contains(inputType);
		}
		return false;
	}

	public bool ConvertsToType(ItemTypeInfo outputType)
	{
		if (m_CanHonourRequests)
		{
			return m_RecipeOutputs.ContainsKey(outputType);
		}
		return false;
	}

	public bool CanAcceptRecipeRequest(RecipeTable.Recipe recipe)
	{
		if (m_ConsumeProgress.requester == Progress.RecipeRequester.User)
		{
			return false;
		}
		if (m_ConsumeProgress.requester == Progress.RecipeRequester.Block)
		{
			if (OneRecipeAtATime)
			{
				return (base.block.tank.Holders.HeartbeatCount - m_LastBuildRequestHeartbeat) switch
				{
					0 => false, 
					1 => recipe == m_ConsumeProgress.currentRecipe, 
					_ => true, 
				};
			}
			return true;
		}
		return true;
	}

	public void MakeRecipeRequest(RecipeTable.Recipe recipe, Bitfield<int> inputItemWarnings)
	{
		d.AssertFormat(CanAcceptRecipeRequest(recipe), "RegisterRecipeRequest called on {0} when it isn't available to handle a request", base.block.tank);
		int heartbeatCount = base.block.tank.Holders.HeartbeatCount;
		if (heartbeatCount != m_LastBuildRequestHeartbeat)
		{
			m_WantedItems.Clear();
			m_LastBuildRequestHeartbeat = heartbeatCount;
		}
		if (OneRecipeAtATime)
		{
			if (recipe != m_ConsumeProgress.currentRecipe)
			{
				m_ConsumeProgress.SetRecipe(recipe, Progress.RecipeRequester.Block);
			}
			if (inputItemWarnings != null)
			{
				m_ConsumeProgress.wantedItemsWarnings.Assign(inputItemWarnings);
			}
			else
			{
				m_ConsumeProgress.wantedItemsWarnings.Clear();
			}
			return;
		}
		for (int i = 0; i < recipe.m_OutputItems.Length; i++)
		{
			RecipeTable.Recipe.ItemSpec itemSpec = recipe.m_OutputItems[i];
			if (!m_WantedItems.Contains(itemSpec.m_Item))
			{
				m_WantedItems.Add(itemSpec.m_Item);
			}
		}
	}

	public bool IsHandlingRecipeRequest()
	{
		bool result = false;
		if (m_ConsumeProgress.requester == Progress.RecipeRequester.User)
		{
			result = true;
		}
		else if (m_ConsumeProgress.requester == Progress.RecipeRequester.Block)
		{
			if (OneRecipeAtATime)
			{
				int heartbeatCount = base.block.tank.Holders.HeartbeatCount;
				if (m_LastBuildRequestHeartbeat >= heartbeatCount - 1)
				{
					result = true;
				}
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	public bool AllowsMultipleRecipes()
	{
		return !OneRecipeAtATime;
	}
}
