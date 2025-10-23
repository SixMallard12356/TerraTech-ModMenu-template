using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimDurationTable : ScriptableObject
{
	[Serializable]
	public struct ControllerRecord
	{
		public string controllerName;

		public List<DurationRecord> durationRecs;
	}

	[Serializable]
	public struct DurationRecord
	{
		public string name;

		public float duration;
	}

	public List<ControllerRecord> m_Records;
}
