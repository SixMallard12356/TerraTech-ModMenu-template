#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

public class ManCustomSkins : Singleton.Manager<ManCustomSkins>
{
	[Serializable]
	public struct CorporationSkins
	{
		public List<CorporationSkinInfo> m_SkinsInCorp;
	}

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private CorporationSkins[] m_SkinInfos;

	public Event<Tank, TankBlock> TankBlockPaintedEvent;

	private Dictionary<int, List<SkinTextures>> m_SkinTextures;

	private Dictionary<int, List<SkinMeshes>> m_SkinMeshes;

	private Dictionary<int, List<CorporationSkinUIInfo>> m_SkinUIInfos;

	private Dictionary<int, Dictionary<byte, byte>> m_SkinIDToIndexMapping;

	private Dictionary<int, Dictionary<byte, byte>> m_SkinIndexToIDMapping;

	private Dictionary<int, int> m_CorpSkinSelections;

	private FactionSubTypes m_CurrentSelectedCorp;

	private bool m_UpdateSkinsHUDVisibility;

	private static int k_CustomSkinsUnlockLevel = 1;

	public const int k_FIRST_MODDED_SKIN_ID = 32;

	public const int k_MAX_SKIN_ID = 255;

	private List<CorporationSkinInfo> m_ModdedSkins = new List<CorporationSkinInfo>();

	public void ChangeSelectedCorporation(bool next)
	{
		int num = (int)m_CurrentSelectedCorp;
		do
		{
			num += (next ? 1 : (-1));
		}
		while (!ShowCorpInUI((FactionSubTypes)num) && num != 0 && num < m_CorpSkinSelections.Count);
		if (num != 0 && num < m_CorpSkinSelections.Count)
		{
			SetCurrentSelectedCorp((FactionSubTypes)num);
		}
	}

	public bool ChangeSkinInSelectedCorp(bool next)
	{
		int num = GetCurrentSelectedSkinInCorp(m_CurrentSelectedCorp);
		do
		{
			num += (next ? 1 : (-1));
		}
		while (num >= 0 && num < GetNumSkinsInCorp(m_CurrentSelectedCorp) && m_SkinUIInfos[(int)m_CurrentSelectedCorp][num].m_SkinLocked);
		if (num >= 0 && num < GetNumSkinsInCorp(m_CurrentSelectedCorp))
		{
			SetSelectedSkinForCorp(num, m_CurrentSelectedCorp);
			return true;
		}
		return false;
	}

	public FactionSubTypes GetCurrentSelectedCorp()
	{
		return m_CurrentSelectedCorp;
	}

	public void SetCurrentSelectedCorp(FactionSubTypes corp)
	{
		m_CurrentSelectedCorp = corp;
	}

	public int GetNumSkinsInCorp(FactionSubTypes corp)
	{
		return m_SkinTextures[(int)corp].Count;
	}

	public string GetSkinNameForSnapshot(FactionSubTypes corp, uint skinID)
	{
		byte index = SkinIDToIndex((byte)skinID, corp);
		CorporationSkinUIInfo corporationSkinUIInfo = m_SkinUIInfos[(int)corp][index];
		if (corporationSkinUIInfo.m_IsModded)
		{
			return corporationSkinUIInfo.m_FallbackString;
		}
		return "";
	}

	public uint GetSkinIDFromSnapshot(FactionSubTypes corp, string skinName)
	{
		d.Assert(skinName != "", "Don't need to lookup vanilla skins when loading snapshot");
		for (int i = 0; i < m_SkinUIInfos[(int)corp].Count; i++)
		{
			CorporationSkinUIInfo corporationSkinUIInfo = m_SkinUIInfos[(int)corp][i];
			if (corporationSkinUIInfo.m_IsModded && corporationSkinUIInfo.m_FallbackString == skinName)
			{
				return SkinIndexToID((byte)i, corp);
			}
		}
		d.LogError("Could not find modded skin with name " + skinName + ". Returning default skin");
		return SkinIndexToID(0, corp);
	}

	public List<SkinTextures> GetCorpSkinTextureInfos(FactionSubTypes corp)
	{
		return m_SkinTextures[(int)corp];
	}

	public List<CorporationSkinUIInfo> GetCorpSkinUIInfos(FactionSubTypes corp)
	{
		return m_SkinUIInfos[(int)corp];
	}

	public SkinTextures GetSkinTexture(FactionSubTypes corp, int skin)
	{
		return m_SkinTextures[(int)corp][skin];
	}

	public CorporationSkinUIInfo GetSkinUIInfo(FactionSubTypes corp, int skin)
	{
		return m_SkinUIInfos[(int)corp][skin];
	}

	public void FloodFillPlayerTech()
	{
		FloodFillTech(Singleton.playerTank);
	}

	public void TryPaintBlock(TankBlock block)
	{
		FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(block.BlockType);
		if (CanPaintVisible(block.visible) && corporation == m_CurrentSelectedCorp)
		{
			DoPaintBlock(block, changeMadeByPlayer: true);
		}
	}

	public void DoPaintBlock(TankBlock block, bool changeMadeByPlayer = false)
	{
		FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(block.BlockType);
		byte b = (byte)m_CorpSkinSelections[(int)corporation];
		if (block.GetSkinIndex() == b)
		{
			return;
		}
		block.SetSkinIndex(b);
		TankBlockPaintedEvent.Send(block.tank, block);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PaintBlockSkin);
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			ReskinBlockMessage message = new ReskinBlockMessage
			{
				m_BlockPoolID = block.blockPoolID,
				m_SkinID = SkinIndexToID(b, corporation)
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ReskinBlock, message);
			if (changeMadeByPlayer && block.tank != null && block.tank.netTech != null)
			{
				block.tank.netTech.SetAuthorMultiplayerSafe(Singleton.Manager<ManNetwork>.inst.MyPlayer.name);
			}
		}
	}

	public int GetCurrentSelectedSkinInCorp(FactionSubTypes corp)
	{
		return m_CorpSkinSelections[(int)corp];
	}

	public void SetSelectedSkinForCorp(int skin, FactionSubTypes corp)
	{
		m_CorpSkinSelections[(int)corp] = skin;
		Singleton.Manager<ManPointer>.inst.ReskinPaintingBlock();
	}

	public bool ShowCorpInUI(FactionSubTypes corp)
	{
		if (Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Contains(corp))
		{
			return NumUnlockedSkinsInCorp(corp) > 1;
		}
		return false;
	}

	public void SwapSkinMeshes(TankBlock block, FactionSubTypes corp, byte skin, bool swapToDefault)
	{
		string text = block.gameObject.name;
		List<SkinMesh> meshSwaps = m_SkinMeshes[(int)corp][skin].m_MeshSwaps;
		if (meshSwaps == null)
		{
			return;
		}
		foreach (SkinMesh item in meshSwaps)
		{
			if (text == item.m_BasePrefab.name)
			{
				SwapSkinMesh(block.gameObject, swapToDefault ? item.m_BasePrefab : item.m_SkinPrefab);
			}
		}
	}

	public void FloodFillTech(Tank inpTech)
	{
		if (!CanPaintVisible(inpTech.visible))
		{
			return;
		}
		bool flag = false;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = inpTech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(current.BlockType);
			if (corporation == m_CurrentSelectedCorp)
			{
				byte b = (byte)m_CorpSkinSelections[(int)corporation];
				current.SetSkinIndex(b);
				TankBlockPaintedEvent.Send(current.tank, current);
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
				{
					ReskinBlockMessage message = new ReskinBlockMessage
					{
						m_BlockPoolID = current.blockPoolID,
						m_SkinID = SkinIndexToID(b, corporation)
					};
					Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ReskinBlock, message);
				}
				flag = true;
			}
		}
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PaintTechSkin);
		if (flag && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && (bool)inpTech.netTech)
		{
			inpTech.netTech.SetAuthorMultiplayerSafe(Singleton.Manager<ManNetwork>.inst.MyPlayer.name);
		}
	}

	public bool CanUseSkin(FactionSubTypes corp, int skinIndex)
	{
		if (skinIndex >= GetNumSkinsInCorp(corp))
		{
			d.LogError("Can't use skin with index " + skinIndex + " because the skin index doesn't exist in corporation " + corp);
			return false;
		}
		return !m_SkinUIInfos[(int)corp][skinIndex].m_SkinLocked;
	}

	public bool CanUseSkinWithUniqueID(byte skinUniqueID, BlockTypes blockType)
	{
		return Singleton.Manager<ManCustomSkins>.inst.CanUseSkin(Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType), Singleton.Manager<ManCustomSkins>.inst.SkinIDToIndex(skinUniqueID, Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType)));
	}

	public bool CanPaintVisible(Visible vis)
	{
		if (vis.IsNull())
		{
			return false;
		}
		bool flag = false;
		Tank tank = null;
		if (vis.type == ObjectTypes.Block)
		{
			if (Singleton.Manager<ManSpawn>.inst.GetCorporation(vis.block.BlockType) != m_CurrentSelectedCorp)
			{
				return false;
			}
			tank = vis.block.tank;
		}
		else
		{
			if (vis.type != ObjectTypes.Vehicle)
			{
				return false;
			}
			bool flag2 = false;
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = vis.tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				if (Singleton.Manager<ManSpawn>.inst.GetCorporation(current.BlockType) == m_CurrentSelectedCorp)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				return false;
			}
			tank = vis.tank;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return tank.IsNull() || tank.netTech.CanPlayerModify(Singleton.Manager<ManNetwork>.inst.MyPlayer);
		}
		return tank.IsNull() || tank.Team == Singleton.playerTank.Team;
	}

	public byte SkinIDToIndex(byte ID, FactionSubTypes corp)
	{
		if (m_SkinIDToIndexMapping.TryGetValue((int)corp, out var value))
		{
			if (value.ContainsKey(ID))
			{
				return value[ID];
			}
			d.LogError($"ManCustomSkins: Tried to convert invalid skin ID {ID} for corp {corp} to an index! Returning 0");
			return 0;
		}
		d.LogError("ManCustomSkins: SkinIDToIndex was given a corporation of value:" + (int)corp + ". That is not a valid corporation!");
		return 0;
	}

	public byte SkinIndexToID(byte index, FactionSubTypes corp)
	{
		if (m_SkinIndexToIDMapping.TryGetValue((int)corp, out var value))
		{
			if (value.ContainsKey(index))
			{
				return value[index];
			}
			d.LogError("ManCustomSkins: Tried to convert invalid skin index to an ID! Returning 0");
			return 0;
		}
		d.LogError("ManCustomSkins: SkinIndexToID was given a corporation of value:" + (int)corp + ". That is not a valid corporation!");
		return 0;
	}

	public void AddCorp(int corpIndex)
	{
		m_SkinTextures[corpIndex] = new List<SkinTextures>();
		m_SkinUIInfos[corpIndex] = new List<CorporationSkinUIInfo>();
		m_SkinMeshes[corpIndex] = new List<SkinMeshes>();
		m_SkinIDToIndexMapping[corpIndex] = new Dictionary<byte, byte>();
		m_SkinIndexToIDMapping[corpIndex] = new Dictionary<byte, byte>();
		m_CorpSkinSelections.Add(corpIndex, 0);
	}

	public void AddSkinToCorp(CorporationSkinInfo skin, bool isModdedSkin = false)
	{
		skin.m_SkinUIInfo.m_SkinLocked = Singleton.Manager<ManDLC>.inst.IsSkinLocked(skin.m_SkinUniqueID, skin.m_Corporation);
		m_SkinTextures[(int)skin.m_Corporation].Add(skin.m_SkinTextureInfo);
		m_SkinUIInfos[(int)skin.m_Corporation].Add(skin.m_SkinUIInfo);
		m_SkinMeshes[(int)skin.m_Corporation].Add(skin.m_SkinMeshes);
		m_SkinIDToIndexMapping[(int)skin.m_Corporation][(byte)skin.m_SkinUniqueID] = (byte)(m_SkinMeshes[(int)skin.m_Corporation].Count - 1);
		m_SkinIndexToIDMapping[(int)skin.m_Corporation][(byte)(m_SkinMeshes[(int)skin.m_Corporation].Count - 1)] = (byte)skin.m_SkinUniqueID;
		if (isModdedSkin)
		{
			m_ModdedSkins.Add(skin);
		}
	}

	public void RemoveModdedCorps()
	{
		List<int> list = new List<int>();
		foreach (int key in m_SkinUIInfos.Keys)
		{
			if (Singleton.Manager<ManMods>.inst.IsModdedCorp((FactionSubTypes)key))
			{
				list.Add(key);
			}
		}
		foreach (int item in list)
		{
			m_SkinUIInfos.Remove(item);
			m_SkinTextures.Remove(item);
			m_SkinMeshes.Remove(item);
			m_SkinIDToIndexMapping.Remove(item);
			m_SkinIndexToIDMapping.Remove(item);
			m_CorpSkinSelections.Remove(item);
		}
	}

	public void RemoveModdedSkins()
	{
		foreach (CorporationSkinInfo moddedSkin in m_ModdedSkins)
		{
			m_SkinIndexToIDMapping[(int)moddedSkin.m_Corporation].Remove((byte)(m_SkinMeshes[(int)moddedSkin.m_Corporation].Count - 1));
			m_SkinIDToIndexMapping[(int)moddedSkin.m_Corporation].Remove((byte)moddedSkin.m_SkinUniqueID);
			m_SkinMeshes[(int)moddedSkin.m_Corporation].Remove(moddedSkin.m_SkinMeshes);
			m_SkinUIInfos[(int)moddedSkin.m_Corporation].Remove(moddedSkin.m_SkinUIInfo);
			m_SkinTextures[(int)moddedSkin.m_Corporation].Remove(moddedSkin.m_SkinTextureInfo);
		}
		m_ModdedSkins.Clear();
	}

	private int NumUnlockedSkinsInCorp(FactionSubTypes corp)
	{
		int num = 0;
		foreach (CorporationSkinUIInfo item in m_SkinUIInfos[(int)corp])
		{
			if (!item.m_SkinLocked)
			{
				num++;
			}
		}
		return num;
	}

	private void UpdateSkinLocksFromDLC()
	{
		FactionSubTypes[] values = EnumValuesIterator<FactionSubTypes>.Values;
		foreach (FactionSubTypes factionSubTypes in values)
		{
			List<CorporationSkinUIInfo> list = m_SkinUIInfos[(int)factionSubTypes];
			for (int j = 0; j < list.Count; j++)
			{
				CorporationSkinUIInfo value = list[j];
				value.m_SkinLocked = Singleton.Manager<ManDLC>.inst.IsSkinLocked(SkinIndexToID((byte)j, factionSubTypes), factionSubTypes);
				list[j] = value;
				if (m_CorpSkinSelections[(int)factionSubTypes] == j && value.m_SkinLocked)
				{
					m_CorpSkinSelections[(int)factionSubTypes] = 0;
					d.Log($"[ManCustomSkins] Auto-deselect newly locked DLC corp={factionSubTypes} index={j} skinID={SkinIndexToID((byte)j, factionSubTypes)}");
				}
			}
		}
	}

	private static void SwapSkinMesh(GameObject destination, GameObject source)
	{
		MeshFilter component = destination.GetComponent<MeshFilter>();
		if ((bool)component)
		{
			MeshFilter component2 = source.GetComponent<MeshFilter>();
			if ((bool)component2)
			{
				component.sharedMesh = component2.sharedMesh;
				if (component2.CompareTag("SkinSwapMaterial"))
				{
					MeshRenderer component3 = destination.GetComponent<MeshRenderer>();
					if ((bool)component3)
					{
						MeshRenderer component4 = source.GetComponent<MeshRenderer>();
						if ((bool)component4)
						{
							component3.sharedMaterial = component4.sharedMaterial;
						}
					}
				}
			}
		}
		Collider[] components = destination.GetComponents<Collider>();
		if (components != null && components.Length != 0)
		{
			Collider[] components2 = source.GetComponents<Collider>();
			if (components2 != null && components2.Length == components.Length)
			{
				for (int i = 0; i < components2.Length; i++)
				{
					MeshCollider meshCollider = components[i] as MeshCollider;
					if ((bool)meshCollider)
					{
						MeshCollider meshCollider2 = components2[i] as MeshCollider;
						if ((bool)meshCollider2)
						{
							meshCollider.sharedMesh = meshCollider2.sharedMesh;
						}
					}
					BoxCollider boxCollider = components[i] as BoxCollider;
					if ((bool)boxCollider)
					{
						BoxCollider boxCollider2 = components2[i] as BoxCollider;
						if ((bool)boxCollider2)
						{
							boxCollider.size = boxCollider2.size;
							boxCollider.center = boxCollider2.center;
						}
					}
					CapsuleCollider capsuleCollider = components[i] as CapsuleCollider;
					if ((bool)capsuleCollider)
					{
						CapsuleCollider capsuleCollider2 = components2[i] as CapsuleCollider;
						if ((bool)capsuleCollider2)
						{
							capsuleCollider.radius = capsuleCollider2.radius;
							capsuleCollider.height = capsuleCollider2.height;
							capsuleCollider.center = capsuleCollider2.center;
							capsuleCollider.direction = capsuleCollider2.direction;
						}
					}
				}
			}
		}
		ColliderSwapper component5 = destination.GetComponent<ColliderSwapper>();
		if ((bool)component5)
		{
			ColliderSwapper component6 = source.GetComponent<ColliderSwapper>();
			if ((bool)component6)
			{
				component5.CopyColliderShapes(component6);
			}
		}
		Animator component7 = destination.GetComponent<Animator>();
		if ((bool)component7)
		{
			Animator component8 = source.GetComponent<Animator>();
			if ((bool)component8)
			{
				component7.avatar = component8.avatar;
				component7.runtimeAnimatorController = component8.runtimeAnimatorController;
			}
		}
		FireData component9 = destination.GetComponent<FireData>();
		if ((bool)component9)
		{
			FireData component10 = source.GetComponent<FireData>();
			if ((bool)component10)
			{
				component9.m_BulletPrefab = component10.m_BulletPrefab;
				component9.m_BulletCasingPrefab = component10.m_BulletCasingPrefab;
			}
		}
		Light component11 = destination.GetComponent<Light>();
		if ((bool)component11)
		{
			Light component12 = source.GetComponent<Light>();
			if ((bool)component12)
			{
				component11.type = component12.type;
				component11.range = component12.range;
				component11.color = component12.color;
				component11.intensity = component12.intensity;
				component11.spotAngle = component12.spotAngle;
				component11.flare = component12.flare;
			}
		}
		ParticleSystem component13 = destination.GetComponent<ParticleSystem>();
		if ((bool)component13)
		{
			ParticleSystem component14 = source.GetComponent<ParticleSystem>();
			if ((bool)component14)
			{
				ParticleSystem.EmissionModule emission = component13.emission;
				emission.enabled = component14.emission.enabled;
			}
		}
		FanJet component15 = destination.GetComponent<FanJet>();
		if ((bool)component15)
		{
			FanJet component16 = source.GetComponent<FanJet>();
			if ((bool)component16)
			{
				component15.m_DisableTrail = component16.m_DisableTrail;
			}
		}
		Spinner component17 = destination.GetComponent<Spinner>();
		if ((bool)component17)
		{
			component17.ReInit();
		}
		ModuleBooster component18 = destination.GetComponent<ModuleBooster>();
		if ((bool)component18)
		{
			ModuleBooster component19 = source.GetComponent<ModuleBooster>();
			if ((bool)component19)
			{
				component18.CopyAudioType(component19);
			}
		}
		ModuleHover component20 = destination.GetComponent<ModuleHover>();
		if ((bool)component20)
		{
			ModuleHover component21 = source.GetComponent<ModuleHover>();
			if ((bool)component21)
			{
				component20.CopyAudioType(component21);
			}
		}
		int childCount = source.transform.childCount;
		int childCount2 = destination.transform.childCount;
		int num = -1;
		for (int j = 0; j < childCount; j++)
		{
			GameObject gameObject = source.transform.GetChild(j).gameObject;
			GameObject gameObject2 = null;
			while (++num < childCount2)
			{
				GameObject gameObject3 = destination.transform.GetChild(num).gameObject;
				if (gameObject3.name == gameObject.name)
				{
					gameObject2 = gameObject3;
					break;
				}
			}
			if ((bool)gameObject2)
			{
				gameObject2.transform.localPosition = gameObject.transform.localPosition;
				gameObject2.transform.localRotation = gameObject.transform.localRotation;
				gameObject2.transform.localScale = gameObject.transform.localScale;
				SwapSkinMesh(gameObject2, gameObject);
			}
		}
	}

	[Conditional("USE_ANALYTICS")]
	private void SendSkinPaintingAnalyticsEvent(string eventName)
	{
		int currentSelectedSkinInCorp = GetCurrentSelectedSkinInCorp(m_CurrentSelectedCorp);
		int num = SkinIndexToID((byte)currentSelectedSkinInCorp, m_CurrentSelectedCorp);
		Dictionary<string, object> dictionary = new Dictionary<string, object>
		{
			{
				"corporation",
				m_CurrentSelectedCorp.ToString()
			},
			{
				"skinID",
				m_CurrentSelectedCorp.ToString() + num
			},
			{
				"game_mode",
				Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().ToString()
			}
		};
		foreach (CorporationSkinInfo moddedSkin in m_ModdedSkins)
		{
			if (moddedSkin.m_Corporation == m_CurrentSelectedCorp && moddedSkin.m_SkinUniqueID == num)
			{
				dictionary.Add("moddedskin_name", moddedSkin.name);
			}
		}
	}

	private void RegisterMessageHandlers()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.ReskinBlock, OnServerReskinBlockMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ReskinBlock, OnClientReskinBlockMessage);
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Subscribe(OnLevelUp);
	}

	private void OnServerReskinBlockMessage(NetworkMessage netMsg)
	{
		ReskinBlockMessage reskinBlockMessage = netMsg.ReadMessage<ReskinBlockMessage>();
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(reskinBlockMessage.m_BlockPoolID);
		if (tankBlock.IsNotNull())
		{
			tankBlock.SetSkinByUniqueID(reskinBlockMessage.m_SkinID);
			if (tankBlock.netBlock.IsNotNull())
			{
				tankBlock.netBlock.OnServerSetSkinID(reskinBlockMessage.m_SkinID);
			}
			else if (tankBlock.tank.IsNotNull())
			{
				tankBlock.tank.netTech.SaveTechData();
			}
			else
			{
				d.LogError("Block with no NetBlock or Tank found in ManCustomSkins!");
			}
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.ReskinBlock, reskinBlockMessage);
		}
	}

	private void OnClientReskinBlockMessage(NetworkMessage netMsg)
	{
		ReskinBlockMessage reskinBlockMessage = netMsg.ReadMessage<ReskinBlockMessage>();
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(reskinBlockMessage.m_BlockPoolID);
		if (tankBlock.IsNotNull())
		{
			tankBlock.SetSkinByUniqueID(reskinBlockMessage.m_SkinID);
		}
	}

	private void OnModeStart(Mode mode)
	{
		m_UpdateSkinsHUDVisibility = true;
	}

	private void OnLevelUp(FactionSubTypes corp, int level)
	{
		m_UpdateSkinsHUDVisibility = true;
	}

	private void UpdateSkinsHUDButtonVisibility()
	{
		if (!Singleton.Manager<ManHUD>.inst.IsVisible || !Singleton.Manager<ManHUD>.inst.CurrentHUD.IsNotNull())
		{
			return;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.MainGame)
		{
			if (Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(FactionSubTypes.GSO) < k_CustomSkinsUnlockLevel && Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(FactionSubTypes.GSO) != -1)
			{
				if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
				{
					Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
				}
			}
			else if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			}
		}
		m_UpdateSkinsHUDVisibility = false;
	}

	private void Awake()
	{
		m_CorpSkinSelections = new Dictionary<int, int>();
		d.Assert(m_SkinInfos.Length == EnumValuesIterator<FactionSubTypes>.Values.Length);
		m_SkinTextures = new Dictionary<int, List<SkinTextures>>();
		m_SkinUIInfos = new Dictionary<int, List<CorporationSkinUIInfo>>();
		m_SkinMeshes = new Dictionary<int, List<SkinMeshes>>();
		m_SkinIndexToIDMapping = new Dictionary<int, Dictionary<byte, byte>>();
		m_SkinIDToIndexMapping = new Dictionary<int, Dictionary<byte, byte>>();
		FactionSubTypes[] values = EnumValuesIterator<FactionSubTypes>.Values;
		foreach (FactionSubTypes factionSubTypes in values)
		{
			m_SkinTextures.Add((int)factionSubTypes, new List<SkinTextures>());
			m_SkinUIInfos.Add((int)factionSubTypes, new List<CorporationSkinUIInfo>());
			m_SkinMeshes.Add((int)factionSubTypes, new List<SkinMeshes>());
			m_SkinIndexToIDMapping.Add((int)factionSubTypes, new Dictionary<byte, byte>());
			m_SkinIDToIndexMapping.Add((int)factionSubTypes, new Dictionary<byte, byte>());
			m_CorpSkinSelections.Add((int)factionSubTypes, 0);
			foreach (CorporationSkinInfo item in m_SkinInfos[(int)factionSubTypes].m_SkinsInCorp)
			{
				AddSkinToCorp(item);
			}
		}
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Subscribe(UpdateSkinLocksFromDLC);
	}

	private void Start()
	{
		RegisterMessageHandlers();
	}

	private void Update()
	{
		if (m_UpdateSkinsHUDVisibility)
		{
			UpdateSkinsHUDButtonVisibility();
		}
	}
}
