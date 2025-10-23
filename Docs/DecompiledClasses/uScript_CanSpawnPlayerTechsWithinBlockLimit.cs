#define UNITY_EDITOR
[FriendlyName("uScript_CanSpawnPlayerTechsWithinBlockLimit", "Check to see whether the given number of player techs can be spawned, but stay within the limit")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_CanSpawnPlayerTechsWithinBlockLimit : uScriptLogic
{
	private SpawnTechData[] m_CachedData;

	private int m_CachedCost;

	private bool m_Result;

	public bool Out => true;

	public bool True => m_Result;

	public bool False => !m_Result;

	public void In([FriendlyName("Tech Data", "The tech data to check the limit on")] SpawnTechData[] spawnData, [FriendlyName("Count", "The number of techs we propose to spawn")] int count = 1)
	{
		if (Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			if (m_CachedData != spawnData)
			{
				m_CachedData = spawnData;
				m_CachedCost = 0;
				for (int i = 0; i < spawnData.Length; i++)
				{
					TechData specificTechData = spawnData[i].SpecificTechData;
					if (specificTechData == null)
					{
						d.LogError($"CanSpawnPlayerTechsWithinBlockLimit cannot calculate cost of spawnData[{i}] ({spawnData[i].UniqueName}) because it does not have a specific tech to spawn");
						m_CachedCost = Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage;
						break;
					}
					m_CachedCost += Singleton.Manager<ManBlockLimiter>.inst.GetTechCost(specificTechData, out var _);
				}
			}
			m_Result = Singleton.Manager<ManBlockLimiter>.inst.AllowCreatePlayerTech(m_CachedCost * count);
		}
		else
		{
			m_Result = true;
		}
	}

	public void OnDisable()
	{
		m_CachedData = null;
		m_CachedCost = 0;
		m_Result = false;
	}
}
