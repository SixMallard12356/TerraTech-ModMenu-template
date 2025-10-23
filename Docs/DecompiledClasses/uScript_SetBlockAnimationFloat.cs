#define UNITY_EDITOR
using System;
using UnityEngine;

[FriendlyName("uScript_SetBlockAnimationFloat", "Set an animation float parameter on this block's animator")]
[NodePath("TerraTech/Actions/Blocks")]
public class uScript_SetBlockAnimationFloat : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, string name, float value)
	{
		if (block == null)
		{
			d.LogError("uScript_SetBlockAnimationFloat is being called on a null block)");
			return;
		}
		ModuleAnimator component = block.gameObject.GetComponent<ModuleAnimator>();
		if ((bool)component)
		{
			AnimatorFloat param = new AnimatorFloat(name);
			if (component.Set(param, value))
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					BlockAnimationChange blockAnimationChange = new BlockAnimationChange();
					blockAnimationChange.m_BlockPoolID = block.blockPoolID;
					blockAnimationChange.m_ParameterType = AnimatorControllerParameterType.Float;
					blockAnimationChange.m_ParameterName = name;
					blockAnimationChange.m_ParameterData = BitConverter.GetBytes(value);
					Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.BlockAnimationChange, blockAnimationChange);
				}
			}
			else
			{
				d.LogError("uScript_SetBlockAnimationFloat is being called with an invalid animation float parameter name (" + name + ")");
			}
		}
		else
		{
			d.LogError("uScript_SetBlockAnimationFloat is being called on a block without a ModuleAnimator component (" + block.name + ")");
		}
	}
}
