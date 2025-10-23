#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class uScript_DamageTechs : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(Tank[] techs, [DefaultValue(100f)] float dmgPercent, [DefaultValue(false)] bool givePlyrCredit, [DefaultValue(0f)] float leaveBlksPercent, [DefaultValue(false)] bool makeVulnerable = false)
	{
		if (techs != null)
		{
			float num = ((dmgPercent >= 100f) ? 999f : (dmgPercent / 100f));
			for (int i = 0; i < techs.Length; i++)
			{
				if (techs[i] != null)
				{
					List<TankBlock> list = new List<TankBlock>(techs[i].blockman.blockCount);
					BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = techs[i].blockman.IterateBlocks().GetEnumerator();
					while (enumerator.MoveNext())
					{
						TankBlock current = enumerator.Current;
						if (leaveBlksPercent > 0f)
						{
							if (current.IsController || ((bool)current.Anchor && current.Anchor.IsAnchored))
							{
								list.Add(current);
							}
							else if (leaveBlksPercent < 100f && Random.Range(0f, 99.99f) >= leaveBlksPercent)
							{
								list.Add(current);
							}
						}
						else
						{
							list.Add(current);
						}
					}
					Tank sourceTank = null;
					if (givePlyrCredit)
					{
						List<Tank> allPlayerTechs = Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs();
						sourceTank = ((Singleton.playerTank != null) ? Singleton.playerTank : ((allPlayerTechs.Count > 0) ? allPlayerTechs[0] : null));
					}
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						Damageable damageable = list[j].visible.damageable;
						if (damageable.Invulnerable)
						{
							if (makeVulnerable)
							{
								damageable.SetInvulnerable(invulnerable: false, unlimitedInvulnerability: false);
								if (!flag)
								{
									d.Log("uScript_DamageTech on tech " + techs[i].name + " has disabled invulnerability in order to do damage");
								}
							}
							else if (!flag)
							{
								d.Log("uScript_DamageTech on tech " + techs[i].name + " will not work become some blocks are invulnerable");
							}
							flag = true;
						}
						Singleton.Manager<ManDamage>.inst.DealDamage(damageable, damageable.MaxHealth * num, ManDamage.DamageType.Standard, m_Encounter, sourceTank);
					}
				}
				else
				{
					d.LogError("uScript_DamageTechs - tech is null");
				}
			}
		}
		else
		{
			d.LogError("uScript_DamageTechs - array of techs is null");
		}
	}
}
