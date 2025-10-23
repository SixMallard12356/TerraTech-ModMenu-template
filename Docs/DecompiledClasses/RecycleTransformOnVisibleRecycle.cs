#define UNITY_EDITOR
using UnityEngine;

public class RecycleTransformOnVisibleRecycle : MonoBehaviour
{
	private Visible m_ParentVisible;

	private void OnVisibleRecycled(Visible vis)
	{
		if ((bool)m_ParentVisible)
		{
			m_ParentVisible.RecycledEvent.Unsubscribe(OnVisibleRecycled);
			m_ParentVisible = null;
			base.transform.Recycle();
		}
	}

	private void OnSpawn()
	{
		m_ParentVisible = GetComponentInParent<Visible>();
		if (m_ParentVisible != null)
		{
			m_ParentVisible.RecycledEvent.Subscribe(OnVisibleRecycled);
			return;
		}
		d.LogErrorFormat("RecycleTransformOnVisibleRecycle - {0} could not find Visible in parents on Spawn!?", base.name);
	}

	private void OnRecycle()
	{
		if ((bool)m_ParentVisible)
		{
			m_ParentVisible.RecycledEvent.Unsubscribe(OnVisibleRecycled);
			m_ParentVisible = null;
		}
	}
}
