[FriendlyName("Spawn preset as host player")]
public class uScript_SpawnPresetAsPlayer : uScriptLogic
{
	private bool m_True;

	private bool m_Started;

	private TrackedVisible m_Tank;

	public bool Done => m_True;

	public bool NotDone => !m_True;

	public void In(TankPreset preset, PositionWithFacing playerPosition)
	{
		if (m_True)
		{
			return;
		}
		if (!m_Started)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = preset.GetTechDataFormatted(),
				blockIDs = null,
				teamID = -2,
				position = playerPosition.position,
				rotation = playerPosition.orientation,
				grounded = true
			};
			m_Tank = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
			if (Mode<ModeMain>.inst.DebugSkipTutorial)
			{
				Singleton.cameraTrans.position = playerPosition.position;
			}
			m_Started = true;
		}
		if ((bool)m_Tank.visible && !m_Tank.visible.tank.blockman.changed && m_Tank.visible.tank.grounded)
		{
			m_Tank.visible.tank.SetTeam(Singleton.Manager<ManPlayer>.inst.PlayerTeam);
			if (m_Tank.visible.tank.netTech != null)
			{
				m_Tank.visible.tank.netTech.OnServerSetTeam(Singleton.Manager<ManPlayer>.inst.PlayerTeam);
			}
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(m_Tank.visible.tank);
			m_True = true;
		}
	}

	public void OnDisable()
	{
		m_True = false;
		m_Started = false;
		m_Tank = null;
	}
}
