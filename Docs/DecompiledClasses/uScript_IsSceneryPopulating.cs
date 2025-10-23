[FriendlyName("uScript_IsSceneryPopulating", "Is the scenery populating or not?")]
[NodePath("TerraTech/Actions/Terrain")]
public class uScript_IsSceneryPopulating : uScriptLogic
{
	private bool m_SceneryPopulating;

	public bool True => m_SceneryPopulating;

	public bool False => !m_SceneryPopulating;

	public void In()
	{
		m_SceneryPopulating = Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating;
	}
}
