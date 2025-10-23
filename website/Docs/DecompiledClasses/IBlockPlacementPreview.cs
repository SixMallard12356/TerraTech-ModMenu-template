using System.Collections.Generic;

public interface IBlockPlacementPreview
{
	void TryPreviewAttachments(IEnumerable<BlockPlacementPreviewHandler.APConnection> previewAPs);
}
