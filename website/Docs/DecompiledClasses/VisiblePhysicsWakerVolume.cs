using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VisiblePhysicsWakerVolume : MonoBehaviour
{
	protected BoxCollider m_Col;

	protected List<Visible> m_WatchedVisibles = new List<Visible>();

	protected bool m_IsActive;

	private static Collider[] _s_ColliderBuffer = new Collider[64];

	private static Visible _s_ColliderVisible;

	private static List<Visible> _s_VisibleRemoveBuffer = new List<Visible>();

	public bool IsActive
	{
		get
		{
			return m_IsActive;
		}
		set
		{
			if (m_IsActive != value)
			{
				m_IsActive = value;
				RefreshActive();
			}
		}
	}

	protected BoxCollider Col
	{
		get
		{
			if (m_Col == null)
			{
				m_Col = GetComponent<BoxCollider>();
				m_Col.isTrigger = true;
			}
			return m_Col;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (((1 << other.gameObject.layer) & Singleton.Manager<ManVisible>.inst.VisiblePickerMaskNoTechs) != 0)
		{
			TryAddColliderVisible(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (((1 << other.gameObject.layer) & Singleton.Manager<ManVisible>.inst.VisiblePickerMaskNoTechs) != 0)
		{
			TryRemoveColliderVisible(other);
		}
	}

	private void TryAddVisiblesInColliderArea()
	{
		int num = 0;
		do
		{
			if (num >= _s_ColliderBuffer.Length)
			{
				Array.Resize(ref _s_ColliderBuffer, _s_ColliderBuffer.Length * 2);
			}
			num = Physics.OverlapBoxNonAlloc(base.transform.position + Col.center, Col.size / 2f, _s_ColliderBuffer, base.transform.rotation, Singleton.Manager<ManVisible>.inst.VisiblePickerMaskNoTechs, QueryTriggerInteraction.Ignore);
		}
		while (num >= _s_ColliderBuffer.Length);
		for (int i = 0; i < num; i++)
		{
			TryAddColliderVisible(_s_ColliderBuffer[i]);
		}
	}

	private void RefreshActive()
	{
		Col.enabled = m_IsActive;
		m_WatchedVisibles.Clear();
		if (m_IsActive)
		{
			TryAddVisiblesInColliderArea();
		}
	}

	private void TryAddColliderVisible(Collider col)
	{
		if (!(col == null))
		{
			_s_ColliderVisible = Visible.FindVisibleUpwards(col);
			if (!(_s_ColliderVisible == null) && !m_WatchedVisibles.Contains(_s_ColliderVisible))
			{
				m_WatchedVisibles.Add(_s_ColliderVisible);
			}
		}
	}

	private void TryRemoveColliderVisible(Collider col)
	{
		if (!(col == null))
		{
			_s_ColliderVisible = Visible.FindVisibleUpwards(col);
			if (!(_s_ColliderVisible == null) && m_WatchedVisibles.Contains(_s_ColliderVisible))
			{
				m_WatchedVisibles.Remove(_s_ColliderVisible);
			}
		}
	}

	private void FixedUpdate()
	{
		_s_VisibleRemoveBuffer.Clear();
		if (m_IsActive)
		{
			for (int i = 0; i < m_WatchedVisibles.Count; i++)
			{
				if (m_WatchedVisibles[i].IsNull() || !m_WatchedVisibles[i].gameObject.activeInHierarchy)
				{
					_s_VisibleRemoveBuffer.Add(m_WatchedVisibles[i]);
				}
				else if (m_WatchedVisibles[i].rbody.IsNotNull() && m_WatchedVisibles[i].rbody.IsSleeping())
				{
					m_WatchedVisibles[i].rbody.WakeUp();
				}
			}
		}
		for (int j = 0; j < _s_VisibleRemoveBuffer.Count; j++)
		{
			m_WatchedVisibles.Remove(_s_VisibleRemoveBuffer[j]);
		}
	}

	private void OnEnable()
	{
		RefreshActive();
	}

	private void OnDisable()
	{
		RefreshActive();
	}
}
