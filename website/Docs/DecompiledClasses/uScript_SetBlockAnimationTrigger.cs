#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("uScript_SetBlockAnimationTrigger", "Set an animation Trigger on this block's animator")]
public class uScript_SetBlockAnimationTrigger : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, string name)
	{
		if (block == null)
		{
			d.LogError("uScript_SetBlockAnimationTrigger is being called on a null block)");
			return;
		}
		ModuleAnimator component = block.gameObject.GetComponent<ModuleAnimator>();
		if ((bool)component)
		{
			AnimatorTrigger param = new AnimatorTrigger(name);
			if (component.Set(param))
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					BlockAnimationChange blockAnimationChange = new BlockAnimationChange();
					blockAnimationChange.m_BlockPoolID = block.blockPoolID;
					blockAnimationChange.m_ParameterType = AnimatorControllerParameterType.Trigger;
					blockAnimationChange.m_ParameterName = name;
					Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.BlockAnimationChange, blockAnimationChange);
				}
			}
			else
			{
				d.LogError("uScript_SetBlockAnimationTrigger is being called with an invalid animation name (" + name + ")");
			}
		}
		else
		{
			d.LogError("uScript_SetBlockAnimationTrigger is being called on a block without a ModuleAnimator component (" + block.name + ")");
		}
	}
}
