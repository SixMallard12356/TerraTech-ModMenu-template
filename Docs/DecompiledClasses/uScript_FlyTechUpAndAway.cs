#define UNITY_EDITOR
using BehaviorDesigner.Runtime;
using UnityEngine;

[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_FlyTechUpAndAway : uScriptLogic
{
	private bool m_HasFired;

	public bool Out => true;

	public void In([FriendlyName("Tech", "Tech to fly away")] Tank tech, [FriendlyName("Max Lifetime", "Maximum time for the tech to stay alive")] float maxLifetime, [FriendlyName("Max Height", "Target height above ground to reach before being removed")] float targetHeight, [FriendlyName("AI Tree", "The ExternalBehaviorTree that will do the flying for us")] ExternalBehaviorTree aiTree, [DefaultValue(null)][FriendlyName("Removal Particles", "Particle object to spawn on reaching the end of its lifetime/height")] Transform removalParticles)
	{
		if (tech != null)
		{
			d.Assert(!m_HasFired, "WARNING - uScript_FlyTechUpAndAway Already fired previously! Are there multiple fire triggers or was this event loaded twice?");
			float num = Singleton.Manager<ManWorld>.inst.ProjectToGround(tech.boundsCentreWorld).y + targetHeight;
			FlyTechAway.InitiateTakeOff(tech, maxLifetime, num, aiTree, removalParticles);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				FlyTechAwayRequest message = new FlyTechAwayRequest
				{
					m_TargetTechId = tech.netTech.netId,
					m_MaxLifetime = maxLifetime,
					m_TargetHeightWorld = num,
					m_UseParticles = true,
					m_ExpectedPositionOfSmoke = tech.boundsCentreWorld
				};
				Debug.Log($"uScript_FlyTechUpAndAway.In - Triggered request to send FlyTechAway to clients tech.netId={tech.netTech.netId} name={tech.name}");
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.FlyTechAwayRequest, message);
			}
			m_HasFired = true;
		}
		else
		{
			d.LogError("ERROR - uScript_FlyTechUpAndAway Null tech passed in!");
		}
	}

	public void OnEnable()
	{
		m_HasFired = false;
	}
}
