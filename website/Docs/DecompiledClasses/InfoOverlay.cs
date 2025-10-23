#define UNITY_EDITOR
using System;
using UnityEngine;

public class InfoOverlay : Overlay
{
	private InfoOverlayData m_Data;

	private Visible.WeakReference m_Subject = new Visible.WeakReference();

	private InfoOverlayDataValues m_DataVO;

	private bool m_PanelNeedsSetup;

	private InfoPanel m_PanelInst;

	private float m_TimeLeftOnScreen;

	private bool m_DragCallbackEnabled;

	private bool m_WantsToDisplay;

	private float m_SubtitleTickerStartTime;

	public Visible Subject => m_Subject.Get();

	public bool ExpandedByDefault { get; set; }

	public bool ShowWhileBuilding { get; set; }

	private float Priority
	{
		get
		{
			if (!m_WantsToDisplay)
			{
				return float.MinValue;
			}
			return m_Data.m_Priority;
		}
	}

	public bool PanelNeedsSetup
	{
		get
		{
			return m_PanelNeedsSetup;
		}
		set
		{
			m_PanelNeedsSetup = value;
		}
	}

	public InfoOverlay(InfoOverlayData data)
	{
		m_Data = data;
		m_DataVO = new InfoOverlayDataValues(data);
		m_DataVO.Reset();
	}

	public bool HasPriorityOver(InfoOverlay other)
	{
		return Priority > other.Priority;
	}

	public void ResetDismissTimer()
	{
		m_TimeLeftOnScreen = m_Data.m_DissappearDelay;
	}

	public bool CanShowNow()
	{
		if (ShowWhileBuilding)
		{
			return true;
		}
		return Singleton.Manager<ManPointer>.inst.DraggingItem == null;
	}

	public void Setup(Visible subject)
	{
		if (m_Subject != null && m_Subject.Get() == subject)
		{
			return;
		}
		m_DataVO.Reset();
		m_DataVO.m_SubjectType = ((subject != null) ? subject.type : ObjectTypes.Null);
		m_DataVO.m_MainTitle = ((subject != null) ? StringLookup.GetItemName(subject.m_ItemType) : string.Empty);
		if (m_Data.m_Expandable)
		{
			string text = null;
			string text2 = null;
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 1, new Localisation.GlyphInfo(27));
				text2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 3, new Localisation.GlyphInfo(27));
			}
			else
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 0);
				string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 2);
				string text3 = Singleton.Manager<ManInput>.inst.GetKeyBoundPrimaryName(27);
				if (text3 == "I")
				{
					text3 = "i";
				}
				text = string.Format(localisedString, text3);
				text2 = string.Format(localisedString2, text3);
			}
			m_DataVO.m_Subtitle = text;
			m_DataVO.m_SubtitleExpanded = text2;
		}
		m_DataVO.m_Description = ((subject != null) ? StringLookup.GetItemDescription(subject.m_ItemType) : "");
		if (subject != null)
		{
			switch (subject.type)
			{
			case ObjectTypes.Block:
			{
				TankBlock block = subject.block;
				d.Assert(block != null, "Visible defined as block doesn't have a valid block reference");
				m_DataVO.m_BlockType = block.BlockType;
				BlockAttributes[] blockAttributes = Singleton.Manager<ManSpawn>.inst.GetBlockAttributes(block.BlockType);
				foreach (BlockAttributes blockAttributes2 in blockAttributes)
				{
					Sprite blockAttributeIcon = Singleton.Manager<ManUI>.inst.GetBlockAttributeIcon(blockAttributes2);
					string blockAttribute = StringLookup.GetBlockAttribute(blockAttributes2);
					m_DataVO.AddBlockItemAttributes(blockAttributeIcon, blockAttribute);
				}
				BlockControlAttributes[] blockControlAttributes = Singleton.Manager<ManSpawn>.inst.GetBlockControlAttributes(block.BlockType);
				foreach (BlockControlAttributes blockControlAttributes2 in blockControlAttributes)
				{
					StringLookup.GetBlockControlAttribute(blockControlAttributes2);
					string text4 = string.Empty;
					switch (blockControlAttributes2)
					{
					case BlockControlAttributes.Boost:
					case BlockControlAttributes.Spin:
					case BlockControlAttributes.Thrust:
					case BlockControlAttributes.SpinThrust:
						text4 = GetInfoStringForBooster(blockControlAttributes2, block, m_DataVO);
						break;
					default:
						d.LogError("InfoOverlay doesn't know how to hanle the BlockControlAttribute: " + blockControlAttributes2);
						break;
					case BlockControlAttributes.deprecated_MenuOptions:
					case BlockControlAttributes.deprecated_RadialOptions:
						break;
					}
					if (!string.IsNullOrEmpty(text4))
					{
						m_DataVO.AddControlItemAttribute(text4);
					}
				}
				if (GameCursor.s_LastCursorState == GameCursor.CursorState.OverInteractableGrabbable)
				{
					string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.BlockUI, 15);
					if (block.tank != null)
					{
						m_DataVO.OverrideSubtitle = true;
					}
					Localisation.GlyphInfo glyphInfo = new Localisation.GlyphInfo();
					glyphInfo.m_RewiredAction = 59;
					if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
					{
						m_DataVO.AddControlItemAttribute(Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(localisedString3, glyphInfo));
						break;
					}
					string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 5);
					m_DataVO.AddControlItemAttribute(string.Format(localisedString3, localisedString4));
				}
				break;
			}
			case ObjectTypes.Chunk:
			{
				ResourcePickup component = subject.GetComponent<ResourcePickup>();
				d.Assert(component != null, "Visible defined as chunk doesn't have a valid chunk reference");
				int hashCode = subject.m_ItemType.GetHashCode();
				ChunkCategory descriptorFlags = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode);
				m_DataVO.IconSprite = Singleton.Manager<ManUI>.inst.GetChunkCategoryIcon(descriptorFlags);
				ChunkCategory chunkDominantCategory = Singleton.Manager<RecipeManager>.inst.GetChunkDominantCategory(descriptorFlags);
				m_DataVO.m_Category = StringLookup.GetChunkCategoryName(chunkDominantCategory);
				m_DataVO.m_Rarity = StringLookup.GetChunkRarityName(component.ChunkRarity);
				m_DataVO.m_RarityIcon = Singleton.Manager<ManUI>.inst.GetChunkRarityIcon(component.ChunkRarity);
				int itemValue = subject.m_ItemType.GetItemValue();
				m_DataVO.m_Price = Singleton.Manager<Localisation>.inst.GetMoneyStringWhenSelling(itemValue);
				break;
			}
			case ObjectTypes.Scenery:
			{
				ResourceDispenser resdisp = subject.resdisp;
				if (resdisp != null)
				{
					m_DataVO.m_DamageableType = resdisp.visible.damageable.DamageableType;
					foreach (ChunkTypes item in resdisp.AllDispensableItems())
					{
						Sprite sprite = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Chunk, (int)item);
						string itemName = StringLookup.GetItemName(ObjectTypes.Chunk, (int)item);
						m_DataVO.AddBlockItemAttributes(sprite, itemName, Color.white);
					}
				}
				else
				{
					d.LogError("Visible defined as ResourceDispencer/Scenery doesn't have a valid resdisp reference");
				}
				break;
			}
			}
		}
		m_Subject.Set(subject);
		PanelNeedsSetup = true;
		SetBlockDraggedCallbackEnabled(subject != null && m_Data.m_DismissWhenGrabbed);
		m_WantsToDisplay = CheckWantsToDisplay(subject);
	}

	public void OverrideDataHook(Func<InfoOverlayDataValues, InfoOverlayDataValues> transform)
	{
		if (transform != null)
		{
			m_DataVO = transform(m_DataVO);
		}
		PanelNeedsSetup = true;
	}

	public void Clear()
	{
		Setup(null);
	}

	public void OnShow()
	{
		ResetDismissTimer();
	}

	public void OnHide()
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.Recycle();
			m_Data.DecreaseCount();
			m_PanelInst = null;
		}
	}

	public override void Update()
	{
		Visible visible = m_Subject.Get();
		m_WantsToDisplay = CheckWantsToDisplay(visible);
		if (visible != null && m_WantsToDisplay)
		{
			if (m_PanelInst == null)
			{
				m_PanelInst = m_Data.m_PanelPrefab.Spawn();
				m_Data.IncreaseCount();
				m_PanelInst.SetFolded(folded: true);
				PanelNeedsSetup = true;
			}
			if (m_PanelInst != null)
			{
				if (PanelNeedsSetup)
				{
					m_PanelInst.Setup(m_DataVO);
					m_SubtitleTickerStartTime = Time.time;
					m_PanelInst.DescriptionScroller.Reset();
					if (m_Data.m_Expandable)
					{
						m_PanelInst.SetFolded(!ExpandedByDefault);
					}
				}
				if (m_PanelInst.PointerInside)
				{
					ResetDismissTimer();
				}
				bool flag = UIHelpers.TryMoveRectToWorldPos(m_PanelInst.Rect, visible.trans.position, m_Data.m_ZPos);
				if (m_Data.m_DismissWhenOffScreen && !flag)
				{
					m_Subject.Set(null);
				}
				else
				{
					bool flag2 = flag && !m_PanelInst.ShouldHide;
					if (flag2 != m_PanelInst.gameObject.activeSelf)
					{
						m_PanelInst.gameObject.SetActive(flag2);
					}
					if (flag2)
					{
						Singleton.Manager<ManLineRenderer>.inst.AddLineForOverlay(m_PanelInst.LineStartPos, m_Data.m_ZPos, visible.trans.position);
					}
					if (!PanelNeedsSetup && m_Data.m_Expandable && Singleton.Manager<ManInput>.inst.GetButtonDown(27))
					{
						m_PanelInst.SetFolded(!m_PanelInst.IsFolded);
						if (m_PanelInst.IsFolded)
						{
							m_SubtitleTickerStartTime = Time.time;
							m_PanelInst.DescriptionScroller.Reset();
						}
					}
					if (m_PanelInst.IsFolded && m_DataVO.OverrideSubtitle)
					{
						int num = m_DataVO.m_ControlAttributes.Count + 1;
						int num2 = Mathf.FloorToInt((Time.time - m_SubtitleTickerStartTime) / m_Data.m_SubtitleTickerDuration) % num;
						string empty = string.Empty;
						empty = ((num2 >= m_DataVO.m_ControlAttributes.Count) ? m_DataVO.m_Subtitle : m_DataVO.m_ControlAttributes[num2].m_Title);
						m_PanelInst.SetupSubtitle(empty, m_DataVO.m_SubtitleExpanded);
					}
				}
				PanelNeedsSetup = false;
			}
		}
		m_TimeLeftOnScreen -= Time.deltaTime;
		if (m_TimeLeftOnScreen <= 0f)
		{
			m_Subject.Set(null);
		}
	}

	public override bool HasExpired()
	{
		return Subject == null;
	}

	public override void PerformCleanup()
	{
		OnHide();
		SetBlockDraggedCallbackEnabled(enableCallback: false);
	}

	private void SetBlockDraggedCallbackEnabled(bool enableCallback)
	{
		if (m_DragCallbackEnabled != enableCallback)
		{
			if (enableCallback)
			{
				Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(DragEventHandler);
			}
			else
			{
				Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(DragEventHandler);
			}
			m_DragCallbackEnabled = enableCallback;
		}
	}

	private bool CheckWantsToDisplay(Visible subject)
	{
		if (subject != null && m_Data.VisibleInCurrentMode)
		{
			return (Singleton.cameraTrans.position - subject.trans.position).sqrMagnitude <= m_Data.m_PanelMaxDisplayDistance * m_Data.m_PanelMaxDisplayDistance;
		}
		return false;
	}

	private static string GetInfoStringForBooster(BlockControlAttributes attr, TankBlock block, InfoOverlayDataValues dataVO)
	{
		string result = string.Empty;
		string text = StringLookup.GetBlockControlAttribute(attr);
		MovementAxis movementAxis = MovementAxis.BoostJets;
		ModuleBooster component = block.GetComponent<ModuleBooster>();
		if (block.tank != null && component != null && component.IsRotor)
		{
			Vector3 vector = block.tank.rootBlockTrans.InverseTransformDirection(component.RotorDefaultDirection);
			movementAxis = MovementAxis.MoveY_MoveUp;
			LocalisationEnums.InfoOverlays stringID = LocalisationEnums.InfoOverlays.ThrottleControlY;
			if (Mathf.Abs(vector.x) > 0.7f)
			{
				movementAxis = MovementAxis.MoveX_MoveRight;
				stringID = LocalisationEnums.InfoOverlays.ThrottleControlX;
			}
			else if (Mathf.Abs(vector.z) > 0.7f)
			{
				movementAxis = MovementAxis.MoveZ_MoveForward;
				stringID = LocalisationEnums.InfoOverlays.ThrottleControlZ;
			}
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, (int)stringID);
		}
		else if (attr == BlockControlAttributes.Spin || attr == BlockControlAttributes.SpinThrust)
		{
			movementAxis = MovementAxis.BoostPropellers;
		}
		if (block.IsAttached && block.tank.control.ActiveScheme != null)
		{
			AxisMapping axisMapping = block.tank.control.ActiveScheme.GetAxisMapping(movementAxis);
			dataVO.OverrideSubtitle = true;
			if (axisMapping.m_InputAxis == InputAxisMapping.Unmapped && axisMapping.m_InputAxis2 == InputAxisMapping.Unmapped)
			{
				result = string.Format(text, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, 27));
			}
			else if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				result = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(text, new Localisation.GlyphInfo(axisMapping.GetRewiredAction()));
			}
			else
			{
				string text2 = Singleton.Manager<ManInput>.inst.GetKeyBoundPrimaryNames(axisMapping, movementAxis == MovementAxis.MoveX_MoveRight);
				if (text2.NullOrEmpty())
				{
					text2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, 27);
				}
				else if (text2 == "Left Shift" || text2 == "Right Shift")
				{
					text2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 6);
				}
				result = string.Format(text, text2);
			}
		}
		return result;
	}

	private void DragEventHandler(Visible visible, ManPointer.DragAction action, Vector3 pos)
	{
		if (action == ManPointer.DragAction.Grab && m_Subject.Get() == visible && m_Data.m_DismissWhenGrabbed)
		{
			m_Subject.Set(null);
		}
	}
}
