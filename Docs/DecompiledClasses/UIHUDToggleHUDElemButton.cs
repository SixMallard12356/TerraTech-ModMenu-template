using UnityEngine;
using UnityEngine.Events;

public class UIHUDToggleHUDElemButton : UIHUDToggleButton
{
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private ManHUD.HUDElementType m_HUDElementTarget;

	public override void Show(object context)
	{
		object context2 = new UnityAction<bool>(ShowHudElement);
		base.Show(context2);
	}

	private void ShowHudElement(bool show)
	{
		if (show)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(m_HUDElementTarget);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(m_HUDElementTarget);
		}
	}

	private void OnHUDElementShown(UIHUDElement element)
	{
		if (element.HudElementType == m_HUDElementTarget)
		{
			SetToggledState(toggled: true);
		}
	}

	private void OnHUDElementHidden(UIHUDElement element)
	{
		if (element.HudElementType == m_HUDElementTarget)
		{
			SetToggledState(toggled: false);
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Subscribe(OnHUDElementShown);
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Subscribe(OnHUDElementHidden);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Unsubscribe(OnHUDElementShown);
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Unsubscribe(OnHUDElementHidden);
	}
}
