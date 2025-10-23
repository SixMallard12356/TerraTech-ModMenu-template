using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UISetElementFromSKU : MonoBehaviour
{
	public enum ShowState
	{
		Interactive,
		NonInteractive
	}

	[SerializeField]
	private ShowState m_ValidState;

	[Header("BuildType")]
	[SerializeField]
	private bool m_Standard = true;

	[FormerlySerializedAs("m_Steam")]
	[Tooltip("Available on platforms that fall under the SKU.GenericPCPlatform definition")]
	[SerializeField]
	private bool m_GenericPCPlatform = true;

	[SerializeField]
	[Tooltip("For 'Steam' targetted builds, only called steam store for technical reasons")]
	private bool m_SteamStore;

	[SerializeField]
	private bool m_EpicGS;

	[SerializeField]
	private bool m_GOG = true;

	[SerializeField]
	private bool m_NetEase = true;

	[SerializeField]
	private bool m_Console = true;

	[SerializeField]
	private bool m_SwitchOverride = true;

	[SerializeField]
	[Header("GameType")]
	private bool m_Demo;

	[SerializeField]
	private bool m_Show;

	[SerializeField]
	private bool m_MainGame = true;

	[Header("DLC")]
	[SerializeField]
	private bool m_WithRandD = true;

	[SerializeField]
	private bool m_WithoutRandD = true;

	[SerializeField]
	[Header("Feature")]
	private bool m_RequireTwitter;

	[SerializeField]
	private bool m_RequireSteam;

	[Tooltip("Only available on builds for the epic game store")]
	[SerializeField]
	private bool m_RequireEpicGameStore;

	[SerializeField]
	[Tooltip("Only available on builds that use epic game services (The backend networking framework, not just epic game store)")]
	private bool m_RequireEpicOnlineServices;

	[SerializeField]
	private bool m_RequireBans;

	private void Awake()
	{
		if (IsAvailableInSKU())
		{
			base.gameObject.SetActive(value: true);
			Button component = GetComponent<Button>();
			if ((bool)component)
			{
				component.interactable = m_ValidState == ShowState.Interactive;
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public bool IsAvailableInSKU()
	{
		bool flag;
		if (SKU.SwitchUI && !m_SwitchOverride)
		{
			flag = false;
		}
		else if (SKU.ConsoleUI)
		{
			flag = m_Console;
		}
		else
		{
			_ = m_Standard;
			int num;
			if ((!m_GenericPCPlatform || !SKU.GenericPCPlatform) && (!m_SteamStore || !SKU.IsSteam) && (!m_EpicGS || !SKU.IsEpicGS))
			{
				_ = m_GOG;
				num = ((m_NetEase && SKU.IsNetEase) ? 1 : 0);
			}
			else
			{
				num = 1;
			}
			flag = (byte)num != 0;
		}
		bool flag2 = false;
		if (m_MainGame)
		{
			flag2 = true;
		}
		bool flag3 = false;
		if (Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
		{
			if (m_WithRandD)
			{
				flag3 = true;
			}
		}
		else if (m_WithoutRandD)
		{
			flag3 = true;
		}
		bool flag4 = !m_RequireTwitter && (!m_RequireSteam || SKU.IsSteam) && (!m_RequireEpicGameStore || SKU.IsEpicGS) && (!m_RequireEpicOnlineServices || SKU.UsesEOS) && (!m_RequireBans || SKU.BansEnabled);
		return flag && flag2 && flag3 && flag4;
	}

	private string GetStateString()
	{
		return $"BUILD TYPEs ~m_Standard[{m_Standard}]~ ~m_GenericPCPlatform[{m_GenericPCPlatform}]~ ~m_Steam[{m_SteamStore}]~ ~m_GOG[{m_GOG}]~ ~m_NetEase[{m_NetEase}]~ ~m_Console[{m_Console}]~ ~m_SwitchOverride[{m_SwitchOverride}]~\n" + $"GAME TYPEs ~m_Demo[{m_Demo}]~ ~m_Show[{m_Show}]~ ~m_MainGame[{m_MainGame}]~\n" + $"DLCs ~m_WithRandD[{m_WithRandD}]~ ~m_WithoutRandD[{m_WithoutRandD}]~\n" + $"FEATUREs ~m_RequireTwitter[{m_RequireTwitter}]~ ~m_RequireSteam[{m_RequireSteam}]~ ~m_RequireBans[{m_RequireBans}]~";
	}
}
