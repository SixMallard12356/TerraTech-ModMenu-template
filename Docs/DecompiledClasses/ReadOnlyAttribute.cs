using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute
{
	public enum EnabledState
	{
		Always,
		EditorOnly,
		PlayModeOnly
	}

	public EnabledState EnabledInState { get; private set; }

	public ReadOnlyAttribute(EnabledState enabledState = EnabledState.Always)
	{
		EnabledInState = enabledState;
	}
}
