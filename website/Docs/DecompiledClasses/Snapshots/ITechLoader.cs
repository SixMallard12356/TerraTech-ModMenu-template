using System;

namespace Snapshots;

public interface ITechLoader
{
	void SetupPlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler);

	void RemovePlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler);

	void SetupScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null);

	void RemoveScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null);

	void SetInventory(IInventory<BlockTypes> inventory);
}
