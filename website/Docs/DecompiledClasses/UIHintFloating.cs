#define UNITY_EDITOR
using System.Collections.Generic;

public class UIHintFloating : UIHUDElement
{
	public enum HintFloatTypes
	{
		Rotate_Mouse,
		Movement_Keyboard,
		Buildbeam_Keyboard,
		Shoot_Keyboard,
		Rotate_Pad,
		Movement_Pad,
		Buildbeam_Pad,
		Shoot_Pad,
		PickUp_Pad
	}

	public struct HintFloatingContext
	{
		public HintFloatTypes m_HintType;
	}

	private Dictionary<HintFloatTypes, UIHintFloatingElement> m_ChildLookup = new Dictionary<HintFloatTypes, UIHintFloatingElement>();

	public override void Show(object context)
	{
		base.Show(context);
		ShowHint(((HintFloatingContext)context).m_HintType);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		HideAllHints();
	}

	public void RegisterChildAsHint(UIHintFloatingElement hint)
	{
		if (m_ChildLookup.ContainsKey(hint.HintType))
		{
			d.LogError(string.Concat("*Error* UIHintFloating.RegisterChildAsHint - ", hint.HintType, " is already defined"));
			return;
		}
		m_ChildLookup.Add(hint.HintType, hint);
		hint.Hide();
	}

	private void ShowHint(HintFloatTypes hintType)
	{
		UIHintFloatingElement value = null;
		if (m_ChildLookup.TryGetValue(hintType, out value))
		{
			value.Show();
		}
		else
		{
			d.LogError("*Error* UIHintFloating.ShowChild - there is no hint registered for " + hintType);
		}
	}

	private void HideAllHints()
	{
		foreach (UIHintFloatingElement value in m_ChildLookup.Values)
		{
			value.Hide();
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHints>.inst.HintFloatContainerReady(this);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManHints>.inst.HintFloatContainerExpired(this);
		m_ChildLookup.Clear();
	}
}
