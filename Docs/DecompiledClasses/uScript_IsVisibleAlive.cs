#define UNITY_EDITOR
public class uScript_IsVisibleAlive : uScriptLogic
{
	private bool m_Alive;

	private Visible m_Visible;

	public bool Alive => m_Alive;

	public bool Destroyed => !m_Alive;

	public void In(object visibleObject)
	{
		if (visibleObject != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject);
			if (visibleFromObject != m_Visible)
			{
				if (m_Visible != null)
				{
					m_Visible.damageable.deathEvent.Unsubscribe(OnDeath);
				}
				visibleFromObject.damageable.deathEvent.Subscribe(OnDeath);
				m_Visible = visibleFromObject;
			}
		}
		else
		{
			d.LogError("uScript_IsVisibleAlive - Visible is null");
		}
	}

	private void OnDeath(Damageable damager, ManDamage.DamageInfo damageInfo)
	{
		m_Alive = false;
	}

	public void OnDisable()
	{
		if (m_Visible != null)
		{
			m_Visible.damageable.deathEvent.Unsubscribe(OnDeath);
		}
		m_Visible = null;
		m_Alive = false;
	}
}
