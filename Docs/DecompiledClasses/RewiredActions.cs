using Rewired.Dev;

public static class RewiredActions
{
	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Action0")]
	public const int Default_MoveVertical = 0;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Action1")]
	public const int Default_MoveHorizontal = 1;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveAxis3")]
	public const int Default_MoveAxis3 = 68;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveAxis4")]
	public const int Default_MoveAxis4 = 69;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveAxis5")]
	public const int Default_MoveAxis5 = 70;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveAxis6")]
	public const int Default_MoveAxis6 = 71;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Fire")]
	public const int Default_Fire = 2;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Beam")]
	public const int Default_Beam = 3;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Boost")]
	public const int Default_Boost = 4;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "BoostSecondary")]
	public const int Default_BoostSecondary = 72;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "InteractionMode")]
	public const int Default_InteractionMode = 5;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "CamRotateHorizontal")]
	public const int Default_CamRotateHorizontal = 6;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "CamRotateVertical")]
	public const int Default_CamRotateVertical = 7;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "CamRecenter")]
	public const int Default_CamRecenter = 14;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "CamZoom")]
	public const int Default_CamZoom = 15;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Rotate Block")]
	public const int Default_RotateBlockDefault = 16;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "FastRepeat")]
	public const int Default_RotateTechPlacement = 118;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "TogglePalette")]
	public const int Default_TogglePalette = 25;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Pause")]
	public const int Default_Pause = 26;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Toggle Info")]
	public const int Default_Info = 27;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Rename Tech")]
	public const int Default_Rename = 28;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Display Mission Log")]
	public const int Default_ToggleMissions = 29;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Display World Map")]
	public const int Default_ToggleWorldMap = 97;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Take Screenshot")]
	public const int Default_Screenshot = 30;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Cycle control scheme")]
	public const int Default_CycleControlScheme = 73;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Swap to tech 1")]
	public const int Default_Hotswap1 = 33;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Swap to tech 2")]
	public const int Default_Hotswap2 = 34;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Swap to tech 3")]
	public const int Default_Hotswap3 = 35;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Swap to tech 4")]
	public const int Default_Hotswap4 = 37;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Swap to tech 5")]
	public const int Default_Hotswap5 = 38;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Focus Block on tech in build beam mode")]
	public const int Default_FocusBuildBlock = 44;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "ExplosiveBoltActivation")]
	public const int Default_ExplosiveBoltActivation = 45;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Toggles multiplayer score board on off")]
	public const int Default_ToggleScoreBoard = 46;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "ItemDragVertical")]
	public const int Default_ItemDragVertical = 53;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Switch Tech Allegiance")]
	public const int Default_ToggleTeam = 80;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "ItemDragHorizontal")]
	public const int Default_ItemDragHorizontal = 52;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Grab nearest block or chunk from floor")]
	public const int Default_GrabNearbyBlock = 54;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Set focus on mp chat input")]
	public const int Default_MPChat = 60;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Set focus on mp team chat")]
	public const int Default_MPChatTeam = 61;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "MouseLB")]
	public const int Default_MouseLB = 63;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "NavigateCategoriesModifier")]
	public const int Default_NavigateCategoriesModifier = 66;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "OpenSearchModifier")]
	public const int Default_OpenSearchModifier = 116;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "OpenSearchActivator")]
	public const int Default_OpenSearchActivator = 117;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "ManualTarget")]
	public const int Default_ManualTarget = 67;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "ToggleSkinsPalette")]
	public const int Default_ToggleSkinsPalette = 74;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "PaintBlockSkin")]
	public const int Default_PaintBlockSkin = 76;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "PaintTechSkin")]
	public const int Default_PaintTechSkin = 77;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Anchor")]
	public const int Default_Anchor = 78;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "NavigateCorpsModifier")]
	public const int Default_NavigateCorpsModifier = 79;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Freecam Forwards Back")]
	public const int Default_Freecam_Accel = 91;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Freecam Right Left")]
	public const int Default_Freecam_Strafe = 92;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Freecam Up Down")]
	public const int Default_Freecam_Vertical = 93;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Freecam Speedup")]
	public const int Default_Freecam_Boost = 94;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Freecam Slowdown")]
	public const int Default_Freecam_Walk = 95;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Non debug hidehud")]
	public const int Default_PlayerHideHud = 96;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Build block eyedropper")]
	public const int Default_Eyedropper = 98;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Temporarily clears the blcok selection in inventory")]
	public const int Default_ClearBlockSelection = 120;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_0 = 105;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_1 = 106;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_2 = 107;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_3 = 108;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_4 = 109;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_5 = 110;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_6 = 111;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_7 = 112;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_8 = 113;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "C&S Key triggers [0, 9]")]
	public const int Default_CSKeyTrigger09_9 = 114;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Hold to make Click detach blocks instead of grabbing them")]
	public const int Default_DetachBlockMode = 121;

	[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Show or hide the Command Window (Console)")]
	public const int Default_CommandWindow = 122;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "ObjectSelectVertical")]
	public const int BlockInteraction_ObjectSelectVertical = 8;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "ObjectSelectHorizontal")]
	public const int BlockInteraction_ObjectSelectHorizontal = 9;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "Select")]
	public const int BlockInteraction_Select = 10;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "Cancel")]
	public const int BlockInteraction_Cancel = 11;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "RotateBlock")]
	public const int BlockInteraction_RotateBlock = 12;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "PickFromInventory")]
	public const int BlockInteraction_PickFromInventory = 13;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "Action0")]
	public const int BlockInteraction_PreviewMove = 49;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "TogglePlacementBlock")]
	public const int BlockInteraction_TogglePlacementBlock = 50;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "Undo")]
	public const int BlockInteraction_Undo = 62;

	[ActionIdFieldInfo(categoryName = "BlockInteraction", friendlyName = "MoveBeam")]
	public const int BlockInteraction_MoveBeam = 64;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Horizontal")]
	public const int UI_UIHorizontal = 19;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Vertical")]
	public const int UI_UIVertical = 20;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Submit")]
	public const int UI_UISubmit = 21;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Cancel")]
	public const int UI_UICancel = 22;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Select Next Menu Group")]
	public const int UI_UITabNext = 41;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Select Prev Menu Group")]
	public const int UI_UITabPrev = 42;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Horizontal scroll pan")]
	public const int UI_UIHorizontalScroll = 100;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Vertical scroll info window")]
	public const int UI_UIVerticalScroll = 47;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIRadialMenu")]
	public const int UI_UIRadialMenu = 48;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIExtra1")]
	public const int UI_UIExtra1 = 57;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIExtra2")]
	public const int UI_UIExtra2 = 58;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIHintCancel")]
	public const int UI_UIHintCancel = 65;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "LeftStick button")]
	public const int UI_UILeftStick = 101;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Right Stick button")]
	public const int UI_UIRightStick = 102;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "DpadHorizontal")]
	public const int UI_UIDPadHorizontal = 103;

	[ActionIdFieldInfo(categoryName = "UI", friendlyName = "DpadVertical")]
	public const int UI_UIDPadVertical = 104;

	[ActionIdFieldInfo(categoryName = "System", friendlyName = "UniversalCancel")]
	public const int System_UniversalCancel = 24;

	[ActionIdFieldInfo(categoryName = "System", friendlyName = "Pause")]
	public const int System_UniversalPause = 43;

	[ActionIdFieldInfo(categoryName = "System", friendlyName = "UniversalPhotoMode")]
	public const int System_UniversalPhotoMode = 115;

	[ActionIdFieldInfo(categoryName = "Interaction", friendlyName = "PickUp")]
	public const int Interaction_PickUp = 51;

	[ActionIdFieldInfo(categoryName = "Interaction", friendlyName = "OpenMenu")]
	public const int Interaction_OpenMenu = 56;

	[ActionIdFieldInfo(categoryName = "Interaction", friendlyName = "OpenRadial")]
	public const int Interaction_OpenRadial = 59;
}
