using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
public class SharedVisible : SharedVariable
{
	[SerializeField]
	private Visible mValue;

	public Visible Value
	{
		get
		{
			return mValue;
		}
		set
		{
			mValue = value;
		}
	}

	public override object GetValue()
	{
		return mValue;
	}

	public override void SetValue(object value)
	{
		mValue = (Visible)value;
	}

	public override string ToString()
	{
		if (!(mValue == null))
		{
			return mValue.ToString();
		}
		return "null";
	}
}
