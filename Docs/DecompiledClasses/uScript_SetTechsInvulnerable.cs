#define UNITY_EDITOR
public class uScript_SetTechsInvulnerable : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("Set Invulnerable")]
	public void SetInvulnerable(Tank[] techs)
	{
		SetTechsInvulnerable(techs, invulnerable: true);
	}

	[FriendlyName("Set Vulnerable")]
	public void SetVulnerable(Tank[] techs)
	{
		SetTechsInvulnerable(techs, invulnerable: false);
	}

	private void SetTechsInvulnerable(Tank[] techs, bool invulnerable)
	{
		if (techs != null)
		{
			foreach (Tank tank in techs)
			{
				if (tank != null)
				{
					tank.SetInvulnerable(invulnerable, forever: true);
				}
			}
		}
		else
		{
			d.Log("uScript_SetTechsInvulnerable - techs is null");
		}
	}
}
