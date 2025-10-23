public static class GameCursor
{
	public enum CursorState
	{
		Default,
		OverGrabbable,
		HoldingGrabbable,
		Painting,
		SkinPainting,
		SkinPaintingOverPaintable,
		SkinTechPainting,
		SkinTechPaintingOverPaintable,
		Disabled,
		OverInteractableGrabbable,
		HoldingCopiedGrabbable,
		SendToSCU,
		SendToSCUOverSendable
	}

	public static CursorState s_LastCursorState;

	public static Event<int> CursorSizeChangedEvent;

	public static int CursorSize { get; private set; }

	public static void SetCursorSize(int size)
	{
		if (CursorSize != size)
		{
			CursorSize = size;
			CursorSizeChangedEvent.Send(CursorSize);
		}
	}

	public static CursorState GetCursorState()
	{
		CursorState result = CursorState.Default;
		ManPointer inst = Singleton.Manager<ManPointer>.inst;
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.WorldMap))
		{
			result = CursorState.Default;
		}
		else if (inst.IsNotNull() && !Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying() && inst.CanSelectTargets())
		{
			switch (inst.BuildMode)
			{
			case ManPointer.BuildingMode.PaintSkin:
				result = (inst.targetBlock.IsNotNull() ? CursorState.SkinPaintingOverPaintable : CursorState.SkinPainting);
				break;
			case ManPointer.BuildingMode.PaintSkinTech:
				result = ((inst.targetVisible.IsNotNull() && ((bool)inst.targetVisible.tank || (bool)inst.targetVisible.block)) ? CursorState.SkinTechPaintingOverPaintable : CursorState.SkinTechPainting);
				break;
			case ManPointer.BuildingMode.Grab:
			case ManPointer.BuildingMode.PaintBlock:
			case ManPointer.BuildingMode.Placing:
				if (inst.DraggingItem.IsNotNull())
				{
					result = ((inst.BuildMode != ManPointer.BuildingMode.PaintBlock) ? CursorState.HoldingGrabbable : (inst.HasBlockClipboard ? CursorState.HoldingCopiedGrabbable : CursorState.Painting));
				}
				else if (inst.targetVisible.IsNotNull() && inst.ItemIsGrabbable(inst.targetVisible))
				{
					result = ((!inst.targetBlock.IsNotNull() || !inst.targetBlock.HasAccessibleContextMenu()) ? CursorState.OverGrabbable : CursorState.OverInteractableGrabbable);
				}
				break;
			case ManPointer.BuildingMode.BlockDetach:
				result = ((inst.targetBlock.IsNotNull() && inst.GetCanDetach(inst.targetBlock)) ? CursorState.SendToSCUOverSendable : CursorState.SendToSCU);
				break;
			}
		}
		s_LastCursorState = result;
		return result;
	}
}
