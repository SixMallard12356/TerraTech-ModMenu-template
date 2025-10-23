#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;
using cakeslice;

public class ModuleMissionPrompt : Module, ManPointer.OpenMenuEventConsumer, INetworkedModule
{
	public static Event<ModuleMissionPrompt, bool> s_ResponseEvent;

	private bool m_Available;

	private bool m_SingleUse;

	private bool m_HighlightBlock;

	private LocalisedString m_BodyText;

	private LocalisedString m_AcceptButtonText;

	private LocalisedString m_RejectButtonText;

	public void SetAvailableMissionPrompt(LocalisedString bodyText, LocalisedString acceptButtonText, LocalisedString rejectButtonText, bool highlightBlock, bool singleUse)
	{
		m_BodyText = bodyText;
		m_AcceptButtonText = acceptButtonText;
		m_RejectButtonText = rejectButtonText;
		m_Available = true;
		m_SingleUse = singleUse;
		m_HighlightBlock = highlightBlock;
		UpdateAvailable(m_Available);
	}

	public void ClearAvailableMissionPrompt()
	{
		m_BodyText = null;
		m_AcceptButtonText = null;
		m_RejectButtonText = null;
		m_Available = false;
		m_SingleUse = false;
		m_HighlightBlock = false;
		UpdateAvailable(m_Available);
	}

	public bool CanOpenMenu(bool isRadial)
	{
		if (base.block.tank != null)
		{
			return m_Available;
		}
		return false;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (m_Available)
		{
			d.Assert(m_BodyText != null, "ModuleMissionPrompt is trying to show a prompt with no text");
			((UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen)).Set((m_BodyText != null) ? m_BodyText.Value : "", AcceptHandler, RejectHandler, (m_AcceptButtonText != null) ? m_AcceptButtonText.Value : null, (m_RejectButtonText != null) ? m_RejectButtonText.Value : null);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			return true;
		}
		return false;
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleMissionPrompt;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(m_Available);
		if (m_Available)
		{
			writer.Write(m_BodyText);
			writer.Write(m_AcceptButtonText);
			writer.Write(m_RejectButtonText);
			writer.Write(m_HighlightBlock);
			writer.Write(m_SingleUse);
		}
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_Available = reader.ReadBoolean();
		if (m_Available)
		{
			m_BodyText = reader.ReadLocalisedString();
			m_AcceptButtonText = reader.ReadLocalisedString();
			m_RejectButtonText = reader.ReadLocalisedString();
			m_HighlightBlock = reader.ReadBoolean();
			m_SingleUse = reader.ReadBoolean();
		}
		else
		{
			m_BodyText = null;
			m_AcceptButtonText = null;
			m_RejectButtonText = null;
			m_HighlightBlock = false;
			m_SingleUse = false;
		}
		UpdateAvailable(m_Available);
	}

	private void AcceptHandler()
	{
		if (base.block != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (base.block.tank != null)
				{
					MissionPromptResponseMessage message = new MissionPromptResponseMessage
					{
						m_Accepted = true,
						m_BlockPoolID = base.block.blockPoolID
					};
					Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SendMissionPromptResponse, message, base.block.tank.netTech.netId);
				}
			}
			else
			{
				HandlePromptResponse(accepted: true);
			}
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void RejectHandler()
	{
		if (base.block != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (base.block.tank != null)
				{
					MissionPromptResponseMessage message = new MissionPromptResponseMessage
					{
						m_Accepted = false,
						m_BlockPoolID = base.block.blockPoolID
					};
					Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SendMissionPromptResponse, message, base.block.tank.netTech.netId);
				}
			}
			else
			{
				HandlePromptResponse(accepted: false);
			}
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void UpdateAvailable(bool isAvailable)
	{
		if (isAvailable && m_HighlightBlock)
		{
			UIBouncingArrow.BouncingArrowContext bouncingArrowContext = new UIBouncingArrow.BouncingArrowContext
			{
				targetTransform = base.block.visible.trans,
				targetOffset = Vector3.zero,
				forTime = -1f
			};
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BouncingArrow, bouncingArrowContext);
			base.block.visible.EnableOutlineGlow(enable: true, Outline.OutlineEnableReason.ScriptHighlight);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BouncingArrow);
			base.block.visible.EnableOutlineGlow(enable: false, Outline.OutlineEnableReason.ScriptHighlight);
		}
	}

	public void HandlePromptResponse(bool accepted)
	{
		if (m_Available)
		{
			s_ResponseEvent.Send(this, accepted);
			if (accepted && m_SingleUse)
			{
				m_Available = false;
				UpdateAvailable(isAvailable: false);
				if (base.block.tank != null && base.block.tank.netTech != null)
				{
					base.block.tank.netTech.SetModuleDirty(this);
				}
			}
		}
		else
		{
			d.Log("Block prompt response has been disregarded, as it is no longer active.");
		}
	}

	private void OnRecycle()
	{
		ClearAvailableMissionPrompt();
	}
}
