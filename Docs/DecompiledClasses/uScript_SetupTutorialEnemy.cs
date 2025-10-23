[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_SetupTutorialEnemy : uScriptLogic
{
	private struct EnemyBlockSetup
	{
		public float m_ThrottleModifier;
	}

	public bool Out => true;

	public void In([FriendlyName("Tech", "Tech to setup")] Tank tech, [FriendlyName("Throttle Drive Modifier", "Value to modify throttle by. 0.5 is half throttle")] float throttleDriveModifier, [FriendlyName("Throttle Turn Modifier", "Value to modify throttle by. 0.5 is half throttle")] float throttleTurnModifier)
	{
		if (tech != null)
		{
			ModuleDriveBot moduleDriveBot = tech.blockman.IterateBlockComponents<ModuleDriveBot>().FirstOrDefault();
			if ((bool)moduleDriveBot)
			{
				float throttleD = moduleDriveBot.m_DefaultThrottle * throttleDriveModifier;
				float throttleT = moduleDriveBot.m_DefaultThrottle * throttleTurnModifier;
				moduleDriveBot.SetThrottle(throttleD, throttleT);
			}
			BlockManager.BlockIterator<ModuleDamage>.Enumerator enumerator = tech.blockman.IterateBlockComponents<ModuleDamage>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.SetHealOnDetatch(1f);
			}
		}
	}
}
