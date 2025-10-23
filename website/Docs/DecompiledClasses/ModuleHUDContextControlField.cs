using System;
using UnityEngine;

public abstract class ModuleHUDContextControlField : Module, IHUDContextControlFieldModel
{
	[SerializeField]
	protected LocalisedString m_HUDTitle;

	[SerializeField]
	public bool m_ApplyChangesRealtime;

	public EventNoParams OptionSetEvent;

	public EventNoParams InstantRefreshEvent;

	public abstract Type BlockContextFieldType { get; }

	public LocalisedString HUDTitle => m_HUDTitle;

	public bool ApplyChangesRealtime => m_ApplyChangesRealtime;
}
