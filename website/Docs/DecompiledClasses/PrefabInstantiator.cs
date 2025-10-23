using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
	public enum DestroyType
	{
		None,
		ThisComponent,
		ThisGameObject
	}

	public interface EditorPreInit
	{
		bool OnPreInitEditor();
	}

	[HideInInspector]
	public Transform[] m_Prefabs;

	public bool instantiateAsChildren;

	public DestroyType destroyAfterInstantiation;

	private bool m_Instantiated;

	public void Instantiate(bool fromEditor = false)
	{
		if (m_Instantiated)
		{
			return;
		}
		Transform[] prefabs = m_Prefabs;
		foreach (Transform transform in prefabs)
		{
			Transform transform2 = Object.Instantiate(transform);
			transform2.name = transform.name;
			if (instantiateAsChildren)
			{
				transform2.parent = base.transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
			}
		}
		if (!fromEditor)
		{
			switch (destroyAfterInstantiation)
			{
			case DestroyType.ThisComponent:
				Object.DestroyImmediate(this);
				break;
			case DestroyType.ThisGameObject:
				if (!instantiateAsChildren)
				{
					Object.DestroyImmediate(base.gameObject);
				}
				break;
			}
		}
		m_Instantiated = true;
	}

	private void Awake()
	{
		Instantiate();
	}

	private void PrePool()
	{
		Instantiate();
	}
}
