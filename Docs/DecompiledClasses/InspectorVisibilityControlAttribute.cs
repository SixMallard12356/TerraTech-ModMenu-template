using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class InspectorVisibilityControlAttribute : PropertyAttribute
{
	public enum ComparisonType
	{
		Equals,
		NotEquals
	}

	public string ParamPath { get; private set; }

	public object ValueToCompareTo { get; private set; }

	public ComparisonType CompareType { get; private set; }

	public InspectorVisibilityControlAttribute(string paramRelativePath, object compareValue, ComparisonType comparisonType = ComparisonType.Equals)
	{
		ParamPath = paramRelativePath;
		ValueToCompareTo = compareValue;
		CompareType = comparisonType;
	}

	public InspectorVisibilityControlAttribute(string booleanParamRelativePath, ComparisonType comparisonType = ComparisonType.Equals)
	{
		ParamPath = booleanParamRelativePath;
		ValueToCompareTo = true;
		CompareType = comparisonType;
	}
}
