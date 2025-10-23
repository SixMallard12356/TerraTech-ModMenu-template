#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Binding;
using UnityEngine;

public class SnapshotCollection<TSnap> where TSnap : Snapshot, new()
{
	public Event<SnapshotCollection<TSnap>> Changed;

	private BindableList<TSnap> m_Snapshots;

	private string m_ModeRestriction;

	private string m_SubModeRestriction;

	private string m_UserDataRestriction = "";

	private TankPreset.UserData m_UserProfileRestriction;

	protected Comparison<TSnap> defaultSort;

	protected Func<TSnap, TSnap, bool> defaultReplaceCheck;

	public BindableList<TSnap> Snapshots => m_Snapshots;

	public bool SortOnAddEnabled { get; set; }

	public SnapshotCollection(string modeRestriction, string subModeRestriction, string userDataRestriction, TankPreset.UserData userProfileRestriction = null)
	{
		m_ModeRestriction = modeRestriction;
		m_SubModeRestriction = subModeRestriction;
		m_UserDataRestriction = userDataRestriction;
		m_Snapshots = new BindableList<TSnap>();
		m_Snapshots.List = new List<TSnap>();
	}

	public virtual void AddSnapshot(TSnap snapshot)
	{
		m_Snapshots.Add(snapshot);
		Changed.Send(this);
	}

	public void RemoveSnapshot(TSnap snapshot)
	{
		if (snapshot != null && m_Snapshots.Contains(snapshot))
		{
			snapshot.DestroyTexture();
			m_Snapshots.Remove(snapshot);
			Changed.Send(this);
		}
	}

	public virtual void AddOrReplaceSnapshot(TSnap snapshot)
	{
		bool flag = false;
		if (defaultReplaceCheck != null)
		{
			for (int i = 0; i < m_Snapshots.Count; i++)
			{
				if (defaultReplaceCheck(snapshot, m_Snapshots[i]))
				{
					d.Assert(m_Snapshots[i] != snapshot, "SnapshotCollection.AddOrReplaceSnapshot - 'Replacing' snapshot with a new snapshot, but it is already the same object!");
					m_Snapshots[i].DestroyTexture();
					m_Snapshots[i] = snapshot;
					flag = true;
					break;
				}
			}
		}
		else
		{
			d.LogError("SnapshotCollection.AddOrReplaceSnapshot: no defaultReplaceCheck set on type " + GetType().ToString() + ". Please specify one or use AddSnapshot instead");
		}
		if (!flag)
		{
			m_Snapshots.Add(snapshot);
			if (SortOnAddEnabled)
			{
				Sort();
			}
		}
		Changed.Send(this);
	}

	public void RemoveAllSnapshots()
	{
		foreach (TSnap snapshot in m_Snapshots)
		{
			snapshot.DestroyTexture();
		}
		Clear();
	}

	public void Clear()
	{
		m_Snapshots.Clear();
		Changed.Send(this);
	}

	public void Sort()
	{
		if (defaultSort != null)
		{
			m_Snapshots.Sort(defaultSort);
			Changed.Send(this);
		}
		else
		{
			d.LogError("ManScreenshot.DecodedCapCollection.Sort() - trying to sort list without a default comparer specified");
		}
	}

	public void Reset(string modeRestriction, string subModeRestriction, string userDataRestriction, TankPreset.UserData userProfileRestriction = null)
	{
		m_ModeRestriction = modeRestriction;
		m_SubModeRestriction = subModeRestriction;
		m_UserDataRestriction = userDataRestriction;
		m_UserProfileRestriction = userProfileRestriction;
		Clear();
	}

	protected virtual TSnap DecodeSnapshotFromImage(Texture2D snapshotRender, bool convertLegacy = true)
	{
		TSnap val = null;
		TechData techData = null;
		if (ManScreenshot.TryDecodeSnapshotRender(snapshotRender, out var techSnapshotData, null, convertLegacy))
		{
			techData = techSnapshotData.CreateTechData();
		}
		if (techData != null)
		{
			val = new TSnap
			{
				image = snapshotRender
			};
			val.techData = techData;
			if (!CheckConverted(val))
			{
				return null;
			}
			val.techData.Name = TrimPresetName(val.techData.Name);
		}
		return val;
	}

	private bool CheckConverted(Snapshot snapshot)
	{
		return CheckConverted(snapshot, m_ModeRestriction, m_SubModeRestriction, m_UserDataRestriction, m_UserProfileRestriction);
	}

	public bool CheckConverted(Snapshot snapshot, string modeRestriction, string subModeRestriction, string userDataRestriction, TankPreset.UserData userProfileRestriction = null)
	{
		bool flag = snapshot.techData != null && modeRestriction != "Sumo" && snapshot.techData.m_CreationData.mode == null;
		bool flag2 = userProfileRestriction == null || userProfileRestriction == snapshot.techData.m_CreationData.m_UserProfile;
		return snapshot.techData != null && (modeRestriction == null || modeRestriction == snapshot.techData.m_CreationData.mode || flag) && (subModeRestriction == null || subModeRestriction == snapshot.techData.m_CreationData.subMode) && (userDataRestriction == null || userDataRestriction == snapshot.techData.m_CreationData.userData) && flag2;
	}

	private string TrimPresetName(string name)
	{
		string text = name.Replace("Tank: ", "").Replace(" (Player)", "").Replace("Tank (Wrapper)", "")
			.Replace("cockpit only", "");
		if (text.Length != 0)
		{
			return text;
		}
		return "<unnamed>";
	}
}
