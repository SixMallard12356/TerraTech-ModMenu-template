public class ModeDeathmatch : ModePVP<ModeDeathmatch>
{
	public override string GetGameMode()
	{
		return "ModeMultiplayer";
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.Deathmatch;
	}

	public override MultiplayerModeType GetMultiplayerGameType()
	{
		return MultiplayerModeType.Deathmatch;
	}

	protected override void GameModeInit()
	{
	}

	protected override void OnServerPhaseEnter(NetController.Phase phase)
	{
		base.OnServerPhaseEnter(phase);
	}

	protected override void OnClientPhaseEnter(NetController.Phase phase)
	{
		base.OnClientPhaseEnter(phase);
	}

	protected override void ServerUpdateGameMode()
	{
	}

	protected override void ClientUpdateGameMode()
	{
	}

	protected override void OnServerPlayerOutOfBounds()
	{
	}

	protected override void GameModeExit()
	{
	}
}
