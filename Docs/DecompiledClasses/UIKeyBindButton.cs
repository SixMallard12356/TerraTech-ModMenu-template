#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;

public class UIKeyBindButton : UIKeyBindDisplay
{
	public void OnButtonClick()
	{
		Singleton.Manager<ManInput>.inst.KeyAssignmentEvent.Subscribe(OnKeyAssignAttempt);
		if (Singleton.Manager<ManInput>.inst.StartPollingForKeyAssignment(m_Index, m_ActionID, m_AxisContribution, m_AssignedActionMap))
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Select);
			m_TextField.text = "?";
		}
	}

	private void OnKeyAssignAttempt(ManInput.ElementAssignmentChange elementAssignmentChange)
	{
		Singleton.Manager<ManInput>.inst.KeyAssignmentEvent.Unsubscribe(OnKeyAssignAttempt);
		if (elementAssignmentChange.invalidKey)
		{
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
			};
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlOptions, 33);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString("UnityInputKeyCode", elementAssignmentChange.pollingInfo.keyboardKey.ToString());
			localisedString = string.Format(localisedString, localisedString2);
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(localisedString, accept, localisedString3);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			Singleton.Manager<ManInput>.inst.OnControlMapAssigned.Send(elementAssignmentChange.uiIndex, elementAssignmentChange.actionId, elementAssignmentChange.actionAxisContribution, elementAssignmentChange.actionElementMap);
			return;
		}
		ControllerPollingInfo pollingInfo = elementAssignmentChange.pollingInfo;
		foreach (ControllerMap item in (IEnumerable<ControllerMap>)Singleton.Manager<ManInput>.inst.GetAllRebindableMaps())
		{
			foreach (ActionElementMap item2 in item.ButtonMaps.ToList())
			{
				if ((item.controllerType == pollingInfo.controllerType && item2.elementType == pollingInfo.elementType && item2.elementIndex == pollingInfo.elementIndex) || item2.id == elementAssignmentChange.actionElementMapId)
				{
					Singleton.Manager<ManInput>.inst.OnControlMapRemoved.Send(item2);
					item.DeleteElementMap(item2.id);
				}
			}
		}
		ControllerMap controllerMapFor = Singleton.Manager<ManInput>.inst.GetControllerMapFor(pollingInfo.controllerType, elementAssignmentChange.actionId);
		ActionElementMap paramD = null;
		if (controllerMapFor != null)
		{
			ActionElementMap result;
			bool flag = controllerMapFor.ReplaceOrCreateElementMap(elementAssignmentChange.ToElementAssignment(), out result);
			d.Log($"ReplaceOrCreateElementMap {controllerMapFor} success={flag} actionElementMapId={elementAssignmentChange.actionElementMapId} result.id={result?.id} result.type={result?.controllerMap?.controllerType} result.element={result?.elementIdentifierName}");
			if (flag)
			{
				paramD = result;
			}
		}
		else
		{
			d.LogError("Failed to resolve ControllerMap for polled ActionID");
		}
		Singleton.Manager<ManInput>.inst.OnControlMapAssigned.Send(elementAssignmentChange.uiIndex, elementAssignmentChange.actionId, elementAssignmentChange.actionAxisContribution, paramD);
	}
}
