#define UNITY_EDITOR
using System;
using UnityEngine;

[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("uScript_SetBlockAnimationInt", "Set an animation int parameter on this block's animator")]
public class uScript_SetBlockAnimationInt : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, string name, int value)
	{
		if (block == null)
		{
			d.LogError("uScript_SetBlockAnimationInt is being called on a null block)");
			return;
		}
		ModuleAnimator component = block.gameObject.GetComponent<ModuleAnimator>();
		if ((bool)component)
		{
			AnimatorInt param = new AnimatorInt(name);
			if (component.Set(param, value))
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					BlockAnimationChange blockAnimationChange = new BlockAnimationChange();
					blockAnimationChange.m_BlockPoolID = block.blockPoolID;
					blockAnimationChange.m_ParameterType = AnimatorControllerParameterType.Int;
					blockAnimationChange.m_ParameterName = name;
					blockAnimationChange.m_ParameterData = BitConverter.GetBytes(value);
					Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.BlockAnimationChange, blockAnimationChange);
				}
			}
			else
			{
				d.LogError("uScript_SetBlockAnimationInt is being called with an invalid animation int parameter name (" + name + ")");
			}
		}
		else
		{
			d.LogError("uScript_SetBlockAnimationInt is being called on a block without a ModuleAnimator component (" + block.name + ")");
		}
	}
}
