using Binding;

public interface ISnapshotInteractionModel
{
	Bindable<PlacementSelection.InvalidResult> SwapValidation { get; }

	Bindable<PlacementSelection.InvalidResult> PlaceValidation { get; }

	void StartSwapValidation(TechData techData);

	void StopSwapValidation();

	void StartPlaceValidation(TechData techData);

	void StopPlaceValidation();
}
