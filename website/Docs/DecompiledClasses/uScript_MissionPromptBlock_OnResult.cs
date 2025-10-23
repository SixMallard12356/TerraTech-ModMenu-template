using System;

[NodePath("TerraTech/Events/Missions")]
[FriendlyName("On Mission Prompt Result", "Is called on the host when a player responds to a mission message prompt.")]
public class uScript_MissionPromptBlock_OnResult : uScriptEvent
{
	public delegate void uScriptEventHandler(TankBlock sender, PromptResultEventArgs args);

	public class PromptResultEventArgs : EventArgs
	{
		private TankBlock m_Block;

		private bool m_Accepted;

		[FriendlyName("Block Triggered")]
		public TankBlock TankBlock => m_Block;

		[FriendlyName("Accepted")]
		public bool Accepted => m_Accepted;

		public PromptResultEventArgs(TankBlock block, bool accepted)
		{
			m_Block = block;
			m_Accepted = accepted;
		}
	}

	[FriendlyName("Response Received")]
	public event uScriptEventHandler ResponseEvent;

	public void OnSpawn()
	{
		ModuleMissionPrompt.s_ResponseEvent.Subscribe(OnResponse);
	}

	public void OnRecycle()
	{
		ModuleMissionPrompt.s_ResponseEvent.Unsubscribe(OnResponse);
	}

	private void OnResponse(ModuleMissionPrompt missionPrompt, bool accepted)
	{
		this.ResponseEvent(missionPrompt.block, new PromptResultEventArgs(missionPrompt.block, accepted));
	}
}
