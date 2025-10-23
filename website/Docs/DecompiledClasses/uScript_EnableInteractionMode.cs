#define UNITY_EDITOR
[FriendlyName("Enable Interaction Mode")]
public class uScript_EnableInteractionMode : uScriptLogic
{
	public bool Out => true;

	public void In(bool enableInteractionMode)
	{
		d.AssertFormat(enableInteractionMode != Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled, "uScript_EnableInteractionMode - Trying to set interactionMode {0}, but it is already {1}", enableInteractionMode, Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled);
		d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem == null, "uScript_EnableInteractionMode - DraggingItem is not null when interactionMode is being set. This is unexpected (though can be dealt with - please talk to code)");
		d.Assert(Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.Grab, "uScript_EnableInteractionMode - Game was not in 'Grab' mode (eg painting or placing tech). This is unexpected! Please talk to code");
		d.Assert(!Singleton.Manager<ManPurchases>.inst.IsPaletteExpanded(), "uScript_EnableInteractionMode - The palette was expanded when interactionMode toggle was invoked. This would need to be closed!");
		Singleton.Manager<ManPointer>.inst.EnableInteractionMode(enableInteractionMode);
	}
}
