using Binding;
using UnityEngine;

public class TechPlacementHelper : ISnapshotInteractionModel
{
	public Event<TechData, Vector3, Quaternion> PlaceTechEvent;

	public EventNoParams PlaceTechCancelledEvent;

	private TechData m_PlaceTechData;

	private TechData m_SwapTechData;

	public Bindable<PlacementSelection.InvalidResult> m_PlacementValidataion = new Bindable<PlacementSelection.InvalidResult>();

	public Bindable<PlacementSelection.InvalidResult> m_SwapValidation = new Bindable<PlacementSelection.InvalidResult>();

	private bool m_PlaceTechIsAnchored;

	private bool m_SwapTechIsAnchored;

	public Bindable<PlacementSelection.InvalidResult> SwapValidation => m_SwapValidation;

	public Bindable<PlacementSelection.InvalidResult> PlaceValidation => m_PlacementValidataion;

	public void StartSwapValidation(TechData techData)
	{
		m_SwapTechData = techData;
		m_SwapTechIsAnchored = m_SwapTechData.CheckIsAnchored();
	}

	public void StopSwapValidation()
	{
		m_SwapValidation.Value = default(PlacementSelection.InvalidResult);
	}

	public void StartPlaceValidation(TechData techData)
	{
		bool num = Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.Placing;
		Vector2 emulatedCursorPos = Vector2.zero;
		if (num)
		{
			emulatedCursorPos = Singleton.Manager<ManPointer>.inst.GetEmulatedCursorPos();
			Singleton.Manager<ManPointer>.inst.DisablePlacementMode();
		}
		m_PlaceTechData = techData;
		float placementRadius = m_PlaceTechData.Radius + 0.5f;
		m_PlaceTechIsAnchored = m_PlaceTechData.CheckIsAnchored();
		Singleton.Manager<ManPointer>.inst.EnablePlaceMode(placementRadius, OnPlaceTech, OnPlaceTechCancelled, PlacementValidator);
		if (num)
		{
			Singleton.Manager<ManPointer>.inst.SetEmulatedCursorPos(emulatedCursorPos);
		}
	}

	public void StopPlaceValidation()
	{
		Singleton.Manager<ManPointer>.inst.DisablePlacementMode();
		m_PlaceTechData = null;
		m_PlacementValidataion.Value = default(PlacementSelection.InvalidResult);
	}

	public static string GetPlacementBlockedReasonText(PlacementSelection.InvalidReason failReason)
	{
		return Singleton.Manager<ManPointer>.inst.GetPlacementBlockedReasonText(failReason);
	}

	public void UpdateValidation()
	{
		if (m_SwapTechData != null)
		{
			UpdateSwapValidation();
		}
	}

	private void UpdateSwapValidation()
	{
		if (Singleton.playerTank != null && !Singleton.playerTank.visible.CanBeSentToSCU)
		{
			m_SwapValidation.Value = new PlacementSelection.InvalidResult
			{
				m_Type = PlacementSelection.InvalidType.General,
				m_Flags = PlacementSelection.InvalidReason.NoIventory
			};
			return;
		}
		Vector3 vector = m_SwapTechData.m_BoundsExtents * 2f;
		int blockLimit = Singleton.Manager<ManSpawn>.inst.BlockLimit;
		if (vector.x > (float)blockLimit || vector.y > (float)blockLimit || vector.z > (float)blockLimit)
		{
			m_SwapValidation.Value = new PlacementSelection.InvalidResult
			{
				m_Type = PlacementSelection.InvalidType.General,
				m_Flags = PlacementSelection.InvalidReason.ExceedsBuildLimit
			};
			return;
		}
		Vector3 obj = (Singleton.playerTank.IsNotNull() ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
		Quaternion orientation = (Singleton.playerTank.IsNotNull() ? Singleton.playerTank.rootBlockTrans.rotation : Quaternion.identity);
		Vector3 position = obj;
		if (Singleton.playerTank != null && Singleton.playerTank.grounded)
		{
			position = Singleton.Manager<ManWorld>.inst.ProjectToGround(Singleton.playerTank.boundsCentreWorld);
		}
		float radius = m_SwapTechData.Radius + 0.5f;
		ManSpawn.TechPlacementValidator(position, orientation, radius, m_SwapTechIsAnchored, out var invalidResult, replacingPlayer: true);
		m_SwapValidation.Value = invalidResult;
	}

	private bool PlacementValidator(Vector3 position, Quaternion orientation, float radius, out PlacementSelection.InvalidResult invalidResult)
	{
		bool result = ManSpawn.TechPlacementValidator(position, orientation, radius, m_PlaceTechIsAnchored, out invalidResult);
		m_PlacementValidataion.Value = invalidResult;
		return result;
	}

	private void OnPlaceTech(Vector3 chosenPosition, Quaternion techOrientation)
	{
		PlaceTechEvent.Send(m_PlaceTechData, chosenPosition, techOrientation);
	}

	private void OnPlaceTechCancelled()
	{
		PlaceTechCancelledEvent.Send();
	}
}
