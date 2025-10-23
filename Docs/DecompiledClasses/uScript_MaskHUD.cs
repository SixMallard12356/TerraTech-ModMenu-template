#define UNITY_EDITOR
using UnityEngine;

public class uScript_MaskHUD : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("Unmask", "Remove any HUD mask from the screen")]
	public void Unmask(RectTransform singleUnmaskedTransform = null, RectTransform[] unmaskedTransforms = null)
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.HUDMask);
	}

	[FriendlyName("Mask Screen", "Mask the entire screen, while allowing input over the areas of the provided rect transforms")]
	public void MaskScreen(RectTransform singleUnmaskedTransform = null, RectTransform[] unmaskedTransforms = null)
	{
		d.Assert(singleUnmaskedTransform == null || unmaskedTransforms == null, "uScript_MaskHUD.MaskScreen - Cannot supply both a single _and_ multiple unmasked transforms. Please use only the array input parameter if multiple are required.");
		UIMaskCutout.UIMaskContext uIMaskContext = new UIMaskCutout.UIMaskContext
		{
			unmaskedTransforms = ((!(singleUnmaskedTransform != null)) ? unmaskedTransforms : new RectTransform[1] { singleUnmaskedTransform })
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.HUDMask, uIMaskContext);
	}
}
