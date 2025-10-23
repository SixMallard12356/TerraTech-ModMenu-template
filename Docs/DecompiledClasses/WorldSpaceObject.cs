#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class WorldSpaceObject : WorldSpaceObjectBase
{
	[SerializeField]
	[FormerlySerializedAs("m_CheckParentHierarchyOnSpawn")]
	private bool m_CheckParentHierarchy;

	private Visible m_Visible;

	private Rigidbody m_Rbody;

	private Transform m_Transform;

	private bool m_Enabled;

	private bool m_ObjectIsAlive;

	private static List<WorldSpaceObject> s_ParentWorldSpaceObjects = new List<WorldSpaceObject>(2);

	public bool IsEnabled => m_Enabled;

	public void SetEnabled(bool enabled)
	{
		d.AssertFormat(m_ObjectIsAlive, "WorldSpaceObject.SetEnabled called while object is recycled!", base.name);
		if (enabled != m_Enabled)
		{
			m_Enabled = enabled;
			if (m_Enabled)
			{
				Singleton.Manager<ManWorldTreadmill>.inst.AddWorldSpaceObject(this);
			}
			else
			{
				Singleton.Manager<ManWorldTreadmill>.inst.RemoveWorldSpaceObject(this);
			}
		}
	}

	public override void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		Rigidbody rigidbody = ((m_Visible != null) ? m_Visible.rbody : m_Rbody);
		if (rigidbody != null)
		{
			rigidbody.position += amountToMove;
			m_Transform.position = rigidbody.position;
		}
		else if (m_Transform != null)
		{
			m_Transform.position += amountToMove;
		}
		else
		{
			d.LogError("OnMoveWorldOrigin - Trying to move object but transform was Null!");
		}
	}

	public override void OnForceSyncToRigidBody()
	{
		Rigidbody rigidbody = ((m_Visible != null) ? m_Visible.rbody : m_Rbody);
		if (rigidbody != null)
		{
			m_Transform.position = rigidbody.position;
		}
	}

	private void OnAttachedToTech()
	{
		SetEnabled(enabled: false);
	}

	private void OnDetachingFromTech()
	{
		SetEnabled(enabled: true);
	}

	private bool FindEnabledWorldSpaceObjectInParents()
	{
		bool result = false;
		if (m_Transform.parent != null)
		{
			m_Transform.parent.GetComponentsInParent(includeInactive: false, s_ParentWorldSpaceObjects);
			for (int i = 0; i < s_ParentWorldSpaceObjects.Count; i++)
			{
				if (s_ParentWorldSpaceObjects[i].IsEnabled)
				{
					result = true;
					break;
				}
			}
			s_ParentWorldSpaceObjects.Clear();
		}
		return result;
	}

	private void OnTransformParentHasChanged()
	{
		if (m_CheckParentHierarchy && m_ObjectIsAlive && m_Transform != null)
		{
			bool flag = FindEnabledWorldSpaceObjectInParents();
			if (IsEnabled == flag)
			{
				SetEnabled(!flag);
			}
		}
	}

	private void OnPool()
	{
		m_Visible = GetComponent<Visible>();
		if (m_Visible != null)
		{
			if (m_Visible.type == ObjectTypes.Block)
			{
				m_Visible.block.AttachedEvent.Subscribe(OnAttachedToTech);
				m_Visible.block.DetachingEvent.Subscribe(OnDetachingFromTech);
			}
		}
		else
		{
			m_Rbody = GetComponent<Rigidbody>();
		}
		m_Transform = base.transform;
		if (m_CheckParentHierarchy)
		{
			TransformParentChangedCatcher.AddTransformParentChangedCatcher(base.gameObject).TransformParentChangedEvent.Subscribe(OnTransformParentHasChanged);
		}
	}

	private void OnSpawn()
	{
		m_ObjectIsAlive = true;
		bool flag = true;
		if (m_CheckParentHierarchy && FindEnabledWorldSpaceObjectInParents())
		{
			flag = false;
		}
		if (flag)
		{
			SetEnabled(enabled: true);
		}
	}

	private void OnRecycle()
	{
		SetEnabled(enabled: false);
		m_ObjectIsAlive = false;
	}
}
